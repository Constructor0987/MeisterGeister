#if NET10_WEB_TESTS
using System;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using MeisterGeister.Net;
using MeisterGeister.Net.Web;
using Newtonsoft.Json.Linq;
using Xunit;

[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace MeisterGeister
{
    public class App : System.Windows.Application
    {
        public static string GetVersionString(Version version) { return version.ToString(); }
        public static Version GetVersionProgramm() { return new Version(2, 6, 7, 0); }
    }

    public static class Global
    {
        public static double HeldenLon { get; set; }
        public static double HeldenLat { get; set; }
    }
}

namespace MeisterGeister.Daten
{
    public static class DatabaseUpdate
    {
        public const int DatenbankVersionAktuell = 133;
    }
}

namespace MeisterGeister.Logic.General
{
    public static class Logger
    {
        public static void LogMsgToFile(string message) { }
    }
}

namespace MeisterGeister.Web.Tests
{
    public class WebServerTests
    {
        private static string Read(Nancy.Response response)
        {
            using (response.Body)
            using (var reader = new StreamReader(response.Body, Encoding.UTF8))
                return reader.ReadToEnd();
        }

        [Fact]
        public void InfoReturnsApplicationAndDatabaseVersions()
        {
            var response = new RestModule().Dispatch("/info", new NameValueCollection());
            Assert.Equal("application/json", response.ContentType);
            var json = JObject.Parse(Read(response));
            Assert.Equal("2.6.7.0", (string)json["ApplicationVersion"]);
            Assert.Equal(133, (int)json["DatabaseVersion"]);
        }

        [Fact]
        public void DereGlobusCoordinatesAreRememberedAndApplied()
        {
            var module = new DereGlobusModule();
            var query = new NameValueCollection
            {
                ["lookatTerrainLon"] = "12.3456789",
                ["lookatTerrainLat"] = "-5.1234567"
            };
            var location = module.Dispatch("/dgloc", query);
            Assert.Equal("application/vnd.google-earth.kml+xml", location.ContentType);
            XNamespace kml = "http://www.opengis.net/kml/2.2";
            Assert.Equal("12.345679,-5.123457", XDocument.Parse(Read(location)).Descendants(kml + "coordinates").Single().Value);

            var response = module.Dispatch("/setloc", new NameValueCollection());
            Assert.Equal("image/jpg", response.ContentType);
            using (response.Body)
            {
                Assert.Equal(0xff, response.Body.ReadByte());
                Assert.Equal(0xd8, response.Body.ReadByte());
            }
            Assert.Equal(12.3456789, Global.HeldenLon);
            Assert.Equal(-5.1234567, Global.HeldenLat);

            var heroLocation = module.Dispatch("/mgloc", new NameValueCollection());
            Assert.Equal("12.345679,-5.123457", XDocument.Parse(Read(heroLocation)).Descendants(kml + "coordinates").Single().Value);
        }

        [Fact]
        public void UnknownRouteIsNotHandled()
        {
            Assert.Null(new RestModule().Dispatch("/unknown", new NameValueCollection()));
            Assert.Null(new DereGlobusModule().Dispatch("/unknown", new NameValueCollection()));
        }

        [Fact]
        public async Task HttpServerServesRoutesAndCanRestart()
        {
            var reservation = new TcpListener(IPAddress.Loopback, 0);
            reservation.Start();
            int port = ((IPEndPoint)reservation.LocalEndpoint).Port;
            reservation.Stop();

            var server = new Server("localhost");
            try
            {
                server.Start(port);
                Assert.True(SpinWait.SpinUntil(() => server.Status == Server.States.Started, TimeSpan.FromSeconds(10)));
                using (var client = new HttpClient { BaseAddress = new Uri($"http://localhost:{port}/") })
                {
                    var info = await client.GetAsync("info");
                    Assert.Equal(HttpStatusCode.OK, info.StatusCode);
                    Assert.Equal(133, (int)JObject.Parse(await info.Content.ReadAsStringAsync())["DatabaseVersion"]);
                    Assert.Equal(HttpStatusCode.NotFound, (await client.GetAsync("unknown")).StatusCode);
                    Assert.Equal(HttpStatusCode.MethodNotAllowed, (await client.PostAsync("info", new StringContent(""))).StatusCode);
                }
                server.Stop();
                Assert.Equal(Server.States.Stopped, server.Status);
                server.Start(port);
                Assert.True(SpinWait.SpinUntil(() => server.Status == Server.States.Started, TimeSpan.FromSeconds(10)));
            }
            finally
            {
                server.Stop();
            }
            Assert.Equal(Server.States.Stopped, server.Status);
        }
    }
}
#endif
