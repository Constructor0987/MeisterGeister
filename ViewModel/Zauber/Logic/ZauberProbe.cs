using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeisterGeister.Logic.General;
using MeisterGeister.Model;

namespace MeisterGeister.ViewModel.Zauber.Logic
{
    public class ZauberProbe : Probe
    {
        public Model.Zauber Zauber;
        Held held;
        public int Zauberdauer;
        public int Wirkungsdauer;
        public int Kosten;

        Repräsentation repräsentation;
        public Repräsentation Repräsentation
        {
            get { return repräsentation; }
            set { 
                repräsentation = value;

            }
        }

        public int ZfW
        {
            get { return Fertigkeitswert; }
            set { Fertigkeitswert = value; }
        }
        public bool Flächenzauber = false;
        /// <summary>
        /// Die Wirkungsdauer ist abhängig von ZfW oder ZfP*
        /// </summary>
        public bool WirkungsdauerAbhängigVomZfW = false;
        public bool Aufrechterhalten = false;
        object Zieltyp; //Objekt, mehrere Objekte, Zone, Wesen, Person, mehrere Wesen, mehrere Personen
        bool ZielIstFreiwillig;
        int MaximaleModifikationen
        {
            get
            {
                if (held == null || !held.Magiebegabt)
                    return 0;
                int leiteigenschaft = held.EigenschaftWert(Repräsentationen.GetLeitattribut(held, Repräsentation));
                return Math.Max(leiteigenschaft - 12, 0);
            }
        }

        //spontane Modifikationen (WdZ 18):

        int zauberdauerMod = 0;
        /// <summary>
        /// Veränderte Zauberdauer (WdZ 20)
        ///  -3 ZfP  x2
        ///  5 ZfP / 1/2
        ///  mindestens 1 Aktion
        /// </summary>
        public int ZauberdauerMod
        {
            get { return zauberdauerMod; }
            set { 
                if (value > 1) value = 1;
                zauberdauerMod = value;
            }
        }

        public int ZauberdauerModDauer
        {
            get { return (int)Math.Pow(2, ZauberdauerMod); }
        }

        public int ZauberdauerModZfP
        {
            get
            {
                if (ZauberdauerMod == 0)
                    return 0;
                else if (ZauberdauerMod == 1)
                    return -3;
                else
                    return ZauberdauerMod * -5;
            }
        }

        int kostenMod = 0;
        /// <summary>
        /// Zauberwirkung erzwingen (WdZ 20), Astralenergie einsparen (WdZ 21)
        ///  n>0
        ///  Erleichterung = n , Kosten = Math.Floor(2^(n-1)), Zauberdauer + n
        ///  -5<=n<0
        ///  Erschwernis = n*3, Zauberdauer + n , Kosten = Math.Max(1, original Kosten * n/10.0)
        /// </summary>
        public int KostenMod
        {
            get { return kostenMod; }
            set {
                if (value < -5) value = -5;
                kostenMod = value;
            }
        }

        public double KostenModKosten
        {
            get
            {
                if (KostenMod < 0)
                    return Math.Max(1, Kosten * KostenMod / 10.0);
                else if (KostenMod > 0)
                    return Math.Floor(Math.Pow(2, KostenMod - 1));
                return 0;
            }
        }

        public int KostenModDauer
        {
            get { return Math.Abs(kostenMod); }
        }

        public double KostenModZfP
        {
            get
            {
                if (KostenMod < 0)
                    return -3 * KostenMod;
                else if (KostenMod > 0)
                    return -KostenMod;
                return 0;
            }
        }

        int wirkungsdauerMod = 0;
        /// <summary>
        /// Modifikation der Wirkungsdauer (WdZ 22)
        ///  7 ZfP : 2^n
        ///  n<0
        ///  3 ZfP : 2^n, Kosten * (2/3)^n // ^n ist hier fraglich. das sollte in die Einstellungen/Regeln und schaltbar sein
        ///  Zauberdauer + Abs(n)
        /// </summary>
        public int WirkungsdauerMod
        {
            get { return wirkungsdauerMod; }
            set {
                wirkungsdauerMod = value; 
            }
        }

        public int WirkungsdauerModDauer
        {
            get { return Math.Abs(WirkungsdauerMod); }
        }

        public double WirkungsdauerModZfP
        {
            get
            {
                if (WirkungsdauerMod < 0)
                    return -3 * WirkungsdauerMod;
                else if (WirkungsdauerMod > 0)
                    return 7 * WirkungsdauerMod;
                return 0;
            }
        }

