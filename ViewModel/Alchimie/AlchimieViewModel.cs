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

namespace MeisterGeister.ViewModel.Alchimie
{
    class AlchimieViewModel : Base.ViewModelBase
    {

        #region //---- FELDER ----
        
        #region //-Allgemein-
        /*
                                    <RadioButton GroupName="Labor" ToolTip="archaisches Labor"  Content="archaisches Labor" IsChecked="true"/>
                                    <RadioButton GroupName="Labor" ToolTip="Hexenküche" Content="Hexenküche"/>
                                    <RadioButton GroupName="Labor" ToolTip="Alchimisten-Labor" Content="Alchimisten-Labor" IsEnabled="{Binding IsEnabledAlchimistenLaborHerstellung}"/>
  */
        
        //intern
        bool IsLoaded = false;
        //Enables

        //Listen
        private List<Held> _heldListe;
        //Selections
        private Held _selectedHeld;
        #endregion

        #region//-Herstellung-
        //Enables
 //       private bool _isEnabledAlchimistenLaborHerstellung;
        private bool _isEnabledAufladen = true;
        private bool _isEnabledLaborArtListeHerstellung = true;
        private bool _isEnabledLaborQualitätListeHerstellung = true;
        //Listen
        private List<Talent> _talentListeHerstellung ;
        private List<string> _gruppeListeHerstellung;
        private List<Alchimierezept> _rezeptListeHerstellung;
        private List<string> _laborArtListeHerstellung = new List<string>(new string[] { "Archaisches Labor", "Hexenküche", "Alchimistenlabor" });
        private List<string> _laborQualitätListeHerstellung = new List<string>(new string[] { "normal", "hochwertig", "aussergew. hochwertig", "beschädigt" });
        private List<Zutat> _substitutionListeHerstellung;
        //Selections
        private Talent _selectedTalentHerstellung;
        private string _selectedGruppeHerstellung;
        private Alchimierezept _selectedRezeptHerstellung;
        private string _selectedLaborArtListeHerstellung;
        private string _selectedLaborQualitätListeHerstellung;
        //Rückgaben
        private bool _herstellungUnmöglich = false;
        private int _wertTaWTalent;
        private int _wertTaPZurückhaltenHerstellung;
        private int _wertAstralesAufladenHerstellung;
        private int _modifikatorLaborHerstellung;
        private int _modifikatorSubstitutionenHerstellung = 0;
        private int _modifikatorTaPZurückhaltenHerstellung = 0;
        private int _modifikatorCHHerstellung =0;
        private int _bonusTaPQualitätHerstellung;
        private int _bonusCHQualitätHerstellung=0;
        private int _bonusAAQualitätHerstellung;
        private int _probenModGesHerstellung;
        private string _herstellungWirkungM;
        private int _aSPEinsatzHerstellung=0;
        private string _bonusQualitätHerstellung="2W6 + TaP* ";
        //Checkboxen
        private bool _checkedChymischeHochzeitHerstellung;
        private bool _checkedFeuerUndEisHerstellung;
        private bool _checkedMandriconsBindungHerstellung;
        private bool _checkedTransmutationDerElementeHerstellung;
        //commands
        private Base.CommandBase onClearSelectedHeld = null;
        private Base.CommandBase OnClearSelectedHeld
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

        #region//-Analyse-
        //Listen
        private List<string> _intensitätsbestimmungListeAnalyse;
        //Selections
        private string _selectedIntensitätsbestimmungAnalyse;
        private List<string> _strukturanalyseListeAnalyse;
        private string _selectedStrukturanalyseAnalyse;
        #endregion

        #region//-Verdünnung-
        //Listen
        #endregion

        #region//-Laborbeute-
        //Listen
        #endregion

        #endregion

        #region //---- EIGENSCHAFTEN ----

        #region //-Allgemein-        
        //Listen
        public List<Held> HeldListe 
        { 
            get { return Global.ContextHeld.LoadHeldenGruppeWithAlchimie(); }
            set { _heldListe = value; OnChanged("HeldListe"); } 
        }
        //Selections
        public Held SelectedHeld { get { return _selectedHeld; } set { 
            _selectedHeld = value; 
            OnChanged("SelectedHeld");
            resetHerstellung();
            resetAnalyse();
            resetVerdünnung();
            TalentListeHerstellung = Global.ContextHeld.LoadAlchimieHerstellungTalenteByHeld(value);
            checkAufladen(value);
        }
        }
        
