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

        //protected override IEnumerable<Probe> ProbenAnlegen()
        //{
        //    Probe p = new Probe();
        //    p.Probenname = Name;
        //    p.Werte = new int[] { Ausführender.Kämpfer.AT ?? 0 };
        //    p.WerteNamen = "AT";
        //    p.Modifikator = Erschwernis;
        //    yield return p;
        //}

        protected override void Erfolg(Probe p, KämpferInfo ziel)
        {
            //TODO: Prüfen wer besser trifft und den Schaden verursachen
            
        }

        protected override int GrößeMod(INahkampfwaffe waffe, Größe value)
        {
            //TODO: Gedanken machen
            return 0;
        }
    }
}
