using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeisterGeister.Logic.Kalender;

namespace MeisterGeister.Logic.Kalender.DsaTool
{
    // based on work from Peter Diefenbach (peter@pdiefenbach.de)
    /// <summary>
    /// Ein Basiskalender für aventurische Tageszählpraktiken.
    /// Er bietet die Möglichkeit der Unterteilung der Zeit in Äon, Ära, Jahr, Monat, Woche und Tag.
    /// Jahr, Monat und Tag werden als grundlegend vorausgesetzt.
    /// Für Kalender ohne diese Konzepte empfieht es sich andere Unterteilungen darauf zuzuweisen.
    /// Unterklassen müssen die Werte für DaysPerYear, DaysPerMonth, DaysPerWeek, DaysFromYear0ToBF sowie HasYear0 in der init()-Methode initialisieren.
    /// DaysPerEra, DaysPerEon und DaysPerWeek können auf 0 gesetzt werden, dann werden diese Unterteilungen übersprungen.
    /// Falls es Sondertage in einem Jahr gibt, dann müssen auch die Eigenschaft SpecialDayName und die Methode SpecialDayCount überschrieben werden.
    /// Die Standarimplementierung von Week geht von einer kontiuierlichen Zählweise aus. Die Eigenschaft kann für andere Zählweisen überschrieben werden.
    /// </summary>
    public class DSADateCalendar : IComparable<DSADateTime>, IComparable<DSADateCalendar>, IEquatable<DSADateTime>, IFormattable
    {

        //private static Logger logger = Logger.getRootLogger();

        /** The number of days the year has for the calendar Bosparans fall. */
        public const int DAYS_PER_SUN_YEAR = 365;

        /** The calendar Bosparans fall has a year zero. */
        public const bool HAS_YEAR_ZERO_BF = true;

        private DSADateTime date;
        /// <summary>
        /// Aktuelles Datum
        /// </summary>
        public DSADateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        private int daysFromYear0ToBF = 0;
        /// <summary>
        /// Die Tage vom ersten Tags des Jahr 0 des Kalenders bis zum 1. Praios 0 BF.
        /// </summary>
        public int DaysFromYear0ToBF
        {
            get { return daysFromYear0ToBF; }
            set { daysFromYear0ToBF = value; }
        }

        private int daysPerEon = 0;
        /// <summary>
        /// Tage pro Äon
        /// </summary>
        public int DaysPerEon
        {
            get { return daysPerEon; }
            protected set { daysPerEon = value; }
        }

        private int daysPerEra = 0;
        /// <summary>
        /// Tage pro Ära
        /// </summary>
        public int DaysPerEra
        {
            get { return daysPerEra; }
            protected set { daysPerEra = value; }
        }

        private int daysPerYear = DAYS_PER_SUN_YEAR;
        /// <summary>
        /// Tage pro Jahr
        /// </summary>
        public int DaysPerYear
        {
            get { return daysPerYear; }
            protected set { daysPerYear = value; }
        }

        private int daysPerMonth = 30;
        /// <summary>
        /// Tage pro Monat
        /// </summary>
        public int DaysPerMonth
        {
            get { return daysPerMonth; }
            protected set { daysPerMonth = value; }
        }

        private int daysPerWeek = 7;
        /// <summary>
        /// Tage pro Woche
        /// </summary>
        public int DaysPerWeek
        {
            get { return daysPerWeek; }
            set { daysPerWeek = value; }
        }

        private bool _hasYear0 = HAS_YEAR_ZERO_BF;
        /// <summary>
        /// Der Kalender hat ein Jahr 0.
        /// </summary>
        public bool HasYear0
        {
            get { return _hasYear0; }
            protected set { _hasYear0 = value; }
        }

        private String name = "Basiskalender";
        /// <summary>
        /// Name des Kalenders
        /// </summary>
        public String Name
        {
            get { return name; }
            protected set { name = value; }
        }

        /// <summary>
        /// Das Äon. Es besteht aus mehreren Ären.
        /// </summary>
        public virtual int Eon
        {
            get
            {
                if (DaysPerEon == 0)
                    return 0;
                return (int)MathUtil.divisio(DaysSinceYear0, DaysPerEon) + (!HasYear0?1:0);
            }
            set { }
        }

