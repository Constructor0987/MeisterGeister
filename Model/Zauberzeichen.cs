using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeisterGeister.Model.Extensions;

namespace MeisterGeister.Model
{
    public partial class Zauberzeichen
    {
        public bool Usergenerated
        {
            get { return !ZauberzeichenGUID.ToString().StartsWith("00000000-0000-0000-00"); }
        }

        public Setting Setting
        {
            get
            {
                var a_s = Zauberzeichen_Setting.Where(s => s.SettingGUID == Setting.AktuellesSettingGUID).FirstOrDefault();
                if (a_s == null)
                    a_s = Zauberzeichen_Setting.FirstOrDefault();
                if (a_s == null)
                    return null;
                return a_s.Setting;
            }
        }

        public string Verbreitung
        {
            get
            {
                var a_s = Zauberzeichen_Setting.Where(s => s.SettingGUID == Setting.AktuellesSettingGUID).FirstOrDefault();
                if (a_s == null)
                    a_s = Zauberzeichen_Setting.FirstOrDefault();
                if (a_s == null)
                    return null;
                return a_s.Verbreitung;
            }
            set
            {
                var a_s = Zauberzeichen_Setting.Where(s => s.SettingGUID == Setting.AktuellesSettingGUID).FirstOrDefault();
                if (a_s == null)
                    a_s = Zauberzeichen_Setting.FirstOrDefault();
                if (a_s == null)
                    return;
                a_s.Verbreitung = value;
                OnChanged("Verbreitung");
            }
        }
    }
}
