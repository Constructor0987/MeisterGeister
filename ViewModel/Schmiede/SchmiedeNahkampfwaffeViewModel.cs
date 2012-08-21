using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
//Eigene usings
using Base = MeisterGeister.ViewModel.Base;
using Model = MeisterGeister.Model;
using Service = MeisterGeister.Model.Service;
using MeisterGeister.ViewModel.Schmiede.Logic;

namespace MeisterGeister.ViewModel.Schmiede
{
    class SchmiedeNahkampfwaffeViewModel : Base.ViewModelBase
    {

        #region //---- FELDER ----

        //Intern: Zeichenketten für DB-Abfragen
        const string TALENTNAHKAMPFWAFFEUNTERKATEGORIE = "Bewaffneter Nahkampf";
        const string TALENTNAHKAMPFWAFFEATTECHNIK = "Bewaffnete AT-Technik";
        const string TALENTNAHKAMPFTALENTRAUFEN = "Bewaffnete AT-Technik";
        const string TALENTFERNKAMPFWAFFEUNTERKATEGORIE = "Fernkampf";
        const string FILTERDEAKTIVIEREN = "Alle";

        //Felder
        private int _bfVerbesserung;
        private int _tpVerbesserung;
        private int _iniVerbesserung;
        private int _atWmVerbesserung;
        private int _paWmVerbesserung;

        private int _probePunkte;
        private int _probeErschwernis;
        private double _probeDauerInZe;

        private Model.Waffe _erstellteNahkampfwaffe;

        //Listen + SelectedItems
        private Model.Waffe _selectedNahkampfwaffe;
        private List<Model.Waffe> _nahkampfwaffeListe = new List<Model.Waffe>();
        private Model.Talent _selectedNahkampfwaffeTalent;
        private List<Model.Talent> _nahkampfwaffeTalentListe = new List<Model.Talent>();

        private Materialien _nahkampfwaffeMaterialListe;
        private Nahkampfwaffenverbesserung _selectedNahkampfwaffeMaterial;
        private Techniken _nahkampfwaffeTechnikListe;
        private Nahkampfwaffenverbesserung _selectedNahkampfwaffeTechnik;
        
        #endregion

        #region //---- EIGENSCHAFTEN ----

        //Felder        

        public int BfVerbesserung
        {
            get { return _bfVerbesserung; }
            set
            {

                if (value < -7)
                    value = -7;
                else if (value > 0)
                    value = 0;
                if (value == _bfVerbesserung) return;
                _bfVerbesserung = value;
                OnChanged("BfVerbesserung");
                BerechneNahkampfwaffe();
            }
        }

        public int TpVerbesserung
        {
            get { return _tpVerbesserung; }
            set
            {
                if (value > 3)
                    value = 3;
                else if (value < 0)
                    value = 0;
                if (value == _tpVerbesserung) return;
                _tpVerbesserung = value;
                OnChanged("TpVerbesserung");
                BerechneNahkampfwaffe();
            }
        }

        public bool IniVerbesserung
        {
            get { return _iniVerbesserung > 0; }
            set
            {
                _iniVerbesserung = value ? 1 : 0;
                OnChanged("IniVerbessert");
                BerechneNahkampfwaffe();
            }
        }

        public bool AtWmVerbesserung
        {
            get { return _atWmVerbesserung > 0; }
            set
            {
                _atWmVerbesserung = value ? 1 : 0;
                OnChanged("AtWmVerbessert");
                BerechneNahkampfwaffe();
            }
        }

