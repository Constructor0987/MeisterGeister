using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.ViewModel.Kampf.Logic.Manöver
{
    public class Angriffsaktion : Manöver
    {
        public Angriffsaktion(IKämpfer ausführender)
            : base(ausführender)
        { }

        public Angriffsaktion(IKämpfer ausführender, IDictionary<IWaffe, IKämpfer> waffe_ziel)
            : base(ausführender, waffe_ziel)
        { }

        public Angriffsaktion(IKämpfer ausführender, IWaffe waffe, IKämpfer ziel)
            : base(ausführender, waffe, ziel)
        { }

        public override String Name
        {
            get { return "Angriffsaktion"; }
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
