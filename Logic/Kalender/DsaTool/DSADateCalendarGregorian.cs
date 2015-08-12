using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.Logic.Kalender.DsaTool
{


    /**
     * A class representing the calendar or days counting practice in Aventuria,
     *  based on the translation from the Gregorian calendar used on earth.
     * <p><b>See:</b></p>
     * <ul>
     *   <li><a href="http://www.wiki-aventurica.de/wiki/Kalender">DSA Wiki Kalender</a></li>
     *   <li><a href="http://www.dasschwarzeauge.de/index.php?id=49">Aventurische Zeitrechnung auf dasschwarzeauge.de</a></li>
     *   <li>"Das Handbuch für den Reisenden", Section "Der Aventurische Kalender" (p. 60..63)</li>
     * </ul>
     *
     * @author Copyright (c) 2009 Peter Diefenbach (peter@pdiefenbach.de)
     */
    public class DSADateCalendarGregorian : DSADateCalendar
    {
        //private static Logger logger = Logger.getRootLogger();

        protected override void init()
        {
            Name = "Erde (Gregorianisch)";
        }

        public override String getHeadingText()
        {
            return String.Format(getDateFormat(), Date.toEarthDate());
        }

        public override String getContentText()
        {
            return getHeadingText();
        }

        public override string ToString()
        {
            return Date.toEarthDate().ToString();
        }

        private string df;

        protected string getDateFormat()
        {
            if (null == df)
            {
                df = "{0}";
            }
            return df;
        }
    }

}
