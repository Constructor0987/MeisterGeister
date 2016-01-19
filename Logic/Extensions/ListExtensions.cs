using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeisterGeister.Logic.General;

namespace MeisterGeister.Logic.Extensions
{
    public static class ListExtensions
    {
        /// <summary>
        /// Gibt ein zufälliges Element aus der übergebenen Liste zurück
        /// </summary>
        public static T RandomElement<T>(this IList<T> list)
        {
            return list[RandomNumberGenerator.Generator.Next(list.Count)];
        }

        /// <summary>
        /// Sortiert eine Liste (fast) zufällig
        /// </summary>
        public static void Shuffle<T>(this IList<T> list)
        {
            T value; 
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = RandomNumberGenerator.Generator.Next(n + 1);
                value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        ///<summary>Finds the index of the first item matching an expression in an enumerable.</summary>
        ///<param name="items">The enumerable to search.</param>
        ///<param name="predicate">The expression to test the items against.</param>
        ///<returns>The index of the first matching item, or -1 if no items match.</returns>
        public static int FindIndex<T>(this IEnumerable<T> items, Func<T, bool> predicate)
        {
            if (items == null) throw new ArgumentNullException("items");
            if (predicate == null) throw new ArgumentNullException("predicate");

            int retVal = 0;
            foreach (var item in items)
            {
                if (predicate(item)) return retVal;
                retVal++;
            }
            return -1;
        }
    }
}
