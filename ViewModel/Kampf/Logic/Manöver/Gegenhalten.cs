using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.ViewModel.Kampf.Logic.Manöver
{
    public class Gegenhalten : Abwehraktion
    {
        public Gegenhalten(KämpferInfo ausführender)
            : base(ausführender)
        { }

        public Gegenhalten(KämpferInfo ausführender, IWaffe waffe, KämpferInfo ziel)
            : base(ausführender, waffe, ziel)
        {
        }

        public override String Name
        {
            get { return "Gegenhalten"; }
        }

        public static new bool BeherrschtManöver(KämpferInfo ausführender)
        {
            if (ausführender == null)
                return false;
            if (ausführender.Kämpfer is Model.Held)
                return ((Model.Held)ausführender.Kämpfer).HatSonderfertigkeitUndVoraussetzungen("Gegenhalten", true);
            else //TODO evtl check auf Kampfregel
                return false;
        }
    }
}
