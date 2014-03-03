using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeisterGeister.ViewModel.Generator.Container;
using MeisterGeister.Logic.General;

namespace MeisterGeister.ViewModel.Generator.Factorys
{
    public abstract class NamenFactory
    {
        #region //---- Konstanten für DB-Abfragen ----
        public const string NAMENSARTVORNAMEN = "Vorname";
        public const string NAMENSARTNACHNAMEN = "Nachname";
        #endregion

        #region //---- Felder ----
        protected PersonNurName _tempPerson;
        #endregion

        #region //---- Eigenschaften ----
        public bool VornamenWeiblichFürAlle { get; protected set; }
        public bool GeneriertOrtsnamen { get; protected set; }
        public bool GeneriertNamensbedeutungen { get; protected set; }
        public bool InformationenNamenVerfügbar { get; protected set; }
        public string Namenstyp { get; protected set; } //auf GUID umstellen
        #endregion

        #region //---- Konstruktor ----
        public NamenFactory(string namenstyp, bool vornamenWeiblichFürAlle = false, bool informationenNamenVerfügbar = false, bool generiertNamensbedeutungen = false, bool generiertOrtsnamen = false)
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
        public NamenFactoryVorname(string namenstyp, bool vornamenWeiblichFürAlle = false, bool informationenNamenVerfügbar = false, bool generiertNamensbedeutungen = false, bool generiertOrtsnamen = false) :
            base(namenstyp, vornamenWeiblichFürAlle, informationenNamenVerfügbar, generiertNamensbedeutungen, generiertOrtsnamen)
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
        #region //---- Felder ----
        protected List<string> _nachnamen = new List<String>();
        #endregion

        #region //---- Konstruktor ----
        public NamenFactoryVornameNachname(string namenstyp, bool vornamenWeiblichFürAlle = false, bool informationenNamenVerfügbar = false, bool generiertNamensbedeutungen = false, bool generiertOrtsnamen = false) :
            base(namenstyp, vornamenWeiblichFürAlle, informationenNamenVerfügbar, generiertNamensbedeutungen, generiertOrtsnamen)
        {
            List<Model.Name> namensliste = Global.ContextHeld.LoadNamenByNamenstyp(Namenstyp);
            _nachnamen.AddRange(namensliste.Where(n => n.Art == NAMENSARTNACHNAMEN).Select(n => n.Name1));
        }
        #endregion

