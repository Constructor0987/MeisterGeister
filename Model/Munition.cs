using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.Model
{
    public partial class Munition
    {
        public Munition()
        {
            MunitionGUID = Guid.NewGuid();
        }

        public bool Usergenerated
        {
            get { return !MunitionGUID.ToString().StartsWith("00000000-0000-0000-00"); }
        }

        /// <summary>
        /// Gibt zurück, ob Munition im aktuellen Setting als gehärtet vorkommt
        /// </summary>
        /// <returns>true oder false</returns>
        public bool HärtbarNachSetting
        {
            // Nur im Myranor-Setting gibt es diverse gehärtete Pfeile / Bolzen
            // in allen anderen Settings nur Jagd/Kriegs-Bolzen/Pfeile
            get {
                if (Model.Setting.SettingAktiv(Model.Setting.MYRANOR_GUID))
                {
                    return this.Härtbar;
                }else{
                    switch (this.MunitionGUID.ToString())
                    {
                        case "00000000-0000-0000-000f-000000000003":  // Jagdpfeil
                        case "00000000-0000-0000-000f-000000000004":  // Kriegspfeil
                        case "00000000-0000-0000-000f-000000000011":  // Jagdbolzen
                        case "00000000-0000-0000-000f-000000000012":  // Kriegsbolzen
                            return true;
                        default: return false; 
                    }
                }
            }
        }
    }
}
