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
        private Model.Zauber zauber = null;
        public Model.Zauber Zauber
        {
            get
            {
                return zauber;
            }
            set
            {
                zauber = value;
            }
        }

        Held held = null;
        public Held Held
        {
            get { return held; }
            set { held = value; }
        }

        public int Zauberdauer
        {
            get
            {
                int zd = zauber.Zauberdauer ?? 0;
                if (!held.HatSonderfertigkeitUndVoraussetzungen("Matrixverständnis"))
                    zd += ZentraleKomponenteModDauer + TechnikModDauer + ReichweiteModDauer + KostenModDauer + WirkungsdauerModDauer + WirkungsdauerFestDauer + ZielModDauer + WirkungsradiusModDauer;
                zd *= ZauberdauerModDauer;
                return Math.Max(1, zd);
            }
        }

        public int Wirkungsdauer;
        public int Kosten
        {
            get 
            {
                if (Zauber == null)
                    return 0;
                //TODO Zauberkosten string -> int. besser gleich in der DB anders ablegen.
                double kosten = Math.Round(Int32.Parse(Zauber.Kosten) * WirkungsdauerModKosten * KostenModKosten, MidpointRounding.AwayFromZero);
                if(held.HatSonderfertigkeitUndVoraussetzungen("Stabzauber: Kraftfokus"))
                    kosten--;
                //TODO SF Kraftkontrolle, dann 1 Punkt Erschöpfung und -1 AsP
                return (int)Math.Max(kosten, 1);
            }
        }
        public int LEKosten
        {
            get
            {
                if (Zauber == null)
                    return 0;
                //TODO Zauberkosten string -> int.
                return (int)Math.Round(Int32.Parse(Zauber.Kosten) * WirkungsdauerModKosten, MidpointRounding.AwayFromZero);
            }
        }

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
        /// <summary>
        /// Die Wirkungsdauer ist abhängig von ZfW oder ZfP*
        /// </summary>
        public bool WirkungsdauerAbhängigVomZfW = false;
        public bool Aufrechterhalten = false;
        object Zieltyp; //Objekt, mehrere Objekte, Zone, Wesen, Person, mehrere Wesen, mehrere Personen

        public int MaximaleModifikationen
        {
            get
            {
                if (held == null || !held.Magiebegabt)
                    return 0;
                int leiteigenschaft = held.EigenschaftWert(Repräsentationen.GetLeitattribut(held, Repräsentation));
                int mods = Math.Max(leiteigenschaft - 12, 0);
                if (held.HatSonderfertigkeitUndVoraussetzungen("Matrixverständnis"))
                    mods += 1;
                if (held.HatSonderfertigkeitUndVoraussetzungen("Stabzauber: Modifikationsfokus"))
                    mods += 1;
                return mods;
            }
        }

        public int Modifikationen
        {
            get
            {
                int mods = 0;
                mods += Math.Abs(ZauberdauerMod);
                if (KostenMod > 0)
                    mods++;
                else
                    mods += Math.Abs(KostenMod);
                //TODO ....
                return mods;
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
                //TODO bei Rep Sch oder Bor keine Verdopplung möglich
                if (value > 0 && (Repräsentation == Repräsentationen.GetRepräsentation("Sch") || Repräsentation == Repräsentationen.GetRepräsentation("Bor")))
                    value = 0;
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
                {
                    if (held.HatSonderfertigkeitUndVoraussetzungen("Repräsentation: Gildenmagier")) //TODO und Held_Zauber hat Mag
                        return -4;
                    return -3;

                }
                else
                    return ZauberdauerMod * -5;
            }
        }

        int kostenMod = 0;
        /// <summary>
        /// Zauberwirkung erzwingen (WdZ 20), Astralenergie einsparen (WdZ 21)
        ///  n>0
        ///  Erleichterung = n , Kosten = Math.Floor(2^(n-1)), Zauberdauer + n, zählt nur als eine Modifikation
        ///  -5<=n<0
        ///  Erschwernis = n*3, Zauberdauer + n , Kosten = Math.Max(1, original Kosten * n/10.0)
        /// </summary>
        public int KostenMod
        {
            get { return kostenMod; }
            set {
                //In Rep Sch kein Erzwingen und Einsparen, Bor kein Einsparen
                if (Repräsentation == Repräsentationen.GetRepräsentation("Sch"))
                    value = 0;
                if (value < 0 && Repräsentation == Repräsentationen.GetRepräsentation("Bor"))
                    value = 0;
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
            get { return Math.Abs(KostenMod); }
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
        /// ZD + 3 / fehlender Komponente
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
        /// ZD + 3 / verletzter zentraler Komponente
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

        Dictionary<string, int> reichweiten;
        int GetReichweitenKategorie(string rw)
        {
            if(!reichweiten.ContainsKey(rw))
                return 0;
            return reichweiten[rw];
        }
        
        public bool ReichweiteAbhängigVomZfW = false;
        int reichweiteMod = 0;
        /// <summary>
        /// Modifikation der Reichweite (WdZ 22)
        /// 5 ZfP / Kategorie+
        /// 3 ZfP / Kategorie-
        /// Kategorien: Selbst - Berührung - 1 Schritt (oder ZfW Spann) - 3 Schritt (oder ZfW/2 Schritt) - 7 Schritt (oder ZfW Schritt) - 21 Schritt (oder ZfW x 3 Schritt) - 49 Schritt (oder ZfW x 7 Schritt) - Horizont - Außer Sicht
        /// Zauberdauer + 1 / Kategorie
        /// </summary>
        public int ReichweiteMod
        {
            get { return reichweiteMod; }
            set { reichweiteMod = value; } //TODO Maxwerte, Schelm maximal 49 schritt
        }

        public int ReichweiteModZfP
        {
            get
            {
                if (ReichweiteMod > 0)
                {
                    if(Repräsentation != null && Repräsentation.Kürzel == "Bor")
                        return ReichweiteMod * 7;
                    return ReichweiteMod * 5;
                }
                else
                    return ReichweiteMod * 3;
            }
        }

        public int ReichweiteModDauer
        {
            get
            {
                return ReichweiteMod;
            }
        }

        public bool WirkungsradiusAbhängigVomZfW = false;
        int wirkungsradiusMod = 0;
        /// <summary>
        /// Modifikation des Wirkungsradius (WdZ 22)
        /// 5 ZfP / Kategorie+
        /// 3 ZfP / Kategorie-
        /// Kategorien: Selbst - Berührung - 1 Schritt (oder ZfW Spann) - 3 Schritt (oder ZfW/2 Schritt) - 7 Schritt (oder ZfW Schritt) - 21 Schritt (oder ZfW x 3 Schritt) - 49 Schritt (oder ZfW x 7 Schritt) - Horizont - Außer Sicht
        /// Zauberdauer + 1 / Kategorie
        /// </summary>
        public int WirkungsradiusMod
        {
            get { return wirkungsradiusMod; }
            set { wirkungsradiusMod = value; } //TODO Maxwerte
        }

        public int WirkungsradiusModZfP
        {
            get
            {
                if (WirkungsradiusMod > 0)
                    return WirkungsradiusMod * 5;
                else
                    return WirkungsradiusMod * 3;
            }
        }

        public int WirkungsradiusModDauer
        {
            get
            {
                return WirkungsradiusMod;
            }
        }

        /// <summary>
        /// Modifikation des Wirkungsradius bei Zonenzaubern ergibt einen Faktor auf die Kosten des Spruchs.
        /// Zonenzauber größerer Wirkungsradius: Kosten * 4 / Kategorie (jeweils für Basiskosten und laufende Kosten)
        /// </summary>
        public int WirkungsradiusModKosten
        {
            get
            {
                if (Zauber == null || !Zauber.Zonenzauber || WirkungsradiusMod <= 0)
                    return 1;
                return WirkungsradiusMod * 4;
            }
        }

        bool zielIstFreiwillig = false;
        /// <summary>
        /// Modifikation des Zielobjektes (WdZ 21)
        ///  Zauberdauer + 1
        ///  freiwillig -> unfreiwillig: 5 ZfP + MR
        ///  unfreiwillig -> freiwillig: 2 ZfP + MR/2 (Ziel muss eine Aktion aufwenden, sonst volle MR)
        ///  </summary>
        public bool ZielIstFreiwillig
        {
            get { return zielIstFreiwillig; }
            set { zielIstFreiwillig = value; }
        }

        private int zielAnzahl = 1;
        /// <summary>
        ///  ein Ziel (freiwillig) -> mehrere Ziele: 3 + Anzahl ZfP 
        ///  ein Ziel (unfreiwillig) -> mehrere Ziele: Anzahl + Max(MR) Erschwernis (keine ZfP abziehen, keine Auswirkung durch Boni auf SpoMos)
        /// </summary>
        public int ZielAnzahl
        {
            get { return zielAnzahl; }
            set
            {
                if (value < 1)
                    value = 1;
                zielAnzahl = value; 
            }
        }

        public int ZielModZfp
        {
            get
            {
                int zfpmod = 0;
                if (ZielAnzahl > 1 && !Zauber.MehrereZiele)
                {
                    if (ZielIstFreiwillig)
                        zfpmod += 3 + ZielAnzahl;
                }
                if (Zauber.Freiwillig && !ZielIstFreiwillig)
                    zfpmod += 5;
                if (!Zauber.Freiwillig && ZielIstFreiwillig)
                    zfpmod += 2;
                return 0; 
            }
        }

        public int ZielModErschwernis
        {
            get
            {
                int zfpmod = 0;
                if (!Zauber.Freiwillig && ZielIstFreiwillig)
                    zfpmod += 0; //TODO + MR/2
                else if (!ZielIstFreiwillig)
                    zfpmod += 0; //TODO + Max(MR)
                if(!ZielIstFreiwillig)
                    zfpmod += ZielAnzahl;
                return zfpmod;
            }
        }

        public int ZielModDauer
        {
            get
            {
                int d = 0;
                if (Zauber.Freiwillig && !ZielIstFreiwillig)
                    d++;
                if (ZielAnzahl > 1 && !Zauber.MehrereZiele)
                    d++;
                return d; 
            }
        }
        
        //wirkende vorteile/sonderfertigkeiten/Ritualgegenstände
        /*
         * Fernzauberei
         */

        public ZauberProbe(Model.Held_Zauber hz)
        {
            Zauber = hz.Zauber;
            ZielIstFreiwillig = Zauber.Freiwillig;
            Held = hz.Held;

            var reichweiten = new Dictionary<string, int>();
            reichweiten.Add("Selbst", 0);
            reichweiten.Add("Berührung", 1);
            reichweiten.Add("1 Schritt", 2); reichweiten.Add("ZfW Spann", 2);
            reichweiten.Add("3 Schritt", 3); reichweiten.Add("ZfW/2 Schritt", 3);
            reichweiten.Add("7 Schritt", 4); reichweiten.Add("ZfW Schritt", 4);
            reichweiten.Add("21 Schritt", 5); reichweiten.Add("ZfW x 3 Schritt", 5);
            reichweiten.Add("49 Schritt", 6); reichweiten.Add("ZfW x 7 Schritt", 6);
            reichweiten.Add("Horizont", 7);
            reichweiten.Add("Außer Sicht", 8);

            //TODO expression parser für reichweiten und kosten schreiben. Dieser muss Kategorien extrahieren und ersetzen können.#
            //TODO Passt eine Kategorie nicht, wird die nächste genommen. ZfP* entspricht zur Bestimmung 3,5.
            //TODO Ist die Reichweite deutlich mehr als 49 Schritt (Zonenzauber/Sichtkomponente überprüfen), wird die Reichweite je Stufe verdoppelt.
            if (Zauber.Reichweite.Contains("ZfW"))
                ReichweiteAbhängigVomZfW = true;
            if (Zauber.Wirkungsradius.Contains("ZfW"))
                WirkungsradiusAbhängigVomZfW = true;
            if (Zauber.Wirkungsdauer.Contains("ZfW"))
                WirkungsdauerAbhängigVomZfW = true;
        }
    }
}
