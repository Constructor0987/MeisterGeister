using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using System.Globalization;
using System.Windows;

namespace DgSuche
{
    public class Ortsmarke : NotifyChangedBase
    {
        /// <summary>
        /// Erstellt eine neue Ortsmarke ohne Werte.
        /// </summary>
        public Ortsmarke()
        {
            Name = string.Empty;
            Latitude = 0;
            Longitude = 0;
            Link = string.Empty;
        }

        /// <summary>
        /// Setzt die Werte der Ortsmarke anhand der Informationen aus der statischen Liste ListOrtsmarken.
        /// Ist der Wert nicht enthalten, wird nichts gesetzt.
        /// 
        /// Ist keine Liste vorhanden, wird versucht die Koordinaten dem Namen zu entnehmen.
        /// Dieser muss dafür das Format Name#Latitude#Longitude aufweisen.
        /// </summary>
        /// <param name="name">Name der Ortsmarke aus der ListOrtsmarken</param>
        public Ortsmarke(string name)
        {
            if (_listOrtsmarken != null && _listOrtsmarken.Count > 0)
            {
                SetFromList(name);
            }
            else
            {
                string[] s = name.Split('#');
                Name = s[0];
                double lat = 0, lon = 0;
                if (TryParseDouble(s[1], out lat))
                    Latitude = lat;
                if (TryParseDouble(s[2], out lon))
                    Longitude = lon;
            }
        }

        /// <summary>
        /// Erstelle eine neue Ortsmarke.
        /// </summary>
        /// <param name="name">Name der Ortsmarke</param>
        /// <param name="latitude">Breite</param>
        /// <param name="longitude">Länge</param>
        public Ortsmarke(string name, double latitude, double longitude)
        {
            Name = name;
            Latitude = latitude;
            Longitude = longitude;
        }

        /// <summary>
        /// Erstelle eine neue Ortsmarke.
        /// </summary>
        /// <param name="name">Name der Ortsmarke</param>
        /// <param name="latitude">Breite</param>
        /// <param name="longitude">Länge</param>
        public Ortsmarke(string name, string latitude, string longitude)
        {
            Name = name;
            SetKoordinatenFromString(latitude, longitude);
        }

        /// <summary>
        /// Setzt die Koordinaten anhand einer Stringrepräsentation einer Gleitkommazahl.
        /// Kann ein Wert nicht geparsed werden, dann wird dieser nicht gesetzt und es wird false zurückgegeben.
        /// </summary>
        /// <param name="latitude">Breite</param>
        /// <param name="longitude">Länge</param>
        public bool SetKoordinatenFromString(string latitude, string longitude)
        {
            double d = 0;
            bool ret = true;
            if (latitude != null)
                if (TryParseDouble(latitude, out d))
                    Latitude = d;
                else
                    ret = false;
            if (longitude != null)
                if (TryParseDouble(longitude, out d))
                    Longitude = d;
                else
                    ret = false;
            return ret;
        }

        /// <summary>
        /// Erstellt eine neue Ortsmarke.
        /// </summary>
        /// <param name="name">Name des Ortes</param>
        /// <param name="point">Koordinaten</param>
        public Ortsmarke(string name, Point point)
        {
            Name = name;
            Latitude = point.X;
            Longitude = point.Y;
        }

        private static bool TryParseDouble(string s, out double d)
        {
            d = 0;
            if(s == null)
                return false;
            return Double.TryParse(s.Trim().Replace(',', '.'), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out d);
        }


        private void SetFromList(string name)
        {
            // In Liste suchen
            var orteFiltered = from ort in _listOrtsmarken
                               let ortName = ort.Name
                               where
                                 ortName == name
                               select ort;
            foreach (var item in orteFiltered)
            {
                Name = item.Name;
                Art = item.Art;
                Latitude = item.Latitude;
                Longitude = item.Longitude;
                KmlLink = item.KmlLink;
                break;
            }
        }

        private static System.Collections.ObjectModel.ObservableCollection<Ortsmarke> _listOrtsmarken =
            new System.Collections.ObjectModel.ObservableCollection<Ortsmarke>();

        public static System.Collections.ObjectModel.ObservableCollection<Ortsmarke> ListOrtsmarken
        {
            get { return _listOrtsmarken; }
            set { _listOrtsmarken = value; }
        }

        public System.Windows.Visibility DerischeSphaeren_Visibility
        {
            get
            {
                if (Art.Contains("Rakshazar"))
                    return System.Windows.Visibility.Visible;
                return System.Windows.Visibility.Hidden;
            }
        }

        string name;
        public string Name
        {
            get { return name; }
            set { 
                Set(ref name,value);
                OnChanged("LinkText");
            }
        }
        
        double longitude;
        /// <summary>
        /// Länge
        /// </summary>
        public double Longitude
        {
            get { return longitude; }
            set { 
                Set(ref longitude, value);
                OnChanged("Koordinaten");
            }
        }

        
        double latitude;
        /// <summary>
        /// Breite
        /// </summary>
        public double Latitude
        {
            get { return latitude; }
            set { 
                Set(ref latitude, value);
                OnChanged("Koordinaten");
            }
        }

        /// <summary>
        /// Die Korrdinaten als Point(Latitude/Breite, Longitude/Länge)
        /// </summary>
        public Point Koordinaten
        {
            get { return new Point(Latitude, Longitude); }
        }

