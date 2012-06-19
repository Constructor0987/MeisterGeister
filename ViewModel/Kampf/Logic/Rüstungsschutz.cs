using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MeisterGeister.Model;

namespace MeisterGeister.ViewModel.Kampf.Logic
{
    /// <summary>
    /// Die Rüstung abhängig vom Trefferzone-Enum
    /// </summary>
    public class Rüstungsschutz : IRüstungsschutz
    {
        private Model.Held _held;
        public Rüstungsschutz(Model.Held held)
        {
            _held = held;
        }

        public int this[Trefferzone zone]
        {
            get
            {
                switch (zone)
                {
                    case Trefferzone.Kopf:
                        return _held.RSKopf ?? 0;
                    case Trefferzone.Brust:
                        return _held.RSBrust ?? 0;
                    case Trefferzone.Rücken:
                        return _held.RSRücken ?? 0;
                    case Trefferzone.ArmL:
                        return _held.RSArmL ?? 0;
                    case Trefferzone.ArmR:
                        return _held.RSArmR ?? 0;
                    case Trefferzone.Bauch:
                        return _held.RSBauch ?? 0;
                    case Trefferzone.BeinL:
                        return _held.RSBeinL ?? 0;
                    case Trefferzone.BeinR:
                        return _held.RSBeinR ?? 0;
                    case Trefferzone.Unlokalisiert:
                        return GesamtRS;
                    case Trefferzone.Zufall:
                        return this[TrefferzonenHelper.ZufallsZone()];
                    case Trefferzone.Gesamt:
                    default:
                        return GesamtRS;
                }
            }
            set
            {
                switch (zone)
                {
                    case Trefferzone.Kopf:
                        _held.RSKopf = value;
                        break;
                    case Trefferzone.Brust:
                        _held.RSBrust = value;
                        break;
                    case Trefferzone.Rücken:
                        _held.RSRücken = value;
                        break;
                    case Trefferzone.ArmL:
                        _held.RSArmL = value;
                        break;
                    case Trefferzone.ArmR:
                        _held.RSArmR = value;
                        break;
                    case Trefferzone.Bauch:
                        _held.RSBauch = value;
                        break;
                    case Trefferzone.BeinL:
                        _held.RSBeinL = value;
                        break;
                    case Trefferzone.BeinR:
                        _held.RSBeinR = value;
                        break;
                    case Trefferzone.Zufall:
                        this[TrefferzonenHelper.ZufallsZone()] = value;
                        break;
                    case Trefferzone.Gesamt:
                    case Trefferzone.Unlokalisiert:
                    default:
                        break;
                }
            }
        }

        private int GesamtRS
        {
            get
            {
                double rs = 0;
                for (int i = 0; i < (int)Trefferzone.Unlokalisiert; i++)
                {
                    double rsplus = 0;
                    switch ((Trefferzone)i)
                    {
                        case Trefferzone.Kopf:
                        case Trefferzone.BeinL:
                        case Trefferzone.BeinR:
                            rsplus = 2;
                            break;
                        case Trefferzone.Brust:
                        case Trefferzone.Bauch:
                        case Trefferzone.Rücken:
                            rsplus = 4;
                            break;
                        case Trefferzone.ArmL:
                        case Trefferzone.ArmR:
                        default:
                            rsplus = 1;
                            break;
                    }
                    rsplus *= this[(Trefferzone)i];
                    rs += rsplus;
                }
                return (int)Math.Round(rs);
            }
        }
    }
}
