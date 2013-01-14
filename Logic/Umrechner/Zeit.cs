using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.Logic.Umrechner
{
    public class Zeit : System.Collections.Generic.Dictionary<string, double>
    {
        public Zeit()
        {
            Add("-- Aventurische Zeiteinheiten --", 0);
            Add("Praioslauf", 86400);
            Add("Mond", 2592000);
            Add("Götterlauf", 31536000);

            Add("-- Regeltechnische Zeiteinheiten --", 0);
            Add("Aktion", 1.5);
            Add("Kampfrunde (KR)", 3);
            Add("Spielrunde (SR)", 300);
            Add("Zeiteinheit", 7200);

            Add("-- Irdische Zeiteinheiten --", 0);
            Add("Sekunde", 1);
            Add("Minute", 60);
            Add("Stunde", 3600);
            Add("Tag", 86400);
            Add("Woche", 604800);
            Add("Monat", 2592000);
            Add("Jahr", 31536000);
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
