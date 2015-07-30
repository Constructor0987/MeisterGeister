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
    public class cGebietListeAktiv
    {
        private Gebiet _geb = new Gebiet();
        public Gebiet geb
        {
            get { return _geb; }
            set
            {
                _geb = value;
            }
        }
        
        private bool _aktiv = false;
        public bool Aktiv 
        {
            get { return _aktiv; }
            set
            {
                _aktiv = value;
            }
        }

    }

    public class GebietAuswahlVM : Base.ViewModelBase
    {
        public Action CloseAction { get; set; }

        private List<cGebietListeAktiv> _listGebietListeAktiv = new List<cGebietListeAktiv>();
        public List<cGebietListeAktiv> ListGebietListeAktiv
        {
            get { return _listGebietListeAktiv; }
            set
            {
                _listGebietListeAktiv = value;
                OnChanged();
            }
        }

        // Listen


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
                    List<cGebietListeAktiv> gAktivList = new List<cGebietListeAktiv>(); 
                    ZooBotVM.GebietListe.ForEach(delegate(Gebiet g)
                    {
                        cGebietListeAktiv gAktiv = new cGebietListeAktiv();
                        gAktiv.geb = g;
                        gAktivList.Add(gAktiv);
                    });
                    ListGebietListeAktiv = gAktivList;
                    OnChanged("BekannteHeldenPflanzen");
                }
            }
        }

        //private List<Gebiet> _gebieteAktivListe = new List<Gebiet>();
        //public List<Gebiet> GebieteAktivListe
        //{
        //    get { return _gebieteAktivListe; }
        //    set
        //    {
        //        _gebieteAktivListe = value;
        //        OnChanged();
        //    }
        //}


        //private List<cGebietListeAktiv> _gebietListeAktiv = new List<cGebietListeAktiv>();
        //public List<cGebietListeAktiv> GebietListeAktiv
        //{
        //    get { return _gebietListeAktiv; }
        //    set
        //    {
        //        _gebietListeAktiv = value;
        //        OnChanged();
        //    }
        //}
           
               
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

        public GebietAuswahlVM()
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
                
        public void Refresh()
        {
            //OnChanged("BekannteHeldenPflanzen");
        }        

        #endregion

        #region //---- EVENTS ----
            
        
        private Base.CommandBase _onBtnGebieteÜbernehmen = null;
        public Base.CommandBase OnBtnGebieteÜbernehmen
        {
            get
            {
                if (_onBtnGebieteÜbernehmen == null)
                    _onBtnGebieteÜbernehmen = new Base.CommandBase(BtnGebieteÜbernehmen, null);
                return _onBtnGebieteÜbernehmen;
            }
        }
        void BtnGebieteÜbernehmen(object obj)
        {
            List<Gebiet> gList = new List<Gebiet>();

            List<cGebietListeAktiv> lGebietAktiv = ListGebietListeAktiv.Where(t => t.Aktiv).ToList();

            foreach (cGebietListeAktiv gAktiv in lGebietAktiv)
                gList.Add(gAktiv.geb);

            ZooBotVM.GebieteSelected =  gList;
        }

        #endregion  
    }
}
