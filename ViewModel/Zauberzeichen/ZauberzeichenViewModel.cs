using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Windows.Controls;
//Eigene usings
using MeisterGeister.ViewModel.Base;
using MeisterGeister.Model;
using MeisterGeister.Model.Service;

namespace MeisterGeister.ViewModel.Zauberzeichen
{
    //TODO MP: Export nach Notiztool
    //TODO MP: Verknüpfung mit Kalender Datum
    public class ZauberzeichenViewModel : Base.ViewModelBase
    {

        //TODO MP: AbrichtenTool

        #region //---- FELDER ----

        #region //-Allgemein-
        //Booleans
        private bool IsLoaded;
        //Listen
        private List<string> _satinavsSiegelListe = new List<string>(new string[] { "kein Siegel", "Monat", "Quartal", "WSWende" });
        private List<string> _verkleinerungListe = new List<string>(new string[] { "keine Verkleinerung", "1/2", "1/4", "1/8" });
        private List<string> _größenZeichenträgerListe = new List<string>(new string[] { "Schrank / 1xRadius", "Kutsche / 2xRadius", "Schiff / 4xRadius", "Burg / 8xRadius", "Berg / 16xRadius" });
        private List<string> _werkzeugListe = new List<string>(new string[] { "normal", "beschädigt", "hochwertig", "aussergew. hochwertig" });
        //Selections
        private Model.Held _selectedHeld;
        //Werte
        private TabItem _tabValue;
        private int _wertRitualkenntnis;
        #endregion

        #region //-Zauberzeichen-
        //interne Variablen
        private int grundkomplexitätZauberzeichen;
        private int _wertTaPÜberZauberzeichen;
        //Enables
        private bool _isEnabledSatinavsSiegelListeZauberzeichen = true;
        private bool _isEnabledPotenzierungZauberzeichen;
        private bool _isEnabledKraftquellenspeisungZauberzeichen;
        private bool _isEnabledMagiewiderstandZauberzeichen;
        private bool _isEnabledSchutzsiegelZauberzeichen;
        private bool _isEnabledTarnungZauberzeichen;
        private bool _isEnabledZielbeschränkungZauberzeichen;
        private bool _isEnabledVerkleinerungListeZauberzeichen;
        private bool _isEnabledSpontanzeichenZauberzeichen;
        //Listen
        private List<Model.Zauberzeichen> _zauberzeichenListe;
        private List<Talent> _talentListeZauberzeichen;
        private List<string> _zusatzzeichenListeZauberzeichen;
        //Selections
        private Model.Zauberzeichen _selectedZauberzeichen;
        private string _selectedSatinavsSiegelZauberzeichen;
        private string _selectedGrößeZeichenträgerZauberzeichen;
        private string _selectedVerkleinerungZauberzeichen;
        private Talent _selectedTalentZauberzeichen;
        private string _selectedWerkzeugZauberzeichen;
        //Checkboxen
        private bool _checkedSpezialisierungZauberzeichen;
        private bool _checkedSpontanzeichenZauberzeichen;
        private bool _checkedZielbeschränkungZauberzeichen;
        private bool _checkedTarnungZauberzeichen;
        private bool _checkedSchutzsiegelZauberzeichen;
        private bool _checkedMagiewiderstandZauberzeichen;
        private bool _checkedKraftquellenspeisungZauberzeichen;
        private bool _checkedPotenzierungZauberzeichen;
        //Rückgaben
        private int _wertKomplexitätZauberzeichen;
        private int _wertAnzahlZauberzeichen;
        private int _wertAktivierungskostenZauberzeichen;
        private int _wertArtefaktkontrolleZauberzeichen;
        private string _wertErkennenZauberzeichen;
        private int _wertHandwerkZauberzeichen;
        private int _wertQualitätZauberzeichen;
        private int _wertAktivierungZauberzeichen;
        private int _wertHerstellungszeitZauberzeichen;
        private int _wertFarbeZauberzeichen;
        private int _wertAffinitätZauberzeichen;
        private int _wertFormZauberzeichen;
        private int _wertTaPZauberzeichen;
        private string _wertGrößeZauberzeichen;
        private string _wertWirkungsdauerZauberzeichen;
        private int _wertRkPZauberzeichen;
        private int _WertErleichterungTaZauberzeichen;
        private int _wertErleichterungRkZauberzeichen;
        private string _wertHerstellungsdauerZauberzeichen;
        private string _wertAktivierungsdauerZauberzeichen;
        private string _wertMerkmaleZauberzeichen;
        private string _wertWirkungsradiusZauberzeichen;
        private string _wertNameZauberzeichen;
        private int _wertRkWErschafferZauberzeichen;
        //Commands
        private Base.CommandBase _onBauZauberzeichen;
        private Base.CommandBase _onAktivierungZauberzeichen;

        private Base.CommandBase onClearSelectedHeld = null;
        public Base.CommandBase OnClearSelectedHeld
        {
            get
            {
                if (onClearSelectedHeld == null)
                    onClearSelectedHeld = new Base.CommandBase(ClearSelectedHeld, null);
                return onClearSelectedHeld;
            }
        }

        private void ClearSelectedHeld(object obj)
        {
            SelectedHeld = null;
        }

        #endregion

        #region //-Runen--
        //interne Felder
        private int grundkomplexitätRunen;
        private int _wertTaPÜberRunen;
        //Enables
        private bool _isEnabledSpontanzeichenRunen;
        //Listen
        private List<Model.Zauberzeichen> _RunenListe;
        private List<Talent> _talentListeRunen;
        //Selections
        private Model.Zauberzeichen _selectedRune;
        private Talent _selectedTalentRunen;
        private string _selectedWerkzeugRunen;
        //Checkboxen
        private bool _checkedSpezialisierungRunen;
        private bool _checkedSpontanzeichenRunen;
        //Rückgaben
        private int _wertKomplexitätRunen;
        private int _wertArtefaktkontrolleRunen;
        private int _wertTaPRunen;
        private string _wertHerstellungsdauerRunen;
        private string _wertWirkungsdauerRunen;
        private int _wertErleichterungRkRunen;
        private string _wertAktivierungsdauerRunen;
        private int _wertAktivierungskostenRunen;
        private string _wertErkennenRunen;
        private int _WertHandwerkRunen;
        private int _wertHerstellungszeitRunen;
        private int _wertRkPRunen;
        private int _wertErleichterungTaRunen;
        private int _wertFormRunen;
        private int _wertAffinitätRunen;
        private int _wertAktivierungRunen;
        private int _wertQualitätRunen;
        private string _wertMerkmaleRunen;
        private string _wertWirkungsradiusRunen;
        private string _wertNameRunen;
        private int _wertRkWErschafferRunen;
        private string _wertGrößeRunen;
        //Commands
        private CommandBase _onBauRunen;
        private CommandBase _onAktivierungRunen;
        #endregion

        #region //-Kreise-
        //interne Felder
        private int grundkomplexitätKreise;
        private int _wertTaPÜberKreise;
        //Enables
        private bool _isEnabledSpezArtKreise;
        private bool _isEnabledSatinavsSiegelListeKreise;
        private bool _isEnabledKraftquellenspeisungKreise;
        private bool _isEnabledMagiewiderstandKreise;
        private bool _isEnabledSchutzsiegelKreise;
        private bool _isEnabledTarnungKreise;
        private bool _isEnabledSpontanzeichenKreise;
        //Listen
        private List<Model.Zauberzeichen> _kreiseListe;
        private List<Talent> _talentListeKreise;
        private List<string> _zusatzzeichenListeKreise;
        //Selections
        private Model.Zauberzeichen _selectedKreis;
        private Talent _selectedTalentKreise;
        private string _selectedSatinavsSiegelKreise;
        private string _selectedWerkzeugKreise;
        //Checkboxen
        private bool _checkedSpontanzeichenKreise;
        private bool _checkedSpezialisierungKreise;
        private bool _checkedKraftquellenspeisungKreise;
        private bool _checkedMagiewiderstandKreise;
        private bool _checkedSchutzsiegelKreise;
        private bool _checkedTarnungKreise;
        private bool _checkedSpezifscheArtKreise;
        private bool _checkedBestimmteArtKreise;
        //Rückgaben
        private int _wertZirkelstärkeKreise;
        private string _wertGrößeKreisKreise;
        private int _wertKomplexitätKreise;
        private int _wertArtefaktkontrolleKreise;
        private int _wertTaPKreise;
        private string _wertHerstellungsdauerKreise;
        private string _wertWirkungsdauerKreise;
        private int _wertErleichterungRkKreise;
        private string _wertAktivierungsdauerKreise;
        private int _wertAktivierungskostenKreise;
        private string _wertErkennenKreise;
        private int _WertHandwerkKreise;
        private int _wertHerstellungszeitKreise;
        private int _wertRkPKreise;
        private int _wertErleichterungTaKreise;
        private int _wertFormKreise;
        private int _wertFarbeKreise;
        private int _wertAffinitätKreise;
        private int _wertAktivierungKreise;
        private int _wertQualitätKreise;
        private string _wertMerkmaleKreise;
        private string _wertWirkungsradiusKreise;
        private string _wertNameKreise;
        private int _wertRkWErschafferKreise;
        //Commands
        private CommandBase _onBauKreise;
        private CommandBase _onAktivierungKreise;
        #endregion

        #region //-Analyse-
        #endregion

        #endregion

        #region //---- EIGENSCHAFTEN ----

        #region //-Allgemein-

