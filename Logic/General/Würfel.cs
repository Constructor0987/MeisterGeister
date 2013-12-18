using System;

namespace MeisterGeister.Logic.General
{
    /// <summary>
    /// Beschreibt einen Würfel.
    /// </summary>
    public class Würfel
    {
        private uint _seiten;
        private WürfelErgebnis _ergebnis;
        private static readonly Random ZahlenGenerator = RandomNumberGenerator.Generator;
        private static Logic.Würfel.Parser _parser;
        private static Logic.Würfel.Scanner _scanner;

        static Würfel()
        {
            _scanner = new Logic.Würfel.Scanner();
            _parser = new Logic.Würfel.Parser(_scanner);
        }

        public static int Parse(string w)
        {
            Logic.Würfel.ParseTree pt = _parser.Parse(w);
            if (pt.Errors.Count != 0)
                //TODO ??: Fehlermeldung ausgeben?
                return 0;
            try
            {
                return (int)pt.Eval();
            }
            catch { return 0; }
        }

        public static int Parse(string w, bool random)
        {
            Logic.Würfel.ParseTree pt = _parser.Parse(w);
            if (pt.Errors.Count != 0)
                //TODO ??: Fehlermeldung ausgeben?
                return 0;
            try
            {
                return (int)pt.Eval(random);
            }
            catch { return 0; }
        }

        public static bool Validate(string w)
        {
            Logic.Würfel.ParseTree pt = _parser.Parse(w);
            if (pt.Errors.Count != 0)
                return false;
            return true;
        }

        public static int Wurf(int seiten, int anzahl)
        {
            //TODO hier einen Dialog starten?
            int result = 0;
            for (int i = 1; i <= anzahl; i++)
                result += RandomNumberGenerator.RandomInt(1, seiten);
            return result;
        }

        public static int Wurf(int seiten)
        {
            return RandomNumberGenerator.RandomInt(1, seiten);
        }

        /// <summary>
        /// Erzeugt einen Würfel mit einer bestimmten Seitenzahl.
        /// </summary>
        /// <param name="s">Seitenzahl des Würfels.</param>
        public Würfel(uint s)
        {
            _seiten = s;
        }

        public uint Seiten
        {
            get
            {
                return _seiten;
            }
            set
            {
                _seiten = value;
            }
        }

        /// <summary>
        /// Letztes Würfelergebnis.
        /// </summary>
        public WürfelErgebnis ErgebnisDetails
        {
            get
            {
                return _ergebnis;
            }
        }

        /// <summary>
        /// Letztes Würfelergebnis.
        /// </summary>
        public int Ergebnis
        {
            get
            {
                return _ergebnis.Summe;
            }
        }

        public double Erwartungswert()
        {
            return (_seiten + 1.0) / 2.0;
        }

        /// <summary>
        /// Berechnet den Erwartungswert des Würfel-Ergebnisses.
        /// </summary>
        /// <param name="anzahl">Anzahl der Würfel, z.B. 2W, 5W.</param>
        /// <param name="mod">Addiert diesen Wert auf den Würfel, z.B. 3W6+2.</param>
        /// <returns></returns>
        public double Erwartungswert(uint anzahl, int mod)
        {
            return anzahl * Erwartungswert() + mod;
        }

        /// <summary>
        /// Gibt eine ganzzahlige Zufallszahl zürück, die zwischen 0 und der Seitenzahl+1 des Würfels liegt.
        /// </summary>
        public uint Würfeln()
        {
            int e = ZahlenGenerator.Next(1, Convert.ToInt32(_seiten) + 1);
            _ergebnis = new WürfelErgebnis(1);
            _ergebnis.Ergebnisse.Add(e);
            _ergebnis.EinzelwürfeListe.Add(e);
            return Convert.ToUInt32(_ergebnis.Summe);
        }

        public WürfelErgebnis Würfeln(uint anzahl, uint wiederholungen = 1, int mod = 0)
        {
            WürfelErgebnis e = new WürfelErgebnis(anzahl);
            int wurf = 0, würfel = 0;

            for (int i = 0; i < wiederholungen; i++)
            {
                wurf = 0;
                for (int j = 0; j < anzahl; j++)
                {
                    würfel = 0;
                    würfel = (int)Würfeln();
                    wurf += würfel;
                    e.EinzelwürfeListe.Add(würfel);
                }
                e.Ergebnisse.Add(wurf + mod);
            }

            _ergebnis = e;
            return _ergebnis;
        }

        public static double Erwartungswert(int seiten, int anzahl, int wiederholungen, int mod)
        {
            return wiederholungen * (anzahl * (seiten + 1.0) / 2.0 + mod);
        }

        public static void PlaySound()
        {
            try
            {
                AudioPlayer.PlayWürfel();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override string ToString()
        {
            return String.Format("W{0}", Seiten);
        }

    }

    public class WürfelErgebnis
    {
        public WürfelErgebnis(uint anzahl)
        {
            Anzahl = anzahl;
            Ergebnisse = new System.Collections.Generic.List<int>();
            EinzelwürfeListe = new System.Collections.Generic.List<int>();
        }

        private uint Anzahl = 1;
        public System.Collections.Generic.List<int> Ergebnisse { get; set; }
        public System.Collections.Generic.List<int> EinzelwürfeListe { get; set; }

        public int Summe
        {
            get
            {
                int sum = 0;
                foreach (int e in Ergebnisse)
                    sum += e;
                return sum;
            }
        }

        public string Staffel
        {
            get
            {
                string s = string.Empty;
                foreach (int e in Ergebnisse)
                {
                    if (s != string.Empty)
                        s += " | ";
                    s += e.ToString();
                }
                return s;
            }
        }

        public string Einzelwürfe
        {
            get
            {
                string s = string.Empty;
                int i = 1;
                foreach (int e in EinzelwürfeListe)
                {
                    if (s != string.Empty)
                    {
                        if (i - 1 == Anzahl)
                        {
                            s += " | ";
                            i = 1;
                        }
                        else
                            s += ",";
                    }
                    s += e.ToString();
                    i++;
                }
                return s;
            }
        }
    }

    /// <summary>
    /// Beschreibt einen W20.
    /// </summary>
    public class W20 : Würfel
    {
        public W20()
            : base(20)
        {
        }
    }

    /// <summary>
    /// Beschreibt einen W6.
    /// </summary>
    public class W6 : Würfel
    {
        public W6()
            : base(6)
        {
        }
    }

    /// <summary>
    /// Beschreibt einen W^3.
    /// </summary>
    public class W3 : Würfel
    {
        public W3()
            : base(3)
        {
        }
    }

    /// <summary>
    /// Beschreibt einen W^3.
    /// </summary>
    public class W10 : Würfel
    {
        public W10()
            : base(10)
        {
        }
    }

    /// <summary>
    /// Beschreibt verschiedene Würfel.
    /// </summary>
    public enum WürfelEnum
    {
        _1W3 = 3,
        _1W6 = 6,
        _2W6 = 12,
        _1W10 = 10,
        _1W20 = 20
    }
}
