using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeisterGeister.Logic.Kalender;

namespace MeisterGeister.Logic.Kalender.DsaTool
{


    /**
     * A base class for all calendars or other days counting practices in Aventuria.
     * We assume that all days counting practices have the concept of a year -
     * that is, the counting repeats after roughly the same timeframe each time.
     * This basic implementation sets the duration of a year to 365 days 
     * and starts counting at 1st of Praios 0 BF - 
     * so it really is a calendar for Bosparans fall, but without the knowledge of months,
     * as we can't assume this concept to be valid for all subclasses.
     * <p><b>See:</b> "Das Handbuch für den Reisenden", Section "Der Aventurische Kalender" (p. 60..63)</p>
     *
     * @author Copyright (c) 2009 Peter Diefenbach (peter@pdiefenbach.de)
     */
    public class DSADateCalendar : IComparable<DSADateTime>, IComparable<DSADateCalendar>, IEquatable<DSADateTime>, IFormattable
    {

        //private static Logger logger = Logger.getRootLogger();

        /** The number of days the year has for the calendar Bosparans fall. */
        public const int DAYS_PER_SUN_YEAR = 365;

        /** The calendar Bosparans fall has a year zero. */
        public const bool HAS_YEAR_ZERO_BF = true;

        private DSADateTime date;
        /// <summary>
        /// Aktuelles Datum
        /// </summary>
        public DSADateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        private int daysFromYear0ToBF = 0;
        /// <summary>
        /// Die Tage vom ersten Tags des Jahr 0 des Kalenders bis zum 1. Praios 0 BF.
        /// </summary>
        public int DaysFromYear0ToBF
        {
            get { return daysFromYear0ToBF; }
            set { daysFromYear0ToBF = value; }
        }

        private int daysPerYear = DAYS_PER_SUN_YEAR;
        /// <summary>
        /// Tage pro Jahr
        /// </summary>
        public int DaysPerYear
        {
            get { return daysPerYear; }
            protected set { daysPerYear = value; }
        }

        private bool _hasYear0 = HAS_YEAR_ZERO_BF;
        /// <summary>
        /// Der Kalender hat ein Jahr 0.
        /// </summary>
        public bool HasYear0
        {
            get { return _hasYear0; }
            protected set { _hasYear0 = value; }
        }

        private String name = "Basiskalender";
        /// <summary>
        /// Name des Kalenders
        /// </summary>
        public String Name
        {
            get { return name; }
            protected set { name = value; }
        }


        /// <summary>
        /// Erstellt einen neuen Kalender mit dem Datum 1. Praios, 0 BF 0:00 Uhr.
        /// </summary>
        public DSADateCalendar()
        {
            init();
            date = new DSADateTime();
        }

        /// <summary>
        /// Erstellt einen neuen Kalender mit dem angegebenen Datum.
        /// </summary>
        /// <param name="date"></param>
        public DSADateCalendar(DSADateTime date)
        {
            init();
            this.date = date;
        }

        /// <summary>
        /// Initialisierung der kalenderspezifischen Werte
        /// </summary>
        protected virtual void init()
        {
        }

        /**
         * @return the days since Praios 1st, 0 BF.
         * @see DSADate#getDaysSinceBF()
         */
        public int getDaysSinceBF()
        {
            return (int)date.getDaysSinceBF();
        }

        /**
         * Sets the days since Praios 1st, 0 BF.
         * @see DSADate#getDaysSinceBF()
         */
        public void setDaysSinceBF(int daysSinceBF)
        {
            date.setDaysSinceBF(daysSinceBF);
        }

        /**
         * @return the day of the year (julian day) for Bosparans fall calendar, ranges from 1 to {@link #DAYS_PER_YEAR_BF}.
         */
        public int getJDayBF()
        {
            return (int)MathUtil.modulo(getDaysSinceBF(), DAYS_PER_SUN_YEAR) + 1;
        }

        /**
         * @return the year of the Bosparans fall calendar.
         */
        public int getYearBF()
        {
            return (int)MathUtil.divisio(getDaysSinceBF(), DAYS_PER_SUN_YEAR);
        }

