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
        //auf GUID umstellen
        //Aventurische Namen
        const string HOLBERKERNAMEN = "Holberker Namen";
        const string UTULUNAMEN = "Utulu Namen";
        const string ZAHORINAMEN = "Zahori Namen";

        private static Dictionary<String, NamenFactory> _namenFactorys = _namenFactorys = new Dictionary<String, NamenFactory>();
        #endregion
        
        #region //---- Klassenmethoden ----
        private static NamenFactory InstantiateFactory(string namenstyp)
        {
            switch (namenstyp)
            {
                //Aventurische Namen
                case HOLBERKERNAMEN: return new NamenFactoryVornameNachname(HOLBERKERNAMEN);
                case UTULUNAMEN: return new NamenFactoryVorname(UTULUNAMEN, true);
                case ZAHORINAMEN: return new NamenFactoryVornameNachname(ZAHORINAMEN);
                default: throw new NotImplementedException("Namenstyp " + namenstyp + " nicht verfügbar.");
            }
        }

        public static NamenFactory GetFactory(String namenstyp)
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
    }
}
