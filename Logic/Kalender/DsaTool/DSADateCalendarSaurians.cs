using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.Logic.Kalender.DsaTool
{

    // based on work from Peter Diefenbach (peter@pdiefenbach.de)
    /// <summary>
    /// Der Echsische Kalender.
    /// Year speichert den Abschnitt. Era das Ehn. Eon das Tsiina.
    /// </summary>
    public class DSADateCalendarSaurians : DSADateCalendar
    {
        public const int DAYS_PER_WEEK = 5;
        public const int DAYS_PER_MONTH = 33;
        public const int MONTHS_PER_SECTION = 553;
        public const int SECTIONS_PER_EHHN = 10;
        public const int EHHNS_PER_TSIINA = 33;

        protected override void init()
        {
            Name = "Echsisch";
            // Der 1. Praios 1000 BF entspricht dem 
            // 18. Tag des 219. Monats im Abschnitt des Drachen (also 5. Abschnitt), 
            // im 4. Ehhn des 4. Tsiina und ist ein Gzht'G. 
            HasYear0 = false; //bezeiht sich hier auf das Tsiina
            DaysPerWeek = DAYS_PER_WEEK; //Woche
            DaysPerYear = DAYS_PER_MONTH * MONTHS_PER_SECTION + 1; //Abschnitt mit abschließendem Zhhszr'G
            DaysPerMonth = DAYS_PER_MONTH; // Monat (6 Wochen 3 Tage)
            DaysPerEra = SECTIONS_PER_EHHN * DaysPerYear; //Ehn
            DaysPerEon = EHHNS_PER_TSIINA * DaysPerEra; //Tsiina
            int d0BF = 1000 * DSADateCalendar.DAYS_PER_SUN_YEAR;
            d0BF -= 18 - 1; // 18. Tag
            d0BF -= (219 - 1) * DaysPerMonth; // 219. Monat
            d0BF -= (5 - 1) * DaysPerYear; //5. Abschnitt
            d0BF -= (4 - 1) * DaysPerEra; //4. Ehn
            d0BF -= (4 - 1) * DaysPerEon; //4. Tsiina
            DaysFromYear0ToBF = d0BF;
        }

        //public override int SpecialDaysCount(int day, int week = 0, int month = 0, int year = 0)
        //{
        //    if (year == 0)
        //        return 0;
        //    return year - 1;
        //}

        public override IList<string> WeekDayNames
        {
            get { return weekdayNames; }
        }

        public override int WeekDay
        {
            get { return (int)MathUtil.modulo(Day, DaysPerWeek, 1); }
            set { }
        }

        public override IList<string> YearNames
        {
            get
            {
                return sectionNames;
            }
        }

        public override String getHeadingText()
        {
            // Der 1. Praios 1000 BF entspricht dem 
            // 18. Tag des 219. Monats im Abschnitt des Drachen (also 5. Abschnitt), 
            // im 4. Ehhn des 4. Tsiina und ist ein Gzht'G (3.) 
            string s = String.Format(
                "{0} ({1}.), {2}. Tag des {3}. Monats im Abschnitt {4} ({5}.) im {6}. Ehn des {7}. Tsiina",
                WeekDayName,
                WeekDay,
                Day,
                Month,
                YearName,
                Year,
                Era,
                Eon
            );
            return s;
        }

        public String getYearString()
        {
            int year = Year;
            return String.Format(year>0?"des {0}. Tsiina":"vor dem {0}. Tsiina", year);
        }

        public static readonly String[] weekdayNames = new String[] {
            "Sz'G", 
            "Drs'G", 
            "Gzht'G", 
            "Lhn'G", 
            "Rsz'G",         
        };

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
