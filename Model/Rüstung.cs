using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// Eigene Usings
using BasarLogic = MeisterGeister.ViewModel.Basar.Logic;
using InventarLogic = MeisterGeister.ViewModel.Inventar.Logic;
using KampfLogic = MeisterGeister.ViewModel.Kampf.Logic;
using MeisterGeister.Model.Extensions;

namespace MeisterGeister.Model
{
    public partial class Rüstung : BasarLogic.IHandelsgut, InventarLogic.IAusrüstung, KampfLogic.IHasZonenRs, MeisterGeister.Logic.Literatur.ILiteratur, IFormattable
    {

        private static int _gRSDivisor = 20;

        public Rüstung()
        {
            Ausrüstung = new Ausrüstung();
            PropertyChanged += DependentProperty.PropagateINotifyProperyChanged;
        }

        /// <summary>
        /// Gibt die gRS im Zonensystem zurück
        /// </summary>
        /// <returns>gRS oder null</returns>
        public Nullable<double> gRS
        {
            get {
                if (ZonenrüstungswerteVorhanden())
                {
                    return (2.0 * (Kopf.Value + LBein.Value + RBein.Value) + 4.0 * (Bauch.Value + Brust.Value + Rücken.Value) + (LArm.Value + RArm.Value)) / _gRSDivisor;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Gibt die gBE im Zonensystem zurück
        /// </summary>
        /// <returns>rBE oder null</returns>
        public Nullable<double> gBE
        {
            get {
                Nullable<double> gesamtBE = this.gRS;
                if (gesamtBE.HasValue)
                {
                    if (this.Art == "Z")
                    {
                        gesamtBE /= (Math.Pow(2, (this.Verarbeitung.HasValue ? this.Verarbeitung.Value : 0)));
                    }
                    else
                    {
                        gesamtBE = Math.Max(0.0, gesamtBE.Value - (this.Verarbeitung.HasValue ? this.Verarbeitung.Value : 0));
                    }
                }
                return gesamtBE;
            }
        }

        public bool Usergenerated
        {
            get { return !RüstungGUID.ToString().StartsWith("00000000-0000-0000-00"); }
        }

        public bool HervorragendeKette()
        {
            return this.Gruppe == "Hervorragende Kette";
        }

        public override string ToString()
        {
            return this.ToString("G", null);
        }

        /// <summary>
        /// Gibt die Rüstung als String zurück
        /// </summary>
        /// <param name="format">Format-String zur Definition der Rückgabe:
        /// "g": nur Name der Rüstung
        /// "e": nur die (einfachen) RS/BE-Werte (keine Zonen/gRS/gBE-Werte)
        /// "z": nur die Zonenrüstungswerte
        /// "l": alle verfügbaren Werte</param>
        public string ToString(string format)
        {
            return this.ToString(format, null);
        }

        /// <summary>
        /// Gibt die Rüstung als String zurück
        /// </summary>
        /// <param name="format">Format-String zur Definition der Rückgabe:
        /// "g": nur Name der Rüstung
        /// "e": nur die (einfachen) RS/BE-Werte (keine Zonen/gRS/gBE-Werte)
        /// "z": nur die Zonenrüstungswerte
        /// "l": alle verfügbaren Werte</param>
        public string ToString(string format, IFormatProvider provider)
        {
            if (String.IsNullOrEmpty(format)) format = "G";
            if (provider != null)
            {
                ICustomFormatter formatter = provider.GetFormat(this.GetType()) as ICustomFormatter;
                if (formatter != null) return formatter.Format(format, this, provider);
            }
            switch(format.ToLowerInvariant())
            {
                case "g": return Name;
                case "l": return string.Format("{0} ({1}): RS {3}, BE {4}, gRS {5}, gBE {6}, Kopf {7}, Brust {8}, Rücken {9}, Bauch {10}, LArm {11}, RArm {12}, LBein {13}, RBein {14}",
                Name, Art, RS, RS, BE, gRS, gBE, Kopf, Brust, Rücken, Bauch, LArm, RArm, LBein, RBein);
                case "e": return string.Format("{0} ({1}): RS {2}, BE {3}",
                Name, Art, RS, BE);
                case "z": return string.Format("{0} ({1}): gRS {5}, gBE {6}, Kopf {7}, Brust {8}, Rücken {9}, Bauch {10}, LArm {11}, RArm {12}, LBein {13}, RBein {14}",
                Name, Art, RS, RS, BE, gRS, gBE, Kopf, Brust, Rücken, Bauch, LArm, RArm, LBein, RBein);
                default: return Name;
            }
        }

        /// <summary>
        /// gibt zurück, ob für jede Zone ein Rüstungswert gesetzt wurde
        /// (gRS, RS, gBE und BE werden nicht geprüft)
        /// </summary>
        /// <returns>true, falls für jede Zone ein Rüstungswert gesetzt wurde</returns>
        public bool ZonenrüstungswerteVorhanden()
        {
            return (Kopf.HasValue && Brust.HasValue && Bauch.HasValue && Rücken.HasValue && RBein.HasValue && LBein.HasValue && LArm.HasValue && RArm.HasValue);
        }

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
            get { return "Rüstungen"; }
        }

        string BasarLogic.IHandelsgut.Tags
        {
            get { return Gruppe + (string.IsNullOrEmpty(Ausrüstung.Tags) ? string.Empty : ", " + Ausrüstung.Tags); }
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
                return string.Format("Ko {0} Br {1} Rü {2} Ba {3} LA {4} RA {5} LB {6} RB {7} gRS/RS {8}/{9} gBE/BE {10}/{11}",
                    Kopf, Brust, Rücken, Bauch, LArm, RArm, LBein, RBein, gRS, RS, gBE, BE)
                    + (string.IsNullOrEmpty(Bemerkung) ? string.Empty : Environment.NewLine + Bemerkung);
            }
        }

        #endregion

        #region IHasZonenRs
        [DependentProperty("Kopf")]
        public int RSKopf
        {
            get
            {
                return Kopf ?? 0;
            }
            set
            {
                Kopf = value;
            }
        }

        [DependentProperty("Brust")]
        public int RSBrust
        {
            get
            {
                return Brust ?? 0;
            }
            set
            {
                Brust = value;
            }
        }

        [DependentProperty("Rücken")]
        public int RSRücken
        {
            get
            {
                return Rücken ?? 0;
            }
            set
            {
                Rücken = value;
            }
        }

        [DependentProperty("LArm")]
        public int RSArmL
        {
            get
            {
                return LArm ?? 0;
            }
            set
            {
                LArm = value;
            }
        }

        [DependentProperty("RArm")]
        public int RSArmR
        {
            get
            {
                return RArm ?? 0;
            }
            set
            {
                RArm = value;
            }
        }

        [DependentProperty("Bauch")]
        public int RSBauch
        {
            get
            {
                return Bauch ?? 0;
            }
            set
            {
                Bauch = value;
            }
        }

        [DependentProperty("LBein")]
        public int RSBeinL
        {
            get
            {
                return LBein ?? 0;
            }
            set
            {
                LBein = value;
            }
        }

        [DependentProperty("RBein")]
        public int RSBeinR
        {
            get
            {
                return RBein ?? 0;
            }
            set
            {
                RBein = value;
            }
        }
        #endregion

        public static KampfLogic.IRüstungsschutz operator +(Rüstung a, KampfLogic.IRüstungsschutz b)
        {
            return KampfLogic.Rüstungsschutz.Add(new KampfLogic.Rüstungsschutz(a), b);
        }

        public static KampfLogic.IRüstungsschutz operator -(Rüstung a, KampfLogic.IRüstungsschutz b)
        {
            return KampfLogic.Rüstungsschutz.Substract(new KampfLogic.Rüstungsschutz(a), b);
        }

        public static KampfLogic.IRüstungsschutz operator *(Rüstung a, double b)
        {
            return KampfLogic.Rüstungsschutz.Multiply(new KampfLogic.Rüstungsschutz(a), b);
        }
    }
}
