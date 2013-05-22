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
        private static List<Model.Regeln> _regelnListe = null;

        static Regeln()
        {
            if (Global.IsInitialized && Global.ContextRegeln != null)
                _regelnListe = Global.ContextRegeln.RegelnListe;
            else
                _regelnListe = new List<Model.Regeln>();
        }

        private static bool GetRegelValue(string name, bool defaultValue = true)
        {
            if (Global.IsInitialized && (_regelnListe == null || _regelnListe.Count == 0))
                _regelnListe = Global.ContextRegeln.RegelnListe;
            var t = _regelnListe.Where(n => n.Name == name).FirstOrDefault();
            return (t == null || t.Anwenden == null)? defaultValue : t.Anwenden.Value;
        }

        private static void SetRegelValue(string name, bool value)
        {
            if (Global.IsInitialized && (_regelnListe == null || _regelnListe.Count == 0))
                _regelnListe = Global.ContextRegeln.RegelnListe;
            var t = _regelnListe.Where(n => n.Name == name).FirstOrDefault();
            if (t == null)
                return;
            t.Anwenden = value;
            Global.ContextRegeln.Update<Model.Regeln>(t);
        }

        public static bool EigenschaftenProbePatzerGlück
        {
            get
            {
                return GetRegelValue("EigenschaftenProbePatzerGlück");
            }
            set
            {
                SetRegelValue("EigenschaftenProbePatzerGlück", value);
            }
        }

        public static bool TPKK
        {
            get
            {
                return GetRegelValue("TPKK");
            }
            set
            {
                SetRegelValue("TPKK", value);
            }
        }

        public static bool NiedrigeLE
        {
            get
            {
                return GetRegelValue("NiedrigeLE");
            }
            set
            {
                SetRegelValue("NiedrigeLE", value);
            }
        }

        public static bool NiedrigeAU
        {
            get
            {
                return GetRegelValue("NiedrigeAU");
            }
            set
            {
                SetRegelValue("NiedrigeAU", value);
            }
        }

        public static bool AusdauerImKampf
        {
            get
            {
                return GetRegelValue("AusdauerImKampf");
            }
            set
            {
                SetRegelValue("AusdauerImKampf", value);
            }
        }

        public static bool NurDreiZonenWunden
        {
            get
            {
                return GetRegelValue("NurDreiZonenWunden");
            }
            set
            {
                SetRegelValue("NurDreiZonenWunden", value);
            }
        }
    }
}