        /// <summary>
        /// Die Ära. Sie besteht aus mehreren Jahren.
        /// </summary>
        public virtual int Era
        {
            get
            {
                if (DaysPerEra == 0)
                    return 0;
                int era = DaysSinceYear0;
                if (DaysPerEon != 0)
                    era = (int)MathUtil.modulo(DaysSinceYear0, DaysPerEon);
                return (int)MathUtil.divisio(era, DaysPerEra) +  ((!HasYear0 || DaysPerEon != 0)?1:0);
            }
            set { }
        }

        /// <summary>
        /// Das Jahr
        /// </summary>
        public virtual int Year
        {
            get
            {
                int year = DaysSinceYear0;
                if (DaysPerEra != 0)
                    year = (int)MathUtil.modulo(DaysSinceYear0, DaysPerEra);
                year = (int)MathUtil.divisio(year, DaysPerYear);
                if (!HasYear0 && DaysPerEra == 0 && DaysPerEon == 0)
                {
                    if (0 >= year)
                    { // If there is no year zero, we start counting at year 1.
                        year++;
                    }
                }
                if (DaysPerEra != 0 || DaysPerEon != 0)
                    year++;
                return year;
            }
            set
            {
                if (!HasYear0 && value == 0)
                    value = 1;
                int yearDiff = Year;
                if (!HasYear0 && Math.Sign(yearDiff) != Math.Sign(value))
                    yearDiff--;
                if (yearDiff != 0)
                    DaysSinceYear0 += yearDiff * DaysPerYear;
            }
        }

        /// <summary>
        /// Der Name des Jahres
        /// </summary>
        public string YearName
        {
            get
            {
                int year = Year;
                if ((!HasYear0 || DaysPerEon != 0 || DaysPerEra != 0) && year > 0)
                    year--;
                if (year < 0 || YearNames == null || year >= YearNames.Count)
                    return null;
                return YearNames[year];
            }
        }

        /// <summary>
        /// Die Namen aller Jahre
        /// </summary>
        public virtual IList<string> YearNames
        {
            get { return null; }
        }

        /// <summary>
        /// Der Tag im Jahr
        /// </summary>
        public int YearDay
        {
            get
            {
                return (int)MathUtil.modulo(DaysSinceYear0, DaysPerYear) + 1;
            }
            set
            {
                int year = Year;
                if (!HasYear0 && year > 0)
                    year--;
                DaysSinceYear0 = year * DaysPerYear + value;
            }
        }

        /// <summary>
        /// Monat im Jahr. Von 1 bis ?.
        /// </summary>
        public virtual int Month
        {
            get
            {
                if (DaysPerMonth == 0)
                    return 0; return (int)MathUtil.divisio(YearDay, DaysPerMonth, 1) + 1;
            }
            set { }
        }

        /// <summary>
        /// Der Name des Monats
        /// </summary>
        public string MonthName
        {
            get
            {
                if (IsSpecialDay)
                    return null;
                int monat = Month;
                if (monat <= 0 || MonthNames == null || monat > MonthNames.Count)
                    return null;
                return MonthNames[--monat];
            }
        }

        /// <summary>
        /// Die Namen aller Monate
        /// </summary>
        public virtual IList<string> MonthNames
        {
            get { return null; }
        }

        /// <summary>
        /// Tag im Monat. Von 1 bis DaysPerMonth
        /// </summary>
        public virtual int Day
        {
            get
            {
                if (DaysPerMonth == 0)
                    return YearDay;
                if (IsSpecialDay)
                    return 0;
                return (int)MathUtil.modulo(YearDay, DaysPerMonth, 1);
            }
            set { }
        }

        /// <summary>
        /// Woche im Monat. Von 1 bis ?.
        /// </summary>
        public virtual int Week
        {
            get {
                if (DaysPerWeek == 0)
                    return 0;
                return (int)MathUtil.divisio(Day, DaysPerWeek) + 1; 
            }
            set { }
        }

