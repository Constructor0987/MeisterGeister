using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeisterGeister.ViewModel.Beschwörung
{
    public enum Element
    {
        [Description("Feuer")]
        Feuer = 0,
        [Description("Wasser")]
        Wasser = ~Feuer,
        [Description("Humus")]
        Humus = 1,
        [Description("Eis")]
        Eis = ~Humus,
        [Description("Luft")]
        Luft = 2,
        [Description("Erz")]
        Erz = ~Luft
    }
}
