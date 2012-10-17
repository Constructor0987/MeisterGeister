using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeisterGeister.Model.Extensions;

namespace MeisterGeister.Model
{
    public partial class Held_Zauber : MeisterGeister.Logic.General.Probe
    {
        #region //---- PROBE ----

        [DependentProperty("Zauber")]
        override public string Probenname
        {
            get { return Zauber.Name; }
        }

        override public int[] Werte
        {
            get
            {
                if (_werte == null)
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

        #endregion //---- PROBE ----
        
    }
}
