using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.Logic.Kalender.DsaTool
{
    // based on work from Peter Diefenbach (peter@pdiefenbach.de)
    public class DSADateCalendarThorwal : DSADateCalendar
    {
        /** Das Jahr 1 JL entspricht 1627 v. BF. */
        public const int YEAR_ZERO_BF_IS = 1627 + 1;
        public const int DAYS_PER_MONTH = 30;

        protected override void init()
        {
            Name = "Thorwalsch (Jurgas Landung)";
            DaysFromYear0ToBF = -YEAR_ZERO_BF_IS * DSADateCalendar.DAYS_PER_SUN_YEAR;
            HasYear0 = false;
            DaysPerYear = DSADateCalendar.DAYS_PER_SUN_YEAR;
        }

        public override IList<string> MonthNames
        {
            get { return monthNames; }
        }


        public override String getHeadingText()
        {
            int jday = YearDay - 1;
            int monthdiv = (int)MathUtil.divisio(jday, DAYS_PER_MONTH);
            int monthmod = (int)MathUtil.modulo(jday, DAYS_PER_MONTH);

            string s = String.Format("{0}. {1} ({2}.) {3}", Day, MonthName, Month , getYearString());
            return s;
        }

        public String getYearString()
        {
            return getYearString(Year, "JL", "v.JL");
        }

        public static readonly String[] monthNames = new String[] {
            "Midsonnmond", 
            "Kornmond", 
            "Heimamond", 
            "Schlachtmond", 
            "Sturmmond", 
            "Frostmond", 
            "Grimfrostmond", 
            "Goimond", 
            "Friskenmond", 
            "Eimond", 
            "Faramond", 
            "Vinmond",
            "Hranngar-Tag",
        };
    }

}
