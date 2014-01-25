using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

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

        public void AddRange(IEnumerable<T> objects)
        {
            if (objects == null) throw new ArgumentNullException("objects");

            _unterdrückeOnCollectionChanged = true;
            foreach (T o in objects)
                Add(o);
            _unterdrückeOnCollectionChanged = false;
                        
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
    }
}
