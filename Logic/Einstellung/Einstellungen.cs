using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MeisterGeister.Logic.General;

namespace MeisterGeister.Logic.Einstellung
{
    public static class Einstellungen
    {
        public static event EinstellungChangedHandler EinstellungChanged
        {
            add { _einstellungChanged.Add(value); }
            remove { _einstellungChanged.Remove(value); }
        }

        public static event EventHandler WuerfelSoundAbspielenChanged
        {
            add { _wuerfelSoundAbspielenChanged.Add(value); }
            remove { _wuerfelSoundAbspielenChanged.Remove(value); }
        }

        public static event EventHandler Fading_Click
        {
            add { _fading_Click.Add(value); }
            remove { _fading_Click.Remove(value); }
        }

        public static event EventHandler ToolTitelAusblendenChanged
        {
            add { _toolTitelAusblendenChanged.Add(value); }
            remove { _toolTitelAusblendenChanged.Remove(value); }
        }

        public static event EventHandler IsReadOnlyChanged
        {
            add { _isReadOnlyChanged.Add(value); }
            remove { _isReadOnlyChanged.Remove(value); }
        }

        public static bool MitUeberlastung
        {
            get
            {
                if (_cached_IsMitUeberlastung == null)
                {
                    _cached_IsMitUeberlastung = GetEinstellung<bool>(nameof(MitUeberlastung));
                }

                return _cached_IsMitUeberlastung.GetValueOrDefault();
            }

            set
            {
                SetEinstellung(nameof(MitUeberlastung), value);
                _cached_IsMitUeberlastung = value;
            }
        }

        /// <summary>
        /// 0 = Automatisch Zonen, 1 = Einfach, 2 = Zonen, 3 = Automatisch Zonen
        /// </summary>
        public static int RSBerechnung
        {
            get
            {
                return GetEinstellung<int>($"01_{nameof(RSBerechnung)}");
            }

            set
            {
                SetEinstellung($"01_{nameof(RSBerechnung)}", value);
            }
        }

        public static int BEBerechnung
        {
            get
            {
                return GetEinstellung<int>($"02_{nameof(BEBerechnung)}");
            }

            set
            {
                SetEinstellung($"02_{nameof(BEBerechnung)}", value);
            }
        }

        public static int UeberlastungBerechnung
        {
            get
            {
                if (_cached_UeberlastungBerechnung == null)
                {
                    _cached_UeberlastungBerechnung = GetEinstellung<int>($"03_{nameof(UeberlastungBerechnung)}");
                }

                return _cached_UeberlastungBerechnung.GetValueOrDefault();
            }

            set
            {
                SetEinstellung($"03_{nameof(UeberlastungBerechnung)}", value);
                _cached_UeberlastungBerechnung = value;
            }
        }

        public static bool FrageNeueKampfrundeAbstellen
        {
            get
            {
                return GetEinstellung<bool>(nameof(FrageNeueKampfrundeAbstellen));
            }

            set
            {
                SetEinstellung(nameof(FrageNeueKampfrundeAbstellen), value);
            }
        }

        public static bool AngriffAutomatischWürfeln
        {
            get
            {
                return GetEinstellung<bool>(nameof(AngriffAutomatischWürfeln));
            }

            set
            {
                SetEinstellung(nameof(AngriffAutomatischWürfeln), value);
            }
        }

        public static bool LebensbalkenImmerAnzeigen
        {
            get
            {
                return GetEinstellung<bool>(nameof(LebensbalkenImmerAnzeigen));
            }

            set
            {
                SetEinstellung(nameof(LebensbalkenImmerAnzeigen), value);
            }
        }

        public static bool NurDreiZonenWunden
        {
            get
            {
                return GetEinstellung<bool>(nameof(NurDreiZonenWunden));
            }

            set
            {
                SetEinstellung(nameof(NurDreiZonenWunden), value);
            }
        }

        public static bool WundenVerändernWundschwelle
        {
            get
            {
                if (_cached_WundenVerändernWundschwelle == null)
                {
                    _cached_WundenVerändernWundschwelle = GetEinstellung<bool>(nameof(WundenVerändernWundschwelle));
                }

                return _cached_WundenVerändernWundschwelle.GetValueOrDefault();
            }

            set
            {
                SetEinstellung(nameof(WundenVerändernWundschwelle), value);
                _cached_WundenVerändernWundschwelle = value;
            }
        }

