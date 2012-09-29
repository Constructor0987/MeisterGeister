using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//Eigene Usings
using Model = MeisterGeister.Model;
using System.Diagnostics;

namespace MeisterGeister.Model.Service
{
    public class NotizService : ServiceBase
    {

        #region //----- EIGENSCHAFT ----

        public List<Model.Notizen> NotizListe
        {
            get { return Liste<Notizen>(); }
        }

        public Model.Notizen NotizAllgemein
        {
            get { return NotizListe.Where(n => n.Name == "Allgemein").FirstOrDefault(); }
        }

        public Model.Notizen NotizErfahrungen
        {
            get { return NotizListe.Where(n => n.Name == "Erfahrungen").FirstOrDefault(); }
        }

        #endregion

        #region //----- KONSTRUKTOR ----

        public NotizService()
        {
        }

        #endregion

        #region //----- DATENBANKABFRAGEN ----

        /// <summary>
        /// Wandelt alle FlowDocumente der Notizen in Strings um und speichert alles in die Datenbank.
        /// </summary>
        /// <returns></returns>
        public void SaveNotizen()
        {
            foreach (var item in NotizListe)
            {
                item.ParseFlowDoumentToText();
                Global.ContextNotizen.Update<Model.Notizen>(item);
            }
        }

        #endregion

    }
}