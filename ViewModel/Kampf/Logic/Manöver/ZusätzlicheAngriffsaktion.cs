using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.ViewModel.Kampf.Logic.Manöver
{
    public class ZusätzlicheAngriffsaktion : Angriffsaktion
    {
        public ZusätzlicheAngriffsaktion(IKämpfer ausführender)
            : base(ausführender)
        { }

        public ZusätzlicheAngriffsaktion(IKämpfer ausführender, IWaffe waffe, IKämpfer ziel)
            : base(ausführender, waffe, ziel)
        {
        }

        public override String Name
        {
            get { return "Zusätzliche Angriffsaktion"; }
        }

        public override String Literatur
        {
            get { return "WdS 72 / TCD 156"; }
        }
        //kombinierbar mit allen Manövern, die keine automatische Erschwernis von > 4 haben
    }
}
