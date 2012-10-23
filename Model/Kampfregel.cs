using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MeisterGeister.Model
{
    public partial class Kampfregel
    {
        public Kampfregel()
        {
            KampfregelGUID = Guid.NewGuid();
            Name = "Neue Kampfregel";
            Manöver = false;
        }

        public bool Usergenerated
        {
            get { return !KampfregelGUID.ToString().StartsWith("00000000-0000-0000-00"); }
        }

        private static Regex reErschwernis = new Regex("((?:\\w+\\s)*\\w+)\\s?\\((\\d+)\\)", RegexOptions.CultureInvariant & RegexOptions.IgnoreCase & RegexOptions.Compiled);

        public static IEnumerable<Kampfregel> Parse(string zeile, out Dictionary<string, int> erschwernisse)
        {
            erschwernisse = new Dictionary<string, int>();

            if (zeile == null || zeile.Length < 2)
                return null;

            foreach (Match m in reErschwernis.Matches(zeile))
            {
                if(m.Success)
                    erschwernisse.Add(m.Groups[1].Value, Int32.Parse(m.Groups[2].Value));
            }

            string zeileOhneErschwernis = reErschwernis.Replace(zeile, "$1");
            IEnumerable<Kampfregel> r;
            r = Global.ContextHeld.Liste<Kampfregel>().Where(kr =>
                    kr.Name.ToUpperInvariant() == zeileOhneErschwernis.Trim().ToUpperInvariant()
                );
            if(r.Count() != 0)
                return r;
            string[] words = zeileOhneErschwernis.Trim().ToUpperInvariant().Split(new char[] { ',', '/', ';', '\n', '\t', '\r' }).Where(z => z != null && z.Trim() != String.Empty).ToArray();
            r = Global.ContextHeld.Liste<Kampfregel>().Where(kr =>
                    words.Any(w => kr.Name.ToUpperInvariant().Contains(w))
                );
            if (r.Count() != 0)
                return r;
            return null;
        }
    }
}
