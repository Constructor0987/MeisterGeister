using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeisterGeister.Daten;
using System.Xml;
// Eigene Usings
using MeisterGeister.Logic.General;

namespace MeisterGeister.Logic.HeldenImport
{
    /// <summary>
    /// Klasse zum Konvertieren von Helden der Helden-Software.
    /// </summary>
    public class HeldenSoftwareImporter
    {
        public HeldenSoftwareImporter(DatabaseDSADataSet dsExport, Held held, string pfad)
        {
            _dsExport = dsExport;
            _held = held;
            _importPfad = pfad;

            // Talent-Mapping
            SetTalentMapping();

            // Zauber-Mapping
            SetZauberMapping();

            // VorNachteil-Mapping
            SetVorNachteilMapping();

            // Sonderfertigkeit-Mapping
            SetSonderfertigkeitenMapping();
        }

        private void SetSonderfertigkeitenMapping()
        {
            _sonderfertigkeitMapping.Add("Akklimatisierung: Hitze", "Akklimatisierung (Hitze)");
            _sonderfertigkeitMapping.Add("Akklimatisierung: Kälte", "Akklimatisierung (Kälte)");
            _sonderfertigkeitMapping.Add("Apport", "Objektritual: Apport");
            _sonderfertigkeitMapping.Add("Bannschwert", "Objektritual: Bannschwert");
            _sonderfertigkeitMapping.Add("Kulturkunde (Andergast/Nostria)", "Kulturkunde (Andergast und Nostria)");
            _sonderfertigkeitMapping.Add("Kulturkunde (Stammesachaz)", "Kulturkunde (Stammes-Achaz)");
            _sonderfertigkeitMapping.Add("Merkmalskenntnis: Dämonisch", "Merkmalskenntnis (Dämonisch (gesamt))");
            _sonderfertigkeitMapping.Add("Merkmalskenntnis: Elementar", "Merkmalskenntnis (Elementar (gesamt))");
            _sonderfertigkeitMapping.Add("Stabzauber: Fackel", "Stabzauber: Ewige Flamme");
            _sonderfertigkeitMapping.Add("Stabzauber: Seil", "Stabzauber: Seil des Adepten");
            _sonderfertigkeitMapping.Add("Repräsentation: Achaz", "Repräsentation (Achaz-Kristallomantisch)");
            _sonderfertigkeitMapping.Add("Repräsentation: Borbaradianer", "Repräsentation (Borbaradianisch)");
            _sonderfertigkeitMapping.Add("Repräsentation: Druide", "Repräsentation (Druidisch)");
            _sonderfertigkeitMapping.Add("Repräsentation: Elf", "Repräsentation (Elfisch)");
            _sonderfertigkeitMapping.Add("Repräsentation: Geode", "Repräsentation (Geodisch)");
            _sonderfertigkeitMapping.Add("Repräsentation: Hexe", "Repräsentation (Hexisch)");
            _sonderfertigkeitMapping.Add("Repräsentation: Magier", "Repräsentation (Gildenmagisch)");
            _sonderfertigkeitMapping.Add("Repräsentation: Scharlatan", "Repräsentation (Scharlatanisch)");
            _sonderfertigkeitMapping.Add("Repräsentation: Schelm", "Repräsentation (Schelmisch)");
            _sonderfertigkeitMapping.Add("Repräsentation: Alhanisch", "Repräsentation (Alhanisch)");
            _sonderfertigkeitMapping.Add("Repräsentation: Druidisch-Geodisch", "Repräsentation (Druidisch-Geodisch)");
            _sonderfertigkeitMapping.Add("Repräsentation: Grolmisch", "Repräsentation (Grolmisch)");
            _sonderfertigkeitMapping.Add("Repräsentation: Güldenländisch", "Repräsentation (Güldenländisch)");
            _sonderfertigkeitMapping.Add("Repräsentation: Kophtanisch", "Repräsentation (Kophtanisch)");
            _sonderfertigkeitMapping.Add("Repräsentation: Mudramulisch", "Repräsentation (Mudramulisch)");
            _sonderfertigkeitMapping.Add("Repräsentation: Satuarisch", "Repräsentation (Satuarisch)");
            _sonderfertigkeitMapping.Add("Ritualkenntnis: Gildenmagie", "Ritualkenntnis (Gildenmagie)");
            _sonderfertigkeitMapping.Add("Ritualkenntnis: Kristallomantie", "Ritualkenntnis (Kristallomantie)");
            _sonderfertigkeitMapping.Add("Ritualkenntnis: Scharlatan", "Ritualkenntnis (Scharlatanerie)");
            _sonderfertigkeitMapping.Add("Ritualkenntnis: Alchimist", "Ritualkenntnis (Alchimie)");
            _sonderfertigkeitMapping.Add("Ritualkenntnis: Hexe", "Ritualkenntnis (Hexenmagie)");
            _sonderfertigkeitMapping.Add("Ritualkenntnis: Geode", "Ritualkenntnis (Geoden)");
            _sonderfertigkeitMapping.Add("Ritualkenntnis: Druide", "Ritualkenntnis (Druiden)");
            _sonderfertigkeitMapping.Add("Ritualkenntnis: Derwisch", "Ritualkenntnis (Derwische)");
            _sonderfertigkeitMapping.Add("Ritualkenntnis: Zaubertänzer", "Ritualkenntnis (Zaubertänze)");
            _sonderfertigkeitMapping.Add("Ritualkenntnis: Zaubertänzer (novadische Sharisad)", "Ritualkenntnis (Zaubertänze)");
            _sonderfertigkeitMapping.Add("Ritualkenntnis: Zaubertänzer (tulamische Sharisad)", "Ritualkenntnis (Zaubertänze)");
            _sonderfertigkeitMapping.Add("Ritualkenntnis: Zaubertänzer (Majuna)", "Ritualkenntnis (Zaubertänze)");
            _sonderfertigkeitMapping.Add("Ritualkenntnis: Zaubertänzer (Hazaqi)", "Ritualkenntnis (Zaubertänze)");
            _sonderfertigkeitMapping.Add("Ritualkenntnis: Zibilja", "Ritualkenntnis (Zibilja)");
            _sonderfertigkeitMapping.Add("Keulenritual: Apport der Keule", "Objektritual: Apport");
            _sonderfertigkeitMapping.Add("Kristallkraft bündeln", "Kristallomantisches Ritual: Kristallkraft Bündeln");
            _sonderfertigkeitMapping.Add("Zaubertanz: El Vanidad (Tanz der Bilder)", "Zaubertanz: Tanz der Bilder");
            _sonderfertigkeitMapping.Add("Zaubertanz: Heschinjas Blick (Tanz der Wahrheit)", "Zaubertanz: Tanz der Wahrheit");
            _sonderfertigkeitMapping.Add("Zaubertanz: Hesindes Macht (Tanz der Erlösung)", "Zaubertanz: Tanz der Erlösung");
            _sonderfertigkeitMapping.Add("Zaubertanz: Khablas Verlockung (Tanz der Liebe)", "Zaubertanz: Tanz der Liebe");
            _sonderfertigkeitMapping.Add("Zaubertanz: Marhibos Hand (Tanz der Erlösung)", "Zaubertanz: Tanz der Erlösung");
            _sonderfertigkeitMapping.Add("Zaubertanz: Nahemas Traum (Tanz ohne Ende)", "Zaubertanz: Tanz ohne Ende");
            _sonderfertigkeitMapping.Add("Zaubertanz: Orhimas Tanz (Tanz der Weisheit)", "Zaubertanz: Tanz der Weisheit");
            _sonderfertigkeitMapping.Add("Zaubertanz: Pavonearse (Tanz der Ermutigung)", "Zaubertanz: Tanz der Ermutigung");
            _sonderfertigkeitMapping.Add("Zaubertanz: Peraines Liebe (Tanz der Freude)", "Zaubertanz: Tanz der Freude");
            _sonderfertigkeitMapping.Add("Zaubertanz: Perhinas Segen (Tanz der Freude)", "Zaubertanz: Tanz der Freude");
            _sonderfertigkeitMapping.Add("Zaubertanz: Phexens Geschmeide (Tanz der Bilder)", "Zaubertanz: Tanz der Bilder");
            _sonderfertigkeitMapping.Add("Zaubertanz: Rahjarra (Tanz der Liebe)", "Zaubertanz: Tanz der Liebe");
            _sonderfertigkeitMapping.Add("Zaubertanz: Rhondaras Forderung (Tanz der Ermutigung)", "Zaubertanz: Tanz der Ermutigung");
            _sonderfertigkeitMapping.Add("Zaubertanz: Rondras Mut (Tanz der Ermutigung)", "Zaubertanz: Tanz der Ermutigung");
            _sonderfertigkeitMapping.Add("Zaubertanz: Satinavs Gabe (Tanz ohne Ende)", "Zaubertanz: Tanz ohne Ende");
            _sonderfertigkeitMapping.Add("Zaubertanz: Shimijas Rausch (Tanz der Bilder)", "Zaubertanz: Tanz der Bilder");
            _sonderfertigkeitMapping.Add("Zaubertanz: Suenyo (Tanz ohne Ende)", "Zaubertanz: Tanz ohne Ende");
            _sonderfertigkeitMapping.Add("Zaubertanz: Tanz für Rastullah (Tanz der Unantastbarkeit)", "Zaubertanz: Tanz der Unantastbarkeit");
            _sonderfertigkeitMapping.Add("Zaubertanz: Zarpada (Tanz der Erlösung)", "Zaubertanz: Tanz der Erlösung");
            _sonderfertigkeitMapping.Add("Ritualkenntnis: Runenzauberei", "Runenkunde");
            _sonderfertigkeitMapping.Add("Meisterliche Zauberkontrolle", "Meisterliche Zauberkontrolle I");
            _sonderfertigkeitMapping.Add("Zauberzeichen: Satinavs Siegel", "Zauberzeichen: Zusatzzeichen Satinavs Siegel");
            _sonderfertigkeitMapping.Add("Zauberzeichen: Schutzsiegel", "Zauberzeichen: Zusatzzeichen Schutzsiegel");
            _sonderfertigkeitMapping.Add("Zauberzeichen: Bannkreis gegen Chimären", "Zauberzeichen: Bann- und Schutzkreis gegen Chimären");
            _sonderfertigkeitMapping.Add("Zauberzeichen: Bannkreis gegen Traumgänger", "Zauberzeichen: Schutzkreis gegen Traumgänger");
            _sonderfertigkeitMapping.Add("Schlangenszepters: Bindung", "Schlangenszepter: Bindung");
            _sonderfertigkeitMapping.Add("Schlangenszepters: Ruf der fliegenden Schlange", "Schlangenszepter: Ruf der fliegenden Schlange");
            _sonderfertigkeitMapping.Add("Ritual: Brazoragh Ghorkai", "Schamanenritual: Brazoragh Ghorkai - Brazoraghs Hieb");
            _sonderfertigkeitMapping.Add("Ritual: Ergochai Tairachi", "Schamanenritual: Ergochai Tairachi -Tairachs Sklaven");
            _sonderfertigkeitMapping.Add("Ritual: Khurkachai Tairachi", "Schamanenritual: Khurkachai Tairachi -Tairachs Krieger");
            _sonderfertigkeitMapping.Add("Ritual: M'char Utrak Rikaii", "Schamanenritual: M'char Utrak Rikaii - Rikais Alchimie");
            _sonderfertigkeitMapping.Add("Liturgie: Phexens Kunstverstand (Blick für das Handwerk)", "Liturgie: Phexens Kunstverstand");
            _sonderfertigkeitMapping.Add("Liturgie: Angroschs Zorn (Waliburias Wehr)", "Liturgie: Waliburias Wehr");
            _sonderfertigkeitMapping.Add("Liturgie: Des Herren Goldener Mittag (Weisung des Himmels)", "Liturgie: Weisung des Himmels");
            _sonderfertigkeitMapping.Add("Liturgie: Wort der Wahrheit (Heiliger Befehl)", "Liturgie: Heiliger Befehl");
            _sonderfertigkeitMapping.Add("Liturgie: Lug und Trug (Unverstellter Blick)", "Liturgie: Lug und Trug");
            _sonderfertigkeitMapping.Add("Liturgie: Rahjas Erquickung (Schlaf des Gesegneten)", "Liturgie: Schlaf des Gesegneten");
        }

