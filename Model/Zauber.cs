using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.Model
{
    public partial class Zauber : MeisterGeister.Logic.General.Probe
    {
        #region //---- PROBE ----

        override public int[] Werte
        {
            get
            {
                if (_werte == null)
                    _werte = new int[3];
                return _werte;
            }
            set
            {
                _werte = value;
                _chanceBerechnet = false;
            }
        }

        #endregion //---- PROBE ----

        private const int maxid = 343;
        public bool Usergenerated
        {
            get { return ZauberID > maxid; }
        }
    }
}
