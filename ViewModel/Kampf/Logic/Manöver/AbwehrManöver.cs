using MeisterGeister.Logic.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.ViewModel.Kampf.Logic.Manöver
{
    public abstract class AbwehrManöver : NahkampfManöver
    {
        public AbwehrManöver(KämpferInfo ausführender)
            : base(ausführender)
        { }

        public AbwehrManöver(KämpferInfo ausführender, IDictionary<INahkampfwaffe, KämpferInfo> waffe_ziel)
            : base(ausführender, waffe_ziel)
        { }

        public AbwehrManöver(KämpferInfo ausführender, INahkampfwaffe waffe, KämpferInfo ziel)
            : base(ausführender, waffe, ziel)
        { }

        //TODO JT: In der Probe muss hier ein Paradeerleichterungsmodifikator abgefragt werden. Ebenso Paradeerschwernisse durch Finten oder Linkshändig.
        //Gezielte Schläge sind einfacher zu parieren. (Modifikator mit dem angreifenden Manöver gespeichert zur Verifikation)

        protected override IEnumerable<Probe> ProbenAnlegen()
        {
            Probe p = new Probe();
            p.Probenname = Name;
            p.Werte = new int[] { Ausführender.Kämpfer.PA ?? 0 };
            p.WerteNamen = "PA";
            yield return p;
        }

        protected override void Init()
        {
            base.Init();
            Abwehraktionen = 1;
        }

        //TODO JT: Wenn AusdauerImKampf
        //Wenn Waffe schwerer als KK*10 Unzen
        // Ausführender.AusdauerAktuell--;
        //Wenn BE / 3 > 0
        // Ausführender.AusdauerAktuell-= BE/3;
    }
}
