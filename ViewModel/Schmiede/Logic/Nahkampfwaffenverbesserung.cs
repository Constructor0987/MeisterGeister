using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.ViewModel.Schmiede.Logic
{
    public class Nahkampfwaffenverbesserung
    {
        #region //---- EIGENSCHAFTEN ----
        public String Name { set; get; }
        public double DauerFaktor { set; get; }
        public int ProbeErschwernis { set; get; }
        public int BfVerbesserung { set; get; }
        public int TpVerbesserung { set; get; }
        public int IniVerbesserung { set; get; }
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

    public class Materialien : System.Collections.Generic.List<Nahkampfwaffenverbesserung>
    {
        #region //---- KONSTRUKTOR ----
        public Materialien()
        {
            Add(new Nahkampfwaffenverbesserung("Standard (Waffenstahl)", 1, 0, 0, 0, 0, 0, 0, "", false, false, 1, 0));

            Add(new Nahkampfwaffenverbesserung("-- Bronze --", 1, 0, 0, 0, 0, 0, 0, "", false, false, 1, 0));
            Add(new Nahkampfwaffenverbesserung("Bronze", 1, 0, 2, 0, 0, 0, 0, "BF-Erhöhung -> AT/PA -1/-1 bis Waffe gerade gebogen wird (1 Akt).", false, false, 1, 0));
            //In Myranor
            if (Model.Setting.SettingAktiv(Model.Setting.MYRANOR_GUID))
            {
                Add(new Nahkampfwaffenverbesserung("Edelbronze", 1, 0, -1, 0, 0, 0, 0, "Berufsgeheimnis", true, true, 1, 0));
            }

            Add(new Nahkampfwaffenverbesserung("-- Stähle --", 1, 0, 0, 0, 0, 0, 0, "", false, false, 1, 0));
            Add(new Nahkampfwaffenverbesserung("schlechter Stahl", 1, 0, 0, 0, 0, 0, 0, "BF+1, falls TaW Grobschmied unter 15", false, false, 1, 0));
            //In Aventurien
            if (Model.Setting.SettingAktiv(Model.Setting.AVENTURIEN_GUID))
            {
                Add(new Nahkampfwaffenverbesserung("Grassodenerz", 1, 2, 1, 0, 0, 0, 0, "", false, false, 1, 0));
                Add(new Nahkampfwaffenverbesserung("Großer Fluss", 1, -1, 0, 0, 0, 0, 0, "", false, false, 1.5, 0));
                Add(new Nahkampfwaffenverbesserung("Khunchomer Stahl", 1, 0, 0, 0, 0, 0, 0, "BF+2 (bei Fechtwaffen), nicht bei TaW 18+", false, false, 1.5, 0));
                Add(new Nahkampfwaffenverbesserung("Maraskanstahl", 1, -1, -1, 0, 0, 0, 0, "gutes Material", true, false, 3, 0));
                Add(new Nahkampfwaffenverbesserung("Mirhamer Stahl", 1, 0, 0, 0, 0, 0, 0, "", false, false, 1, 0));
                Add(new Nahkampfwaffenverbesserung("Premer Stahl", 1, 2, 0, 0, 0, 0, 0, "BF+2 (bei schlanken Klingen)", false, false, 0.75, 0));
                Add(new Nahkampfwaffenverbesserung("Uhdenberger Stahl", 1, 2, 1, 0, 0, 0, 0, "", false, false, 0.75, 0));
                Add(new Nahkampfwaffenverbesserung("Zwergenstahl", 1, -1, -2, 0, 0, 0, 0, "gutes Material", true, false, 5, 0));
            }
            //In Myranor
            if (Model.Setting.SettingAktiv(Model.Setting.MYRANOR_GUID))
            {
                Add(new Nahkampfwaffenverbesserung("Meteoreisen", 1, 0, 0, 0, 0, 0, 0, "Nutzen bei Technomantie", false, false, 7, 0));
                Add(new Nahkampfwaffenverbesserung("Buntstahl", 1, 0, 0, 0, 0, 0, 0, "Durchgefärbter Waffenstahl", false, false, 1.5, 0));
                Add(new Nahkampfwaffenverbesserung("Leuchtbronze (Talglicht)", 1, 0, 0, 0, 0, 0, 0, "Leuchtet golden, hell wie ein Talglicht", false, false, 5, 0));
                Add(new Nahkampfwaffenverbesserung("Leuchtbronze (Fackel)", 1, 0, 0, 0, 0, 0, 0, "Leuchtet golden, hell wie eine Fackel", false, false, 10, 0));
                Add(new Nahkampfwaffenverbesserung("Leuchtstahl (Talglicht)", 1, 0, 0, 0, 0, 0, 0, "Leuchtet silbern, hell wie ein Talglicht", false, false, 5, 0));
                Add(new Nahkampfwaffenverbesserung("Leuchtstahl (Fackel)", 1, 0, 0, 0, 0, 0, 0, "Leuchtet silbern, hell wie eine Fackel", false, false, 10, 0));
            }
            Add(new Nahkampfwaffenverbesserung("Schwarzstahl (0,5% Endurium)", 1, 0, -1, 0, 0, 0, 0, "", true, true, 1, 15));

            Add(new Nahkampfwaffenverbesserung("-- magische Metalle --", 1, 0, 0, 0, 0, 0, 0, "", false, false, 1, 0));
            //In Aventurien
            if (Model.Setting.SettingAktiv(Model.Setting.AVENTURIEN_GUID))
            {
                Add(new Nahkampfwaffenverbesserung("Titanium (100%)", 4, 15, -7, 4, 0, 2, 2, "Waffe ist magisch", true, true, 1, 50000));
                Add(new Nahkampfwaffenverbesserung("Titanium (50%, +2/+1)", 3.5, 12, -6, 3, 0, 2, 1, "Waffe ist magisch", true, true, 1, 25000));
                Add(new Nahkampfwaffenverbesserung("Titanium (50%, +1/+2)", 3.5, 12, -6, 3, 0, 1, 2, "Waffe ist magisch", true, true, 1, 25000));
                Add(new Nahkampfwaffenverbesserung("Endurium (100%)", 2, 12, -5, 2, 0, 1, 1, "Waffe ist magisch", true, true, 1, 3000));
                Add(new Nahkampfwaffenverbesserung("Endurium (50%, +1/+0)", 1.5, 7, -4, 1, 0, 1, 0, "Waffe ist magisch", true, true, 1, 1500));
                Add(new Nahkampfwaffenverbesserung("Endurium (50%, +0/+1)", 1.5, 7, -4, 1, 0, 0, 1, "Waffe ist magisch", true, true, 1, 1500));
                Add(new Nahkampfwaffenverbesserung("Endurium (25%)", 1, 0, -1, 0, 0, 0, 0, "Waffe ist magisch", true, true, 1, 750));
                Add(new Nahkampfwaffenverbesserung("Mindorium (100%)", 1, 0, 0, 0, 0, 0, 0, "nur Streitkolben, o.ä.; Waffe ist magisch; verletzend gegen Geister; +1 mTP", true, true, 1, 50));
                Add(new Nahkampfwaffenverbesserung("Mindorium (75%)", 1, 0, 0, 0, 0, 0, 0, "nur Streitkolben, o.ä.; Waffe ist magisch; verletzend gegen Geister; +1 mTP", true, true, 1, 37.5));
                Add(new Nahkampfwaffenverbesserung("Mindorium (50%)", 1, 0, 0, 0, 0, 0, 0, "nur Streitkolben, o.ä.; Waffe ist magisch; +1 mTP", true, true, 1, 25));
                Add(new Nahkampfwaffenverbesserung("Arkanium (100%)", 1, 0, 0, 0, 0, 0, 0, "nur Streitkolben, o.ä.; Waffe ist magisch; +2 mTP", true, true, 1, 500));
                Add(new Nahkampfwaffenverbesserung("Arkanium (50%)", 1, 0, 0, 0, 0, 0, 0, "nur Streitkolben, o.ä.; Waffe ist magisch; +2 mTP", true, true, 1, 250));
                Add(new Nahkampfwaffenverbesserung("Arcanoferit", 1, 0, 0, 0, 0, 0, 0, "Vorteile wie Silberwaffen; Preis ist Mindestpreis", true, true, 1, 25));
                Add(new Nahkampfwaffenverbesserung("Wolfsstahl", 2, 7, -3, 1, 0, 0, 0, "", true, true, 8, 0));
            }
            //In Myranor
            if (Model.Setting.SettingAktiv(Model.Setting.MYRANOR_GUID))
            {
                Add(new Nahkampfwaffenverbesserung("Titaniumstahl (Myranor), +2/+1", 1, 12, -6, 0, 3, 2, 1, "Waffe ist magisch", false, false, 100, 0));
                Add(new Nahkampfwaffenverbesserung("Titaniumstahl (Myranor), +1/+2", 1, 12, -6, 0, 3, 1, 2, "Waffe ist magisch", false, false, 100, 0));
                Add(new Nahkampfwaffenverbesserung("Enduriumstahl (Myranor), +1/+0", 1, 7, -4, 0, 1, 1, 0, "Waffe ist magisch", false, false, 20, 0));
                Add(new Nahkampfwaffenverbesserung("Enduriumstahl (Myranor), +0/+1", 1, 7, -4, 0, 1, 0, 1, "Waffe ist magisch", false, false, 20, 0));
                Add(new Nahkampfwaffenverbesserung("Endurium (Myranor)", 1, 12, -5, 0, 2, 1, 1, "Waffe ist magisch", false, false, 50, 0));
                Add(new Nahkampfwaffenverbesserung("Arkaniumwaffe (Myranor)", 1, 0, 3, 0, 0, 0, 0, "Waffe ist magisch", false, false, 10, 0));
                Add(new Nahkampfwaffenverbesserung("Mindoriumbuntstahl (Myranor)", 1, 0, 0, 0, 0, 0, 0, "Waffe ist magisch", false, false, 5, 0));
            }
        }
        #endregion
    }

    public class Techniken : System.Collections.Generic.List<Nahkampfwaffenverbesserung>
    {
        #region //---- KONSTRUKTOR ----
        public Techniken()
        {
            Add(new Nahkampfwaffenverbesserung("Stahlwaffe", 1, 0, 0, 0, 0, 0, 0, "", false, false, 1, 0));
            Add(new Nahkampfwaffenverbesserung("Eisengusswaffe", 0.25, 0, 5, 0, 0, 0, 0, "", false, false, 0.75, 0));
            // in Myranor sind Eisengusswaffen günstiger
            if (Model.Setting.SettingAktiv(Model.Setting.MYRANOR_GUID))
            {
                Add(new Nahkampfwaffenverbesserung("Eisengusswaffe (Myranor)", 0.25, 0, 5, 0, 0, 0, 0, "", false, false, 0.5, 0));
            }
            Add(new Nahkampfwaffenverbesserung("Eisenwaffe", 1, -1, 2, 0, 0, 0, 0, "BF-Erhöhung -> AT/PA -1/-1 bis Waffe gerade gebogen wird (1 Akt).", false, false, 0.75, 0));
            Add(new Nahkampfwaffenverbesserung("Geflämmt (3fach)", 2, 1, -1, 0, 0, 0, 0, "Berufsgeheimnis; bei Material-Mix: teuerstes Material, beste BF-Reduzierung gilt.", false, true, 2, 0));
            Add(new Nahkampfwaffenverbesserung("Geflämmt (12fach)", 3, 2, -2, 0, 0, 0, 0, "Berufsgeheimnis; bei Material-Mix: teuerstes Material, beste BF-Reduzierung gilt.", false, true, 4, 0));
            Add(new Nahkampfwaffenverbesserung("Geflämmt (33fach)", 4, 5, -3, 1, 0, 0, 0, "Berufsgeheimnis; bei Material-Mix: teuerstes Material, beste BF-Reduzierung gilt.", false, true, 12, 0));
            Add(new Nahkampfwaffenverbesserung("Geflämmt (3fach - Fälschung)", 1, 2, 1, 0, 0, 0, 0, "bis 12 Punkte Aufschlag auf Erkennen (Erwerb und Beurteilung, Wds 181)", false, false, 2, 0));
            Add(new Nahkampfwaffenverbesserung("Geflämmt (12fach - Fälschung)", 1, 3, 1, 0, 0, 0, 0, "bis 12 Punkte Aufschlag auf Erkennen (Erwerb und Beurteilung, Wds 181)", false, false, 4, 0));
            Add(new Nahkampfwaffenverbesserung("Geflämmt (33fach - Fälschung)", 1, 4, 1, 0, 0, 0, 0, "bis 12 Punkte Aufschlag auf Erkennen (Erwerb und Beurteilung, Wds 181)", false, false, 12, 0));
            // nur in Aventurien
            if (Model.Setting.SettingAktiv(Model.Setting.AVENTURIEN_GUID))
            {
                Add(new Nahkampfwaffenverbesserung("Lehmbacktechnik", 1.5, 0, -1, 0, 0, 0, 0, "Berufsgeheimnis", false, true, 2, 0));
                Add(new Nahkampfwaffenverbesserung("Orkische Schmiede", 1, 2, -1, 0, 0, 0, 0, "z.B. Kharkush, Khezzara", false, false, 1.5, 0));
                Add(new Nahkampfwaffenverbesserung("Zwergenspan", 100, 7, -4, 2, 0, 0, 0, "Berufsgeheimnis; Waffen unverkäuflich", false, true, 1, 0));
            }
        }
        #endregion
    }
}
