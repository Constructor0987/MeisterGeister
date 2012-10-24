using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.ViewModel.Helden.Logic
{
    public interface IHeld
    {
        Model.Held Held
        {
            get;
            set;
        }
    }
}
