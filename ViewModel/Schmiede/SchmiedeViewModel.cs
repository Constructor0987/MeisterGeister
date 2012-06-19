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
    class SchmiedeViewModel : Base.ViewModelBase
    {

        #region //---- FELDER ----

        //Intern: Zeichenketten für DB-Abfragen
        const string TALENTNAHKAMPFWAFFEUNTERKATEGORIE = "Bewaffneter Nahkampf";
        const string TALENTNAHKAMPFWAFFEATTECHNIK = "Bewaffnete AT-Technik";
        const string TALENTNAHKAMPFTALENTRAUFEN = "Bewaffnete AT-Technik";
        const string TALENTFERNKAMPFWAFFEUNTERKATEGORIE = "Fernkampf";
        const string FILTERDEAKTIVIEREN = "Alle";
        // TODO FK: Buckler,.. über UIDS
        
        //Felder
        private int _waffeBfVerbesserung;
        private int _waffeTpVerbesserung;
        private int _waffeIniVerbesserung;
        private int _waffeAtWmVerbesserung;
        private int _waffePaWmVerbesserung;

        private int _nahkampfwaffeProbePunkte;
        private int _nahkampfwaffeProbeErschwernis;
        private double _nahkampfwaffeProbeDauerInZe;

        private Model.Waffe _erstellteNahkampfwaffe;

        private int _schildProbePunkte;

        //Listen + SelectedItems
        private Model.Waffe _selectedNahkampfwaffe;
        private List<Model.Waffe> _nahkampfwaffeListe = new List<Model.Waffe>();
        private Model.Talent _selectedNahkampfwaffeTalent;
        private List<Model.Talent> _nahkampfwaffeTalentListe = new List<Model.Talent>();

        private Materialien _nahkampfwaffeMaterialListe;
        private Nahkampfwaffenverbesserung _selectedNahkampfwaffeMaterial;
        private Techniken _nahkampfwaffeTechnikListe;
        private Nahkampfwaffenverbesserung _selectedNahkampfwaffeTechnik;

        private Model.Schild _selectedSchild;
        private List<Model.Schild> _schildListe = new List<Model.Schild>();

        #endregion

        #region //---- EIGENSCHAFTEN ----

        //Felder        

        public int WaffeBfVerbesserung
        {
            get { return _waffeBfVerbesserung; }
            set
            {

                if (value < -7)
                    value = -7;
                else if (value > 0)
                    value = 0;
                if (value == _waffeBfVerbesserung) return;
                _waffeBfVerbesserung = value;
                OnChanged("WaffeBfVerbesserung");
                BerechneNahkampfwaffe();
            }
        }

        public int WaffeTpVerbesserung
        {
            get { return _waffeTpVerbesserung; }
            set
            {
                if (value > 3)
                    value = 3;
                else if (value < 0)
                    value = 0;
                if (value == _waffeTpVerbesserung) return;
                _waffeTpVerbesserung = value;
                OnChanged("WaffeTpVerbesserung");
                BerechneNahkampfwaffe();
            }
        }

        public bool WaffeIniVerbesserung
        {
            get { return _waffeIniVerbesserung > 0; }
            set
            {
                _waffeIniVerbesserung = value ? 1 : 0;
                OnChanged("WaffeIniVerbessert");
                BerechneNahkampfwaffe();
            }
        }

        public bool WaffeAtWmVerbesserung
        {
            get { return _waffeAtWmVerbesserung > 0; }
            set
            {
                _waffeAtWmVerbesserung = value ? 1 : 0;
                OnChanged("WaffeAtWmVerbessert");
                BerechneNahkampfwaffe();
            }
        }

        public bool WaffePaWmVerbesserung
        {
            get { return _waffePaWmVerbesserung > 0; }
            set
            {
                _waffePaWmVerbesserung = value ? 1 : 0;
                OnChanged("WaffePaWmVerbessert");
                BerechneNahkampfwaffe();
            }
        }

        public int NahkampfwaffeProbePunkte
        {
            get { return _nahkampfwaffeProbePunkte; }
            private set
            {
                _nahkampfwaffeProbePunkte = value;
                OnChanged("NahkampfwaffeProbePunkte");
            }
        }

        public int NahkampfwaffeProbeErschwernis
        {
            get { return _nahkampfwaffeProbeErschwernis; }
            private set
            {
                _nahkampfwaffeProbeErschwernis = value;
                OnChanged("NahkampfwaffeProbeErschwernis");
            }
        }

        public double NahkampfwaffeProbeDauerInZe
        {
            get { return _nahkampfwaffeProbeDauerInZe; }
            private set
            {
                _nahkampfwaffeProbeDauerInZe = value;
                OnChanged("NahkampfwaffeProbeDauerInZe");
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

        public int SchildProbePunkte
        {
            get { return _schildProbePunkte; }
            private set
            {
                _schildProbePunkte = value;
                OnChanged("SchildProbePunkte");
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

        public SchmiedeViewModel()
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

            // Schilde - keine Parierwaffen
            SchildListe.AddRange(Global.ContextInventar.SchildListe.Where(w => (w.Typ == "S" || w.Name == "Buckler" || w.Name == "Großer (Vollmetall-) Buckler") && !SchildListe.Contains(w)).OrderBy(w => w.Name));
            SchildListe = SchildListe;
            OnChanged("SchildListe");
        }

        private void BerechneNahkampfwaffe()
        {
            if (_selectedNahkampfwaffe == null || _erstellteNahkampfwaffe == null || _selectedNahkampfwaffeMaterial == null || _selectedNahkampfwaffeTechnik == null) return;
            // Berechne zunächst ohne Einbeziehung von Material und Technik
            int erschwernis = 3 * _waffeTpVerbesserung + (-2) * _waffeBfVerbesserung + 5 * (_waffeAtWmVerbesserung + _waffePaWmVerbesserung + _waffeIniVerbesserung);
            int verbesserungen = _waffeTpVerbesserung - _waffeBfVerbesserung + (_waffeAtWmVerbesserung + _waffePaWmVerbesserung + _waffeIniVerbesserung);
            int tap = _selectedNahkampfwaffe.TPWürfelAnzahl * (int)Math.Round((_selectedNahkampfwaffe.TPWürfel + 1) / 2.0) + _selectedNahkampfwaffe.TPBonus + _waffeTpVerbesserung;
            tap = (int)Math.Round(tap * (3 + verbesserungen / 2.0));
            double dauer = verbesserungen > 0 ? 12 : 4;

            NahkampfwaffeProbePunkte = tap;
            NahkampfwaffeProbeDauerInZe = dauer * _selectedNahkampfwaffeMaterial.DauerFaktor * _selectedNahkampfwaffeTechnik.DauerFaktor;
            NahkampfwaffeProbeErschwernis = erschwernis + _selectedNahkampfwaffeMaterial.ProbeErschwernis + _selectedNahkampfwaffeTechnik.ProbeErschwernis;

            // Passe die Werte der zu erstellenden Waffe an
            _erstellteNahkampfwaffe.BF = _selectedNahkampfwaffe.BF + _waffeBfVerbesserung + _selectedNahkampfwaffeMaterial.BfVerbesserung + _selectedNahkampfwaffeTechnik.BfVerbesserung;
            _erstellteNahkampfwaffe.TPBonus = _selectedNahkampfwaffe.TPBonus + _waffeTpVerbesserung + _selectedNahkampfwaffeMaterial.TpVerbesserung + _selectedNahkampfwaffeTechnik.TpVerbesserung;
            _erstellteNahkampfwaffe.WMAT = _selectedNahkampfwaffe.WMAT + _waffeAtWmVerbesserung + _selectedNahkampfwaffeMaterial.AtWmVerbesserung + _selectedNahkampfwaffeTechnik.AtWmVerbesserung;
            _erstellteNahkampfwaffe.WMPA = _selectedNahkampfwaffe.WMPA + _waffePaWmVerbesserung + _selectedNahkampfwaffeMaterial.PaWmVerbesserung + _selectedNahkampfwaffeTechnik.PaWmVerbesserung;
            _erstellteNahkampfwaffe.INI = _selectedNahkampfwaffe.INI + _waffeIniVerbesserung + _selectedNahkampfwaffeMaterial.IniVerbesserung + _selectedNahkampfwaffeTechnik.IniVerbesserung;
            double preis = _selectedNahkampfwaffe.Preis;
            double preisfaktor = erschwernis;
            if (_selectedNahkampfwaffeMaterial.PreisFaktor > 1) preisfaktor += _selectedNahkampfwaffeMaterial.PreisFaktor; else preisfaktor *= _selectedNahkampfwaffeMaterial.PreisFaktor;
            if (_selectedNahkampfwaffeTechnik.PreisFaktor > 1) preisfaktor += _selectedNahkampfwaffeTechnik.PreisFaktor; else preisfaktor *= _selectedNahkampfwaffeTechnik.PreisFaktor;
            if (preisfaktor != 0) preis *= preisfaktor;
            preis += _selectedNahkampfwaffeMaterial.PreisProUnzeInSilber * _selectedNahkampfwaffe.Gewicht;
            _erstellteNahkampfwaffe.Preis = preis;
            OnChanged("ErstellteNahkampfwaffe");
        }

        private void BerechneSchild()
        {
            if (_selectedSchild == null) return;
            SchildProbePunkte = _selectedSchild.WMPA * 3;
        }
        #endregion

        #region //---- EVENTS ----

        #endregion

    }
}
