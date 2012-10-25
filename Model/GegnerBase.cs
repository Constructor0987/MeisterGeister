using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KampfLogic = MeisterGeister.ViewModel.Kampf.Logic;

namespace MeisterGeister.Model
{
    public partial class GegnerBase : KampfLogic.IHasZonenRs
    {
        public GegnerBase()
        {
            GegnerBaseGUID = Guid.NewGuid();
            Name = "Neuer Gegner";
            INIZufall = "1W6";
        }

        public bool Usergenerated
        {
            get { return !GegnerBaseGUID.ToString().StartsWith("00000000-0000-0000-00"); }
        }

        #region Import Export

        public static GegnerBase Import(string pfad, bool batch = false)
        {
            return Import(pfad, Guid.Empty, batch);
        }

        public static GegnerBase Import(string pfad, Guid newGuid, bool batch = false)
        {
            Service.SerializationService serialization = Service.SerializationService.GetInstance(!batch);
            Guid gegnerGuid = serialization.ImportGegner(pfad, newGuid);
            if (gegnerGuid == Guid.Empty)
                return null;
            Global.ContextKampf.UpdateList<GegnerBase>();
            return Global.ContextKampf.Liste<GegnerBase>().Where(g => g.GegnerBaseGUID == gegnerGuid).First();
        }

        public void Export(string pfad, bool batch = false)
        {
            Service.SerializationService serialization = Service.SerializationService.GetInstance(!batch);
            serialization.ExportGegner(GegnerBaseGUID, pfad);
        }

        public GegnerBase Clone(bool batch = false)
        {
            return Clone(Guid.NewGuid(), batch);
        }

        public GegnerBase Clone(Guid newGuid, bool batch = false)
        {
            Service.SerializationService serialization = Service.SerializationService.GetInstance(!batch);
            Guid gegnerGuid = serialization.CloneGegner(GegnerBaseGUID, newGuid);
            if (gegnerGuid == Guid.Empty)
                return null;
            Global.ContextHeld.UpdateList<GegnerBase>();
            return Global.ContextHeld.Liste<GegnerBase>().Where(h => h.GegnerBaseGUID == gegnerGuid).FirstOrDefault();
        }
        #endregion

        private KampfLogic.Rüstungsschutz _rs = null;
        public KampfLogic.IRüstungsschutz RS
        {
            get
            {
                if (_rs == null)
                    _rs = new KampfLogic.Rüstungsschutz((GegnerBase)this);
                return _rs;
            }
        }

        #region IHasZonenRs
        int? KampfLogic.IHasZonenRs.RSKopf
        {
            get
            {
                return RSKopf;
            }
            set
            {
                RSKopf = value ?? 0;
            }
        }

        int? KampfLogic.IHasZonenRs.RSBrust
        {
            get
            {
                return RSBrust;
            }
            set
            {
                RSBrust = value ?? 0;
            }
        }

        int? KampfLogic.IHasZonenRs.RSRücken
        {
            get
            {
                return RSRücken;
            }
            set
            {
                RSRücken = value ?? 0;
            }
        }

        int? KampfLogic.IHasZonenRs.RSArmL
        {
            get
            {
                return RSArmL;
            }
            set
            {
                RSArmL = value ?? 0;
            }
        }

        int? KampfLogic.IHasZonenRs.RSArmR
        {
            get
            {
                return RSArmR;
            }
            set
            {
                RSArmR = value ?? 0;
            }
        }

        int? KampfLogic.IHasZonenRs.RSBauch
        {
            get
            {
                return RSBauch;
            }
            set
            {
                RSBauch = value ?? 0;
            }
        }

        int? KampfLogic.IHasZonenRs.RSBeinL
        {
            get
            {
                return RSBeinL;
            }
            set
            {
                RSBeinL = value ?? 0;
            }
        }

        int? KampfLogic.IHasZonenRs.RSBeinR
        {
            get
            {
                return RSBeinR;
            }
            set
            {
                RSBeinR = value ?? 0;
            }
        }
        #endregion
    }
}