        public static bool CheckForUpdates
        {
            get
            {
                if (_checkForUpdates == null)
                {
                    _checkForUpdates = GetEinstellung<bool>(nameof(CheckForUpdates));
                }

                return _checkForUpdates.GetValueOrDefault();
            }

            set
            {
                SetEinstellung(nameof(CheckForUpdates), value);
                _checkForUpdates = value;
            }
        }

        public static string LastUpdateCheck
        {
            get
            {
                if (_lastUpdateCheck == null)
                {
                    _lastUpdateCheck = GetEinstellung<string>(nameof(LastUpdateCheck));
                }

                return _lastUpdateCheck;
            }

            set
            {
                SetEinstellung(nameof(LastUpdateCheck), value);
                _lastUpdateCheck = value;
            }
        }

        public static string HeldenSoftwareOnlineURL
        {
            get
            {
                if (_heldenSoftwareOnlineURL == null)
                {
                    _heldenSoftwareOnlineURL = GetEinstellung<string>(nameof(HeldenSoftwareOnlineURL));
                }

                return _heldenSoftwareOnlineURL;
            }

            set
            {
                SetEinstellung(nameof(HeldenSoftwareOnlineURL), value);
                _heldenSoftwareOnlineURL = value;
            }
        }

        public static string HeldenSoftwareOnlineToken
        {
            get
            {
                if (_heldenSoftwareOnlineToken == null)
                {
                    _heldenSoftwareOnlineToken = GetEinstellung<string>(nameof(HeldenSoftwareOnlineToken));
                }

                return _heldenSoftwareOnlineToken;
            }

            set
            {
                SetEinstellung(nameof(HeldenSoftwareOnlineToken), value);
                _heldenSoftwareOnlineToken = value;
            }
        }

        public static string Regeledition
        {
            get
            {
                if (_regeledition == null)
                {
                    _regeledition = GetEinstellung<string>(nameof(Regeledition));
                }

                return _regeledition;
            }

            set
            {
                SetEinstellung(nameof(Regeledition), value);
                _regeledition = value;
            }
        }

        public static bool JingleAbstellen
        {
            get
            {
                if (Global.IsInitialized)
                {
                    return GetEinstellung<bool>(nameof(JingleAbstellen));
                }

                try
                {
                    return Convert.ToBoolean(Daten.DatabaseUpdate.GetScalarFromDatabase($"SELECT Wert FROM Einstellung WHERE Name = '{nameof(JingleAbstellen)}'",
                        Properties.Settings.Default.DatabaseDSAConnectionString));
                }
                catch (Exception) {; }

                return false;
            }

            set
            {
                SetEinstellung(nameof(JingleAbstellen), value);
            }
        }

        public static bool WuerfelSoundAbspielen
        {
            get
            {
                return GetEinstellung<bool>(nameof(WuerfelSoundAbspielen));
            }

            set
            {
                SetEinstellung(nameof(WuerfelSoundAbspielen), value);
                RaiseWuerfelSoundAbspielenChanged();
            }
        }

        public static bool PflanzenwissenIntegrieren
        {
            get
            {
                return GetEinstellung<bool>(nameof(PflanzenwissenIntegrieren));
            }

            set
            {
                SetEinstellung(nameof(PflanzenwissenIntegrieren), value);
            }
        }

        public static string AudioVerzeichnis
        {
            get
            {
                return GetEinstellung<string>(nameof(AudioVerzeichnis));
            }

            set
            {
                SetEinstellung(nameof(AudioVerzeichnis), value);
            }
        }

        public static bool AudioSpieldauerBerechnen
        {
            get
            {
                return GetEinstellung<bool>(nameof(AudioSpieldauerBerechnen));
            }

            set
            {
                SetEinstellung(nameof(AudioSpieldauerBerechnen), value);
            }
        }

