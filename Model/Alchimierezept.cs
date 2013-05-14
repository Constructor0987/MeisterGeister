using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.Model
{
    public partial class Alchimierezept : MeisterGeister.Logic.Literatur.ILiteratur
    {
        public Alchimierezept()
        {
            AlchimierezeptGUID = Guid.NewGuid();
        }

        public bool Usergenerated
        {
            get { return !AlchimierezeptGUID.ToString().StartsWith("00000000-0000-0000-00"); }
        }
    }
}
