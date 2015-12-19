using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace MeisterGeister.Logic.Kalender
{
    public enum Monat
    {
        [Description("Praios")]
        Praios = 0,     // Juli
        [Description("Rondra")]
        Rondra = 1,     // August
        [Description("Efferd")]
        Efferd = 2,     // September
        [Description("Travia")]
        Travia = 3,     // Oktober
        [Description("Boron")]
        Boron = 4,      // November
        [Description("Hesinde")]
        Hesinde = 5,    // Dezember
        [Description("Firun")]
        Firun = 6,      // Januar
        [Description("Tsa")]
        Tsa = 7,        // Februar
        [Description("Phex")]
        Phex = 8,       // März
        [Description("Peraine")]
        Peraine = 9,    // April
        [Description("Ingerimm")]
        Ingerimm = 10,  // Mai
        [Description("Rahja")]
        Rahja = 11,     // Juni
        [Description("Namenlose Tage")]
        NamenloseTage = 12
    }
}
