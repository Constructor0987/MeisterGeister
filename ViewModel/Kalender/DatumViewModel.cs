using MeisterGeister.Logic.Kalender.DsaTool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeisterGeister.ViewModel.Kalender
{
    public class DatumViewModel : Base.ViewModelBase
    {
        /// <summary>
        /// Verbindet ein DSADateTime mit einem Kalender
        /// </summary>
        /// <param name="datum"></param>
        /// <param name="kalender"></param>
        public DatumViewModel(DSADateTime datum, DSADateCalendar kalender = null)
        {
            if(kalender != null)
                Kalender = kalender;
            Datum = datum;
        }

        public override void RegisterEvents()
        {
            //Global.DatumChanged += ...
        }

        public override void UnregisterEvents()
        {

        }

        private void Invalidate()
        {
            text = null;
            isSpecialDay = null;
            weekDay = null;
        }

        bool SyncDatum()
        {
            if (Datum == null || Kalender == null)
                return false;
            if (Kalender.Date != Datum)
                Kalender.Date = Datum;
            return true;
        }

        private DSADateCalendar kalender = new DSADateCalendarTwelve();
        public DSADateCalendar Kalender
        {
            get {
                if (kalender == null)
                    kalender = new DSADateCalendarTwelve();
                return kalender; 
            }
            set
            {
                if (value == null)
                    value = new DSADateCalendarTwelve();
                Set(ref kalender, value);
                Invalidate();
            }
        }

        private DSADateTime datum = new DSADateTime(DateTime.Now);
        public DSADateTime Datum
        {
            get { return datum; }
            set
            {
                Set(ref datum, value);
                Invalidate();
            }
        }

        string text = null;
        public string Text
        {
            get {
                if(text != null)
                    return text;
                if (!SyncDatum())
                    return text = null;
                return text = Kalender.getHeadingText();
            }
        }

        bool? isSpecialDay = null;
        public bool IsSpecialDay
        {
            get
            {
                if (isSpecialDay.HasValue)
                    return isSpecialDay.Value;
                if (!SyncDatum())
                    return false;
                isSpecialDay = Kalender.IsSpecialDay;
                return isSpecialDay.Value;
            }
        }

        int? weekDay = null;
        public int WeekDay
        {
            get
            {
                if (weekDay.HasValue)
                    return weekDay.Value;
                if (!SyncDatum())
                    return 0;
                weekDay = Kalender.WeekDay;
                return weekDay.Value;
            }
        }

        
    }
}
