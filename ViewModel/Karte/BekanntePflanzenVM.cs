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
using System.Collections.ObjectModel;
using MeisterGeister.ViewModel.ZooBot.Logic.Regionen;
using MeisterGeister.ViewModel.ZooBot.Logic.Landschaften;
using MeisterGeister.ViewModel.ZooBot.Logic.Pflanzen;
using MeisterGeister.ViewModel.ZooBot.Logic.Tiere;
using System.Collections;
using MeisterGeister.View.Karte;
using System.Windows.Forms;
using MeisterGeister.Logic.Kalender;
using MeisterGeister.ViewModel.Karte.Logic;
using System.Windows.Data;
using System.Globalization;

using MeisterGeister.ViewModel.Karte;
using MeisterGeister.View.General;

namespace MeisterGeister.ViewModel.Karte
{

    public class BekanntePflanzenVM : Base.ViewModelBase
    {
        //public Action CloseAction { get; set; }
        
        #region //---- FELDER ----

        // Listen
        private List<Model.Pflanze> _pflanzenImGebiet = new List<Model.Pflanze>();
        private List<Model.Pflanze> _pflanzenImGebietUndLandschaft = new List<Model.Pflanze>();


        private string _pflanzenFilter = "";
        public string PflanzenFilter
        {
            get { return _pflanzenFilter; }
            set
            {
                if (Set(ref _pflanzenFilter, value) && PflanzenListe != null)
                    FilteredPflanzenListe = PflanzenListe.FindAll(t => t.Name.ToLower().Contains(value.ToLower()));                     
            }
        }

        private PflanzenAnOrtViewModel _PflanzenAnOrtVM;
        public PflanzenAnOrtViewModel PflanzenAnOrtVM
        {
            get { return _PflanzenAnOrtVM; }
            set
            {
                Set(ref _PflanzenAnOrtVM, value);
                if (value != null)
                {
                    InitPflanzenListe();
                    OnChanged("BekannteHeldenPflanzen");
                }
            }
        }     
           
        public List<Held_Pflanze> BekannteHeldenPflanzen
        {
            get { return Global.SelectedHeld.Held_Pflanze.OrderBy(t => t.Pflanze.Name).ToList(); }
        }

        private List<Pflanze> _filteredPflanzenListe;
        public List<Pflanze> FilteredPflanzenListe
        {
            get { return _filteredPflanzenListe; }
            set
            {
                _filteredPflanzenListe = value;

                OnChanged();
            }
        }

        private List<Pflanze> _pflanzenListe;
        public List<Pflanze> PflanzenListe
        {
            get { return _pflanzenListe; }
            set
            {
                _pflanzenListe = value;
                OnChanged();
            }
        }

        private Pflanze _pflanzeAuswahl;
        public Pflanze PflanzeAuswahl
        {
            get { return _pflanzeAuswahl; }
            set
            {
                _pflanzeAuswahl = value;
                OnChanged();
            }
        }
        
        private bool _pflanzeBekannt = false;
        public bool PflanzeBekannt
        {
            get { return _pflanzeBekannt; }
            set
            {
                _pflanzeBekannt = value;
                OnChanged();
            }
        }
        

        #region //-Allgemein-
        //Booleans
        private bool IsLoaded;
        //Listen
        
        //Werte
        
        #endregion

        #endregion

        #region //---- EIGENSCHAFTEN ----

        #endregion

        #region //---- KONSTRUKTOR ----

        public BekanntePflanzenVM()
            : base(View.General.ViewHelper.ShowProbeDialog)
        {
            Init();
        }

        #endregion
                        
        #region //---- INSTANZMETHODEN ----

        public void Init()
        {
            if (IsLoaded == false)
            {
                Refresh();
                IsLoaded = true;
            }
        }

        public void InitPflanzenListe()
        {
            List<Model.Pflanze> uListe = new List<Model.Pflanze>();
            
            uListe = new List<Pflanze>();
            uListe = PflanzenAnOrtVM.Pflanzen.OrderBy(t => t.Name).ToList();

            PflanzenListe = uListe;
            FilteredPflanzenListe = PflanzenListe;
        }
        
        public void Refresh()
        {
            OnChanged("BekannteHeldenPflanzen");
        }        

        #endregion

        #region //---- EVENTS ----


        private Base.CommandBase _onPflanzenFilterLöschen = null;
        public Base.CommandBase OnPflanzenFilterLöschen
        {
            get
            {
                if (_onPflanzenFilterLöschen == null)
                    _onPflanzenFilterLöschen = new Base.CommandBase(PflanzenFilterLöschen, null);
                return _onPflanzenFilterLöschen;
            }
        }
        void PflanzenFilterLöschen(object obj)
        {
            PflanzenFilter = "";
        }

        private Base.CommandBase _onBekanntePflanzeEntfernen = null;
        public Base.CommandBase OnBekanntePflanzeEntfernen
        {
            get
            {
                if (_onBekanntePflanzeEntfernen == null)
                    _onBekanntePflanzeEntfernen = new Base.CommandBase(BekanntePflanzeEntfernen, null);
                return _onBekanntePflanzeEntfernen;
            }
        }
        void BekanntePflanzeEntfernen(object obj)
        {
            Held_Pflanze hPflanze = obj as Held_Pflanze;
            if (Global.SelectedHeld.Held_Pflanze.Contains(hPflanze))
            {
                Global.ContextZooBot.Delete<Held_Pflanze>(hPflanze);
                Global.SelectedHeld.Held_Pflanze.Remove(hPflanze);
                Refresh();
            }
        }


        private Base.CommandBase _onAddBekanntePflanzeClick = null;
        public Base.CommandBase OnAddBekanntePflanzeClick
        {
            get
            {
                if (_onAddBekanntePflanzeClick == null)
                    _onAddBekanntePflanzeClick = new Base.CommandBase(AddBekanntePflanzeClick, null);
                return _onAddBekanntePflanzeClick;
            }
        }
        void AddBekanntePflanzeClick(object obj)
        {
            if (Global.SelectedHeld.Held_Pflanze.Where(t => t.Pflanze.PflanzeGUID == PflanzeAuswahl.PflanzeGUID).ToList().Count > 0)
                return;
            Held_Pflanze hPflanze = new Held_Pflanze();
            hPflanze.HeldGUID = Global.SelectedHeld.HeldGUID;
            hPflanze.ID = Guid.NewGuid();
            hPflanze.PflanzeGUID = PflanzeAuswahl.PflanzeGUID;
            hPflanze.Bekannt = true;
            if (Global.ContextZooBot.Insert<Held_Pflanze>(hPflanze))
            {
                PflanzeAuswahl = null;
                OnChanged("BekannteHeldenPflanzen");
            }
            else
            {
                ViewHelper.ShowError("Datenbankfehler" + Environment.NewLine + "Beim Erstellen einer neuen bekannten Pflanze ist ein Fehler aufgetreten.",null);
            }
            
        }

        #endregion  
    }
}
