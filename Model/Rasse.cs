using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.Model
{
    public partial class Rasse : MeisterGeister.Logic.Literatur.ILiteratur
    {
        public Rasse()
        {
            RasseGUID = Guid.NewGuid();
        }

        public bool Usergenerated
        {
            get { return !RasseGUID.ToString().StartsWith("00000000-0000-0000-00"); }
        }
    }
}
