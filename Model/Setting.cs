using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.Model
{
    public partial class Setting
    {
        private static Guid aktuellesSettingGUID = Guid.Parse("00000000-0000-0000-5e77-000000000001"); //Aventurien
        public static Guid AktuellesSettingGUID
        {
            get { return aktuellesSettingGUID; }
            set { aktuellesSettingGUID = value; }
        }

        public override string ToString()
        {
            return Name;
        }

        public static List<Setting> AktiveSettings
        {
            get { return Global.ContextHeld.Liste<Setting>().Where(s => s.Aktiv == true).ToList(); }
        }
    }
}
