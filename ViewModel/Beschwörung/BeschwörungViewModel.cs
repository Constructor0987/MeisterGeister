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

namespace MeisterGeister.ViewModel.Beschwörung
{
    class BeschwörungViewModel : Base.ViewModelBase
    {

        #region //---- FELDER ----
        
        #region //-Allgemein-
        //intern
        bool IsLoaded = false;
        //Enables

        //Listen
        private List<Held> _heldListe;
        //Selections
        private Held _selectedHeld;
        //Rückgaben

        //Checkboxen
        //commands
        private Base.CommandBase _onClearSelectedHeld;

        #endregion
        #region//-Dämonen-
        //intern

        //Enables
        private bool _dämonenIsEnabledKleidung;
        private bool _dämonenIsEnabledZauberkerzen;
        private bool _dämonenIsEnabledZauberkreide;
        private bool _dämonenIsEnabledDonariaAnrufung;
        private bool _dämonenIsEnabledDonariaKontrolle;
        private bool _dämonenIsEnabledPaktierer;
        private bool _dämonenIsEnabledIntegra;
        private bool _dämonenIsEnabledKraftlinie =true;
        //Listen
        private List<string> _dämonenListeUmständeOrt = ort;
        private List<string> _dämonenListeDämon;
        //Selections
        private string _selectedListeUmständeOrt;
        private string _selectedListeDämon;
        //Rückgaben
        private int _dämonenWertKleidungAnrufung = -1; 
        private int _dämonenWertKleidungKontrolle = -1;
        private int _dämonenWertZauberkerzen = -1;
        private int _dämonenWertZauberkreide = 0;
        private int _dämonenWertDonariaAnrufung = 0;
        private int _dämonenWertDonariaKontrolle = 0;
        private int _dämonenWertPaktierer = 0;
        private int _dämonenWertBannschwert;
        private int _dämonenWertPaktiererAnrufung;
        private int _dämonenWertPaktiererKontrolle;
        private int _dämonenWertAffinitätKontrolle;
        private int _dämonenWertPaktGP;
        private int _dämonenWertPaktGPKontrolle;
        private int _dämonenWertUmständeOrtAnrufung;
        private int _dämonenWertUmständeOrtKontrolle;
        private int _dämonenWertIntegraMalen;
        private int _dämonenWertIntegraMagiekunde;
        private int _dämonenWertKraftlinieStärke;
        private int _dämonenWertKraftlinieKontrolle;
        //Checkboxen
        private bool _dämonenCheckedKleidung;
        private bool _dämonenCheckedBannschwert;
        private bool _dämonenCheckedZauberkerzen;
        private bool _dämonenCheckedZauberkreide;
        private bool _dämonenCheckedDonaria;
        private bool _dämonenCheckedPaktierer;
        private bool _dämonenCheckedAffinität;
        private bool _dämonenCheckedPaktGP;
        private bool _dämonenCheckedIntegra;
        private bool _dämonenCheckedKraftlinie;
        //commands 
         private Base.CommandBase _dämonenOnProbeMagiekunde;
         private Base.CommandBase _dämonenOnProbeMalen;
        #endregion

        #endregion

        #region //---- EIGENSCHAFTEN ----

        #region //-Allgemein-  
         
        //intern

        //Enables 

        //Listen 
        public List<Held> HeldListe
        {
            get { return Global.ContextHeld.LoadHeldenGruppeWithAlchimie(); }
            set { _heldListe = value; OnChanged("HeldListe"); }
        }
        //Selections
        public Held SelectedHeld
        {
            get { return _selectedHeld; }
            set
            {
                _selectedHeld = value;
                if (value != null)
                {
                    resetDämonen();
                    resetElementare();
                    resetGeister();
                    resetUntote();
                    resetGolems();
                    resetChimären();

                }
                OnChanged("SelectedHeld");
            }
        }
        //Rückgaben 

        //Checkboxen     

