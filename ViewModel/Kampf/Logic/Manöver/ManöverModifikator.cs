using MeisterGeister.Model.Extensions;
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
    public abstract class ManöverModifikator<TWaffe> : INotifyPropertyChanged where TWaffe : IWaffe
    {
        protected Manöver<TWaffe> manöver;
        public ManöverModifikator(Manöver<TWaffe> manöver)
        {
            this.manöver = manöver;
            PropertyChanged += DependentProperty.PropagateINotifyProperyChanged;
        }

        public abstract int Result
        {
            get;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnChanged(string property)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(property));
        }
    }

    public class ManöverModifikator<T, TWaffe> : ManöverModifikator<TWaffe> where TWaffe : IWaffe
    {
        public ManöverModifikator(Manöver<TWaffe> manöver) : base(manöver)
        {
            if (typeof(T) == typeof(int))
                GetMod = (w, v) => Convert.ToInt32(Value);
        }

        private Func<TWaffe, T, int> getMod;
        public Func<TWaffe, T, int> GetMod
        {
            get { return getMod; }
            set
            {
                getMod = value;
                OnChanged("GetMod");
            }
        }

        public override int Result
        {
            get
            {
                return GetMod(manöver.Waffen.FirstOrDefault(), Value);
            }
        }

        private T value;
        public T Value
        {
            get { return value; }
            set
            {
                this.value = value;
                OnChanged("Value");
                OnChanged("Result");
            }
        }
    }

    public class NahkampfModifikator<T> : ManöverModifikator<T, INahkampfwaffe>
    {
        public NahkampfModifikator(Manöver<INahkampfwaffe> manöver) : base(manöver)
        {
        }
    }
}
