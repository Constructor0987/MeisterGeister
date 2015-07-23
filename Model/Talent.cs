using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using MeisterGeister.Logic.General;
using MeisterGeister.Model.Extensions;

namespace MeisterGeister.Model
{
    public partial class Talent : Probe, MeisterGeister.Logic.Literatur.ILiteratur
    {
        public const string UNTERGRUPPE_NAHKAMPF = "Bewaffneter Nahkampf";
        public const string UNTERGRUPPE_FERNKAMPF = "Fernkampf";
        public const string UNTERGRUPPE_ATTECHNIK = "Bewaffnete AT-Technik";
        public const string UNTERGRUPPE_WAFFENLOS = "Waffenloser Kampf";

        public const int GRUPPE_KAMPF = 1;
        public const int GRUPPE_META = 8;

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
                if (_werte == null || _werte.Length != 3)
                    _werte = new int[3];
                return _werte;
            }
            set
            {
                _werte = value;
                //_chanceBerechnet = false;
            }
        }

        override public string WerteNamen
        {
            get
            {
                return string.Format("({0}/{1}/{2})", Eigenschaft1, Eigenschaft2, Eigenschaft3);
            }
        }

        public void WerteSetzen(Held h, string spezialisierung = null)
        {
            Fertigkeitswert = h.Talentwert(this);
            if (spezialisierung != null)
            {
                List<string> spezLi = Talentspezialisierungen(h);
                if (spezLi != null && spezLi.Contains(spezialisierung))
                Fertigkeitswert += 2;
            }
            Werte = new int[] { h.EigenschaftWert(Eigenschaft1), h.EigenschaftWert(Eigenschaft2), h.EigenschaftWert(Eigenschaft3) };
        }

        #endregion //---- PROBE ----

        public bool HatVoraussetzungen
        {
            get { return !String.IsNullOrEmpty(Voraussetzungen); }
        }

        public List<string> Talentspezialisierungen(Held h)
        {
            //TODO ??: bei GUID Umstellung statt Sonderfertigkeit.Name evtl auf GUID prüfen
            if (h.Held_Sonderfertigkeit != null)
            {
                List<string> r = h.Held_Sonderfertigkeit.Where(hs => hs.Sonderfertigkeit.Name == "Talentspezialisierung" && hs.Wert != null && hs.Wert.StartsWith(Talentname)).OrderBy(hs => hs.Wert).Select(hs => hs.Wert).ToList();
                //r.ForEach(w => w = Talent.GetSpezialisierungName(Talentname, w));
                for (int i = 0; i < r.Count; i++)
                    r[i] = Talent.GetSpezialisierungName(Talentname, r[i]);
                return r;
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
            get { return TalentgruppeID == GRUPPE_META; }
        }

        public bool IsKampfTalent
        {
            get { return TalentgruppeID == GRUPPE_KAMPF; }
        }

        public string Name
        {
            get { return Talentname; }
        }
    }
}
