using MeisterGeister.Logic.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeisterGeister.ViewModel.Beschwörung
{
    public class GeisterBeschwörungViewModel : BeschwörungViewModel
    {
        protected override List<Model.GegnerBase> loadWesen()
        {
            throw new NotImplementedException();
        }

        protected override void beschwörungMisslungen(ProbenErgebnis erg)
        {
            throw new NotImplementedException();
        }

        protected override void beherrschungMisslungen(ProbenErgebnis erg)
        {
            throw new NotImplementedException();
        }

        protected override int calcKontrollWert()
        {
            return (int)Math.Round((Held.Mut + Held.Intuition + Held.Charisma * 2 + ZauberWert) / 5.0, MidpointRounding.AwayFromZero);
        }

        public override string KontrollFormel
        {
            get { return "(MU + IN + CH + CH + ZfW) / 5"; }
        }
    }
}
