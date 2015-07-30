using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//Eigene Usings
using Model = MeisterGeister.Model;

namespace MeisterGeister.Model.Service
{
    public class ZooBotService : ServiceBase
    {

        #region //----- EIGENSCHAFTEN ----

        public List<Model.Gebiet> ZooBotGebieteListe
        {
            get 
            {
                return Liste<Gebiet>();
            }
        }

        public List<Model.Pflanze> ZooBotPflanzenListe
        {
            get 
            {
                return Liste<Pflanze>();
            }
        }

        public List<Model.Landschaft> ZooBotLandschaftenListe
        {
            get 
            {
                return Liste<Landschaft>();
            }
        }

        public List<Model.Pflanze_Gebiet> ZooBotPflanze_GebietListe
        {
            get
            {
                return Liste<Pflanze_Gebiet>();
            }
        }

        #endregion

        #region //----- KONSTRUKTOR ----

        public ZooBotService()
        {
        }

        #endregion

        #region //----- DATENBANKABFRAGEN ----

        #endregion

    }
}

