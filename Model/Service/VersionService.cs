using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.Model.Service
{
    public class VersionService : ServiceBase
    {
        #region //----- EIGENSCHAFTEN ----

        public Model.Version Datenbank
        {
            get
            {
                var li = Liste<Version>();
                if (li != null && li.Count > 0)
                    return li.Where(v => v.Name == "Datenbank").FirstOrDefault();
                return null;
            }
        }

        #endregion

        #region //----- KONSTRUKTOR ----

        public VersionService()
        {
        }

        #endregion

        #region //----- DATENBANKABFRAGEN ----

        #endregion

    }
}