        private void SetVorNachteilMapping()
        {
            _vorNachteilMapping.Add("Adlige Abstammung", "Adlig (Adlige Abstammung)");
            _vorNachteilMapping.Add("Adliges Erbe", "Adlig (Adliges Erbe)");
            _vorNachteilMapping.Add("Amtsadel", "Adlig (Amtsadel)");
            _vorNachteilMapping.Add("Astrale Regeneration 1", "Astrale Regeneration I");
            _vorNachteilMapping.Add("Astrale Regeneration 2", "Astrale Regeneration II");
            _vorNachteilMapping.Add("Astrale Regeneration 3", "Astrale Regeneration III");
            _vorNachteilMapping.Add("Begabung für [Merkmal] Dämonisch", "Begabung für Merkmal (Dämonisch (gesamt))");
            _vorNachteilMapping.Add("Begabung für [Merkmal] Elementar", "Begabung für Merkmal (Elementar (gesamt))");
            _vorNachteilMapping.Add("Begabung für [Talent]", "Begabung für Talent");
            _vorNachteilMapping.Add("Begabung für [Ritual]", "Begabung für Ritual");
            _vorNachteilMapping.Add("Begabung für [Zauber]", "Begabung für Zauber");
            _vorNachteilMapping.Add("Schnelle Heilung 1", "Schnelle Heilung I");
            _vorNachteilMapping.Add("Schnelle Heilung 2", "Schnelle Heilung II");
            _vorNachteilMapping.Add("Schnelle Heilung 3", "Schnelle Heilung III");
            _vorNachteilMapping.Add("Gutaussehend", "Gut Aussehend");
            _vorNachteilMapping.Add("Unfähigkeit für [Talentgruppe] Körperlich", "Unfähigkeit für Talentgruppe (Körper)");
            _vorNachteilMapping.Add("Unfähigkeit für [Talent]", "Unfähigkeit für Talent");
            _vorNachteilMapping.Add("Herausragender Sinn Gehör", "Herausragender Sinn (Gehör)");
            _vorNachteilMapping.Add("Herausragender Sinn Geruchssinn", "Herausragender Sinn (Geruchssinn)");
            _vorNachteilMapping.Add("Herausragender Sinn Sicht", "Herausragender Sinn (Sicht)");
            _vorNachteilMapping.Add("Herausragender Sinn Tastsinn", "Herausragender Sinn (Tastsinn)");
            _vorNachteilMapping.Add("Magiedilletant", "Magiedilettant");
            _vorNachteilMapping.Add("Titularadel", "Adlig (Amtsadel)");
            _vorNachteilMapping.Add("Wolfskind intuitiv", "Wolfskind (intuitiv)");
            _vorNachteilMapping.Add("Wolfskind wissentlich", "Wolfskind (wissentlich)");
            _vorNachteilMapping.Add("Schlafstörungen 1", "Schlafstörungen I");
            _vorNachteilMapping.Add("Schlafstörungen 2", "Schlafstörungen II");
            _vorNachteilMapping.Add("Krankhafte Nekromantie", "Nekromantismus");
            _vorNachteilMapping.Add("Gesucht 1", "Gesucht I");
            _vorNachteilMapping.Add("Gesucht 2", "Gesucht II");
            _vorNachteilMapping.Add("Gesucht 3", "Gesucht III");
            _vorNachteilMapping.Add("Unbewusster Viertelzauberer ", "Viertelzauberer (unbewusst)");
            _vorNachteilMapping.Add("Eingeschränkter Sinn Gehör", "Eingeschränkter Sinn (schwerhörig)");
            _vorNachteilMapping.Add("Eingeschränkter Sinn Geruchssinn", "Eingeschränkter Sinn (Geruchssinn)");
            _vorNachteilMapping.Add("Eingeschränkter Sinn Sicht", "Eingeschränkter Sinn (kurzsichtig)");
            _vorNachteilMapping.Add("Eingeschränkter Sinn Tastsinn", "Eingeschränkter Sinn (Tastsinn)");
            _vorNachteilMapping.Add("Tolpatsch", "Tollpatsch");
        }

