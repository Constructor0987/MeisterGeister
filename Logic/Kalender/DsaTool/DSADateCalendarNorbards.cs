using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.Logic.Kalender.DsaTool
{
    // based on work from Peter Diefenbach (peter@pdiefenbach.de)
    public class DSADateCalendarNorbards : DSADateCalendar
    {
        //private static Logger logger = Logger.getRootLogger();

        // This is just guessing.
        public const int YEAR_ZERO_BF_IS = 0;
        // This is just guessing.
        public const int YEAR_OFFSET_IN_DAYS = 0;
        public const int DAYS_PER_MONTH = DSADateTime.MOON_MONTH_DAYS;
        public const int MONTHS_PER_YEAR = 100;

        protected override void init()
        {
            Name = "Norbardisch";
            DaysFromYear0ToBF = YEAR_ZERO_BF_IS * DSADateCalendar.DAYS_PER_SUN_YEAR + YEAR_OFFSET_IN_DAYS;
            HasYear0 = false;
            DaysPerYear = DAYS_PER_MONTH * MONTHS_PER_YEAR;
            DaysPerMonth = DAYS_PER_MONTH;
            DaysPerWeek = 0;
        }

        public override String getHeadingText()
        {
            string s = String.Format("{0}. Tag im {1}. Mondmonat im {2}. Uh'Jun", Day, Month, Year);
            return s;
        }

    }

}
