using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace DgSuche
{
    public class Ortsmarke
    {
        public Ortsmarke()
        {
            Name = string.Empty;
            Latitude = string.Empty;
            Longitude = string.Empty;
            Link = string.Empty;
        }

        public Ortsmarke(string name)
        {
            SetFromList(name);
        }

        public Ortsmarke(string name, bool point)
        {
            if (_listOrtsmarken != null && _listOrtsmarken.Count > 0)
            {
                SetFromList(name);
            }
            else
            {
                string[] s = name.Split('#');
                Name = s[0];
                Latitude = s[1];
                Longitude = s[2];
            }
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

        public string Name { get; set; }
        public string Longitude { get; set; } // Länge
        public string Latitude { get; set; } // Breite
        public string Art { get; set; }
        public string KmlLink { get; set; }
        public string Link { get; set; }

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
                return string.Format("{0}\n\nLänge (Longitude): {1}\nBreite (Latitude): {2}", Name, Longitude, Latitude);
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
                System.Reflection.Assembly assem = System.Reflection.Assembly.GetExecutingAssembly();
                System.Reflection.AssemblyName assemName = assem.GetName();
                return string.Format("/{0};component/Grafik/Globus/{1}.png", assemName.Name, ArtKurz);
            }
        }

        public override string ToString()
        {
            return string.Format("{0}", Name);
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

        public static void StarteDereGlobus(string name, string lon, string lat)
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
                kmlDatei += string.Format("<longitude>{0}</longitude> <!-- kml:angle180 -->", lon);
                kmlDatei += string.Format("<latitude>{0}</latitude> <!-- kml:angle90 -->", lat);
                kmlDatei += "<altitude>300000</altitude> <!-- double -->";
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
            string lat = "-";
            string lon = "-";
            if (Latitude != null && Longitude != null)
            {
                int dotIndex = Latitude.IndexOf('.');
                if (dotIndex + 4 < Latitude.Length)
                    lat = Latitude.Remove(dotIndex + 5);
                else
                    lat = Latitude;
                dotIndex = Longitude.IndexOf('.');
                if (dotIndex + 4 < Longitude.Length)
                    lon = Longitude.Remove(dotIndex + 5);
                else
                    lat = Latitude;
            }
            return lat + " | " + lon;
        }

        public object ToStringKoordinaten()
        {
            return string.Format("{0} ({1})", Name, KoordinatenShort());
        }
    }
}
