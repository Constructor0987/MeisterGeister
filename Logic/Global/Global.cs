using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Input;
using MeisterGeister.Logic.General;

//Eigene Usings
using MeisterGeister.Model;
using MeisterGeister.Net;
using MeisterGeister.ViewModel;
using Service = MeisterGeister.Model.Service;

namespace MeisterGeister
{
    public delegate void GruppenProbeWürfelnEventHandler(Probe probe, EventArgs e);

    public static class Global
    {
        #region //CONTEXTE

        public static Service.AudioService ContextAudio;
        public static Service.DataService ContextHeld;
        public static Service.GeoService ContextGeo;
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
                {
                    _contextMenuLink = new Service.MenuLinkService();
                }

                return _contextMenuLink;
            }

            set { _contextMenuLink = value; }
        }

        #endregion //CONTEXTE

        #region //EIGENSCHAFTSMETHODEN

        /// <summary>
        /// Gibt an, ob der INTERN Modus aktiviert ist.
        /// </summary>
        public static bool INTERN
        {
            get
            {
                if (_INTERN == null)
                {
                    _INTERN = Logic.Einstellung.Einstellungen.GetEinstellung<bool>("INTERN");
                }

                return _INTERN.Value;
            }

            set
            {
                var pwd = View.General.ViewHelper.InputDialog("Passwort", "Passwort für INTERN Modus eingeben.", string.Empty);
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

        public static bool DSA5
        {
            get
            {
                return _dsa5;
            }
        }

        /// <summary>
        /// Gibt 'Visible' zurück, wenn die Regeledition DSA5 ist, sonst 'Collapsed'.
        /// </summary>
        public static System.Windows.Visibility DSA5_Visibility
        {
            get { return _dsa5_Visibility; }
        }

        public static bool DSA4_1
        {
            get
            {
                return _dsa4_1;
            }
        }

        /// <summary>
        /// Gibt 'Visible' zurück, wenn die Regeledition DSA4.1 ist, sonst 'Collapsed'.
        /// </summary>
        public static System.Windows.Visibility DSA4_1_Visibility
        {
            get { return _dsa4_1_Visibility; }
        }

        public static string Text_Generierungseinheit_Abk
        {
            get
            {
                return _text_Generierungseinheit_Abk;
            }
        }

        public static string Text_Generierungseinheit
        {
            get
            {
                return _text_Generierungseinheit;
            }
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

        public static MeisterGeister.Logic.Kalender.DsaTool.DSADateTime Zeitpunkt
        {
            get { return Global.zeitpunkt; }

            set
            {
                Global.zeitpunkt = value;
                OnZeitpunktChanged();
            }
        }
        private static MainViewModel _mainVM = null;
        public static MainViewModel MainVM
        {
            get { return _mainVM; }
            set { _mainVM = value; }
        }
        public static string AktuellesDatum
        {
            get { return Global.aktuellesDatum; }

            set
            {
                Global.aktuellesDatum = value;
                OnDatumChanged();
                MainViewModel.Instance.AktuellesDatum = value;
            }
        }

        public static DgSuche.Ortsmarke Standort
        {
            get
            {
                if (standort == null)
                {
                    standort = new DgSuche.Ortsmarke(Logic.Einstellung.Einstellungen.Standort);
                }

                return standort;
            }

            set
            {
                standort = value;
                HeldenPosition = new System.Windows.Point(standort.Longitude, standort.Latitude);
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

        /// <summary>
        /// Längengrad (X)
        /// </summary>
        public static double HeldenLon
        {
            get { return Global.heldenPosition.X; }

            set
            {
                Global.heldenPosition.X = value;
                Standort.Longitude = value;
                standort.Name = "";
                OnStandortChanged();
            }
        }

        /// <summary>
        /// Breitengrad (Y)
        /// </summary>
        public static double HeldenLat
        {
            get { return Global.heldenPosition.Y; }

            set
            {
                Global.heldenPosition.Y = value;
                Standort.Latitude = value;
                standort.Name = "";
                OnStandortChanged();
            }
        }

        /// <summary>
        /// Position als Point (Länge, Breite)
        /// </summary>
        public static System.Windows.Point HeldenPosition
        {
            get { return heldenPosition; }

            set
            {
                heldenPosition = value;
                standort.Longitude = heldenPosition.X;
                standort.Latitude = heldenPosition.Y;
                standort.Name = "";
                OnStandortChanged();

                List<string> altR = MomentaneRegion;
                MomentaneRegion = Global.ContextZooBot.GetRegion(value, 0);

                if ((!altR.SequenceEqual(MomentaneRegion) || altR.Count == 0) &&
                    MainViewModel.Instance.OpenTools.FirstOrDefault(t => t.Name == "Basar") != null)
                {
                    (MainViewModel.Instance.OpenTools.FirstOrDefault(t => t.Name == "Basar") as ViewModel.Basar.BasarViewModel).FillBasarListe();
                }
            }
        }

        /// <summary>
        /// Ruft den aktuell ausgewählten Helden ab, oder legt ihn fest.
        /// </summary>
        public static List<string> MomentaneRegion
        {
            get { return _momentaneRegion ?? Global.ContextZooBot.GetRegion(HeldenPosition, 0); }

            set
            {
                _momentaneRegion = value;
                MainViewModel.Instance.MomentaneRegion = value;
            }
        }

        /// <summary>
        /// Ruft den aktuell ausgewählten Helden ab, oder legt ihn fest.
        /// </summary>
        public static Model.Held SelectedHeld
        {
            get { return MainViewModel.Instance.SelectedHeld; }
            set { MainViewModel.Instance.SelectedHeld = value; }
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

        private static MainViewModel mainVM = null;
        public static MainViewModel MainVM
        {
            get { return mainVM; }
            set { mainVM = value; }
        }


        /// <summary>
        /// Der aktuell geöffnete Kampf.
        /// </summary>
        public static ViewModel.Kampf.KampfViewModel CurrentKampf { get; set; }

        public static bool Intern_CheckPwd(string pwd)
        {
            return INTERN_PWD == pwd;
        }

        private const string INTERN_PWD = "%m3ist3rg3ist3r%Intern";

        private static readonly Model.Held _selectedHeld;

        // TODO: Einstellung wird gecached, um Absturz zu verhindern. Da sich dadurch die Einstellung nach Änderung ggf. nicht mehr aktuell sein könnte, sollte das Caching noch überarbeitet werden.
        private static Nullable<bool> _INTERN = null;

        private static string _regeleditionNummer = string.Empty;
        private static bool _dsa5 = false;
        private static System.Windows.Visibility _dsa5_Visibility = System.Windows.Visibility.Collapsed;
        private static bool _dsa4_1 = false;
        private static System.Windows.Visibility _dsa4_1_Visibility = System.Windows.Visibility.Collapsed;
        private static string _text_Generierungseinheit_Abk = "GP";
        private static string _text_Generierungseinheit = "Gernerierungspunkte";
        private static MeisterGeister.Logic.Kalender.DsaTool.DSADateTime zeitpunkt;
        private static string aktuellesDatum;
        private static DgSuche.Ortsmarke standort = null;
        private static System.Windows.Point heldenPosition = new System.Windows.Point();
        private static List<string> _momentaneRegion;
        //public static ViewModel.SpielerScreen.SpielerScreenControlViewModel CurrentSpielerScreen { get; set; }

        #endregion //EIGENSCHAFTSMETHODEN

        #region //KONSTRUKTOR

        static Global()
        {
            IsInitialized = false;
        }

        #endregion //KONSTRUKTOR

        #region //KLASSENMETHODEN

        /// <summary>
        /// INIT lädt Daten aus der Datenbank, 1x aus APP aufgerufen beim start
        /// </summary>
        public static void Init()
        {
            LogInfo log = Logger.PerformanceLogStart("Daten aus Datenbank laden");

            ContextAudio = new Service.AudioService();
            ContextHeld = new Service.DataService();
            ContextGeo = new Service.GeoService();
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
                if (Guid.TryParse(Logic.Einstellung.Einstellungen.SelectedHeld, out Guid heldguid))
                {
                    SelectedHeldGUID = heldguid;
                }
            }
            var sarr = Logic.Einstellung.Einstellungen.Standort.Split('#');
            Standort = new DgSuche.Ortsmarke(sarr[0], sarr[1], sarr[2]);
            Logger.PerformanceLogEnd(log);

            AktuellesDatum = Logic.Kalender.Datum.Aktuell.ToStringShort();

            //webserver
            WebServer = new Server();

            Downloader = new Net.Web.Downloader();

            //Hilfsweise Events - TODO löschen sobald alle Referenzen aufs MainViewModel umgestellt sind:
            MainViewModel.Instance.PropertyChanging += MVM_PropertyChanging;
            MainViewModel.Instance.PropertyChanged += MVM_PropertyChanged;
        }

        public static void CleanUp()
        {
            if (WebServer != null && WebServer.Status != Server.States.Stopped && WebServer.Status != Server.States.Stopping)
            {
                WebServer.Stop();
            }
        }

        /// <summary>
        /// Versetzt die Anwendung in einen "Beschäftig-Status" bzw. entfernt diesen Status. Das
        /// Hauptfenster zeigt dabei ein Busy-Overlay. (deaktiviert)
        /// </summary>
        /// <param name="isBusy">'True' falls Anwendung gerade arbeitet.</param>
        /// <param name="info">Info Text.</param>
        public static void SetIsBusy(bool isBusy, string info = "Beschäftigt...")
        {
            if (isBusy)
            {
                Mouse.OverrideCursor = Cursors.Wait;
            }
            else
            {
                Mouse.OverrideCursor = null;
            }

            //if (App.Current.MainWindow != null
            //    && App.Current.MainWindow is View.MainView && App.Current.MainWindow.IsLoaded)
            //{
            //    (App.Current.MainWindow as View.MainView).IsBusyInfoText = info;
            //    (App.Current.MainWindow as View.MainView).IsBusy = isBusy;
            //}
        }

        /// <summary>
        /// Wechselt ins Proben-Tool und würfelt eine Probe.
        /// </summary>
        /// <param name="probe">Die zu würfelnde Probe.</param>
        public static void WürfelGruppenProbe(Probe probe)
        {
            if (App.Current.MainWindow == null
                || !(App.Current.MainWindow is View.MainView))
            {
                return;
            } (App.Current.MainWindow as View.MainView).StarteTab("Proben");

            if (GruppenProbeWürfeln != null)
            {
                GruppenProbeWürfeln(probe, new EventArgs());
            }
        }

        private static void MVM_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedHeld")
            {
                OnSelectedHeldChanged();
            }
        }

        private static void MVM_PropertyChanging(object sender, System.ComponentModel.PropertyChangingEventArgs e)
        {
            if (e.PropertyName == "SelectedHeld")
            {
                OnSelectedHeldChanging();
            }
        }

        private static void RefreshRegeledition()
        {
            // Editionswerte aus Performance-Gründen cachen
            _dsa5 = Regeledition == "DSA 5" || Regeledition == "DSA5" || Regeledition == "5";
            _dsa4_1 = Regeledition == "DSA 4.1" || Regeledition == "DSA4.1" || Regeledition == "4.1";
            _regeleditionNummer = Regeledition.Replace(" ", "").Replace("DSA", string.Empty);
            _dsa5_Visibility = DSA5 ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
            _dsa4_1_Visibility = DSA4_1 ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
            _text_Generierungseinheit = _dsa5 ? "Abenteuerpunkte" : "Generierungspunkte";
            _text_Generierungseinheit_Abk = _dsa5 ? "AP" : "GP";
        }

        #endregion //KLASSENMETHODEN

        #region //EVENTS

        public static event EventHandler ZeitpunktChanged;

        public static event EventHandler HeldSelectionChanged;

        public static event EventHandler HeldSelectionChanging;

        public static event EventHandler StandortChanged;

        public static event EventHandler DatumChanged;

        public static event GruppenProbeWürfelnEventHandler GruppenProbeWürfeln;

        private static void OnStandortChanged()
        {
            Logic.Einstellung.Einstellungen.Standort = string.Format(CultureInfo.InvariantCulture, "{0}#{1}#{2}", Standort.Name, Standort.Latitude, Standort.Longitude);

            if (StandortChanged != null)
            {
                StandortChanged(null, new EventArgs());
            }
        }

        private static void OnDatumChanged()
        {
            //Logic.Einstellung.Einstellungen.Standort = string.Format(CultureInfo.InvariantCulture, "{0}#{1}#{2}", Standort.Name, Standort.Latitude, Standort.Longitude);

            if (DatumChanged != null)
            {
                DatumChanged(null, new EventArgs());
            }
        }

        private static void OnZeitpunktChanged()
        {
            //Logic.Einstellung.Einstellungen.Standort = string.Format(CultureInfo.InvariantCulture, "{0}#{1}#{2}", Standort.Name, Standort.Latitude, Standort.Longitude);

            if (ZeitpunktChanged != null)
            {
                ZeitpunktChanged(null, new EventArgs());
            }
        }

        private static void OnSelectedHeldChanged()
        {
            if (HeldSelectionChanged != null)
            {
                HeldSelectionChanged(null, new EventArgs());
            }
        }

        private static void OnSelectedHeldChanging()
        {
            if (HeldSelectionChanging != null)
            {
                HeldSelectionChanging(null, new EventArgs());
            }
        }

        #endregion //EVENTS
    }
}
