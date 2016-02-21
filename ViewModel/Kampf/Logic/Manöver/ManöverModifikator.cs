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
        private Manöver<TWaffe> manöver;
        public ManöverModifikator(Manöver<TWaffe> manöver)
        {
            this.manöver = manöver;
            PropertyChanged += DependentProperty.PropagateINotifyProperyChanged;
        }

        private Func<TWaffe, int> getMod;
        public Func<TWaffe, int> GetMod
        {
            get { return getMod; }
            set
            {
                getMod = value;
                OnChanged("GetMod");
            }
        }

        public int Result
        {
            get
            {
                int result = GetMod(manöver.Waffen.FirstOrDefault());
                //Debug.WriteLine("ManöverResult = " + result);
                return result;
            }
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
                GetMod = (w) => Convert.ToInt32(Value);
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
