using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeisterGeister.Logic.Kalender;

namespace MeisterGeister.Logic.Kalender.DsaTool
{

    // based on work from Peter Diefenbach (peter@pdiefenbach.de)
    public class DSADateCalendarTwelve : DSADateCalendar
    {
        //private static Logger logger = Logger.getRootLogger();

        public DSADateCalendarTwelve()
            : base()
        {
            init(MeisterGeister.Logic.Kalender.Kalender.BosparansFall);
        }

        public DSADateCalendarTwelve(DSADateTime date)
            : base(date)
        {
            init(MeisterGeister.Logic.Kalender.Kalender.BosparansFall);
        }

        public DSADateCalendarTwelve(MeisterGeister.Logic.Kalender.Kalender kal)
            : base()
        {
            init(kal);
        }

        public DSADateCalendarTwelve(DSADateTime date, MeisterGeister.Logic.Kalender.Kalender kal)
            : base(date)
        {
            init(kal);
        }

        protected override void init()
        {
            //alle Konstruktoren sind überschrieben
        }

        protected void init(MeisterGeister.Logic.Kalender.Kalender kal)
        {
            this.calendar = Zeitrechnung.ZeitrechnungenDictionary[kal];

            Name = calendar.Name;
            //-calendar.getYearZeroBFis() * DSADateCalendar.DAYS_PER_YEAR_BF
            DaysFromYear0ToBF = (calendar.BeginnJahreszählung - (calendar.HatNullJahr ? 0 : 1)) * DSADateCalendar.DAYS_PER_SUN_YEAR;
            HasYear0 = calendar.HatNullJahr;
            DaysPerYear = DSADateCalendar.DAYS_PER_SUN_YEAR;
            DaysPerWeek = 7;
            DaysPerMonth = 30;
        }

        public override int WeekDay
        {
            get
            {
                return (int)MathUtil.modulo((DaysSinceBF + 3), 7) + 1;
            }
            set
            {
                //base.WeekDay = value;
            }
        }

        public override IList<string> WeekDayNames
        {
            get { return weekdayNames; }
        }

        public override IList<string> MonthNames
        {
            get { return monthNames; }
        }

        /**
         * Gets the calendar for the output of the date.
         */
        public Zeitrechnung getCalendar()
        {
            return calendar;
        }

        /**
         * Gets the calendar for the output of the date.
         */
        public void setCalendar(MeisterGeister.Logic.Kalender.Kalender calendar)
        {
            init(calendar);
        }

        public override String getHeadingText()
        {
            string s = String.Format("{0}.{1}. ({2}) {3}", Day, Month, MonthName, getYearString());
            return s;
        }

        public String getYearString()
        {
            return getYearString(Year, calendar.KürzelPlus, calendar.KürzelMinus);
        }

        /** The calendar the date is displayed in. */
        private Zeitrechnung calendar;

        public static readonly String[] weekdayNames = new String[] {
            "Windstag",   // Donnerstag
            "Erdstag",    // Freitag
            "Markttag",   // Samstag
            "Praiostag",  // Sonntag
            "Rohalstag",  // Montag
            "Feuertag",   // Dienstag
            "Wassertag"   // Mittwoch
        };

        public static readonly String[] monthNames = new String[] {
            "Praios",     // Juli
            "Rondra",     // August
            "Efferd",     // September
            "Travia",     // Oktober
            "Boron",      // November
            "Hesinde",    // Dezember
            "Firun",      // Januar
            "Tsa",        // Februar
            "Phex",       // März
            "Peraine",    // April
            "Ingerimm",  // Mai
            "Rahja",     // Juni
            "Tag des Namenlosen"
        };

    }

}
