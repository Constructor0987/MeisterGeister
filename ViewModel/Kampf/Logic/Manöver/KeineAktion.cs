using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.ViewModel.Kampf.Logic.Manöver
{
    public class KeineAktion : Manöver
    {
        public KeineAktion(IKämpfer ausführender)
            : base(ausführender)
        { }

        public override String Name
        {
            get { return "Keine Aktion"; }
        }
    }
}