        /**
         * Sets the date according to Bosparans fall.
         * @param jday The day of the year (julian day), ranges from 1 to {@link #getDaysPerYear()}.
         * @param year The year (according to BF).
         * @exception DSAException may be thrown, depending on implementation of subclass. The default implementation does not throw anything.
         */
        public void setJDayYearBF(int jday, int year)
        {
            //        if ((jday < 1) || (jday > daysPerYear())) {
            //             throw new DSAException("jday out of range");
            //        }
            setDaysSinceBF((year * DAYS_PER_SUN_YEAR) + (jday - 1));
        }

        /**
         * @return the days since the beginning of year 0 of this calendar.
         */
        public int getDaysSinceYear0()
        {
            return getDaysSinceBF() - daysFromYear0ToBF;
        }

        /**
         * Sets the days since the beginning of year 0 of this calendar.
         */
        public void setDaysSinceYear0(int daysSinceYear0)
        {
            setDaysSinceBF(daysSinceYear0 + daysFromYear0ToBF);
        }

        /**
         * @return The day of the year (julian day) for this calendar, ranges from 1 to {@link #getDaysPerYear()}.
         */
        public int getJDay()
        {
            return (int)MathUtil.modulo(getDaysSinceYear0(), DaysPerYear) + 1;
        }



        /**
         * @return The year of this calendar.
         */
        public int getYear()
        {
            int year = (int)MathUtil.divisio(getDaysSinceYear0(), DaysPerYear);
            if (!HasYear0)
            {
                if (0 >= year)
                { // If there is no year zero, we start counting at year 1.
                    year--;
                }
            }
            return year;
        }

        /**
         * Sets the date.
         * @param jday The day of the year (julian day), ranges from 1 to {@link #getDaysPerYear()}.
         * @param year The year (according to BF).
         * @exception DSAException may be thrown, depending on implementation of subclass. The default implementation does not throw anything.
         */
        public void setJDayYear(int jday, int year)
        {
            //        if ((jday < 1) || (jday > daysPerYear())) {
            //             throw new DSAException("jday out of range");
            //        }
            if (!HasYear0)
            {
                if (0 == year)
                {
                    throw new ArgumentException(String.Format("Der Kalender hat kein Jahr 0: {0}", Name));
                }
                else if (0 > year)
                { // If there is no year zero, we start counting at year 1.
                    year++;
                }
            }
            int daysSinceYear0 = (year * DaysPerYear) + (jday - 1);
            setDaysSinceYear0(daysSinceYear0);
        }

        public String getYearString(int year, String positiveUnitName, String negativeUnitName)
        {
            string s = String.Format("{0} {1}", (year < 0)? -year: year, (year < 0) ? negativeUnitName : positiveUnitName);
            return s;
        }

        /**
         * @return the day of the moon.
         * @see DSADate#getMoonday()
         */
        public int getMoonday()
        {
            return date.getMoonday();
        }

        /**
         * @return the current season.
         * @see DSADate#season()
         */
        public Season season()
        {
            return date.season();
        }

        public virtual String getHeadingText()
        {
            return "Year " + getYear() + " JDay " + getJDay() + " BF";
        }

        public virtual String getContentText()
        {
            string s = String.Format("{0}: {1}: {2}", getHeadingText(), "Mondphase", Datum.MondphaseString(getMoonday() + 1));
            return s;
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (String.IsNullOrEmpty(format) || format == "G")
            {
                return "DSADateCalendar " + getDaysSinceBF() + " (Year " + getYearBF() + " JDay " + getJDayBF() + " BF)";
            }
            return "DSADateCalendar " + getDaysSinceBF() + " (Year " + getYearBF() + " JDay " + getJDayBF() + " BF)";
        }

        #region Interfaces
        public override string ToString()
        {
            return this.ToString(null, System.Globalization.CultureInfo.CurrentCulture);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public int CompareTo(DSADateTime other)
        {
            return Date.CompareTo(other);
        }

        public int CompareTo(DSADateCalendar other)
        {
            return Date.CompareTo(other.Date);
        }

        public bool Equals(DSADateTime other)
        {
            return Date.Equals(other);
        }

        public static bool operator ==(DSADateCalendar emp1, DSADateTime emp2)
        {
            if (object.ReferenceEquals(emp1, null)) return false;
            return emp1.Equals(emp2);
        }

        public static bool operator !=(DSADateCalendar emp1, DSADateTime emp2)
        {
            if (object.ReferenceEquals(emp1, null)) return true;
            return !emp1.Equals(emp2);
        }
        #endregion
    }

}
