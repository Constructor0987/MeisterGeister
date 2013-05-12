using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeisterGeister.ViewModel.NscGenerator.Logic;

namespace MeisterGeister.ViewModel.NscGenerator.Factorys
{
    abstract class NamenFactoryHelper
    {
        #region //---- FELDER ----
        // Die folgenden Strings sind für die Datenbank-Zuordnungen nötig
        const String THORWALERNAMEN = "Thorwaler Namen"; //auf GUID umstellen

        // 
        private static Dictionary<String, NamenFactoryHelper> _namenFactorys = new Dictionary<String, NamenFactoryHelper>();
        #endregion

        #region //---- Klassenmethoden ----
        private static NamenFactoryHelper InstantiateFactory(string namenstyp)
        {
            switch (namenstyp)
            {
                case THORWALERNAMEN:
                default: throw new NotImplementedException("Namenstyp "+namenstyp+" nicht verfügbar.");
            }
        }

        public static NamenFactoryHelper GetFactory(String namenstyp)
        {
            NamenFactoryHelper nFactory;
            if (!_namenFactorys.TryGetValue(namenstyp, out nFactory))
            {
                nFactory = InstantiateFactory(namenstyp);
                if (nFactory!=null) _namenFactorys[namenstyp] = nFactory;
            }
            return nFactory;
        }
        #endregion
    }

}
