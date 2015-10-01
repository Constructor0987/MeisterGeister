using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
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

        #endregion

        #region //----- KONSTRUKTOR ----

        public ZooBotService()
        {
        }

        #endregion

        #region //----- DATENBANKABFRAGEN ----

        /// <summary>
        /// Gibt eine Liste von Gebieten zurück in denen der Punkt (in DG-Koordinaten) enthalten ist.
        /// </summary>
        public IEnumerable<Gebiet> GetGebiete(Point dgCoords, double tolerance = 0)
        {
            //Das geht solange die Polygone nicht über die 180 Grad Grenze gehen.
            string sql = @"select P.*
            from Gebiet G
            INNER JOIN Gebiet_Polygon GP ON G.GebietGUID=GP.GebietGUID
            INNER JOIN Polygon P ON GP.PolygonGUID=P.PolygonGUID
            where G.[Left]<{0} and G.[Right]>{1} and G.[Top]>{2}  and G.[Bot]<{3}
            and P.[Left]<{0} and P.[Right]>{1} and P.[Top]>{2}  and P.[Bot]<{3}";
            sql = String.Format(System.Globalization.CultureInfo.InvariantCulture, sql, dgCoords.X+tolerance, dgCoords.X-tolerance, dgCoords.Y-tolerance, dgCoords.Y+tolerance);
            var l = Context.ExecuteStoreQuery<Polygon>(sql, "Polygon", System.Data.Entity.Core.Objects.MergeOption.PreserveChanges, null).ToList<Polygon>();
            //Hittest
            var l2 = l.Where(p => p.Contains(dgCoords, tolerance)).SelectMany(p => p.Gebiet).Distinct();
            return l2;
        }


        #endregion

    }
}

