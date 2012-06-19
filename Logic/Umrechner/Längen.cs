using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.Logic.Umrechner
{
    public class Längen : System.Collections.Generic.Dictionary<string, double>
    {
        public Längen()
        {
            // siehe im Wiki unter
            // Aventurische Maße

            Add("-- Menschliche Längenmaße --", 0);
            Add("Halbfinger", 0.01);
            Add("Finger", 0.02);
            Add("Spann", 0.2);
            Add("Kaiserarm", 0.8);
            Add("Schritt", 1);
            Add("Lot", 10);
            Add("Meile", 1000);
            Add("Tagesreise (nivesisch)", 12000);
            Add("Baryd (novadisch)", 15000);

            Add("-- Zwergische Längenmaße --", 0);
            Add("Rim", 0.004);
            Add("Drom", 0.28);
            Add("Drumod", 1.7);
            Add("Drasch", 6.7);
            Add("Dumad", 74);
            Add("Dorgrosch", 1180);
            Add("Pakasch", 25000);

            Add("-- Irdische Längenmaße --", 0);
            Add("Meter (m)", 1);
            Add("Kilometer (km)", 1000);
            Add("Dezimeter (dm)", 0.1);
            Add("Zentimeter (cm)", 0.01);
            Add("Millimeter (mm)", 0.001);
            Add("Zoll/inch (in.)", 0.0254);
            Add("Fuß/foot (ft.)", 0.3048);
            Add("Schritt/yard (yd.)", 0.9144);
            Add("Meile/mile (mi.)", 1609.344);
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