        public static bool AudioInAnderemPfadSuchen
        {
            get
            {
                return GetEinstellung<bool>(nameof(AudioInAnderemPfadSuchen));
            }

            set
            {
                SetEinstellung(nameof(AudioInAnderemPfadSuchen), value);
            }
        }

        public static bool ShowPlaylistFavorite
        {
            get
            {
                return GetEinstellung<bool>(nameof(ShowPlaylistFavorite));
            }

            set
            {
                SetEinstellung(nameof(ShowPlaylistFavorite), value);
            }
        }

        public static int GeneralMusikVolume
        {
            get
            {
                return GetEinstellung<int>(nameof(GeneralMusikVolume));
            }

            set
            {
                SetEinstellung(nameof(GeneralMusikVolume), value);
            }
        }

        public static int GeneralGeräuscheVolume
        {
            get
            {
                return GetEinstellung<int>(nameof(GeneralGeräuscheVolume));
            }

            set
            {
                SetEinstellung(nameof(GeneralGeräuscheVolume), value);
            }
        }

        public static int GeneralHotkeyVolume
        {
            get
            {
                return GetEinstellung<int>(nameof(GeneralHotkeyVolume));
            }

            set
            {
                SetEinstellung(nameof(GeneralHotkeyVolume), value);
            }
        }

        public static int Fading
        {
            get
            {
                return GetEinstellung<int>(nameof(Fading));
            }

            set
            {
                SetEinstellung(nameof(Fading), value);
                RaiseFading_Click();
            }
        }

        public static int SelectedTab
        {
            get
            {
                return GetEinstellung<int>(nameof(SelectedTab));
            }

            set
            {
                SetEinstellung(nameof(SelectedTab), value);
            }
        }

        public static string StartTabs
        {
            get
            {
                return GetEinstellung<string>(nameof(StartTabs));
            }

            set
            {
                SetEinstellung(nameof(StartTabs), value);
            }
        }

        public static string KalenderExpandedSections
        {
            get
            {
                return GetEinstellung<string>(nameof(KalenderExpandedSections));
            }

            set
            {
                SetEinstellung(nameof(KalenderExpandedSections), value);
            }
        }

        public static string ProbenAnzeigeModus
        {
            get
            {
                return GetEinstellung<string>(nameof(ProbenAnzeigeModus));
            }

            set
            {
                SetEinstellung(nameof(ProbenAnzeigeModus), value);
            }
        }

        public static string DatumAktuell
        {
            get
            {
                return GetEinstellung<string>(nameof(DatumAktuell));
            }

            set
            {
                SetEinstellung(nameof(DatumAktuell), value);
            }
        }

        public static string UmrechnerExpandedSections
        {
            get
            {
                return GetEinstellung<string>(nameof(UmrechnerExpandedSections));
            }

            set
            {
                SetEinstellung(nameof(UmrechnerExpandedSections), value);
            }
        }

        public static string GegnerViewExpandedSections
        {
            get
            {
                return GetEinstellung<string>(nameof(GegnerViewExpandedSections));
            }

            set
            {
                SetEinstellung(nameof(GegnerViewExpandedSections), value);
            }
        }

        public static string GegnerDetailViewExpandedSections
        {
            get
            {
                return GetEinstellung<string>(nameof(GegnerDetailViewExpandedSections));
            }

            set
            {
                SetEinstellung(nameof(GegnerDetailViewExpandedSections), value);
            }
        }

        public static string Standort
        {
            get
            {
                var s = GetEinstellung<string>(nameof(Standort));
                // TODO MT: Diese Syntax-Prüfung sollte in dem DG-Suche-PlugIn erfolgen, wenn eine
                // Ortsmarke erstellt wird.
                return (string.IsNullOrEmpty(s) || s == "##") ? "Gareth#29.79180235685203#3.735098459067687" : s;
            }

            set
            {
                SetEinstellung(nameof(Standort), value);
            }
        }

        public static string SelectedHeld
        {
            get
            {
                return GetEinstellung<string>(nameof(SelectedHeld));
            }

            set
            {
                SetEinstellung(nameof(SelectedHeld), value);
            }
        }

