using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.Model
{
    public partial class Inventar : MeisterGeister.Logic.Literatur.ILiteratur
    {
        public Inventar()
        {
            InventarGUID = Guid.NewGuid();
        }

        public bool Usergenerated
        {
            get { return !InventarGUID.ToString().StartsWith("00000000-0000-0000-00"); }
        }
    }
}
