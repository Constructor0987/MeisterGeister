using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.ViewModel.Kampf.Logic
{
    public interface INahkampfwaffe : IWaffe
    {
        Distanzklasse Distanzklasse { get; }
        int PA { get; }
    }
}
