﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// Eigene Usings
using BasarLogic = MeisterGeister.ViewModel.Basar.Logic;
using InventarLogic = MeisterGeister.ViewModel.Inventar.Logic;

namespace MeisterGeister.Model
{
    public partial class Schild : BasarLogic.IHandelsgut, InventarLogic.IAusrüstung, MeisterGeister.Logic.Literatur.ILiteratur, IFormattable
    {
        public Schild()
        {
            Ausrüstung = new Ausrüstung();
        }

        public bool Usergenerated
        {
            get { return !SchildGUID.ToString().StartsWith("00000000-0000-0000-00"); }
        }

        public override string ToString()
        {
            return this.ToString("G", null);
        }

        /// <summary>
        /// Gibt den Schild als String zurück
        /// </summary>
        /// <param name="format">Format-String zur Definition der Rückgabe:
        /// "g": nur Name des Schilds
        /// "l": alle verfügbaren Werte</param>
        public string ToString(string format)
        {
            return this.ToString(format, null);
        }

        /// <summary>
        /// Gibt den Schild als String zurück
        /// </summary>
        /// <param name="format">Format-String zur Definition der Rückgabe:
        /// "g": nur Name des Schilds
        /// "l": alle verfügbaren Werte</param>
        public string ToString(string format, IFormatProvider provider)
        {
            if (String.IsNullOrEmpty(format)) format = "G";
            if (provider != null)
            {
                ICustomFormatter formatter = provider.GetFormat(this.GetType()) as ICustomFormatter;
                if (formatter != null) return formatter.Format(format, this, provider);
            }
            switch (format.ToLowerInvariant())
            {
                case "g": return Name;
                case "l": return string.Format("{0} ({1}, {2}): WM {3}/{4}, INI {5}, BF {6}",
                Name, Typ, Größe, WMAT, WMPA, INI, BF);
                default: return Name;
            }
        }

        #region //---- IAusrüstung ----
        public string BasisName
        {
            get { return Ausrüstung.BasisAusrüstung ?? Ausrüstung.Name; }
        }

        public string Name
        {
            get
            {
                return Ausrüstung.Name;
            }
            set
            {
                Ausrüstung.Name = value;
            }
        }

        public double Preis
        {
            get
            {
                return Ausrüstung.Preis;
            }
            set
            {
                Ausrüstung.Preis = value;
            }
        }

        public int Gewicht
        {
            get
            {
                return Ausrüstung.Gewicht;
            }
            set
            {
                Ausrüstung.Gewicht = value;
            }
        }

        public string Verbreitung
        {
            get
            {
                return Ausrüstung.Verbreitung;
            }
            set
            {
                Ausrüstung.Verbreitung = value;
            }
        }

        public string Literatur
        {
            get
            {
                return Ausrüstung.Literatur;
            }
            set
            {
                Ausrüstung.Literatur = value;
            }
        }

        public string Bemerkung
        {
            get
            {
                return Ausrüstung.Bemerkung;
            }
            set
            {
                Ausrüstung.Bemerkung = value;
            }
        }
        #endregion

        #region //---- IHandelsgut ----

        public string Kategorie
        {
            get
            {
                switch (Typ)
                {
                    case "S": return "Schilde";
                    case "P": return "Parierwaffen";
                    default: return "Schilde & Parierwaffen";
                }
            }
        }

        string BasarLogic.IHandelsgut.Tags
        {
            get { return Größe + (string.IsNullOrEmpty(Ausrüstung.Tags) ? string.Empty : ", " + Ausrüstung.Tags); }
        }

        double? BasarLogic.IHandelsgut.Gewicht
        {
            get { return Gewicht; }
        }

        string BasarLogic.IHandelsgut.ME
        {
            get { return string.Empty; }
        }

        string BasarLogic.IHandelsgut.Preis
        {
            get { return Preis.ToString(); }
        }

        string BasarLogic.IHandelsgut.Bemerkung
        {
            get
            {
                return String.Format("");
            }
        }

        #endregion

    }
}
