using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using MeisterGeister.Model;

namespace MeisterGeister.ViewModel.Kampf.Logic
{
    /// <summary>
    /// Die Rüstung abhängig vom Trefferzone-Enum
    /// </summary>
    public class Rüstungsschutz : IRüstungsschutz, INotifyPropertyChanged
    {
        private IHasZonenRs _wesen;

        public Rüstungsschutz(IHasZonenRs wesen)
        {
            _wesen = wesen;
        }

        public int this[Trefferzone zone]
        {
            get
            {
                switch (zone)
                {
                    case Trefferzone.Kopf:
                        return _wesen.RSKopf;
                    case Trefferzone.Brust:
                        return _wesen.RSBrust;
                    case Trefferzone.Rücken:
                        return _wesen.RSRücken;
                    case Trefferzone.ArmL:
                        return _wesen.RSArmL;
                    case Trefferzone.ArmR:
                        return _wesen.RSArmR;
                    case Trefferzone.Bauch:
                        return _wesen.RSBauch;
                    case Trefferzone.BeinL:
                        return _wesen.RSBeinL;
                    case Trefferzone.BeinR:
                        return _wesen.RSBeinR;
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
                        _wesen.RSKopf = value;
                        break;
                    case Trefferzone.Brust:
                        _wesen.RSBrust = value;
                        break;
                    case Trefferzone.Rücken:
                        _wesen.RSRücken = value;
                        break;
                    case Trefferzone.ArmL:
                        _wesen.RSArmL = value;
                        break;
                    case Trefferzone.ArmR:
                        _wesen.RSArmR = value;
                        break;
                    case Trefferzone.Bauch:
                        _wesen.RSBauch = value;
                        break;
                    case Trefferzone.BeinL:
                        _wesen.RSBeinL = value;
                        break;
                    case Trefferzone.BeinR:
                        _wesen.RSBeinR = value;
                        break;
                    case Trefferzone.Zufall:
                        this[TrefferzonenHelper.ZufallsZone()] = value;
                        break;
                    //Ändern des Gesamt-RS sollte automatisch die Werte in einer Zone setzen
                    case Trefferzone.Gesamt:
                        for (int i = 0; i < (int)Trefferzone.Unlokalisiert; i++)
                            this[(Trefferzone)i] = value;
                        break;
                    case Trefferzone.Unlokalisiert:
                    default:
                        break;
                }
                OnChanged(string.Empty);
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
                return (int)Math.Round(rs/20.0);
            }
        }

        #region //---- INotifyPropertyChanged ----

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
