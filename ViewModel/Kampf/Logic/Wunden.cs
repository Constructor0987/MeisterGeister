using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using MeisterGeister.Logic.General;
using Mod = MeisterGeister.ViewModel.Kampf.Logic.Modifikatoren;

namespace MeisterGeister.ViewModel.Kampf.Logic
{
    /// <summary>
    /// Wunden abhängig vom Trefferzonen-Enum
    /// </summary>
    public class Wunden : IWunden, INotifyPropertyChanged
    {
        private IHasWunden _held;
        public Wunden(IHasWunden held)
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
            if (!MeisterGeister.Logic.Settings.Regeln.AusdauerImKampf)
                return 0;
            if (neu > alt)
            {
                // TODO ??: Dialog MVVM-konform aufrufen
                int count = neu - alt;
                string wundeText = count > 1 ? "Wunden" : "Wunde";
                return View.General.ViewHelper.ShowWürfelDialog(count + "W6", string.Format("Ausdauer-Schaden durch {0} {1}.", count, wundeText)); //WdS 83
            }
            return 0;
        }

        public void UpdateWundenModifikatoren()
        {
            Wesen w = _held as Wesen;
            if (w == null)
                return;
            for (int i = (int)Trefferzone.Kopf; i <= (int)Trefferzone.Unlokalisiert; i++)
            {
                Trefferzone zone = (Trefferzone)i;
                Type modTyp = Mod.WundenModifikator.TypByZone(zone);
                w.SetModifikatorCount(modTyp, this[zone]);
            }
        }

        public int this[Trefferzone zone]
        {
            get
            {
                switch (zone)
                {
                    case Trefferzone.Kopf:
                        return _held.WundenKopf;
                    case Trefferzone.Brust:
                    case Trefferzone.Rücken:
                        return _held.WundenBrust;
                    case Trefferzone.ArmL:
                        return _held.WundenArmL;
                    case Trefferzone.ArmR:
                        return _held.WundenArmR;
                    case Trefferzone.Bauch:
                        return _held.WundenBauch;
                    case Trefferzone.BeinL:
                        return _held.WundenBeinL;
                    case Trefferzone.BeinR:
                        return _held.WundenBeinR;
                    case Trefferzone.Unlokalisiert:
                        return _held.Wunden;
                    case Trefferzone.Zufall:
                        return this[TrefferzonenHelper.ZufallsZone()];
                    case Trefferzone.Gesamt:
                    default:
                        return _held.Wunden;
                }
            }
            set
            {
                if (value < 0)
                    value = 0;
                if (value > 3 && (MeisterGeister.Logic.Settings.Einstellungen.NurDreiZonenWunden || zone == Trefferzone.Unlokalisiert || zone == Trefferzone.Gesamt)) //WdS 108
                    value = 3;
                if (value == this[zone])
                    return;
                if (zone == Trefferzone.Zufall)
                {
                    this[TrefferzonenHelper.ZufallsZone()] = value;
                    return;
                }
                Wesen w = _held as Wesen;
                IKämpfer k = _held as IKämpfer;
                if (w == null || k==null)
                    return;

                Type modTyp = Mod.WundenModifikator.TypByZone(zone);
                int changes = w.SetModifikatorCount(modTyp, value);

                k.AusdauerAktuell -= AusdauerSchaden(this[zone], value); //WdS 83

                bool dreiWundenOderMehr = (this[zone] < 3 && value >= 3);

                switch (zone)
                {
                    case Trefferzone.Kopf:
                        if (value > _held.WundenKopf)
                        {
                            if (dreiWundenOderMehr)
                            {
                                // TODO ??: bewusstlos + blutverlust
                                
                                // TODO ??: Dialog MVVM-konform aufrufen
                                k.LebensenergieAktuell -= View.General.ViewHelper.ShowWürfelDialog("2W6",
                                    string.Format("SP durch {0}. Kopf-Wunde.", value));
                            }
                        }
                        _held.WundenKopf = value;
                        break;
                    case Trefferzone.Rücken:
                    case Trefferzone.Brust:
                        if (value > _held.WundenBrust)
                        {
                            int count = value - _held.WundenBrust;
                            string wundeText = count > 1 ? "Wunden" : "Wunde";

                            // TODO ??: Dialog MVVM-konform aufrufen
                            k.LebensenergieAktuell -= View.General.ViewHelper.ShowWürfelDialog( count + "W6",
                                string.Format("SP durch {0} Brust-{1}.", count, wundeText));
                            if (dreiWundenOderMehr)
                            {
                                // TODO ??: bewusstlos + blutverlust
                            }
                        }
                        _held.WundenBrust = value;
                        break;
                    case Trefferzone.ArmL:
                        if (value > _held.WundenArmL)
                        {
                            if (dreiWundenOderMehr)
                            {
                                // TODO ??: arm handlungsunfähig
                            }
                        }
                        _held.WundenArmL = value;
                        break;
                    case Trefferzone.ArmR:
                        if (value > _held.WundenArmR)
                        {
                            if (dreiWundenOderMehr)
                            {
                                // TODO ??: arm handlungsunfähig
                            }
                        }
                        _held.WundenArmR = value;
                        break;
                    case Trefferzone.Bauch:
                        if (value > _held.WundenBauch)
                        {
                            int count = value - _held.WundenBauch;
                            string wundeText = count > 1 ? "Wunden" : "Wunde";

                            // TODO ??: Dialog MVVM-konform aufrufen
                            k.LebensenergieAktuell -= View.General.ViewHelper.ShowWürfelDialog(count + "W6",
                                string.Format("SP durch {0} Bauch-{1}.", count, wundeText));
                            if (dreiWundenOderMehr)
                            {
                                // TODO ??: bewusstlos + blutverlust
                            }
                        }
                        _held.WundenBauch = value;
                        break;
                    case Trefferzone.BeinL:
                        if (value > _held.WundenBeinL)
                        {
                            if (dreiWundenOderMehr)
                            {
                                // TODO ??: sturz, kampfunfähig
                            }
                        }
                        _held.WundenBeinL = value;
                        break;
                    case Trefferzone.BeinR:
                        if (value > _held.WundenBeinR)
                        {
                            if (dreiWundenOderMehr)
                            {
                                // TODO ??: sturz, kampfunfähig
                            }
                        }
                        _held.WundenBeinR = value;
                        break;
                    case Trefferzone.Unlokalisiert:
                    case Trefferzone.Gesamt:
                    default:
                        _held.Wunden = value;
                        break;
                }
                OnChanged("");
            }
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnChanged(String info, object sender = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(sender ?? this, new PropertyChangedEventArgs(info));
            }
        }

        #endregion
    }
}
