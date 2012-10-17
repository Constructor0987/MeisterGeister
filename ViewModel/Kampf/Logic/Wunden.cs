using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MeisterGeister.Logic.General;
using Mod = MeisterGeister.ViewModel.Kampf.Logic.Modifikatoren;

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
            UpdateWundenModifikatoren();
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

        private int AusdauerSchaden(int alt, int neu)
        {
            if (neu > alt)
            {
                // TODO ??: Dialog MVVM-konform aufrufen
                return View.General.ViewHelper.ShowWürfelDialog((neu - alt) + "W6", "Ausdauer-Schaden durch Wunde"); //WdS 83
            }
            return 0;
        }

        public void UpdateWundenModifikatoren()
        {
            Type modTyp = Mod.WundenModifikator.TypByZone(Trefferzone.Kopf);
            _held.SetModifikatorCount(modTyp, this[Trefferzone.Kopf]);
            for (int i = (int)Trefferzone.Rücken; i <= (int)Trefferzone.Unlokalisiert; i++)
            {
                Trefferzone zone = (Trefferzone)i;
                modTyp = Mod.WundenModifikator.TypByZone(zone);
                _held.SetModifikatorCount(modTyp, this[zone]);
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
                if (value < 0)
                    value = 0;
                if (value == this[zone])
                    return;
                Type modTyp = Mod.WundenModifikator.TypByZone(zone);
                int changes = _held.SetModifikatorCount(modTyp, value);

                _held.AusdauerAktuell -= AusdauerSchaden(this[zone], value); //WdS 83

                bool mehrAlsDreiWunden = (this[zone] < 3 && value >= 3);

                switch (zone)
                {
                    case Trefferzone.Kopf:
                        if (value > _held.WundenKopf)
                        {
                            if (mehrAlsDreiWunden)
                            {
                                // TODO ??: bewusstlos + blutverlust
                                
                                // TODO ??: Dialog MVVM-konform aufrufen
                                _held.LebensenergieAktuell -= View.General.ViewHelper.ShowWürfelDialog("2W6",
                                    string.Format("SP durch {0}. Kopf-Wunde", value));
                            }
                        }
                        _held.WundenKopf = value;
                        break;
                    case Trefferzone.Rücken:
                    case Trefferzone.Brust:
                        if (value > _held.WundenBrust)
                        {
                            // TODO ??: Dialog MVVM-konform aufrufen
                            _held.LebensenergieAktuell -= View.General.ViewHelper.ShowWürfelDialog( (value - _held.WundenBrust ?? 0) + "W6",
                                string.Format("SP durch {0}. Brust-Wunde", value));
                            if (mehrAlsDreiWunden)
                            {
                                // TODO ??: bewusstlos + blutverlust
                            }
                        }
                        _held.WundenBrust = value;
                        break;
                    case Trefferzone.ArmL:
                        if (value > _held.WundenArmL)
                        {
                            if (mehrAlsDreiWunden)
                            {
                                // TODO ??: arm handlungsunfähig
                            }
                        }
                        _held.WundenArmL = value;
                        break;
                    case Trefferzone.ArmR:
                        if (value > _held.WundenArmR)
                        {
                            if (mehrAlsDreiWunden)
                            {
                                // TODO ??: arm handlungsunfähig
                            }
                        }
                        _held.WundenArmR = value;
                        break;
                    case Trefferzone.Bauch:
                        if (value > _held.WundenBauch)
                        {
                            // TODO ??: Dialog MVVM-konform aufrufen
                            _held.LebensenergieAktuell -= View.General.ViewHelper.ShowWürfelDialog((value - _held.WundenBauch ?? 0) + "W6",
                                string.Format("SP durch {0}. Bauch-Wunde", value));
                            if (mehrAlsDreiWunden)
                            {
                                // TODO ??: bewusstlos + blutverlust
                            }
                        }
                        _held.WundenBauch = value;
                        break;
                    case Trefferzone.BeinL:
                        if (value > _held.WundenBeinL)
                        {
                            if (mehrAlsDreiWunden)
                            {
                                // TODO ??: sturz, kampfunfähig
                            }
                        }
                        _held.WundenBeinL = value;
                        break;
                    case Trefferzone.BeinR:
                        if (value > _held.WundenBeinR)
                        {
                            if (mehrAlsDreiWunden)
                            {
                                // TODO ??: sturz, kampfunfähig
                            }
                        }
                        _held.WundenBeinR = value;
                        break;
                    case Trefferzone.Zufall:
                        this[TrefferzonenHelper.ZufallsZone()] = value;
                        break;
                    case Trefferzone.Unlokalisiert:
                    case Trefferzone.Gesamt:
                    default:
                        _held.Wunden = value;
                        break;
                }
            }
        }
    }
}
