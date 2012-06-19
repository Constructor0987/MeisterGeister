using System;
using System.ComponentModel;
using MeisterGeister.Daten;
using System.Xml;
using System.Collections.Generic;

namespace MeisterGeister.Logic.General
{
    public class Sonderfertigkeit
    {
        public const string Aufmerksamkeit = "Aufmerksamkeit";
        public const string Ausfall = "Ausfall";
        public const string DefensiverKampfstil = "Defensiver Kampfstil";
        public const string Finte = "Finte";
        public const string Kampfreflexe = "Kampfreflexe";
        public const string Kampfgespür = "Kampfgespür";
        public const string Meisterparade = "Meisterparade";
        public const string Klingentänzer = "Klingentänzer";
        public const string Klingensturm = "Klingensturm";
        public const string Klingenwand = "Klingenwand";
        public const string GefäßDerSterne = "Gefäß der Sterne";
        public const string Runenkunde = "Runenkunde";

        // Ritualkenntnis (Schamanen)
        public const string RitualkenntnisSchamaneAchaz = "Ritualkenntnis (Achaz-Schamane)";
        public const string RitualkenntnisSchamaneFerkina = "Ritualkenntnis (Ferkina-Schamane)";
        public const string RitualkenntnisSchamaneGjalsker = "Ritualkenntnis (Gjalsker-Schamane)";
        public const string RitualkenntnisSchamaneGoblin = "Ritualkenntnis (Goblin-Schamanin)";
        public const string RitualkenntnisSchamaneNivesen = "Ritualkenntnis (Nivesen-Schamane)";
        public const string RitualkenntnisSchamaneOrk = "Ritualkenntnis (Ork-Schamane)";
        public const string RitualkenntnisSchamaneTrollzacker = "Ritualkenntnis (Trollzacker-Schamane)";
        public const string RitualkenntnisSchamaneWaldmenschen = "Ritualkenntnis (Waldmenschen-Schamane)";

        // Spätweihe
        public const string SpätweiheAlveranischeGottheit = "Spätweihe Alveranische Gottheit";
        public const string SpätweiheNichtAlveranischeGottheit = "Spätweihe Nichtalveranische Gottheit";
        public const string SpätweiheNamenloser = "Spätweihe Namenloser";
        public const string SpätweiheDunkleZeiten = "Spätweihe Dunkle Zeiten";


        public static int GetSonderfertigkeitId(string sonderfertigkeit)
        {
            var sonderfertigkeitRows = App.DatenDataSet.Sonderfertigkeit.Select(string.Format("Name = '{0}'", sonderfertigkeit.Replace("'", "''")));
            if (sonderfertigkeitRows.Length == 1)
                return Convert.ToInt32(sonderfertigkeitRows[0]["SonderfertigkeitID"]);
            return -1;
        }
    }
}
