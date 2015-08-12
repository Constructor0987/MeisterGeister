using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeisterGeister.Logic.General.DataVirtualization;
using MeisterGeister.Logic.Kalender.DsaTool;

namespace MeisterGeister.ViewModel.Kalender.Logic
{
    public class DateProvider : IItemsProvider<DatumViewModel> //TODO mit Typ ergänzen, der auch Ereignisse zu dem Datum kennt.
    {
        private long bf0 = 0;
        public DateProvider(int dayoffset = 0)
        {
            bf0 = FetchCount() / 2 + dayoffset;
        }

        public int FetchCount()
        {
            return (int)(Int64.MaxValue / DSADateTime.TICKS_PER_DAY);
        }

        public IList<DatumViewModel> FetchRange(int startIndex, int count, out int overallCount)
        {
            overallCount = FetchCount();
            var l = new List<DatumViewModel>(count);
            for (int i = 0; i < count; i++)
            {
                var d = new DatumViewModel(new DSADateTime( (startIndex + i -bf0) * DSADateTime.TICKS_PER_DAY));
                l.Add(d);
            }
            return l;
        }
    }
}
