using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// Eigene Usings
using BasarLogic = MeisterGeister.ViewModel.Basar.Logic;
using InventarLogic = MeisterGeister.ViewModel.Inventar.Logic;
using KampfLogic = MeisterGeister.ViewModel.Kampf.Logic;

namespace MeisterGeister.Model
{
    public partial class Fernkampfwaffe : BasarLogic.IHandelsgut, InventarLogic.IAusrüstung, KampfLogic.IWaffeMitTPKK
    {
        #region //---- EIGENSCHAFTEN FÜR HANDELSGUT INTERFACE ----

        public string Kategorie
        {
            get { return "Fernkampfwaffe"; }
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
                return String.Format("{0} TP{1}, Reichweiten: {2}, TP-Entfernung: {3}", TPString, (bool)AusdauerSchaden ? "(A)" : "", Reichweiten, TPReichweiten);
            }
        }

        #endregion

        #region //---- EIGENSCHAFTEN ----

        public string TPString
        {
            get
            {
                return String.Format("{0}W{1}", TPWürfelAnzahl, TPWürfel)
                    + ((TPBonus != 0) ? ((TPBonus > 0) ? "+" : "-") + TPBonus.ToString() : String.Empty);
            }
        }

        public string TPReichweiten
        {
            get
            {
                return TPSehrNah + "/" + TPNah + "/" + TPMittel + "/" + TPWeit + "/" + TPSehrWeit;
            }
        }

        public string Reichweiten
        {
            get
            {
                return RWSehrNah + "/" + RWNah + "/" + RWMittel + "/" + RWWeit + "/" + RWSehrWeit;
            }
        }

        #endregion

        public Fernkampfwaffe()
        {
            Ausrüstung = new Ausrüstung();
            Ausrüstung.AusrüstungGUID = FernkampfwaffeGUID = Guid.NewGuid();
        }

        public bool Usergenerated
        {
            get { return !FernkampfwaffeGUID.ToString().StartsWith("00000000-0000-0000-00"); }
        }

        public int TPKKBonus(Held held)
        {
            return Waffe.TPKKBonus(held, this);
        }

        #region IAusrüstung
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
    }
}
