using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.Logic.Umrechner
{
    public class Massen : System.Collections.Generic.Dictionary<string, double>
    {
        public Massen()
        {
            // siehe im Wiki unter
            // Aventurische Maße

            Add("-- Menschliche Gewichtsmaße --", 0);
            Add("Gran", 0.00004);
            Add("Karat", 0.0002);
            Add("Skrupel", 0.001);
            Add("Unze", 0.025);
            Add("Stein", 1);
            Add("Sack", 100);
            Add("Quader", 1000);

            Add("-- Zwergische Gewichtsmaße --", 0);
            Add("Brox", 0.01);
            Add("Boruk", 0.125);
            Add("Brok", 6.5);

            Add("-- Irdische Gewichtsmaße --", 0);
            Add("Zentner", 50);
            Add("Tonne (t)", 1000);
            Add("Kilogramm (kg)", 1);
            Add("Pfund", 0.5);
            Add("Gramm (g)", 0.001);
            Add("Milligramm (mg)", 0.000001);
            Add("Mikrogramm (µg)", 0.000000001);
        }

        public double WertUmrechnen(string von, string nach, double wert)
        {
            double ergebnis = 0;

            if (ContainsKey(von) && ContainsKey(nach))
                ergebnis = wert * this[von] / this[nach];

            return ergebnis;
        }
    }
}
