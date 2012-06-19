using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.Logic.Kalender.DsaTool
{
    /**
 * A class representing the calendar or days counting practice in Aventuria
 *  as used by Norbards.
 * <p><b>See:</b> "Das Handbuch für den Reisenden", Section "Der Aventurische Kalender" (p. 60..63)</p>
 *
 * @author Copyright (c) 2009 Peter Diefenbach (peter@pdiefenbach.de)
 */
public class DSADateCalendarNorbards : DSADateCalendar {
    //private static Logger logger = Logger.getRootLogger();

    /** 
     * Creates a calendar showing a date representing Praios 1st, 0 BF.
     * @see DSADateCalendar#DSADateCalendar() 
     */
    public DSADateCalendarNorbards() : base() {
        init();
    }
    
    /** 
     * Creates a view following this calendar to the given date.
     * @see DSADateCalendar#DSADateCalendar(DSADate) 
     */
    public DSADateCalendarNorbards(DSADate date) : base(date){
        init();
    }

    protected void init() {
        setName("Norbardisch");
        setDaysFromYear0ToBF(YEAR_ZERO_BF_IS * DSADateCalendar.DAYS_PER_YEAR_BF + YEAR_OFFSET_IN_DAYS);
        setHasYear0(false);
        setDaysPerYear(DAYS_PER_MONTH * MONTHS_PER_YEAR);
    }

    public override String getHeadingText()
    {
        int jday = getJDay() - 1;
        int monthdiv = MathUtil.divisio(jday, DAYS_PER_MONTH);
        int monthmod = MathUtil.modulo(jday, DAYS_PER_MONTH);

        string s = String.Format("{0}. Tag im {1}. Mondmonat im {2}. Uh'Jun", monthmod + 1, monthdiv + 1, getYear());
        return s;
    }

    /** This is just guessing. */
    public const int YEAR_ZERO_BF_IS = 0;
    
    /** This is just guessing. */
    public const int YEAR_OFFSET_IN_DAYS = 0;
    
    public const int DAYS_PER_MONTH = 28;

    public const int MONTHS_PER_YEAR = 100;
}

}
