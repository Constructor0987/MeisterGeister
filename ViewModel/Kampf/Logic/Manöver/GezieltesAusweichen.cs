using MeisterGeister.Logic.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeisterGeister.ViewModel.Kampf.Logic.Manöver
{
    class GezieltesAusweichen : AbwehrManöver
    {
        public GezieltesAusweichen(KämpferInfo ausführender) : base(ausführender)
        { }

        #region Mods

        public const string FERNKAMPFWAFFE_MOD = "Fernkampfwaffe";

        protected NahkampfModifikator<int> fernkampfwaffe;

        protected override void InitMods(IWaffe waffe)
        {
            base.InitMods(waffe);

            fernkampfwaffe = new NahkampfModifikator<int>(this);
            mods.Add(FERNKAMPFWAFFE_MOD, fernkampfwaffe);
        }

        //protected override int PositionSelbstMod(INahkampfwaffe waffe, Position value)
        //{        
            //if (Global.CurrentKampf.BodenplanViewModel.DoChangeModPositionSelbst &&
            //   ((ManöverModifikator<Position, INahkampfwaffe>)Mods[POS_SELBST_MOD]).Value != Global.CurrentKampf.SelectedManöver.Manöver.Ausführender.PositionSelbst)
            //{
            //    ((ManöverModifikator<Position, INahkampfwaffe>)Mods[POS_SELBST_MOD]).Value = Global.CurrentKampf.SelectedManöver.Manöver.Ausführender.PositionSelbst;
            //    value = Global.CurrentKampf.SelectedManöver.Manöver.Ausführender.PositionSelbst;
            //}
            //Global.CurrentKampf.BodenplanViewModel.DoChangeModPositionSelbst = false;

            //IKämpfer bodenplanKämpfer = (Global.CurrentKampf.BodenplanViewModel.BattlegroundObjects.Where(t => t is IKämpfer)
            //    .FirstOrDefault(t => ((IKämpfer)t) == Global.CurrentKampf.SelectedManöver.Manöver.Ausführender.Kämpfer) as IKämpfer);

            //if (bodenplanKämpfer != null && bodenplanKämpfer.Position != ((ManöverModifikator<Position, INahkampfwaffe>)Mods[POS_SELBST_MOD]).Value)
            //    bodenplanKämpfer.Position = ((ManöverModifikator<Position, INahkampfwaffe>)Mods[POS_SELBST_MOD]).Value;
        //
        //    switch (value)
        //    {
        //        case Position.Kniend:
        //            return 4;
        //        case Position.Liegend:
        //            return 100;
        //        default:
        //            return 0;
        //    }
        //}

        protected override int PositionGegnerMod(INahkampfwaffe waffe, Position value)
        {
            switch(value)
            {
                case Position.Fliegend:
                    return 4;
                case Position.Kniend:
                    return -3;
                case Position.Liegend:
                    return -5;
                default:
                    return 0;
            }
        }

        protected override int ÜberzahlMod(INahkampfwaffe waffe, int value)
        {
            if (value >= 4)
                return 100;
            else if (value == 0)
                return 0;
            else return (value - 1) * 2;
        }


        #endregion

        protected override void Init()
        {
            base.Init();
            Name = "Gezieltes Ausweichen";
            Literatur = "WdS 66";
        }

        protected override void Erfolg(Probe p, KämpferInfo ziel, INahkampfwaffe waffe, ManöverEventArgs e_init)
        {
            base.Erfolg(p, ziel, waffe, e_init);
            //Bei winzigen Gegnern verliert man keine INI wenn man ausweicht
            if (größe.Value != Größe.Winzig)
                Ausführender.Initiative -= 2;
        }

        protected override int GrößeMod(INahkampfwaffe waffe, Größe value)
        {
            return 0;
        }
    }
}
