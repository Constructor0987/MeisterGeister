using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//Eigene Usings
using Model = MeisterGeister.Model;
using System.Diagnostics;

namespace MeisterGeister.Model.Service {
    public class DataService : ServiceBase {

        #region //----- EIGENSCHAFTEN ----

        public List<Model.Held> HeldenGruppeListe
        {
            get { return Liste<Held>().Where(h => h.AktiveHeldengruppe == true).OrderBy(h => h.Name).ToList(); }
        }

        #endregion

        #region //----- KONSTRUKTOR ----

        public DataService() {         
        }

        #endregion

        #region //----- DATENBANKABFRAGEN ----

        public Held LoadHeldByName(string aName) {
            var tmp = Context.Held.Where(held => held.Name == aName).FirstOrDefault();
            return tmp;
        }

        public Held LoadHeldByGUID(Guid id)
        {
            var tmp = Context.Held.Where(held => held.HeldGUID == id).FirstOrDefault();
            return tmp;
        }

        /// <summary>
        /// Case-insensitiv.
        /// </summary>
        public Sonderfertigkeit LoadSonderfertigkeitByName(string aName)
        {
            var tmp = Context.Sonderfertigkeit.Where(s => s.Name.ToLower() == aName.ToLower()).FirstOrDefault();
            return tmp;
        }

        /// <summary>
        /// Case-insensitiv.
        /// </summary>
        public Zauber LoadZauberByName(string aName)
        {
            var tmp = Context.Zauber.Where(s => s.Name.ToLower() == aName.ToLower()).FirstOrDefault();
            if(tmp==null)
                tmp = Context.Zauber.Where(s => s.Name.ToLower().StartsWith(aName.ToLower())).FirstOrDefault();
            return tmp;
        }

        /// <summary>
        /// Case-insensitiv.
        /// </summary>
        public Talent LoadTalentByName(string aName)
        {
            var tmp = Context.Talent.Where(s => s.Talentname.ToLower() == aName.ToLower()).FirstOrDefault();
            return tmp;
        }

        /// <summary>
        /// Case-insensitiv.
        /// </summary>
        /// <param name="aName"></param>
        /// <returns></returns>
        public VorNachteil LoadVorNachteilByName(string aName)
        {
            var tmp = Context.VorNachteil.Where(s => s.Name.ToLower() == aName.ToLower()).FirstOrDefault();
            return tmp;
        }

        #endregion

        #region Zauberzeichen
        public List<Held> LoadHeldenGruppeWithZauberzeichen()
        {
            List<Held> tmp = Liste<Sonderfertigkeit>().Where(s => s.Name == "Zauberzeichen" || s.Name == "Runenkunde" || s.Name.StartsWith("Zauberzeichen: Bann") || s.Name.StartsWith("Zauberzeichen: Schutz"))
                .Join(Context.Held_Sonderfertigkeit, s => s.SonderfertigkeitID, hs => hs.SonderfertigkeitID, (s, hs) => hs)
                .Join(Context.Held, hs => hs.HeldGUID, h => h.HeldGUID, (hs, h) => h).ToList();
            return tmp.Distinct().ToList();
        }
        public List<Zauberzeichen> LoadZauberzeichenByHeld(Model.Held held)
        {
            if (held == null)
                return Liste<Zauberzeichen>().Where(z => z.Typ == "Arkanoglyphe").ToList();
            List<Zauberzeichen> zauberzeichen = Liste<Held>().Where(h=>h.HeldGUID==held.HeldGUID).Join(Context.Held_Sonderfertigkeit,h=>h.HeldGUID,hs=>hs.HeldGUID,(h,hs)=>hs)
                .Join(Context.Sonderfertigkeit,hs=>hs.SonderfertigkeitID,s=>s.SonderfertigkeitID,(hs,s)=>s)
                .Join(Context.Zauberzeichen, s=>s.SonderfertigkeitID, zz=>zz.SonderfertigkeitID,(s,zz)=>zz).Where(z=>z.Typ=="Arkanoglyphe").ToList();
            return zauberzeichen;
        }

