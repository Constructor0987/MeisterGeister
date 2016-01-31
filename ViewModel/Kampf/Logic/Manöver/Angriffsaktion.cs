using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeisterGeister.Logic.General;

namespace MeisterGeister.ViewModel.Kampf.Logic.Manöver
{
    public abstract class Angriffsaktion : Manöver
    {
        public Angriffsaktion(KämpferInfo ausführender)
            : base(ausführender)
        { }

        public Angriffsaktion(KämpferInfo ausführender, IDictionary<IWaffe, KämpferInfo> waffe_ziel)
            : base(ausführender, waffe_ziel)
        { }

        public Angriffsaktion(KämpferInfo ausführender, IWaffe waffe, KämpferInfo ziel)
            : base(ausführender, waffe, ziel)
        { }

        protected override void Init()
        {
            base.Init();
            Angriffsaktionen = 1;
        }

        protected override IEnumerable<Probe> ProbenAnlegen()
        {
            Probe p = new Probe();
            p.Probenname = Name;
            p.Werte = new int[] { Ausführender.Kämpfer.AT ?? 0 };
            p.WerteNamen = "AT";
            yield return p;
        }

        //TODO JT: Wenn AusdauerImKampf
        //Wenn Waffe schwerer als KK*10 Unzen
        // Ausführender.AusdauerAktuell--;
        //Wenn BE / 3 > 0
        // Ausführender.AusdauerAktuell-= BE/3;
    }
}
