using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using MeisterGeister.Logic.General;

namespace MeisterGeister.Logic.Einstellung
{
    public static class Einstellungen
    {
        public static void UpdateEinstellungen()
        {
            if(Global.IsInitialized)
            {
                foreach(var e in Global.ContextHeld.Liste<Model.Einstellung>())
                {
                    CopyDefaultValues(e.Name, e);
                    Global.ContextHeld.Update<Model.Einstellung>(e);
                }
                Global.ContextHeld.Save();
            }
        }


        private static List<Model.Einstellung> GetDefaults()
        {
            return new List<Model.Einstellung>()
            {
                new Model.Einstellung() { Name = "JingleAbstellen", Kontext = "Allgemein", Kategorie = null, Typ = "Boolean", Beschreibung = "Jingle beim Start deaktivieren", Wert = "False" },
                new Model.Einstellung() { Name = "ShowChangeLog", Kontext = "Allgemein", Kategorie = null, Typ = "Boolean", Beschreibung = "ChangeLog beim Start anzeigen", Wert = "True" },

                new Model.Einstellung() { Name = "Fading", Kontext = "Audioplayer", Kategorie = null, Typ = "Integer", Beschreibung = "", Wert = "600" },               
                new Model.Einstellung() { Name = "AudioInAnderemPfadSuchen", Kontext = "Audioplayer", Kategorie = null, Typ = "Boolean", Beschreibung = "", Wert = "1" },
                new Model.Einstellung() { Name = "AudioDirektAbspielen", Kontext = "Audioplayer", Kategorie = null, Typ = "Boolean", Beschreibung = "", Wert = "1" },
                new Model.Einstellung() { Name = "AudioVerzeichnis", Kontext = "Audioplayer", Kategorie = null, Typ = "String", Beschreibung = null, Wert = "C:\\" },
                new Model.Einstellung() { Name = "AudioSpieldauerBerechnen", Kontext = "Audioplayer", Kategorie = null, Typ = "Boolean", Beschreibung = null, Wert = "1" },
                new Model.Einstellung() { Name = "GeneralMusikVolume", Kontext = "Audioplayer", Kategorie = null, Typ = "Integer", Beschreibung = null, Wert = "50" },
                new Model.Einstellung() { Name = "GeneralGeräuscheVolume", Kontext = "Audioplayer", Kategorie = null, Typ = "Integer", Beschreibung = null, Wert = "100" },
                new Model.Einstellung() { Name = "GeneralHotkeyVolume", Kontext = "Audioplayer", Kategorie = null, Typ = "Integer", Beschreibung = null, Wert = "50" },
                
                new Model.Einstellung() { Name = "PdfReaderCommand", Kontext = "Almanach", Kategorie = null, Typ = "String", Beschreibung = "Befehl zum starten des PDF-Readers", Wert = null },
                new Model.Einstellung() { Name = "PdfReaderArguments", Kontext = "Almanach", Kategorie = null, Typ = "String", Beschreibung = "Parameter für den Aufruf des PDF-Readers", Wert = null },

                new Model.Einstellung() { Name = "FrageNeueKampfrundeAbstellen", Kontext = "Kampf", Kategorie = null, Typ = "Boolean", Beschreibung = "Frage bei neuer Kampfrunde unterbinden", Wert = "False" },
                new Model.Einstellung() { Name = "TPKK", Kontext = "Kampf", Kategorie = "Optional", Typ = "Boolean", Beschreibung = "Trefferpunkte und Körperkraft (TP/KK) (WdS 81f)", Wert = "True" },
                new Model.Einstellung() { Name = "NiedrigeLE", Kontext = "Kampf", Kategorie = "Optional", Typ = "Boolean", Beschreibung = "Auswirkungen niedriger LE (WdS 57)", Wert = "True" },
                new Model.Einstellung() { Name = "NiedrigeAU", Kontext = "Kampf", Kategorie = "Optional", Typ = "Boolean", Beschreibung = "Auswirkungen niedriger AU (WdS 83)", Wert = "True" },
                new Model.Einstellung() { Name = "AusdauerImKampf", Kontext = "Kampf", Kategorie = "Optional", Typ = "Boolean", Beschreibung = "Ausdauerverlust (WdS 83)", Wert = "True" },
                new Model.Einstellung() { Name = "NurDreiZonenWunden", Kontext = "Kampf", Kategorie = "Hausregel", Typ = "Boolean", Beschreibung = "Maximal drei Wunden pro Trefferzone", Wert = "True" },
                new Model.Einstellung() { Name = "WundenVerändernWundschwelle", Kontext = "Kampf", Kategorie = "Unklarheit", Typ = "Boolean", Beschreibung = "Veränderungen der KO durch Wunden beinflussen die Wundschwellen", Wert = "True" },
                
                new Model.Einstellung() { Name = "EigenschaftenProbePatzerGlück", Kontext = "Proben", Kategorie = "Optional", Typ = "Boolean", Beschreibung = "Patzer und Glückswürfe bei Eigenschafts-Proben (WdS 7)", Wert = "True" },
                new Model.Einstellung() { Name = "WuerfelSoundAbspielen", Kontext = "Proben", Kategorie = null, Typ = "Boolean", Beschreibung = "Würfelsound abspielen", Wert = "True" },
                                
                new Model.Einstellung() { Name = "01_RSBerechnung", Kontext = "Inventar", Kategorie = null, Typ = "Integer", Beschreibung = "Wie wird die Rüstung eines Helden ermittelt?", Wert = "0" },
                new Model.Einstellung() { Name = "02_BEBerechnung", Kontext = "Inventar", Kategorie = null, Typ = "Integer", Beschreibung = "Wie wird die Behinderung eines Helden ermittelt?", Wert = "0" },
                new Model.Einstellung() { Name = "03_UeberlastungBerechnung", Kontext = "Inventar", Kategorie = null, Typ = "Integer", Beschreibung = "Spielt die Gruppe mit Überlastung?", Wert = "0" },
                new Model.Einstellung() { Name = "MitUeberlastung", Kontext = "Inventar", Kategorie = "Optional", Typ = "Boolean", Beschreibung = "(Optional) Mit Ueberlastung spielen?", Wert = "True" },
                
                new Model.Einstellung() { Name = "ToolTitelAusblenden", Kontext = "Allgemein", Kategorie = null, Typ = "Boolean", Beschreibung = "Tool Name im Tab-Titel ausblenden", Wert = "False" },

                new Model.Einstellung() { Name = "CheckForUpdates", Kontext = "Allgemein", Kategorie = null, Typ = "Boolean", Beschreibung = "Täglich nach neuen Updates suchen", Wert = "True" },
                new Model.Einstellung() { Name = "LastUpdateCheck", Kontext = "Allgemein", Kategorie = "Versteckt", Typ = "String", Beschreibung = "", Wert = DateTime.Now.ToString() },

                //Versteckte
                new Model.Einstellung() { Name = "IsReadOnly", Kontext = "Allgemein", Kategorie = "Versteckt", Typ = "Boolean", Beschreibung = "", Wert = "False" },
                new Model.Einstellung() { Name = "Standort", Kontext = "Allgemein", Kategorie = "Versteckt", Typ = "String", Beschreibung = "", Wert = "Gareth#29.79180235685203#3.735098459067687" },
                new Model.Einstellung() { Name = "SelectedHeld", Kontext = "Helden", Kategorie = "Versteckt", Typ = "String", Beschreibung = "", Wert = null },
                new Model.Einstellung() { Name = "ProbenFavoriten", Kontext = "Proben", Kategorie = "Versteckt", Typ = "String", Beschreibung = "", Wert = null },
                new Model.Einstellung() { Name = "KampfRecentColors", Kontext = "Kampf", Kategorie = "Versteckt", Typ = "String", Beschreibung = "", Wert = null },
                new Model.Einstellung() { Name = "SelectedTab", Kontext = "Allgemein", Kategorie = "Versteckt", Typ = "Integer", Beschreibung = "", Wert = "0" },
                new Model.Einstellung() { Name = "SelectedHeldenTab", Kontext = "Helden", Kategorie = "Versteckt", Typ = "Integer", Beschreibung = "", Wert = "0" },
                new Model.Einstellung() { Name = "StartTabs", Kontext = "Allgemein", Kategorie = "Versteckt", Typ = "String", Beschreibung = "", Wert = "" },
                new Model.Einstellung() { Name = "KalenderExpandedSections", Kontext = "Kalender", Kategorie = "Versteckt", Typ = "String", Beschreibung = "", Wert = "111111" },
                new Model.Einstellung() { Name = "ProbenAnzeigeModus", Kontext = "Proben", Kategorie = "Versteckt", Typ = "String", Beschreibung = "", Wert = "Zeile" },
                new Model.Einstellung() { Name = "DatumAktuell", Kontext = "Kalender", Kategorie = "Versteckt", Typ = "String", Beschreibung = "", Wert = "1|0|993|0" },
                new Model.Einstellung() { Name = "UmrechnerExpandedSections", Kontext = "Umrechner", Kategorie = "Versteckt", Typ = "String", Beschreibung = "", Wert = "111111" },
                new Model.Einstellung() { Name = "GegnerViewExpandedSections", Kontext = "Gegner", Kategorie = "Versteckt", Typ = "String", Beschreibung = "", Wert = "11" },
                new Model.Einstellung() { Name = "GegnerDetailViewExpandedSections", Kontext = "Gegner", Kategorie = "Versteckt", Typ = "String", Beschreibung = "", Wert = "110" },
                new Model.Einstellung() { Name = "SpielerInfoBilderPfad", Kontext = "SpielerInfo", Kategorie = "Versteckt", Typ = "String", Beschreibung = "", Wert = string.Empty },
                new Model.Einstellung() { Name = "INTERN", Kontext = "Allgemein", Kategorie = "Versteckt", Typ = "Boolean", Beschreibung = "", Wert = "False" },
                new Model.Einstellung() { Name = "SlideShowInterval", Kontext = "SpielerInfo", Kategorie = "Versteckt", Typ = "Double", Beschreibung = "", Wert = "6" },
                new Model.Einstellung() { Name = "MeisterGeisterID", Kontext = "Allgemein", Kategorie = "Versteckt", Typ = "String", Beschreibung = "", Wert = Guid.NewGuid().ToString() }
            };
        }

