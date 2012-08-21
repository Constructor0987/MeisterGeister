using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
//Eigene usings
using Base = MeisterGeister.ViewModel.Base;
using Model = MeisterGeister.Model;
using Service = MeisterGeister.Model.Service;

namespace MeisterGeister.ViewModel.Schmiede
{
    class SchmiedeSchildViewModel : Base.ViewModelBase
    {

        #region //---- FELDER ----

        //Intern: Zeichenketten für DB-Abfragen
        const string UIDBUCKLER = "";
        const string UIDBUCKLERVOLLMETAL = "";
        // TODO FK: Buckler,.. über UIDS

        //Felder
        private int _probePunkte;
        
        //Listen + SelectedItems
        private Model.Schild _selectedSchild;
        private List<Model.Schild> _schildListe = new List<Model.Schild>();

        #endregion

        #region //---- EIGENSCHAFTEN ----

        //Felder        
        public int ProbePunkte
        {
            get { return _probePunkte; }
            private set
            {
                _probePunkte = value;
                OnChanged("ProbePunkte");
            }
        }

        //Auswahl der Listen
        public Model.Schild SelectedSchild
        {
            get { return _selectedSchild; }
            set
            {
                if (value == null) return;
                _selectedSchild = value;
                OnChanged("SelectedSchild");
                BerechneSchild();
            }
        }

        //Listen
        public List<Model.Schild> SchildListe
        {
            get { return _schildListe; }
            set
            {
                _schildListe = value;
                OnChanged("SchildListe");
            }
        }

        //Commands

        #endregion

        #region //---- KONSTRUKTOR ----

        public SchmiedeSchildViewModel()
        {

        }

        #endregion

        #region //---- INSTANZMETHODEN ----

        public void LoadDaten()
        {
            // Schilde - keine Parierwaffen
            SchildListe.AddRange(Global.ContextInventar.SchildListe.Where(w => (w.Typ == "S" || w.Name == "Buckler" || w.Name == "Großer (Vollmetall-) Buckler") && !SchildListe.Contains(w)).OrderBy(w => w.Name));
            SchildListe = SchildListe;
            OnChanged("SchildListe");
        }

        private void BerechneSchild()
        {
            if (_selectedSchild == null) return;
            ProbePunkte = _selectedSchild.WMPA * 3;
        }
        #endregion

        #region //---- EVENTS ----

        #endregion

    }
}