        private void SetZauberMapping()
        {
            _zauberMapping.Add("Analys Arkanstruktur", "Analys Arcanstruktur");
            _zauberMapping.Add("Aquafaxius Wasserstrahl", "Aquafaxius");
            _zauberMapping.Add("Archofaxius Erzstrahl", "Archofaxius");
            _zauberMapping.Add("Arcanovi Artefakt", "Arcanovi Artefakt (Spruchspeicher)");
            _zauberMapping.Add("Brenne toter Stoff!", "Brenne, toter Stoff!");
            _zauberMapping.Add("Chronoautos Zeitenfahrt", "Chrononautos Zeitenfahrt");
            _zauberMapping.Add("Eigenschaft wiederherstellen", "Eigenschaften wiederherstellen");
            _zauberMapping.Add("Frigifaxius Eisstrahl", "Frigifaxius");
            _zauberMapping.Add("Frigisphaero Eisball", "Frigosphaero");
            _zauberMapping.Add("Gletscherwand", "Wand aus Eis (Gletscherwand)");
            _zauberMapping.Add("Humofaxius Humusstrahl", "Humofaxius");
            _zauberMapping.Add("Karnifilio Raserei", "Karnifilo Raserei");
            _zauberMapping.Add("Orcanofaxius Luftstrahl", "Orcanofaxius");
            _zauberMapping.Add("Orcanosphaero Orkanball", "Orcanosphaero");
            _zauberMapping.Add("Orkanwand", "Wand aus Luft (Orkanwand)");
            _zauberMapping.Add("Planastrale Anderswelt", "Planastrale Anderwelt");
            _zauberMapping.Add("Unsichtbare Jäger", "Unsichtbarer Jäger");
            _zauberMapping.Add("Weiße Mähn und goldener Huf", "Weiße Mähn'' und gold''ner Huf");
            _zauberMapping.Add("Aquasphaero Wasserball", "Aquasphaero");
            _zauberMapping.Add("Archosphaero Erzball", "Archosphaero");
            _zauberMapping.Add("Humosphaero Humusball", "Humosphaero");
        }