        private static WeakEvent<EinstellungChangedHandler> _einstellungChanged = new WeakEvent<EinstellungChangedHandler>();
        public static event EinstellungChangedHandler EinstellungChanged
        {
            add { _einstellungChanged.Add(value); }
            remove { _einstellungChanged.Remove(value); }
        }

        private static void OnEinstellungChanged(string propertyName, string einstellungName)
        {
            _einstellungChanged.Raise(null, new EinstellungChangedEventArgs(propertyName, einstellungName));
        }

        private static Dictionary<string, Model.Einstellung> defaultValues = null;
        private static Dictionary<string, Model.Einstellung> DefaultValues
        {
            get { 
                if(defaultValues == null && Global.IsInitialized)
                {
                    defaultValues = new Dictionary<string, Model.Einstellung>();
                    foreach(var e in GetDefaults())
                        defaultValues.Add(e.Name, e);
                }
                return defaultValues; 
            }
        }

        public static T GetEinstellung<T>(string name)
        {
            if (Global.IsInitialized)
            {
                Model.Einstellung e = Global.ContextHeld.LoadEinstellungByName(name);
                if (e == null)
                    e = CreateEinstellung<T>(name);
                return e.Get<T>();
            }
            return default(T);
        }

        public static Model.Einstellung CreateEinstellung<T>(string name)
        {
            var e = Global.ContextHeld.New<Model.Einstellung>();
            CopyDefaultValues(name, e, true);
            Global.ContextHeld.Insert<Model.Einstellung>(e);
            return e;
        }

