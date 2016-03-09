using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.ViewModel.Kampf.Logic.Manöver
{
    public class Parade : AbwehrManöver
    {
        public Parade(KämpferInfo ausführender)
            : base(ausführender)
        { }

        protected override int GrößeMod(INahkampfwaffe waffe, Größe value)
        {
            switch (value)
            {
                case Größe.Winzig:
                    return 100;
                case Größe.SehrKlein:
                    //TODO: Bei Schilden 0
                    return 4;
                case Größe.Klein:
                case Größe.Mittel:
                    return 0;
                case Größe.Groß:
                    //TODO: Bei Schilden 0, ansonsten unmöglich
                    return 100;
                case Größe.SehrGroß:
                    //Bei sehr großen Gegnern geht nur ausweichen
                    return 100;
                default:
                    return 0;
            }
        }

        protected override void Init()
        {
            base.Init();
            Name = "Parade";
            Literatur = "WdS 66";
        }

        protected override void Erfolg(IKämpfer ziel)
        {
            //Bei Erfolg passiert garnichts
        }
    }
}
