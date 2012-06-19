using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.Logic.Kalender
{
    public class Zeitrechnung
    {
        public static Dictionary<Kalender, Zeitrechnung> ZeitrechnungenDictionary =
            new Dictionary<Kalender, Zeitrechnung>();

        static Zeitrechnung()
        {
            ZeitrechnungenDictionary.Add(Kalender.BosparansFall,
                new Zeitrechnung(Kalender.BosparansFall, "Bosparans Fall", true, 0, "BF", "v. BF"));
            ZeitrechnungenDictionary.Add(Kalender.Hal,
                new Zeitrechnung(Kalender.Hal, "Hal", true, 993, "Hal", "v. Hal"));
            ZeitrechnungenDictionary.Add(Kalender.Horas,
                new Zeitrechnung(Kalender.Horas, "Horas", false, -1491, "Horas", "v. Horas"));
            ZeitrechnungenDictionary.Add(Kalender.Reto,
                new Zeitrechnung(Kalender.Reto, "Reto", true, 975, "Reto", "v. Reto"));
            ZeitrechnungenDictionary.Add(Kalender.BardoCella,
                new Zeitrechnung(Kalender.BardoCella, "Bardo und Cella", true, 948, "Bardo und Cella", "v. Bardo und Cella"));
            ZeitrechnungenDictionary.Add(Kalender.Perval,
                new Zeitrechnung(Kalender.Perval, "Perval", true, 933, "Perval", "v. Perval"));
            ZeitrechnungenDictionary.Add(Kalender.Golgari,
                new Zeitrechnung(Kalender.Golgari, "Golgaris Erscheinen", false, 686, "GE", "v. GE"));
            ZeitrechnungenDictionary.Add(Kalender.AndergastNostria,
                new Zeitrechnung(Kalender.AndergastNostria, "Nach der Unabhängigkeit Andergasts/Nostrias", true, -854, "d.U.", "v. d.U."));
            ZeitrechnungenDictionary.Add(Kalender.Kurkum,
                new Zeitrechnung(Kalender.Kurkum, "Kurkum", true, 415, "Kurkum", "v. Kurkum"));
            ZeitrechnungenDictionary.Add(Kalender.JahreDesLichts,
                new Zeitrechnung(Kalender.JahreDesLichts, "Jahre des Lichts", true, 334, "Jahr des Lichts", "v. den Jahren des Lichts"));
            ZeitrechnungenDictionary.Add(Kalender.Thorwal,
                new Zeitrechnung(Kalender.Thorwal, "Jurgas Landung", false, -1627, "JL", "v. JL"));
            ZeitrechnungenDictionary.Add(Kalender.Engasal,
                new Zeitrechnung(Kalender.Engasal, "Nach der Engasal-Akte", true, -346, "nach der E.-Akte", "v. der E.-Akte"));
            ZeitrechnungenDictionary.Add(Kalender.Imperium,
                new Zeitrechnung(Kalender.Imperium, "Imperiale Zeitrechnung", true, -3747, "IZ", "v. IZ", new Datum(0, Monat.Firun, 4)));
            ZeitrechnungenDictionary.Add(Kalender.Rastullah,
                new Zeitrechnung(Kalender.Rastullah, "Rastullahs Erscheinen", false, 760, "Rastullah", "v. d. O.", new Datum(0, Monat.Boron, 23)));
        }

        public Zeitrechnung(Kalender kalender)
        {
            if (ZeitrechnungenDictionary.ContainsKey(kalender))
            {
                Zeitrechnung zeitRe = ZeitrechnungenDictionary[kalender];
                Name = zeitRe.Name;
                HatNullJahr = zeitRe.HatNullJahr;
                BeginnJahreszählung = zeitRe.BeginnJahreszählung;
                KürzelPlus = zeitRe.KürzelPlus;
                KürzelMinus = zeitRe.KürzelMinus;
            }
        }

        private Zeitrechnung(Kalender kalender, string zeitrechnung, bool jahrNull, int beginnZählung, string kzPlus, string kzMinus, Datum jahresBeginn = null)
        {
            Kalender = kalender;
            Name = zeitrechnung;
            HatNullJahr = jahrNull;
            BeginnJahreszählung = beginnZählung;
            KürzelPlus = kzPlus;
            KürzelMinus = kzMinus;
            VerschobenerJahresanfang = jahresBeginn;
        }

        public string Name { get; private set; }

        public Kalender Kalender { get; private set; }

        public string KürzelPlus { get; private set; }

        public string KürzelMinus { get; private set; }

        /// <summary>
        /// Besitzt die Zeitrcehnung ein Jahr '0'?
        /// </summary>
        public bool HatNullJahr { get; private set; }

        /// <summary>
        /// In welchem Jahr nach BF die Jahreszählung beginnt. Bei verschobenem Jahresanfang die 2. Jahreshälfte verwenden.
        /// </summary>
        public int BeginnJahreszählung { get; private set; }

        private Datum VerschobenerJahresanfang { get; set; }

        public static string JahrUmrechnenToString(Kalender von, Kalender nach, int jahr, Datum d = null)
        {
            string ergebnisString = string.Empty;

            if (!ZeitrechnungenDictionary.ContainsKey(nach))
                nach = Kalender.BosparansFall;

            int ergebnisJahr = JahrUmrechnen(von, nach, jahr, d);

            ergebnisString = ergebnisJahr >= 0 ? ergebnisJahr.ToString() + " " + ZeitrechnungenDictionary[nach].KürzelPlus
                : Math.Abs(ergebnisJahr).ToString() + " " + ZeitrechnungenDictionary[nach].KürzelMinus;

            return ergebnisString;
        }

        public static int JahrUmrechnen(Kalender von, Kalender nach, int jahr, Datum d = null)
        {
            int ergebnis = 0;

            if (!ZeitrechnungenDictionary.ContainsKey(von))
                von = Kalender.BosparansFall;
            if (!ZeitrechnungenDictionary.ContainsKey(nach))
                nach = Kalender.BosparansFall;

            if (!ZeitrechnungenDictionary[von].HatNullJahr)
                jahr = jahr == 0 ? 1 : jahr;

            int jahrInBF = jahr + ZeitrechnungenDictionary[von].BeginnJahreszählung;

            if (!ZeitrechnungenDictionary[von].HatNullJahr)
                jahrInBF -= jahr >= 0 ? 1 : 0;

            int jahrInNach = jahrInBF - ZeitrechnungenDictionary[nach].BeginnJahreszählung;

            if (ZeitrechnungenDictionary[nach].VerschobenerJahresanfang != null && d != null)
            {
                Datum jahresBeginn = ZeitrechnungenDictionary[nach].VerschobenerJahresanfang;
                if (d.Tageszahl() < jahresBeginn.Tageszahl())
                    jahrInNach--;
            }

            if (!ZeitrechnungenDictionary[nach].HatNullJahr)
                jahrInNach += jahrInNach >= 0 ? 1 : 0;

            ergebnis = jahrInNach;

            return ergebnis;
        }
    }
}
