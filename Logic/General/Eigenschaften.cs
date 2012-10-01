using System.Collections.Generic;

namespace MeisterGeister.LogicAlt.General
{
    /// <summary>
    /// Beschreibt eine Liste von Eigenschaften.
    /// </summary>
    public static class Eigenschaften
    {
        public static Dictionary<string, Eigenschaft> Dict;

        /// <summary>
        /// Erzeugt eine Liste von Eigenschaften.
        /// </summary>
        static Eigenschaften()
        {
            Dict = new Dictionary<string, Eigenschaft>(9);
            Dict.Add("MU", new Eigenschaft("Mut"));
            Dict.Add("KL", new Eigenschaft("Klugheit"));
            Dict.Add("IN", new Eigenschaft("Intuition"));
            Dict.Add("CH", new Eigenschaft("Charisma"));
            Dict.Add("FF", new Eigenschaft("Fingerfertigkeit"));
            Dict.Add("GE", new Eigenschaft("Gewandtheit"));
            Dict.Add("KO", new Eigenschaft("Konstitution"));
            Dict.Add("KK", new Eigenschaft("Körperkraft"));
            Dict.Add("SO", new Eigenschaft("Sozialstatus"));
        }

        public static bool ContainsKürzel(string p)
        {
            return Dict.ContainsKey(p);
        }

        public static IEnumerable<string> Kürzel
        {
            get { return Dict.Keys; }
        }

        public static Eigenschaft Get(string kürzel)
        {
            return Dict[kürzel];
        }

        public static string GetKürzel(string p)
        {
            foreach (var item in Dict)
            {
                if (item.Value.Name == p)
                    return item.Key;
            }
            return null;
        }
    }
}