        //Commands  
        public Base.CommandBase OnClearSelectedHeld
        {
            get { return _onClearSelectedHeld; }
        }
        

        #endregion
        #region //-Dämonen-

        //intern

        //Enables 
        public bool DämonenIsEnabledKleidung
        {
            get { return _dämonenIsEnabledKleidung; }
            set
            {
                _dämonenIsEnabledKleidung = value;
                OnChanged("DämonenIsEnabledKleidung");
            }
        }
        public bool DämonenIsEnabledZauberkerzen
        {
            get { return _dämonenIsEnabledZauberkerzen; }
            set
            {
                _dämonenIsEnabledZauberkerzen = value;
                OnChanged("DämonenIsEnabledZauberkerzen");
            }
        }
        public bool DämonenIsEnabledZauberkreide
        {
            get { return _dämonenIsEnabledZauberkreide; }
            set
            {
                _dämonenIsEnabledZauberkreide = value;
                OnChanged("DämonenIsEnabledZauberkreide");
            }
        }
        public bool DämonenIsEnabledDonariaAnrufung
        {
            get { return _dämonenIsEnabledDonariaAnrufung; }
            set
            {
                _dämonenIsEnabledDonariaAnrufung = value;
                OnChanged("DämonenIsEnabledDonariaAnrufung");
            }
        }
        public bool DämonenIsEnabledDonariaKontrolle
        {
            get { return _dämonenIsEnabledDonariaKontrolle; }
            set
            {
                _dämonenIsEnabledDonariaKontrolle = value;
                OnChanged("DämonenIsEnabledDonariaKontrolle");
            }
        }
        public bool DämonenIsEnabledIntegra
        {
            get { return _dämonenIsEnabledIntegra; }
            set
            {
                //TODO MP abhängig von Sonderfertigkeit
                _dämonenIsEnabledIntegra = value;
                OnChanged("DämonenIsEnabledIntegra");
            }
        }
        public bool DämonenIsEnabledKraftlinie
        {
            get { return _dämonenIsEnabledKraftlinie; }
            set
            {
                //TODO MP abhängig von Sonderfertigkeit
                _dämonenIsEnabledKraftlinie = value;
                _dämonenCheckedKraftlinie = value;
                OnChanged("DämonenIsEnabledKraftlinie");
            }
        }
        //Listen   
        public List<string> DämonenListeUmständeOrt
        {
            get { return _dämonenListeUmständeOrt; }
            set { _dämonenListeUmständeOrt = value; OnChanged("DämonenListeUmständeOrt"); }
        }
        public List<string> DämonenListeDämon
        {
            get { return _dämonenListeDämon; }
            set { _dämonenListeDämon = value; OnChanged("DämonenListeDämon"); }
        }
        //Selections 
        public string SelectedListeUmständeOrt
        {
            get { return _selectedListeUmständeOrt; }
            set
            {
                _selectedListeUmständeOrt = value;
                int i = ort.IndexOf(value);
                DämonenWertUmständeOrtAnrufung = ortAnrufung[ort.IndexOf(value)];
                DämonenWertUmständeOrtKontrolle = ortKontrolle[ort.IndexOf(value)];
                OnChanged("SelectedListeUmständeOrt");
            }
        }
        public string SelectedListeDämon
        {
            get { return _selectedListeDämon; }
            set
            {
                _selectedListeDämon = value;
                OnChanged("SelectedListeDämon");
            }
        }

