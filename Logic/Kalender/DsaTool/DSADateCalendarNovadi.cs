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
        /** Am 23. Boron 760 BF offenbarte sich Rastullah, das ist Jahr 1 Rastullah. */
        public const int YEAR_ZERO_BF_IS = 760 - 1;
        public const int YEAR_OFFSET_IN_DAYS = 4 * 30 + 23 - 1;
        public const int DAYS_PER_WEEK = 9;
        public const int WEEKS_PER_MONTH = 8;
        public const int DAYS_PER_MONTH = DAYS_PER_WEEK * WEEKS_PER_MONTH + 1;

        protected override void init()
        {
            Name = "Novadisch";
            DaysFromYear0ToBF = YEAR_ZERO_BF_IS * DSADateCalendar.DAYS_PER_SUN_YEAR + YEAR_OFFSET_IN_DAYS;
            HasYear0 = false;
            DaysPerYear = DSADateCalendar.DAYS_PER_SUN_YEAR;
        }

        public void setDayWeekMonthYear(int day, int week, int month, int year)
        {
            month = (int)MathUtil.modulo(month, 5);
            day = (int)MathUtil.modulo(day, 10);
            week = (int)MathUtil.modulo(week, 9);
            int days = month * DAYS_PER_MONTH;
            if (day != 0 && week != 0) //not {month}. Rastullahellah
                days += day + (week - 1) * DAYS_PER_WEEK;
            int daysSince0BF = (year + YEAR_ZERO_BF_IS) * DaysPerYear;
            daysSince0BF += YEAR_OFFSET_IN_DAYS - 1;
            daysSince0BF += days;
            setDaysSinceBF((int)daysSince0BF);
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
                int week = (int)MathUtil.divisio(rastmod, DAYS_PER_WEEK); //+ rastdiv * WEEKS_PER_MONTH;
                s = String.Format("{0}. Tag ({1}) des {2}. Gottesnamens nach dem {3}. Rastullahellah des Jahres {4}", weekday + 1, weekdayNames[weekday], week + 1, MathUtil.modulo(rastdiv + 4, 5) + 1, getYearString());
            }
            return s;
        }

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
