using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.ViewModel.Kampf.Logic.Manöver
{
    public class Parade : Abwehraktion
    {
        public Parade(KämpferInfo ausführender)
            : base(ausführender)
        { }

        public Parade(KämpferInfo ausführender, IWaffe waffe, KämpferInfo ziel)
            : base(ausführender, waffe, ziel)
        {
        }

        public override String Name
        {
            get { return "Parade"; }
        }
    }
}
