using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.Logic.Umrechner
{
    public class Volumen : System.Collections.Generic.Dictionary<string, double>
    {
        public Volumen()
        {
            // siehe im Wiki unter
            // Aventurische Maße

            Add("-- Menschliche Volumenmaße --", 0);
            Add("Flux/Hohlfinger", 0.000008);
            Add("Schank/Viertelmaß", 0.0002);
            Add("Maß", 0.0008);
            Add("Urn/Hohlspann", 0.008);
            Add("Fass", 0.08);
            Add("Ox", 0.96);
            Add("Raumschritt", 1);

            Add("-- Zwergische Volumenmaße --", 0);
            Add("Baroscht", 0.0008);
            Add("Baroschtrom", 0.0608);

            Add("-- Irdische Volumenmaße --", 0);
            Add("Kubikmeter (m³)", 1);
            Add("Liter (l)", 0.001);
            Add("Milliliter (ml)", 0.000001);
            Add("Kubikzentimeter (cm³)", 0.000001);
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
