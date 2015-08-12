using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.Logic.Kalender.DsaTool
{

    /**
     * A class representing the calendar or days counting practice in Aventuria
     *  as used on the continent Myranor.
     * <p><b>See:</b> Jenseits des Horizonts 261</p>
     *
     * @author Based on work by Peter Diefenbach (peter@pdiefenbach.de), rewritten by TheyWor
     */
    public class DSADateCalendarMyranisch : DSADateCalendar
    {
        //    private static Logger logger = Logger.getRootLogger();
        public const int YEAR_ZERO_BF_IS = 3747;
        public const int DAYS_PER_NONE = 9;
        public const int NONES_PER_OCTADE = 5;
        public const int YEAR_OFFSET_IN_DAYS = 183; // 365 / 2 + 1

        protected override void init()
        {
            Name = "Myranisch";
            DaysFromYear0ToBF = -YEAR_ZERO_BF_IS * DSADateCalendar.DAYS_PER_SUN_YEAR + YEAR_OFFSET_IN_DAYS;
            HasYear0 = true;
            DaysPerYear = DSADateCalendar.DAYS_PER_SUN_YEAR;
        }

        public override String getHeadingText()
        {
            //Feiertag, 2 Oktale, Feiertag, 2 Oktale, ...
            // 1, 92, 183, 274, 365 // Mod 91 == 1
            // "Thearchentag", "Nebeltag", "Sonnentag", "Sturmtag", "Frosttag"

            int jday = getJDay() - 1;
            string s = "";
            if (jday % 91 == 1)
                s = String.Format("{0} {1}", ((Sondertag)getSondertage(jday)).ToString(), getYearString());
            else
                s = String.Format("{5} ({0}) der {1}. None im {4} ({2}) {3}", getDay() + 1, getNone() + 1, getOctade() + 1, getYearString(), getOctadeString(), getDayString());
            return s;
        }

        public int getSondertage(int jday)
        {
            return jday / 91 + ((jday>1)?1:0);
        }

        public int getDay()
        {
            int jday = getJDay() - 1;
            jday -= getSondertage(jday);
            return getPart(jday, 1, DAYS_PER_NONE);
        }

        public string getDayString()
        {
            int o = getDay();
            return ((Wochentag)o).ToString();
        }

        public int getNone()
        {
            int jday = getJDay() - 1;
            jday -= getSondertage(jday);
            return getPart(jday, DAYS_PER_NONE, NONES_PER_OCTADE);
        }

        public int getOctade()
        {
            int jday = getJDay() - 1;
            jday -= getSondertage(jday);
            return getPart(jday, DAYS_PER_NONE * NONES_PER_OCTADE, DaysPerYear);
        }

        public string getOctadeString()
        {
            int o = getOctade();
            return ((Oktade)o).ToString();
        }

        public void setDayNoneOctadeYear(int day, int none, int octade, int year)
        {
            int days = (octade - 1) * DAYS_PER_NONE * NONES_PER_OCTADE + year * DaysPerYear;
            if (day > 0 && none > 0)
                days += (day - 1) + (none - 1) * DAYS_PER_NONE;
            else
                days -= 1; //es ist ein sondertag der der angegebenen oktade vorangeht.
            days += (octade + 1) / 2; //Sondertage
            days += DaysFromYear0ToBF;
            setDaysSinceBF(days);
        }

        public String getYearString()
        {
            int year = getYear();
            string s = String.Format("{0} IZ", year);
            return s;
        }

        public int getPart(int value, int divisor, int modulo)
        {
            if (0 == modulo)
            {
                return (int)MathUtil.divisio(value, divisor);
            }
            else
            {
                return (int)MathUtil.divisio(MathUtil.modulo(value, divisor * modulo), divisor);
            }
        }

        public enum Oktade
        {
            Nereton,
            Siminia,
            Zatura,
            Shinxir,
            Brajan,
            Raia,
            Chrysir,
            Gyldara
        }

        public enum Sondertag
        {
            Thearchentag,
            Nebeltag,
            Sonnentag,
            Sturmtag,
            Frosttag
        }

        public enum Wochentag
        {
            Schaffenstag,
            Ahnentag,
            Ackertag,
            Markttag,
            Opfertag,
            Rechtstag,
            Fuhrtag,
            Streittag,
            Ruhetag
        }

    }

}
