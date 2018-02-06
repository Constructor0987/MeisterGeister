using MeisterGeister.Model.Extensions;
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
                Set(ref this.value, value);
                // berechne gesamt...
                base.manöver.GetGesamt = CalcModifikator();
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
                return GetMod(manöver.Ausführender.Kämpfer.Fernkampfwaffen.FirstOrDefault(), Value);
            }
        }
    }
}
