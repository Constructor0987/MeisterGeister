using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.ViewModel.Kampf.Logic.Manöver
{
    public class Abwehraktion : Manöver
    {
        public Abwehraktion(KämpferInfo ausführender)
            : base(ausführender)
        { }

        public Abwehraktion(KämpferInfo ausführender, IDictionary<IWaffe, KämpferInfo> waffe_ziel)
            : base(ausführender, waffe_ziel)
        { }

        public Abwehraktion(KämpferInfo ausführender, IWaffe waffe, KämpferInfo ziel)
            : base(ausführender, waffe, ziel)
        { }

        //TODO JT: In der Probe muss hier ein Paradeerleichterungsmodifikator abgefragt werden. Ebenso Paradeerschwernisse durch Finten oder Linkshändig.
        //Gezielte Schläge sind einfacher zu parieren. (Modifikator mit dem angreifenden Manöver gespeichert zur Verifikation)

        public override String Name
        {
            get { return "Abwehraktion"; }
        }

        public override string Literatur
        {
            get
            {
                return "WdS 66";
            }
        }

        public override int Abwehraktionen
        {
            get
            {
                return 1;
            }
        }

        public override int Angriffsaktionen
        {
            get
            {
                return 0;
            }
        }

        protected override void OnAktion()
        {
            //TODO JT: Wenn AusdauerImKampf
            //Wenn Waffe schwerer als KK*10 Unzen
            // Ausführender.AusdauerAktuell--;
            //Wenn BE / 3 > 0
            // Ausführender.AusdauerAktuell-= BE/3;
            base.OnAktion();
        }
    }
}
