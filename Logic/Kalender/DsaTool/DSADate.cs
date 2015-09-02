using System;
using System.Runtime.Serialization;
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

    public class DSADateTime : IComparable<DSADateTime>, IEquatable<DSADateTime>, ISerializable, IFormattable
    {
        public const long TICKS_PER_DAY = TimeSpan.TicksPerDay;
        public const int DayMaxValue = (int)(Int64.MaxValue / TICKS_PER_DAY);
        public const int DayMinValue = (int)(Int64.MinValue / TICKS_PER_DAY);
        /// <summary>
        /// Die Dauer zwischen zwei Neumonden in Tagen.
        /// </summary>
        public const int MOON_MONTH_DAYS = 28;
        /// <summary>
        /// Die Dauer zwischen zwei two Neumondfinsternissen in Mondzyklen.
        /// </summary>
        public const int MOON_MONTHS_BETWEEN_LUNAR_ECLIPSES = 240;
        /// <summary>
        /// Die Dauer zwischen zwei two Neumondfinsternissen in Tagen.
        /// </summary>
        public const int DAYS_BETWEEN_LUNAR_ECLIPSES = MOON_MONTH_DAYS * MOON_MONTHS_BETWEEN_LUNAR_ECLIPSES;

        private long ticksSinceBF = 0;

        /// <summary>
        /// Erstellt ein Datum, welches 1. Praios, 0 BF, 00:00 repräsentiert. 
        /// </summary>
        public DSADateTime()
        {
            ticksSinceBF = 0;
        }

        /// <summary>
        /// Erstellt ein Datum, welches 1. Praios, 0 BF, 00:00 plus die Anzahl an Ticks repräsentiert.
        /// </summary>
        public DSADateTime(long ticksSinceBF)
        {
            this.ticksSinceBF = ticksSinceBF;
        }

        /// <summary>
        /// Erstellt ein Datum anhand eines Erdendatums.
        /// </summary>
        /// <param name="earthDate"></param>
        public DSADateTime(DateTime earthDate)
        {
            setFromEarthDate(earthDate);
        }

        /// <returns>the ticks since Praios 1st, 0 BF</returns>
        public long getTicksSinceBF()
        {
            return ticksSinceBF;
        }

        /// <returns>the days since Praios 1st, 0 BF</returns>
        public long getDaysSinceBF()
        {
            return ticksSinceBF / TICKS_PER_DAY;
        }

        public long DaysSinceBF
        {
            get { return getDaysSinceBF(); }
            set { setDaysSinceBF(value); }
        }

        /// <param name="ticksSinceBF">the ticks since Praios 1st, 0 BF</param>
        public void setTicksSinceBF(long ticksSinceBF)
        {
            this.ticksSinceBF = ticksSinceBF;
            //setChanged();
        }

        /// <param name="daysSinceBF">the ticks since Praios 1st, 0 BF</param>
        public void setDaysSinceBF(long daysSinceBF)
        {
            this.ticksSinceBF = daysSinceBF * TICKS_PER_DAY + this.ticksSinceBF % TICKS_PER_DAY;
            //setChanged();
        }

        /// <returns>The time part in a TimeSpan</returns>
        public TimeSpan getTime()
        {
            return new TimeSpan(ticksSinceBF % TICKS_PER_DAY);
        }

        /// <returns>The time part in a TimeSpan</returns>
        public void setTime(TimeSpan time)
        {
            ticksSinceBF = ticksSinceBF / TICKS_PER_DAY + time.Ticks;
        }

        /// <returns>the day of the moon phase. 0 represents new moon.</returns>
        public int getMoonday()
        {
            const int moonDayAtPraios1st0BF = 16;
            return (int)MathUtil.modulo(getDaysSinceBF() + moonDayAtPraios1st0BF, MOON_MONTH_DAYS);
        }

        /**
         * @return the number of days since the last lunar eclipse. 
         *  0 represents lunar eclipse.
         *  Lunar eclipse always happens two days after new moon, but only rarely.
         */
        public int getDaysSinceLastLunarEclipse()
        {
            const int daysSinceLastLunarEclipseAtPraios1st0BF = (15 - 1) + (24 - 1) * MOON_MONTH_DAYS;
            return (int)MathUtil.modulo(getDaysSinceBF() + daysSinceLastLunarEclipseAtPraios1st0BF, DAYS_BETWEEN_LUNAR_ECLIPSES);
        }

        /// <summary>
        /// Die Jahreszeit auf der Nordhalbkugel.
        /// </summary>
        /// <returns></returns>
        public Season season()
        {
            int jday = (int)MathUtil.modulo(getDaysSinceBF(), DAYS_PER_EARTH_YEAR);
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

        #region Earth Date
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
            long daysSinceBF = getDaysSinceBF();
            long avYear = MathUtil.divisio(daysSinceBF, DAYS_PER_EARTH_YEAR);
            long avJday = MathUtil.modulo(daysSinceBF, DAYS_PER_EARTH_YEAR);
            long earthYear;
            long earthJday;
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

            bool isLeapYear = DateTime.IsLeapYear((int)earthYear);
            if (isLeapYear && earthJday >= JDAY_FEB29)
            { // Get rid of February 29th.
                earthJday++;
            }

            DateTime earthDate = new DateTime((int)earthYear, 1, 1, 0, 0, 0) + getTime();
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
            long ticksSinceBF = daysSinceBF + earthDate.TimeOfDay.Ticks;
            //logger.debug("Earth date " + earthDate.toString() + " --> " + daysSinceBF + " (Year " + avYear + " JDay " + avJday + ")");
            setTicksSinceBF(ticksSinceBF);
        }
        #endregion

        /**
         * How many days is this date after the other in the calendar?
         * (The result may get negative if this date is before the other.)
         * @param other the date with which is compared
         */
        public long daysAfter(DSADateTime other)
        {
            return (ticksSinceBF - other.ticksSinceBF);
        }

        /**
         * Adds days to the actual date.
         * @param days so many days will go by. Is allowed to be a negative number as well.
         */
        public void addDays(int days)
        {
            ticksSinceBF += days * TICKS_PER_DAY;
            //setChanged();
        }

        /**
         * Is this date after the other in the calendar?
         * @param other The date with which is compared.
         */
        public bool isAfter(DSADateTime other)
        {
            if (other == null)
                return true;
            return (ticksSinceBF > other.ticksSinceBF);
        }

        /**
         * Is this date before the other in the calendar?
         * @param other The date with which is compared.
         */
        public bool isBefore(DSADateTime other)
        {
            if (other == null)
                return false;
            return (ticksSinceBF < other.ticksSinceBF);
        }

        #region Interfaces
        public override int GetHashCode()
        {
            return ((Int64)ticksSinceBF).GetHashCode();
        }

        public int CompareTo(DSADateTime other)
        {
            if (isAfter(other))
                return -1;
            if (Equals(other))
                return 0;
            return 1;
        }

        public bool Equals(DSADateTime other)
        {
            if (other == null)
                return false;
            return (ticksSinceBF == other.ticksSinceBF);
        }

        public override bool Equals(object other)
        {
            if (other == null)
                return false;
            var o = other as DSADateTime;
            if (o == null)
                return false;
            return (ticksSinceBF == o.ticksSinceBF);
        }

        public override string ToString()
        {
            return this.ToString(null, System.Globalization.CultureInfo.CurrentCulture);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if(String.IsNullOrEmpty(format) || format == "G")
            {
                return "DSADate(" + ticksSinceBF + ")";
            }
            return ticksSinceBF.ToString(format, formatProvider);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException("info");
            info.AddValue("DaysSinceBF", DaysSinceBF);
        }

        protected DSADateTime(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException("info");
            DaysSinceBF = info.GetInt64("DaysSinceBF");
        }

        public static bool operator ==(DSADateTime emp1, DSADateTime emp2)
        {
            if (object.ReferenceEquals(emp1, emp2)) return true;
            if (object.ReferenceEquals(emp1, null)) return false;
            if (object.ReferenceEquals(emp2, null)) return false;

            return emp1.Equals(emp2);
        }

        public static bool operator !=(DSADateTime emp1, DSADateTime emp2)
        {
            if (object.ReferenceEquals(emp1, emp2)) return false;
            if (object.ReferenceEquals(emp1, null)) return true;
            if (object.ReferenceEquals(emp2, null)) return true;

            return !emp1.Equals(emp2);
        }
        #endregion
    }
}