using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeisterGeister.ViewModel.Kampf.Logic.Manöver
{
    public class Todesstoß : GezielterStich
    {
        public Todesstoß(KämpferInfo ausführender) : base(ausführender)
        {
        }
    }
}
