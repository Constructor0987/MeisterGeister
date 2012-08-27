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
    class SchmiedeFernkampfwaffeViewModel : Base.ViewModelBase
    {

        #region //---- FELDER ----

        //Intern: Zeichenketten für DB-Abfragen
        const string TALENTFERNKAMPFWAFFEUNTERKATEGORIE = "Fernkampf";
        const string TALENTARMBRUST = "Armbrust";
        const string TALENTBELAGERUNGSWAFFEN = "Belagerungswaffen";
        const string TALENTBLASROHR = "Blasrohr";
        const string TALENTBOGEN = "Bogen";
        const string TALENTDISKUS = "Diskus";
        const string TALENTSCHLEUDER = "Schleuder";
        const string TALENTWURFBEILE = "Wurfbeile";
        const string TALENTWURFMESSER = "Wurfmesser";
        const string TALENTWURFSPEERE = "Wurfspeere";
        const string FILTERDEAKTIVIEREN = "Alle";

        //Felder
        private int _bfVerbesserung;
        private int _tpVerbesserung;
        private int _iniVerbesserung;
        private int _atWmVerbesserung;
        private int _paWmVerbesserung;
        private int _fkVerbesserung;
        private int _kkVerbesserung;

        private int _probePunkte;
        private int _probeErschwernis;
        private double _probeDauerInZe;

        private Model.Fernkampfwaffe _erstellteFernkampfwaffe;
        private Model.Waffe _erstellteNahkampfwaffe;

        //Listen + SelectedItems
        private Model.Waffe _selectedNahkampfwaffe;
        private Model.Fernkampfwaffe _selectedFernkampfwaffe;
        private List<Model.Fernkampfwaffe> _fernkampfwaffeListe = new List<Model.Fernkampfwaffe>();
        private Model.Talent _selectedFernkampfwaffeTalent;
        private List<Model.Talent> _fernkampfwaffeTalentListe = new List<Model.Talent>();

        private Materialien _fernkampfwaffeMaterialListe;
        private Nahkampfwaffenverbesserung _selectedFernkampfwaffeMaterial;
        private Techniken _fernkampfwaffeTechnikListe;
        private Nahkampfwaffenverbesserung _selectedFernkampfwaffeTechnik;
        
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
                BerechneFernkampfwaffe();
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
                BerechneFernkampfwaffe();
            }
        }

        public bool IniVerbesserung
        {
            get { return _iniVerbesserung > 0; }
            set
            {
                _iniVerbesserung = value ? 1 : 0;
                OnChanged("IniVerbesserung");
                BerechneFernkampfwaffe();
            }
        }

        public bool AtWmVerbesserung
        {
            get { return _atWmVerbesserung > 0; }
            set
            {
                _atWmVerbesserung = value ? 1 : 0;
                OnChanged("AtWmVerbesserung");
                BerechneFernkampfwaffe();
            }
        }

        public bool PaWmVerbesserung
        {
            get { return _paWmVerbesserung > 0; }
            set
            {
                _paWmVerbesserung = value ? 1 : 0;
                OnChanged("PaWmVerbesserung");
                BerechneFernkampfwaffe();
            }
        }

        public int FkVerbesserung
        {
            get { return _fkVerbesserung; }
            set
            {
                if (value > 2)
                    value = 2;
                else if (value < 0)
                    value = 0;
                if (value == _fkVerbesserung) return;
                _fkVerbesserung = value;
                OnChanged("FkVerbesserung");
                BerechneFernkampfwaffe();
            }
        }
        
        public bool KkVerbesserung
        {
            get { return _kkVerbesserung > 0; }
            set
            {
                _kkVerbesserung = value ? 1 : 0;
                OnChanged("KkVerbesserung");
                BerechneFernkampfwaffe();
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

        public Model.Fernkampfwaffe ErstellteFernkampfwaffe
        {
            get { return _erstellteFernkampfwaffe; }
            private set
            {
                if (value == null) return;
                _erstellteFernkampfwaffe = value;
                OnChanged("ErstellteFernkampfwaffe");
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

        public Model.Fernkampfwaffe SelectedFernkampfwaffe
        {
            get { return _selectedFernkampfwaffe; }
            set
            {
                if (value == null) return;
                _selectedFernkampfwaffe = value;
                OnChanged("SelectedFernkampfwaffe");
                _erstellteFernkampfwaffe = Global.ContextFernkampfwaffe.Clone<Model.Fernkampfwaffe>(_selectedFernkampfwaffe);
                _erstellteFernkampfwaffe.FernkampfwaffeGUID = Guid.Empty;
                Model.Ausrüstung ausr = Global.ContextWaffe.Clone<Model.Ausrüstung>(_selectedFernkampfwaffe.Ausrüstung);
                ausr.AusrüstungGUID = Guid.Empty;
                _erstellteFernkampfwaffe.Ausrüstung = ausr;
                ausr.Fernkampfwaffe = _erstellteFernkampfwaffe;
                //Prüfen, ob FK-Waffe auch NK-Waffe ist
                if (_selectedFernkampfwaffe.Ausrüstung.Waffe != null)
                {
                    _selectedNahkampfwaffe = _selectedFernkampfwaffe.Ausrüstung.Waffe;
                    _erstellteNahkampfwaffe = Global.ContextWaffe.Clone<Model.Waffe>(_selectedNahkampfwaffe);
                    _erstellteNahkampfwaffe.WaffeGUID = Guid.Empty;
                    foreach (Model.Talent t in _selectedNahkampfwaffe.Talent)
                    {
                        _erstellteNahkampfwaffe.Talent.Add(t);
                    }
                    ausr.Waffe = _erstellteNahkampfwaffe;
                    _erstellteNahkampfwaffe.Ausrüstung = ausr;
                }
                else
                {
                    _selectedNahkampfwaffe = null;
                    _erstellteNahkampfwaffe = null;
                }
                
                foreach (Model.Talent t in _selectedFernkampfwaffe.Talent)
                {
                    _erstellteFernkampfwaffe.Talent.Add(t);
                }

                BerechneFernkampfwaffe();
            }
        }

        public Model.Talent SelectedFernkampfwaffeTalent
        {
            get { return _selectedFernkampfwaffeTalent; }
            set
            {
                if (value == null) return;
                _selectedFernkampfwaffeTalent = value;
                if (value.Talentname != FILTERDEAKTIVIEREN) {
                    FernkampfwaffeListe = Global.ContextInventar.FernkampfwaffeListe.Where(w => w.Talent.Contains(value)).OrderBy(w => w.Name).ToList();
                } else {
                    FernkampfwaffeListe = Global.ContextInventar.FernkampfwaffeListe.ToList();
                }
                OnChanged("SelectedFernkampfwaffeTalent");
            }
        }

        public Nahkampfwaffenverbesserung SelectedFernkampfwaffeMaterial
        {
            get { return _selectedFernkampfwaffeMaterial; }
            set
            {
                if (value == null) return;
                _selectedFernkampfwaffeMaterial = value;
                OnChanged("SelectedFernkampfwaffeMaterial");
                BerechneFernkampfwaffe();
            }
        }

        public Nahkampfwaffenverbesserung SelectedFernkampfwaffeTechnik
        {
            get { return _selectedFernkampfwaffeTechnik; }
            set
            {
                if (value == null) return;
                _selectedFernkampfwaffeTechnik = value;
                OnChanged("SelectedFernkampfwaffeTechnik");
                BerechneFernkampfwaffe();
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

        public Materialien FernkampfwaffeMaterialListe
        {
            get { return _fernkampfwaffeMaterialListe; }
            set
            {
                _fernkampfwaffeMaterialListe = value;
                OnChanged("FernkampfwaffeMaterialListe");
            }
        }

        public Techniken FernkampfwaffeTechnikListe
        {
            get { return _fernkampfwaffeTechnikListe; }
            set
            {
                _fernkampfwaffeTechnikListe = value;
                OnChanged("FernkampfwaffeTechnikListe");
            }
        }

        //Commands

        #endregion

        #region //---- KONSTRUKTOR ----

        public SchmiedeFernkampfwaffeViewModel()
        {
            FernkampfwaffeMaterialListe = new Materialien();
            SelectedFernkampfwaffeMaterial = FernkampfwaffeMaterialListe.First();
            FernkampfwaffeTechnikListe = new Techniken();
            SelectedFernkampfwaffeTechnik = FernkampfwaffeTechnikListe.First();
        }

        #endregion

        #region //---- INSTANZMETHODEN ----

        public void LoadDaten()
        {
            FernkampfwaffeTalentListe.Add(new Model.Talent() { Talentname = FILTERDEAKTIVIEREN });
            FernkampfwaffeTalentListe.AddRange(Global.ContextTalent.TalentListe.Where(t => t.TalentgruppeID == 1 && t.Untergruppe == TALENTFERNKAMPFWAFFEUNTERKATEGORIE && !FernkampfwaffeTalentListe.Contains(t)).OrderBy(t => t.Talentname));
            FernkampfwaffeListe.AddRange(Global.ContextInventar.FernkampfwaffeListe.Where(w => !FernkampfwaffeListe.Contains(w)).OrderBy(w => w.Name));
            OnChanged("FernkampfwaffeListe");
        }

        private void BerechneFernkampfwaffe()
        {
            if (_selectedFernkampfwaffe == null || _erstellteFernkampfwaffe == null) return;
            // Berechne zunächst ohne Einbeziehung von Material und Technik
            int erschwernis = 3 * _tpVerbesserung + (-2) * _bfVerbesserung + 5 * (_atWmVerbesserung + _paWmVerbesserung + _iniVerbesserung + _kkVerbesserung) + 7 * _fkVerbesserung;
            int verbesserungen = _tpVerbesserung - _bfVerbesserung + (_atWmVerbesserung + _paWmVerbesserung + _iniVerbesserung + _kkVerbesserung + _fkVerbesserung);
            int tap = (_selectedFernkampfwaffe.TPWürfelAnzahl.HasValue ? _selectedFernkampfwaffe.TPWürfelAnzahl.Value : 0) * (int)Math.Round(((_selectedFernkampfwaffe.TPWürfel.HasValue ? _selectedFernkampfwaffe.TPWürfel.Value : 0) + 1) / 2.0) + (_selectedFernkampfwaffe.TPBonus.HasValue ? _selectedFernkampfwaffe.TPBonus.Value : 0) + _tpVerbesserung;
            tap = (int)Math.Round(tap * (3 + verbesserungen / 2.0));
            double dauer = 4;
            if (_selectedFernkampfwaffe.Talent.Contains(Global.ContextTalent.TalentListe.Where(t => t.TalentgruppeID == 1 && t.Talentname == TALENTARMBRUST).First())){
                dauer = verbesserungen > 0 ? 20 : 10;
            }
            else if (_selectedFernkampfwaffe.Talent.Contains(Global.ContextTalent.TalentListe.Where(t => t.TalentgruppeID == 1 && t.Talentname == TALENTBOGEN).First()))
            {
                dauer = verbesserungen > 0 ? 6 : 3;
            }
            else
            {
                dauer = verbesserungen > 0 ? 12 : 4;
            }

            ProbePunkte = tap;
            ProbeDauerInZe = dauer * _selectedFernkampfwaffeMaterial.DauerFaktor * _selectedFernkampfwaffeTechnik.DauerFaktor;
            ProbeErschwernis = erschwernis + _selectedFernkampfwaffeMaterial.ProbeErschwernis + _selectedFernkampfwaffeTechnik.ProbeErschwernis;

            // Passe die Werte der zu erstellenden Waffe an
            _erstellteFernkampfwaffe.TPBonus = _selectedFernkampfwaffe.TPBonus + _tpVerbesserung + _selectedFernkampfwaffeMaterial.TpVerbesserung + _selectedFernkampfwaffeTechnik.TpVerbesserung;

            double preis = _selectedFernkampfwaffe.Preis;
            double preisfaktor = erschwernis;
            if (_selectedFernkampfwaffeMaterial.PreisFaktor > 1) preisfaktor += _selectedFernkampfwaffeMaterial.PreisFaktor; else preisfaktor *= _selectedFernkampfwaffeMaterial.PreisFaktor;
            if (_selectedFernkampfwaffeTechnik.PreisFaktor > 1) preisfaktor += _selectedFernkampfwaffeTechnik.PreisFaktor; else preisfaktor *= _selectedFernkampfwaffeTechnik.PreisFaktor;
            if (preisfaktor != 0) preis *= preisfaktor;
            preis += _selectedFernkampfwaffeMaterial.PreisProUnzeInSilber * _selectedFernkampfwaffe.Gewicht;
            _erstellteFernkampfwaffe.Preis = preis;
            if (_selectedNahkampfwaffe != null && _erstellteNahkampfwaffe != null)
            {
                _erstellteNahkampfwaffe.BF = _selectedNahkampfwaffe.BF + _bfVerbesserung + _selectedFernkampfwaffeMaterial.BfVerbesserung + _selectedFernkampfwaffeTechnik.BfVerbesserung;
                _erstellteNahkampfwaffe.TPBonus = _selectedNahkampfwaffe.TPBonus + _tpVerbesserung + _selectedFernkampfwaffeMaterial.TpVerbesserung + _selectedFernkampfwaffeTechnik.TpVerbesserung;
                _erstellteNahkampfwaffe.WMAT = _selectedNahkampfwaffe.WMAT + _atWmVerbesserung + _selectedFernkampfwaffeMaterial.AtWmVerbesserung + _selectedFernkampfwaffeTechnik.AtWmVerbesserung;
                _erstellteNahkampfwaffe.WMPA = _selectedNahkampfwaffe.WMPA + _paWmVerbesserung + _selectedFernkampfwaffeMaterial.PaWmVerbesserung + _selectedFernkampfwaffeTechnik.PaWmVerbesserung;
                _erstellteNahkampfwaffe.INI = _selectedNahkampfwaffe.INI + _iniVerbesserung + _selectedFernkampfwaffeMaterial.IniVerbesserung + _selectedFernkampfwaffeTechnik.IniVerbesserung;
                _erstellteNahkampfwaffe.Preis = preis;
            }
            OnChanged("ErstellteFernkampfwaffe");
            OnChanged("ErstellteNahkampfwaffe");
        }
        #endregion

        #region //---- EVENTS ----

        #endregion

    }
}
