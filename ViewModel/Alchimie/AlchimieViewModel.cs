using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows.Documents;
using System.IO;
using MeisterGeister.View.Windows;
using System.Windows;
//eigene usings
using MeisterGeister.Model;
using MeisterGeister.ViewModel.Alchimie.Logic;
using System.ComponentModel;
using MeisterGeister.Logic.General;
using MeisterGeister.Model.Extensions;

namespace MeisterGeister.ViewModel.Alchimie
{
    class AlchimieViewModel : Base.ViewModelBase
    {

        #region //---- FELDER ----
        
        #region //-Allgemein-
        
        //intern
        bool IsLoaded = false;
        //Enables
        private bool _isEnabledLaborArtListe = true;
        private bool _isEnabledLaborQualitätListe = true;
        //Listen
        private List<Held> _heldListe;
        private List<string> _gruppeListe;
        private List<Alchimierezept> _rezeptListe;
        private List<string> _laborArtListe = new List<string>(new string[] { "Archaisches Labor", "Hexenküche", "Alchimistenlabor" });
        private List<string> _laborQualitätListe = new List<string>(new string[] { "normal", "hochwertig", "aussergew. hochwertig", "beschädigt" });

        //Selections
        private Held _selectedHeld;
        private string _selectedGruppe;
        private Alchimierezept _selectedRezept;
        private string _selectedLaborArtListe = "Archaisches Labor";
        private string _selectedLaborQualitätListe = "normal";

        //Rückgaben
        private Qualität _ergebnisQualität = Qualität.M;

        public Qualität ErgebnisQualität
        {
            get { return _ergebnisQualität; }
            set { Set(ref _ergebnisQualität, value); }
        }
        private string _ergebnisHaltbarkeit;

        public string ErgebnisHaltbarkeit
        {
            get { return _ergebnisHaltbarkeit; }
            set { Set(ref _ergebnisHaltbarkeit, value); }
        }
        //Checkboxen
        private bool _checkedChymischeHochzeit;
        //private bool _checkedFeuerUndEis;
        //private bool _checkedMandriconsBindung;
        //private bool _checkedTransmutationDerElemente;

        #endregion
        #region//-Herstellung-
        //Enables
 //       private bool _isEnabledAlchimistenLaborHerstellung;
        private bool _isEnabledAufladen = true;
        //Listen
        private List<Talent> _talentListeHerstellung ;
        private List<Zutat> _substitutionListeHerstellung;
        //Selections
        private Talent _selectedTalentHerstellung;
        //Rückgaben
        private int _wertTaWTalent;
        private int _wertTaPZurückhaltenHerstellung;
        private int _wertAstralesAufladenHerstellung;
        private int _modifikatorSubstitutionenHerstellung = 0;
        private int _modifikatorTaPZurückhaltenHerstellung = 0;
        private int _bonusTaPQualitätHerstellung;
        private int _bonusAAQualitätHerstellung;
        private string _herstellungWirkungM;
        private int _aSPEinsatzHerstellung=0;
        //private int _bonusQualitätHerstellung = 0; //"2W6 + TaP* ";
        private int _taPHerstellung;

        //Checkboxen

        //commands
        private Base.CommandBase _onClearSelectedHeld;
        private Base.CommandBase _onProbeHerstellung;
        #endregion
        #region//-Analyse-
        //Checkboxen
        private bool _checkedErhaltAnalyseVisibility = false;
        private bool _checkedErhaltAnalyseIsChecked = false;
        //Listen
        private List<string> _intensitätsbestimmungListeAnalyse = new List<string>(new string[]
        {"Odem Arcanum (Grundversion)",
            "Odem Arcanum (Sichtbereich)",
            "Odem Arcanum (Umgebung)",
            "Oculus Astralis",
            "Sicht auf Madas Welt",
            "Magiegespür (gezielte Anwendung)",
            "Magiegespür (ungezielte Anwendung)",
            "Gespür der Keule",
            "Blutblatt"});
        private List<string> _strukturanalyseListeAnalyse= new List<string>(new string[]
        {"Analyse nach Augenschein",
            "Laboranalyse",
            "Allegorische Analyse",
            "Analys Arcanstruktur",
            "Oculus Astralis",
            "Infundibulum der Allweisen",
            "Blick der Weberin",
            "Blick durch Tairachs Augen"});
        //Selections
        private string _selectedIntensitätsbestimmungAnalyse;
        private string _selectedStrukturanalyseAnalyse;
        //Rückgaben
        private int _wertIBAnalyse;
        private int _wertFIBAnalyse;
        private int _wertSAAnalyse;
        private string _analyseProbeIB;
        private string _analyseProbeSA;
        private string _analyseDauerSA;
        private string _analyseBemerkung;
        private int _wertDetailgradAnalyse =0;
        private string _wertErgebnisDetailgradAnalyse;
        private int _wertStrukturergebnisAnalyse;
        //Commands
        private Base.CommandBase _onProbeIBAnalyse;
        private Base.CommandBase _onProbeSAAnalyse;
        #endregion
        #region//-Verdünnung-
        //Listen 
        private List<string> _qualitätListeVerdünnung = new List<string>(new string[] { "M", "A", "B", "C", "D", "E", "F" });
        private List<string> _verdünnungQualitätListeVerdünnung = new List<string>(new string[] { "M", "A", "B", "C", "D", "E", "F" });
        //Selections
        private string _selectedQualitätVerdünnung = "B";
        private string _selectedVerdünnungQualitätVerdünnung = "A";
        //Rückgaben
        private string _verdünnungProbe;
        private int _wertProbeVerdünnung;
        private int _wertTaPVerdünnung;
        private int _wertZfPVerdünnung;
        private string _verdünnungQualität;
        private int _verdünnungAnzahl;
        //Commands
        private Base.CommandBase _onProbeVerdünnung;

        #endregion
        #region//-Haltbarkeit-
        //Rückgaben
        private int _wertProbeHaltbarkeit;
        private string _haltbarkeitProbe;
        private string _haltbarkeitWirkung;
        //Commands
        private Base.CommandBase _onProbeHaltbarkeit;
        #endregion
        #region//-Laborbeute-
        //Listen
        #endregion

        #endregion

        #region //---- EIGENSCHAFTEN ----

        #region //-Allgemein-  
        //Enables
        public bool IsEnabledLaborArtListe
        {
            get { return _isEnabledLaborArtListe; }
            set
            {
                _isEnabledLaborArtListe = value;
                OnChanged("IsEnabledLaborArtListe");
            }
        }
        public bool IsEnabledLaborQualitätListe
        {
            get { return _isEnabledLaborQualitätListe; }
            set
            {
                _isEnabledLaborQualitätListe = value;
                OnChanged("IsEnabledLaborQualitätListe");
            }
        }

        //Listen
        public List<Held> HeldListe 
        { 
            get { return Global.ContextHeld.LoadHeldenGruppeWithAlchimie(); }
            set { _heldListe = value; OnChanged("HeldListe"); } 
        }
        public List<string> GruppeListe
        {
            get { return Global.ContextHeld.LoadAlchimieGruppe(); }
            set
            {
                _gruppeListe = value;
                OnChanged("GruppeListe");
            }
        }
        public List<Alchimierezept> RezeptListe
        {
            get { return _rezeptListe; }
            set
            {
                _rezeptListe = value;
                OnChanged("RezeptListe");
            }
        }
        public List<string> LaborArtListe
        {
            get { return _laborArtListe; }
            set
            {
                _laborArtListe = value;
                OnChanged("LaborArtListe");
            }
        }
        public List<string> LaborQualitätListe
        {
            get { return _laborQualitätListe; }
            set
            {
                _laborQualitätListe = value;
                OnChanged("LaborQualitätListe");
            }
        }

        //Selections
        public Held SelectedHeld { get { return _selectedHeld; } set { 
            _selectedHeld = value;
            if (value != null)
            {
                resetHerstellung();
                resetAnalyse();
                resetVerdünnung();
                resetHaltbarkeit();
                TalentListeHerstellung = Global.ContextHeld.LoadAlchimieHerstellungTalenteByHeld(value);
                //TODO Talent wählen/Wert aktualisieren
                checkAufladen(value);
            }
            else IsEnabledAufladen = true;
            OnChanged("SelectedHeld");

        }
        }
        public string SelectedGruppe
        {
            get { return _selectedGruppe; }
            set
            {
                _selectedGruppe = value;
                OnChanged("SelectedGruppe");
                RezeptListe = Global.ContextHeld.LoadAlchimieRezepteByGruppe(value);
            }
        }
        public Alchimierezept SelectedRezept
        {
            get { return _selectedRezept; }
            set
            {
                if (value != null)
                {
                    if (_selectedRezept == value)
                        return;
                    _selectedRezept = value;
                    OnChanged("SelectedRezept");
                    SubstitutionListeHerstellung = getZutatenByRezept(_selectedRezept);
                    HerstellungWirkungM = getMWirkung();
                }
            }
        }
        public string SelectedLaborArtListe
        {
            get { return _selectedLaborArtListe; }
            set
            {
                _selectedLaborArtListe = value;
                OnChanged("SelectedLaborArtListe");
            }
        }
        public string SelectedLaborQualitätListe
        {
            get { return _selectedLaborQualitätListe; }
            set
            {
                _selectedLaborQualitätListe = value;
                OnChanged("SelectedLaborQualitätListe");
            }
        }
        //Rückgaben
        [DependentProperty("SelectedRezept"), DependentProperty("SelectedLaborArtListe"), DependentProperty("SelectedLaborQualitätListe")]
        public int ModifikatorLabor
        {
            get
            {

                int mod = 0;
                if (SelectedRezept != null)
                {
                    switch (SelectedLaborArtListe)
                    {
                        case "Archaisches Labor": switch (SelectedRezept.Labor)
                            {
                                case "Hexenküche": mod = 7; break;
                                //keine Herstellung möglich
                                case "Alchimistenlabor": return 99;
                            }; break;
                        case "Hexenküche": switch (SelectedRezept.Labor)
                            {
                                case "Alchimistenlabor": mod = 7; break;
                            }; break;
                        case "Alchimistenlabor": switch (SelectedRezept.Labor)
                            {
                                case "archaisches Labor": mod = -3; break;
                            }; break;
                        default: break;
                    }
                }
                switch (SelectedLaborQualitätListe)
                {
                    case "hochwertig": mod -= 3; break;
                    case "aussergew. hochwertig": mod -= 7; break;
                    case "beschädigt": mod += 3; break;
                    default: break;
                }
                return mod;
            }
        }
        //Checkboxen
        /// <summary>
        /// Chymische Hochzeit erleichter die Probe und gibt einen Qualitätsbonus. WdA
        /// </summary>
        public bool CheckedChymischeHochzeit
        {
            get { return _checkedChymischeHochzeit; }
            set
            {
                _checkedChymischeHochzeit = value;
                OnChanged("CheckedChymischeHochzeit");
                //TODO MP neccessary?!
                WertAstralesAufladenHerstellung = WertAstralesAufladenHerstellung;
            }
        }
        
