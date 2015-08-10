using MeisterGeister.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeisterGeister.ViewModel.Beschwörung
{
    public class GolemBeschwörungViewModel : BeschwörungViewModel
    {
        protected override void addMods()
        {
            throw new NotImplementedException();
        }

        protected override List<GegnerBase> loadWesen()
        {
            return null;
        }

        protected override int calcKontrollWert()
        {
            return div(Held.Mut * 2 + Held.Klugheit + Held.Charisma + ZauberWert, 5);
        }

        public override string KontrollFormel
        {
            get { return "(MU + MU + KL + CH + ZfW) / 5"; }
        }
    }
}
