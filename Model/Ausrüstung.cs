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
                //TODO ??: hier fehlt ein Event-Handler, welches bei einer Änderung der Talente _talente wieder auf null setzt.
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

        public Setting Setting
        {
            get
            {
                var a_s = Ausrüstung_Setting.Where(s => s.SettingGUID == Setting.AktuellesSettingGUID).FirstOrDefault();
                if (a_s == null)
                    a_s = Ausrüstung_Setting.FirstOrDefault();
                if (a_s == null)
                    return null;
                return a_s.Setting;
            }
        }

        public double Preis
        {
            get {
                var a_s = Ausrüstung_Setting.Where(s => s.SettingGUID == Setting.AktuellesSettingGUID).FirstOrDefault();
                if (a_s == null)
                    a_s = Ausrüstung_Setting.FirstOrDefault();
                if (a_s == null)
                    return 0;
                return a_s.Preis;
            }
            set
            {
                var a_s = Ausrüstung_Setting.Where(s => s.SettingGUID == Setting.AktuellesSettingGUID).FirstOrDefault();
                if (a_s == null)
                    a_s = Ausrüstung_Setting.FirstOrDefault();
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
                    a_s = Ausrüstung_Setting.FirstOrDefault();
                if (a_s == null)
                    return null;
                return a_s.Verbreitung;
            }
            set
            {
                var a_s = Ausrüstung_Setting.Where(s => s.SettingGUID == Setting.AktuellesSettingGUID).FirstOrDefault();
                if (a_s == null)
                    a_s = Ausrüstung_Setting.FirstOrDefault();
                if (a_s == null)
                    return;
                a_s.Verbreitung = value;
                OnChanged("Verbreitung");
            }
        }

        /// <summary>
        /// Erstellt einen Klon der Ausrüstung mit einer neuen Guid.
        /// Waffe, Fernkampfwaffe, Schild und Rüstung werden mit kopiert.
        /// </summary>
        /// <returns></returns>
        public Ausrüstung Clone()
        {
            Ausrüstung a = Global.ContextInventar.Clone<Ausrüstung>(this);
            Guid guid = Guid.NewGuid();
            a.AusrüstungGUID = guid;
            Waffe w = null;
            Fernkampfwaffe f = null;
            Schild s = null;
            Rüstung r = null;
            if(Waffe != null)
                w = Global.ContextInventar.Clone<Waffe>(Waffe);
            if (Fernkampfwaffe != null)
                f = Global.ContextInventar.Clone<Fernkampfwaffe>(Fernkampfwaffe);
            if (Schild != null)
                s = Global.ContextInventar.Clone<Schild>(Schild);
            if (Rüstung != null)
                r = Global.ContextInventar.Clone<Rüstung>(Rüstung);
            
            a.Waffe = w;
            a.Fernkampfwaffe = f;
            a.Schild = s;
            a.Rüstung = r;
            return a;
        }
    }
}
