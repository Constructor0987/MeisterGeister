using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//Eigene Usings
using Model = MeisterGeister.Model;

namespace MeisterGeister.Model.Service
{
    public class HandelsgutService : ServiceBase
    {

        #region //----- EIGENSCHAFTEN ----

        public List<Model.Handelsgut> HandelsgüterListe
        {
            get { return Liste<Handelsgut>(); }
        }

        #endregion

        #region //----- KONSTRUKTOR ----

        public HandelsgutService()
        {
        }

        #endregion

        #region //----- DATENBANKABFRAGEN ----

        #endregion

    }
}