        //Spielen bei der Herstellung gar keine direkte Rolle.
        //public bool CheckedFeuerUndEis
        //{
        //    get { return _checkedFeuerUndEis; }
        //    set
        //    {
        //        _checkedFeuerUndEis = value;
        //        OnChanged("CheckedFeuerUndEis");
        //    }
        //}
        //public bool CheckedMandriconsBindung
        //{
        //    get { return _checkedMandriconsBindung; }
        //    set
        //    {
        //        _checkedMandriconsBindung = value;
        //        OnChanged("CheckedMandriconsBindung");
        //    }
        //}
        //public bool CheckedTransmutationDerElemente
        //{
        //    get { return _checkedTransmutationDerElemente; }
        //    set
        //    {
        //        _checkedTransmutationDerElemente = value;
        //        OnChanged("CheckedTransmutationDerElemente");
        //    }
        //}

        #endregion
        //TODO MP wrap Alchimierezept; add WirkungM, calc Haltbarkeit and others fix?!
        #region//-Herstellung-
        //Dictionary<Qualität, double> qualitätsWahrscheinlichkeit;

        ///// <summary>
        ///// Zurücksetzen von allen gecachten Werten, die in direktem Zusammenhang mit der Herstellungsschwierigkeit stehen.
        ///// </summary>
        //private void Invalidate()
        //{
        //    qualitätsWahrscheinlichkeit = null;
        //}
        
        [DependentProperty("BonusQualitätHerstellung"), DependentProperty("WertTaWTalent"), DependentProperty("ProbenModGesHerstellung")]
        public Dictionary<Qualität, double> QualitätsWahrscheinlichkeit
        {
            get {
                //if (qualitätsWahrscheinlichkeit == null)
                //{
                    Dictionary<Qualität, double> qualitätsWahrscheinlichkeit;
                    string[] qualitäten = new string[] { "M", "A", "B", "C", "D", "E", "F" };
                    qualitätsWahrscheinlichkeit = new Dictionary<Qualität, double>();
                    foreach (Qualität q in Enum.GetValues(typeof(Qualität)))
                        qualitätsWahrscheinlichkeit.Add(q, 0);

                    var d = new Dictionary<int, double>();
                    var qualiwurfV = Würfel.WurfVerteilung(2, 6, BonusQualitätHerstellung);
                    Probe p = GetProbeHerstellung();
                    var tapV = p.TaPVerteilung;
                    foreach(var wurf in qualiwurfV.Keys)
                    {
                        foreach(var tap in tapV.Keys)
                        {
                            Qualität q = Qualität.M;
                            if (tap > 0)
                                q = getQualität(wurf + tap);
                            qualitätsWahrscheinlichkeit[q] += tapV[tap] * qualiwurfV[wurf];
                        }
                    }
                    foreach (var k in qualitätsWahrscheinlichkeit.Keys.ToList())
                        qualitätsWahrscheinlichkeit[k] *= 100;

                //}
                return qualitätsWahrscheinlichkeit;
            }
        }

        [DependentProperty("SelectedLaborArtListe"), DependentProperty("SelectedRezept")]
        public bool HerstellungUnmöglich
        {
            get {
                if (SelectedLaborArtListe == "Archaisches Labor" && (SelectedRezept == null || SelectedRezept.Labor == "Alchimistenlabor"))
                    return true;
                return false; 
            }
        }

        //Enables
 //       public bool IsEnabledAlchimistenLaborHerstellung { get { return _isEnabledAlchimistenLaborHerstellung; } set { _isEnabledAlchimistenLaborHerstellung = value; OnChanged("IsEnabledAlchimistenLaborHerstellung"); } }
        public bool IsEnabledAufladen 
        { 
            get { return _isEnabledAufladen; }
            set {
                _isEnabledAufladen = value;
                OnChanged("IsEnabledAufladen");
            } 
        }

        //Listen
        public List<Talent> TalentListeHerstellung
        { 
            get { return _talentListeHerstellung; } 
            set 
            { 
                _talentListeHerstellung = value; 
                OnChanged("TalentListeHerstellung"); 
            } 
        }

         public List<Zutat> SubstitutionListeHerstellung { 
            get { return _substitutionListeHerstellung; }
            set 
            {
                if (_substitutionListeHerstellung != null)
                    _substitutionListeHerstellung.ForEach(z => z.PropertyChanged -= Zutat_PropertyChanged);
                _substitutionListeHerstellung = value;
                if (_substitutionListeHerstellung != null)
                    _substitutionListeHerstellung.ForEach(z => z.PropertyChanged += Zutat_PropertyChanged);
                OnChanged("SubstitutionListeHerstellung");
                calculateModSubstitutionen();
            }
        }
        //Selections
        public Talent SelectedTalentHerstellung
        {
            get { return _selectedTalentHerstellung; } 
            set {
                if (value == null)
                {
                    value = Global.ContextHeld.LoadTalentByName("Alchimie");
                }
                _selectedTalentHerstellung = value;
                OnChanged("SelectedTalentHerstellung");
                WertTaWTalent = value == null ? 0 : SelectedHeld == null ? 0 : SelectedHeld.Talentwert(value);
            }
        }
        //Rückgaben
        //Kein abgeleiteter Wert, weil die Möglichkeit zur manuellen Anpassung gegeben sein muss.
        public int WertTaWTalent 
        { 
            get { return _wertTaWTalent; } 
            set { 
                _wertTaWTalent = value;
                OnChanged("WertTaWTalent");
                WertTaPZurückhaltenHerstellung = WertTaPZurückhaltenHerstellung;
            } 
        }
        public int WertTaPZurückhaltenHerstellung
        { 
            get { return _wertTaPZurückhaltenHerstellung; } 
            set {
                if (SelectedRezept!=null)
                { 
                    value = Math.Max(0, Math.Min(value , WertTaWTalent - SelectedRezept.Brauschwierigkeit));
                    if (value != _wertTaPZurückhaltenHerstellung)
                    {
                        _wertTaPZurückhaltenHerstellung = value;
                        OnChanged("WertTaPZurückhaltenHerstellung");
                        ModifikatorTaPZurückhaltenHerstellung = _wertTaPZurückhaltenHerstellung;
                        BonusTaPQualitätHerstellung = _wertTaPZurückhaltenHerstellung;
                    }
                } 
            } 
        }
        public int WertAstralesAufladenHerstellung
        { 
            get { return _wertAstralesAufladenHerstellung; }
            set 
            {
                if (value >= 0)
                {
                    _wertAstralesAufladenHerstellung = value;
                    if (value == 0 || value == 1) 
                    {
                        BonusAAQualitätHerstellung = value; 
                        if (CheckedChymischeHochzeit) 
                            ASPEinsatzHerstellung = (int)Math.Ceiling(((double)value)/2); 
                        else 
                            ASPEinsatzHerstellung = value; 
                    }
                    else if (((Math.Log10(value) / Math.Log10(2.0)) % 1) == 0) 
                    { 
                        BonusAAQualitätHerstellung = (int)(Math.Log10(value) / Math.Log10(2.0)) + 1;
                        if (CheckedChymischeHochzeit) 
                            ASPEinsatzHerstellung = (int)Math.Ceiling((double)value / 2.0); 
                        else 
                            ASPEinsatzHerstellung = value; 
                    }
                    OnChanged("WertAstralesAufladenHerstellung");
                }
            }
        }

        public int ModifikatorSubstitutionenHerstellung
        { 
            get { return _modifikatorSubstitutionenHerstellung; } 
            set { 
                _modifikatorSubstitutionenHerstellung = value; 
                OnChanged("ModifikatorSubstitutionenHerstellung");
            } 
        }

