using MeisterGeister.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeisterGeister.ViewModel.Beschwörung
{
    public class UntotenBeschwörungViewModel : LoyalitätBeschwörungViewModel
    {
        protected override void reset()
        {
            base.reset();
            zauber.Value = "Skelettarius";
        }

        protected override List<GegnerBase> loadWesen()
        {
            return Global.ContextHeld.LoadUntote();
        }

        protected override int calcKontrollWert()
        {
            return div(Held.Mut * 2 + Held.Klugheit + Held.Charisma + ZauberWert, 5);
        }

        public override string KontrollFormel
        {
            get { return "(MU + MU + KL + CH + ZfW) / 5"; }
        }

        private const string MOD_PAKTIERER = "Paktierer";
        private const string MOD_ZAUBER = "Zauber";

        private BeschwörungsModifikator<int> paktierer;
        private BeschwörungsModifikator<string> zauber;

        protected override void addMods()
        {
            base.addMods();
            //Beim Skelettarius ist die Kontrollprobe um 7 erleichtert
            zauber = new BeschwörungsModifikator<string>();
            zauber.PropertyChanged += (s, e) => Zauber = zauber.Value;
            zauber.GetKontrollMod = () => (zauber.Value == "Skelettarius") ? -7 : 0;
            Mods.Add(MOD_ZAUBER, zauber);

            //Für einen Paktierer der Thargunitoth ist Anrufung und Kontrolle um seinen Kreis der Verdammnis erleichtert
            //Die Kontrolle ist zusätzlich um 3 erleichtert
            paktierer = new BeschwörungsModifikator<int>();
            paktierer.GetAnrufungsMod = () => -paktierer.Value;
            paktierer.GetKontrollMod = () => (paktierer.Value > 0) ? -paktierer.Value - 3 : 0;
            Mods.Add(MOD_PAKTIERER, paktierer);
        }
    }
}
