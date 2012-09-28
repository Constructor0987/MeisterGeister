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
    public class SchmiedeRüstungViewModel : Base.ViewModelBase
    {

        #region //---- FELDER ----

        //Intern: Zeichenketten
        const string RÜSTUNGGRUPPEEXOTISCH = "Exotische Materialien";
        const string RÜSTUNGGRUPPEKLEIDUNG = "Kleidung";
        const string RÜSTUNGGRUPPELEDER = "Lederrüstungen";
        const string RÜSTUNGGRUPPEPLATTE = "Plattenrüstungen";
        const string RÜSTUNGGRUPPEKETTESCHUPPE = "Kette/Schuppe";

        //Felder
        private int _probePunkte;
        private int _probeDauerInZe;
        private int _probeErschwernis;
        private int _tawSchmied;
        private int _tawSchmiedMod;
        private int _probeDauerNApprox;
        
        //Listen + SelectedItems
        private Model.Rüstung _selectedRüstung;
        private List<Model.Rüstung> _rüstungListe = new List<Model.Rüstung>();

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

        public int ProbeErschwernis
        {
            get { return _probeErschwernis; }
            private set
            {
                _probeErschwernis = value;
                OnChanged("ProbeErschwernis");
            }
        }

        public int ProbeDauerInZe
        {
            get { return _probeDauerInZe; }
            private set
            {
                _probeDauerInZe = value;
                OnChanged("ProbeDauerInZe");
            }
        }

        public int ProbeDauerNApprox
        {
            get { return _probeDauerNApprox; }
            private set
            {
                _probeDauerNApprox = value;
                OnChanged("ProbeDauerNApprox");
            }
        }

        public int TawSchmied
        {
            get { return _tawSchmied; }
            set
            {
                if (value < 0) value = 0;
                if (value == _tawSchmied) return;
                _tawSchmied = value;
                OnChanged("TawSchmied");
                BerechneNicwinscheApproximation();
            }
        }

        public int TawSchmiedMod
        {
            get { return _tawSchmiedMod; }
            set
            {
                if (value < -7)
                    value = -7;
                else if (value > 7)
                    value = 7;
                if (value == _tawSchmiedMod) return;
                _tawSchmiedMod = value;
                OnChanged("TawSchmiedMod");
                BerechneNicwinscheApproximation();
            }
        }

        //Auswahl der Listen
        public Model.Rüstung SelectedRüstung
        {
            get { return _selectedRüstung; }
            set
            {
                if (value == null) return;
                _selectedRüstung = value;
                OnChanged("SelectedRüstung");
                BerechneRüstung();
            }
        }

        //Listen
        public List<Model.Rüstung> RüstungListe
        {
            get { return _rüstungListe; }
            set
            {
                _rüstungListe = value;
                OnChanged("RüstungListe");
            }
        }

        //Commands

        #endregion

        #region //---- KONSTRUKTOR ----

        public SchmiedeRüstungViewModel()
        {
            TawSchmied = 12;
        }

        #endregion

        #region //---- INSTANZMETHODEN ----

        public void LoadDaten()
        {
            RüstungListe = Global.ContextRüstung.RüstungListe;
            OnChanged("RüstungListe");
        }

        private void BerechneNicwinscheApproximation()
        {
            int tapStern = TawSchmied - TawSchmiedMod;
            if (tapStern > TawSchmied) tapStern = TawSchmied;
            tapStern /= 2;
            if (tapStern < 1) tapStern = 1;
            tapStern = ProbePunkte * 2 / tapStern;
            ProbeDauerNApprox = (tapStern > 0) ? tapStern : 1;
        }

        private void BerechneRüstung()
        {
            if (_selectedRüstung == null) return;
            ProbePunkte = (int)Math.Round((_selectedRüstung.gRS.HasValue ? _selectedRüstung.gRS.Value : 0)* 7,0);
            ProbeErschwernis = 0;
            ProbeDauerInZe = 6;
            switch (_selectedRüstung.Gruppe)
            {
                case RÜSTUNGGRUPPEEXOTISCH :
                    ProbeErschwernis = 7;
                    break;
                case RÜSTUNGGRUPPEKETTESCHUPPE :
                    ProbeDauerInZe = 16;
                    break;
                case RÜSTUNGGRUPPEPLATTE:
                    ProbeDauerInZe = 8;
                    break;
            }
            BerechneNicwinscheApproximation();
        }
        #endregion

        #region //---- EVENTS ----

        #endregion

    }
}
