using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// Eigene Usings
using Model = MeisterGeister.Model;

namespace MeisterGeister.Logic.Einstellung
{
    static class Regeln
    {
        static Regeln()
        {
            _eigenschaftenProbePatzerGlück = GetRegelValue("EigenschaftenProbePatzerGlück");
        }

        private static bool GetRegelValue(string name, bool defaultValue = true)
        {
            if (Global.IsInitialized)
            {
                Model.Einstellung e = Global.ContextHeld.LoadEinstellungByName(name);
                if (e == null)
                    return defaultValue;
                return e.Get<Boolean>();
            }
            return defaultValue;
        }

        private static void SetRegelValue(string name, bool value)
        {
            if (Global.IsInitialized)
            {
                Model.Einstellung e = Global.ContextHeld.LoadEinstellungByName(name);
                if (e == null)
                    return;
                e.Set<Boolean>(value);
                Global.ContextHeld.Update<Model.Einstellung>(e);
            }
        }

        // Nach einer Änderung über das Einstellungen-Fenster muss MG erst neu gestartet werden, damit diee Änderung wirkt.
        private static bool _eigenschaftenProbePatzerGlück = false;
        public static bool EigenschaftenProbePatzerGlück
        {
            get
            {
                return _eigenschaftenProbePatzerGlück;
            }
            set
            {
                _eigenschaftenProbePatzerGlück = value;
                SetRegelValue("EigenschaftenProbePatzerGlück", value);
            }
        }

        // TODO: Einstellung wird gecached, um Absturz bei Poben zu verhindern. Da sich dadurch die Einstellung nach Änderung ggf. nicht mehr aktuell sein könnte, sollte das Caching noch überarbeitet werden.
        private static Nullable<bool> _cached_tpKK = null;
        public static bool TPKK
        {
            get
            {
                if (_cached_tpKK == null)
                    _cached_tpKK = GetRegelValue("TPKK");
                return _cached_tpKK.GetValueOrDefault();
            }
            set
            {
                SetRegelValue("TPKK", value);
                _cached_tpKK = value;
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
    }
}
