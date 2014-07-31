using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

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
                new Model.Einstellung() { Name = "INTERN", Kontext = "Allgemein", Kategorie = "Versteckt", Typ = "Boolean", Beschreibung = "", Wert = "False" }
            };
        }

        public static event EinstellungChangedHandler EinstellungChanged;
        private static void OnEinstellungChanged(string propertyName, string einstellungName)
        {
            if (EinstellungChanged != null)
                EinstellungChanged(new EinstellungChangedEventArgs(propertyName, einstellungName));
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

        public static bool INTERN
        {
            get
            {
                return GetEinstellung<bool>("INTERN");
            }
            set
            {
                string pwd = View.General.ViewHelper.InputDialog("Passwort", "Passwort für INTERN Modus eingeben.", string.Empty);
                if (Global.Intern_CheckPwd(pwd))
                    SetEinstellung<bool>("INTERN", value);
                else
                    SetEinstellung<bool>("INTERN", false);
            }
        }

        // TODO: Einstellung wird gecached, um Absturz bei Poben zu verhindern. Da sich dadurch die Einstellung nach Änderung ggf. nicht mehr aktuell sein könnte, sollte das Caching noch überarbeitet werden.
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

        public static bool WundenVerändernWundschwelle
        {
            get
            {
                return GetEinstellung<bool>("WundenVerändernWundschwelle");
            }
            set
            {
                SetEinstellung<bool>("WundenVerändernWundschwelle", value);
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
                if (WuerfelSoundAbspielenChanged != null)
                    WuerfelSoundAbspielenChanged(null, new EventArgs());
            }
        }

        public static EventHandler WuerfelSoundAbspielenChanged;


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

        public static int Fading
        {
            get
            {
                return GetEinstellung<int>("Fading");
            }
            set
            {
                SetEinstellung<int>("Fading", value);
                if (Fading_Click != null)
                    Fading_Click(null, new EventArgs());
            }
        }
        public static EventHandler Fading_Click;
        

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

        public static bool ToolTitelAusblenden
        {
            get
            {
                return GetEinstellung<bool>("ToolTitelAusblenden");
            }
            set
            {
                SetEinstellung<bool>("ToolTitelAusblenden", value);
                if (ToolTitelAusblendenChanged != null)
                    ToolTitelAusblendenChanged(null, new EventArgs());
            }
        }

        public static EventHandler ToolTitelAusblendenChanged;


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
                if (IsReadOnlyChanged != null)
                    IsReadOnlyChanged(null, new EventArgs());
            }
        }

        public static EventHandler IsReadOnlyChanged;

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
    }
}
