using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Windows;
//Eigene usings
using Base = MeisterGeister.ViewModel.Base;
using Model = MeisterGeister.Model;
using Service = MeisterGeister.Model.Service;
using MeisterGeister.ViewModel.Schmiede.Logic;

namespace MeisterGeister.ViewModel.Schmiede
{
    public class SchmiedeGeschossViewModel : Base.ViewModelBase
    {

        #region //---- FELDER ----

        //Intern: Zeichenketten für DB-Abfragen
        const string TALENTFERNKAMPFWAFFEUNTERKATEGORIE = "Fernkampf";
        const string MUNITIONSTYPPFEIL = "Pfeil";
        const string MUNITIONSTYPBOLZEN = "Bolzen";
        const string TALENTFERNKAMPFBOGEN = "Bogen";
        const string TALENTFERNKAMPFARMBRUST = "Armbrust";
        const string FILTERDEAKTIVIEREN = "Alle";

        //Felder
        private int _anzahl;

        private int _probePunkte;
        private int _probeErschwernisSpitze;
        private int _probeErschwernis;

        private int _tawSchmied;
        private int _tawSchmiedMod;
        private int _probeDauerNApprox;

        private int _tawSchmiedSpitze;
        private int _tawSchmiedModSpitze;
        private int _probeDauerNApproxSpitze;
        
        //Listen + SelectedItems
        private Model.Fernkampfwaffe _selectedFernkampfwaffe;
        private List<Model.Fernkampfwaffe> _fernkampfwaffeListe = new List<Model.Fernkampfwaffe>();
        private Model.Talent _selectedFernkampfwaffeTalent;
        private List<Model.Talent> _fernkampfwaffeTalentListe = new List<Model.Talent>();
        private Model.Munition _selectedMunition;
        private List<Model.Munition> _munitionListe = new List<Model.Munition>();

        #endregion

        #region //---- EIGENSCHAFTEN ----

        //Felder        

        public int Anzahl
        {
            get { return _anzahl; }
            private set
            {
                if (value < 1)
                    value = 1;
                if (value == _anzahl) return;
                _anzahl = value;
                OnChanged("Anzahl");
            }
        }

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

