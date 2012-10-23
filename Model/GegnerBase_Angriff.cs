using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using MeisterGeister.ViewModel.Kampf.Logic;

namespace MeisterGeister.Model
{
    public partial class GegnerBase_Angriff : IWaffe, IFernkampfwaffe, INahkampfwaffe
    {
        #region IWaffe
        public int TPKKBonus
        {
            get { return 0; }
        }
        #endregion

        #region IFernkampfwaffe
        public int? RWSehrNah
        {
            get { return Reichweite; }
        }

        public int? RWNah
        {
            get { return Reichweite; }
        }

        public int? RWMittel
        {
            get { return Reichweite; }
        }

        public int? RWWeit
        {
            get { return Reichweite; }
        }

        public int? RWSehrWeit
        {
            get { return Reichweite; }
        }

        public int? TPSehrNah
        {
            get { return 0; }
        }

        public int? TPNah
        {
            get { return 0; }
        }

        public int? TPMittel
        {
            get { return 0; }
        }

        public int? TPWeit
        {
            get { return 0; }
        }

        public int? TPSehrWeit
        {
            get { return 0; }
        }
        #endregion

        #region INahkampfwaffe
        public Distanzklasse Distanzklasse
        {
            get { return Waffe.ParseDistanzklasse(DK); }
            set { DK = Waffe.DistanzklasseToString(value); }
        }
        #endregion

        private static Regex
                reName = new Regex("^((?!(AT|PA|TP|DK))[\\w]+(?:\\s+(?!(AT|PA|TP|DK|:))[\\w]+)*)", RegexOptions.CultureInvariant & RegexOptions.IgnoreCase & RegexOptions.Compiled),
                reAttacke = new Regex("(?<![\\w\\d])(?:AT|FK)\\s+(\\d+)(?!\\w)", RegexOptions.CultureInvariant & RegexOptions.IgnoreCase & RegexOptions.Compiled),
                reWürfel = new Regex("(?<!\\w)(\\d+)?W(\\d+)?([\\+-])?(\\d?)(?!\\w)", RegexOptions.CultureInvariant & RegexOptions.IgnoreCase & RegexOptions.Compiled),
                reSchadensart = new Regex("(?<![\\w\\d])(TP(\\(A\\))?|SP)(?![\\w\\d])", RegexOptions.CultureInvariant & RegexOptions.IgnoreCase & RegexOptions.Compiled),
                reDistanzklasse = new Regex("(?<![\\w\\d])(H|HN|N|NS|HNS|S|SP|NSP|HNSP|P|X|PX|SPX|NSPX|HNSPX)(?![\\w\\d])", RegexOptions.CultureInvariant & RegexOptions.IgnoreCase & RegexOptions.Compiled),
                reReichweite = new Regex("(?<!\\w)(?:(\\d+)\\sSchritt|\\((\\d+|-)/(\\d+|-)/(\\d+|-)/(\\d+|-)/(\\d+|-)\\))(?![\\w\\d])", RegexOptions.CultureInvariant & RegexOptions.IgnoreCase & RegexOptions.Compiled);
        public static GegnerBase_Angriff Parse(string zeile)
        {
            if (zeile == null || zeile.Length < 4)
                return null;
            Match m;

            m = reAttacke.Match(zeile);
            if (m == null || !m.Success)
                return null;
            int attacke = 0;
            Int32.TryParse(m.Groups[1].Captures[0].Value, out attacke);

            m = reWürfel.Match(zeile);
            if (m == null || !m.Success)
                return null;
            int wAnzahl = 1, wSeiten = 6, tpBonus = 0;
            if(m.Groups[1] != null)
                Int32.TryParse(m.Groups[1].Captures[0].Value, out wAnzahl);
            if (m.Groups[2] != null)
                Int32.TryParse(m.Groups[2].Captures[0].Value, out wSeiten);
            if (m.Groups[3] != null)
            {
                if (m.Groups[4] != null)
                    Int32.TryParse(m.Groups[4].Captures[0].Value, out tpBonus);
                if (m.Groups[3].Value == "-")
                    tpBonus *= -1;
            }

            //m = reSchadensart.Match(zeile); //TODO: Kein Feld für Ausdauerschaden/SP
            
            string name = "Angriff";
            m = reName.Match(zeile);
            if (m != null && m.Success)
            {
                name = m.Groups[1].Captures[0].Value;
            }

            string dk = null;
            m = reDistanzklasse.Match(zeile);
            if (m != null && m.Success)
            {
                dk = m.Groups[1].Captures[0].Value.ToUpperInvariant();
            }

            int? reichweite = null;
            m = reDistanzklasse.Match(zeile);
            if (m != null && m.Success)
            {
                int rw = 0;
                if (m.Groups[1] != null && m.Groups[1].Success)
                    Int32.TryParse(m.Groups[1].Captures[0].Value, out rw);
                else
                {
                    for (int i = 6; i >= 2; i--)
                    {
                        if (m.Groups[i] != null && m.Groups[i].Success && m.Groups[i].Captures[0].Value != "-")
                        {
                            Int32.TryParse(m.Groups[i].Captures[0].Value, out rw);
                            break;
                        }
                    }
                }
                reichweite = rw;
            }
            
            GegnerBase_Angriff a = new GegnerBase_Angriff();
            a.Name = name;
            a.TPWürfelAnzahl = wAnzahl;
            a.TPWürfel = wSeiten;
            a.TPBonus = tpBonus;
            a.DK = dk;
            a.AT = attacke;
            a.Reichweite = reichweite;
            return a;
        }
    }
}
