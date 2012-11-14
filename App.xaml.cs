using System;
using System.Data.SqlServerCe;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows;
using MeisterGeister.Daten;
using Application = System.Windows.Application;
using MessageBox = System.Windows.MessageBox;
using System.Xml;
using System.Windows.Media.Imaging;
using System.Threading;
// Eigene Usings
using MeisterGeister.Logic.Settings;
using MeisterGeister.Logic.General;
using MeisterGeister.View.Windows;
using System.Globalization;
using System.Windows.Markup;

namespace MeisterGeister {
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application {
        /// <summary>
        /// Startup-Fenster, das während der Initialisierung und des Abspielens des Jingles angezeigt wird.
        /// </summary>
        static StartupWindow _splashScreen = new StartupWindow();

        public static object SqlCompactVersion = "-";

        public App() {
#if !DEBUG
            this.DispatcherUnhandledException += Application_DispatcherUnhandledException;
#endif
            // Startup-Fenster anzeigen (schließt sich automatisch)
            _splashScreen.Show();

            // Windows-Forms-Controls Windows-Theme aktivieren
            System.Windows.Forms.Application.EnableVisualStyles();

            // Prüfen, ob Datenbank vorhanden ist
            if (!File.Exists(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + @"\Daten\DatabaseDSA.sdf")) {
                // Es handelt sich vermutlich um eine Neuinstallation:
                // Standard-Datenbank kopieren und umbenennen
                try {
                    File.Copy(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + @"\Daten\DatabaseDSA_Standard.sdf",
                         Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + @"\Daten\DatabaseDSA.sdf", false);
                } catch (Exception ex) {
                    MsgWindow errWin = new MsgWindow("Datenbank-Fehler", "Die Standard-Datanbank konnte nicht gefunden werden!", ex);
                    errWin.ShowDialog();
                    Shutdown();
                    return;
                }
            }

            // Erneut prüfen...
            if (!File.Exists(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + @"\Daten\DatabaseDSA.sdf"))
            {
                string msg = "Die Datenbank-Datei \"Daten\\DatabaseDSA.sdf\" konnte nicht gefunden werden. Das Programm wird geschlossen.";
                MsgWindow errWin = new MsgWindow("Datenbank-Fehler", msg);
                errWin.ShowDialog();
                Shutdown();
                return;
            }

            // Schreibrecht prüfen
            if (!IsWriteable(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + @"\Daten\DatabaseDSA.sdf"))
            {
                string msg = "Das Programm hat keinen schreibenden Zugriff auf die Datenbank-Datei \"Daten\\DatabaseDSA.sdf\". "
                    + "Kopiere DSA MeisterGeister in ein Verzeichnis mit Schreibzugriff und starte es erneut.";
                MsgWindow errWin = new MsgWindow("Datenbank-Fehler", msg);
                errWin.ShowDialog();
                Shutdown();
                return;
            }

            // SQL-Server Compact Version prüfen
            SqlCompactVersion = "-";
            SqlCompactVersion = Microsoft.Win32.Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server Compact Edition\v4.0", "Version", "-");
            string sqlCompactVersionNeeded = "4.0.8"; //4.0.8482.1 oder 4.0.8876.1
            if (SqlCompactVersion == null || !SqlCompactVersion.ToString().StartsWith(sqlCompactVersionNeeded))
            {
                string msg = "Es ist keine, eine falsche oder eine beschädigte Microsoft SQL Server Compact Edition installiert.\n"
                    + "Installiert:\t" + SqlCompactVersion
                    + "\nBenötigt:\t\t" + sqlCompactVersionNeeded + "\n\nDie benötigte Edition kann unter folgendem Link heruntergeladen werden:\n\n"
                    + @"http://www.microsoft.com/downloads/de-de/details.aspx?familyid=033cfb76-5382-44fb-bc7e-b3c8174832e2&displaylang=de";
                MsgWindow errWin = new MsgWindow("Datenbank-Fehler", msg);
                errWin.ShowDialog();
                Shutdown();
                return;
            }

            //Version der Datenbank überprüfen und upgraden.
            try {
                SqlCeUpgrade.Run(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + @"\Daten\DatabaseDSA.sdf");
            } catch (Exception ex) {
                MsgWindow errWin = new MsgWindow("Fehler beim Datenbank-Upgrade", "Beim Datenbank-Upgrade ist ein Fehler aufgetreten!", ex);
                errWin.ShowDialog();
                Shutdown();
            }

            // Auf Datenbank-Update prüfen
            try {
                string msg = string.Empty;
                MsgWindow msgWin = null;

                switch (DatabaseUpdate.PerformDatabaseUpdate()) {
                    case DatabaseUpdateResult.DatenbankVersionOK:
                        break;
                    case DatabaseUpdateResult.DatenbankUpdateOK:
                        msg = string.Format("Es wurde ein Update Ihrer Datenbank durchgeführt, da Ihre Version veraltet war!"
                            + "\nBitte starten Sie die Anwendung neu.\n\nBisherige Datenbank-Version: {0}\nNeue Datenbank-Version: {1}"
                            + "\n\nEs wurde ein Backup der Datenbank im Daten-Verzeichnis erstellt.", DatabaseUpdate.UserDatabaseVersion, DatabaseUpdate.DatenbankVersionAktuell);
                        msgWin = new MsgWindow("Datenbank Update", msg, false);
                        msgWin.ShowDialog();

                        // nicht mehr benötigte Dateien aus früheren Versionen löschen bzw. verschieben
                        DatabaseUpdate.CleanUpDirectory();
                        Shutdown();
                        return;
                    case DatabaseUpdateResult.DatenbankNeuer:
                        msg = string.Format("Die verwendete Datenbank-Version ist neuer als die erwartete Version!"
                            + "\n Die MeisterGeister Programm-Version ist zu alt für die Datenbak."
                            + "\nBitte lade eine neue MeisterGeister Version herunter (www.meistergeister.org).\n\nVerwendete Datenbank-Version: {0}\nErwartete Datenbank-Version: {1}",
                        DatabaseUpdate.UserDatabaseVersion, DatabaseUpdate.DatenbankVersionAktuell);
                        msgWin = new MsgWindow("Fehler beim Datenbank-Update", msg, false);
                        msgWin.ShowDialog();
                        Shutdown();
                        return;
                    case DatabaseUpdateResult.UnbekanntesErgebnis:
                    case DatabaseUpdateResult.DatenbankUpdateError:
                    default:
                        msgWin = new MsgWindow("Fehler beim Datenbank-Update", "Unbekannter Datenbank-Update Fehler.\n" + DatabaseUpdate.UpdateStatus, false);
                        msgWin.ShowDialog();
                        Shutdown();
                        return;
                }
            } catch (Exception ex) {
                MsgWindow errWin = new MsgWindow("Fehler beim Datenbank-Update", "Beim Datenbank-Update ist ein Fehler aufgetreten!\n" + DatabaseUpdate.UpdateStatus, ex);
                errWin.ShowDialog();
                Shutdown();
                return;
            }

            InitializeComponent();

            try {
                // Jingle abspielen
#if !(NO_JINGLE)
                if (!Einstellungen.JingleAbstellen)
                    AudioPlayer.PlayJingle();
#endif
            } catch (Exception ex) {
                MsgWindow errWin = new MsgWindow("Audio Fehler", "Beim Abspielen des Start-Jingles ist ein Fehler aufgetreten.", ex);
                Nullable<bool> dialogResult = errWin.ShowDialog();
                errWin.Close();
                Application.Current.MainWindow.Opacity = 1;
            }

            // Restliche Daten aus Datenbank laden
            LoadDataFromDatabase();
        }

        private void LoadDataFromDatabase() {
            //DW 2012-01-24: Globale-Klasse mit allen Daten wird initialisiert
            Global.Init();
        }

        public static bool IsWriteable(string filename) {
            if (File.Exists(filename)) {
                FileInfo fi = new FileInfo(filename);
                if (fi.IsReadOnly)
                    return false;
            }
            return true;
        }

        public static void SaveHelden() {
            //Wenn Helden gespeichert werden, werden sie für die neue Struktur neu geladen
            Global.ContextHeld.UpdateList<Model.Held>();
        }

        public static void SaveAll() {
            // Änderungen in Datenbank speichern
            if (Global.IsInitialized) {
                try {
                    Global.ContextHeld.Save();
                } catch (Exception ex) {
                    MsgWindow errWin = new MsgWindow("Fehler beim Speichern der Datenbank", "Beim Speichern der Datenbank ist ein Fehler aufgetreten!", ex);
                    errWin.ShowDialog();
                }
            }
        }

        public static Version[] GetVersionDownload() {
            Version[] v = new Version[] { new Version(), new Version() };
            WebClient w = new WebClient();
            try {
                string s = w.DownloadString("http://www.meistergeister.org/version/");

                MatchCollection m1 = Regex.Matches(s, "(<span id=\"version\".*?>.*?</span>)", RegexOptions.Singleline);
                MatchCollection m2 = Regex.Matches(s, "(<span id=\"versionBeta\".*?>.*?</span>)", RegexOptions.Singleline);

                if (m1.Count > 0) {
                    string value = m1[0].Value;
                    value = value.Replace("<span id=\"version\">", string.Empty).Replace("</span>", string.Empty);
                    string[] values = value.Split('.');
                    if (values.Length == 3)
                        v[0] = new Version(Convert.ToInt32(values[0]), Convert.ToInt32(values[1]), 0, Convert.ToInt32(values[2]));
                    else if (values.Length > 3)
                        v[0] = new Version(Convert.ToInt32(values[0]), Convert.ToInt32(values[1]), Convert.ToInt32(values[3]), Convert.ToInt32(values[2]));
                }
                if (m2.Count > 0) {
                    string value = m2[0].Value;
                    value = value.Replace("<span id=\"versionBeta\">", string.Empty).Replace("</span>", string.Empty);
                    string[] values = value.Split('.');
                    if (values.Length == 3)
                        v[1] = new Version(Convert.ToInt32(values[0]), Convert.ToInt32(values[1]), 0, Convert.ToInt32(values[2]));
                    else if (values.Length > 3)
                        v[1] = new Version(Convert.ToInt32(values[0]), Convert.ToInt32(values[1]), Convert.ToInt32(values[3]), Convert.ToInt32(values[2]));
                }
                return v;
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Auf Updates prüfen");
            }

            return v;
        }

        public static string GetVersionString(Version v) {
            if (v.Build == 0)
                return string.Format("{0}.{1}.{2}", v.Major, v.Minor, v.Revision);
            else
                return string.Format("{0}.{1}.{2}.{3}", v.Major, v.Minor, v.Revision, v.Build);
        }

        public static Version GetVersionProgramm() {
            // Programm-Version abrufen
            Assembly assem = Assembly.GetExecutingAssembly();
            AssemblyName assemName = assem.GetName();
            return assemName.Version;
        }

        public static void CheckUpdates() {
            Version[] vDownload = GetVersionDownload();
            Version vProgramm = GetVersionProgramm();
            string vDownloadString = GetVersionString(vDownload[0]);
            string vDownloadBetaString = GetVersionString(vDownload[1]);
            string vProgrammString = GetVersionString(vProgramm);
            string infoText = string.Empty;

            if (vDownload == null || vDownload[0] == new Version())
                infoText = "Die aktuelle Programm Version konnte nicht geprüft werden.";
            else {
                vProgramm = new Version(vProgramm.Major, vProgramm.Minor, vProgramm.Revision, vProgramm.Build);
                vDownload[1] = new Version(vDownload[1].Major, vDownload[1].Minor, vDownload[1].Revision, vDownload[1].Build);

                int compareVersions = vDownload[1].CompareTo(vProgramm);
                if (compareVersions == 0)
                    infoText = string.Format("Das Programm ist auf dem aktuellsten Stand.\n\nInstallierte Version: {0}", vProgrammString);
                else if (compareVersions > 0)
                    infoText = string.Format("Es liegt eine neue Programm-Version vor.\n\nInstallierte Version: {0}\nDownload Version: {1}\nDownload Version: {2} BETA"
                    + "\n\nDie aktuelle Version kann unter '{3}' runtergeladen werden.", vProgrammString, vDownloadString, vDownloadBetaString,
                    MeisterGeister.Properties.Settings.Default.MeisterGeisterURL);
                else
                    infoText = string.Format("Die installierte Programm-Version ist neuer als die Download-Version.\n\nInstallierte Version: {0}\nDownload Version: {1}\nDownload Version: {2} BETA",
                    vProgrammString, vDownloadString, vDownloadBetaString);
            }

            MessageBox.Show(infoText, "Versions-Prüfung", MessageBoxButton.OK);
        }


        internal static string GetOSName() {
            if (Environment.OSVersion.Version.ToString().StartsWith("6.2"))
                return "Windows 8";
            else if (Environment.OSVersion.Version.ToString().StartsWith("6.1.8400"))
                return "Windows Home Server 2011";
            else if (Environment.OSVersion.Version.ToString().StartsWith("6.1"))
                return "Windows 7 / Server 2008 R2";
            else if (Environment.OSVersion.Version.ToString().StartsWith("6.0"))
                return "Windows Vista / Server 2008";
            else if (Environment.OSVersion.Version.ToString().StartsWith("5.2"))
                return "Windows XP 64bit / Server 2003 / Server 2003 R2";
            else if (Environment.OSVersion.Version.ToString().StartsWith("5.1"))
                return "Windows XP";
            else if (Environment.OSVersion.Version.ToString().StartsWith("5.0"))
                return "Windows 2000";
            else if (Environment.OSVersion.Version.ToString().StartsWith("4.9"))
                return "Windows Me";
            return "Unbekannt";
        }

        internal static void CloseSplashScreen() {
            if (_splashScreen != null) {
                _splashScreen.KillMe();
                _splashScreen = null;
            }
        }

        protected override void OnStartup(StartupEventArgs e) {
            base.OnStartup(e);

            FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement),
                new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(CultureInfo.CreateSpecificCulture("de-DE").IetfLanguageTag)));

        }

        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e) {
            // Dieser EventHandler fängt alle unbehandelten Ausnahmen und zeigt den Fehler an.
            MsgWindow errWin = new MsgWindow("Unbehandelte Ausnahme", "Es ist eine unbehandelte Ausnahme aufgetreten. Das Programm wird beendet.\n", e.Exception);
            errWin.ShowDialog();
            e.Handled = true;
            Shutdown();
        }
    }
}
