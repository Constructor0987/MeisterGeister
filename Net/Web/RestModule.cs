using MeisterGeister.Daten;
using Nancy;
using System.Dynamic;

namespace MeisterGeister.Net.Web
{
    public class RestModule : NancyModule
    {
        public RestModule()
        {
            Get["/info"] = _ => {
                VersionInfo info = new VersionInfo();
                info.ApplicationVersion = App.GetVersionString(App.GetVersionProgramm());
                info.DatabaseVersion = DatabaseUpdate.DatenbankVersionAktuell;
                return Response.AsJson(info);
            };
        }

        struct VersionInfo
        {
            public string ApplicationVersion;
            public int DatabaseVersion;
        }
    }
}
