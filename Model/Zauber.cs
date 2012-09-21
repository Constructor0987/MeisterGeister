using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.Model
{
    public partial class Zauber : MeisterGeister.Logic.General.Probe
    {
        private const int maxid = 343;
        public bool Usergenerated
        {
            get { return ZauberID > maxid; }
        }
    }
}
