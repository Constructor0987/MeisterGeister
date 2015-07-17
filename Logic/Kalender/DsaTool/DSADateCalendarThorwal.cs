using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.Logic.Kalender.DsaTool
{
    /**
     * A class representing the calendar used in Thorwal. 
     * <p><b>See:</b> "Das Handbuch für den Reisenden", Section "Der Aventurische Kalender" (p. 60..63)</p>
     *
     * @author Copyright (c) 2009 Peter Diefenbach (peter@pdiefenbach.de)
     */
    public class DSADateCalendarThorwal : DSADateCalendar
    {
        //private static Logger logger = Logger.getRootLogger();

        /** 
         * Creates a calendar based on Jurgas landing, showing a date representing Praios 1st, 0 BF.
         * @see DSADateCalendar#DSADateCalendar() 
         */
        public DSADateCalendarThorwal()
            : base()
        {
            init();
        }

        /** 
         * Creates a view following the calendar based on Jurgas landing to the given date.
         * @see DSADateCalendar#DSADateCalendar(DSADate) 
         */
        public DSADateCalendarThorwal(DSADateTime date)
            : base(date)
        {
            init();
        }

        protected void init()
        {
            setName("Thorwalsch (Jurgas Landung)");
            setDaysFromYear0ToBF(-YEAR_ZERO_BF_IS * DSADateCalendar.DAYS_PER_YEAR_BF);
            setHasYear0(false);
            setDaysPerYear(DSADateCalendar.DAYS_PER_YEAR_BF);
        }

        /** Das Jahr 1 JL entspricht 1627 v. BF. */
        public const int YEAR_ZERO_BF_IS = 1627;

        public const int DAYS_PER_MONTH = 30;

        public override String getHeadingText()
        {
            int jday = getJDay() - 1;
            int monthdiv = (int)MathUtil.divisio(jday, DAYS_PER_MONTH);
            int monthmod = (int)MathUtil.modulo(jday, DAYS_PER_MONTH);

            string s = String.Format("{0}. {1} ({2}.) {3}", monthmod + 1, monthNames[monthdiv], monthdiv + 1, getYearString());
            return s;
        }

        public String getYearString()
        {
            return getYearString(getYear(), "JL", "v.JL");
        }

        public static readonly String[] monthNames = new String[] {
            "Midsonnmond", 
            "Kornmond", 
            "Heimamond", 
            "Schlachtmond", 
            "Sturmmond", 
            "Frostmond", 
            "Grimfrostmond", 
            "Goimond", 
            "Friskenmond", 
            "Eimond", 
            "Faramond", 
            "Vinmond",
            "Hranngar-Tag",
        };
    }

}
