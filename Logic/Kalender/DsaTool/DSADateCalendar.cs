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
    public class DSADateCalendar
    {

        //private static Logger logger = Logger.getRootLogger();

        /** The number of days the year has for the calendar Bosparans fall. */
        public const int DAYS_PER_YEAR_BF = 365;

        /** The calendar Bosparans fall has a year zero. */
        public const bool HAS_YEAR_ZERO_BF = true;

        /** The date this calendar knows as current. */
        private DSADate date;

        private int daysFromYear0ToBF = 0;

        private int daysPerYear = DAYS_PER_YEAR_BF;

        private bool _hasYear0 = HAS_YEAR_ZERO_BF;

        private String name = "Basiskalender";

        /** 
         * Creates a date representing Praios 1st, 0 BF.
         * @see DSADate#DSADate() 
         */
        public DSADateCalendar()
        {
            date = new DSADate();
        }

        /** 
         * Creates a view following this calendar to the given date.
         */
        public DSADateCalendar(DSADate date)
        {
            this.date = date;
        }

        /**
         * @return the {@link DSADate} that is represented using this calendar.
         */
        public DSADate getDSADate()
        {
            return date;
        }

        /**
         * Sets another {@link DSADate} to be represented using this calendar.
         */
        public void setDSADate(DSADate date)
        {
            this.date = date;
        }

        /** @return the name of this calendar. */
        public virtual String getName()
        {
            return name;
        }

        /** Sets the name of this calendar. */
        public void setName(String name)
        {
            this.name = name;
        }

        /** @return the number of days a year in this calendar has. */
        public int getDaysPerYear()
        {
            return daysPerYear;
        }

        /** Sets the number of days a year in this calendar has. */
        public void setDaysPerYear(int daysPerYear)
        {
            this.daysPerYear = daysPerYear;
        }

        /** @return <code>true</code> if this calender knows a year 0 (or <code>false</code> if year -1 follows year +1). */
        public bool hasYear0()
        {
            return _hasYear0;
        }

        /** Sets if this calender knows a year 0 (or if otherwise to year -1 follows year +1). */
        public void setHasYear0(bool hasYear0)
        {
            _hasYear0 = hasYear0;
        }

        /** @return the offset in days between BF calendar and this calendar. */
        public int getDaysFromYear0ToBF()
        {
            return daysFromYear0ToBF;
        }

        /** Sets the offset in days between BF calendar and this calendar. */
        public void setDaysFromYear0ToBF(int daysFromYear0ToBF)
        {
            this.daysFromYear0ToBF = daysFromYear0ToBF;
        }

        /**
         * @return the days since Praios 1st, 0 BF.
         * @see DSADate#getDaysSinceBF()
         */
        public int getDaysSinceBF()
        {
            return date.getDaysSinceBF();
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
            return MathUtil.modulo(getDaysSinceBF(), DAYS_PER_YEAR_BF) + 1;
        }

        /**
         * @return the year of the Bosparans fall calendar.
         */
        public int getYearBF()
        {
            return MathUtil.divisio(getDaysSinceBF(), DAYS_PER_YEAR_BF);
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
            setDaysSinceBF((year * DAYS_PER_YEAR_BF) + (jday - 1));
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
            return MathUtil.modulo(getDaysSinceYear0(), getDaysPerYear()) + 1;
        }

        /**
         * @return The year of this calendar.
         */
        public int getYear()
        {
            int year = MathUtil.divisio(getDaysSinceYear0(), getDaysPerYear());
            if (!hasYear0())
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
            if (!hasYear0())
            {
                if (0 == year)
                {
                    throw new ArgumentException(String.Format("There is no year zero in this calendar: {0}", getName()));
                }
                else if (0 > year)
                { // If there is no year zero, we start counting at year 1.
                    year++;
                }
            }
            int daysSinceYear0 = (year * getDaysPerYear()) + (jday - 1);
            setDaysSinceYear0(daysSinceYear0);
        }

        public String getYearString(int year, String positiveUnitName, String negativeUnitName)
        {
            string s = String.Format("{0:5} {1}", (year < 0)? -year: year, (year < 0) ? negativeUnitName : positiveUnitName);
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

        public class FieldInfo
        {
            public String name;
            public int minimumValue;
            public int maximumValue;
        }

        public override string ToString()
        {
            return "DSADateCalendar " + getDaysSinceBF() + " (Year " + getYearBF() + " JDay " + getJDayBF() + " BF)";
        }
    }

}
