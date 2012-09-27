using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KampfLogic = MeisterGeister.ViewModel.Kampf.Logic;
using MeisterGeister.Model.Extensions;

namespace MeisterGeister.Model
{
    public partial class Gegner : KampfLogic.Wesen, KampfLogic.IKämpfer, KampfLogic.IGegnerBase, Extensions.IInitializable
    {
        public Gegner()
        {
            GegnerGUID = Guid.NewGuid();
            PropertyChanged += DependentProperty.PropagateINotifyProperyChanged;
        }

        #region IInitializable
        public void Initialize()
        {
            Angriffsaktionen = Aktionen - Abwehraktionen;
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

        public int LebensenergieAktuell
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        // TODO ??: Property implementieren (siehe: ViewModel.Kampf.LogicAlt.Wesen)
        public string LebensenergieStatus
        {
            get { throw new NotImplementedException(); }
        }
        // TODO ??: Property implementieren (siehe: ViewModel.Kampf.LogicAlt.Wesen)
        public string LebensenergieStatusDetails
        {
            get { throw new NotImplementedException(); }
        }

        [DependentProperty("AU")]
        public int AusdauerMax
        {
            get { return AU; }
        }

        public int AusdauerAktuell
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        // TODO ??: Property implementieren (siehe: ViewModel.Kampf.LogicAlt.Wesen)
        public string AusdauerStatus
        {
            get { throw new NotImplementedException(); }
        }
        // TODO ??: Property implementieren (siehe: ViewModel.Kampf.LogicAlt.Wesen)
        public string AusdauerStatusDetails
        {
            get { throw new NotImplementedException(); }
        }

        [DependentProperty("AE")]
        public int AstralenergieMax
        {
            get { return AE; }
        }

        public int AstralenergieAktuell
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
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
            get { throw new NotImplementedException(); }
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

        public KampfLogic.IRüstungsschutz RS
        {
            get { return null;  }
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

        KampfLogic.IWunden KampfLogic.IKämpfer.Wunden
        {
            get { throw new NotImplementedException(); }
        }


        //beschreibbar, da es von der INI abhängt. Die Initiative wird in Kampf gespeichert und verwaltet.
        private int _freieAktionen = 2;
        public int FreieAktionen
        {
            get
            {
                return _freieAktionen;
            }
            set
            {
                _freieAktionen = value;
            }
        }

        private int _angriffsaktionen = 1;

        public int Angriffsaktionen
        {
            get { return _angriffsaktionen; }
            set { _angriffsaktionen = value; }
        }

        private int _abwehraktionen = 1;

        public int Abwehraktionen
        {
            get { return _abwehraktionen; }
            set { _abwehraktionen = value; }
        }


        private int _verbrauchteAngriffsaktionen = 0;

        public int VerbrauchteAngriffsaktionen
        {
            get { return _verbrauchteAngriffsaktionen; }
            set { _verbrauchteAngriffsaktionen = value; }
        }

        private int _verbrauchteAbwehraktionen = 0;

        public int VerbrauchteAbwehraktionen
        {
            get { return _verbrauchteAbwehraktionen; }
            set { _verbrauchteAbwehraktionen = value; }
        }

        private int _verbrauchteFreieAktionen = 0;

        public int VerbrauchteFreieAktionen
        {
            get { return _verbrauchteFreieAktionen; }
            set { _verbrauchteFreieAktionen = value; }
        }

        public KampfLogic.Kampfstil Kampfstil
        {
            get { return KampfLogic.Kampfstil.Keiner; }
            set { }
        }

        public KampfLogic.WaffenloserKampfstil WaffenloserKampfstil
        {
            get { return KampfLogic.WaffenloserKampfstil.Raufen; }
            set { }
        }

        public List<KampfLogic.Manöver.Manöver> Manöver
        {
            get { throw new NotImplementedException(); }
        }

        public IList<KampfLogic.IWaffe> Angriffswaffen
        {
            get { return GegnerBase.GegnerBase_Angriff.Select(ga => (KampfLogic.IWaffe)ga).ToList(); }
        }
        #endregion

        public override string ToString()
        {
            return Name;
        }

        #region IGegnerBase
        public int INIBasis
        {
            get
            {
                return GegnerBase.INIBasis;
            }
        }

        public string INIZufall
        {
            get
            {
                return GegnerBase.INIZufall;
            }
        }

        public int Aktionen
        {
            get
            {
                return GegnerBase.Aktionen;
            }
        }

        public int PA
        {
            get
            {
                return GegnerBase.PA;
            }
        }

        public int LE
        {
            get
            {
                return GegnerBase.LE;
            }
        }

        public int AU
        {
            get
            {
                return GegnerBase.AU;
            }
        }

        public int AE
        {
            get
            {
                return GegnerBase.AE;
            }
        }

        public int KE
        {
            get
            {
                return GegnerBase.KE;
            }
        }

        public int KO
        {
            get
            {
                return GegnerBase.KO;
            }
        }

        public int MRGeist
        {
            get
            {
                return GegnerBase.MRGeist;
            }
        }

        public int? MRKörper
        {
            get
            {
                return GegnerBase.MRKörper;
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

        public int RSKopf
        {
            get
            {
                return GegnerBase.RSKopf;
            }
        }

        public int RSBrust
        {
            get
            {
                return GegnerBase.RSBrust;
            }
        }

        public int RSRücken
        {
            get
            {
                return GegnerBase.RSRücken;
            }
        }

        public int RSArmL
        {
            get
            {
                return GegnerBase.RSArmL;
            }
        }

        public int RSArmR
        {
            get
            {
                return GegnerBase.RSArmR;
            }
        }

        public int RSBauch
        {
            get
            {
                return GegnerBase.RSBauch;
            }
        }

        public int RSBeinL
        {
            get
            {
                return GegnerBase.RSBeinL;
            }
        }

        public int RSBeinR
        {
            get
            {
                return GegnerBase.RSBeinR;
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
