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
using MeisterGeister.View.ZooBot;
using System.Windows.Forms;
using MeisterGeister.Logic.Kalender;
using MeisterGeister.ViewModel.ZooBot.Logic;
using System.Windows.Data;
using System.Globalization;

using MeisterGeister.ViewModel.ZooBot;
using MeisterGeister.View.General;

namespace MeisterGeister.ViewModel.ZooBot
{

    public class BekanntePflanzenVM : Base.ViewModelBase
    {
        public Action CloseAction { get; set; }

        // Listen
        private List<Model.Pflanze> _pflanzenImGebiet = new List<Model.Pflanze>();
        private List<Model.Pflanze> _pflanzenImGebietUndLandschaft = new List<Model.Pflanze>();


        private ZooBotViewModel _zooBotVM;
        public ZooBotViewModel ZooBotVM
        {
            get { return _zooBotVM; }
            set
            {
                _zooBotVM = value;
                OnChanged();
                if (value != null)
                {
                    InitUnbekanntePflanzenListe();
                    OnChanged("BekannteHeldenPflanzen");
                }
            }
        }     
           
        public List<Held_Pflanze> BekannteHeldenPflanzen
        {
            get { return ZooBotVM.SelectedHeld.Held_Pflanze.OrderBy(t => t.Pflanze.Name).ToList(); }
        }

        private List<Pflanze> _unbekanntePflanzenListe;
        public List<Pflanze> UnbekanntePflanzenListe
        {
            get { return _unbekanntePflanzenListe; }
            set
            {
                _unbekanntePflanzenListe = value;
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
                OnChanged();
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
                OnChanged();
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

        public void Init()
        {
            if (IsLoaded == false)
            {
                Refresh();
                IsLoaded = true;
            }
        }

        public void InitUnbekanntePflanzenListe()
        {
            List<Model.Pflanze> uListe = new List<Model.Pflanze>();
            
            uListe = new List<Pflanze>();
            uListe = ZooBotVM.PflanzenListe.ToList();
            if (ZooBotVM.SelectedHeld != null)
                foreach (Held_Pflanze bPflanze in ZooBotVM.SelectedHeld.Held_Pflanze)
                {
                    uListe.Remove(bPflanze.Pflanze);
                }
            UnbekanntePflanzenListe = uListe;
        }
        
        public void Refresh()
        {
            OnChanged("BekannteHeldenPflanzen");
        }        

        #endregion

        #region //---- EVENTS ----
         
        
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
            Held_Pflanze hPflanze = new Held_Pflanze();
            hPflanze.HeldGUID = ZooBotVM.SelectedHeld.HeldGUID;
            hPflanze.ID = Guid.NewGuid();
            hPflanze.PflanzeGUID = PflanzeAuswahl.PflanzeGUID;
            hPflanze.Bekannt = true;
            if (Global.ContextZooBot.Insert<Held_Pflanze>(hPflanze))
            {
                UnbekanntePflanzenListe.Remove(PflanzeAuswahl);
                PflanzeAuswahl = null;
                OnChanged("BekannteHeldenPflanzen");
            }
            else
            {
                ViewHelper.ShowError("Datenbankfehler" + Environment.NewLine + "Beim Erstellen einer neuen bekannten Pflanze ist ein Fehler aufgetreten.",null);
            }
            
        }
        
        //private Base.CommandBase _onBtnBekanntePflanzeEntfernenClick = null;
        //public Base.CommandBase OnBtnBekanntePflanzeEntfernenClick
        //{
        //    get
        //    {
        //        if (_onBtnBekanntePflanzeEntfernenClick == null)
        //            _onBtnBekanntePflanzeEntfernenClick = new Base.CommandBase(BtnBekanntePflanzeEntfernenClick, null);
        //        return _onBtnBekanntePflanzeEntfernenClick;
        //    }
        //}
        //void BtnBekanntePflanzeEntfernenClick(object obj)
        //{
        //    if (((System.Windows.Controls.Button)obj).Tag != null) 
        //    {}
        //    //SelectedHeld.Held_Pflanze.Remove( )
        //    //Held_Pflanze hPflanze = ((obj)Button).Tag as Guid;
        //}    

        #endregion  
    }
}
