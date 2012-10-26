using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeisterGeister.ViewModel.Helden.Logic;

namespace MeisterGeister.Model
{
    // Man kann Superklassen hinzufügen. Es sollten jedoch nicht die gleichen Eigenschaften, wie in der Datenbankklasse existieren.
    public partial class Held_VorNachteil : IHatHeld
    {
        public Nullable<int> WertInt
        {
            get
            {
                if (Wert == null)
                    return 0;
                int nr = 0;
                if (int.TryParse(Wert, out nr))
                    return nr;
                return 0;
            }
            set { Wert = value.ToString(); }
        }
    }
}