        private void SetTalentMapping()
        {
            _talentMapping.Add("Zweihandhiebwaffen", "Zweihand-Hiebwaffen");
            _talentMapping.Add("Fallen stellen", "Fallenstellen");
            _talentMapping.Add("Geografie", "Geographie");
            _talentMapping.Add("Götter und Kulte", "Götter/Kulte");
            _talentMapping.Add("Sagen und Legenden", "Sagen/Legenden");
            _talentMapping.Add("Heilkunde: Gift", "Heilkunde Gift");
            _talentMapping.Add("Heilkunde: Krankheiten", "Heilkunde Krankheiten");
            _talentMapping.Add("Heilkunde: Seele", "Heilkunde Seele");
            _talentMapping.Add("Heilkunde: Wunden", "Heilkunde Wunden");
            _talentMapping.Add("Kartografie", "Kartographie");
            _talentMapping.Add("Sprachen Kennen (Alt-Imperial/Aureliani)", "Sprachen Kennen (Aureliani)");
            _talentMapping.Add("Sprachen Kennen (Urtulamidya)", "Sprachen Kennen (Ur-Tulamidya)");
            _talentMapping.Add("Lesen/Schreiben ((Alt-)Imperiale Zeichen)", "Lesen/Schreiben (Imperiale Zeichen)");
            _talentMapping.Add("Lesen/Schreiben (Altes Amulashtra)", "Lesen/Schreiben (Amulashtra)");
            _talentMapping.Add("Lesen/Schreiben (Chrmk)", "Lesen/Schreiben (Chrmk/Zelemja)");
            _talentMapping.Add("Lesen/Schreiben (Chuchas)", "Lesen/Schreiben (Chuchas/Yash-Hualay-Glyphen)");
            _talentMapping.Add("Lesen/Schreiben (Gimaril-Glyphen)", "Lesen/Schreiben (Gimaril)");
            _talentMapping.Add("Lesen/Schreiben (Urtulamidya)", "Lesen/Schreiben (Ur-Tulamidya)");
            _talentMapping.Add("Ritualkenntnis: Gildenmagie", "Ritualkenntnis (Gildenmagie)");
            _talentMapping.Add("Ritualkenntnis: Kristallomantie", "Ritualkenntnis (Kristallomantie)");
            _talentMapping.Add("Ritualkenntnis: Scharlatan", "Ritualkenntnis (Scharlatanerie)");
            _talentMapping.Add("Ritualkenntnis: Alchimist", "Ritualkenntnis (Alchimie)");
            _talentMapping.Add("Ritualkenntnis: Hexe", "Ritualkenntnis (Hexenmagie)");
            _talentMapping.Add("Ritualkenntnis: Geode", "Ritualkenntnis (Geoden)");
            _talentMapping.Add("Ritualkenntnis: Druide", "Ritualkenntnis (Druiden)");
            _talentMapping.Add("Ritualkenntnis: Derwisch", "Ritualkenntnis (Derwische)");
            _talentMapping.Add("Ritualkenntnis: Durro-Dûn", "Ritualkenntnis (Durro-Dûn)");
            _talentMapping.Add("Ritualkenntnis: Zaubertänzer", "Ritualkenntnis (Zaubertänze)");
            _talentMapping.Add("Ritualkenntnis: Zaubertänzer (novadische Sharisad)", "Ritualkenntnis (Zaubertänze)");
            _talentMapping.Add("Ritualkenntnis: Zaubertänzer (tulamische Sharisad)", "Ritualkenntnis (Zaubertänze)");
            _talentMapping.Add("Ritualkenntnis: Zaubertänzer (Majuna)", "Ritualkenntnis (Zaubertänze)");
            _talentMapping.Add("Ritualkenntnis: Zaubertänzer (Hazaqi)", "Ritualkenntnis (Zaubertänze)");
            _talentMapping.Add("Ritualkenntnis: Zibilja", "Ritualkenntnis (Zibilja)");
            _talentMapping.Add("Ritualkenntnis: Runenzauberei", "Ritualkenntnis (Runenzauberei)");
            _talentMapping.Add("Ritualkenntnis: Güldenländisch", "Ritualkenntnis (Güldenländisch)");
            _talentMapping.Add("Ritualkenntnis: Alhanisch", "Ritualkenntnis (Alhanisch)");
            _talentMapping.Add("Ritualkenntnis: Kophtanisch", "Ritualkenntnis (Kophtanisch)");
            _talentMapping.Add("Ritualkenntnis: Mudramulisch", "Ritualkenntnis (Mudramulisch)");
            _talentMapping.Add("Ritualkenntnis: Satuarisch", "Ritualkenntnis (Satuarisch)");
            _talentMapping.Add("Ritualkenntnis: Tapasuul", "Ritualkenntnis (Tapasuul)");
            _talentMapping.Add("Brettspiel", "Brett-/Kartenspiel");
        }

        private string _importPfad = string.Empty;
        private DatabaseDSADataSet _dsExport;
        private Held _held;

        private System.Collections.Generic.Dictionary<string, string> _talentMapping = new Dictionary<string, string>();
        private System.Collections.Generic.Dictionary<string, string> _zauberMapping = new Dictionary<string, string>();
        private System.Collections.Generic.Dictionary<string, string> _vorNachteilMapping = new Dictionary<string, string>();
        private System.Collections.Generic.Dictionary<string, string> _sonderfertigkeitMapping = new Dictionary<string, string>();

        private System.Collections.Generic.List<string> _importLog = new List<string>();

        private XmlDocument _xmlDoc = new XmlDocument();

        /// <summary>
        /// Konvertiert den key von Heldensoftware in eine Guid, die den key erhält.
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Guid, die mit "4e1d3250-f700-3000-" beginnt.</returns>
        public static Guid KeyToGuid(string key)
        {
            key = key.PadLeft(16, '0');
            Guid g = Guid.Parse("4e1d3250-f700-3000-" + key.Substring(0, 4) + "-" + key.Substring(4));
            return g;
        }

        /// <summary>
        /// Extrahiert den key aus einer Guid, sofern er darin abgelegt wurde.
        /// </summary>
        /// <param name="guid"></param>
        /// <returns>key oder null</returns>
        public static string GuidToKey(Guid guid)
        {
            string s = guid.ToString();
            if (!s.ToLowerInvariant().StartsWith("4e1d3250-f700-3000-"))
                return null;
            s = s.Substring(18, 0).Replace("-", "");
            s = s.TrimStart('0');
            return s;
        }

