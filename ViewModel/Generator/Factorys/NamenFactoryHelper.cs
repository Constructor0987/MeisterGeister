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
        public const string ALBERNISCHENAMEN = "Albernische Namen";
        public const string ALMADISCHENAMEN = "Almadanische Namen";
        public const string ANDERGASTSCHENAMEN = "Andergastsche Namen";
        public const string ARANISCHENAMEN = "Aranische Namen";
        public const string BORNLÄNDISCHENAMEN = "Bornländische Namen";
        //public const string CYCLOPENINSELNNAMEN = "Cyclopeninseln";
        //public const string ELEMISCHENAMEN = "Elem Oberschicht";
        public const string ELFISCHENAMEN = "Elfische Namen";
        public const string FERKINANAMEN = "Ferkina Namen";
        public const string FJARNINGSCHENAMEN = "Fjarningsche Namen";
        public const string GARETHISCHENAMEN = "Garethische Namen";
        public const string GJALSKERLÄNDISCHENAMEN = "Gjalskerländische Namen";
        public const string GOBLINISCHENAMEN = "Goblinische Namen";
        public const string GROLMISCHENAMEN = "Grolmische Namen";
        public const string HOLBERKERNAMEN = "Holberker Namen";
        //public const string HORASIATCYCLOPENINSELNAMEN = "Horasiat/Cyclopeninseln";
        public const string HORASISCHENAMEN = "Horasische Namen";
        public const string HÜGELZWERGISCHENAMEN = "Hügelzwergische Namen";
        public const string MARASKANISCHENAMEN = "Maraskanische Namen";
        public const string NIVESISCHENAMEN = "Nivesische Namen";
        public const string NORBADISCHENAMEN = "Norbardische Namen";
        public const string NOVADISCHENAMEN = "Novadische Namen";
        //public const string NORDPROVINZENNAMEN = "Nordprovinzen";
        public const string NOSTRISCHENAMEN = "Nostrische Namen";
        public const string ORKISCHENAMEN = "Orkische Namen";
        public const string ORKISCHESVELLTALNAMEN = "Orkische Namen (Svelltal)";
        public const string SÜDLÄNDISCHENAMEN = "Südländische Namen";
        public const string THORWALSCHENAMEN = "Thorwalsche Namen";
        public const string TOCAMUYACNAMEN = "Tocamuyac Namen";
        public const string TROLLISCHENAMEN = "Trollische Namen";
        public const string TROLLZACKERNAMEN = "Trollzacker Namen";
        public const string TULAMIDISCHENAMEN = "Tulamidische Namen";
        public const string UTULUNAMEN = "Utulu Namen";
        public const string WALDMENSCHENNAMEN = "Waldmenschen Namen";
        public const string WEIDENERNAMEN = "Weidener Namen";
        public const string ZAHORINAMEN = "Zahori Namen";
        public const string ZWERGISCHENAMEN = "Zwergische Namen";
        public const string ZWERGISCHEVORNAMEN = "Zwergische Vornamen";
        public const string ZYKLOPÄISCHENAMEN = "Zyklopäische Namen";
        #endregion

        private static Dictionary<String, NamenFactory> _namenFactorys = _namenFactorys = new Dictionary<String, NamenFactory>();
        #endregion

        #region //---- Klassenmethoden ----
        private static NamenFactory InstantiateFactory(string namenstyp)
        {
            switch (namenstyp)
            {
                #region /---- Aventurische Namen ----
                case ACHAZNAMEN: return new AchazNamenFactory();

                case ALBERNISCHENAMEN: return new AlbernischeNamenFactory();

                case ALMADISCHENAMEN: return new AlmadanischeNamenFactory();

                case ANDERGASTSCHENAMEN: return new NamenFactoryVornameNachname(ANDERGASTSCHENAMEN, false, true);

                case ARANISCHENAMEN: return new AranischeNamenFactory();

                case BORNLÄNDISCHENAMEN: return new BornländischeNamenFactory();
                
                //case CYCLOPENINSELNNAMEN:
                
                //case ELEMISCHENAMEN:

                case ELFISCHENAMEN: return new ElfischeNamenFactory();

                case FERKINANAMEN: return new FerkinaNamenFactory();

                case FJARNINGSCHENAMEN: return new NamenFactoryVorname(FJARNINGSCHENAMEN, false, true);

                case GARETHISCHENAMEN: return new GarethischeNamenFactory();

                case GJALSKERLÄNDISCHENAMEN: return new GjalskerländischeNamenFactory();

                case GOBLINISCHENAMEN: return new NamenFactoryVorname(GOBLINISCHENAMEN, false, true);

                case GROLMISCHENAMEN: return new NamenFactoryVorname(GROLMISCHENAMEN, false, true);
                
                case HOLBERKERNAMEN: return new NamenFactoryVornameNachname(HOLBERKERNAMEN);
                
                //case HORASIATCYCLOPENINSELNAMEN:

                case HORASISCHENAMEN: return new HorasischeNamenFactory();

                case HÜGELZWERGISCHENAMEN: return new HügelzwergischeNamenFactory();

                case MARASKANISCHENAMEN: return new MaraskanischeNamenFactory();

                case NIVESISCHENAMEN: return new NivesischeNamenFactory();

                case NORBADISCHENAMEN: return new NorbardischeNamenFactory();

                case NOVADISCHENAMEN: return new NovadischeNamenFactory();
                
                //case NORDPROVINZENNAMEN:

                case NOSTRISCHENAMEN: return new NostrischeNamenFactory();

                case ORKISCHENAMEN: return new OrkischeNamenFactory();

                case ORKISCHESVELLTALNAMEN: return new OrkischeSvellttalNamenFactory();

                case SÜDLÄNDISCHENAMEN: return new SüdländischeNamenFactory();

                case THORWALSCHENAMEN: return new ThorwalscheNamenFactory();

                case TOCAMUYACNAMEN: return new NamenFactoryVorname(TOCAMUYACNAMEN, true, true);
                
                case TROLLISCHENAMEN: return new TrollischeNamenFactory();

                case TROLLZACKERNAMEN: return new NamenFactoryVorname(TROLLZACKERNAMEN, false, true);

                case TULAMIDISCHENAMEN: return new TulamidischeNamenFactory();

                case UTULUNAMEN: return new NamenFactoryVorname(UTULUNAMEN, true, true);

                case WALDMENSCHENNAMEN: return new WaldmenschenNamenFactory();

                case WEIDENERNAMEN: return new WeidenerNamenFactory();

                case ZAHORINAMEN: return new NamenFactoryVornameNachname(ZAHORINAMEN, false, true);

                case ZWERGISCHENAMEN: return new ZwergischeNamenFactory();

                case ZWERGISCHEVORNAMEN: return new ZwergischeVornamenFactory();

                case ZYKLOPÄISCHENAMEN: return new ZyklopäischeNamenFactory();
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