        private static void CopyDefaultValues(string name, Model.Einstellung target, bool mitWert = false)
        {
            var source = DefaultValues[name];
            target.Typ = source.Typ;
            target.Kontext = source.Kontext;
            target.Kategorie = source.Kategorie;
            target.Name = source.Name;
            target.Beschreibung = source.Beschreibung;
            if(mitWert) 
                target.Wert = source.Wert;
        }

        public static Model.Einstellung SetEinstellung<T>(string name, T value, [CallerMemberName] String propertyName = null)
        {
            if (Global.IsInitialized)
            {
                Model.Einstellung e = Global.ContextHeld.LoadEinstellungByName(name);
                if (e == null)
                    e = CreateEinstellung<T>(name);
                e.Set<T>(value);
                Global.ContextHeld.Update<Model.Einstellung>(e);
                OnEinstellungChanged(propertyName, name);
                return e;
            }
            return null;
        }

        // TODO: Einstellung wird gecached, um Absturz bei Proben zu verhindern. Da sich dadurch die Einstellung nach Änderung ggf. nicht mehr aktuell sein könnte, sollte das Caching noch überarbeitet werden.
        private static Nullable<bool> _cached_IsMitUeberlastung = null;
        public static bool IsMitUeberlastung
        {
            get
            {
                if (_cached_IsMitUeberlastung == null)
                    _cached_IsMitUeberlastung = GetEinstellung<bool>("MitUeberlastung");
                return _cached_IsMitUeberlastung.GetValueOrDefault();
            }
            set
            {
                SetEinstellung<bool>("MitUeberlastung", value);
                _cached_IsMitUeberlastung = value;
            }
        }

