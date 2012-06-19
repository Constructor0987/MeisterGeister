using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//Eigene Usings
using Model = MeisterGeister.Model;
using System.Diagnostics;

namespace MeisterGeister.Model.Service
{
    public class SchildService : ServiceBase
    {

        #region //----- EIGENSCHAFT ----

        public List<Model.Schild> SchildListe
        {
            get { return Liste<Schild>(); }
        }

        #endregion

        #region //----- KONSTRUKTOR ----

        public SchildService()
        {
        }

        #endregion

        #region //----- DATENBANKABFRAGEN ----



        #endregion

    }
}