        public void ImportHeldenSoftwareFile()
        {
            _importLog.Clear();
            _xmlDoc.Load(_importPfad);

            var rowHeld = _dsExport.Held.NewHeldRow();

            var held = _xmlDoc.GetElementsByTagName("held");
            if (held.Count == 1)
            {
                // Name
                rowHeld.Name = held[0].Attributes["name"].Value.Trim();
                if (held[0].Attributes["key"] != null)
                {
                    string key = held[0].Attributes["key"].Value.Trim();
                    _held.HeldDataRow.HeldGUID = KeyToGuid(key);
                }
            }
            else // keine oder mehrere Helden in Datei
                return;

            // Rasse
            XmlNodeList rassen = _xmlDoc.SelectNodes("helden/held/basis/rasse");
            if (rassen.Count == 0)
                rassen = _xmlDoc.SelectNodes("helden/held/rasse");

            foreach (XmlNode rasse in rassen)
            {
                if (rowHeld.IsRasseNull())
                    rowHeld.Rasse = string.Empty;
                if (rowHeld.Rasse != string.Empty)
                    rowHeld.Rasse += ", ";
                rowHeld.Rasse += rasse.Attributes["string"].Value;
            }
            // Kultur
            XmlNodeList kulturen = _xmlDoc.SelectNodes("helden/held/basis/kultur");
            if (kulturen.Count == 0)
                kulturen = _xmlDoc.SelectNodes("helden/held/kultur");

            foreach (XmlNode kultur in kulturen)
            {
                if (rowHeld.IsKulturNull())
                    rowHeld.Kultur = string.Empty;
                if (rowHeld.Kultur != string.Empty)
                    rowHeld.Kultur += ", ";
                rowHeld.Kultur += kultur.Attributes["string"].Value;
            }
            // Profession
            XmlNodeList professionen = _xmlDoc.SelectNodes("helden/held/basis/ausbildungen/ausbildung");
            if (professionen.Count == 0)
                professionen = _xmlDoc.SelectNodes("helden/held/ausbildungen/ausbildung");

            foreach (XmlNode prof in professionen)
            {
                if (rowHeld.IsProfessionNull())
                    rowHeld.Profession = string.Empty;
                if (rowHeld.Profession != string.Empty)
                    rowHeld.Profession += " / ";
                rowHeld.Profession += prof.Attributes["string"].Value;
                if (professionen.Count > 1)
                    rowHeld.Profession += " (" + prof.Attributes["art"].Value + ")";
            }

            // Bild
            XmlNode bild = _xmlDoc.SelectSingleNode("helden/held/basis/portraet");
            if (bild == null)
                bild = _xmlDoc.SelectSingleNode("helden/held/portraet");

            if (bild != null)
            {
                rowHeld.BildLink = bild.Attributes["value"].Value;
            }

            // Eigenschaften
            int mod, wert;
            XmlNodeList eigenschaften = _xmlDoc.SelectNodes("helden/held/eigenschaften/eigenschaft");
            if (eigenschaften.Count == 0)
                eigenschaften = _xmlDoc.SelectNodes("helden/held/eigenschaft");

            foreach (XmlNode eigenschaft in eigenschaften)
            {
                // Wert
                if (eigenschaft.Attributes["value"] != null)
                    wert = Convert.ToInt32(eigenschaft.Attributes["value"].Value);
                else
                    wert = 0;

                // Modifikator
                if (eigenschaft.Attributes["mod"] != null)
                    mod = Convert.ToInt32(eigenschaft.Attributes["mod"].Value);
                else
                    mod = 0;

                switch (eigenschaft.Attributes["name"].Value)
                {
                    case "Mut":
                        rowHeld.MU = wert + mod;
                        break;
                    case "Klugheit":
                        rowHeld.KL = wert + mod;
                        break;
                    case "Intuition":
                        rowHeld.IN = wert + mod;
                        break;
                    case "Charisma":
                        rowHeld.CH = wert + mod;
                        break;
                    case "Fingerfertigkeit":
                        rowHeld.FF = wert + mod;
                        break;
                    case "Gewandtheit":
                        rowHeld.GE = wert + mod;
                        break;
                    case "Konstitution":
                        rowHeld.KO = wert + mod;
                        break;
                    case "Körperkraft":
                        rowHeld.KK = wert + mod;
                        break;
                    case "Sozialstatus":
                        rowHeld.SO = wert + mod;
                        break;
                    case "Lebensenergie":
                        rowHeld.LE_Mod = wert + mod;
                        break;
                    case "Ausdauer":
                        rowHeld.AU_Mod = wert + mod;
                        break;
                    case "Astralenergie":
                        rowHeld.AE_Mod = wert + mod;
                        break;
                    case "Karmaenergie":
                        rowHeld.KE_Mod = wert + mod;
                        break;
                    case "Magieresistenz":
                        rowHeld.MR_Mod = wert + mod;
                        break;
                    default:
                        break;
                }
            }
            _dsExport.Held.AddHeldRow(rowHeld);
            _dsExport.Held[0].HeldGUID = _held.Id;

            // Vor-/Nachteile
            ImportVorNachteile();

            // Sonderfertigkeiten
            ImportSonderfertigkeiten();

            // Talente
            ImportTalente();

            // Zauber
            ImportZauber();

            // Import-Log erzeugen
            ShowLogWindow();
        }

        private void ImportZauber()
        {
            int wert;
            string zauberName = string.Empty;
            string rep = string.Empty;
            string variante = string.Empty;
            string bemerkung = null;
            XmlNodeList zauberProfil = _xmlDoc.SelectNodes("helden/held/zauberliste/zauber");
            if (zauberProfil.Count == 0)
                zauberProfil = _xmlDoc.SelectNodes("helden/held/zauber");

            foreach (XmlNode zauber in zauberProfil)
            {
                bemerkung = null;
                bool added = false;
                zauberName = zauber.Attributes["name"].Value;

                if (zauber.Attributes["value"] != null)
                    wert = Convert.ToInt32(zauber.Attributes["value"].Value);
                else
                    wert = 0;

                if (zauber.Attributes["repraesentation"] != null)
                {
                    rep = zauber.Attributes["repraesentation"].Value;
                    rep = Repräsentationen.GetKürzel(Repräsentationen.GetName(rep));
                }
                if (zauber.Attributes["variante"] != null && zauber.Attributes["variante"].Value != string.Empty)
                {
                    variante = zauber.Attributes["variante"].Value;
                    if (zauberName == "Adlerschwinge Wolfsgestalt")
                        bemerkung = variante;
                    else
                        zauberName += " (" + variante + ")";
                }

                var z = App.DatenDataSet.Zauber.Select("Name LIKE '" + zauberName + "%'");
                if (z == null || z.Length == 0)
                { // Zauber wurde nicht gefunden, evtl. Konvertierung möglich
                    if (_zauberMapping.ContainsKey(zauberName))
                    {
                        z = App.DatenDataSet.Zauber.Select("Name LIKE '" + _zauberMapping[zauberName] + "%'");
                    }
                    else
                        added = false;
                }

                if (z != null && z.Length > 0)
                {
                    MeisterGeister.Daten.DatabaseDSADataSet.ZauberRow zRow = (MeisterGeister.Daten.DatabaseDSADataSet.ZauberRow)z[0];
                    if (_dsExport.Held_Zauber.FindByHeldGUIDZauberIDRepräsentation(_dsExport.Held[0].HeldGUID, zRow.ZauberID, rep) == null)
                    {
                        _dsExport.Held_Zauber.AddHeld_ZauberRow(_dsExport.Held[0], zRow, wert, rep, bemerkung, zRow.Name);
                        added = true;
                    }
                }

                if (!added) // Import nicht möglich
                    AddImportLog(ImportTypen.Zauber, string.Format("{0} [{1}]", zauberName, rep), wert);
            }
        }