        //Listen
        public List<Model.Held> HeldListe
        {
            get { return Global.ContextHeld.LoadHeldenGruppeWithZauberzeichen(); }
        }
        //Selections
        public Model.Held SelectedHeld { get { return _selectedHeld; } 
            set 
            { 
                _selectedHeld = value;
                OnChanged("SelectedHeld");
                resetKreise(); resetRunen(); resetZauberzeichen();
                checkZusatzzeichen(value);
                WertRitualkenntnis = Global.ContextHeld.LoadMaxRitualkenntnisWertByHeld(value);
                WertRkWErschafferKreise = Global.ContextHeld.LoadMaxRitualkenntnisWertByHeld(value);
                WertRkWErschafferRunen = Global.ContextHeld.LoadMaxRitualkenntnisWertByHeld(value);
                WertRkWErschafferZauberzeichen = Global.ContextHeld.LoadMaxRitualkenntnisWertByHeld(value);
                TalentListeZauberzeichen = Global.ContextHeld.LoadZauberzeichenTalenteByHeld(value);
                TalentListeKreise = Global.ContextHeld.LoadZauberzeichenTalenteByHeld(value);
                TalentListeRunen = Global.ContextHeld.LoadRunenTalenteByHeld(value);
                ZauberzeichenListe = Global.ContextHeld.LoadZauberzeichenByHeld(value);
                KreiseListe = Global.ContextHeld.LoadKreiseByHeld(value);
                RunenListe = Global.ContextHeld.LoadRunenByHeld(value); 
            } 
        }
        //Werte
        public TabItem TabItem { get { return _tabValue; } set { _tabValue = value; OnChanged("TabValue"); } }
        public int WertRitualkenntnis
        {
            get { return _wertRitualkenntnis; }
            set
            {
                _wertRitualkenntnis = value; OnChanged("WertRitualkenntnis");
                WertGrößeKreisKreise = Math.Round(((double)value / 2), MidpointRounding.AwayFromZero) + " Schritt";
                setWirkungsradiusZauberzeichen();
                setWirkungsradiusRunen();
                setWirkungsradiusKreise();
            }
        }
        #endregion

        #region //-Zauberzeichen-
        //Enables 
        public bool IsEnabledSpontanzeichenZauberzeichen { get { return _isEnabledSpontanzeichenZauberzeichen; } set { _isEnabledSpontanzeichenZauberzeichen = value; OnChanged("IsEnabledSpontanzeichenZauberzeichen"); } }
        public bool IsEnabledSatinavsSiegelListeZauberzeichen { get { return _isEnabledSatinavsSiegelListeZauberzeichen; } set { _isEnabledSatinavsSiegelListeZauberzeichen = value; OnChanged("IsEnabledSatinavsSiegelListeZauberzeichen"); } }
        public bool IsEnabledPotenzierungZauberzeichen { get { return _isEnabledPotenzierungZauberzeichen; } set { _isEnabledPotenzierungZauberzeichen = value; OnChanged("IsEnabledPotenzierungZauberzeichen"); } }
        public bool IsEnabledKraftquellenspeisungZauberzeichen { get { return _isEnabledKraftquellenspeisungZauberzeichen; } set { _isEnabledKraftquellenspeisungZauberzeichen = value; OnChanged("IsEnabledKraftquellenspeisungZauberzeichen"); } }
        public bool IsEnabledMagiewiderstandZauberzeichen { get { return _isEnabledMagiewiderstandZauberzeichen; } set { _isEnabledMagiewiderstandZauberzeichen = value; OnChanged("IsEnabledMagiewiderstandZauberzeichen"); } }
        public bool IsEnabledSchutzsiegelZauberzeichen { get { return _isEnabledSchutzsiegelZauberzeichen; } set { _isEnabledSchutzsiegelZauberzeichen = value; OnChanged("IsEnabledSchutzsiegelZauberzeichen"); } }
        public bool IsEnabledTarnungZauberzeichen { get { return _isEnabledTarnungZauberzeichen; } set { _isEnabledTarnungZauberzeichen = value; OnChanged("IsEnabledTarnungZauberzeichen"); } }
        public bool IsEnabledZielbeschränkungZauberzeichen { get { return _isEnabledZielbeschränkungZauberzeichen; } set { _isEnabledZielbeschränkungZauberzeichen = value; OnChanged("IsEnabledZielbeschränkungZauberzeichen"); } }
        public bool IsEnabledVerkleinerungListeZauberzeichen { get { return _isEnabledVerkleinerungListeZauberzeichen; } set { _isEnabledVerkleinerungListeZauberzeichen = value; OnChanged("IsEnabledVerkleinerungListeZauberzeichen"); } }
        //Listen
        public List<Model.Zauberzeichen> ZauberzeichenListe { get { return _zauberzeichenListe; } set { _zauberzeichenListe = value; OnChanged("ZauberzeichenListe"); } }
        public List<string> GrößenZeichenträgerListeZauberzeichen { get { return _größenZeichenträgerListe; } set { _größenZeichenträgerListe = value; OnChanged("GrößenZeichenträgerListeZauberzeichen"); } }
        public List<string> SatinavsSiegelListeZauberzeichen { get { return _satinavsSiegelListe; } set { _satinavsSiegelListe = value; OnChanged("SatinavsSiegelListeZauberzeichen"); } }
        public List<string> VerkleinerungListeZauberzeichen { get { return _verkleinerungListe; } set { _verkleinerungListe = value; OnChanged("VerkleinerungListeZauberzeichen"); } }
        public List<Talent> TalentListeZauberzeichen { get { return _talentListeZauberzeichen; } set { _talentListeZauberzeichen = value; OnChanged("TalentListeZauberzeichen"); } }
        public List<string> ZusatzzeichenListeZauberzeichen { get { return _zusatzzeichenListeZauberzeichen; } set { _zusatzzeichenListeZauberzeichen = value; OnChanged("ZusatzzeichenListeZauberzeichen"); } }
        public List<string> WerkzeugListeZauberzeichen { get { return _werkzeugListe; } set { _werkzeugListe = value; OnChanged("WerkzeugListeZauberzeichen"); } }
        //Selections
        public string SelectedSatinavsSiegelZauberzeichen { get { return _selectedSatinavsSiegelZauberzeichen; } set { _selectedSatinavsSiegelZauberzeichen = value; OnChanged("SelectedSatinavsSiegelZauberzeichen"); berechneAbhängigkeitenZauberzeichen(); } }
        public string SelectedGrößeZeichenträgerZauberzeichen { get { return _selectedGrößeZeichenträgerZauberzeichen; } set { _selectedGrößeZeichenträgerZauberzeichen = value; OnChanged("SelectedGrößeZeichenträgerZauberzeichen"); berechneAbhängigkeitenZauberzeichen(); setWirkungsradiusZauberzeichen(); } }
        public string SelectedVerkleinerungZauberzeichen { get { return _selectedVerkleinerungZauberzeichen; } set { _selectedVerkleinerungZauberzeichen = value; OnChanged("SelectedVerkleinerungZauberzeichen"); berechneAbhängigkeitenZauberzeichen(); } }
        public Model.Zauberzeichen SelectedZauberzeichen { get { return _selectedZauberzeichen; } set { _selectedZauberzeichen = value; OnChanged("SelectedZauberzeichen"); resetZauberzeichen(); if (value != null) { WertMerkmaleZauberzeichen = value.Merkmal; WertKomplexitätZauberzeichen = value.Komplexität; grundkomplexitätZauberzeichen = value.Komplexität; } else { WertMerkmaleZauberzeichen = null; WertKomplexitätZauberzeichen = 0; grundkomplexitätZauberzeichen = 0; }; setWirkungsradiusZauberzeichen(); berechneAbhängigkeitenZauberzeichen(); } }
        public Talent SelectedTalentZauberzeichen { get { return _selectedTalentZauberzeichen; } set { _selectedTalentZauberzeichen = value; OnChanged("SelectedTalentZauberzeichen"); WertHandwerkZauberzeichen = SelectedHeld.Talentwert(value); } }
        public string SelectedWerkzeugZauberzeichen { get { return _selectedWerkzeugZauberzeichen; } set { _selectedWerkzeugZauberzeichen = value; OnChanged("SelectedWerkzeugZauberzeichen"); berechneAbhängigkeitenZauberzeichen(); } }
        //Checkboxen
        public bool CheckedSpontanzeichenZauberzeichen { get { return _checkedSpontanzeichenZauberzeichen; } set { _checkedSpontanzeichenZauberzeichen = value; OnChanged("CheckedSpontanzeichenZauberzeichen"); if (value) SelectedSatinavsSiegelZauberzeichen = SatinavsSiegelListeZauberzeichen[0]; IsEnabledSatinavsSiegelListeZauberzeichen = !value; berechneAbhängigkeitenZauberzeichen(); } }
        public bool CheckedKraftquellenspeisungZauberzeichen { get { return _checkedKraftquellenspeisungZauberzeichen; } set { _checkedKraftquellenspeisungZauberzeichen = value; OnChanged("CheckedKraftquellenspeisungZauberzeichen"); berechneAbhängigkeitenZauberzeichen(); } }
        public bool CheckedMagiewiderstandZauberzeichen { get { return _checkedMagiewiderstandZauberzeichen; } set { _checkedMagiewiderstandZauberzeichen = value; OnChanged("CheckedMagiewiderstandZauberzeichen"); berechneAbhängigkeitenZauberzeichen(); } }
        public bool CheckedSchutzsiegelZauberzeichen { get { return _checkedSchutzsiegelZauberzeichen; } set { _checkedSchutzsiegelZauberzeichen = value; OnChanged("CheckedSchutzsiegelZauberzeichen"); berechneAbhängigkeitenZauberzeichen(); } }
        public bool CheckedTarnungZauberzeichen { get { return _checkedTarnungZauberzeichen; } set { _checkedTarnungZauberzeichen = value; OnChanged("CheckedTarnungZauberzeichen"); berechneAbhängigkeitenZauberzeichen(); } }
        public bool CheckedZielbeschränkungZauberzeichen { get { return _checkedZielbeschränkungZauberzeichen; } set { _checkedZielbeschränkungZauberzeichen = value; OnChanged("CheckedZielbeschränkungZauberzeichen"); berechneAbhängigkeitenZauberzeichen(); } }
        public bool CheckedPotenzierungZauberzeichen { get { return _checkedPotenzierungZauberzeichen; } set { _checkedPotenzierungZauberzeichen = value; OnChanged("CheckedPotenzierungZauberzeichen"); berechneAbhängigkeitenZauberzeichen(); } }
        public bool CheckedSpezialisierungZauberzeichen { get { return _checkedSpezialisierungZauberzeichen; } set { _checkedSpezialisierungZauberzeichen = value; OnChanged("CheckedSpezialisierungZauberzeichen"); if (value) WertHandwerkZauberzeichen = WertHandwerkZauberzeichen + 2; else WertHandwerkZauberzeichen = WertHandwerkZauberzeichen - 2; } }
        //Rückgaben
        public string WertWirkungsdauerZauberzeichen { get { return _wertWirkungsdauerZauberzeichen; } set { _wertWirkungsdauerZauberzeichen = value; OnChanged("WertWirkungsdauerZauberzeichen"); } }
        public int WertAnzahlZauberzeichen { get { return _wertAnzahlZauberzeichen; } set { _wertAnzahlZauberzeichen = value; OnChanged("WertAnzahlZauberzeichen"); } }
        public int WertAktivierungskostenZauberzeichen { get { return _wertAktivierungskostenZauberzeichen; } set { _wertAktivierungskostenZauberzeichen = value; OnChanged("WertAktivierungskostenZauberzeichen"); } }
        public int WertArtefaktkontrolleZauberzeichen { get { return _wertArtefaktkontrolleZauberzeichen; } set { _wertArtefaktkontrolleZauberzeichen = value; OnChanged("WertArtefaktkontrolleZauberzeichen"); } }
        public string WertErkennenZauberzeichen { get { return _wertErkennenZauberzeichen; } set { _wertErkennenZauberzeichen = value; OnChanged("WertErkennenZauberzeichen"); } }
        public int WertHandwerkZauberzeichen { get { return _wertHandwerkZauberzeichen; } set { _wertHandwerkZauberzeichen = value; OnChanged("WertHandwerkZauberzeichen"); } }
        public int WertFormZauberzeichen { get { return _wertFormZauberzeichen; } set { if (!(value > 3 || value < 0)) { _wertFormZauberzeichen = value; } OnChanged("WertFormZauberzeichen"); berechneAbhängigkeitenZauberzeichen(); } }
        public int WertFarbeZauberzeichen { get { return _wertFarbeZauberzeichen; } set { if (!(value > 2 || value < 0)) { _wertFarbeZauberzeichen = value; } OnChanged("WertFarbeZauberzeichen"); berechneAbhängigkeitenZauberzeichen(); } }
        public int WertAffinitätZauberzeichen { get { return _wertAffinitätZauberzeichen; } set { if (!(value > 25 || value < 0)) { _wertAffinitätZauberzeichen = value; } OnChanged("WertAffinitätZauberzeichen"); berechneAbhängigkeitenZauberzeichen(); } }
        public int WertTaPZauberzeichen { get { return _wertTaPZauberzeichen; } set { _wertTaPZauberzeichen = value; OnChanged("WertTaPZauberzeichen"); berechneAbhängigkeitenZauberzeichen(); } }
        public int WertRkPZauberzeichen { get { return _wertRkPZauberzeichen; } set { _wertRkPZauberzeichen = value; OnChanged("WertRkPZauberzeichen"); } }
        public int WertErleichterungRkZauberzeichen { get { return _wertErleichterungRkZauberzeichen; } set { _wertErleichterungRkZauberzeichen = value; OnChanged("WertErleichterungRkZauberzeichen"); } }
        public int WertErleichterungTaZauberzeichen { get { return _WertErleichterungTaZauberzeichen; } set { _WertErleichterungTaZauberzeichen = value; OnChanged("WertErleichterungTaZauberzeichen"); } }
        public string WertGrößeZauberzeichen { get { return _wertGrößeZauberzeichen; } set { _wertGrößeZauberzeichen = value; OnChanged("WertGrößeZauberzeichen"); } }
        public string WertHerstellungsdauerZauberzeichen { get { return _wertHerstellungsdauerZauberzeichen; } set { _wertHerstellungsdauerZauberzeichen = value; OnChanged("WertHerstellungsdauerZauberzeichen"); } }
        public string WertAktivierungsdauerZauberzeichen { get { return _wertAktivierungsdauerZauberzeichen; } set { _wertAktivierungsdauerZauberzeichen = value; OnChanged("WertAktivierungsdauerZauberzeichen"); } }
        public int WertKomplexitätZauberzeichen { get { return _wertKomplexitätZauberzeichen; } set { _wertKomplexitätZauberzeichen = value; OnChanged("WertKomplexitätZauberzeichen"); } }
        public string WertMerkmaleZauberzeichen { get { return _wertMerkmaleZauberzeichen; } set { _wertMerkmaleZauberzeichen = value; OnChanged("WertMerkmaleZauberzeichen"); } }
        public string WertWirkungsradiusZauberzeichen { get { return _wertWirkungsradiusZauberzeichen; } set { _wertWirkungsradiusZauberzeichen = value; OnChanged("WertWirkungsradiusZauberzeichen"); } }
        public string WertNameZauberzeichen { get { return _wertNameZauberzeichen; } set { _wertNameZauberzeichen = value; OnChanged("WertNameZauberzeichen"); } }
        public int WertRkWErschafferZauberzeichen { get { return _wertRkWErschafferZauberzeichen; } set { _wertRkWErschafferZauberzeichen = value; OnChanged("WertRkWErschafferZauberzeichen"); } }

