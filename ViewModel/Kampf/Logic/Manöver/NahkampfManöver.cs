using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeisterGeister.ViewModel.Kampf.Logic.Manöver
{
    public abstract class NahkampfManöver : Manöver<INahkampfwaffe>
    {
        public NahkampfManöver(KämpferInfo ausführender)
            : base(ausführender)
        { }

        public NahkampfManöver(KämpferInfo ausführender, IDictionary<INahkampfwaffe, KämpferInfo> waffe_ziel)
            : base(ausführender, waffe_ziel)
        { }

        public NahkampfManöver(KämpferInfo ausführender, INahkampfwaffe waffe, KämpferInfo ziel)
            : base(ausführender, waffe, ziel)
        { }
    }
}