        private void ImportTalente()
        {
            int wert;
            int atZuteilung = 0;
            int paZuteilung = 0;
            int atBasis = 0;
            int paBasis = 0;
            string talentName = string.Empty;

            // AT/PA-Basiswert
            XmlNode atBasisNode = _xmlDoc.SelectSingleNode(string.Format("helden/held/eigenschaften/eigenschaft[@name='at']"));
            if (atBasisNode == null)
                atBasisNode = _xmlDoc.SelectSingleNode(string.Format("helden/held/eigenschaft[@name='at']"));
            XmlNode paBasisNode = _xmlDoc.SelectSingleNode(string.Format("helden/held/eigenschaften/eigenschaft[@name='pa']"));
            if (paBasisNode == null)
                paBasisNode = _xmlDoc.SelectSingleNode(string.Format("helden/held/eigenschaft[@name='pa']"));
            if (atBasisNode != null)
                if (atBasisNode.Attributes["value"] != null)
                    atBasis = Convert.ToInt32(atBasisNode.Attributes["value"].Value);
            if (paBasisNode != null)
                if (paBasisNode.Attributes["value"] != null)
                    paBasis = Convert.ToInt32(atBasisNode.Attributes["value"].Value);

            XmlNodeList talente = _xmlDoc.SelectNodes("helden/held/talentliste/talent");
            if (talente.Count == 0)
                talente = _xmlDoc.SelectNodes("helden/held/talent");

            foreach (XmlNode talent in talente)
            {
                bool added = false;
                talentName = talent.Attributes["name"].Value;

                // Kampfwerte
                XmlNode atNode = _xmlDoc.SelectSingleNode(string.Format("helden/held/kampf/kampfwerte[@name='{0}']/attacke", talentName));
                if (atNode == null)
                    atNode = _xmlDoc.SelectSingleNode(string.Format("helden/held/kampfwerte[@name='{0}']/attacke", talentName));
                XmlNode paNode = _xmlDoc.SelectSingleNode(string.Format("helden/held/kampf/kampfwerte[@name='{0}']/parade", talentName));
                if (paNode == null)
                    paNode = _xmlDoc.SelectSingleNode(string.Format("helden/held/kampfwerte[@name='{0}']/parade", talentName));

                atZuteilung = 0;
                paZuteilung = 0;

                if (atNode != null)
                    if (atNode.Attributes["value"] != null)
                        atZuteilung = Convert.ToInt32(atNode.Attributes["value"].Value) - atBasis;
                if (paNode != null)
                    if (paNode.Attributes["value"] != null)
                        paZuteilung = Convert.ToInt32(paNode.Attributes["value"].Value) - paBasis;

                // Sonderfälle: Sprachen Kennen und Lesen/Schreiben
                if (talentName.StartsWith("Lesen/Schreiben"))
                    talentName = string.Format("Lesen/Schreiben ({0})", talentName.Replace("Lesen/Schreiben ", string.Empty));
                else if (talentName.StartsWith("Sprachen kennen"))
                    talentName = string.Format("Sprachen Kennen ({0})", talentName.Replace("Sprachen kennen ", string.Empty));

                if (talent.Attributes["value"] != null)
                    wert = Convert.ToInt32(talent.Attributes["value"].Value);
                else
                    wert = 0;

                var t = App.DatenDataSet.Talent.FindByTalentname(talentName);
                if (t != null)
                {
                    _dsExport.Held_Talent.AddHeld_TalentRow(t, wert, _dsExport.Held[0], null, atZuteilung, paZuteilung, t.TalentgruppeID);
                    added = true;
                }
                else
                { // Talent wurde nicht gefunden, evtl. Konvertierung möglich
                    if (_talentMapping.ContainsKey(talentName))
                    {
                        t = App.DatenDataSet.Talent.FindByTalentname(_talentMapping[talentName]);
                        if (t != null)
                        {
                            _dsExport.Held_Talent.AddHeld_TalentRow(t, wert, _dsExport.Held[0], null, 0, 0, t.TalentgruppeID);
                            added = true;
                        }
                        else
                            added = false;
                    }
                    else
                        added = false;
                }
                if (!added) // Import nicht möglich
                    AddImportLog(ImportTypen.Talent, talentName, wert);
            }
        }

