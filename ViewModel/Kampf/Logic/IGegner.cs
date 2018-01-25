using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.ViewModel.Kampf.Logic
{
    public interface IHatGegnerBase
    {
        Model.GegnerBase GegnerBase
        {
            get;
            set;
        }
    }
}
