using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KampfLogic = MeisterGeister.ViewModel.Kampf.Logic;
using MeisterGeister.Model.Extensions;
using Mod = MeisterGeister.ViewModel.Kampf.Logic.Modifikatoren;
using MeisterGeister.ViewModel.Kampf.Logic;

namespace MeisterGeister.Model
{
    public partial class Gegner : KampfLogic.Wesen, KampfLogic.IKämpfer, KampfLogic.IGegnerBase, Extensions.IInitializable, KampfLogic.IHasZonenRs
    {
        public Gegner() : base()
        {
            GegnerGUID = Guid.NewGuid();
            PropertyChanged += DependentProperty.PropagateINotifyProperyChanged;
        }

        public Gegner(GegnerBase gegnerBase) : this()
        {
            GegnerBaseGUID = gegnerBase.GegnerBaseGUID;
            GegnerBase = gegnerBase;
            AE = gegnerBase.AE;
            AstralenergieAktuell = AstralenergieMax;
            AU = gegnerBase.AU;
            AusdauerAktuell = AusdauerMax;
            LE = gegnerBase.LE;
            LebensenergieAktuell = LebensenergieMax;
            KE = gegnerBase.KE;
            KarmaenergieAktuell = KarmaenergieMax;
            INIBasis = gegnerBase.INIBasis;
            KO = gegnerBase.KO;
            MRGeist = gegnerBase.MRGeist;
            MRKörper = gegnerBase.MRKörper;
            RSArmL = gegnerBase.RSArmL;
            RSArmR = gegnerBase.RSArmR;
            RSBauch = gegnerBase.RSBauch;
            RSBeinL = gegnerBase.RSBeinL;
            RSBeinR = gegnerBase.RSBeinR;
            RSBrust = gegnerBase.RSBrust;
            RSKopf = gegnerBase.RSKopf;
            RSRücken = gegnerBase.RSRücken;
            Name = gegnerBase.Name;
            Bild = gegnerBase.Bild;
            Bemerkung = gegnerBase.Bemerkung;
        }

        #region IInitializable
        public void Initialize()
        {
            //Angriffsaktionen = Aktionen - Abwehraktionen;
        }
        #endregion

        #region IKämpfer
        public int Initiative()
        {
            return INIBasis + Logic.General.Würfel.Parse(INIZufall);
        }

        public int InitiativeMax()
        {
            return INIBasis + Logic.General.Würfel.Parse(INIZufall, false);
        }

        [DependentProperty("INIBasis")]
        public int InitiativeBasis
        {
            get { return INIBasis; }
        }

        public string Position
        {
            get { throw new NotImplementedException(); }
        }

        [DependentProperty("KO")]
        public int Körperkraft
        {
            get { return KO; }
        }

        [DependentProperty("KO")]
        public int Gewandtheit
        {
            get { return KO; }
        }

        [DependentProperty("KO")]
        public int Konstitution
        {
            get { return KO; }
        }

        //return GS abhängig vom Modus (fliegend, am boden, galopp, etc.)
        [DependentProperty("GS")]
        public int Geschwindigkeit
        {
            get { return GS; }
        }

        [DependentProperty("LE")]
        public int LebensenergieMax
        {
            get { return LE;  }
        }

        [DependentProperty("LEAktuell")]
        public int LebensenergieAktuell
        {
            get
            {
                return LEAktuell;
            }
            set
            {
                LEAktuell = value;
            }
        }

        [DependentProperty("LebensenergieAktuell"), DependentProperty("LebensenergieMax"), DependentProperty("Konstitution")]
        public string LebensenergieStatus
        {
            get
            {
                return GetLebensenergieStatus();
            }
        }
        
        [DependentProperty("AU")]
        public int AusdauerMax
        {
            get { return AU; }
        }

        [DependentProperty("AUAktuell")]
        public int AusdauerAktuell
        {
            get
            {
                return AUAktuell;
            }
            set
            {
                AUAktuell = value;
            }
        }

        [DependentProperty("AusdauerAktuell"), DependentProperty("AusdauerMax")]
        public string AusdauerStatus
        {
            get
            {
                return GetAusdauerStatus();
            }
        }

        [DependentProperty("AE")]
        public int AstralenergieMax
        {
            get { return AE; }
        }

        [DependentProperty("AEAktuell")]
        public int AstralenergieAktuell
        {
            get
            {
                return AEAktuell;
            }
            set
            {
                AEAktuell = value;
            }
        }

