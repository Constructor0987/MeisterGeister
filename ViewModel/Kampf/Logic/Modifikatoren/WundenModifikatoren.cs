using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.ViewModel.Kampf.Logic.Modifikatoren
{
    public class WundenModifikatorBase : Modifikator
    {
        public override string Name
        {
            get { return "Wunde"; }
        }

        public override string Literatur
        {
            get { return "WdS 108"; }
        }

        /// <summary>
        /// Gibt den Typ des Modifikators anhand des Namens der Property auf dem Helden zurück.
        /// </summary>
        public static Type TypByZone(Trefferzone zone)
        {
            switch (zone)
            {
                case Trefferzone.Kopf:
                    return typeof(WundenKopfModifikator);
                case Trefferzone.Brust:
                case Trefferzone.Rücken:
                    return typeof(WundenBrustModifikator);
                case Trefferzone.ArmL:
                    return typeof(WundenArmLModifikator);
                case Trefferzone.ArmR:
                    return typeof(WundenArmRModifikator);
                case Trefferzone.Bauch:
                    return typeof(WundenBauchModifikator);
                case Trefferzone.BeinL:
                    return typeof(WundenBeinLModifikator);
                case Trefferzone.BeinR:
                    return typeof(WundenBeinRModifikator);
                case Trefferzone.Zufall:
                    return WundenModifikatorBase.TypByZone(TrefferzonenHelper.ZufallsZone());
                case Trefferzone.Unlokalisiert:
                case Trefferzone.Gesamt:
                default:
                    return typeof(WundenModifikator);
            }
        }

    }

    public class WundenModifikator : WundenModifikatorBase, IModATBasis, IModPABasis, IModFKBasis, IModINIBasis, IModGE, IModGS
    {
        public override string Literatur
        {
            get { return "WdS 57"; }
        }

        public override string Auswirkung
        {
            get { return "AT/PA/FK/INI-Basis, GE -2; GE -1"; }
        }

        public int ApplyATBasisMod(int wert)
        {
            return wert - 2;
        }

        public int ApplyPABasisMod(int wert)
        {
            return wert - 2;
        }

        public int ApplyFKBasisMod(int wert)
        {
            return wert - 2;
        }

        public int ApplyINIBasisMod(int wert)
        {
            return wert - 2;
        }

        public int ApplyGEMod(int wert)
        {
            return wert - 2;
        }

        public int ApplyGSMod(int wert)
        {
            return Math.Max(wert - 1, 1);
        }
    }

    public class WundenKopfModifikator : WundenModifikatorBase, IModMU, IModKL, IModIN, IModINIBasis
    {
        public override string Name
        {
            get { return "Wunde am Kopf"; }
        }

        public override string Auswirkung
        {
            get { return "MU, KL, IN, INI-Basis -2"; }
        }

        public int ApplyMUMod(int wert)
        {
            return wert - 2;
        }

        public int ApplyKLMod(int wert)
        {
            return wert + 2;
        }

        public int ApplyINMod(int wert)
        {
            return wert - 2;
        }

        public int ApplyINIBasisMod(int wert)
        {
            return wert - 2;
        }
    }

    public class WundenBrustModifikator : WundenModifikatorBase, IModPA, IModAT, IModKO, IModKK
    {
        public override string Name
        {
            get { return "Wunde an der Brust"; }
        }

        public override string Auswirkung
        {
            get { return "AT, PA, KO, KK -1"; }
        }

        public int ApplyPAMod(int wert)
        {
            return wert - 1;
        }

        public int ApplyATMod(int wert)
        {
            return wert - 1;
        }

        public int ApplyKOMod(int wert)
        {
            return wert - 1;
        }

        public int ApplyKKMod(int wert)
        {
            return wert - 1;
        }
    }

    public class WundenArmLModifikator : WundenModifikatorBase, IModPAArmL, IModATArmL, IModFFArmL, IModKKArmL
    {
        public override string Name
        {
            get { return "Wunde am linken Arm (Schildarm)"; }
        }

        public override string Auswirkung
        {
            get { return "AT, PA, FF, KK -2"; }
        }

        public int ApplyPAMod(int wert)
        {
            return wert - 2;
        }

        public int ApplyATMod(int wert)
        {
            return wert - 2;
        }

        public int ApplyFFMod(int wert)
        {
            return wert - 2;
        }

        public int ApplyKKMod(int wert)
        {
            return wert - 2;
        }
    }

    public class WundenArmRModifikator : WundenModifikatorBase, IModPAArmR, IModATArmR, IModFFArmR, IModKKArmR
    {
        public override string Name
        {
            get { return "Wunde am rechten Arm (Waffenarm)"; }
        }

        public override string Auswirkung
        {
            get { return "AT, PA, FF, KK -2"; }
        }

        public int ApplyPAMod(int wert)
        {
            return wert - 2;
        }

        public int ApplyATMod(int wert)
        {
            return wert - 2;
        }

        public int ApplyFFMod(int wert)
        {
            return wert - 2;
        }

        public int ApplyKKMod(int wert)
        {
            return wert - 2;
        }
    }

    public class WundenBauchModifikator : WundenModifikatorBase, IModPA, IModAT, IModKO, IModKK, IModGS, IModINIBasis
    {
        public override string Name
        {
            get { return "Wunde am Bauch"; }
        }

        public override string Auswirkung
        {
            get { return "AT, PA, KO, KK, GS, INI-Basis -1"; }
        }

        public int ApplyPAMod(int wert)
        {
            return wert - 1;
        }

        public int ApplyATMod(int wert)
        {
            return wert - 1;
        }

        public int ApplyKOMod(int wert)
        {
            return wert - 1;
        }

        public int ApplyKKMod(int wert)
        {
            return wert - 1;
        }

        public int ApplyGSMod(int wert)
        {
            return Math.Max(wert - 1, 1);
        }

        public int ApplyINIBasisMod(int wert)
        {
            return wert - 1;
        }
    }

    public class WundenBeinLModifikator : WundenModifikatorBase, IModPA, IModAT, IModGE, IModGS, IModINIBasis
    {
        public override string Name
        {
            get { return "Wunde am linken Bein"; }
        }

        public override string Auswirkung
        {
            get { return "AT, PA, GE, INI-Basis -2; GS -1"; }
        }

        public int ApplyPAMod(int wert)
        {
            return wert - 2;
        }

        public int ApplyATMod(int wert)
        {
            return wert - 2;
        }

        public int ApplyGEMod(int wert)
        {
            return wert - 2;
        }

        public int ApplyGSMod(int wert)
        {
            return Math.Max(wert - 1, 1);
        }

        public int ApplyINIBasisMod(int wert)
        {
            return wert - 2;
        }
    }

    public class WundenBeinRModifikator : WundenModifikatorBase, IModPA, IModAT, IModGE, IModGS, IModINIBasis
    {
        public override string Name
        {
            get { return "Wunde am rechten Bein"; }
        }

        public override string Auswirkung
        {
            get { return "AT, PA, GE, INI-Basis -2; GS -1"; }
        }

        public int ApplyPAMod(int wert)
        {
            return wert - 2;
        }

        public int ApplyATMod(int wert)
        {
            return wert - 2;
        }

        public int ApplyGEMod(int wert)
        {
            return wert - 2;
        }

        public int ApplyGSMod(int wert)
        {
            return Math.Max(wert - 1, 1);
        }

        public int ApplyINIBasisMod(int wert)
        {
            return wert - 2;
        }
    }

    
}
