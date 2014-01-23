using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeisterGeister.ViewModel.Helden.Logic;

namespace MeisterGeister.Model
{
    // Man kann Superklassen hinzufügen. Es sollten jedoch nicht die gleichen Eigenschaften, wie in der Datenbankklasse existieren.
    public partial class Held_Sonderfertigkeit : IHatHeld
    {
        public Held_Sonderfertigkeit()
        {
            Wert = String.Empty;            
        }
    }
}
