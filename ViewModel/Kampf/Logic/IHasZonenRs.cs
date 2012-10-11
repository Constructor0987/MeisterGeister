using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.ViewModel.Kampf.Logic
{
    public interface IHasZonenRs
    {
        int? RSKopf{ get; set; }
        int? RSBrust{ get; set; }
        int? RSRücken{ get; set; }
        int? RSArmL{ get; set; }
        int? RSArmR{ get; set; }
        int? RSBauch{ get; set; }
        int? RSBeinL{ get; set; }
        int? RSBeinR{ get; set; }
    }
}
