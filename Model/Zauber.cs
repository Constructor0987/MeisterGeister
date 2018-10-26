using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeisterGeister.Model.Extensions;

namespace MeisterGeister.Model
{
    public partial class Zauber : MeisterGeister.Logic.General.Probe, MeisterGeister.Logic.Literatur.ILiteratur
    {
        #region //---- PROBE ----

        [DependentProperty("Name")]
        override public string Probenname
        {
            get { return Name; }
            set { Name = value; }

        }

        override public int[] Werte
        {
            get
            {
                if (_werte == null || _werte.Length != 3)
                    _werte = new int[3];
                return _werte;
            }
            set
            {
                _werte = value;
                //_chanceBerechnet = false;
            }
        }

        override public string WerteNamen
        {
            get
            {
                return string.Format("({0}/{1}/{2})", Eigenschaft1, Eigenschaft2, Eigenschaft3);
            }
        }

        #endregion //---- PROBE ----

        public bool Usergenerated
        {
            get { return !ZauberGUID.ToString().StartsWith("00000000-0000-0000-00"); }
        }

        public Setting Setting
        {
            get
            {
                var a_s = Zauber_Setting.Where(s => s.SettingGUID == Setting.AktuellesSettingGUID).FirstOrDefault();
                if (a_s == null)
                    a_s = Zauber_Setting.FirstOrDefault();
                if (a_s == null)
                    return null;
                return a_s.Setting;
            }
        }

        public string Repräsentationen
        {
            get
            {
                var a_s = Zauber_Setting.Where(s => s.SettingGUID == Setting.AktuellesSettingGUID).FirstOrDefault();
                if (a_s == null)
                    a_s = Zauber_Setting.FirstOrDefault();
                if (a_s == null)
                    return null;
                return a_s.Repräsentationen;
            }
            set
            {
                var a_s = Zauber_Setting.Where(s => s.SettingGUID == Setting.AktuellesSettingGUID).FirstOrDefault();
                if (a_s == null)
                    a_s = Zauber_Setting.FirstOrDefault();
                if (a_s == null)
                    return;
                a_s.Repräsentationen = value;
                OnChanged("Repräsentationen");
            }
        }


        
        //public System.Windows.Media.SolidColorBrush MerkmalColor1
        //{
        //    get
        //    {
        //        List<string> lstMerkmale = new List<string>();
        //        lstMerkmale = this.Merkmale.Trim(' ').Split(',').ToList();
        //        return möglicheMerkmale.FirstOrDefault(t => t.b == lstMerkmale[0]) != null? möglicheMerkmale.FirstOrDefault(t => t.b == lstMerkmale[0]).c: System.Windows.Media.Brushes.Transparent;
        //    }
        //}
        //public System.Windows.Media.SolidColorBrush MerkmalColor2
        //{
        //    get
        //    {
        //        List<string> lstMerkmale = new List<string>();
        //        lstMerkmale = this.Merkmale.Split(',').ToList();
        //        return lstMerkmale.Count >= 2? 
        //            (möglicheMerkmale.FirstOrDefault(t => t.b.Trim(' ') == lstMerkmale[1]) != null ? möglicheMerkmale.FirstOrDefault(t => t.b == lstMerkmale[1]).c : System.Windows.Media.Brushes.Transparent):
        //            System.Windows.Media.Brushes.Transparent;
        //    }
        //}

        //TODO DB-Feld Kosten in mehrere Werte aufspalten:
        public int LEKosten = 0;
        public int AsPKosten = 0;
        public int LEKostenProZeiteinheit = 0;
        public int AsPKostenProZeiteinheit = 0;
        public int Zeiteinheit = 0; // TODO Zeiteinheiten definieren (Akt, KR, SR, ...)
        //TODO Ziel oder Zone? gibt es Zonenzauber mit zusätzlichen Zielkosten?
        //TODO Kosten je Ziel, wie bei ABVENUM oder ADAMANTIUM: pro angefangenem Stein Gewicht
        public int LEKostenProZiel = 0;
        public int AsPKostenProZiel = 0;
        public string Zieleinheit = ""; // TODO Zieleinheiten definieren: Raumschritt, angefangene 5 Stein, Illusionskomponente, ...
        //TODO Minimale Kosten wie bei ALPGESTALT: mindestens aber 6 AsP
        public int AsPKostenMin = 0;
        public int LEKostenMin = 0;
        //TODO Kostenmod wie bei ARMATRUTZ: RS*RS -ZfP*/2 AsP
        //TODO Sonderkosten wie bei ALPGESTALT: MR des Opfers AsP
        
        //TODO Variable Kosten je nach Variante wie beim ADLERSCHWINGE: boden 1 AsP, amphibisch 2 AsP, fliegend 3 AsP - sehr hohe LE, Giftwirkung, etc.
        //TODO Variable Kosten mit Schwellen je nach Zielgewicht/größe wie beim ANIMATIO
        
        //TODO Schelme haben geringere Zauberkosten
        
        

        public bool Zonenzauber = false; //TODO aus Zielobjekt ablesen
        public bool Freiwillig = false; //TODO aus Zielobjekt ablesen
        public bool MehrereZiele = false; //TODO aus Zielobjekt ablesen

    }
}
