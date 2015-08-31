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
        public DateProvider(DSADateCalendar calendar,  int dayoffset = 0)
        {
            Calendar = calendar;
            bf0 = FetchCount() / 2 + dayoffset;
        }

        private DSADateCalendar calendar;
        public DSADateCalendar Calendar
        {
            get { return calendar; }
            set { calendar = value; }
        }

        public int GetIndex(int day)
        {
            long r = bf0 + day;
            if(r>FetchCount() || r < 0)
                return -1;
            return (int)r;
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
                var d = new DatumViewModel(new DSADateTime((startIndex + i - bf0) * DSADateTime.TICKS_PER_DAY), Calendar);
                l.Add(d);
            }
            return l;
        }
    }
}
