using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.ViewModel.Kampf.Logic
{
    public enum Trefferzone
    {
        //erst die einzelnen Zonen
        Kopf = 0,
        Brust,
        Rücken,
        ArmL,
        ArmR,
        Bauch,
        BeinL,
        BeinR = 7,
        //dann Unlokalisiert
        Unlokalisiert = 8,
        //dann Gesamt
        Gesamt,
        //am Schluss Zufall
        Zufall
    }
}
