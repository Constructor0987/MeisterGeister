using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace MeisterGeister.ViewModel.Kampf.Logic.Modifikatoren
{
    public class ModifikatorenListe : List<Modifikatoren.IModifikator>, INotifyPropertyChanged, INotifyCollectionChanged
    {
        public ModifikatorenListe()
            : base()
        {

        }

        #region Add and Remove
        public new void Add(Modifikatoren.IModifikator mod)
        {
            base.Add(mod);
            Sort();
            OnCollectionChanged(NotifyCollectionChangedAction.Add, mod);
        }

        public new void Remove(Modifikatoren.IModifikator mod)
        {
            base.Remove(mod);
            OnCollectionChanged(NotifyCollectionChangedAction.Remove, mod);
        }

        public new void RemoveAt(int index)
        {
            var removed = this[index];
            base.RemoveAt(index);
            OnCollectionChanged(NotifyCollectionChangedAction.Remove, removed);
        }

        public new void RemoveAll(Predicate<Modifikatoren.IModifikator> match)
        {
            foreach (Modifikatoren.IModifikator item in this.Where(mod => match(mod)).ToList())
                Remove(item);
        }

        public new void RemoveRange(int index, int range)
        {
            throw new NotImplementedException();
        }

        #endregion

        public new void Sort()
        {
            //base.Sort();
            // TODO ??: Vergleich für Modifikatoren implementieren, um die Liste sortieren zu können.
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

        private void OnCollectionChanged(NotifyCollectionChangedAction action, object element)
        {
            if (CollectionChanged != null)
            {
                CollectionChanged(this, new NotifyCollectionChangedEventArgs(action, element));
            }
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;
    }
}
