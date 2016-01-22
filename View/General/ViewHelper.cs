using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using MeisterGeister.Logic.General;
using MeisterGeister.View.Kampf;
using MeisterGeister.View.Windows;
using System.Text.RegularExpressions;
using System.Windows.Markup;

namespace MeisterGeister.View.General
{
    /// <summary>
    /// Stellt einige statische Methoden bereit, die jedes View gebrauchen kann.
    /// </summary>
    public static class ViewHelper
    {
        public static void Popup(string msg)
        {
            System.Windows.MessageBox.Show(msg);
        }

        public static void ShowError(string msg, Exception ex)
        {
            MsgWindow errWin = new MsgWindow("Fehler", msg, ex);
            errWin.ShowDialog();
        }

        public static bool Confirm(string caption, string msg)
        {
            return (System.Windows.MessageBox.Show(msg, caption, MessageBoxButton.YesNo) == MessageBoxResult.Yes);
        }

        /// <summary>
        /// Zeigt ein Dialog-Fenster mit Ja/Nein/Abbruch an.
        /// </summary>
        /// <param name="caption"></param>
        /// <param name="msg"></param>
        /// <returns>2 = Ja
        /// 1 = Nein
        /// 0 = Abbruch
        /// </returns>
        public static int ConfirmYesNoCancel(string caption, string msg)
        {
            MessageBoxResult res = System.Windows.MessageBox.Show(msg, caption, MessageBoxButton.YesNoCancel);
            if (res == MessageBoxResult.Yes || res == MessageBoxResult.OK)
                return 2;
            if (res == MessageBoxResult.No)
                return 1;
            return 0;
        }

        public static string InputDialog(string caption, string msg, string init)
        {
            string input = null;
            InputWindow inBox = new InputWindow();
            inBox.Owner = App.Current.MainWindow; // MainWindow als Owner setzen
            inBox.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            inBox.Title = caption;
            inBox.Beschreibung = msg;
            inBox.Wert = init;
            inBox.ShowDialog();
            if (inBox.OK_Click)
                input = inBox.Wert;
            return input;
        }

        public static int? InputIntDialog(string caption, string msg, int init, int min = int.MinValue, int max = int.MaxValue)
        {
            int? input = null;
            InputIntWindow inBox = new InputIntWindow();
            inBox.Owner = App.Current.MainWindow; // MainWindow als Owner setzen
            inBox.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            inBox.Title = caption;
            inBox.Beschreibung = msg;
            inBox.Wert = init;
            inBox.WertMin = min;
            inBox.WertMax = max;
            inBox.ShowDialog();
            if (inBox.OK_Click)
                input = inBox.Wert;
            return input;
        }

        public static ViewModel.Helden.VorNachteilAuswahlItem VorNachteilAuswahlDialog(Model.VorNachteil vn)
        {
            ViewModel.Helden.VorNachteilAuswahlItem auswahl = null;
            Helden.VorNachteilAuswahlView dlg = new Helden.VorNachteilAuswahlView(vn);
            dlg.Owner = App.Current.MainWindow; // MainWindow als Owner setzen
            dlg.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            dlg.ShowDialog();
            if (dlg.OK_Click)
                auswahl = dlg.Auswahl;
            return auswahl;
        }

        private static Regex invalidChars = null;
        public static string GetValidFilename(string path)
        {
            if (invalidChars == null)
            {
                string regexSearch = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
                invalidChars = new Regex(string.Format("[{0}]", Regex.Escape(regexSearch)));
            }
            return invalidChars.Replace(path, "_");
        }

        /// <summary>
        /// Auswahl eines Verzeichnisses.
        /// </summary>
        /// <param name="path">Vorausgewähltes Verzeichnis.</param>
        /// <param name="askRelativePath">true, falls der User gefragt werden soll, ob der Pfad relativ oder absolut angegeben werden soll</param>
        /// <returns></returns>
        public static string ChooseDirectory(string path, bool askRelativePath)
        {
            FolderBrowserDialog objDialog = new FolderBrowserDialog();
            objDialog.SelectedPath = string.IsNullOrWhiteSpace(path) ? Environment.CurrentDirectory : path;

            if (objDialog.ShowDialog() == DialogResult.OK)
            {
                Environment.CurrentDirectory = objDialog.SelectedPath;
                string pathAbsolute = objDialog.SelectedPath;
                if (askRelativePath && IsSameRootPath(pathAbsolute))
                {
                    string pathRelative = Logic.Extensions.FileExtensions.ConvertAbsoluteToRelativePath(objDialog.SelectedPath);

                    if (ViewHelper.ConfirmYesNoCancel("Pfadangabe", string.Format("Absolute Pfadangabe (Ja)?\n{0}\n\nOder relative Pfadangabe (Nein)?\n{1}", pathAbsolute, pathRelative)) == 1)
                        return pathRelative;
                    else
                        return pathAbsolute;
                }
                return pathAbsolute;
            }
            return null;
        }

