using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeisterGeister.ViewModel.Kampf.Logic.Manöver
{
    public class GezielterStich : Finte
    {
        public GezielterStich(KämpferInfo ausführender) : base(ausführender)
        {
        }

        public GezielterStich(KämpferInfo ausführender, INahkampfwaffe waffe, KämpferInfo ziel) : base(ausführender, waffe, ziel)
        {
        }
    }
}
