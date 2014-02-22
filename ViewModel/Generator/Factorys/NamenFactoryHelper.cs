using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeisterGeister.ViewModel.Generator.Container;

namespace MeisterGeister.ViewModel.Generator.Factorys
{
    abstract class NamenFactoryHelper
    {
        #region //---- FELDER ----
        // Die folgenden Strings sind für die Datenbank-Zuordnungen nötig
        //TODO Namnestypen auf GUID umstellen
        //TODO Datenbank umbauen -> Settings <-> Namen
        //TODO Index auf Namensart
        // Bei Strukturänderungen das Model updaten
        #region //---- KONSTANTEN AVENTURISCHE NAMEN ----
        public const string ACHAZNAMEN = "Achaz Namen";
        //public const string ALBERNISCHENAMEN = "Albernische Namen";
        //public const string ALMADISCHENAMEN = "Almadanische Namen";
        //public const string ANDERGASTSCHENAMEN = "Andergastsche Namen";
        //public const string ARANISCHENAMEN = "Aranische Namen";
        //public const string BORNLÄNDISCHENAMEN = "Bornländische Namen";
        //public const string CYCLOPENINSELNNAMEN = "Cyclopeninseln";
        //public const string ELEMISCHENAMEN = "Elem Oberschicht";
        //public const string ELFISCHENAMEN = "Elfische Namen";
        //public const string FERKINANAMEN = "Ferkina Namen";
        //public const string FJARNINGSCHENAMEN = "Fjarningsche Namen";
        //public const string GARETHISCHENAMEN = "Garethische Namen";
        //public const string GJALSKERLÄNDISCHENAMEN = "Gjalskerländische Namen";
        //public const string GOBLINISCHENAMEN = "Goblinische Namen";
        //public const string GROLMISCHENAMEN = "Grolmische Namen";
        public const string HOLBERKERNAMEN = "Holberker Namen";
        //public const string HORASIATCYCLOPENINSELNAMEN = "Horasiat/Cyclopeninseln";
        public const string HORASISCHENAMEN = "Horasische Namen";
        //public const string HÜGELZWERGISCHENAMEN = "Hügelzwergische Namen";
        //public const string MARASKANISCHENAMEN = "Maraskanische Namen";
        //public const string NIVESISCHENAMEN = "Nivesische Namen";
        //public const string NORBADISCHENAMEN = "Norbardische Namen";
        //public const string NORBADISCHESIPPENNAMEN = "Norbardische Namen (Sippenname)";
        //public const string NORDPROVINZENNAMEN = "Nordprovinzen";
        //public const string NOSTRISCHENAMEN = "Nostrische Namen";
        //public const string ORKISCHENAMEN = "Orkische Namen";
        //public const string ORKISCHESVELLTALNAMEN = "Orkische Namen (Svelltal)";
        //public const string SÜDLÄNDISCHENAMEN = "Südländische Namen";
        //public const string THORWALSCHENAMEN = "Thorwalsche Namen";
        //public const string TOCAMUYACNAMEN = "Tocamuyac Namen";
        public const string TROLLISCHENAMEN = "Trollische Namen";
        //public const string TROLLZACKERNAMEN = "Trollzacker Namen";
        //public const string TULAMIDISCHENAMEN = "Tulamidische Namen";
        public const string UTULUNAMEN = "Utulu Namen";
        //public const string WALDMENSCHENNAMEN = "Waldmenschen Namen";
        //public const string WEIDENERNAMEN = "Weidener Namen";
        public const string ZAHORINAMEN = "Zahori Namen";
        //public const string ZWERGISCHENAMEN = "Zwergische Namen";
        //public const string ZYKLOPÄISCHENAMEN = "Zyklopäische Namen";
        #endregion

        private static Dictionary<String, NamenFactory> _namenFactorys = _namenFactorys = new Dictionary<String, NamenFactory>();
        #endregion

