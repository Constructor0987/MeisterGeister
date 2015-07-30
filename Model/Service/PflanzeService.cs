using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//Eigene Usings
using Model = MeisterGeister.Model;

namespace MeisterGeister.Model.Service
{
    public class PflanzeService : ServiceBase
    {

        #region //----- EIGENSCHAFTEN ----

        public List<Model.Pflanze> PflanzenListe
        {
            get 
            {
                // TODO: Funktioniert so leider noch nicht ganz, da Setting spezifische Daten nicht geladen werden, wie z.B. der Preis
//                string sql = @"SELECT Handelsgut.* FROM Handelsgut INNER JOIN Handelsgut_Setting ON Handelsgut.HandelsgutGUID = Handelsgut_Setting.HandelsgutGUID
//                                WHERE Handelsgut_Setting.SettingGUID IN (SELECT SettingGUID FROM Setting WHERE Aktiv = 1)";
//                return Context.ExecuteStoreQuery<Handelsgut>(sql).ToList<Handelsgut>();
                return Liste<Pflanze>();
            }
        }

        public List<Model.Gebiet> GebietListe
        {
            get
            {
                // TODO: Funktioniert so leider noch nicht ganz, da Setting spezifische Daten nicht geladen werden, wie z.B. der Preis
                //                string sql = @"SELECT Handelsgut.* FROM Handelsgut INNER JOIN Handelsgut_Setting ON Handelsgut.HandelsgutGUID = Handelsgut_Setting.HandelsgutGUID
                //                                WHERE Handelsgut_Setting.SettingGUID IN (SELECT SettingGUID FROM Setting WHERE Aktiv = 1)";
                //                return Context.ExecuteStoreQuery<Handelsgut>(sql).ToList<Handelsgut>();
                return Liste<Gebiet>();
            }
        }

        public List<Model.Landschaft> LandschaftenListe
        {
            get
            {
                // TODO: Funktioniert so leider noch nicht ganz, da Setting spezifische Daten nicht geladen werden, wie z.B. der Preis
                //                string sql = @"SELECT Handelsgut.* FROM Handelsgut INNER JOIN Handelsgut_Setting ON Handelsgut.HandelsgutGUID = Handelsgut_Setting.HandelsgutGUID
                //                                WHERE Handelsgut_Setting.SettingGUID IN (SELECT SettingGUID FROM Setting WHERE Aktiv = 1)";
                //                return Context.ExecuteStoreQuery<Handelsgut>(sql).ToList<Handelsgut>();
                return Liste<Landschaft>();
            }
        }

        #endregion

        #region //----- KONSTRUKTOR ----

        public PflanzeService()
        {
        }

        #endregion

        #region //----- DATENBANKABFRAGEN ----

        #endregion

    }
}

