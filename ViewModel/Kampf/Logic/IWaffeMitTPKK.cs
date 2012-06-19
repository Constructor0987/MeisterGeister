using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.ViewModel.Kampf.Logic
{
    public interface IWaffeMitTPKK
    {
        int? TPKKSchwelle { get; }
        int? TPKKSchritt { get; }
    }
}
