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
                OnChanged("Initiative");
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

        private Manöver.Manöver manöver;
        public Manöver.Manöver Manöver
        {
            get { return manöver; }
            private set { manöver = value; }
        }

        public ManöverInfo(KämpferInfo ki, Manöver.Manöver m, int inimod)
        {
            KämpferInfo = ki;
            ki.PropertyChanged += OnKämpferInfoChanged;
            InitiativeMod = inimod;
            Manöver = m;
            Ausgeführt = false;
        }

        public bool Ausgeführt
        {
            get;
            set; //private set; //sollte von einem Manöver-Event OnAusführung gesetzt werden
        }

        private void OnKämpferInfoChanged(object o, System.ComponentModel.PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Initiative")
                OnChanged("Initiative");
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnChanged(String info)
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
        public new void Add(ManöverInfo mi)
        {
            //hier werden alle KeineAktion-Manöver des Kämpfers entfernt. man könnte auch für jede mögliche Aktion ein KeineAktion-Manöver anlegen, dann müsste man hier wieder anpassen.
            foreach (ManöverInfo minfo in this.Where(m => m.KämpferInfo == mi.KämpferInfo && m.Manöver is Manöver.KeineAktion).ToList())
                Remove(minfo);
            base.Add(mi);
            mi.PropertyChanged += OnManöverInfoChanged;
            OnCollectionChanged(CollectionChangeAction.Add, mi);
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
            OnCollectionChanged(CollectionChangeAction.Remove, mi);
        }

        public new void RemoveAt(int index)
        {
            var mi = this[index];
            this[index].PropertyChanged -= OnManöverInfoChanged;
            base.RemoveAt(index);
            OnCollectionChanged(CollectionChangeAction.Remove, mi);
        }

        public new void RemoveAll(Predicate<ManöverInfo> match)
        {
            throw new NotImplementedException();
        }

        public new void RemoveRange(int index, int range)
        {
            throw new NotImplementedException();
        }

        public void Remove(KämpferInfo ki)
        {
            Remove(ki.Kämpfer);
        }

        public void Remove(IKämpfer k)
        {
            foreach (ManöverInfo mi in this[k])
            {
                mi.PropertyChanged -= OnManöverInfoChanged;
                base.Remove(mi);
                OnCollectionChanged(CollectionChangeAction.Remove, mi);
            }
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
            OnChanged("Sort");
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

        public void OnChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        #endregion

        private void OnCollectionChanged(CollectionChangeAction action, object element)
        {
            if (CollectionChanged != null)
            {
                CollectionChanged(this, new CollectionChangeEventArgs(action, element));
            }
        }

        public event CollectionChangeEventHandler CollectionChanged;
    }
}
