using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeisterGeister.ViewModel.Kampf.Logic
{
    public enum Wassertiefe
    {
        [Description("Kein Wasser")]
        KeinWasser,
        [Description("Knietiefes Wasser")]
        Knietief,
        [Description("Hüfttiefes Wasser")]
        Hüfttief,
        [Description("Schultertiefes Wasser")]
        Schultertief,
        [Description("Unter Wasser")]
        UnterWasser
    }
}