        //Rückgaben 
        public int DämonenWertKleidungAnrufung
        {
            get { return _dämonenWertKleidungAnrufung; }
            set
            {
                if (value >= -2 && value <= -1) _dämonenWertKleidungAnrufung = value;
                OnChanged("DämonenWertKleidungAnrufung");
            }
        }
        public int DämonenWertKleidungKontrolle
        {
            get { return _dämonenWertKleidungKontrolle; }
            set
            {
                if (value >= -2 && value <= -1) _dämonenWertKleidungKontrolle = value;
                OnChanged("DämonenWertKleidungKontrolle");
            }
        }
        public int DämonenWertZauberkerzen
        {
            get { return _dämonenWertZauberkerzen; }
            set
            {
                if (value >= -3 && value <= -1) _dämonenWertZauberkerzen = value;
                OnChanged("DämonenWertZauberkerzen");
            }
        }
        public int DämonenWertZauberkreide
        {
            get { return _dämonenWertZauberkreide; }
            set
            {
                if (value >= -3 && value <= 3) _dämonenWertZauberkreide = value;
                OnChanged("DämonenWertZauberkreide");
            }
        }
        public int DämonenWertDonariaAnrufung
        {
            get { return _dämonenWertDonariaAnrufung; }
            set
            {
                if (value >= -7 && value <= 7) _dämonenWertDonariaAnrufung = value;
                OnChanged("DämonenWertDonariaAnrufung");
            }
        }
        public int DämonenWertDonariaKontrolle
        {
            get { return _dämonenWertDonariaKontrolle; }
            set
            {
                if (value >= -2 && value <= 0) _dämonenWertDonariaKontrolle = value;
                OnChanged("DämonenWertDonariaKontrolle");
            }
        }
        public int DämonenWertPaktierer
        {
            get { return _dämonenWertPaktierer; }
            set
            {
                if (value >= 1 && value <= 7)
                {
                    _dämonenWertPaktierer = value;
                    DämonenWertPaktiererAnrufung = -value;
                    DämonenWertPaktiererKontrolle = -value - 3;
                }
                OnChanged("DämonenWertPaktierer");
            }
        }
        public int DämonenWertBannschwert
        {
            get { return _dämonenWertBannschwert; }
            set
            {
                _dämonenWertBannschwert = value;
                OnChanged("DämonenWertBannschwert");
            }
        }
        public int DämonenWertPaktiererAnrufung
        {
            get { return _dämonenWertPaktiererAnrufung; }
            set
            {
                _dämonenWertPaktiererAnrufung = value;
                OnChanged("DämonenWertPaktiererAnrufung");
            }
        }
        public int DämonenWertPaktiererKontrolle
        {
            get { return _dämonenWertPaktiererKontrolle; }
            set
            {
                _dämonenWertPaktiererKontrolle = value;
                OnChanged("DämonenWertPaktiererKontrolle");
            }
        }
        public int DämonenWertAffinitätKontrolle
        {
            get { return _dämonenWertAffinitätKontrolle; }
            set
            {
                _dämonenWertAffinitätKontrolle = value;
                OnChanged("DämonenWertAffinitätKontrolle");
            }
        }
        public int DämonenWertPaktGP
        {
            get { return _dämonenWertPaktGP; }
            set
            {
                if (value >= 0)
                {
                    _dämonenWertPaktGP = value;
                    DämonenWertPaktGPKontrolle = value / 3;
                }
                OnChanged("DämonenWertPaktGP");
            }
        }
        public int DämonenWertPaktGPKontrolle
        {
            get { return _dämonenWertPaktGPKontrolle; }
            set
            {
                _dämonenWertPaktGPKontrolle = value;
                OnChanged("DämonenWertPaktGPKontrolle");
            }
        }
        public int DämonenWertUmständeOrtAnrufung
        {
            get { return _dämonenWertUmständeOrtAnrufung; }
            set
            {
                _dämonenWertUmständeOrtAnrufung = value;
                OnChanged("DämonenWertUmständeOrtAnrufung");
            }
        }
        public int DämonenWertUmständeOrtKontrolle
        {
            get { return _dämonenWertUmständeOrtKontrolle; }
            set
            {
                _dämonenWertUmständeOrtKontrolle = value;
                OnChanged("DämonenWertUmständeOrtKontrolle");
            }
        }
        public int DämonenWertIntegraMalen
        {
            get { return _dämonenWertIntegraMalen; }
            set
            {
                _dämonenWertIntegraMalen = value;
                OnChanged("DämonenWertIntegraMalen");
            }
        }
        public int DämonenWertIntegraMagiekunde
        {
            get { return _dämonenWertIntegraMagiekunde; }
            set
            {
                _dämonenWertIntegraMagiekunde = value;
                OnChanged("DämonenWertIntegraMagiekunde");
            }
        }
        public int DämonenWertKraftlinieStärke
        {
            get { return _dämonenWertKraftlinieStärke; }
            set
            {
                if (value > 0) {
                _dämonenWertKraftlinieStärke = value;
                DämonenWertKraftlinieKontrolle = -value / 4;
                }
                OnChanged("DämonenWertKraftlinieStärke");
            }
        }
        public int DämonenWertKraftlinieKontrolle
        {
            get { return _dämonenWertKraftlinieKontrolle; }
            set
            {
                _dämonenWertKraftlinieKontrolle = value;
                OnChanged("DämonenWertKraftlinieKontrolle");
            }
        }
        //Checkboxen        
        public bool DämonenCheckedKleidung
        {
            get { return _dämonenCheckedKleidung; }
            set
            {
                _dämonenCheckedKleidung = value;
                DämonenIsEnabledKleidung = value;
                OnChanged("DämonenCheckedKleidung");
            }
        }
        public bool DämonenCheckedBannschwert
        {
            get { return _dämonenCheckedBannschwert; }
            set
            {
                _dämonenCheckedBannschwert = value;
                if (value) DämonenWertBannschwert = -1;
                else DämonenWertBannschwert = 0;
                OnChanged("DämonenCheckedBannschwert");
            }
        }
        public bool DämonenCheckedZauberkerzen
        {
            get { return _dämonenCheckedZauberkerzen; }
            set
            {
                _dämonenCheckedZauberkerzen = value;
                DämonenIsEnabledZauberkerzen = value;
                OnChanged("DämonenCheckedZauberkerzen");
            }
        }
        public bool DämonenCheckedZauberkreide
        {
            get { return _dämonenCheckedZauberkreide; }
            set
            {
                _dämonenCheckedZauberkreide = value;
                DämonenIsEnabledZauberkreide = value;
                OnChanged("DämonenCheckedZauberkreide");
            }
        }
        public bool DämonenCheckedDonaria
        {
            get { return _dämonenCheckedDonaria; }
            set
            {
                _dämonenCheckedDonaria = value;
                DämonenIsEnabledDonariaAnrufung = value;
                DämonenIsEnabledDonariaKontrolle = value;
                OnChanged("DämonenCheckedDonaria");
            }
        }
        public bool DämonenCheckedPaktierer
        {
            get { return _dämonenCheckedPaktierer; }
            set
            {
                _dämonenCheckedPaktierer = value;
                if (!value)
                {
                    DämonenWertPaktiererAnrufung = 0;
                    DämonenWertPaktiererKontrolle = 0;
                }
                else DämonenWertPaktierer = DämonenWertPaktierer;
                OnChanged("DämonenCheckedPaktierer");
            }
        }
        public bool DämonenCheckedAffinität
        {
            get { return _dämonenCheckedAffinität; }
            set
            {
                _dämonenCheckedAffinität = value;
                if (!value) DämonenWertAffinitätKontrolle = 0;
                else DämonenWertAffinitätKontrolle = -3;
                OnChanged("DämonenCheckedAffinität");
            }
        }
        public bool DämonenCheckedPaktGP
        {
            get { return _dämonenCheckedPaktGP; }
            set
            {
                _dämonenCheckedPaktGP = value;
                if (!value)  DämonenWertPaktGPKontrolle = 0;
                else DämonenWertPaktGP = DämonenWertPaktGP;
                OnChanged("DämonenCheckedPaktGP");
            }
        }
        public bool DämonenCheckedIntegra
        {
            get { return _dämonenCheckedIntegra; }
            set
            {
                _dämonenCheckedIntegra = value;
                OnChanged("DämonenCheckedIntegra");
            }
        }
        public bool DämonenCheckedKraftlinie
        {
            get { return _dämonenCheckedKraftlinie; }
            set
            {
                _dämonenCheckedKraftlinie = value;
                if (!value) DämonenWertKraftlinieKontrolle = 0;
                else DämonenWertKraftlinieStärke = DämonenWertKraftlinieStärke;
                OnChanged("DämonenCheckedKraftlinie");
            }
        }
        //Commands  
        public Base.CommandBase DämonenOnProbeMagiekunde
        {
            get { return _dämonenOnProbeMagiekunde; }
        }
        public Base.CommandBase DämonenOnProbeMalen
        {
            get { return _dämonenOnProbeMalen; }
        }
        
        
            
