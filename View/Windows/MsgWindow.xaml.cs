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
using System.Runtime.InteropServices;

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
            _textBoxMsg.Text = App.GetVersionStringLong() + "\n\n" + GetSysInfoString();
        }

        public MsgWindow(string header, string msg, bool showHilfe = true)
        {
            //SplashScreen schließen, falls geöffnet
            App.CloseSplashScreen();

            InitializeComponent();
            Title = header;
            if (showHilfe)
                msg += "\n\n" + App.GetVersionStringLong() + "\n\n" + GetHilfeString();

            _textBoxMsg.Text = msg;
        }

        public MsgWindow(string header, string msg, Exception ex)
        {
            //SplashScreen schließen, falls geöffnet
            App.CloseSplashScreen();

            InitializeComponent();
            Title = header;

            msg += "\n\n" + GetHilfeString();

            msg += "\n\n" + App.GetVersionStringLong();
            msg += "\n\nSource: " + ex.Source;
            msg += "\nMessage: " + ex.Message;
            msg += "\nType: " + ex.GetType().ToString();
            
            msg += GetExceptionDetails(ex);

            msg += "\n\nInnerException: " + (ex.InnerException != null ? ex.InnerException.ToString() : "-");

            msg += "\n\nStackTrace: " + ex.StackTrace;
            if (ex.Data.Count > 0)
            {
                msg += "\n\nData: ";
                foreach (System.Collections.DictionaryEntry item in ex.Data)
                    msg += "\n  " + ((item.Key != null) ? item.Key.ToString() : "null") + ": " + ((item.Value != null) ? item.Value.ToString() : "null");
            }

            // aktuelle Anwendungssituation
            try
            {
                msg += "\n\nAktuelles Tool: " + ((App.Current.MainWindow as MainView)._tabControlMain.SelectedItem as View.General.TabItemControl).Titel;
            }
            catch (Exception) { }
            try
            {
                msg += "\nOffene Tools: " + Logic.Einstellung.Einstellungen.StartTabs;
            }
            catch (Exception) {  }

            // Syteminformationen
            msg += "\n\n" + GetSysInfoString();
            _textBoxMsg.Text = msg;

            // Fehler in Log-Datei schreiben
            Logic.General.Logger.LogMsgToFile(msg);
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
            if (ex is System.IO.FileLoadException)
            {
                System.IO.FileLoadException fiLoEx = (System.IO.FileLoadException)ex;
                msg += "\nFileName: " + fiLoEx.FileName;
                msg += "\nFusionLog: " + fiLoEx.FusionLog;
            }
            if (ex is System.PlatformNotSupportedException && ex.StackTrace.Contains("System.Data.SqlServerCe.SqlCeSHA256.Initialize()"))
            {
                msg += "\n\nHINWEIS: Falls als Betriebssystem Windows XP verwendet wird, muss das Service Pack 3 installiert sein! Dies behebt evtl. diesen Fehler.\n\n";
            }
            if (ex is System.Data.SqlServerCe.SqlCeException)
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
            if (ex is System.Data.UpdateException)
            {
                System.Data.UpdateException exDet = (System.Data.UpdateException)ex;
                msg += "\nStateEntries: ";
                foreach (var item in exDet.StateEntries)
                    msg += item.State + " - " + (item.Entity ?? "null") + "\n   ";
            }
            if (ex is System.Data.Entity.Core.UpdateException)
            {
                System.Data.Entity.Core.UpdateException exUpd = (System.Data.Entity.Core.UpdateException)ex;
                msg += "\nStateEntries: ";
                foreach (var item in exUpd.StateEntries)
                    msg += item.State + " - " + (item.Entity ?? "null") + "\n   ";
            }
            return msg;
        }

        private string GetHilfeString()
        {
            return "Bitte prüfe die FAQ-Seite auf www.meistergeister.org und unser Forum (http://forum.meistergeister.org/) nach einer Fehlerlösung."
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

            string dirRoot = System.IO.Directory.GetDirectoryRoot(System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));
            string driveInfoText = dirRoot + " (Name), UNC (Typ)";

            try
            {
                System.IO.DriveInfo driveInfo = new System.IO.DriveInfo(dirRoot);
                driveInfoText = string.Format("{0} (Name), {1} (Format), {2} (Typ)", driveInfo.Name, driveInfo.DriveFormat, driveInfo.DriveType);
            }
            catch (Exception) { }

            string auflösung = SystemParameters.PrimaryScreenWidth.ToString() + "x" + SystemParameters.PrimaryScreenHeight.ToString()
                + " (" + SystemParameters.FullPrimaryScreenWidth.ToString() + "x" + SystemParameters.FullPrimaryScreenHeight.ToString() + ")";

            string screenInfo = System.Windows.Forms.Screen.AllScreens.Length.ToString() + Environment.NewLine;
            foreach (var item in System.Windows.Forms.Screen.AllScreens)
            {
                screenInfo += string.Format("{0} ({1}, BitsPerPixel: {2}, Primary: {3})\n", item.DeviceName, item.Bounds.ToString(), item.BitsPerPixel, item.Primary.ToString());
            }

            // zusätzliche Display Informationen ermitteln
            var device = new DISPLAY_DEVICE();
            device.cb = Marshal.SizeOf(device);
            try
            {
                for (uint id = 0; EnumDisplayDevices(null, id, ref device, 0); id++)
                {
                    device.cb = Marshal.SizeOf(device);
                    EnumDisplayDevices(device.DeviceName, 0, ref device, 0);
                    device.cb = Marshal.SizeOf(device);

                    screenInfo += string.Format("Nr={0}, Name={1}, DeviceString={2}, StateFlags={3}\n", id, device.DeviceName, device.DeviceString, device.StateFlags.ToString());
                    if (device.DeviceName == null || device.DeviceName == "") continue;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(String.Format("{0}", ex.ToString()));
            }

            return string.Format("Systeminformationen\n\nBetriebssystem: {0} ({1})\n64bit-System: {2}\nCLR-Version: {3}\n.NET Framework:{14}\nSQL-CE-Version: {4}\nArbeitsverzeichnis: {5}\nMeisterGeister-Verzeichnis: {6}"
                + "\nLaufwerk: {7}\nProzessoranzahl: {8}\nWorkingSet: {9}\nRenderingebene: {10}\nAuflösung: {11}\nBildschirme: {12}\n\nPerformance:{13}",
                Environment.OSVersion.ToString(), App.GetOSName(), Environment.Is64BitOperatingSystem.ToString(), Environment.Version.ToString(), App.SqlCompactVersion == null ? "-" : App.SqlCompactVersion.ToString(), Environment.CurrentDirectory,
                Logic.Extensions.FileExtensions.GetHomeDirectory(), driveInfoText, Environment.ProcessorCount, Environment.WorkingSet, renderTierInfo, auflösung, screenInfo,
                Logic.General.Logger.PerformanceLog, App.GetFrameworkFromRegistry());
        }

        #region // Display Informationen

        [DllImport("user32.dll")]
        public extern static bool EnumDisplayDevices(string lpDevice, uint iDevNum, ref DISPLAY_DEVICE lpDisplayDevice, uint dwFlags);

        [Flags()]
        public enum DisplayDeviceStateFlags : int
        {
            /// <summary>The device is part of the desktop.</summary>
            AttachedToDesktop = 0x1,
            MultiDriver = 0x2,
            /// <summary>The device is part of the desktop.</summary>
            PrimaryDevice = 0x4,
            /// <summary>Represents a pseudo device used to mirror application drawing for remoting or other purposes.</summary>
            MirroringDriver = 0x8,
            /// <summary>The device is VGA compatible.</summary>
            VGACompatible = 0x16,
            /// <summary>The device is removable; it cannot be the primary display.</summary>
            Removable = 0x20,
            /// <summary>The device has more display modes than its output devices support.</summary>
            ModesPruned = 0x8000000,
            Remote = 0x4000000,
            Disconnect = 0x2000000
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct DISPLAY_DEVICE
        {
            [MarshalAs(UnmanagedType.U4)]
            public int cb;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string DeviceName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string DeviceString;
            [MarshalAs(UnmanagedType.U4)]
            public DisplayDeviceStateFlags StateFlags;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string DeviceID;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string DeviceKey;
        }

        #endregion

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