        public static int RSBerechnung
        {
            get
            {
                return GetEinstellung<int>("01_RSBerechnung");
            }
            set
            {
                SetEinstellung<int>("01_RSBerechnung", value);
            }
        }

        public static int BEBerechnung
        {
            get
            {
                return GetEinstellung<int>("02_BEBerechnung");
            }
            set
            {
                SetEinstellung<int>("02_BEBerechnung", value);
            }
        }

        // TODO: Einstellung wird gecached, um Absturz bei Poben zu verhindern. Da sich dadurch die Einstellung nach Änderung ggf. nicht mehr aktuell sein könnte, sollte das Caching noch überarbeitet werden.
        private static Nullable<int> _cached_UeberlastungBerechnung = null;
        public static int UeberlastungBerechnung
        {
            get
            {
                if (_cached_UeberlastungBerechnung == null)
                    _cached_UeberlastungBerechnung = GetEinstellung<int>("03_UeberlastungBerechnung");
                return _cached_UeberlastungBerechnung.GetValueOrDefault();
            }
            set
            {
                SetEinstellung<int>("03_UeberlastungBerechnung", value);
                _cached_UeberlastungBerechnung = value;
            }
        }

        public static bool FrageNeueKampfrundeAbstellen
        {
            get
            {
                return GetEinstellung<bool>("FrageNeueKampfrundeAbstellen");
            }
            set
            {
                SetEinstellung<bool>("FrageNeueKampfrundeAbstellen", value);
            }
        }

        public static bool NurDreiZonenWunden
        {
            get
            {
                return GetEinstellung<bool>("NurDreiZonenWunden");
            }
            set
            {
                SetEinstellung<bool>("NurDreiZonenWunden", value);
            }
        }

        // TODO: Einstellung wird gecached, um Absturz bei Poben zu verhindern. Da sich dadurch die Einstellung nach Änderung ggf. nicht mehr aktuell sein könnte, sollte das Caching noch überarbeitet werden.
        private static Nullable<bool> _cached_WundenVerändernWundschwelle = null;
        public static bool WundenVerändernWundschwelle
        {
            get
            {
                if (_cached_WundenVerändernWundschwelle == null)
                    _cached_WundenVerändernWundschwelle = GetEinstellung<bool>("WundenVerändernWundschwelle");
                return _cached_WundenVerändernWundschwelle.GetValueOrDefault();
            }
            set
            {
                SetEinstellung<bool>("WundenVerändernWundschwelle", value);
                _cached_WundenVerändernWundschwelle = value;
            }
        }

