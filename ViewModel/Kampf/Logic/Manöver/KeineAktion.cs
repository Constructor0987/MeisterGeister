using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeisterGeister.Logic.General;

namespace MeisterGeister.ViewModel.Kampf.Logic.Manöver
{
    public class KeineAktion : Manöver<IWaffe>
    {
        public KeineAktion(KämpferInfo ausführender)
            : base(ausführender)
        { }

        protected override void Erfolg(Probe p, KämpferInfo ziel)
        {
        }
    }
}
