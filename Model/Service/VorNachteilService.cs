﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//Eigene Usings
using Model = MeisterGeister.Model;

namespace MeisterGeister.Model.Service
{
    public class VorNachteilService : ServiceBase
    {

        #region //----- EIGENSCHAFTEN ----

        public List<Model.VorNachteil> VorNachteilListe
        {
            get { return Liste<VorNachteil>(); }
        }

        #endregion

        #region //----- KONSTRUKTOR ----

        public VorNachteilService()
        {
        }

        #endregion

        #region //----- DATENBANKABFRAGEN ----

        #endregion

    }
}