        public int WertHerstellungszeitZauberzeichen
        {
            get { return _wertHerstellungszeitZauberzeichen; }
            set
            {
                int summe = value * 3 + WertAktivierungZauberzeichen + WertQualitätZauberzeichen;
                if (summe <= _wertTaPÜberZauberzeichen && value >= 0)
                {
                    _wertHerstellungszeitZauberzeichen = value;
                    WertTaPZauberzeichen = _wertTaPÜberZauberzeichen - WertHerstellungszeitZauberzeichen * 3 - WertAktivierungZauberzeichen - WertQualitätZauberzeichen;
                    OnChanged("WertHerstellungszeitZauberzeichen");
                    berechneAbhängigkeitenZauberzeichen();
                }
            }
        }

        public int WertAktivierungZauberzeichen
        {
            get { return _wertAktivierungZauberzeichen; }
            set
            {
                int summe = _wertHerstellungszeitZauberzeichen * 3 + value + WertQualitätZauberzeichen;
                if (summe <= _wertTaPÜberZauberzeichen && value >= 0)
                {
                    _wertAktivierungZauberzeichen = value;
                    WertTaPZauberzeichen = _wertTaPÜberZauberzeichen - WertHerstellungszeitZauberzeichen * 3 - WertAktivierungZauberzeichen - WertQualitätZauberzeichen;
                    OnChanged("WertAktivierungZauberzeichen");
                    berechneAbhängigkeitenZauberzeichen();
                }
            }
        }
        public int WertQualitätZauberzeichen
        {
            get { return _wertQualitätZauberzeichen; }
            set
            {
                int summe = _wertHerstellungszeitZauberzeichen * 3 + WertAktivierungZauberzeichen + value;
                if (summe <= _wertTaPÜberZauberzeichen && value >= 0)
                {
                    _wertQualitätZauberzeichen = value;
                    WertTaPZauberzeichen = _wertTaPÜberZauberzeichen - WertHerstellungszeitZauberzeichen * 3 - WertAktivierungZauberzeichen - WertQualitätZauberzeichen;
                    OnChanged("WertQualitätZauberzeichen");
                    berechneAbhängigkeitenZauberzeichen();
                }
            }
        }
        //Commands
        public Base.CommandBase OnBauZauberzeichen
        {
            get { return _onBauZauberzeichen; }
        }

        public Base.CommandBase OnAktivierungZauberzeichen
        {
            get { return _onAktivierungZauberzeichen; }
        }
        #endregion

