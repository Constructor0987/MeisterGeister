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
        const string RÜSTUNGGRUPPEHERVORRAGENDEKETTE = "Hervorragende Kette";
        const string RÜSTUNGGRUPPEKETTE = "Kette";
        const string RÜSTUNGGRUPPESCHUPPE = "Schuppe";
        const string FILTERDEAKTIVIEREN = "Alle";

        //Felder
        private int _probePunkte;
        private int _probeDauerInZe;
        private int _probeErschwernis;
        private int _tawSchmied;
        private int _tawSchmiedMod;
        private int _probeDauerNApprox;
        
        //Listen + SelectedItems
        private Model.Rüstung _selectedRüstung;
        private String _selectedRüstungTyp;
        private List<Model.Rüstung> _rüstungListe = new List<Model.Rüstung>();
        private List<String> _rüstungTypenListe = new List<string>();

        #endregion

        #region //---- COMMANDS ----
        private Base.CommandBase onAddZuNotizen = null;
        public Base.CommandBase OnAddZuNotizen
        {
            get
            {
                if (onAddZuNotizen == null)
                    onAddZuNotizen = new Base.CommandBase(AddZuNotizen, null);
                return onAddZuNotizen;
            }
        }
        private void AddZuNotizen(object sender)
        {
            if (_selectedRüstung != null)
            {
                string fromschmiede = "\n--------- " + MeisterGeister.Logic.Kalender.Datum.Aktuell.ToStringShort() + "---------\n";
                fromschmiede += _selectedRüstung.ToString("l");
                Global.ContextNotizen.NotizAllgemein.AppendText(fromschmiede);
            }
        }
        #endregion

        #region //---- EIGENSCHAFTEN ----
        public string Icon { get; protected set; }
        public string Name { get; protected set; }
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

        public String SelectedRüstungTyp
        {
            get { return _selectedRüstungTyp; }
            set
            {
                _selectedRüstungTyp = value;
                if (value != FILTERDEAKTIVIEREN)
                {
                    RüstungListe = Global.ContextInventar.RuestungListe.Where(r => (r.Gruppe == null ? string.Empty : r.Gruppe).Contains(value)).ToList();
                }
                else
                {
                    RüstungListe = Global.ContextInventar.RuestungListe.ToList();
                }
                OnChanged("SelectedRüstungTyp");
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

        public List<String> RüstungTypenListe
        {
            get { return _rüstungTypenListe; }
            set
            {
                _rüstungTypenListe = value;
                OnChanged("RüstungTypenListe");
            }
        }

        //Commands

        #endregion

        #region //---- KONSTRUKTOR ----

        public SchmiedeRüstungViewModel()
        {
            this.Name = "Rüstung";
            this.Icon = "/DSA%20MeisterGeister;component/Images/Icons/ruestung.png";
            Init();
            TawSchmied = 12;
        }

        #endregion

        #region //---- INSTANZMETHODEN ----

        private void Init()
        {
            RüstungTypenListe.Add(FILTERDEAKTIVIEREN);
            if (Global.ContextInventar != null)
            {
                RüstungListe.AddRange(Global.ContextInventar.RuestungListe.Where(w => !RüstungListe.Contains(w)).OrderBy(w => w.Name));
                RüstungTypenListe.AddRange(Global.ContextInventar.RuestungListe.Where(r => !RüstungTypenListe.Contains(r.Gruppe)).Select(r => r.Gruppe));
            }
            OnChanged("RüstungListe");
            OnChanged("RüstungTypenListe");
        }

        public void Refresh()
        {
            // derzeit nichts beim erneuten Anzeigen der Tabs erforderlich
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
            if (SelectedRüstung.HervorragendeKette())
            {
                ProbeErschwernis = 5;
                ProbePunkte *= 2;
            }
            ProbeDauerInZe = 6;
            switch (SelectedRüstung.Gruppe)
            {
                case RÜSTUNGGRUPPEEXOTISCH :
                    ProbeErschwernis = 7;
                    break;
                case RÜSTUNGGRUPPESCHUPPE :
                    ProbeDauerInZe = 16;
                    break;
                case RÜSTUNGGRUPPEKETTE :
                    ProbeDauerInZe = 16;
                    break;
                case RÜSTUNGGRUPPEHERVORRAGENDEKETTE :
                    ProbeDauerInZe = 24;
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
