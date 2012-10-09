using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// Eigene Usings
using BasarLogic = MeisterGeister.ViewModel.Basar.Logic;
using InventarLogic = MeisterGeister.ViewModel.Inventar.Logic;

namespace MeisterGeister.Model
{
    public partial class Rüstung : BasarLogic.IHandelsgut, InventarLogic.IAusrüstung
    {
        public Rüstung()
        {
            Ausrüstung = new Ausrüstung();
            Ausrüstung.AusrüstungGUID = RüstungGUID = Guid.NewGuid();
        }

        public bool Usergenerated
        {
            get { return !RüstungGUID.ToString().StartsWith("00000000-0000-0000-00"); }
        }

        #region //---- IAusrüstung ----
        public string Name
        {
            get
            {
                return Ausrüstung.Name;
            }
            set
            {
                Ausrüstung.Name = value;
            }
        }

        public double Preis
        {
            get
            {
                return Ausrüstung.Preis;
            }
            set
            {
                Ausrüstung.Preis = value;
            }
        }

        public int Gewicht
        {
            get
            {
                return Ausrüstung.Gewicht;
            }
            set
            {
                Ausrüstung.Gewicht = value;
            }
        }

        public string Verbreitung
        {
            get
            {
                return Ausrüstung.Verbreitung;
            }
            set
            {
                Ausrüstung.Verbreitung = value;
            }
        }

        public string Literatur
        {
            get
            {
                return Ausrüstung.Literatur;
            }
            set
            {
                Ausrüstung.Literatur = value;
            }
        }

        public string Setting
        {
            get
            {
                return Ausrüstung.Setting;
            }
            set
            {
                Ausrüstung.Setting = value;
            }
        }

        public string Bemerkung
        {
            get
            {
                return Ausrüstung.Bemerkung;
            }
            set
            {
                Ausrüstung.Bemerkung = value;
            }
        }
        #endregion

        #region //---- IHandelsgut ----

        public string Kategorie
        {
            get { return "Rüstungen"; }
        }

        string BasarLogic.IHandelsgut.Tags
        {
            get { return Gruppe; }
        }

        double? BasarLogic.IHandelsgut.Gewicht
        {
            get { return Gewicht; }
        }

        string BasarLogic.IHandelsgut.ME
        {
            get { return string.Empty; }
        }

        string BasarLogic.IHandelsgut.Preis
        {
            get { return Preis.ToString(); }
        }

        string BasarLogic.IHandelsgut.Bemerkung
        {
            get
            {
                return string.Format("Ko {0} Br {1} Rü {2} Ba {3} LA {4} RA {5} LB {6} RB {7} gRS/RS {8}/{9} gBE/BE {10}/{11}",
                    Kopf, Brust, Rücken, Bauch, LArm, RArm, LBein, RBein, gRS, RS, gBE, BE)
                    + (string.IsNullOrEmpty(Bemerkung) ? string.Empty : Environment.NewLine + Bemerkung);
            }
        }

        #endregion
    }
}
