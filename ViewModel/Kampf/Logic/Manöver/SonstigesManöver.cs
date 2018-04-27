using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeisterGeister.Logic.General;

namespace MeisterGeister.ViewModel.Kampf.Logic.Manöver
{
    public class SonstigesManöver : Manöver
    {
        public SonstigesManöver(KämpferInfo ausführender) : base(ausführender)
        {
        }

        private int _sonstigeDauer = 1;
        public int SonstigeDauer
        {
            get { return _sonstigeDauer; }
            set 
            {
                Set(ref _sonstigeDauer, value);
                Dauer = value;
                VerbleibendeDauer = Dauer;
            }
        }

        public override IEnumerable<KämpferInfo> Ziele
        {
            get
            {
                yield return null;
            }
        }
    }
}
