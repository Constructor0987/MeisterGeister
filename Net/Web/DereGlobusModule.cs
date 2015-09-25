using MeisterGeister.Daten;
using Nancy;
using System;
using System.Dynamic;

namespace MeisterGeister.Net.Web
{
    public class DereGlobusModule : NancyModule
    {
        public DereGlobusModule()
        {
            Get["/mgloc"] = _ => {
                return ReturnMeisterGeisterLocation();
            };
            Get["/setloc"] = _ =>
            {
                return SetMeisterGeisterLocationFromDereGlobus();
            };
            Get["/mg_icon.png"] = _ =>
            {
                return Response.FromStream(App.GetResourceStream(new Uri("/DSA MeisterGeister;component/Images/Logos/MG_Icon_mit_Zeiger.png", UriKind.Relative)).Stream,
                    "image/png");
            };
            Get["/dgloc"] = _ =>
            {
                return ReturnAndRememberDereGlobusLocation();
            };
        }

        static double dglon = 0;
	    static double dglat = 0;
        static System.Drawing.Bitmap img = new System.Drawing.Bitmap(1, 1);

        Response SetMeisterGeisterLocationFromDereGlobus()
	    {
	        //gespeicherte dg-location auf die heldengruppe anwenden.
	        Global.HeldenLat = dglat;
	        Global.HeldenLon = dglon;
	           
	        var stream = new System.IO.MemoryStream();
	        img.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            stream.Position = 0;
            return Response.FromStream(stream, "image/jpg");
	    }

        Response ReturnAndRememberDereGlobusLocation()
        {

            string lonStr = Request.Query["lookatTerrainLon"];
            string latStr = Request.Query["lookatTerrainLat"];

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
            return Response.AsText(kml, "application/vnd.google-earth.kml+xml");
        }

        Response ReturnMeisterGeisterLocation()
        {
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
            return Response.AsText(kml, "application/vnd.google-earth.kml+xml");
        }
    }
}