        #region //-Runen--
        //Enables 
        public bool IsEnabledSpontanzeichenRunen { get { return _isEnabledSpontanzeichenRunen; } set { _isEnabledSpontanzeichenRunen = value; OnChanged("IsEnabledSpontanzeichenRunen"); } }
        //Listen
        public List<Model.Zauberzeichen> RunenListe { get { return _RunenListe; } set { _RunenListe = value; OnChanged("RunenListe"); } }
        public List<Talent> TalentListeRunen { get { return _talentListeRunen; } set { _talentListeRunen = value; OnChanged("TalentListeRunen"); } }
        public List<string> WerkzeugListeRunen { get { return _werkzeugListe; } set { _werkzeugListe = value; OnChanged("WerkzeugListeRunen"); } }
        //Selections
        public Model.Zauberzeichen SelectedRune { get { return _selectedRune; } set { _selectedRune = value; OnChanged("SelectedRune"); resetRunen(); if (value != null) { WertMerkmaleRunen = value.Merkmal; WertKomplexitätRunen = value.Komplexität; grundkomplexitätRunen = value.Komplexität; } else { WertMerkmaleRunen = null; WertKomplexitätRunen = 0; grundkomplexitätRunen = 0; };setWirkungsradiusRunen(); if (value != null)WertNameRunen = value.Name; } }
        public Talent SelectedTalentRunen { get { return _selectedTalentRunen; } set { _selectedTalentRunen = value; OnChanged("SelectedTalentRunen"); WertHandwerkRunen = SelectedHeld.Talentwert(value); } }
        public string SelectedWerkzeugRunen { get { return _selectedWerkzeugRunen; } set { _selectedWerkzeugRunen = value; OnChanged("SelectedWerkzeugRunen"); berechneAbhängigkeitenRunen(); } }
        //Werte
        public int WertKomplexitätRunen { get { return _wertKomplexitätRunen; } set { _wertKomplexitätRunen = value; OnChanged("WertKomplexitätRunen"); WertGrößeRunen = WertKomplexitätRunen * 2 + " Finger"; } }
        public int WertArtefaktkontrolleRunen { get { return _wertArtefaktkontrolleRunen; } set { _wertArtefaktkontrolleRunen = value; OnChanged("WertArtefaktkontrolleRunen"); } }
        public int WertTaPRunen { get { return _wertTaPRunen; } set { _wertTaPRunen = value; OnChanged("WertTaPRunen"); berechneAbhängigkeitenRunen(); } }
        public string WertHerstellungsdauerRunen { get { return _wertHerstellungsdauerRunen; } set { _wertHerstellungsdauerRunen = value; OnChanged("WertHerstellungsdauerRunen"); } }
        public string WertWirkungsdauerRunen { get { return _wertWirkungsdauerRunen; } set { _wertWirkungsdauerRunen = value; OnChanged("WertWirkungsdauerRunen"); } }
        public int WertErleichterungRkRunen { get { return _wertErleichterungRkRunen; } set { _wertErleichterungRkRunen = value; OnChanged("WertErleichterungRkRunen"); } }
        public string WertAktivierungsdauerRunen { get { return _wertAktivierungsdauerRunen; } set { _wertAktivierungsdauerRunen = value; OnChanged("WertAktivierungsdauerRunen"); } }
        public int WertAktivierungskostenRunen { get { return _wertAktivierungskostenRunen; } set { _wertAktivierungskostenRunen = value; OnChanged("WertAktivierungskostenRunen"); } }
        public string WertErkennenRunen { get { return _wertErkennenRunen; } set { _wertErkennenRunen = value; OnChanged("WertErkennenRunen"); } }
        public int WertHandwerkRunen { get { return _WertHandwerkRunen; } set { _WertHandwerkRunen = value; OnChanged("WertHandwerkRunen"); } }
        public int WertHerstellungszeitRunen { get { return _wertHerstellungszeitRunen; } set { int summe = value * 3 + WertAktivierungRunen + WertQualitätRunen; if (summe <= _wertTaPÜberRunen && value >= 0) { _wertHerstellungszeitRunen = value; WertTaPRunen = _wertTaPÜberRunen - _wertHerstellungszeitRunen * 3 - WertAktivierungRunen - WertQualitätRunen; OnChanged("WertHerstellungszeitRunen"); berechneAbhängigkeitenRunen(); } } }
        public int WertAktivierungRunen { get { return _wertAktivierungRunen; } set { int summe = _wertHerstellungszeitZauberzeichen * 3 + value + WertQualitätRunen; if (summe <= _wertTaPÜberRunen && value >= 0) { _wertAktivierungRunen = value; WertTaPRunen = _wertTaPÜberRunen - WertHerstellungszeitRunen * 3 - WertAktivierungRunen - WertQualitätRunen; OnChanged("WertAktivierungRunen"); berechneAbhängigkeitenRunen(); } } }
        public int WertQualitätRunen { get { return _wertQualitätRunen; } set { int summe = _wertHerstellungszeitRunen * 3 + WertAktivierungRunen + value; if (summe <= _wertTaPÜberRunen && value >= 0) { _wertQualitätRunen = value; WertTaPRunen = _wertTaPÜberRunen - WertHerstellungszeitRunen * 3 - WertAktivierungRunen - WertQualitätRunen; OnChanged("WertQualitätRunen"); berechneAbhängigkeitenRunen(); } } }
        public int WertFormRunen { get { return _wertFormRunen; } set { if (!(value > 3 || value < 0)) { _wertFormRunen = value; } OnChanged("WertFormRunen"); berechneAbhängigkeitenRunen(); } }
        public int WertAffinitätRunen { get { return _wertAffinitätRunen; } set { if (!(value > 25 || value < 0)) { _wertAffinitätRunen = value; } OnChanged("WertAffinitätRunen"); berechneAbhängigkeitenRunen(); } }
        public int WertRkPRunen { get { return _wertRkPRunen; } set { _wertRkPRunen = value; OnChanged("WertRkPRunen"); } }
        public int WertErleichterungTaRunen { get { return _wertErleichterungTaRunen; } set { _wertErleichterungTaRunen = value; OnChanged("WertErleichterungTaRunen"); } }
        public string WertMerkmaleRunen { get { return _wertMerkmaleRunen; } set { _wertMerkmaleRunen = value; OnChanged("WertMerkmaleRunen"); } }
        public string WertWirkungsradiusRunen { get { return _wertWirkungsradiusRunen; } set { _wertWirkungsradiusRunen = value; OnChanged("WertWirkungsradiusRunen"); } }
        public string WertNameRunen { get { return _wertNameRunen; } set { _wertNameRunen = value; OnChanged("WertNameRunen"); } }
        public int WertRkWErschafferRunen { get { return _wertRkWErschafferRunen; } set { _wertRkWErschafferRunen = value; OnChanged("WertRkWErschafferRunen"); } }
        public string WertGrößeRunen { get { return _wertGrößeRunen; } set { _wertGrößeRunen = value; OnChanged("WertGrößeRunen"); } }
        //Checkboxen 
        public bool CheckedSpontanzeichenRunen { get { return _checkedSpontanzeichenRunen; } set { _checkedSpontanzeichenRunen = value; OnChanged("CheckedSpontanzeichenRunen"); berechneAbhängigkeitenRunen(); } }
        public bool CheckedSpezialisierungRunen { get { return _checkedSpezialisierungRunen; } set { _checkedSpezialisierungRunen = value; OnChanged("CheckedSpezialisierungRunen"); if (value) WertHandwerkRunen = WertHandwerkRunen + 2; else WertHandwerkRunen = WertHandwerkRunen - 2; } }
        //Commands
        public Base.CommandBase OnBauRunen
        {
            get { return _onBauRunen; }
        }

        public Base.CommandBase OnAktivierungRunen
        {
            get { return _onAktivierungRunen; }
        }
        #endregion