        [DependentProperty("SelectedRezept"), DependentProperty("ModifikatorLabor"), DependentProperty("ModifikatorSubstitutionenHerstellung"), DependentProperty("ModifikatorTaPZurückhaltenHerstellung"), DependentProperty("ModifikatorCHHerstellung")]
        public int ProbenModGesHerstellung 
        { 
            get {
                if (SelectedRezept == null || HerstellungUnmöglich)
                    return 0;
                return SelectedRezept.Brauschwierigkeit
                    + ModifikatorCHHerstellung
                    + ModifikatorTaPZurückhaltenHerstellung
                    + ModifikatorSubstitutionenHerstellung
                    + ModifikatorLabor;
            } 
        }
        //TODO nur bei Bedarf berechnen
        public string HerstellungWirkungM
        {
            get { return _herstellungWirkungM; } 
            set {
                _herstellungWirkungM = value; 
                OnChanged("HerstellungWirkungM");
            } 
        }
        /// <summary>
        /// Wie viele TaP sollen zurückgehalten werden? WdA 19
        /// </summary>
        public int ModifikatorTaPZurückhaltenHerstellung
        { 
            get { return _modifikatorTaPZurückhaltenHerstellung; } 
            set { 
                if (value >= 2)
                {
                    _modifikatorTaPZurückhaltenHerstellung = value;
                } 
                else 
                    _modifikatorTaPZurückhaltenHerstellung = 0; 
                OnChanged("ModifikatorTaPZurückhaltenHerstellung"); 
            } 
        }
        /// <summary>
        /// Bonus durch Chymische Hochzeit. WdZ 116, WdA 18
        /// </summary>
        [DependentProperty("CheckedChymischeHochzeit")]
        public int ModifikatorCHHerstellung 
        { 
            get
            {
                if(CheckedChymischeHochzeit)
                    return -1;
                return 0;
            } 
        }
        public int BonusTaPQualitätHerstellung
        {
            get { return _bonusTaPQualitätHerstellung; }
            set
            {
                if (value >= 2)
                {
                    _bonusTaPQualitätHerstellung = value * 2;
                }
                else
                    _bonusTaPQualitätHerstellung = 0;
                OnChanged("BonusTaPQualitätHerstellung");
            }
        }
        
        [DependentProperty("CheckedChymischeHochzeit")]
        public int BonusCHQualitätHerstellung
        { 
            get
            {
                if (CheckedChymischeHochzeit)
                    return 2;
                return 0;
            } 
        }
        public int BonusAAQualitätHerstellung
        {
            get { return _bonusAAQualitätHerstellung; } 
            set { 
                _bonusAAQualitätHerstellung = value; 
                OnChanged("BonusAAQualitätHerstellung"); 
            }
        }
        public int ASPEinsatzHerstellung
        { 
            get { return _aSPEinsatzHerstellung; } 
            set {
                _aSPEinsatzHerstellung = value; 
                OnChanged("ASPEinsatzHerstellung"); 
            } 
        }
        
        [DependentProperty("BonusCHQualitätHerstellung"), DependentProperty("BonusAAQualitätHerstellung"), DependentProperty("BonusTaPQualitätHerstellung")]
        public int BonusQualitätHerstellung
        {
            get
            {
                if (HerstellungUnmöglich)
                    return 0;
                return BonusCHQualitätHerstellung + BonusAAQualitätHerstellung + BonusTaPQualitätHerstellung;
            }
        }
        [DependentProperty("SelectedRezept")]
        public string WertHaltbarkeitHerstellung
        {
            get
            {
                if (SelectedRezept == null)
                    return "";
                return SelectedRezept.Haltbarkeit;
            }
        }
        public int TaPHerstellung
        {
            get { return _taPHerstellung; }
            set
            {
                _taPHerstellung = value;
                OnChanged("TaPHerstellung");
            }
        }

        
        //Checkboxen  
        //TODO MP funktioniert nicht
        public bool CheckedErhaltAnalyseVisibility 
        { 
            get { return _checkedErhaltAnalyseVisibility; } 
            set { _checkedErhaltAnalyseVisibility = value; 
                OnChanged("CheckedErhaltAnalyseVisibility"); 
            }
        }
        public bool CheckedErhaltAnalyseIsChecked 
        {
            get { return _checkedErhaltAnalyseIsChecked; }
            set
            {
                _checkedErhaltAnalyseIsChecked = value;
            OnChanged("CheckedErhaltAnalyseIsChecked"); 
            }
        }
        //Commands

        public Base.CommandBase OnProbeHerstellung
        {
            get { return _onProbeHerstellung; }
        }

        #endregion 
        #region//-Analyse-
        //Listen
        public List<string> IntensitätsbestimmungListeAnalyse 
        { 
            get { return _intensitätsbestimmungListeAnalyse; } 
            set { _intensitätsbestimmungListeAnalyse = value; 
                OnChanged("IntensitätsbestimmungListeAnalyse"); 
            }
        }
        public List<string> StrukturanalyseListeAnalyse
        { 
            get { return _strukturanalyseListeAnalyse; }
            set { 
                _strukturanalyseListeAnalyse = value;
                OnChanged("StrukturanalyseListeAnalyse"); 
            } 
        }
        
        //Selections
        public string SelectedIntensitätsbestimmungAnalyse
        { 
            get { return _selectedIntensitätsbestimmungAnalyse; } 
            set { 
                _selectedIntensitätsbestimmungAnalyse = value;
                OnChanged("SelectedIntensitätsbestimmungAnalyse");
                AnalyseProbeIB = getIBTalent(SelectedIntensitätsbestimmungAnalyse);
                if(SelectedHeld!=null){
                    WertIBAnalyse = getIBFertigkeitswert(SelectedIntensitätsbestimmungAnalyse);
                }else{
                    WertIBAnalyse =0;
                }                
            }
        }
        public string SelectedStrukturanalyseAnalyse 
        {
            get { return _selectedStrukturanalyseAnalyse; }
            set {
                _selectedStrukturanalyseAnalyse = value;
                OnChanged("SelectedStrukturanalyseAnalyse");
                AnalyseProbeSA = getSATalent(SelectedStrukturanalyseAnalyse);
                AnalyseDauerSA = getSADauer(SelectedStrukturanalyseAnalyse);
                AnalyseBemerkung = getBemerkung(SelectedStrukturanalyseAnalyse);
                if (SelectedHeld != null)
                {
                    WertSAAnalyse = getSAFertigkeitswert(SelectedStrukturanalyseAnalyse);
                }
                else
                {
                    WertSAAnalyse = 0;
                }
                if (SelectedStrukturanalyseAnalyse == "Laboranalyse") CheckedErhaltAnalyseVisibility = true;
                else CheckedErhaltAnalyseVisibility = false;
            }
        }
        
        //Rückgaben
        public int WertIBAnalyse
        {
            get{return _wertIBAnalyse;}
            set{
                _wertIBAnalyse = value;
                OnChanged("WertIBAnalyse");
            }
        }        
        public int WertFIBAnalyse
        {
            get{return _wertFIBAnalyse;}
            set{
                _wertFIBAnalyse = value;
                OnChanged("WertFIBAnalyse");
            }
        }
        public string AnalyseProbeIB
        {
            get{return _analyseProbeIB;}
            set{
                _analyseProbeIB = value;
                OnChanged("AnalyseProbeIB");
            }
        }
        public int WertSAAnalyse
        {
            get{return _wertSAAnalyse;}
            set{
                _wertSAAnalyse = value;
                OnChanged("WertSAAnalyse");
            }
        }
        public string AnalyseProbeSA
        {
            get { return _analyseProbeSA; }
            set
            {
                _analyseProbeSA = value;
                OnChanged("AnalyseProbeSA");
            }
        }
        public string AnalyseDauerSA
        {
            get { return _analyseDauerSA; }
            set
            {
                _analyseDauerSA = value;
                OnChanged("AnalyseDauerSA");
            }
        }
        public string AnalyseBemerkung
        {
            get { return _analyseBemerkung; }
            set
            {
                _analyseBemerkung = value;
                OnChanged("AnalyseBemerkung");
            }
        }
        public int WertDetailgradAnalyse
        {
            get { return _wertDetailgradAnalyse; }
            set
            {
                _wertDetailgradAnalyse = value;
                OnChanged("WertDetailgradAnalyse");
                if (value == 0)
                {
                    WertErgebnisDetailgradAnalyse = "Keine Aussage möglich";
                }
                else if (value <= 2)
                {
                    WertErgebnisDetailgradAnalyse = "Ist ein alchimistisches Erzeugnis";
                }
                else
                {
                    WertErgebnisDetailgradAnalyse = "Schätzung der Qualität: schwach (Qualitäten M, A, B, C, D) oder stark (Qualitäten M, C, D, E, F)";
                }
            }
        }
        public int WertStrukturergebnisAnalyse
        {
            get { return _wertStrukturergebnisAnalyse; }
            set
            {
                _wertStrukturergebnisAnalyse = value;
                OnChanged("WertStrukturergebnisAnalyse");
            }
        }
        public string WertErgebnisDetailgradAnalyse
        {
            get { return _wertErgebnisDetailgradAnalyse; }
            set{
                _wertErgebnisDetailgradAnalyse = value;
                OnChanged("WertErgebnisDetailgradAnalyse");
            }
        }
        //Commands

