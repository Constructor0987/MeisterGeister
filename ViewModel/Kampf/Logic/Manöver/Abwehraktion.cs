using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.ViewModel.Kampf.Logic.Manöver
{
    public class Abwehraktion : Manöver
    {
        public Abwehraktion(IKämpfer ausführender)
            : base(ausführender)
        { }

        public Abwehraktion(IKämpfer ausführender, IDictionary<IWaffe, IKämpfer> waffe_ziel)
            : base(ausführender, waffe_ziel)
        { }

        public Abwehraktion(IKämpfer ausführender, IWaffe waffe, IKämpfer ziel)
            : base(ausführender, waffe, ziel)
        { }

        //TODO JT: In der Probe muss hier ein Paradeerleichterungsmodifikator abgefragt werden. Ebenso Paradeerschwernisse durch Finten oder Linkshändig.
        //Gezielte Schläge sind einfacher zu parieren. (Modifikator mit dem angreifenden Manöver gespeichert zur Verifikation)

        public override String Name
        {
            get { return "Abwehraktion"; }
        }
    }
}
