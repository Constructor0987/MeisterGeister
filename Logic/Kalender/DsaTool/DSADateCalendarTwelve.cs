using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeisterGeister.Logic.Kalender;

namespace MeisterGeister.Logic.Kalender.DsaTool
{
    
/**
 * A class representing all calendars or days counting practices in Aventuria 
 *  which are based on the twelve gods.
 * <p><b>See:</b> "Das Handbuch für den Reisenden", Section "Der Aventurische Kalender" (p. 60..63)</p>
 *
 * @author Copyright (c) 2009 Peter Diefenbach (peter@pdiefenbach.de)
 */
public class DSADateCalendarTwelve : DSADateCalendar {
    //private static Logger logger = Logger.getRootLogger();

    /** 
     * Creates a calendar based on Bosparans fall, showing a date representing Praios 1st, 0 BF.
     * @see DSADateCalendar#DSADateCalendar() 
     */
    public DSADateCalendarTwelve() : base() {
        init(MeisterGeister.Logic.Kalender.Kalender.BosparansFall);
    }
    
    /** 
     * Creates a view following Bosparans fall calendar to the given date.
     * @see DSADateCalendar#DSADateCalendar(DSADate) 
     */
    public DSADateCalendarTwelve(DSADate date) : base(date) {
        init(MeisterGeister.Logic.Kalender.Kalender.BosparansFall);
    }

    /** 
     * Creates a calendar based on the given calendarID, showing a date representing Praios 1st, 0 BF.
     * @see DSADateCalendar#DSADateCalendar() 
     */
    public DSADateCalendarTwelve(MeisterGeister.Logic.Kalender.Kalender kal) : base() {
        init(kal);
    }
    
    /** 
     * Creates a view following the given calendar to the given date.
     * @see DSADateCalendar#DSADateCalendar(DSADate) 
     */
    public DSADateCalendarTwelve(DSADate date, MeisterGeister.Logic.Kalender.Kalender kal)
        : base(date)
    {
        init(kal);
    }

    protected void init(MeisterGeister.Logic.Kalender.Kalender kal)
    {
        this.calendar = Zeitrechnung.ZeitrechnungenDictionary[kal];

        setName(calendar.Name);
        //-calendar.getYearZeroBFis() * DSADateCalendar.DAYS_PER_YEAR_BF

        setDaysFromYear0ToBF((calendar.BeginnJahreszählung - (calendar.HatNullJahr ? 0 : 1)) * DSADateCalendar.DAYS_PER_YEAR_BF);
        setHasYear0(calendar.HatNullJahr);
        setDaysPerYear(DSADateCalendar.DAYS_PER_YEAR_BF);
    }
    
    /**
     * Gets the calendar for the output of the date.
     */
    public Zeitrechnung getCalendar() {
        return calendar;
    }

    /**
     * Gets the calendar for the output of the date.
     */
    public void setCalendar(MeisterGeister.Logic.Kalender.Kalender calendar)
    {
        init(calendar);
    }

    /**
     * Gets the calendar for the output of the date.
     */
    /*
    public int getCalendarIDnum() {
        return calendar.getIDnum();
    }
    */

    public override String getHeadingText() {
        string s = String.Format("{0}.{1}. ({2}) {3}", getDay(), getMonthAsNumber(), getMonthString(getMonth()), getYearString());
        return s;
    }

    /**
     * Calculates the julian day from day and month (in DSA months)
     * @param day The day of the month, ranges from 1 to 30.
     * @param month The month of the year, ranges from 1 to 13.
     * @return The day of the year (julian day), ranges from 1 to 365.
     */
    public int jday(int day, int month) {
//        if ((month < 1) || (month > 13) || (day < 1) || (day > 30)) {
//            throw new DSAException("day or month out of range");
//        }
        return ((month - 1) * 30 + day);
    }

    /**
     * Sets the date.
     * @param day The day of the month, ranges from 1 to 30.
     * @param month The month of the year, ranges from 1 to 13.
     * @param year The year (according to BF).
     * @exception DSADateYearZeroException year==0, but calendar does not support a year 0.
     */
    public void setDayMonthYear(int day, int month, int year)  {
        setJDayYear(jday(day, month), year);
    }

    /**
     * @return The day of the month, ranges from 1 to 30.
     */
    public int getDay() {
        return (MathUtil.modulo(getJDay() - 1, 30) + 1);
    }

    /**
     * @return The month of the year.
     */
    public Monat getMonth() {
        return (Monat)(getMonthAsNumber() - 1);
    }

    public static string getMonthString(Monat m)
    {
        if (m == Monat.NamenloseTage)
            return "Tag des Namenlosen";
        else
            return m.ToString();
    }

    /**
     * @return The month of the year, ranges from 1 to 13.
     */
    public int getMonthAsNumber() {
        return (((getJDay() - 1) / 30) + 1);
    }

    public String getYearString() {
        return getYearString(getYear(), calendar.KürzelPlus, calendar.KürzelMinus);
    }

    /** The calendar the date is displayed in. */
    private Zeitrechnung calendar;
    
    /**
     * @return The day of the week.
     */
    /*
    public DayOfWeekType getDayOfWeek() {
        // The 1st of Praios 0 BF was a Praiostag.
        return DayOfWeek.findByIDnum(MathUtil.modulo((getDaysSinceBF() + 6), 7));
    }
    */
}

}
