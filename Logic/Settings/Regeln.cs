using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// Eigene Usings
using Model = MeisterGeister.Model;

namespace MeisterGeister.Logic.Settings
{
    static class Regeln
    {
        //Entitylisten
        private static List<Model.Regeln> _regelnListe = Global.ContextRegeln.RegelnListe;

        public static bool SettingDunkleZeiten
        {
            get
            {
                var t = _regelnListe.Where(n => n.Name == "DunkleZeiten").FirstOrDefault();
                return t.Anwenden.Value;
            }
        }

        public static bool EigenschaftenProbePatzerGlück
        {
            get
            {
                var t = _regelnListe.Where(n => n.Name == "EigenschaftenProbePatzerGlück").FirstOrDefault();
                return t.Anwenden.Value;
            }
        }

        public static bool TPKK
        {
            get
            {
                var t = _regelnListe.Where(n => n.Name == "TPKK").FirstOrDefault();
                return t.Anwenden.Value;
            }
        }

        public static bool NiedrigeLE
        {
            get
            {
                var t = _regelnListe.Where(n => n.Name == "NiedrigeLE").FirstOrDefault();
                return t.Anwenden.Value;
            }
        }

        public static bool NiedrigeAU
        {
            get
            {
                var t = _regelnListe.Where(n => n.Name == "NiedrigeAU").FirstOrDefault();
                return t.Anwenden.Value;
            }
        }
    }
}