        public double WirkungsdauerModKosten
        {
            get {
                if (WirkungsdauerMod < 0)
                    return 1/3;
                return 0;
            }
        }

        bool wirkungsdauerFest = false;
        /// <summary>
        /// Von (A) auf feste Wirkungsdauer ändern.
        ///  7 + 1 je weitere Zeiteinheit ZfP
        ///  ZD + 1
        /// </summary>
        public bool WirkungsdauerFest
        {
            get { return wirkungsdauerFest; }
            set { wirkungsdauerFest = value; }
        }

        int wirkungsdauerFestMod = 0;
        /// <summary>
        /// Bonuseinheiten der festen Wirkungsdauer
        /// </summary>
        public int WirkungsdauerFestMod
        {
            get { return wirkungsdauerFestMod; }
            set {
                if (value < 0)
                    value = 0;
                wirkungsdauerFestMod = value; 
            }
        }

        public int WirkungsdauerFestZfP
        {
            get
            {
                if (WirkungsdauerFest && Aufrechterhalten)
                    return 7 + wirkungsdauerFestMod;
                return 0;
            }
        }

        public int WirkungsdauerFestDauer
        {
            get
            {
                if (WirkungsdauerFest && Aufrechterhalten)
                    return 1;
                return 0;
            }
        }

        int technikMod = 0;
        /// <summary>
        /// Veränderte Technik (WdZ 19)
        /// 7 ZfP / fehlender Komponente
        /// </summary>
        public int TechnikMod
        {
            get { return technikMod; }
            set
            {
                if (value < 0)
                    value = 0;
                technikMod = value;
            }
        }

        public int TechnikModZfP
        {
            get
            {
                return TechnikMod * 7;
            }
        }

        public int TechnikModDauer
        {
            get
            {
                return TechnikMod * 3;
            }
        }

        int zentraleKomponenteMod = 0;
        /// <summary>
        /// Veränderte Technik (WdZ 19)
        /// 12 ZfP / verletzter zentraler Komponente
        /// </summary>
        public int ZentraleKomponenteMod
        {
            get { return zentraleKomponenteMod; }
            set
            {
                if (value < 0)
                    value = 0;
                zentraleKomponenteMod = value;
            }
        }

        public int ZentraleKomponenteModZfP
        {
            get
            {
                return ZentraleKomponenteMod * 12;
            }
        }

        public int ZentraleKomponenteModDauer
        {
            get
            {
                return ZentraleKomponenteMod * 3;
            }
        }
        

        int reichweiteMod = 0;
        /// <summary>
        /// 
        /// </summary>
        public int ReichweiteMod
        {
            get { return reichweiteMod; }
            set { reichweiteMod = value; }
        }

        
        /*
         * Modifikation des Zielobjektes (WdZ 21)
         *  Zauberdauer + 1
         *  freiwillig -> unfreiwillig: 5 ZfP + MR
         *  unfreiwillig -> freiwillig: 2 ZfP + MR/2 (Ziel muss eine Aktion aufwenden, sonst volle MR)
         *  ein Ziel (freiwillig) -> mehrere Ziele: 3 + Anzahl ZfP 
         *  ein Ziel (unfreiwillig) -> mehrere Ziele: Anzahl + Max(MR) Erschwernis (keine ZfP abziehen, keine Auswirkung durch boni auf SpoMos)
         * Modifikation der Reichweite und des Wirkungsradius (WdZ 22)
         *  Kategorien: Selbst - Berührung - 1 Schritt (oder ZfW Spann) - 3 Schritt (oder ZfW/2 Schritt) - 7 Schritt (oder ZfW Schritt) - 21 Schritt (oder ZfW x 3 Schritt) - 49 Schritt (oder ZfW x 7 Schritt) - Horizont - Außer Sicht
         *  5 ZfP / Kategorie+
         *  Zonenzauber größerer Wirkungsradius: Kosten * 4 / Kategorie (jeweils für Basiskosten und laufende Kosten)
         *  3 ZfP / Kategorie-
         *  Zauberdauer + 1 / Kategorie
         */

        //wirkende vorteile/sonderfertigkeiten/Ritualgegenstände
        /*
         * Matrixverständnis
         * Fernzauberei
         * Stabzauber: für Kosten
         * Stabzauber: Modifikationsfokus
         * 
         */

        public ZauberProbe()
        {
        }
    }
}
