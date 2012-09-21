using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MeisterGeister.Model
{
    public partial class Talent : MeisterGeister.Logic.General.Probe
    {
        public List<string> Talentspezialisierungen(Held h)
        {
            //TODO ??: bei GUID Umstellung statt Sonderfertigkeit.Name evtl auf GUID prüfen
            if (h.Held_Sonderfertigkeit != null)
            {
                List<string> r = h.Held_Sonderfertigkeit.Where(hs => hs.Sonderfertigkeit.Name == "Talentspezialisierung" && hs.Wert != null && hs.Wert.StartsWith(Talentname)).OrderBy(hs => hs.Wert).Select(hs => hs.Wert).ToList();
                r.ForEach(w => w = Talent.GetSpezialisierungName(Talentname, w));
            }
            return null;
        }

        public static string GetSpezialisierungName(string talentname, string wert)
        {
            Regex regex = new Regex(talentname + " \\((.+)\\)");
            return regex.Replace(wert, "$1");
        }
    }
}