        public Base.CommandBase OnProbeIBAnalyse
        {
            get { return _onProbeIBAnalyse; }
        }
        public Base.CommandBase OnProbeSAAnalyse
        {
            get { return _onProbeSAAnalyse; }
        }
        public Base.CommandBase OnClearSelectedHeld
        {
            get { return _onClearSelectedHeld;   }
        }
        #endregion
        #region//-Verdünnung-
        //Listen
        public List<string> QualitätListeVerdünnung
        {
            get { return _qualitätListeVerdünnung; }
            set
            {
                _qualitätListeVerdünnung = value;
                OnChanged("QualitätListeVerdünnung");
            }
        }
        public List<string> VerdünnungQualitätListeVerdünnung
        {
            get { return _verdünnungQualitätListeVerdünnung; }
            set
            {
                _verdünnungQualitätListeVerdünnung = value;
                OnChanged("VerdünnungQualitätListeVerdünnung");
            }
        }
        //Selections
        public string SelectedQualitätVerdünnung
        {
            get { return _selectedQualitätVerdünnung; }
            set
            {
                _selectedQualitätVerdünnung = value;
                OnChanged("SelectedQualitätVerdünnung");
            }
        }
        public string SelectedVerdünnungQualitätVerdünnung
        {
            get { return _selectedVerdünnungQualitätVerdünnung; }
            set
            {
                _selectedVerdünnungQualitätVerdünnung = value;
                OnChanged("SelectedVerdünnungQualitätVerdünnung");
            }
        }
        //Rückgaben
        public string VerdünnungProbe
        {
            get { return _verdünnungProbe; }
            set
            {
                _verdünnungProbe = value;
                OnChanged("VerdünnungProbe");
            }
        }
        public int WertProbeVerdünnung
        {
            get { return _wertProbeVerdünnung; }
            set
            {
                _wertProbeVerdünnung = value;
                OnChanged("WertProbeVerdünnung");
            }
        }
        public int WertTaPVerdünnung
        {
            get { return _wertTaPVerdünnung; }
            set
            {
                _wertTaPVerdünnung = value;
                OnChanged("WertTaPVerdünnung");
            }
        }
        public int WertZfPVerdünnung
        {
            get { return _wertZfPVerdünnung; }
            set
            {
                _wertZfPVerdünnung = value;
                OnChanged("WertZfPVerdünnung");
            }
        }
        public string VerdünnungQualität
        {
            get { return _verdünnungQualität; }
            set
            {
                _verdünnungQualität = value;
                OnChanged("VerdünnungQualität");
            }
        }
        public int VerdünnungAnzahl
        {
            get { return _verdünnungAnzahl; }
            set
            {
                _verdünnungAnzahl = value;
                OnChanged("VerdünnungAnzahl");
            }
        }

        [DependentProperty("SelectedRezept")]
        public string VerdünnungKosten
        {
            get {
                if(SelectedRezept != null)
                    return SelectedRezept.Brauschwierigkeit.ToString() + " D";
                return "";
            }
        }
        [DependentProperty("SelectedRezept")]
        public string VerdünnungZeit
        {
            get {
                if (SelectedRezept != null)
                    return SelectedRezept.Brauschwierigkeit.ToString() + " h";
                return "";
            }
        }
        //Commands

        public Base.CommandBase OnProbeVerdünnung
        {
            get { return _onProbeVerdünnung; }
        }
        #endregion
        #region//-Haltbarkeit-
        public int WertProbeHaltbarkeit
        {
            get { return _wertProbeHaltbarkeit; }
            set
            {
                _wertProbeHaltbarkeit = value;
                OnChanged("WertProbeHaltbarkeit");
            }
        }
        public string HaltbarkeitProbe
        {
            get { return _haltbarkeitProbe; }
            set
            {
                _haltbarkeitProbe = value;
                OnChanged("HaltbarkeitProbe");
            }
        }
        public string HaltbarkeitWirkung
        {
            get { return _haltbarkeitWirkung; }
            set
            {
                _haltbarkeitWirkung = value;
                OnChanged("HaltbarkeitWirkung");
            }
        }
        
        //Commands

        public Base.CommandBase OnProbeHaltbarkeit
        {
            get { return _onProbeHaltbarkeit; }
        }
        #endregion
        #region//-Laborbeute-
        //Listen
        #endregion

        #endregion

        #region //---- KONSTRUKTOR ----

        public AlchimieViewModel()
            : base(View.General.ViewHelper.ShowProbeDialog)
        {
            _onProbeHerstellung = new Base.CommandBase(ProbeHerstellung, null);
            _onProbeIBAnalyse = new Base.CommandBase(ProbeIBAnalyse, null);
            _onProbeSAAnalyse = new Base.CommandBase(ProbeSAAnalyse, null);
            _onClearSelectedHeld = new Base.CommandBase(ClearSelectedHeld, null);
            _onProbeVerdünnung = new Base.CommandBase(ProbeVerdünnung, null);
            _onProbeHaltbarkeit = new Base.CommandBase(ProbeHaltbarkeit, null);
            SelectedTalentHerstellung = null;
            PropertyChanged += DependentProperty.PropagateINotifyProperyChanged;
        }

        #endregion

        #region //---- INSTANZMETHODEN ----

        public void LoadDaten()
        {
            if (IsLoaded == false)
            {
                //Nur Helden mit entsprechender Sonderfertigkeit
                HeldListe = Global.ContextHeld.HeldenGruppeListe;
                IsLoaded = true;
            }
        }


        #endregion

        #region //---- METHODEN ----

        #region//-Herstellung-
        private void resetHerstellung()
        {
            //TODO: MP implement
        }
        private void checkAufladen(Held held)
        {
            //check Magiekunde>7 && Alchimie>7; set IsEnabledAufladen
            if (held.Talentwert("Alchimie") >= 7 && held.Talentwert("Magiekunde") >= 7) IsEnabledAufladen = true;
            else IsEnabledAufladen = false;
        }
        private void calculateModSubstitutionen()
        {
            int mod = 0;
            foreach (Zutat z in SubstitutionListeHerstellung)
            {
                mod = mod + z.Mod;
            }
            ModifikatorSubstitutionenHerstellung = mod;
        }

        private List<Zutat> getZutatenByRezept(Alchimierezept rezept)
        {
            if (rezept!=null)
            {
                List<Zutat> zutaten = new List<Zutat>();
                string[] tmp = rezept.Zutaten.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string zutat in tmp)
                {
                    string[] tt = zutat.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    double anzahl = Convert.ToDouble(tt[0], System.Globalization.CultureInfo.InvariantCulture);
                    string einheit = tt[1];
                    string name = "";
                    for (int i = 2; i < tt.Length; i++)
                    {
                        name = name + tt[i] + " ";
                    }
                    Zutat zz = new ViewModel.Alchimie.Logic.Zutat(anzahl, einheit, name.Trim());
                    zutaten.Add(zz);
                }
                return zutaten;
            }
            return new List<Zutat>();
        }

        private string getMWirkung()
        {
            return wirkungen[MeisterGeister.Logic.General.RandomNumberGenerator.RandomInt(0, wirkungen.Count-1)];
        }

