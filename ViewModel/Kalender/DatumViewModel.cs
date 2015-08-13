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
        public DatumViewModel(DSADateTime datum, DSADateCalendar kalender = null)
        {
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
        }

        private DSADateCalendar kalender = new DSADateCalendarTwelve();
        public DSADateCalendar Kalender
        {
            get { return kalender; }
            set
            {
                if(value == null)
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
                if(Datum == null || Kalender == null)
                    return text = null;
                Kalender.Date = Datum;
                return text = Kalender.getHeadingText();
            }
        }
    }
}
