using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public static IEnumerable<Kampfregel> Parse(string zeile)
        {
            if (zeile == null || zeile.Length < 2)
                return null;
            IEnumerable<Kampfregel> r;
            r = Global.ContextHeld.Liste<Kampfregel>().Where(kr => 
                    kr.Name.ToUpperInvariant() == zeile.Trim().ToUpperInvariant()
                );
            if(r.Count() != 0)
                return r;
            string[] words = zeile.Trim().ToUpperInvariant().Split(new char[] { ',', '/', ';', '\n', '\t', '\r' }).Where(z => z != null && z.Trim() != String.Empty).ToArray();
            r = Global.ContextHeld.Liste<Kampfregel>().Where(kr =>
                    words.Any(w => kr.Name.ToUpperInvariant().Contains(w))
                );
            if (r.Count() != 0)
                return r;
            return null;
        }
    }
}
