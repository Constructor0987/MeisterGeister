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

        public bool BehinderungSteigtDurchSchwereTreffer()
        {
            String thisGuid = RüstungGUID.ToString();
            return thisGuid == "00000000-0000-0000-0004-000000000014" //Bronzeharnisch
                || thisGuid == "00000000-0000-0000-0004-000000000020" //Eisenmantel
                || thisGuid == "00000000-0000-0000-0004-000000000022" //Fünflagenharnisch
                || thisGuid == "00000000-0000-0000-0004-000000000024" //Garether Platte
                || thisGuid == "00000000-0000-0000-0004-000000000026" //Gladiatorenschulter
                || thisGuid == "00000000-0000-0000-0004-000000000028" //Horasischer Reiterharnisch
                || thisGuid == "00000000-0000-0000-0004-000000000025" //Gestechrüstung
                || thisGuid == "00000000-0000-0000-0004-000000000041" //Kürass
                || thisGuid == "00000000-0000-0000-0004-000000000042" //Kusliker Lamellar
                || thisGuid == "00000000-0000-0000-0004-000000000043" //Lederharnisch
                || thisGuid == "00000000-0000-0000-0004-000000000049" //Leichte Platte
                || thisGuid == "00000000-0000-0000-0004-000000000051" //Mammutonpanzer
                || thisGuid == "00000000-0000-0000-0004-000000000052" //Maraskanischer Hartholzharnisch
                || thisGuid == "00000000-0000-0000-0004-000000000064" //Schup­penpanzer, lang
                || thisGuid == "00000000-0000-0000-0004-000000000065" //Schup­penpanzer, lang
                || thisGuid == "00000000-0000-0000-0004-000000000046" //Lederzeug
                || thisGuid == "00000000-0000-0000-0004-000000000004" //Armschinen, Leder
                || thisGuid == "00000000-0000-0000-0004-000000000009" //Beinschienen, Leder
                || thisGuid == "00000000-0000-0000-0004-000000000003" //Armschienen, Bronze
                || thisGuid == "00000000-0000-0000-0004-000000000005" //Armschienen, Stahl
                || thisGuid == "00000000-0000-0000-0004-000000000008" //Beinschienen, Bronze
                || thisGuid == "00000000-0000-0000-0004-000000000010" //Beinschienen, Stahl
                || thisGuid == "00000000-0000-0000-0004-000000000060" //Plattenzeug
                || thisGuid == "00000000-0000-0000-0004-000000000055" //Panzerbein
                || thisGuid == "00000000-0000-0000-0004-000000000056" //Panzerhandschuhe, Paar
                || thisGuid == "00000000-0000-0000-0004-000000000057" //Panzerschuh
                || thisGuid == "00000000-0000-0000-0004-000000000058" //Plattenarme
                || thisGuid == "00000000-0000-0000-0004-000000000059"; //Plattenschultern
        }

        public bool MeisterlicheRüstung()
        {
            String thisGuid = RüstungGUID.ToString();
            return thisGuid == "00000000-0000-0000-0004-000000000144" //meisterliche Kettenbeinlinge, Paar
                || thisGuid == "00000000-0000-0000-0004-000000000145" //meisterliche Kettenhandschuhe, Paar
                || thisGuid == "00000000-0000-0000-0004-000000000146" //meisterliche Kettenhaube
                || thisGuid == "00000000-0000-0000-0004-000000000147" //meisterliches Kettenzeug
                || thisGuid == "00000000-0000-0000-0004-000000000148" //meisterliche Kettenhaube, mit Gesichtsschutz
                || thisGuid == "00000000-0000-0000-0004-000000000149" //meisterliches Kettenhemd, 1/2 Arm
                || thisGuid == "00000000-0000-0000-0004-000000000150" //meisterliches Kettenhemd, lang
                || thisGuid == "00000000-0000-0000-0004-000000000151" //meisterlicher Kettenmantel
                || thisGuid == "00000000-0000-0000-0004-000000000152" //meisterliche Kettenweste
                || thisGuid == "00000000-0000-0000-0004-000000000153" //meisterlicher Ringelpanzer
                || thisGuid == "00000000-0000-0000-0004-000000000154" //meisterlicher Spiegelpanzer
                || thisGuid == "00000000-0000-0000-0004-000000000156" //meisterlicher Kettenkragen
                || thisGuid == "00000000-0000-0000-0004-000000000157"; //meisterliche Löwenmähne
        }

        /// <summary>
        /// gibt zurück, ob für jede Zone ein Rüstungswert gesetzt wurde
        /// (gRS, RS, gBE und BE werden nicht geprüft)
        /// </summary>
        /// <returns>true, falls für jede Zone ein Rüstungswert gesetzt wurde</returns>
        public bool ZonenrüstungswerteVorhanden()
        {
            return (Kopf.HasValue && Brust.HasValue && Bauch.HasValue && Rücken.HasValue && RBein.HasValue && LBein.HasValue && LArm.HasValue && RArm.HasValue);
        }

        /// <summary>
        /// Berechnet den gRS im Zonensystem
        /// </summary>
        /// <param name="gRSDivisor">optional, 20 falls nicht angegeben (WdS110)</param>
        /// <returns>gRS oder null</returns>
        public Nullable<double> BerechneGesamtRS(int gRSDivisor = 20)
        {
            if (ZonenrüstungswerteVorhanden())
            {
                return (2 * (Kopf.Value + LBein.Value + RBein.Value) + 4 * (Bauch.Value + Brust.Value + Rücken.Value) + (LArm.Value + RArm.Value)) / gRSDivisor;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Berechnet die gBE im Zonensystem
        /// </summary>
        /// <param name="gRSDivisor">optional, 20 falls nicht angegeben (WdS110)</param>
        /// <returns>gBE oder null</returns>
        public Nullable<double> BerechneGesamtBE(int gRSDivisor = 20)
        {
            Nullable<double> gesamtBE = BerechneGesamtRS(gRSDivisor);
            if (gesamtBE.HasValue && this.Verarbeitung.HasValue)
            {
                if (this.Art == "Z")
                {
                    gesamtBE /= (Math.Pow(2, this.Verarbeitung.Value));
                }
                else
                {
                    gesamtBE = Math.Max(0.0, gesamtBE.Value - this.Verarbeitung.Value);
                }
            }
            return gesamtBE;
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
