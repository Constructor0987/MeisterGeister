using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// Eigene Usings
using BasarLogic = MeisterGeister.ViewModel.Basar.Logic;
using InventarLogic = MeisterGeister.ViewModel.Inventar.Logic;

namespace MeisterGeister.Model
{
    public partial class Schild : BasarLogic.IHandelsgut, InventarLogic.IAusrüstung
    {
        public Schild()
        {
            Ausrüstung = new Ausrüstung();
            Ausrüstung.AusrüstungGUID = SchildGUID = Guid.NewGuid();
        }

        public bool Usergenerated
        {
            get { return !SchildGUID.ToString().StartsWith("00000000-0000-0000-000"); }
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
            get
            {
                switch (Typ)
                {
                    case "S": return "Schilde";
                    case "P": return "Parierwaffen";
                    default: return "Schilde & Parierwaffen";
                }
            }
        }

        string BasarLogic.IHandelsgut.Tags
        {
            get { return Größe; }
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
                return String.Format("");
            }
        }

        #endregion

    }
}
