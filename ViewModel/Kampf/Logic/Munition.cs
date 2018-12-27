using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeisterGeister.ViewModel.Kampf.Logic
{
    public enum Munition
    {
        [Description("Jagdpfeil/-bolzen")]
        Jagdpfeil,
        [Description("Kriegspfeil/-bolzen")]
        Kriegspfeil,
        [Description("Kettenbrecher")]
        Kettenbrecher,
        [Description("Stumpfer Pfeil/Bolzen")]
        StumpferPfeil,
        [Description("Sehnen-/Seilschneider")]
        Seilschneider,
        [Description("Brandpfeil/-bolzen")]
        Brandpfeil
    }
}
