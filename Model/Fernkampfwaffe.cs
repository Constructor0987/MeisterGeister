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
    public partial class Fernkampfwaffe : BasarLogic.IHandelsgut, InventarLogic.IAusrüstung, KampfLogic.IWaffeMitTPKK, MeisterGeister.Logic.Literatur.ILiteratur
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
                    + (((TPBonus ?? 0) != 0) ? ((TPBonus > 0) ? "+" : "-") + TPBonus.ToString() : String.Empty);
            }
        }

        public string TPReichweiten
        {
            get
            {
                return ((TPSehrNah == null) ? "-" : TPSehrNah.Value.ToString()) + "/" + ((TPNah == null) ? "-" : TPNah.Value.ToString()) + "/" + ((TPMittel == null) ? "-" : TPMittel.Value.ToString()) + "/" + ((TPWeit == null) ? "-" : TPWeit.Value.ToString()) + "/" + ((TPSehrWeit == null) ? "-" : TPSehrWeit.Value.ToString());
            }
            set
            {
                var rw = SplitReichweitenString(value);
                if (rw == null)
                    return;
                TPSehrNah = rw[0];
                TPNah = rw[1];
                TPMittel = rw[2];
                TPWeit = rw[3];
                TPSehrWeit = rw[4];
            }
        }

        public string Reichweiten
        {
            get
            {
                return ((RWSehrNah == null) ? "-" : RWSehrNah.Value.ToString()) + "/" + ((RWNah == null) ? "-" : RWNah.Value.ToString()) + "/" + ((RWMittel == null) ? "-" : RWMittel.Value.ToString()) + "/" + ((RWWeit == null) ? "-" : RWWeit.Value.ToString()) + "/" + ((RWSehrWeit == null) ? "-" : RWSehrWeit.Value.ToString());
            }
            set
            {
                var rw = SplitReichweitenString(value);
                if(rw == null)
                    return;
                RWSehrNah = rw[0];
                RWNah = rw[1];
                RWMittel = rw[2];
                RWWeit = rw[3];
                RWSehrWeit = rw[4];
            }
        }

        #endregion

        #region Helper-Methoden
        public static int?[] SplitReichweitenString(string rwString)
        {
            if(rwString == null)
                return null;
            rwString = rwString.Trim('(', ')', ' ', '\t');
            var rw = rwString.Split('/');
            if (rw.Length != 5)
                return null;
            int?[] outArray = new int?[5];
            for (int i = 0; i < 5; i++)
            {
                int o = Int32.MinValue;
                outArray[i] = null;
                if(Int32.TryParse(rw[i].Trim(), out o))
                    outArray[i] = o;
            }
            return outArray;
        }
        #endregion

        public Fernkampfwaffe()
        {
            Ausrüstung = new Ausrüstung();
        }

        public bool Usergenerated
        {
            get { return !FernkampfwaffeGUID.ToString().StartsWith("00000000-0000-0000-00"); }
        }

        public int TPKKBonus(Held held)
        {
            return Waffe.TPKKBonus(held, this);
        }

        public override string ToString()
        {
            //Name TP, TP/KK, RW, TP/RW, Laden
            string TPAusdauer = AusdauerSchaden ? "(A)" : string.Empty;
            string TPVerwundend = Verwundend ? "*" : string.Empty;
            return string.Format("{0}: {1}{2} TP{3}, TP/KK {4}/{5}, Reichweite {6}, {7}, Laden {8}",
                Name, TPString, TPVerwundend, TPAusdauer, TPKKSchwelle, TPKKSchritt, Reichweiten, TPReichweiten, Laden);
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
