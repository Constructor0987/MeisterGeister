using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeisterGeister.Logic.General;

namespace MeisterGeister.ViewModel.Kampf.Logic.Manöver
{
    public class Gegenhalten : Abwehraktion
    {
        public static new bool BeherrschtManöver(KämpferInfo ausführender)
        {
            if (ausführender == null)
                return false;
            if (ausführender.Kämpfer is Model.Held)
                return ((Model.Held)ausführender.Kämpfer).HatSonderfertigkeitUndVoraussetzungen("Gegenhalten", true);
            else //TODO evtl check auf Kampfregel
                return false;
        }

        public Gegenhalten(KämpferInfo ausführender)
            : base(ausführender)
        { }

        public Gegenhalten(KämpferInfo ausführender, IWaffe waffe, KämpferInfo ziel)
            : base(ausführender, waffe, ziel)
        {
        }

        protected override void Init()
        {
            base.Init();
            Name = "Gegenhalten";
            Literatur = "WdS 68";
            Abwehraktionen = 1;
            Grunderschwernis = 4;
        }

        protected override IEnumerable<Probe> ProbenAnlegen()
        {
            Probe p = new Probe();
            p.Probenname = Name;
            p.Werte = new int[] { Ausführender.Kämpfer.AT ?? 0 };
            p.WerteNamen = "AT";
            p.Modifikator = Erschwernis;
            yield return p;
        }

        protected override void Erfolg(IKämpfer ziel)
        {
            //TODO: Prüfen wer besser trifft und den Schaden verursachen
            throw new NotImplementedException();
        }
    }
}
