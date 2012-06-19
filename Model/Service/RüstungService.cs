using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//Eigene Usings
using Model = MeisterGeister.Model;
using System.Diagnostics;

namespace MeisterGeister.Model.Service
{
    public class RüstungService : ServiceBase
    {

        #region //----- EIGENSCHAFT ----

        public List<Model.Rüstung> RüstungListe
        {
            get { return Liste<Rüstung>(); }
        }

        #endregion

        #region //----- KONSTRUKTOR ----

        public RüstungService()
        {
        }

        #endregion

        #region //----- DATENBANKABFRAGEN ----



        #endregion

    }
}