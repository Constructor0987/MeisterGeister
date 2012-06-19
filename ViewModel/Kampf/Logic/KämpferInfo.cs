﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Mod = MeisterGeister.ViewModel.Kampf.Logic.Modifikatoren;

namespace MeisterGeister.ViewModel.Kampf.Logic
{
    public class KämpferInfo : INotifyPropertyChanged
    {
        private IKämpfer _kämpfer;

        public IKämpfer Kämpfer
        {
            get { return _kämpfer; }
            private set { _kämpfer = value; }
        }

        private int _initiative;
        public int Initiative
        {
            get
            {
                return _initiative;
            }
            set
            {
                _initiative = value;
                int bonus = (int)Math.Floor((_initiative - 10) / 10.0);
                Kämpfer.FreieAktionen = 2 + bonus;
                //Kämpfer.Modifikatoren.RemoveAll(m => m is Mod.PABonusDurchHoheIni);
                //TODO JT: PABonusDurchHoheIni(bonus) anlegen , wenn bonus > 0;
                NotifyPropertyChanged("Initiative");
            }
        }
        public int InitiativeBasis
        {
            get { return Kämpfer.InitiativeBasis; }
        }

        private int _team;
        public int Team
        {
            get { return _team; }
            set { _team = value; }
        }

        public KämpferInfo(IKämpfer k)
        {
            Kämpfer = k;
            Team = 1;
            Initiative = k.Initiative();

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

    public class KämpferInfoListe : List<KämpferInfo>, INotifyPropertyChanged
    {
        private Dictionary<IKämpfer, KämpferInfo> _kämpfer_kämpferinfo;

        public KämpferInfoListe(Kampf kampf)
        {
            _kampf = kampf;
            _kämpfer_kämpferinfo = new Dictionary<IKämpfer, KämpferInfo>();
        }

        public KämpferInfo this[IKämpfer k]
        {
            get
            {
                return _kämpfer_kämpferinfo[k];
            }
        }

        public IEnumerable<IKämpfer> Kämpfer
        {
            get { return _kämpfer_kämpferinfo.Keys; }
        }

        private Kampf _kampf;
        public Kampf Kampf
        {
            get { return _kampf; }
            set { _kampf = value; }
        }

        #region Add and Remove
        public new void Add(KämpferInfo ki)
        {
            base.Add(ki);
            _kämpfer_kämpferinfo.Add(ki.Kämpfer, ki);
            ki.PropertyChanged += OnKämpferInfoChanged;
            NotifyPropertyChanged("List");
            Sort();
        }

        public void Add(IKämpfer k)
        {
            Add(k, 1);
        }

        public void Add(IKämpfer k, int team)
        {
            Add(new KämpferInfo(k) { Team = team });
            k.Abwehraktionen = 1;
            k.Angriffsaktionen = Math.Max(k.Aktionen - k.Abwehraktionen, 0);
        }

        public new void Remove(KämpferInfo ki)
        {
            ki.PropertyChanged -= OnKämpferInfoChanged;
            base.Remove(ki);
            NotifyPropertyChanged("List");
        }

        public new void RemoveAt(int index)
        {
            this[index].PropertyChanged -= OnKämpferInfoChanged;
            base.RemoveAt(index);
            NotifyPropertyChanged("List");
        }

        public new void RemoveAll(Predicate<KämpferInfo> match)
        {
            throw new NotImplementedException();
        }

        public new void RemoveRange(int index, int range)
        {
            throw new NotImplementedException();
        }

        public void Remove(IKämpfer k)
        {
            Remove(this[k]);
        }
        #endregion

        private void OnKämpferInfoChanged(object o, System.ComponentModel.PropertyChangedEventArgs args)
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
        public static int CompareInitiative(KämpferInfo x, KämpferInfo y)
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
            return x.Kämpfer.Name.CompareTo(y.Kämpfer.Name);
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
