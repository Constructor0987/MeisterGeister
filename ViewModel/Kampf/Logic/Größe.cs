using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeisterGeister.ViewModel.Kampf.Logic
{
    public enum Größe
    {
        [Description("Winzig")]
        Winzig,
        [Description("Sehr klein")]
        SehrKlein,
        [Description("Klein")]
        Klein,
        [Description("Mittel")]
        Mittel,
        [Description("Groß")]
        Groß,
        [Description("Sehr groß")]
        SehrGroß
    }
}
