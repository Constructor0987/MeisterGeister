using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.Logic.Kalender.DsaTool
{

    /**
     * A class representing the calendar or days counting practice in Aventuria
     *  as used by Orcs.
     * <p><b>See:</b> "Das Handbuch für den Reisenden", Section "Der Aventurische Kalender" (p. 60..63)</p>
     *
     * @author Copyright (c) 2009 Peter Diefenbach (peter@pdiefenbach.de)
     */
    public class DSADateCalendarOrcs : DSADateCalendar
    {
        //private static Logger logger = Logger.getRootLogger();
        /** 18. Efferd 1029 BF = erster Tag des Großen Jahres 2001 */
        public const int REFERENCE_DAY_YEAR_ZERO_BF_IS = 1029;
        public const int REFERENCE_DAY_YEAR_OFFSET_IN_DAYS = 2 * 30 + 18 - 1;
        public const int REFERENCE_YEAR = 2001;
        public const int MONTHS_PER_YEAR = DSADateTime.MOON_MONTHS_BETWEEN_LUNAR_ECLIPSES;
        public const int DAYS_PER_MONTH = DSADateTime.MOON_MONTH_DAYS;
        public const int DAYS_PER_YEAR = DSADateTime.DAYS_BETWEEN_LUNAR_ECLIPSES;

        protected override void init()
        {
            Name = "Orkisch";
            int daysPerYear = DAYS_PER_YEAR;
            int offset = REFERENCE_DAY_YEAR_ZERO_BF_IS * DSADateCalendar.DAYS_PER_SUN_YEAR + REFERENCE_DAY_YEAR_OFFSET_IN_DAYS - daysPerYear * REFERENCE_YEAR;
            //logger.debug("Orc calendar offset is " + offset);
            DaysFromYear0ToBF = offset;
            HasYear0 = true;
            DaysPerYear = daysPerYear;
        }

        public override String getHeadingText()
        {
            int jday = getJDay() - 1;
            int monthdiv = (int)MathUtil.divisio(jday, DAYS_PER_MONTH);
            int monthmod = (int)MathUtil.modulo(jday, DAYS_PER_MONTH);

            string s = String.Format("{0}. Tag im {1}. Mond ({2}. Tag) im {3}", monthmod + 1, monthdiv + 1, Date.getDaysSinceLastLunarEclipse() + 1, getYearString());
            return s;
        }



        public String getYearString()
        {
            string s = String.Format("{0}. Jahr des Tairach", getYear());
            return s;
        }
    }

}
