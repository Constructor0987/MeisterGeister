using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace MeisterGeister.ViewModel.Kampf.Logic
{
    public class ManöverInfo : INotifyPropertyChanged, IDisposable
    {
        private KämpferInfo _kämpferInfo;

        public KämpferInfo KämpferInfo
        {
            get { return _kämpferInfo; }
            private set { _kämpferInfo = value; }
        }

        private int _inimod;
        public int InitiativeMod
        {
            get
            {
                return _inimod;
            }
            set
            {
                _inimod = value;
                NotifyPropertyChanged("Initiative");
            }
        }

        public int Initiative
        {
            get { return KämpferInfo.Initiative + InitiativeMod; }
        }
        public int InitiativeBasis
        {
            get { return KämpferInfo.InitiativeBasis; }
        }

        public ManöverInfo(KämpferInfo ki, Manöver.Manöver m, int inimod)
        {
            KämpferInfo = ki;
            InitiativeMod = inimod;
            ki.PropertyChanged += OnKämpferInfoChanged;
        }

        private void OnKämpferInfoChanged(object o, System.ComponentModel.PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Initiative")
                NotifyPropertyChanged("Initiative");
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        #endregion

        public void Dispose()
        {
            if (KämpferInfo != null)
                KämpferInfo.PropertyChanged -= OnKämpferInfoChanged;
        }
    }

    public class InitiativListe : List<ManöverInfo>, INotifyPropertyChanged
    {
        public InitiativListe(Kampf kampf)
        {
            _kampf = kampf;
        }

        public ManöverInfo[] this[IKämpfer k]
        {
            get
            {
                return this.Where(mi => mi.KämpferInfo.Kämpfer == k).ToArray();
            }
        }

        private Kampf _kampf;
        public Kampf Kampf
        {
            get { return _kampf; }
            set { _kampf = value; }
        }

        #region Add and Remove
        public new void Add(ManöverInfo ki)
        {
            base.Add(ki);
            ki.PropertyChanged += OnManöverInfoChanged;
            NotifyPropertyChanged("List");
            Sort();
        }

        //public void Add(IKämpfer k)
        //{
        //    Add(k, 1);
        //}

        //public void Add(IKämpfer k, int team)
        //{
        //    Add(new KämpferInfo(k) { Team = team });
        //    k.Abwehraktionen = 1;
        //    k.Angriffsaktionen = Math.Max(k.Aktionen - k.Abwehraktionen, 0);
        //}

        public void Add(KämpferInfo ki, Manöver.Manöver m, int inimod)
        {
            Add(new ManöverInfo(ki, m, inimod));
        }

        public new void Remove(ManöverInfo mi)
        {
            mi.PropertyChanged -= OnManöverInfoChanged;
            base.Remove(mi);
            NotifyPropertyChanged("List");
        }

        public new void RemoveAt(int index)
        {
            this[index].PropertyChanged -= OnManöverInfoChanged;
            base.RemoveAt(index);
            NotifyPropertyChanged("List");
        }

        public new void RemoveAll(Predicate<ManöverInfo> match)
        {
            throw new NotImplementedException();
        }

        public new void RemoveRange(int index, int range)
        {
            throw new NotImplementedException();
        }

        public void Remove(IKämpfer k)
        {
            foreach (ManöverInfo mi in this[k])
            {
                mi.PropertyChanged -= OnManöverInfoChanged;
                base.Remove(mi);
            }
            NotifyPropertyChanged("List");
        }
        #endregion

        private void OnManöverInfoChanged(object o, System.ComponentModel.PropertyChangedEventArgs args)
        {
            if(args.PropertyName == "Initiative")
                Sort();
        }

        public new void Sort()
        {
            base.Sort(CompareInitiative);
            NotifyPropertyChanged("Sort");
        }

        /// <summary>
        /// Höhere Initiative nach oben.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static int CompareInitiative(ManöverInfo x, ManöverInfo y)
        {
            // prüfen auf null-Übergabe
            if (x == null && y == null) return 0;
            if (x == null) return 1;
            if (y == null) return -1;
            // Vergleich
            if (x.Initiative > y.Initiative)
                return -1;
            if (x.Initiative < y.Initiative)
                return 1;
            if (x.InitiativeBasis > y.InitiativeBasis)
                return -1;
            if (x.InitiativeBasis < y.InitiativeBasis)
                return 1;
            return x.KämpferInfo.Kämpfer.Name.CompareTo(y.KämpferInfo.Kämpfer.Name);
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        #endregion
    }
}