        #region //-Kreise-
        //Enables 
        public bool IsEnabledSpontanzeichenKreise { get { return _isEnabledSpontanzeichenKreise; } set { _isEnabledSpontanzeichenKreise = value; OnChanged("IsEnabledSpontanzeichenKreise"); } }
        public bool IsEnabledSpezArtKreise { get { return _isEnabledSpezArtKreise; } set { _isEnabledSpezArtKreise = value; OnChanged("IsEnabledSpezArtKreise"); } }
        public bool IsEnabledSatinavsSiegelListeKreise { get { return _isEnabledSatinavsSiegelListeKreise; } set { _isEnabledSatinavsSiegelListeKreise = value; OnChanged("IsEnabledSatinavsSiegelListeKreise"); } }
        public bool IsEnabledKraftquellenspeisungKreise { get { return _isEnabledKraftquellenspeisungKreise; } set { _isEnabledKraftquellenspeisungKreise = value; OnChanged("IsEnabledKraftquellenspeisungKreise"); } }
        public bool IsEnabledMagiewiderstandKreise { get { return _isEnabledMagiewiderstandKreise; } set { _isEnabledMagiewiderstandKreise = value; OnChanged("IsEnabledMagiewiderstandKreise"); } }
        public bool IsEnabledSchutzsiegelKreise { get { return _isEnabledSchutzsiegelKreise; } set { _isEnabledSchutzsiegelKreise = value; OnChanged("IsEnabledSchutzsiegelKreise"); } }
        public bool IsEnabledTarnungKreise { get { return _isEnabledTarnungKreise; } set { _isEnabledTarnungKreise = value; OnChanged("IsEnabledTarnungKreise"); } }
        //Listen
        public List<Model.Zauberzeichen> KreiseListe { get { return _kreiseListe; } set { _kreiseListe = value; OnChanged("KreiseListe"); } }
        public List<string> SatinavsSiegelListeKreise { get { return _satinavsSiegelListe; } set { _satinavsSiegelListe = value; OnChanged("SatinavsSiegelListeKreise"); } }
        public List<Talent> TalentListeKreise { get { return _talentListeKreise; } set { _talentListeKreise = value; OnChanged("TalentListeKreise"); } }
        public List<string> ZusatzzeichenListeKreise { get { return _zusatzzeichenListeKreise; } set { _zusatzzeichenListeKreise = value; OnChanged("ZusatzzeichenListeKreise"); } }
        public List<string> WerkzeugListeKreise { get { return _werkzeugListe; } set { _werkzeugListe = value; OnChanged("WerkzeugListeKreise"); } }
        //Selections
        public Model.Zauberzeichen SelectedKreis { get { return _selectedKreis; } set { _selectedKreis = value; OnChanged("SelectedKreis"); resetKreise(); if (value != null) { WertMerkmaleKreise = value.Merkmal; WertKomplexitätKreise = value.Komplexität; grundkomplexitätKreise = value.Komplexität; } else { WertMerkmaleKreise = null; WertKomplexitätKreise = 0; grundkomplexitätKreise = 0; };setWirkungsradiusKreise(); if (value != null)WertNameKreise = value.Name; } }
        public Talent SelectedTalentKreise { get { return _selectedTalentKreise; } set { _selectedTalentKreise = value; OnChanged("SelectedTalentKreise"); WertHandwerkKreise = SelectedHeld.Talentwert(value); } }
        public string SelectedSatinavsSiegelKreise { get { return _selectedSatinavsSiegelKreise; } set { _selectedSatinavsSiegelKreise = value; OnChanged("SelectedSatinavsSiegelKreise"); berechneAbhängigkeitenKreise(); } }
        public string SelectedWerkzeugKreise { get { return _selectedWerkzeugKreise; } set { _selectedWerkzeugKreise = value; OnChanged("SelectedWerkzeugKreise"); berechneAbhängigkeitenKreise(); } }
        //Werte
        public int WertZirkelstärkeKreise { get { return _wertZirkelstärkeKreise; } set { _wertZirkelstärkeKreise = value; OnChanged("WertZirkelstärkeKreise"); } }
        public int WertKomplexitätKreise { get { return _wertKomplexitätKreise; } set { _wertKomplexitätKreise = value; OnChanged("WertKomplexitätKreise"); } }
        public int WertArtefaktkontrolleKreise { get { return _wertArtefaktkontrolleKreise; } set { _wertArtefaktkontrolleKreise = value; OnChanged("WertArtefaktkontrolleKreise"); } }
        public int WertTaPKreise { get { return _wertTaPKreise; } set { _wertTaPKreise = value; OnChanged("WertTaPKreise"); berechneAbhängigkeitenKreise(); } }
        public string WertHerstellungsdauerKreise { get { return _wertHerstellungsdauerKreise; } set { _wertHerstellungsdauerKreise = value; OnChanged("WertHerstellungsdauerKreise"); } }
        public string WertWirkungsdauerKreise { get { return _wertWirkungsdauerKreise; } set { _wertWirkungsdauerKreise = value; OnChanged("WertWirkungsdauerKreise"); } }
        public int WertErleichterungRkKreise { get { return _wertErleichterungRkKreise; } set { _wertErleichterungRkKreise = value; OnChanged("WertErleichterungRkKreise"); } }
        public string WertAktivierungsdauerKreise { get { return _wertAktivierungsdauerKreise; } set { _wertAktivierungsdauerKreise = value; OnChanged("WertAktivierungsdauerKreise"); } }
        public int WertAktivierungskostenKreise { get { return _wertAktivierungskostenKreise; } set { _wertAktivierungskostenKreise = value; OnChanged("WertAktivierungskostenKreise"); } }
        public string WertErkennenKreise { get { return _wertErkennenKreise; } set { _wertErkennenKreise = value; OnChanged("WertErkennenKreise"); } }
        public int WertHandwerkKreise { get { return _WertHandwerkKreise; } set { _WertHandwerkKreise = value; OnChanged("WertHandwerkKreise"); } }
        public int WertHerstellungszeitKreise { get { return _wertHerstellungszeitKreise; } set { int summe = value * 3 + WertAktivierungKreise + WertQualitätKreise; if (summe <= _wertTaPÜberKreise && value >= 0) { _wertHerstellungszeitKreise = value; WertTaPKreise = _wertTaPÜberKreise - _wertHerstellungszeitKreise * 3 - WertAktivierungKreise - WertQualitätKreise; OnChanged("WertHerstellungszeitKreise"); berechneAbhängigkeitenKreise(); } } }
        public int WertAktivierungKreise { get { return _wertAktivierungKreise; } set { int summe = _wertHerstellungszeitZauberzeichen * 3 + value + WertQualitätKreise; if (summe <= _wertTaPÜberKreise && value >= 0) { _wertAktivierungKreise = value; WertTaPKreise = _wertTaPÜberKreise - WertHerstellungszeitKreise * 3 - WertAktivierungKreise - WertQualitätKreise; OnChanged("WertAktivierungKreise"); berechneAbhängigkeitenKreise(); } } }
        public int WertQualitätKreise { get { return _wertQualitätKreise; } set { int summe = _wertHerstellungszeitKreise * 3 + WertAktivierungKreise + value; if (summe <= _wertTaPÜberKreise && value >= 0) { _wertQualitätKreise = value; WertTaPKreise = _wertTaPÜberKreise - WertHerstellungszeitKreise * 3 - WertAktivierungKreise - WertQualitätKreise; OnChanged("WertQualitätKreise"); berechneAbhängigkeitenKreise(); } } }
        public int WertFormKreise { get { return _wertFormKreise; } set { if (!(value > 3 || value < 0)) { _wertFormKreise = value; } OnChanged("WertFormKreise"); berechneAbhängigkeitenKreise(); } }
        public int WertFarbeKreise { get { return _wertFarbeKreise; } set { if (!(value > 2 || value < 0)) { _wertFarbeKreise = value; } OnChanged("WertFarbeKreise"); berechneAbhängigkeitenKreise(); } }
        public int WertAffinitätKreise { get { return _wertAffinitätKreise; } set { if (!(value > 25 || value < 0)) { _wertAffinitätKreise = value; } OnChanged("WertAffinitätKreise"); berechneAbhängigkeitenKreise(); } }
        public int WertRkPKreise { get { return _wertRkPKreise; } set { _wertRkPKreise = value; OnChanged("WertRkPKreise"); } }
        public int WertErleichterungTaKreise { get { return _wertErleichterungTaKreise; } set { _wertErleichterungTaKreise = value; OnChanged("WertErleichterungTaKreise"); } }
        public string WertGrößeKreisKreise { get { return _wertGrößeKreisKreise; } set { _wertGrößeKreisKreise = value; OnChanged("WertGrößeKreisKreise"); } }
        public string WertMerkmaleKreise { get { return _wertMerkmaleKreise; } set { _wertMerkmaleKreise = value; OnChanged("WertMerkmaleKreise"); } }
        public string WertWirkungsradiusKreise { get { return _wertWirkungsradiusKreise; } set { _wertWirkungsradiusKreise = value; OnChanged("WertWirkungsradiusKreise"); } }
        public string WertNameKreise { get { return _wertNameKreise; } set { _wertNameKreise = value; OnChanged("WertNameKreise"); } }
        public int WertRkWErschafferKreise { get { return _wertRkWErschafferKreise; } set { _wertRkWErschafferKreise = value; OnChanged("WertRkWErschafferKreise"); } }
        //Checkboxen
        public bool CheckedSpontanzeichenKreise { get { return _checkedSpontanzeichenKreise; } set { _checkedSpontanzeichenKreise = value; OnChanged("CheckedSpontanzeichenKreise"); if (value) SelectedSatinavsSiegelKreise = SatinavsSiegelListeKreise[0]; IsEnabledSatinavsSiegelListeKreise = !value; berechneAbhängigkeitenKreise(); } }
        public bool CheckedSpezialisierungKreise { get { return _checkedSpezialisierungKreise; } set { _checkedSpezialisierungKreise = value; OnChanged("CheckedSpezialisierungKreise"); if (value) WertHandwerkKreise = WertHandwerkKreise + 2; else WertHandwerkKreise = WertHandwerkKreise - 2; } }
        public bool CheckedKraftquellenspeisungKreise { get { return _checkedKraftquellenspeisungKreise; } set { _checkedKraftquellenspeisungKreise = value; OnChanged("CheckedKraftquellenspeisungKreise"); berechneAbhängigkeitenKreise(); } }
        public bool CheckedMagiewiderstandKreise { get { return _checkedMagiewiderstandKreise; } set { _checkedMagiewiderstandKreise = value; OnChanged("CheckedMagiewiderstandKreise"); berechneAbhängigkeitenKreise(); } }
        public bool CheckedSchutzsiegelKreise { get { return _checkedSchutzsiegelKreise; } set { _checkedSchutzsiegelKreise = value; OnChanged("CheckedSchutzsiegelKreise"); berechneAbhängigkeitenKreise(); } }
        public bool CheckedTarnungKreise { get { return _checkedTarnungKreise; } set { _checkedTarnungKreise = value; OnChanged("CheckedTarnungKreise"); berechneAbhängigkeitenKreise(); } }
        public bool CheckedBestimmteArtKreise { get { return _checkedBestimmteArtKreise; } set { _checkedBestimmteArtKreise = value; OnChanged("CheckedBestimmteArtKreise"); if (!value) CheckedSpezifscheArtKreise = value; IsEnabledSpezArtKreise = value; if (value) WertZirkelstärkeKreise += 3; else WertZirkelstärkeKreise -= 3; } }
        public bool CheckedSpezifscheArtKreise { get { return _checkedSpezifscheArtKreise; } set { _checkedSpezifscheArtKreise = value; OnChanged("CheckedSpezifscheArtKreise"); if (value) WertZirkelstärkeKreise += 3; else WertZirkelstärkeKreise -= 3; } }
        //Commands
        public Base.CommandBase OnBauKreise
        {
            get { return _onBauKreise; }
        }

        public Base.CommandBase OnAktivierungKreise
        {
            get { return _onAktivierungKreise; }
        }
        #endregion

        #region //-Analyse-
        #endregion

        #endregion

        #region //---- KONSTRUKTOR ----

        public ZauberzeichenViewModel()
        {
            _onBauZauberzeichen = new Base.CommandBase(BauZauberzeichen, null);
            _onAktivierungZauberzeichen = new Base.CommandBase(AktivierungZauberzeichen, null);
            _onBauRunen = new Base.CommandBase(BauRunen, null);
            _onAktivierungRunen = new Base.CommandBase(AktivierungRunen, null);
            _onBauKreise = new Base.CommandBase(BauKreise, null);
            _onAktivierungKreise = new Base.CommandBase(AktivierungKreise, null);

            Init();
        }

        #endregion

        #region //---- INSTANZMETHODEN ----

        public void Init()
        {
            if (IsLoaded == false)
            {
                resetZauberzeichen();
                resetRunen();
                resetKreise();
                Refresh();
                IsLoaded = true;
            }
        }

        public void Refresh()
        {
            //Nur Helden mit entsprechender Sonderfertigkeit
            OnChanged("HeldListe");
        }

