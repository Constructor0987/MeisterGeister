using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace MeisterGeister.Logic.Extensions
{
    public static class ObservableCollectionExtensions
    {
        public static void RemoveAll<T>(this ObservableCollection<T> list, Func<T, bool> predicate)
        {
            foreach (var item in list.Where(predicate).ToArray())
                list.Remove(item);
        }
    }
}
