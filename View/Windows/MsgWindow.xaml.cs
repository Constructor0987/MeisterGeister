using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MeisterGeister.Daten;

namespace MeisterGeister.View.Windows
{
    /// <summary>
    /// Interaktionslogik für MsgWindow.xaml
    /// </summary>
    public partial class MsgWindow : Window
    {
        /// <summary>
        /// Standardkonstruktor zeigt Systeminformationen an.
        /// </summary>
        public MsgWindow()
        {
            //SplashScreen schließen, falls geöffnet
            App.CloseSplashScreen();

            InitializeComponent();
            Title = "Systeminformationen";
            _textBoxMsg.Text = GetVersionString() + "\n\n" + GetSysInfoString();
        }

        public MsgWindow(string header, string msg, bool showHilfe = true)
        {
            //SplashScreen schließen, falls geöffnet
            App.CloseSplashScreen();

            InitializeComponent();
            Title = header;
            if (showHilfe)
                msg += "\n\n" + GetVersionString() + "\n\n" + GetHilfeString();

            _textBoxMsg.Text = msg;
        }

        public MsgWindow(string header, string msg, Exception ex)
        {
            //SplashScreen schließen, falls geöffnet
            App.CloseSplashScreen();

            InitializeComponent();
            Title = header;

            msg += "\n\n" + GetHilfeString();

            msg += "\n\n" + GetVersionString();
            msg += "\n\nSource: " + ex.Source;
            msg += "\nMessage: " + ex.Message;
            msg += "\nType: " + ex.GetType().ToString();
            
            msg += GetExceptionDetails(ex);

            msg += "\nStackTrace: " + ex.StackTrace;
            if (ex.Data.Count > 0)
            {
                msg += "\n\nData: ";
                foreach (System.Collections.DictionaryEntry item in ex.Data)
                    msg += "\n  " + ((item.Key != null) ? item.Key.ToString() : "null") + ": " + ((item.Value != null) ? item.Value.ToString() : "null");
            }

            // Syteminformationen
            msg += "\n\n" + GetSysInfoString();
            _textBoxMsg.Text = msg;

            // Fehler in Log-Datei schreiben
            Logic.General.Logger.LogMsg(msg);
        }

        private static string GetExceptionDetails(Exception ex)
        {
            string msg = string.Empty;

            if (ex is System.Net.WebException)
            {
                System.Net.WebException webEx = (System.Net.WebException)ex;
                if (webEx.Response != null)
                {
                    msg += "\nResponse: " + webEx.Response.ToString();
                    if (webEx.Response.Headers != null)
                    {
                        msg += "\n   Headers: ";
                        for (int i = 0; i < webEx.Response.Headers.Count; i++)
                            msg += string.Format("{0}: {1}; ", webEx.Response.Headers.Keys[i], webEx.Response.Headers[i]);
                    }
                    if (webEx.Response.ResponseUri != null)
                        msg += "\n   ResponseUri: " + webEx.Response.ResponseUri.ToString();
                }
                msg += "\nStatus: " + webEx.Status.ToString();
            }
            else if (ex is System.IO.FileLoadException)
            {
                System.IO.FileLoadException fiLoEx = (System.IO.FileLoadException)ex;
                msg += "\nFileName: " + fiLoEx.FileName;
                msg += "\nFusionLog: " + fiLoEx.FusionLog;
            }
            else if (ex is System.PlatformNotSupportedException && ex.StackTrace.Contains("System.Data.SqlServerCe.SqlCeSHA256.Initialize()"))
            {
                msg += "\n\nHINWEIS: Falls als Betriebssystem Windows XP verwendet wird, muss das Service Pack 3 installiert sein! Dies behebt evtl. diesen Fehler.\n\n";
            }
            else if (ex is System.Data.SqlServerCe.SqlCeException)
            {
                System.Data.SqlServerCe.SqlCeException exDet = (System.Data.SqlServerCe.SqlCeException)ex;
                msg += "\nErrorCode: " + exDet.ErrorCode;
                msg += "\nNativeError: " + exDet.NativeError;
                msg += "\nHResult: " + exDet.HResult;
                if (exDet.Errors != null && exDet.Errors.Count > 0)
                {
                    msg += "\nErrors: ";
                    foreach (System.Data.SqlServerCe.SqlCeError item in exDet.Errors)
                        msg += "\n  " + ((item != null) ? item.ToString() : "null");
                }
            }
            return msg;
        }

        private string GetVersionString()
        {
            return string.Format("Version: {0} / {1}", App.GetVersionString(App.GetVersionProgramm()), DatabaseUpdate.DatenbankVersionAktuell);
        }

        private string GetHilfeString()
        {
            return "Bitte prüfe die FAQ-Seite auf www.meistergeister.org und unser Forum (http://meistergeister.siteboard.org/) nach einer Fehlerlösung."
                + "\nBei weiteren Problemen kannst du das Problem im Forum melden oder dich an info@meistergeister.org wenden (Bitte die gesamte Meldung vollständig kopieren)."
                + "\nDie Fehlermeldung wird in einer Log-Datei im MeisterGeister-Verzeichnis gespeichert.";
        }

        private string GetSysInfoString()
        {
            int renderingTier = (RenderCapability.Tier >> 16);
            string renderTierInfo = renderingTier.ToString();
            switch (renderingTier)
            {
                case 0:
                    renderTierInfo += " (Softwarebeschleunigung; DirectX < 9.0)";
                    break;
                case 1:
                case 2:
                    renderTierInfo += " (Hardwarebeschleunigung; DirectX >= 9.0)";
                    break;
                default:
                    break;
            }

            string auflösung = SystemParameters.PrimaryScreenWidth.ToString() + "x" + SystemParameters.PrimaryScreenHeight.ToString()
                + " (" + SystemParameters.FullPrimaryScreenWidth.ToString() + "x" + SystemParameters.FullPrimaryScreenHeight.ToString() + ")";

            return string.Format("Systeminformationen\n\nBetriebssystem: {0} ({1})\n64bit-System: {2}\nCLR-Version: {3}\nArbeitsverzeichnis: {4}\nMeisterGeister-Verzeichnis {5}\nProzessoranzahl: {6}\nWorkingSet: {7}"
                + "\nRenderingebene: {8}\nAuflösung: {9}\nBildschirme: {10}",
                Environment.OSVersion.ToString(), App.GetOSName(), Environment.Is64BitOperatingSystem.ToString(), Environment.Version.ToString(), Environment.CurrentDirectory, System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location),
                Environment.ProcessorCount, Environment.WorkingSet, renderTierInfo, auflösung, System.Windows.Forms.Screen.AllScreens.Length);
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            if (Owner != null)
            {
                try
                {
                    DialogResult = true;
                }
                finally
                {
                    Close();
                }
            }
            else
                Close();
        }

        private void ButtonCopy_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(_textBoxMsg.Text);
        }
    }
}
