using System.Diagnostics;
using System.Linq;
using MeisterGeister.Logic.Voraussetzungen;

namespace MeisterGeister.Model
{
    public partial class Sonderfertigkeit : MeisterGeister.Logic.Literatur.ILiteratur
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

        public const string Talentspezialisierung = "Talentspezialisierung";
        public const string AusweichenIII = "Ausweichen III";
        public const string AusweichenII = "Ausweichen II";
        public const string AusweichenI = "Ausweichen I";
        public const string RüstungsgewöhnungIII = "Rüstungsgewöhnung III";
        public const string RüstungsgewöhnungII = "Rüstungsgewöhnung II";
        public const string RüstungsgewöhnungI = "Rüstungsgewöhnung I";

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

        public const string SpätweiheDunkleZeitenI = "Spätweihe Dunkle Zeiten I";

        public const string SpätweiheDunkleZeitenII = "Spätweihe Dunkle Zeiten II";

        public const string SpätweiheDunkleZeitenIII = "Spätweihe Dunkle Zeiten III";

        public const string SpätweiheXoArtal = "Spätweihe (Xo'Artal-Pantheon)";

        public const string KontaktZumGroßenGeist = "Kontakt zum Großen Geist";

        public bool Usergenerated
        {
            get { return !SonderfertigkeitGUID.ToString().StartsWith("00000000-0000-0000-00"); }
        }

        public Setting Setting
        {
            get
            {
                Sonderfertigkeit_Setting a_s = Sonderfertigkeit_Setting.Where(s => s.SettingGUID == Setting.AktuellesSettingGUID).FirstOrDefault();
                if (a_s == null)
                {
                    a_s = Sonderfertigkeit_Setting.FirstOrDefault();
                }

                if (a_s == null)
                {
                    return null;
                }

                return a_s.Setting;
            }
        }

        public string Verbreitung
        {
            get
            {
                Sonderfertigkeit_Setting a_s = Sonderfertigkeit_Setting.Where(s => s.SettingGUID == Setting.AktuellesSettingGUID).FirstOrDefault();
                if (a_s == null)
                {
                    a_s = Sonderfertigkeit_Setting.FirstOrDefault();
                }

                if (a_s == null)
                {
                    return null;
                }

                return a_s.Verbreitung;
            }

            set
            {
                Sonderfertigkeit_Setting a_s = Sonderfertigkeit_Setting.Where(s => s.SettingGUID == Setting.AktuellesSettingGUID).FirstOrDefault();
                if (a_s == null)
                {
                    a_s = Sonderfertigkeit_Setting.FirstOrDefault();
                }

                if (a_s == null)
                {
                    return;
                }

                a_s.Verbreitung = value;
                OnChanged("Verbreitung");
            }
        }

        public bool CheckVoraussetzungen(Held held)
        {
            // TODO ??: Prüfung der Sonderfertigkeiten wird derzeit abgebrochen, da es zu langsam ist
            return true;
#pragma warning disable 0162
            //Parsen und verifizieren der Vorraussetzung-Property
            if (Voraussetzungen != null)
            {
                var scanner = new Scanner();
                var parser = new Parser(scanner);
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
#pragma warning restore 0162
        }
    }
}
