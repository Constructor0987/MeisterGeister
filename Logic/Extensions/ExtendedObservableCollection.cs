using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace MeisterGeister.Logic.Extensions
{
    /// <summary>
    /// Unterklasse von ObservableCollection, die um AddRange erweitert wurde.
    /// </summary>
    /// <remarks>Die üblicherweise nach jedem Add vorkommenden OnCollectionChanged werden unterdrückt, um die Geschwindigkeit zu erhöhen.</remarks>
    public class ExtendedObservableCollection<T> : ObservableCollection<T>
    {
        private bool _unterdrückeOnCollectionChanged = false;

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (!_unterdrückeOnCollectionChanged)
                base.OnCollectionChanged(e);
        }

        public void AddRange(IEnumerable<T> objects, bool supressNotification = true)
        {
            if (objects == null) throw new ArgumentNullException("objects");

            if(supressNotification)
                _unterdrückeOnCollectionChanged = true;
            foreach (T o in objects)
                Add(o);
            if (supressNotification)
            {
                _unterdrückeOnCollectionChanged = false;
                //OnPropertyChanged(new PropertyChangedEventArgs("Count"));
                //OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }

        public int RemoveAll(Predicate<T> condition, bool supressNotification = true)
        {
            if(supressNotification)
                _unterdrückeOnCollectionChanged = true;
            int cnt = 0;
            for (int i = Count - 1; i >= 0; i--)
            {
                if (condition(this[i]))
                {
                    cnt++;
                    RemoveAt(i);
                }
            }
            if (supressNotification)
            {
                _unterdrückeOnCollectionChanged = false;
                //OnPropertyChanged(new PropertyChangedEventArgs("Count"));
                //OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
            return cnt;
        }
    }
}
