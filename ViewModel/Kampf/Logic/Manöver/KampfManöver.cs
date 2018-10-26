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
            if (e.PropertyName == "Sicht")
            {
                UpdateSicht();
            }
            if (e.PropertyName == "Handgemenge")
            {

            }
            if (e.PropertyName == "PositionSelbst")
            {
                UpdatePositionSelbst();
            }
        }

        private void UpdateLicht()
        {
            if (((ManöverModifikator<Lichtstufe, TWaffe>)Mods[LICHT_MOD]).Value != Ausführender.Kampf.Licht)
                ((ManöverModifikator<Lichtstufe, TWaffe>)Mods[LICHT_MOD]).Value = Ausführender.Kampf.Licht;
        }

        private void UpdateSicht()
        {
            if (((ManöverModifikator<Sichtstufe, TWaffe>)Mods[SICHT_MOD]).Value != Ausführender.Kampf.Sicht)
                ((ManöverModifikator<Sichtstufe, TWaffe>)Mods[SICHT_MOD]).Value = Ausführender.Kampf.Sicht;
        }

        private void UpdatePositionSelbst()
        {
           // ((ManöverModifikator<Position, TWaffe>)Mods[POS_SELBST_MOD]).Value = Ausführender.Kämpfer.Position;
        }

        public const string LICHT_MOD = "Licht";
        public const string SICHT_MOD = "Sicht";
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
            UpdateSicht();
            UpdatePositionSelbst();
        }

        protected abstract int LichtMod(TWaffe waffe, Lichtstufe value);
        protected abstract int SichtMod(TWaffe waffe, Sichtstufe value);
       // protected int PositionSelbstMod(TWaffe waffe, Position value)
       //// protected override int PositionSelbstMod(IFernkampfwaffe waffe, Position value)
       // {
       //     if (Global.CurrentKampf.SelectedManöver != null)
       //     {
       //         if (Global.CurrentKampf.BodenplanViewModel.DoChangeModPositionSelbst &&
       //             ((ManöverModifikator<Position, TWaffe>)Mods[POS_SELBST_MOD]).Value != Global.CurrentKampf.SelectedManöver.Manöver.Ausführender.PositionSelbst)
       //         //   ((ManöverModifikator<Position, Waffe>)Mods[POS_SELBST_MOD]).Value != Global.CurrentKampf.SelectedManöver.Manöver.Ausführender.PositionSelbst)
       //         {
       //             ((ManöverModifikator<Position, TWaffe>)Mods[POS_SELBST_MOD]).Value = Global.CurrentKampf.SelectedManöver.Manöver.Ausführender.PositionSelbst.Value;
       //             value = Global.CurrentKampf.SelectedManöver.Manöver.Ausführender.PositionSelbst.Value;
       //         }

       //         IKämpfer bodenplanKämpfer = (Global.CurrentKampf.BodenplanViewModel.BattlegroundObjects.Where(t => t is IKämpfer)
       //             .FirstOrDefault(t => ((IKämpfer)t) == Global.CurrentKampf.SelectedManöver.Manöver.Ausführender.Kämpfer) as IKämpfer);

       //         if (Global.CurrentKampf.Kampf.tempP == null &&
       //             bodenplanKämpfer != null && bodenplanKämpfer.Position != ((ManöverModifikator<Position, TWaffe>)Mods[POS_SELBST_MOD]).Value)
       //             bodenplanKämpfer.Position = ((ManöverModifikator<Position, TWaffe>)Mods[POS_SELBST_MOD]).Value;
       //     }
       //     Global.CurrentKampf.BodenplanViewModel.DoChangeModPositionSelbst = false;

       //     return 0;
       // }
        protected abstract int GrößeMod(TWaffe waffe, Größe value);
    }
}
