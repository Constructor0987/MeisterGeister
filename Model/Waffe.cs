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
    public partial class Waffe : BasarLogic.IHandelsgut, InventarLogic.IAusrüstung, KampfLogic.IWaffeMitTPKK
    {
        public Waffe()
        {
            Ausrüstung = new Ausrüstung();
            Ausrüstung.AusrüstungGUID = WaffeGUID = Guid.NewGuid();
        }

        public bool Usergenerated
        {
            get { return !WaffeGUID.ToString().StartsWith("00000000-0000-0000-000"); }
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

        public static int TPKKBonus(Held held, KampfLogic.IWaffeMitTPKK waffe)
        {
            if(waffe == null || held == null || !MeisterGeister.Logic.Settings.Regeln.TPKK)
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

        public string Setting
        {
            get
            {
                return Ausrüstung.Setting;
            }
            set
            {
                Ausrüstung.Setting = value;
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
                return t;
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
                return String.Format("{0} TP{1} {2}/{3} TP/KK, DK {4}, WM {5}/{6}", TPString, (bool)AusdauerSchaden ? "(A)" : "", TPKKSchwelle, TPKKSchritt, DK, WMAT, WMPA);
            }
        }

        #endregion
    }
}
