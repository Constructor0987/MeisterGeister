using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeisterGeister.Logic.General;

namespace MeisterGeister.ViewModel.Kampf.Logic.Manöver
{
    public class Attacke : AngriffsManöver
    {
        public Attacke(KämpferInfo ausführender)
            : base(ausführender)
        { }

        protected override void Init()
        {
            base.Init();
            Name = "Attacke";
            Literatur = "WdS 59";
        }
    }
}
