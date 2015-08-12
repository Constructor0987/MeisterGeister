using MeisterGeister.Logic.Kalender.DsaTool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeisterGeister.Logic.General.DataVirtualization;

namespace MeisterGeister.ViewModel.Kalender.Logic
{
    public class DateList : AsyncVirtualizingCollection<DatumViewModel>
    {
        public DateList(int tageProMonat = 30, int tageProWoche = 7, int aktuellerTag = 0) : base(new DateProvider(aktuellerTag), tageProMonat + 2 * tageProWoche, 1000)
        {
        }
    }
}