        /// <summary>
        /// Zur Auswahl einer Datei.
        /// </summary>
        /// <param name="title">Fenstertitel</param>
        /// <param name="filename">vorbesetzter Dateiname</param>
        /// <param name="saveFile">true für einen SaveDialog, false für einen OpenDialog</param>
        /// <param name="extensions">erlaubte Dateierweiterungen</param>
        /// <returns>Den ausgewählten Dateipfad oder null</returns>
        public static string ChooseFile(string title, string filename, bool saveFile, params string[] extensions)
        {
            return ChooseFile(title, filename, saveFile, false, false, extensions);
        }

        /// <summary>
        /// Zur Auswahl einer Datei.
        /// </summary>
        /// <param name="title">Fenstertitel</param>
        /// <param name="filename">vorbesetzter Dateiname</param>
        /// <param name="saveFile">true für einen SaveDialog, false für einen OpenDialog</param>
        /// <param name="askRelativePath">true, falls der User gefragt werden soll, ob der Pfad relativ oder absolut angegeben werden soll</param>
        /// <param name="extensions">erlaubte Dateierweiterungen</param>
        /// <returns>Den ausgewählten Dateipfad oder null</returns>
        public static string ChooseFile(string title, string filename, bool saveFile, bool askRelativePath, params string[] extensions)
        {
            return ChooseFile(title, filename, saveFile, false, askRelativePath, extensions);
        }

        /// <summary>
        /// Zur Auswahl einer Datei.
        /// </summary>
        /// <param name="title">Fenstertitel</param>
        /// <param name="filename">vorbesetzter Dateiname</param>
        /// <param name="saveFile">true für einen SaveDialog, false für einen OpenDialog</param>
        /// <param name="checkFileExists">Soll eine Warnung angezeigt werden, wenn der Benutzer eine Datei auswählt, die nicht existiert?</param>
        /// <param name="askRelativePath">true, falls der User gefragt werden soll, ob der Pfad relativ oder absolut angegeben werden soll</param>
        /// <param name="extensions">erlaubte Dateierweiterungen</param>
        /// <returns>Den ausgewählten Dateipfad oder null</returns>
        public static string ChooseFile(string title, string filename, bool saveFile, bool checkFileExists, bool askRelativePath, params string[] extensions)
        {
            filename = GetValidFilename(filename);
            FileDialog objDialog;
            if (saveFile)
                objDialog = new SaveFileDialog();
            else
                objDialog = new OpenFileDialog();
            objDialog.Title = title;
            objDialog.DefaultExt = String.Empty;
            objDialog.CheckFileExists = checkFileExists;
            objDialog.Filter = GetExtensionsFilter(extensions);
            
            objDialog.AddExtension = true;
            objDialog.FileName = filename;
            objDialog.InitialDirectory = Environment.CurrentDirectory;
            if (objDialog.ShowDialog() == DialogResult.OK)
            {
                Environment.CurrentDirectory = Path.GetDirectoryName(objDialog.FileName);
                string fileAbsolute = objDialog.FileName;
                if (askRelativePath && IsSameRootPath(fileAbsolute))
                {
                    string fileRelative = Logic.Extensions.FileExtensions.ConvertAbsoluteToRelativePath(objDialog.FileName);

                    if (ViewHelper.ConfirmYesNoCancel("Pfadangabe", string.Format("Absolute Pfadangabe (Ja)?\n{0}\n\nOder relative Pfadangabe (Nein)?\n{1}", fileAbsolute, fileRelative)) == 1)
                        return fileRelative;
                    else
                        return fileAbsolute;
                }
                return fileAbsolute;
            }
            return null;
        }

        /// <summary>
        /// Zur Auswahl mehrerer Dateien.
        /// </summary>
        /// <param name="title">Fenstertitel</param>
        /// <param name="filename">vorbesetzter Dateiname</param>
        /// <param name="saveFile">true für einen SaveDialog, false für einen OpenDialog</param>
        /// <param name="checkFileExists">Soll eine Warnung angezeigt werden, wenn der Benutzer eine Datei auswählt, die nicht existiert?</param>
        /// <param name="extensions">erlaubte Dateierweiterungen</param>
        /// <returns>Den ausgewählten Dateipfad oder null</returns>
        public static List<string> ChooseFiles(string title, string filename, bool checkFileExists, params string[] extensions)
        {
            filename = GetValidFilename(filename);
            OpenFileDialog objDialog = new OpenFileDialog();
            objDialog.Title = title;
            objDialog.DefaultExt = String.Empty;
            objDialog.CheckFileExists = checkFileExists;
            objDialog.Filter = GetExtensionsFilter(extensions);
            
            objDialog.AddExtension = true;
            objDialog.FileName = filename;
            objDialog.Multiselect = true;
            objDialog.InitialDirectory = Environment.CurrentDirectory;
            if (objDialog.ShowDialog() == DialogResult.OK)
            {
                Environment.CurrentDirectory = Path.GetDirectoryName(objDialog.FileName);
                return new List<string>(objDialog.FileNames);
            }
            return null;
        }

