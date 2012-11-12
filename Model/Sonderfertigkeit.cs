using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MeisterGeister.Logic.Voraussetzungen;
using System.Diagnostics;

namespace MeisterGeister.Model
{
    public partial class Sonderfertigkeit
    {
        public bool Usergenerated
        {
            get { return !SonderfertigkeitGUID.ToString().StartsWith("00000000-0000-0000-00"); }
        }

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
        public const string KontaktZumGroßenGeist = "Kontakt zum Großen Geist";

        public bool CheckVoraussetzungen(Held held)
        {
            //Parsen und verifizieren der Vorraussetzung-Property
            if (Voraussetzungen != null)
            {
                Scanner scanner = new Scanner();
                Parser parser = new Parser(scanner);
                ParseTree tree = parser.Parse(Voraussetzungen);
                if (tree.Errors.Count > 0)
                {
                    Debug.WriteLine("Fehler beim parsen der Voraussetzungen ({0}) der Sonderfertigkeit {1}", Voraussetzungen, Name);
                    //Einfach mal true zurückgeben, damit keine Funktionalität eingeschränkt ist.
                    return true;
                }
                return (bool)tree.Eval(held);
            }
            return true;
        }

        public Setting Setting
        {
            get
            {
                var a_s = Sonderfertigkeit_Setting.Where(s => s.SettingGUID == Setting.AktuellesSettingGUID).FirstOrDefault();
                if (a_s == null)
                    a_s = Sonderfertigkeit_Setting.FirstOrDefault();
                if (a_s == null)
                    return null;
                return a_s.Setting;
            }
        }

        public string Verbreitung
        {
            get
            {
                var a_s = Sonderfertigkeit_Setting.Where(s => s.SettingGUID == Setting.AktuellesSettingGUID).FirstOrDefault();
                if (a_s == null)
                    a_s = Sonderfertigkeit_Setting.FirstOrDefault();
                if (a_s == null)
                    return null;
                return a_s.Verbreitung;
            }
            set
            {
                var a_s = Sonderfertigkeit_Setting.Where(s => s.SettingGUID == Setting.AktuellesSettingGUID).FirstOrDefault();
                if (a_s == null)
                    a_s = Sonderfertigkeit_Setting.FirstOrDefault();
                if (a_s == null)
                    return;
                a_s.Verbreitung = value;
                OnChanged("Verbreitung");
            }
        }
    }
}