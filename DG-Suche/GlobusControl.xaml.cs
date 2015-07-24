using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.Xml;

namespace DgSuche
{
    /// <summary>
    /// Interaktionslogik für GlobusControl.xaml
    /// </summary>
    public partial class GlobusControl : UserControl
    {
        private bool? KmlLinks
        {
            get { return _checkBoxKmlLinks.IsChecked; }
        }

        private bool? InOff
        {
            get { return _checkBoxInOff.IsChecked; }
        }

        private bool? Off
        {
            get { return _checkBoxOff.IsChecked; }
        }

        public GlobusControl(bool meisterGeisterMode)
        {
            InitializeComponent();
            InitializeComponent2();

            if (meisterGeisterMode)
            {
                _listBoxOrtsmarken.BorderBrush = Brushes.Transparent;
                _listBoxOrtsmarken.SetResourceReference(ListBox.BackgroundProperty, "BackgroundPergamentQuer");
            }
        }

        public GlobusControl()
        {
            InitializeComponent();
            InitializeComponent2();
        }

        private void InitializeComponent2()
        {
#if !INTERN
            _checkBoxKmlLinks.Visibility = System.Windows.Visibility.Collapsed;
            _buttonParseDG.Visibility = System.Windows.Visibility.Collapsed;
#endif

            List<string> artList = new List<string>() { "Alle", "Metropole", "Großstadt", "Stadt", "Kleinstadt", "Dorf", 
                "Festung", "Sakralbauwerk", "Ruine", "Handelsstätte", "Werkstätte", "Privathaus", "Rakshazar" };
            _comboBoxArt.ItemsSource = artList;
            _comboBoxArt.SelectedIndex = 0;

            if (Ortsmarke.ListOrtsmarken.Count <= 0)
                LoadIndexFile();
            else
                Filtern();
        }

        private void _buttonParseDG_Click(object sender, RoutedEventArgs e)
        {
            ParseKML_Files();
        }

        private void LoadIndexFile()
        {
            //Mouse.OverrideCursor = Cursors.Wait;

            DateTime timeStart = DateTime.Now;

            Ortsmarke.ListOrtsmarken.Clear();

            WebClient w = new WebClient();
            w.Encoding = System.Text.Encoding.UTF8;
            try
            {
                Load_From_CSV(w);

                _textBoxFilter.Text = string.Empty;
                Filtern();

                DateTime timeEnde = DateTime.Now;
                TimeSpan dauer = timeEnde - timeStart;
                _textBlockDauer.Text = "Ladedauer: " + dauer.ToString(@"mm\:ss\.fff");
            }
            catch (Exception ex)
            {
                MsgWindow errWin = new MsgWindow("DereGlobus Daten laden", "Beim Laden der DereGlobus Daten ist ein Fehler aufgetreten!", ex);
                errWin.ShowDialog();
            }

            //Mouse.OverrideCursor = Cursors.Arrow;
        }

