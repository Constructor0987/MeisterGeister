using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.ViewModel.Kampf.Logic.Manöver
{
    public class Attacke : Angriffsaktion
    {
        public Attacke(KämpferInfo ausführender)
            : base(ausführender)
        { }

        public Attacke(KämpferInfo ausführender, IWaffe waffe, KämpferInfo ziel)
            : base(ausführender, waffe, ziel)
        {
        }

        protected override void Init()
        {
            base.Init();
            Name = "Attacke";
            Literatur = "WdS 59";
        }

        protected override void Erfolg(IKämpfer ziel)
        {
            //TODO: Parade des Ziels anfordern und schaden machen
        }
    }
}
