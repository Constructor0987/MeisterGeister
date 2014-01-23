using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// Eigene Usings
using VM = MeisterGeister.ViewModel;
using BasarLogic = MeisterGeister.ViewModel.Basar.Logic;
using InventarLogic = MeisterGeister.ViewModel.Inventar.Logic;
using KampfLogic = MeisterGeister.ViewModel.Kampf.Logic;

namespace MeisterGeister.Model
{
    public partial class Waffe : BasarLogic.IHandelsgut, InventarLogic.IAusrüstung, KampfLogic.IWaffeMitTPKK, MeisterGeister.Logic.Literatur.ILiteratur, IFormattable
    {
        public Waffe()
        {
            Ausrüstung = new Ausrüstung();
        }

        public bool Usergenerated
        {
            get { return !WaffeGUID.ToString().StartsWith("00000000-0000-0000-00"); }
        }

        public string TPString
        {
            get
            {
                return String.Format("{0}W{1}", TPWürfelAnzahl, TPWürfel)
                    + ((TPBonus!=0)?((TPBonus>0)?"+":"-")+TPBonus.ToString() : String.Empty);
            }
        }

        public KampfLogic.Distanzklasse Distanzklasse
        {
            get
            {
                return Waffe.ParseDistanzklasse(DK);
            }
        }

        public override string ToString()
        {
            return this.ToString("G", null);
        }

        /// <summary>
        /// Gibt die Waffe als String zurück
        /// </summary>
        /// <param name="format">Format-String zur Definition der Rückgabe:
        /// "g": nur Name der Waffe
        /// "l": alle verfügbaren Werte</param>
        public string ToString(string format)
        {
            return this.ToString(format, null);
        }

        /// <summary>
        /// Gibt die Waffe als String zurück
        /// </summary>
        /// <param name="format">Format-String zur Definition der Rückgabe:
        /// "g": nur Name der Waffe
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
                case "l": return string.Format("{0}: {1} TP{2}, DK {3}, TP/KK {4}/{5}, WM {6}/{7}, INI {8}, BF {9}",
                    Name, TPString, AusdauerSchaden ? "(A)" : string.Empty, DK, TPKKSchwelle, TPKKSchritt, WMAT, WMPA, INI, BF);
                default: return Name;
            }
        }

        #region Helper-Methoden
        public static KampfLogic.Distanzklasse ParseDistanzklasse(string DK)
        {
            try
            {
                return (KampfLogic.Distanzklasse)Enum.Parse(typeof(KampfLogic.Distanzklasse), DK);
            }
            catch (Exception)
            {
                return KampfLogic.Distanzklasse.None;
            }
        }

        public static string DistanzklasseToString(KampfLogic.Distanzklasse DK)
        {
            if (DK == KampfLogic.Distanzklasse.None)
                return null;
            return DK.ToString();
        }

        public static int TPKKBonus(Held held, KampfLogic.IWaffeMitTPKK waffe)
        {
            if (waffe == null || held == null || !MeisterGeister.Logic.Einstellung.Regeln.TPKK)
                return 0;
            if (waffe.TPKKSchwelle == null || waffe.TPKKSchritt == null || waffe.TPKKSchritt == 0)
                return 0;
            int bonustp = held.Körperkraft - waffe.TPKKSchwelle.Value;
            bonustp = bonustp / waffe.TPKKSchritt.Value;
            return bonustp;
        }

        public int TPKKBonus(Held held)
        {
            return TPKKBonus(held, this);
        }
        #endregion

        #region //---- IAusrüstung ----
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
            get { return "Nahkampfwaffen"; }
        }

        // Als 'Tags' werden die möglichen alente zurück gegeben
        string BasarLogic.IHandelsgut.Tags
        {
            get
            {
                string t = string.Empty;
                foreach (Talent item in Talent)
                {
                    if (t == string.Empty)
                        t = item.Talentname;
                    else
                        t += ", " + item.Talentname;
                }
                return t + (string.IsNullOrEmpty(Ausrüstung.Tags) ? string.Empty : ", " + Ausrüstung.Tags);
            }
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
                return String.Format("{0} TP{1}, DK {2}, {3}/{4} TP/KK, WM {5}/{6}, INI {7}, BF {8}", TPString, (bool)AusdauerSchaden ? "(A)" : "", DK, TPKKSchwelle, TPKKSchritt, WMAT, WMPA, INI, BF);
            }
        }

        #endregion
    }
}
