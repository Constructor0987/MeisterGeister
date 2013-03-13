using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace MeisterGeister.Logic.Kalender
{
    public enum Kalender
    {
        [Description("BF")]
        BosparansFall,
        [Description("Hal")]
        Hal,
        [Description("Horas")]
        Horas,
        [Description("Reto")]
        Reto,
        [Description("Bardo/Cella")]
        BardoCella,
        [Description("Perval")]
        Perval,
        [Description("Golgari")]
        Golgari,
        [Description("d. U.")]
        AndergastNostria,
        [Description("Kurkum")]
        Kurkum,
        [Description("Jahr des Lichts")]
        JahreDesLichts,
        [Description("Rastullah")]
        Rastullah,
        [Description("JL")]
        Thorwal,
        [Description("Zwergisch")]
        Zwerge,
        [Description("A. D.")]
        Irdisch,
        [Description("Bornland")]
        Bornland,
        [Description("Imperium")]
        Imperium,
        [Description("Engasal")]
        Engasal
    }

}
