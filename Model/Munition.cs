using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.Model
{
    public partial class Munition
    {
        public Munition()
        {
            MunitionGUID = Guid.NewGuid();
        }

        public bool Usergenerated
        {
            get { return !MunitionGUID.ToString().StartsWith("00000000-0000-0000-00"); }
        }
    }
}
