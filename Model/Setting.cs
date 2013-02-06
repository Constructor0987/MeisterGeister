using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeisterGeister.Model.Extensions;

namespace MeisterGeister.Model
{
    public partial class Setting
    {
        public Setting()
        {
            PropertyChanged += Setting_PropertyChanged;
        }

        void Setting_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Setting.aktiveSettings = null;
        }

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

        private static List<Setting> aktiveSettings = null;
        public static List<Setting> AktiveSettings
        {
            get { 
                if(Setting.aktiveSettings == null)
                    Setting.aktiveSettings = Global.ContextHeld.Liste<Setting>().Where(s => s.Aktiv == true).ToList();
                return Setting.aktiveSettings; 
            }
            set { Setting.aktiveSettings = value; }
        }

        public static string AktiveSettingsToString()
        {
            string settings = string.Empty;

            foreach (var setting in AktiveSettings)
            {
                if (settings != string.Empty)
                    settings += ", ";
                settings += setting.Name;
            }

            return settings;
        }
    }
}
