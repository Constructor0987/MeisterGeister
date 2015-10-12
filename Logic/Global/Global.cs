using System;
using System.Collections.Generic;
using System.Linq;
using MeisterGeister.Logic.General;
//Eigene Usings
using MeisterGeister.Model;
using Service = MeisterGeister.Model.Service;
using System.Globalization;
using MeisterGeister.Net;

namespace MeisterGeister
{
    public static class Global
    {

        #region //CONTEXTE

        public static Service.AudioService ContextAudio;
        public static Service.DataService ContextHeld;
        public static Service.InventarService ContextInventar;
        public static Service.KampfService ContextKampf;
        public static Service.TalentService ContextTalent;
        public static Service.HandelsgutService ContextHandelsgut;
        public static Service.NscService ContextNsc;
        public static Service.NamenService ContextNamen;
        public static Service.NotizService ContextNotizen;
        public static Service.VorNachteilService ContextVorNachteil;
        public static Service.ZauberService ContextZauber;
        public static Service.ZooBotService ContextZooBot;

        // MenuLink
        public static Service.MenuLinkService _contextMenuLink;
        public static Service.MenuLinkService ContextMenuLink
        {
            get
            {
                if (_contextMenuLink == null)
                    _contextMenuLink = new Service.MenuLinkService();
                return _contextMenuLink;
            }
            set { _contextMenuLink = value; }
        }


        #endregion

        #region //FELDER

        private static Model.Held _selectedHeld;
        private static double _heldenLon = 32;
        private static double _heldenLat = 3;

        #endregion

        #region //EIGENSCHAFTSMETHODEN

        // TODO: Einstellung wird gecached, um Absturz zu verhindern. Da sich dadurch die Einstellung nach Änderung ggf. nicht mehr aktuell sein könnte, sollte das Caching noch überarbeitet werden.
        private static Nullable<bool> _INTERN = null;
        /// <summary>
        /// Gibt an, ob der INTERN Modus aktiviert ist.
        /// </summary>
        public static bool INTERN
        {
            get
            {
                if (_INTERN == null)
                    _INTERN = Logic.Einstellung.Einstellungen.GetEinstellung<bool>("INTERN");
                return _INTERN.Value;
            }
            set
            {
                string pwd = View.General.ViewHelper.InputDialog("Passwort", "Passwort für INTERN Modus eingeben.", string.Empty);
                if (Global.Intern_CheckPwd(pwd))
                {
                    Logic.Einstellung.Einstellungen.SetEinstellung<bool>("INTERN", value);
                    _INTERN = value;
                }
                else
                {
                    Logic.Einstellung.Einstellungen.SetEinstellung<bool>("INTERN", false);
                    _INTERN = false;
                }
            }
        }

        /// <summary>
        /// Gibt 'Visible' zurück, wenn interner Modus aktiv, sonst 'Collapsed'.
        /// </summary>
        public static System.Windows.Visibility INTERN_Visibility
        {
            get { return Global.INTERN ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed; }
        }

        private const string INTERN_PWD = "%m3ist3rg3ist3r%Intern";

        public static bool Intern_CheckPwd(string pwd)
        {
            return INTERN_PWD == pwd;
        }

        /// <summary>
        /// Die Regeledition in voller Schreibweise: 'DSA X'.
        /// </summary>
        public static string Regeledition
        {
            get
            {
                return Logic.Einstellung.Einstellungen.Regeledition;
            }
            set
            {
                Logic.Einstellung.Einstellungen.Regeledition = value;
                RefreshRegeledition();
            }
        }

        private static string _regeleditionNummer = string.Empty;
        /// <summary>
        /// Gibt die Nummer der Regeledition an, ohne Textzusatz.
        /// </summary>
        public static string RegeleditionNummer
        {
            get
            {
                return _regeleditionNummer;
            }
        }

        private static bool _dsa5 = false;
        public static bool DSA5
        {
            get
            {
                return _dsa5;
            }
        }

        private static System.Windows.Visibility _dsa5_Visibility = System.Windows.Visibility.Collapsed;
        /// <summary>
        /// Gibt 'Visible' zurück, wenn die Regeledition DSA5 ist, sonst 'Collapsed'.
        /// </summary>
        public static System.Windows.Visibility DSA5_Visibility
        {
            get { return _dsa5_Visibility; }
        }