        public bool PaWmVerbesserung
        {
            get { return _paWmVerbesserung > 0; }
            set
            {
                _paWmVerbesserung = value ? 1 : 0;
                OnChanged("PaWmVerbessert");
                BerechneNahkampfwaffe();
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

        public double ProbeDauerInZe
        {
            get { return _probeDauerInZe; }
            private set
            {
                _probeDauerInZe = value;
                OnChanged("ProbeDauerInZe");
            }
        }

        public Model.Waffe ErstellteNahkampfwaffe
        {
            get { return _erstellteNahkampfwaffe; }
            private set
            {
                if (value == null) return;
                _erstellteNahkampfwaffe = value;
                OnChanged("ErstellteNahkampfwaffe");
            }

        }

        //Auswahl der Listen

        public Model.Waffe SelectedNahkampfwaffe
        {
            get { return _selectedNahkampfwaffe; }
            set
            {
                if (value == null) return;
                _selectedNahkampfwaffe = value;
                OnChanged("SelectedNahkampfwaffe");
                _erstellteNahkampfwaffe = Global.ContextWaffe.Clone<Model.Waffe>(_selectedNahkampfwaffe);
                _erstellteNahkampfwaffe.WaffeGUID = Guid.Empty;
                Model.Ausrüstung ausr = Global.ContextWaffe.Clone<Model.Ausrüstung>(_selectedNahkampfwaffe.Ausrüstung);
                ausr.AusrüstungGUID = Guid.Empty;
                _erstellteNahkampfwaffe.Ausrüstung = ausr;
                foreach (Model.Talent t in _selectedNahkampfwaffe.Talent)
                {
                    _erstellteNahkampfwaffe.Talent.Add(t);
                }

                BerechneNahkampfwaffe();
            }
        }

        public Model.Talent SelectedNahkampfwaffeTalent
        {
            get { return _selectedNahkampfwaffeTalent; }
            set
            {
                if (value == null) return;
                _selectedNahkampfwaffeTalent = value;
                if (value.Talentname != FILTERDEAKTIVIEREN)
                {
                    NahkampfwaffeListe = Global.ContextInventar.WaffeListe.Where(w => w.Talent.Contains(value)).OrderBy(w => w.Name).ToList();
                }
                else
                {
                    NahkampfwaffeListe = Global.ContextInventar.WaffeListe.ToList();
                }
                OnChanged("SelectedNahkampfwaffeTalent");
            }
        }

        public Nahkampfwaffenverbesserung SelectedNahkampfwaffeMaterial
        {
            get { return _selectedNahkampfwaffeMaterial; }
            set
            {
                if (value == null) return;
                _selectedNahkampfwaffeMaterial = value;
                OnChanged("SelectedNahkampfwaffeMaterial");
                BerechneNahkampfwaffe();
            }
        }

        public Nahkampfwaffenverbesserung SelectedNahkampfwaffeTechnik
        {
            get { return _selectedNahkampfwaffeTechnik; }
            set
            {
                if (value == null) return;
                _selectedNahkampfwaffeTechnik = value;
                OnChanged("SelectedNahkampfwaffeTechnik");
                BerechneNahkampfwaffe();
            }
        }
        
        //Listen
        public List<Model.Waffe> NahkampfwaffeListe
        {
            get { return _nahkampfwaffeListe; }
            set
            {
                _nahkampfwaffeListe = value;
                OnChanged("NahkampfwaffeListe");
            }
        }

        public List<Model.Talent> NahkampfwaffeTalentListe
        {
            get { return _nahkampfwaffeTalentListe; }
            set
            {
                _nahkampfwaffeTalentListe = value;
                OnChanged("NahkampfwaffeTalentListe");
            }
        }

        public Materialien NahkampfwaffeMaterialListe
        {
            get { return _nahkampfwaffeMaterialListe; }
            set
            {
                _nahkampfwaffeMaterialListe = value;
                OnChanged("NahkampfwaffeMaterialListe");
            }
        }

        public Techniken NahkampfwaffeTechnikListe
        {
            get { return _nahkampfwaffeTechnikListe; }
            set
            {
                _nahkampfwaffeTechnikListe = value;
                OnChanged("NahkampfwaffeTechnikListe");
            }
        }

        //Commands

        #endregion

        #region //---- KONSTRUKTOR ----

        public SchmiedeNahkampfwaffeViewModel()
        {
            NahkampfwaffeMaterialListe = new Materialien();
            SelectedNahkampfwaffeMaterial = NahkampfwaffeMaterialListe.First();
            NahkampfwaffeTechnikListe = new Techniken();
            SelectedNahkampfwaffeTechnik = NahkampfwaffeTechnikListe.First();
        }

        #endregion

        #region //---- INSTANZMETHODEN ----

        public void LoadDaten()
        {
            // Neben Nahkampfwaffen auch Raufen-Waffen (-> Parierwaffen)
            NahkampfwaffeTalentListe.Add(new Model.Talent() { Talentname = FILTERDEAKTIVIEREN });
            NahkampfwaffeTalentListe.AddRange(Global.ContextTalent.TalentListe.Where(t =>
                t.TalentgruppeID == 1
                && (t.Untergruppe == TALENTNAHKAMPFWAFFEUNTERKATEGORIE
                || t.Untergruppe == TALENTNAHKAMPFWAFFEATTECHNIK)
                && !NahkampfwaffeTalentListe.Contains(t)).OrderBy(t => t.Talentname));
            OnChanged("NahkampfwaffeTalentListe");
            NahkampfwaffeListe.AddRange(Global.ContextInventar.WaffeListe.Where(w => !NahkampfwaffeListe.Contains(w)).OrderBy(w => w.Name));
            OnChanged("NahkampfwaffeListe");
        }

        private void BerechneNahkampfwaffe()
        {
            if (_selectedNahkampfwaffe == null || _erstellteNahkampfwaffe == null || _selectedNahkampfwaffeMaterial == null || _selectedNahkampfwaffeTechnik == null) return;
            // Berechne zunächst ohne Einbeziehung von Material und Technik
            int erschwernis = 3 * _tpVerbesserung + (-2) * _bfVerbesserung + 5 * (_atWmVerbesserung + _paWmVerbesserung + _iniVerbesserung);
            int verbesserungen = _tpVerbesserung - _bfVerbesserung + (_atWmVerbesserung + _paWmVerbesserung + _iniVerbesserung);
            int tap = _selectedNahkampfwaffe.TPWürfelAnzahl * (int)Math.Round((_selectedNahkampfwaffe.TPWürfel + 1) / 2.0) + _selectedNahkampfwaffe.TPBonus + _tpVerbesserung;
            tap = (int)Math.Round(tap * (3 + verbesserungen / 2.0));
            double dauer = verbesserungen > 0 ? 12 : 4;

            ProbePunkte = tap;
            ProbeDauerInZe = dauer * _selectedNahkampfwaffeMaterial.DauerFaktor * _selectedNahkampfwaffeTechnik.DauerFaktor;
            ProbeErschwernis = erschwernis + _selectedNahkampfwaffeMaterial.ProbeErschwernis + _selectedNahkampfwaffeTechnik.ProbeErschwernis;

            // Passe die Werte der zu erstellenden Waffe an
            _erstellteNahkampfwaffe.BF = _selectedNahkampfwaffe.BF + _bfVerbesserung + _selectedNahkampfwaffeMaterial.BfVerbesserung + _selectedNahkampfwaffeTechnik.BfVerbesserung;
            _erstellteNahkampfwaffe.TPBonus = _selectedNahkampfwaffe.TPBonus + _tpVerbesserung + _selectedNahkampfwaffeMaterial.TpVerbesserung + _selectedNahkampfwaffeTechnik.TpVerbesserung;
            _erstellteNahkampfwaffe.WMAT = _selectedNahkampfwaffe.WMAT + _atWmVerbesserung + _selectedNahkampfwaffeMaterial.AtWmVerbesserung + _selectedNahkampfwaffeTechnik.AtWmVerbesserung;
            _erstellteNahkampfwaffe.WMPA = _selectedNahkampfwaffe.WMPA + _paWmVerbesserung + _selectedNahkampfwaffeMaterial.PaWmVerbesserung + _selectedNahkampfwaffeTechnik.PaWmVerbesserung;
            _erstellteNahkampfwaffe.INI = _selectedNahkampfwaffe.INI + _iniVerbesserung + _selectedNahkampfwaffeMaterial.IniVerbesserung + _selectedNahkampfwaffeTechnik.IniVerbesserung;
            double preis = _selectedNahkampfwaffe.Preis;
            double preisfaktor = erschwernis;
            if (_selectedNahkampfwaffeMaterial.PreisFaktor > 1) preisfaktor += _selectedNahkampfwaffeMaterial.PreisFaktor; else preisfaktor *= _selectedNahkampfwaffeMaterial.PreisFaktor;
            if (_selectedNahkampfwaffeTechnik.PreisFaktor > 1) preisfaktor += _selectedNahkampfwaffeTechnik.PreisFaktor; else preisfaktor *= _selectedNahkampfwaffeTechnik.PreisFaktor;
            if (preisfaktor != 0) preis *= preisfaktor;
            preis += _selectedNahkampfwaffeMaterial.PreisProUnzeInSilber * _selectedNahkampfwaffe.Gewicht;
            _erstellteNahkampfwaffe.Preis = preis;
            OnChanged("ErstellteNahkampfwaffe");
        }
        #endregion

        #region //---- EVENTS ----

        #endregion

    }
}
