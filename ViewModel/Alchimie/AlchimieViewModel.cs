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
        //Listen
        private List<Talent> _talentListeHerstellung;
        //Selections
        private Talent _selectedTalentHerstellung;
        //Rückgaben
        private string _wertLaborHerstellung;
        private int _wertTaPZurückhaltenHerstellung;
        private int _wertAstralesAufladenHerstellung;
        //Checkboxen
        private bool _checkedChymischeHochzeitHerstellung;
        private bool _checkedFeuerUndEisHerstellung;
        private bool _checkedMandriconsBindungHerstellung;
        private bool _checkedTransmutationDerElementeHerstellung;
        
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
        public List<Held> HeldListe { get { return _heldListe; } set { _heldListe = value; OnChanged("HeldListe"); } }
        //Selections
        public Held SelectedHeld { get { return _selectedHeld; } set { _selectedHeld = value; OnChanged("SelectedHeld"); initAnalyse(); checkAufladen(value); } }
        
        #endregion

        #region//-Herstellung-
        //Enables
        public bool IsEnabledAlchimistenLaborHerstellung { get { return _isEnabledAlchimistenLaborHerstellung; } set { _isEnabledAlchimistenLaborHerstellung = value; OnChanged("IsEnabledAlchimistenLaborHerstellung"); } }
        public bool IsEnabledAufladen { get { return _isEnabledAufladen; } set { _isEnabledAufladen = value; OnChanged("IsEnabledAufladen"); } }
        //Listen
        public List<Talent> TalentListeHerstellung { get { return _talentListeHerstellung; } set { _talentListeHerstellung = value; OnChanged("TalentListeHerstellung"); } }
        //Selections
        public Talent SelectedTalentHerstellung { get { return _selectedTalentHerstellung; } set { _selectedTalentHerstellung = value; OnChanged("SelectedTalentHerstellung"); } }
        //Rückgaben
        public string WertLaborHerstellung { get { return _wertLaborHerstellung; } set { _wertLaborHerstellung = value; OnChanged("WertLaborHerstellung"); } }
        public int WertTaPZurückhaltenHerstellung { get { return _wertTaPZurückhaltenHerstellung; } set { _wertTaPZurückhaltenHerstellung = value; OnChanged("WertTaPZurückhaltenHerstellung"); } }
        public int WertAstralesAufladenHerstellung { get { return _wertAstralesAufladenHerstellung; } set { _wertAstralesAufladenHerstellung = value; OnChanged("WertAstralesAufladenHerstellung"); } }
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

        private void initAnalyse()
        {
            IntensitätsbestimmungListeAnalyse = Global.ContextHeld.LoadIntensitätsbestimmungFertigkeitenAlchimieByHeld(SelectedHeld);
            StrukturanalyseListeAnalyse = Global.ContextHeld.LoadStrukturanalyseFertigkeitenAlchimieByHeld(SelectedHeld);
        }

        #endregion

        #region //---- METHODEN ----
 
        #endregion

        #region //---- EVENTS ----

  
        #endregion


    }
}


