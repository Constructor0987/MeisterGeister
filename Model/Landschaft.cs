using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeisterGeister.Model
{
    public partial class Landschaft
    {
        public bool GeländekundeAktiv
        {
            get
            {
                return !String.IsNullOrEmpty(Kundig) &&
                    Global.SelectedHeld != null &&
                    Global.SelectedHeld.HatSonderfertigkeitUndVoraussetzungen("Geländekunde (" + Kundig + ")");
            }
        }
    }
}