        static List<string> artList = new List<string>() { "Alle", "Metropole", "Großstadt", "Stadt", "Kleinstadt", "Dorf", 
                "Festung", "Sakralbauwerk", "Ruine", "Handelsstätte", "Werkstätte", "Privathaus", "Rakshazar" };

        /// <summary>
        /// Liste der Arten
        /// </summary>
        public static List<string> ArtListe
        {
            get { return artList; }
        }

        string art;
        /// <summary>
        /// Art aus der ArtListe
        /// </summary>
        public string Art
        {
            get { return art; }
            set { Set(ref art, value); }
        }

        string kmlLink;
        public string KmlLink
        {
            get { return kmlLink; }
            set { Set(ref kmlLink, value); }
        }

        string link;
        public string Link
        {
            get { return link; }
            set {
                Set(ref link, value);
                OnChanged("LinkText");
            }
        }

        public string LinkText
        {
            get
            {
                if (Link == null || Link == string.Empty)
                    return Name;
                else
                    return Link;
            }
        }

        public string ToolTipText
        {
            get
            {
                return string.Format("{0}\n\nLänge (Longitude): {1:0.0#####}\nBreite (Latitude): {2:0.0#####}", Name, Longitude, Latitude);
            }
        }

        public string ArtKurz
        {
            get
            {
                if (Art != null)
                    return Art.Trim().Replace("-X", "");
                return string.Empty;
            }
        }

        public string Bild
        {
            get
            {
                return string.Format("pack://application:,,,/Grafik/Globus/{0}.png", ArtKurz);
            }
        }

        public override string ToString()
        {
            return Name??"";
        }

        public string ToCSV(bool mitLink = false)
        {
            if (mitLink)
                return string.Format("{0};{1};{2};{3};{4};{5}", Name, Art, Longitude, Latitude, KmlLink, Link);
            else
                return string.Format("{0};{1};{2};{3};{4}", Name, Art, Longitude, Latitude, Link);
        }

        public string XML_Escape_Attribute(string text)
        {
            return text.Replace("&", "&amp;")
                .Replace(">", "&gt;")
                .Replace("<", "&lt;")
                .Replace("\"", "&quot;")
                .Replace("'", "&apos;");
        }

        public void BetrachteInDereGlobus()
        {
            StarteDereGlobus(Name, Longitude, Latitude);
        }

        public static void StarteDereGlobus(string name, string lon, string lat, double altitude = 300000)
        {
            double dlon = 0, dlat = 0;
            TryParseDouble(lat, out dlat);
            TryParseDouble(lon, out dlon);
            StarteDereGlobus(name, dlon, dlat, altitude);
        }

        public static void StarteDereGlobus(string name, Point point, double altitude = 300000)
        {
            StarteDereGlobus(name, point.Y, point.X, altitude);
        }

        public static void StarteDereGlobus(string name, double lon, double lat, double altitude = 300000)
        {
            try
            {
                string kmlDatei = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
                kmlDatei += "<kml xmlns=\"http://www.opengis.net/kml/2.2\" xmlns:gx=\"http://www.google.com/kml/ext/2.2\" xmlns:kml=\"http://www.opengis.net/kml/2.2\" xmlns:atom=\"http://www.w3.org/2005/Atom\">";
                kmlDatei += "<Placemark>";
                kmlDatei += "<LookAt>";
                kmlDatei += "<gx:TimeStamp>";
                kmlDatei += "<when></when>";
                kmlDatei += "</gx:TimeStamp>";
                kmlDatei += String.Format(CultureInfo.InvariantCulture, "<longitude>{0}</longitude> <!-- kml:angle180 -->", lon);
                kmlDatei += String.Format(CultureInfo.InvariantCulture, "<latitude>{0}</latitude> <!-- kml:angle90 -->", lat);
                kmlDatei += String.Format(CultureInfo.InvariantCulture, "<altitude>{0}</altitude> <!-- double -->", altitude);
                kmlDatei += "<heading></heading> <!-- kml:angle360 -->";
                kmlDatei += "<tilt></tilt> <!-- kml:anglepos180 -->";
                kmlDatei += "<range></range> <!-- double -->";
                kmlDatei += "<altitudeMode>relativeToGround</altitudeMode> <!-- kml:altitudeModeEnum:clampToGround, relativeToGround, absolute -->";
                kmlDatei += "</LookAt>";
                kmlDatei += "</Placemark>";
                kmlDatei += "</kml>";

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(kmlDatei);

                System.Reflection.Assembly assem = System.Reflection.Assembly.GetExecutingAssembly();
                System.Reflection.AssemblyName assemName = assem.GetName();

                string path = System.IO.Path.GetTempPath() + assemName.Name + "\\" + name.Trim('#', '/', '"') + ".kml";
                if (!Directory.Exists(System.IO.Path.GetTempPath() + assemName.Name + "\\"))
                    Directory.CreateDirectory(System.IO.Path.GetTempPath() + assemName.Name + "\\");
                doc.Save(path);

                System.Diagnostics.Process.Start(path);
            }
            catch (Exception ex)
            {
                MsgWindow errWin = new MsgWindow("Betrachten in DereGlobus", "Beim Betrachten in DereGlobus ist ein Fehler aufgetreten!", ex);
                errWin.ShowDialog();
            }
        }

        public string KoordinatenShort()
        {
            return String.Format("{0:0.0###} | {1:0.0###}", Latitude, Longitude);
        }

        public object ToStringKoordinaten()
        {
            return string.Format("{0} ({1})", Name, KoordinatenShort());
        }

        
    }
}
