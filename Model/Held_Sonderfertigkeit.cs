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
            ValidatePropertyChanging += Held_Sonderfertigkeit_ValidatePropertyChanging;
        }

        void Held_Sonderfertigkeit_ValidatePropertyChanging(object sender, string propertyName, object currentValue, object newValue)
        {
            if (Held != null && propertyName == "Wert" && newValue != currentValue)
            {
                if (Held.Held_Sonderfertigkeit.Where(hs => hs.SonderfertigkeitGUID == SonderfertigkeitGUID && hs.Wert == (newValue as string)).Count() >= 1)
                    throw new ArgumentException("Die Sonderfertigkeit ist mit diesem Wert bereits vorhanden.");
            }
        }
    }
}
