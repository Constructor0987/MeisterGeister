using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using MeisterGeister.Model;
using System.Windows.Data;

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
                        return GesamtRS(this);
                    case Trefferzone.Zufall:
                        return this[TrefferzonenHelper.ZufallsZone()];
                    case Trefferzone.Gesamt:
                    default:
                        return GesamtRS(this);
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
                        if (this[Trefferzone.Gesamt] == value)
                            break;
                        for (int i = 0; i < (int)Trefferzone.Unlokalisiert; i++)
                            this[(Trefferzone)i] = value;
                        break;
                    case Trefferzone.Unlokalisiert:
                    default:
                        break;
                }
                OnChanged(Binding.IndexerName);
            }
        }

        private static double gRsDivisor = 20.0;
        public static double GRsDivisor
        {
            get { return Rüstungsschutz.gRsDivisor; }
            set { Rüstungsschutz.gRsDivisor = value; }
        }

        public static int GesamtRS(IRüstungsschutz r)
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
                    rsplus *= r[(Trefferzone)i];
                    rs += rsplus;
                }
                return (int)Math.Round(rs / GRsDivisor);
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

        public static RüstungsWerte Add(IRüstungsschutz a, IRüstungsschutz b)
        {
            RüstungsWerte rw = new RüstungsWerte();
            for (Trefferzone i = Trefferzone.Kopf; i < Trefferzone.Unlokalisiert; i++)
                rw[i] = Math.Max(a[i] + b[i], 0);
            return rw;
        }

        public static RüstungsWerte Substract(IRüstungsschutz a, IRüstungsschutz b)
        {
            RüstungsWerte rw = new RüstungsWerte();
            for (Trefferzone i = Trefferzone.Kopf; i < Trefferzone.Unlokalisiert; i++)
                rw[i] = Math.Max(a[i] - b[i], 0);
            return rw;
        }

        public static RüstungsWerte Multiply(IRüstungsschutz a, double b)
        {
            RüstungsWerte rw = new RüstungsWerte();
            for (Trefferzone i = Trefferzone.Kopf; i < Trefferzone.Unlokalisiert; i++)
                rw[i] = Math.Max((int)Math.Round(a[i] * b, MidpointRounding.AwayFromZero) , 0);
            return rw;
        }

        public static IRüstungsschutz operator +(Rüstungsschutz a, IRüstungsschutz b)
        {
            return Rüstungsschutz.Add(a, b);
        }

        public static IRüstungsschutz operator -(Rüstungsschutz a, IRüstungsschutz b)
        {
            return Rüstungsschutz.Substract(a, b);
        }

        public static IRüstungsschutz operator *(Rüstungsschutz a, double b)
        {
            return Rüstungsschutz.Multiply(a, b);
        }

        public void SetValues(IRüstungsschutz a)
        {
            Rüstungsschutz.Set(this, a);
            OnChanged(string.Empty);
        }

        public static void Set(IRüstungsschutz a, IRüstungsschutz b)
        {
            for (Trefferzone i = Trefferzone.Kopf; i < Trefferzone.Unlokalisiert; i++)
                a[i] = b[i];
        }


    }

    public class RüstungsWerte : IRüstungsschutz
    {

        int[] data = new int[(int)Trefferzone.Unlokalisiert];
        
        public RüstungsWerte()
        {
            for (int i = 0; i < data.Length; i++)
                data[i] = 0;
        }

        public void SetValues(IRüstungsschutz a)
        {
            Rüstungsschutz.Set(this, a);
        }

        public int this[Trefferzone zone]
        {
            get
            {
                if (zone == Trefferzone.Gesamt)
                    return 0;
                if (zone >= Trefferzone.Unlokalisiert)
                    return 0;
                return data[(int)zone];
            }
            set
            {
                if (zone == Trefferzone.Gesamt)
                {
                    if (this[Trefferzone.Gesamt] == value)
                        return;
                    for (int i = 0; i < (int)Trefferzone.Unlokalisiert; i++)
                        this[(Trefferzone)i] = value;
                    return;
                }
                if (zone >= Trefferzone.Unlokalisiert)
                    return;
                data[(int)zone] = value;
            }
        }

        public static IRüstungsschutz operator +(RüstungsWerte a, IRüstungsschutz b)
        {
            return Rüstungsschutz.Add(a, b);
        }

        public static IRüstungsschutz operator -(RüstungsWerte a, IRüstungsschutz b)
        {
            return Rüstungsschutz.Substract(a, b);
        }

        public static IRüstungsschutz operator *(RüstungsWerte a, double b)
        {
            return Rüstungsschutz.Multiply(a, b);
        }
    }

}
