using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.ViewModel.Kampf.LogicAlt
{
    public class Trefferzone
    {
        public static List<string> TrefferzonenListe()
        {
            List<string> trefferzonenList = new List<string>();
            trefferzonenList.AddRange(new string[] { "Unlokalisiert", "Kopf", "Brust", "Rücken", "Arm links", "Arm rechts", "Bauch", "Bein links", "Bein rechts" });
            return trefferzonenList;
        }

        public static TrefferzoneEnum GetTrefferzoneEnum(string trefferzone)
        {
            switch (trefferzone)
            {
                case "Unlokalisiert":
                    return TrefferzoneEnum.Unlokalisiert;
                case "Zufall":
                    return TrefferzoneEnum.Zufall;
                case "Kopf":
                    return TrefferzoneEnum.Kopf;
                case "Brust":
                    return TrefferzoneEnum.Brust;
                case "Rücken":
                    return TrefferzoneEnum.Rücken;
                case "Arm links":
                    return TrefferzoneEnum.ArmL;
                case "Arm rechts":
                    return TrefferzoneEnum.ArmR;
                case "Bauch":
                    return TrefferzoneEnum.Bauch;
                case "Bein links":
                    return TrefferzoneEnum.BeinL;
                case "Bein rechts":
                    return TrefferzoneEnum.BeinR;
                default:
                    return TrefferzoneEnum.Zufall;
            }
        }
    }
}
