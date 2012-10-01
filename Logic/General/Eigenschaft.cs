using System;
using MeisterGeister.Logic.General;

namespace MeisterGeister.LogicAlt.General
{
    public class Eigenschaft : IProbe
    {
        public Eigenschaft(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        public string Literatur
        {
            get { return "WdS 6"; }
        }

        public string WikiLink
        {
            get { return Name; }
        }

        public static MeisterGeister.LogicAlt.General.EigenschaftProbenErgebnis Probe(int wert, Held held = null, int mod = 0, uint wErgebnis = 0u)
        {
            var ergebnis = new MeisterGeister.LogicAlt.General.EigenschaftProbenErgebnis { Wert = wert, Held = held };

            if (wErgebnis == 0)
            {
                W20 w = new W20();
                w.Würfeln();
                ergebnis.Gewürfelt = (uint)w.Ergebnis;
            }
            else
                ergebnis.Gewürfelt = wErgebnis;
            ergebnis.Wert = wert;
            int diff = wert - mod - (int)ergebnis.Gewürfelt;
            ergebnis.Übrig = diff;
            ergebnis.Mod = mod;

            // Automatischer (Miss-)Erfolg bei erleichterten/erschwerten Proben
            if (mod > wert)
                ergebnis.Gelungen = false;
            else if (mod + wert > 20)
                ergebnis.Gelungen = true;
            else
                ergebnis.Gelungen = diff >= 0;

            return ergebnis;
        }

        public static void Probe(ref MeisterGeister.LogicAlt.General.EigenschaftProbenErgebnis probe, int mod = 0, uint wErgebnis = 0u)
        {
            if (probe != null)
            {
                if (wErgebnis == 0)
                {
                    W20 w = new W20();
                    w.Würfeln();
                    probe.Gewürfelt = (uint)w.Ergebnis;
                }
                else
                    probe.Gewürfelt = wErgebnis;
                int diff = probe.Wert - mod - (int) probe.Gewürfelt;
                probe.Übrig = diff;

                // Automatischer (Miss-)Erfolg bei erleichterten/erschwerten Proben
                if (mod > probe.Wert)
                    probe.Gelungen = false;
                else if (mod + probe.Wert > 20)
                    probe.Gelungen = true;
                else
                    probe.Gelungen = diff >= 0;
            }
        }

        public string ProbenText
        {
            get
            {
                string txt = String.Format("{0}-Probe", Name);
                return txt;
            }
        }
    }
}
