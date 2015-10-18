using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeisterGeister.ViewModel.Helden.Logic;
using MeisterGeister.Model.Extensions;

namespace MeisterGeister.Model
{
    // Man kann Superklassen hinzufügen. Es sollten jedoch nicht die gleichen Eigenschaften, wie in der Datenbankklasse existieren.
    public partial class Held_VorNachteil : IHatHeld
    {
        public Held_VorNachteil()
        {
            Wert = String.Empty;
            ValidatePropertyChanging += Held_VorNachteil_ValidatePropertyChanging;
            PropertyChanged += DependentProperty.PropagateINotifyProperyChanged;
        }

        void Held_VorNachteil_ValidatePropertyChanging(object sender, string propertyName, object currentValue, object newValue)
        {
            if (Held != null && propertyName == "Wert" && newValue != currentValue)
            {
                if (Held.Held_VorNachteil.Where(hvn => hvn.VorNachteilGUID == VorNachteilGUID && hvn.Wert == (newValue as string)).Count() >= 1)
                    throw new ArgumentException("Der Vor-/Nachteil ist mit diesem Wert bereits vorhanden.");
            }
        }

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

        /// <summary>
        /// Gibt die gesamten AP/GP-Kosten zurück. Bei mehrstufigem VorNachteil also die Einzelkosten * Anzahl.
        /// </summary>
        [DependentProperty("Kosten"), DependentProperty("WertInt")]
        public double KostenGesamt
        {
            get
            {
                if (VorNachteil != null && (VorNachteil.HatWert ?? false) && VorNachteil.WertTyp == "int")
                    return Kosten * WertInt.Value;
                return Kosten;
            }
        }
    }
}
