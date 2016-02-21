using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeisterGeister.Logic.General;
using MeisterGeister.Model;

namespace MeisterGeister.ViewModel.Kampf.Logic.Manöver
{
    public class Zauber : Manöver<IWaffe>
    {
        public Zauber(KämpferInfo ausführender, Held_Zauber zauber):base(ausführender, 1)
        {

        }
        protected override void Init()
        {
            base.Init();
            Name = "Zorn der Elemente";
        }
        protected override void Erfolg(IKämpfer ziel)
        {
            
        }

        protected override IEnumerable<Probe> ProbenAnlegen()
        {
            //TODO: Implementieren
            yield return new Probe();
        }
    }
}