        private void resetKreise()
        {
            if (SelectedHeld != null)
            {
                if (SelectedHeld.HatSonderfertigkeitUndVoraussetzungen("Spontanzeichen (Arkanoglyphen)")) IsEnabledSpontanzeichenKreise = true;
                else IsEnabledSpontanzeichenKreise = false;
            }
            CheckedBestimmteArtKreise = false;
            CheckedKraftquellenspeisungKreise = false;
            CheckedMagiewiderstandKreise = false;
            CheckedSchutzsiegelKreise = false;
            CheckedSpezialisierungKreise = false;
            CheckedSpezifscheArtKreise = false;
            CheckedSpontanzeichenKreise = false;
            CheckedTarnungKreise = false;
            WertFarbeKreise = 0;
            WertFormKreise = 0;
            WertAktivierungKreise = 0;
            WertHerstellungszeitKreise = 0;
            WertQualitätKreise = 0;
            WertRkPKreise = 0;
            WertTaPKreise = 0;
            WertAffinitätKreise = 0;
            WertHandwerkKreise = 0;
            WertZirkelstärkeKreise = 0;
            SelectedSatinavsSiegelKreise = SatinavsSiegelListeKreise[0];
            SelectedWerkzeugKreise = WerkzeugListeKreise[0];
        }

        private void resetRunen()
        {
            if (SelectedHeld != null)
            {
                if (SelectedHeld.HatSonderfertigkeitUndVoraussetzungen("Spontanzeichen (Runenkunde)")) IsEnabledSpontanzeichenRunen = true;
                else IsEnabledSpontanzeichenRunen = false;
            }
            CheckedSpezialisierungRunen = false;
            WertFormRunen = 0;
            WertAktivierungRunen = 0;
            WertHerstellungszeitRunen = 0;
            WertQualitätRunen = 0;
            WertRkPRunen = 0;
            WertTaPRunen = 0;
            WertAffinitätRunen = 0;
            WertHandwerkRunen = 0;
            WertGrößeRunen = 0 + " Finger";
            SelectedWerkzeugRunen = WerkzeugListeRunen[0];
        }

        private void resetZauberzeichen()
        {
            if (SelectedHeld != null)
            {
                if (SelectedHeld.HatSonderfertigkeitUndVoraussetzungen("Spontanzeichen (Arkanoglyphen)")) IsEnabledSpontanzeichenZauberzeichen = true;
                else IsEnabledSpontanzeichenZauberzeichen = false;
            }
            CheckedKraftquellenspeisungZauberzeichen = false;
            CheckedMagiewiderstandZauberzeichen = false;
            CheckedPotenzierungZauberzeichen = false;
            CheckedSchutzsiegelZauberzeichen = false;
            CheckedSpezialisierungZauberzeichen = false;
            CheckedSpontanzeichenZauberzeichen = false;
            CheckedTarnungZauberzeichen = false;
            CheckedZielbeschränkungZauberzeichen = false;
            WertFarbeZauberzeichen = 0;
            WertFormZauberzeichen = 0;
            WertAktivierungZauberzeichen = 0;
            WertHerstellungszeitZauberzeichen = 0;
            WertQualitätZauberzeichen = 0;
            WertRkPZauberzeichen = 0;
            WertTaPZauberzeichen = 0;
            WertAffinitätZauberzeichen = 0;
            WertHandwerkZauberzeichen = 0;
            WertGrößeZauberzeichen = 0 + " Finger";
            SelectedSatinavsSiegelZauberzeichen = SatinavsSiegelListeZauberzeichen[0];
            SelectedVerkleinerungZauberzeichen = VerkleinerungListeZauberzeichen[0];
            SelectedGrößeZeichenträgerZauberzeichen = GrößenZeichenträgerListeZauberzeichen[0];
            SelectedWerkzeugZauberzeichen = WerkzeugListeZauberzeichen[0];
        }

        private void berechneAbhängigkeitenZauberzeichen()
        {
            //update Name
            if (SelectedZauberzeichen != null)
            {
                if (CheckedSpontanzeichenZauberzeichen) WertNameZauberzeichen = SelectedZauberzeichen.Name + " (Spontanzeichen)";
                else WertNameZauberzeichen = SelectedZauberzeichen.Name;
            }
            else WertNameZauberzeichen = "";
            //Berechne Erleichterung TaP
            switch (SelectedWerkzeugZauberzeichen)
            {
                case "normal": WertErleichterungTaZauberzeichen = 0; break;
                case "beschädigt": WertErleichterungTaZauberzeichen = +3; break;
                case "hochwertig": WertErleichterungTaZauberzeichen = -3; break;
                case "aussergew. hochwertig": WertErleichterungTaZauberzeichen = -7; break;
                default: WertErleichterungTaZauberzeichen = 0; break;
            }
            //Berechne Erleichterung RkP
            WertErleichterungRkZauberzeichen = -WertFarbeZauberzeichen - WertFormZauberzeichen - WertAktivierungZauberzeichen;
            //Berechne Erkennen            
            WertErkennenZauberzeichen = "+" + WertQualitätZauberzeichen * 2 + " (+" + WertQualitätZauberzeichen + ")";
            //Berechne Anzahl aufgrund von Größe des Zeichenträgers
            switch (SelectedGrößeZeichenträgerZauberzeichen)
            {
                case "Kutsche / 2xRadius": WertAnzahlZauberzeichen = 3; break;
                case "Schiff / 4xRadius": WertAnzahlZauberzeichen = 6; break;
                case "Burg / 8xRadius": WertAnzahlZauberzeichen = 10; break;
                case "Berg / 16xRadius": WertAnzahlZauberzeichen = 15; break;
                default: WertAnzahlZauberzeichen = 1; break;
            }
            //berechne Komplexität
            int komp = grundkomplexitätZauberzeichen;
            if (CheckedKraftquellenspeisungZauberzeichen) komp += 5;
            if (CheckedMagiewiderstandZauberzeichen) komp += 2;
            if (CheckedSchutzsiegelZauberzeichen) komp += 3;
            if (CheckedTarnungZauberzeichen) komp += 2;
            if (CheckedZielbeschränkungZauberzeichen) komp += 2;
            if (CheckedPotenzierungZauberzeichen)
            {
                switch (SelectedGrößeZeichenträgerZauberzeichen)
                {
                    case "Kutsche / 2xRadius": komp += 2; break;
                    case "Schiff / 4xRadius": komp += 4; break;
                    case "Burg / 8xRadius": komp += 6; break;
                    case "Berg / 16xRadius": komp += 8; break;
                }
                WertAnzahlZauberzeichen = 1;
            }
            switch (SelectedSatinavsSiegelZauberzeichen)
            {
                case "Monat": komp += 1; break;
                case "Quartal": komp += 2; break;
                case "WSWende": komp += 3; break;
            }
            switch (SelectedVerkleinerungZauberzeichen)
            {
                case "1/2": komp += 1; break;
                case "1/4": komp += 2; break;
                case "1/8": komp += 3; break;
            }
            WertKomplexitätZauberzeichen = komp;
            //Berechne Kosten
            double modi = 1.0 - (((double)WertAffinitätZauberzeichen / 100) + (WertFormZauberzeichen != 0 ? 10 / 3 * (double)WertFormZauberzeichen / 100 : 0.0));
            WertAktivierungskostenZauberzeichen = (int)Math.Round(((double)komp * 3) * modi, MidpointRounding.AwayFromZero);
            //Berechne Herstellungszeit
            double herstellung = (double)komp * 2 / 3;
            for (int i = 0; i < WertHerstellungszeitZauberzeichen; i++)
            {
                herstellung *= 0.5;
            }
            if (CheckedSpontanzeichenZauberzeichen) WertHerstellungsdauerZauberzeichen = decimal.Round((decimal)(herstellung), 2, MidpointRounding.AwayFromZero) + " SR";
            else WertHerstellungsdauerZauberzeichen = decimal.Round((decimal)(herstellung * 2), 2, MidpointRounding.AwayFromZero) + " Stunden";
            //Berechne Aktivierungszeit
            if (CheckedSpontanzeichenZauberzeichen) WertAktivierungsdauerZauberzeichen = decimal.Round((decimal)((double)komp / 3), 2, MidpointRounding.AwayFromZero) + " SR";
            else WertAktivierungsdauerZauberzeichen = decimal.Round((decimal)((double)komp / 3 * 2), 2, MidpointRounding.AwayFromZero) + " Stunden";
            //Berechne Größe
            if (CheckedPotenzierungZauberzeichen)
            {
                switch (SelectedGrößeZeichenträgerZauberzeichen)
                {
                    case "Kutsche / 2xRadius": switch (SelectedVerkleinerungZauberzeichen)
                        {
                            case "1/2": WertGrößeZauberzeichen = decimal.Round((decimal)(((double)(komp - 3) * 10 / 50 / 2)), 2, MidpointRounding.AwayFromZero) + " Schritt"; break;
                            case "1/4": WertGrößeZauberzeichen = decimal.Round((decimal)(((double)(komp - 4) * 10 / 50 / 4)), 2, MidpointRounding.AwayFromZero) + " Schritt"; break;
                            case "1/8": WertGrößeZauberzeichen = decimal.Round((decimal)(((double)(komp - 5) * 10 / 8)), 2, MidpointRounding.AwayFromZero) + " Finger"; break;
                            default: WertGrößeZauberzeichen = decimal.Round((decimal)(((double)(komp - 2) * 10 / 50)), 2, MidpointRounding.AwayFromZero) + " Schritt"; break;
                        }; break;
                    case "Schiff / 4xRadius": switch (SelectedVerkleinerungZauberzeichen)
                        {
                            case "1/2": WertGrößeZauberzeichen = decimal.Round((decimal)(((double)(komp - 5) * 25 / 50 / 2)), 2, MidpointRounding.AwayFromZero) + " Schritt"; break;
                            case "1/4": WertGrößeZauberzeichen = decimal.Round((decimal)(((double)(komp - 6) * 25 / 50 / 4)), 2, MidpointRounding.AwayFromZero) + " Schritt"; break;
                            case "1/8": WertGrößeZauberzeichen = decimal.Round((decimal)(((double)(komp - 8) * 25 / 8)), 2, MidpointRounding.AwayFromZero) + " Finger"; break;
                            default: WertGrößeZauberzeichen = decimal.Round((decimal)(((double)(komp - 4) * 25 / 50)), 2, MidpointRounding.AwayFromZero) + " Schritt"; break;
                        }; break;
                    case "Burg / 8xRadius": switch (SelectedVerkleinerungZauberzeichen)
                        {
                            case "1/2": WertGrößeZauberzeichen = decimal.Round((decimal)(((double)(komp - 7) / 2)), 2, MidpointRounding.AwayFromZero) + " Schritt"; break;
                            case "1/4": WertGrößeZauberzeichen = decimal.Round((decimal)(((double)(komp - 8) / 4)), 2, MidpointRounding.AwayFromZero) + " Schritt"; break;
                            case "1/8": WertGrößeZauberzeichen = decimal.Round((decimal)(((double)(komp - 9) * 5 / 8)), 2, MidpointRounding.AwayFromZero) + " Spann"; break;
                            default: WertGrößeZauberzeichen = decimal.Round((decimal)(((double)(komp - 6))), 2, MidpointRounding.AwayFromZero) + " Schritt"; break;
                        }; break;
                    case "Berg / 16xRadius": switch (SelectedVerkleinerungZauberzeichen)
                        {
                            case "1/2": WertGrößeZauberzeichen = decimal.Round((decimal)(((double)komp - 9) * 5 / 2), 2, MidpointRounding.AwayFromZero) + " Schritt"; break;
                            case "1/4": WertGrößeZauberzeichen = decimal.Round((decimal)(((double)komp - 10) * 5 / 4), 2, MidpointRounding.AwayFromZero) + " Schritt"; break;
                            case "1/8": WertGrößeZauberzeichen = decimal.Round((decimal)(((double)komp - 11) * 5 / 8), 2, MidpointRounding.AwayFromZero) + " Schritt"; break;
                            default: WertGrößeZauberzeichen = decimal.Round((decimal)(((double)komp - 8) * 5), 2, MidpointRounding.AwayFromZero) + " Schritt"; break;
                        }; break;
                    default: switch (SelectedVerkleinerungZauberzeichen)
                        {
                            case "1/2": WertGrößeZauberzeichen = komp - 1 + " Finger"; break;
                            case "1/4": WertGrößeZauberzeichen = komp - 2 + " Halbinger"; break;
                            case "1/8": WertGrößeZauberzeichen = (komp - 3) / 2 + " Halbfinger"; break;
                            default: WertGrößeZauberzeichen = komp * 2 + " Finger"; break;
                        }; break;
                }
            }
            else
            {
                switch (SelectedVerkleinerungZauberzeichen)
                {
                    case "1/2": WertGrößeZauberzeichen = komp - 1 + " Finger"; break;
                    case "1/4": WertGrößeZauberzeichen = komp - 2 + " Halbinger"; break;
                    case "1/8": WertGrößeZauberzeichen = (komp - 3) / 2 + " Halbfinger"; break;
                    default: WertGrößeZauberzeichen = komp * 2 + " Finger"; break;
                };
            }
            //berechne Artefaktkontrolle
            WertArtefaktkontrolleZauberzeichen = 1;
            //berechne Wirkungsdauer
            if (CheckedSpontanzeichenZauberzeichen) WertWirkungsdauerZauberzeichen = "bis Sonnenaufgang";
            else
            {
                switch (SelectedSatinavsSiegelZauberzeichen)
                {
                    case "Monat": WertWirkungsdauerZauberzeichen = 1 + " Monat"; break;
                    case "Quartal": WertWirkungsdauerZauberzeichen = 1 + " Quartal"; break;
                    case "WSWende": WertWirkungsdauerZauberzeichen = "bis Wintersonnenwende"; break;
                    default: WertWirkungsdauerZauberzeichen = WertRitualkenntnis / 2 + " Tage"; break;
                }
            }
            //update Zusatzzeichenliste
            List<string> zusatzzeichen = new List<string>();
            switch (SelectedSatinavsSiegelZauberzeichen)
            {
                case "Monat":
                case "Quartal":
                case "WSWende": zusatzzeichen.Add("Satinavs Siegel"); break;
                default: break;
            }
            switch (SelectedVerkleinerungZauberzeichen)
            {
                case "1/2":
                case "1/4":
                case "1/8": zusatzzeichen.Add("Verkleinerung"); break;
                default: break;
            };
            if (CheckedPotenzierungZauberzeichen)
            {
                switch (SelectedGrößeZeichenträgerZauberzeichen)
                {
                    case "Kutsche / 2xRadius": zusatzzeichen.Add("Potenzierung (Kutsche / 2xRadius)"); break;
                    case "Schiff / 4xRadius": zusatzzeichen.Add("Potenzierung (Schiff / 4xRadius)"); break;
                    case "Burg / 8xRadius": zusatzzeichen.Add("Potenzierung (Burg / 8xRadius)"); break;
                    case "Berg / 16xRadius": zusatzzeichen.Add("Potenzierung (Berg / 16xRadius)"); break;
                    default: break;
                }
            }
            if (CheckedKraftquellenspeisungZauberzeichen) zusatzzeichen.Add("Kraftquellenspeisung");
            if (CheckedMagiewiderstandZauberzeichen) zusatzzeichen.Add("Magiewiderstand");
            if (CheckedSchutzsiegelZauberzeichen) zusatzzeichen.Add("Schutzsiegel");
            if (CheckedTarnungZauberzeichen) zusatzzeichen.Add("Tarnung");
            if (CheckedZielbeschränkungZauberzeichen) zusatzzeichen.Add("Zielbeschränkung");
            ZusatzzeichenListeZauberzeichen = zusatzzeichen;
        }

