using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.Logic.Kalender.DsaTool
{
   

/**
 * A class representing the calendar or days counting practice in Aventuria
 *  as used by dwarfs.
 * <p><b>See:</b> "Das Handbuch für den Reisenden", Section "Der Aventurische Kalender" (p. 60..63)</p>
 *
 * @author Copyright (c) 2009 Peter Diefenbach (peter@pdiefenbach.de)
 */
public class DSADateCalendarDwarfs : DSADateCalendar {

    //private static Logger logger = Logger.getRootLogger();

    /** 
     * Creates a calendar showing a date representing Praios 1st, 0 BF.
     * @see DSADateCalendar#DSADateCalendar() 
     */
    public DSADateCalendarDwarfs() : base() {
        init();
    }

    /** 
     * Creates a view following this calendar to the given date.
     * @see DSADateCalendar#DSADateCalendar(DSADate) 
     */
    public DSADateCalendarDwarfs(DSADate date) :base(date) {
        init();
    }

    protected void init() {
        setName("Zwergisch");
        setDaysFromYear0ToBF(-YEAR_ZERO_BF_IS * DSADateCalendar.DAYS_PER_YEAR_BF);
        setHasYear0(true);
        setDaysPerYear(DSADateCalendar.DAYS_PER_YEAR_BF);
    }
    
    public override String getHeadingText() {
        int jday = getJDay() - 1;
        int monthdiv = MathUtil.divisio(jday, DAYS_PER_MONTH);
        int monthmod = MathUtil.modulo(jday, DAYS_PER_MONTH);

        string s = String.Format("{0:2}. {1:13} ({2}.) {3}", monthmod + 1, monthNames[monthdiv], monthdiv + 1, getYearString());
        return s;
    }

    public String getYearString() {
        int year = getYear();
        string unitName = "Jahre nach der zweiten Dämonenschlacht";
        if (-1 == year)
           unitName = "Jahr vor der zweiten Dämonenschlacht";
        else if (0 == year)
            unitName = "im Jahr der zweiten Dämonenschlacht";
        else if (1 == year)
            unitName = "Jahr nach der zweiten Dämonenschlacht";
        else if (year < 0)
            unitName = "Jahre vor der zweiten Dämonenschlacht";
        
        string s = String.Format("{0:5} {1}", (year < 0) ? -year : year, unitName);

        return s;
    }
    
    public const int YEAR_ZERO_BF_IS = 0;
    
    public const int DAYS_PER_MONTH = 30;

    public static readonly String[] monthNames = new String[] {
        "Sommermond", 
        "Hitzemond", 
        "Regenmond", 
        "Weinmond", 
        "Nebelmond", 
        "Dunkelmond", 
        "Frostmond", 
        "Neugeburt", 
        "Marktmond", 
        "Saatmond", 
        "Feuer- (oder Feier)mond",
        "Brautmond",
        "Drachentag",
    };
}

}
