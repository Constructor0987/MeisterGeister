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
    public class SchmiedeFernkampfwaffeViewModel : Base.ViewModelBase
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
        private bool _istAuchNahkampfwaffe;
        private bool _istWurfwaffe;
        private bool _istFernkampfPersonalisierbar;
        private bool _istNahkampfPersonalisierbar;
        private bool _istBogen;
        private Visibility _anzeigenNahkampfwerte;
        private Visibility _anzeigenTpVerbesserung;
        private Visibility _anzeigenBfVerbesserung;
        private Visibility _anzeigenFernkampfPersonalisierbar;
        private Visibility _anzeigenKkPersonalisierbar;
        private Visibility _anzeigenNahkampfPersonalisierbar;
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

        private int _tawSchmied;
        private int _tawSchmiedMod;
        private int _probeDauerNApprox;

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

        public bool IstAuchNahkampfwaffe
        {
            get { return _istAuchNahkampfwaffe; }
            private set
            {
                if (_istAuchNahkampfwaffe == value) return;
                _istAuchNahkampfwaffe = value;
                BerechneSichtbarkeitErstellungsoptionen();
                OnChanged("IstAuchNahkampfwaffe");
            }

        }

        public bool IstWurfwaffe
        {
            get { return _istWurfwaffe; }
            private set
            {
                if (_istWurfwaffe == value) return;
                _istWurfwaffe = value;
                BerechneSichtbarkeitErstellungsoptionen();
                OnChanged("IstWurfwaffe");
            }

        }

        public bool IstFernkampfPersonalisierbar
        {
            get { return _istFernkampfPersonalisierbar; }
            private set
            {
                if (_istFernkampfPersonalisierbar == value) return;
                _istFernkampfPersonalisierbar = value;
                BerechneSichtbarkeitErstellungsoptionen();
                OnChanged("IstFernkampfPersonalisierbar");
            }

        }

        public bool IstNahkampfPersonalisierbar
        {
            get { return _istNahkampfPersonalisierbar; }
            private set
            {
                if (_istNahkampfPersonalisierbar == value) return;
                _istNahkampfPersonalisierbar = value;
                BerechneSichtbarkeitErstellungsoptionen();
                OnChanged("IstNahkampfPersonalisierbar");
            }

        }

        public bool IstBogen
        {
            get { return _istBogen; }
            private set
            {
                if (_istBogen == value) return;
                _istBogen = value;
                BerechneSichtbarkeitErstellungsoptionen();
                OnChanged("IstBogen");
            }

        }

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
                if (value == (_iniVerbesserung > 0)) return;
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
                if (value == (_atWmVerbesserung > 0)) return;
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
                if (value == (_paWmVerbesserung > 0)) return;
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
                if (value == (_kkVerbesserung > 0)) return;
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
        //Sichtbarkeiten für Erstellungsoptionen

        public Visibility AnzeigenNahkampfwerte
        {
            get { return _anzeigenNahkampfwerte; }
            private set
            {
                _anzeigenNahkampfwerte = value;
                OnChanged("AnzeigenNahkampfwerte");
            }

        }

        public Visibility AnzeigenTpVerbesserung
        {
            get { return _anzeigenTpVerbesserung; }
            private set
            {
                _anzeigenTpVerbesserung = value;
                OnChanged("AnzeigenTpVerbesserung");
            }

        }

        public Visibility AnzeigenBfVerbesserung
        {
            get { return _anzeigenBfVerbesserung; }
            private set
            {
                _anzeigenBfVerbesserung = value;
                OnChanged("AnzeigenBfVerbesserung");
            }

        }

        public Visibility AnzeigenFernkampfPersonalisierbar
        {
            get { return _anzeigenFernkampfPersonalisierbar; }
            private set
            {
                _anzeigenFernkampfPersonalisierbar = value;
                OnChanged("AnzeigenFernkampfPersonalisierbar");
            }

        }

        public Visibility AnzeigenKkPersonalisierbar
        {
            get { return _anzeigenKkPersonalisierbar; }
            private set
            {
                _anzeigenKkPersonalisierbar = value;
                OnChanged("AnzeigenKkPersonalisierbar");
            }

        }

        public Visibility AnzeigenNahkampfPersonalisierbar
        {
            get { return _anzeigenNahkampfPersonalisierbar; }
            private set
            {
                _anzeigenNahkampfPersonalisierbar = value;
                OnChanged("AnzeigenNahkampfPersonalisierbar");
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
                _erstellteFernkampfwaffe = Global.ContextInventar.Clone<Model.Fernkampfwaffe>(_selectedFernkampfwaffe);
                _erstellteFernkampfwaffe.FernkampfwaffeGUID = Guid.Empty;
                Model.Ausrüstung ausr = Global.ContextInventar.Clone<Model.Ausrüstung>(_selectedFernkampfwaffe.Ausrüstung);
                ausr.AusrüstungGUID = Guid.Empty;
                _erstellteFernkampfwaffe.Ausrüstung = ausr;
                ausr.Fernkampfwaffe = _erstellteFernkampfwaffe;
                foreach (var item in _selectedFernkampfwaffe.Ausrüstung.Ausrüstung_Setting)
                    _erstellteFernkampfwaffe.Ausrüstung.Ausrüstung_Setting.Add(Global.ContextInventar.Clone<Model.Ausrüstung_Setting>(item));
                //Prüfen, ob FK-Waffe auch NK-Waffe ist
                if (_selectedFernkampfwaffe.Ausrüstung.Waffe != null)
                {
                    _selectedNahkampfwaffe = _selectedFernkampfwaffe.Ausrüstung.Waffe;
                    _erstellteNahkampfwaffe = Global.ContextInventar.Clone<Model.Waffe>(_selectedNahkampfwaffe);
                    _erstellteNahkampfwaffe.WaffeGUID = Guid.Empty;
                    // Auskommentiert bis Waffe später wirklich in die DB eingetragen wird
                    //foreach (Model.Talent t in _selectedNahkampfwaffe.Talent)
                    //{
                    //    _erstellteNahkampfwaffe.Talent.Add(t);
                    //}
                    ausr.Waffe = _erstellteNahkampfwaffe;
                    _erstellteNahkampfwaffe.Ausrüstung = ausr;
                    IstAuchNahkampfwaffe = true;
                }
                else
                {
                    _selectedNahkampfwaffe = null;
                    _erstellteNahkampfwaffe = null;
                    IstAuchNahkampfwaffe = false;
                }

                IstWurfwaffe = false;
                IstFernkampfPersonalisierbar = false;
                IstNahkampfPersonalisierbar = false;
                IstBogen = false;

                foreach (Model.Talent t in _selectedFernkampfwaffe.Talent)
                {
                    // Auskommentiert bis Waffe später wirklich in die DB eingetragen wird
                    //_erstellteFernkampfwaffe.Talent.Add(t);
                    switch (t.Talentname)
                    {
                        case TALENTDISKUS:
                            IstWurfwaffe |= true;
                            IstFernkampfPersonalisierbar |= true;
                            IstNahkampfPersonalisierbar |= IstAuchNahkampfwaffe;
                            break;
                        case TALENTWURFBEILE:
                            IstWurfwaffe |= true;
                            IstFernkampfPersonalisierbar |= true;
                            IstNahkampfPersonalisierbar |= IstAuchNahkampfwaffe;
                            break;
                        case TALENTWURFMESSER:
                            IstWurfwaffe |= true;
                            IstNahkampfPersonalisierbar |= IstAuchNahkampfwaffe;
                            break;
                        case TALENTWURFSPEERE:
                            IstWurfwaffe |= true;
                            IstFernkampfPersonalisierbar |= true;
                            IstNahkampfPersonalisierbar |= IstAuchNahkampfwaffe;
                            break;
                        case TALENTBOGEN:
                            IstBogen |= true;
                            IstFernkampfPersonalisierbar |= true;
                            break;
                        case TALENTARMBRUST:
                            IstFernkampfPersonalisierbar |= true;
                            break;

                    }

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
                if (value.Talentname != FILTERDEAKTIVIEREN)
                {
                    FernkampfwaffeListe = Global.ContextInventar.FernkampfwaffeListe.Where(w => w.Talent.Contains(value)).OrderBy(w => w.Name).ToList();
                }
                else
                {
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
            Init();
            TawSchmied = 12;
        }

        #endregion

        #region //---- INSTANZMETHODEN ----

        private void Init()
        {
            // Listen erstellen
            FernkampfwaffeMaterialListe = new Materialien();
            FernkampfwaffeTechnikListe = new Techniken();
            FernkampfwaffeTalentListe.Add(new Model.Talent() { Talentname = FILTERDEAKTIVIEREN });
            if (Global.ContextInventar != null)
            {
                FernkampfwaffeTalentListe.AddRange(Global.ContextTalent.TalentListe.Where(t => t.TalentgruppeID == 1 && t.Untergruppe == TALENTFERNKAMPFWAFFEUNTERKATEGORIE && t.Talentname != TALENTBELAGERUNGSWAFFEN && !FernkampfwaffeTalentListe.Contains(t)).OrderBy(t => t.Talentname));
                FernkampfwaffeListe.AddRange(Global.ContextInventar.FernkampfwaffeListe.Where(w => !FernkampfwaffeListe.Contains(w)).OrderBy(w => w.Name));
            }
            OnChanged("FernkampfwaffeTalentListe");
            OnChanged("FernkampfwaffeListe");

            SelectedFernkampfwaffeMaterial = FernkampfwaffeMaterialListe.First();
            SelectedFernkampfwaffeTechnik = FernkampfwaffeTechnikListe.First();

            // Verbesserungsoptionen zunächst verbergen
            IstAuchNahkampfwaffe = false;
            IstBogen = false;
            IstFernkampfPersonalisierbar = false;
            IstNahkampfPersonalisierbar = false;
            IstWurfwaffe = false;
            BerechneSichtbarkeitErstellungsoptionen();
        }

        public void Refresh()
        {
            // derzeit nichts beim erneuten Anzeigen der Tabs erforderlich
        }

        private void BerechneSichtbarkeitErstellungsoptionen()
        {
            AnzeigenNahkampfwerte = IstAuchNahkampfwaffe ? Visibility.Visible : Visibility.Hidden;
            AnzeigenBfVerbesserung = IstAuchNahkampfwaffe ? Visibility.Visible : Visibility.Hidden;
            AnzeigenFernkampfPersonalisierbar = IstFernkampfPersonalisierbar ? Visibility.Visible : Visibility.Hidden;
            AnzeigenKkPersonalisierbar = IstBogen ? Visibility.Visible : Visibility.Hidden;
            AnzeigenNahkampfPersonalisierbar = IstNahkampfPersonalisierbar ? Visibility.Visible : Visibility.Hidden;
            AnzeigenTpVerbesserung = (IstWurfwaffe || IstAuchNahkampfwaffe) ? Visibility.Visible : Visibility.Hidden;
        }

        private void BerechneNicwinscheApproximation()
        {
            int tapStern = TawSchmied - TawSchmiedMod - ProbeErschwernis;
            if (tapStern > TawSchmied) tapStern = TawSchmied;
            tapStern /= 2;
            if (tapStern < 1) tapStern = 1;
            ProbeDauerNApprox = (ProbePunkte  * (int)ProbeDauerInZe) / (4 * tapStern);
            ProbeDauerNApprox = (tapStern > 0) ? tapStern : 1;
        }

        private void BerechneFernkampfwaffe()
        {
            if (_selectedFernkampfwaffe == null || _erstellteFernkampfwaffe == null) return;
            // nur die relevanten Verbesserungen einberechnen
            int tpVerbesserung = (IstWurfwaffe || IstAuchNahkampfwaffe) ? _tpVerbesserung : 0;
            int bfVerbesserung = IstAuchNahkampfwaffe ? _bfVerbesserung : 0;
            int atWmVerbesserung = IstNahkampfPersonalisierbar ? _atWmVerbesserung : 0;
            int paWmVerbesserung = IstNahkampfPersonalisierbar ? _paWmVerbesserung : 0;
            int iniVerbesserung = IstNahkampfPersonalisierbar ? _iniVerbesserung : 0;
            int kkVerbesserung = IstBogen ? _kkVerbesserung : 0;
            int fkVerbesserung = IstFernkampfPersonalisierbar ? _fkVerbesserung : 0;

            // Berechne zunächst ohne Einbeziehung von Material und Technik
            int erschwernis = 3 * tpVerbesserung + (-2) * bfVerbesserung + 5 * (atWmVerbesserung + paWmVerbesserung + iniVerbesserung + kkVerbesserung) + 7 * fkVerbesserung;
            int verbesserungen = tpVerbesserung - bfVerbesserung + (atWmVerbesserung + paWmVerbesserung + iniVerbesserung + kkVerbesserung + fkVerbesserung);
            int tap = (_selectedFernkampfwaffe.TPWürfelAnzahl.HasValue ? _selectedFernkampfwaffe.TPWürfelAnzahl.Value : 0) * (int)Math.Round(((_selectedFernkampfwaffe.TPWürfel.HasValue ? _selectedFernkampfwaffe.TPWürfel.Value : 0) + 1) / 2.0) + (_selectedFernkampfwaffe.TPBonus.HasValue ? _selectedFernkampfwaffe.TPBonus.Value : 0) + tpVerbesserung;
            tap = (int)Math.Round(tap * (3 + verbesserungen / 2.0));
            double dauer = 4;
            if (_selectedFernkampfwaffe.Talent.Contains(Global.ContextTalent.TalentListe.Where(t => t.TalentgruppeID == 1 && t.Talentname == TALENTARMBRUST).First()))
            {
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
            // ab hier Technik und Material einbeziehen
            bool technikUndMaterial = (IstWurfwaffe || IstAuchNahkampfwaffe);

            ProbeDauerInZe = dauer * (technikUndMaterial ? _selectedFernkampfwaffeMaterial.DauerFaktor * _selectedFernkampfwaffeTechnik.DauerFaktor : 1);
            ProbeErschwernis = erschwernis + (technikUndMaterial ? _selectedFernkampfwaffeMaterial.ProbeErschwernis + _selectedFernkampfwaffeTechnik.ProbeErschwernis : 0);

            // Passe die Werte der zu erstellenden Waffe an
            _erstellteFernkampfwaffe.TPBonus = _selectedFernkampfwaffe.TPBonus + tpVerbesserung + (technikUndMaterial ? _selectedFernkampfwaffeMaterial.TpVerbesserung + _selectedFernkampfwaffeTechnik.TpVerbesserung : 0);

            double preis = _selectedFernkampfwaffe.Preis;
            double preisfaktor = erschwernis;
            if (technikUndMaterial)
            {
                if (_selectedFernkampfwaffeMaterial.PreisFaktor > 1) preisfaktor += _selectedFernkampfwaffeMaterial.PreisFaktor; else preisfaktor *= _selectedFernkampfwaffeMaterial.PreisFaktor;
                if (_selectedFernkampfwaffeTechnik.PreisFaktor > 1) preisfaktor += _selectedFernkampfwaffeTechnik.PreisFaktor; else preisfaktor *= _selectedFernkampfwaffeTechnik.PreisFaktor;
            }
            if (preisfaktor != 0) preis *= preisfaktor;
            if (technikUndMaterial) preis += _selectedFernkampfwaffeMaterial.PreisProUnzeInSilber * _selectedFernkampfwaffe.Gewicht;
            _erstellteFernkampfwaffe.Preis = preis;
            if (_selectedNahkampfwaffe != null && _erstellteNahkampfwaffe != null)
            {
                _erstellteNahkampfwaffe.BF = _selectedNahkampfwaffe.BF + bfVerbesserung + (technikUndMaterial ? _selectedFernkampfwaffeMaterial.BfVerbesserung + _selectedFernkampfwaffeTechnik.BfVerbesserung : 0);
                _erstellteNahkampfwaffe.TPBonus = _selectedNahkampfwaffe.TPBonus + tpVerbesserung + (technikUndMaterial ? _selectedFernkampfwaffeMaterial.TpVerbesserung + _selectedFernkampfwaffeTechnik.TpVerbesserung : 0);
                _erstellteNahkampfwaffe.WMAT = _selectedNahkampfwaffe.WMAT + atWmVerbesserung + (technikUndMaterial ? _selectedFernkampfwaffeMaterial.AtWmVerbesserung + _selectedFernkampfwaffeTechnik.AtWmVerbesserung : 0);
                _erstellteNahkampfwaffe.WMPA = _selectedNahkampfwaffe.WMPA + paWmVerbesserung + (technikUndMaterial ? _selectedFernkampfwaffeMaterial.PaWmVerbesserung + _selectedFernkampfwaffeTechnik.PaWmVerbesserung : 0);
                _erstellteNahkampfwaffe.INI = _selectedNahkampfwaffe.INI + iniVerbesserung + (technikUndMaterial ? _selectedFernkampfwaffeMaterial.IniVerbesserung + _selectedFernkampfwaffeTechnik.IniVerbesserung : 0);
                _erstellteNahkampfwaffe.Preis = preis;
            }
            OnChanged("ErstellteFernkampfwaffe");
            OnChanged("ErstellteNahkampfwaffe");
            BerechneNicwinscheApproximation();
        }
        #endregion

        #region //---- EVENTS ----

        #endregion

    }
}