        private void Load_From_CSV(WebClient w)
        {
            string link = string.Empty;
            string s = string.Empty;

            if (System.IO.File.Exists("DereGlobusIndex.csv"))
            {
                try
                {
                    s = System.IO.File.ReadAllText("DereGlobusIndex.csv");
                }
                catch (Exception)
                {
                }

            }
            else
            {
                link = Properties.Settings.Default.DG_IndexPfad_CSV;

                try
                {
                    s = w.DownloadString(link);
                }
                catch (WebException)
                {
                    // hier kann man einen Download der Index-Datei von einer alternativen Webadresse versuchen
                    link = Properties.Settings.Default.DG_IndexPfad_CSV_Fallback;
                    try
                    {
                        s = w.DownloadString(link);
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }

            System.IO.StringReader reader = new System.IO.StringReader(s);
            // Erste Zeile ignorieren, da Header
            reader.ReadLine();
            string line = null;
            string[] attributes;
            while (true)
            {
                line = reader.ReadLine();
                if (line != null)
                {
                    attributes = line.Split(';');

                    Ortsmarke lm = new Ortsmarke();
                    if (attributes != null && attributes.Length >= 1)
                        lm.Name = attributes[0];
                    if (attributes != null && attributes.Length >= 2)
                        lm.Art = attributes[1];
                    if (attributes != null && attributes.Length >= 3)
                        lm.Longitude = attributes[2];
                    if (attributes != null && attributes.Length >= 4)
                        lm.Latitude = attributes[3];
                    if (attributes != null && attributes.Length >= 5)
                        lm.Link = attributes[4];
                    if (attributes != null && attributes.Length >= 6)
                        lm.KmlLink = attributes[5];

                    Ortsmarke.ListOrtsmarken.Add(lm);
                }
                else
                    break; // Ende der Datei erreicht
            }
        }

        private void ParseKML_Files()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            DateTime timeStart = DateTime.Now;

            Ortsmarke.ListOrtsmarken.Clear();

            WebClient w = new WebClient();
            w.Encoding = System.Text.Encoding.UTF8;
            //try
            {
                string s = w.DownloadString("http://www.dereglobus.orkenspalter.de/public/DereGlobus/Staedte/kml/Siedlungen/Siedlungen.kml");
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(s);
                string xmlns = string.Empty;
                string xmlns_gx = string.Empty;
                string xmlns_kml = string.Empty;
                string xmlns_atom = string.Empty;
                XmlNodeList linksSiedlungenFolder;
                XmlNamespaceManager nsmgr;
                if (doc.DocumentElement.Attributes["xmlns"] != null)
                {
                    nsmgr = new XmlNamespaceManager(doc.NameTable);

                    xmlns = doc.DocumentElement.Attributes["xmlns"].Value;
                    nsmgr.AddNamespace("x", xmlns);

                    if (doc.DocumentElement.Attributes["xmlns:gx"] != null)
                    {
                        xmlns_gx = doc.DocumentElement.Attributes["xmlns:gx"].Value;
                        nsmgr.AddNamespace("gx", xmlns_gx);
                    }
                    if (doc.DocumentElement.Attributes["xmlns:kml"] != null)
                    {
                        xmlns_kml = doc.DocumentElement.Attributes["xmlns:kml"].Value;
                        nsmgr.AddNamespace("kml", xmlns_kml);
                    }
                    if (doc.DocumentElement.Attributes["xmlns:atom"] != null)
                    {
                        xmlns_atom = doc.DocumentElement.Attributes["xmlns:atom"].Value;
                        nsmgr.AddNamespace("atom", xmlns_atom);
                    }

                    linksSiedlungenFolder = doc.SelectNodes("//x:Folder/x:NetworkLink/x:Link/x:href", nsmgr);
                }
                else
                {
                    linksSiedlungenFolder = doc.SelectNodes("//Folder/NetworkLink/Link/href");
                }

                string indexString = "Name;Art;Longitude;Latitude;Link" + Environment.NewLine;

                foreach (XmlNode nodeSiedlungenLink in linksSiedlungenFolder)
                {
                    string linkSiedlungen = nodeSiedlungenLink.InnerText;
                    s = w.DownloadString(linkSiedlungen);
                    doc = new XmlDocument();

                    // fehlerhafte Dokumente reparieren
                    if (!s.Contains("xmlns:gx=\"http://www.google.com/kml/ext/2.2\""))
                        s= s.Replace("<kml xmlns=\"http://earth.google.com/kml/2.2\">",
                            "<kml xmlns=\"http://earth.google.com/kml/2.2\" xmlns:gx=\"http://www.google.com/kml/ext/2.2\">");

                    doc.LoadXml(s);
                    XmlNodeList list;
                    if (doc.DocumentElement.Attributes["xmlns"] != null)
                    {
                        nsmgr = new XmlNamespaceManager(doc.NameTable);

                        xmlns = doc.DocumentElement.Attributes["xmlns"].Value;
                        nsmgr.AddNamespace("x", xmlns);

                        if (doc.DocumentElement.Attributes["xmlns:gx"] != null)
                        {
                            xmlns_gx = doc.DocumentElement.Attributes["xmlns:gx"].Value;
                            nsmgr.AddNamespace("gx", xmlns_gx);
                        }
                        if (doc.DocumentElement.Attributes["xmlns:kml"] != null)
                        {
                            xmlns_kml = doc.DocumentElement.Attributes["xmlns:kml"].Value;
                            nsmgr.AddNamespace("kml", xmlns_kml);
                        }
                        if (doc.DocumentElement.Attributes["xmlns:atom"] != null)
                        {
                            xmlns_atom = doc.DocumentElement.Attributes["xmlns:atom"].Value;
                            nsmgr.AddNamespace("atom", xmlns_atom);
                        }

                        list = doc.SelectNodes("//x:Placemark", nsmgr);
                    }
                    else
                    {
                        list = doc.SelectNodes("//Placemark");
                    }
                    foreach (XmlNode node in list)
                    {
                        string point = string.Empty;
                        if (node["Point"] != null)
                            point = node["Point"].InnerText;
                        string[] coordinates = point.Split(',');

                        // Link
                        string link = string.Empty;
                        XmlNode linkNode;
                        if (node["ExtendedData"] != null && (linkNode = node["ExtendedData"]["Data"]) != null)
                        {
                            XmlAttribute at = linkNode.Attributes["name"];
                            if (at != null && at.Value == "Link" && linkNode["value"] != null)
                            {
                                if (linkNode["value"].InnerText != "$[name]")
                                    link = linkNode["value"].InnerText;
                            }
                        }

                        Ortsmarke lm = new Ortsmarke()
                        {
                            Name = node["name"].InnerText.Trim(),
                            Longitude = (coordinates.Length >= 1 ? coordinates[0].Trim() : string.Empty),
                            Latitude = (coordinates.Length >= 2 ? coordinates[1].Trim() : string.Empty),
                            KmlLink = linkSiedlungen,
                            Art = node["styleUrl"].InnerText.Split('#')[1].Trim(),
                            Link = link
                        };

                        Ortsmarke.ListOrtsmarken.Add(lm);

                        indexString += lm.ToCSV((bool)KmlLinks) + Environment.NewLine;
                    }
                }
                _listBoxOrtsmarken.BeginInit();
                _listBoxOrtsmarken.ItemsSource = Ortsmarke.ListOrtsmarken;
                _textBoxFilter.Text = string.Empty;
                _listBoxOrtsmarken.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("Name", System.ComponentModel.ListSortDirection.Ascending));
                textBlock3.Text = Ortsmarke.ListOrtsmarken.Count + " Ortsmarken";
                _listBoxOrtsmarken.EndInit();

                DateTime timeEnde = DateTime.Now;
                TimeSpan dauer = timeEnde - timeStart;
                _textBlockDauer.Text = "Ladedauer: " + dauer.ToString(@"mm\:ss\.fff");

                System.IO.TextWriter csvWriter = System.IO.File.CreateText("Index.csv");
                csvWriter.Write(indexString);
                csvWriter.Close();

                MessageBox.Show("Die Ortsmarken-Daten wurden vom DereGlobus extrahiert!\n\nDie Index-Datei wurde im Programm-Verzeichnis gespeichert.");
            }
            //catch (Exception ex)
            //{
            //    MsgWindow errWin = new MsgWindow("DereGlobus Daten laden", "Beim Laden der DereGlobus Daten ist ein Fehler aufgetreten!", ex);
            //    errWin.ShowDialog();
            //}

            Mouse.OverrideCursor = Cursors.Arrow;
        }

