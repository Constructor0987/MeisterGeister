using MeisterGeister.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeisterGeister.ViewModel.Kampf.Logic.Manöver
{
    public abstract class KampfManöver<TWaffe> : Manöver<TWaffe> where TWaffe : IWaffe
    {
        public KampfManöver(KämpferInfo ausführender) : base(ausführender)
        {
        }

        public const string LICHT_MOD = "Licht";
        public const string GRÖSSE_MOD = "Zielgröße";

        protected override void InitMods()
        {
            base.InitMods();
        }

        protected abstract int LichtMod(TWaffe waffe, Lichtstufe value);
        protected abstract int GrößeMod(TWaffe waffe, Größe value);
    }
}
