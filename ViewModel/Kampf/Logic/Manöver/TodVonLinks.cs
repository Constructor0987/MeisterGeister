using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.ViewModel.Kampf.Logic.Manöver
{
    public class TodVonLinks : Angriffsaktion
    {
        public TodVonLinks(IKämpfer ausführender)
            : base(ausführender)
        { }

        public TodVonLinks(IKämpfer ausführender, IWaffe waffe, IKämpfer ziel)
            : base(ausführender, waffe, ziel)
        {
        }

        public override String Name
        {
            get { return "Tod von Links"; }
        }
    }
}
