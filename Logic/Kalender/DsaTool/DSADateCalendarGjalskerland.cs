using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.Logic.Kalender.DsaTool
{
    // based on work from Peter Diefenbach (peter@pdiefenbach.de)
    public class DSADateCalendarGjalskerland : DSADateCalendar
    {
        /** Die Jahreszählung erfolgt nach dem ersten Tag der Geburt im Mond des Blutes im Jahr 1 nach der großen Schlacht (gS). 
         * Das Datum entspricht dem 19. Ingerimm 1370 v. BF. */
        public const int YEAR_ZERO_BF_IS = 1370 + 1;
        public const int YEAR_OFFSET_IN_DAYS = 10 * 30 + 19;
        public const int DAYS_PER_MONTH = DSADateTime.MOON_MONTH_DAYS;
        public const int MONTHS_PER_YEAR = 13;
        public const int YEARS_PER_ERA = 364;
        public const int DAYS_PER_WEEK = 7;

        protected override void init()
        {
            Name = "Gjalskerland";
            DaysFromYear0ToBF = -YEAR_ZERO_BF_IS * DSADateCalendar.DAYS_PER_SUN_YEAR + YEAR_OFFSET_IN_DAYS;
            HasYear0 = false;
            DaysPerYear = DAYS_PER_MONTH * MONTHS_PER_YEAR;
            DaysPerMonth = DAYS_PER_MONTH;
            DaysPerEra = YEARS_PER_ERA * DaysPerYear;
            DaysPerWeek = DAYS_PER_WEEK;
        }

        public override IList<string> MonthNames
        {
            get { return monthNames; }
        }

        public override IList<string> WeekNames
        {
            get { return weekNames; }
        }

        public override int Week
        {
            get { return (int)MathUtil.divisio(Day, DaysPerWeek) + 1; }
            set { }
        }

        public override int WeekDay
        {
            get
            {
                return (int)MathUtil.modulo(Day, DaysPerWeek, 1);
            }
            set
            {
                base.WeekDay = value;
            }
        }

        /// <summary>
        /// Das Jahr für den Gjalsker Kalender ohne Abzug der Ära
        /// </summary>
        public override int Year
        {
            get
            {
                int year = DaysSinceYear0;
                year = (int)MathUtil.divisio(year, DaysPerYear);
                if (!HasYear0)
                {
                    if (0 >= year)
                    { // If there is no year zero, we start counting at year 1.
                        year++;
                    }
                }
                return year;
            }
            set
            {
                if (!HasYear0 && value == 0)
                    value = 1;
                int yearDiff = Year;
                if (!HasYear0 && Math.Sign(yearDiff) != Math.Sign(value))
                    yearDiff--;
                if (yearDiff != 0)
                    DaysSinceYear0 += yearDiff * DaysPerYear;
            }
        }

        public override string getHeadingText()
        {
            string s = String.Format("{0}. Tag der Geburt im {1} ({2}) {3}", Day, MonthName, Month, getYearString());
            return s;
        }

        public String getYearString()
        {
            return getYearString(Year, "gS", "v.gS");
        }

        public static readonly String[] monthNames = new String[] {
            "Mond des Blutes", 
            "Siegesmond", 
            "Trauermond", 
            "Mond des Bundes", 
            "Mond des Schaffens", 
            "Mond des Sturms", 
            "Mond der Stille", 
            "Mond des Hungers", 
            "Mond der Jagd", 
            "Göttermond", 
            "Mond des Odûn", 
            "Mond der Niederkunft", 
            "Ahnenmond",     
        };

        public static readonly String[] weekNames = new String[] {
            "Geburt", 
            "Blüte", 
            "Reife", 
            "Welke", 
        };
    }

}