        private static bool _dsa4_1 = false;
        public static bool DSA4_1
        {
            get
            {
                return _dsa4_1;
            }
        }

        private static System.Windows.Visibility _dsa4_1_Visibility = System.Windows.Visibility.Collapsed;
        /// <summary>
        /// Gibt 'Visible' zurück, wenn die Regeledition DSA4.1 ist, sonst 'Collapsed'.
        /// </summary>
        public static System.Windows.Visibility DSA4_1_Visibility
        {
            get { return _dsa4_1_Visibility; }
        }

        public static bool IsInitialized
        {
            get;
            private set;
        }

        public static Server WebServer
        {
            get;
            private set;
        }

        public static MeisterGeister.Net.Web.Downloader Downloader
        {
            get;
            private set;
        }

        private static MeisterGeister.Logic.Kalender.DsaTool.DSADateTime zeitpunkt;
        public static MeisterGeister.Logic.Kalender.DsaTool.DSADateTime Zeitpunkt
        {
            get { return Global.zeitpunkt; }
            set
            {
                Global.zeitpunkt = value;
                OnZeitpunktChanged();
            }
        }

        private static DgSuche.Ortsmarke standort = null;
        public static DgSuche.Ortsmarke Standort
        {
            get
            {
                if (standort == null)
                    standort = new DgSuche.Ortsmarke(Logic.Einstellung.Einstellungen.Standort);
                return standort;
            }
            set
            {
                standort = value;
                OnStandortChanged();
            }
        }

        public static string HeldenRegion
        {
            get { return standort.Name; }
            set
            {
                standort.Name = value;
                OnStandortChanged();
            }
        }

        public static double HeldenLon
        {
            get { return Global._heldenLon; }
            set
            {
                Global._heldenLon = value;
                Standort.Longitude = _heldenLon;
                //OnStandortChanged();
            }
        }

        public static double HeldenLat
        {
            get { return Global._heldenLat; }
            set
            {
                Global._heldenLat = value;
                Standort.Latitude = _heldenLat;
                //OnStandortChanged();
            }
        }

        /// <summary>
        /// Ruft den aktuell ausgewählten Helden ab, oder legt ihn fest.
        /// </summary>
        public static Model.Held SelectedHeld
        {
            get { return _selectedHeld; }
            set
            {
                // Falls der gleiche Held erneut gesetzt werden soll
                // -> abbrechen, da keine Änderung erfolgt
                if (_selectedHeld == value)
                    return;

                // Event vor der Held-Änderung werfen
                if (HeldSelectionChanging != null)
                    HeldSelectionChanging(null, new EventArgs());

                // neuen Helden setzen und Änderungen in DB speichern
                _selectedHeld = value;
                if (_selectedHeld != null)
                {
                    Global.ContextHeld.Update<Model.Held>(SelectedHeld);
                    Logic.Einstellung.Einstellungen.SelectedHeld = value.HeldGUID.ToString();
                }
                else
                    Logic.Einstellung.Einstellungen.SelectedHeld = null;

                // Event nach der Held-Änderung werfen
                if (HeldSelectionChanged != null)
                    HeldSelectionChanged(null, new EventArgs());
            }
        }

        /// <summary>
        /// Ruft die GUID des aktuell ausgewählten Helden ab, oder legt ihn über die GUID fest.
        /// </summary>
        public static Guid SelectedHeldGUID
        {
            get { return SelectedHeld == null ? Guid.Empty : SelectedHeld.HeldGUID; }
            set
            {
                SelectedHeld = ContextHeld.Liste<Held>().Where(h => h.HeldGUID == value).FirstOrDefault();
            }
        }

        /// <summary>
        /// Der aktuell geöffnete Kampf.
        /// </summary>
        public static ViewModel.Kampf.KampfViewModel CurrentKampf { get; set; }

        public static ViewModel.SpielerScreen.SpielerScreenControlViewModel CurrentSpielerScreen { get; set; }

        #endregion

        #region //KONSTRUKTOR

        static Global()
        {
            IsInitialized = false;
        }

        #endregion

        #region //KLASSENMETHODEN

