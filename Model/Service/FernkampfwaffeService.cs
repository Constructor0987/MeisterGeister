using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//Eigene Usings
using Model = MeisterGeister.Model;
using System.Diagnostics;

namespace MeisterGeister.Model.Service
{
    public class FernkampfwaffeService : ServiceBase
    {

        #region //----- EIGENSCHAFT ----

        public List<Model.Fernkampfwaffe> FernkampfwaffeListe
        {
            get { return Liste<Fernkampfwaffe>(); }
        }

        #endregion

        #region //----- KONSTRUKTOR ----

        public FernkampfwaffeService()
        {
        }

        #endregion

        #region //----- DATENBANKABFRAGEN ----

        

        #endregion

    }
}