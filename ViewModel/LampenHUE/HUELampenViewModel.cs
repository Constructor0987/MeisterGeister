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

namespace MeisterGeister.ViewModel.LampenHUE
{
    class HUELampenViewModel : Base.ToolViewModelBase
    {

        #region //---- FELDER ----
        
        #region //-Allgemein-
        
        //intern
        bool IsLoaded = false;
        //Listen
        private List<Held> _heldListe;
        private List<string> _gruppeListe;

        //Selections
        private Held _selectedHeld;
        private string _selectedGruppe;

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
        //public List<string> GruppeListe
        //{
        //    get { return Global.ContextHeld.LoadAlchimieGruppe(); }
        //    set
        //    {
        //        _gruppeListe = value;
        //        OnChanged("GruppeListe");
        //    }
        //}

        //Selections
        public Held SelectedHeld { get { return _selectedHeld; } set { 
            _selectedHeld = value;
            
            OnChanged("SelectedHeld");

        }
        }
        //public string SelectedGruppe
        //{
        //    get { return _selectedGruppe; }
        //    set
        //    {
        //        _selectedGruppe = value;
        //        OnChanged("SelectedGruppe");
        //        RezeptListe = Global.ContextHeld.LoadAlchimieRezepteByGruppe(value);
        //    }
        //}
        

        #endregion

        #endregion

        #region //---- KONSTRUKTOR ----

        public HUELampenViewModel()
            : base(View.General.ViewHelper.ShowProbeDialog)
        {
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

        #endregion

        #region //---- EVENTS ----
        private void ClearSelectedHeld(object obj)
        {
            SelectedHeld = null;
        }
        #endregion

    }
}


