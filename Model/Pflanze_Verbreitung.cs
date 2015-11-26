using MeisterGeister.ViewModel.Karte;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeisterGeister.Model
{
    public partial class Pflanze_Verbreitung
    {
        public PflanzenSucheViewModel Suche
        {
            get
            {
                return new PflanzenSucheViewModel() { Pflanze = this };
            }
        }
    }
}
