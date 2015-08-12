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

    // This is just guessing.
    public const int YEAR_ZERO_BF_IS = 0;
    // This is just guessing.
    public const int YEAR_OFFSET_IN_DAYS = 0;
    public const int DAYS_PER_MONTH = DSADateTime.MOON_MONTH_DAYS;
    public const int MONTHS_PER_YEAR = 100;

    protected override void init() {
        Name = "Norbardisch";
        DaysFromYear0ToBF = YEAR_ZERO_BF_IS * DSADateCalendar.DAYS_PER_SUN_YEAR + YEAR_OFFSET_IN_DAYS;
        HasYear0 = false;
        DaysPerYear = DAYS_PER_MONTH * MONTHS_PER_YEAR;
    }

    public override String getHeadingText()
    {
        int jday = getJDay() - 1;
        int monthdiv = (int)MathUtil.divisio(jday, DAYS_PER_MONTH);
        int monthmod = (int)MathUtil.modulo(jday, DAYS_PER_MONTH);

        string s = String.Format("{0}. Tag im {1}. Mondmonat im {2}. Uh'Jun", monthmod + 1, monthdiv + 1, getYear());
        return s;
    }

}

}
