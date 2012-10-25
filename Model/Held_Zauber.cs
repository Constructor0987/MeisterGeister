using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeisterGeister.Model.Extensions;
using MeisterGeister.ViewModel.Helden.Logic;

namespace MeisterGeister.Model
{
    public partial class Held_Zauber : MeisterGeister.Logic.General.Probe, IHeld
    {
        #region //---- PROBE ----

        [DependentProperty("Zauber")]
        override public string Probenname
        {
            get { return Zauber != null ? string.Format("{0} [{1}]", Zauber.Name, Repräsentation) : string.Empty; }
            set
            {
                if (Zauber != null)
                    Zauber.Name = value;
            }

        }

        override public int[] Werte
        {
            get
            {
                if (_werte == null || _werte.Length != 3)
                    _werte = new int[3];
                if (Held != null && Zauber != null)
                {
                    _werte[0] = Held.EigenschaftWert(Zauber.Eigenschaft1);
                    _werte[1] = Held.EigenschaftWert(Zauber.Eigenschaft2);
                    _werte[2] = Held.EigenschaftWert(Zauber.Eigenschaft3);
                }
                return _werte;
            }
            set
            {
                _werte = value;
                _chanceBerechnet = false;
                OnChanged("Werte");
            }
        }

        [DependentProperty("ZfW")]
        public override int Fertigkeitswert
        {
            get
            {
                return ZfW ?? 0;
            }
            set
            {
                ZfW = value;
                _chanceBerechnet = false;
                OnChanged("Fertigkeitswert");
            }
        }

        override public string WerteNamen
        {
            get
            {
                if (Zauber != null)
                    return string.Format("({0}/{1}/{2})", Zauber.Eigenschaft1, Zauber.Eigenschaft2, Zauber.Eigenschaft3);
                return string.Empty;
            }
        }

        #endregion //---- PROBE ----
        
    }
}
