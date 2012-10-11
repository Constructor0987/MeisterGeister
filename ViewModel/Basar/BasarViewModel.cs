using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//Eigene usings
using MeisterGeister.ViewModel.Basar.Logic;
using Base = MeisterGeister.ViewModel.Base;
using Model = MeisterGeister.Model;
using Service = MeisterGeister.Model.Service;

namespace MeisterGeister.ViewModel.Basar
{
    public class BasarViewModel : Base.ViewModelBase
    {
        #region //---- FELDER ----

        // Felder
        private double _rabattAufschlag = 0.0;
        private double _anzahl = 1.0;
        private string _suchText = string.Empty;

        private Model.Held _selectedHeld;

        // Listen
        private List<Model.Handelsgut> _handelsgutListe;
        private List<Model.Waffe> _waffeListe;
        private List<Model.Fernkampfwaffe> _fernkampfwaffeListe;
        private List<Model.Schild> _schildListe;
        private List<Model.Rüstung> _rüstungListe;
        private List<BasarItem> _basarItemListe;
        private List<BasarItem> _filteredBasarItemListe;

        //Commands
        private Base.CommandBase _onGoToBugForum;

        #endregion

        #region //---- EIGENSCHAFTEN ----

        public double RabattAufschlag
        {
            get { return _rabattAufschlag; }
            set
            {
                _rabattAufschlag = value;
                OnChanged("RabattAufschlag");

                // Änderung an BasarItems weiterreichen
                foreach (var item in BasarItemListe)
                    item.RabattAufschlag = _rabattAufschlag;
            }
        }

        public double Anzahl
        {
            get { return _anzahl; }
            set
            {
                if (_anzahl == value || value < 0.0)
                    return;
                _anzahl = value;
                OnChanged("Anzahl");

                // Änderung an BasarItems weiterreichen
                foreach (var item in BasarItemListe)
                    item.Anzahl = _anzahl;
            }
        }

        public string SuchText
        {
            get { return _suchText; }
            set
            {
                _suchText = value;
                OnChanged("SuchText");
                FilterListe();
            }
        }

        public Model.Held SelectedHeld
        {
            get { return _selectedHeld; }
            set
            {
                _selectedHeld = value;
                OnChanged("SelectedHeld");
                OnChanged("HeldTalentwerte");
            }
        }

        public string HeldTalentwerte
        {
            get
            {
                string tawSchätzen = "-"; string tawHandel = "-"; string tawFeilschen = "-";
                if (SelectedHeld != null)
                {
                    if (SelectedHeld.HatTalent("Schätzen"))
                        tawSchätzen = SelectedHeld.Talentwert("Schätzen").ToString();
                    if (SelectedHeld.HatTalent("Handel"))
                        tawHandel = SelectedHeld.Talentwert("Handel").ToString();
                    if (SelectedHeld.HatTalent("Überreden"))
                        tawFeilschen = SelectedHeld.Talentwert("Überreden").ToString();
                }
                return string.Format("Schätzen {0}, Handel {1}, Überreden (Feilschen) {2}", tawSchätzen, tawHandel, tawFeilschen);
            }
        }

        #region //---- LISTEN ----

        public List<Model.Held> HeldListe
        {
            get { return Global.ContextHeld.HeldenGruppeListe; }
        }

        public List<Model.Handelsgut> HandelsgutListe
        {
            get { return _handelsgutListe; }
            set
            {
                _handelsgutListe = value;
                OnChanged("HandelsgutListe");
            }
        }

        public List<Model.Waffe> WaffeListe
        {
            get { return _waffeListe; }
            set
            {
                _waffeListe = value;
                OnChanged("WaffeListe");
            }
        }

        public List<Model.Fernkampfwaffe> FernkampfwaffeListe
        {
            get { return _fernkampfwaffeListe; }
            set
            {
                _fernkampfwaffeListe = value;
                OnChanged("FernkampfwaffeListe");
            }
        }

        public List<Model.Schild> SchildListe
        {
            get { return _schildListe; }
            set
            {
                _schildListe = value;
                OnChanged("SchildListe");
            }
        }

