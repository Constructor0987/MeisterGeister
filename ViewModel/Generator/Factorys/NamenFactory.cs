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
        public const string NAMENSARTVORNAMENNACHSILBEN = "Nachsilbe Vorname";
        public const string NAMENSARTEHRENNAMEN = "Ehrenname";
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
            if (VornamenWeiblichFürAlle)
            {
                _vornamenWeiblich.AddRange(Global.ContextHeld.LoadNamenByNamenstypArt(namenstyp,NAMENSARTVORNAMEN));
            }
            else
            {
                _vornamenWeiblich.AddRange(Global.ContextHeld.LoadNamenByNamenstypArtGeschlecht(namenstyp, NAMENSARTVORNAMEN, true));
                _vornamenMännlich.AddRange(Global.ContextHeld.LoadNamenByNamenstypArtGeschlecht(namenstyp, NAMENSARTVORNAMEN, false));
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
            _nachnamen.AddRange(Global.ContextHeld.LoadNamenByNamenstypArt(namenstyp,NAMENSARTNACHNAMEN));
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

    /**
     * Generiert Zwergische Vornamen
     * Oberklasse für Hügelzwerge und Zwerge
     */
    public class ZwergischeVornamenFactory : NamenFactoryVorname
    {
        #region //---- Felder ----
        protected List<String> _vornamenNachsilben = new List<string>();
        protected delegate bool EmptyNameSelector();
        /**
         * Funktion, die wahr als Rückgabewert liefert, um leere Vornamenssilben zu steuern.
         * Die Funktion wird bei Generierung einer weiteren Vornamenssilben genutzt.
         */
        protected EmptyNameSelector emptyVornameSilbenSelector = () => RandomNumberGenerator.Generator.Next(2) == 1;
        /**
         * Funktion, die wahr als Rückgabewert liefert, um keine Nachsilbe "a" bzw. "e" für weiblich Zwergnamen zu setzen.
         */
        protected EmptyNameSelector emptyWeiblicheVornamenNachsilbeSelector = () => RandomNumberGenerator.Generator.Next(20) == 0;
        /**
         * Funktion, die wahr als Rückgabewert liefert, um "a" statt "e" für weiblich Zwergnamen zu setzen.
         */
        protected EmptyNameSelector aWeiblicheNachsilbeSelector = () => RandomNumberGenerator.Generator.Next(2) == 0;
        protected string _anfangsbuchstabeAktuellerName;
        #endregion

        #region //---- Konstruktor ----
        public ZwergischeVornamenFactory(bool informationenNamenVerfügbar = false) :
            base(NamenFactoryHelper.ZWERGISCHENAMEN, true, informationenNamenVerfügbar)
        {
            _vornamenNachsilben.AddRange(Global.ContextHeld.LoadNamenByNamenstypArt(Namenstyp, NAMENSARTVORNAMENNACHSILBEN));
        }
        #endregion

        #region //---- Instanzmethoden ----
        public override void RegenerateName(ref PersonNurName person)
        {
            person.Name = getZwergenvorname(ref person);
        }

        protected string getZwergenvorname(ref PersonNurName person, bool alliterationZuAktuellemNamen = false)
        {
            if (alliterationZuAktuellemNamen && person.Name.Length > 0)
                _anfangsbuchstabeAktuellerName = person.Name.Remove(1);
            else
                alliterationZuAktuellemNamen = false;
            
            string vorname = string.Format("{0}{1}{2}",
                alliterationZuAktuellemNamen ? _vornamenWeiblich.Where(n => n.StartsWith(_anfangsbuchstabeAktuellerName)).ToList().RandomElement() : _vornamenWeiblich.RandomElement(),
                    _vornamenNachsilben.RandomElement(),
                    emptyVornameSilbenSelector() ? "" : _vornamenNachsilben.RandomElement());

            // Endung für weibliche Zwerge, falls diese nicht auf "a" oder "e" enden
            if (person.Geschlecht == Geschlecht.weiblich
                && !vorname.EndsWith("a")
                && !vorname.EndsWith("e")
                //Die Endungen kommen in seltenen Fällen nicht vor
                && !emptyWeiblicheVornamenNachsilbeSelector())
            {
                vorname += aWeiblicheNachsilbeSelector() ? "a" : "e";
            }
            return vorname;
        }
        #endregion
    }

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
            _namensbedeutungen.AddRange(Global.ContextHeld.LoadNamenByNamenstypArt(Namenstyp, NAMENSARTVORNAMEN, true));
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

    public class HügelzwergischeNamenFactory : ZwergischeVornamenFactory
    {
        #region //---- Felder ----
        List<String> _sippennamen = new List<string>();
        #endregion

        #region //---- Konstruktor ----
        public HügelzwergischeNamenFactory() :
            base(true)
        {
            _sippennamen.AddRange(Global.ContextHeld.LoadNamenByNamenstypArt(NamenFactoryHelper.HÜGELZWERGISCHENAMEN, NAMENSARTNACHNAMEN));
            this.Namenstyp = NamenFactoryHelper.HÜGELZWERGISCHENAMEN;
        }
        #endregion

        #region //---- Instanzmethoden ----
        public override void RegenerateName(ref PersonNurName person)
        {
            person.Name = string.Format("{0} {1}",
                this.getZwergenvorname(ref person),
                _sippennamen.RandomElement());
        }
        #endregion
    }

    public class ZwergischeNamenFactory : ZwergischeVornamenFactory
    {
        #region //---- Felder ----
        private delegate bool AlliterationNameSelector();
        /**
         * Funktion, die wahr als Rückgabewert liefert, Alliterationen in Namen zu steuern.
         * Die Funktion wird bei Generierung des Namen der Mutter oder des Vaters genutzt.
         * Sie definiert, ob ein Alliterationen-Name vergeben wird oder nicht.
         */
        private EmptyNameSelector alliterationNameSelector = () => RandomNumberGenerator.Generator.Next(2) == 1;
        #endregion

        #region //---- Konstruktor ----
        public ZwergischeNamenFactory() :
            base(true)
        {
            
        }
        #endregion

        #region //---- Instanzmethoden ----
        public override void RegenerateName(ref PersonNurName person)
        {
            person.Name = string.Format(
                person.Geschlecht == Geschlecht.weiblich ? "{0} Tochter der {1}" : "{0} Sohn des {1}",
                person.Name = getZwergenvorname(ref person),
                alliterationNameSelector() ? getZwergenvorname(ref person, true) : getZwergenvorname(ref person));            
        }
        #endregion
    }

    public class AlbernischeNamenFactory : NamenFactoryVornameNachname
    {
        #region //---- Felder ----
        private delegate bool ReputationNameSelector();
        /**
         * Funktion, die wahr als Rückgabewert liefert, um Reputations-Namen zu steuern.
         * Die Funktion wird bei Generierung von Bürerlichen Namen genutzt und bestimmt, ob ein
         * Reputationsname genutzt wird, oder nicht
         */
        private ReputationNameSelector reputationNameSelector = () => RandomNumberGenerator.Generator.Next(10) == 1;
        #endregion

        #region //---- Konstruktor ----
        public AlbernischeNamenFactory() :
            base(NamenFactoryHelper.ALBERNISCHENAMEN, false, true)
        {
            // Bastel weitere Vornamen aus garethischen, männlichen Vornamen
            foreach (string name in Global.ContextHeld.LoadNamenByNamenstypArtGeschlecht(NamenFactoryHelper.GARETHISCHENAMEN, NAMENSARTVORNAMEN, false))
            {
                if (name.EndsWith("ian"))
                {
                    _vornamenMännlich.Add(name.Substring(0, name.Length - 3) + "wyn");
                    _vornamenMännlich.Add(name.Substring(0, name.Length - 3) + "uin");
                } 
                else if (name.EndsWith("dan"))
                {
                    _vornamenMännlich.Add(name.Substring(0, name.Length - 3) + "tin");
                    _vornamenMännlich.Add(name.Substring(0, name.Length - 3) + "den");
                }
            }
        }
        #endregion

        #region //---- Instanzmethoden ----
        public override void RegenerateName(ref PersonNurName person)
        {
            switch (person.Stand)
            {
                // manchmal Reputationsnamen
                case Stand.stadtfrei:
                    setzeStadtfreienname(ref person);
                    break;
                // immer Reputationsnamen
                case Stand.adelig:
                    setzeReputationname(ref person);
                    break;
                //normaler Name
                default:
                    base.RegenerateName(ref person);
                    break;
            }
        }


        private void setzeStadtfreienname(ref PersonNurName person)
        {
            if(reputationNameSelector())
                setzeReputationname(ref person);
            else 
                base.RegenerateName(ref person);
        }


        private void setzeReputationname(ref PersonNurName person)
        {
            if (person.Geschlecht == Geschlecht.weiblich )
            {
                person.Name = string.Format("{0} ni {1}",
                    _vornamenWeiblich.RandomElement(),
                    RandomNumberGenerator.Generator.Next(2)==1 ? _vornamenWeiblich.RandomElement() : _vornamenMännlich.RandomElement());
            } else  {
                person.Name = string.Format("{0} ui {1}",
                    _vornamenMännlich.RandomElement(),
                    RandomNumberGenerator.Generator.Next(2)==1 ? _vornamenWeiblich.RandomElement() : _vornamenMännlich.RandomElement());
            }
        }
        #endregion
    }

    public class AlmadanischeNamenFactory : NamenFactoryVornameNachname
    {
        #region //---- Felder ----
        List<string> _zweiterVornameMännlich = new List<string>(new string[] { "Almadano", "Amirato", "Amado", "Beatus", "Bonus", "Celebratus", "Desidero", "Dulcineo", "Eximio", "Flavo",
            "Glaciano", "Hilado", "Honorio", "Ingenioso", "Leovigildo", "Magnifico", "Maldonado", "Merito", "Misterio", "Nobilis",
            "Nocturnus", "Optimas", "Pacifico", "Promeso", "Rosario", "Sempervivens", "Superbus", "Tenax", "Universalis", "Valeroso", 
            "Violante", "Zonzo" });
        List<string> _zweiterVornameWeiblich = new List<string>(new string[] { "Almadana", "Amirata", "Amada", "Beata", "Bona", "Celebrata", "Desidera", "Dulcinea", "Eximia", "Flava",
            "Glaciana", "Hilada", "Honoria", "Ingeniosa", "Leovigilda", "Magnifica", "Maldonada", "Merita", "Misteria", "Nobilis",
            "Nocturna", "Optimas", "Pacifica", "Promesa", "Rosaria", "Sempervivens", "Superba", "Tenax", "Universalis", "Valerosa", 
            "Violanta", "Zonza" });
        #endregion

        #region //---- Konstruktor ----
        public AlmadanischeNamenFactory() :
            base(NamenFactoryHelper.ALMADISCHENAMEN, false, true)
        {
            
        }
        #endregion

        #region //---- Instanzmethoden ----
        public override void RegenerateName(ref PersonNurName person)
        {
            if (person.Geschlecht == Geschlecht.weiblich)
                person.Name = string.Format("{0} {1} {2}", _vornamenWeiblich.RandomElement(), _zweiterVornameWeiblich.RandomElement(), _nachnamen.RandomElement());
            else
                person.Name = string.Format("{0} {1} {2}", _vornamenMännlich.RandomElement(), _zweiterVornameMännlich.RandomElement(), _nachnamen.RandomElement());
        }
        #endregion
    }

    public class BornländischeNamenFactory : NamenFactoryVornameNachname
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
        public BornländischeNamenFactory() :
            base(NamenFactoryHelper.BORNLÄNDISCHENAMEN, false, true)
        {
        }
        #endregion

        #region //---- Instanzmethoden ----
        public override void RegenerateName(ref PersonNurName person)
        {
            switch (person.Stand)
            {
                case Stand.adelig:
                    setzeAdeligenname(ref person);
                    break;
                case Stand.stadtfrei:
                    base.RegenerateName(ref person);
                    break;
                case Stand.landfrei:
                    setzeLandfreienname(ref person);
                    break;
                default:
                    setzeUnfreienname(ref person);
                    break;
            }
        }

        private void setzeAdeligenname(ref PersonNurName person)
        {
            if (person.Geschlecht == Geschlecht.weiblich)
            {
                person.Name = string.Format("{0} {1} {2} von {3} zu {4}",
                    _vornamenWeiblich.RandomElement(),
                    emptyNameSelector() ? "" : _vornamenWeiblich.RandomElement(),
                    emptyNameSelector() ? "" : _vornamenWeiblich.RandomElement(),
                    "<Stammsitz>",
                    "<Lehen>").EntferneMehrfacheLeerzeichen();
            }
            else // Geschlecht kann nur männlich sein
            {
                person.Name = string.Format("{0} {1} {2} von {3} zu {4}",
                    _vornamenMännlich.RandomElement(),
                    emptyNameSelector() ? "" : _vornamenMännlich.RandomElement(),
                    emptyNameSelector() ? "" : _vornamenMännlich.RandomElement(),
                    "<Stammsitz>",
                    "<Lehen>").EntferneMehrfacheLeerzeichen();
            }
        }

        private void setzeLandfreienname(ref PersonNurName person)
        {
            person.Name = string.Format("{0} aus {1}",
                (person.Geschlecht == Geschlecht.weiblich) ? _vornamenWeiblich.RandomElement() : _vornamenMännlich.RandomElement(),
                "<Ortsname>");
        }

        private void setzeUnfreienname(ref PersonNurName person)
        {
            person.Name = (person.Geschlecht == Geschlecht.weiblich) ? _vornamenWeiblich.RandomElement() : _vornamenMännlich.RandomElement();
        }
        #endregion
    }
    
    public class GjalskerländischeNamenFactory : NamenFactoryVorname
    {
        #region //---- Felder ----
        private List<string> _haerads = new List<string>(new string[] { "Alrudh", "Arryach-Mûr", "Chombaich", "Conneach", "Dhartaech", "Lyrgach", "Mortakh", "Nebbachodh", "Niellyn", "Rayyadh" });
        private delegate bool MännlicheMentorenNameSelector();
        /**
         * Funktion, die wahr als Rückgabewert liefert, um männliche Mentoren bei Schamanen zu steuern.
         */
        private MännlicheMentorenNameSelector männlicheMentorenNameSelector = () => RandomNumberGenerator.Generator.Next(2) == 1;
        #endregion

        #region //---- Konstruktor ----
        public GjalskerländischeNamenFactory() :
            base(NamenFactoryHelper.GJALSKERLÄNDISCHENAMEN, false, true)
        {
        }
        #endregion

        #region //---- Instanzmethoden ----
        public override void RegenerateName(ref PersonNurName person)
        {
            switch (person.Stand)
            {
                case Stand.adelig:
                    setzeSchamanenname(ref person);
                    break;
                default:
                    setzeGjalskerländischenName(ref person);
                    break;
            }
        }

        private void setzeSchamanenname(ref PersonNurName person)
        {
            person.Name = string.Format("{0} dur {1} aus dem Haerad {2}",
                person.Geschlecht == Geschlecht.weiblich ? _vornamenWeiblich.RandomElement() : _vornamenMännlich.RandomElement(),
                männlicheMentorenNameSelector() ? _vornamenMännlich.RandomElement() : _vornamenWeiblich.RandomElement(),
                _haerads.RandomElement());
        }

        private void setzeGjalskerländischenName(ref PersonNurName person)
        {
            if (person.Geschlecht == Geschlecht.weiblich)
                person.Name = string.Format("{0} brai {1} aus dem Haerad {2}",
                    _vornamenWeiblich.RandomElement(), _vornamenWeiblich.RandomElement(), _haerads.RandomElement());
            else
                person.Name = string.Format("{0} bren {1} aus dem Haerad {2}",
                    _vornamenMännlich.RandomElement(), _vornamenMännlich.RandomElement(), _haerads.RandomElement());
        }
        #endregion
    }

    public class ThorwalscheNamenFactory : NamenFactoryVorname
    {
        #region //---- Felder ----
        private delegate bool NameSelector();
        /**
         * Funktionen, die wahr als Rückgabewert liefert, um Namen zu steuern.
         */
        private NameSelector dottirStattdotterNameSelector = () => RandomNumberGenerator.Generator.Next(2) == 1;
        private NameSelector abstammungsnameVonMutterNameSelector = () => RandomNumberGenerator.Generator.Next(2) == 1;
        #endregion

        #region //---- Konstruktor ----
        public ThorwalscheNamenFactory() :
            base(NamenFactoryHelper.THORWALSCHENAMEN, false, true)
        {
        }
        #endregion

        #region //---- Instanzmethoden ----
        public override void RegenerateName(ref PersonNurName person)
        {
            if (person.Geschlecht == Geschlecht.weiblich)
                person.Name = string.Format("{0} {1}{2}",
                    _vornamenWeiblich.RandomElement(),
                    abstammungsnameVonMutterNameSelector() ? _vornamenWeiblich.RandomElement() : _vornamenMännlich.RandomElement(),
                    dottirStattdotterNameSelector() ? "dottir" : "dotter");
            else
                person.Name = string.Format("{0} {1}son",
                    _vornamenMännlich.RandomElement(),
                    abstammungsnameVonMutterNameSelector() ? _vornamenWeiblich.RandomElement() : _vornamenMännlich.RandomElement());
        }
        #endregion
    }

    public class NivesischeNamenFactory : NamenFactoryVorname
    {
        #region //---- Felder ----
        #endregion

        #region //---- Konstruktor ----
        public NivesischeNamenFactory() :
            base(NamenFactoryHelper.NIVESISCHENAMEN, false, true)
        {
        }
        #endregion

        #region //---- Instanzmethoden ----
        public override void RegenerateName(ref PersonNurName person)
        {
            person.Name = string.Format("{0} von {1}s und {2}s Stamm",
                person.Geschlecht == Geschlecht.weiblich ? _vornamenWeiblich.RandomElement() : _vornamenMännlich.RandomElement(),
                _vornamenMännlich.RandomElement(), _vornamenWeiblich.RandomElement());
        }
        #endregion
    }

    public class NorbardischeNamenFactory : NamenFactoryVornameNachname
    {
        #region //---- Felder ----
        private delegate bool NameSelector();
        /**
         * Funktionen, die wahr als Rückgabewert liefert, um Namen zu steuern.
         */
        private NameSelector männlicheSippennamenNameSelector = () => RandomNumberGenerator.Generator.Next(10) == 1;
        private NameSelector männlicheSippennamenJoStattONameSelector = () => RandomNumberGenerator.Generator.Next(2) == 1;
        #endregion

        #region //---- Konstruktor ----
        public NorbardischeNamenFactory() :
            base(NamenFactoryHelper.NORBADISCHENAMEN, false, true)
        {
            //Bastel weitere weibliche Vornamen aus den Männlichen durch anhängen von "ja" oder "a"
            foreach (string name in _vornamenMännlich)
            {
                if (!name.EndsWith("ja"))
                {
                    _vornamenWeiblich.Add(name + "a");
                    if (!name.EndsWith("j"))
                        _vornamenWeiblich.Add(name + "ja");
                }
            }
        }
        #endregion

        #region //---- Instanzmethoden ----
        public override void RegenerateName(ref PersonNurName person)
        {
            person.Name = string.Format("{0} {1}",
                person.Geschlecht == Geschlecht.weiblich ? _vornamenWeiblich.RandomElement() : _vornamenMännlich.RandomElement(),
                männlicheSippennamenNameSelector() ? (_nachnamen.RandomElement() + (männlicheSippennamenJoStattONameSelector() ? "jo" : "o")) : _nachnamen.RandomElement());
        }
        #endregion
    }

    public class GarethischeNamenFactory : NamenFactoryVornameNachname
    {
        #region //---- Felder ----
        private delegate bool NameSelector();
        /**
         * Funktion, die wahr als Rückgabewert liefert, um leere Zweitnamen bei Adelsnamen zu steuern.
         */
        private NameSelector ausStattVonNameSelector = () => RandomNumberGenerator.Generator.Next(3) == 1;
        private NameSelector aufStattZuAdelsnameSelector = () => RandomNumberGenerator.Generator.Next(2) == 1;
        private NameSelector emptyZweiterDritterAdelsnameSelector = () => RandomNumberGenerator.Generator.Next(2) == 1;
        private NameSelector einfacheVorsilbenAdelsnameSelector = () => RandomNumberGenerator.Generator.Next(4) == 1;
        private NameSelector bosperanischeAdelsnameSelector = () => RandomNumberGenerator.Generator.Next(2) == 1;
        private NameSelector kriegerischeAdelsnameSelector = () => RandomNumberGenerator.Generator.Next(2) == 1;
        private NameSelector berühmtheitenAdelsnameSelector = () => RandomNumberGenerator.Generator.Next(2) == 1;
        #endregion

        #region //---- Konstruktor ----
        public GarethischeNamenFactory() :
            base(NamenFactoryHelper.GARETHISCHENAMEN, false, true)
        {

        }
        #endregion

        #region //---- Instanzmethoden ----
        public override void RegenerateName(ref PersonNurName person)
        {
            switch (person.Stand)
            {
                case Stand.adelig:
                    setzeAdelsname(ref person);
                    break;
                case Stand.stadtfrei:
                    base.RegenerateName(ref person);
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

        private void setzeAdelsname(ref PersonNurName person)
        {
            if (person.Geschlecht == Geschlecht.weiblich)
            {
                person.Name = string.Format("{0} {1} {2} von {3} {4} {5}",
                    _vornamenWeiblich.RandomElement(),
                    emptyZweiterDritterAdelsnameSelector() ? "" : _vornamenWeiblich.RandomElement(),
                    emptyZweiterDritterAdelsnameSelector() ? "" : _vornamenWeiblich.RandomElement(),
                    "<Familienname>",
                    aufStattZuAdelsnameSelector() ? "auf" : "zu",
                    "<Lehen>").EntferneMehrfacheLeerzeichen();
            }
            else // Geschlecht kann nur männlich sein
            {
                person.Name = string.Format("{0} {1} {2} von {3} {4} {5}",
                    _vornamenMännlich.RandomElement(),
                    emptyZweiterDritterAdelsnameSelector() ? "" : _vornamenMännlich.RandomElement(),
                    emptyZweiterDritterAdelsnameSelector() ? "" : _vornamenMännlich.RandomElement(),
                    "<Familienname>",
                    aufStattZuAdelsnameSelector() ? "auf" : "zu",
                    "<Lehen>").EntferneMehrfacheLeerzeichen();
            }
        }

        private void setzeLandfreienname(ref PersonNurName person)
        {
            person.Name = string.Format("{0} {1} {2}",
                person.Geschlecht == Geschlecht.weiblich ? _vornamenWeiblich.RandomElement() : _vornamenMännlich.RandomElement(),
                ausStattVonNameSelector() ? "aus" : "von",
                "<Ortsname>");
        }

        private void setzeUnfreienname(ref PersonNurName person)
        {
            if (person.Geschlecht == Geschlecht.weiblich)
            {
                person.Name = _vornamenWeiblich.RandomElement();
            }
            else // Geschlecht kann nur männlich sein
            {
                person.Name =  _vornamenMännlich.RandomElement();
            }
        }
        #endregion
    }

    public class OrkischeNamenFactory : NamenFactoryVornameNachname
    {
        #region //---- Felder ----
        #endregion

        #region //---- Konstruktor ----
        public OrkischeNamenFactory() :
            base(NamenFactoryHelper.ORKISCHENAMEN, false, true)
        {
        }
        #endregion

        #region //---- Instanzmethoden ----
        public override void RegenerateName(ref PersonNurName person)
        {
            person.Name = string.Format("{0} (Stamm:{1})",
                person.Geschlecht == Geschlecht.weiblich ? _vornamenWeiblich.RandomElement() : _vornamenMännlich.RandomElement(),
                _nachnamen.RandomElement());
        }
        #endregion
    }

    public class OrkischeSvellttalNamenFactory : NamenFactoryVornameNachname
    {
        #region //---- Felder ----
        #endregion

        #region //---- Konstruktor ----
        public OrkischeSvellttalNamenFactory() :
            base(NamenFactoryHelper.ORKISCHENAMEN, false, true)
        {
            this.Namenstyp = NamenFactoryHelper.ORKISCHESVELLTALNAMEN;
            _nachnamen.Clear();
            _nachnamen = Global.ContextHeld.LoadNamenByNamenstypArt(NamenFactoryHelper.ORKISCHESVELLTALNAMEN, NAMENSARTVORNAMEN);
        }
        #endregion

        #region //---- Instanzmethoden ----
        public override void RegenerateName(ref PersonNurName person)
        {
            base.RegenerateName(ref person);
        }
        #endregion
    }

    public class AranischeNamenFactory : TulamidischeNamenFactory
    {
        #region //---- Felder ----
        #endregion

        #region //---- Konstruktor ----
        public AranischeNamenFactory() :
            base()
        {
            this.Namenstyp = NamenFactoryHelper.ARANISCHENAMEN;
            _vornamenMännlich.Clear();
            _vornamenWeiblich.Clear();
            _vornamenMännlich.AddRange(Global.ContextHeld.LoadNamenByNamenstypArtGeschlecht(Namenstyp, NAMENSARTVORNAMEN, false));
            _vornamenWeiblich.AddRange(Global.ContextHeld.LoadNamenByNamenstypArtGeschlecht(Namenstyp, NAMENSARTVORNAMEN, true));
             mutterBeiWeiblichenNamen = () => RandomNumberGenerator.Generator.Next(10) < 9;
        }
        #endregion

        #region //---- Instanzmethoden ----
        #endregion
    }

    public class TulamidischeNamenFactory : NamenFactoryVorname
    {
        #region //---- Felder ----
        protected delegate bool NameSelector();
        /**
         * Funktion, die wahr als Rückgabewert liefert, um leere Zweitnamen bei Adelsnamen zu steuern.
         */
        protected NameSelector mutterBeiWeiblichenNamen = () => false;
        protected NameSelector alStattEl = () => RandomNumberGenerator.Generator.Next(2) == 1;
        protected List<string> _tochter = new List<string>(new string[] { "{0} saba {1}", "{0} {1}suni", "{0} {1}sunni", "{0} {1}sunya", "{0} {1}sunyara" });
        protected string _sohn = "{0} ibn {1}";
        protected List<string> _ehrennamenMännlich = new List<string>();
        protected List<string> _ehrennamenMännlichBedeutung = new List<string>();
        protected List<string> _ehrennamenWeiblich = new List<string>();
        protected List<string> _ehrennamenWeiblichBedeutung = new List<string>();
        protected int _selectedEhrenname;
        protected string _vorname, _elternname, _ehrenname;
        #endregion

        #region //---- Konstruktor ----
        public TulamidischeNamenFactory() :
            base(NamenFactoryHelper.TULAMIDISCHENAMEN, false, true, true)
        {
            _ehrennamenWeiblich.AddRange(Global.ContextHeld.LoadNamenByNamenstypArtGeschlecht(Namenstyp, NAMENSARTEHRENNAMEN, true, false));
            _ehrennamenWeiblichBedeutung.AddRange(Global.ContextHeld.LoadNamenByNamenstypArtGeschlecht(Namenstyp, NAMENSARTEHRENNAMEN, true, true));
            _ehrennamenMännlich.AddRange(Global.ContextHeld.LoadNamenByNamenstypArtGeschlecht(Namenstyp, NAMENSARTEHRENNAMEN, false, false));
            _ehrennamenMännlichBedeutung.AddRange(Global.ContextHeld.LoadNamenByNamenstypArtGeschlecht(Namenstyp, NAMENSARTEHRENNAMEN, false, true));
        }
        #endregion

        #region //---- Instanzmethoden ----
        public override void RegenerateName(ref PersonNurName person)
        {
            //Adel -> Ehrenname hinzufügen
            if (person.Geschlecht == Geschlecht.weiblich)
            {
                _vorname = _vornamenWeiblich.RandomElement();
                _elternname = mutterBeiWeiblichenNamen() ? _vornamenWeiblich.RandomElement() : _vornamenMännlich.RandomElement();
                _selectedEhrenname = RandomNumberGenerator.Generator.Next(_ehrennamenWeiblich.Count());
                if (person.Stand == Stand.adelig)
                    _ehrenname = string.Format(alStattEl() ? "al {0}" : "el {0}", _ehrennamenWeiblich.ElementAt(_selectedEhrenname));
                person.Name = string.Format(_tochter.RandomElement(),
                    person.Stand == Stand.adelig ? _vorname + " " + _ehrenname : _vorname,
                    _elternname);
                person.Namensbedeutung = string.Format("{0} Tochter der/des {1}",
                    person.Stand == Stand.adelig ? _vorname + " " + _ehrennamenWeiblichBedeutung.ElementAt(_selectedEhrenname) : _vorname,
                    _elternname);
            }
            else
            {
                _vorname = _vornamenMännlich.RandomElement();
                _elternname = _vornamenMännlich.RandomElement();
                _selectedEhrenname = RandomNumberGenerator.Generator.Next(_ehrennamenMännlich.Count());
                if (person.Stand == Stand.adelig)
                    _ehrenname = string.Format(alStattEl() ? "al {0}" : "el {0}", _ehrennamenMännlich.ElementAt(_selectedEhrenname));
                person.Name = string.Format(_sohn,
                    person.Stand == Stand.adelig ? _vorname + " " + _ehrenname : _vorname,
                    _elternname);
                person.Namensbedeutung = string.Format(_sohn,
                    person.Stand == Stand.adelig ? _vorname + " " + _ehrennamenMännlichBedeutung.ElementAt(_selectedEhrenname) : _vorname,
                    _elternname);
            }
        }
        #endregion
    }

    public class NovadischeNamenFactory : TulamidischeNamenFactory
    {
        #region //---- Felder ----
        private List<string> _sippennamen = new List<string>(new string[] { "Tirah", "Ranah", "Ulah", "Sanrash" });
        private int _selectedSippe;
        #endregion

        #region //---- Konstruktor ----
        public NovadischeNamenFactory() :
            base()
        {
            this.Namenstyp = NamenFactoryHelper.NOVADISCHENAMEN;
            _sohn = "{0} ben {1}";
        }
        #endregion

        #region //---- Instanzmethoden ----
        public override void RegenerateName(ref PersonNurName person)
        {
            base.RegenerateName(ref person);
            // Sippe anhängen
            _selectedSippe = RandomNumberGenerator.Generator.Next(_sippennamen.Count());
            if (person.Geschlecht == Geschlecht.weiblich)
            {
                person.Name += " ben " + _sippennamen.ElementAt(_selectedSippe);
                person.Namensbedeutung += " Sohn der Sippe " + _sippennamen.ElementAt(_selectedSippe);
            }
            else
            {
                person.Name += " saba " + _sippennamen.ElementAt(_selectedSippe);
                person.Namensbedeutung += " Tochter der Sippe " + _sippennamen.ElementAt(_selectedSippe);
            }
        }
        #endregion
    }

    public class FerkinaNamenFactory : NamenFactoryVorname
    {
        #region //---- Felder ----
        private List<string> sippe = new List<string>(new string[] { " Bân ", " Ulad ", " Bem " });
        #endregion

        #region //---- Konstruktor ----
        public FerkinaNamenFactory() :
            base(NamenFactoryHelper.FERKINANAMEN, false, true)
        {
        }
        #endregion

        #region //---- Instanzmethoden ----
        public override void RegenerateName(ref PersonNurName person)
        {
            person.Name = string.Format(person.Geschlecht == Geschlecht.weiblich ? "{0} sabu {1}" : "{0} iban {1}",
                person.Geschlecht == Geschlecht.weiblich ? _vornamenWeiblich.RandomElement() : _vornamenMännlich.RandomElement(),
                _vornamenMännlich.RandomElement());
        }
        #endregion
    }

    #endregion
}