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
        private const string AFFINITÄT = "Affinität zu Geistern";

        public GeisterBeschwörungViewModel()
        {
            Zauber = "Geisterruf";
        }

        protected override void checkHeld()
        {
            base.checkHeld();
            if (Held != null)
                affinität.Value = Held.HatVorNachteil("Affinität zu Geistern");
            else
                affinität.Value = false;
        }

        protected override List<Model.GegnerBase> loadWesen()
        {
            return Global.ContextHeld.LoadGeister();
        }

        protected override int calcKontrollWert()
        {
            return div(Held.Mut + Held.Intuition + Held.Charisma * 2 + ZauberWert, 5);
        }

        public override string KontrollFormel
        {
            get { return "(MU + IN + CH + CH + ZfW) / 5"; }
        }

        private const string MOD_TAG = "Tag";
        private const string MOD_AFFINITÄT = "Affinität";

        private BeschwörungsModifikator<bool> tag;
        private BeschwörungsModifikator<bool> affinität;

        protected override void addMods()
        {
            tag = new BeschwörungsModifikator<bool>();
            tag.GetAnrufungsMod = () => tag.Value ? 7 : 0;
            Mods.Add(MOD_TAG, tag);

            affinität = new BeschwörungsModifikator<bool>();
            affinität.GetKontrollMod = () => affinität.Value ? -3 : 0;
            Mods.Add(MOD_AFFINITÄT, affinität);
        }
    }
}
