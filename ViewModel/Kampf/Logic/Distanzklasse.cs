using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.ViewModel.Kampf.Logic
{
    [Flags]
    public enum Distanzklasse
    {
        None = 0x0,
        H = 0x1,
        N = 0x2,
        HN = 0x3,
        S = 0x4,
        NS = 0x6,
        HNS = 0x7,
        P = 0x8,
        SP = 0xC,
        NSP = 0xE,
        HNSP = 0xF,
        X = 0x10,
        PX = 0x18,
        SPX = 0x1C,
        NSPX = 0x1E,
        HNSPX = 0x1F
    }
}
