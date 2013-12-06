using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.Net.Web
{
    public class RequestProcessor
    {
        static HttpService service = null;
        public static void Start()
        {
            if (!System.Net.HttpListener.IsSupported)
                return;
            service = new HttpService(4);
            service.ProcessRequest += service_ProcessRequest;
            service.Start(50132);
        }

        public static void Stop()
        {
            if(service != null)
                service.Stop();
        }

        static void ReturnMeisterGeisterLocation(System.Net.HttpListenerContext context)
        {
            var response = context.Response;
            
            //Position der Heldengruppe zurückgeben
            double lon = Global.HeldenLon;
            double lat = Global.HeldenLat;

            // Construct a response. 
            string kml1 =
@"<?xml version=""1.0""  encoding=""UTF-8""?>
<kml xmlns=""http://www.opengis.net/kml/2.2"">
<Document>
    <Style id=""sn_mg_icon"">
		<IconStyle>
			<Icon>
				<href>http://localhost:50132/mg_icon.png</href>
			</Icon>
			<hotSpot x=""0"" y=""0"" xunits=""fraction"" yunits=""fraction"" />
		</IconStyle>
		<ListStyle>
		</ListStyle>
	</Style>
	<Style id=""sh_mg_icon"">
		<IconStyle>
			<scale>1.2</scale>
			<Icon>
				<href>http://localhost:50132/mg_icon.png</href>
			</Icon>
			<hotSpot x=""0"" y=""0"" xunits=""fraction"" yunits=""fraction"" />
		</IconStyle>
		<ListStyle>
		</ListStyle>
	</Style>
	<StyleMap id=""msn_mg_icon"">
		<Pair>
			<key>normal</key>
			<styleUrl>#sn_mg_icon</styleUrl>
		</Pair>
		<Pair>
			<key>highlight</key>
			<styleUrl>#sh_mg_icon</styleUrl>
		</Pair>
	</StyleMap>
	<Placemark>
		<name>Helden</name>
		<description>Die Position der Heldengruppe in MeisterGeister</description>
		<styleUrl>#msn_mg_icon</styleUrl> 
        <Point>";
            string kml2 =
@"        </Point>
    </Placemark>
</Document>
</kml>";
            string coords = "<coordinates>{0:0.######},{1:0.######}</coordinates>";
            coords = String.Format(System.Globalization.CultureInfo.InvariantCulture, coords, lon, lat);
            string kml = kml1 + coords + kml2;
            string responseString = kml;

            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
            // Get a response stream and write the response to it.
            response.ContentLength64 = buffer.Length;
            response.ContentType = "application/vnd.google-earth.kml+xml";
            System.IO.Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            // You must close the output stream.
            output.Close();
        }

        static double dglon = 0;
        static double dglat = 0;

        static void ReturnAndRememberDereGlobusLocation(System.Net.HttpListenerContext context)
        {
            string lonStr = context.Request.QueryString["lookatTerrainLon"];
            string latStr = context.Request.QueryString["lookatTerrainLat"];

            var response = context.Response;
            double lon = Global.HeldenLon;
            double lat = Global.HeldenLat;

            Double.TryParse(lonStr, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out lon);
            Double.TryParse(latStr, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out lat);

            //dg position merken
            dglon = lon;
            dglat = lat;


            // Construct a response. 
            string kml1 =
@"<?xml version=""1.0""  encoding=""UTF-8""?>
<kml xmlns=""http://www.opengis.net/kml/2.2"">
<Document>
   <Style id=""sn_cross-hairs"">
		<IconStyle>
			<color>b2ffffff</color>
			<scale>1.0</scale>
			<Icon>
				<href>http://maps.google.com/mapfiles/kml/shapes/cross-hairs.png</href>
			</Icon>
		</IconStyle>
		<LabelStyle>
			<scale>0</scale>
		</LabelStyle>
		<ListStyle>
		</ListStyle>
	</Style>
	<StyleMap id=""msn_cross-hairs"">
		<Pair>
			<key>normal</key>
			<styleUrl>#sn_cross-hairs</styleUrl>
		</Pair>
		<Pair>
			<key>highlight</key>
			<styleUrl>#sh_cross-hairs_highlight</styleUrl>
		</Pair>
	</StyleMap>
	<Style id=""sh_cross-hairs_highlight"">
		<IconStyle>
			<color>b2ffffff</color>
			<scale>1.0</scale>
			<Icon>
				<href>http://maps.google.com/mapfiles/kml/shapes/cross-hairs_highlight.png</href>
			</Icon>
		</IconStyle>
		<LabelStyle>
			<scale>0</scale>
		</LabelStyle>
		<ListStyle>
		</ListStyle>
	</Style>
 <Placemark>
  <name>Kameraposition</name>
  <styleUrl>#msn_cross-hairs</styleUrl>
  <description>Die Kameraposition wird bei Klick auf das Icon für die Heldengruppe übernommen.<![CDATA[<img src=""http://localhost:50132/setloc?x=d.jpg"">]]></description>
  <Point>";
            string kml2 =
            @"  </Point>
 </Placemark>
</Document>
</kml>";
            string coords = "<coordinates>{0:0.######},{1:0.######}</coordinates>";
            coords = String.Format(System.Globalization.CultureInfo.InvariantCulture, coords, lon, lat);
            string kml = kml1 + coords + kml2;
            string responseString = kml;

            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
            // Get a response stream and write the response to it.
            response.ContentLength64 = buffer.Length;
            response.ContentType = "application/vnd.google-earth.kml+xml";
            System.IO.Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            // You must close the output stream.
            output.Close();
        }

        static System.Drawing.Bitmap img = new System.Drawing.Bitmap(1, 1);

        static void SetMeisterGeisterLocationFromDereGlobus(System.Net.HttpListenerContext context)
        {
            //gespeicherte dg-location auf die heldengruppe anwenden.
            Global.HeldenLat = dglat;
            Global.HeldenLon = dglon;
            
            //müsste anhand der koordinaten aus einer datenbank bestimmt werden.
            Global.HeldenRegion = "";

            var response = context.Response;
            response.ContentType = "image/jpeg";

            var output = response.OutputStream;
            var stream = new System.IO.MemoryStream();
            img.Save(output, System.Drawing.Imaging.ImageFormat.Jpeg);
            // You must close the output stream.
            output.Close();
        }

        static void ReturnMgIcon(System.Net.HttpListenerContext context)
        {
            var response = context.Response;
            response.ContentType = "image/png";

            var output = response.OutputStream;
            var stream = new System.IO.MemoryStream();
            var mglogo = System.Drawing.Image.FromStream(App.GetResourceStream(new Uri("/DSA MeisterGeister;component/Images/Logos/MG_Icon_mit_Zeiger.png", UriKind.Relative)).Stream);
            mglogo.Save(output, System.Drawing.Imaging.ImageFormat.Png);
            // You must close the output stream.
            output.Close();
        }

        static void Return404(System.Net.HttpListenerContext context)
        {
            var response = context.Response;
            string responseString = "<BODY>404 File not found</BODY></HTML>";
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
            // Get a response stream and write the response to it.
            response.StatusCode = 404;
            response.ContentLength64 = buffer.Length;
            response.ContentType = "text/html";
            System.IO.Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            // You must close the output stream.
            output.Close();
        }

        static void service_ProcessRequest(System.Net.HttpListenerContext context)
        {
            //context.Request.Url.Query;
#if DEBUG
            System.Diagnostics.Debug.WriteLine(context.Request.Url.AbsolutePath);
#endif

            switch (context.Request.Url.AbsolutePath)
            {
                case "/mgloc":
                    ReturnMeisterGeisterLocation(context);
                    break;
                case "/setloc":
                    SetMeisterGeisterLocationFromDereGlobus(context);
                    break;
                case "/mg_icon.png":
                    ReturnMgIcon(context);
                    break;
                case "/dgloc":
                    ReturnAndRememberDereGlobusLocation(context);
                    break;
                default:
                    Return404(context);
                    break;
            }


            

            
        }
    }
}
