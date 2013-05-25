using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.ViewModel.NscGenerator.Logic
{
    public class PersonNurName
    {
        #region //---- EIGENSCHAFTEN ----
        public string Name { set; get; }
        public string Namensbedeutung { set; get; }
        public Geschlecht Geschlecht { set; get; }
        public Stand Stand { set; get; }
        public string Namenstyp { set; get; } //auf GUID umstellen
        #endregion

        #region //---- KONSTRUKTOR ----
        public PersonNurName(string name, string namensbedeutung, string namenstyp, Geschlecht geschlecht, Stand stand)
        {
            Name = name;
            Namensbedeutung = namensbedeutung;
            Namenstyp = namenstyp; 
            Geschlecht = geschlecht;
            Stand = stand;
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
