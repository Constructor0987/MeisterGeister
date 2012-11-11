using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.Model
{
    public partial class Ausrüstung
    {
        public Ausrüstung()
        {
            AusrüstungGUID = Guid.NewGuid();
        }

        public bool Usergenerated
        {
            get { return !AusrüstungGUID.ToString().StartsWith("00000000-0000-0000-00"); }
        }

        private List<Talent> _talente = null;
        public List<Talent> Talente
        {
            get
            {
                if (_talente == null)
                {
                    _talente = new List<Talent>();
                    if (Waffe != null)
                        _talente.AddRange(Waffe.Talent);
                    if (Fernkampfwaffe != null)
                        _talente.AddRange(Fernkampfwaffe.Talent);
                }
                return _talente;
            }
        }

        public double Preis
        {
            get {
                var a_s = Ausrüstung_Setting.Where(s => s.SettingGUID == Setting.AktuellesSettingGUID).FirstOrDefault();
                if (a_s == null)
                    return 0;
                return a_s.Preis;
            }
            set
            {
                var a_s = Ausrüstung_Setting.Where(s => s.SettingGUID == Setting.AktuellesSettingGUID).FirstOrDefault();
                if (a_s == null)
                    return;
                a_s.Preis = value;
                OnChanged("Preis");
            }
        }

        public string Verbreitung
        {
            get
            {
                var a_s = Ausrüstung_Setting.Where(s => s.SettingGUID == Setting.AktuellesSettingGUID).FirstOrDefault();
                if (a_s == null)
                    return null;
                return a_s.Verbreitung;
            }
            set
            {
                var a_s = Ausrüstung_Setting.Where(s => s.SettingGUID == Setting.AktuellesSettingGUID).FirstOrDefault();
                if (a_s == null)
                    return;
                a_s.Verbreitung = value;
                OnChanged("Verbreitung");
            }
        }
    }
}
