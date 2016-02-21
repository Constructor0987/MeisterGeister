using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.ViewModel.Kampf.Logic.Manöver
{
    public class ZusätzlicheAngriffsaktion : Attacke
    {
        public ZusätzlicheAngriffsaktion(KämpferInfo ausführender)
            : base(ausführender)
        { }

        public ZusätzlicheAngriffsaktion(KämpferInfo ausführender, INahkampfwaffe waffe, KämpferInfo ziel)
            : base(ausführender, waffe, ziel)
        {
        }

        protected override void Init()
        {
            base.Init();
            Name = "Zusätzliche Angriffsaktion";
            Literatur = "WdS 72 / TCD 156";
        }

        //kombinierbar mit allen Manövern, die keine automatische Erschwernis von > 4 haben
    }
}