        /// <summary>
        /// Der Name der Woche
        /// </summary>
        public string WeekName
        {
            get
            {
                if (IsSpecialDay)
                    return null;
                int week = Week;
                if (week <= 0 || WeekNames == null || week > WeekNames.Count)
                    return null;
                return WeekNames[--week];
            }
        }

        /// <summary>
        /// Die Namen aller Wochen
        /// </summary>
        public virtual IList<string> WeekNames
        {
            get { return null; }
        }

        /// <summary>
        /// Tag in der Woche. Von 1 bis DaysPerWeek.
        /// </summary>
        public virtual int WeekDay
        {
            get {
                if (IsSpecialDay)
                    return 0;
                return (int)MathUtil.modulo(DaysSinceYear0, DaysPerWeek, 1); 
            }
            set { }
        }

        /// <summary>
        /// Der Name des Wochentages
        /// </summary>
        public string WeekDayName
        {
            get
            {
                if (IsSpecialDay)
                    return null;
                int weekday = WeekDay;
                if (weekday <= 0 || WeekDayNames == null || weekday > WeekDayNames.Count)
                    return null;
                return WeekDayNames[--weekday];
            }
        }

        /// <summary>
        /// Die Namen aller Wochentage
        /// </summary>
        public virtual IList<string> WeekDayNames
        {
            get { return null; }
        }

        /// <summary>
        /// Die Uhrzeit
        /// </summary>
        public TimeSpan Time
        {
            get { return date.getTime(); }
            set { date.setTime(value); }
        }

        /// <summary>
        /// Name des Sondertages
        /// </summary>
        public virtual string SpecialDayName
        {
            get { return null; }
        }

        /// <summary>
        /// Ist der Tag ein Sondertag?
        /// Ein Sondertag steht außerhalb der normalen Abschnittszählung.
        /// </summary>
        public bool IsSpecialDay
        {
            get { return !String.IsNullOrEmpty(SpecialDayName); }
        }

        /// <summary>
        /// Die Anzahl der vergangenen Sondertage
        /// </summary>
        /// <param name="day">Tage</param>
        /// <param name="week">Wochen</param>
        /// <param name="month">Monate</param>
        /// <param name="year">Jahre</param>
        public virtual int SpecialDaysCount(int day, int week = 0, int month = 0, int year = 0)
        {
            return 0;
        }


        #region Konstruktoren und init
        /// <summary>
        /// Erstellt einen neuen Kalender mit dem Datum 1. Praios, 0 BF 0:00 Uhr.
        /// </summary>
        public DSADateCalendar()
        {
            init();
            date = new DSADateTime();
        }

        /// <summary>
        /// Erstellt einen neuen Kalender mit dem angegebenen Datum.
        /// </summary>
        /// <param name="date"></param>
        public DSADateCalendar(DSADateTime date)
        {
            init();
            this.date = date;
        }

        /// <summary>
        /// Initialisierung der kalenderspezifischen Werte
        /// </summary>
        protected virtual void init()
        {
        }
        #endregion

        /// <summary>
        /// Tage seit Bosparans Fall
        /// </summary>
        public int DaysSinceBF
        {
            get { return (int)date.getDaysSinceBF(); }
            set { date.setDaysSinceBF(value); }
        }

        /// <summary>
        /// Tag des Jahres im Bosparans Fall Kalender
        /// </summary>
        public int YearDayBF
        {
            get { return (int)MathUtil.modulo(DaysSinceBF, DAYS_PER_SUN_YEAR, 1); }
        }

        /// <summary>
        /// Jahr im Bosparans Fall Kalender
        /// </summary>
        public int YearBF
        {
            get { return (int)MathUtil.divisio(DaysSinceBF, DAYS_PER_SUN_YEAR); }
        }

        /// <summary>
        /// Tage seit dem Anfang des Jahres 0 dieses Kalenders
        /// </summary>
        public int DaysSinceYear0
        {
            get { return DaysSinceBF - daysFromYear0ToBF; }
            set { DaysSinceBF = value + daysFromYear0ToBF; }
        }

