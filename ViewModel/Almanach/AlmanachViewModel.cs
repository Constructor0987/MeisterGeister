using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Eigene usings
using MeisterGeister.ViewModel.Almanach.Logic;
using Base = MeisterGeister.ViewModel.Base;

namespace MeisterGeister.ViewModel.Almanach
{
    public class AlmanachViewModel : Base.ToolViewModelBase
    {
         #region //---- FELDER ----

        // Felder
        private string _suchText = string.Empty;

        // Listen
        private List<Model.Handelsgut> _handelsgutListe;
        private List<Model.Waffe> _waffeListe;
        private List<Model.Fernkampfwaffe> _fernkampfwaffeListe;
        private List<Model.Schild> _schildListe;
        private List<Model.Rüstung> _rüstungListe;
        private List<AlmanachItem> _almanachItemListe;
        private List<AlmanachItem> _filteredAlmanachItemListe;

        //Commands

        #endregion

        #region //---- EIGENSCHAFTEN ----

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

        #region //---- LISTEN ----

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

        public List<AlmanachItem> AlmanachItemListe
        {
            get { return _almanachItemListe; }
            set
            {
                _almanachItemListe = value;
                OnChanged("AlmanachItemListe");
            }
        }

        public List<AlmanachItem> FilteredAlmanachItemListe
        {
            get { return _filteredAlmanachItemListe; }
            set
            {
                _filteredAlmanachItemListe = value;
                OnChanged("FilteredAlmanachItemListe");
            }
        }

        #endregion

        //Commands

        #endregion

        #region //---- KONSTRUKTOR ----

        public AlmanachViewModel()
        {
            Init();
        }

        #endregion

        #region //---- INSTANZMETHODEN ----

        public void Refresh()
        {
            
        }

        public void Init()
        {
            HandelsgutListe = Global.ContextHandelsgut == null ? new List<Model.Handelsgut>() : Global.ContextHandelsgut.HandelsgüterListe;
            WaffeListe = Global.ContextInventar == null ? new List<Model.Waffe>() : Global.ContextInventar.WaffeListe;
            FernkampfwaffeListe = Global.ContextInventar == null ? new List<Model.Fernkampfwaffe>() : Global.ContextInventar.FernkampfwaffeListe;
            SchildListe = Global.ContextInventar == null ? new List<Model.Schild>() : Global.ContextInventar.SchildListe;
            RüstungListe = Global.ContextInventar == null ? new List<Model.Rüstung>() : Global.ContextInventar.RuestungListe;
            var TalentListe = Global.ContextTalent == null ? new List<Model.Talent>() : Global.ContextTalent.TalentListe;
            var ZauberListe = Global.ContextZauber == null ? new List<Model.Zauber>() : Global.ContextZauber.ZauberListe;
            var SonderfertigkeitListe = Global.ContextHeld == null ? new List<Model.Sonderfertigkeit>() : Global.ContextHeld.SonderfertigkeitListe;
            var VorNachteilListe = Global.ContextVorNachteil == null ? new List<Model.VorNachteil>() : Global.ContextVorNachteil.VorNachteilListe;

            // Globale Listen der unterschiedlichen Handelsgütern in eine Gesamt-Liste zusammenführen
            List<AlmanachItem> itemList = new List<AlmanachItem>();

            // Handelsgüter einfügen
            foreach (var item in HandelsgutListe)
                itemList.Add(new AlmanachItem(item, item.Kategorie));

            // Waffen einfügen
            foreach (var item in WaffeListe)
                itemList.Add(new AlmanachItem(item, item.Kategorie));

            // Fernkampfwaffen einfügen
            foreach (var item in FernkampfwaffeListe)
                itemList.Add(new AlmanachItem(item, item.Kategorie));

            // Schilde einfügen
            foreach (var item in SchildListe)
                itemList.Add(new AlmanachItem(item, item.Kategorie));

            // Rüstungen einfügen
            foreach (var item in RüstungListe)
                itemList.Add(new AlmanachItem(item, item.Kategorie));

            // Talente einfügen
            foreach (var item in TalentListe)
                itemList.Add(new AlmanachItem(item, item.Talentgruppe.Gruppenname));

            // Zauber einfügen
            foreach (var item in ZauberListe)
                itemList.Add(new AlmanachItem(item, "Zauber"));

            // Sonderfertigkeiten einfügen
            foreach (var item in SonderfertigkeitListe)
                itemList.Add(new AlmanachItem(item, "Sonderfertigkeit"));

            // VorNachteile einfügen
            foreach (var item in VorNachteilListe)
                itemList.Add(new AlmanachItem(item, item.Typ));

            AlmanachItemListe = itemList;

            Refresh();

            FilterListe();
        }

        /// <summary>
        /// Filtert die BasarItem-Liste auf Basis des SuchTextes.
        /// </summary>
        private void FilterListe()
        {
            string suchText = _suchText.ToLower().Trim();
            string[] suchWorte = suchText.Split(' ');

            if (suchText == string.Empty) // kein Suchwort
                FilteredAlmanachItemListe = AlmanachItemListe.AsParallel().OrderBy(n => n.Name).ToList();
            else if (suchWorte.Length == 1) // nur ein Suchwort
                FilteredAlmanachItemListe = AlmanachItemListe.AsParallel().Where(s => s.Contains(suchWorte[0])).OrderBy(n => n.Name).ToList();
            else // mehrere Suchwörter
                FilteredAlmanachItemListe = AlmanachItemListe.AsParallel().Where(s => s.Contains(suchWorte)).OrderBy(n => n.Name).ToList();
        }

        #endregion

        #region //---- EVENTS ----

        #endregion
    }
}