        private static string GetExtensionsFilter(string[] extensions)
        {
            //TODO: mehr extension-Typen?
            Dictionary<string, string> dFilter = new Dictionary<string, string>();
            foreach (string extension in extensions)
            {
                string sname = "";
                string sext = String.Format("*.{0}", extension);
                switch (extension)
                {
                    case "mp3":
                    case "wav":
                    case "ogg":
                    case "wma":
                        sname = "Audio-Dateien";
                        break;
                    case "m3u8":
                        sname = "Winamp-Playlist";
                        break;
                    case "wpl":
                        sname = "Windows MediaPlayer-Wiedergabeliste";
                        break;
                    case "bmp":
                    case "gif":
                    case "jpg":
                    case "jpeg":
                    case "jpe":
                    case "jtif":
                    case "png":
                    case "tif":
                    case "tiff":
                        sname = "Bild-Dateien";
                        break;
                    case "xml":
                        sname = "XML-Dateien";
                        break;
                    case "xls":
                    case "xlsx":
                    case "xlsb":
                        sname = "Excel-Dateien";
                        break;
                    case "*":
                    case "*.*":
                        sname = "Alle Dateien";
                        break;
                    default:
                        sname = String.Format("{0}-Dateien", extension);
                        break;
                }
                if (!dFilter.ContainsKey(sname))
                    dFilter.Add(sname, sext);
                else
                    dFilter[sname] = dFilter[sname] + ";" + sext;
            }
            string filter = string.Empty;
            string allValid = string.Empty;
            foreach (string sname in dFilter.Keys)
            {
                allValid += (filter != String.Empty) ? ";" + dFilter[sname] : dFilter[sname];
                filter += (filter != String.Empty) ? "|" : "";
                filter += sname + "|" + dFilter[sname];
            }

            if (filter == String.Empty)
                filter = "Alle Dateien|*.*";
            else
                filter = "Alle erlaubten Dateien|" + allValid + "|" + filter;
            return filter;
        }

        /// <summary>
        /// Prüft ob 'path' im selben RootPath wie die MeisterGeister.exe liegt.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static bool IsSameRootPath(string path)
        {
            return System.IO.Path.GetPathRoot(System.Reflection.Assembly.GetEntryAssembly().Location) == System.IO.Path.GetPathRoot(path);
        }

        public static ProbenErgebnis ShowProbeDialog(Probe probe, Model.Held held)
        {
            View.Proben.ProbeDialog dlg = new View.Proben.ProbeDialog(probe, held);
            dlg.Owner = App.Current.MainWindow; // MainWindow als Owner setzen
            dlg.ShowDialog();
            return dlg.Ergebnis;
        }

        /// <summary>
        /// Zeigt ein Würfel-Fenster an.
        /// </summary>
        /// <param name="würfel"></param>
        /// <param name="infoText"></param>
        public static void OpenWürfelDialog(string würfel, string infoText = "")
        {
            if (System.Threading.Thread.CurrentThread.GetApartmentState() == System.Threading.ApartmentState.STA)
            {
                if (String.IsNullOrWhiteSpace(würfel))
                    würfel = ViewHelper.InputDialog("Gebe einen Würfel ein", "Welcher Würfel soll erstellt werden?", "W20");

                // Öffnet ein kompaktes Würfel-Fenster
                View.Würfeln.WürfelDialog dlg = new View.Würfeln.WürfelDialog(würfel, würfel, false);
                dlg.Owner = App.Current.MainWindow; // MainWindow als Owner setzen
                dlg.Show();
            }
            else
                return;
        }

        /// <summary>
        /// Zeigt ein Würfel Dialog-Fenster an.
        /// </summary>
        /// <param name="würfel">Würfel-Text (z.B. 2W+3, W20).</param>
        /// <param name="infoText">Info-Text, der angezeigt wird und den Würfelwurf beschreibt.</param>
        /// <returns>Würfelergebnis.</returns>
        public static int ShowWürfelDialog(string würfel, string infoText = "")
        {
            int ergebnis = 0;
            if (System.Threading.Thread.CurrentThread.GetApartmentState() == System.Threading.ApartmentState.STA)
            {
                View.Würfeln.WürfelDialog dlg = new View.Würfeln.WürfelDialog(würfel, infoText);
                dlg.Owner = App.Current.MainWindow; // MainWindow als Owner setzen
                bool? result = dlg.ShowDialog();
                if (result == true)
                    ergebnis = dlg.Ergebnis;
            }
            else
                ergebnis = Logic.General.Würfel.Parse(würfel, true);
            return ergebnis;
        }

