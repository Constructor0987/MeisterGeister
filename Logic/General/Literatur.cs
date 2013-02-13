using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.Logic.General
{
    public static class Literatur
    {
        public static readonly SortedDictionary<string, string> Abkürzungen = new SortedDictionary<string, string>() {
            {"AA", "Aventurisches Arsenal"},
            {"H&K", "Handelsherr und Kiepenkerl"},
            {"K&K", "Katakomben und Kavernen"},
            {"LCD", "Liber Cantiones"},
            {"MyA", "Myranisches Arsenal"},
            {"SRD", "Stäbe, Ringe, Dschinnenlampen"},
            {"TCD", "Tractatus contra Daemones"},
            {"WdA", "Wege der Alchimie"},
            {"WdG", "Wege der Götter"},
            {"WdH", "Wege der Helden"},
            {"WdZ", "Wege der Zauberei"},
            {"WdS", "Wege des Schwerts"},
            {"ZBA", "Zoo-Botanica Aventurica"}
        };

        /// <summary>
        /// Tauscht abgekürzte Literturangaben gegen die Langfassung aus.
        /// </summary>
        /// <param name="litertur">Literturangaben mit Abkürzungen.</param>
        /// <returns>Literurangaben in Langfassung.</returns>
        public static string ReplaceAbkürzungen(string litertur)
        {
            if (string.IsNullOrWhiteSpace(litertur))
                return string.Empty;

            string text = litertur;

            foreach (var abk in Abkürzungen)
                text = text.Replace(abk.Key, abk.Value);

            return text;
        }
    }
}