        public static int SelectedHeldenTab
        {
            get
            {
                return GetEinstellung<int>(nameof(SelectedHeldenTab));
            }

            set
            {
                SetEinstellung(nameof(SelectedHeldenTab), value);
            }
        }

        public static string ProbenFavoriten
        {
            get
            {
                return GetEinstellung<string>(nameof(ProbenFavoriten));
            }

            set
            {
                SetEinstellung(nameof(ProbenFavoriten), value);
            }
        }

        public static string PdfReaderCommand
        {
            get
            {
                return GetEinstellung<string>(nameof(PdfReaderCommand));
            }

            set
            {
                SetEinstellung(nameof(PdfReaderCommand), value);
            }
        }

        public static string PdfReaderArguments
        {
            get
            {
                return GetEinstellung<string>(nameof(PdfReaderArguments));
            }

            set
            {
                SetEinstellung(nameof(PdfReaderArguments), value);
            }
        }

        public static string SpielerInfoBilderPfad
        {
            get
            {
                return GetEinstellung<string>(nameof(SpielerInfoBilderPfad));
            }

            set
            {
                SetEinstellung(nameof(SpielerInfoBilderPfad), value);
            }
        }

        public static bool SpielerScreenUnterordnerEinbeziehen
        {
            get
            {
                return GetEinstellung<bool>(nameof(SpielerScreenUnterordnerEinbeziehen));
            }

            set
            {
                SetEinstellung(nameof(SpielerScreenUnterordnerEinbeziehen), value);
            }
        }

        public static double SlideShowInterval
        {
            get
            {
                return GetEinstellung<double>(nameof(SlideShowInterval));
            }

            set
            {
                SetEinstellung(nameof(SlideShowInterval), value);
            }
        }

        public static bool ToolTitelAusblenden
        {
            get
            {
                return GetEinstellung<bool>(nameof(ToolTitelAusblenden));
            }

            set
            {
                SetEinstellung(nameof(ToolTitelAusblenden), value);
                RaiseToolTitelAusblendenChanged();
            }
        }

        /// <summary>
        /// Gibt an, ob sich MeisterGeister im Nur-Lesen-Modus befindet. Ist der Schreibschutz
        /// aktiviert, werden bestimmte Eingabe- und Änderungsmöglichkeiten eingeschränkt.
        /// </summary>
        public static bool IsReadOnly
        {
            get
            {
                return GetEinstellung<bool>(nameof(IsReadOnly));
            }

            set
            {
                SetEinstellung(nameof(IsReadOnly), value);
                RaiseIsReadOnlyChanged();
            }
        }

        public static string KampfRecentColors
        {
            get
            {
                return GetEinstellung<string>(nameof(KampfRecentColors));
            }

            set
            {
                SetEinstellung(nameof(KampfRecentColors), value);
            }
        }

        public static bool ShowChangeLog
        {
            get
            {
                return GetEinstellung<bool>(nameof(ShowChangeLog));
            }

            set
            {
                SetEinstellung(nameof(ShowChangeLog), value);
            }
        }

        public static Guid MeisterGeisterID
        {
            get
            {
                if (Global.IsInitialized)
                {
                    if (_meisterGeisterID == null)
                    {
                        _meisterGeisterID = Guid.Parse(GetEinstellung<string>(nameof(MeisterGeisterID)));
                    }

                    if (_meisterGeisterID == Guid.Empty)
                    { // MeisterGeisterID ist 00000000-0000-0000-0000-000000000000, also die Standard-Datenbank -> Neue ID erzeugen
                        _meisterGeisterID = Guid.NewGuid();
                        MeisterGeisterID = _meisterGeisterID.GetValueOrDefault();
                    }
                }
                return _meisterGeisterID.GetValueOrDefault();
            }

            set
            {
                SetEinstellung(nameof(MeisterGeisterID), value.ToString());
                _meisterGeisterID = value;
            }
        }

        public static void UpdateEinstellungen()
        {
            if (Global.IsInitialized)
            {
                foreach (Model.Einstellung e in Global.ContextHeld.Liste<Model.Einstellung>())
                {
                    CopyDefaultValues(e.Name, e);
                    Global.ContextHeld.Update<Model.Einstellung>(e);
                }
                Global.ContextHeld.Save();
            }
        }

