using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeisterGeister.ViewModel.NscGenerator.Logic;

namespace MeisterGeister.ViewModel.NscGenerator.Factorys
{
    abstract class NamenFactory
    {
        #region //---- FELDER ----
        //Die folgenden Strings sind für die Zuordnung Namenstyp.Name -> Factory nötig
        const string THORWALERNAMEN = "Thorwaler Namen";

        protected static Dictionary<string, NamenFactory> _namenFactorys = new Dictionary<string, NamenFactory>();

        //Vorgaben für die Namensfactoys; evtl. unterscheiden in drei Grundklassen: 2x nur Vorname, Vorname + Nachname
        protected List<String> _vornamenMännlich = new List<string>();
        protected List<String> _vornamenWeiblich = new List<string>();
        #endregion

        #region //---- Klassenmethoden ----
        protected static NamenFactory InstantiateFactory(string namenstyp)
        {
            switch (namenstyp)
            {
                case THORWALERNAMEN:
                default: throw new NotImplementedException("Namenstyp "+namenstyp+" nicht verfügbar.");
            }
        }

        public static NamenFactory GetFactory(string namenstyp)
        {
            NamenFactory nFactory;
            if (!_namenFactorys.TryGetValue(namenstyp, out nFactory))
            {
                nFactory = InstantiateFactory(namenstyp);
                if (nFactory!=null) _namenFactorys[namenstyp] = nFactory;
            }
            return nFactory;
        }
        #endregion

        #region //---- Instanzmethoden ----
        public abstract PersonNurName GetName();
        #endregion
    }

}
