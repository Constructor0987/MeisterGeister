﻿using MeisterGeister.Model.Extensions;
using MeisterGeister.View.General;
using MeisterGeister.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeisterGeister.ViewModel.Kampf.Logic.Manöver
{
    public abstract class ManöverModifikator<TWaffe> : ViewModelBase where TWaffe : IWaffe
    {
        protected Manöver<TWaffe> manöver;
        public ManöverModifikator(Manöver<TWaffe> manöver)
        {
            this.manöver = manöver;
            PropertyChanged += DependentProperty.PropagateINotifyProperyChanged;
        }

        [DependentProperty("Value")]
        public abstract int Result
        {
            get;
        }
    }

    public abstract class ManöverModifikator<T, TWaffe> : ManöverModifikator<TWaffe> where TWaffe : IWaffe
    {
        public ManöverModifikator(Manöver<TWaffe> manöver) : base(manöver)
        {
            if (typeof(T) == typeof(int))
                GetMod = (w, v) => Convert.ToInt32(Value);
        }

        private int _calcModifikator;
        public int CalcModifikator()
        {
            int to_return = 0;
            try
            {
                to_return = base.manöver.Mods.Values.Sum(mod => mod.Result);
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Fehler bei Berechnung", ex);
            }            
            return to_return;
        }



        private Func<TWaffe, T, int> getMod;
        public Func<TWaffe, T, int> GetMod
        {
            get { return getMod; }
            set { Set(ref getMod, value); }
        }

        private T value;
        public T Value
        {
            get { return value; }
            set
            {
                T oldValue = value;
                Set(ref this.value, value);
                // berechne gesamt...
                if ((manöver.Ausführender.Kämpfer as Bodenplan.Logic.BattlegroundCreature).IsSelected)
                {
                    base.manöver.GetGesamt = CalcModifikator();

                    if (!manöver.InitPase)
                    {
                        if (manöver.GetType().Name == "Attacke")
                        {
                            manöver.Ausführender.PreAngriffsMods = new Dictionary<string, ManöverModifikator<INahkampfwaffe>>();
                            for (var i = 0; i < base.manöver.Mods.Values.Count; i++)
                            {
                                manöver.Ausführender.PreAngriffsMods.Add(base.manöver.Mods.Keys.ElementAt(i), base.manöver.Mods.Values.ElementAt(i) as ManöverModifikator<INahkampfwaffe>);
                            }
                        }
                        if (manöver.GetType().Name == "FernkampfManöver")
                        {
                            manöver.Ausführender.PreFernkampfMods = new Dictionary<string, ManöverModifikator<IFernkampfwaffe>>();
                            manöver.Ausführender.PreFernkampfWaffe = (manöver as FernkampfManöver).FernkampfWaffeSelected;
                            for (var i = 0; i < base.manöver.Mods.Values.Count; i++)
                            {
                                manöver.Ausführender.PreFernkampfMods.Add(base.manöver.Mods.Keys.ElementAt(i), base.manöver.Mods.Values.ElementAt(i) as ManöverModifikator<IFernkampfwaffe>);
                            }
                        }
                        if (manöver.GetType().Name == "AbwehrManöver")
                        {
                            manöver.Ausführender.PreAbwehrMods = new Dictionary<string, ManöverModifikator<INahkampfwaffe>>();
                            for (var i = 0; i < base.manöver.Mods.Values.Count; i++)
                            {
                                manöver.Ausführender.PreAbwehrMods.Add(base.manöver.Mods.Keys.ElementAt(i), base.manöver.Mods.Values.ElementAt(i) as ManöverModifikator<INahkampfwaffe>);
                            }
                        }
                    }
                }
            }
        }
    }

    public class ZauberModifikator<T> : ManöverModifikator<T, IWaffe>
    {
        public ZauberModifikator(Manöver<IWaffe> manöver) : base(manöver)
        {
        }

        public override int Result
        {
            get
            {
                return GetMod(manöver.Ausführender.Kämpfer.Angriffswaffen.FirstOrDefault(), Value);
            }
        }
    }

    public class NahkampfModifikator<T> : ManöverModifikator<T, INahkampfwaffe>
    {
        public NahkampfModifikator(Manöver<INahkampfwaffe> manöver) : base(manöver)
        {
        }

        public override int Result
        {
            get
            {
                return GetMod(manöver.Ausführender.Kämpfer.Angriffswaffen.FirstOrDefault(), Value);
            }
        }
    }

    public class FernkampfModifikator<T> : ManöverModifikator<T, IFernkampfwaffe>
    {
        public FernkampfModifikator(Manöver<IFernkampfwaffe> manöver) : base(manöver)
        {
        }

        public override int Result
        {
            get
            {
                if ((manöver as FernkampfManöver).FernkampfWaffeSelected == null && manöver.Ausführender.Kämpfer.Fernkampfwaffen.Count > 0)
                    (manöver as FernkampfManöver).FernkampfWaffeSelected = manöver.Ausführender.Kämpfer.Fernkampfwaffen.FirstOrDefault();
                return GetMod((manöver as FernkampfManöver).FernkampfWaffeSelected, Value);
            }
        }
    }

    //public class SumMod
    //{
    //    public GetSumMod(NahkampfModifikator )
    //    public int value = 0;
    //}
}