        private void ImportSonderfertigkeiten()
        {
            string sfName = string.Empty;
            string wertString = string.Empty;
            string sfXpath = "helden/held/sf/sonderfertigkeit";
            XmlNodeList sonderfertigkeiten = _xmlDoc.SelectNodes(sfXpath);

            if (sonderfertigkeiten.Count == 0)
            {
                sfXpath = "helden/held/sonderfertigkeit";
                sonderfertigkeiten = _xmlDoc.SelectNodes(sfXpath);
            }

            foreach (XmlNode sonderfertigkeit in sonderfertigkeiten)
            {
                sfName = sonderfertigkeit.Attributes["name"].Value;

                if (sonderfertigkeit.Attributes["value"] != null)
                    wertString = sonderfertigkeit.Attributes["value"].Value;
                else
                    wertString = null;

                bool added = false;

                // Kulturkunde,  Scharfschütze, Meisterschütze, Schnellladen
                if (!added && (sfName == "Kulturkunde" || sfName == "Scharfschütze"
                    || sfName == "Meisterschütze" || sfName == "Schnellladen"))
                {
                    string sub = null;
                    if (sfName == "Kulturkunde")
                        sub = "kultur";
                    else if (sfName == "Ortskenntnis")
                        sub = "auswahl";
                    else if (sfName == "Scharfschütze" || sfName == "Meisterschütze" || sfName == "Schnellladen")
                        sub = "talent";
                    XmlNodeList subNodes = _xmlDoc.SelectNodes(string.Format("{2}[contains(@name,'{0}')]/{1}", sfName, sub, sfXpath));
                    foreach (XmlNode s in subNodes)
                    {
                        string sfNameNeu = string.Format("{0} ({1})", sfName, s.Attributes["name"].Value);
                        if (_sonderfertigkeitMapping.ContainsKey(sfNameNeu))
                            sfNameNeu = _sonderfertigkeitMapping[sfNameNeu];

                        added = AddSonderfertigkeit(sfNameNeu, wertString);
                        if (!added) // Import nicht mögliche
                            AddImportLog(ImportTypen.Sonderfertigkeit, sfName, s.Attributes["name"].Value);
                    }
                    continue;
                }
                // Rüstungsgewöhnung I, Ortskenntnis
                if (!added && (sfName == "Rüstungsgewöhnung I" || sfName == "Ortskenntnis"))
                {
                    string sub = null;
                    if (sfName == "Rüstungsgewöhnung I")
                        sub = "gegenstand";
                    else if (sfName == "Ortskenntnis")
                        sub = "auswahl";
                    XmlNodeList subNodes = _xmlDoc.SelectNodes(string.Format("{2}[contains(@name,'{0}')]/{1}", sfName, sub, sfXpath));
                    foreach (XmlNode s in subNodes)
                    {
                        added = AddSonderfertigkeit(sfName, s.Attributes["name"].Value);
                        if (!added) // Import nicht mögliche
                            AddImportLog(ImportTypen.Sonderfertigkeit, sfName, s.Attributes["name"].Value);
                    }
                    continue;
                }
                // Talentspezialisierung
                if (!added && sfName.StartsWith("Talentspezialisierung"))
                    added = AddSonderfertigkeit("Talentspezialisierung", sfName.Replace("Talentspezialisierung ", null));
                // Zauberspezialisierung
                if (!added && sfName.StartsWith("Zauberspezialisierung"))
                    added = AddSonderfertigkeit("Zauberspezialisierung", sfName.Replace("Zauberspezialisierung ", null));
                // Ritualkenntnis (Schamanentradition)
                if (!added && sfName.StartsWith("Ritualkenntnis"))
                    added = AddSonderfertigkeit(sfName.Replace("Ritualkenntnis: ", "Ritualkenntnis (") + ")", wertString);
                // Waffenlose Kampftechniken
                if (!added && sfName.StartsWith("Waffenloser Kampfstil"))
                    added = AddSonderfertigkeit(sfName.Replace("Waffenloser Kampfstil: ", "Waffenlose Kampftechnik (") + ")", wertString);
                if (!added && sfName.StartsWith("Merkmalskenntnis"))
                    added = AddSonderfertigkeit(sfName.Replace("Merkmalskenntnis: ", "Merkmalskenntnis (") + ")", wertString); // Merkmalskenntnis
                if (!added && sfName.StartsWith("Gabe des Odûn"))
                    added = AddSonderfertigkeit(sfName.Replace("Gabe des Odûn", "Odûn-Gabe"), wertString); // Odûn-Gabe
                if (!added && sfName.StartsWith("Ritual:"))
                    added = AddSonderfertigkeit(sfName.Replace("Ritual:", "Schamanenritual:"), wertString); // Schamanenritual
                if (!added)
                    added = AddSonderfertigkeit(sfName, wertString);
                if (!added)
                    added = AddSonderfertigkeit(string.Format("Waffenloses Manöver ({0})", sfName), wertString); // Waffenlose Manöver
                if (!added)
                    added = AddSonderfertigkeit(string.Format("Geländekunde ({0})", sfName), wertString); // Geländekunden

                if (!added)
                {
                    if (_sonderfertigkeitMapping.ContainsKey(sfName))
                        added = AddSonderfertigkeit(_sonderfertigkeitMapping[sfName], wertString);
                }
                // Sonderfertigkeit wurde immer noch nicht gefunden, evtl. Mapping mit Wert möglich
                if (!added)
                    added = AddSonderfertigkeit(sfName + " " + wertString, null);
                // Vor-/Nachteil wurde immer noch nicht gefunden, evtl. Mapping mit Wert möglich
                if (!added)
                {
                    if (_sonderfertigkeitMapping.ContainsKey(sfName + " " + wertString))
                        added = AddSonderfertigkeit(_sonderfertigkeitMapping[sfName + " " + wertString], null);
                }

                if (!added) // Import nicht mögliche
                    AddImportLog(ImportTypen.Sonderfertigkeit, sfName, wertString);
            }
        }

        private void ImportVorNachteile()
        {
            string vorNachteilName = string.Empty;
            string wertString = string.Empty;
            string vtXpath = "helden/held/vt/vorteil";
            XmlNodeList vorNachteile = _xmlDoc.SelectNodes(vtXpath);
            if (vorNachteile.Count == 0)
            {
                vtXpath = "helden/held/vorteil";
                vorNachteile = _xmlDoc.SelectNodes(vtXpath);
            }

            foreach (XmlNode vorNachteil in vorNachteile)
            {
                vorNachteilName = vorNachteil.Attributes["name"].Value.Trim();

                if (vorNachteil.Attributes["value"] != null)
                    wertString = vorNachteil.Attributes["value"].Value;
                else
                    wertString = null;

                bool added = false;

                if (!added && vorNachteilName == "Begabung für [Merkmal]")
                    added = AddVorNachteil(string.Format("Begabung für Merkmal ({0})", wertString), null); // Begabung für Merkmal
                else if (!added && vorNachteilName == "Begabung für [Talentgruppe]")
                    added = AddVorNachteil(string.Format("Begabung für Talentgruppe ({0})", (wertString == "Körperlich" ? "Körper" : wertString)), null); // Begabung für Talentgruppe
                else if (!added && vorNachteilName == "Unfähigkeit für [Merkmal]")
                    added = AddVorNachteil(string.Format("Unfähigkeit für Merkmal ({0})", wertString), null); // Unfähigkeit für Merkmal
                else if (!added && vorNachteilName == "Unfähigkeit für [Talentgruppe]")
                    added = AddVorNachteil(string.Format("Unfähigkeit für Talentgruppe ({0})", wertString), null); // Unfähigkeit für Talentgruppe
                else if (!added && vorNachteilName.StartsWith("Angst vor")) // Angst vor...
                {
                    string[] angstVorTeile = vorNachteilName.Split(' ');
                    string angstVor = angstVorTeile[2] + string.Format(" ({0})", wertString);
                    added = AddVorNachteil("Angst vor", angstVor);
                }
                else if (!added && vorNachteilName == "Vorurteile (stark)") // Vorurteile (stark)...
                    added = AddVorNachteil("Vorurteile", string.Format("stark ({0})", vorNachteil.Attributes["value"].Value));
                else if (!added && vorNachteilName.StartsWith("Moralkodex")) // Moralkodex
                    added = AddVorNachteil("Moralkodex Kirche", vorNachteilName.Replace("Moralkodex [", null).TrimEnd(']'));
                else if (!added && (vorNachteilName.StartsWith("Herausragende Eigenschaft")
                    || vorNachteilName.StartsWith("Miserable Eigenschaft"))) // Herausragende/Miserable Eigenschaft
                {
                    string eigenschaft = vorNachteilName.Split(':')[1].Trim();
                    string eigenschaftKürzel = Eigenschaften.GetKürzel(eigenschaft);
                    if (vorNachteilName.StartsWith("Herausragende Eigenschaft"))
                        added = AddVorNachteil(vorNachteilName.Replace("Herausragende Eigenschaft: ", "Herausragende Eigenschaft (")
                            .Replace(eigenschaft, eigenschaftKürzel + ")"), wertString);
                    else
                        added = AddVorNachteil(vorNachteilName.Replace("Miserable Eigenschaft: ", "Miserable Eigenschaft (")
                            .Replace(eigenschaft, eigenschaftKürzel + ")"), wertString);
                }

                if (!added)
                    added = AddVorNachteil(vorNachteilName, wertString);

                // Vor-/Nachteil wurde nicht gefunden, evtl. Mapping möglich
                if (!added)
                {
                    if (_vorNachteilMapping.ContainsKey(vorNachteilName))
                        added = AddVorNachteil(_vorNachteilMapping[vorNachteilName], wertString);
                }
                // Vor-/Nachteil wurde immer noch nicht gefunden, evtl. Mapping mit Wert möglich
                if (!added)
                    added = AddVorNachteil(vorNachteilName + " " + wertString, null);
                // Vor-/Nachteil wurde immer noch nicht gefunden, evtl. Mapping mit Wert möglich
                if (!added)
                {
                    if (_vorNachteilMapping.ContainsKey(vorNachteilName + " " + wertString))
                        added = AddVorNachteil(_vorNachteilMapping[vorNachteilName + " " + wertString], null);
                }

                if (!added) // Import nicht möglich
                    AddImportLog(ImportTypen.VorNachteil, vorNachteilName, wertString);
            }
        }

