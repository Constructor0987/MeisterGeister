using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.Logic.Kalender.DsaTool
{
    // based on work from Peter Diefenbach (peter@pdiefenbach.de)
    public class DSADateCalendarNovadi : DSADateCalendar
    {
        //private static Logger logger = Logger.getRootLogger();
        /** Am 23. Boron 760 BF offenbarte sich Rastullah, das ist Jahr 1 Rastullah. */
        public const int YEAR_ZERO_BF_IS = -760 + 1;
        public const int YEAR_OFFSET_IN_DAYS = (5 - 1) * 30 + 23 - 1;
        public const int DAYS_PER_WEEK = 9;
        public const int WEEKS_PER_MONTH = 8;
        public const int DAYS_PER_MONTH = DAYS_PER_WEEK * WEEKS_PER_MONTH + 1;

        protected override void init()
        {
            Name = "Novadisch";
            DaysFromYear0ToBF = -YEAR_ZERO_BF_IS * DSADateCalendar.DAYS_PER_SUN_YEAR + YEAR_OFFSET_IN_DAYS;
            HasYear0 = false;
            DaysPerYear = DSADateCalendar.DAYS_PER_SUN_YEAR;
            DaysPerMonth = DAYS_PER_MONTH - 1;
            DaysPerWeek = DAYS_PER_WEEK;
        }

        public override IList<string> WeekDayNames
        {
            get { return weekdayNames; }
        }

        public override IList<string> MonthNames
        {
            get { return monthNames; }
        }

        public override int Day
        {
            get
            {
                if (IsSpecialDay)
                    return 0;
                return (int)MathUtil.modulo(YearDay, DaysPerMonth + 1, 1); 
            }
            set { }
        }

        public override int WeekDay
        {
            get { return (int)MathUtil.modulo(Day, DaysPerWeek, 1); }
            set { }
        }

        public override int SpecialDaysCount(int day, int week = 0, int month = 0, int year = 0)
        {
            if (month > 1)
                return month - 1;
            return 0;
        }

        public override string SpecialDayName
        {
            get
            {
                int jday = YearDay - 1;
                int rastdiv = (int)MathUtil.divisio(jday, DAYS_PER_MONTH);
                int rastmod = (int)MathUtil.modulo(jday, DAYS_PER_MONTH);
                if (DAYS_PER_MONTH - 1 == rastmod)
                { // Rastullahellah
                    return String.Format("{0}. Rastullahellah", rastdiv + 1);
                }
                return null;
            }
        }

        public override String getHeadingText()
        {
            if(IsSpecialDay)
                return String.Format("{0} {1}", SpecialDayName, getYearString());
            return String.Format("{0}. Tag ({1}) des {2}. Gottesnamens {3} des Jahres {4}", WeekDay, WeekDayName, Week, MonthName, getYearString());
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

        public static readonly String[] monthNames = new String[] {
            "nach dem 5. Rastullahellah",
            "nach dem 1. Rastullahellah",
            "nach dem 2. Rastullahellah",
            "nach dem 3. Rastullahellah",
            "nach dem 4. Rastullahellah",
        };

        public String getYearString()
        {
            return getYearString(Year, "Rastullah (n. d. O.)", "vor Rastullahs Erscheinen (v. d. O.)");
        }
    }

}