        public int KarmaenergieMax
        {
            get { return 0; }
        }

        public int KarmaenergieAktuell
        {
            get
            {
                return 0;
            }
            set {}
        }

        public int? AT
        {
            get {
                //TODO JT: stattdessen selectedangriff verwenden
                GegnerBase_Angriff ga = GegnerBase.GegnerBase_Angriff.FirstOrDefault();
                return (ga == null)?0:ga.AT; 
            }
        }

        int? KampfLogic.IKämpfer.PA
        {
            get { return PA; }
        }

        [DependentProperty("MRKörper")]
        public int MR
        {
            get { return MRKörper ?? 0; }
        }

        private Rüstungsschutz _rs = null;
        public IRüstungsschutz RS
        {
            get
            {
                if (_rs == null)
                    _rs = new Rüstungsschutz((Model.Gegner)this);
                return _rs;
            }
        }

        [DependentProperty("PA")]
        public int? Ausweichen
        {
            get { return ((KampfLogic.IKämpfer)this).PA; }
        }

        public int? BE
        {
            get { return 0; }
        }

        [DependentProperty("Konstitution")]
        public int Wundschwelle
        {
            get { return (int)Math.Round(Konstitution / 2.0, MidpointRounding.AwayFromZero); }
        }

        [DependentProperty("Konstitution")]
        public int Wundschwelle2
        {
            get { return Konstitution; }
        }

        [DependentProperty("Konstitution")]
        public int Wundschwelle3
        {
            get { return (int)Math.Round(Konstitution * 1.5, MidpointRounding.AwayFromZero); }
        }

        private Wunden kämpferWunden = null;
        public IWunden WundenByZone
        {
            get
            {
                if (kämpferWunden == null)
                    kämpferWunden = new KampfLogic.Wunden((Model.Gegner)this);
                return kämpferWunden;
            }
        }

        IWunden IKämpfer.Wunden
        {
            get
            {
                return WundenByZone;
            }
        }

        public List<KampfLogic.Manöver.Manöver> Manöver
        {
            get { throw new NotImplementedException(); }
        }

        public IList<KampfLogic.IWaffe> Angriffswaffen
        {
            get { return GegnerBase.GegnerBase_Angriff.Select(ga => (KampfLogic.IWaffe)ga).ToList(); }
        }

        public bool Magiebegabt
        {
            get { return AstralenergieMax > 0; }
        }

        public bool Geweiht
        {
            get { return KarmaenergieMax > 0; }
        }

        private System.Windows.Media.Brush _farbmarkierung = System.Windows.Media.Brushes.Transparent;
        public System.Windows.Media.Brush Farbmarkierung
        {
            get { return _farbmarkierung; }
            set { _farbmarkierung = value; OnChanged("Farbmarkierung"); }
        }

        #endregion

        public override string ToString()
        {
            return Name;
        }

        #region IGegnerBase
        public string INIZufall
        {
            get
            {
                return GegnerBase.INIZufall;
            }
        }

        private int _aktionen = Int32.MinValue;
        public int Aktionen
        {
            get
            {
                if(_aktionen == Int32.MinValue)
                    _aktionen = GegnerBase.Aktionen;
                return _aktionen;
            }
            set
            {
                _aktionen = value;
                OnChanged("Aktionen");
            }
        }

        public int GS
        {
            get
            {
                return GegnerBase.GS;
            }
        }

        public int? GS2
        {
            get
            {
                return GegnerBase.GS2;
            }
        }

        public int? GS3
        {
            get
            {
                return GegnerBase.GS3;
            }
        }

        public int? GW
        {
            get
            {
                return GegnerBase.GW;
            }
        }

        public int? Jagd
        {
            get
            {
                return GegnerBase.Jagd;
            }
        }

        public int? Beschwörung
        {
            get
            {
                return GegnerBase.Beschwörung;
            }
        }

        public int? Kontrolle
        {
            get
            {
                return GegnerBase.Kontrolle;
            }
        }

        public int? Beschwörungskosten
        {
            get
            {
                return GegnerBase.Beschwörungskosten;
            }
        }

        public string Tags
        {
            get
            {
                return GegnerBase.Tags;
            }
        }

        public string Literatur
        {
            get
            {
                return GegnerBase.Literatur;
            }
        }

        public string Setting
        {
            get
            {
                return GegnerBase.Setting;
            }
        }
        #endregion
    }
}
