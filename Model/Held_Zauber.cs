using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.Model
{
    public partial class Held_Zauber : MeisterGeister.Logic.General.Probe
    {
        public override int Fertigkeitswert
        {
            get
            {
                return ZfW ?? 0;
            }
            set
            {
                ZfW = value;
            }
        }
    }
}
