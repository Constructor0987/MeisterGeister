using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeisterGeister.Logic.General;

namespace MeisterGeister.ViewModel.Kampf.Logic.Manöver
{
    public class Gegenhalten : AbwehrManöver
    {

        public Gegenhalten(KämpferInfo ausführender)
            : base(ausführender)
        { }


        protected override void Init()
        {
            base.Init();
            Name = "Gegenhalten";
            Literatur = "WdS 68";
            Abwehraktionen = 1;
            Grunderschwernis = 4;
        }

        protected override int GrößeMod(INahkampfwaffe waffe, Größe value)
        {
            //TODO: Gedanken machen
            return 0;
        }
    }
}