        #endregion

        #endregion

        #region //---- KONSTRUKTOR ----

        public BeschwörungViewModel()
            : base(View.General.ViewHelper.ShowProbeDialog)
        {
            _onClearSelectedHeld = new Base.CommandBase(ClearSelectedHeld, null);
            _dämonenOnProbeMagiekunde = new Base.CommandBase(DämonenProbeMagiekunde, null);
            _dämonenOnProbeMalen = new Base.CommandBase(DämonenProbeMalen, null);
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

        #region //-Allgemein- 


        #endregion
        #region//-Dämonen-

        private void resetDämonen()
        {
          
        }
        #endregion
        #region//-Elementare-
 
        private void resetElementare()
        {
        
        }

        #endregion       
        #region//-Geister-
 
        private void resetGeister()
        {
 
        }

        #endregion       
        #region//-Untote-
 
        private void resetUntote()
        {
           
        }

        #endregion       
        #region//-Golems-
 
        private void resetGolems()
        {
           
        }

        #endregion       
        #region//-Chimären-
        private void resetChimären()
        {
           
        }
        #endregion
        #endregion

        #region //---- EVENTS ----
        #region//-Allgemein-
        private void ClearSelectedHeld(object obj)
        {
            SelectedHeld = null;
        }
        #endregion
        #region//-Dämonen-
        private void DämonenProbeMagiekunde(object obj)
        {

        }
        private void DämonenProbeMalen(object obj)
        {

        }
        #endregion

        #endregion


        private static List<string> ort = new List<string>() {
            "Pforte des Grauens (passende Domäne)",
            "Pforte des Grauens (andere Domäne)",
            "großes Unheiligtum (Zauber.B. Yol-Ghurmak)",
            "kleinere Kultstätte (z.B. Opferplatz)",
            "verseuchter Ort (z.B. schwarze Lande)",
            "gut vorbereiteter Ort (Zauber.B. Reinigung)",
            "sorgfältig ausgewählter ort (Zauber.BackgroundWorker. Affinität)",
            "nicht vorbereitet (z.B. spontane Anrufung)",
            "belebter Ort (z.B. Stadt, Reichsstraße)",
            "störende Aura (z.B. Wasser bei Azzitai)",
            "Elementarheiligtum (z.B. Tal der Elemente)",
            "einfach geweihter Boden (z.B. Kapelle)",
            "zweifach geweihter Boden (z.B. Tempel)",
            "heiliger Boden (z.B. Stadt des Lichts)"
        };
        private static List<int> ortAnrufung = new List<int>() {
            -7,
            -6,
            -5,
            -4,
            -3,
            -2,
            -1,
            1,
            2,
            3,
            4,
            5,
            6,
            7,
        };
        private static List<int> ortKontrolle = new List<int>() {
            -2,
            -2,
            -2,
            -1,
            -1,
            -1,
            0,
            0,
            1,
            1,
            1,
            2,
            2,
            2
        };

    }
}


