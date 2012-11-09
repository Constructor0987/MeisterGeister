using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeisterGeister.Model.Extensions;

namespace MeisterGeister.Model
{
    public partial class Zauber : MeisterGeister.Logic.General.Probe
    {
        #region //---- PROBE ----

        [DependentProperty("Name")]
        override public string Probenname
        {
            get { return Name; }
            set { Name = value; }

        }

        override public int[] Werte
        {
            get
            {
                if (_werte == null || _werte.Length != 3)
                    _werte = new int[3];
                return _werte;
            }
            set
            {
                _werte = value;
                //_chanceBerechnet = false;
            }
        }

        override public string WerteNamen
        {
            get
            {
                return string.Format("({0}/{1}/{2})", Eigenschaft1, Eigenschaft2, Eigenschaft3);
            }
        }

        #endregion //---- PROBE ----

        public bool Usergenerated
        {
            get { return !ZauberGUID.ToString().StartsWith("00000000-0000-0000-00"); }
        }
    }
}