        #region //---- Klassenmethoden ----
        private static NamenFactory InstantiateFactory(string namenstyp)
        {
            switch (namenstyp)
            {
                #region /---- Aventurische Namen ----
                /* 
                 * Achaz haben nur einen Vornamen
                 * Name ist nicht geschlechtsspezifisch
                 */
                case ACHAZNAMEN: return new NamenFactoryVorname(ACHAZNAMEN, true, true);

                /* Albernische Namen haben einen Vornamen, annotiert mit Vorname
                 * Vorname kann von bestimmten Garehtischen Vornamen abgeleitet sein
                 * Nachname annotiert mit Nachname
                 */
                //case ALBERNISCHENAMEN: return new AlbernischeNamenFactory();

                //case ALMADISCHENAMEN:

                //case ANDERGASTSCHENAMEN:
                
                //case ARANISCHENAMEN:
                
                //case BORNLÄNDISCHENAMEN:
                
                //case CYCLOPENINSELNNAMEN:
                
                //case ELEMISCHENAMEN:
                
                //case ELFISCHENAMEN:
                
                //case FERKINANAMEN:
                
                //case FJARNINGSCHENAMEN:
                
                //case GARETHISCHENAMEN:
                
                //case GJALSKERLÄNDISCHENAMEN:
                
                //case GOBLINISCHENAMEN:
                
                //case GROLMISCHENAMEN:
                
                /* 
                 * Holberker Namen haben einen Vornamen und Nachnamen
                 */
                case HOLBERKERNAMEN: return new NamenFactoryVornameNachname(HOLBERKERNAMEN);
                
                //case HORASIATCYCLOPENINSELNAMEN:

                /*
                 * Horasische Namen haben einen Vornamen und einen Nachnamen
                 * bei Adel je nach Rang "ya","de","di","du","della"
                 * Nachname bei Stadtfreien wie aufgeführt
                 */
                case HORASISCHENAMEN: return new HorasischeNamenFactory();
                
                //case HÜGELZWERGISCHENAMEN:
                
                //case MARASKANISCHENAMEN:
                
                //case NIVESISCHENAMEN:
                
                //case NORBADISCHENAMEN:
                
                //case NORBADISCHESIPPENNAMEN:
                
                //case NORDPROVINZENNAMEN:
                
                //case NOSTRISCHENAMEN:
                
                //case ORKISCHENAMEN:
                
                //case ORKISCHESVELLTALNAMEN:
                
                //case SÜDLÄNDISCHENAMEN:
                
                //case THORWALSCHENAMEN:
                
                //case TOCAMUYACNAMEN:
                
                /*
                 * Trolle haben nur einen Vornamen
                 * zusätzlich führen sie "Sohn/Tochter des <Vatername>"
                 */
                case TROLLISCHENAMEN: return new TrollischeNamenFactory();
                
                //case TROLLZACKERNAMEN:
                
                //case TULAMIDISCHENAMEN:

                /* 
                 * Utulu Namen haben nur einen Vornamen
                 * Name ist nicht geschlechtsspezifisch
                 */
                case UTULUNAMEN: return new NamenFactoryVorname(UTULUNAMEN, true, true);
                
                //case WALDMENSCHENNAMEN:
                
                //case WEIDENERNAMEN:

                /* 
                 * Zahori Namen haben einen Vornamen und Nachnamen 
                 * TODO Es sind Namensinformationen verfügbar
                 */
                case ZAHORINAMEN: return new NamenFactoryVornameNachname(ZAHORINAMEN, false, true);
                
                //case ZWERGISCHENAMEN:
                
                //case ZYKLOPÄISCHENAMEN:
                #endregion

                default: return null;
            }
        }

        public static NamenFactory GetFactory(String namenstyp)
        {
            NamenFactory nFactory;
            if (!_namenFactorys.TryGetValue(namenstyp, out nFactory))
            {
                nFactory = InstantiateFactory(namenstyp);
                if (nFactory != null)
                {
                    _namenFactorys[namenstyp] = nFactory;
                } else {
                    throw new NotImplementedException("Namenstyp " + namenstyp + " nicht verfügbar.");
                }
            }
            return nFactory;
        }
        #endregion
    }
}