        /// <summary>
        /// INIT lädt Daten aus der Datenbank, 1x aus APP aufgerufen beim start
        /// </summary>
        public static void Init()
        {
            LogInfo log = Logger.PerformanceLogStart("Daten aus Datenbank laden");

            ContextAudio = new Service.AudioService();
            ContextHeld = new Service.DataService();
            ContextInventar = new Service.InventarService();
            ContextKampf = new Service.KampfService();
            ContextTalent = new Service.TalentService();
            ContextVorNachteil = new Service.VorNachteilService();
            ContextHandelsgut = new Service.HandelsgutService();
            ContextNsc = new Service.NscService();
            ContextNamen = new Service.NamenService();
            ContextNotizen = new Service.NotizService();
            ContextZauber = new Service.ZauberService();
            ContextZooBot = new Service.ZooBotService();

            IsInitialized = true;
            Logic.Einstellung.Einstellungen.UpdateEinstellungen();

            RefreshRegeledition();

            if (Logic.Einstellung.Einstellungen.SelectedHeld != null)
            {
                Guid heldguid;
                if (Guid.TryParse(Logic.Einstellung.Einstellungen.SelectedHeld, out heldguid))
                    SelectedHeldGUID = heldguid;
            }
            OnStandortChanged();


            Logger.PerformanceLogEnd(log);

            //webserver
            WebServer = new Server();

            Downloader = new Net.Web.Downloader();
        }

        private static void RefreshRegeledition()
        {
            // Editionswerte aus Performance-Gründen cachen
            _dsa5 = Regeledition == "DSA 5" || Regeledition == "DSA5" || Regeledition == "5";
            _dsa4_1 = Regeledition == "DSA 4.1" || Regeledition == "DSA4.1" || Regeledition == "4.1";
            _regeleditionNummer = Regeledition.Replace(" ", "").Replace("DSA", string.Empty);
            _dsa5_Visibility = DSA5 ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
            _dsa4_1_Visibility = DSA4_1 ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
        }

        public static void CleanUp()
        {
            if (WebServer != null && WebServer.Status != Server.States.Stopped && WebServer.Status != Server.States.Stopping)
            {
                WebServer.Stop();
            }
        }

        /// <summary>
        /// Versetzt die Anwendung in einen "Beschäftig-Status" bzw. entfernt diesen Status.
        /// Das Hauptfenster zeigt dabei ein Busy-Overlay.
        /// </summary>
        /// <param name="isBusy">'True' falls Anwendung gerade arbeitet.</param>
        /// <param name="info">Info Text.</param>
        public static void SetIsBusy(bool isBusy, string info = "Beschäftigt...")
        {
            if (App.Current.MainWindow != null
                && App.Current.MainWindow is View.MainView && App.Current.MainWindow.IsLoaded)
            {
                (App.Current.MainWindow as View.MainView).IsBusyInfoText = info;
                (App.Current.MainWindow as View.MainView).IsBusy = isBusy;
            }
        }

        /// <summary>
        /// Wechselt ins Proben-Tool und würfelt eine Probe.
        /// </summary>
        /// <param name="probe">Die zu würfelnde Probe.</param>
        public static void WürfelGruppenProbe(Probe probe)
        {
            if (App.Current.MainWindow == null
                || !(App.Current.MainWindow is View.MainView))
                return;

            (App.Current.MainWindow as View.MainView).StarteTab("Proben");

            if (GruppenProbeWürfeln != null)
                GruppenProbeWürfeln(probe, new EventArgs());
        }

        #endregion

        #region //EVENTS


        public static event EventHandler ZeitpunktChanged;
        public static event EventHandler HeldSelectionChanged;
        public static event EventHandler HeldSelectionChanging;
        public static event EventHandler StandortChanged;

        static void OnStandortChanged()
        {
            Logic.Einstellung.Einstellungen.Standort = string.Format(CultureInfo.InvariantCulture, "{0}#{1}#{2}", Standort.Name, Standort.Latitude, Standort.Longitude);
            _heldenLon = Standort.Longitude;
            _heldenLat = Standort.Latitude;

            if (StandortChanged != null)
                StandortChanged(null, new EventArgs());
        }

        static void OnZeitpunktChanged()
        {
            //Logic.Einstellung.Einstellungen.Standort = string.Format(CultureInfo.InvariantCulture, "{0}#{1}#{2}", Standort.Name, Standort.Latitude, Standort.Longitude);

            if (ZeitpunktChanged != null)
                ZeitpunktChanged(null, new EventArgs());
        }

        public static event GruppenProbeWürfelnEventHandler GruppenProbeWürfeln;

        #endregion
    }

    public delegate void GruppenProbeWürfelnEventHandler(Probe probe, EventArgs e);

}
