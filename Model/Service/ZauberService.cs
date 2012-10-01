using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//Eigene Usings
using Model = MeisterGeister.Model;

namespace MeisterGeister.Model.Service
{
    public class ZauberService : ServiceBase
    {

        #region //----- EIGENSCHAFTEN ----

        public List<Model.Zauber> ZauberListe
        {
            get { return Liste<Zauber>(); }
        }

        #endregion

        #region //----- KONSTRUKTOR ----

        public ZauberService()
        {
        }

        #endregion

        #region //----- DATENBANKABFRAGEN ----

        #endregion

    }
}