        public static T GetEinstellung<T>(string name)
        {
            if (Global.IsInitialized)
            {
                Model.Einstellung e = Global.ContextHeld.LoadEinstellungByName(name);
                if (e == null)
                {
                    e = CreateEinstellung<T>(name);
                }

                return e.Get<T>();
            }
            return default(T);
        }

        public static Model.Einstellung CreateEinstellung<T>(string name)
        {
            Model.Einstellung e = Global.ContextHeld.New<Model.Einstellung>();
            CopyDefaultValues(name, e, true);
            Global.ContextHeld.Insert(e);
            return e;
        }

        public static Model.Einstellung SetEinstellung<T>(string name, T value, [CallerMemberName] string propertyName = null)
        {
            if (Global.IsInitialized)
            {
                Model.Einstellung e = Global.ContextHeld.LoadEinstellungByName(name);
                if (e == null)
                {
                    e = CreateEinstellung<T>(name);
                }

                e.Set<T>(value);
                Global.ContextHeld.Update(e);
                OnEinstellungChanged(propertyName, name);
                return e;
            }
            return null;
        }

        public static void RaiseWuerfelSoundAbspielenChanged(object sender = null, EventArgs args = null)
        {
            _wuerfelSoundAbspielenChanged.Raise(sender, args ?? EventArgs.Empty);
        }

        public static void RaiseFading_Click(object sender = null, EventArgs args = null)
        {
            _fading_Click.Raise(sender, args ?? EventArgs.Empty);
        }

        public static void RaiseToolTitelAusblendenChanged(object sender = null, EventArgs args = null)
        {
            _toolTitelAusblendenChanged.Raise(sender, args ?? EventArgs.Empty);
        }

        public static void RaiseIsReadOnlyChanged(object sender = null, EventArgs args = null)
        {
            _isReadOnlyChanged.Raise(sender, args ?? EventArgs.Empty);
        }

        private static WeakEvent<EinstellungChangedHandler> _einstellungChanged = new WeakEvent<EinstellungChangedHandler>();

        private static Dictionary<string, Model.Einstellung> defaultValues = null;

        // TODO: Einstellung wird gecached, um Absturz bei Proben zu verhindern. Da sich dadurch die Einstellung nach Änderung ggf. nicht mehr aktuell sein könnte, sollte das Caching noch überarbeitet werden.
        private static Nullable<bool> _cached_IsMitUeberlastung = null;

        // TODO: Einstellung wird gecached, um Absturz bei Poben zu verhindern. Da sich dadurch die Einstellung nach Änderung ggf. nicht mehr aktuell sein könnte, sollte das Caching noch überarbeitet werden.
        private static Nullable<int> _cached_UeberlastungBerechnung = null;

        // TODO: Einstellung wird gecached, um Absturz bei Poben zu verhindern. Da sich dadurch die Einstellung nach Änderung ggf. nicht mehr aktuell sein könnte, sollte das Caching noch überarbeitet werden.
        private static Nullable<bool> _cached_WundenVerändernWundschwelle = null;

        // TODO: Einstellung wird gecached, um Absturz verhindern. Da sich dadurch die Einstellung nach Änderung ggf. nicht mehr aktuell sein könnte, sollte das Caching noch überarbeitet werden.
        private static Nullable<bool> _checkForUpdates = null;

        // TODO: Einstellung wird gecached, um Absturz verhindern. Da sich dadurch die Einstellung nach Änderung ggf. nicht mehr aktuell sein könnte, sollte das Caching noch überarbeitet werden.
        private static string _lastUpdateCheck = null;

        private static string _heldenSoftwareOnlineURL = null;

        private static string _heldenSoftwareOnlineToken = null;

        private static string _regeledition = null;

        private static WeakEvent<EventHandler> _wuerfelSoundAbspielenChanged = new WeakEvent<EventHandler>();

        private static WeakEvent<EventHandler> _fading_Click = new WeakEvent<EventHandler>();

        private static WeakEvent<EventHandler> _toolTitelAusblendenChanged = new WeakEvent<EventHandler>();

