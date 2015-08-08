using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeisterGeister.ViewModel.Beschwörung
{
    public class UntotenBeschwörungsViewModel : BeschwörungViewModel
    {
        protected override List<Model.GegnerBase> loadWesen()
        {
            return Global.ContextHeld.LoadUntote();
        }

        protected override int calcKontrollWert()
        {
            return (int)Math.Round((Held.Mut * 2 + Held.Klugheit + Held.Charisma + ZauberWert) / 5.0, MidpointRounding.AwayFromZero);
        }

        public override string KontrollFormel
        {
            get { return "(MU + MU + KL + CH + ZfW) / 5"; }
        }

        protected override void addMods()
        {
            throw new NotImplementedException();
        }
    }
}
