using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeisterGeister.ViewModel.Kampf.Logic
{
    public enum Bewegung
    {
        [Description("Unbewegliches fest montiertes Ziel")]
        Unbeweglich,
        [Description("Still stehendes Ziel")]
        StillStehend,
        [Description("Leichte Bewegung")]
        Leicht,
        [Description("Schnelle Bewegung")]
        Schnell,
        [Description("Sehr Schnelle Bewegung(Ausweichen)")]
        SehrSchnell
    }
}
