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
        private bool _isEnabledAlchimistenLaborHerstellung;
        private bool _isEnabledAufladen;
        private bool _isEnabledLaborArtListeHerstellung = true;
        private bool _isEnabledLaborQualitätListeHerstellung = true;
        //Listen
        private List<Talent> _talentListeHerstellung;
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
        private int _wertTaWTalent;
        private int _wertTaPZurückhaltenHerstellung;
        private int _wertAstralesAufladenHerstellung;
        private string _modifikatorLaborHerstellung;
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
        public List<Held> HeldListe { get { return Global.ContextHeld.LoadHeldenGruppeWithAlchimie(); } set { _heldListe = value; OnChanged("HeldListe"); } }
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

        #region//-Herstellung-
        //Enables
        public bool IsEnabledAlchimistenLaborHerstellung { get { return _isEnabledAlchimistenLaborHerstellung; } set { _isEnabledAlchimistenLaborHerstellung = value; OnChanged("IsEnabledAlchimistenLaborHerstellung"); } }
        public bool IsEnabledAufladen { get { return _isEnabledAufladen; } set { _isEnabledAufladen = value; OnChanged("IsEnabledAufladen"); } }
        public bool IsEnabledLaborArtListeHerstellung { get { return _isEnabledLaborArtListeHerstellung; } set { _isEnabledLaborArtListeHerstellung = value; OnChanged("IsEnabledLaborArtListeHerstellung"); } }
        public bool IsEnabledLaborQualitätListeHerstellung { get { return _isEnabledLaborQualitätListeHerstellung; } set { _isEnabledLaborQualitätListeHerstellung = value; OnChanged("IsEnabledLaborQualitätListeHerstellung"); } }
        //Listen
        public List<Talent> TalentListeHerstellung { get { return _talentListeHerstellung; } set { _talentListeHerstellung = value; OnChanged("TalentListeHerstellung"); } }
        public List<string> GruppeListeHerstellung { get { return Global.ContextHeld.LoadAlchimieGruppe(); } set { _gruppeListeHerstellung = value; OnChanged("GruppeListeHerstellung"); } }
        public List<Alchimierezept> RezeptListeHerstellung { get { return _rezeptListeHerstellung; } set { _rezeptListeHerstellung = value; OnChanged("RezeptListeHerstellung"); } }
        public List<string> LaborArtListeHerstellung { get { return _laborArtListeHerstellung; } set { _laborArtListeHerstellung = value; OnChanged("LaborArtListeHerstellung"); } }
        public List<string> LaborQualitätListeHerstellung { get { return _laborQualitätListeHerstellung; } set { _laborQualitätListeHerstellung = value; OnChanged("LaborQualitätListeHerstellung"); } }
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
            }
        }
        //Selections
        public Talent SelectedTalentHerstellung { get { return _selectedTalentHerstellung; } set { 
            _selectedTalentHerstellung = value; 
            OnChanged("SelectedTalentHerstellung");
            WertTaWTalent = SelectedHeld == null ? 0 : SelectedHeld.Talentwert(value);
        }
        }
        public string SelectedGruppeHerstellung { get { return _selectedGruppeHerstellung; } set { _selectedGruppeHerstellung = value; OnChanged("SelectedGruppeHerstellung"); RezeptListeHerstellung = Global.ContextHeld.LoadAlchimieRezepteByGruppe(value); } }
        public Alchimierezept SelectedRezeptHerstellung { get { return _selectedRezeptHerstellung; } set { _selectedRezeptHerstellung = value; OnChanged("SelectedRezeptHerstellung"); SubstitutionListeHerstellung = getZutatenByRezept(value); } }
        public string SelectedLaborArtListeHerstellung { get { return _selectedLaborArtListeHerstellung; } set { _selectedLaborArtListeHerstellung = value; OnChanged("SelectedLaborArtListeHerstellung"); calculateModLab(); } }
        public string SelectedLaborQualitätListeHerstellung { get { return _selectedLaborQualitätListeHerstellung; } set { _selectedLaborQualitätListeHerstellung = value; OnChanged("SelectedLaborQualitätListeHerstellung"); calculateModLab(); } }
        //Rückgaben
        public int WertTaWTalent { get { return _wertTaWTalent; } set { _wertTaWTalent = value; OnChanged("WertTaWTalent"); } }
        public int WertTaPZurückhaltenHerstellung { get { return _wertTaPZurückhaltenHerstellung; } set { _wertTaPZurückhaltenHerstellung = value; OnChanged("WertTaPZurückhaltenHerstellung"); } }
        public int WertAstralesAufladenHerstellung { get { return _wertAstralesAufladenHerstellung; } set { _wertAstralesAufladenHerstellung = value; OnChanged("WertAstralesAufladenHerstellung"); } }
        public string ModifikatorLaborHerstellung { get { return _modifikatorLaborHerstellung; } set { _modifikatorLaborHerstellung = value; OnChanged("ModifikatorLaborHerstellung"); } }
        //Checkboxen
        public bool CheckedChymischeHochzeitHerstellung { get { return _checkedChymischeHochzeitHerstellung; } set { _checkedChymischeHochzeitHerstellung = value; OnChanged("CheckedChymischeHochzeitHerstellung"); } }
        public bool CheckedFeuerUndEisHerstellung { get { return _checkedFeuerUndEisHerstellung; } set { _checkedFeuerUndEisHerstellung = value; OnChanged("CheckedFeuerUndEisHerstellung"); } }
        public bool CheckedMandriconsBindungHerstellung { get { return _checkedMandriconsBindungHerstellung; } set { _checkedMandriconsBindungHerstellung = value; OnChanged("CheckedMandriconsBindungHerstellung"); } }
        public bool CheckedTransmutationDerElementeHerstellung { get { return _checkedTransmutationDerElementeHerstellung; } set { _checkedTransmutationDerElementeHerstellung = value; OnChanged("CheckedTransmutationDerElementeHerstellung"); } }
        //Commands

        #endregion
 
        #region//-Analyse-
        //Listen
        public List<string> IntensitätsbestimmungListeAnalyse { get { return _intensitätsbestimmungListeAnalyse; } set { _intensitätsbestimmungListeAnalyse = value; OnChanged("IntensitätsbestimmungListeAnalyse"); } }
        public List<string> StrukturanalyseListeAnalyse { get { return _strukturanalyseListeAnalyse; } set { _strukturanalyseListeAnalyse = value; OnChanged("StrukturanalyseListeAnalyse"); } }
        
        //Selections
        public string SelectedIntensitätsbestimmungAnalyse { get { return _selectedIntensitätsbestimmungAnalyse; } set { _selectedIntensitätsbestimmungAnalyse = value; OnChanged("SelectedIntensitätsbestimmungAnalyse"); } }
        public string SelectedStrukturanalyseAnalyse { get { return _selectedStrukturanalyseAnalyse; } set { _selectedStrukturanalyseAnalyse = value; OnChanged("SelectedStrukturanalyseAnalyse"); } }
        
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
                switch (SelectedLaborArtListeHerstellung)
                {
                    case "Archaisches Labor": switch (SelectedRezeptHerstellung.Labor)
                        {
                            case "Hexenküche": mod = 7; break;
                            case "Alchimistenlabor": ModifikatorLaborHerstellung = "nicht möglich"; return;
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
            ModifikatorLaborHerstellung = mod.ToString();
        }
        public List<Zutat> getZutatenByRezept(Alchimierezept rezept)
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
            //TODO: MP implement
        }

        #endregion




    }
}


