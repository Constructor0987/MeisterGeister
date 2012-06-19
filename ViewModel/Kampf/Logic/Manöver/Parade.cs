using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.ViewModel.Kampf.Logic.Manöver
{
    public class Parade : Abwehraktion
    {
        public Parade(IKämpfer ausführender)
            : base(ausführender)
        { }

        public Parade(IKämpfer ausführender, IWaffe waffe, IKämpfer ziel)
            : base(ausführender, waffe, ziel)
        {
        }

        public override String Name
        {
            get { return "Parade"; }
        }
    }
}