        public List<Model.Rüstung> RüstungListe
        {
            get { return _rüstungListe; }
            set
            {
                _rüstungListe = value;
                OnChanged("RüstungListe");
            }
        }

        public List<BasarItem> BasarItemListe
        {
            get { return _basarItemListe; }
            set
            {
                _basarItemListe = value;
                OnChanged("BasarItemListe");
            }
        }

        public List<BasarItem> FilteredBasarItemListe
        {
            get { return _filteredBasarItemListe; }
            set
            {
                _filteredBasarItemListe = value;
                OnChanged("FilteredBasarItemListe");
            }
        }

        #endregion

        //Commands
        public Base.CommandBase OnGoToBugForum
        {
            get { return _onGoToBugForum; }
        }

        #endregion

        #region //---- KONSTRUKTOR ----

        public BasarViewModel()
        {
            _onGoToBugForum = new Base.CommandBase(GoToBugForum, null);

            Global.HeldSelectionChanged += (s, ev) => { SelectedHeldChanged(); };

            Init();
        }

        #endregion

        #region //---- INSTANZMETHODEN ----

        public void Refresh()
        {
            OnChanged("HeldListe");
        }

        public void Init()
        {
            HandelsgutListe = Global.ContextHandelsgut == null ? new List<Model.Handelsgut>() : Global.ContextHandelsgut.HandelsgüterListe;
            WaffeListe = Global.ContextWaffe == null ? new List<Model.Waffe>() : Global.ContextWaffe.WaffeListe;
            FernkampfwaffeListe = Global.ContextFernkampfwaffe == null ? new List<Model.Fernkampfwaffe>() : Global.ContextFernkampfwaffe.FernkampfwaffeListe;
            SchildListe = Global.ContextSchild == null ? new List<Model.Schild>() : Global.ContextSchild.SchildListe;
            RüstungListe = Global.ContextRüstung == null ? new List<Model.Rüstung>() : Global.ContextRüstung.RüstungListe;

            // Globale Listen der unterschiedlichen Handelsgütern in eine Gesamt-Liste zusammenführen
            List<BasarItem> itemList = new List<BasarItem>();

            // Handelsgüter einfügen
            foreach (var item in HandelsgutListe)
                itemList.Add(new BasarItem() { Item = item });

            // Waffen einfügen
            foreach (var item in WaffeListe)
                itemList.Add(new BasarItem() { Item = item });

            // Fernkampfwaffen einfügen
            foreach (var item in FernkampfwaffeListe)
                itemList.Add(new BasarItem() { Item = item });

            // Schilde einfügen
            foreach (var item in SchildListe)
                itemList.Add(new BasarItem() { Item = item });

            // Rüstungen einfügen
            foreach (var item in RüstungListe)
                itemList.Add(new BasarItem() { Item = item });

            BasarItemListe = itemList;
            FilteredBasarItemListe = BasarItemListe.OrderBy(n => n.Name).ToList();

            Refresh();
        }

        /// <summary>
        /// Filtert die BasarItem-Liste auf Basis des SuchTextes.
        /// </summary>
        private void FilterListe()
        {
            string suchText = _suchText.ToLower().Trim();
            string[] suchWorte = suchText.Split(' ');

            if (suchText == string.Empty) // kein Suchwort
                FilteredBasarItemListe = BasarItemListe.OrderBy(n => n.Name).ToList();
            else if (suchWorte.Length == 1) // nur ein Suchwort
                FilteredBasarItemListe = BasarItemListe.AsParallel().Where(s => s.Contains(suchWorte[0])).OrderBy(n => n.Name).ToList();
            else // mehrere Suchwörter
                FilteredBasarItemListe = BasarItemListe.AsParallel().Where(s => s.Contains(suchWorte)).OrderBy(n => n.Name).ToList();
        }

        #endregion

        #region //---- EVENTS ----

        void SelectedHeldChanged()
        {
            SelectedHeld = Global.SelectedHeld;
        }

        void GoToBugForum(object sender)
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo("http://meistergeister.siteboard.org/f14t209-fehler-in-den-basar-daten.html"));
        }

        #endregion

    }
}