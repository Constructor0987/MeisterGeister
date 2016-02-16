using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.ComponentModel;

namespace MeisterGeister.Logic.Extensions
{
    /// <summary>
    /// Unterklasse von ObservableCollection, die um AddRange erweitert wurde.
    /// </summary>
    /// <remarks>Die üblicherweise nach jedem Add vorkommenden OnCollectionChanged werden unterdrückt, um die Geschwindigkeit zu erhöhen.</remarks>
    public class ExtendedObservableCollection<T> : ObservableCollection<T>
    {
        /// <summary>
        /// Stellt eine bessere Variante des CollectionChangedEvents dar
        /// Besser heißt in diesem Fall dass Bereichsaktionen unterstützt werden
        /// Die Funktionalität dieses Events lässt sich leider nicht ins bestehende CollectionChanged-Event integrieren
        /// da sich dort die GUI registriert, welche abstürtzt sobald Bereichsoperationen auftreten
        /// </summary>
        public event NotifyCollectionChangedEventHandler CollectionChangedExtended;

        protected virtual void OnCollectionChangedExtended(NotifyCollectionChangedEventArgs e)
        {
            NotifyCollectionChangedEventHandler handler = CollectionChangedExtended;
            if (handler != null)
                handler(this, e);
        }

        private bool _unterdrückeOnCollectionChanged = false;

        public bool SuppressNotification
        {
            get { return _unterdrückeOnCollectionChanged; }
            set { _unterdrückeOnCollectionChanged = value; }
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (!_unterdrückeOnCollectionChanged)
            {
                OnCollectionChangedExtended(e);
                if ((e.NewItems != null && e.NewItems.Count > 1) || (e.OldItems != null && e.OldItems.Count > 1))
                {
                    //Bei Berechsoperationen wird die GUI nur durch einen Reset benachrichtigt weil sie sonst abstürtzt
                    base.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                }
                else
                    base.OnCollectionChanged(e);
            }
        }

        public void AddRange(IEnumerable<T> objects, bool supressNotification = true)
        {
            if (objects == null) throw new ArgumentNullException("objects");

            int changedAtIndex = Count;
            bool addedItems = false;
            ArrayList newItems = new ArrayList();

            if (supressNotification)
                _unterdrückeOnCollectionChanged = true;
            foreach (T o in objects)
            {
                Add(o);
                newItems.Add(o);
                addedItems = true;
            }
            if (supressNotification)
            {
                _unterdrückeOnCollectionChanged = false;
                //OnPropertyChanged(new PropertyChangedEventArgs("Count"));
                //OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
                if (addedItems)
                    OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, newItems, changedAtIndex));
            }
        }

        public void RemoveRange(IEnumerable<T> objects, bool supressNotification = true)
        {
            if (objects == null) throw new ArgumentNullException("objects");

            if (supressNotification)
                _unterdrückeOnCollectionChanged = true;

            bool removedItems = false;
            ArrayList oldItems = new ArrayList();

            foreach (T o in objects)
            {
                Remove(o);
                oldItems.Add(o);
                removedItems = true;
            }
            if (supressNotification)
            {
                _unterdrückeOnCollectionChanged = false;
                //OnPropertyChanged(new PropertyChangedEventArgs("Count"));
                //OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
                if (removedItems)
                    OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, oldItems));
            }
        }

        public int RemoveAll(Predicate<T> condition, bool supressNotification = true)
        {
            if (supressNotification)
                _unterdrückeOnCollectionChanged = true;

            int cnt = 0;
            int startingIndex = 0;
            ArrayList oldItems = new ArrayList();
            for (int i = Count - 1; i >= 0; i--)
            {
                T item = this[i];
                if (condition(item))
                {
                    cnt++;
                    oldItems.Add(item);
                    startingIndex = i;
                    RemoveAt(i);
                }
            }
            if (supressNotification)
            {
                _unterdrückeOnCollectionChanged = false;
                //OnPropertyChanged(new PropertyChangedEventArgs("Count"));
                //OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
                if (oldItems.Count > 0)
                    OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, oldItems, startingIndex));
            }
            return cnt;
        }

        public void Sort<TKey>(Func<T, TKey> selector)
        {
            List<T> l = this.OrderByDescending(selector).ToList();
            foreach (T item in l)
            {
                int i1 = IndexOf(item), i2 = l.IndexOf(item);
                if (i1 != i2)
                    Move(i1, i2);
            }
        }
    }
}
