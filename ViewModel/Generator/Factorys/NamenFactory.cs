using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeisterGeister.ViewModel.Generator.Container;
using MeisterGeister.Logic.General;
using MeisterGeister.Logic.Extensions;

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
        public virtual void RegeneratePersonNurName(ref PersonNurName person, Geschlecht geschlecht = Geschlecht.weiblich, Stand stand = Stand.stadtfrei)
        {
            person.Namenstyp = this.Namenstyp;
            person.Geschlecht = geschlecht;
            person.Stand = stand;
            RegenerateName(ref person);
        }

        public abstract void RegenerateName(ref PersonNurName person);

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
        public override void RegenerateName(ref PersonNurName person)
        {
            if (VornamenWeiblichFürAlle || person.Geschlecht == Geschlecht.weiblich)
            {
                person.Name = _vornamenWeiblich.RandomElement();
            }
            else // Geschlecht kann nur männlich sein
            {
                person.Name = _vornamenMännlich.RandomElement();
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
        public override void RegenerateName(ref PersonNurName person)
        {
            if (VornamenWeiblichFürAlle || person.Geschlecht == Geschlecht.weiblich)
            {
                person.Name = string.Format("{0} {1}",
                    _vornamenWeiblich.RandomElement(),
                    _nachnamen.RandomElement());
            }
            else // Geschlecht kann nur männlich sein
            {
                person.Name = string.Format("{0} {1}",
                    _vornamenMännlich.RandomElement(),
                    _nachnamen.RandomElement());
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
        public override void RegenerateName(ref PersonNurName person)
        {
            _selectedName = RandomNumberGenerator.Generator.Next(_vornamenWeiblich.Count());
            person.Name = _vornamenWeiblich.ElementAt(_selectedName);
            person.Namensbedeutung = _namensbedeutungen.ElementAt(_selectedName);
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
        public override void RegenerateName(ref PersonNurName person)
        {

            if (person.Geschlecht == Geschlecht.weiblich)
            {
                person.Name = string.Format("{0} Tochter des {1}",
                    _vornamenWeiblich.RandomElement(),
                    _vornamenMännlich.RandomElement());
            }
            else
            {
                person.Name = string.Format("{0} Sohn des {1}",
                    _vornamenMännlich.RandomElement(),
                    _vornamenMännlich.RandomElement());
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
        public override void RegenerateName(ref PersonNurName person)
        {
            switch (person.Stand) {
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
                    _vornamenWeiblich.RandomElement(),
                    _adelskürzel.RandomElement(),
                    _nachnamen.RandomElement());
            }
            else // Geschlecht kann nur männlich sein
            {
                person.Name = string.Format("{0} {1} {2}",
                    _vornamenMännlich.RandomElement(),
                    _adelskürzel.RandomElement(),
                    _nachnamen.RandomElement());
            }
        }

        private void setzeStadtfreienname(ref PersonNurName person)
        {
            if (person.Geschlecht == Geschlecht.weiblich)
            {
                person.Name = string.Format("{0} {1}",
                    _vornamenWeiblich.RandomElement(),
                    _nachnamen.RandomElement());
            }
            else // Geschlecht kann nur männlich sein
            {
                person.Name = string.Format("{0} {1}",
                    _vornamenMännlich.RandomElement(),
                    _nachnamen.RandomElement());
            }
        }

        private void setzeLandfreienname(ref PersonNurName person)
        {
            if (person.Geschlecht == Geschlecht.weiblich)
            {
                person.Name = string.Format("{0}",
                    _vornamenWeiblich.RandomElement());
            }
            else // Geschlecht kann nur männlich sein
            {
                person.Name = string.Format("{0}",
                    _vornamenMännlich.RandomElement());
            }
        }
        #endregion
    }

    public class SüdländischeNamenFactory : NamenFactoryVornameNachname
    {
        #region //---- Felder ----
        private string weitereVornamen;
        private delegate bool EmptyNameSelector();
        /**
         * Funktion, die wahr als Rückgabewert liefert, um leere Namen zu steuern.
         * Die Funktion wird bei Generierung von bis zu zwei weiteren Vornamen genutzt.
         * Durch den wiederholten Aufruf für jeden Namen ergibt sich eine Normalverteilung.
         */
        private EmptyNameSelector emptyNameSelector = () => RandomNumberGenerator.Generator.Next(2) == 1;
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
        public override void RegenerateName(ref PersonNurName person)
        {
            // erzeuge zunächst einen regulären Vor/Nachnamen
            base.RegenerateName(ref person);

            // erzeuge ein bis drei weitere Vornamen
            if (person.Geschlecht == Geschlecht.weiblich)
            {
                weitereVornamen = string.Format("{0} {1} {2} ",
                    _vornamenWeiblich.RandomElement(),
                    emptyNameSelector() ? "" : _vornamenWeiblich.RandomElement(),
                    emptyNameSelector() ? "" : _vornamenWeiblich.RandomElement());
            }
            else
            {
                weitereVornamen = string.Format("{0} {1} {2} ",
                    _vornamenMännlich.RandomElement(),
                    emptyNameSelector() ? "" : _vornamenMännlich.RandomElement(),
                    emptyNameSelector() ? "" : _vornamenMännlich.RandomElement());
            }

            // bereinige weitereVornamen und verkette
            person.Name = weitereVornamen.EntferneMehrfacheLeerzeichen() + person.Name;
        }
        #endregion
    }

    public class ZyklopäischeNamenFactory : NamenFactoryVornameNachname
    {
        #region //---- Felder ----
        protected List<string> _städtenamen = new List<String>(new string[] { "Akiras", "Sienna", "Merymakon", "Rhetis", "Garén", "Mura", "Drum", "Athyros", "Tul'ka'var", "Tyrakos", "Balträa", "Aryios", "Kutaki", "Ferein", "Rhun", "Putras", "Kemethis", "Ayodon", "Teremon", "Skebos", "Laryios", "Palakar", "Lyios", "Rhetis", "Garén" });
        protected List<string> _adelskürzel = new List<String>(new string[] { "dylli", "dyll" });
        #endregion

        #region //---- Konstruktor ----
        public ZyklopäischeNamenFactory() :
            base(NamenFactoryHelper.ZYKLOPÄISCHENAMEN, false, true) { }
        #endregion

        #region //---- Instanzmethoden ----
        public override void RegenerateName(ref PersonNurName person)
        {
            switch (person.Stand)
            {
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
                person.Name = string.Format("{0} {1} A' {2} {3} {4}",
                    _vornamenWeiblich.RandomElement(),
                    _nachnamen.RandomElement(),
                    _städtenamen.RandomElement(),
                    _adelskürzel.RandomElement(),
                    _städtenamen.RandomElement());
            }
            else // Geschlecht kann nur männlich sein
            {
                person.Name = string.Format("{0} {1} A' {2} {3} {4}",
                    _vornamenMännlich.RandomElement(),
                    _nachnamen.RandomElement(),
                    _städtenamen.RandomElement(),
                    _adelskürzel.RandomElement(),
                    _städtenamen.RandomElement());
            }
        }

        private void setzeStadtfreienname(ref PersonNurName person)
        {
            if (person.Geschlecht == Geschlecht.weiblich)
            {
                person.Name = string.Format("{0} {1}",
                    _vornamenWeiblich.RandomElement(),
                    _nachnamen.RandomElement());
            }
            else // Geschlecht kann nur männlich sein
            {
                person.Name = string.Format("{0} {1}",
                    _vornamenMännlich.RandomElement(),
                    _nachnamen.RandomElement());
            }
        }

        private void setzeLandfreienname(ref PersonNurName person)
        {
            if (person.Geschlecht == Geschlecht.weiblich)
            {
                person.Name = string.Format("{0}",
                    _vornamenWeiblich.RandomElement());
            }
            else // Geschlecht kann nur männlich sein
            {
                person.Name = string.Format("{0}",
                    _vornamenMännlich.RandomElement());
            }
        }
        #endregion
    }

    public class WeidenerNamenFactory : NamenFactoryVornameNachname
    {
        #region //---- Felder ----
        private delegate bool EmptyNameSelector();
        /**
         * Funktion, die wahr als Rückgabewert liefert, um leere Namen zu steuern.
         * Die Funktion wird bei Generierung von bis zu zwei weiteren Vornamen genutzt.
         * Durch den wiederholten Aufruf für jeden Namen ergibt sich eine Normalverteilung.
         */
        private EmptyNameSelector emptyNameSelector = () => RandomNumberGenerator.Generator.Next(2) == 1;
        /**
         * Funktion, die die Auswahl eines Nachnamens anstatt eines Familienstammsitz steuert
         */
        private EmptyNameSelector nachnamenSelector = () => RandomNumberGenerator.Generator.Next(2) == 1;
        #endregion

        #region //---- Konstruktor ----
        public WeidenerNamenFactory() :
            base(NamenFactoryHelper.WEIDENERNAMEN, false, true)
        {

        }
        #endregion

        #region //---- Instanzmethoden ----
        public override void RegenerateName(ref PersonNurName person)
        {
            switch (person.Stand)
            {
                case Stand.stadtfrei:
                    setzeStadtfreienname(ref person);
                    break;
                case Stand.adelig:
                    setzeAdelsname(ref person);
                    break;
                case Stand.landfrei:
                    setzeLandfreienname(ref person);
                    break;
                case Stand.unfrei:
                default:
                    setzeUnfreienname(ref person);
                    break;
            }
        }

        /**
         * Adelsnamen erzeugen: 
         * - Einen oder mehrere Vornamen (bis zu 3)
         * - Nachname oder Familienstammsitz
         * - "von <Geburtsort oder Lehen>"
         */
        private void setzeAdelsname(ref PersonNurName person)
        {
            if (person.Geschlecht == Geschlecht.weiblich)
            {
                person.Name = string.Format("{0} {1} {2} {3} von {4}",
                    _vornamenWeiblich.RandomElement(),
                    emptyNameSelector() ? "" : _vornamenWeiblich.RandomElement(),
                    emptyNameSelector() ? "" : _vornamenWeiblich.RandomElement(),
                    nachnamenSelector() ? _nachnamen.RandomElement() : "<Ort: Familienstammsitz>",
                    "<Geburtsort oder Lehen>").EntferneMehrfacheLeerzeichen();
            }
            else // Geschlecht kann nur männlich sein
            {
                person.Name = string.Format("{0} {1} {2} {3} von {4}",
                    _vornamenMännlich.RandomElement(),
                    emptyNameSelector() ? "" : _vornamenMännlich.RandomElement(),
                    emptyNameSelector() ? "" : _vornamenMännlich.RandomElement(),
                    nachnamenSelector() ? _nachnamen.RandomElement() : "<Ort: Familienstammsitz>",
                    "<Geburtsort oder Lehen>").EntferneMehrfacheLeerzeichen();
            }
        }

        private void setzeStadtfreienname(ref PersonNurName person)
        {
            if (person.Geschlecht == Geschlecht.weiblich)
            {
                person.Name = string.Format("{0} {1}",
                    _vornamenWeiblich.RandomElement(),
                    _nachnamen.RandomElement());
            }
            else // Geschlecht kann nur männlich sein
            {
                person.Name = string.Format("{0} {1}",
                    _vornamenMännlich.RandomElement(),
                    _nachnamen.RandomElement());
            }
        }

        private void setzeLandfreienname(ref PersonNurName person)
        {
            if (person.Geschlecht == Geschlecht.weiblich)
            {
                person.Name = string.Format("{0} von <Ort>",
                    _vornamenWeiblich.RandomElement());
            }
            else // Geschlecht kann nur männlich sein
            {
                person.Name = string.Format("{0} von <Ort>",
                    _vornamenMännlich.RandomElement());
            }
        }

        private void setzeUnfreienname(ref PersonNurName person)
        {
            if (person.Geschlecht == Geschlecht.weiblich)
            {
                person.Name = string.Format("{0} aus <Ort>",
                    _vornamenWeiblich.RandomElement());
            }
            else // Geschlecht kann nur männlich sein
            {
                person.Name = string.Format("{0} aus <Ort>",
                    _vornamenMännlich.RandomElement());
            }
        }
        #endregion
    }

    public class NostrischeNamenFactory : NamenFactoryVornameNachname
    {
        #region //---- Felder ----
        private delegate bool EmptyNameSelector();
        /**
         * Funktion, die wahr als Rückgabewert liefert, um leere Namen zu steuern.
         * Die Funktion wird bei Generierung von bis zu zwei weiteren Vornamen genutzt.
         * Durch den wiederholten Aufruf für jeden Namen ergibt sich eine Normalverteilung.
         */
        private EmptyNameSelector emptyNameSelector = () => RandomNumberGenerator.Generator.Next(2) == 1;
        #endregion

        #region //---- Konstruktor ----
        public NostrischeNamenFactory() :
            base(NamenFactoryHelper.NOSTRISCHENAMEN, false, true)
        {

        }
        #endregion

        #region //---- Instanzmethoden ----
        public override void RegenerateName(ref PersonNurName person)
        {
            switch (person.Stand)
            {
                case Stand.stadtfrei:
                    setzeStadtfreienname(ref person);
                    break;
                case Stand.adelig:
                    setzeAdelsname(ref person);
                    break;
                //keine Unfreien Nostrianer
                case Stand.landfrei:
                case Stand.unfrei:
                default:
                    setzeLandfreienname(ref person);
                    break;
            }
        }

        private void setzeAdelsname(ref PersonNurName person)
        {
            if (person.Geschlecht == Geschlecht.weiblich)
            {
                person.Name = string.Format("{0} {1} {2} von {3}-{4}",
                    _vornamenWeiblich.RandomElement().Replace("(", "").Replace(")", ""),
                    emptyNameSelector() ? "" : _vornamenWeiblich.RandomElement().Replace("(", "").Replace(")", ""),
                    emptyNameSelector() ? "" : _vornamenWeiblich.RandomElement().Replace("(", "").Replace(")", ""),
                    _nachnamen.RandomElement(),
                    "<Stammsitz>").EntferneMehrfacheLeerzeichen();
            }
            else // Geschlecht kann nur männlich sein
            {
                person.Name = string.Format("{0} {1} {2} von {3}-{4}",
                    _vornamenMännlich.RandomElement().Replace("(", "").Replace(")", ""),
                    emptyNameSelector() ? "" : _vornamenMännlich.RandomElement().Replace("(", "").Replace(")", ""),
                    emptyNameSelector() ? "" : _vornamenMännlich.RandomElement().Replace("(", "").Replace(")", ""),
                    _nachnamen.RandomElement(),
                    "<Stammsitz>").EntferneMehrfacheLeerzeichen();
            }
        }

        private void setzeStadtfreienname(ref PersonNurName person)
        {
            if (person.Geschlecht == Geschlecht.weiblich)
            {
                person.Name = string.Format("{0} {1}",
                    _vornamenWeiblich.RandomElement().Replace("(","").Replace(")",""),
                    _nachnamen.RandomElement());
            }
            else // Geschlecht kann nur männlich sein
            {
                person.Name = string.Format("{0} {1}",
                    _vornamenMännlich.RandomElement().Replace("(", "").Replace(")", ""),
                    _nachnamen.RandomElement());
            }
        }

        private void setzeLandfreienname(ref PersonNurName person)
        {
            if (person.Geschlecht == Geschlecht.weiblich)
            {
                person.Name = string.Format("{0} {1}",
                    _vornamenWeiblich.RandomElement(),
                    _nachnamen.RandomElement());
            }
            else // Geschlecht kann nur männlich sein
            {
                person.Name = string.Format("{0} {1}",
                    _vornamenMännlich.RandomElement(),
                    _nachnamen.RandomElement());
            }
        }
        #endregion
    }

    #endregion
}