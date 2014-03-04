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
        /// Gibt ein zufälliges Element aus der übergenenen Liste zurück
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
    }
}
