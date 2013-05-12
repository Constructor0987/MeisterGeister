using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeisterGeister.ViewModel.NscGenerator.Logic;

namespace MeisterGeister.ViewModel.NscGenerator.Factorys
{
    public abstract class NamenFactory
    {
        #region /---- Felder ----
        protected String _informationenNamen; //später FlowDocuments nutzen; ggf. externe Ressource
        #endregion

        #region /---- Eigenschaften ----
        public bool VornamenWeiblichFürAlle { get; protected set; }
        public bool GeneriertOrtsnamen { get; protected set; }
        public bool InformationenNamenVerfügbar { get; protected set; }
        public String Namenstyp { get; protected set; } //auf GUID umstellen
        #endregion

        #region //---- Instanzmethoden ----
        public abstract PersonNurName GetName(Geschlecht geschlecht, Stand stand);
        
        public String GetOrtsname()
        {
            return "";
        }
        #endregion
    }

    public abstract class NamenFactoryVorname : NamenFactory
    {
        #region /---- Felder ----
        protected List<String> _vornamenMännlich = new List<String>();
        protected List<String> _vornamenWeiblich = new List<String>();
        #endregion

        #region /---- Konstruktor ----
        public NamenFactoryVorname(String namenstyp, bool vornamenWeiblichFürAlle = false, bool informationenNamenVerfügbar = false, bool generiertOrtsnamen = false)
        {
            this.Namenstyp = namenstyp;
            this.VornamenWeiblichFürAlle = vornamenWeiblichFürAlle;
            this.InformationenNamenVerfügbar = informationenNamenVerfügbar;
            this.GeneriertOrtsnamen = generiertOrtsnamen;
            //TODO: Listen der Vornamen aus der Datenbank befüllen, vornameWeiblichFürAlle beachten
            // Bedeutungen des Namen mitgenerieren -> Unterklasse
        }
        #endregion

        #region /---- Instanzmethoden ----
        // getName(Geschlecht geschlecht, Stand stand)) überschreiben
        #endregion
    }

    public abstract class NamenFactoryVornameNachname : NamenFactoryVorname
    {
        #region /---- Felder ----
        protected List<String> _nachnamen = new List<String>();
        #endregion

        #region /---- Konstruktor ----
        public NamenFactoryVornameNachname(String namenstyp, bool vornamenWeiblichFürAlle = false, bool informationenNamenVerfügbar = false, bool generiertOrtsnamen = false) :
            base(namenstyp, vornamenWeiblichFürAlle, informationenNamenVerfügbar, generiertOrtsnamen)
        {
            //TODO: Listen der Nachnamen aus der Datenbank befüllen, vornameWeiblichFürAlle beachten
        }
        #endregion

        #region /---- Instanzmethoden ----
        // getName(Geschlecht geschlecht, Stand stand)) überschreiben
        #endregion
    }
}