        public List<Zauberzeichen> LoadKreiseByHeld(Held held)
        {
            if (held == null)
                return Liste<Zauberzeichen>().Where(z => z.Typ == "Bannkreis" || z.Typ == "Schutzkreis").ToList();
            List<Zauberzeichen> kreise = Liste<Held>().Where(h => h.HeldGUID == held.HeldGUID).Join(Context.Held_Sonderfertigkeit, h => h.HeldGUID, hs => hs.HeldGUID, (h, hs) => hs)
                .Join(Context.Sonderfertigkeit, hs => hs.SonderfertigkeitID, s => s.SonderfertigkeitID, (hs, s) => s)
                .Join(Context.Zauberzeichen, s => s.SonderfertigkeitID, zz => zz.SonderfertigkeitID, (s, zz) => zz).Where(z => z.Typ == "Bannkreis" || z.Typ == "Schutzkreis").ToList();
            return kreise;
        }

        public List<Zauberzeichen> LoadRunenByHeld(Held held)
        {
            if (held == null)
                return Liste<Zauberzeichen>().Where(z => z.Typ == "Rune").ToList();
            List<Zauberzeichen> runen = Liste<Held>().Where(h => h.HeldGUID == held.HeldGUID).Join(Context.Held_Sonderfertigkeit, h => h.HeldGUID, hs => hs.HeldGUID, (h, hs) => hs)
                .Join(Context.Sonderfertigkeit, hs => hs.SonderfertigkeitID, s => s.SonderfertigkeitID, (hs, s) => s)
                .Join(Context.Zauberzeichen, s => s.SonderfertigkeitID, zz => zz.SonderfertigkeitID, (s, zz) => zz).Where(z => z.Typ == "Rune").ToList();
            return runen;
        }

        public int LoadMaxRitualkenntnisWertByHeld(Held held)
        {
            if (held == null)
                return 0;

            List<int> werte= new List<int>();
            if (held.HatTalent("Ritualkenntnis (Gildenmagie)")) werte.Add(held.Talentwert("Ritualkenntnis (Gildenmagie)"));
            if (held.HatTalent("Ritualkenntnis (Kristallomantie)")) werte.Add(held.Talentwert("Ritualkenntnis (Kristallomantie)"));
            if (held.HatTalent("Ritualkenntnis (Alchimie)")) werte.Add(held.Talentwert("Ritualkenntnis (Alchimie)"));
            if (held.HatTalent("Ritualkenntnis (Zibiljas)")) werte.Add(held.Talentwert("Ritualkenntnis (Zibiljas)"));
            if (held.HatTalent("Ritualkenntnis (Geister binden)")) werte.Add(held.Talentwert("Ritualkenntnis (Geister binden)"));
            if (held.HatTalent("Ritualkenntnis (Runenkunde)")) werte.Add(held.Talentwert("Ritualkenntnis (Runenkunde)"));
            werte.Sort();
            return (werte.Count == 0) ? 0 : werte[werte.Count-1];
        }

        public List<Talent> LoadZauberzeichenTalenteByHeld(Held held)
        {
            if (held == null)
                return Liste<Talent>().Where(t => t.Talentname == "Feinmechanik" || t.Talentname == "Holzbearbeitung" || t.Talentname == "Malen/Zeichnen" || t.Talentname == "Schneidern" || t.Talentname == "Webkunst").ToList();
            List<Talent> talente = Liste<Held>().Where(h => h.HeldGUID == held.HeldGUID).Join(Context.Held_Talent, h => h.HeldGUID, ht => ht.HeldGUID, (h, ht) => ht)
                .Join(Context.Talent, ht => ht.Talentname, t => t.Talentname, (ht, t) => t).Where(t => t.Talentname == "Feinmechanik" || t.Talentname == "Holzbearbeitung" || t.Talentname == "Malen/Zeichnen" || t.Talentname == "Schneidern" || t.Talentname == "Webkunst").ToList();
            return talente;
        }

        public List<Talent> LoadRunenTalenteByHeld(Held held)
        {
            if (held == null)
                return Liste<Talent>().Where(t => t.Talentname == "Feinmechanik" || t.Talentname == "Holzbearbeitung" || t.Talentname == "Malen/Zeichnen" || t.Talentname == "Schneidern" || t.Talentname == "Webkunst" || t.Talentname == "Tätowieren").ToList();
            List<Talent> talente = Liste<Held>().Where(h => h.HeldGUID == held.HeldGUID).Join(Context.Held_Talent, h => h.HeldGUID, ht => ht.HeldGUID, (h, ht) => ht)
                .Join(Context.Talent, ht => ht.Talentname, t => t.Talentname, (ht, t) => t).Where(t => t.Talentname == "Feinmechanik" || t.Talentname == "Holzbearbeitung" || t.Talentname == "Malen/Zeichnen" || t.Talentname == "Schneidern" || t.Talentname == "Webkunst" || t.Talentname == "Tätowieren").ToList();
            return talente;
        }
        #endregion

