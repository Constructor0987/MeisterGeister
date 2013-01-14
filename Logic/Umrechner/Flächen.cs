using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.Logic.Umrechner
{
    public class Flächen : System.Collections.Generic.Dictionary<string, double>
    {
        public Flächen()
        {
            // siehe im Wiki unter
            // Aventurische Maße

            Add("-- Menschliche Flächenmaße --", 0);
            Add("Rechtspann", 0.04);
            Add("Rechtschritt", 1);
            Add("Platz", 625);
            Add("Morgan (gorisch)", 3000);
            Add("Acker", 10000);
            Add("Rechtmeile", 1000000);
            Add("Land", 4000000);

            Add("-- Irdische Flächenmaße --", 0);
            Add("Quadratkilometer (km²)", 1000000);
            Add("Hektar (ha)", 10000);
            Add("Ar (a)", 100);
            Add("Quadratmeter (m²)", 1);
            Add("Quadratdezimeter (dm²)", 0.01);
            Add("Quadratzentimeter (cm²)", 0.0001);
            Add("Quadratmillimeter (mm²)", 0.000001);
        }

        public double WertUmrechnen(string von, string nach, double? wert)
        {
            double ergebnis = 0;

            if (ContainsKey(von) && ContainsKey(nach))
                ergebnis = (double)wert * this[von] / this[nach];

            return ergebnis;
        }
    }
}
