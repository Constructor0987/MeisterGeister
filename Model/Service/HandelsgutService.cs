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
            // TODO: Mit Setting Filter extrem langsam (2-3 Mal länger)
            //get { return Liste<Handelsgut>().Where(s => s.Handelsgut_Setting.Any(a_s => Setting.AktiveSettings.Contains(a_s.Setting))).ToList(); }
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

