using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.ViewModel.Schmiede.Logic
{
    class Nahkampfwaffenverbesserung
    {
        #region //---- EIGENSCHAFTEN ----
        public String Name { set; get; }
        public double DauerFaktor { set; get; }
        public int ProbeErschwernis { set; get; }
        public int BfVerbesserung { set; get; }
        public int TpVerbesserung { set; get; }
        public int IniVerbesserung {set; get; }
        public int AtWmVerbesserung { set; get; }
        public int PaWmVerbesserung { set; get; }
        public String Anmerkung { set; get; }
        public bool GutesMaterial { set; get; }
        public bool Berufsgeheimnis { set; get; }
        public double PreisFaktor { set; get; }
        public double PreisProUnzeInSilber { set; get; }
        #endregion

        #region //---- KONSTRUKTOR ----
        public Nahkampfwaffenverbesserung(String name, double dauerFaktor, int probeErschwernis, int bfVerbesserung, int tpVerbesserung, int iniVerbesserung, int atWmVerbesserung, int paWmVerbesserung, String anmerkung, bool gutesMaterial, bool berufsgeheimnis, double preisFaktor, double preisProUnzeInSilber)
        {
            Name = name;
            DauerFaktor = dauerFaktor;
            ProbeErschwernis = probeErschwernis;
            BfVerbesserung = bfVerbesserung;
            TpVerbesserung = tpVerbesserung;
            IniVerbesserung = iniVerbesserung;
            AtWmVerbesserung = atWmVerbesserung;
            PaWmVerbesserung = paWmVerbesserung;
            Anmerkung = anmerkung;
            GutesMaterial = gutesMaterial;
            Berufsgeheimnis = berufsgeheimnis;
            PreisFaktor = preisFaktor;
            PreisProUnzeInSilber = preisProUnzeInSilber;
        }
        #endregion
    }

    class Materialien : System.Collections.Generic.List<Nahkampfwaffenverbesserung>
    {
        public Materialien()
        {
            Add(new Nahkampfwaffenverbesserung("Standard", 1, 0, 0, 0, 0, 0, 0, "", false, false, 1, 0));

            Add(new Nahkampfwaffenverbesserung("-- Bronze --", 1, 0, 0, 0, 0, 0, 0, "", false, false, 1, 0));
            Add(new Nahkampfwaffenverbesserung("Bronze", 1, 0, 2, 0, 0, 0, 0, "BF-Erhöhung -> AT/PA -1/-1 bis Waffe gerade gebogen wird.", false, false, 1, 0));

            Add(new Nahkampfwaffenverbesserung("-- Stähle --", 1, 0, 0, 0, 0, 0, 0, "", false, false, 1, 0));
            Add(new Nahkampfwaffenverbesserung("Grassodenerz", 1, 2, 1, 0, 0, 0, 0, "", false, false, 1, 0));
            Add(new Nahkampfwaffenverbesserung("Großer Fluss", 1, -1, 0, 0, 0, 0, 0, "", false, false, 1.5, 0));
            Add(new Nahkampfwaffenverbesserung("Khunchomer Stahl", 1, 0, 0, 0, 0, 0, 0, "BF+2 (bei Fechtwaffen), nicht bei TaW 18+", false, false, 1.5, 0));
            Add(new Nahkampfwaffenverbesserung("Maraskanstahl", 1, -1, -1, 0, 0, 0, 0, "gutes Material", true, false, 3, 0));
            Add(new Nahkampfwaffenverbesserung("Mirhamer Stahl", 1, 0, 0, 0, 0, 0, 0, "", false, false, 1, 0));
            Add(new Nahkampfwaffenverbesserung("Premer Stahl", 1, 2, 0, 0, 0, 0, 0, "BF+2 (bei schlanken Klingen)", false, false, 0.75, 0));
            Add(new Nahkampfwaffenverbesserung("Uhdenberger Stahl", 1, 2, 1, 0, 0, 0, 0, "", false, false, 0.75, 0));
            Add(new Nahkampfwaffenverbesserung("Zwergenstahl", 1, -1, -2, 0, 0, 0, 0, "gutes Material", true, false, 5, 0));

            Add(new Nahkampfwaffenverbesserung("-- magische Metalle --", 1, 0, 0, 0, 0, 0, 0, "", false, false, 1, 0));
            Add(new Nahkampfwaffenverbesserung("Titanium (100%)", 4, 15, -7, 4, 0, 2, 2, "Waffe ist magisch", true, true, 1, 50000));
            Add(new Nahkampfwaffenverbesserung("Titanium (50%, +2/+1)", 3.5, 12, -6, 3, 0, 2, 1, "Waffe ist magisch", true, true, 1, 25000));
            Add(new Nahkampfwaffenverbesserung("Titanium (50%, +1/+2)", 3.5, 12, -6, 3, 0, 1, 2, "Waffe ist magisch", true, true, 1, 25000));
            Add(new Nahkampfwaffenverbesserung("Endurium (100%)", 2, 12, -5, 2, 0, 1, 1, "Waffe ist magisch", true, true, 1, 3000));
            Add(new Nahkampfwaffenverbesserung("Endurium (50%, +1/+0)", 1.5, 7, -4, 1, 0, 1, 0, "Waffe ist magisch", true, true, 1, 1500));
            Add(new Nahkampfwaffenverbesserung("Endurium (50%, +0/+1)", 1.5, 7, -4, 1, 0, 0, 1, "Waffe ist magisch", true, true, 1, 1500));
            Add(new Nahkampfwaffenverbesserung("Endurium (25%)", 1, 0, -1, 0, 0, 0, 0, "Waffe ist magisch", true, true, 1, 750));
            Add(new Nahkampfwaffenverbesserung("Endurium (0,5%, Schwarzstahl)", 1, 0, -1, 0, 0, 0, 0, "", true, true, 1, 15));
            Add(new Nahkampfwaffenverbesserung("Mindorium (100%)", 1, 0, 0, 0, 0, 0, 0, "nur Streitkolben, o.ä.; Waffe ist magisch; verletzend gegen Geister; +1 mTP", true, true, 1, 50));
            Add(new Nahkampfwaffenverbesserung("Mindorium (75%)", 1, 0, 0, 0, 0, 0, 0, "nur Streitkolben, o.ä.; Waffe ist magisch; verletzend gegen Geister; +1 mTP", true, true, 1, 37.5));
            Add(new Nahkampfwaffenverbesserung("Mindorium (50%)", 1, 0, 0, 0, 0, 0, 0, "nur Streitkolben, o.ä.; Waffe ist magisch; +1 mTP", true, true, 1, 25));
            Add(new Nahkampfwaffenverbesserung("Arkanium (100%)", 1, 0, 0, 0, 0, 0, 0, "nur Streitkolben, o.ä.; Waffe ist magisch; +2 mTP", true, true, 1, 500));
            Add(new Nahkampfwaffenverbesserung("Arkanium (50%)", 1, 0, 0, 0, 0, 0, 0, "nur Streitkolben, o.ä.; Waffe ist magisch; +2 mTP", true, true, 1, 250));
            Add(new Nahkampfwaffenverbesserung("Arcanoferit", 1, 0, 0, 0, 0, 0, 0, "Vorteile wie Silberwaffen; Preis ist Mindestpreis", true, true, 1, 25));
            Add(new Nahkampfwaffenverbesserung("Wolfsstahl", 2, 7, -3, 1, 0, 0, 0, "", true, true, 8, 0));
        }
    }

    class Techniken : System.Collections.Generic.List<Nahkampfwaffenverbesserung>
    {
        public Techniken()
        {
            Add(new Nahkampfwaffenverbesserung("Stahlwaffe", 1, 0, 0, 0, 0, 0, 0, "", false, false, 1, 0));
            Add(new Nahkampfwaffenverbesserung("Eisengusswaffe", 0.25, 0, 5, 0, 0, 0, 0, "", false, false, 0.75, 0));
            Add(new Nahkampfwaffenverbesserung("Eisenwaffe", 1, -1, 2, 0, 0, 0, 0, "", false, false, 0.75, 0));
            Add(new Nahkampfwaffenverbesserung("Geflämmt (3fach)", 2, 1, -1, 0, 0, 0, 0, "Berufsgeheimnis", false, true, 2, 0));
            Add(new Nahkampfwaffenverbesserung("Geflämmt (12fach)", 3, 2, -2, 0, 0, 0, 0, "Berufsgeheimnis", false, true, 4, 0));
            Add(new Nahkampfwaffenverbesserung("Geflämmt (33fach)", 4, 5, -3, 1, 0, 0, 0, "Berufsgeheimnis", false, true, 12, 0));
            Add(new Nahkampfwaffenverbesserung("Geflämmt (3fach - Fälschung)", 1, 2, 1, 0, 0, 0, 0, "bis 12 Punkte Aufschlag auf Erkennen (Erwerb und Beurteilung, Wds 181)", false, false, 2, 0));
            Add(new Nahkampfwaffenverbesserung("Geflämmt (12fach - Fälschung)", 1, 3, 1, 0, 0, 0, 0, "bis 12 Punkte Aufschlag auf Erkennen (Erwerb und Beurteilung, Wds 181)", false, false, 4, 0));
            Add(new Nahkampfwaffenverbesserung("Geflämmt (33fach - Fälschung)", 1, 4, 1, 0, 0, 0, 0, "bis 12 Punkte Aufschlag auf Erkennen (Erwerb und Beurteilung, Wds 181)", false, false, 12, 0));
            Add(new Nahkampfwaffenverbesserung("Lehmbacktechnik", 1.5, 0, -1, 0, 0, 0, 0, "Berufsgeheimnis", false, true, 2, 0));
            Add(new Nahkampfwaffenverbesserung("Orkische Schmiede", 1, 2, -1, 0, 0, 0, 0, "z.B. Kharkush, Khezzara", false, false, 1.5, 0));
            Add(new Nahkampfwaffenverbesserung("Zwergenspan", 100, 7, -4, 2, 0, 0, 0, "Berufsgeheimnis; Waffen unverkäuflich", false, true, 1, 0));
        }
    }
}
