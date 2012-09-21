using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.Model
{
    public partial class Kampfregel
    {
        public Kampfregel()
        {
            KampfregelGUID = Guid.NewGuid();
        }

        public bool Usergenerated
        {
            get { return !KampfregelGUID.ToString().StartsWith("00000000-0000-0000-000"); }
        }
    }
}