        public static int ShowWürfelDialog(WürfelEnum würfel, string infoText = "")
        {
            string w = string.Empty;
            switch (würfel)
            {
                case WürfelEnum._1W3:
                    w = "1W3";
                    break;
                case WürfelEnum._1W6:
                    w = "1W6";
                    break;
                case WürfelEnum._2W6:
                    w = "2W6";
                    break;
                case WürfelEnum._1W10:
                    w = "1W10";
                    break;
                case WürfelEnum._1W20:
                    w = "1W20";
                    break;
                default:
                    break;
            }
            return ShowWürfelDialog(w, infoText);
        }

        public static void ShowGegnerView(ViewModel.Kampf.Logic.Kampf kampf)
        {
            GegnerWindow win = new GegnerWindow(kampf);
            win.Show();
        }

        public static string SelectImage()
        {
            string path = null;
            if (System.Threading.Thread.CurrentThread.GetApartmentState() == System.Threading.ApartmentState.STA)
            {
                View.General.SelectImageDialog dlg = new SelectImageDialog();
                dlg.Owner = App.Current.MainWindow; // MainWindow als Owner setzen
                bool? result = dlg.ShowDialog();
                if (result == true)
                    path = dlg.SelectedPath;
            }
            return path;
        }

        public static Image GetImageFromControl(Visual controlToRender, double dpiX = 96.0, double dpiY = 96.0)
        {
            if (controlToRender == null)
            {
                return null;
            }
            Rect bounds = VisualTreeHelper.GetDescendantBounds(controlToRender);
            RenderTargetBitmap rtb = new RenderTargetBitmap((int)(bounds.Width * dpiX / 96.0),
                                                            (int)(bounds.Height * dpiY / 96.0),
                                                            dpiX,
                                                            dpiY,
                                                            PixelFormats.Pbgra32);
            DrawingVisual dv = new DrawingVisual();
            using (DrawingContext ctx = dv.RenderOpen())
            {
                VisualBrush vb = new VisualBrush(controlToRender);
                ctx.DrawRectangle(vb, null, new Rect(new Point(), bounds.Size));
            }
            rtb.Render(dv);

            Image img = new Image();
            img.Source = rtb;

            return img;
        }

        public static Window ShowBrowser(string url, string title = "")
        {
            if (System.Threading.Thread.CurrentThread.GetApartmentState() == System.Threading.ApartmentState.STA)
            {
                Web.WebBrowserWindow win = new Web.WebBrowserWindow(title);
                win.Owner = App.Current.MainWindow; // MainWindow als Owner setzen
                win.Show();
                win.Navigate(url);
                return win;
            }
            return null;
        }

        public static Window ShowBrowserChangeLog(bool startUp = false)
        {
            string version = App.GetVersionString(App.GetVersionProgramm());
            Window win = ShowBrowser(string.Format("http://moonvega.pmhost.de/trac/query?group=status&milestone={0}", version), string.Format("ChangeLog dieser Version ({0})", version));
            if (startUp && win != null)
            {
                win.Closed += delegate(System.Object o, System.EventArgs e)
                {
                    Logic.Einstellung.Einstellungen.ShowChangeLog = ViewHelper.Confirm("ChangeLog anzeigen", "Soll beim nächsten Programmstart der ChangeLog wieder angezeigt werden?");
                };
            }
            return win;
        }

        public static object ParseXAML(string xaml, IDictionary<string, Type> userNamespaceTypes = null)
        {
            var context = new ParserContext();
            context.XamlTypeMapper = new XamlTypeMapper(new string[0]);
            if (userNamespaceTypes != null)
                foreach(var abbreviation in userNamespaceTypes.Keys)
                {
                    context.XamlTypeMapper.AddMappingProcessingInstruction(abbreviation, userNamespaceTypes[abbreviation].Namespace, userNamespaceTypes[abbreviation].Assembly.FullName);
                    context.XmlnsDictionary.Add(abbreviation, abbreviation);
                }
            context.XmlnsDictionary.Add("", "http://schemas.microsoft.com/winfx/2006/xaml/presentation");
            context.XmlnsDictionary.Add("x", "http://schemas.microsoft.com/winfx/2006/xaml");

            return XamlReader.Load((Stream)new MemoryStream(Encoding.UTF8.GetBytes(xaml)), context);
        }

    }
}
