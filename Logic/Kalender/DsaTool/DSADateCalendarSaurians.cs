using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.Logic.Kalender.DsaTool
{

    /**
     * A class representing the calendar or days counting practice in Aventuria
     *  as used by Saurians ("Echsen" in German).
     * <p><b>See:</b> "Das Handbuch für den Reisenden", Section "Der Aventurische Kalender" (p. 60..63)</p>
     *
     * @author Copyright (c) 2009 Peter Diefenbach (peter@pdiefenbach.de)
     */
    public class DSADateCalendarSaurians : DSADateCalendar
    {
        public const int DAYS_PER_MONTH = 33;
        public const int MONTHS_PER_SECTION = 553;
        public const int SECTIONS_PER_EHHN = 10;
        public const int EHHNS_PER_TSIINA = 33;

        protected void init()
        {
            Name = "Echsisch";
            // Der 1. Praios 1000 BF entspricht dem 
            // 18. Tag des 219. Monats im Abschnitt des Drachen (also 5. Abschnitt), 
            // im 4. Ehhn des 4. Tsiina und ist ein Gzht'G. 
            DaysFromYear0ToBF = 1000 * DSADateCalendar.DAYS_PER_SUN_YEAR -
                    ((18 - 1) + DAYS_PER_MONTH *
                        ((219 - 1) + MONTHS_PER_SECTION *
                            ((5 - 1) + SECTIONS_PER_EHHN *
                                ((4 - 1) + EHHNS_PER_TSIINA *
                                    ((4 - 1))))));
            HasYear0 = true;
            DaysPerYear = DAYS_PER_MONTH * MONTHS_PER_SECTION;
        }

        public override String getHeadingText()
        {
            int section = getSection();
            int weekday = getWeekday();

            // Der 1. Praios 1000 BF entspricht dem 
            // 18. Tag des 219. Monats im Abschnitt des Drachen (also 5. Abschnitt), 
            // im 4. Ehhn des 4. Tsiina und ist ein Gzht'G (3.) 

            string s = String.Format(
                "{0} ({1}.), {2}. Tag des {3}. Monats im Abschnitt {4} ({5}.) im {6}. Ehn des {7}. Tsiina",
                weekdayNames[weekday],
                weekday + 1,
                getDay() + 1,
                getMonth() + 1,
                sectionNames[section],
                section + 1,
                getEhhn() + 1,
                getTsiina() + 1
            );
            return s;
        }

        public String getYearString()
        {
            return getYearString(getYear(), "gS", "v.gS");
        }

        public int getWeekday()
        {
            return (int)MathUtil.modulo(getDaysSinceBF() + 2, DAYS_PER_WEEK);
        }

        public int getDay()
        {
            return getPart(1, DAYS_PER_MONTH);
        }

        public int getMonth()
        {
            return getPart(DAYS_PER_MONTH, MONTHS_PER_SECTION);
        }

        public int getSection()
        {
            return getPart(DAYS_PER_MONTH * MONTHS_PER_SECTION, SECTIONS_PER_EHHN);
        }

        public int getEhhn()
        {
            return getPart(DAYS_PER_MONTH * MONTHS_PER_SECTION * SECTIONS_PER_EHHN, EHHNS_PER_TSIINA);
        }

        public int getTsiina()
        {
            return getPart(DAYS_PER_MONTH * MONTHS_PER_SECTION * SECTIONS_PER_EHHN * EHHNS_PER_TSIINA, 0);
        }

        public int getPart(int divisor, int modulo)
        {
            if (0 == modulo)
            {
                return (int)MathUtil.divisio(getDaysSinceYear0(), divisor);
            }
            else
            {
                return (int)MathUtil.divisio(MathUtil.modulo(getDaysSinceYear0(), divisor * modulo), divisor);
            }
        }

        public static readonly String[] weekdayNames = new String[] {
            "Sz'G", 
            "Drs'G", 
            "Gzht'G", 
            "Lhn'G", 
            "Rsz'G",         
        };

        public static readonly int DAYS_PER_WEEK = weekdayNames.Length;

        public static readonly String[] sectionNames = new String[] {
            "des Waran", 
            "des Schlingers", 
            "der Schlange", 
            "des Salamanders", 
            "des Drachen", 
            "der Flugechse", 
            "der Schildkröte", 
            "des Chamäleons", 
            "der Seeschlange",
            "des Krokodils", 
        };
    }

}
