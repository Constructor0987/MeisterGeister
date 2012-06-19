using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//Eigene Usings
using Model = MeisterGeister.Model;
using System.Diagnostics;

namespace MeisterGeister.Model.Service
{
    public class RegelnService : ServiceBase
    {

        #region //----- EIGENSCHAFT ----

        public List<Model.Regeln> RegelnListe
        {
            get { return Liste<Regeln>(); }
        }

        #endregion

        #region //----- KONSTRUKTOR ----

        public RegelnService()
        {
        }

        #endregion

        #region //----- DATENBANKABFRAGEN ----

        public Regeln LoadRegelByName(string rName)
        {
            var tmp = Context.Regeln.Where(regel => regel.Name == rName).FirstOrDefault();
            return tmp;
        }

        #endregion

    }
}