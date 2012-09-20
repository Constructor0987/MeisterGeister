using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.ViewModel.Kampf.Logic
{
    /// <summary>
    /// Wunden abhängig vom Trefferzonen-Enum
    /// </summary>
    public class Wunden : IWunden
    {
        private Model.Held _held;
        public Wunden(Model.Held held)
        {
            _held = held;
        }

        public static Trefferzone GetTrefferZoneByPropertyName(string wundenProperty)
        {
            switch (wundenProperty)
            {
                case "WundenKopf":
                    return Trefferzone.Kopf;
                case "WundenBrust":
                    return Trefferzone.Brust;
                case "WundenArmL":
                    return Trefferzone.ArmL;
                case "WundenArmR":
                    return Trefferzone.ArmR;
                case "WundenBauch":
                    return Trefferzone.Bauch;
                case "WundenBeinL":
                    return Trefferzone.BeinL;
                case "WundenBeinR":
                    return Trefferzone.BeinR;
                case "Wunden":
                default:
                    return Trefferzone.Unlokalisiert;
            }
        }

        public int this[Trefferzone zone]
        {
            get
            {
                switch (zone)
                {
                    case Trefferzone.Kopf:
                        return _held.WundenKopf ?? 0;
                    case Trefferzone.Brust:
                    case Trefferzone.Rücken:
                        return _held.WundenBrust ?? 0;
                    case Trefferzone.ArmL:
                        return _held.WundenArmL ?? 0;
                    case Trefferzone.ArmR:
                        return _held.WundenArmR ?? 0;
                    case Trefferzone.Bauch:
                        return _held.WundenBauch ?? 0;
                    case Trefferzone.BeinL:
                        return _held.WundenBeinL ?? 0;
                    case Trefferzone.BeinR:
                        return _held.WundenBeinR ?? 0;
                    case Trefferzone.Unlokalisiert:
                        return _held.Wunden ?? 0;
                    case Trefferzone.Zufall:
                        return this[TrefferzonenHelper.ZufallsZone()];
                    case Trefferzone.Gesamt:
                    default:
                        return _held.Wunden ?? 0;
                }
            }
            set
            {
                switch (zone)
                {
                    case Trefferzone.Kopf:
                        _held.WundenKopf = value;
                        break;
                    case Trefferzone.Rücken:
                    case Trefferzone.Brust:
                        _held.WundenBrust = value;
                        break;
                    case Trefferzone.ArmL:
                        _held.WundenArmL = value;
                        break;
                    case Trefferzone.ArmR:
                        _held.WundenArmR = value;
                        break;
                    case Trefferzone.Bauch:
                        _held.WundenBauch = value;
                        break;
                    case Trefferzone.BeinL:
                        _held.WundenBeinL = value;
                        break;
                    case Trefferzone.BeinR:
                        _held.WundenBeinR = value;
                        break;
                    case Trefferzone.Unlokalisiert:
                        _held.Wunden = value;
                        break;
                    case Trefferzone.Zufall:
                        this[TrefferzonenHelper.ZufallsZone()] = value;
                        break;
                    case Trefferzone.Gesamt:
                    default:
                        _held.Wunden = value;
                        break;
                }
            }
        }
    }
}
