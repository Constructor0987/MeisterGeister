using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.Logic.General
{
    public static class WikiAventurica
    {
        public const string URL = @"http://www.wiki-aventurica.de/wiki/";

        public static void OpenBrowser(string artikel)
        {
            System.Diagnostics.Process.Start(URL + artikel.Replace(" ", "_"));
        }

        public static void OpenBrowser(Probe probe)
        {
            if (probe != null && probe is Model.Talent)
            {
                OpenBrowser(probe as Model.Talent); return;
            }
            else if (probe != null && probe is Model.Zauber)
            {
                OpenBrowser((probe as Model.Zauber).Name); return;
            }
            else if (probe != null)
            {
                OpenBrowser(probe.Probenname); return;
            }
            OpenBrowser(string.Empty);
        }

        public static void OpenBrowser(Model.Talent talent)
        {
            if (talent != null && !string.IsNullOrEmpty(talent.WikiLink))
            {
                OpenBrowser(talent.WikiLink); return;
            }
            if (talent != null && talent.Talentname.StartsWith("Lesen/Schreiben"))
            {
                OpenBrowser(talent.Talentname.Replace("Lesen/Schreiben (", "").TrimEnd(')')); return;
            }
            if (talent != null && talent.Talentname.StartsWith("Sprachen Kennen"))
            {
                OpenBrowser(talent.Talentname.Replace("Sprachen Kennen (", "").TrimEnd(')')); return;
            }
            if (talent != null && talent.Talentname.StartsWith("Nahrung Sammeln"))
            {
                OpenBrowser("Nahrung Sammeln"); return;
            }
            if (talent != null && talent.Talentname.StartsWith("Pirschjagd"))
            {
                OpenBrowser("Pirschjagd"); return;
            }
            if (talent != null && talent.Talentname.StartsWith("Ansitzjagd"))
            {
                OpenBrowser("Ansitzjagd"); return;
            }
            OpenBrowser(talent.Talentname);
        }
    }
}