        // TODO: Einstellung wird gecached, um Absturz verhindern. Da sich dadurch die Einstellung nach Änderung ggf. nicht mehr aktuell sein könnte, sollte das Caching noch überarbeitet werden.
        private static Nullable<bool> _checkForUpdates = null;
        public static bool CheckForUpdates
        {
            get
            {
                if (_checkForUpdates == null)
                    _checkForUpdates = GetEinstellung<bool>("CheckForUpdates");
                return _checkForUpdates.GetValueOrDefault();
            }
            set
            {
                SetEinstellung<bool>("CheckForUpdates", value);
                _checkForUpdates = value;
            }
        }

        // TODO: Einstellung wird gecached, um Absturz verhindern. Da sich dadurch die Einstellung nach Änderung ggf. nicht mehr aktuell sein könnte, sollte das Caching noch überarbeitet werden.
        private static string _lastUpdateCheck = null;
        public static string LastUpdateCheck
        {
            get
            {
                if (_lastUpdateCheck == null)
                    _lastUpdateCheck = GetEinstellung<string>("LastUpdateCheck");
                return _lastUpdateCheck;
            }
            set
            {
                SetEinstellung<string>("LastUpdateCheck", value);
                _lastUpdateCheck = value;
            }
        }

        public static bool JingleAbstellen
        {
            get
            {
                if (Global.IsInitialized)
                    return GetEinstellung<bool>("JingleAbstellen");
                try
                {
                    return Convert.ToBoolean(Daten.DatabaseUpdate.GetScalarFromDatabase("SELECT Wert FROM Einstellung WHERE Name = 'JingleAbstellen'", 
                        MeisterGeister.Properties.Settings.Default.DatabaseDSAConnectionString));
                }
                catch (Exception) { ; }

                return false;
            }
            set
            {
                SetEinstellung<bool>("JingleAbstellen", value);
            }
        }

        public static bool WuerfelSoundAbspielen
        {
            get
            {
                return GetEinstellung<bool>("WuerfelSoundAbspielen");
            }
            set
            {
                SetEinstellung<bool>("WuerfelSoundAbspielen", value);
                RaiseWuerfelSoundAbspielenChanged();
            }
        }

        private static WeakEvent<EventHandler> _wuerfelSoundAbspielenChanged = new WeakEvent<EventHandler>();
        public static event EventHandler WuerfelSoundAbspielenChanged
        {
            add { _wuerfelSoundAbspielenChanged.Add(value); }
            remove { _wuerfelSoundAbspielenChanged.Remove(value); }
        }

        public static void RaiseWuerfelSoundAbspielenChanged(object sender = null, EventArgs args = null)
        {
            _wuerfelSoundAbspielenChanged.Raise(sender, args??EventArgs.Empty);
        }

        public static string AudioVerzeichnis
        {
            get
            {
                return GetEinstellung<string>("AudioVerzeichnis");
            }
            set
            {
                SetEinstellung<string>("AudioVerzeichnis", value);
            }
        }

        public static bool AudioSpieldauerBerechnen
        {
            get
            {
                return GetEinstellung<bool>("AudioSpieldauerBerechnen");
            }
            set
            {
                SetEinstellung<bool>("AudioSpieldauerBerechnen", value);
            }
        }

        public static bool AudioInAnderemPfadSuchen
        {
            get
            {
                return GetEinstellung<bool>("AudioInAnderemPfadSuchen");
            }
            set
            {
                SetEinstellung<bool>("AudioInAnderemPfadSuchen", value);
            }
        }

        public static int GeneralMusikVolume
        {
            get
            {
                return GetEinstellung<int>("GeneralMusikVolume");
            }
            set
            {
                SetEinstellung<int>("GeneralMusikVolume", value);
            }
        }

        public static int GeneralGeräuscheVolume
        {
            get
            {
                return GetEinstellung<int>("GeneralGeräuscheVolume");
            }
            set
            {
                SetEinstellung<int>("GeneralGeräuscheVolume", value);
            }
        }