        private Qualität getQualität(int qualitätszahl){
            if(qualitätszahl<=6){
                return Qualität.A;
            }else if(qualitätszahl<=12){
                return Qualität.B;
            }else if(qualitätszahl<=18){
                return Qualität.C;
            }else if(qualitätszahl<=24){
                return Qualität.D;
            }else if(qualitätszahl<=30){
                return Qualität.E;
            }else return Qualität.F;
        }
        #endregion
        #region//-Analyse-
        private int getSAFertigkeitswert(string auswahl)
        {
            switch (auswahl)
            {
                case "Analyse nach Augenschein":
                case "Laboranalyse":
                case "Infundibulum der Allweisen":
                    return SelectedHeld.Talentwert("Alchimie");
                case "Allegorische Analyse":
                    return SelectedHeld.Talentwert("Ritualkenntnis");
                case "Analys Arcanstruktur":
                    return SelectedHeld.Zauberfertigkeitswert("Analys Arcanstruktur");
                case "Oculus Astralis":
                    return SelectedHeld.Talentwert("Sinnenschärfe");                
                case "Blick der Weberin": 
                case "Blick durch Tairachs Augen":
                    int taw;
                    SelectedHeld.GetHeldTalent("Liturgiekenntnis",true,out taw,false);
                    return taw;
                default: return 0;
            }
        }
        private int getIBFertigkeitswert(string auswahl)
        {
            switch (auswahl)
            {
                case "Odem Arcanum (Grundversion)": 
                case "Odem Arcanum (Sichtbereich)":
                case "Odem Arcanum (Umgebung)":
                    return SelectedHeld.Zauberfertigkeitswert("Odem Arcanum");
                case "Oculus Astralis":
                    return SelectedHeld.Zauberfertigkeitswert("Oculus Astralis");
                case "Sicht auf Madas Welt":
                    int taw;
                    SelectedHeld.GetHeldTalent("Liturgiekenntnis",true,out taw,false);
                    return taw;
                case "Magiegespür (gezielte Anwendung)": 
                case "Magiegespür (ungezielte Anwendung)":
                    return SelectedHeld.Talentwert("Magiegespür");
                case "Gespür der Keule": return 0;
                case "Blutblatt":
                    return SelectedHeld.Talentwert("Pflanzenkunde");
                default: return 0;
            }
        }
        private string getIBTalent(string auswahl)
        {
            switch (auswahl)
            {
                case "Odem Arcanum (Grundversion)": return "Odem Arcanum";
                case "Odem Arcanum (Sichtbereich)": return "Odem Arcanum";
                case "Odem Arcanum (Umgebung)": return "Odem Arcanum";
                case "Oculus Astralis": return "Oculus Astralis";
                case "Sicht auf Madas Welt": return "Liturgiekenntnis";
                case "Magiegespür (gezielte Anwendung)": return "Magiegespür";
                case "Magiegespür (ungezielte Anwendung)": return "Magiegespür";
                case "Gespür der Keule": return "Gespür-Probe";
                case "Blutblatt": return "Pflanzenkunde";
                default: return "";
            }
        }
        private string getSATalent(string auswahl)
        {
            switch (auswahl)
            {
                case "Analyse nach Augenschein": return "Alchimie";
                case "Laboranalyse": return "Alchimie";
                case "Allegorische Analyse": return "Ritualkenntnis";
                case "Analys Arcanstruktur": return "Analys Arcanstruktur";
                case "Oculus Astralis": return "Sinnenschärfe";
                case "Infundibulum der Allweisen": return "Alchimie";
                case "Blick der Weberin": return "Liturgiekenntnis";
                case "Blick durch Tairachs Augen": return "Liturgiekenntnis";
                default: return "";
            }
        }
        private string getSADauer(string auswahl)
        {
            switch (auswahl)
            {
                case "Analyse nach Augenschein": return "1 SR";
                case "Laboranalyse": return SelectedRezept.Analyseschwierigkeit.ToString() +  " h";
                case "Allegorische Analyse": return "1 SR";
                case "Analys Arcanstruktur": return "1 SR";
                case "Oculus Astralis": return "1 SR";
                case "Infundibulum der Allweisen": return "1 SR";
                case "Blick der Weberin": return "0,5 h (Andacht)";
                case "Blick durch Tairachs Augen": return "0,5 h (Andacht)";
                default: return "";
            }
        }
        private string getBemerkung(string auswahl)
        {
            switch (auswahl)
            {
                case "Analyse nach Augenschein": return "kein Ansammeln möglich; max. Detailgrad 4;";
                case "Laboranalyse": return "mind. Labor archaisch benötigt; kein Ansammeln möglich; eine Mengeneinheit kann verbraucht werden;";
                case "Allegorische Analyse": return "Schale der Alchimie benötigt; kein Ansammeln möglich";
                case "Analys Arcanstruktur": return "Ansammeln möglich;";
                case "Oculus Astralis": return "Ansammeln möglich; IB & SA zur selben Zeit;";
                case "Infundibulum der Allweisen": return "Infindibulum benötigt; kein Ansammeln möglich; eine Mengeneinheit wird verbraucht;";
                case "Blick der Weberin": return "speziell; Ansammeln möglich;";
                case "Blick durch Tairachs Augen": return "speziell; Ansammeln möglich;";
                default: return "";
            }
        }
        private int getModLab(string vorraussetzung)
        {
            int mod = 0;
            switch (vorraussetzung)
            {
                case "Archaisches Labor": switch (SelectedLaborArtListe)
                    {
                        case "Archaisches Labor": mod = 0; break;
                        case "Hexenküche": mod = 0; break;
                        case "Alchimistenlabor": mod = -3; break;
                        default: mod = 999; break;
                    }; break;
                case "Hexenküche": switch (SelectedLaborArtListe)
                    {
                        case "Archaisches Labor": mod = 7; break;
                        case "Hexenküche": mod = 0; break;
                        case "Alchimistenlabor": mod = 0; break;
                        default: mod = 999; break;
                    }; break;
                case "Alchimistenlabor": switch (SelectedLaborArtListe)
                    {
                        case "Archaisches Labor": mod = 999; break;
                        case "Hexenküche": mod = 7; break;
                        case "Alchimistenlabor": mod = 0; break;
                        default: mod = 999; break;
                    }; break;
                default: mod = 999; break;
            }
            switch (SelectedLaborQualitätListe)
            {
                case "hochwertig": mod -= 3; break;
                case "aussergew. hochwertig": mod -= 7; break;
                case "beschädigt": mod += 3; break;
                default: break;
            }
            return mod;
        }
        private void resetAnalyse()
        {
            //hol mögliche Talente
            List<string> iBfertigkeiten = Global.ContextHeld.LoadIntensitätsbestimmungFertigkeitenAlchimieByHeld(SelectedHeld);
            List<string> ibListe=new List<string>();
            if (iBfertigkeiten.Contains("Odem Arcanum"))
            {
                ibListe.Add("Odem Arcanum (Grundversion)");
                ibListe.Add("Odem Arcanum (Sichtbereich)");
                ibListe.Add("Odem Arcanum (Umgebung)");
            }
            if (iBfertigkeiten.Contains("Oculus Astralis"))
            {
                ibListe.Add("Oculus Astralis");
            }
            if (iBfertigkeiten.Contains("Magiegespür"))
            {
                ibListe.Add("Magiegespür (gezielte Anwendung)");
                ibListe.Add("Magiegespür (ungezielte Anwendung)");
            }
            if (iBfertigkeiten.Contains("Pflanzenkunde"))
            {
                ibListe.Add("Blutblatt");
            }
            if (iBfertigkeiten.Contains("Liturgie: Sicht auf Madas Welt"))
            {
                ibListe.Add("Sicht auf Madas Welt");
            }
            if (iBfertigkeiten.Contains("Keulenritual: Gespür der Keule"))
            {
                ibListe.Add("Gespür der Keule");
            }
            //add  Gespür
            IntensitätsbestimmungListeAnalyse = ibListe;

            //hol mögliche Talente
            List<string> sAfertigkeiten = Global.ContextHeld.LoadStrukturanalyseFertigkeitenAlchimieByHeld(SelectedHeld);
            List<string> sAListe = new List<string>();
            if (sAfertigkeiten.Contains("Alchimie"))
            {
                sAListe.Add("Analyse nach Augenschein");
                sAListe.Add("Laboranalyse");
                sAListe.Add("Infundibulum der Allweisen");
            }
            if (sAfertigkeiten.Contains("Ritualkenntnis"))
            {
                sAListe.Add("Allegorische Analyse");
            }
            if (sAfertigkeiten.Contains("Analys Arcanstruktur"))
            {
                sAListe.Add("Analys Arcanstruktur");
            }
            if (sAfertigkeiten.Contains("Oculus Astralis"))
            {
                sAListe.Add("Oculus Astralis");
            }
            if (sAfertigkeiten.Contains("Liturgie: Blick der Weberin"))
            {
                sAListe.Add("Blick der Weberin");
            }
            if (sAfertigkeiten.Contains("Liturgie: Blick durch Tairachs Augen"))
            {
                sAListe.Add("Blick durch Tairachs Augen");
            }
            StrukturanalyseListeAnalyse = sAListe;
        }
        
        #endregion
        #region//-Verdünnung-
        private void resetVerdünnung()
        {
            HaltbarkeitProbe = "Alchimie";
            WertProbeHaltbarkeit = SelectedHeld.Talentwert("Alchimie");
        }
        #endregion
        #region//-Haltbarkeit-
        private void resetHaltbarkeit()
        {
            VerdünnungProbe = "Alchimie";
            WertProbeVerdünnung = SelectedHeld.Talentwert("Alchimie");
        }
        #endregion
        #region//-Laborbeute-
        #endregion

        #endregion

