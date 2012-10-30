using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.ViewModel.Kampf.Logic
{
    public interface IHasWunden
    {
        int? WundenKopf{ get; set; }
        int? WundenBrust{ get; set; }
        int? WundenArmL{ get; set; }
        int? WundenArmR{ get; set; }
        int? WundenBauch{ get; set; }
        int? WundenBeinL{ get; set; }
        int? WundenBeinR{ get; set; }
        int? Wunden { get; set; }
    }
}
