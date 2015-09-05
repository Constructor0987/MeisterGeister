using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeisterGeister.ViewModel.Beschwörung
{
    public abstract class BeschwörungsModifikator : INotifyPropertyChanged
    {
        private Func<int> getAnrufungsMod;
        private Func<int> getKontrollMod;
        private Func<int> getZauberMod;
        private Func<int> getKostenMod;

        public Func<int> GetAnrufungsMod
        {
            get { return getAnrufungsMod; }
            set { getAnrufungsMod = value; }
        }
        public Func<int> GetKontrollMod
        {
            get { return getKontrollMod; }
            set { getKontrollMod = value; }
        }
        public Func<int> GetZauberMod
        {
            get { return getZauberMod; }
            set { getZauberMod = value; }
        }
        public Func<int> GetKostenMod
        {
            get { return getKostenMod; }
            set { getKostenMod = value; }
        }

        public BeschwörungsModifikator()
        {
            GetAnrufungsMod = GetKontrollMod = GetZauberMod = GetKostenMod = () => 0;
        }

        private bool isActive = true;
        public bool IsActive
        {
            get { return isActive; }
            set
            {
                isActive = value;
                OnPropertyChanged("IsActive");
                Invalidate();
            }
        }


        public int AnrufungsMod
        {
            get { return IsActive ? getAnrufungsMod() : 0; }
        }
        public int KontrollMod
        {
            get { return IsActive ? getKontrollMod() : 0; }
        }
        public int ZauberMod
        {
            get { return IsActive ? getZauberMod() : 0; }
        }
        public int KostenMod
        {
            get { return IsActive ? getKostenMod() : 0; }
        }


        public void Invalidate()
        {
            OnPropertyChanged("AnrufungsMod");
            OnPropertyChanged("KontrollMod");
            OnPropertyChanged("ZauberMod");
            OnPropertyChanged("KostenMod");
        }

        public abstract void Reset();

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(name));
        }
    }

    public class BeschwörungsModifikator<T> : BeschwörungsModifikator
    {
        private T value;
        public T Value
        {
            get { return value; }
            set
            {
                this.value = value;
                OnPropertyChanged("Value");
                Invalidate();
            }
        }

        public override void Reset()
        {
            Value = default(T);
        }
    }

    public class BeschwörungsModifikator<T1, T2> : BeschwörungsModifikator
    {
        public BeschwörungsModifikator()
            : base()
        {
            //Es gibt viele freie Modifikatoren, welche Anrufungs- und KontrollMod direkt übernehmen
            //Wenn wir 2 Ints als InputTypen haben tragen wir solche Mod-Funktionen schon mal standardmäßig ein
            if (typeof(T1) == typeof(int) && typeof(T2) == typeof(int))
            {
                GetAnrufungsMod = () => (int)Convert.ChangeType(value1, typeof(int));
                GetKontrollMod = () => (int)Convert.ChangeType(value2, typeof(int));
            }
        }

        private T1 value1;
        public T1 Value1
        {
            get { return value1; }
            set
            {
                value1 = value;
                OnPropertyChanged("Value1");
                Invalidate();
            }
        }

        private T2 value2;
        public T2 Value2
        {
            get { return value2; }
            set
            {
                value2 = value;
                OnPropertyChanged("Value2");
                Invalidate();
            }
        }

        public override void Reset()
        {
            Value1 = default(T1);
            Value2 = default(T2);
        }
    }
}
