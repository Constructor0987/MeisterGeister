using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeisterGeister.ViewModel.NscGenerator.Logic;
using MeisterGeister.Logic.General;

namespace MeisterGeister.ViewModel.NscGenerator.Factorys
{
    public abstract class NamenFactory
    {
        #region /---- Konstanten für DB-Abfragen ----
        public const string NAMENSARTVORNAMEN = "Vorname";
        #endregion

        #region /---- Felder ----
        protected string _informationenNamen; //später FlowDocuments nutzen; ggf. externe Ressource
        #endregion

        #region /---- Eigenschaften ----
        public bool VornamenWeiblichFürAlle { get; protected set; }
        public bool StandHatAuswirkung { get; protected set; }
        public bool GeneriertOrtsnamen { get; protected set; }
        public bool InformationenNamenVerfügbar { get; protected set; }
        public string Namenstyp { get; protected set; } //auf GUID umstellen
        #endregion

        #region /---- Konstruktor ----
        public NamenFactory(string namenstyp, bool vornamenWeiblichFürAlle = false, bool informationenNamenVerfügbar = false, bool generiertOrtsnamen = false)
        {
            this.Namenstyp = namenstyp;
            this.VornamenWeiblichFürAlle = vornamenWeiblichFürAlle;
            this.InformationenNamenVerfügbar = informationenNamenVerfügbar;
            this.GeneriertOrtsnamen = generiertOrtsnamen;
        }
        public abstract void InitListen();
        #endregion

        #region //---- Instanzmethoden ----
        public abstract PersonNurName GetPersonNurName(Geschlecht geschlecht, Stand stand = Stand.stadtfrei);
        public abstract string GetName(Geschlecht geschlecht, Stand stand = Stand.stadtfrei);

        public virtual string GeneriereOrtsname()
        {
            return "";
        }
        #endregion
    }

    public class NamenFactoryVorname : NamenFactory
    {
        #region /---- Felder ----
        protected List<string> _vornamenMännlich = new List<String>();
        protected List<string> _vornamenWeiblich = new List<String>();
        #endregion

        #region /---- Konstruktor ----
        public NamenFactoryVorname(string namenstyp, bool vornamenWeiblichFürAlle = false, bool informationenNamenVerfügbar = false, bool generiertOrtsnamen = false) :
            base(namenstyp, vornamenWeiblichFürAlle, informationenNamenVerfügbar, generiertOrtsnamen)
        {
            this.InitListen();
        }

        public override void InitListen()
        {
            List<Model.Name> namensliste = Global.ContextHeld.LoadNamenByNamenstyp(Namenstyp);
            if (VornamenWeiblichFürAlle)
            {
                _vornamenWeiblich.AddRange(namensliste.Where(n => n.Art == NAMENSARTVORNAMEN).Select(n => n.Name1));
            }
            else
            {
                _vornamenWeiblich.AddRange(namensliste.Where(n => n.Art == NAMENSARTVORNAMEN && (n.Geschlecht == "w" || n.Geschlecht == null)).Select(n => n.Name1));
                _vornamenMännlich.AddRange(namensliste.Where(n => n.Art == NAMENSARTVORNAMEN && (n.Geschlecht == "m" || n.Geschlecht == null)).Select(n => n.Name1));
            }
        }
        #endregion

        #region /---- Instanzmethoden ----
        public override PersonNurName GetPersonNurName(Geschlecht geschlecht, Stand stand = Stand.stadtfrei)
        {
            return new PersonNurName(this.GetName(geschlecht, stand), string.Empty, Namenstyp, geschlecht, stand);
        }

        public override string GetName(Geschlecht geschlecht, Stand stand = Stand.stadtfrei)
        {
            if (VornamenWeiblichFürAlle || geschlecht == Geschlecht.weiblich)
            {
                return _vornamenWeiblich[RandomNumberGenerator.Generator.Next(_vornamenWeiblich.Count())];
            }
            else // Geschlecht kann nur männlich sein
            {
                return _vornamenMännlich[RandomNumberGenerator.Generator.Next(_vornamenMännlich.Count())];
            }
        }
        #endregion
    }

    public class NamenFactoryVornameNachname : NamenFactoryVorname
    {
        #region /---- Felder ----
        protected List<string> _nachnamen = new List<String>();
        #endregion

        #region /---- Konstruktor ----
        public NamenFactoryVornameNachname(String namenstyp, bool vornamenWeiblichFürAlle = false, bool informationenNamenVerfügbar = false, bool generiertOrtsnamen = false) :
            base(namenstyp, vornamenWeiblichFürAlle, informationenNamenVerfügbar, generiertOrtsnamen){}

        public override void InitListen()
        {
            base.InitListen();
        }
        #endregion

        #region /---- Instanzmethoden ----
        //public override PersonNurName getName(Geschlecht geschlecht, Stand stand)
        #endregion
    }
}
