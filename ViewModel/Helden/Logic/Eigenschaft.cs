using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeisterGeister.Logic.General;

namespace MeisterGeister.ViewModel.Helden.Logic
{
    public class Eigenschaft : Probe
    {
        #region //---- KONSTRUKTOREN & INITIALISIERUNG ----

        public Eigenschaft() { }

        public Eigenschaft(string name)
        {
            Name = name;
        }

        #endregion //---- KONSTRUKTOREN & INITIALISIERUNG ----

        #region //---- EIGENSCHAFTEN & FELDER ----

        public string Name { get; set; }

        #endregion //---- EIGENSCHAFTEN & FELDER ----

        #region //---- STATIC ----

        /// <summary>
        /// Erzeugt eine Liste von Eigenschaften.
        /// </summary>
        static Eigenschaft()
        {
            EigenschaftenDictionary.Add("MU", new Eigenschaft("Mut"));
            EigenschaftenDictionary.Add("KL", new Eigenschaft("Klugheit"));
            EigenschaftenDictionary.Add("IN", new Eigenschaft("Intuition"));
            EigenschaftenDictionary.Add("CH", new Eigenschaft("Charisma"));
            EigenschaftenDictionary.Add("FF", new Eigenschaft("Fingerfertigkeit"));
            EigenschaftenDictionary.Add("GE", new Eigenschaft("Gewandtheit"));
            EigenschaftenDictionary.Add("KO", new Eigenschaft("Konstitution"));
            EigenschaftenDictionary.Add("KK", new Eigenschaft("Körperkraft"));
            EigenschaftenDictionary.Add("SO", new Eigenschaft("Sozialstatus"));
        }

        private static Dictionary<string, Eigenschaft> _eigenschaftenDictionary = new Dictionary<string, Eigenschaft>(9);
        public static Dictionary<string, Eigenschaft> EigenschaftenDictionary { get { return _eigenschaftenDictionary; } }

        /// <summary>
        /// Gibt die Abkürzung zu einer Eigenschaft zurük.
        /// </summary>
        /// <param name="name">Name der Eigenschaft.</param>
        /// <returns>Abkürzung der Eigenschaft.</returns>
        public static string GetAbkürzung(string name)
        {
            foreach (var item in EigenschaftenDictionary)
            {
                if (item.Value.Name == name)
                    return item.Key;
            }
            return string.Empty;
        }

        #endregion //---- STATIC ----

        #region //---- PROBE ----

        override public int[] Werte
        {
            get
            {
                if (_werte == null)
                    _werte = new int[1];
                return _werte;
            }
            set
            {
                _werte = value;
                _chanceBerechnet = false;
            }
        }

        #endregion //---- PROBE ----
    }
}
