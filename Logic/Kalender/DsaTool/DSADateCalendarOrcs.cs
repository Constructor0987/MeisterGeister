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

        /** 
         * Creates a calendar showing a date representing Praios 1st, 0 BF.
         * @see DSADateCalendar#DSADateCalendar() 
         */
        public DSADateCalendarOrcs()
            : base()
        {
            init();
        }

        /** 
         * Creates a view following this calendar to the given date.
         * @see DSADateCalendar#DSADateCalendar(DSADate) 
         */
        public DSADateCalendarOrcs(DSADateTime date)
            : base(date)
        {
            init();
        }

        protected void init()
        {
            setName("Orkisch");
            int daysPerYear = DSADateTime.DAYS_BETWEEN_LUNAR_ECLIPSES;
            int offset = REFERENCE_DAY_YEAR_ZERO_BF_IS * DSADateCalendar.DAYS_PER_YEAR_BF + REFERENCE_DAY_YEAR_OFFSET_IN_DAYS - daysPerYear * REFERENCE_YEAR;
            //logger.debug("Orc calendar offset is " + offset);
            setDaysFromYear0ToBF(offset);
            setHasYear0(true);
            setDaysPerYear(daysPerYear);
        }

        public override String getHeadingText()
        {
            int jday = getJDay() - 1;
            int monthdiv = (int)MathUtil.divisio(jday, DAYS_PER_MONTH);
            int monthmod = (int)MathUtil.modulo(jday, DAYS_PER_MONTH);

            string s = String.Format("{0}. Tag im {1}. Mond ({2}. Tag) im {3}", monthmod + 1, monthdiv + 1, getDSADate().getDaysSinceLastLunarEclipse() + 1, getYearString());
            return s;
        }

        /** 18. Efferd 1029 BF = erster Tag des Großen Jahres 2001 */
        public const int REFERENCE_DAY_YEAR_ZERO_BF_IS = 1029;

        public const int REFERENCE_DAY_YEAR_OFFSET_IN_DAYS = 2 * 30 + 18 - 1;

        public const int REFERENCE_YEAR = 2001;

        public const int MONTHS_PER_YEAR = DSADateTime.MOON_MONTHS_BETWEEN_LUNAR_ECLIPSES;

        public const int DAYS_PER_MONTH = DSADateTime.MOON_MONTH_DAYS;

        public String getYearString()
        {
            string s = String.Format("{0}. Jahr des Tairach", getYear());
            return s;
        }
    }

}