        public static int GeneralHotkeyVolume
        {
            get
            {
                return GetEinstellung<int>("GeneralHotkeyVolume");
            }
            set
            {
                SetEinstellung<int>("GeneralHotkeyVolume", value);
            }
        }

        public static int Fading
        {
            get
            {
                return GetEinstellung<int>("Fading");
            }
            set
            {
                SetEinstellung<int>("Fading", value);
                RaiseFading_Click();
            }
        }
        private static WeakEvent<EventHandler> _fading_Click = new WeakEvent<EventHandler>();
        public static event EventHandler Fading_Click
        {
            add { _fading_Click.Add(value); }
            remove { _fading_Click.Remove(value); }
        }

        public static void RaiseFading_Click(object sender = null, EventArgs args = null)
        {
            _fading_Click.Raise(sender, args ?? EventArgs.Empty);
        }
        

        public static int SelectedTab
        {
            get
            {
                return GetEinstellung<int>("SelectedTab");
            }
            set
            {
                SetEinstellung<int>("SelectedTab", value);
            }
        }

        public static string StartTabs
        {
            get
            {
                return GetEinstellung<string>("StartTabs");
            }
            set
            {
                SetEinstellung<string>("StartTabs", value);
            }
        }

        public static string KalenderExpandedSections
        {
            get
            {
                return GetEinstellung<string>("KalenderExpandedSections");
            }
            set
            {
                SetEinstellung<string>("KalenderExpandedSections", value);
            }
        }

        public static string ProbenAnzeigeModus
        {
            get
            {
                return GetEinstellung<string>("ProbenAnzeigeModus");
            }
            set
            {
                SetEinstellung<string>("ProbenAnzeigeModus", value);
            }
        }

        public static string DatumAktuell
        {
            get
            {
                return GetEinstellung<string>("DatumAktuell");
            }
            set
            {
                SetEinstellung<string>("DatumAktuell", value);
            }
        }

        public static string UmrechnerExpandedSections
        {
            get
            {
                return GetEinstellung<string>("UmrechnerExpandedSections");
            }
            set
            {
                SetEinstellung<string>("UmrechnerExpandedSections", value);
            }
        }

        public static string GegnerViewExpandedSections
        {
            get
            {
                return GetEinstellung<string>("GegnerViewExpandedSections");
            }
            set
            {
                SetEinstellung<string>("GegnerViewExpandedSections", value);
            }
        }

        public static string GegnerDetailViewExpandedSections
        {
            get
            {
                return GetEinstellung<string>("GegnerDetailViewExpandedSections");
            }
            set
            {
                SetEinstellung<string>("GegnerDetailViewExpandedSections", value);
            }
        }
        
        public static string Standort
        {
            get
            {
                string s = GetEinstellung<string>("Standort");
                // TODO MT: Diese Syntax-Prüfung sollte in dem DG-Suche-PlugIn erfolgen, wenn eine Ortsmarke erstellt wird.
                return string.IsNullOrEmpty(s) ? "Gareth#29.79180235685203#3.735098459067687" : s;
            }
            set
            {
                SetEinstellung<string>("Standort", value);
            }
        }

        public static string SelectedHeld
        {
            get
            {
                return GetEinstellung<string>("SelectedHeld");
            }
            set
            {
                SetEinstellung<string>("SelectedHeld", value);
            }
        }

        public static int HeldenSelectedTab
        {
            get
            {
                return GetEinstellung<int>("SelectedHeldenTab");
            }
            set
            {
                SetEinstellung<int>("SelectedHeldenTab", value);
            }
        }

        public static string ProbenFavoriten
        {
            get
            {
                return GetEinstellung<string>("ProbenFavoriten");
            }
            set
            {
                SetEinstellung<string>("ProbenFavoriten", value);
            }
        }

        public static string PdfReaderCommand
        {
            get
            {
                return GetEinstellung<string>("PdfReaderCommand");
            }
            set
            {
                SetEinstellung<string>("PdfReaderCommand", value);
            }
        }
        
