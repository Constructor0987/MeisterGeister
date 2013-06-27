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
        public const string NAMENSARTNACHNAMEN = "Nachname";
        #endregion

        #region /---- Felder ----
        protected string _informationenNamen; //später FlowDocuments nutzen; ggf. externe Ressource
        protected PersonNurName _tempPerson;
        #endregion

        #region /---- Eigenschaften ----
        public bool VornamenWeiblichFürAlle { get; protected set; }
        public bool StandHatAuswirkung { get; protected set; }
        public bool GeneriertOrtsnamen { get; protected set; }
        public bool GeneriertNamensbedeutungen { get; protected set; }
        public bool InformationenNamenVerfügbar { get; protected set; }
        public string Namenstyp { get; protected set; } //auf GUID umstellen
        #endregion

        #region /---- Konstruktor ----
        public NamenFactory(string namenstyp, bool vornamenWeiblichFürAlle = false, bool generiertNamensbedeutungen = false, bool informationenNamenVerfügbar = false, bool generiertOrtsnamen = false)
        {
            this.Namenstyp = namenstyp;
            this.VornamenWeiblichFürAlle = vornamenWeiblichFürAlle;
            this.GeneriertNamensbedeutungen = generiertNamensbedeutungen;
            this.InformationenNamenVerfügbar = informationenNamenVerfügbar;
            this.GeneriertOrtsnamen = generiertOrtsnamen;
            _tempPerson = new PersonNurName(this.Namenstyp, string.Empty, string.Empty);
        }
        #endregion

        #region //---- Instanzmethoden ----
        public abstract void RegeneratePersonNurName(ref PersonNurName person, Geschlecht geschlecht = Geschlecht.weiblich, Stand stand = Stand.stadtfrei);

        public virtual PersonNurName GeneratePersonNurName(Geschlecht geschlecht = Geschlecht.weiblich, Stand stand = Stand.stadtfrei)
        {
            PersonNurName person = new PersonNurName();
            RegeneratePersonNurName(ref person, geschlecht, stand);
            return person;
        }

        public virtual string GetName(Geschlecht geschlecht = Geschlecht.weiblich, Stand stand = Stand.stadtfrei)
        {
            RegeneratePersonNurName(ref _tempPerson, geschlecht, stand);
            return _tempPerson.Name;
        }

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
        public NamenFactoryVorname(string namenstyp, bool vornamenWeiblichFürAlle = false, bool generiertNamensbedeutungen = false, bool informationenNamenVerfügbar = false, bool generiertOrtsnamen = false) :
            base(namenstyp, vornamenWeiblichFürAlle, generiertNamensbedeutungen, informationenNamenVerfügbar, generiertOrtsnamen)
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
        public override void RegeneratePersonNurName(ref PersonNurName person, Geschlecht geschlecht = Geschlecht.weiblich, Stand stand = Stand.stadtfrei)
        {
            person.Namenstyp = this.Namenstyp;
            person.Geschlecht = geschlecht;
            person.Stand = stand;
            if (VornamenWeiblichFürAlle || geschlecht == Geschlecht.weiblich)
            {
                person.Name = _vornamenWeiblich[RandomNumberGenerator.Generator.Next(_vornamenWeiblich.Count())];
            }
            else // Geschlecht kann nur männlich sein
            {
                person.Name = _vornamenMännlich[RandomNumberGenerator.Generator.Next(_vornamenMännlich.Count())];
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
        public NamenFactoryVornameNachname(string namenstyp, bool vornamenWeiblichFürAlle = false, bool generiertNamensbedeutungen = false, bool informationenNamenVerfügbar = false, bool generiertOrtsnamen = false) :
            base(namenstyp, vornamenWeiblichFürAlle, generiertNamensbedeutungen, informationenNamenVerfügbar, generiertOrtsnamen)
        {
            List<Model.Name> namensliste = Global.ContextHeld.LoadNamenByNamenstyp(Namenstyp);
            _nachnamen.AddRange(namensliste.Where(n => n.Art == NAMENSARTNACHNAMEN).Select(n => n.Name1));
        }
        #endregion

        #region /---- Instanzmethoden ----
        public override void RegeneratePersonNurName(ref PersonNurName person, Geschlecht geschlecht = Geschlecht.weiblich, Stand stand = Stand.stadtfrei)
        {
            person.Namenstyp = this.Namenstyp;
            person.Geschlecht = geschlecht;
            person.Stand = stand;
            if (VornamenWeiblichFürAlle || geschlecht == Geschlecht.weiblich)
            {
                person.Name = string.Format("{0} {1}",
                    _vornamenWeiblich[RandomNumberGenerator.Generator.Next(_vornamenWeiblich.Count())],
                    _nachnamen[RandomNumberGenerator.Generator.Next(_nachnamen.Count())]);
            }
            else // Geschlecht kann nur männlich sein
            {
                person.Name = string.Format("{0} {1}",
                person.Name = _vornamenMännlich[RandomNumberGenerator.Generator.Next(_vornamenMännlich.Count())],
                _nachnamen[RandomNumberGenerator.Generator.Next(_nachnamen.Count())]);
            }
        }
        #endregion
    }
}
