using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.Logic.Kalender.DsaTool
{
    /**
     * A class representing the calendar used in Thorwal. 
     * <p><b>See:</b> "Das Handbuch für den Reisenden", Section "Der Aventurische Kalender" (p. 60..63)</p>
     *
     * @author Copyright (c) 2009 Peter Diefenbach (peter@pdiefenbach.de)
     */
    public class DSADateCalendarThorwal : DSADateCalendar
    {
        /** Das Jahr 1 JL entspricht 1627 v. BF. */
        public const int YEAR_ZERO_BF_IS = 1627;
        public const int DAYS_PER_MONTH = 30;

        protected void init()
        {
            Name = "Thorwalsch (Jurgas Landung)";
            DaysFromYear0ToBF = -YEAR_ZERO_BF_IS * DSADateCalendar.DAYS_PER_SUN_YEAR;
            HasYear0 = false;
            DaysPerYear = DSADateCalendar.DAYS_PER_SUN_YEAR;
        }

        

        public override String getHeadingText()
        {
            int jday = getJDay() - 1;
            int monthdiv = (int)MathUtil.divisio(jday, DAYS_PER_MONTH);
            int monthmod = (int)MathUtil.modulo(jday, DAYS_PER_MONTH);

            string s = String.Format("{0}. {1} ({2}.) {3}", monthmod + 1, monthNames[monthdiv], monthdiv + 1, getYearString());
            return s;
        }

        public String getYearString()
        {
            return getYearString(getYear(), "JL", "v.JL");
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
