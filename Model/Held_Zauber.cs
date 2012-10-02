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

        override public int[] Werte
        {
            get
            {
                if (_werte == null)
                    _werte = new int[3];
                if (Held != null && Zauber != null)
                {
                    _werte[0] = Held.GetEigenschaftWert(Zauber.Eigenschaft1);
                    _werte[1] = Held.GetEigenschaftWert(Zauber.Eigenschaft2);
                    _werte[2] = Held.GetEigenschaftWert(Zauber.Eigenschaft3);
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
