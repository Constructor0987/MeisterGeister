using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// Eigene Usings
using BasarLogic = MeisterGeister.ViewModel.Basar.Logic;

namespace MeisterGeister.Model
{
    public partial class Handelsgut : BasarLogic.IHandelsgut, MeisterGeister.Logic.Literatur.ILiteratur
    {
        public bool Usergenerated
        {
            get { return !HandelsgutGUID.ToString().StartsWith("00000000-0000-0000-00"); }
        }

        public Setting Setting
        {
            get
            {
                var a_s = Handelsgut_Setting.Where(s => s.SettingGUID == Setting.AktuellesSettingGUID).FirstOrDefault();
                if (a_s == null)
                    a_s = Handelsgut_Setting.FirstOrDefault();
                if (a_s == null)
                    return null;
                return a_s.Setting;
            }
        }

        public string Preis
        {
            get
            {
                var a_s = Handelsgut_Setting.Where(s => s.SettingGUID == Setting.AktuellesSettingGUID).FirstOrDefault();
                if (a_s == null)
                    a_s = Handelsgut_Setting.FirstOrDefault();
                if (a_s == null)
                    return null;
                return a_s.Preis;
            }
            set
            {
                var a_s = Handelsgut_Setting.Where(s => s.SettingGUID == Setting.AktuellesSettingGUID).FirstOrDefault();
                if (a_s == null)
                    a_s = Handelsgut_Setting.FirstOrDefault();
                if (a_s == null)
                    return;
                a_s.Preis = value;
                OnChanged("Preis");
            }
        }
    }
}
