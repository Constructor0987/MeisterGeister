using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.Logic.Kalender
{
    /// <summary>
    /// Noch unverwendetete Klasse. Könnte eventuell für eine bessere Strukturierung oder detailliertere 
    /// Informationen in den Kalenderklassen anstelle des WochentagsEnum verwendet werden.
    /// </summary>
    public class Woche
    {
        private static Dictionary<Kalender, Woche> wochen = new Dictionary<Kalender, Woche>(12);
        static Woche()
        {
            string[] namen;
            Woche w;
            //Woche der Zwölfgötter
            namen = new string[] { "Praiostag", "Rohalstag", "Feuertag", "Wassertag", "Windstag", "Erdstag", "Markttag" };
            w = new Woche(namen);
            

            wochen.Add(Kalender.BosparansFall, w);
            wochen.Add(Kalender.AndergastNostria, w);
            wochen.Add(Kalender.Bornland, w);
            wochen.Add(Kalender.Hal, w);
            wochen.Add(Kalender.Horas, w);
            wochen.Add(Kalender.Imperium, w);
            wochen.Add(Kalender.JahreDesLichts, w);
            wochen.Add(Kalender.Reto, w);
            wochen.Add(Kalender.Thorwal, w);
        }

        public static Woche GetWoche(Kalender kal)
        {
            return wochen[kal];
        }

        public Woche(string[] tagesNamen)
        {
            Tage = tagesNamen;
        }

        public string[] Tage { get; private set; }
    }
}