        private void _textBoxFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filtern();
        }

        private void Filtern()
        {
            string art = _comboBoxArt.SelectedValue.ToString();
            switch (art)
            {
                case "Großstadt":
                    art = "Grossstadt";
                    break;
                case "Sakralbauwerk":
                    art = "Sonstige_Sakralbauwerk";
                    break;
                case "Ruine":
                    art = "Sonstige_Ruine";
                    break;
                case "Handelsstätte":
                    art = "Sonstige_Handelsstaette";
                    break;
                case "Werkstätte":
                    art = "Sonstige_Werkstaette";
                    break;
                case "Privathaus":
                    art = "Sonstige_Privathaus";
                    break;
                default:
                    break;
            }
            string txtOrig = _textBoxFilter.Text;
            string upper = txtOrig.ToUpper();
            string lower = txtOrig.ToLower();

            _listBoxOrtsmarken.BeginInit();

            var orteFiltered = from ort in Ortsmarke.ListOrtsmarken
                               let ortName = ort.Name
                               where
                                 ortName.ToLower().Contains(lower)
                                 && (art == "Alle" ? true       // Art
                                    : ort.Art.Contains(art))
                                 && (InOff == true ? true       // Inoffiziell
                                    : !(ort.Name.ToLower().Contains("inoff") || ort.Art.Contains("Rakshazar")))
                                 && (Off == true ? true         // Offiziell
                                    : (ort.Name.ToLower().Contains("inoff") || ort.Art.Contains("Rakshazar")))
                               select ort;
            _listBoxOrtsmarken.ItemsSource = orteFiltered;

            _listBoxOrtsmarken.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("Name", System.ComponentModel.ListSortDirection.Ascending));
            if (_listBoxOrtsmarken.Items != null)
                textBlock3.Text = _listBoxOrtsmarken.Items.Count + " Ortsmarke" + (_listBoxOrtsmarken.Items.Count != 1 ? "n" : string.Empty);
            else
                textBlock3.Text = "0 Ortsmarken";
            _listBoxOrtsmarken.EndInit();
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(e.Uri.AbsoluteUri));

            e.Handled = true;
        }

        private void _comboBoxArt_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filtern();
        }

        public object SelectedItem
        {
            get { return _listBoxOrtsmarken.SelectedItem; }
        }

        public ListBox ListBoxOrtsmarken
        {
            get { return _listBoxOrtsmarken; }
        }

        private void _buttonadenDG_Click(object sender, RoutedEventArgs e)
        {
            LoadIndexFile();
        }

        private void CheckBoxInOff_UnChecked(object sender, RoutedEventArgs e)
        {
            if (IsInitialized && IsLoaded)
                Filtern();
        }

        private void CheckBoxOff_UnChecked(object sender, RoutedEventArgs e)
        {
            if (IsInitialized && IsLoaded)
                Filtern();
        }
    }
}