        #region //---- EVENTS ----
        private void ClearSelectedHeld(object obj)
        {
            SelectedHeld = null;
        }
        #region//-Herstellung-
        private void Zutat_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            calculateModSubstitutionen();
        }

        /// <summary>
        /// Gibt die Herstellungsprobe zurück.
        /// </summary>
        /// <param name="werte">optionale NPC Eigenschaftswerte - sonst 10, 10, 10</param>
        private Probe GetProbeHerstellung(int[] werte = null)
        {
            //Probe auf Erstellung würfeln
            if (SelectedTalentHerstellung != null)
            {
                SelectedTalentHerstellung.Modifikator = ProbenModGesHerstellung;
                SelectedTalentHerstellung.Fertigkeitswert = WertTaWTalent;
                if (SelectedHeld == null && SelectedTalentHerstellung.Werte[0] == 0) //Wenn kein Held, dann die eigenschaften mit 10 vorbesetzen
                {
                    if (werte == null)
                        werte = new int[] { 10, 10, 10 };
                    for (int i = 0; i < 3; i++)
                        SelectedTalentHerstellung.Werte[i] = werte[i];

                }
                return SelectedTalentHerstellung;
            }
            return null;
        }

        /// <summary>
        /// Würfelt die Herstellungsprobe.
        /// </summary>
        /// <param name="sender"></param>
        private void ProbeHerstellung(object sender)
        {
            //Probe auf Erstellung würfeln
            var probe = GetProbeHerstellung();
            if (probe != null)
            {
                var ergebnis = ShowProbeDialog(SelectedTalentHerstellung, SelectedHeld);
                if (ergebnis != null && ergebnis.Gelungen)
                {
                    TaPHerstellung = ergebnis.Übrig;
                    ErgebnisQualität = getQualität(MeisterGeister.Logic.General.Würfel.Parse("2W6", true) + TaPHerstellung + WertAstralesAufladenHerstellung + WertTaPZurückhaltenHerstellung * 2);
                    if (_selectedRezept != null)
                        ErgebnisHaltbarkeit = MeisterGeister.Logic.General.Würfel.Parse(_selectedRezept.Haltbarkeit, true).ToString();
                }
                else {
                    TaPHerstellung = -1;
                    ErgebnisQualität = Qualität.M;
                }
            }
        }
        #endregion     
        #region//-Analyse-
        private void ProbeIBAnalyse(object sender)
        {
            //Probe auf Erstellung würfeln
            if (SelectedHeld != null && SelectedIntensitätsbestimmungAnalyse != null)
            {
                //suche Probenart
                switch (SelectedIntensitätsbestimmungAnalyse)
                {
                    case "Odem Arcanum (Grundversion)":
                        Model.Zauber oag = Global.ContextHeld.LoadZauberByName("Odem Arcanum");
                        oag.Modifikator = SelectedRezept.Analyseschwierigkeit;
                        oag.Fertigkeitswert = SelectedHeld.Zauberfertigkeitswert("Odem Arcanum");
                        var ergebnisOAG = ShowProbeDialog(oag, SelectedHeld);
                        if (ergebnisOAG != null && ergebnisOAG.Gelungen)
                        {
                            WertDetailgradAnalyse = ergebnisOAG.Übrig;
                        }
                        else
                        {
                            WertDetailgradAnalyse = 0;
                        }
                        break;
                    case "Odem Arcanum (Sichtbereich)": 
                        Model.Zauber oas = Global.ContextHeld.LoadZauberByName("Odem Arcanum");
                        oas.Modifikator = SelectedRezept.Analyseschwierigkeit+2;
                        oas.Fertigkeitswert = SelectedHeld.Zauberfertigkeitswert("Odem Arcanum");
                        var ergebnisOAS = ShowProbeDialog(oas, SelectedHeld);
                        if (ergebnisOAS != null && ergebnisOAS.Gelungen)
                        {
                            WertDetailgradAnalyse = (int)Math.Ceiling((double)ergebnisOAS.Übrig / 2.0);
                        }
                        else
                        {
                            WertDetailgradAnalyse = 0;
                        }
                        break;
                    case "Odem Arcanum (Umgebung)": 
                        Model.Zauber oau = Global.ContextHeld.LoadZauberByName("Odem Arcanum");
                        oau.Modifikator = SelectedRezept.Analyseschwierigkeit + 7;
                        oau.Fertigkeitswert = SelectedHeld.Zauberfertigkeitswert("Odem Arcanum");
                        var ergebnisOAU = ShowProbeDialog(oau, SelectedHeld);
                        if (ergebnisOAU != null && ergebnisOAU.Gelungen)
                        {
                            WertDetailgradAnalyse = (int)Math.Ceiling((double)ergebnisOAU.Übrig / 2.0);
                        }
                        else
                        {
                            WertDetailgradAnalyse = 0;
                        }
                        break;
                    case "Oculus Astralis":
                        Model.Zauber oa = Global.ContextHeld.LoadZauberByName("Oculus Astralis");
                        oa.Modifikator = SelectedRezept.Analyseschwierigkeit;
                        oa.Fertigkeitswert = SelectedHeld.Zauberfertigkeitswert("Oculus Astralis");
                        var ergebnisOA = ShowProbeDialog(oa, SelectedHeld);
                        if (ergebnisOA != null && ergebnisOA.Gelungen)
                        {
                            WertDetailgradAnalyse = (int)Math.Max(ergebnisOA.Übrig-SelectedRezept.Analyseschwierigkeit,0);
                        }
                        else
                        {
                            WertDetailgradAnalyse = 0;
                        }
                        break;
                    case "Sicht auf Madas Welt": 
                        int taw;
                        Model.Held_Talent samw=SelectedHeld.GetHeldTalent("Liturgiekenntnis", true, out taw,false);
                        samw.Modifikator = SelectedRezept.Analyseschwierigkeit;
                        samw.Fertigkeitswert = taw;
                        var ergebnisSAMW = ShowProbeDialog(samw, SelectedHeld);
                        if (ergebnisSAMW != null && ergebnisSAMW.Gelungen)
                        {
                            WertDetailgradAnalyse = ergebnisSAMW.Übrig;
                        }
                        else
                        {
                            WertDetailgradAnalyse = 0;
                        }
                        break;
                    case "Magiegespür (gezielte Anwendung)":
                        Model.Talent mg = Global.ContextHeld.LoadTalentByName("Magiegespür");
                        mg.Modifikator = SelectedRezept.Analyseschwierigkeit;
                        mg.Fertigkeitswert = SelectedHeld.Talentwert("Magiegespür");
                        var ergebnisMG = ShowProbeDialog(mg, SelectedHeld);
                        if (ergebnisMG != null && ergebnisMG.Gelungen)
                        {
                            WertDetailgradAnalyse = ergebnisMG.Übrig;
                        }
                        else
                        {
                            WertDetailgradAnalyse = 0;
                        }
                        break;
                    case "Magiegespür (ungezielte Anwendung)": 
                        Model.Talent mu = Global.ContextHeld.LoadTalentByName("Magiegespür");
                        mu.Modifikator = SelectedRezept.Analyseschwierigkeit;
                        mu.Fertigkeitswert = SelectedHeld.Talentwert("Magiegespür");
                        var ergebnisMU = ShowProbeDialog(mu, SelectedHeld);
                        if (ergebnisMU != null && ergebnisMU.Gelungen)
                        {
                            WertDetailgradAnalyse = (int)Math.Ceiling((double)ergebnisMU.Übrig / 2.0);
                        }
                        else
                        {
                            WertDetailgradAnalyse = 0;
                        }
                        break;
                    case "Blutblatt": 
                        Model.Talent p = Global.ContextHeld.LoadTalentByName("Pflanzenkunde");
                        p.Modifikator = SelectedRezept.Analyseschwierigkeit;
                        p.Fertigkeitswert = SelectedHeld.Zauberfertigkeitswert("Pflanzenkunde");
                        var ergebnisP = ShowProbeDialog(p, SelectedHeld);
                        if (ergebnisP != null && ergebnisP.Gelungen)
                        {
                            WertDetailgradAnalyse = ergebnisP.Übrig;
                        }
                        else
                        {
                            WertDetailgradAnalyse = 0;
                        }
                        break;
                    case "Gespür der Keule":
                    default: break;
                }
            }
        }

        private void ProbeSAAnalyse(object sender)
        {
            //Probe auf Erstellung würfeln
            if (SelectedHeld != null && SelectedStrukturanalyseAnalyse != null)
            {
                int detailgrad = 0;
                //suche Probenart
                switch (SelectedStrukturanalyseAnalyse)
                {
                    case "Analyse nach Augenschein":
                        Model.Talent ana = Global.ContextHeld.LoadTalentByName("Alchimie");
                        int modANA = 0;
                        if (SelectedHeld.Talentwert("Sinnesschärfe") >= 10)
                        {
                            modANA = -(int)Math.Floor((double)(SelectedHeld.Talentwert("Sinnesschärfe") - 7) / 3.0);
                        }
                        detailgrad = (int)Math.Max(WertDetailgradAnalyse, WertFIBAnalyse);
                        ana.Modifikator = SelectedRezept.Analyseschwierigkeit - (int)Math.Ceiling(((double)detailgrad) / 2) + modANA;
                        ana.Fertigkeitswert = SelectedHeld.Talentwert("Alchimie");
                        var ergebnisANA = ShowProbeDialog(ana, SelectedHeld);
                        if (ergebnisANA != null && ergebnisANA.Gelungen)
                        {
                            WertStrukturergebnisAnalyse = Math.Min((int)Math.Ceiling((double)ergebnisANA.Übrig/2.0),4);
                        }
                        else
                        {
                            WertStrukturergebnisAnalyse = 0;
                        }
                        break;
                    case "Laboranalyse":
                        Model.Talent la = Global.ContextHeld.LoadTalentByName("Alchimie");
                        //Erhalt der Substanz
                        int modLA = 0;
                        if (CheckedErhaltAnalyseIsChecked) modLA += 3;
                        if (SelectedHeld.Talentwert("Sinnesschärfe") >= 10 || SelectedHeld.Talentwert("Pflanzenkunde") >= 10)
                        {
                            int maxT = Math.Max(SelectedHeld.Talentwert("Sinnesschärfe"), SelectedHeld.Talentwert("Pflanzenkunde"));
                            modLA = modLA - (int)Math.Floor((double)(maxT - 7) / 3.0);
                        }
                        detailgrad = (int)Math.Max(WertDetailgradAnalyse, WertFIBAnalyse);
                        la.Modifikator = SelectedRezept.Analyseschwierigkeit - (int)Math.Ceiling(((double)detailgrad) / 2) + modLA + getModLab("Archaisches Labor");
                        la.Fertigkeitswert = SelectedHeld.Talentwert("Alchimie");
                        var ergebnisLA = ShowProbeDialog(la, SelectedHeld);
                        if (ergebnisLA != null && ergebnisLA.Gelungen)
                        {
                            WertStrukturergebnisAnalyse = ergebnisLA.Übrig;
                        }
                        else
                        {
                            WertStrukturergebnisAnalyse = 0;
                        }
                        break;
                    case "Infundibulum der Allweisen":
                        Model.Talent ida = Global.ContextHeld.LoadTalentByName("Alchimie");
                        int modida = 0;
                        //Spezialisierung Mag Analyse beachten!
                        int wertMagiekunde = SelectedHeld.Talentwert("Magiekunde");
                        Model.Talent magiekunde = Global.ContextHeld.LoadTalentByName("Magiekunde");
                        var spez = magiekunde.Talentspezialisierungen(SelectedHeld);
                        if (spez!=null && spez.Where(t => t.StartsWith("Magische Analyse")).Count() > 0) wertMagiekunde += 2;
                        if (wertMagiekunde >= 10 || SelectedHeld.Talentwert("Pflanzenkunde") >= 10)
                        {
                            int maxT = Math.Max(wertMagiekunde, SelectedHeld.Talentwert("Pflanzenkunde"));
                            modida = modida - (int)Math.Floor((double)(maxT - 7) / 3.0);
                        }
                        ida.Modifikator =  modida;
                        ida.Fertigkeitswert = SelectedHeld.Talentwert("Alchimie");
                        var ergebnisIDA = ShowProbeDialog(ida, SelectedHeld);
                        if (ergebnisIDA != null && ergebnisIDA.Gelungen)
                        {
                            WertStrukturergebnisAnalyse = ergebnisIDA.Übrig+8;
                        }
                        else
                        {
                            WertStrukturergebnisAnalyse = 8;
                        }
                        break;
                    case "Allegorische Analyse":
                        Model.Talent aa = Global.ContextHeld.LoadTalentByName("Ritualkenntnis");
                        //Erhalt der Substanz
                        int modAA = 0;
                        if (SelectedHeld.Talentwert("Alchimie") >= 10)
                        {
                            modAA = -(int)Math.Floor((double)(SelectedHeld.Talentwert("Alchimie") - 7) / 3.0);
                        }
                        detailgrad = (int)Math.Max(WertDetailgradAnalyse, WertFIBAnalyse);
                        aa.Modifikator = SelectedRezept.Analyseschwierigkeit - (int)Math.Ceiling(((double)detailgrad) / 2) + modAA;
                        aa.Fertigkeitswert = SelectedHeld.Talentwert("Ritualkenntnis");
                        var ergebnisAA = ShowProbeDialog(aa, SelectedHeld);
                        if (ergebnisAA != null && ergebnisAA.Gelungen)
                        {
                            WertStrukturergebnisAnalyse = ergebnisAA.Übrig;
                        }
                        else
                        {
                            WertStrukturergebnisAnalyse = 0;
                        }
                        break;
                    case "Analys Arcanstruktur":
                        Model.Zauber zaa = Global.ContextHeld.LoadZauberByName("Analys Arcanstruktur");
                        int modZAA = 0;
                        //Spezialisierung Mag Analyse beachten!
                        int wertMagiekundeZAA = SelectedHeld.Talentwert("Magiekunde");
                        Model.Talent magiekundeZAA = Global.ContextHeld.LoadTalentByName("Magiekunde");
                        var spezZAA = magiekundeZAA.Talentspezialisierungen(SelectedHeld);
                        if (spezZAA != null && spezZAA.Where(t => t.StartsWith("Magische Analyse")).Count() > 0) wertMagiekundeZAA += 2;
                        if (SelectedHeld.Talentwert("Magiekunde") >= 10)
                        { 
                            modZAA = -(int)Math.Floor((double)(SelectedHeld.Talentwert("Magiekunde") - 7) / 3.0);
                        }
                        detailgrad = (int)Math.Max(WertDetailgradAnalyse, WertFIBAnalyse);
                        zaa.Modifikator = SelectedRezept.Analyseschwierigkeit - (int)Math.Ceiling(((double)detailgrad) / 2) + modZAA;
                        zaa.Fertigkeitswert = SelectedHeld.Zauberfertigkeitswert("Analys Arcanstruktur");
                        var ergebnisZAA = ShowProbeDialog(zaa, SelectedHeld);
                        if (ergebnisZAA != null && ergebnisZAA.Gelungen)
                        {
                            WertStrukturergebnisAnalyse = WertStrukturergebnisAnalyse+ ergebnisZAA.Übrig;
                        }
                        else
                        {
                            WertStrukturergebnisAnalyse = WertStrukturergebnisAnalyse;
                        }
                        break;
                    case "Oculus Astralis":
                        Model.Talent toa = Global.ContextHeld.LoadTalentByName("Sinnenschärfe");
                        int modZOA = 0;
                        //Spezialisierung Mag Analyse beachten!
                        int wertMagiekundeZOA = SelectedHeld.Talentwert("Magiekunde");
                        Model.Talent magiekundeZOA = Global.ContextHeld.LoadTalentByName("Magiekunde");
                        var spezZOA = magiekundeZOA.Talentspezialisierungen(SelectedHeld);
                        if (spezZOA!=null && spezZOA.Where(t => t.StartsWith("Magische Analyse")).Count() > 0) wertMagiekundeZOA += 2;
                        if (SelectedHeld.Talentwert("Alchimie") >= 10)
                        {
                            modZOA = -(int)Math.Floor((double)(SelectedHeld.Talentwert("Alchimie") - 7) / 3.0);
                        }
                        toa.Modifikator = SelectedRezept.Analyseschwierigkeit - (int)Math.Ceiling(((double)WertDetailgradAnalyse) / 2) + modZOA;
                        toa.Fertigkeitswert = SelectedHeld.Talentwert("Sinnenschärfe");
                        var ergebnisZOA = ShowProbeDialog(toa, SelectedHeld);
                        if (ergebnisZOA != null && ergebnisZOA.Gelungen)
                        {
                            WertStrukturergebnisAnalyse = WertStrukturergebnisAnalyse + ergebnisZOA.Übrig;
                        }
                        else
                        {
                            WertStrukturergebnisAnalyse = WertStrukturergebnisAnalyse;
                        }
                        break;
                    case "Blick der Weberin":
                        int taw;
                        Model.Held_Talent bdw = SelectedHeld.GetHeldTalent("Liturgiekenntnis", true, out taw, false);
                        //Model.Talent bdw = Global.ContextHeld.LoadTalentByName("Liturgiekenntnis");
                        // TODO MP zusätzliche Erschwernisse berechnen
                        int modBDW = 4;
                        //Spezialisierung Mag Analyse beachten!
                        int wertMagiekundeBDW = SelectedHeld.Talentwert("Magiekunde");
                        Model.Talent magiekundeBDW = Global.ContextHeld.LoadTalentByName("Magiekunde");
                        var spezBDW = magiekundeBDW.Talentspezialisierungen(SelectedHeld);
                        if (spezBDW != null && spezBDW.Where(t => t.StartsWith("Magische Analyse")).Count() > 0) wertMagiekundeBDW += 2;
                        if (SelectedHeld.Talentwert("Magiekunde") >= 10)
                        {
                            modBDW = -(int)Math.Floor((double)(SelectedHeld.Talentwert("Magiekunde") - 7) / 3.0);
                        }
                        detailgrad = (int)Math.Max(WertDetailgradAnalyse, WertFIBAnalyse);
                        bdw.Modifikator = SelectedRezept.Analyseschwierigkeit - (int)Math.Ceiling(((double)detailgrad) / 2) + modBDW;
                        bdw.Fertigkeitswert = taw;
                        var ergebnisBDW = ShowProbeDialog(bdw, SelectedHeld);
                        if (ergebnisBDW != null && ergebnisBDW.Gelungen)
                        {
                            WertStrukturergebnisAnalyse = WertStrukturergebnisAnalyse + ergebnisBDW.Übrig+5;
                        }
                        else
                        {
                            WertStrukturergebnisAnalyse = WertStrukturergebnisAnalyse;
                        }
                        break;                        
                    case "Blick durch Tairachs Augen":
                        int tawbdta;
                        Model.Held_Talent bdta = SelectedHeld.GetHeldTalent("Liturgiekenntnis", true, out tawbdta, false);
                        //Model.Talent bdta = Global.ContextHeld.LoadTalentByName("Liturgiekenntnis");
                        // TODO MP zusätzliche Erschwernisse berechnen
                        int modBDTA = 4;
                        //Spezialisierung Mag Analyse beachten!
                        int wertMagiekundeBDTA = SelectedHeld.Talentwert("Magiekunde");
                        Model.Talent magiekundeBDTA = Global.ContextHeld.LoadTalentByName("Magiekunde");
                        var spezBDTA = magiekundeBDTA.Talentspezialisierungen(SelectedHeld);
                        if (spezBDTA != null && spezBDTA.Where(t => t.StartsWith("Magische Analyse")).Count() > 0) wertMagiekundeBDTA += 2;
                        if (SelectedHeld.Talentwert("Magiekunde") >= 10)
                        {
                            modBDTA = -(int)Math.Floor((double)(SelectedHeld.Talentwert("Magiekunde") - 7) / 3.0);
                        }
                        detailgrad = (int)Math.Max(WertDetailgradAnalyse, WertFIBAnalyse);
                        bdta.Modifikator = SelectedRezept.Analyseschwierigkeit - (int)Math.Ceiling(((double)detailgrad) / 2) + modBDTA;
                        bdta.Fertigkeitswert = tawbdta;
                        var ergebnisBDTA = ShowProbeDialog(bdta, SelectedHeld);
                        if (ergebnisBDTA != null && ergebnisBDTA.Gelungen)
                        {
                            WertStrukturergebnisAnalyse = WertStrukturergebnisAnalyse + ergebnisBDTA.Übrig +5;
                        }
                        else
                        {
                            WertStrukturergebnisAnalyse = WertStrukturergebnisAnalyse;
                        }
                        break;                        
                    default: break;
                }
            }
        }
            
        #endregion
        #region//-Verdünnung-
        private void ProbeVerdünnung(object sender)
        {
            //Probe auf Erstellung würfeln
            if (SelectedHeld != null)
            { 
                Model.Talent alchimie = Global.ContextHeld.LoadTalentByName("Alchimie");
                int q = Convert.ToByte(Convert.ToChar(SelectedQualitätVerdünnung));
                int vq = Convert.ToByte(Convert.ToChar(SelectedVerdünnungQualitätVerdünnung));
                int diff = q - vq;
                alchimie.Modifikator = (diff * SelectedRezept.Brauschwierigkeit) - (int)Math.Ceiling(((double)(WertTaPVerdünnung + WertZfPVerdünnung)) / 2) + getModLab("Archaisches Labor");
                alchimie.Fertigkeitswert = SelectedHeld.Talentwert("Alchimie");
                var ergebnis = ShowProbeDialog(alchimie, SelectedHeld);
                if (ergebnis != null && ergebnis.Gelungen)
                {
                    VerdünnungQualität = SelectedVerdünnungQualitätVerdünnung;
                    VerdünnungAnzahl = diff+1;
                }
                else
                {
                    VerdünnungQualität = "M";
                    VerdünnungAnzahl = diff+1;
                }

            }
        }
         
        #endregion
        #region//-Haltbarkeit-
        private void ProbeHaltbarkeit(object sender)
        {
            
            //Probe auf Erstellung würfeln
            if (SelectedHeld != null)
            {
                Model.Talent alchimie = Global.ContextHeld.LoadTalentByName("Alchimie");
                alchimie.Modifikator = +9 + getModLab("Archaisches Labor");
                alchimie.Fertigkeitswert = SelectedHeld.Talentwert("Alchimie");
                var ergebnis = ShowProbeDialog(alchimie, SelectedHeld);
                if (ergebnis != null && ergebnis.Gelungen)
                {
                    HaltbarkeitWirkung = "Haltbarkeit verdoppelt";
                }
                else if (ergebnis.Ergebnis == MeisterGeister.Logic.General.ErgebnisTyp.PATZER || ergebnis.Ergebnis ==MeisterGeister.Logic.General.ErgebnisTyp.FATALER_PATZER)
                {
                    HaltbarkeitWirkung = wirkungenHaltbarkeit[MeisterGeister.Logic.General.Würfel.Parse("1W6 + 4", true)];
                }
                else{
                    HaltbarkeitWirkung = wirkungenHaltbarkeit[MeisterGeister.Logic.General.Würfel.Parse("1W6", true)];
                }

            }
        }

        #endregion
        #endregion

        private static List<string> wirkungenHaltbarkeit = new List<string>() 
        { "doppelte Haltbarkeit",
            "doppelte Haltbarkeit",
            "anderthalbfache Haltbarkeit, Qualität sinkt um eine Stufe",
            "keine Verlängerung der Haltbarkeit, Qualität sinkt um eine Stufe",
            "anderthalbfache Haltbarkeit",
            "Trank wird vollkommen wirkungslos",
            "Trank wird vollkommen wirkungslos",
            "Trank wird vollkommen wirkungslos",
            "die Wirkung des Trankes schlägt um in ein Gift (siehe Mandragora, GA 213: Stufe 2, 1W6 SP, Brechreiz/1W3 SP, +3 auf Handlungen",
            "die Wirkung des Trankes schlägt um in ein Gift (siehe Mandragora, GA 213: Stufe 2, 1W6 SP, Brechreiz/1W3 SP, +3 auf Handlungen"
        };
        private static List<string> wirkungen = new List<string>()
        {"Das Mittel hat keinerlei Wirkung.",
         "Die Mixtur verdampft völlig ohne jeden Rückstand.",
         "Vom Braugut bleibt nichts zurück, als ein Klumpen unmagischer Schlacke oder Asche.",
         "Das fertige Elixier hat genau die Gegenteilige Wirkung eines gelungenen. Würfeln Sie 1W20 um die Qualität zu bestimmen (1-5: A; 6-10: B; 11-14: C; 15-17: D; 18-19: E; 20: F) und drehen Sie die Wirkung um. Würde ein Trank 3W6 LeP zurückgeben, so verursacht er dieselbe Menge an Schaden.",
         "Das Gebräu neutralisiert jegliches andere im Moment wirkende oder binnen der nächsten 1W6 eingenommene Elixier - sei es ein alchimistisch hergestelltes (nicht natürliches) Gift oder Heiltrank.",
         "Eine Explosion während der Zubereitung steckt das Labor in Brand (5W6 SP; je 2 Schritt Entfernung sinkt der Schaden um 1W6; vorübergehende Taubheit)",
         "Die fertige Mixtur explodiert bei einer 1 bis 4 auf 1W6 beim Lagern, mit Sicherheit aber beim Entzünden oder einer nahen Hitzequelle, und verursacht je nach gelagerter Menge bis zu 5W20 SP im Umkreis von 3 Schritt (je weitere 3 Schritt Entfernung fällt die Schadenswirkung um 1W20 niedriger aus; vorübergehende Taubheit).",
         "Während der Zubereitung spritzt die Mixtur in alle Richtungen, entzündet sich und steckt das Labor in Brand.",
         "Bei der Herstellung kocht die Substanz über und macht alle nicht hitze- und feuchtigkeitsbeständigen Materialien in der Nähe unbrauchbar.",
         "Die Mixtur spritzt während der Zubereitung in alle Richtungen und frisst Löcher in alle nicht säure- oder hitzebeständigen Materialien (bis 3W6 SP und zurückbleibende Narben, wenn der Alchimist nicht ausreichend geschützt ist).",
         "Eine große Wolke aus Gasen breitet sich aus und lässt alle Lebewesen im Umkreis von 50 Schritt für 3W6 Stunden erblinden.",
         "Beim Brauen entwickeln sich giftige Dämpfe, der Alchimist erleidet 3W20 SP.",
         "Auf Grund austretender Dämpfe erleidet der Alchimist mehrere Tage lang Albträume (keine Regeneration).",
         "Das Gebräu stößt Dämpfe aus, die beim Alchimisten eine mehrtägige Geistesverwirrung auslösen.",
         "Beim Brauen kommt es zu einer Verpuffung, bei der eine farbige Wolke freigesetzt wird, die alles verfärbt, was mit ihr in Berührung kommt (also vermutlich auch den Alchimisten selbst). Die Farbe verblasst auch nach hartnäckigen Bürsten nur langsam und ist noch wenigstens 1W20 Tage lang zu sehen. Haare und Stoffe behalten die neue Farbe dauerhaft, wobei die Haare jedoch mit der tatsächlichen Farbe normal weiterwachsen. Insbesondere schmutzige oder giftige Farben wie Blutrot, Grasgrün, Purpur oder Zitronengelb kommen bei den Wolken sehr häufig vor.",
         "Der nach dem Entzünden des fertigen Mittels entstehende Rauch ist giftig und verursacht bei allen Anwesenden im Umkreis von drei Schritt minutenlangen Hustenreiz und 2W20 SP(A).",
         "Das Mittel verbrennt mit verschiedenen leuchtenden Farben, zeigt sonst aber keine Wirkung.",
         "Die Anwendung führt zu 2W6 SR mit starken Nebenwirkungen (lW6 Stunden lang z.B. häufiges Erbrechen, arger Juckreiz, Schlieren vor den Augen, schmerzender Ausschlag, starkes Brennen im Mundraum, Haarausfall, heftiges Bauchgrimmen, pochende Kopfschmerzen, sprießende Warzen etc.).",
         "Es entstand ein alchimistisches Einnahmegift der Stufe 10, der Anwender erleidet für 2W6+2 Stunden jeweils 1W6+1 SP.",
         "Der Anwender verliert 2W6 AsP (sinkt die AE dadurch unter 0, so werden die überzähligen Punkte von der LE abgezogen - beispielsweise bei Personen, die keine AsP besitzen).",
         "Die Einnahme fUhrt zu völliger Entkräftung (Ausdauer auf 0, 3W6 Punkte Erschöpfung).",
         "Die Anwendung bringt für 2W6 Stunden starke Kopfschmerzen mit sich (KL, IN, GE, FF je -2).",
         "Der Anwender ist für einen Tag vollkommen desorientiert (AT/PA, Fernkampf -4, KL/FF/GE -2; keine weiteren Auswirkungen auf die abgeleiteten Grundwerte).",
         "Der Anwender fällt für 5W6 Stunden in einen Tiefschlaf ohne jede Heilwirkung (Regeneration entfällt).",
         "Nach der Anwendung vermag der Betroffene selbst bei größter Müdigkeit für 1W6 Nächte keinen Schlaf zu finden (in der ersten Nacht zwei Punkte Erschöpfung, in der zweiten drei Punkte, in der dritten vier Punkte usw.).",
         "Der Betroffene wird für 1W20 KR zum Berserker (siehe Qualität C des Berserkerelixiers, WdA 59).",
         "Nach der Einnahme beginnt der Anwender für 1W20 Minuten unkontrolliert zu zucken, umher zu springen und ungezielt mit den Armen zu fuchteln (die AU sinkt dabei um den W20-Wurf x5 in Prozent).",
         "Eine (oder mehrere) passende Schlechte Eigenschaft steigt für 3W6 Stunden um 2W6 Punkte (anschließend besteht eine Chance von 10% (19 bis 20 auf 1W20), dass die Schlechte Eigenschaft permanent um einen Punkt steigt).",
         "Die Anwendung führt für 1W6 Stunden zu einer gichtartigen Lähmung der Finger (FF halbiert, min. -7).",
         "Der Anwender wird für 1W6 Stunden so gewandt, als wenn er sich in Thorwaler Rübenbrei bewegen würde (GE halbiert, min. -7).",
         "Der Anwender erhält für 3W6 Stunden das Einfühlungs- und Reaktionsvermögen eines Stalagmiten (IN halbiert, min. -7).",
         "Für kurze Zeit (3W6 SR) verfügt der Anwender über die unvergleichliche Intelligenz einer Riesenamöbe und ist zu wirklich nichts fahig (KL 0).",
         "Der Anwender ist für 1W20 Stunden schlapp wie tulamidischer Weichkäse, reagiert auf körperliche Beanspruchung überempfindlich und bekommt bereits blaue Flecken, wenn man ihn nur anstupst (KO halbiert, min. -7, AU auf 0).",
         "Der Benutzer erleidet einen W6 Stunden andauernden Schwächeanfall (KK halbiert, min. -7, KO -3) und mag sich überlegen, sein geliebtes Runenschwert vielleicht zu profaneren Zwecken - als Krücke - zu benutzen.",
         "Für 2W6 Stunden bekommt der Anwender eine ungesunde Hautfarbe und gelb angelaufene Augen und stinkt fürchterlich aus allen Poren (Nachteile Übler Geruch und Widerwärtiges Aussehen sowie CH halbiert, min. -7) .",
         "Der Anwender ist 1W6 Stunden lang mutlos, von Selbstzweifeln geplagt und zu nichts zu bewegen (alle vorhandenen Ängste aus Nachteilen werden verdoppelt, MU halbiert, min. -7).",
         "Der Benutzer begint unverständlich zu lallen, lässt sich zu fast jeder Schandtat überreden und ist ständig unentschlossen - kein Wunder bei einem MU- und KL-Wert von jeweils 6. Zum Glück hält dieser Zustand nur 3W6 Stunden lang an.",
         "Das Mittel hat keinerlei Wirkung - außer der, dass der unglückliche Anwender fur die nächsten 1W6 Tage irgendetwas ausströmt, das Kobolde, Feen, Wichtel und andere Feenwesen regelrecht anzuziehen scheint. In der Folgezeit ist der Arme sicherlich einigen derben Späßen ausgesetzt, die ihn Ansehen, Geld und Ruhm kosten könnten, jedoch niemals zu Lasten der Gesundheit gehen.",
         "Das betroffene Objekt oder Körperteil beginnt, binnen 1W6 Tagen völlig zu verrosten, verrotten, zerfallen oder verkümmern.",
         "Alles, was mit dem alchimistischen Produkt in Berührung kommt (oder jedes Wesen, das es einnimmt), fängt für 1W6 SR an, mystisch zu schimmern."
        };


    }
}


