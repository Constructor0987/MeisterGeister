using System;
using System.Text;

/**
 * A class representing a single unique day in Aventuria.
 * This representation is independent of any calendar or other time counting practices 
 * and should be used to translate between the different calendars in use in Aventuria.
 * It uses an integer to count the days since 
 * Praios 1st of the year of Bosparans fall (0 BF).
 * <p><b>See:</b> "Das Handbuch für den Reisenden", Section "Der Aventurische Kalender" (p. 60..63)</p>
 *
 * @author Copyright (c) 2009 Peter Diefenbach (peter@pdiefenbach.de)
 */
namespace MeisterGeister.Logic.Kalender.DsaTool
{

    public class DSADate
    {

        private int daysSinceBF = 0;

        /** Creates a date representing Praios 1st, 0 BF. */
        public DSADate()
        {
            daysSinceBF = 0;
        }

        /**
         * Creates a date with the given number of days distance to Praios 1st, 0 BF. 
         */
        public DSADate(int daysSinceBF)
        {
            this.daysSinceBF = daysSinceBF;
        }

        /**
         * Creates an Aventurian date based on the date on earth.
         * @see #setFromEarthDate(Date)
         */
        public DSADate(DateTime earthDate)
        {
            setFromEarthDate(earthDate);
        }

        /**
         * @return the days since Praios 1st, 0 BF
         */
        public int getDaysSinceBF()
        {
            return daysSinceBF;
        }

        /**
         * @param daysSinceBF the days since Praios 1st, 0 BF
         */
        public void setDaysSinceBF(int daysSinceBF)
        {
            this.daysSinceBF = daysSinceBF;
            //setChanged();
        }

        /** The duration between two new moons in days. */
        public const int MOON_MONTH_DAYS = 28;

        /** The duration between two new lunar eclipses in moon cycles. */
        public const int MOON_MONTHS_BETWEEN_LUNAR_ECLIPSES = 240;

        /** The duration between two new lunar eclipses in days. */
        public const int DAYS_BETWEEN_LUNAR_ECLIPSES = MOON_MONTH_DAYS * MOON_MONTHS_BETWEEN_LUNAR_ECLIPSES;

        /**
         * @return the day of the moon phase. 0 represents new moon.
         */
        public int getMoonday()
        {
            const int moonDayAtPraios1st0BF = 16;
            return MathUtil.modulo(daysSinceBF + moonDayAtPraios1st0BF, MOON_MONTH_DAYS);
        }

        /**
         * @return the number of days since the last lunar eclipse. 
         *  0 represents lunar eclipse.
         *  Lunar eclipse always happens two days after new moon, but only rarely.
         */
        public int getDaysSinceLastLunarEclipse()
        {
            const int daysSinceLastLunarEclipseAtPraios1st0BF = (15 - 1) + (24 - 1) * MOON_MONTH_DAYS;
            return MathUtil.modulo(daysSinceBF + daysSinceLastLunarEclipseAtPraios1st0BF, DAYS_BETWEEN_LUNAR_ECLIPSES);
        }

        /** The duration in days of a standard earth year. */
        public const int DAYS_PER_EARTH_YEAR = 365;

        /** The duration in days of half a standard earth year (rounded up). */
        private const int DAYS_PER_EARTH_HALF_YEAR = (DAYS_PER_EARTH_YEAR + 1) / 2;

        /** The julian earth day of February 29 (if it's a leap year). */
        private const int JDAY_FEB29 = 60;

        /** The year Pope Gregor added 10 days to correct the errors piled up by the Julian calendar. */
        private const int GREGORIAN_CORRECTION_YEAR = 1582;

        /** The julian earth day of October 25 (if it's not a leap year). */
        private const int JDAY_OCT25 = 288;

        /**
         * Calculates the date on earth from the DSA date.
         * Calculation is based on the year change method, that means,
         *  January 1st is always equal to Praios 1st.
         * There is no leap year in Aventuria, so 1st of March is the same as Feb 29.
         * Furthermore, before 1996 (or 1019 BF), one earth year was two Aventurian years, 
         *  and since 1996, one earth year is one Aventurian year.
         * @see <a href="http://www.dasschwarzeauge.de/index.php?id=49">Aventurische Zeitrechnung auf dasschwarzeauge.de</a>
         * @see <a href="http://www.wiki-aventurica.de/wiki/Kalender">Zeitrechnung auf wiki-aventurica.de</a>
         */
        public DateTime toEarthDate()
        {
            int daysSinceBF = getDaysSinceBF();
            int avYear = MathUtil.divisio(daysSinceBF, DAYS_PER_EARTH_YEAR);
            int avJday = MathUtil.modulo(daysSinceBF, DAYS_PER_EARTH_YEAR);
            int earthYear;
            int earthJday;
            if (1019 <= avYear)
            { // Since 1996 (or 1019 BF), one earth year is one Aventurian year
                earthYear = 1996 - 1019 + avYear;
                earthJday = avJday;
            }
            else
            {
                earthYear = 1996 - (1019 + 1 - avYear) / 2; //+1 to round correctly
                if (0 == MathUtil.modulo(avYear, 2))
                {
                    // odd avYears come first, so even avYears represent the second half of the earth year
                    earthJday = (avJday + DAYS_PER_EARTH_YEAR) / 2;
                }
                else
                { // Now the easy part, the first half of the earth year
                    earthJday = avJday / 2;
                }
            }

            bool isLeapYear = DateTime.IsLeapYear(earthYear);
            if (isLeapYear && earthJday >= JDAY_FEB29)
            { // Get rid of February 29th.
                earthJday++;
            }

            DateTime earthDate = new DateTime(earthYear, 1, 1, 12, 0, 0);
            earthDate = earthDate.AddDays(earthJday);
            // We choose noon to avoid problems at night when switching summer and winter time
            //logger.debug("Aventurian date " + daysSinceBF + " (Year " + avYear + " JDay " + avJday + ") --> " + earthDate);
            return earthDate;
        }

