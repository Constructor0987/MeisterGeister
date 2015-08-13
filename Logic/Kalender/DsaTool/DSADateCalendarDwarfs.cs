using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.Logic.Kalender.DsaTool
{


    // based on work from Peter Diefenbach (peter@pdiefenbach.de)
    public class DSADateCalendarDwarfs : DSADateCalendar
    {

        //private static Logger logger = Logger.getRootLogger();
        public const int YEAR_ZERO_BF_IS = 0;
        public const int DAYS_PER_MONTH = 30;

        protected override void init()
        {
            Name = "Zwergisch";
            DaysFromYear0ToBF = -YEAR_ZERO_BF_IS * DSADateCalendar.DAYS_PER_SUN_YEAR;
            HasYear0 = true;
            DaysPerYear = DSADateCalendar.DAYS_PER_SUN_YEAR;
            DaysPerMonth = DAYS_PER_MONTH;
            DaysPerWeek = 7;
        }

        public override IList<string> WeekDayNames
        {
            get { return DSADateCalendarTwelve.weekdayNames; }
        }

        public override IList<string> MonthNames
        {
            get { return monthNames; }
        }

        public override String getHeadingText()
        {
            string s = String.Format("{0}. {1} ({2}.) {3}", Day, MonthName, Month, getYearString());
            return s;
        }

        public String getYearString()
        {
            int year = Year;
            string unitName = "Jahre nach der zweiten Dämonenschlacht";
            if (-1 == year)
                unitName = "Jahr vor der zweiten Dämonenschlacht";
            else if (0 == year)
                unitName = "im Jahr der zweiten Dämonenschlacht";
            else if (1 == year)
                unitName = "Jahr nach der zweiten Dämonenschlacht";
            else if (year < 0)
                unitName = "Jahre vor der zweiten Dämonenschlacht";

            string s = String.Format("{0} {1}", (year < 0) ? -year : year, unitName);

            return s;
        }

        public static readonly String[] monthNames = new String[] {
            "Sommermond", 
            "Hitzemond", 
            "Regenmond", 
            "Weinmond", 
            "Nebelmond", 
            "Dunkelmond", 
            "Frostmond", 
            "Neugeburt", 
            "Marktmond", 
            "Saatmond", 
            "Feuer- (oder Feier)mond",
            "Brautmond",
            "Drachentag",
    };
    }

}
