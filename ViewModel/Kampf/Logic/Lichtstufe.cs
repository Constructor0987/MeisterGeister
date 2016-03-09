using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeisterGeister.ViewModel.Kampf.Logic
{
    public enum Lichtstufe
    {
        [Description("Tageslicht")]
        Tageslicht,
        [Description("Dämmerung")]
        Dämmerung,
        [Description("Mondlicht")]
        Mondlicht,
        [Description("Sternenlicht")]
        Sternenlicht,
        [Description("Finsternis")]
        Finsternis
    }
}
