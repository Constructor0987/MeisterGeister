using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.Logic.Kalender.DsaTool
{

    /**
     * A class representing the calendar or days counting practice in Aventuria
     *  as used on the continent Myranor.
     * <p><b>See:</b> "Das Handbuch für den Reisenden", Section "Der Aventurische Kalender" (p. 60..63)</p>
     *
     * @author Copyright (c) 2009 Peter Diefenbach (peter@pdiefenbach.de)
     */
    public class DSADateCalendarMyranisch : DSADateCalendar
    {

        //    private static Logger logger = Logger.getRootLogger();

        /** 
         * Creates a calendar showing a date representing Praios 1st, 0 BF.
         * @see DSADateCalendar#DSADateCalendar() 
         */
        public DSADateCalendarMyranisch()
            : base()
        {
            init();
        }

        /** 
         * Creates a view following this calendar to the given date.
         * @see DSADateCalendar#DSADateCalendar(DSADate) 
         */
        public DSADateCalendarMyranisch(DSADateTime date)
            : base(date)
        {
            init();
        }

        protected void init()
        {
            setName("Myranisch");
            setDaysFromYear0ToBF(-YEAR_ZERO_BF_IS * DSADateCalendar.DAYS_PER_YEAR_BF);
            setHasYear0(true);
            setDaysPerYear(DSADateCalendar.DAYS_PER_YEAR_BF);
        }

        public override String getHeadingText()
        {
            int jday = getJDay() - 1;
            string s = "";
            if (360 <= jday)
                s = String.Format("{0}. Sondertag {1}", jday - 360 + 1, getYearString());
            else
                s = String.Format("{0}. Tag der {1}. None der {2}. Oktade {3}", getDay() + 1, getNone() + 1, getOctade() + 1, getYearString());
            return s;
        }

        public int getDay()
        {
            int jday = getJDay() - 1;
            return getPart(jday, 1, DAYS_PER_NONE);
        }

        public int getNone()
        {
            int jday = getJDay() - 1;
            return getPart(jday, DAYS_PER_NONE, NONES_PER_OCTADE);
        }

        public int getOctade()
        {
            int jday = getJDay() - 1;
            return getPart(jday, DAYS_PER_NONE * NONES_PER_OCTADE, getDaysPerYear());
        }

        public void setDayNoneOctadeYear(int day, int none, int octade, int year)
        {
            int days = (day - 1) + (none - 1) * DAYS_PER_NONE + (octade - 1) * DAYS_PER_NONE * NONES_PER_OCTADE + year * getDaysPerYear();
            days += getDaysFromYear0ToBF();
            setDaysSinceBF(days);
        }

        public String getYearString()
        {
            int year = getYear();
            string s = String.Format("{0} IZ", year);
            return s;
        }

        public int getPart(int value, int divisor, int modulo)
        {
            if (0 == modulo)
            {
                return (int)MathUtil.divisio(value, divisor);
            }
            else
            {
                return (int)MathUtil.divisio(MathUtil.modulo(value, divisor * modulo), divisor);
            }
        }

        public const int YEAR_ZERO_BF_IS = 3747;

        public const int DAYS_PER_NONE = 9;

        public const int NONES_PER_OCTADE = 5;

    }

}