        #region //---- Instanzmethoden ----
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
                    _vornamenMännlich[RandomNumberGenerator.Generator.Next(_vornamenMännlich.Count())],
                    _nachnamen[RandomNumberGenerator.Generator.Next(_nachnamen.Count())]);
            }
        }
        #endregion
    }

    #region //---- Aventurische Namen Factorys ----

    public class AchazNamenFactory : NamenFactoryVorname
    {
        #region //---- Felder ----
        private List<string> _namensbedeutungen = new List<string>();
        private int _selectedName;
        #endregion

        #region //---- Konstruktor ----
        public AchazNamenFactory() :
            base(NamenFactoryHelper.ACHAZNAMEN, true, true, true)
        {
            _namensbedeutungen.AddRange(Global.ContextHeld.LoadNamenByNamenstyp(Namenstyp).Where(n => n.Art == NAMENSARTVORNAMEN).Select(n => n.Bedeutung));
        }
        #endregion

        #region //---- Instanzmethoden ----
        public override void RegeneratePersonNurName(ref PersonNurName person, Geschlecht geschlecht = Geschlecht.weiblich, Stand stand = Stand.stadtfrei)
        {
            person.Namenstyp = this.Namenstyp;
            person.Geschlecht = geschlecht;
            person.Stand = stand;

            _selectedName = RandomNumberGenerator.Generator.Next(_vornamenWeiblich.Count());
            person.Name = _vornamenWeiblich[_selectedName];
            person.Namensbedeutung = _namensbedeutungen[_selectedName];
        }
        #endregion
    }

    public class TrollischeNamenFactory : NamenFactoryVorname
    {

        #region //---- Konstruktor ----
        public TrollischeNamenFactory() :
            base(NamenFactoryHelper.TROLLISCHENAMEN,false,true)
        {

        }
        #endregion

        #region //---- Instanzmethoden ----
        public override void RegeneratePersonNurName(ref PersonNurName person, Geschlecht geschlecht = Geschlecht.weiblich, Stand stand = Stand.stadtfrei)
        {
            person.Namenstyp = this.Namenstyp;
            person.Geschlecht = geschlecht;
            person.Stand = stand;

            if (geschlecht == Geschlecht.weiblich)
            {
                person.Name = string.Format("{0} Tochter des {1}",
                    _vornamenWeiblich[RandomNumberGenerator.Generator.Next(_vornamenWeiblich.Count())],
                    _vornamenMännlich[RandomNumberGenerator.Generator.Next(_vornamenMännlich.Count())]);
            }
            else
            {
                person.Name = string.Format("{0} Sohn des {1}",
                    _vornamenMännlich[RandomNumberGenerator.Generator.Next(_vornamenMännlich.Count())],
                    _vornamenMännlich[RandomNumberGenerator.Generator.Next(_vornamenMännlich.Count())]);
            }
        }
        #endregion
    }

    public class HorasischeNamenFactory : NamenFactoryVornameNachname
    {
        #region //---- Felder ----
        protected List<string> _adelskürzel = new List<String>(new string[] { "ya", "da", "de", "di", "du", "della" });
        #endregion


        #region //---- Konstruktor ----
        public HorasischeNamenFactory() :
            base(NamenFactoryHelper.HORASISCHENAMEN, false, true, false, false)
        {

        }
        #endregion

        #region //---- Instanzmethoden ----
        public override void RegeneratePersonNurName(ref PersonNurName person, Geschlecht geschlecht = Geschlecht.weiblich, Stand stand = Stand.stadtfrei)
        {
            person.Namenstyp = this.Namenstyp;
            person.Geschlecht = geschlecht;
            person.Stand = stand;

            switch (stand) {
                // Unfrei gibt es nicht; Landfreie haben nur einen Vornamen
                case Stand.stadtfrei:
                    setzeStadtfreienname(ref person);
                    break;
                case Stand.adelig:
                    setzeAdelsname(ref person);
                    break;
                case Stand.unfrei:
                case Stand.landfrei:
                default:
                    setzeLandfreienname(ref person);
                    break;
            }
        }

        private void setzeAdelsname(ref PersonNurName person)
        {
            if (person.Geschlecht == Geschlecht.weiblich)
            {
                person.Name = string.Format("{0} {1} {2}",
                    _vornamenWeiblich[RandomNumberGenerator.Generator.Next(_vornamenWeiblich.Count())],
                    _adelskürzel[RandomNumberGenerator.Generator.Next(_adelskürzel.Count())],
                    _nachnamen[RandomNumberGenerator.Generator.Next(_nachnamen.Count())]);
            }
            else // Geschlecht kann nur männlich sein
            {
                person.Name = string.Format("{0} {1} {2}",
                    _vornamenMännlich[RandomNumberGenerator.Generator.Next(_vornamenMännlich.Count())],
                    _adelskürzel[RandomNumberGenerator.Generator.Next(_adelskürzel.Count())],
                    _nachnamen[RandomNumberGenerator.Generator.Next(_nachnamen.Count())]);
            }
        }

        private void setzeStadtfreienname(ref PersonNurName person)
        {
            if (person.Geschlecht == Geschlecht.weiblich)
            {
                person.Name = string.Format("{0} {1}",
                    _vornamenWeiblich[RandomNumberGenerator.Generator.Next(_vornamenWeiblich.Count())],
                    _nachnamen[RandomNumberGenerator.Generator.Next(_nachnamen.Count())]);
            }
            else // Geschlecht kann nur männlich sein
            {
                person.Name = string.Format("{0} {1}",
                    _vornamenMännlich[RandomNumberGenerator.Generator.Next(_vornamenMännlich.Count())],
                    _nachnamen[RandomNumberGenerator.Generator.Next(_nachnamen.Count())]);
            }
        }

        private void setzeLandfreienname(ref PersonNurName person)
        {
            if (person.Geschlecht == Geschlecht.weiblich)
            {
                person.Name = string.Format("{0}",
                    _vornamenWeiblich[RandomNumberGenerator.Generator.Next(_vornamenWeiblich.Count())]);
            }
            else // Geschlecht kann nur männlich sein
            {
                person.Name = string.Format("{0}",
                    _vornamenMännlich[RandomNumberGenerator.Generator.Next(_vornamenMännlich.Count())]);
            }
        }
        #endregion
    }

    public class SüdländischeNamenFactory : NamenFactoryVornameNachname
    {
        #region //---- Felder ----
        private string weitereVornamen;
        #endregion

        #region //---- Konstruktor ----
        public SüdländischeNamenFactory() :
            base(NamenFactoryHelper.SÜDLÄNDISCHENAMEN, false, true)
        {
            // erzeuge mehr Nachnamen durch das ersetzen von -a/-o im Vornamen durch (uez)
           foreach (string neuerName in _vornamenMännlich.Where(n => n.EndsWith("o")))
           {
               _nachnamen.Add(neuerName.Remove(neuerName.Length - 1) + "(u)ez");
           }
           foreach (string neuerName in _vornamenWeiblich.Where(n => n.EndsWith("a")))
           {
               _nachnamen.Add(neuerName.Remove(neuerName.Length - 1) + "(u)ez");
           }
        }
        #endregion

        #region //---- Instanzmethoden ----
        public override void RegeneratePersonNurName(ref PersonNurName person, Geschlecht geschlecht = Geschlecht.weiblich, Stand stand = Stand.stadtfrei)
        {
            base.RegeneratePersonNurName(ref person, geschlecht, stand);

            
            if (RandomNumberGenerator.Generator.Next(2)==1)
            {
                // zwei weiterer Vorname
                weitereVornamen = (person.Geschlecht == Geschlecht.weiblich) ?
                    string.Format("{0} {1} ", _vornamenWeiblich[RandomNumberGenerator.Generator.Next(_vornamenWeiblich.Count())], _vornamenWeiblich[RandomNumberGenerator.Generator.Next(_vornamenWeiblich.Count())]) :
                    string.Format("{0} {1} ", _vornamenMännlich[RandomNumberGenerator.Generator.Next(_vornamenMännlich.Count())], _vornamenMännlich[RandomNumberGenerator.Generator.Next(_vornamenMännlich.Count())]);
            }
            else
            {
                //ein weiterer Vorname
                weitereVornamen = (person.Geschlecht == Geschlecht.weiblich) ?
                    string.Format("{0} ", _vornamenWeiblich[RandomNumberGenerator.Generator.Next(_vornamenWeiblich.Count())]) :
                    string.Format("{0} ", _vornamenMännlich[RandomNumberGenerator.Generator.Next(_vornamenMännlich.Count())]);
            }
            person.Name = weitereVornamen + person.Name;
        }
        #endregion
    }

    //public class albernischenamenfactory : namenfactoryvornamenachname
    //{
    //    #region //---- konstruktor ----
    //    public albernischenamenfactory() :
    //        base(namenfactoryhelper.albernischenamen, false, true, false, true)
    //    {

    //    }
    //    #endregion

    //    #region //---- instanzmethoden ----
    //    public override void regeneratepersonnurname(ref personnurname person, geschlecht geschlecht = geschlecht.weiblich, stand stand = stand.stadtfrei)
    //    {
    //        person.namenstyp = this.namenstyp;
    //        person.geschlecht = geschlecht;
    //        person.stand = stand;
    //        if (vornamenweiblichfüralle || geschlecht == geschlecht.weiblich)
    //        {
    //            person.name = string.format("{0} {1}",
    //                _vornamenweiblich[randomnumbergenerator.generator.next(_vornamenweiblich.count())],
    //                _nachnamen[randomnumbergenerator.generator.next(_nachnamen.count())]);
    //        }
    //        else // geschlecht kann nur männlich sein
    //        {
    //            person.name = string.format("{0} {1}",
    //                _vornamenmännlich[randomnumbergenerator.generator.next(_vornamenmännlich.count())],
    //                _nachnamen[randomnumbergenerator.generator.next(_nachnamen.count())]);
    //        }
    //    }
    //    #endregion
    //}
    #endregion
}