using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.Logic.Kalender.DsaTool
{


    /**
     * A class representing the calendar or days counting practice in Aventuria
     *  as used in Gjalskerland.
     * <p><b>See:</b> "Das Handbuch für den Reisenden", Section "Der Aventurische Kalender" (p. 60..63)</p>
     *
     * @author Copyright (c) 2009 Peter Diefenbach (peter@pdiefenbach.de)
     */
    public class DSADateCalendarGjalskerland : DSADateCalendar
    {
        /** Die Jahreszählung erfolgt nach dem ersten Tag der Geburt im Mond des Blutes im Jahr 1 nach der großen Schlacht (gS). 
         * Das Datum entspricht dem 19. Ingerimm 1370 v. BF. */
        public const int YEAR_ZERO_BF_IS = -1370 - 1;
        public const int YEAR_OFFSET_IN_DAYS = 10 * 30 + 19;
        public const int DAYS_PER_MONTH = DSADateTime.MOON_MONTH_DAYS;
        public const int MONTHS_PER_YEAR = 13;
        public const int YEARS_PER_ERA = 364;

        protected override void init()
        {
            Name = "Gjalskerland";
            DaysFromYear0ToBF = YEAR_ZERO_BF_IS * DSADateCalendar.DAYS_PER_SUN_YEAR + YEAR_OFFSET_IN_DAYS;
            HasYear0 = false;
            DaysPerYear = DAYS_PER_MONTH * MONTHS_PER_YEAR;
        }

        public override string getHeadingText()
        {
            int jday = getJDay() - 1;
            int monthdiv = (int)MathUtil.divisio(jday, DAYS_PER_MONTH);
            int monthmod = (int)MathUtil.modulo(jday, DAYS_PER_MONTH);

            string s = String.Format("{0}. Tag der Geburt im {1} ({2}) {3}", monthmod + 1, monthNames[monthdiv], monthdiv + 1, getYearString());
            return s;
        }

        public String getYearString()
        {
            return getYearString(getYear(), "gS", "v.gS");
        }

        public static readonly String[] monthNames = new String[] {
            "Mond des Blutes", 
            "Siegesmond", 
            "Trauermond", 
            "Mond des Bundes", 
            "Mond des Schaffens", 
            "Mond des Sturms", 
            "Mond der Stille", 
            "Mond des Hungers", 
            "Mond der Jagd", 
            "Göttermond", 
            "Mond des Odûn", 
            "Mond der Niederkunft", 
            "Ahnenmond",     
        };
    }

}
