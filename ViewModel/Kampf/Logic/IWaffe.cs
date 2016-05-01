using MeisterGeister.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.ViewModel.Kampf.Logic
{
    public interface IWaffe
    {
        string Name {get;}
        int TPWürfel {get;}
        int TPWürfelAnzahl { get; }
        int TPBonus { get; }
        /// <summary>
        /// effektive Bonuspunkte durch KK
        /// </summary>
        int TPKKBonus { get; }
        int AT {get;}
        Talent Talent { get; }
    }
}
