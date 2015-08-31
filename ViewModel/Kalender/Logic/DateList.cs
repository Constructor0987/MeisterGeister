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
        public DateList(DSADateCalendar kalender, int tageProMonat, int tageProWoche, int aktuellerTag)
            : this(kalender, tageProMonat + 2 * tageProWoche, aktuellerTag)
        {
        }

        public DateList(DSADateCalendar kalender, int anzuzeigendeTage = 30 + 7 * 2, int aktuellerTag = 0)
            : base(new DateProvider(kalender, aktuellerTag), anzuzeigendeTage, 3000)
        {
        }

        public int GetIndex(DSADateTime date)
        {
            var prov = ItemsProvider as DateProvider;
            return prov.GetIndex((int)date.DaysSinceBF);
        }
    }
}