        private void berechneAbhängigkeitenRunen()
        {
            //update Name
            if (SelectedRune != null)
            {
                if (CheckedSpontanzeichenRunen) WertNameRunen = SelectedRune.Name + " (Spontanzeichen)";
                else WertNameRunen = SelectedRune.Name;
            }
            //Berechne Erleichterung TaP
            switch (SelectedWerkzeugRunen)
            {
                case "normal": WertErleichterungTaRunen = 0; break;
                case "beschädigt": WertErleichterungTaRunen = +3; break;
                case "hochwertig": WertErleichterungTaRunen = -3; break;
                case "aussergew. hochwertig": WertErleichterungTaRunen = -7; break;
                default: WertErleichterungTaRunen = 0; break;
            }
            //Berechne Erleichterung RkP
            WertErleichterungRkRunen = -WertFormRunen - WertAktivierungRunen;
            //Berechne Erkennen            
            WertErkennenRunen = "+" + WertQualitätRunen * 2 + " (+" + WertQualitätRunen + ")";
            //berechne Komplexität
            int komp = grundkomplexitätRunen;
            WertKomplexitätRunen = komp;
            //Berechne Kosten
            double modi = 1.0 - (((double)WertAffinitätRunen / 100) + (WertFormRunen != 0 ? 10 / 3 * (double)WertFormRunen / 100 : 0.0));
            WertAktivierungskostenRunen = (int)Math.Round(((double)komp * 3) * modi, MidpointRounding.AwayFromZero);
            //Berechne Wirkungsdauer
            //Berechne Herstellungszeit
            double herstellung = (double)komp * 2 / 3;
            for (int i = 0; i < WertHerstellungszeitRunen; i++)
            {
                herstellung *= 0.5;
            }
            if (CheckedSpontanzeichenRunen) WertHerstellungsdauerRunen = decimal.Round((decimal)(herstellung), 2, MidpointRounding.AwayFromZero) + " SR";
            else WertHerstellungsdauerRunen = decimal.Round((decimal)(herstellung * 2), 2, MidpointRounding.AwayFromZero) + " Stunden";
            //Berechne Aktivierungszeit
            if (CheckedSpontanzeichenRunen) WertAktivierungsdauerRunen = decimal.Round((decimal)((double)komp / 3), 2, MidpointRounding.AwayFromZero) + " SR";
            else WertAktivierungsdauerRunen = decimal.Round((decimal)((double)komp / 3 * 2), 2, MidpointRounding.AwayFromZero) + " Stunden";
            //berechne Artefaktkontrolle
            WertArtefaktkontrolleRunen = 1;
            //berechne Wirkungsdauer
            if (CheckedSpontanzeichenRunen) WertWirkungsdauerRunen = "bis Sonnenaufgang";
            else
            {
                WertWirkungsdauerRunen = "bis Sommersonnenwende";
            }
            //update Zusatzzeichenliste
        }

