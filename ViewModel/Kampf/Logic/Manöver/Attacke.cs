using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.ViewModel.Kampf.Logic.Manöver
{
    public class Attacke : Angriffsaktion
    {
        public Attacke(IKämpfer ausführender)
            : base(ausführender)
        { }

        public Attacke(IKämpfer ausführender, IWaffe waffe, IKämpfer ziel)
            : base(ausführender, waffe, ziel)
        {
        }

        public override String Name
        {
            get { return "Attacke"; }
        }
    }
}
