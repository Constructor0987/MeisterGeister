using System;
using System.ComponentModel;
// Eigene Usings
using MeisterGeister.Logic.General;

namespace MeisterGeister.ViewModel.Kampf.LogicAlt
{
    public class Gegner : Wesen, IKämpfer
    {
        #region // Konstruktor

        public Gegner(string name, int ini, int le, int au = 0
            , int ae = 0)
        {
            Name = name;
            InitiativeBasis = ini;
            LebensenergieMax = le;
            LebensenergieAktuell = le;
            AusdauerMax = au;
            AusdauerAktuell = au;
            AstralenergieMax = ae;
            AstralenergieAktuell = ae;
        }

        #endregion

        #region // Allgemeine Eigenschaften

        private string _name;
        public override string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged("Name"); }
        }

        private int _parade;
        public override int Parade
        {
            get { return _parade; }
            set { _parade = value; OnPropertyChanged("Parade"); }
        }

        private string _besonderheiten;
        public string Besonderheiten
        {
            get { return _besonderheiten; }
            set { _besonderheiten = value; OnPropertyChanged("Besonderheiten"); }
        }

        private string _kampfwerte;
        public string Kampfwerte
        {
            get { return _kampfwerte; }
            set { _kampfwerte = value; OnPropertyChanged("Kampfwerte"); }
        }

        #endregion

        #region // Initiative

        public string InitiativeInfo
        {
            get
            {
                return string.Format("INI: {0}, Basis {1}", Initiative, InitiativeBasis);
            }
        }

        public string InitiativeInfoDetails
        {
            get
            {
                return string.Format("INI: {0}\nINI-Basis ({1}) + INI-Wurf ({2}) + INI-Modifikator ({3})",
                    Initiative, InitiativeBasis, (int)InitiativeWurf, InitiativeMod);
            }
        }

        public override WürfelEnum InitiativeZufall
        {
            get
            {
                //TODO ??: INI-Würfel
                return WürfelEnum._1W6;
            }
        }

        public void InitiativeWürfeln()
        {
            InitiativeWurf = new W6().Würfeln();
        }

        #endregion

        #region // Lebensenergie, Ausdauer, Astralenergie, Karmaenergie

        private int _lebensenergieAktuell;
        public override int LebensenergieAktuell
        {
            get { return _lebensenergieAktuell; }
            set { _lebensenergieAktuell = value; OnPropertyChanged(string.Empty); }
        }

        private int _ausdauerMax;
        public override int AusdauerMax
        {
            get { return _ausdauerMax; }
            set { _ausdauerMax = value; OnPropertyChanged("AusdauerMax"); }
        }

        private int _ausdauerAktuell;
        public override int AusdauerAktuell
        {
            get { return _ausdauerAktuell; }
            set { _ausdauerAktuell = value; OnPropertyChanged(string.Empty); }
        }

        private int _lebensenergieMax;
        public override int LebensenergieMax
        {
            get { return _lebensenergieMax; }
            set { _lebensenergieMax = value; OnPropertyChanged("LebensenergieMax"); }
        }

        private int _astralenergieMax;
        public int AstralenergieMax
        {
            get { return _astralenergieMax; }
            set { _astralenergieMax = value; OnPropertyChanged("AstralenergieMax"); }
        }

        private int _astralenergieAktuell;
        public override int AstralenergieAktuell
        {
            get { return _astralenergieAktuell; }
            set { _astralenergieAktuell = value; OnPropertyChanged(string.Empty); }
        }

        private int _karmaenergieMax;
        public int KarmaenergieMax
        {
            get { return _karmaenergieMax; }
            set { _karmaenergieMax = value; OnPropertyChanged("KarmaenergieMax"); }
        }

        private int _karmaenergieAktuell;
        public override int KarmaenergieAktuell
        {
            get { return _karmaenergieAktuell; }
            set { _karmaenergieAktuell = value; OnPropertyChanged(string.Empty); }
        }

        #endregion

        #region // Konstitution, Wundschwellen

        public string Wundschwellen
        {
            get
            {
                return string.Format("{0} / {1} / {2}", Wundschwelle, Wundschwelle2, Wundschwelle3);
            }
        }

        public override int Wundschwelle
        {
            get
            {
                if (KO == 0)
                {
                    return Convert.ToInt32(Math.Round(LebensenergieMax / 5.0, 0, MidpointRounding.AwayFromZero));
                }
                int ko = KO;
                ko += ModKO; // Abzüger durch Wunden
                return Convert.ToInt32(Math.Round(ko / 2.0, 0, MidpointRounding.AwayFromZero));
            }
        }

        public override int Wundschwelle2
        {
            get
            {
                if (KO == 0)
                {
                    return Convert.ToInt32(Math.Round(LebensenergieMax / 3.0, 0, MidpointRounding.AwayFromZero));
                }
                int ko = KO;
                ko += ModKO; // Abzüger durch Wunden
                return ko;
            }
        }

        public override int Wundschwelle3
        {
            get
            {
                if (KO == 0)
                {
                    return Wundschwelle * 3;
                }
                int ko = KO;
                ko += ModKO; // Abzüger durch Wunden
                return Convert.ToInt32(Math.Round(ko * 1.5, 0, MidpointRounding.AwayFromZero));
            }
        }

        #endregion
        
        #region // Rüstungsschutz

        public override int RüstungsschutzGesamt
        {
            get
            {
                return (int)Math.Round(BerechneRüstungsschutzGesamt(), 0, MidpointRounding.AwayFromZero);
            }
            set
            {
                SetRüstungsschutz(value); OnPropertyChanged(string.Empty); 
            }
        }

        private int _rüstungsschutzKopf;
        public override int RüstungsschutzKopf
        {
            get { return _rüstungsschutzKopf; }
            set { _rüstungsschutzKopf = value; OnPropertyChanged(string.Empty); }
        }

        private int _rüstungsschutzBrust;
        public override int RüstungsschutzBrust
        {
            get { return _rüstungsschutzBrust; }
            set { _rüstungsschutzBrust = value; OnPropertyChanged(string.Empty); }
        }

        private int _rüstungsschutzRücken;
        public override int RüstungsschutzRücken
        {
            get { return _rüstungsschutzRücken; }
            set { _rüstungsschutzRücken = value; OnPropertyChanged(string.Empty); }
        }

        private int _rüstungsschutzArmL;
        public override int RüstungsschutzArmL
        {
            get { return _rüstungsschutzArmL; }
            set { _rüstungsschutzArmL = value; OnPropertyChanged(string.Empty); }
        }

        private int _rüstungsschutzArmR;
        public override int RüstungsschutzArmR
        {
            get { return _rüstungsschutzArmR; }
            set { _rüstungsschutzArmR = value; OnPropertyChanged(string.Empty); }
        }

        private int _rüstungsschutzBauch;
        public override int RüstungsschutzBauch
        {
            get { return _rüstungsschutzBauch; }
            set { _rüstungsschutzBauch = value; OnPropertyChanged(string.Empty); }
        }

        private int _rüstungsschutzBeinL;
        public override int RüstungsschutzBeinL
        {
            get { return _rüstungsschutzBeinL; }
            set { _rüstungsschutzBeinL = value; OnPropertyChanged(string.Empty); }
        }

        private int _rüstungsschutzBeinR;
        public override int RüstungsschutzBeinR
        {
            get { return _rüstungsschutzBeinR; }
            set { _rüstungsschutzBeinR = value; OnPropertyChanged(string.Empty); }
        }

        public override int Behinderung { get { return 0; } set { ;} }

        #endregion

        #region // VorNachteile, Sonderfertigkeiten

        public override bool Magiebegabt
        {
            get
            {
                return AstralenergieMax > 0;
            }
        }

        public override bool Geweiht
        {
            get
            {
                return KarmaenergieMax > 0;
            }
        }

        public override bool HatAufmerksamkeit
        {
            get
            {
                return HatSonderfertigkeit("Aufmerksamkeit");
            }
        }

        public override bool HatVoraussetzungenAufmerksamkeit
        {
            get
            {
                return true;
            }
        }

        private bool HatSonderfertigkeit(string sf)
        {
            return Sonderfertigkeiten.Contains(sf);
        }

        private string _sonderfertigkeiten;
        public string Sonderfertigkeiten
        {
            get { return _sonderfertigkeiten; }
            set { _sonderfertigkeiten = value; OnPropertyChanged("Sonderfertigkeiten"); }
        }

        #endregion

    }
}