        /**
         * Calculates the DSA date from the date on earth.
         * Calculation is based on the year change method, that means,
         *  January 1st is always equal to Praios 1st.
         * There is no leap year in Aventuria, so 1st of March is the same as Feb 29.
         * Furthermore, before 1996 (or 1019 BF), one earth year was two aventurian years, 
         *  and since 1996, one earth year is one aventurian year.
         * @see <a href="http://www.dasschwarzeauge.de/index.php?id=49">Aventurische Zeitrechnung auf dasschwarzeauge.de</a>
         * @see <a href="http://www.wiki-aventurica.de/wiki/Kalender">Zeitrechnung auf wiki-aventurica.de</a>
         */
        public void setFromEarthDate(DateTime earthDate)
        {
            int earthYear = earthDate.Year;
            int earthDayOfYear = earthDate.DayOfYear;
            bool isLeapYear = DateTime.IsLeapYear(earthYear);
            int jday = earthDayOfYear - 1; // jday is 0 for January 1st.
            if (isLeapYear && jday >= JDAY_FEB29)
            { // Get rid of February 29th.
                jday--;
            }
            int avYear;
            int avJday;
            if (1996 <= earthYear)
            { // Since 1996 (or 1019 BF), one earth year is one Aventurian year
                avYear = 1019 - 1996 + earthYear;
                avJday = jday;
            }
            else
            { // Before 1996 (or 1019 BF), one earth year was two Aventurian years
                avYear = 1019 - 2 * (1996 - earthYear);
                if (DAYS_PER_EARTH_HALF_YEAR < jday)
                {
                    avYear++;
                    avJday = 2 * (jday - DAYS_PER_EARTH_HALF_YEAR) + 1;
                }
                else
                {
                    avJday = 2 * jday;
                }
                if ((GREGORIAN_CORRECTION_YEAR == earthYear) && (JDAY_OCT25 <= earthDayOfYear))
                {
                    // Correct the 10 days inserted by Pope Gregor
                    avJday += 2 * 10;
                }
            }
            int daysSinceBF = avYear * DAYS_PER_EARTH_YEAR + avJday;
            //logger.debug("Earth date " + earthDate.toString() + " --> " + daysSinceBF + " (Year " + avYear + " JDay " + avJday + ")");
            setDaysSinceBF(daysSinceBF);
        }

        /**
         * @return The actual season, as defined for the calendars based on the twelve gods.
         */
        public Season season()
        {
            int jday = MathUtil.modulo(daysSinceBF, DAYS_PER_EARTH_YEAR);
            Season s = Season.Summer;
            if (jday < 60)
            {
                s = Season.Summer;
            }
            else if (jday < 60 + 90)
            {
                s = Season.Autumn;
            }
            else if (jday < 60 + 90 + 90)
            {
                s = Season.Winter;
            }
            else if (jday < 60 + 90 + 90 + 90)
            {
                s = Season.Spring;
            }
            else
            {
                s = Season.Summer;
            }
            return s;
        }

        /**
         * How many days is this date after the other in the calendar?
         * (The result may get negative if this date is before the other.)
         * @param other the date with which is compared
         */
        public int daysAfter(DSADate other)
        {
            return (daysSinceBF - other.daysSinceBF);
        }

        /**
         * Adds days to the actual date.
         * @param days so many days will go by. Is allowed to be a negative number as well.
         */
        public void addDays(int days)
        {
            daysSinceBF += days;
            //setChanged();
        }

        /**
         * Is this date after the other in the calendar?
         * @param other The date with which is compared.
         */
        public bool isAfter(DSADate other)
        {
            return (daysSinceBF > other.daysSinceBF);
        }

        /**
         * Is this date before the other in the calendar?
         * @param other The date with which is compared.
         */
        public bool isBefore(DSADate other)
        {
            return (daysSinceBF < other.daysSinceBF);
        }

        /**
         * Is this date the same as the other?
         * @param other The date with which is compared.
         */
        public override bool Equals(object other)
        {
            if (other is DSADate)
            {
                DSADate o = (DSADate)other;
                //            logger.debug("Comparing days since BF " + daysSinceBF + " with " + o.daysSinceBF);
                return (daysSinceBF == o.daysSinceBF);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return daysSinceBF;
        }

        public override string ToString()
        {
            return "DSADate(" + daysSinceBF + ")";
        }
    }
}