        public int ProbeErschwernisSpitze
        {
            get { return _probeErschwernisSpitze; }
            private set
            {
                _probeErschwernisSpitze = value;
                OnChanged("ProbeErschwernisSpitze");
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

        public int ProbeDauerNApproxSpitze
        {
            get { return _probeDauerNApproxSpitze; }
            private set
            {
                _probeDauerNApproxSpitze = value;
                OnChanged("ProbeDauerNApproxSpitze");
            }
        }

        public int TawSchmiedSpitze
        {
            get { return _tawSchmiedSpitze; }
            set
            {
                if (value < 0) value = 0;
                if (value == _tawSchmiedSpitze) return;
                _tawSchmiedSpitze = value;
                OnChanged("TawSchmiedSpitze");
                BerechneNicwinscheApproximation();
            }
        }

        public int TawSchmiedModSpitze
        {
            get { return _tawSchmiedModSpitze; }
            set
            {
                if (value < -7)
                    value = -7;
                else if (value > 7)
                    value = 7;
                if (value == _tawSchmiedModSpitze) return;
                _tawSchmiedModSpitze = value;
                OnChanged("TawSchmiedModSpitze");
                BerechneNicwinscheApproximation();
            }
        }
        
        //Auswahl der Listen

        public Model.Fernkampfwaffe SelectedFernkampfwaffe
        {
            get { return _selectedFernkampfwaffe; }
            set
            {
                if (value == null) return;
                _selectedFernkampfwaffe = value;
                OnChanged("SelectedFernkampfwaffe");
                //BerechneGeschoss();
                //SelectedMunition = MunitionListe.First();
            }
        }

        public Model.Talent SelectedFernkampfwaffeTalent
        {
            get { return _selectedFernkampfwaffeTalent; }
            set
            {
                if (value == null) return;
                _selectedFernkampfwaffeTalent = value;
                if (value.Talentname != FILTERDEAKTIVIEREN)
                {
                    FernkampfwaffeListe = Global.ContextInventar.FernkampfwaffeListe.Where(w => (w.Munitionsart == MUNITIONSTYPBOLZEN || w.Munitionsart == MUNITIONSTYPPFEIL) && w.Talent.Contains(value)).OrderBy(w => w.Name).ToList();
                }
                else
                {
                    FernkampfwaffeListe = Global.ContextInventar.FernkampfwaffeListe.ToList();
                }
                OnChanged("SelectedFernkampfwaffeTalent");
            }
        }

        public Model.Munition SelectedMunition
        {
            get { return _selectedMunition; }
            set
            {
                if (value == null) return;
                _selectedMunition = value;
                OnChanged("SelectedMunition");
                //BerechneGeschoss();
            }
        }

        //Listen
        public List<Model.Fernkampfwaffe> FernkampfwaffeListe
        {
            get { return _fernkampfwaffeListe; }
            set
            {
                _fernkampfwaffeListe = value;
                OnChanged("FernkampfwaffeListe");
            }
        }

        public List<Model.Talent> FernkampfwaffeTalentListe
        {
            get { return _fernkampfwaffeTalentListe; }
            set
            {
                _fernkampfwaffeTalentListe = value;
                OnChanged("FernkampfwaffeTalentListe");
            }
        }

        public List<Model.Munition> MunitionListe
        {
            get { return _munitionListe; }
            set
            {
                _munitionListe = value;
                OnChanged("MunitionListe");
            }
        }

        //Commands

        #endregion

        #region //---- KONSTRUKTOR ----

        public SchmiedeGeschossViewModel()
        {
            TawSchmied = 12;
            TawSchmiedSpitze = 12;
        }

        #endregion

        #region //---- INSTANZMETHODEN ----

        public void LoadDaten()
        {
            // TODO FK: Belagerungswaffen rausnehmen
            FernkampfwaffeTalentListe.Add(new Model.Talent() { Talentname = FILTERDEAKTIVIEREN });
            FernkampfwaffeTalentListe.AddRange(Global.ContextTalent.TalentListe.Where(t => t.TalentgruppeID == 1 && t.Untergruppe == TALENTFERNKAMPFWAFFEUNTERKATEGORIE && (t.Talentname == TALENTFERNKAMPFBOGEN || t.Talentname == TALENTFERNKAMPFARMBRUST) && !FernkampfwaffeTalentListe.Contains(t)).OrderBy(t => t.Talentname));
            OnChanged("FernkampfwaffeTalentListe");
            FernkampfwaffeListe.AddRange(Global.ContextInventar.FernkampfwaffeListe.Where(w => (w.Munitionsart == MUNITIONSTYPBOLZEN || w.Munitionsart == MUNITIONSTYPPFEIL) &&  !FernkampfwaffeListe.Contains(w)).OrderBy(w => w.Name));
            OnChanged("FernkampfwaffeListe");
            OnChanged("MunitionListe");
        }

        private void BerechneNicwinscheApproximation()
        {
            // Pfeil
            int tapStern = TawSchmied - TawSchmiedMod - ProbeErschwernis;
            if (tapStern > TawSchmied) tapStern = TawSchmied;
            tapStern /= 2;
            if (tapStern < 1) tapStern = 1;
            tapStern = (ProbePunkte / (8 * tapStern));
            ProbeDauerNApprox = (tapStern > 0) ? tapStern : 1; 
            // Spitze
            tapStern = TawSchmiedSpitze - TawSchmiedModSpitze - ProbeErschwernisSpitze;
            if (tapStern > TawSchmiedSpitze) tapStern = TawSchmiedSpitze;
            tapStern /= 2;
            if (tapStern < 1) tapStern = 1;
            tapStern = (ProbePunkte / (8 * tapStern));
            ProbeDauerNApproxSpitze = (tapStern > 0) ? tapStern : 1; 
        }

        private void BerechneGeschoss()
        {
            ProbeErschwernisSpitze = 0;
            ProbeErschwernis = 0;
            if (_selectedMunition == null || _selectedFernkampfwaffe == null) return;
            ProbePunkte = (int)Math.Round(((_selectedFernkampfwaffe.TPWürfelAnzahl.HasValue ? _selectedFernkampfwaffe.TPWürfelAnzahl.Value : 0) * (int)Math.Round(((_selectedFernkampfwaffe.TPWürfel.HasValue ? _selectedFernkampfwaffe.TPWürfel.Value : 0) + 1) / 2.0) + (_selectedFernkampfwaffe.TPBonus.HasValue ? _selectedFernkampfwaffe.TPBonus.Value : 0))/3.0);
        }
        #endregion

        #region //---- EVENTS ----

        #endregion

    }
}
