using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using MeisterGeister.Logic.General;
using System.Windows;
using MeisterGeister.Logic.Extensions;

namespace MeisterGeister.ViewModel.Kampf.Logic
{
    public class InitiativListe : ExtendedObservableCollection<ManöverInfo>
    {
        public InitiativListe(Kampf kampf)
        {
            _kampf = kampf;
            CollectionChangedExtended += InitiativListe_CollectionChangedExtended;
            
        }

        public IEnumerable<ZeitImKampf> Aktionszeiten
        {
            get
            {
                return this.SelectMany(ki => ki.Aktionszeiten).OrderBy(zeit => zeit).Distinct();                
            }
        }

        private void InitiativListe_CollectionChangedExtended(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
                foreach (ManöverInfo mi in e.OldItems)
                    mi.PropertyChanged -= ManöverInfo_PropertyChanged;
            OnPropertyChanged(new PropertyChangedEventArgs("Aktionszeiten"));
            if (e.NewItems != null)
                foreach (ManöverInfo mi in e.NewItems)
                    mi.PropertyChanged += ManöverInfo_PropertyChanged;
        }

        public ManöverInfo[] this[IKämpfer k]
        {
            get
            {
                return this.Where(mi => mi.Manöver.Ausführender.Kämpfer == k).ToArray();
            }
        }

        private Kampf _kampf;
        public Kampf Kampf
        {
            get { return _kampf; }
            set { _kampf = value; }
        }

        //public void LöscheBeendeteManöver()
        //{
        //    RemoveAll(mi => mi.Manöver.VerbleibendeDauer == 0);
        //    //Längerfristige Aktionen hier 1-2x neu einstellen.
        //    var l = this.Distinct(new KeyEqualityComparer<ManöverInfo, Manöver.Manöver>(m => m.Manöver)).ToList();
        //    Clear();
        //    foreach (var mi in l)
        //    {
        //        Add(mi.KämpferInfo, mi.Manöver, 0);
        //        if (mi.Manöver.VerbleibendeDauer >= 2)
        //            Add(mi.KämpferInfo, mi.Manöver, -8);
        //    }
        //    l = null;
        //}

        #region Add and Remove

        public void Add(KämpferInfo ki)
        {
            Add(ki.Kämpfer);
        }
        public void Add(IKämpfer k)
        {
            foreach (ManöverInfo mi in this[k])
            {
                Add(mi);
            }
        }

        public void Remove(KämpferInfo ki)
        {
            Remove(ki.Kämpfer);
        }

        public void Remove(IKämpfer k)
        {
            foreach (ManöverInfo mi in this[k])
            {
                Remove(mi);
            }
        }

        public void RemoveAll(Predicate<ManöverInfo> match)
        {
            foreach (ManöverInfo mi in Items.Where(mi => match(mi)).ToList())
            {
                Remove(mi);
            }
        }

        public new void Clear()
        {
            foreach (ManöverInfo mi in Items)
            {
                mi.PropertyChanged -= ManöverInfo_PropertyChanged;
            }
            base.Clear();
        }
        #endregion

        private void ManöverInfo_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Start" || args.PropertyName == "End")
            {
                foreach (ManöverInfo mi in this)
                    mi.NotifyKampfaktionenChanged();
            }
            else if (args.PropertyName == "Aktionszeiten")
            {
                OnPropertyChanged(new PropertyChangedEventArgs("Aktionszeiten"));
            }
        }
    }
}
