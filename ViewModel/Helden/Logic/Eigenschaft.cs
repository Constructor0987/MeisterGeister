﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeisterGeister.Logic.General;
using Mod = MeisterGeister.ViewModel.Kampf.Logic.Modifikatoren;

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

        /// <summary>
        /// Gibt den Modifikator-Typ zu einer Eigenschaft zurück.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Type GetModType(string name)
        {
            switch (name)
            {
                case "MU":
                case "Mut":
                    return typeof(Mod.IModMU);
                case "KL":
                case "Klugheit":
                    return typeof(Mod.IModKL);
                case "IN":
                case "Intuition":
                    return typeof(Mod.IModIN);
                case "CH":
                case "Charisma":
                    return typeof(Mod.IModCH);
                case "FF":
                case "Fingerfertigkeit":
                    return typeof(Mod.IModFF);
                case "GE":
                case "Gewandtheit":
                    return typeof(Mod.IModGE);
                case "KO":
                case "Konstitution":
                    return typeof(Mod.IModKO);
                case "KK":
                case "Körperkraft":
                    return typeof(Mod.IModKK);
                default:
                    return null;
            }
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
