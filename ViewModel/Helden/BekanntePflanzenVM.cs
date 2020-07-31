using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Windows.Controls;
//Eigene usings
using MeisterGeister.Model;
using MeisterGeister.View.General;
using MeisterGeister.Model.Service;
using MeisterGeister.Model.Extensions;

namespace MeisterGeister.ViewModel.Helden
{
    public class BekanntePflanzenVM : Base.ViewModelBase
    {
        public Action CloseAction { get; set; }

        // Listen
        private List<Model.Pflanze> _pflanzenImGebiet = new List<Model.Pflanze>();
        private List<Model.Pflanze> _pflanzenImGebietUndLandschaft = new List<Model.Pflanze>();


        private string _pflanzenFilter = null;
        public string PflanzenFilter
        {
            get { return _pflanzenFilter; }
            set {
                if (Set(ref _pflanzenFilter, value) && PflanzenListe != null)
                FilteredPflanzenListe = PflanzenListe.Where(t => !SelectedHeld.Held_Pflanze.Select(s => s.Pflanze).Contains(t)).ToList().
                        FindAll(z => z.Name.ToLower().Contains(value.ToLower()));                     
            }
        }
        
        public Model.Held SelectedHeld
        {
            get { return Global.SelectedHeld; }
            set
            {
                Global.SelectedHeld = value;
                OnChanged("SelectedHeld");
            }
        }

        public List<Held_Pflanze> BekannteHeldenPflanzen
        {
            get { return SelectedHeld?.Held_Pflanze.OrderBy(t => t.Pflanze.Name).ToList(); }
        }

        private List<Pflanze> _filteredPflanzenListe = new List<Pflanze>();
        [DependentProperty("SelectedHeld")]
        public List<Pflanze> FilteredPflanzenListe
        {
            get { return _filteredPflanzenListe; }
            set { Set(ref _filteredPflanzenListe, value); }
        }

        private List<Pflanze> _pflanzenListe;
        public List<Pflanze> PflanzenListe
        {
            get { return _pflanzenListe; }
            set { Set(ref _pflanzenListe, value); }
        }

        private Pflanze _pflanzeAuswahl;
        public Pflanze PflanzeAuswahl
        {
            get { return _pflanzeAuswahl; }
            set { Set(ref _pflanzeAuswahl, value); }
        }


        private bool _pflanzeBekannt = false;
        public bool PflanzeBekannt
        {
            get { return _pflanzeBekannt; }
            set { Set(ref _pflanzeBekannt, value); }
        }

        #region //---- FELDER ----


        #region //-Allgemein-
        //Booleans
        private bool IsLoaded;
        //Listen
        
        //Werte

        private bool _weltAventurien = true;
        public bool WeltAventurien
        {
            get { return _weltAventurien; }
            set
            {
                if (_weltAventurien != value)                    
                    _weltAventurien = value;
                if (_weltMyranor == value)
                    _weltMyranor = !value;
                OnChanged("WeltAventurien");
            }
        }

        private bool _weltMyranor = false;
        public bool WeltMyranor
        {
            get { return _weltMyranor; }
            set
            {
                if (_weltMyranor != value)
                    _weltMyranor = value;
                if (_weltAventurien == value)
                    _weltAventurien = !value;
                OnChanged("WeltMyranor");
            }
        }
        
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


        public override void RegisterEvents()
        {
            base.RegisterEvents();
            Global.HeldSelectionChanged += SelectedHeldChanged;
            SelectedHeldChanged(this, new EventArgs());
        }
        public override void UnregisterEvents()
        {
            base.UnregisterEvents();
            Global.HeldSelectionChanged -= SelectedHeldChanged;
        }

        public void NotifyRefresh()
        {
            OnChanged("SelectedHeld");
            OnChanged("BekannteHeldenPflanzen");
            PflanzenFilter = "";
            FilteredPflanzenListe = PflanzenListe.Where(t => !SelectedHeld.Held_Pflanze.Select(s => s.Pflanze).Contains(t)).ToList();
        }


        public void Init()
        {
            if (IsLoaded == false)
            {
                IsLoaded = true;
                PflanzenListe = Global.ContextZooBot.ZooBotPflanzenListe;
                FilteredPflanzenListe = PflanzenListe.Where(t => !SelectedHeld.Held_Pflanze.Select(s => s.Pflanze).Contains(t)).ToList();
            }
        }

        public void InitPflanzenListe()
        {
            List<Model.Pflanze> uListe = new List<Model.Pflanze>();
            
            PflanzenListe = PflanzenListe.OrderBy(t => t.Name).ToList();
            FilteredPflanzenListe = PflanzenListe;
        }

        #endregion

        #region //---- EVENTS ----
        
        private void SelectedHeldChanged(object sender, EventArgs e)
        {
            NotifyRefresh();
        }

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
            if (SelectedHeld.Held_Pflanze.Contains(hPflanze))
            {
                Global.ContextZooBot.Delete<Held_Pflanze>(hPflanze);
                SelectedHeld.Held_Pflanze.Remove(hPflanze);
                NotifyRefresh();
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
            if (PflanzeAuswahl == null || SelectedHeld.Held_Pflanze.Where(t => t.Pflanze.PflanzeGUID == PflanzeAuswahl.PflanzeGUID).ToList().Count > 0)
                return;
            Held_Pflanze hPflanze = new Held_Pflanze();
            hPflanze.HeldGUID = SelectedHeld.HeldGUID;
            hPflanze.ID = Guid.NewGuid();
            hPflanze.PflanzeGUID = PflanzeAuswahl.PflanzeGUID;
            hPflanze.Bekannt = true;
            if (Global.ContextZooBot.Insert<Held_Pflanze>(hPflanze))
            {
                //UnbekanntePflanzenListe.Remove(PflanzeAuswahl);
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
