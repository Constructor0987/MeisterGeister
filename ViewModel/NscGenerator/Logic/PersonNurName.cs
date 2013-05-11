using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.ViewModel.NscGenerator.Logic
{
    class PersonNurName
    {
        #region //---- EIGENSCHAFTEN ----
        public String Name { set; get; }
        public String Namensbedeutung { set; get; }
        public Geschlecht Geschlecht { set; get; }
        public Model.Kultur Kultur { set; get; }
        public Model.Rasse Rasse { set; get; }
        #endregion

        #region //---- KONSTRUKTOR ----
        public PersonNurName(String name, String namensbedeutung, Geschlecht geschlecht, Model.Kultur kultur, Model.Rasse rasse)
        {
            Name = name;
            Namensbedeutung = namensbedeutung;
            Geschlecht = geschlecht;
            Kultur = kultur;
            Rasse = rasse;
        }
        #endregion
    }

    public enum Geschlecht
    {
        geschlechtslos = 0,
        weiblich = 1,
        männlich = 2
    }
}
