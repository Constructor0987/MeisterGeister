using MeisterGeister.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public override void RegisterEvents()
        {
            base.RegisterEvents();
            Ausführender.Kampf.PropertyChanged += Kampf_PropertyChanged;
        }

        public override void UnregisterEvents()
        {
            Ausführender.Kampf.PropertyChanged -= Kampf_PropertyChanged;
            base.UnregisterEvents();
        }

        private void Kampf_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Licht")
            {
                UpdateLicht();
            }
        }

        private void UpdateLicht()
        {
            ((ManöverModifikator<Lichtstufe, TWaffe>)Mods[LICHT_MOD]).Value = Ausführender.Kampf.Licht;
        }

        public const string LICHT_MOD = "Licht";
        public const string GRÖSSE_MOD = "Zielgröße";

        protected bool KämpftMitTalent(TWaffe waffe, params string[] talente)
        {
            if (waffe.Talent == null)
                return false;
            return talente.Contains(waffe.Talent.Name);
        }

        protected override void InitMods()
        {
            base.InitMods();
        }

        protected override void SetDefaultModValues()
        {
            base.SetDefaultModValues();

            UpdateLicht();
        }

        protected abstract int LichtMod(TWaffe waffe, Lichtstufe value);
        protected abstract int GrößeMod(TWaffe waffe, Größe value);
    }
}