        private bool AddVorNachteil(string vorNachteilName, string wertString)
        {
            var sf = App.DatenDataSet.VorNachteil.Select("Name = '" + vorNachteilName.Replace("'", "''") + "'");
            if (sf != null && sf.Length > 0)
            {
                var vorNachteilRow = App.DatenDataSet.VorNachteil.FindByVorNachteilID(Convert.ToInt32(sf[0]["VorNachteilID"]));
                var vorNachteilHeldRow = _dsExport.Held_VorNachteil.FindByHeldGUIDVorNachteilID(_held.Id, vorNachteilRow.VorNachteilID);
                if (vorNachteilHeldRow == null)
                    _dsExport.Held_VorNachteil.AddHeld_VorNachteilRow(_dsExport.Held[0], vorNachteilRow, wertString, vorNachteilRow.Name, vorNachteilRow.Typ);
                else // Vor-Nachteil bereits vorhanden
                {
                    if ((vorNachteilHeldRow.Wert + ", " + wertString).Length > App.DatenDataSet.Held_VorNachteil.WertColumn.MaxLength)
                        return false;
                    vorNachteilHeldRow.Wert += ", " + wertString;
                }
                return true;
            }
            return false;
        }

        private bool AddSonderfertigkeit(string sfName, string wertString = null)
        {
            var sf = App.DatenDataSet.Sonderfertigkeit.Select("Name = '" + sfName.Replace("'", "''") + "'");
            if (sf != null && sf.Length > 0)
            {
                var sfRow = App.DatenDataSet.Sonderfertigkeit.FindBySonderfertigkeitID(Convert.ToInt32(sf[0]["SonderfertigkeitID"]));
                var sfHeldRow = _dsExport.Held_Sonderfertigkeit.FindByHeldGUIDSonderfertigkeitID(_held.Id, sfRow.SonderfertigkeitID);
                if (sfHeldRow == null)
                    _dsExport.Held_Sonderfertigkeit.AddHeld_SonderfertigkeitRow(_dsExport.Held[0], sfRow, wertString, string.Empty, string.Empty);
                else // Sonderfertigkeit bereits vorhanden
                {
                    if (sfHeldRow.IsWertNull())
                        sfHeldRow.Wert = wertString;
                    else
                    {
                        if ((sfHeldRow.Wert + ", " + wertString).Length > App.DatenDataSet.Held_Sonderfertigkeit.WertColumn.MaxLength)
                            return false;
                        sfHeldRow.Wert += ", " + wertString;
                    }
                }
                return true;
            }
            return false;
        }

        private void AddImportLog(ImportTypen typ, string name, object wert)
        {
            string typString = string.Empty;
            string hinweis = string.Empty;
            switch (typ)
            {
                case ImportTypen.VorNachteil:
                    typString = "Vor-/Nachteil: ";
                    break;
                case ImportTypen.Sonderfertigkeit:
                    typString = "Sonderfertigkeit: ";
                    break;
                case ImportTypen.Talent:
                    typString = "Talent: ";
                    break;
                case ImportTypen.Zauber:
                    typString = "Zauber: ";
                    break;
                default:
                    break;
            }

            _importLog.Add(string.Format("{0}{1} {2} {3}", typString, name, wert, hinweis));
        }

        private void ShowLogWindow()
        {
            if (_importLog.Count > 0)
            {
                System.Windows.Window gui = new System.Windows.Window();
                gui.Title = "Import von Helden-Software";
                gui.Height = 650;
                gui.Width = 500;
                string log = string.Empty;
                foreach (var item in _importLog)
                    log += item + "\n";
                System.Windows.Controls.TextBox txtLog = new System.Windows.Controls.TextBox();
                txtLog.IsReadOnly = true;
                txtLog.AcceptsReturn = true;
                txtLog.Text = _importPfad 
                    + "\n\nEinige Werte konnten nicht importiert werden. Wenn du bei der Verbesserung der Import-Funktion mitwirken möchtest, sende bitte die Helden-XML-Datei an 'info@meistergeister.org' oder melde das Problem im Forum (http://meistergeister.siteboard.org/f14-bug-meldungen.html). Vielen Dank!.\n\n";
                txtLog.Text += log;
                txtLog.TextWrapping = System.Windows.TextWrapping.Wrap;
                txtLog.VerticalScrollBarVisibility = System.Windows.Controls.ScrollBarVisibility.Visible;
                gui.Content = txtLog;
                gui.Show();
            }
        }
    }

    public enum ImportTypen
    {
        VorNachteil,
        Sonderfertigkeit,
        Talent,
        Zauber
    }
}
