using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using MeisterGeister.Logic.General;
using MeisterGeister.Model.Extensions;

namespace MeisterGeister.Model
{
    public partial class Talent : Probe
    {
        #region //---- PROBE ----

        [DependentProperty("Talentname")]
        override public string Probenname
        {
            get { return Talentname; }
            set { Talentname = value; }
        }

        override public int[] Werte
        {
            get 
            {
                if (_werte == null)
                    _werte = new int[3];
                return _werte;
            }
            set
            {
                _werte = value;
                _chanceBerechnet = false;
            }
        }

        override public string WerteNamen
        {
            get
            {
                return string.Format("({0}/{1}/{2})", Eigenschaft1, Eigenschaft2, Eigenschaft3);
            }
        }

        #endregion //---- PROBE ----

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

        public bool IsMetaTalent
        {
            get { return TalentgruppeID == 8; }
        }

        public string GetWikiLink()
        {
            if (!string.IsNullOrEmpty(WikiLink))
                return WikiLink.Replace(" ", "_");
            if (Talentname.StartsWith("Lesen/Schreiben"))
                return Talentname.Replace("Lesen/Schreiben (", "").TrimEnd(')').Replace(" ", "_");
            if (Talentname.StartsWith("Sprachen Kennen"))
                return Talentname.Replace("Sprachen Kennen (", "").TrimEnd(')').Replace(" ", "_");
            if (Talentname.StartsWith("Nahrung Sammeln"))
                return "Nahrung Sammeln";
            if (Talentname.StartsWith("Pirschjagd"))
                return "Pirschjagd";
            if (Talentname.StartsWith("Ansitzjagd"))
                return "Ansitzjagd";
            return Talentname.Replace(" ", "_");
        }
    }
}
