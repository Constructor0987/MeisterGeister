using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.Logic.Kalender.DsaTool
{
    /**
     * A class representing the calendar or days counting practice in Aventuria
     *  as used by Novadi.
     * <p><b>See:</b> "Das Handbuch für den Reisenden", Section "Der Aventurische Kalender" (p. 60..63)</p>
     *
     * @author Copyright (c) 2009 Peter Diefenbach (peter@pdiefenbach.de)
     */
    public class DSADateCalendarNovadi : DSADateCalendar
    {
        //private static Logger logger = Logger.getRootLogger();

        /** 
         * Creates a calendar showing a date representing Praios 1st, 0 BF.
         * @see DSADateCalendar#DSADateCalendar() 
         */
        public DSADateCalendarNovadi()
            : base()
        {
            init();
        }

        /** 
         * Creates a view following this calendar to the given date.
         * @see DSADateCalendar#DSADateCalendar(DSADate) 
         */
        public DSADateCalendarNovadi(DSADateTime date)
            : base(date)
        {
            init();
        }

        protected void init()
        {
            setName("Novadisch");
            setDaysFromYear0ToBF(YEAR_ZERO_BF_IS * DSADateCalendar.DAYS_PER_YEAR_BF + YEAR_OFFSET_IN_DAYS);
            setHasYear0(false);
            setDaysPerYear(DSADateCalendar.DAYS_PER_YEAR_BF);
        }

        public override String getHeadingText()
        {
            int jday = getJDay() - 1;
            int rastdiv = (int)MathUtil.divisio(jday, DAYS_PER_MONTH);
            int rastmod = (int)MathUtil.modulo(jday, DAYS_PER_MONTH);
            string s = "";
            if (DAYS_PER_MONTH - 1 == rastmod)
            { // Rastullahellah
                s = String.Format("{0}. Rastullahellah {1}", rastdiv + 1, getYearString());
            }
            else
            {
                int weekday = (int)MathUtil.modulo(rastmod, DAYS_PER_WEEK);
                int week = (int)MathUtil.divisio(rastmod, DAYS_PER_WEEK) + rastdiv * WEEKS_PER_MONTH;
                s = String.Format("{0}. Tag ({1}) im {2}. Gottesnamen {3}", weekday + 1, weekdayNames[weekday], week + 1, getYearString());
            }
            return s;
        }

        /** Am 23. Boron 760 BF offenbarte sich Rastullah, das ist Jahr 1 Rastullah. */
        public const int YEAR_ZERO_BF_IS = 760 - 1;

        public const int YEAR_OFFSET_IN_DAYS = 4 * 30 + 23 - 1;

        public const int DAYS_PER_WEEK = 9;

        public const int WEEKS_PER_MONTH = 8;

        public const int DAYS_PER_MONTH = DAYS_PER_WEEK * WEEKS_PER_MONTH + 1;

        public static readonly String[] weekdayNames = new String[] {
            "Hellah",
            "Orhima",
            "Shimja",
            "Rhondara",
            "Heschinja",
            "Dschella",
            "Marhibo",
            "Khabla ",
            "Amm el-Thona",
        };

        public String getYearString()
        {
            return getYearString(getYear(), "Rastullah (n. d. O.)", "vor Rastullahs Erscheinen (v. d. O.)");
        }
    }

}