        #region Alchimie
        public List<string> LoadIntensitätsbestimmungFertigkeitenAlchimieByHeld(Held held)
        {
            if (held == null)
                return new List<string>();

            List<string> zauber = Liste<Held>().Where(h => h.HeldGUID == held.HeldGUID).Join(Context.Held_Zauber, h => h.HeldGUID, hz => hz.HeldGUID, (h, hz) => hz)
                .Join(Context.Zauber, hz => hz.ZauberID, z => z.ZauberID, (hz, z) => z).Where(z => z.Name == "Odem Arcanum" || z.Name == "Oculus Astralis").OrderBy(z=>z.Name).Select(z=>z.Name).ToList();
            if (zauber.Contains("Odem Arcanum")) zauber.Add("Odem Arcanum (Sicht)");
            //TODO ??: MP liturgie "Sicht auf Madas Welt"
            List<string> talente = Liste<Held>().Where(h => h.HeldGUID == held.HeldGUID).Join(Context.Held_Talent, h => h.HeldGUID, ht => ht.HeldGUID, (h, ht) => ht)
                .Join(Context.Talent, ht => ht.Talentname, t => t.Talentname, (ht, t) => t).Where(t => t.Talentname == "Pflanzenkunde" || t.Talentname == "Magiegespür").Select(t => t.Talentname).ToList();

            List<string> ret = new List<string>();
            ret.AddRange(zauber);
            ret.AddRange(talente);
            return ret;
        }

        internal List<string> LoadStrukturanalyseFertigkeitenAlchimieByHeld(Held held)
        {
            if (held == null)
                return new List<string>();

            List<string> zauber = Liste<Held>().Where(h => h.HeldGUID == held.HeldGUID).Join(Context.Held_Zauber, h => h.HeldGUID, hz => hz.HeldGUID, (h, hz) => hz)
                .Join(Context.Zauber, hz => hz.ZauberID, z => z.ZauberID, (hz, z) => z).Where(z => z.Name == "Analys Arcanstruktur" || z.Name == "Oculus Astralis").OrderBy(z => z.Name).Select(z => z.Name).ToList();
            if (zauber.Contains("Odem Arcanum")) zauber.Add("Odem Arcanum (Sicht)");
            //TODO ??: MP liturgien "Blick der Weberin" "Blick durch Tairachs Augen"
            //TODO ??: MP Abfrage nach Allegorischer Analyse (Schale)
            List<string> ret = new List<string>(); 
            ret.AddRange(zauber);
            ret.Add("Analyse nach Augenschein");
            ret.Add("Laboranalyse");
            ret.Add("Infundibulum der Allweisen");
            return ret;
        }
        #endregion

        #region Einstellungen/Settings
        public Einstellungen LoadEinstellungByName(string name)
        {
            return Context.Einstellungen.Where(e => e.Name == name).FirstOrDefault();
        }

        /// <summary>
        /// Alle Einstellungen, deren name so beginnt.
        /// </summary>
        /// <param name="name">Anfang des Namens (case insensitiv)</param>
        /// <returns></returns>
        public IList<Einstellungen> LoadEinstellungenByName(string name)
        {
            return Context.Einstellungen.Where(e => e.Name.ToLower().StartsWith(name)).ToList();
        }
        #endregion

        #region Kampf und Gegner
        
        public Gegner CreateGegnerInstance(GegnerBase gegnerBase)
        {
            Gegner g = New<Gegner>();
            g.GegnerBaseGUID = gegnerBase.GegnerBaseGUID;
            g.GegnerBase = gegnerBase;
            g.Name = gegnerBase.Name;
            g.Bild = gegnerBase.Bild;
            g.Bemerkung = gegnerBase.Bemerkung;
            Insert<Gegner>(g);
            return g;
        }

        #endregion
    }
}