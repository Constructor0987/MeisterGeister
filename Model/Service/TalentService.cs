﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//Eigene Usings
using Model = MeisterGeister.Model;

namespace MeisterGeister.Model.Service {
    public class TalentService : ServiceBase {

        #region //----- EIGENSCHAFTEN ----

        public List<Model.Talent> TalentListe
        {
            get { return Liste<Talent>(); }
        }

        #endregion

        #region //----- KONSTRUKTOR ----

        public TalentService() {         
        }

        #endregion

        #region //----- DATENBANKABFRAGEN ----        

        #endregion

    }
}





