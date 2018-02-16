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
        [Description("2 Kleiner als Winzig")]
        Kleiner2AlsWinzig,
        [Description("1 Kleiner als Winzig")]
        Kleiner1AlsWinzig,
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
        SehrGroß,
        [Description("1 Größer als sehr groß")]
        Größer1AlsSehrGroß,
        [Description("2 Größer als sehr groß")]
        Größer2AlsSehrGroß
    }
}
