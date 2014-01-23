using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.ViewModel.Generator.Container
{
    public class PersonNurName : IFormattable
    {
        #region //---- EIGENSCHAFTEN ----
        public string Name { set; get; }
        public string Namensbedeutung { set; get; }
        public Geschlecht Geschlecht { set; get; }
        public Stand Stand { set; get; }
        public string Namenstyp { set; get; } //auf GUID umstellen
        #endregion

        #region //---- KONSTRUKTOR ----
        public PersonNurName (string namenstyp, string name, string namensbedeutung, Geschlecht geschlecht = Geschlecht.weiblich, Stand stand = Stand.unfrei)
        {

        }

        public PersonNurName()
            : this(string.Empty, string.Empty, string.Empty)
        {

        }
        #endregion

        #region //---- INSTANZMETHODEN ----

        public override string ToString()
        {
            return this.ToString("G", null);
        }

        /// <summary>
        /// Gibt den Namen als String zurück
        /// </summary>
        /// <param name="format">Format-String zur Definition der Rückgabe:
        /// "g": nur den Name
        /// "l": alle verfügbaren Werte</param>
        public string ToString(string format)
        {
            return this.ToString(format, null);
        }

        /// <summary>
        /// Gibt den Namen als String zurück
        /// </summary>
        /// <param name="format">Format-String zur Definition der Rückgabe:
        /// "g": nur den Name
        /// "l": alle verfügbaren Werte</param>
        public string ToString(string format, IFormatProvider provider)
        {
            if (String.IsNullOrEmpty(format)) format = "G";
            if (provider != null)
            {
                ICustomFormatter formatter = provider.GetFormat(this.GetType()) as ICustomFormatter;
                if (formatter != null) return formatter.Format(format, this, provider);
            }
            switch (format.ToLowerInvariant())
            {
                case "g": return Name;
                case "l": return string.Format("{0}\nBedeutung: {1}\nGeschlecht: {3}, Stand:{2}\nUrsprung: {4}",
                Name, Namensbedeutung, Stand.ToString("f"), Geschlecht.ToString("f"), Namenstyp);
                default: return Name;
            }
        }

        #endregion
    }

    public enum Geschlecht
    {
        weiblich = 0,
        männlich = 1
    }

    public enum Stand
    {
        unfrei = 0,
        landfrei = 1,
        stadtfrei = 2,
        adelig = 3
    }
}