        private void berechneAbhängigkeitenKreise()
        {
            //update Name
            if (SelectedKreis != null)
            {
                if (CheckedSpontanzeichenKreise) WertNameKreise = SelectedKreis.Name + " (Spontanzeichen)";
                else WertNameKreise = SelectedKreis.Name;
            }
            //Berechne Erleichterung TaP
            switch (SelectedWerkzeugKreise)
            {
                case "normal": WertErleichterungTaKreise = 0; break;
                case "beschädigt": WertErleichterungTaKreise = +3; break;
                case "hochwertig": WertErleichterungTaKreise = -3; break;
                case "aussergew. hochwertig": WertErleichterungTaKreise = -7; break;
                default: WertErleichterungTaKreise = 0; break;
            }
            //Berechne Erleichterung RkP
            WertErleichterungRkKreise = -WertFarbeKreise - WertFormKreise - WertAktivierungKreise;
            //Berechne Erkennen            
            WertErkennenKreise = "+" + WertQualitätKreise * 2 + " (+" + WertQualitätKreise + ")";
            //berechne Komplexität
            int komp = grundkomplexitätKreise;
            if (CheckedKraftquellenspeisungKreise) komp += 5;
            if (CheckedMagiewiderstandKreise) komp += 2;
            if (CheckedSchutzsiegelKreise) komp += 3;
            if (CheckedTarnungKreise) komp += 2;
            switch (SelectedSatinavsSiegelKreise)
            {
                case "Monat": komp += 1; break;
                case "Quartal": komp += 2; break;
                case "WSWende": komp += 3; break;
            }
            WertKomplexitätKreise = komp;
            //Berechne Kosten
            double modi = 1.0 - (((double)WertAffinitätKreise / 100) + (WertFormKreise != 0 ? 10 / 3 * (double)WertFormKreise / 100 : 0.0));
            WertAktivierungskostenKreise = (int)Math.Round(((double)komp * 3) * modi, MidpointRounding.AwayFromZero);
            //Berechne Wirkungsdauer
            //Berechne Herstellungszeit
            double herstellung = (double)komp * 2 / 3;
            for (int i = 0; i < WertHerstellungszeitKreise; i++)
            {
                herstellung *= 0.5;
            }
            if (CheckedSpontanzeichenKreise) WertHerstellungsdauerKreise = decimal.Round((decimal)(herstellung), 2, MidpointRounding.AwayFromZero) + " SR";
            else WertHerstellungsdauerKreise = decimal.Round((decimal)(herstellung * 2), 2, MidpointRounding.AwayFromZero) + " Stunden";
            //Berechne Aktivierungszeit
            if (CheckedSpontanzeichenKreise) WertAktivierungsdauerKreise = decimal.Round((decimal)((double)komp / 3), 2, MidpointRounding.AwayFromZero) + " SR";
            else WertAktivierungsdauerKreise = decimal.Round((decimal)((double)komp / 3 * 2), 2, MidpointRounding.AwayFromZero) + " Stunden";
            //berechne Artefaktkontrolle
            WertArtefaktkontrolleKreise = 1;
            //berechne Wirkungsdauer
            if (CheckedSpontanzeichenKreise) WertWirkungsdauerKreise = "bis Sonnenaufgang";
            else
            {
                switch (SelectedSatinavsSiegelKreise)
                {
                    case "Monat": WertWirkungsdauerKreise = 1 + " Monat"; break;
                    case "Quartal": WertWirkungsdauerKreise = 1 + " Quartal"; break;
                    case "WSWende": WertWirkungsdauerKreise = "bis Wintersonnenwende"; break;
                    default: WertWirkungsdauerKreise = WertRitualkenntnis / 2 + " Tage"; break;
                }
            }
            //update Zusatzzeichenliste
            List<string> zusatzzeichen = new List<string>();
            switch (SelectedSatinavsSiegelKreise)
            {
                case "Monat":
                case "Quartal":
                case "WSWende": zusatzzeichen.Add("Satinavs Siegel"); break;
                default: break;
            }
            if (CheckedKraftquellenspeisungKreise) zusatzzeichen.Add("Kraftquellenspeisung");
            if (CheckedMagiewiderstandKreise) zusatzzeichen.Add("Magiewiderstand");
            if (CheckedSchutzsiegelKreise) zusatzzeichen.Add("Schutzsiegel");
            if (CheckedTarnungKreise) zusatzzeichen.Add("Tarnung");
            ZusatzzeichenListeKreise = zusatzzeichen;
        }

        private void checkZusatzzeichen(Held held)
        {
            if (held == null)
                return;
            //Zauberzeichen
            IsEnabledSatinavsSiegelListeZauberzeichen = held.HatSonderfertigkeitUndVoraussetzungen("Zauberzeichen: Zusatzzeichen Satinavs Siegel");
            IsEnabledVerkleinerungListeZauberzeichen = held.HatSonderfertigkeitUndVoraussetzungen("Zauberzeichen: Zusatzzeichen Verkleinerung");
            IsEnabledTarnungZauberzeichen = held.HatSonderfertigkeitUndVoraussetzungen("Zauberzeichen: Zusatzzeichen Tarnung");
            IsEnabledSchutzsiegelZauberzeichen = held.HatSonderfertigkeitUndVoraussetzungen("Zauberzeichen: Zusatzzeichen Schutzsiegel");
            IsEnabledPotenzierungZauberzeichen = held.HatSonderfertigkeitUndVoraussetzungen("Zauberzeichen: Zusatzzeichen Potenzierung");
            IsEnabledMagiewiderstandZauberzeichen = held.HatSonderfertigkeitUndVoraussetzungen("Zauberzeichen: Zusatzzeichen Magiewiderstand");
            IsEnabledKraftquellenspeisungZauberzeichen = held.HatSonderfertigkeitUndVoraussetzungen("Zauberzeichen: Zusatzzeichen Kraftquellenspeisung");
            IsEnabledZielbeschränkungZauberzeichen = held.HatSonderfertigkeitUndVoraussetzungen("Zauberzeichen: Zusatzzeichen Zielbeschränkung");
            //Runen
            //Kreise
            IsEnabledSatinavsSiegelListeKreise = held.HatSonderfertigkeitUndVoraussetzungen("Zauberzeichen: Zusatzzeichen Satinavs Siegel");
            IsEnabledTarnungKreise = held.HatSonderfertigkeitUndVoraussetzungen("Zauberzeichen: Zusatzzeichen Tarnung");
            IsEnabledSchutzsiegelKreise = held.HatSonderfertigkeitUndVoraussetzungen("Zauberzeichen: Zusatzzeichen Schutzsiegel");
            IsEnabledMagiewiderstandKreise = held.HatSonderfertigkeitUndVoraussetzungen("Zauberzeichen: Zusatzzeichen Magiewiderstand");
            IsEnabledKraftquellenspeisungKreise = held.HatSonderfertigkeitUndVoraussetzungen("Zauberzeichen: Zusatzzeichen Kraftquellenspeisung");

        }

        private void setWirkungsradiusZauberzeichen()
        {
            if (SelectedZauberzeichen != null)
            {
                if (SelectedZauberzeichen.ReichweitenDivisor != 0)
                {
                    int radius = (int)(WertRitualkenntnis / SelectedZauberzeichen.ReichweitenDivisor);
                    switch (SelectedGrößeZeichenträgerZauberzeichen)
                    {
                        case "Kutsche / 2xRadius": radius *= 2; break;
                        case "Schiff / 4xRadius": radius *= 4; break;
                        case "Burg / 8xRadius": radius *= 8; break;
                        case "Berg / 16xRadius": radius *= 16; break;
                        default: break;
                    }
                    WertWirkungsradiusZauberzeichen = radius + " Schritt";
                    if (SelectedZauberzeichen.Name == "Zähne des Feuers") WertWirkungsradiusZauberzeichen = radius + " Schritt Tiefe";
                }
                else WertWirkungsradiusZauberzeichen = 0 + " Schritt";
            }
        }
        private void setWirkungsradiusRunen()
        {
            if (SelectedRune != null)
            {
                if (SelectedRune.ReichweitenDivisor != 0)
                {
                    WertWirkungsradiusRunen = WertRitualkenntnis / SelectedRune.ReichweitenDivisor + " Schritt";
                }
                else WertWirkungsradiusRunen = 0 + " Schritt";
            }
        }
        private void setWirkungsradiusKreise()
        {
            if (SelectedKreis != null)
            {
                if (SelectedKreis.ReichweitenDivisor != 0)
                {
                    WertWirkungsradiusKreise = WertRitualkenntnis / SelectedKreis.ReichweitenDivisor + " Schritt";
                }
                else WertWirkungsradiusKreise = 0 + " Schritt";
            }
        }
        #endregion

        #region //---- EVENTS ----

        void BauZauberzeichen(object sender)
        {
            //Reset values
            WertHerstellungszeitZauberzeichen = 0;
            WertAktivierungZauberzeichen = 0;
            WertQualitätZauberzeichen = 0;
            //TODO ??: Probe auf Erstellung würfeln
            WertTaPZauberzeichen = WertHandwerkZauberzeichen - 3;
            _wertTaPÜberZauberzeichen = WertTaPZauberzeichen;
        }

        void AktivierungZauberzeichen(object sender)
        {
            //TODO ??: Probe auf Aktivierung würfeln
            int rkpstern = 5;
            if (CheckedSpontanzeichenZauberzeichen) WertRkPZauberzeichen = (int)Math.Round(((double)rkpstern / 2), MidpointRounding.AwayFromZero);
            else WertRkPZauberzeichen = rkpstern;
        }

        void BauRunen(object sender)
        {
            //Reset values
            WertHerstellungszeitRunen = 0;
            WertAktivierungRunen = 0;
            WertQualitätRunen = 0;
            //TODO ??: Probe auf Erstellung würfeln
            WertTaPRunen = WertHandwerkRunen - 3;
            _wertTaPÜberRunen = WertTaPRunen;
        }

        void AktivierungRunen(object sender)
        {
            //TODO ??: Probe auf Aktivierung würfeln
            int rkpstern = 5;
            WertRkPRunen = rkpstern;
        }

        void BauKreise(object sender)
        {
            //Reset values
            WertHerstellungszeitKreise = 0;
            WertAktivierungKreise = 0;
            WertQualitätKreise = 0;
            //TODO ??: Probe auf Erstellung würfeln
            WertTaPKreise = WertHandwerkKreise - 3;
            _wertTaPÜberKreise = WertTaPKreise;
        }

        void AktivierungKreise(object sender)
        {
            //TODO ??: Probe auf Aktivierung würfeln
            int rkpstern = 5;
            if (CheckedSpontanzeichenKreise) WertRkPKreise = (int)Math.Round(((double)rkpstern / 2), MidpointRounding.AwayFromZero);
            else WertRkPKreise = rkpstern;
            WertZirkelstärkeKreise += WertRkPKreise;
        }
        #endregion

    }
}
