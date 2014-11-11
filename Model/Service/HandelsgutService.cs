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
            get 
            {
                string sql = @"SELECT Handelsgut.* FROM Handelsgut INNER JOIN Handelsgut_Setting ON Handelsgut.HandelsgutGUID = Handelsgut_Setting.HandelsgutGUID
                                WHERE Handelsgut_Setting.SettingGUID IN (SELECT SettingGUID FROM Setting WHERE Aktiv = 1)";
                return Context.ExecuteStoreQuery<Handelsgut>(sql).ToList<Handelsgut>();
            }
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

