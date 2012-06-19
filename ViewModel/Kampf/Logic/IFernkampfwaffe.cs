using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.ViewModel.Kampf.Logic
{
    public interface IFernkampfwaffe : IWaffe
    {
        int? RWSehrNah { get; }
        int? RWNah { get; }
        int? RWMittel { get; }
        int? RWWeit { get; }
        int? RWSehrWeit { get; }
        
        int? TPSehrNah { get; }
        int? TPNah { get; }
        int? TPMittel { get; }
        int? TPWeit { get; }
        int? TPSehrWeit { get; }
    }
}
