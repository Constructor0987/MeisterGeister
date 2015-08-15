using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.Logic.Kalender.DsaTool
{
    // based on work from Peter Diefenbach (peter@pdiefenbach.de)
    public class DSADateCalendarMyranisch : DSADateCalendar
    {
        //    private static Logger logger = Logger.getRootLogger();
        public const int YEAR_ZERO_BF_IS = 3747;
        public const int DAYS_PER_NONE = 9;
        public const int NONES_PER_OCTADE = 5;
        public const int YEAR_OFFSET_IN_DAYS = 183; // 365 / 2 + 1

        protected override void init()
        {
            Name = "Myranisch";
            DaysFromYear0ToBF = -YEAR_ZERO_BF_IS * DSADateCalendar.DAYS_PER_SUN_YEAR + YEAR_OFFSET_IN_DAYS;
            HasYear0 = true;
            DaysPerYear = DSADateCalendar.DAYS_PER_SUN_YEAR;
            DaysPerWeek = DAYS_PER_NONE;
            DaysPerMonth = DAYS_PER_NONE * NONES_PER_OCTADE;
        }

        public override int Day
        {
            get
            {
                if (IsSpecialDay)
                    return 0;
                int jday = YearDay;
                jday -= getSondertage(jday);
                return (int)MathUtil.modulo(jday, DaysPerMonth, 1);
            }
            set
            {}
        }

        public override int WeekDay
        {
            get
            {
                if (IsSpecialDay)
                    return 0;
                return (int)MathUtil.modulo(Day, DaysPerWeek, 1);
            }
            set {}
        }

        public override IList<string> WeekDayNames
        {
            get { return wochenTage; }
        }

        public override IList<string> MonthNames
        {
            get { return oktaden; }
        }

        /// <summary>
        /// Setzt man day=0 und week=0 bekommt man den Sondertag vor der Oktade.
        /// </summary>
        /// <param name="day">Tag der None</param>
        /// <param name="week">Woche der Oktade</param>
        /// <param name="month">Oktade 1-9, 9 nur wenn day und week 0 sind</param>
        /// <param name="year"></param>
        /// <returns></returns>
        public override int SpecialDaysCount(int day, int week = 0, int month = 0, int year = 0)
        {
            if (day == 1 && week == 0 && month == 0)
                return 1;
            return (month + 1) / 2;
        }

        public override string SpecialDayName
        {
            get
            {
                int jday = YearDay - 1;
                if (jday % 91 == 1)
                    return sondertage[getSondertage(jday)];
                return null;
            }
        }

        public override String getHeadingText()
        {
            //Feiertag, 2 Oktale, Feiertag, 2 Oktale, ...
            // 1, 92, 183, 274, 365 // Mod 91 == 1
            // "Thearchentag", "Nebeltag", "Sonnentag", "Sturmtag", "Frosttag"
            if(IsSpecialDay)
                return String.Format("{0} {1}", SpecialDayName, getYearString());
            return String.Format("{5} ({0}) der {1}. None im {4} ({2}) {3}", WeekDay, Week, Month, getYearString(), MonthName, WeekDayName);
        }

        private int getSondertage(int jday)
        {
            return jday / 91 + ((jday > 1) ? 1 : 0);
        }

        public String getYearString()
        {
            int year = Year;
            string s = String.Format("{0} IZ", year);
            return s;
        }

        public static readonly String[] oktaden = new String[] {
            "Nereton",
            "Siminia",
            "Zatura",
            "Shinxir",
            "Brajan",
            "Raia",
            "Chrysir",
            "Gyldara"
        };


        public static readonly String[] sondertage = new String[] {
            "Thearchentag",
            "Nebeltag",
            "Sonnentag",
            "Sturmtag",
            "Frosttag"
        };

        public static readonly String[] wochenTage = new String[] {
            "Schaffenstag",
            "Ahnentag",
            "Ackertag",
            "Markttag",
            "Opfertag",
            "Rechtstag",
            "Fuhrtag",
            "Streittag",
            "Ruhetag"
        };

    }

}