        public static string PdfReaderArguments
        {
            get
            {
                return GetEinstellung<string>("PdfReaderArguments");
            }
            set
            {
                SetEinstellung<string>("PdfReaderArguments", value);
            }
        }

        public static string SpielerInfoBilderPfad
        {
            get
            {
                return GetEinstellung<string>("SpielerInfoBilderPfad");
            }
            set
            {
                SetEinstellung<string>("SpielerInfoBilderPfad", value);
            }
        }

        public static double SlideShowInterval
        {
            get
            {
                return GetEinstellung<double>("SlideShowInterval");
            }
            set
            {
                SetEinstellung<double>("SlideShowInterval", value);
            }
        }

        public static bool ToolTitelAusblenden
        {
            get
            {
                return GetEinstellung<bool>("ToolTitelAusblenden");
            }
            set
            {
                SetEinstellung<bool>("ToolTitelAusblenden", value);
                RaiseToolTitelAusblendenChanged();
            }
        }

        private static WeakEvent<EventHandler> _toolTitelAusblendenChanged = new WeakEvent<EventHandler>();
        public static event EventHandler ToolTitelAusblendenChanged
        {
            add { _toolTitelAusblendenChanged.Add(value); }
            remove { _toolTitelAusblendenChanged.Remove(value); }
        }

        public static void RaiseToolTitelAusblendenChanged(object sender = null, EventArgs args = null)
        {
            _toolTitelAusblendenChanged.Raise(sender, args ?? EventArgs.Empty);
        }


        /// <summary>
        /// Gibt an, ob sich MeisterGeister im Nur-Lesen-Modus befindet.
        /// Ist der Schreibschutz aktiviert, werden bestimmte Eingabe- und Änderungsmöglichkeiten eingeschränkt.
        /// </summary>
        public static bool IsReadOnly
        {
            get
            {
                return GetEinstellung<bool>("IsReadOnly");
            }
            set
            {
                SetEinstellung<bool>("IsReadOnly", value);
                RaiseIsReadOnlyChanged();
            }
        }

        private static WeakEvent<EventHandler> _isReadOnlyChanged = new WeakEvent<EventHandler>();
        public static event EventHandler IsReadOnlyChanged
        {
            add { _isReadOnlyChanged.Add(value); }
            remove { _isReadOnlyChanged.Remove(value); }
        }

        public static void RaiseIsReadOnlyChanged(object sender = null, EventArgs args = null)
        {
            _isReadOnlyChanged.Raise(sender, args ?? EventArgs.Empty);
        }

        public static string KampfRecentColors
        {
            get
            {
                return GetEinstellung<string>("KampfRecentColors");
            }
            set
            {
                SetEinstellung<string>("KampfRecentColors", value);
            }
        }

        public static bool ShowChangeLog
        {
            get
            {
                return GetEinstellung<bool>("ShowChangeLog");
            }
            set
            {
                SetEinstellung<bool>("ShowChangeLog", value);
            }
        }

        // TODO: Einstellung wird gecached, um Absturz verhindern. Da sich dadurch die Einstellung nach Änderung ggf. nicht mehr aktuell sein könnte, sollte das Caching noch überarbeitet werden.
        private static Nullable<Guid> _meisterGeisterID = null;
        public static Guid MeisterGeisterID
        {
            get
            {
                if (_meisterGeisterID == null)
                    _meisterGeisterID = Guid.Parse(GetEinstellung<string>("MeisterGeisterID"));
                if (_meisterGeisterID == Guid.Empty)
                { // MeisterGeisterID ist 00000000-0000-0000-0000-000000000000, also die Standard-Datenbank -> Neue ID erzeugen
                    _meisterGeisterID = Guid.NewGuid();
                    MeisterGeisterID = _meisterGeisterID.GetValueOrDefault();
                }
                return _meisterGeisterID.GetValueOrDefault();
            }
            set
            {
                SetEinstellung<string>("MeisterGeisterID", value.ToString());
                _meisterGeisterID = value;
            }
        }
    }
}
