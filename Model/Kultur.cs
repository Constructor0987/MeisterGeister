using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.Model
{
    public partial class Kultur : MeisterGeister.Logic.Literatur.ILiteratur
    {
        public Kultur()
        {
            KulturGUID = Guid.NewGuid();
        }

        public bool Usergenerated
        {
            get { return !KulturGUID.ToString().StartsWith("00000000-0000-0000-00"); }
        }
    }
}