        #endregion

        //TODO MP wrap Alchimierezept; add WirkungM, calc Haltbarkeit and others fix?!
        #region//-Herstellung-
        public bool HerstellungUnmöglich
        {
            get { return _herstellungUnmöglich; }
            set { 
                _herstellungUnmöglich = value;
                OnChanged("HerstellungUnmöglich");
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

        public bool IsEnabledLaborArtListeHerstellung
        { 
            get { return _isEnabledLaborArtListeHerstellung; }
            set {
                _isEnabledLaborArtListeHerstellung = value;
                OnChanged("IsEnabledLaborArtListeHerstellung");
            }
        }
        public bool IsEnabledLaborQualitätListeHerstellung
        { 
            get { return _isEnabledLaborQualitätListeHerstellung; } 
            set {
                _isEnabledLaborQualitätListeHerstellung = value; 
                OnChanged("IsEnabledLaborQualitätListeHerstellung"); 
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
        public List<string> GruppeListeHerstellung
        {
            get { return Global.ContextHeld.LoadAlchimieGruppe(); } 
            set { 
                _gruppeListeHerstellung = value; 
                OnChanged("GruppeListeHerstellung");
            }
        }
        public List<Alchimierezept> RezeptListeHerstellung 
        { 
            get { return _rezeptListeHerstellung; }
            set {
                _rezeptListeHerstellung = value;
                OnChanged("RezeptListeHerstellung");
            } 
        }
        public List<string> LaborArtListeHerstellung
        { 
            get { return _laborArtListeHerstellung; }
            set { 
                _laborArtListeHerstellung = value;
                OnChanged("LaborArtListeHerstellung"); 
            } 
        }
        public List<string> LaborQualitätListeHerstellung
        { 
            get { return _laborQualitätListeHerstellung; } 
            set {
                _laborQualitätListeHerstellung = value;
                OnChanged("LaborQualitätListeHerstellung");
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
                _selectedTalentHerstellung = value; 
                OnChanged("SelectedTalentHerstellung");
                WertTaWTalent = SelectedHeld == null ? 0 : SelectedHeld.Talentwert(value);
            }
        }
        public string SelectedGruppeHerstellung
        { 
            get { return _selectedGruppeHerstellung; } 
            set { 
                _selectedGruppeHerstellung = value;
                OnChanged("SelectedGruppeHerstellung"); 
                RezeptListeHerstellung = Global.ContextHeld.LoadAlchimieRezepteByGruppe(value);
            } 
        }
        public Alchimierezept SelectedRezeptHerstellung
        { 
            get { return _selectedRezeptHerstellung; } 
            set { 
                _selectedRezeptHerstellung = value;
                OnChanged("SelectedRezeptHerstellung"); 
                SubstitutionListeHerstellung = getZutatenByRezept(value); 
                HerstellungWirkungM = getMWirkung(); 
                calculateProbenModGes();
            } 
        }
        public string SelectedLaborArtListeHerstellung 
        { 
            get { return _selectedLaborArtListeHerstellung; } 
            set {
                _selectedLaborArtListeHerstellung = value; 
                OnChanged("SelectedLaborArtListeHerstellung");
                calculateModLab(); 
                calculateProbenModGes();
            } 
        }
        public string SelectedLaborQualitätListeHerstellung
        { 
            get { return _selectedLaborQualitätListeHerstellung; } 
            set { 
                _selectedLaborQualitätListeHerstellung = value; 
                OnChanged("SelectedLaborQualitätListeHerstellung");
                calculateModLab(); 
                calculateProbenModGes();
            } 
        }
        //Rückgaben
        public int WertTaWTalent 
        { 
            get { return _wertTaWTalent; } 
            set { 
                _wertTaWTalent = value;
                OnChanged("WertTaWTalent");
            } 
        }
        public int WertTaPZurückhaltenHerstellung
        { 
            get { return _wertTaPZurückhaltenHerstellung; } 
            set {
                if (SelectedRezeptHerstellung!=null && value >= 0 && value <= SelectedRezeptHerstellung.Brauschwierigkeit) 
                { 
                    _wertTaPZurückhaltenHerstellung = value; 
                    OnChanged("WertTaPZurückhaltenHerstellung"); 
                    ModifikatorTaPZurückhaltenHerstellung = _wertTaPZurückhaltenHerstellung; 
                    BonusTaPQualitätHerstellung = _wertTaPZurückhaltenHerstellung;
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
                        if (CheckedChymischeHochzeitHerstellung) 
                            ASPEinsatzHerstellung = (int)Math.Ceiling(((double)value)/2); 
                        else 
                            ASPEinsatzHerstellung = value; 
                    }
                    else if (((Math.Log10(value) / Math.Log10(2.0)) % 1) == 0) 
                    { 
                        BonusAAQualitätHerstellung = (int)(Math.Log10(value) / Math.Log10(2.0)) + 1;
                        if (CheckedChymischeHochzeitHerstellung) 
                            ASPEinsatzHerstellung = (int)Math.Ceiling((double)value / 2.0); 
                        else 
                            ASPEinsatzHerstellung = value; 
                    }
                    OnChanged("WertAstralesAufladenHerstellung");
                }
            }
        }
        public int ModifikatorLaborHerstellung
        { 
            get { return _modifikatorLaborHerstellung; } 
            set {
                _modifikatorLaborHerstellung = value; 
                OnChanged("ModifikatorLaborHerstellung");
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
        public int ProbenModGesHerstellung 
        { 
            get { return _probenModGesHerstellung; } 
            set { 
                _probenModGesHerstellung = value; 
                OnChanged("ProbenModGesHerstellung"); 
            } 
        }
        public string HerstellungWirkungM
        {
            get { return _herstellungWirkungM; } 
            set {
                _herstellungWirkungM = value; 
                OnChanged("HerstellungWirkungM");
            } 
        }
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
                calculateProbenModGes();
            } 
        }
        public int ModifikatorCHHerstellung 
        { 
            get { return _modifikatorCHHerstellung; } 
            set {
                _modifikatorCHHerstellung = value;
                OnChanged("ModifikatorCHHerstellung");
            } 
        }
        public int BonusTaPQualitätHerstellung 
        {
            get { return _bonusTaPQualitätHerstellung; } 
            set { 
                if (value >= 2)
                {
                    _bonusTaPQualitätHerstellung = value * 2;
                } 
                else
                    _bonusTaPQualitätHerstellung = 0; 
                OnChanged("BonusTaPQualitätHerstellung");
                calculateQualiModGes();
            }
        }
        public int BonusCHQualitätHerstellung
        { 
            get { return _bonusCHQualitätHerstellung; } 
            set {
                _bonusCHQualitätHerstellung = value; 
                OnChanged("BonusCHQualitätHerstellung");
                calculateQualiModGes();
            }
        }
        public int BonusAAQualitätHerstellung
        {
            get { return _bonusAAQualitätHerstellung; } 
            set { 
                _bonusAAQualitätHerstellung = value; 
                OnChanged("BonusAAQualitätHerstellung"); 
                calculateQualiModGes();
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
        public string BonusQualitätHerstellung 
        { 
            get { return _bonusQualitätHerstellung; } 
            set {
                _bonusQualitätHerstellung = value; 
                OnChanged("BonusQualitätHerstellung"); 
            } 
        }

        //Checkboxen
        public bool CheckedChymischeHochzeitHerstellung
        {
            get { return _checkedChymischeHochzeitHerstellung; }
            set {
                _checkedChymischeHochzeitHerstellung = value;
                OnChanged("CheckedChymischeHochzeitHerstellung");
                if (value) 
                { 
                    ModifikatorCHHerstellung = -1; 
                    BonusCHQualitätHerstellung = 2; 
                }
                else 
                { 
                    ModifikatorCHHerstellung = 0; 
                    BonusCHQualitätHerstellung = 0;
                } 
                WertAstralesAufladenHerstellung = WertAstralesAufladenHerstellung; 
                calculateProbenModGes();
            }
        }
        public bool CheckedFeuerUndEisHerstellung
        { 
            get { return _checkedFeuerUndEisHerstellung; } 
            set { 
                _checkedFeuerUndEisHerstellung = value;
                OnChanged("CheckedFeuerUndEisHerstellung");
            } 
        }
        public bool CheckedMandriconsBindungHerstellung 
        { 
            get { return _checkedMandriconsBindungHerstellung; } 
            set {
                _checkedMandriconsBindungHerstellung = value; 
                OnChanged("CheckedMandriconsBindungHerstellung");
            }
        }
        public bool CheckedTransmutationDerElementeHerstellung 
        { 
            get { return _checkedTransmutationDerElementeHerstellung; } 
            set {
                _checkedTransmutationDerElementeHerstellung = value;
                OnChanged("CheckedTransmutationDerElementeHerstellung");
            } 
        }
        //Commands

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
            }
        }
        public string SelectedStrukturanalyseAnalyse 
        {
            get { return _selectedStrukturanalyseAnalyse; }
            set {
                _selectedStrukturanalyseAnalyse = value;
                OnChanged("SelectedStrukturanalyseAnalyse");
            }
        }
        
        #endregion
        #region//-Verdünnung-
        //Listen
        #endregion
        #region//-Laborbeute-
        //Listen
        #endregion

        #endregion

        #region //---- KONSTRUKTOR ----

        public AlchimieViewModel()
        {
         
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


        private void checkAufladen(Held held)
        {
            //check Magiekunde>7 && Alchimie>7; set IsEnabledAufladen
        }

        #endregion

        #region //---- METHODEN ----

        #region //-Allgemein- 
        #endregion
        #region//-Herstellung-
        private void resetHerstellung()
        {
            //TODO: MP implement
        }
        private void calculateModLab()
        {
            int mod = 0;
            if (SelectedRezeptHerstellung != null) {
                HerstellungUnmöglich = false;
                switch (SelectedLaborArtListeHerstellung)
                {
                    case "Archaisches Labor": switch (SelectedRezeptHerstellung.Labor)
                        {
                            case "Hexenküche": mod = 7; break;
                            //keine Herstellung möglich
                            case "Alchimistenlabor": HerstellungUnmöglich = true; ModifikatorLaborHerstellung = 0; return;
                        }; break;
                    case "Hexenküche": switch (SelectedRezeptHerstellung.Labor)
                        {
                            case "Alchimistenlabor": mod = 7; break;
                        }; break;
                    case "Alchimistenlabor": switch (SelectedRezeptHerstellung.Labor)
                        {
                            case "archaisches Labor": mod = -3; break;
                        }; break;
                    default: break;
                }
            }
            switch (SelectedLaborQualitätListeHerstellung)
            {
                case "hochwertig": mod -= 3; break;
                case "aussergew. hochwertig": mod -= 7; break;
                case "beschädigt": mod += 3; break;
                default: break;
            }
            ModifikatorLaborHerstellung = mod;
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
                    int anzahl = Convert.ToInt32(tt[0]);
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

        private void calculateProbenModGes()
        {
            if (SelectedRezeptHerstellung == null)
                return;
            if (HerstellungUnmöglich)
            {
                return;
            }
            else
            {
                int mod = SelectedRezeptHerstellung.Brauschwierigkeit;
                if (CheckedChymischeHochzeitHerstellung)
                    mod += ModifikatorCHHerstellung;
                mod += ModifikatorTaPZurückhaltenHerstellung;
                mod += ModifikatorSubstitutionenHerstellung;
                mod += ModifikatorLaborHerstellung;
                ProbenModGesHerstellung = mod;
                return;
            }
        }

        private void calculateQualiModGes()
        {
            if (HerstellungUnmöglich)
            {
                BonusQualitätHerstellung = "nicht möglich";
                return;
            }
            else
            {
                int mod = 0;
                if (CheckedChymischeHochzeitHerstellung)
                    mod += BonusCHQualitätHerstellung;
                mod += BonusTaPQualitätHerstellung;
                mod += BonusAAQualitätHerstellung;
                BonusQualitätHerstellung = "2W6 + TaP* +" + mod.ToString();                
                return;
            }
        }
        private string getMWirkung()
        {
            return wirkungen[MeisterGeister.Logic.General.RandomNumberGenerator.RandomInt(0, wirkungen.Count-1)];
        }

        #endregion
        #region//-Analyse-
        private void resetAnalyse()
        {
            IntensitätsbestimmungListeAnalyse = Global.ContextHeld.LoadIntensitätsbestimmungFertigkeitenAlchimieByHeld(SelectedHeld);
            StrukturanalyseListeAnalyse = Global.ContextHeld.LoadStrukturanalyseFertigkeitenAlchimieByHeld(SelectedHeld);
        }
        #endregion
        #region//-Verdünnung-
        private void resetVerdünnung()
        {
            //TODO: MP implement
        }
        #endregion
        #region//-Laborbeute-
        #endregion

        #endregion

        #region //---- EVENTS ----
        void Zutat_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            calculateModSubstitutionen();
            calculateProbenModGes();
        }
        #endregion


        private List<string> wirkungen = new List<string>()
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