        /// <summary>
        /// Setzt das Datum anhand der Unterteilungen wie Tag, Woche, Monat und Jahr.
        /// setDate(13, 0, 12, 1013) entspricht dem 13. Tag im 12. Monat des 1013. Jahres
        /// setDate(8, 3, 7, 4073) entspricht dem 8. Tag der 3. Woche im 7. Monat des 4073. Jahres
        /// </summary>
        /// <param name="day">Tag</param>
        /// <param name="week">Woche</param>
        /// <param name="month">Monat</param>
        /// <param name="year">Jahr</param>
        /// <param name="era">Ära</param>
        /// <param name="eon">Äon</param>
        public virtual void setDate(int day, int week, int month, int year, int era = 0, int eon = 0)
        {
            int daysSinceYear0 = 0;
            if (day != 0)
                daysSinceYear0 += day - 1;
            // Addieren der Sondertage
            daysSinceYear0 += SpecialDaysCount(day, week, month, year);
            if (week != 0)
                daysSinceYear0 += --week * DaysPerWeek;
            if (month != 0)
                daysSinceYear0 += --month * DaysPerMonth;
            if (year != 0)
            {
                if (!HasYear0 && era == 0 && eon == 0 && year < 0)
                    year++;
                daysSinceYear0 += ((era != 0 || eon != 0) ? --year : year) * DaysPerYear;
            }
            if (era != 0)
            {
                if (!HasYear0 && eon == 0 && era < 0)
                    era++;
                daysSinceYear0 += ((eon != 0) ? --era : era) * DaysPerEra;
            }
            if (eon != 0)
            {
                if (!HasYear0 && eon < 0)
                    eon++;
                daysSinceYear0 += --eon * DaysPerEon;
            }
            DaysSinceYear0 = daysSinceYear0;
        }

        public static String getYearString(int year, String positiveUnitName, String negativeUnitName)
        {
            string s = String.Format("{0} {1}", (year < 0) ? -year : year, (year < 0) ? negativeUnitName : positiveUnitName);
            return s;
        }

        /// <summary>
        /// Tag des Mondes. 0 entspricht Neumond.
        /// </summary>
        public int Moonday
        {
            get { return date.getMoonday(); }
        }

        /// <summary>
        /// Die Jahreszeit
        /// </summary>
        public Season Season
        {
            get { return date.season(); }
        }

        public virtual String getHeadingText()
        {
            return "Year " + Year + " YearDay " + YearDay + " BF";
        }

        public virtual String getContentText()
        {
            string s = String.Format("{0}: {1}: {2}", getHeadingText(), "Mondphase", Datum.MondphaseString(Moonday + 1));
            return s;
        }

        public virtual string ToString(string format, IFormatProvider formatProvider)
        {
            if (String.IsNullOrEmpty(format) || format == "G")
            {
                return "DSADateCalendar " + DaysSinceBF + " (Year " + YearBF + " YearDay " + YearDayBF + " BF)";
            }
            return "DSADateCalendar " + DaysSinceBF + " (Year " + YearBF + " YearDay " + YearDayBF + " BF)";
        }

        #region Interfaces
        public override string ToString()
        {
            return this.ToString(null, System.Globalization.CultureInfo.CurrentCulture);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public int CompareTo(DSADateTime other)
        {
            return Date.CompareTo(other);
        }

        public int CompareTo(DSADateCalendar other)
        {
            return Date.CompareTo(other.Date);
        }

        public bool Equals(DSADateTime other)
        {
            return Date.Equals(other);
        }

        public override bool Equals(object obj)
        {
            if (obj is DSADateTime)
                return Date.Equals(obj);
            return base.Equals(obj);
        }

        public static bool operator ==(DSADateCalendar emp1, DSADateTime emp2)
        {
            if (object.ReferenceEquals(emp1, null) && object.ReferenceEquals(emp2, null))
                return true;
            if (object.ReferenceEquals(emp1, null)) return false;
            return emp1.Equals(emp2);
        }

        public static bool operator !=(DSADateCalendar emp1, DSADateTime emp2)
        {
            if (object.ReferenceEquals(emp1, null) && object.ReferenceEquals(emp2, null))
                return false;
            if (object.ReferenceEquals(emp1, null)) return true;
            return !emp1.Equals(emp2);
        }
        #endregion
    }

}
