using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//Eigene Usings
using Model = MeisterGeister.Model;
using System.Diagnostics;

namespace MeisterGeister.Model.Service
{
    public class MenuLinkService : ServiceBase
    {

        #region //----- EIGENSCHAFT ----

        public List<Model.MenuLink> MenuLinkListe
        {
            get { return Liste<MenuLink>(); }
        }

        #endregion

        #region //----- KONSTRUKTOR ----

        public MenuLinkService()
        {
        }

        #endregion

        #region //----- DATENBANKABFRAGEN ----



        #endregion

    }
}