        private static WeakEvent<EventHandler> _isReadOnlyChanged = new WeakEvent<EventHandler>();

        // TODO: Einstellung wird gecached, um Absturz verhindern. Da sich dadurch die Einstellung nach Änderung ggf. nicht mehr aktuell sein könnte, sollte das Caching noch überarbeitet werden.
        private static Nullable<Guid> _meisterGeisterID = null;

        private static Dictionary<string, Model.Einstellung> DefaultValues
        {
            get
            {
                if (defaultValues == null && Global.IsInitialized)
                {
                    defaultValues = new Dictionary<string, Model.Einstellung>();
                    foreach (Model.Einstellung e in GetDefaults())
                    {
                        defaultValues.Add(e.Name, e);
                    }
                }
                return defaultValues;
            }
        }

        private static List<Model.Einstellung> GetDefaults()
        {
            return new List<Model.Einstellung>()
            {
                new Model.Einstellung() { Name = "JingleAbstellen", Kontext = "Allgemein", Kategorie = null, Typ = "Boolean", Beschreibung = "Jingle beim Start deaktivieren", Wert = "False" },
                new Model.Einstellung() { Name = "ShowChangeLog", Kontext = "Allgemein", Kategorie = null, Typ = "Boolean", Beschreibung = "ChangeLog beim Start anzeigen", Wert = "True" },
                //Regedition als Standard auf DSA 4.1 stellen
                new Model.Einstellung() { Name = "Regeledition", Kontext = "Allgemein", Kategorie = "Versteckt", Typ = "String", Beschreibung = "Regeledition", Wert = "DSA 4.1" },

                new Model.Einstellung() { Name = "PflanzenwissenIntegrieren", Kontext = "Held", Kategorie = "Hausregel", Typ = "Boolean", Beschreibung = "Held kennt nur bestimmte Pflanzen", Wert = "False" },

                new Model.Einstellung() { Name = "Fading", Kontext = "Audioplayer", Kategorie = null, Typ = "Integer", Beschreibung = "", Wert = "600" },
                new Model.Einstellung() { Name = "AudioInAnderemPfadSuchen", Kontext = "Audioplayer", Kategorie = null, Typ = "Boolean", Beschreibung = "", Wert = "1" },
                new Model.Einstellung() { Name = "AudioDirektAbspielen", Kontext = "Audioplayer", Kategorie = null, Typ = "Boolean", Beschreibung = "", Wert = "1" },
                new Model.Einstellung() { Name = "AudioVerzeichnis", Kontext = "Audioplayer", Kategorie = null, Typ = "String", Beschreibung = null, Wert = "C:\\" },
                new Model.Einstellung() { Name = "AudioSpieldauerBerechnen", Kontext = "Audioplayer", Kategorie = null, Typ = "Boolean", Beschreibung = null, Wert = "1" },
                new Model.Einstellung() { Name = "GeneralMusikVolume", Kontext = "Audioplayer", Kategorie = null, Typ = "Integer", Beschreibung = null, Wert = "50" },
                new Model.Einstellung() { Name = "GeneralGeräuscheVolume", Kontext = "Audioplayer", Kategorie = null, Typ = "Integer", Beschreibung = null, Wert = "100" },
                new Model.Einstellung() { Name = "GeneralHotkeyVolume", Kontext = "Audioplayer", Kategorie = null, Typ = "Integer", Beschreibung = null, Wert = "50" },
                new Model.Einstellung() { Name = "ShowPlaylistFavorite", Kontext = "Audioplayer", Kategorie = null, Typ = "Boolean", Beschreibung = null, Wert = "1" },

                new Model.Einstellung() { Name = "PdfReaderCommand", Kontext = "Almanach", Kategorie = null, Typ = "String", Beschreibung = "Befehl zum starten des PDF-Readers", Wert = null },
                new Model.Einstellung() { Name = "PdfReaderArguments", Kontext = "Almanach", Kategorie = null, Typ = "String", Beschreibung = "Parameter für den Aufruf des PDF-Readers", Wert = null },

                new Model.Einstellung() { Name = "FrageNeueKampfrundeAbstellen", Kontext = "Kampf", Kategorie = null, Typ = "Boolean", Beschreibung = "Frage bei neuer Kampfrunde unterbinden", Wert = "False" },
                new Model.Einstellung() { Name = "TPKK", Kontext = "Kampf", Kategorie = "Optional", Typ = "Boolean", Beschreibung = "Trefferpunkte und Körperkraft (TP/KK) (WdS 81f)", Wert = "True" },
                new Model.Einstellung() { Name = "NiedrigeLE", Kontext = "Kampf", Kategorie = "Optional", Typ = "Boolean", Beschreibung = "Auswirkungen niedriger LE (WdS 57)", Wert = "True" },
                new Model.Einstellung() { Name = "NiedrigeAU", Kontext = "Kampf", Kategorie = "Optional", Typ = "Boolean", Beschreibung = "Auswirkungen niedriger AU (WdS 83)", Wert = "True" },
                new Model.Einstellung() { Name = "AusdauerImKampf", Kontext = "Kampf", Kategorie = "Optional", Typ = "Boolean", Beschreibung = "Ausdauerverlust (WdS 83)", Wert = "True" },
                new Model.Einstellung() { Name = "NurDreiZonenWunden", Kontext = "Kampf", Kategorie = "Hausregel", Typ = "Boolean", Beschreibung = "Maximal drei Wunden pro Trefferzone", Wert = "True" },
                new Model.Einstellung() { Name = "WundenVerändernWundschwelle", Kontext = "Kampf", Kategorie = "Unklarheit", Typ = "Boolean", Beschreibung = "Veränderungen der KO durch Wunden beinflussen die Wundschwellen", Wert = "True" },

                new Model.Einstellung() { Name = "AngriffAutomatischWürfeln", Kontext = "Kampf", Kategorie = null, Typ = "Boolean", Beschreibung = "Automatisches Würfeln der Angriffe (wenn Ini-Position erreicht)", Wert = "True" },
                new Model.Einstellung() { Name = "LebensbalkenImmerAnzeigen", Kontext = "Kampf", Kategorie = null, Typ = "Boolean", Beschreibung = "Lebensbalken im Bodenplan immer anzeigen", Wert = "False" },

                new Model.Einstellung() { Name = "EigenschaftenProbePatzerGlück", Kontext = "Proben", Kategorie = "Optional", Typ = "Boolean", Beschreibung = "Patzer und Glückswürfe bei Eigenschafts-Proben (WdS 7)", Wert = "True" },
                new Model.Einstellung() { Name = "WuerfelSoundAbspielen", Kontext = "Proben", Kategorie = null, Typ = "Boolean", Beschreibung = "Würfelsound abspielen", Wert = "True" },

                new Model.Einstellung() { Name = "01_RSBerechnung", Kontext = "Inventar", Kategorie = null, Typ = "Integer", Beschreibung = "Wie wird die Rüstung eines Helden ermittelt?", Wert = "0" },
                new Model.Einstellung() { Name = "02_BEBerechnung", Kontext = "Inventar", Kategorie = null, Typ = "Integer", Beschreibung = "Wie wird die Behinderung eines Helden ermittelt?", Wert = "0" },
                new Model.Einstellung() { Name = "03_UeberlastungBerechnung", Kontext = "Inventar", Kategorie = null, Typ = "Integer", Beschreibung = "Spielt die Gruppe mit Überlastung?", Wert = "0" },
                new Model.Einstellung() { Name = "MitUeberlastung", Kontext = "Inventar", Kategorie = "Optional", Typ = "Boolean", Beschreibung = "(Optional) Mit Ueberlastung spielen?", Wert = "True" },

                new Model.Einstellung() { Name = "ToolTitelAusblenden", Kontext = "Allgemein", Kategorie = null, Typ = "Boolean", Beschreibung = "Tool Name im Tab-Titel ausblenden", Wert = "False" },

                new Model.Einstellung() { Name = "CheckForUpdates", Kontext = "Allgemein", Kategorie = null, Typ = "Boolean", Beschreibung = "Täglich nach neuen Updates suchen", Wert = "True" },

                new Model.Einstellung() { Name = "HeldenSoftwareOnlineURL", Kontext = "Allgemein", Kategorie = null, Typ = "String", Beschreibung = "URL zu HeldenSoftware-Online", Wert = "https://online.helden-software.de/index.php" },
                new Model.Einstellung() { Name = "HeldenSoftwareOnlineToken", Kontext = "Allgemein", Kategorie = null, Typ = "String", Beschreibung = "Token zum HeldenSoftware-Online-Account", Wert = null },

                new Model.Einstellung() { Name = "HUE_GatewayID", Kontext = "Allgemein", Kategorie = "Versteckt", Typ = "String", Beschreibung = "HUE Lampen-Steuerung zu MeisterGeister - Gateway ID", Wert = null },
                new Model.Einstellung() { Name = "HUE_Registerkey", Kontext = "Allgemein", Kategorie = "Versteckt", Typ = "String", Beschreibung = "HUE Lampen-Steuerung zu MeisterGeister - Register-Key", Wert = null },

                new Model.Einstellung() { Name = "SpielerScreenUnterordnerEinbeziehen", Kontext = "Allgemein", Kategorie = null, Typ = "Boolean", Beschreibung = "SpielerInfo-Tool: Unterordner beim Bilder-Laden mit einbeziehen?", Wert = "False" },

                new Model.Einstellung() { Name = "FoundryGegnerPortraitPfad", Kontext = "Foundry", Kategorie = "Versteckt", Typ = "String", Beschreibung = "", Wert = "systems/dsa41/portraits/meistergeister" },
                new Model.Einstellung() { Name = "FoundryHeldPortraitPfad", Kontext = "Foundry", Kategorie = "Versteckt", Typ = "String", Beschreibung = "", Wert = "systems/dsa41/portraits/meistergeister" },
                new Model.Einstellung() { Name = "FoundryGegnerTokenPfad", Kontext = "Foundry", Kategorie = "Versteckt", Typ = "String", Beschreibung = "", Wert = "systems/dsa41/tokens/meistergeister" },
                new Model.Einstellung() { Name = "FoundryHeldTokenPfad", Kontext = "Foundry", Kategorie = "Versteckt", Typ = "String", Beschreibung = "", Wert = "systems/dsa41/tokens/meistergeister" },
                new Model.Einstellung() { Name = "FoundryMusikPfad", Kontext = "Foundry", Kategorie = "Versteckt", Typ = "String", Beschreibung = "", Wert = "Eigene-Daten/Allgemein/Musik" },
                
                //Versteckte
                new Model.Einstellung() { Name = "FoundryFTPAdresse", Kontext = "Foundry", Kategorie = "Versteckt", Typ = "String", Beschreibung = "", Wert = "ftp://1.2.3.4:21/Data" },
                new Model.Einstellung() { Name = "FoundryFTPUser", Kontext = "Foundry", Kategorie = "Versteckt", Typ = "String", Beschreibung = "", Wert = "user" },
                new Model.Einstellung() { Name = "FoundryFTPPasswort", Kontext = "Foundry", Kategorie = "Versteckt", Typ = "String", Beschreibung = "", Wert = "passwort" },
                new Model.Einstellung() { Name = "IsLocalInstalliert", Kontext = "Foundry", Kategorie = "Versteckt", Typ = "String", Beschreibung = "", Wert = "False" },
                new Model.Einstellung() { Name = "LastUpdateCheck", Kontext = "Allgemein", Kategorie = "Versteckt", Typ = "String", Beschreibung = "", Wert = DateTime.Now.ToString() },
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

        private static void OnEinstellungChanged(string propertyName, string einstellungName)
        {
            _einstellungChanged.Raise(null, new EinstellungChangedEventArgs(propertyName, einstellungName));
        }

        private static void CopyDefaultValues(string name, Model.Einstellung target, bool mitWert = false)
        {
            Model.Einstellung source = DefaultValues[name];
            target.Typ = source.Typ;
            target.Kontext = source.Kontext;
            target.Kategorie = source.Kategorie;
            target.Name = source.Name;
            target.Beschreibung = source.Beschreibung;
            if (mitWert)
            {
                target.Wert = source.Wert;
            }
        }
    }
}
