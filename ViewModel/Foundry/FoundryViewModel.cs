using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Net;
using System.Windows.Media.Imaging;
using MeisterGeister.Model;
using MeisterGeister.View.General;
//Eigene usings
using MeisterGeister.Logic.Einstellung;
using System.Windows.Forms;
using System.Windows.Documents;
using Newtonsoft.Json;

namespace MeisterGeister.ViewModel.Foundry
{
    public class FoundryViewModel : Base.ToolViewModelBase
    {

        //TODO:  Helden: Bars -Always visible, Lep, AsP
        //TODO:  Foundry Pfad vom User definierbar, read Options, set Web-Connection (lokal oder Inet auswählbar)
        //TODO:  SpielerScreen: Zeige WebBrowser
        #region //---- Constante ----

        private static string ErsetzeUmlaute(string s)
        {
            if (s == null)
                return "";
            s = s.Replace("ä", "ae");
            s = s.Replace("ü", "ue");
            s = s.Replace("ö", "oe");
            s = s.Replace("ß", "ss");
            return s.Replace("(", "").Replace(")", "").Replace(" ", "_").Replace("/", "_").ToLower();
        }
        public static string GetTalent_sid(string talent)
        {
            switch (talent)
            {
                case "Zweihandschwerter":
                    return "talent-zweihandschwerter__saebel";
                case "Zweihandsäbel":
                    return "talent-zweihandschwerter__saebel";
                case "Wurfbeile":
                    return "talent-wurfbeile";
                case "Peitsche":
                    return "talent-peitsche";
                case "Infanteriewaffen":
                    return "talent-infanteriewaffen";
                case "Wurfspeere":
                    return "talent-wurfspeere";
                case "Blasrohr":
                    return "talent-blasrohr";
                case "Ringen":
                    return "talent-ringen";
                case "Schleuder":
                    return "talent-schleuder";
                case "Diskus":
                    return "talent-diskus";
                case "Zweihandhiebwaffen":
                    return "talent-zweihand_hiebwaffen";
                case "Belagerungswaffen":
                    return "talent-belagerungswaffen";
                case "Raufen":
                    return "talent-raufen";
                case "Stäbe":
                    return "talent-staebe";
                case "Kettenstäbe":
                    return "talent-kettenstaebe";
                case "Lanzenreiten":
                    return "talent-lanzenreiten";
                case "Bogen":
                    return "talent-bogen";
                case "Kettenwaffen":
                    return "talent-kettenwaffen";
                case "Säbel":
                    return "talent-saebel";
                case "Hiebwaffen":
                    return "talent-hiebwaffen";
                case "Anderthalbhaender":
                    return "talent-anderthalbhaender";
                case "Fechtwaffen":
                    return "talent-fechtwaffen";
                case "Speere":
                    return "talent-speere";
                case "Dolche":
                    return "talent-dolche";
                case "Wurfmesser":
                    return "talent-wurfmesser";
                case "Zweihandflegel":
                    return "talent-zweihandflegel";
                case "Schwerter":
                    return "talent-schwerter";
                case "Armbrust":
                    return "talent-armbrust";
                default:
                    return "talent-" + ErsetzeUmlaute(talent);
            }
        }
        public static string GetCategory(string cat)
        {
            switch (cat)
            {
                case "Kampf":
                    return "combat";
                case "Körper":
                    return "physical";
                case "Handwerk":
                    return "crafting";
                case "Natur":
                    return "nature";
                case "Gabe":
                    return "gift";
                case "Wissen":
                    return "knowledge";
                case "Gesellschaft":
                    return "social";
                case "Liturgiekenntnis":
                    return "ability-liturgiekenntnis";     //????
                case "Ritualkenntnis":
                    return "ability-ritualkenntnis";     //????
                case "Meta":
                    return "meta";       //????
                case "Sprachen/Schriften":
                    return "language";     //????
                default:
                    return cat.ToLower();
            }
        }
        public static string GetAttribute(string cat)
        {
            switch (cat)
            {
                case "MU":
                    return "courage";
                case "KL":
                    return "cleverness";
                case "IN":
                    return "intuition";
                case "CH":
                    return "charisma";
                case "FF":
                    return "agility";
                case "GE":
                    return "dexterity";
                case "KO":
                    return "constitution";
                case "KK":
                    return "strength";
                default:
                    return "";
            }
        }
        public static string GetSF_sid(string sf)
        {
            if (sf == "Traumgänger I")
                sf = "Traumgänger";
            if (sf.StartsWith("Geländekunde") || sf.StartsWith("Kulturkunde") || sf.StartsWith("Schnellladen") || sf.StartsWith("Schnellziehen") || sf.StartsWith("Scharfschütze") ||
                sf.StartsWith("Repräsentation")|| sf.StartsWith("Waffenloses Manöver")|| sf.StartsWith("Merkmalskenntnis")|| sf.StartsWith("Ritualkenntnis"))
                sf = sf.Substring(0, sf.IndexOf(" ("));
            if (sf.StartsWith("Elfenlied:")|| sf.StartsWith("Hexenfluch:"))
                sf = sf.Substring(0, sf.IndexOf(":"));
            switch (sf)
            {
                case "Ritualkenntnis":
                    return "ability-ritualkenntnis";
                case "Schildkampf II":
                    return "ability-schildkampf-ii";
                case "Ausweichen III":
                    return "ability-ausweichen-iii";
                case "Turnierreiterei":
                    return "ability-turnierreiterei";
                case "Ausfall":
                    return "ability-ausfall";
                case "Fälscher":
                    return "ability-faelscher";
                case "Hammerschlag":
                    return "ability-hammerschlag";
                case "Meisterliche Zauberkontrolle II":
                    return "ability-meisterliche-zauberkontrolle-ii";
                case "Standfest":
                    return "ability-standfest";
                case "Golembauer":
                    return "ability-golembauer";
                case "Matrixverständnis":
                    return "ability-matrixverstaendnis";
                case "Tierischer Begleiter":
                    return "ability-tierischer-begleiter-";
                case "Astrale Meditation":
                    return "ability-astrale-meditation";
                case "Regeneration II":
                    return "ability-regeneration-ii";
                case "Verbotene Pforten":
                    return "ability-verbotene-pforten";
                case "Defensiver Kampfstil":
                    return "ability-defensiver-kampfstil";
                case "Druidenrache":
                    return "ability-druidenrache";
                case "Berittener Schütze":
                    return "ability-berittener-schuetze";
                case "Exorzist":
                    return "ability-exorzist";
                case "Klingentänzer":
                    return "ability-klingentaenzer";
                case "Schnellziehen":
                    return "ability-schnellziehen";
                case "Elementarharmonisierte Aura":
                    return "ability-elementarharmonisierte-aura";
                case "Eiserner Wille II":
                    return "ability-eiserner-wille-ii";
                case "Stapeleffekt":
                    return "ability-stapeleffekt";
                case "Karmalqueste":
                    return "ability-karmalqueste";
                case "Waffenlose Kampftechnik (Bornländisch/Gossenstil)":
                    return "ability-waffenlose-kampftechnik-bornlaendisch";
                case "Salasandra":
                    return "ability-salasandra";
                case "Schamanistische":
                    return "ability-schamanistische";
                case "Elfenlied":
                    return "ability-elfenlieder";
                case "Schildkampf I":
                    return "ability-schildkampf-i";
                case "Semipermanenz I":
                    return "ability-semipermanenz-i";
                case "Matrixkontrolle":
                    return "ability-matrixkontrolle";
                case "Parierwaffen II":
                    return "ability-parierwaffen-ii";
                case "Parierwaffen I":
                    return "ability-parierwaffen-i";
                case "Invocatio Integra":
                    return "ability-invocatio-integra";
                case "Gedankenschutz":
                    return "ability-gedankenschutz";
                case "Spätweihe":
                    return "ability-spaetweihe";
                case "Rüstungsgewöhnung III":
                    return "ability-ruestungsgewoehnung-iii";
                case "Eiserner Wille I":
                    return "ability-eiserner-wille-i";
                case "Vertrautenbindung":
                    return "ability-vertrautenbindung";
                case "Ottagaldr, Nichtzauberer":
                    return "ability-ottagaldr-nichtzauberer";
                case "Schnellladen":
                    return "ability-schnellladen";
                case "Runenkunde":
                    return "ability-runenkunde";
                case "Kampf im Wasser":
                    return "ability-kampf-im-wasser";
                case "Beidhändiger Kampf II":
                    return "ability-beidhaendiger-kampf-ii";
                case "Betäubungsschlag":
                    return "ability-betaeubungsschlag";
                case "Hexenfluch":
                    return "ability-hexenflueche-";
                case "Zauberspezialisierung":
                    return "ability-zauberspezialisierung";
                case "Nandusgefälliges Wissen":
                    return "ability-nandusgefaelliges-wissen";
                case "Zauberkontrolle":
                    return "ability-zauberkontrolle";
                case "Waffenmeister":
                    return "ability-waffenmeister";
                case "Spießgespann":
                    return "ability-spieszgespann";
                case "Aufmerksamkeit":
                    return "ability-aufmerksamkeit";
                case "Druidische Herrschaftsrituale":
                    return "ability-druidische-herrschaftsrituale";
                case "Formation":
                    return "ability-formation";
                case "Wuchtschlag":
                    return "ability-wuchtschlag";
                case "Kristallomantische Rituale":
                    return "ability-kristallomantische-rituale";
                case "Linkhand":
                    return "ability-linkhand";
                case "Akklimatisierung Hitze":
                    return "ability-akklimatisierung-hitze";
                case "Befreiungsschlag":
                    return "ability-befreiungsschlag";
                case "Regeneration I":
                    return "ability-regeneration-i";
                case "Meisterliche Regeneration":
                    return "ability-meisterliche-regeneration";
                case "Kriegsreiterei":
                    return "ability-kriegsreiterei";
                case "Traumgänger":
                    return "ability-traumgaenger";
                case "Kugelzauber":
                    return "ability-kugelzauber";
                case "Waffenlose Kampftechnik (Hammerfaust)":
                    return "ability-waffenlose-kampftechnik-hammerfaust";
                case "Finte":
                    return "ability-finte";
                case "Nekromant":
                    return "ability-nekromant";
                case "Geschützmeister":
                    return "ability-geschuetzmeister";
                case "Waffenlose Kampftechnik (Unauer Schule)":
                    return "ability-waffenlose-kampftechnik-unauer-schule";
                case "Aura der Heiligkeit":
                    return "ability-aura-der-heiligkeit";
                case "Gegenhalten":
                    return "ability-gegenhalten";
                case "Ottagaldr, Zauberer":
                    return "ability-ottagaldr-zauberer";
                case "Blutmagie":
                    return "ability-blutmagie";
                case "Schuppenbeutel":
                    return "ability-schuppenbeutel";
                case "Binden":
                    return "ability-binden";
                case "Meisterliche Zauberkontrolle I":
                    return "ability-meisterliche-zauberkontrolle-i";
                case "Festnageln":
                    return "ability-festnageln";
                case "Kraftspeicher":
                    return "ability-kraftspeicher";
                case "Signaturkenntnis":
                    return "ability-signaturkenntnis";
                case "Vielfache Ladungen":
                    return "ability-vielfache-ladungen";
                case "Kontakt zum Großen Geist":
                    return "ability-kontakt-zum-groszen-geist";
                case "Odûns Gaben":
                    return "ability-odns-gaben";
                case "Lockeres Zaubern":
                    return "ability-lockeres-zaubern";
                case "Tanz der Mada":
                    return "ability-tanz-der-mada";
                case "Zauberzeichen":
                    return "ability-zauberzeichen";
                case "Zaubertänzer":
                    return "ability-zaubertaenze";
                case "Meister der Improvisation":
                    return "ability-meister-der-improvisation";
                case "Todesstoß":
                    return "ability-todesstosz";
                case "Repräsentation":
                    return "ability-repraesentation";
                case "Waffenlose Kampftechnik (Gladiatorenstil)":
                    return "ability-waffenlose-kampftechnik-gladiatorenstil";
                case "Trommelzauber":
                    return "ability-trommelzauber-";
                case "Matrixregeneration I":
                    return "ability-matrixregeneration-i";
                case "Höhere Dämonenbindung":
                    return "ability-hoehere-daemonenbindung";
                case "Ausweichen II":
                    return "ability-ausweichen-ii";
                case "Liturgiekenntnis":
                    return "ability-liturgiekenntnis";
                case "Schlangenring-Zauber":
                    return "ability-schlangenringzauber";
                case "Improvisierte Waffen":
                    return "ability-improvisierte-waffen";
                case "Meisterschütze":
                    return "ability-meisterschuetze";
                case "Geodenrituale":
                    return "ability-geodenrituale-";
                case "Beidhändiger Kampf I":
                    return "ability-beidhaendiger-kampf-i";
                case "Klingensturm":
                    return "ability-klingensturm";
                case "Dämonenbindung I":
                    return "ability-daemonenbindung-i";
                case "Simultanzaubern":
                    return "ability-simultanzaubern";
                case "Keulenrituale":
                    return "ability-keulenrituale-";
                case "Waffenloses Manöver":
                    return "ability-waffenloses-manoever";
                case "Zauber vereinigen":
                    return "ability-zauber-vereinigen";
                case "Unterwasserkampf":
                    return "ability-unterwasserkampf";
                case "Rüstungsgewöhnung I":
                    return "ability-ruestungsgewoehnung-i";
                case "Ortskenntnis":
                    return "ability-ortskenntnis";
                case "Waffenlose Kampftechnik (Mercenario)":
                    return "ability-waffenlose-kampftechnik-mercenario";
                case "Geber der Gestalt":
                    return "ability-geber-der-gestalt";
                case "Merkmalskenntnis":
                    return "ability-merkmalskenntnis";
                case "Zauberroutine":
                    return "ability-zauberroutine";
                case "Sturmangriff":
                    return "ability-sturmangriff";
                case "Zibilja-Rituale":
                    return "ability-zibiljarituale";
                case "Aura verhüllen":
                    return "ability-aura-verhuellen";
                case "Halbschwert":
                    return "ability-halbschwert";
                case "Akklimatisierung Kälte":
                    return "ability-akklimatisierung-kaelte";
                case "Chimärenmeister":
                    return "ability-chimaerenmeister";
                case "Geländekunde":
                    return "ability-gelaendekunde";
                case "Zauber bereithalten":
                    return "ability-zauber-bereithalten";
                case "Hypervehemenz":
                    return "ability-hypervehemenz";
                case "Waffenspezialisierung":
                    return "ability-waffenspezialisierung";
                case "Umreißen":
                    return "ability-umreiszen";
                case "Scharfschütze":
                    return "ability-scharfschuetze";
                case "Matrixregeneration II":
                    return "ability-matrixregeneration-ii";
                case "Berufsgeheimnis":
                    return "ability-berufsgeheimnis";
                case "Stabzauber":
                    return "ability-stabzauber";
                case "Waffe zerbrechen":
                    return "ability-waffe-zerbrechen";
                case "Zauber unterbrechen":
                    return "ability-zauber-unterbrechen";
                case "Gezielter Stich":
                    return "ability-gezielter-stich";
                case "Entwaffnen":
                    return "ability-entwaffnen";
                case "Kraftlinienmagie II":
                    return "ability-kraftlinienmagie-ii";
                case "Konzentrationsstärke":
                    return "ability-konzentrationsstaerke";
                case "Akoluth":
                    return "ability-akoluth";
                case "Eisenhagel":
                    return "ability-eisenhagel";
                case "Fernzauberei":
                    return "ability-fernzauberei";
                case "Gefäß der Sterne":
                    return "ability-gefaesz-der-sterne";
                case "Apport":
                    return "ability-apport";
                case "Meisterparade":
                    return "ability-meisterparade";
                case "Klingenwand":
                    return "ability-klingenwand";
                case "Liturgien":
                    return "ability-liturgien";
                case "Tod von links":
                    return "ability-tod-von-links";
                case "Kraftkontrolle":
                    return "ability-kraftkontrolle";
                case "Kampfreflexe":
                    return "ability-kampfreflexe";
                case "Schalenzauber":
                    return "ability-schalenzauber";
                case "Waffenlose Kampftechnik (Hruruzat)":
                    return "ability-waffenlose-kampftechnik-hruruzat";
                case "Bannschwert":
                    return "ability-bannschwert";
                case "Große Meditation":
                    return "ability-grosze-meditation";
                case "Blindkampf":
                    return "ability-blindkampf";
                case "Reiterkampf":
                    return "ability-reiterkampf";
                case "Talentspezialisierung":
                    return "ability-talentspezialisierung";
                case "Kulturkunde":
                    return "ability-kulturkunde";
                case "Kampfgespür":
                    return "ability-kampfgespuer";
                case "Doppelangriff":
                    return "ability-doppelangriff";
                case "Ausweichen I":
                    return "ability-ausweichen-i";
                case "Meisterliches Entwaffnen":
                    return "ability-meisterliches-entwaffnen";
                case "Aurapanzer":
                    return "ability-aurapanzer";
                case "Niederwerfen":
                    return "ability-niederwerfen";
                case "Semipermanenz II":
                    return "ability-semipermanenz-ii";
                case "Rosstäuscher":
                    return "ability-rosstaeuscher";
                case "Rüstungsgewöhnung II":
                    return "ability-ruestungsgewoehnung-ii";
                case "Druidische Dolchrituale":
                    return "ability-druidische-dolchrituale";
                case "Matrixgeber":
                    return "ability-matrixgeber";
                case "Schildspalter":
                    return "ability-schildspalter";
                case "Windmühle":
                    return "ability-windmuehle";
                case "Kraftlinienmagie I":
                    return "ability-kraftlinienmagie-i";
                case "Dämonenbindung II":
                    return "ability-daemonenbindung-ii";
                case "Form der Formlosigkeit":
                    return "ability-form-der-formlosigkeit";
                default:
                    return "ability-" + ErsetzeUmlaute(sf);
            }
        }

        public static string GetZauber_sid(string z)
        {
            switch (z)
            {
                case "Applicatus Zauberspeicher": return "spell-applicatus";
                case "Murks und Patz": return "spell-murks-und-patz";
                case "Aquasphaero": return "spell-aquasphaero";
                case "Falkenauge Meisterschuss": return "spell-falkenauge";
                case "Humofaxius Humusstrahl": return "spell-humofaxius";
                case "Levthans Feuer": return "spell-levthans-feuer";
                case "Heilkraft bannen": return "spell-heilkraft-bannen";
                case "Manifesto Element": return "spell-manifesto";
                case "Gedankenbilder Elfenruf": return "spell-gedankenbilder";
                case "Skelettarius": return "spell-skelettarius";
                case "Visibili Vanitar": return "spell-visibili";
                case "Meister der Elemente": return "spell-meister-der-elemente";
                case "Aquafaxius": return "spell-aquafaxius";
                case "Verständigung stören": return "spell-verstaendigung-stoeren";
                case "Beherrschung brechen": return "spell-beherrschung-brechen";
                case "Lach dich gesund": return "spell-lach-dich-gesund";
                case "Elementarbann": return "spell-elementarbann";
                case "Ignifaxius Flammenstrahl": return "spell-ignifaxius";
          //      case "Wand aus Erz": return "spell-wand-aus-gletscher";
                case "Nebelwand und Morgendunst": return "spell-nebelwand";
                case "Pestilenz erspüren": return "spell-pestilenz-erspueren";
                case "Ignisphaero Feuerball": return "spell-ignisphaero";
                case "Leib des Feuers": return "spell-leib-des-feuers";
                case "Motoricus": return "spell-motoricus";
                case "Transformatio Formgestalt": return "spell-transformatio";
                case "Zorn der Elemente": return "spell-zorn-der-elemente";
                case "Vogelzwitschern Glockenspiel": return "spell-vogelzwitschern";
                case "Große Verwirrung": return "spell-grosze-verwirrung";
                case "Staub wandle!": return "spell-staub-wandle";
                case "Traumgestalt": return "spell-traumgestalt";
                case "Adlerauge Luchsenohr": return "spell-adlerauge";
                case "Band und Fessel": return "spell-band-und-fessel";
                case "Tempus Stasis": return "spell-tempus-stasis";
                case "Fulminictus Donnerkeil": return "spell-fulminictus";
                case "Ängste lindern": return "spell-aengste-lindern";
                case "Custodosigil Diebesbann": return "spell-custodosigil";
                case "Zunge lähmen": return "spell-zunge-laehmen";
                case "Tlalucs Odem Pestgestank": return "spell-tlalucs-odem";
                case "Spurlos Trittlos": return "spell-spurlos";
                case "Arcanovi Artefakt": return "spell-arcanovi-aufladbar";
                case "Memorans Gedächtniskraft": return "spell-memorans";
                case "Corpofrigo Kälteschock": return "spell-corpofrigo";
                case "Böser Blick": return "spell-boeser-blick";
                case "Foramen Foraminor": return "spell-foramen";
        //        case "Destructibo Arcanitas": return "spell-aurarcania-deleatur";
                case "Zagibu Ubigaz": return "spell-zagibu";
                case "Wand aus Wogen": return "spell-wand-aus-wogen";
                case "Tiere besprechen": return "spell-tiere-besprechen";
                case "Pfeil der Luft": return "spell-pfeil-der-luft";
                case "Invocatio maior": return "spell-invocatio-maior";
                case "Archosphaero Erzball": return "spell-archophaero";
                case "Caldofrigo heiß und kalt": return "spell-caldofrigo";
                case "Blick in die Gedanken": return "spell-blick-in-die-gedanken";
                case "Leib des Windes": return "spell-leib-des-windes";
                case "Langer Lulatsch": return "spell-langer-lulatsch";
                case "Aerogelo Atemqual": return "spell-aerogelo";
                case "Krötensprung": return "spell-kroetensprung";
                case "Serpentialis Schlangenleib": return "spell-serpentialis";
                case "Mahlstrom": return "spell-mahlstrom";
                case "Blick durch fremde Augen": return "spell-blick-durch-fremde-augen";
                case "Sanftmut": return "spell-sanftmut";
                case "Wipfellauf": return "spell-wipfellauf";
                case "Eigne Ängste quälen dich!": return "spell-eigne-aengste";
                case "Herr über das Tierreich": return "spell-herr-ueber-das-tierreich";
                case "Totes handle!": return "spell-totes-handle";
                case "Reversalis Revidum": return "spell-reversalis";
                case "Transversalis Teleport": return "spell-transversalis";
                case "Psychostabilis": return "spell-psychostabilis";
                case "Metamagie neutralisieren": return "spell-metamagie-neutral";
                case "Humosphaero Humusball": return "spell-humophaero";
                case "Elfenstimme Flötenton": return "spell-elfenstimme";
                case "Orcanofaxius Luftstrahl": return "spell-orcanofaxius";
                case "Lunge des Leviatan": return "spell-lunge-des-leviatan";
                case "Wellenlauf": return "spell-wellenlauf";
                case "Objekt entzaubern": return "spell-objekt-entzaubern";
                case "Tiergedanken": return "spell-tiergedanken";
                case "Zaubernahrung Hungerbann": return "spell-zaubernahrung";
                case "Dichter und Denker": return "spell-dichter-und-denker";
                case "Delicioso Gaumenschmaus": return "spell-delicioso";
                case "Unitatio Geistesbund": return "spell-unitatio";
                case "Gardianum Zauberschild": return "spell-gardianum";
                case "Sumus Elixiere": return "spell-sumus-elixiere";
                case "Zauberzwang": return "spell-zauberzwang";
                case "Große Gier": return "spell-grosze-gier";
              //  case "Fortifex arkane Wand": return "spell-wand-aus-wind";
                case "Pectetondo Zauberhaar": return "spell-pectetondo";
                case "Weiße Mähn' und gold'ner Huf": return "spell-weisze-maehn";
                case "Zwingtanz": return "spell-zwingtanz";
                case "Klickeradomms": return "spell-klickeradomms";
                case "Chamaelioni Mimikry": return "spell-chamaelioni";
                case "Fortifex arkane Wand": return "spell-fortifex";
                case "Magischer Raub": return "spell-magischer-raub";
                case "Pfeil des Erzes": return "spell-pfeil-des-erzes";
                case "Archofaxius Erzstrahl": return "spell-archofaxius";
                case "Respondami": return "spell-respondami";
                case "Invocatio minor": return "spell-invocatio-minor";
                case "Memorabia Falsifir": return "spell-memorabia";
                case "Schwarzer Schrecken": return "spell-schwarzer-schrecken";
                case "Projektimago Ebenbild": return "spell-projektimago";
                case "Herzschlag ruhe!": return "spell-herzschlag";
                case "Aureolus Güldenglanz": return "spell-aureolus";
                case "Blick aufs Wesen": return "spell-blick-aufs-wesen";
                case "Bärenruhe Winterschlaf": return "spell-baerenruhe";
                case "Ecliptifactus Schattenkraft": return "spell-ecliptifactus";
                case "Flim Flam Funkel": return "spell-flim-flam";
                case "Metamorpho Gletscherform": return "spell-metamorpho-gletscherform";
                case "Herbeirufung vereiteln": return "spell-herbeirufung-vereiteln";
                case "Harmlose Gestalt": return "spell-harmlose-gestalt";
                case "Eisenrost und Patina": return "spell-eisenrost";
                case "Hexengalle": return "spell-hexengalle";
                case "Verwandlung beenden": return "spell-verwandlung-beenden";
                case "Unberührt von Satinav": return "spell-unberuehrt-von-satinav";
                case "Meister minderer Geister": return "spell-meister-mind-geister";
                case "Lachkrampf": return "spell-lachkrampf";
                case "Klarum Purum": return "spell-klarum-purum";
                case "Hexenkrallen": return "spell-hexenkrallen";
                case "Objectofixo": return "spell-objectofixo";
                case "Axxeleratus Blitzgeschwind": return "spell-axxeleratus";
                case "Windstille": return "spell-windstille";
                case "Armatrutz": return "spell-armatrutz";
                case "Metamorpho Felsenform": return "spell-metamorpho-felsenform";
                case "Invercano Spiegeltrick": return "spell-invercano";
                case "Unsichtbarer Jäger": return "spell-unsichtbarer-jaeger";
                case "Dschinnenruf": return "spell-dschinnenruf";
                case "Arachnea Krabbeltier": return "spell-arachnea";
                case "Geisterbann": return "spell-geisterbann";
                case "Analys Arkanstruktur": return "spell-analys";
                case "Schelmenmaske": return "spell-schelmenmaske";
                case "Objectovoco": return "spell-objectovoco";
                case "Stein wandle!": return "spell-stein-wandle";
                case "Bewegung stören": return "spell-bewegung-stoeren";
                case "Illusion auflösen": return "spell-illusion-aufloesen";
                case "Pentagramma Sphärenbann": return "spell-pentagramma";
                case "Karnifilio Raserei": return "spell-karnifilo";
                case "Krabbelnder Schrecken": return "spell-krabbelnder-schrecken";
                case "Aerofugo Vakuum": return "spell-aerofugo";
                case "Weihrauchwolke Wohlgeruch": return "spell-weihrauchwolke";
                case "Hellsicht trüben": return "spell-hellsicht-trueben";
                case "Solidirid Weg aus Licht": return "spell-solidirid";
                case "Xenographus Schriftenkunde": return "spell-xenographus";
                case "Seidenzunge Elfenwort": return "spell-seidenzunge";
                case "Schelmenkleister": return "spell-schelmenkleister";
                case "Pfeil des Feuers": return "spell-pfeil-des-feuers";
                case "Reptilea Natternest": return "spell-reptilea";
            //    case "Auris Nasus Oculus": return "spell-oculus";
                case "Zauberklinge Geisterspeer": return "spell-zauberklinge";
                case "Koboldgeschenk": return "spell-koboldgeschenk";
                case "Firnlauf": return "spell-firnlauf";
                case "Windhose": return "spell-windhose";
                case "Kusch!": return "spell-kusch";
                case "Holterdipolter": return "spell-holterdipolter";
                case "Corpofesso Gliederschmerz": return "spell-corpofesso";
                case "Eiseskälte Kämpferherz": return "spell-eiseskaelte";
                case "Imperavi Handlungszwang": return "spell-imperavi";
                case "Beschwörung vereiteln": return "spell-beschwoerung-vereiteln";
                case "Exposami Lebenskraft": return "spell-exposami";
                case "Horriphobus Schreckgestalt": return "spell-horriphobus";
                case "Wand aus Erz": return "spell-wand-aus-erz";
                case "Vipernblick": return "spell-vipernblick";
                case "Schadenszauber bannen": return "spell-schadenszauber-bannen";
                case "Menetekel Flammenschrift": return "spell-menetekel";
                case "Aufgeblasen Abgehoben": return "spell-aufgeblasen";
                case "Vocolimbo hohler Klang": return "spell-vocolimbo";
                case "Radau": return "spell-radau";
                case "Hilfreiche Tatze, rettende Schwinge": return "spell-hilfreiche-tatze";
                case "Bannbaladin": return "spell-bannbaladin";
                case "Orcanosphaero Orkanball": return "spell-orcanosphaero";
                case "Iribaars Hand": return "spell-iribaars-hand";
                case "Madas Spiegel": return "spell-madas-spiegel";
                case "Pfeil des Humus": return "spell-pfeil-des-humus";
                case "Eins mit der Natur": return "spell-eins-mit-der-natur";
                case "Leib der Erde": return "spell-leib-der-erde";
                case "Favilludo Funkentanz": return "spell-favilludo";
                case "Stillstand": return "spell-stillstand";
                case "Penetrizzel Tiefenblick": return "spell-penetrizzel";
                case "Objecto Obscuro": return "spell-objecto-obscuro";
                case "Immortalis Lebenszeit": return "spell-immortalis";
                case "Juckreiz": return "spell-juckreiz-daemlicher";
                case "Verschwindibus": return "spell-verschwindibus";
                case "Seelentier erkennen": return "spell-seelentier-erkennen";
                case "Panik überkomme euch!": return "spell-panik";
                case "Auris Nasus Oculus": return "spell-auris-nasus";
                case "Hexenholz": return "spell-hexenholz";
                case "Körperlose Reise": return "spell-koerperlose-reise";
                case "Pfeil des Eises": return "spell-pfeil-des-eises";
                case "Nebelleib": return "spell-nebelleib";
                case "Ruhe Körper, ruhe Geist": return "spell-ruhe-koerper";
                case "Leidensbund": return "spell-leidensbund";
                case "Sapefacta Zauberschwamm": return "spell-sapefacta";
                case "Accuratum Zaubernadel": return "spell-accuratum";
                case "Widerwille Ungemach": return "spell-widerwille";
                case "Gefunden!": return "spell-gefunden";
                case "Alpgestalt": return "spell-alpgestalt";
                case "Claudibus Clavistibor": return "spell-claudibus";
                case "Kulminatio Kugelblitz": return "spell-kulminatio";
                case "Pandaemonium": return "spell-pandaemonium";
                case "Schleier der Unwissenheit": return "spell-schleier-der-unwissenheit";
                case "Hexenknoten": return "spell-hexenknoten";
                case "Halluzination": return "spell-halluzination";
                case "Chrononautos Zeitenfahrt": return "spell-chrononautos";
                case "Dunkelheit": return "spell-dunkelheit";
                case "Nuntiovolo Botenvogel": return "spell-nuntiovolo";
                case "Tauschrausch": return "spell-tauschrausch";
                case "Duplicatus Doppelbild": return "spell-duplicatus";
                case "Seidenweich Schuppengleich": return "spell-seidenweich";
                case "Plumbumbarum schwerer Arm": return "spell-plumbumbarum";
                case "Blitz dich find": return "spell-blitz-dich-find";
                case "Höllenpein zerreiße dich!": return "spell-hoellenpein";
                case "Spinnenlauf": return "spell-spinnenlauf";
                case "Leib des Eises": return "spell-leib-des-eises";
                case "Aeolitus Windgebraus": return "spell-aeolitus";
                case "Erinnerung verlasse dich!": return "spell-erinnerung-verlasse-dich";
                case "Blendwerk": return "spell-blendwerk";
                case "Salander Mutander": return "spell-salander";
                case "Kraft des Erzes": return "spell-kraft-des-erzes";
                case "Weisheit der Bäume": return "spell-weisheit";
                case "Transmutare Körperform": return "spell-transmutare";
                case "Chimaeroform Hybridgestalt": return "spell-chimaeroform";
                case "Zauberwesen der Natur": return "spell-zauberwesen";
                case "Protectionis Kontrabann": return "spell-protectionis";
                case "Sensibar Empathicus": return "spell-sensibar";
                case "Balsam Salabunde": return "spell-balsam";
                case "Wasseratem": return "spell-wasseratem";
                case "Impersona Maskenbild": return "spell-impersona";
                case "Granit und Marmor": return "spell-granit";
                case "Seelenwanderung": return "spell-seelenwanderung";
                case "Movimento Dauerlauf": return "spell-movimento";
                case "Eigenschaft wiederherstellen": return "spell-eigenschaften-wiederherstellen";
                case "Paralysis starr wie Stein": return "spell-paralysis";
                case "Lockruf und Feenfüße": return "spell-lockruf";
                case "Attributo": return "spell-attributo";
                case "Einfluss bannen": return "spell-einfluss-bannen";
                case "Last des Alters": return "spell-last-des-alters";
                case "Warmes Blut": return "spell-warmes-blut";
                case "Katzenaugen": return "spell-katzenaugen";
                case "Nihilogravo Schwerelos": return "spell-nihilogravo";
                case "Gefäß der Jahre": return "spell-gefaesz-der-jahre";
                case "Ignorantia Ungesehn": return "spell-ignorantia";
                case "Schelmenlaune": return "spell-schelmenlaune";
                case "Odem Arcanum": return "spell-odem";
                case "Auge des Limbus": return "spell-auge-des-limbus";
                case "Weiches erstarre!": return "spell-weiches-erstarre";
                case "Hartes schmelze!": return "spell-hartes-schmelze";
                case "Schwarz und Rot": return "spell-schwarz-und-rot";
                case "Krähenruf": return "spell-kraehenruf";
                case "Somnigravis tiefer Schlaf": return "spell-somnigravis";
                case "Dämonenbann": return "spell-daemonenbann";
                case "Haselbusch und Ginsterkraut": return "spell-haselbusch";
                case "Adlerschwinge Wolfsgestalt": return "spell-adlerschwinge";
                case "Hexenspeichel": return "spell-hexenspeichel";
                case "Sensattacco Meisterstreich": return "spell-sensattacco";
                case "Cryptographo Zauberschrift": return "spell-cryptographo";
                case "Leib des Erzes": return "spell-leib-des-erzes";
                case "Pfeil des Wassers": return "spell-pfeil-des-wassers";
                case "Schabernack": return "spell-schabernack";
                case "Papperlapapp": return "spell-papperlapapp";
                case "Destructibo Arcanitas": return "spell-destructibo";
                case "Hexenblick": return "spell-hexenblick";
                case "Atemnot": return "spell-atemnot";
                case "Elementarer Diener": return "spell-elementarer-diener";
                case "Geisterruf": return "spell-geisterruf";
                case "Nekropathia Seelenreise": return "spell-nekropathia";
                case "Reflectimago Spiegelschein": return "spell-reflectimago";
                case "Schelmenrausch": return "spell-schelmenrausch";
                case "Blick in die Vergangenheit": return "spell-blick-in-die-vergangenheit";
                case "Silentium": return "spell-silentium";
                case "Frigifaxius Eisstrahl": return "spell-frigifaxius";
                case "Limbus versiegeln": return "spell-limbus-versiegeln";
                case "Standfest Katzengleich": return "spell-standfest";
                case "Animatio stummer Diener": return "spell-animatio";
                case "Wand aus Flammen": return "spell-wand-aus-flammen";
                case "Infinitum Immerdar": return "spell-infinitum";
                case "Desintegratus Pulverstaub": return "spell-desintegratus";
                case "Abvenenum reine Speise": return "spell-abvenenum";
                case "Nackedei": return "spell-nackedei";
                case "Wand aus Dornen": return "spell-wand-aus-dornen";
                case "Zappenduster": return "spell-zappenduster";
                case "Veränderung aufheben": return "spell-veraenderung-aufheben";
                case "Komm Kobold Komm": return "spell-komm-kobold-komm";
                case "Fluch der Pestilenz": return "spell-fluch-der-pestilenz";
                case "Planastrale Anderswelt": return "spell-planastrale";
                case "Wettermeisterschaft": return "spell-wettermeisterschaft";
                case "Leib der Wogen": return "spell-leib-der-wogen";
                case "Koboldovision": return "spell-koboldovision";
                case "Chronoklassis Urfossil": return "spell-chronoklassis";
                case "Sinesigil unerkannt": return "spell-sinesigil";
                case "Satuarias Herrlichkeit": return "spell-satuarias-herrlichkeit";
                case "Adamantium Erzstruktur": return "spell-adamantium";
                case "Frigisphaero Eisball": return "spell-frigosphaero";
                case "Brenne toter Stoff!": return "spell-brenne-toter-stoff";

                default:
                    return "spell-" + ErsetzeUmlaute(z);
            }
        }
        #endregion
        #region //---- Classes ----
        public class MyTimer
        {
            static int start = 0;
            static int stop = 0;
            public static void start_timer()
            {
                start = Environment.TickCount;
            }
            public static void stop_timer()
            {
                stop_timer("");
            }
            public static string stop_timer(string msg)
            {
                stop = Environment.TickCount;
                return print(msg);
            }
            private static string print(string msg)
            {
                string output = "MyTimer(" + msg + "): " + (stop - start) + " Millisekunden";
                System.Diagnostics.Debug.WriteLine(output);
                return output;
            }
        }


        public class folder
        {
            public string name { get; set; }
            public string typ { get; set; }
            public string sorting { get; set; }
            public string parent { get; set; }
            public string color { get; set; }
            public string _id { get; set; }
        }
        public class WaffeTalent
        {
            public string DSA5Gruppe { get; set; }
            public string DSA4Gruppe { get; set; }
            public string StF { get; set; }
            public string guidvalue { get; set; }
            public string wtype { get; set; }
            public string img { get; set; }
        }

        public class DSA41_KämpferTalent
        {
            public Guid _id { get; set; }
            public string GMid { get; set; }
            public string USERid { get; set; }
            public string name { get; set; }
            public string type { get; set; }
            public string img { get; set; }
            public bool isKampfTalent { get; set; }
            public dat Data { get; set; }
            public Held_Talent ht { get; set; }
            public Held_Ausrüstung ha { get; set; }
            public Held_Inventar hi { get; set; }
            public Held_Sonderfertigkeit hsf { get; set; }
            public Held_Zauber hz { get; set; }
            public GegnerBase_Zauber gbz { get; set; }
            public Held_VorNachteil hvn { get; set; }

            public string shortInfo { get; set; }
            public DSA41_KämpferTalent()
            {
                _id = Guid.NewGuid();
            }

            public string GetShortInfo()
            {
                isKampfTalent = true;
                char A = (char)34;
                string back = A + _id.ToString().Substring(19, 17).Replace("-", "") + A + ":{";
                back += A + "data" + A + ":{" + A + "combat" + A + ":{";
                if (ht.HatAttacke)
                    back += A + "attack" + A + ":" + Convert.ToString(ht.ZuteilungAT) + ",";
                if (ht.HatParade)
                    back += A + "parry" + A + ":" + Convert.ToString(ht.ZuteilungPA) + ",";
                if (ht.HatFernkampf)
                    back += A + "rangedAttack" + A + ":" + Convert.ToString(ht.Fernkampfwert) + ",";
                back = back.TrimEnd(new Char[] { ',' });
                back += "},";
                back += A + "value" + A + ":" + (ht.TaW != null? ht.TaW.Value.ToString():"null") + "}},";

                return back;            
            }

            public string GetLongInfoSF()
            {
                char A = (char)34;

                string back = "{" + A + "_id" + A + ":" + A + _id.ToString().Substring(19, 17).Replace("-", "") + A + ",";
                back += A + "name" + A + ":" + A + hsf.Sonderfertigkeit.Name + A + ",";
                back += A + "type" + A + ":" + A + "specialAbility" + A + ",";
                back += A + "img" + A + ":" + A + "icons/svg/item-bag.svg" + A + ",";
                back += A + "data" + A + ":{";
                string beschreibung = 
                    (hsf.Sonderfertigkeit.Name.StartsWith("Geländekunde") || 
                     hsf.Sonderfertigkeit.Name.StartsWith("Kulturkunde") ||
                     hsf.Sonderfertigkeit.Name.StartsWith("Schnellladen") ||
                     hsf.Sonderfertigkeit.Name.StartsWith("Schnellziehen")||
                     hsf.Sonderfertigkeit.Name.StartsWith("Scharfschütze") ||
                     hsf.Sonderfertigkeit.Name.StartsWith("Repräsentation")||
                     hsf.Sonderfertigkeit.Name.StartsWith("Waffenloses Manöver")||
                     hsf.Sonderfertigkeit.Name.StartsWith("Merkmalskenntnis")||
                     hsf.Sonderfertigkeit.Name.StartsWith("Ritualkenntnis")) ?
                     hsf.Sonderfertigkeit.Name.Substring(hsf.Sonderfertigkeit.Name.IndexOf(" (")+1) :
                    (hsf.Sonderfertigkeit.Name.StartsWith("Elfenlied:")|| hsf.Sonderfertigkeit.Name.StartsWith("Hexenfluch:")) ?
                     hsf.Sonderfertigkeit.Name.Substring(hsf.Sonderfertigkeit.Name.IndexOf(":")+2) :
                    hsf.Sonderfertigkeit.Voraussetzungen;

                back += A + "description" + A + ":" + A + beschreibung + A + ",";
                back += A + "isUniquelyOwnable" + A + ":" + "true" + ",";
                back += A + "sid" + A + ":" + A + GetSF_sid(hsf.Sonderfertigkeit.Name) + A + ",";
                back += A + "type" + A + ":" + (hsf.Sonderfertigkeit.HatWert.HasValue && hsf.Sonderfertigkeit.HatWert.Value ? A + hsf.Wert + A : "null") + "},";
                back += A + "effects" + A + ":[],";
                back += A + "folder" + A + ":null,";
                back += A + "sort" + A + ":0,";
                back += A + "permission" + A + ":{";
                back += A + "default" + A + ":0,";
                back += A + USERid + A + ":3,";
                back += A + GMid + A + ":3},";
                back += A + "flags" + A + ":{}},";
                return back;
            }

            public string GetLongInfoVN()
            {
                string VNname = hvn.VorNachteil.Name;
                char A = (char)34;
                if (hvn.Wert != null && hvn.Wert.Contains("("))
                    VNname += " " + hvn.Wert.Substring(0, hvn.Wert.LastIndexOf("(") - 1);

                string back = "{" + A + "_id" + A + ":" + A + _id.ToString().Substring(19, 17).Replace("-", "") + A + ",";
                back += A + "name" + A + ":" + A + VNname + A + ",";
                back += A + "type" + A + ":" + A + (hvn.VorNachteil.Nachteil.HasValue && hvn.VorNachteil.Nachteil.Value?"dis":"")+ "advantage" + A + ",";
                back += A + "img" + A + ":" + A + "icons/svg/item-bag.svg" + A + ",";
                back += A + "data" + A + ":{";
                
                back += A + "description" + A + ":" + A +  A + ",";
                back += A + "isUniquelyOwnable" + A + ":" + "true" + ",";

                string b = (hvn.VorNachteil.Nachteil.HasValue && hvn.VorNachteil.Nachteil.Value ? "dis" : "") +
                    "advantage-" + ErsetzeUmlaute(VNname);
                if (b.Contains("("))
                {
                    b = b.Substring(0, b.IndexOf("("));
                    back += A + "value" + A + ":" + A + hvn.Wert + A + ",";
                }
                back += A + "sid" + A + ":" + A + b + A + ",";
                back += A + "negativeAttribute" + A + ":" + (hvn.VorNachteil.Nachteil.HasValue && hvn.VorNachteil.Nachteil.Value ? "true" : "false") + ",";
                if (!string.IsNullOrEmpty(hvn.Wert) && hvn.Wert.Contains("(") && hvn.Wert.Any(char.IsDigit))
                    back += A + "value" + A + ":" + hvn.Wert.Substring(hvn.Wert.LastIndexOf("(") + 1, hvn.Wert.IndexOf(")") - hvn.Wert.LastIndexOf("(") - 1) + ",";
                else
                if (!string.IsNullOrEmpty(hvn.Wert) && hvn.VorNachteil.WertTyp?.ToLowerInvariant() == "int")
                    back += A + "value" + A + ":" + hvn.Wert + ",";
                else
                    back += A + "value" + A + ":" + A + A + ",";

                back += A + "type" + A + ":" +  "null" + "},";
                back += A + "effects" + A + ":[],";
                back += A + "folder" + A + ":null,";
                back += A + "sort" + A + ":0,";
                back += A + "permission" + A + ":{";
                back += A + "default" + A + ":0,";
                back += A + USERid + A + ":3,";
                back += A + GMid + A + ":3},";
                back += A + "flags" + A + ":{}},";
                return back;
            }

            public string GetLongInfoZauber(int nummer)
            {
                Model.Zauber z = hz != null ? hz.Zauber : gbz != null ? gbz.Zauber : null;
                if (z == null)
                    return null;
                char A = (char)34;

                string back = "{" + A + "_id" + A + ":" + A + _id.ToString().Substring(19, 17).Replace("-", "") + A + ",";
                back += A + "name" + A + ":" + A + z.Name + A + ",";
                back += A + "type" + A + ":" + A + "spell" + A + ",";
                back += A + "img" + A + ":" + A + "icons/svg/item-bag.svg" + A + ",";
                back += A + "data" + A + ":{";
                back += A + "description" + A + ":" + A + (hz!= null? hz.Bemerkung: gbz.Bemerkung) + A + ",";
                back += A + "test" + A + ":{";
                back += A + "firstAttribute" + A + ":" + (z.Eigenschaft1 != null ? (A + GetAttribute(z.Eigenschaft1) + A) : "null") + ",";
                back += A + "secondAttribute" + A + ":" + (z.Eigenschaft2 != null ? (A + GetAttribute(z.Eigenschaft2) + A) : "null") + ",";
                back += A + "thirdAttribute" + A + ":" + (z.Eigenschaft3 != null ? (A + GetAttribute(z.Eigenschaft3) + A) : "null") + "},";
                back += A + "castTime" + A + ":{";
                back += A + "duration" + A + ":" + (z.Zauberdauer??0) + ",";
                back += A + "unit" + A + ":" + A+"Aktionen"+A + ",";
                back += A + "info" + A + ":" + A+A + "},";
                back += A + "effectTime" + A + ":{";
                back += A + "duration" + A + ":" + (z.Wirkungsdauer??"0") + ",";
                back += A + "unit" + A + ":" + A + "Aktionen" + A + ",";
                back += A + "info" + A + ":" + A + (z.Wirkungsradius??"0")+ A + "},";
                back += A + "targetClasses" + A + ":[],";
                back += A + "range" + A + ":" + A + (z.Reichweite??"0") + A + ",";
                back += A + "technique" + A + ":" + A + A + ",";
                back += A + "effect" + A + ":" + A + A + ",";
                back += A + "variants" + A + ":[],";
                back += A + "isUniquelyOwnable" + A + ":" + "true" + ",";
                back += A + "sid" + A + ":" + A + GetZauber_sid(z.Name) + A + ",";
                back += A + "value" + A + ":" + (hz != null ? hz.ZfW: gbz.ZfW)+ ",";
                back += A + "testMod" + A + ":null" + ",";
                back += A + "astralCost" + A + ":" +A+(z.Kosten??"0")+A +",";
                back += A + "modifications" + A + ":[],";
                back += A + "lcdPage" + A + ":" + A+z.Literatur +A+ ",";
                back += A + "reversalis" + A + ":" + A + A + ",";
                back += A + "antimagic" + A + ":" + A + A + ",";
                back += A + "properties" + A + ":[],";
                back += A + "complexity" + A + ":" + A+z.Komplex + A + ",";
                back += A + "representation" + A + ":" + A+z.Repräsentationen + A + ",";
                back += A + "spread" + A + ":null" + "},";
                back += A + "effects" + A + ":[],";
                back += A + "folder" + A + ":null,";
                back += A + "sort" + A + ":" + nummer + "00000,";
                back += A + "permission" + A + ":{";
                back += A + "default" + A + ":0,";
                back += A + USERid + A + ":3,";
                back += A + GMid + A + ":3},";
                back += A + "flags" + A + ":{}},";
                return back;
            }

            public string GetLongInfoTalent(int nummer)
            {
                char A = (char)34;

                string back = "{" + A + "_id" + A + ":" + A + _id.ToString().Substring(19, 17).Replace("-", "") + A + ",";
                back += A + "name" + A + ":" + A + ht.Talent.Name + A + ",";
                back += A + "type" + A + ":" + A + "talent" + A + ",";
                back += A + "data" + A + ":{";
                back += A + "description" + A + ":" + (string.IsNullOrEmpty(ht.Bemerkung) ? ("" + A + A) : ht.Bemerkung) + ",";
                string typ = ht.Talent.Talenttyp == "Basis" ? "basic" : "special";
                back += A + "type" + A + ":" + A + typ + A + ",";
                back += A + "category" + A + ":" + A + GetCategory(ht.Talent.Talentgruppe.Kurzname) + A + ",";
                back += A + "effectiveEncumbarance" + A + ":{";
                back += A + "type" + A + ":" + A + "formula" + A + ",";
                back += A + "formula" + A + ":" + A + (ht.Talent.IsBehinderung && ht.Talent.eBE != null? ht.Talent.eBE : "none") + A + "},";
                back += A + "value" + A + ":" + ht.TaW + ",";
                back += A + "test" + A + ":{";
                back += A + "firstAttribute" + A + ":" +(ht.Talent.Eigenschaft1!= null?(A + GetAttribute(ht.Talent.Eigenschaft1) + A):"null") + ",";
                back += A + "secondAttribute" + A + ":" + (ht.Talent.Eigenschaft2 != null ? (A + GetAttribute(ht.Talent.Eigenschaft2) + A) : "null") + ",";
                back += A + "thirdAttribute" + A + ":" + (ht.Talent.Eigenschaft3 != null ? (A + GetAttribute(ht.Talent.Eigenschaft3) + A) : "null") + "},";
                back += A + "isUniquelyOwnable" + A + ":" + "true" + ",";
                back += A + "sid" + A + ":" + A + GetTalent_sid(ht.Talent.Name) + A + "},";
                back += A + "sort" + A + ":" + nummer + "00000,";
                back += A + "flags" + A + ":{},";
                back += A + "img" + A + ":" + A + "icons/svg/mystery-man.svg" + A + ",";
                back += A + "effects" + A + ":[],";
                back += A + "folder" + A + ":null,";
                back += A + "permission" + A + ":{";
                back += A + "default" + A + ":0},";
                back += A + USERid + A + ":3,";
                back += A + GMid + A + ":3},";
                return back;
            }

            public string GetLongInfo(int nummer)
            {
                char A = (char)34;

                string back = "{" + A + "_id" + A + ":" + A + _id.ToString().Substring(19, 17).Replace("-", "") + A + ",";
                back += A + "name" + A + ":" + A + ht.Talent.Name + A + ",";
                string typ = ht.Talent.Talentgruppe.Kurzname == "Kampf"? "combatTalent":"";
                back += A + "type" + A + ":" + A + typ + A + ",";
                back += A + "data" + A + ":{";
                back += A + "description" + A + ":" + A + ht.Bemerkung + A + ",";
                typ = ht.Talent.Talenttyp == "Basis" ? "basic" :
                    "special";
                back += A + "type" + A + ":" + A + typ + A + ",";
                back += A + "category" + A + ":" + A + "combat" + A + ",";
                back += A + "effectiveEncumbarance" + A + ":{";
                back += A + "type" + A + ":" + A + "formula" + A + ",";
                back += A + "formula" + A + ":" + A + (ht.Talent.IsBehinderung? ht.Talent.eBE: "null") + A + "},";
                back += A + "value" + A + ":" + ht.TaW + ",";
                back += A + "isUniquelyOwnable" + A + ":" + "true" + ",";
                back += A + "sid" + A + ":" + A + GetTalent_sid(ht.Talent.Name) + A + ",";
                back += A + "combat" + A + ":{";
                string cat = ht.Talent.Untergruppe == "Fernkampf" ? "ranged" :
                    ht.Talent.Untergruppe == "Bewaffneter Nahkampf" ? "melee":
                    ht.Talent.Untergruppe == "Waffenloser Kampf" ? "unarmed" :
                    "special";
                // unarmed armed ranged melee special
                back += A + "category" + A + ":" + A + cat + A + ",";
                back += A + "attack" + A + ":" +  Convert.ToString(ht.ZuteilungAT) + ","; //ht.Talent.ModifikatorenListeAT ???
                back += A + "parry" + A + ":" + Convert.ToString(ht.HatFernkampf ? ht.Fertigkeitswert : ht.ZuteilungPA) + ",";
                back += A + "rangedAttack" + A + ":" + Convert.ToString(ht.Fertigkeitswert) + "}},";
                back += A + "sort" + A + ":" + nummer + "00000,";
                back += A + "flags" + A + ":{},";
                back += A + "img" + A + ":" + A + "icons/svg/mystery-man.svg" + A + ",";
                back += A + "effects" + A + ":[],";
                back += A + "folder" + A + ":null,";
                back += A + "permission" + A + ":{";
                back += A + "default" + A + ":0},";
                back += A + USERid + A + ":3,";
                back += A + GMid + A + ":3},";

                return back;
            }

            public string GetLongInfoInventar(int nummer)
            {
                char A = (char)34;

                string back = "{" + A + "_id" + A + ":" + A + _id.ToString().Substring(19, 17).Replace("-", "") + A + ",";
                back += A + "name" + A + ":" + A + hi.Inventar.Name.Replace(A.ToString(), "'") + A + ",";
                back += A + "type" + A + ":" + A + "genericItem" + A + ",";
                back += A + "img" + A + ":" + A + "icons/svg/item-bag.svg" + A + ",";
                back += A + "data" + A + ":{";
                back += A + "description" + A + ":" + A + hi.Inventar.Gewicht +" Uz" + A + ",";
                back += A + "isConsumable" + A + ":" + "false" + ",";
                back += A + "quantity" + A + ":" + hi.Anzahl + "},";
                back += A + "effects" + A + ":[],";
                back += A + "folder" + A + ":null,";
                back += A + "sort" + A + ":" + nummer + "00000,";
                back += A + "permission" + A + ":{";
                back += A + "default" + A + ":0,";

                back += A + USERid + A + ":3,";
                back += A + GMid + A + ":3},";
                back += A + "flags" + A + ":{}},";
                return back;
            }

            public string GetLongInfoAusrüstung(int nummer, string folder)
            {
                char A = (char)34;

                string back = "{" + A + "_id" + A + ":" + A + _id.ToString().Substring(19, 17).Replace("-", "") + A + ",";
                back += A + "name" + A + ":" + A +ha.Name.Replace(A.ToString(),"'") + A + ",";

                string typ =
                    ha.Waffe != null ? "meleeWeapon" :
                    ha.Held_Fernkampfwaffe != null ? "rangedWeapon" :
                    ha.Held_Rüstung != null ? "armor" :
                    ha.Schild != null ? "shield" :
                    "genericItem";

                back += A + "type" + A + ":" + A + typ + A + ",";

                switch (typ)
                {
                    case "genericItem":
                back += A + "img" + A + ":" + A + "icons/svg/item-bag.svg" + A + ",";
                back += A + "data" + A + ":{";
                        back += A + "description" + A + ":" + A+ha.Ausrüstung.Bemerkung +A+ ",";
                        back += A + "isConsumable" + A + ":" + "false" + ",";
                        back += A + "quantity" + A + ":" + "0" + "},";
                    break;
                    case "meleeWeapon":
                        back += A + "img" + A + ":" + A + "icons/svg/sword.svg" + A + ",";
                        back += A + "data" + A + ":{";
                        back += A + "description" + A + ":" + A + ha.Waffe.Bemerkung + A + ",";
                        switch (ha.Waffe.Talent.FirstOrDefault().Name)
                        {
                            case "Zweihandschwerter/-säbel": back += A + "talent" + A + ":" + A + "talent-zweihandschwerter__saebel" + A + ",";
                                break;
                            case "Infanteriewaffen": back += A + "talent" + A + ":" + A + "talent-infanteriewaffen" + A + ",";
                                break;
                            case "Zweihand-Hiebwaffen":
                                back += A + "talent" + A + ":" + A + "talent-zweihand-Hiebwaffen" + A + ",";
                                break;
                            case "Stäbe":
                                back += A + "talent" + A + ":" + A + "talent-staebe" + A + ",";
                                break;
                            case "Kettenstäbe":
                                back += A + "talent" + A + ":" + A + "talent-kettenstaebe" + A + ",";
                                break;
                            case "Kettenwaffen":
                                back += A + "talent" + A + ":" + A + "talent-kettenwaffen" + A + ",";
                                break;
                            case "Säbel":
                                back += A + "talent" + A + ":" + A + "talent-saebel" + A + ",";
                                break;
                            case "Hiebwaffen":
                                back += A + "talent" + A + ":" + A + "talent-hiebwaffen" + A + ",";
                                break;
                            case "Anderthalbhänder":
                                back += A + "talent" + A + ":" + A + "talent-anderthalbhaender" + A + ",";
                                break;
                            case "Fechtwaffen":
                                back += A + "talent" + A + ":" + A + "talent-fechtwaffen" + A + ",";
                                break;
                            case "Speere":
                                back += A + "talent" + A + ":" + A + "talent-speere" + A + ",";
                                break;
                            case "Dolche":
                                back += A + "talent" + A + ":" + A + "talent-dolche" + A + ",";
                                break;
                            case "Zweihandflegel":
                                back += A + "talent" + A + ":" + A + "talent-zweihandflegel" + A + ",";
                                break;
                            case "Schwerter":
                                back += A + "talent" + A + ":" + A + "talent-schwerter" + A + ",";
                                break;
                        }
                        back += A + "damage" + A + ":" + A + ha.Waffe.TPString.Replace("W", "d") + A + ",";
                        back += A + "price" + A + ":" + Math.Round(ha.Waffe.Preis) + ",";
                        back += A + "weight" + A + ":" + ha.Waffe.Gewicht + ",";
                        back += A + "length" + A + ":"  + (ha.Waffe.Länge??0) + ",";
                        back += A + "strengthMod" + A + ":{";
                        back += A + "threshold" + A + ":" + (ha.Waffe.TPKKSchwelle??0) + ",";
                        back += A + "hitPointStep" + A + ":" + (ha.Waffe.TPKKSchritt??0) + "},";
                        back += A + "breakingFactor" + A + ":" + (ha.Waffe.BF??0) + ",";
                        back += A + "initiativeMod" + A + ":" + (ha.Waffe.INI??0) + ",";
                        back += A + "weaponMod" + A + ":{";
                        back += A + "attack" + A + ":" + (ha.Waffe.WMAT ?? 0) + ",";
                        back += A + "parry" + A + ":" + (ha.Waffe.WMPA ?? 0) + "},";
                        back += A + "distanceClass" + A + ":" + A + ha.Waffe.DK + A + ",";
                        back += A + "twoHanded" + A + ":" + "false" + ",";
                        back += A + "improvised" + A + ":" +  ha.Waffe.Improvisiert.ToString().ToLower() + ",";
                        back += A + "priviledged" + A + ":" + "false" + "},";
                    break;
                    case "rangedWeapon":
                        back += A + "img" + A + ":" + A + "icons/svg/net.svg" + A + ",";
                        back += A + "data" + A + ":{";
                        back += A + "description" + A + ":" + A + ha.Fernkampfwaffe.Bemerkung + A + ",";
                        switch (ha.Fernkampfwaffe.Talent.FirstOrDefault()?.Name)
                        {
                            case "Wurfbeile": back += A + "talent" + A + ":" + A + "talent-wurfbeile" + A + ",";
                                break;
                            case "Wurfspeere": back += A + "talent" + A + ":" + A + "talent-wurfspeere" + A + ",";
                                break;
                            case "Blasrohr": back += A + "talent" + A + ":" + A + "talent-blasrohr" + A + ",";
                                break;
                            case "Schleuder": back += A + "talent" + A + ":" + A + "talent-schleuder" + A + ",";
                                break;
                            case "Diskus": back += A + "talent" + A + ":" + A + "talent-diskus" + A + ",";
                                break;
                            case "Belagerungswaffen": back += A + "talent" + A + ":" + A + "talent-belagerungswaffen" + A + ",";
                                break;
                            case "Bogen": back += A + "talent" + A + ":" + A + "talent-bogen" + A + ",";
                                break;
                            case "Wurfmesser": back += A + "talent" + A + ":" + A + "talent-wurfmesser" + A + ",";
                                break;
                            case "Armbrust": back += A + "talent" + A + ":" + A + "talent-armbrust" + A + ",";
                                break;
                            default: back += A + "talent" + A + ":" + A + "talent-bogen" + A + ",";
                                break;
                        }
                        back += A + "damage" + A + ":" + A + ha.Fernkampfwaffe.TPString.Replace("W","d") + A + ",";
                        back += A + "price" + A + ":" + Math.Round(ha.Fernkampfwaffe.Preis) + ",";
                        back += A + "weight" + A + ":" + ha.Fernkampfwaffe.Gewicht + ",";
                        back += A + "ranges" + A + ":{";
                        back += A + "veryClose" + A + ":" + (ha.Fernkampfwaffe.TPSehrNah??0) + ",";
                        back += A + "close" + A + ":" + (ha.Fernkampfwaffe.TPNah ?? 0) + ",";
                        back += A + "medium" + A + ":" + (ha.Fernkampfwaffe.TPMittel ?? 0) + ",";
                        back += A + "far" + A + ":" + (ha.Fernkampfwaffe.TPWeit ?? 0) + ",";
                        back += A + "veryFar" + A + ":" + (ha.Fernkampfwaffe.TPSehrWeit ?? 0) + "},";
                        back += A + "loadtime" + A + ":" + ha.Fernkampfwaffe.LadeZeit + ",";
                        back += A + "projectilePrice" + A + ":" + (ha.Fernkampfwaffe.Munitionspreis??0).ToString().Replace(",",".") + ",";
                        back += A + "loweredWoundThreshold" + A + ":" + ha.Fernkampfwaffe.Verwundend.ToString().ToLower() + ",";
                        back += A + "improvised" + A + ":" + ha.Fernkampfwaffe.Improvisiert.ToString().ToLower() + ",";
                        back += A + "entangles" + A + ":" + "false" + "},";
                    break;

                    case "armor":
                        back += A + "img" + A + ":" + A + "icons/svg/statue.svg" + A + ",";
                        back += A + "data" + A + ":{";
                        back += A + "description" + A + ":" + A + ha.Rüstung.Bemerkung + A + ",";
                        back += A + "price" + A + ":" + Math.Round(ha.Rüstung.Preis) + ",";
                        back += A + "weight" + A + ":" + ha.Rüstung.Gewicht + ",";
                        back += A + "equipped" + A + ":" + ha.Angelegt.ToString().ToLower() + ",";
                        back += A + "armorClass" + A + ":" + (ha.Rüstung.RS?? 0) + ",";
                        back += A + "encumbarance" + A + ":" + Convert.ToString(ha.Rüstung.gBE??0).Replace(",",".") + "},";
                    break;
                    case "shield":
                        back += A + "img" + A + ":" + A + "icons/svg/shield.svg" + A + ",";
                        back += A + "data" + A + ":{";
                        back += A + "description" + A + ":" + A + ha.Schild.Bemerkung + A + ",";
                        back += A + "price" + A + ":" + Math.Round(ha.Schild.Preis) + ",";
                        back += A + "weight" + A + ":" + ha.Schild.Gewicht + ",";
                        back += A + "type" + A + ":" + A + "???" + A + ",";
                        back += A + "sizeClass" + A + ":" + A + ha.Schild.Größe + A + ",";
                        back += A + "weaponMod" + A + ":{";
                        back += A + "attack" + A + ":" + ha.Schild.WMAT  + ",";
                        back += A + "parry" + A + ":" + ha.Schild.WMPA  + "},";
                        back += A + "initiativeMod" + A + ":" + ha.Schild.INI + ",";
                        back += A + "breakingFactor" + A + ":" + ha.Schild.BF + "},";
                        break;
                };
                back += A + "effects" + A + ":[],";
                back += A + "folder" + A + ":" + (folder == null ? "null" : A + folder + A) + ",";
                back += A + "sort" + A + ":" +( nummer != 0?nummer + "00000,":"0,");
                back += A + "permission" + A + ":{";
                back += A + "default" + A + ":0,";

                if (USERid != null)
                    back += A + USERid + A + ":3,";
                back += A + GMid + A + ":3},";
                back += A + "flags" + A + ":{}},";

                return back;
            }

            public class dat
            {
                public string description { get; set; }
                public string type { get; set; }
                public string category { get; set; }
                public eE effectiveEncumbarance { get; set; }
                public string value { get; set; }

                public bool isUniquelyOwnable { get; set; }
                public string sid { get; set; }
                public com combat { get; set; }
                public string effects { get; set; }
                public string folder { get; set; }
                public int sort { get; set; }
                public per permission { get; set; }

                public class eE
                {
                    public string type { get; set; }
                    public string formular { get; set; }
                }
                public class com
                {
                    public string category { get; set; }
                    public int attack { get; set; }
                    public int parry { get; set; }
                    public int rangedAttack { get; set; }
                }
                public class per
                {
                    public int Default { get; set; }
                    public string gm { get; set; }
                    public int gm_int { get; set; }
                    public string user { get; set; }
                    public int user_int { get; set; }
                    public string flags { get; set; }
                }
            }


        }

        #endregion

        #region //---- Variablen ----

        List<GegnerArgument> lstGegnerArgument = new List<GegnerArgument>();
        List<HeldenArgument> lstHeldArgument = new List<HeldenArgument>();
        List<GegenstandArgument> lstWaffenArgument = new List<GegenstandArgument>();
        List<PlaylistArgument> lstPListArgument = new List<PlaylistArgument>();

        private void ChangePath(string vonS, string inS)
        {
            if (GegnerPortraitPfad == null)
                return;
            string neu = null;
            neu = GegnerPortraitPfad.Replace(vonS, inS);
            if (neu != GegnerPortraitPfad)
            {
                GegnerPortraitPfad = neu;
                Einstellungen.SetEinstellung<string>("FoundryGegnerPortraitPfad", GegnerPortraitPfad);
            }
            neu = HeldPortraitPfad.Replace(vonS, inS);
            if (neu != HeldPortraitPfad)
            {
                HeldPortraitPfad = neu;
                Einstellungen.SetEinstellung<string>("FoundryHeldPortraitPfad", HeldPortraitPfad);
            }
            neu = GegnerTokenPfad.Replace(vonS, inS);
            if (neu != GegnerTokenPfad)
            {
                GegnerTokenPfad = neu;
                Einstellungen.SetEinstellung<string>("FoundryGegnerTokenPfad", GegnerTokenPfad);
            }
            neu = HeldTokenPfad.Replace(vonS, inS);
            if (neu != HeldTokenPfad)
            {
                HeldTokenPfad = neu;
                Einstellungen.SetEinstellung<string>("FoundryHeldTokenPfad", HeldTokenPfad);
            }
        }

        private bool _startup = true;
        public bool startup
        {
            get { return _startup; }
            set { Set(ref _startup, value); }
        }
        private bool _isWaffenInKompendium = true;
        public bool IsWaffenInKompendium
        {
            get { return _isWaffenInKompendium; }
            set { Set(ref _isWaffenInKompendium, value); }
        }

        private bool _isPlaylistsInKompendium = true;
        public bool IsPlaylistsInKompendium
        {
            get { return _isPlaylistsInKompendium; }
            set { Set(ref _isPlaylistsInKompendium, value); }
        }

        private bool _isGegnerInKompendium = true;
        public bool IsGegnerInKompendium
        {
            get { return _isGegnerInKompendium; }
            set { Set(ref _isGegnerInKompendium, value); }
        }

        private bool _isHeldenInKompendium = true;
        public bool IsHeldenInKompendium
        {
            get { return _isHeldenInKompendium; }
            set { Set(ref _isHeldenInKompendium, value); }
        }

        private bool _doLoad = false;
        public bool doLoad
        {
            get { return _doLoad; }
            set { Set(ref _doLoad, value); }
        }

        private bool _isLokalInstalliert = false;
        public bool IsLocalInstalliert
        {
            get { return _isLokalInstalliert; }
            set
            {
                lstWorlds = new List<string>();
                bool didChange = _isLokalInstalliert != value;
                Set(ref _isLokalInstalliert, value);
                if (!value)
                {
                    FTPAdresse = FTPAdresse.TrimEnd(new Char[] { '/' });
                    FoundryPfad = FTPAdresse + "/Data/";
                    ReadFoundryOptions(string.Format("{0}/config/options.json", FTPAdresse));

                    ChangePath(@"\", "/");
                }
                else
                {
                    FoundryPfad = localFoundryPfad;
                    string appFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                    string path = appFolderPath + @"\FoundryVTT\Config\";
                    if (Directory.Exists(path))
                        ReadFoundryOptions(path + @"\options.json");

                    ChangePath("/", @"\");
                }
                if (didChange)
                    Init();
            }
        }

        #region //---- FTP ----

        private string _ftpAdresse = "ftp://1.2.3.4:21";
        public string FTPAdresse
        {
            get { return _ftpAdresse; }
            set
            {
                Set(ref _ftpAdresse, value);
                Einstellungen.SetEinstellung("FoundryFTPAdresse", value);
            }
        }

        private string _ftpUser = "user";
        public string FTPUser
        {
            get { return _ftpUser; }
            set
            {
                Set(ref _ftpUser, value);
                Einstellungen.SetEinstellung("FoundryFTPUser", value);
            }
        }

        private string _ftpPasswort = "passwort";
        public string FTPPasswort
        {
            get { return _ftpPasswort; }
            set
            {
                Set(ref _ftpPasswort, value);
                Einstellungen.SetEinstellung("FoundryFTPPasswort", value);
            }
        }

        private string _testDatei = @"C:\temp\Test.txt";
        public string TestDatei
        {
            get { return _testDatei; }
            set { Set(ref _testDatei, value); }
        }


        private Base.CommandBase _onBtnFTPConfig = null;
        public Base.CommandBase OnBtnFTPConfig
        {
            get
            {
                if (_onBtnFTPConfig == null)
                    _onBtnFTPConfig = new Base.CommandBase(FTPConfig, null);
                return _onBtnFTPConfig;
            }
        }
        private void FTPConfig(object sender)
        {
            string back = ViewHelper.InputDialog("FTP-Adresse", "Gebe die FTP-Adresse zu dem Server ein\n\nBeispiel Lokal:      " + @"C:\FoundryVTT            " +
                "\nBeispiel Server:    ftp://1.2.3.4:21/", FTPAdresse);
            if (!string.IsNullOrEmpty(back))
                FTPAdresse = back;
            back = ViewHelper.InputDialog("FTP-User", "Gebe den FTP-Usernamen zu dem Server ein", FTPUser);
            if (!string.IsNullOrEmpty(back))
                FTPUser = back;
            back = ViewHelper.InputDialog("FTP-Passwort", "Gebe das FTP-Passwort zu dem Server ein", "");
            if (!string.IsNullOrEmpty(back))
                FTPPasswort = back;
            Init();
        }


        private Base.CommandBase _onBtnConnectFTP = null;
        public Base.CommandBase OnBtnConnectFTP
        {
            get
            {
                if (_onBtnConnectFTP == null)
                    _onBtnConnectFTP = new Base.CommandBase(ConnectFTP, null);
                return _onBtnConnectFTP;
            }
        }
        private void ConnectFTP(object sender)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(FTPAdresse + "/Data/worlds/test.txt");
            request.Method = WebRequestMethods.Ftp.UploadFile;

            request.Credentials = new NetworkCredential(FTPUser, FTPPasswort, "");


            // Copy the contents of the file to the request stream.
            byte[] fileContents;
            using (StreamReader sourceStream = new StreamReader(TestDatei))
            {
                fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
            }

            request.ContentLength = fileContents.Length;

            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(fileContents, 0, fileContents.Length);
            }

            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            {
                Console.WriteLine($"Upload File Complete, status {response.StatusDescription}");
            }
        }


        #endregion

        #region //---- Classes ----

        public class PlaylistArgument
        {
            public string GMid { get; set; }
            public string _id { get; set; }
            public string name { get; set; }
            public string permission { get; set; }  // default = 0 user:Gamemaster = 3
            public int sort { get; set; }
            public string flags { get; set; }

            public string preArg
            {
                get
                {
                    char A = (char)34;
                    string arg = "{" + A + "_id" + A + ":" + A + _id + A + "," +
                        A + "name" + A + ":" + A + name + A + "," +
                        A + "permission" + A + ":{" + A + "default" + A + ":0," + A + GMid + A + ":3}," +
                        A + "sort" + A + ":" + sort.ToString() + "," +
                        A + "flags" + A + ":{}";
                    return arg;
                }
                //{
                //  "_id":"DUBBfsfZPdvWN8Mn",
                //"name":"neu",
                //"permission":{"default":0,"gNZOk6idrMy6uSkk":3},
                //"sort":100001,
                //"flags":{},
            }
            public class SoundArg
            {
                public List<string> lstArg { get; set; }
                public string lstTitel { get; set; }
            }

            /*
             * 
             {
            "_id":"hMKrznmD7vhNLDNq",
            "flags":{},
            "path":"Musik/_Stra%C3%9Fenmusik/104-miguel_angel_tallante--cruzada-oma.mp3",
            "repeat":false,
            "volume":0.35355339059327373,
            "name":"104-miguel_angel_tallante--cruzada-oma",
            "playing":false,
            "streaming":false
        }
             * 
             * 
             * 
             */
            public List<SoundArg> lstSounds { get; set; }

            public string postArg
            {
                get
                {
                    char A = (char)34;
                    string arg = A + "mode" + A + ":" + mode.ToString() + "," +
                        A + "playing" + A + ":" + playing + "}";
                    return arg;
                }
            }
            public int mode { get; set; }
            public string playing { get; set; }

            public string outtext
            {
                get
                {
                    char A = (char)34;
                    return preArg + "," + A + "sounds" + A + ":[" + string.Join(",", lstSounds.Select(t => t.lstTitel)) + "]," + postArg;
                }
            }

            // {"_id":"DUBBfsfZPdvWN8Mn",
            //"name":"neu",
            //"permission":{"default":0,"gNZOk6idrMy6uSkk":3},
            //"sort":100001,
            //"flags":{},
            //"sounds":[
            //        ]

            //"mode":1,
            //"playing":false}
        }
        public class dbArgument
        {
            public string Argument
            { get; set; }
            public string Prefix
            { get; set; }
            public string Suffix
            { get; set; }
            public string ArgString
            { get; set; }
        }

        public class GegnerArgument
        {
            public GegnerBase g
            { get; set; }
            public List<dbArgument> lstArguments
            { get; set; }
            public string outcome
            { get; set; }
        }

        public class HeldenArgument
        {
            public Held h
            { get; set; }
            public List<dbArgument> lstArguments
            { get; set; }
            public string outcome
            { get; set; }
        }

        public class GegenstandArgument
        {
            public string name
            { get; set; }
            public List<dbArgument> lstArguments
            { get; set; }
            public string img
            { get; set; }
            public string outcome
            { get; set; }
        }

        #endregion


        private string _gegnerPortraitPfad = null;
        public string GegnerPortraitPfad
        {
            get { return _gegnerPortraitPfad; }
            set
            {
                string prevalue = value;
                if (prevalue != null && !prevalue.EndsWith("/") && !prevalue.EndsWith(@"\"))
                    prevalue = prevalue + (IsLocalInstalliert ? @"\" : "/");
                Set(ref _gegnerPortraitPfad, prevalue);
                Einstellungen.SetEinstellung<string>("FoundryGegnerPortraitPfad", GegnerPortraitPfad);
            }
        }


        private string _heldPortraitPfad = null;
        public string HeldPortraitPfad
        {
            get { return _heldPortraitPfad; }
            set
            {
                string prevalue = value;
                if (!prevalue.EndsWith("/") && !prevalue.EndsWith(@"\"))
                    prevalue = prevalue + (IsLocalInstalliert ? @"\" : "/");
                Set(ref _heldPortraitPfad, prevalue);
                Einstellungen.SetEinstellung<string>("FoundryHeldPortraitPfad", HeldPortraitPfad);
            }
        }

        private string _gegnerTokenPfad = null;
        public string GegnerTokenPfad
        {
            get { return _gegnerTokenPfad; }
            set
            {
                string prevalue = value;
                if (!prevalue.EndsWith("/") && !prevalue.EndsWith(@"\"))
                    prevalue = prevalue + (IsLocalInstalliert ? @"\" : "/");
                Set(ref _gegnerTokenPfad, prevalue);
                Einstellungen.SetEinstellung<string>("FoundryGegnerTokenPfad", GegnerTokenPfad);
            }
        }

        private string _heldTokenPfad = null;
        public string HeldTokenPfad
        {
            get { return _heldTokenPfad; }
            set
            {
                string prevalue = value;
                if (!prevalue.EndsWith("/") && !prevalue.EndsWith(@"\"))
                    prevalue = prevalue + (IsLocalInstalliert ? @"\" : "/");
                Set(ref _heldTokenPfad, prevalue);
                Einstellungen.SetEinstellung<string>("FoundryHeldTokenPfad", HeldTokenPfad);
            }
        }

        private string _musikPfad = null;
        public string MusikPfad
        {
            get { return _musikPfad; }
            set
            {
                string prevalue = value;
                if (!prevalue.EndsWith("/") && !prevalue.EndsWith(@"\"))
                    prevalue = prevalue + (IsLocalInstalliert ? @"\" : "/");
                Set(ref _musikPfad, prevalue);
                Einstellungen.SetEinstellung<string>("FoundryMusikPfad", MusikPfad);
            }
        }


        private Held _selectedDBHeld = null;
        public Held SelectedDBHeld
        {
            get { return _selectedDBHeld; }
            set { Set(ref _selectedDBHeld, value); }
        }

        private string _tokenName = null;
        public string TokenName
        {
            get { return _tokenName; }
            set { Set(ref _tokenName, value); }
        }

        private List<string> _lstdisplayName = new List<string>();
        public List<string> lstDisplayName
        {
            get { return _lstdisplayName; }
            set { Set(ref _lstdisplayName, value); }
        }

        private string _displayName = null;
        public string DisplayName
        {
            get { return _displayName; }
            set { Set(ref _displayName, value); }
        }

        private List<string> _lstRepresentedName = new List<string>();
        public List<string> lstRepresentedName
        {
            get { return _lstRepresentedName; }
            set { Set(ref _lstRepresentedName, value); }
        }

        private string _representedName = null;
        public string RepresentedName
        {
            get { return _representedName; }
            set { Set(ref _representedName, value); }
        }

        private bool _dsa41Version = true;
        public bool DSA41Version
        {
            get { return _dsa41Version; }
            set { Set(ref _dsa41Version, value); }
        }

        private bool _linkActorData = false;
        public bool LinkActorData
        {
            get { return _linkActorData; }
            set { Set(ref _linkActorData, value); }
        }

        private List<string> _lstTokenDisposition = new List<string>();
        public List<string> lstTokenDisposition
        {
            get { return _lstTokenDisposition; }
            set { Set(ref _lstTokenDisposition, value); }
        }

        private string _tokenDisposition = null;
        public string TokenDisposition
        {
            get { return _tokenDisposition; }
            set { Set(ref _tokenDisposition, value); }
        }

        private string _localFoundryPfad = null;
        public string localFoundryPfad
        {
            get { return _localFoundryPfad; }
            set { Set(ref _localFoundryPfad, value); }
        }


        private string _foundryPfad = null;
        public string FoundryPfad
        {
            get { return _foundryPfad; }
            set { Set(ref _foundryPfad, value); }
        }

        private string _playlistStatus = null;
        public string PlaylistStatus
        {
            get { return _playlistStatus; }
            set { Set(ref _playlistStatus, value); }
        }

        private bool _overwritePictureFile = false;
        public bool OverwritePictureFile
        {
            get { return _overwritePictureFile; }
            set { Set(ref _overwritePictureFile, value); }
        }
        private bool _overwritePlaylistFile = false;
        public bool OverwritePlaylistFile
        {
            get { return _overwritePlaylistFile; }
            set { Set(ref _overwritePlaylistFile, value); }
        }

        private bool _copyTitelFile = false;
        public bool CopyTitelFile
        {
            get { return _copyTitelFile; }
            set { Set(ref _copyTitelFile, value); }
        }

        private folder _selectedWaffenFolder = null;
        public folder SelectedWaffenFolder
        {
            get { return _selectedWaffenFolder; }
            set { Set(ref _selectedWaffenFolder, value);
                if (value != null && string.IsNullOrEmpty(WaffenKompendium))
                    WaffenKompendium = value.name;
            }
        }

        private folder _selectedHeldenFolder = null;
        public folder SelectedHeldenFolder
        {
            get { return _selectedHeldenFolder; }
            set
            {
                Set(ref _selectedHeldenFolder, value);
                if (value != null && string.IsNullOrEmpty(HeldenKompendium))
                    HeldenKompendium = value.name;
            }
        }
        private folder _selectedGegnerFolder = null;
        public folder SelectedGegnerFolder
        {
            get { return _selectedGegnerFolder; }
            set { Set(ref _selectedGegnerFolder, value);
                if (value != null && string.IsNullOrEmpty(GegnerKompendium))
                    GegnerKompendium = value.name;
            }
        }

        private string _gegnerKompendium = null;
        public string GegnerKompendium
        {
            get { return _gegnerKompendium; }
            set { Set(ref _gegnerKompendium, value); }
        }
        private string _heldenKompendium = null;
        public string HeldenKompendium
        {
            get { return _heldenKompendium; }
            set { Set(ref _heldenKompendium, value); }
        }
        private string _playlistsKompendium = null;
        public string PlaylistsKompendium
        {
            get { return _playlistsKompendium; }
            set { Set(ref _playlistsKompendium, value); }
        }

        private string _waffenKompendium = null;
        public string WaffenKompendium
        {
            get { return _waffenKompendium; }
            set { Set(ref _waffenKompendium, value); }
        }

        private string _selectedWorld = null;
        public string SelectedWorld
        {
            get { return _selectedWorld; }
            set
            {
                Set(ref _selectedWorld, value);

                if (value != null)
                {
                    if (IsLocalInstalliert)
                        GetActorFolders(string.Format(@"{0}worlds\{1}\data\folders.db", FoundryPfad, value));
                    else
                        GetActorFolders(string.Format("{0}worlds/{1}/data/folders.db", FoundryPfad, value));
                    HeldenKompendium = null;
                    GegnerKompendium = null;
                    PlaylistsKompendium = null;
                }
            }
        }
        #endregion

        #region //---- FELDER ----


        #endregion

        #region //---- EIGENSCHAFTEN ----

        #endregion

        #region //---- LISTEN ----

        private List<WaffeTalent> lstWTalent = new List<WaffeTalent>()
        {
            new WaffeTalent{ DSA5Gruppe = "Anderthalbhänder", DSA4Gruppe = "Anderthalbhänder", StF = "E", guidvalue = "kk", wtype = "melee" , img = "meleeweapon/Anderthalbhaender2H.webp"},
            new WaffeTalent{ DSA5Gruppe = "Armbrüste", DSA4Gruppe = "Armbrust", StF = "C", guidvalue = "ff", wtype = "range", img = "rangeweapon/LeichteArmbrust.webp"},
            new WaffeTalent{ DSA5Gruppe = "Belagerungswaffen", DSA4Gruppe = "Belagerungswaffen",StF =  "D", guidvalue = "ko", wtype = "melee" , img = "rangeweapon/Stein.webp"},
            new WaffeTalent{ DSA5Gruppe = "Blasrohre", DSA4Gruppe = "Blasrohr",StF =  "D", guidvalue = "ff", wtype = "range", img = "rangeweapon/Stein.webp"},
            new WaffeTalent{ DSA5Gruppe = "Bögen",DSA4Gruppe =  "Bogen",StF =  "E", guidvalue = "ff", wtype = "range", img = "rangeweapon/Kurzbogen.webp.webp"},
            new WaffeTalent{ DSA5Gruppe = "Diskusse", DSA4Gruppe = "Diskus", StF =  "D", guidvalue = "ff", wtype = "range", img = "rangeweapon/Stein.webp"},
            new WaffeTalent{ DSA5Gruppe = "Dolche", DSA4Gruppe = "Dolche",StF =  "D", guidvalue = "ge", wtype = "melee", img = "meleeweapon/Dolch.webp"},
            new WaffeTalent{ DSA5Gruppe = "Fechtwaffen", DSA4Gruppe = "Fechtwaffen", StF = "E",guidvalue =  "ge", wtype = "melee", img = "meleeweapon/Rapier.webp"},
            new WaffeTalent{ DSA5Gruppe = "Hiebwaffen", DSA4Gruppe = "Hiebwaffen",StF =  "D",guidvalue =  "kk", wtype = "melee", img = "meleeweapon/Streitkolben.webp"},
            new WaffeTalent{ DSA5Gruppe = "Spiesswaffen",DSA4Gruppe =  "Infanteriewaffen",StF =  "D", guidvalue = "kk", wtype = "melee", img = "meleeweapon/Kriegslanze.webp"},
            new WaffeTalent{ DSA5Gruppe = "Kettenstäbe", DSA4Gruppe = "Kettenstäbe", StF = "E", guidvalue = "ff", wtype = "melee", img = "meleeweapon/Dschadra2H.webp"},
            new WaffeTalent{ DSA5Gruppe = "Kettenwaffen", DSA4Gruppe = "Kettenwaffen", StF = "D", guidvalue = "kk", wtype = "melee", img = "meleeweapon/Dschadra2H.webp"},
            new WaffeTalent{ DSA5Gruppe = "Lanzen", DSA4Gruppe = "Lanzenreiten",StF =  "E",guidvalue =  "kk", wtype = "melee", img = "meleeweapon/Kriegslanze.webp"},
            new WaffeTalent{ DSA5Gruppe = "Peitschen", DSA4Gruppe = "Peitsche", StF = "E", guidvalue = "ff", wtype = "melee", img = "meleeweapon/Dolch.webp"},
            new WaffeTalent{ DSA5Gruppe = "Raufen",DSA4Gruppe =  "Raufen", StF = "C", guidvalue = "ge", wtype = "melee", img = "meleeweapon/Dolch.webp"},
            new WaffeTalent{ DSA5Gruppe = "Ringen", DSA4Gruppe = "Ringen", StF = "D", guidvalue = "ge", wtype = "melee", img = "meleeweapon/Dolch.webp"},
            new WaffeTalent{ DSA5Gruppe = "Säbel", DSA4Gruppe = "Säbel", StF = "D", guidvalue = "ge", wtype = "melee", img = "meleeweapon/Saebel.webp"},
            new WaffeTalent{ DSA5Gruppe = "Schleudern", DSA4Gruppe = "Schleuder", StF = "E", guidvalue = "ff", wtype = "range", img = "rangeweapon/Stein.webp"},
            new WaffeTalent{ DSA5Gruppe = "Schwerter", DSA4Gruppe = "Schwerter",StF =  "E", guidvalue = "kk", wtype = "melee", img = "meleeweapon/Barbarenschwert.webp"},
            new WaffeTalent{ DSA5Gruppe = "Speere", DSA4Gruppe = "Speere", StF = "D", guidvalue = "ff", wtype = "melee", img = "meleeweapon/Speer2H.webp"},
            new WaffeTalent{ DSA5Gruppe = "Wurfwaffen", DSA4Gruppe = "Wurfbeile",StF =  "D", guidvalue = "ff", wtype = "range", img = "rangeweapon/Wurfbeil.webp"},
            new WaffeTalent{ DSA5Gruppe = "Wurfwaffen",DSA4Gruppe =  "Wurfmesser",StF =  "C", guidvalue = "ff", wtype = "range", img = "rangeweapon/Messer.webp"},
            new WaffeTalent{ DSA5Gruppe = "Wurfwaffen",DSA4Gruppe =  "Wurfspeere",StF =  "C", guidvalue = "ff", wtype = "range", img = "rangeweapon/Wurfspeer.webp"},
            new WaffeTalent{ DSA5Gruppe = "Zweihandflegel", DSA4Gruppe = "Zweihandflegel",StF =  "D", guidvalue = "kk", wtype = "melee", img = "meleeweapon/Zwergenschlaegel2H.webp"},
            new WaffeTalent{ DSA5Gruppe = "Zweihandhiebwaffen", DSA4Gruppe = "Zweihand-Hiebwaffen", StF = "D", guidvalue = "kk", wtype = "melee", img = "meleeweapon/Barbarenstreitaxt2H.webp"},
            new WaffeTalent{ DSA5Gruppe = "Zweihandschwerter/-säbel", DSA4Gruppe = "Zweihandschwerter/-säbel", StF = "E", guidvalue = "kk", wtype = "melee", img = "meleeweapon/Rondrakamm2H.webp"} };

        public List<GegnerBase> lstGegnerBase
        {
            get { return Global.ContextHeld.Liste<GegnerBase>().OrderBy(h => h.Name).ToList(); }
        }

        public List<Audio_Playlist> lstPlaylists
        {
            get { return Global.ContextAudio.PlaylistListe.OrderBy(h => h.Name).ToList(); }
        }

        private List<folder> _lstFolders = new List<folder>();
        public List<folder> lstFolders
        {
            get { return _lstFolders; }
            set { Set(ref _lstFolders, value); }
        }

        private List<string> _lstWorlds = new List<string>();
        public List<string> lstWorlds
        {
            get { return _lstWorlds; }
            set { Set(ref _lstWorlds, value); }
        }
        private int _portNo = 0;
        public int PortNo
        {
            get { return _portNo; }
            set { Set(ref _portNo, value); }
        }

        private string _localUri = "http://192.168.178.181:30000/";
        public string LocalUri
        {
            get { return _localUri; }
            set { Set(ref _localUri, value); }
        }
        private string _inetUri = "http://1.2.3.4:30000/";
        public string InetUri
        {
            get { return _inetUri; }
            set { Set(ref _inetUri, value); }
        }
        #endregion

        #region //---- KONSTRUKTOR ----

        public FoundryViewModel()
        {
            FTPAdresse = Einstellungen.GetEinstellung<string>("FoundryFTPAdresse");
            FTPUser = Einstellungen.GetEinstellung<string>("FoundryFTPUser");
            FTPPasswort = Einstellungen.GetEinstellung<string>("FoundryFTPPasswort");

            IsLocalInstalliert = Einstellungen.GetEinstellung<bool>("IsLocalInstalliert");

            Init();
            lstRepresentedName.AddRange(lstHeldArgument.Select(t => t.h.Name));
            lstDisplayName.AddRange(new List<string>() {
                "Never Displayed", "When Controlled", "Hovered by Owner", "Hovered by Anyone", "Always for Owner", "Always for Everyone" });
            DisplayName = lstDisplayName.First();
            LinkActorData = false;
            lstTokenDisposition = new List<string>() { "Neutral", "Friendly", "Hostile" };
            TokenDisposition = lstTokenDisposition.Last();

            stdPfad.AddRange(MeisterGeister.Logic.Einstellung.Einstellungen.AudioVerzeichnis.Split(new Char[] { '|' }));
            startup = false;
        }

        private List<string> _stdPfad = new List<string>();
        public List<string> stdPfad
        {
            get { return _stdPfad; }
            set { Set(ref _stdPfad, value); }
        }

        private FtpWebRequest _listRequest = null;
        public FtpWebRequest listRequest
        {
            get { return _listRequest; }
            set { Set(ref _listRequest, value); }
        }

        #endregion

        #region //---- Funktionen ----
        void ListFtpDirectory(string url, string rootPath, bool onlyDir, NetworkCredential credentials, List<string> list)
        {
            StreamReader listReader = null;
            var listRequest = (FtpWebRequest)WebRequest.Create(url + rootPath);
            listRequest.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
            listRequest.Credentials = credentials;
            listRequest.KeepAlive = false;
            listRequest.UsePassive = true;
            listRequest.Timeout = 4000;

            var lines = new List<string>();
            try
            {
                using (var listResponse = (FtpWebResponse)listRequest.GetResponse())
                using (var listStream = listResponse.GetResponseStream())
                using (listReader = new StreamReader(listStream))
                {
                    while (!listReader.EndOfStream)
                    {
                        lines.Add(listReader.ReadLine());
                        if (onlyDir)
                        {
                            string[] tokens =
                                lines.Last().Split(new[] { ' ' }, 9, StringSplitOptions.RemoveEmptyEntries);
                            string permissions = tokens[0];
                            string name = tokens[8];
                            if (permissions[0] == 'd')
                                list.Add(name);
                        }
                    }
                }

                if (!onlyDir)
                {
                    foreach (string line in lines)
                    {
                        string[] tokens =
                            line.Split(new[] { ' ' }, 9, StringSplitOptions.RemoveEmptyEntries);
                        string name = tokens[8];
                        string permissions = tokens[0];

                        string filePath = rootPath + name;

                        if (permissions[0] == 'd')
                        {
                            ListFtpDirectory(url, filePath + "/", onlyDir, credentials, list);
                        }
                        else
                        {
                            list.Add(filePath);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError(string.Format("Beim Lesen der FTP Seite {0} ist ein Fehler aufgetreten", url + rootPath), ex);
            }
            finally
            {
                if (listReader != null)
                    listReader.Close();
            }
        }
        private void LoadWorldsFolder()
        {
            if (IsLocalInstalliert)
            {
                List<string> lst = new List<string>();
                string worldPfad = FoundryPfad + @"worlds";
                List<string> lstFullDir = Directory.GetDirectories(worldPfad).ToList();
                lstFullDir.ForEach(delegate (string s)
                { lst.Add(new Uri(s).Segments.Last().ToUpper()); });

                lstWorlds = lst;
            }
            else
            {
                List<string> list = new List<string>();
                NetworkCredential credentials = new NetworkCredential(FTPUser, FTPPasswort);
                ListFtpDirectory(FTPAdresse + "/data/worlds/", "", true, credentials, list);
                lstWorlds = list;
            }
        }

        private void GetActorFolders(string filepath)
        {
            string FileData = GetFileData(filepath);

            if (FileData != null)
            {
                List<string> lstFileData = FileData.Split(new Char[] { '\n' }).ToList();
                List<folder> lst = new List<folder>();

                lst.Add(new folder() { name = "" });
                foreach (string data in lstFileData)
                {
                    if (string.IsNullOrEmpty(data))
                        continue;
                    string d = data.Substring(1, data.Length - 3);
                    List<string> line = d.Split(new Char[] { ',' }).ToList();
                    string nameF = line.FirstOrDefault(t => t.StartsWith("\"name\":"));
                    string typF = line.FirstOrDefault(t => t.StartsWith("\"type\":"));
                    string sortingF = line.FirstOrDefault(t => t.StartsWith("\"sorting\":"));
                    string parentF = line.FirstOrDefault(t => t.StartsWith("\"parent\":"));
                    string colorF = line.FirstOrDefault(t => t.StartsWith("\"color\":"));
                    string _idF = line.FirstOrDefault(t => t.StartsWith("\"_id\":"));

                    lst.Add(new folder()
                    {
                        name = nameF.Split(new Char[] { ':' }).Last().Replace("\"", ""),
                        typ = typF.Split(new Char[] { ':' }).Last().Replace("\"", ""),
                        sorting = sortingF.Split(new Char[] { ':' }).Last().Replace("\"", ""),
                        parent = parentF.Split(new Char[] { ':' }).Last().Replace("\"", ""),
                        color = colorF?.Split(new Char[] { ':' }).Last().Replace("\"", ""),
                        _id = _idF.Split(new Char[] { ':' }).Last().Replace("\"", "")
                    });
                }
                lstFolders = lst;
            }
        }

        private void SetFileData(string file, string daten, bool datenInFile = false)
        {
            if (IsLocalInstalliert)
                File.WriteAllText(file, daten);
            else
            {
                string tempDatei = null;
                if (!datenInFile)
                {
                    tempDatei = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\tempData.txt";
                    File.WriteAllText(tempDatei, daten);
                }
                else
                    tempDatei = daten;

                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(file);
                request.Credentials = new NetworkCredential(FTPUser, FTPPasswort);
                request.Method = WebRequestMethods.Ftp.UploadFile;

                using (Stream fileStream = File.OpenRead(tempDatei))
                using (Stream ftpStream = request.GetRequestStream())
                {
                    fileStream.CopyTo(ftpStream);
                }

                if (!datenInFile)
                    File.Delete(tempDatei);
            }
        }
        private string GetFileData(string filepath)
        {
            try
            {
                string FileData = null;
                if (IsLocalInstalliert)
                {
                    try
                    {
                        if (File.Exists(filepath))
                            FileData = File.ReadAllText(filepath).Trim();
                    }
                    catch (Exception ex)
                    {
                        ViewHelper.ShowError("Beim Schreiben der Datei ist ein Fehler aufgetreten", ex);
                    }
                }
                else
                {
                    if (startup)
                        doLoad = ViewHelper.Confirm("Auf FTP-Adresse zugreifen?", "Soll versucht werden auf die vorherig gespeicherte FTP-Seite zuzugreifen:\n\n" +
                        "FTP-Adresse:  " + FTPAdresse + "/config/options.json<\n" + "Pfad:   " + FoundryPfad + "\n"+
                        "FTP-User:  "+FTPUser+"\nFPT-Password:  "+MachSterne(FTPPasswort));
                    if (!doLoad)
                        return null;
                    FtpWebRequest request = (FtpWebRequest)WebRequest.Create(filepath);
                    request.Method = WebRequestMethods.Ftp.DownloadFile;
                    request.Credentials = new NetworkCredential(FTPUser, FTPPasswort, "");
                    FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                    Stream responseStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(responseStream);
                    FileData = reader.ReadToEnd().Trim();
                    Console.WriteLine($"Download Complete, status {response.StatusDescription}");
                    reader.Close();
                    response.Close();
                }
                return FileData;
            }
            catch (Exception ex)
            {

                ViewHelper.ShowError(string.Format("Beim Verbinden mit der FTP Seite {0} ist ein Fehler aufgetreten", filepath), ex);
                return null;
            }
        }

        private string MachSterne(string s)
        {
            string back = "";
            bool jump = false;
            for (int i = 0; i < s.Length; i++)
            {
                if (jump) back += s.Substring(i, 1); else back += "*";
                jump = !jump;
            }
            return back;
        }
        private string GetUserID(string filepath, string username)
        {
            char A = (char)34;
            string id = null;
            try
            {
                string FileData = GetFileData(filepath);

                if (FileData != null)
                {
                    List<string> lstFileData = FileData.Split(new Char[] { '\n' }).ToList();
                    string uLine = lstFileData.FirstOrDefault(t => t.Contains(A + "name" + A + ":" + A + username + A));
                    if (uLine == null)
                        return null;
                    string d = uLine.Substring(1, uLine.Length - 3);
                    List<string> line = d.Split(new Char[] { ',' }).ToList();
                    string _idF = line.FirstOrDefault(t => t.StartsWith(A + "_id" + A + ":"));
                    id = _idF.Substring(7, _idF.Length - 8);
                }
                return id;
            }
            catch (Exception ex)
            { return null; }
        }

        #endregion

        #region //---- INSTANZMETHODEN ----

        private void ReadFoundryOptions(string filepath)
        {
            string FileData = GetFileData(filepath);
            if (FileData != null)
            {
                List<string> lstFileData = FileData.Split(new Char[] { '\n' }).ToList();

                string portLine = lstFileData.FirstOrDefault(t => t.StartsWith("  \"port\":"));
                portLine = portLine.Trim().TrimEnd(new Char[] { ',' });
                int portNo = 0;
                int.TryParse(portLine.Substring(portLine.IndexOf(":") + 1), out portNo);
                PortNo = portNo;

                if (IsLocalInstalliert)
                {
                    string dataPathLine = lstFileData.FirstOrDefault(t => t.StartsWith("  \"dataPath\":"));
                    dataPathLine = dataPathLine.Trim().TrimEnd(new Char[] { ',' }).Trim();
                    string dataPath = dataPathLine.Substring(dataPathLine.IndexOf(":") + 1).Trim().Trim(new Char[] { '"' });
                    dataPath = dataPath.Replace("/", @"\");
                    dataPath += @"\data\";
                    FoundryPfad = dataPath;
                    localFoundryPfad = FoundryPfad;
                    LoadWorldsFolder();
                }
            }
            else
            {
                FoundryPfad = null;
                if (startup)
                    IsLocalInstalliert = true;
            }
        }
        private void BildSpeichern(string bildDateiname, string WesenPfad, string MGPfad, List<string> lstFTPGegnerPics, List<string> lstDateienKopiert)
        {
            if (!string.IsNullOrEmpty(bildDateiname) && !lstDateienKopiert.Contains(bildDateiname))
            {
                string charBild = bildDateiname;
                string srcCharFilename = System.IO.Path.GetFileName(charBild);
                if (bildDateiname.StartsWith("/"))
                {
                    BitmapImage bmpi1 = new BitmapImage(new Uri("pack://application:,,," + bildDateiname));
                    using (MemoryStream outStream = new MemoryStream())
                    {
                        PngBitmapEncoder enc = new PngBitmapEncoder();
                        enc.Frames.Add(BitmapFrame.Create(bmpi1));

                        FileStream fs = null;
                        if (IsLocalInstalliert)
                        {
                            if (!File.Exists(FoundryPfad + srcCharFilename) || OverwritePictureFile)
                                fs = File.Open(FoundryPfad + srcCharFilename, FileMode.Create);
                        }
                        else
                        {
                            //Speichern temporär ins MG-Verzeichnis 
                            if (!lstFTPGegnerPics.Contains(srcCharFilename) || OverwritePictureFile)
                                fs = File.Open(MGPfad + srcCharFilename, FileMode.Create);
                        }
                        if (fs != null)
                        {
                            enc.Save(fs);
                            fs.Close();
                        }
                        if (!IsLocalInstalliert)
                        {
                            if (!lstFTPGegnerPics.Contains(srcCharFilename) || OverwritePictureFile)
                                SetFileData(FoundryPfad + WesenPfad + System.IO.Path.GetFileName(charBild), MGPfad + srcCharFilename, true);
                            File.Delete(MGPfad + srcCharFilename);
                        }
                    }
                }
                else
                if (File.Exists(charBild))
                {
                    if (IsLocalInstalliert)
                    {
                        if (!File.Exists(FoundryPfad + WesenPfad.Replace("/", @"\") + srcCharFilename) || OverwritePictureFile)
                        {
                            try
                            {
                                File.Copy(charBild, FoundryPfad + WesenPfad.Replace("/", @"\") + srcCharFilename, OverwritePictureFile);
                            }
                            catch (Exception)
                            { }
                        }
                    }
                    else
                    {
                        if (!lstFTPGegnerPics.Contains(srcCharFilename) || OverwritePictureFile)
                        {
                            SetFileData(FoundryPfad + WesenPfad + System.IO.Path.GetFileName(charBild), charBild, true);
                        }
                    }
                }
                lstDateienKopiert.Add(charBild);
            }
        }

        public static string FlowDocument_GetText(System.Windows.Documents.FlowDocument fd)
        {
            System.Windows.Documents.TextRange tr = new System.Windows.Documents.TextRange(fd.ContentStart, fd.ContentEnd);
            return tr.Text;
        }

        private List<dbArgument> SetDSA41Arguments(GegnerBase g, string GetFilenamePortrait, string GetFilenameToken, string id, int AnzChr)
        {
            char A = (char)34;
            string GMid = GetUserID(string.Format(@"{0}worlds\{1}\data\users.db", FoundryPfad, SelectedWorld), "Gamemaster");

            List<dbArgument> lstArg = new List<dbArgument>();

            lstArg.Add(new dbArgument { Prefix = "{" + A + "_id" + A + ":\"", ArgString = id, Suffix = A + "," });
            lstArg.Add(new dbArgument { Prefix = A + "name" + A + ":\"", ArgString = g.Name, Suffix = A + "," });
            lstArg.Add(new dbArgument { Prefix = A + "type" + A + ":\"", ArgString = "character", Suffix = A + "," });
            lstArg.Add(new dbArgument { Prefix = A + "img" + A + ":\"", ArgString = (string.IsNullOrEmpty(GetFilenamePortrait) ? "icons/svg/mystery-man.svg" : GetFilenamePortrait), Suffix = A + "," });
            //lstArg.Add(new dbArgument { Prefix = A + "permission" + A + ":{\"default" + A + ":", ArgString = "0,\"3WHJiGe2LNC2VNeR" + A + ":", Suffix = "3}," });
            lstArg.Add(new dbArgument { Prefix = A + "data" + A + ":{" });
            lstArg.Add(new dbArgument { Prefix = A + "base" + A + ":{" });
            lstArg.Add(new dbArgument { Prefix = A + "basicAttributes" + A + ":{" });
            lstArg.Add(new dbArgument { Prefix = A + "courage" + A + ":{" + A + "value" + A + ":", ArgString = (g.MU ?? 0).ToString(), Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "cleverness" + A + ":{" + A + "value" + A + ":", ArgString = (g.KL ?? 0).ToString(), Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "intuition" + A + ":{" + A + "value" + A + ":", ArgString = (g.IN ?? 0).ToString(), Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "charisma" + A + ":{" + A + "value" + A + ":", ArgString = (g.CH ?? 0).ToString(), Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "dexterity" + A + ":{" + A + "value" + A + ":", ArgString = (g.FF ?? 0).ToString(), Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "agility" + A + ":{" + A + "value" + A + ":", ArgString = (g.GE ?? 0).ToString(), Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "constitution" + A + ":{" + A + "value" + A + ":", ArgString = (g.KO).ToString(), Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "strength" + A + ":{" + A + "value" + A + ":", ArgString = (g.KK ?? 0).ToString(), Suffix = "}}," });

            lstArg.Add(new dbArgument { Prefix = A + "resources" + A + ":{" });

            lstArg.Add(new dbArgument { Prefix = A + "vitality" + A + ":{" });
            lstArg.Add(new dbArgument { Prefix = A + "value" + A + ":", ArgString = g.LE.ToString(), Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "min" + A + ":", ArgString = "0", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "max" + A + ":", ArgString = g.LE.ToString(), Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "endurance" + A + ":{" });
            lstArg.Add(new dbArgument { Prefix = A + "value" + A + ":", ArgString = g.AU.ToString(), Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "min" + A + ":", ArgString = "0", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "max" + A + ":", ArgString = g.AU.ToString(), Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "karmicEnergy" + A + ":{" });
            lstArg.Add(new dbArgument { Prefix = A + "value" + A + ":", ArgString = g.KE.ToString(), Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "min" + A + ":", ArgString = "0", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "max" + A + ":", ArgString = g.KE.ToString(), Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "astralEnergy" + A + ":{" });
            lstArg.Add(new dbArgument { Prefix = A + "value" + A + ":", ArgString = g.AE.ToString(), Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "min" + A + ":", ArgString = "0", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "max" + A + ":", ArgString = g.AE.ToString(), Suffix = "}}," });

            lstArg.Add(new dbArgument { Prefix = A + "combatAttributes" + A + ":{" });
            lstArg.Add(new dbArgument { Prefix = A + "active" + A + ":{" + A + "baseInitiative" + A + ":{" });
            lstArg.Add(new dbArgument { Prefix = A + "value" + A + ":", ArgString = g.INIBasis.ToString(), Suffix = "}," });

            lstArg.Add(new dbArgument { Prefix = A + "baseAttack" + A + ":{" });
            lstArg.Add(new dbArgument { Prefix = A + "value" + A + ":", ArgString = g.AT.ToString(), Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "baseParry" + A + ":{" });
            lstArg.Add(new dbArgument { Prefix = A + "value" + A + ":", ArgString = g.PA.ToString(), Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "baseRangedAttack" + A + ":{" });
            lstArg.Add(new dbArgument { Prefix = A + "value" + A + ":", ArgString = g.FK.ToString(), Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "dodge" + A + ":{" });
            lstArg.Add(new dbArgument { Prefix = A + "value" + A + ":", ArgString = g.PA.ToString(), Suffix = "}}," });

            lstArg.Add(new dbArgument { Prefix = A + "passive" + A + ":{" });
            lstArg.Add(new dbArgument { Prefix = A + "magicResistance" + A + ":{" });
            lstArg.Add(new dbArgument { Prefix = A + "value" + A + ":", ArgString = g.MRGeist.ToString(), Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "physicalResistance" + A + ":{" });
            lstArg.Add(new dbArgument { Prefix = A + "value" + A + ":", ArgString = (g.RS[Kampf.Logic.Trefferzone.Gesamt]).ToString(), Suffix = "}}}," });

            lstArg.Add(new dbArgument { Prefix = A + "movement" + A + ":{" });
            lstArg.Add(new dbArgument { Prefix = A + "speed" + A + ":{" });
            lstArg.Add(new dbArgument { Prefix = A + "value" + A + ":", ArgString = g.GS.ToString(), Suffix = "," + A + "unit" + A + ":" + A + "Schritt(" +(g.GS2??0)+"/"+(g.GS3??0)+")"+ A + "}}," });

            lstArg.Add(new dbArgument { Prefix = A + "combatState" + A + ":{" });
            lstArg.Add(new dbArgument { Prefix = A + "isArmed" + A + ":true", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "primaryHand" + A + ":" + A + "null" + A, Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "secondaryHand" + A + ":" + A + "null" + A, Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "unarmedTalent" + A + ":" + "null", Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "race" + A + ":" + "null", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "culture" + A + ":" + "null", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "profession" + A + ":" + "null", Suffix = "," });

            lstArg.Add(new dbArgument { Prefix = A + "social" + A + ":{" });
            lstArg.Add(new dbArgument { Prefix = A + "social_status" + A + ":{" });
            lstArg.Add(new dbArgument { Prefix = A + "value" + A + ":", ArgString = (g.SO ?? 0).ToString(), Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "titles" + A + ":[]," });
            lstArg.Add(new dbArgument { Prefix = A + "nobility" + A + ":" + "null", Suffix = "}," });

            lstArg.Add(new dbArgument { Prefix = A + "appearance" + A + ":{" });
            lstArg.Add(new dbArgument { Prefix = A + "height" + A + ":" + A + A, Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "weight" + A + ":" + A + A, Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "birthday" + A + ":" + A + A, Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "eye_colour" + A + ":" + A + A, Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "hair_colour" + A + ":" + A + A, Suffix = "," });

            string outcome = g.Bemerkung??"";
            outcome = outcome.Replace(A.ToString(), ((char)92).ToString() + ((char)34).ToString())
                    .Replace(((char)9).ToString(), "  ")
                    .Replace("\r\n", ((char)92).ToString() + ((char)110).ToString())
                    .Replace(((char)13).ToString() , ((char)92).ToString() + ((char)110).ToString())
                    .Replace(((char)10).ToString(), ((char)92).ToString() + ((char)110).ToString());
            
            lstArg.Add(new dbArgument { Prefix = A + "description" + A + ":" + A + (outcome) + A, Suffix = "}," });

            lstArg.Add(new dbArgument { Prefix = A + "equiped" + A + ":{" });
            lstArg.Add(new dbArgument { Prefix = A + "primaryHand" + A + ":" + A + "null" + A, Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "secondaryHand" + A + ":" + A + "null" + A, Suffix = "}}," });

            //Talent-Werte
            lstArg.Add(new dbArgument { Prefix = A + "owned" + A + ":{" });
            lstArg.Add(new dbArgument { Prefix = "", Suffix = "}}," });
            //ENDE Talent-werte

            lstArg.Add(new dbArgument { Prefix = A + "token" + A + ":{" });
            lstArg.Add(new dbArgument { Prefix = A + "flags" + A + ":{", Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "name" + A + ":\"", ArgString = g.Name, Suffix = A + "," });
            lstArg.Add(new dbArgument { Prefix = A + "displayName" + A + ":", ArgString = "0", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "img" + A + ":\"", ArgString = (string.IsNullOrEmpty(GetFilenameToken) ? "icons/svg/mystery-man.svg" : GetFilenameToken), Suffix = A + "," });
            lstArg.Add(new dbArgument { Prefix = A + "tint" + A + ":", ArgString = "null", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "width" + A + ":", ArgString = "1", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "height" + A + ":", ArgString = "1", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "scale" + A + ":", ArgString = g.TokenOversize == null ? "1" : Math.Round(g.TokenOversize.Value, 1).ToString(), Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "lockRotation" + A + ":", ArgString = "false", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "rotation" + A + ":", ArgString = "0", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "vision" + A + ":", ArgString = "false", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "dimSight" + A + ":", ArgString = "0", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "brightSight" + A + ":", ArgString = "0", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "dimLight" + A + ":", ArgString = "0", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "brightLight" + A + ":", ArgString = "0", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "sightAngle" + A + ":", ArgString = "360", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "lightAngle" + A + ":", ArgString = "360", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "lightAlpha" + A + ":", ArgString = "1", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "lightAnimation" + A + ":{" });
            lstArg.Add(new dbArgument { Prefix = A + "speed" + A + ":", ArgString = "5", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "intensity" + A + ":", ArgString = "5", Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "actorId" + A + ":", ArgString = A + id + A, Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "actorLink" + A + ":", ArgString = "false", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "disposition" + A + ":", ArgString = "-1", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "displayBars" + A + ":", ArgString = "0", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "bar1" + A + ":{", ArgString = A + "attribute" + A + ":" + A + "base.resources.vitality" + A, Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "bar2" + A + ":{", ArgString = A + "attribute" + A + ":" + A + "base.resources.astralEnergy" + A, Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "randomImg" + A + ":", ArgString = "false", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "mirrorX" + A + ":", ArgString = "false", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "mirrorY" + A + ":", ArgString = "false", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "alpha" + A + ":", ArgString = "1", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "light" + A + ":{" });
            lstArg.Add(new dbArgument { Prefix = A + "alpha" + A + ":", ArgString = "0.5", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "angle" + A + ":", ArgString = "0", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "bright" + A + ":", ArgString = "0", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "coloration" + A + ":", ArgString = "1", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "dim" + A + ":", ArgString = "0", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "gradual" + A + ":", ArgString = "true", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "luminosity" + A + ":", ArgString = "0.5", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "saturation" + A + ":", ArgString = "0", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "contrast" + A + ":", ArgString = "0", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "shadows" + A + ":", ArgString = "0", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "animation" + A + ":{" });
            lstArg.Add(new dbArgument { Prefix = A + "speed" + A + ":", ArgString = "5", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "intensity" + A + ":", ArgString = "5", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "reverse" + A + ":", ArgString = "false", Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "darkness" + A + ":{", ArgString = A + "min" + A + ":0," + A + "max" + A + ":1", Suffix = "}}}," });

            lstArg.Add(new dbArgument { Prefix = A + "items" + A + ":[" });
            
            int no = 1;
            foreach (GegnerBase_Zauber gbZ in g.GegnerBase_Zauber)
            {
                GegnerBase_Zauber gbZauber = new GegnerBase_Zauber();
                DSA41_KämpferTalent KämpferT = new DSA41_KämpferTalent();
                KämpferT.gbz = gbZ;
                KämpferT.GMid = GMid;
                KämpferT.USERid = "wEwtIaGkqxrjiQ8r";
                lstArg.Add(new dbArgument { Prefix = KämpferT.GetLongInfoZauber(no) });
                no++;
            }
            lstArg.Last().Prefix = lstArg.Last().Prefix.TrimEnd(new Char[] { ',' });

            lstArg.Add(new dbArgument { Prefix = "", Suffix = "]," });
            lstArg.Add(new dbArgument { Prefix = A + "effects" + A + ":", ArgString = "[]", Suffix = "," });
            if (SelectedGegnerFolder== null)
                lstArg.Add(new dbArgument { Prefix = A + "folder" + A + ":", ArgString = "null", Suffix = "," });
            else
                lstArg.Add(new dbArgument { Prefix = A + "folder" + A + ":", ArgString = A + SelectedGegnerFolder._id + A, Suffix = "," });   //welches Verzeichnis 
            lstArg.Add(new dbArgument { Prefix = A + "sort" + A + ":", ArgString = (100000 + AnzChr).ToString(), Suffix = "," });

            lstArg.Add(new dbArgument { Prefix = A + "permission" + A + ":{", ArgString = A + "default" + A + ":0," + A + GMid + A + ":3", Suffix = "}," });  //wer darf editieren
            lstArg.Add(new dbArgument { Prefix = A + "flags" + A + ":{", ArgString = A + "core" + A + ":{" + A + "sheetClass" + A + ":" + A + "dsa-4.1.DsaFourOneActorSheet" + A + "}", Suffix = "}}" });


            return lstArg;
        }
        private List<dbArgument> SetDSA41Arguments(Held h, string GetFilenamePortrait, string GetFilenameToken, string id, int AnzChr)
        {
            char A = (char)34;
            string GMid = GetUserID(string.Format(@"{0}worlds\{1}\data\users.db", FoundryPfad, SelectedWorld), "Gamemaster");
            string Userid = GetUserID(string.Format(@"{0}worlds\{1}\data\users.db", FoundryPfad, SelectedWorld), h.Spieler) ?? "wEwtIaGkqxrjiQ8r";

            List<dbArgument> lstArg = new List<dbArgument>();

            lstArg.Add(new dbArgument { Prefix = "{" + A + "_id" + A + ":\"", ArgString = id, Suffix = A + "," });
            lstArg.Add(new dbArgument { Prefix = A + "name" + A + ":\"", ArgString = h.Name, Suffix = A + "," });
            lstArg.Add(new dbArgument { Prefix = A + "type" + A + ":\"", ArgString = "character", Suffix = A + "," });
            lstArg.Add(new dbArgument { Prefix = A + "img" + A + ":\"", ArgString = (string.IsNullOrEmpty(GetFilenamePortrait) ? "icons/svg/mystery-man.svg" : GetFilenamePortrait), Suffix = A + "," });
            //lstArg.Add(new dbArgument { Prefix = A + "permission" + A + ":{\"default" + A + ":", ArgString = "0,\"3WHJiGe2LNC2VNeR" + A + ":", Suffix = "3}," });
            lstArg.Add(new dbArgument { Prefix = A + "data" + A + ":{" });
            lstArg.Add(new dbArgument { Prefix = A + "base" + A + ":{" });
            lstArg.Add(new dbArgument { Prefix = A + "basicAttributes" + A + ":{" });
            lstArg.Add(new dbArgument { Prefix = A + "courage" + A + ":{" + A + "value" + A + ":", ArgString = (h.MU ?? 0).ToString(), Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "cleverness" + A + ":{" + A + "value" + A + ":", ArgString = (h.KL ?? 0).ToString(), Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "intuition" + A + ":{" + A + "value" + A + ":", ArgString = (h.IN ?? 0).ToString(), Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "charisma" + A + ":{" + A + "value" + A + ":", ArgString = (h.CH ?? 0).ToString(), Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "dexterity" + A + ":{" + A + "value" + A + ":", ArgString = (h.FF ?? 0).ToString(), Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "agility" + A + ":{" + A + "value" + A + ":", ArgString = (h.GE ?? 0).ToString(), Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "constitution" + A + ":{" + A + "value" + A + ":", ArgString = (h.KO ?? 0).ToString(), Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "strength" + A + ":{" + A + "value" + A + ":", ArgString = (h.KK ?? 0).ToString(), Suffix = "}}," });

            lstArg.Add(new dbArgument { Prefix = A + "resources" + A + ":{" });

            lstArg.Add(new dbArgument { Prefix = A + "vitality" + A + ":{" });
            lstArg.Add(new dbArgument { Prefix = A + "value" + A + ":", ArgString = h.LebensenergieAktuell.ToString(), Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "min" + A + ":", ArgString = "0", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "max" + A + ":", ArgString = h.LebensenergieMax.ToString(), Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "endurance" + A + ":{" });
            lstArg.Add(new dbArgument { Prefix = A + "value" + A + ":", ArgString = h.AusdauerAktuell.ToString(), Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "min" + A + ":", ArgString = "0", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "max" + A + ":", ArgString = h.AusdauerMax.ToString(), Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "karmicEnergy" + A + ":{" });
            lstArg.Add(new dbArgument { Prefix = A + "value" + A + ":", ArgString = h.KarmaenergieAktuell.ToString(), Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "min" + A + ":", ArgString = "0", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "max" + A + ":", ArgString = h.KarmaenergieMax.ToString(), Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "astralEnergy" + A + ":{" });
            lstArg.Add(new dbArgument { Prefix = A + "value" + A + ":", ArgString = h.AstralenergieAktuell.ToString(), Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "min" + A + ":", ArgString = "0", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "max" + A + ":", ArgString = h.AstralenergieMax.ToString(), Suffix = "}}," });

            lstArg.Add(new dbArgument { Prefix = A + "combatAttributes" + A + ":{" });
            lstArg.Add(new dbArgument { Prefix = A + "active" + A + ":{"+A+ "baseInitiative"+A+":{" });
            lstArg.Add(new dbArgument { Prefix = A + "value" + A + ":", ArgString = h.InitiativeBasis.ToString(), Suffix = "}," });

            lstArg.Add(new dbArgument { Prefix = A + "baseAttack" + A + ":{" });
            lstArg.Add(new dbArgument { Prefix = A + "value" + A + ":", ArgString = h.AttackeBasis.ToString(), Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "baseParry" + A + ":{" });
            lstArg.Add(new dbArgument { Prefix = A + "value" + A + ":", ArgString = h.ParadeBasis.ToString(), Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "baseRangedAttack" + A + ":{" });
            lstArg.Add(new dbArgument { Prefix = A + "value" + A + ":", ArgString = h.FernkampfBasis.ToString(), Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "dodge" + A + ":{" });
            lstArg.Add(new dbArgument { Prefix = A + "value" + A + ":", ArgString = h.Ausweichen.ToString(), Suffix = "}}," });

            lstArg.Add(new dbArgument { Prefix = A + "passive" + A + ":{" });
            lstArg.Add(new dbArgument { Prefix = A + "magicResistance" + A + ":{" });
            lstArg.Add(new dbArgument { Prefix = A + "value" + A + ":", ArgString = h.Magieresistenz.ToString(), Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "physicalResistance" + A + ":{" });
            lstArg.Add(new dbArgument { Prefix = A + "value" + A + ":", ArgString =  h.BerechneRüstungswerte().ToString(), Suffix = "}}}," });  

            lstArg.Add(new dbArgument { Prefix = A + "movement" + A + ":{" });
            lstArg.Add(new dbArgument { Prefix = A + "speed" + A + ":{" });
            lstArg.Add(new dbArgument { Prefix = A + "value" + A + ":", ArgString = h.Geschwindigkeit.ToString(), Suffix = ","+A+"unit"+A+":"+A+"Schritt"+A+"}}," });

            lstArg.Add(new dbArgument { Prefix = A + "combatState" + A + ":{" });
            lstArg.Add(new dbArgument { Prefix = A + "isArmed" + A + ":true", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "primaryHand" + A + ":" + A + "null" + A, Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "secondaryHand" + A + ":" + A + "null" + A, Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "unarmedTalent" + A + ":" + "null" , Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "race" + A + ":" +  "null" , Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "culture" + A + ":" + "null" , Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "profession" + A + ":" + "null", Suffix = "," });

            lstArg.Add(new dbArgument { Prefix = A + "social" + A + ":{" });
            lstArg.Add(new dbArgument { Prefix = A + "social_status" + A + ":{" });
            lstArg.Add(new dbArgument { Prefix = A + "value" + A + ":", ArgString = (h.SO??0).ToString(), Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "titles" + A + ":[]," });
            lstArg.Add(new dbArgument { Prefix = A + "nobility" + A + ":" + "null" , Suffix = "}," });

            lstArg.Add(new dbArgument { Prefix = A + "appearance" + A + ":{" });
            lstArg.Add(new dbArgument { Prefix = A + "height" + A + ":" + A + A, Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "weight" + A + ":" + A + A, Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "birthday" + A + ":" + A + A, Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "eye_colour" + A + ":" + A + A, Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "hair_colour" + A + ":" + A + A, Suffix = "," });

            string outcome = null;
            if (h.Notizen != null)
            {
                FlowDocument Document = System.Windows.Markup.XamlReader.Parse(h.Notizen) as FlowDocument;
                outcome = FlowDocument_GetText(Document)
                    .Replace(A.ToString(), ((char)92).ToString() + ((char)34).ToString())
                    .Replace(((char)9).ToString(), "  ")
                    .Replace("\r\n", ((char)92).ToString() + ((char)110).ToString());
            }
            lstArg.Add(new dbArgument { Prefix = A + "description" + A + ":" + A + outcome + A, Suffix = "}," });

            lstArg.Add(new dbArgument { Prefix = A + "equiped" + A + ":{" });
            lstArg.Add(new dbArgument { Prefix = A + "primaryHand" + A + ":" +A+ "null"+A, Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "secondaryHand" + A + ":" +A+ "null"+A, Suffix = "}}," });

            //Talent-Werte
            lstArg.Add(new dbArgument { Prefix = A + "owned" + A + ":{" });
            List<DSA41_KämpferTalent> lstHeldT = new List<DSA41_KämpferTalent>();
            foreach (Held_Talent ht in h.Kampftalente)
            {
                DSA41_KämpferTalent heldT = new DSA41_KämpferTalent();
                heldT.ht = ht;
                heldT.GMid = GMid;
                heldT.USERid = Userid;
                lstHeldT.Add(heldT);
                lstArg.Add(new dbArgument { Prefix = heldT.GetShortInfo() });
            }

            foreach (Held_Talent ht in h.Held_Talent.Where(t => t.Talent.Talentgruppe.Kurzname != "Kampf"))
            {
                DSA41_KämpferTalent heldT = new DSA41_KämpferTalent();
                heldT.ht = ht;
                heldT.GMid = GMid;
                heldT.USERid = Userid;
                lstHeldT.Add(heldT);

                lstArg.Add(new dbArgument { 
                    Prefix = A + heldT._id.ToString().Substring(19, 17).Replace("-", "") + A + ":{", 
                    ArgString= A + "data" + A + ":{" + A + "value" + A + ":"+ht.TaW, 
                    Suffix="}},"
                });
            }
            lstArg.Last().Suffix = lstArg.Last().Suffix.TrimEnd(new Char[] { ',' });
            lstArg.Add(new dbArgument { Prefix = "",Suffix="}}," });
            //ENDE Talent-werte

            lstArg.Add(new dbArgument { Prefix = A + "token" + A + ":{" });
            lstArg.Add(new dbArgument { Prefix = A + "flags" + A + ":{", Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "name" + A + ":\"", ArgString = h.Name, Suffix = A + "," });
            lstArg.Add(new dbArgument { Prefix = A + "displayName" + A + ":", ArgString = "0", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "img" + A + ":\"", ArgString = (string.IsNullOrEmpty(GetFilenameToken) ? "icons/svg/mystery-man.svg" : GetFilenameToken), Suffix = A + "," });
            lstArg.Add(new dbArgument { Prefix = A + "tint" + A + ":", ArgString = "null", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "width" + A + ":", ArgString = "1", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "height" + A + ":", ArgString = "1", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "scale" + A + ":", ArgString = h.TokenOversize == null?"1":Math.Round(h.TokenOversize.Value, 1).ToString(), Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "lockRotation" + A + ":", ArgString = "false", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "rotation" + A + ":", ArgString = "0", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "vision" + A + ":", ArgString = "true", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "dimSight" + A + ":", ArgString = "5", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "brightSight" + A + ":", ArgString = "7", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "dimLight" + A + ":", ArgString = "0", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "brightLight" + A + ":", ArgString = "0", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "sightAngle" + A + ":", ArgString = "360", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "lightAngle" + A + ":", ArgString = "0", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "lightAlpha" + A + ":", ArgString = "1", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "lightAnimation" + A + ":{" });
            lstArg.Add(new dbArgument { Prefix = A + "speed" + A + ":", ArgString = "5", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "intensity" + A + ":", ArgString = "5", Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "actorId" + A + ":", ArgString = A+id+A, Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "actorLink" + A + ":", ArgString = "false", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "disposition" + A + ":", ArgString = "-1", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "displayBars" + A + ":", ArgString = "0", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "bar1" + A + ":{", ArgString = A + "attribute" + A + ":" + A + "base.resources.vitality" + A, Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "bar2" + A + ":{", ArgString = A + "attribute" + A + ":" + A + "base.resources.astralEnergy" + A, Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "randomImg" + A + ":", ArgString = "false", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "mirrorX" + A + ":", ArgString = "false", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "mirrorY" + A + ":", ArgString = "false", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "alpha" + A + ":", ArgString = "1", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "light" + A + ":{" });
            lstArg.Add(new dbArgument { Prefix = A + "alpha" + A + ":", ArgString = "0.5", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "angle" + A + ":", ArgString = "0", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "bright" + A + ":", ArgString = "0", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "coloration" + A + ":", ArgString = "1", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "dim" + A + ":", ArgString = "0", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "gradual" + A + ":", ArgString = "true", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "luminosity" + A + ":", ArgString = "0.5", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "saturation" + A + ":", ArgString = "0", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "contrast" + A + ":", ArgString = "0", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "shadows" + A + ":", ArgString = "0", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "animation" + A + ":{" });
            lstArg.Add(new dbArgument { Prefix = A + "speed" + A + ":", ArgString = "5", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "intensity" + A + ":", ArgString = "5", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "reverse" + A + ":", ArgString = "false", Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "darkness" + A + ":{", ArgString =A+ "min"+A+":0,"+A+"max"+A+":1", Suffix = "}}}," });

            lstArg.Add(new dbArgument { Prefix = A + "items" + A + ":[" });
            //Talente
            int no = 1;

            foreach (DSA41_KämpferTalent heldT in lstHeldT)
            {
                if (heldT.isKampfTalent)
                    lstArg.Add(new dbArgument { Prefix = heldT.GetLongInfo(no) }); 
                else
                {
                    lstArg.Add(new dbArgument { Prefix = heldT.GetLongInfoTalent(no) });
                }
                no++;
            }
            foreach (Held_Sonderfertigkeit heldSF in h.Held_Sonderfertigkeit)
            {
                DSA41_KämpferTalent heldT = new DSA41_KämpferTalent();
                heldT.hsf = heldSF;
                heldT.GMid = GMid;
                heldT.USERid = Userid;
                lstArg.Add(new dbArgument { Prefix = heldT.GetLongInfoSF() });
            }
            foreach (Held_VorNachteil heldVN in h.Held_VorNachteil)
            {
                DSA41_KämpferTalent heldT = new DSA41_KämpferTalent();
                heldT.hvn = heldVN;
                heldT.GMid = GMid;
                heldT.USERid = Userid;
                lstArg.Add(new dbArgument { Prefix = heldT.GetLongInfoVN() });
            }
            foreach (Held_Zauber heldZ in h.Held_Zauber)
            {
                DSA41_KämpferTalent heldT = new DSA41_KämpferTalent();
                heldT.hz = heldZ;
                heldT.GMid = GMid;
                heldT.USERid = Userid;
                lstArg.Add(new dbArgument { Prefix = heldT.GetLongInfoZauber(no) });
                no++;
            }

            foreach (Held_Ausrüstung heldA in h.Held_Ausrüstung)
            {
                DSA41_KämpferTalent heldT = new DSA41_KämpferTalent();
                heldT.ha = heldA;
                heldT.GMid = GMid;
                heldT.USERid = Userid;
                lstArg.Add(new dbArgument { Prefix = heldT.GetLongInfoAusrüstung(no, null) });
                no++;
            }

            foreach (Held_Inventar heldI in h.Held_Inventar)
            {
                DSA41_KämpferTalent heldT = new DSA41_KämpferTalent();
                heldT.hi = heldI;
                heldT.GMid = GMid;
                heldT.USERid = Userid;
                lstArg.Add(new dbArgument { Prefix = heldT.GetLongInfoInventar(no) });
                no++;
            }

            lstArg.Last().Prefix = lstArg.Last().Prefix.TrimEnd(new Char[] { ',' });
            //ENDE Talente
            lstArg.Add(new dbArgument { Prefix ="",Suffix="]," });
            lstArg.Add(new dbArgument { Prefix = A + "effects" + A + ":", ArgString = "[]", Suffix = "," });
            if (SelectedHeldenFolder == null)
                lstArg.Add(new dbArgument { Prefix = A + "folder" + A + ":", ArgString = "null", Suffix = "," });
            else
                lstArg.Add(new dbArgument { Prefix = A + "folder" + A + ":", ArgString =A+ SelectedHeldenFolder._id+A, Suffix = "," });   //welches Verzeichnis 
            lstArg.Add(new dbArgument { Prefix = A + "sort" + A + ":", ArgString = (100000+AnzChr).ToString(), Suffix = "," });

            lstArg.Add(new dbArgument { Prefix = A + "permission" + A + ":{", ArgString = 
                A + "default" + A + ":0," +
                A + GMid + A + ":3," +
                A + Userid + A + ":3", Suffix = "}," });  //wer darf editieren
            lstArg.Add(new dbArgument { Prefix = A + "flags" + A + ":{",ArgString=A+"core"+A+":{"+A+ "sheetClass"+A+":"+A+ "dsa-4.1.DsaFourOneActorSheet" +A+"}", Suffix = "}}" });


            return lstArg;
        }


        private List<dbArgument> AddGegnerArgument(string eigenschaft, string color, int? wert, string lastEigenschaft = ",")
        {
            List<dbArgument> addArg = new List<dbArgument>();
            char A = (char)34;

            addArg.Add(new dbArgument { Prefix = A + eigenschaft + A + ":{" });
            addArg.Add(new dbArgument { Prefix = A + "label" + A + ":" + A + "CHAR." + eigenschaft.ToUpper() + A, Suffix = "," });
            addArg.Add(new dbArgument { Prefix = A + "abrev" + A + ":" + A + "CHARAbbrev." + eigenschaft.ToUpper() + A, Suffix = "," });
            addArg.Add(new dbArgument { Prefix = A + "color" + A + ":" + A + color + A, Suffix = "," });
            addArg.Add(new dbArgument { Prefix = A + "initial" + A + ":" + (wert ?? 0), Suffix = "," });
            addArg.Add(new dbArgument { Prefix = A + "species" + A + ":0", Suffix = "," });
            addArg.Add(new dbArgument { Prefix = A + "modifier" + A + ":0", Suffix = "," });
            addArg.Add(new dbArgument { Prefix = A + "advances" + A + ":0", Suffix = "," });
            addArg.Add(new dbArgument { Prefix = A + "value" + A + ":" + (wert ?? 0).ToString(), Suffix = "," });
            addArg.Add(new dbArgument { Prefix = A + "bonus" + A + ":0", Suffix = "," });
            addArg.Add(new dbArgument { Prefix = A + "cost" + A + ":" + A + A, Suffix = "," });
            addArg.Add(new dbArgument { Prefix = A + "refund" + A + ":" + A + A, Suffix = "}" + lastEigenschaft });
            return addArg;
        }

        private List<dbArgument> AddGegnerStatus(string status, string Iinitial, string Ivalue, string Iadvances, string Imodifier, string Icurrent, string last = ",")
        {
            List<dbArgument> addArg = new List<dbArgument>();
            char A = (char)34;

            addArg.Add(new dbArgument { Prefix = A + status + A + ":{" });
            if (Iinitial != null)
                addArg.Add(new dbArgument { Prefix = A + "initial" + A + ":", ArgString = Iinitial, Suffix = "," });
            if (Ivalue != null && status != "speed")
                addArg.Add(new dbArgument { Prefix = A + "value" + A + ":", ArgString = Ivalue, Suffix = "," });
            if (Iadvances != null)
                addArg.Add(new dbArgument { Prefix = A + "advances" + A + ":", ArgString = Iadvances, Suffix = "," });
            if (Imodifier != null)
                addArg.Add(new dbArgument { Prefix = A + "modifier" + A + ":", ArgString = Imodifier, Suffix = "," });
            if (Ivalue != null && status == "speed")
                addArg.Add(new dbArgument { Prefix = A + "value" + A + ":", ArgString = Ivalue, Suffix = "," });
            if (Icurrent != null)
                addArg.Add(new dbArgument { Prefix = A + "current" + A + ":", ArgString = Icurrent, Suffix = "," });

            addArg.Add(new dbArgument { Prefix = A + "gearmodifier" + A + ":", ArgString = "0", Suffix = "," });
            addArg.Add(new dbArgument { Prefix = A + "max" + A + ":", ArgString = Icurrent ?? Ivalue ?? "0", Suffix = "}" });

            addArg.Last().Suffix = last;
            return addArg;
        }

        private List<dbArgument> AddStats(string stat, int? Svalue = null, int? Smin = null, int? Smax = null, string last = ",")
        {
            List<dbArgument> addArg = new List<dbArgument>();
            char A = (char)34;

            addArg.Add(new dbArgument { Prefix = A + stat + A + ":{" });
            if (Svalue != null)
                addArg.Add(new dbArgument { Prefix = A + "value" + A + ":", ArgString = Svalue.ToString(), Suffix = "," });
            if (Smin != null)
                addArg.Add(new dbArgument { Prefix = A + "min" + A + ":", ArgString = Smin.ToString(), Suffix = "," });
            if (Smax != null)
                addArg.Add(new dbArgument { Prefix = A + "max" + A + ":", ArgString = Smax.ToString(), Suffix = "}," });
            if (last != ",")
                addArg.Last().Suffix = last;
            return addArg;
        }

        private List<dbArgument> AddAttributes(string attrib, int? Astart = null, int? Amod = null, int? Acurrent = null, string last = ",")
        {
            List<dbArgument> addArg = new List<dbArgument>();
            char A = (char)34;

            addArg.Add(new dbArgument { Prefix = A + attrib + A + ":{" });
            addArg.Add(new dbArgument { Prefix = A + "start" + A + ":", ArgString = (Astart ?? 0).ToString(), Suffix = "," });
            addArg.Add(new dbArgument { Prefix = A + "mod" + A + ":", ArgString = (Amod ?? 0).ToString(), Suffix = "," });
            addArg.Add(new dbArgument { Prefix = A + "current" + A + ":", ArgString = (Acurrent ?? 0).ToString(), Suffix = "}," });

            if (last != ",")
                addArg.Last().Suffix = last;
            return addArg;
        }
        private List<dbArgument> AddZauberArgument(GegnerBase g)
        {
            List<dbArgument> addArg = new List<dbArgument>();
            char A = (char)34;

            if (g.GegnerBase_Zauber.Count == 0)
                return addArg;

            g.GegnerBase_Zauber.ToList().ForEach(delegate (GegnerBase_Zauber gz)
            {
                string atGuid = Guid.NewGuid().ToString().Substring(19, 17).Replace("-", "");
                addArg.Add(new dbArgument { Prefix = "{" + A + "_id" + A + ":\"", ArgString = atGuid, Suffix = A + "," });
                addArg.Add(new dbArgument { Prefix = A + "name" + A + ":\"", ArgString = gz.Zauber.Name, Suffix = A + "," });
                addArg.Add(new dbArgument { Prefix = A + "type" + A + ":\"", ArgString = "spell", Suffix = A + "," });
                addArg.Add(new dbArgument { Prefix = A + "data" + A + ":{" });
                addArg.Add(new dbArgument { Prefix = A + "description" + A + ":{\"", ArgString = "value" + A + ":" + A + gz.Bemerkung, Suffix = A + "}," });
                addArg.Add(new dbArgument { Prefix = A + "gmdescription" + A + ":{\"", ArgString = "value" + A + ":" + A, Suffix = A + "}," });
                addArg.Add(new dbArgument { Prefix = A + "characteristic1" + A + ":{\"", ArgString = "value" + A + ":" + A + gz.Zauber.Eigenschaft1.ToLower(), Suffix = A + "}," });
                addArg.Add(new dbArgument { Prefix = A + "characteristic2" + A + ":{\"", ArgString = "value" + A + ":" + A + gz.Zauber.Eigenschaft2.ToLower(), Suffix = A + "}," });
                addArg.Add(new dbArgument { Prefix = A + "characteristic3" + A + ":{\"", ArgString = "value" + A + ":" + A + gz.Zauber.Eigenschaft3.ToLower(), Suffix = A + "}," });
                addArg.Add(new dbArgument { Prefix = A + "effect" + A + ":{\"", ArgString = "value" + A + ":" + A, Suffix = A + "}," });
                addArg.Add(new dbArgument { Prefix = A + "castingTime" + A + ":{\"", ArgString = "value" + A + ":" + A + gz.Zauber.Zauberdauer, Suffix = A + "}," });
                addArg.Add(new dbArgument { Prefix = A + "AsPCost" + A + ":{\"", ArgString = "value" + A + ":" + A + gz.Zauber.AsPKosten, Suffix = A + "}," });
                addArg.Add(new dbArgument { Prefix = A + "AsPCostDetail" + A + ":{\"", ArgString = "value" + A + ":" + A + gz.Zauber.Kosten, Suffix = A + "}," });
                addArg.Add(new dbArgument { Prefix = A + "maintainCost" + A + ":{\"", ArgString = "value" + A + ":" + A + "0", Suffix = A + "}," });
                addArg.Add(new dbArgument { Prefix = A + "distribution" + A + ":{\"", ArgString = "value" + A + ":" + A + gz.Zauber.Repräsentationen, Suffix = A + "}," });
                //Spalte F gibt es in DSA 5 nicht
                addArg.Add(new dbArgument
                {
                    Prefix = A + "StF" + A + ":{\"",
                    ArgString = "value" + A + ":" + A +
                    (gz.Zauber.Komplex != "F" ? gz.Zauber.Komplex : "E"),
                    Suffix = A + "},"
                });
                addArg.Add(new dbArgument { Prefix = A + "StF" + A + ":{\"", ArgString = "value" + A + ":" + A + gz.Zauber.Komplex, Suffix = A + "}," });
                addArg.Add(new dbArgument { Prefix = A + "resistanceModifier" + A + ":{\"", ArgString = "value" + A + ":" + A + "SK", Suffix = A + "}," });
                addArg.Add(new dbArgument { Prefix = A + "canChangeCastingTime" + A + ":{\"", ArgString = "value" + A + ":" + A + "true", Suffix = A + "}," });
                addArg.Add(new dbArgument { Prefix = A + "canChangeCost" + A + ":{\"", ArgString = "value" + A + ":" + A + "true", Suffix = A + "}," });
                addArg.Add(new dbArgument { Prefix = A + "canChangeRange" + A + ":{\"", ArgString = "value" + A + ":" + A + "true", Suffix = A + "}," });
                addArg.Add(new dbArgument { Prefix = A + "effectFormula" + A + ":{\"", ArgString = "value" + A + ":" + A, Suffix = A + "}," });
                addArg.Add(new dbArgument { Prefix = A + "range" + A + ":{\"", ArgString = "value" + A + ":" + A + gz.Zauber.Reichweite, Suffix = A + "}," });
                addArg.Add(new dbArgument { Prefix = A + "duration" + A + ":{\"", ArgString = "value" + A + ":" + A + gz.Zauber.Wirkungsdauer, Suffix = A + "}," });
                addArg.Add(new dbArgument { Prefix = A + "targetCategory" + A + ":{\"", ArgString = "value" + A + ":" + A + gz.Zauber.Zieleinheit, Suffix = A + "}," });
                addArg.Add(new dbArgument { Prefix = A + "talentValue" + A + ":{\"", ArgString = "value" + A + ":" + A + (gz.ZfW ?? 0).ToString(), Suffix = A + "}," });
                addArg.Add(new dbArgument { Prefix = A + "feature" + A + ":{\"", ArgString = "value" + A + ":" + A, Suffix = A + "}}," });

                addArg.Add(new dbArgument { Prefix = A + "flags" + A + ":{", Suffix = "}," });
                addArg.Add(new dbArgument { Prefix = A + "img" + A + ":", ArgString = A + "systems/dsa5/icons/categories/Spell.webp", Suffix = A + "," });
                addArg.Add(new dbArgument { Prefix = A + "effect" + A + ":", ArgString = "[]" });
                addArg.Add(new dbArgument { Suffix = "}," });
            });

            return addArg;
        }
        private List<dbArgument> AddZauberArgument(Model.Held h)
        {
            List<dbArgument> addArg = new List<dbArgument>();
            char A = (char)34;

            if (h.Held_Zauber.Count == 0)
                return addArg;

            h.Held_Zauber.ToList().ForEach(delegate (Held_Zauber hz)
            {
                string atGuid = Guid.NewGuid().ToString().Substring(19, 17).Replace("-", "");
                addArg.Add(new dbArgument { Prefix = "{" + A + "_id" + A + ":\"", ArgString = atGuid, Suffix = A + "," });
                addArg.Add(new dbArgument { Prefix = A + "name" + A + ":\"", ArgString = hz.Zauber.Name, Suffix = A + "," });
                addArg.Add(new dbArgument { Prefix = A + "type" + A + ":\"", ArgString = "spell", Suffix = A + "," });
                addArg.Add(new dbArgument { Prefix = A + "data" + A + ":{" });
                addArg.Add(new dbArgument { Prefix = A + "description" + A + ":{\"", ArgString = "value" + A + ":" + A + hz.Zauber.Bemerkung, Suffix = A + "}," });
                addArg.Add(new dbArgument { Prefix = A + "gmdescription" + A + ":{\"", ArgString = "value" + A + ":" + A, Suffix = A + "}," });
                addArg.Add(new dbArgument { Prefix = A + "characteristic1" + A + ":{\"", ArgString = "value" + A + ":" + A + hz.Zauber.Eigenschaft1.ToLower(), Suffix = A + "}," });
                addArg.Add(new dbArgument { Prefix = A + "characteristic2" + A + ":{\"", ArgString = "value" + A + ":" + A + hz.Zauber.Eigenschaft2.ToLower(), Suffix = A + "}," });
                addArg.Add(new dbArgument { Prefix = A + "characteristic3" + A + ":{\"", ArgString = "value" + A + ":" + A + hz.Zauber.Eigenschaft3.ToLower(), Suffix = A + "}," });
                addArg.Add(new dbArgument { Prefix = A + "effect" + A + ":{\"", ArgString = "value" + A + ":" + A, Suffix = A + "}," });
                addArg.Add(new dbArgument { Prefix = A + "castingTime" + A + ":{\"", ArgString = "value" + A + ":" + A + hz.Zauber.Zauberdauer, Suffix = A + "}," });
                addArg.Add(new dbArgument { Prefix = A + "AsPCost" + A + ":{\"", ArgString = "value" + A + ":" + A + hz.Zauber.AsPKosten, Suffix = A + "}," });
                addArg.Add(new dbArgument { Prefix = A + "AsPCostDetail" + A + ":{\"", ArgString = "value" + A + ":" + A + hz.Zauber.Kosten, Suffix = A + "}," });
                addArg.Add(new dbArgument { Prefix = A + "maintainCost" + A + ":{\"", ArgString = "value" + A + ":" + A + "0", Suffix = A + "}," });
                addArg.Add(new dbArgument { Prefix = A + "distribution" + A + ":{\"", ArgString = "value" + A + ":" + A + hz.Zauber.Repräsentationen, Suffix = A + "}," });
                //Spalte F gibt es in DSA 5 nicht
                addArg.Add(new dbArgument
                {
                    Prefix = A + "StF" + A + ":{\"",
                    ArgString = "value" + A + ":" + A +
                    (hz.Zauber.Komplex != "F" ? hz.Zauber.Komplex : "E"),
                    Suffix = A + "},"
                });
                addArg.Add(new dbArgument { Prefix = A + "resistanceModifier" + A + ":{\"", ArgString = "value" + A + ":" + A + "SK", Suffix = A + "}," });
                addArg.Add(new dbArgument { Prefix = A + "canChangeCastingTime" + A + ":{\"", ArgString = "value" + A + ":" + A + "true", Suffix = A + "}," });
                addArg.Add(new dbArgument { Prefix = A + "canChangeCost" + A + ":{\"", ArgString = "value" + A + ":" + A + "true", Suffix = A + "}," });
                addArg.Add(new dbArgument { Prefix = A + "canChangeRange" + A + ":{\"", ArgString = "value" + A + ":" + A + "true", Suffix = A + "}," });
                addArg.Add(new dbArgument { Prefix = A + "effectFormula" + A + ":{\"", ArgString = "value" + A + ":" + A, Suffix = A + "}," });
                addArg.Add(new dbArgument { Prefix = A + "range" + A + ":{\"", ArgString = "value" + A + ":" + A + hz.Zauber.Reichweite, Suffix = A + "}," });
                addArg.Add(new dbArgument { Prefix = A + "duration" + A + ":{\"", ArgString = "value" + A + ":" + A + hz.Zauber.Wirkungsdauer, Suffix = A + "}," });
                addArg.Add(new dbArgument { Prefix = A + "targetCategory" + A + ":{\"", ArgString = "value" + A + ":" + A + hz.Zauber.Zieleinheit, Suffix = A + "}," });
                addArg.Add(new dbArgument { Prefix = A + "talentValue" + A + ":{\"", ArgString = "value" + A + ":" + A + (hz.ZfW ?? 0).ToString(), Suffix = A + "}," });
                addArg.Add(new dbArgument { Prefix = A + "feature" + A + ":{\"", ArgString = "value" + A + ":" + A, Suffix = A + "}}," });

                addArg.Add(new dbArgument { Prefix = A + "flags" + A + ":{", Suffix = "}," });
                addArg.Add(new dbArgument { Prefix = A + "img" + A + ":", ArgString = A + "systems/dsa5/icons/categories/Spell.webp", Suffix = A + "," });
                addArg.Add(new dbArgument { Prefix = A + "effect" + A + ":", ArgString = "[]" });
                addArg.Add(new dbArgument { Suffix = "}," });
            });

            return addArg;
        }
        private List<dbArgument> AddAngriffeArgument(GegnerBase g)
        {
            List<dbArgument> addArg = new List<dbArgument>();
            char A = (char)34;
            if (g.GegnerBase_Angriff.Count > 0)
            {
                g.GegnerBase_Angriff.ToList().ForEach(delegate (GegnerBase_Angriff ga)
                {
                    string atGuid = Guid.NewGuid().ToString().Substring(19, 17).Replace("-", "");
                    addArg.Add(new dbArgument { Prefix = "{" + A + "_id" + A + ":\"", ArgString = atGuid, Suffix = A + "," });
                    addArg.Add(new dbArgument { Prefix = A + "name" + A + ":\"", ArgString = ga.Name, Suffix = A + "," });
                    addArg.Add(new dbArgument { Prefix = A + "type" + A + ":\"", ArgString = "trait", Suffix = A + "," });
                    addArg.Add(new dbArgument { Prefix = A + "data" + A + ":{" });
                    addArg.Add(new dbArgument { Prefix = A + "description" + A + ":{\"", ArgString = "value" + A + ":" + A + "Natürliche Waffe", Suffix = A + "}," });
                    addArg.Add(new dbArgument { Prefix = A + "gmdescription" + A + ":{\"", ArgString = "value" + A + ":" + A, Suffix = A + "}," });
                    addArg.Add(new dbArgument { Prefix = A + "APValue" + A + ":{\"", ArgString = "value" + A + ":" + A + "0", Suffix = A + "}," });
                    addArg.Add(new dbArgument { Prefix = A + "traitType" + A + ":{\"", ArgString = "value" + A + ":" + A + "meleeAttack", Suffix = A + "}," });
                    addArg.Add(new dbArgument { Prefix = A + "at" + A + ":{\"", ArgString = "value" + A + ":" + A + ga.AT, Suffix = A + "}," });
                    addArg.Add(new dbArgument { Prefix = A + "pa" + A + ":{\"", ArgString = "value" + A + ":" + A + ga.PA, Suffix = A + "}," });
                    string dk = ga.DK == null ? "medium" : ga.DK.Contains("H") ? "short" : ga.DK.Contains("N") ? "medium" : ga.DK.Contains("S") ? "long" : ga.DK.Contains("P") ? "wide" : "medium";
                    addArg.Add(new dbArgument { Prefix = A + "reach" + A + ":{\"", ArgString = "value" + A + ":" + A + dk, Suffix = A + "}," });
                    addArg.Add(new dbArgument { Prefix = A + "damage" + A + ":{\"", ArgString = "value" + A + ":" + A + ga.TPWürfelAnzahl + "W" + ga.TPWürfel + "+" + ga.TPBonus, Suffix = A + "}," });
                    addArg.Add(new dbArgument { Prefix = A + "reloadTime" + A + ":{\"", ArgString = "value" + A + ":" + A + "1", Suffix = A + "}," });
                    addArg.Add(new dbArgument { Prefix = A + "AsPCost" + A + ":{\"", ArgString = "value" + A + ":" + A + "0", Suffix = A + "}," });
                    addArg.Add(new dbArgument { Prefix = A + "duration" + A + ":{\"", ArgString = "value" + A + ":" + A, Suffix = A + "}," });
                    addArg.Add(new dbArgument { Prefix = A + "aspect" + A + ":{\"", ArgString = "value" + A + ":" + A, Suffix = A + "}," });
                    addArg.Add(new dbArgument { Prefix = A + "effect" + A + ":{\"", ArgString = "value" + A + ":" + A, Suffix = A + "}}," });

                    addArg.Add(new dbArgument { Prefix = A + "flags" + A + ":{", Suffix = "}," });
                    addArg.Add(new dbArgument { Prefix = A + "img" + A + ":", ArgString = A + "systems/dsa5/icons/categories/trait.webp", Suffix = A + "," });
                    addArg.Add(new dbArgument { Prefix = A + "effect" + A + ":", ArgString = "[]" });
                    addArg.Add(new dbArgument { Suffix = "}," });
                });
            }
            else
            {
                if (g.AT != 0)
                {
                    string atGuid = Guid.NewGuid().ToString().Substring(19, 17).Replace("-", "");
                    addArg.Add(new dbArgument { Prefix = "{" + A + "_id" + A + ":\"", ArgString = atGuid, Suffix = A + "," });
                    addArg.Add(new dbArgument { Prefix = A + "name" + A + ":\"", ArgString = "Nahkampf", Suffix = A + "," });
                    addArg.Add(new dbArgument { Prefix = A + "type" + A + ":\"", ArgString = "trait", Suffix = A + "," });
                    addArg.Add(new dbArgument { Prefix = A + "data" + A + ":{" });
                    addArg.Add(new dbArgument { Prefix = A + "description" + A + ":{\"", ArgString = "value" + A + ":" + A + "Natürliche Waffe", Suffix = A + "}," });
                    addArg.Add(new dbArgument { Prefix = A + "gmdescription" + A + ":{\"", ArgString = "value" + A + ":" + A, Suffix = A + "}," });
                    addArg.Add(new dbArgument { Prefix = A + "APValue" + A + ":{\"", ArgString = "value" + A + ":" + A + "0", Suffix = A + "}," });
                    addArg.Add(new dbArgument { Prefix = A + "traitType" + A + ":{\"", ArgString = "value" + A + ":" + A + "meleeAttack", Suffix = A + "}," });
                    addArg.Add(new dbArgument { Prefix = A + "at" + A + ":{\"", ArgString = "value" + A + ":" + A + g.AT, Suffix = A + "}," });
                    addArg.Add(new dbArgument { Prefix = A + "reach" + A + ":{\"", ArgString = "value" + A + ":" + A + "medium", Suffix = A + "}," });
                    addArg.Add(new dbArgument { Prefix = A + "damage" + A + ":{\"", ArgString = "value" + A + ":" + A + "1W6", Suffix = A + "}," });
                    addArg.Add(new dbArgument { Prefix = A + "reloadTime" + A + ":{\"", ArgString = "value" + A + ":" + A + "1", Suffix = A + "}," });
                    addArg.Add(new dbArgument { Prefix = A + "AsPCost" + A + ":{\"", ArgString = "value" + A + ":" + A + "0", Suffix = A + "}," });
                    addArg.Add(new dbArgument { Prefix = A + "duration" + A + ":{\"", ArgString = "value" + A + ":" + A, Suffix = A + "}," });
                    addArg.Add(new dbArgument { Prefix = A + "aspect" + A + ":{\"", ArgString = "value" + A + ":" + A, Suffix = A + "}," });
                    addArg.Add(new dbArgument { Prefix = A + "effect" + A + ":{\"", ArgString = "value" + A + ":" + A, Suffix = A + "}}," });

                    addArg.Add(new dbArgument { Prefix = A + "flags" + A + ":{", Suffix = "}," });
                    addArg.Add(new dbArgument { Prefix = A + "img" + A + ":", ArgString = A + "systems/dsa5/icons/categories/trait.webp", Suffix = A + "," });
                    addArg.Add(new dbArgument { Prefix = A + "effect" + A + ":", ArgString = "[]" });
                    addArg.Add(new dbArgument { Suffix = "}," });
                }
                if (g.FK != 0)
                {
                    string atGuid = Guid.NewGuid().ToString().Substring(19, 17).Replace("-", "");
                    addArg.Add(new dbArgument { Prefix = "{" + A + "_id" + A + ":\"", ArgString = atGuid, Suffix = A + "," });
                    addArg.Add(new dbArgument { Prefix = A + "name" + A + ":\"", ArgString = "Fernkampf", Suffix = A + "," });
                    addArg.Add(new dbArgument { Prefix = A + "type" + A + ":\"", ArgString = "trait", Suffix = A + "," });
                    addArg.Add(new dbArgument { Prefix = A + "data" + A + ":{" });
                    addArg.Add(new dbArgument { Prefix = A + "description" + A + ":{\"", ArgString = "value" + A + ":" + A + "Natürliche Waffe", Suffix = A + "}," });
                    addArg.Add(new dbArgument { Prefix = A + "gmdescription" + A + ":{\"", ArgString = "value" + A + ":" + A, Suffix = A + "}," });
                    addArg.Add(new dbArgument { Prefix = A + "APValue" + A + ":{\"", ArgString = "value" + A + ":" + A + "0", Suffix = A + "}," });
                    addArg.Add(new dbArgument { Prefix = A + "traitType" + A + ":{\"", ArgString = "value" + A + ":" + A + "meleeAttack", Suffix = A + "}," });
                    addArg.Add(new dbArgument { Prefix = A + "at" + A + ":{\"", ArgString = "value" + A + ":" + A + g.AT, Suffix = A + "}," });
                    addArg.Add(new dbArgument { Prefix = A + "reach" + A + ":{\"", ArgString = "value" + A + ":" + A + "medium", Suffix = A + "}," });
                    addArg.Add(new dbArgument { Prefix = A + "damage" + A + ":{\"", ArgString = "value" + A + ":" + A + "1W6", Suffix = A + "}," });
                    addArg.Add(new dbArgument { Prefix = A + "reloadTime" + A + ":{\"", ArgString = "value" + A + ":" + A + "1", Suffix = A + "}," });
                    addArg.Add(new dbArgument { Prefix = A + "AsPCost" + A + ":{\"", ArgString = "value" + A + ":" + A + "0", Suffix = A + "}," });
                    addArg.Add(new dbArgument { Prefix = A + "duration" + A + ":{\"", ArgString = "value" + A + ":" + A, Suffix = A + "}," });
                    addArg.Add(new dbArgument { Prefix = A + "aspect" + A + ":{\"", ArgString = "value" + A + ":" + A, Suffix = A + "}," });
                    addArg.Add(new dbArgument { Prefix = A + "effect" + A + ":{\"", ArgString = "value" + A + ":" + A, Suffix = A + "}}," });

                    addArg.Add(new dbArgument { Prefix = A + "flags" + A + ":{", Suffix = "}," });
                    addArg.Add(new dbArgument { Prefix = A + "img" + A + ":", ArgString = A + "systems/dsa5/icons/categories/trait.webp", Suffix = A + "," });
                    addArg.Add(new dbArgument { Prefix = A + "effect" + A + ":", ArgString = "[]" });
                    addArg.Add(new dbArgument { Suffix = "}," });
                }
            }
            return addArg;
        }
        private List<dbArgument> AddAngriffeArgument(Held h)
        {

            List<dbArgument> addArg = new List<dbArgument>();
            char A = (char)34;

            if (h.Angriffswaffen.Count > 0)
            {
                h.Angriffswaffen.ToList().ForEach(delegate (Kampf.Logic.INahkampfwaffe nw)
                {
                    string atGuid = Guid.NewGuid().ToString().Substring(19, 17).Replace("-", "");
                    addArg.Add(new dbArgument { Prefix = "{" + A + "_id" + A + ":\"", ArgString = atGuid, Suffix = A + "," });
                    addArg.Add(new dbArgument { Prefix = A + "name" + A + ":\"", ArgString = nw.Name, Suffix = A + "," });
                    addArg.Add(new dbArgument { Prefix = A + "type" + A + ":\"", ArgString = "trait", Suffix = A + "," });
                    addArg.Add(new dbArgument { Prefix = A + "data" + A + ":{" });
                    addArg.Add(new dbArgument { Prefix = A + "description" + A + ":{\"", ArgString = "value" + A + ":" + A + nw.Name, Suffix = A + "}," });
                    addArg.Add(new dbArgument { Prefix = A + "gmdescription" + A + ":{\"", ArgString = "value" + A + ":" + A, Suffix = A + "}," });
                    addArg.Add(new dbArgument { Prefix = A + "APValue" + A + ":{\"", ArgString = "value" + A + ":" + A + "0", Suffix = A + "}," });
                    addArg.Add(new dbArgument { Prefix = A + "traitType" + A + ":{\"", ArgString = "value" + A + ":" + A + "meleeAttack", Suffix = A + "}," });
                    addArg.Add(new dbArgument { Prefix = A + "at" + A + ":{\"", ArgString = "value" + A + ":" + A + nw.AT, Suffix = A + "}," });
                    addArg.Add(new dbArgument { Prefix = A + "pa" + A + ":{\"", ArgString = "value" + A + ":" + A + nw.PA, Suffix = A + "}," });
                    string dk = nw.Distanzklasse == 0x0 ? "medium" :
                        nw.Distanzklasse.ToString().Contains("H") ? "short" :
                        nw.Distanzklasse.ToString().Contains("N") ? "medium" :
                        nw.Distanzklasse.ToString().Contains("S") ? "long" :
                        nw.Distanzklasse.ToString().Contains("P") ? "wide" : "medium";
                    addArg.Add(new dbArgument { Prefix = A + "reach" + A + ":{\"", ArgString = "value" + A + ":" + A + dk, Suffix = A + "}," });
                    addArg.Add(new dbArgument { Prefix = A + "damage" + A + ":{\"", ArgString = "value" + A + ":" + A + nw.TPWürfelAnzahl + "W" + nw.TPWürfel + "+" + nw.TPBonus, Suffix = A + "}," });
                    addArg.Add(new dbArgument { Prefix = A + "reloadTime" + A + ":{\"", ArgString = "value" + A + ":" + A + "1", Suffix = A + "}," });
                    addArg.Add(new dbArgument { Prefix = A + "AsPCost" + A + ":{\"", ArgString = "value" + A + ":" + A + "0", Suffix = A + "}," });
                    addArg.Add(new dbArgument { Prefix = A + "duration" + A + ":{\"", ArgString = "value" + A + ":" + A, Suffix = A + "}," });
                    addArg.Add(new dbArgument { Prefix = A + "aspect" + A + ":{\"", ArgString = "value" + A + ":" + A, Suffix = A + "}," });
                    addArg.Add(new dbArgument { Prefix = A + "effect" + A + ":{\"", ArgString = "value" + A + ":" + A, Suffix = A + "}}," });

                    addArg.Add(new dbArgument { Prefix = A + "flags" + A + ":{", Suffix = "}," });
                    addArg.Add(new dbArgument { Prefix = A + "img" + A + ":", ArgString = A + "systems/dsa5/icons/categories/trait.webp", Suffix = A + "," });
                    addArg.Add(new dbArgument { Prefix = A + "effect" + A + ":", ArgString = "[]" });
                    addArg.Add(new dbArgument { Suffix = "}," });
                });
            }

            return addArg;
        }

        private void AddKampfTalent(List<dbArgument> addArg, GegnerBase g, string dsa5name, string name, string StF, string guidevalue, int pa, int at, int TaW, string wtype)
        {
            char A = (char)34;
            string atGuid = Guid.NewGuid().ToString().Substring(19, 17).Replace("-", "");
            addArg.Add(new dbArgument { Prefix = "{" + A + "_id" + A + ":\"", ArgString = atGuid, Suffix = A + "," });
            addArg.Add(new dbArgument { Prefix = A + "name" + A + ":\"", ArgString = name, Suffix = A + "," });
            addArg.Add(new dbArgument { Prefix = A + "type" + A + ":\"", ArgString = "combatskill", Suffix = A + "," });
            addArg.Add(new dbArgument { Prefix = A + "data" + A + ":{" });
            addArg.Add(new dbArgument { Prefix = A + "StF" + A + ":{\"", ArgString = "value" + A + ":" + A + StF + A + "," + A + "type" + A + ":" + A + "String" + A + "," + A + "label" + A + ":" + A + "Advancementfactor", Suffix = A + "}," });
            addArg.Add(new dbArgument { Prefix = A + "guidevalue" + A + ":{\"", ArgString = "value" + A + ":" + A + guidevalue + A + "," + A + "type" + A + ":" + A + "String" + A + "," + A + "label" + A + ":" + A + "Guide value", Suffix = A + "}," });
            addArg.Add(new dbArgument { Prefix = A + "parry" + A + ":{\"", ArgString = "value" + A + ":" + g.PA + "," + A + "type" + A + ":" + A + "Number" + A + "," + A + "label" + A + ":" + A + "parryValue", Suffix = A + "}," });
            addArg.Add(new dbArgument { Prefix = A + "attack" + A + ":{\"", ArgString = "value" + A + ":" + g.AT + "," + A + "type" + A + ":" + A + "Number" + A + "," + A + "label" + A + ":" + A + "attackValue", Suffix = A + "}," });
            addArg.Add(new dbArgument { Prefix = A + "talentValue" + A + ":{\"", ArgString = "value" + A + ":0," + A + "type" + A + ":" + A + "Number" + A + "," + A + "label" + A + ":" + A + "attackValue", Suffix = A + "}," });
            addArg.Add(new dbArgument { Prefix = A + "weapontype" + A + ":{\"", ArgString = "value" + A + ":" + A + wtype + A + "," + A + "label" + A + ":" + A + "weapontype" + A + "," + A + "type" + A + ":" + A + "String", Suffix = A + "}," });
            addArg.Add(new dbArgument { Prefix = A + "description" + A + ":{\"", ArgString = "value" + A + ":" + A, Suffix = A + "}}," });

            addArg.Add(new dbArgument { Prefix = A + "flags" + A + ":{", Suffix = "}," });
            addArg.Add(new dbArgument { Prefix = A + "img" + A + ":", ArgString = A + "icons/environment/people/charge.webp", Suffix = A + "," });
            addArg.Add(new dbArgument { Prefix = A + "effect" + A + ":", ArgString = "[]" });
            addArg.Add(new dbArgument { Suffix = "}," });
        }
        private void AddKampfTalent(List<dbArgument> addArg, Held h, string dsa5name, string name, string StF, string guidevalue, int pa, int at, int TaW, string wtype)
        {
            char A = (char)34;
            Held_Talent ht = h.Kampftalente.FirstOrDefault(t => t.Talentname == name);
            if (ht == null)
                return;

            string atGuid = Guid.NewGuid().ToString().Substring(19, 17).Replace("-", "");
            addArg.Add(new dbArgument { Prefix = "{" + A + "_id" + A + ":\"", ArgString = atGuid, Suffix = A + "," });
            addArg.Add(new dbArgument { Prefix = A + "name" + A + ":\"", ArgString = dsa5name, Suffix = A + "," });
            addArg.Add(new dbArgument { Prefix = A + "type" + A + ":\"", ArgString = "combatskill", Suffix = A + "," });
            addArg.Add(new dbArgument { Prefix = A + "data" + A + ":{" });
            addArg.Add(new dbArgument { Prefix = A + "StF" + A + ":{\"", ArgString = "value" + A + ":" + A + StF + A + "," + A + "type" + A + ":" + A + "String" + A + "," + A + "label" + A + ":" + A + "Advancementfactor", Suffix = A + "}," });
            addArg.Add(new dbArgument { Prefix = A + "guidevalue" + A + ":{\"", ArgString = "value" + A + ":" + A + guidevalue + A + "," + A + "type" + A + ":" + A + "String" + A + "," + A + "label" + A + ":" + A + "Guide value", Suffix = A + "}," });
            addArg.Add(new dbArgument { Prefix = A + "parry" + A + ":{\"", ArgString = "value" + A + ":" + ht.Parade + "," + A + "type" + A + ":" + A + "Number" + A + "," + A + "label" + A + ":" + A + "parryValue", Suffix = A + "}," });
            addArg.Add(new dbArgument { Prefix = A + "attack" + A + ":{\"", ArgString = "value" + A + ":" + ht.Attacke + "," + A + "type" + A + ":" + A + "Number" + A + "," + A + "label" + A + ":" + A + "attackValue", Suffix = A + "}," });
            addArg.Add(new dbArgument { Prefix = A + "talentValue" + A + ":{\"", ArgString = "value" + A + ":" + ht.TaW + "," + A + "type" + A + ":" + A + "Number" + A + "," + A + "label" + A + ":" + A + "attackValue", Suffix = A + "}," });
            addArg.Add(new dbArgument { Prefix = A + "weapontype" + A + ":{\"", ArgString = "value" + A + ":" + A + wtype + A + "," + A + "label" + A + ":" + A + "weapontype" + A + "," + A + "type" + A + ":" + A + "String", Suffix = A + "}," });
            addArg.Add(new dbArgument { Prefix = A + "description" + A + ":{\"", ArgString = "value" + A + ":" + A, Suffix = A + "}}," });

            addArg.Add(new dbArgument { Prefix = A + "flags" + A + ":{", Suffix = "}," });
            addArg.Add(new dbArgument { Prefix = A + "img" + A + ":", ArgString = A + "icons/environment/people/charge.webp", Suffix = A + "," });
            addArg.Add(new dbArgument { Prefix = A + "effect" + A + ":", ArgString = "[]" });
            addArg.Add(new dbArgument { Suffix = "}," });
        }


        private List<dbArgument> AddKampftalenteArgument(GegnerBase g)
        {
            List<dbArgument> addArg = new List<dbArgument>();
            foreach (WaffeTalent w in lstWTalent)
            {
                AddKampfTalent(addArg, g, w.DSA5Gruppe, w.DSA4Gruppe, w.StF, w.guidvalue, g.PA, g.AT, 0, w.wtype);
            };
            return addArg;
        }

        private List<dbArgument> AddKampftalenteArgument(Held h)
        {
            List<dbArgument> addArg = new List<dbArgument>();
            foreach (WaffeTalent w in lstWTalent)
            {
                AddKampfTalent(addArg, h, w.DSA5Gruppe, w.DSA4Gruppe, w.StF, w.guidvalue, h.ParadeBasis, h.AttackeBasis, 0, w.wtype);
            };
            return addArg;
        }
        private List<dbArgument> AddMoneyArgument(int Du, int Si, int He, int Kr)
        {
            List<dbArgument> addArg = new List<dbArgument>();
            char A = (char)34;

            //Dukaten
            string atGuid = Guid.NewGuid().ToString().Substring(19, 17).Replace("-", "");
            addArg.Add(new dbArgument { Prefix = "{" + A + "_id" + A + ":\"", ArgString = atGuid, Suffix = A + "," });
            addArg.Add(new dbArgument { Prefix = A + "name" + A + ":\"", ArgString = "Money-D", Suffix = A + "," });
            addArg.Add(new dbArgument { Prefix = A + "type" + A + ":\"", ArgString = "money", Suffix = A + "," });

            addArg.Add(new dbArgument { Prefix = A + "data" + A + ":{" });
            addArg.Add(new dbArgument { Prefix = A + "description" + A + ":{\"", ArgString = "value" + A + ":" + A + A + "," + A + "type" + A + ":" + A + "String" + A + "," + A + "label" + A + ":" + A + "Description", Suffix = A + "}," });
            addArg.Add(new dbArgument { Prefix = A + "gmdescription" + A + ":{\"", ArgString = "value" + A + ":" + A + A + "," + A + "type" + A + ":" + A + "String" + A + "," + A + "label" + A + ":" + A + "Description", Suffix = A + "}," });
            addArg.Add(new dbArgument { Prefix = A + "price" + A + ":{\"", ArgString = "value" + A + ":" + Du + "," + A + "type" + A + ":" + A + "Number" + A + "," + A + "label" + A + ":" + A + "Price", Suffix = A + "}," });
            addArg.Add(new dbArgument { Prefix = A + "quantity" + A + ":{\"", ArgString = "value" + A + ":0," + A + "type" + A + ":" + A + "Number" + A + "," + A + "label" + A + ":" + A + "Quantity", Suffix = A + "}," });
            addArg.Add(new dbArgument { Prefix = A + "weight" + A + ":{\"", ArgString = "value" + A + ":0," + A + "type" + A + ":" + A + "Number" + A + "," + A + "label" + A + ":" + A + "weight", Suffix = A + "}," });
            addArg.Add(new dbArgument { Prefix = A + "effect" + A + ":{\"", ArgString = "value" + A + ":" + A + A + "," + A + "type" + A + ":" + A + "String" + A + "," + A + "label" + A + ":" + A + "Special effect", Suffix = A + "}}," });
            addArg.Add(new dbArgument { Prefix = A + "flags" + A + ":{\"", ArgString = "core" + A + ":{ " + A + "sourceId" + A + ":" + A + "Item.BGfRbsrZ2M2Y2wZ3", Suffix = A + "}}," });
            addArg.Add(new dbArgument { Prefix = A + "img" + A + ":", ArgString = A + "systems/dsa5/icons/money-D.webp", Suffix = A + "," });
            addArg.Add(new dbArgument { Prefix = A + "effect" + A + ":", ArgString = "[]" });
            addArg.Add(new dbArgument { Suffix = "}," });


            //Silber
            atGuid = Guid.NewGuid().ToString().Substring(19, 17).Replace("-", "");
            addArg.Add(new dbArgument { Prefix = "{" + A + "_id" + A + ":\"", ArgString = atGuid, Suffix = A + "," });
            addArg.Add(new dbArgument { Prefix = A + "name" + A + ":\"", ArgString = "Money-S", Suffix = A + "," });
            addArg.Add(new dbArgument { Prefix = A + "type" + A + ":\"", ArgString = "money", Suffix = A + "," });

            addArg.Add(new dbArgument { Prefix = A + "data" + A + ":{" });
            addArg.Add(new dbArgument { Prefix = A + "description" + A + ":{\"", ArgString = "value" + A + ":" + A + A + "," + A + "type" + A + ":" + A + "String" + A + "," + A + "label" + A + ":" + A + "Description", Suffix = A + "}," });
            addArg.Add(new dbArgument { Prefix = A + "gmdescription" + A + ":{\"", ArgString = "value" + A + ":" + A + A + "," + A + "type" + A + ":" + A + "String" + A + "," + A + "label" + A + ":" + A + "Description", Suffix = A + "}," });
            addArg.Add(new dbArgument { Prefix = A + "price" + A + ":{\"", ArgString = "value" + A + ":" + Si + "," + A + "type" + A + ":" + A + "Number" + A + "," + A + "label" + A + ":" + A + "Price", Suffix = A + "}," });
            addArg.Add(new dbArgument { Prefix = A + "quantity" + A + ":{\"", ArgString = "value" + A + ":0," + A + "type" + A + ":" + A + "Number" + A + "," + A + "label" + A + ":" + A + "Quantity", Suffix = A + "}," });
            addArg.Add(new dbArgument { Prefix = A + "weight" + A + ":{\"", ArgString = "value" + A + ":0," + A + "type" + A + ":" + A + "Number" + A + "," + A + "label" + A + ":" + A + "weight", Suffix = A + "}," });
            addArg.Add(new dbArgument { Prefix = A + "effect" + A + ":{\"", ArgString = "value" + A + ":" + A + A + "," + A + "type" + A + ":" + A + "String" + A + "," + A + "label" + A + ":" + A + "Special effect", Suffix = A + "}}," });
            addArg.Add(new dbArgument { Prefix = A + "flags" + A + ":{\"", ArgString = "core" + A + ":{ " + A + "sourceId" + A + ":" + A + "Item.ntX4yPa0e9rmmqSZ", Suffix = A + "}}," });
            addArg.Add(new dbArgument { Prefix = A + "img" + A + ":", ArgString = A + "systems/dsa5/icons/money-S.webp", Suffix = A + "," });
            addArg.Add(new dbArgument { Prefix = A + "effect" + A + ":", ArgString = "[]" });
            addArg.Add(new dbArgument { Suffix = "}," });


            //Heller
            atGuid = Guid.NewGuid().ToString().Substring(19, 17).Replace("-", "");
            addArg.Add(new dbArgument { Prefix = "{" + A + "_id" + A + ":\"", ArgString = atGuid, Suffix = A + "," });
            addArg.Add(new dbArgument { Prefix = A + "name" + A + ":\"", ArgString = "Money-H", Suffix = A + "," });
            addArg.Add(new dbArgument { Prefix = A + "type" + A + ":\"", ArgString = "money", Suffix = A + "," });

            addArg.Add(new dbArgument { Prefix = A + "data" + A + ":{" });
            addArg.Add(new dbArgument { Prefix = A + "description" + A + ":{\"", ArgString = "value" + A + ":" + A + A + "," + A + "type" + A + ":" + A + "String" + A + "," + A + "label" + A + ":" + A + "Description", Suffix = A + "}," });
            addArg.Add(new dbArgument { Prefix = A + "gmdescription" + A + ":{\"", ArgString = "value" + A + ":" + A + A + "," + A + "type" + A + ":" + A + "String" + A + "," + A + "label" + A + ":" + A + "Description", Suffix = A + "}," });
            addArg.Add(new dbArgument { Prefix = A + "price" + A + ":{\"", ArgString = "value" + A + ":" + He + "," + A + "type" + A + ":" + A + "Number" + A + "," + A + "label" + A + ":" + A + "Price", Suffix = A + "}," });
            addArg.Add(new dbArgument { Prefix = A + "quantity" + A + ":{\"", ArgString = "value" + A + ":0," + A + "type" + A + ":" + A + "Number" + A + "," + A + "label" + A + ":" + A + "Quantity", Suffix = A + "}," });
            addArg.Add(new dbArgument { Prefix = A + "weight" + A + ":{\"", ArgString = "value" + A + ":0," + A + "type" + A + ":" + A + "Number" + A + "," + A + "label" + A + ":" + A + "weight", Suffix = A + "}," });
            addArg.Add(new dbArgument { Prefix = A + "effect" + A + ":{\"", ArgString = "value" + A + ":" + A + A + "," + A + "type" + A + ":" + A + "String" + A + "," + A + "label" + A + ":" + A + "Special effect", Suffix = A + "}}," });
            addArg.Add(new dbArgument { Prefix = A + "flags" + A + ":{\"", ArgString = "core" + A + ":{ " + A + "sourceId" + A + ":" + A + "Item.2wwyflxgKCiz9UIQ", Suffix = A + "}}," });
            addArg.Add(new dbArgument { Prefix = A + "img" + A + ":", ArgString = A + "systems/dsa5/icons/money-H.webp", Suffix = A + "," });
            addArg.Add(new dbArgument { Prefix = A + "effect" + A + ":", ArgString = "[]" });
            addArg.Add(new dbArgument { Suffix = "}," });


            //Kreuzer
            atGuid = Guid.NewGuid().ToString().Substring(19, 17).Replace("-", "");
            addArg.Add(new dbArgument { Prefix = "{" + A + "_id" + A + ":\"", ArgString = atGuid, Suffix = A + "," });
            addArg.Add(new dbArgument { Prefix = A + "name" + A + ":\"", ArgString = "Money-K", Suffix = A + "," });
            addArg.Add(new dbArgument { Prefix = A + "type" + A + ":\"", ArgString = "money", Suffix = A + "," });

            addArg.Add(new dbArgument { Prefix = A + "data" + A + ":{" });
            addArg.Add(new dbArgument { Prefix = A + "description" + A + ":{\"", ArgString = "value" + A + ":" + A + A + "," + A + "type" + A + ":" + A + "String" + A + "," + A + "label" + A + ":" + A + "Description", Suffix = A + "}," });
            addArg.Add(new dbArgument { Prefix = A + "gmdescription" + A + ":{\"", ArgString = "value" + A + ":" + A + A + "," + A + "type" + A + ":" + A + "String" + A + "," + A + "label" + A + ":" + A + "Description", Suffix = A + "}," });
            addArg.Add(new dbArgument { Prefix = A + "price" + A + ":{\"", ArgString = "value" + A + ":" + Kr + "," + A + "type" + A + ":" + A + "Number" + A + "," + A + "label" + A + ":" + A + "Price", Suffix = A + "}," });
            addArg.Add(new dbArgument { Prefix = A + "quantity" + A + ":{\"", ArgString = "value" + A + ":0," + A + "type" + A + ":" + A + "Number" + A + "," + A + "label" + A + ":" + A + "Quantity", Suffix = A + "}," });
            addArg.Add(new dbArgument { Prefix = A + "weight" + A + ":{\"", ArgString = "value" + A + ":0," + A + "type" + A + ":" + A + "Number" + A + "," + A + "label" + A + ":" + A + "weight", Suffix = A + "}," });
            addArg.Add(new dbArgument { Prefix = A + "effect" + A + ":{\"", ArgString = "value" + A + ":" + A + A + "," + A + "type" + A + ":" + A + "String" + A + "," + A + "label" + A + ":" + A + "Special effect", Suffix = A + "}}," });
            addArg.Add(new dbArgument { Prefix = A + "flags" + A + ":{\"", ArgString = "core" + A + ":{ " + A + "sourceId" + A + ":" + A + "Item.IHSzojKYEn0FloQm", Suffix = A + "}}," });
            addArg.Add(new dbArgument { Prefix = A + "img" + A + ":", ArgString = A + "systems/dsa5/icons/money-K.webp", Suffix = A + "," });
            addArg.Add(new dbArgument { Prefix = A + "effect" + A + ":", ArgString = "[]" });
            addArg.Add(new dbArgument { Suffix = "}," });

            return addArg;
        }

        private List<dbArgument> SetDSA5Arguments(GegnerBase g, string GetFilenamePortrait, string GetFilenameToken, string id, int AnzChr)
        {
            List<dbArgument> lstArg = new List<dbArgument>();
            char A = (char)34;
            char b = (char)92;

            //gArg.lstArguments = new List<dbArgument>();
            lstArg.Add(new dbArgument { Prefix = "{" + A + "_id" + A + ":\"", ArgString = id, Suffix = A + "," });
            lstArg.Add(new dbArgument { Prefix = A + "name" + A + ":\"", ArgString = g.Name, Suffix = A + "," });
            lstArg.Add(new dbArgument { Prefix = A + "permission" + A + ":{\"default" + A + ":", ArgString = "0,\"3WHJiGe2LNC2VNeR" + A + ":", Suffix = "3}," });
            lstArg.Add(new dbArgument { Prefix = A + "type" + A + ":\"", ArgString = "creature", Suffix = A + "," });
            // lstArg.Add(new dbArgument { Prefix = A + "data" + A + ":{\"biography" + A + ":\"", ArgString = "", Suffix = A + "," });

            lstArg.Add(new dbArgument { Prefix = A + "data" + A + ":{" });
            lstArg.Add(new dbArgument { Prefix = A + "characteristics" + A + ":{" });

            lstArg.AddRange(AddGegnerArgument("mu", "#b3241a", g.MU));
            lstArg.AddRange(AddGegnerArgument("kl", "#8259a3", g.KL));
            lstArg.AddRange(AddGegnerArgument("in", "#388834", g.IN));
            lstArg.AddRange(AddGegnerArgument("ch", "#0d0d0d", g.CH));
            lstArg.AddRange(AddGegnerArgument("ff", "#d5b467", g.FF));
            lstArg.AddRange(AddGegnerArgument("ge", "#688ec4", g.GE));
            lstArg.AddRange(AddGegnerArgument("ko", "#fbf5ea", g.KO));
            lstArg.AddRange(AddGegnerArgument("kk", "#d6a878", g.KK, "},"));

            lstArg.Add(new dbArgument { Prefix = A + "status" + A + ":{" });
            lstArg.AddRange(AddGegnerStatus("wounds", (g.KO * 2).ToString(), g.LE.ToString(), "0", (g.LE - g.KO * 2).ToString(), g.LE.ToString(), "},"));
            lstArg.AddRange(AddGegnerStatus("astralenergy", g.AE.ToString(), g.AE.ToString(), "0", "0", g.AE.ToString(), "},"));
            lstArg.AddRange(AddGegnerStatus("karmaenergy", g.KE.ToString(), g.KE.ToString(), "0", "0", g.KE.ToString(), "},"));
            lstArg.AddRange(AddGegnerStatus("soulpower", "0", "0", null, "0", null, "},"));
            lstArg.AddRange(AddGegnerStatus("toughness", "0", "0", null, "0", null, "},"));
            lstArg.AddRange(AddGegnerStatus("dodge", null, "0", null, "0", null, "},"));
            lstArg.AddRange(AddGegnerStatus("fatePoints", null, "0", null, "0", "0", "},"));                            //Schicksalspunkte
            lstArg.AddRange(AddGegnerStatus("speed", g.GS.ToString(), "0", null, "0", null, "},"));
            lstArg.AddRange(AddGegnerStatus("initiative", null, g.INIBasis.ToString(), null, "0", g.INIBasis.ToString(), "},"));
            lstArg.Add(new dbArgument { Prefix = A + "size" + A + ":{" + A + "value" + A + ":", ArgString = A + "average" + A, Suffix = "}}," });

            lstArg.Add(new dbArgument { Prefix = A + "guidevalue" + A + ":{" + A + "value" + A + ":" + A + "-" + A, Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "tradition" + A + ":{" + A + "value" + A + ":" + A + A, Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "feature" + A + ":{" + A + "value" + A + ":" + A + A, Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "happyTalents" + A + ":{" + A + "value" + A + ":" + A + A, Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "details" + A + ":{" });
            lstArg.Add(new dbArgument { Prefix = A + "species" + A + ":{" + A + "value" + A + ":" + A + A, Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "gender" + A + ":{" + A + "value" + A + ":" + A + A, Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "culture" + A + ":{" + A + "value" + A + ":" + A + A, Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "career" + A + ":{" + A + "value" + A + ":" + A + A, Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "socialstate" + A + ":{" + A + "value" + A + ":" + A + A, Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "home" + A + ":{" + A + "value" + A + ":" + A + A, Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "family" + A + ":{" + A + "value" + A + ":" + A + A, Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "age" + A + ":{" + A + "value" + A + ":" + A + A, Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "haircolor" + A + ":{" + A + "value" + A + ":" + A + A, Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "eyecolor" + A + ":{" + A + "value" + A + ":" + A + A, Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "height" + A + ":{" + A + "value" + A + ":" + A + A, Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "weight" + A + ":{" + A + "value" + A + ":" + A + A, Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "distinguishingmark" + A + ":{" + A + "value" + A + ":" + A + A, Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "biography" + A + ":{" + A + "value" + A + ":" + A + A, Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "notes" + A + ":{" + A + "value" + A + ":" + A + A, Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "experience" + A + ":{" + A + "total" + A + ":0," + A + "spent" + A + ":0", Suffix = "}}," });
            lstArg.Add(new dbArgument { Prefix = A + "Home" + A + ":{" + A + "value" + A + ":" + A + A, Suffix = "}," });

            lstArg.Add(new dbArgument { Prefix = A + "actionCount" + A + ":{\"", ArgString = "value" + A + ":" + A + g.Aktionen, Suffix = A + "}," });
            lstArg.Add(new dbArgument { Prefix = A + "count" + A + ":{\"", ArgString = "value" + A + ":" + A + g.Auftreten, Suffix = A + "}," });
            lstArg.Add(new dbArgument { Prefix = A + "creatureClass" + A + ":{\"", ArgString = "value" + A + ":" + A + g.Tags, Suffix = A + "}," });
            lstArg.Add(new dbArgument { Prefix = A + "behaviour" + A + ":{\"", ArgString = "value" + A + ":" + A, Suffix = A + "}," });// + g.Bemerkung
            lstArg.Add(new dbArgument { Prefix = A + "flight" + A + ":{\"", ArgString = "value" + A + ":" + A + g.Jagdreaktion, Suffix = A + "}," });
            lstArg.Add(new dbArgument { Prefix = A + "specialRules" + A + ":{\"", ArgString = "value" + A + ":" + A + g.Verbreitung, Suffix = A + "}," });
            lstArg.Add(new dbArgument { Prefix = A + "conjuringDifficulty" + A + ":{\"", ArgString = "value" + A + ":0", Suffix = "}," });
            string bemerkungen = (g.Bemerkung == null ? "" : ("<p>" + g.Bemerkung.Replace("\n", "</p>" + b + "n<p>") + "</p>"));
            if (bemerkungen != null)
                bemerkungen = bemerkungen.Replace("\r", "");

            lstArg.Add(new dbArgument { Prefix = A + "description" + A + ":{\"", ArgString = "value" + A + ":" + A + bemerkungen, Suffix = A + "}," });// + g.Bemerkung
            //lstArg.Add(new dbArgument { Prefix = A + "freeLanguagePoints" + A + ":{" +A+"value"+A+":0,"+A+"used"+A+":0", Suffix="},"});
            lstArg.Add(new dbArgument { Prefix = A + "sheetLocked" + A + ":{" + A + "value" + A + ":" + "false", Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "flags" + A + ":{", Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "itemModifiers" + A + ":{", Suffix = "}}," });

            //Folder
            if (SelectedGegnerFolder != null && !string.IsNullOrEmpty(SelectedGegnerFolder.name))
            {
                lstArg.Add(new dbArgument { Prefix = A + "folder" + A + ":\"", ArgString = SelectedGegnerFolder._id, Suffix = "\"," });
            }

            lstArg.Add(new dbArgument { Prefix = A + "sort" + A + ":", ArgString = "100001", Suffix = "," });

            lstArg.Add(new dbArgument { Prefix = A + "flags" + A + ":{" });
            lstArg.Add(new dbArgument { Prefix = A + "core" + A + ":{" });
            lstArg.Add(new dbArgument { Prefix = A + "sourceId" + A + ":\"", ArgString = "Compendium.world.dsa-gegner.KBxxxx0000000001", Suffix = A + "}" });
            lstArg.Add(new dbArgument { Prefix = "}," });

            //Portrait Image
            lstArg.Add(new dbArgument
            {
                Prefix = A + "img" + A + ":\"",
                ArgString =
                (string.IsNullOrEmpty(GetFilenamePortrait) ? "icons/svg/mystery-man.svg" :
                 GetFilenamePortrait),
                Suffix = A + ","
            });

            lstArg.Add(new dbArgument { Prefix = A + "token" + A + ":{" });
            lstArg.Add(new dbArgument { Prefix = A + "flags" + A + ":{" });
            lstArg.Add(new dbArgument { Prefix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "name" + A + ":\"", ArgString = g.Name, Suffix = A + "," });
            lstArg.Add(new dbArgument { Prefix = A + "displayName" + A + ":", ArgString = "0", Suffix = "," });

            //Token Image
            lstArg.Add(new dbArgument
            {
                Prefix = A + "img" + A + ":\"",
                ArgString =
                (string.IsNullOrEmpty(GetFilenameToken) ? "icons/svg/mystery-man.svg" :
                GetFilenameToken),
                Suffix = A + ","
            });

            lstArg.Add(new dbArgument { Prefix = A + "tint" + A + ":", ArgString = "null", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "width" + A + ":", ArgString = "1", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "height" + A + ":", ArgString = "1", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "scale" + A + ":", ArgString = g.TokenOversize == null ? "1" : Math.Round(g.TokenOversize.Value, 1).ToString(), Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "lockRotation" + A + ":", ArgString = "false", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "vision" + A + ":", ArgString = "false", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "dimSight" + A + ":", ArgString = "0", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "brightSight" + A + ":", ArgString = "0", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "dimLight" + A + ":", ArgString = "0", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "brightLight" + A + ":", ArgString = "0", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "sightAngle" + A + ":", ArgString = "360", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "lightAngle" + A + ":", ArgString = "360", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "lightAlpha" + A + ":", ArgString = "1", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "lightAnimation" + A + ":{" });
            lstArg.Add(new dbArgument { Prefix = A + "speed" + A + ":", ArgString = "5", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "intensity" + A + ":", ArgString = "5" });
            lstArg.Add(new dbArgument { Prefix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "actorId" + A + ":\"", ArgString = id, Suffix = A + "," });
            lstArg.Add(new dbArgument { Prefix = A + "actorLink" + A + ":", ArgString = "false", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "disposition" + A + ":", ArgString = "-1", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "displayBars" + A + ":", ArgString = "0", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "bar1" + A + ":{", ArgString = "", Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "bar2" + A + ":{", ArgString = "", Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "randomImg" + A + ":", ArgString = "false", Suffix = "" });
            lstArg.Add(new dbArgument { Prefix = "}," });

            lstArg.Add(new dbArgument { Prefix = A + "items" + A + ":[", ArgString = "", Suffix = "" });
            lstArg.AddRange(AddMoneyArgument(0, 0, 0, 0));

            lstArg.AddRange(AddKampftalenteArgument(g));

            //Add Vor- Nachteile
            /*
             {"_id":"cokjaU2gwCgI6l2r","name":"Angst vor (eigenem Spiegelbild)","type":"disadvantage","data":{"description":{"value":"<p>Der Held fürchtet sich derart vor etwas, dass er hierdurch in seinem Handeln eingeschränkt wird. Der Auslöser der Angst sollte nicht zu selten vorkommen. Beispiele für Ängste sind Angst vor Blut, Magie, Spinnen, Dunkelheit, dem Meer, großer Höhe, freien Flächen, engen Räumen oder vor Toten und Untoten. Grundsätzlich hat der Spielleiter bei der Frage, ob eine bestimmte Angst möglich ist oder nicht, das letzte Wort.</p><p>Beispiele für Ängste</p><p><ul><li>Angst vor bestimmten Tieren, etwa Reptilien, Insekten, Spinnen</li><li>Angst vor Blut</li><li>Angst vor Dunkelheit (Dunkelangst)</li><li>Angst vor Höhe (Höhenangst)</li><li>Angst vor dem Meer (Meeresangst)</li><li>Angst vor engen Räumen (Raumangst)</li><li>Angst vor Toten und Untoten (Totenangst)</li></ul></p>"},"gmdescription":{"value":""},"APValue":{"value":"-8"},"max":{"value":"3"},"requirements":{"value":"keine"},"step":{"value":3},"effect":{"value":""}},"flags":{},"img":"systems/dsa5/icons/categories/Nachteil.webp","effects":[]},{"_id":"LHhkMjt3qqR2Xz47","name":"Schlechte Eigenschaft (Neugier)","type":"disadvantage","data":{"description":{"value":"<p>Jeder Abenteurer ist neugierig, aber einige Abenteurer werden so von ihrer Neugierde getrieben, dass sie unnötige Risiken eingehen, um sie zu befriedigen.</p><p>Viele Helden werden nicht nur von rationalen Motiven angetrieben. Sie sind  beispielsweise gierig, abergläubisch oder jähzornig.</p><p><b>Regel</b>: In jeder Situation, in der der Held mit einem potenziellen Auslöser seiner Schlechten Eigenschaft konfrontiert wird, muss er eine Probe auf Willenskraft bestehen, um sich zu beherrschen. Gelingt ihm diese Probe, ist alles in Ordnung, ansonsten muss er entsprechend der Schlechten Eigenschaft agieren. Seine Schlechte Eigenschaft hat ihn so lange im Griff, wie er dem Auslöser ausgesetzt ist.\n    Der Meister kann für die Probe auf Willenskraft entsprechend der Stärke des Auslösers Erschwernisse oder Erleichterungen aussprechen.\n    Es dürfen bis zu zwei Schlechte Eigenschaften pro Held gewählt werden. Kombinationen, die sich ausschließen (z.B. Geiz und Verschwendungssucht), dürfen nicht gewählt werden. Das letzte Wort darüber hat der Spielleiter.</b></p>"},"gmdescription":{"value":""},"APValue":{"value":"-5"},"max":{"value":"0"},"requirements":{"value":"keine"},"step":{"value":1},"effect":{"value":""}},"flags":{},"img":"systems/dsa5/icons/categories/Nachteil.webp","effects":[]}
             * 
             * 
             * 
             * 
             * 
             * 
             * 
              Waffe:
             {"_id":"6BVLO6Q8311fc2OJ","name":"Heshthot-Peitsche*","type":"meleeweapon","data":{"price":{"value":0},"quantity":{"value":1},"weight":{"value":1},"effect":{"value":"1 Stufe Schmerz"},"description":{"value":"Erzielt der Heshthot mit der Peitsche SP, erhält sein Opfer pro Treffer eine Stufe Schmerz bis zum nächsten Sonnenaufgang."},"gmdescription":{"value":""},"damage":{"value":"1d6+1"},"atmod":{"value":0,"offhandMod":0},"pamod":{"value":0,"offhandMod":0},"reach":{"value":"long"},"damageThreshold":{"value":16},"guidevalue":{"value":"ff"},"combatskill":{"value":"Peitschen"},"worn":{"value":true,"offhand":false,"offHand":false},"structure":{"max":0,"value":0}},"sort":100000,"flags":{},"img":"icons/weapons/swords/greatsword-blue.webp","effects":[]}
              */
            lstArg.AddRange(AddAngriffeArgument(g));
            lstArg.AddRange(AddZauberArgument(g));

            string atGuid = Guid.NewGuid().ToString().Substring(19, 17).Replace("-", "");
            lstArg.Add(new dbArgument { Prefix = "{" + A + "_id" + A + ":\"", ArgString = atGuid, Suffix = A + "," });
            lstArg.Add(new dbArgument { Prefix = A + "name" + A + ":\"", ArgString = "Natürliche Rüstung", Suffix = A + "," });
            lstArg.Add(new dbArgument { Prefix = A + "type" + A + ":\"", ArgString = "trait", Suffix = A + "," });
            lstArg.Add(new dbArgument { Prefix = A + "data" + A + ":{" });
            lstArg.Add(new dbArgument { Prefix = A + "description" + A + ":{\"", ArgString = "value" + A + ":" + A + "Natürlicher Schutz durch Haut, Schuppen oder dergleichen.", Suffix = A + " }," });
            lstArg.Add(new dbArgument { Prefix = A + "gmdescription" + A + ":{\"", ArgString = "value" + A + ":" + A, Suffix = A + "}," });
            lstArg.Add(new dbArgument { Prefix = A + "APValue" + A + ":{\"", ArgString = "value" + A + ":" + A + "0", Suffix = A + "}," });
            lstArg.Add(new dbArgument { Prefix = A + "traitType" + A + ":{\"", ArgString = "value" + A + ":" + A + "armor", Suffix = A + "}," });
            lstArg.Add(new dbArgument { Prefix = A + "at" + A + ":{\"", ArgString = "value" + A + ":" + A + g.RS[Kampf.Logic.Trefferzone.Gesamt], Suffix = A + "}," });
            lstArg.Add(new dbArgument { Prefix = A + "reach" + A + ":{\"", ArgString = "value" + A + ":" + A, Suffix = A + "}," });
            lstArg.Add(new dbArgument { Prefix = A + "damage" + A + ":{\"", ArgString = "value" + A + ":" + A + "1d6", Suffix = A + "}," });
            lstArg.Add(new dbArgument { Prefix = A + "reloadTime" + A + ":{\"", ArgString = "value" + A + ":" + A + "1", Suffix = A + "}," });
            lstArg.Add(new dbArgument { Prefix = A + "AsPCost" + A + ":{\"", ArgString = "value" + A + ":" + A + "0", Suffix = A + "}," });
            lstArg.Add(new dbArgument { Prefix = A + "duration" + A + ":{\"", ArgString = "value" + A + ":" + A, Suffix = A + "}," });
            lstArg.Add(new dbArgument { Prefix = A + "aspect" + A + ":{\"", ArgString = "value" + A + ":" + A, Suffix = A + "}," });
            lstArg.Add(new dbArgument { Prefix = A + "effect" + A + ":{\"", ArgString = "value" + A + ":" + A, Suffix = A + "}}," });

            lstArg.Add(new dbArgument { Prefix = A + "flags" + A + ":{", Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "img" + A + ":", ArgString = A + "systems/dsa5/icons/categories/Armor.webp", Suffix = A + "," });
            lstArg.Add(new dbArgument { Prefix = A + "effect" + A + ":", ArgString = "[]" });
            lstArg.Add(new dbArgument { Suffix = "}," });


            if (lstArg.Last().Suffix != "")
                lstArg.Last().Suffix = "}";
            //add Std.Talente?
            lstArg.Add(new dbArgument { Prefix = "]," });

            lstArg.Add(new dbArgument { Prefix = A + "effects" + A + ":[", ArgString = "", Suffix = "]" });
            lstArg.Add(new dbArgument { Prefix = "}" });

            return lstArg;

        }

        private List<dbArgument> SetDSA5Arguments(Held h, string GetFilenamePortrait, string GetFilenameToken, string id, int AnzChr )
        {
            List<dbArgument> lstArg = new List<dbArgument>();
            char A = (char)34;
            char b = (char)92;

            //gArg.lstArguments = new List<dbArgument>();
            lstArg.Add(new dbArgument { Prefix = "{" + A + "_id" + A + ":\"", ArgString = id, Suffix = A + "," });
            lstArg.Add(new dbArgument { Prefix = A + "name" + A + ":\"", ArgString = h.Name, Suffix = A + "," });
            lstArg.Add(new dbArgument { Prefix = A + "permission" + A + ":{\"default" + A + ":", ArgString = "0,\"3WHJiGe2LNC2VNeR" + A + ":", Suffix = "3}," });
            lstArg.Add(new dbArgument { Prefix = A + "type" + A + ":\"", ArgString = "character", Suffix = A + "," });
            // lstArg.Add(new dbArgument { Prefix = A + "data" + A + ":{\"biography" + A + ":\"", ArgString = "", Suffix = A + "," });

            lstArg.Add(new dbArgument { Prefix = A + "data" + A + ":{" });
            lstArg.Add(new dbArgument { Prefix = A + "characteristics" + A + ":{" });

            lstArg.AddRange(AddGegnerArgument("mu", "#b3241a", h.MU));
            lstArg.AddRange(AddGegnerArgument("kl", "#8259a3", h.KL));
            lstArg.AddRange(AddGegnerArgument("in", "#388834", h.IN));
            lstArg.AddRange(AddGegnerArgument("ch", "#0d0d0d", h.CH));
            lstArg.AddRange(AddGegnerArgument("ff", "#d5b467", h.FF));
            lstArg.AddRange(AddGegnerArgument("ge", "#688ec4", h.GE));
            lstArg.AddRange(AddGegnerArgument("ko", "#fbf5ea", h.KO));
            lstArg.AddRange(AddGegnerArgument("kk", "#d6a878", h.KK, "},"));

            lstArg.Add(new dbArgument { Prefix = A + "status" + A + ":{" });

            lstArg.AddRange(AddGegnerStatus("wounds", "0", h.LebensenergieAktuell.ToString(), "0", (h.LebensenergieMax - h.KO * 2).ToString(), h.LebensenergieMax.ToString(), "},"));
            lstArg.AddRange(AddGegnerStatus("astralenergy", h.AstralenergieMax.ToString(), h.AstralenergieAktuell.ToString(), "0", "0", h.AstralenergieMax.ToString(), "},"));
            lstArg.AddRange(AddGegnerStatus("karmaenergy", h.KarmaenergieMax.ToString(), h.KarmaenergieAktuell.ToString(), "0", "0", h.KarmaenergieMax.ToString(), "},"));
            lstArg.AddRange(AddGegnerStatus("soulpower", "0", "0", null, "0", null, "},"));
            lstArg.AddRange(AddGegnerStatus("toughness", "0", "0", null, "0", null, "},"));
            lstArg.AddRange(AddGegnerStatus("dodge", null, "0", null, "0", null, "},"));
            lstArg.AddRange(AddGegnerStatus("fatePoints", null, "0", null, "0", "0", "},"));                            //Schicksalspunkte
            lstArg.AddRange(AddGegnerStatus("speed", h.Geschwindigkeit.ToString(), "0", null, "0", null, "},"));
            lstArg.AddRange(AddGegnerStatus("initiative", null, h.InitiativeBasis.ToString(), null, "0", h.InitiativeMax().ToString(), "},"));
            lstArg.Add(new dbArgument { Prefix = A + "size" + A + ":{" + A + "value" + A + ":", ArgString = A + "average" + A, Suffix = "}}," });

            lstArg.Add(new dbArgument { Prefix = A + "guidevalue" + A + ":{" + A + "value" + A + ":" + A + "-" + A, Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "tradition" + A + ":{" + A + "value" + A + ":" + A + A, Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "feature" + A + ":{" + A + "value" + A + ":" + A + A, Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "happyTalents" + A + ":{" + A + "value" + A + ":" + A + A, Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "details" + A + ":{" });
            lstArg.Add(new dbArgument { Prefix = A + "species" + A + ":{" + A + "value" + A + ":" + A + A, Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "gender" + A + ":{" + A + "value" + A + ":" + A + A, Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "culture" + A + ":{" + A + "value" + A + ":" + A + A, Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "career" + A + ":{" + A + "value" + A + ":" + A + A, Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "socialstate" + A + ":{" + A + "value" + A + ":" + A + A, Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "home" + A + ":{" + A + "value" + A + ":" + A + A, Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "family" + A + ":{" + A + "value" + A + ":" + A + A, Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "age" + A + ":{" + A + "value" + A + ":" + A + A, Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "haircolor" + A + ":{" + A + "value" + A + ":" + A + A, Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "eyecolor" + A + ":{" + A + "value" + A + ":" + A + A, Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "height" + A + ":{" + A + "value" + A + ":" + A + A, Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "weight" + A + ":{" + A + "value" + A + ":" + A + A, Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "distinguishingmark" + A + ":{" + A + "value" + A + ":" + A + A, Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "biography" + A + ":{" + A + "value" + A + ":" + A + A, Suffix = "}," });

            lstArg.Add(new dbArgument { Prefix = A + "notes" + A + ":{" + A + "value" + A + ":" + A + A, Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "experience" + A + ":{" + A + "total" + A + ":0," + A + "spent" + A + ":0", Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "Home" + A + ":{" + A + "value" + A + ":" + A + A, Suffix = "}}," });

            lstArg.Add(new dbArgument { Prefix = A + "freeLanguagePoints" + A + ":{" + A + "value" + A + ":0," + A + "used" + A + ":0", Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "sheetLocked" + A + ":{" + A + "value" + A + ":" + "false", Suffix = "}," });

            lstArg.Add(new dbArgument { Prefix = A + "stats" + A + ":{" });
            lstArg.AddRange(AddStats("health", h.LebensenergieAktuell, 0, h.LebensenergieMax));
            lstArg.AddRange(AddStats("endurance", h.AusdauerAktuell, 0, h.AusdauerMax));
            lstArg.AddRange(AddStats("astral", h.AstralenergieAktuell, 0, h.AstralenergieMax));
            lstArg.AddRange(AddStats("karmal", h.KarmaenergieAktuell, 0, h.KarmaenergieMax));
            lstArg.AddRange(AddStats("magic_resistance", h.MRGeist, null, null, "},"));
            lstArg.AddRange(AddStats("speed", (int)h.Geschwindigkeit, null, null, "},"));
            lstArg.AddRange(AddStats("INI", h.InitiativeBasis, null, null, "},"));
            lstArg.AddRange(AddStats("AT", h.AT, null, null, "},"));
            lstArg.AddRange(AddStats("PA", h.PA, null, null, "},"));
            lstArg.AddRange(AddStats("FK", h.Fernkampf, null, null, "},"));
            lstArg.AddRange(AddStats("WS", Convert.ToInt32(h.KO / 2), null, null, "}},"));

            lstArg.Add(new dbArgument { Prefix = A + "attributes" + A + ":{" });
            lstArg.AddRange(AddAttributes("MU", h.MU, 0, h.MU));
            lstArg.AddRange(AddAttributes("KL", h.KL, 0, h.KL));
            lstArg.AddRange(AddAttributes("IN", h.IN, 0, h.IN));
            lstArg.AddRange(AddAttributes("CH", h.CH, 0, h.CH));
            lstArg.AddRange(AddAttributes("FF", h.FF, 0, h.FF));
            lstArg.AddRange(AddAttributes("GE", h.GE, 0, h.GE));
            lstArg.AddRange(AddAttributes("KO", h.KO, 0, h.KO));
            lstArg.AddRange(AddAttributes("KK", h.KK, 0, h.KK, "}}},"));

            //Folder
            if (SelectedHeldenFolder != null && !string.IsNullOrEmpty(SelectedHeldenFolder.name))
            {
                lstArg.Add(new dbArgument { Prefix = A + "folder" + A + ":\"", ArgString = SelectedHeldenFolder._id, Suffix = "\"," });
            }

            lstArg.Add(new dbArgument { Prefix = A + "sort" + A + ":", ArgString = "100001", Suffix = "," });

            lstArg.Add(new dbArgument { Prefix = A + "flags" + A + ":{" });
            lstArg.Add(new dbArgument { Prefix = A + "core" + A + ":{" });
            lstArg.Add(new dbArgument { Prefix = A + "sourceId" + A + ":\"", ArgString = "Compendium.world.dsa-gegner.KBxxxx0000000001", Suffix = A + "}" });
            lstArg.Add(new dbArgument { Prefix = "}," });

            //Portrait Image
            lstArg.Add(new dbArgument
            {
                Prefix = A + "img" + A + ":\"",
                ArgString =
                (string.IsNullOrEmpty(GetFilenamePortrait) ? "icons/svg/mystery-man.svg" :
                 GetFilenamePortrait),
                Suffix = A + ","
            });

            lstArg.Add(new dbArgument { Prefix = A + "token" + A + ":{" });
            lstArg.Add(new dbArgument { Prefix = A + "flags" + A + ":{" });
            lstArg.Add(new dbArgument { Prefix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "name" + A + ":\"", ArgString = h.Name, Suffix = A + "," });
            lstArg.Add(new dbArgument { Prefix = A + "displayName" + A + ":", ArgString = "0", Suffix = "," });

            //Token Image
            lstArg.Add(new dbArgument
            {
                Prefix = A + "img" + A + ":\"",
                ArgString =
                (string.IsNullOrEmpty(GetFilenameToken) ? "icons/svg/mystery-man.svg" :
                GetFilenameToken),
                Suffix = A + ","
            });

            lstArg.Add(new dbArgument { Prefix = A + "tint" + A + ":", ArgString = A + "" + A, Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "width" + A + ":", ArgString = "1", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "height" + A + ":", ArgString = "1", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "scale" + A + ":", ArgString = h.TokenOversize == null ? "1" : Math.Round(h.TokenOversize.Value, 1).ToString(), Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "lockRotation" + A + ":", ArgString = "false", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "vision" + A + ":", ArgString = "false", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "dimSight" + A + ":", ArgString = "0", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "brightSight" + A + ":", ArgString = "0", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "dimLight" + A + ":", ArgString = "0", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "brightLight" + A + ":", ArgString = "0", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "sightAngle" + A + ":", ArgString = "360", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "lightAngle" + A + ":", ArgString = "360", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "lightAlpha" + A + ":", ArgString = "1", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "lightAnimation" + A + ":{" });
            lstArg.Add(new dbArgument { Prefix = A + "speed" + A + ":", ArgString = "5", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "intensity" + A + ":", ArgString = "5" });
            lstArg.Add(new dbArgument { Prefix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "actorId" + A + ":\"", ArgString = id, Suffix = A + "," });
            lstArg.Add(new dbArgument { Prefix = A + "actorLink" + A + ":", ArgString = "false", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "disposition" + A + ":", ArgString = "-1", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "displayBars" + A + ":", ArgString = "0", Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "bar1" + A + ":{", ArgString = "", Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "bar2" + A + ":{", ArgString = "", Suffix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "randomImg" + A + ":", ArgString = "false", Suffix = "" });
            lstArg.Add(new dbArgument { Prefix = "}," });
            lstArg.Add(new dbArgument { Prefix = A + "items" + A + ":[", ArgString = "", Suffix = "" });

            lstArg.AddRange(AddMoneyArgument(0, 0, 0, 0));

            lstArg.AddRange(AddKampftalenteArgument(h));

            //Add Vor- Nachteile
            /*
             {"_id":"cokjaU2gwCgI6l2r","name":"Angst vor (eigenem Spiegelbild)","type":"disadvantage","data":{"description":{"value":"<p>Der Held fürchtet sich derart vor etwas, dass er hierdurch in seinem Handeln eingeschränkt wird. Der Auslöser der Angst sollte nicht zu selten vorkommen. Beispiele für Ängste sind Angst vor Blut, Magie, Spinnen, Dunkelheit, dem Meer, großer Höhe, freien Flächen, engen Räumen oder vor Toten und Untoten. Grundsätzlich hat der Spielleiter bei der Frage, ob eine bestimmte Angst möglich ist oder nicht, das letzte Wort.</p><p>Beispiele für Ängste</p><p><ul><li>Angst vor bestimmten Tieren, etwa Reptilien, Insekten, Spinnen</li><li>Angst vor Blut</li><li>Angst vor Dunkelheit (Dunkelangst)</li><li>Angst vor Höhe (Höhenangst)</li><li>Angst vor dem Meer (Meeresangst)</li><li>Angst vor engen Räumen (Raumangst)</li><li>Angst vor Toten und Untoten (Totenangst)</li></ul></p>"},"gmdescription":{"value":""},"APValue":{"value":"-8"},"max":{"value":"3"},"requirements":{"value":"keine"},"step":{"value":3},"effect":{"value":""}},"flags":{},"img":"systems/dsa5/icons/categories/Nachteil.webp","effects":[]},{"_id":"LHhkMjt3qqR2Xz47","name":"Schlechte Eigenschaft (Neugier)","type":"disadvantage","data":{"description":{"value":"<p>Jeder Abenteurer ist neugierig, aber einige Abenteurer werden so von ihrer Neugierde getrieben, dass sie unnötige Risiken eingehen, um sie zu befriedigen.</p><p>Viele Helden werden nicht nur von rationalen Motiven angetrieben. Sie sind  beispielsweise gierig, abergläubisch oder jähzornig.</p><p><b>Regel</b>: In jeder Situation, in der der Held mit einem potenziellen Auslöser seiner Schlechten Eigenschaft konfrontiert wird, muss er eine Probe auf Willenskraft bestehen, um sich zu beherrschen. Gelingt ihm diese Probe, ist alles in Ordnung, ansonsten muss er entsprechend der Schlechten Eigenschaft agieren. Seine Schlechte Eigenschaft hat ihn so lange im Griff, wie er dem Auslöser ausgesetzt ist.\n    Der Meister kann für die Probe auf Willenskraft entsprechend der Stärke des Auslösers Erschwernisse oder Erleichterungen aussprechen.\n    Es dürfen bis zu zwei Schlechte Eigenschaften pro Held gewählt werden. Kombinationen, die sich ausschließen (z.B. Geiz und Verschwendungssucht), dürfen nicht gewählt werden. Das letzte Wort darüber hat der Spielleiter.</b></p>"},"gmdescription":{"value":""},"APValue":{"value":"-5"},"max":{"value":"0"},"requirements":{"value":"keine"},"step":{"value":1},"effect":{"value":""}},"flags":{},"img":"systems/dsa5/icons/categories/Nachteil.webp","effects":[]}
             * 
             * 
             * 
             * 
              Waffe:
             {"_id":"6BVLO6Q8311fc2OJ","name":"Heshthot-Peitsche*","type":"meleeweapon","data":{"price":{"value":0},"quantity":{"value":1},"weight":{"value":1},"effect":{"value":"1 Stufe Schmerz"},"description":{"value":"Erzielt der Heshthot mit der Peitsche SP, erhält sein Opfer pro Treffer eine Stufe Schmerz bis zum nächsten Sonnenaufgang."},"gmdescription":{"value":""},"damage":{"value":"1d6+1"},"atmod":{"value":0,"offhandMod":0},"pamod":{"value":0,"offhandMod":0},"reach":{"value":"long"},"damageThreshold":{"value":16},"guidevalue":{"value":"ff"},"combatskill":{"value":"Peitschen"},"worn":{"value":true,"offhand":false,"offHand":false},"structure":{"max":0,"value":0}},"sort":100000,"flags":{},"img":"icons/weapons/swords/greatsword-blue.webp","effects":[]}
              */
            lstArg.AddRange(AddAngriffeArgument(h));
            lstArg.AddRange(AddZauberArgument(h));

            //Add all Talente
            int i = 1;
            foreach (Held_Talent ht in h.Held_Talent)
            {
                if (ht.Talent.Talentgruppe.Kurzname == "Kampf" ||
                    ht.Talent.Talentgruppe.Kurzname == "Ritualkenntnis" ||
                    ht.Talent.Talentgruppe.Kurzname == "Meta")
                    continue;
                lstArg.AddRange(GetArgumenteHeldTalent(ht, i));
                i++;
            }

            lstArg.Last().Suffix = "}";

            // end Talente

            lstArg.Add(new dbArgument { Prefix = "]," });

            lstArg.Add(new dbArgument { Prefix = A + "effects" + A + ":[", ArgString = "", Suffix = "]" });
            lstArg.Add(new dbArgument { Prefix = "}" });

            return lstArg;

        }
        private List<dbArgument> GetArgumenteHeldTalent(Held_Talent ht, int sortNo)
        {
            List<dbArgument> lstArg = new List<dbArgument>();
            string GruppenTyp =
                ht.Talent.Talentgruppe.Kurzname == "Kampf" ? "combat" :
                ht.Talent.Talentgruppe.Kurzname == "Körper" ? "body" :
                ht.Talent.Talentgruppe.Kurzname == "Gesellschaft" ? "social" :
                ht.Talent.Talentgruppe.Kurzname == "Natur" ? "nature" :
                ht.Talent.Talentgruppe.Kurzname == "Wissen" ? "knowledge" :
                ht.Talent.Talentgruppe.Kurzname == "Handwerk" ? "trade" :
                ht.Talent.Talentgruppe.Kurzname == "Sprachen/Schriften" ? "knowledge" :
                ht.Talent.Talentgruppe.Kurzname == "Meta" ? "meta" :
                ht.Talent.Talentgruppe.Kurzname == "Gabe" ? "gift" :
                ht.Talent.Talentgruppe.Kurzname == "Ritualkenntnis" ? "ritual" :
                ht.Talent.Talentgruppe.Kurzname == "Liturgiekenntnis" ? "liturg" : "";
            char A = (char)34;
            string id = Guid.NewGuid().ToString().Substring(19, 17).Replace("-", "");
            //gArg.lstArguments = new List<dbArgument>();
            lstArg.Add(new dbArgument { Prefix = "{" + A + "_id" + A + ":\"", ArgString = id, Suffix = A + "," });
            lstArg.Add(new dbArgument { Prefix = A + "name" + A + ":\"", ArgString = ht.Talentname, Suffix = A + "," });
            lstArg.Add(new dbArgument { Prefix = A + "type" + A + ":\"", ArgString = "skill", Suffix = A + "," });
            lstArg.Add(new dbArgument { Prefix = A + "data" + A + ":", ArgString = "{" });
            lstArg.Add(new dbArgument { Prefix = A + "group" + A + ":{", ArgString = A + "value" + A + ":\"" + GruppenTyp + A + "," + A + "type" + A + ":" + A + "String" + A + "," + A + "label" + A + ":\"Skillgroup", Suffix = A + "}," });

            lstArg.Add(new dbArgument { Prefix = A + "talentValue" + A + ":{\"", ArgString = "value" + A + ":" + ht.TaW + "," + A + "type" + A + ":" + A + "Number" + A + "," + A + "label" + A + ":" + A + "Fw", Suffix = A + "}," });
            lstArg.Add(new dbArgument { Prefix = A + "characteristic1" + A + ":{\"", ArgString = "value" + A + ":\"" + ht.Talent.Eigenschaft1.ToLower() + A + "," + A + "type" + A + ":" + A + "String" + A + "," + A + "label" + A + ":" + A + "Characteristic1", Suffix = A + "}," });
            lstArg.Add(new dbArgument { Prefix = A + "characteristic2" + A + ":{\"", ArgString = "value" + A + ":\"" + ht.Talent.Eigenschaft2.ToLower() + A + "," + A + "type" + A + ":" + A + "String" + A + "," + A + "label" + A + ":" + A + "Characteristic2", Suffix = A + "}," });
            lstArg.Add(new dbArgument { Prefix = A + "characteristic3" + A + ":{\"", ArgString = "value" + A + ":\"" + ht.Talent.Eigenschaft3.ToLower() + A + "," + A + "type" + A + ":" + A + "String" + A + "," + A + "label" + A + ":" + A + "Characteristic3", Suffix = A + "}," });
            lstArg.Add(new dbArgument { Prefix = A + "RPr" + A + ":{\"", ArgString = "value" + A + ":\"maybe" + A + "," + A + "type" + A + ":" + A + "String" + A + "," + A + "label" + A + ":" + A + "Routinecheck", Suffix = A + "}," });
            string be = string.IsNullOrEmpty(ht.Talent.eBE) ? "no" : ht.Talent.eBE == "Belastung" ? "BE" : ht.Talent.eBE;
            lstArg.Add(new dbArgument { Prefix = A + "burden" + A + ":{\"", ArgString = "value" + A + ":\"" + be + A + "," + A + "type" + A + ":" + A + "String" + A + "," + A + "label" + A + ":" + A + "Burden", Suffix = A + "}," });
            lstArg.Add(new dbArgument { Prefix = A + "description" + A + ":{\"", ArgString = "type" + A + ":" + A + "String" + A + "," + A + "label" + A + ":" + A + "Description", Suffix = A + "}," });
            lstArg.Add(new dbArgument { Prefix = A + "gmdescription" + A + ":{\"", ArgString = "type" + A + ":" + A + "String" + A + "," + A + "label" + A + ":" + A + "Description", Suffix = A + "}," });
            lstArg.Add(new dbArgument { Prefix = A + "Rpr" + A + ":{\"", ArgString = "value" + A + ":\"maybe" + A + "," + A + "type" + A + ":" + A + "String" + A + "," + A + "label" + A + ":" + A + "Routinecheck", Suffix = A + "}," });
            lstArg.Add(new dbArgument { Prefix = A + "StF" + A + ":{\"", ArgString = "value" + A + ":\"" + ht.Talent.Steigerung, Suffix = A + "}}," });

            lstArg.Add(new dbArgument { Prefix = A + "sort" + A + ":", ArgString = (sortNo * 100000).ToString(), Suffix = "," });
            lstArg.Add(new dbArgument { Prefix = A + "flags" + A + ":{" });
            lstArg.Add(new dbArgument { Prefix = A + "core" + A + ":{\"" + "sourceId" + A + ":" + A + "Item.W0KTljWMKO7otAZR" + A + "}}," + A + "img" + A + ":\"" + "icons/tools/hand/spinning-wheel-brown.webp", Suffix = A + "," });
            lstArg.Add(new dbArgument { Prefix = A + "effects" + A + ":[]", Suffix = "}," });

            return lstArg;
        }
        /*           
             {"_id":"JDDZiwiT2Myr1BjE",
            "name":"Betören",
            "type":"skill",
            "data":
                {
                "group":{"value":"social","type":"String","label":"Skillgroup"},
                "talentValue":{"value":0,"type":"Number","label":"Fw"},
                "characteristic1":{"value":"mu","type":"String","label":"Characteristic1"},
                "characteristic2":{"value":"ch","type":"String","label":"Characteristic2"},
                "characteristic3":{"value":"ch","type":"String","label":"Characteristic3"},
                "RPr":{"value":"no","type":"String","label":"Routinecheck"},
                "burden":{"value":"maybe","type":"String","label":"Burden"},
                "description":{"type":"String","label":"Description","value":""},
                "gmdescription":{"type":"String","label":"Description","value":""},
                "Rpr":{"value":"no","type":"String","label":"Routinecheck"},
                "StF":{"value":"B"}
                },
            "sort":100000,
            "flags":{"core":{"sourceId":"Item.W0KTljWMKO7otAZR"}},"img":"icons/tools/hand/spinning-wheel-brown.webp",
            "effects":[]},
             */

        public void GetGegnerData()
        {
            Global.SetIsBusy(true, "Gegner werden exportiert");
            try
            {
                lstGegnerArgument.Clear();
                MyTimer.start_timer();
                List<string> lstPicKopiert = new List<string>();
                List<string> lstTokenKopiert = new List<string>();
                string MGPfad = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\";

                List<string> lstFTPGegnerPics = new List<string>();
                if (!IsLocalInstalliert)
                {
                    List<string> list = new List<string>();
                    NetworkCredential credentials = new NetworkCredential(FTPUser, FTPPasswort);
                    ListFtpDirectory(FoundryPfad + GegnerPortraitPfad, "", false, credentials, list);
                    lstFTPGegnerPics = list;
                }
                int AnzChr = 1001;
                foreach (GegnerBase g in lstGegnerBase)
                {
                    Global.SetIsBusy(true, "Exportiere: " + g.Name);
                    BildSpeichern(g.Bild, GegnerPortraitPfad, MGPfad, lstFTPGegnerPics, lstPicKopiert);
                    BildSpeichern(g.Bild, GegnerTokenPfad, MGPfad, lstFTPGegnerPics, lstTokenKopiert);

                    string GetFilenamePortrait = string.IsNullOrEmpty(g.Bild) ? null : (GegnerPortraitPfad.Replace(@"\", "/") + System.IO.Path.GetFileName(g.Bild));
                    //Todo: muss auf g.Token abgeändert werden, sobald Token DB vorhanden & GegnerTokenPfad
                    string GetFilenameToken = string.IsNullOrEmpty(g.Bild) ? GetFilenamePortrait : (GegnerPortraitPfad.Replace(@"\", "/") + System.IO.Path.GetFileName(g.Bild));

                    //char A = (char)34; // (new Char[] { '"' });
                    //                       //4e1d3250-f700-3000-0001-387712958942  => 0001387712958942
                    string id = g.GegnerBaseGUID.ToString().Substring(19, 17).Replace("-", "");

                    GegnerArgument gArg = new GegnerArgument();
                    gArg.g = g;
                    gArg.lstArguments = new List<dbArgument>();
                    if (DSA41Version)
                        gArg.lstArguments.AddRange(SetDSA41Arguments(g, GetFilenamePortrait, GetFilenameToken, id, AnzChr));
                    else
                        gArg.lstArguments.AddRange(SetDSA5Arguments(g, GetFilenamePortrait, GetFilenameToken, id, AnzChr));
                    AnzChr++;
                    gArg.outcome = "";
                    foreach (dbArgument arg in gArg.lstArguments)
                    { gArg.outcome += arg.Prefix + arg.ArgString + arg.Suffix; }

                    lstGegnerArgument.Add(gArg);
                }
                MyTimer.stop_timer("Gegner-DB-Argument");
                Global.SetIsBusy(false);
            }
            catch (Exception ex)
            {

                Global.SetIsBusy(false);
            }
        }


        public void GetWaffenData_DSA50()
        {
            List<dbArgument> lstWaffen = new List<dbArgument>();
            lstWaffenArgument.Clear();
            MyTimer.start_timer();

            string MGPfad = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\";

            int i = 100001;
            string wname = "";
            foreach (Waffe w in WaffeListe.OrderBy(t => t.Name))
            {
                List<dbArgument> lstArg = new List<dbArgument>();
                char A = (char)34;
                char b = (char)92;
                string id = w.WaffeGUID.ToString().Substring(19, 17).Replace("-", "");
                wname = w.Name;
                lstArg.Add(new dbArgument { Prefix = "{" + A + "_id" + A + ":\"", ArgString = id, Suffix = A + "," });
                lstArg.Add(new dbArgument { Prefix = A + "name" + A + ":\"", ArgString = w.Name, Suffix = A + "," });
                lstArg.Add(new dbArgument { Prefix = A + "permission" + A + ":{\"default" + A + ":", ArgString = "0,\"3WHJiGe2LNC2VNeR" + A + ":", Suffix = "3}," });
                lstArg.Add(new dbArgument { Prefix = A + "type" + A + ":\"", ArgString = "meleeweapon", Suffix = A + "," });
                lstArg.Add(new dbArgument { Prefix = A + "data" + A + ":{" });
                lstArg.Add(new dbArgument { Prefix = A + "price" + A + ":{\"", ArgString = "value" + A + ":" + w.Preis, Suffix = "}," });
                lstArg.Add(new dbArgument { Prefix = A + "quantity" + A + ":{\"", ArgString = "value" + A + ":1", Suffix = "}," });
                lstArg.Add(new dbArgument { Prefix = A + "weight" + A + ":{\"", ArgString = "value" + A + ":" + w.Gewicht, Suffix = "}," });
                lstArg.Add(new dbArgument { Prefix = A + "effect" + A + ":{\"", ArgString = "value" + A + ":" + A + A, Suffix = "}," });
                lstArg.Add(new dbArgument
                {
                    Prefix = A + "description" + A + ":{\"",
                    ArgString = "value" + A + ":" + A + "<p>" +
                    ((MeisterGeister.ViewModel.Basar.Logic.IHandelsgut)w)?.Bemerkung + "</p>" + b + "n<p>" +
                    w.Bemerkung + "<p>" + A,
                    Suffix = "},"
                });
                lstArg.Add(new dbArgument { Prefix = A + "gmdescription" + A + ":{\"", ArgString = "value" + A + ":" + A + A, Suffix = "}," });
                lstArg.Add(new dbArgument { Prefix = A + "damage" + A + ":{\"", ArgString = "value" + A + ":" + A + w.TPWürfelAnzahl + "W" + w.TPWürfel + "+" + w.TPBonus + A, Suffix = "}," });
                lstArg.Add(new dbArgument { Prefix = A + "atmod" + A + ":{\"", ArgString = "value" + A + ":0, " + A + "offhandMod" + A + ":0", Suffix = "}," });
                lstArg.Add(new dbArgument { Prefix = A + "pamod" + A + ":{\"", ArgString = "value" + A + ":0, " + A + "offhandMod" + A + ":0", Suffix = "}," });
                string dk = w.DK == null ? "medium" : w.DK.Contains("H") ? "short" : w.DK.Contains("N") ? "medium" : w.DK.Contains("S") ? "long" : w.DK.Contains("P") ? "wide" : "medium";
                lstArg.Add(new dbArgument { Prefix = A + "reach" + A + ":{\"", ArgString = "value" + A + ":" + A + dk + A, Suffix = "}," });
                lstArg.Add(new dbArgument { Prefix = A + "damageThreshold" + A + ":{\"", ArgString = "value" + A + ":15", Suffix = "}," });
                lstArg.Add(new dbArgument { Prefix = A + "guidevalue" + A + ":{\"", ArgString = "value" + A + ":" + A + (w.Talent.FirstOrDefault()?.Eigenschaft1?.ToLower() ?? "kk") + A, Suffix = "}," });

                string talentName = w.Talent.FirstOrDefault()?.Talentname ?? "Säbel";
                lstArg.Add(new dbArgument { Prefix = A + "combatskill" + A + ":{\"", ArgString = "value" + A + ":" + A + talentName + A, Suffix = "}," });
                lstArg.Add(new dbArgument { Prefix = A + "worn" + A + ":{\"", ArgString = "value" + A + ":false," + A + "offhand" + A + ":false", Suffix = "}," });
                lstArg.Add(new dbArgument { Prefix = A + "structure" + A + ":{\"", ArgString = "max" + A + ":0," + A + "value" + A + ":0", Suffix = "}}," });
                string folderID = SelectedWaffenFolder == null ? "null" : (A + SelectedWaffenFolder._id + A);
                lstArg.Add(new dbArgument { Prefix = A + "folder" + A + ":", ArgString = folderID, Suffix = "," });
                lstArg.Add(new dbArgument { Prefix = A + "sort" + A + ":", ArgString = i.ToString(), Suffix = "," });
                i += 100000;
                lstArg.Add(new dbArgument { Prefix = A + "flags" + A + ":{}", Suffix = "," });

                WaffeTalent wTal = lstWTalent.FirstOrDefault(t => t.DSA5Gruppe == talentName) ??
                    lstWTalent.FirstOrDefault(t => t.DSA5Gruppe == "Dolche");
                lstArg.Add(new dbArgument { Prefix = A + "img" + A + ":", ArgString = A + "modules/dsa5-core/icons/" + wTal.img + A, Suffix = "," });
                lstArg.Add(new dbArgument { Prefix = A + "effects" + A + ":[]", Suffix = "" });
                lstArg.Add(new dbArgument { Suffix = "}" });

                string outc = "";
                foreach (dbArgument arg in lstArg)
                { outc += arg.Prefix + arg.ArgString + arg.Suffix; }
                lstWaffenArgument.Add(new GegenstandArgument() { name = wname, lstArguments = lstArg, outcome = outc });
            }
        }

        public void GetWaffenData_DSA41()
        {
            char A = (char)34;
            List<dbArgument> lstWaffen = new List<dbArgument>();
            lstWaffenArgument.Clear();
            MyTimer.start_timer();

            string MGPfad = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\";
            string wname = "";
            List<dbArgument> lstArg = new List<dbArgument>();

            string GMid = GetUserID(string.Format(@"{0}worlds\{1}\data\users.db", FoundryPfad, SelectedWorld), "Gamemaster");

            string folderID = SelectedWaffenFolder == null ? "null" : (SelectedWaffenFolder._id);
            //Klappt nicht ?!?
            //folder f = lstFolders.FirstOrDefault(t => t.name == "Fernkampfwaffen" && t.parent == folderID && t.typ == "JournalEntry");            
            //if (f == null)
            //    folderID = DoCreateFolder("Fernkampfwaffen", folderID, "JournalEntry", "#816a6a");
            //lstArg.Add(new dbArgument { Prefix = A + "folder" + A + ":", ArgString = folderID, Suffix = "," });

            foreach (Waffe w in Global.ContextInventar.WaffeListe)
            {
                GegenstandArgument newGeg = new GegenstandArgument();
                newGeg.name = w.Name;
                Held_Ausrüstung newHA = new Held_Ausrüstung();
                newHA.Held_BFAusrüstung = new Held_BFAusrüstung();
                newHA.Held_BFAusrüstung.Held_Waffe = new Held_Waffe() { Waffe = w };

                DSA41_KämpferTalent heldT = new DSA41_KämpferTalent();
                heldT.ha = newHA;
                heldT.GMid = GMid;
                heldT.USERid = null;
                lstArg.Add(new dbArgument { Prefix = heldT.GetLongInfoAusrüstung(0, folderID) });
                lstArg.Last().Prefix = lstArg.Last().Prefix.TrimEnd(new Char[] { ',' });

                string outc = lstArg.Last().Prefix;
                lstWaffenArgument.Add(new GegenstandArgument() { name = newGeg.name, lstArguments = lstArg, outcome = outc });
            }

            foreach (Fernkampfwaffe fern in Global.ContextInventar.FernkampfwaffeListe)
            {
                GegenstandArgument newGeg = new GegenstandArgument();
                newGeg.name = fern.Name;
                Held_Ausrüstung newHA = new Held_Ausrüstung();
                newHA.Held_Fernkampfwaffe = new Held_Fernkampfwaffe();
                newHA.Held_Fernkampfwaffe.Fernkampfwaffe = fern;

                DSA41_KämpferTalent heldT = new DSA41_KämpferTalent();
                heldT.ha = newHA;
                heldT.GMid = GMid;
                heldT.USERid = null;
                lstArg.Add(new dbArgument { Prefix = heldT.GetLongInfoAusrüstung(0, folderID) });
                lstArg.Last().Prefix = lstArg.Last().Prefix.TrimEnd(new Char[] { ',' });

                string outc = lstArg.Last().Prefix;
                lstWaffenArgument.Add(new GegenstandArgument() { name = newGeg.name, lstArguments = lstArg, outcome = outc });
            }

            foreach (Schild s in Global.ContextInventar.SchildListe)
            {
                GegenstandArgument newGeg = new GegenstandArgument();
                newGeg.name = s.Name;
                Held_Ausrüstung newHA = new Held_Ausrüstung();
                newHA.Held_BFAusrüstung = new Held_BFAusrüstung();
                newHA.Held_BFAusrüstung.Schild = s;

                DSA41_KämpferTalent heldT = new DSA41_KämpferTalent();
                heldT.ha = newHA;
                heldT.GMid = GMid;
                heldT.USERid = null;
                lstArg.Add(new dbArgument { Prefix = heldT.GetLongInfoAusrüstung(0, folderID) });
                lstArg.Last().Prefix = lstArg.Last().Prefix.TrimEnd(new Char[] { ',' });

                string outc = lstArg.Last().Prefix;
                lstWaffenArgument.Add(new GegenstandArgument() { name = newGeg.name, lstArguments = lstArg, outcome = outc });
            }

            foreach (Rüstung r in Global.ContextInventar.RuestungListe)
            {
                GegenstandArgument newGeg = new GegenstandArgument();
                newGeg.name = r.Name;
                Held_Ausrüstung newHA = new Held_Ausrüstung();
         //       newHA.Held_BFAusrüstung = new Held_BFAusrüstung();
         //       newHA.Held_BFAusrüstung.Held_Ausrüstung = new Held_Ausrüstung() { Held_Rüstung = new Held_Rüstung() { Rüstung = r } };

                DSA41_KämpferTalent heldT = new DSA41_KämpferTalent();
                newHA.Held_Rüstung = new Held_Rüstung() { Rüstung = r } ;
                heldT.ha = newHA;
                heldT.GMid = GMid;
                heldT.USERid = null;
                lstArg.Add(new dbArgument { Prefix = heldT.GetLongInfoAusrüstung(0, folderID) });
                lstArg.Last().Prefix = lstArg.Last().Prefix.TrimEnd(new Char[] { ',' });

                string outc = lstArg.Last().Prefix;
                lstWaffenArgument.Add(new GegenstandArgument() { name = newGeg.name, lstArguments = lstArg, outcome = outc });
            }
        }


        public void GetHeldenData()
        {
            Global.SetIsBusy(true, "Exportiere Helden");
            try
            {
                lstHeldArgument.Clear();
                MyTimer.start_timer();

                List<string> lstPicKopiert = new List<string>();
                List<string> lstTokenKopiert = new List<string>();
                string MGPfad = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\";

                List<string> lstFTPHeldenPics = new List<string>();
                if (!IsLocalInstalliert)
                {
                    List<string> list = new List<string>();
                    NetworkCredential credentials = new NetworkCredential(FTPUser, FTPPasswort);
                    ListFtpDirectory(FoundryPfad + HeldPortraitPfad, "", false, credentials, list);
                    lstFTPHeldenPics = list;
                }
                int AnzChr = 1;
                foreach (Held h in Global.ContextHeld.HeldenGruppeListe.Where(t => t.AktiveHeldengruppe.Value))
                {
                    Global.SetIsBusy(true, "Exportiere: " + h.Name);
                    BildSpeichern(h.Bild, HeldPortraitPfad, MGPfad, lstFTPHeldenPics, lstPicKopiert);
                    BildSpeichern(h.Token, HeldTokenPfad, MGPfad, lstFTPHeldenPics, lstTokenKopiert);

                    string GetFilenamePortrait = (string.IsNullOrEmpty(h.Bild) ? null : (HeldPortraitPfad.Replace(@"\", "/") + System.IO.Path.GetFileName(h.Bild)));
                    string GetFilenameToken = string.IsNullOrEmpty(h.Token) ? GetFilenamePortrait : (HeldTokenPfad.Replace(@"\", "/") + System.IO.Path.GetFileName(h.Token));

                    char A = (char)34;
                    string id = h.HeldGUID.ToString().Substring(19, 17).Replace("-", "");

                    HeldenArgument hArg = new HeldenArgument();
                    hArg.h = h;
                    hArg.lstArguments = new List<dbArgument>();
                    if (DSA41Version)
                        hArg.lstArguments.AddRange(SetDSA41Arguments(h, GetFilenamePortrait, GetFilenameToken, id, AnzChr));
                    else
                        hArg.lstArguments.AddRange(SetDSA5Arguments(h, GetFilenamePortrait, GetFilenameToken, id, AnzChr));
                    AnzChr++;
                    hArg.outcome = "";
                    foreach (dbArgument arg in hArg.lstArguments)
                    { hArg.outcome += arg.Prefix + arg.ArgString + arg.Suffix; }

                    lstHeldArgument.Add(hArg);
                }
                MyTimer.stop_timer("Helden-DB-Argument");
                Global.SetIsBusy(false);
            }
            catch (Exception ex)
            {
                Global.SetIsBusy(false);
            }
        }

        public void InitWaffen()
        {
            if (WaffeListe.Count == 0)
                //HandelsgutListe = Global.ContextHandelsgut == null ? new List<Model.Handelsgut>() : Global.ContextHandelsgut.HandelsgüterListe;
                WaffeListe = Global.ContextInventar == null ? new List<Model.Waffe>() : Global.ContextInventar.WaffeListe;
            //FernkampfwaffeListe = Global.ContextInventar == null ? new List<Model.Fernkampfwaffe>() : Global.ContextInventar.FernkampfwaffeListe;
            //SchildListe = Global.ContextInventar == null ? new List<Model.Schild>() : Global.ContextInventar.SchildListe;
            //RüstungListe = Global.ContextInventar == null ? new List<Model.Rüstung>() : Global.ContextInventar.RuestungListe;

        }
        public void Init()
        {
            if (IsLocalInstalliert)
                FoundryPfad = localFoundryPfad;
            else
                FoundryPfad = FTPAdresse + "/data/";

            if (IsLocalInstalliert)
            {
                string appFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                string path = appFolderPath + @"\FoundryVTT\Config\";

                if (Directory.Exists(path))
                    ReadFoundryOptions(path + @"\options.json");
            }
            else
            {
                if (doLoad) ReadFoundryOptions(FTPAdresse + "/config/options.json");
            }

            GegnerPortraitPfad = Einstellungen.GetEinstellung<string>("FoundryGegnerPortraitPfad");
            HeldPortraitPfad = Einstellungen.GetEinstellung<string>("FoundryHeldPortraitPfad");
            GegnerTokenPfad = Einstellungen.GetEinstellung<string>("FoundryGegnerTokenPfad");
            HeldTokenPfad = Einstellungen.GetEinstellung<string>("FoundryHeldTokenPfad");
            MusikPfad = Einstellungen.GetEinstellung<string>("FoundryMusikPfad");

            //Read Worlds
            if (string.IsNullOrEmpty(FoundryPfad) ||
                (IsLocalInstalliert && !Directory.Exists(FoundryPfad)))
                return;

            if (doLoad || IsLocalInstalliert)
                LoadWorldsFolder();
        }
        private void CreateFolder(object sender)
        {
            if (SelectedWorld == null)
            {
                ViewHelper.Popup("Wähle zuerst eine Welt");
                return;
            }
            string newFolder = ViewHelper.InputDialog("Erstelle Actor Verzeichnis", "Gebe den Namen des neuen Verzeichnisses\nfür die Actor ein", "");

            if (string.IsNullOrEmpty(newFolder))
                return;
            if (lstFolders.FirstOrDefault(t => t.name == newFolder) != null)
            {
                ViewHelper.Popup(string.Format("Das Verzeichnis {0} existiert bereits.\nFunktion abgebrochen", newFolder));
                return;
            }
            string typ = sender as string;

            DoCreateFolder(newFolder, null, typ);

            ViewHelper.Popup("Verzeichnis erstellt");
        }

        private string DoCreateFolder(string newFolder, string partentID, string typ, string color = null)
        {            
            char A = (char)34; 
            string _id = Guid.NewGuid().ToString().Substring(19, 17).Replace("-", "");

            string outdata = "{" + A + "name" + A + ":" + A + newFolder + A + "," + A + "type" + A + ":" + A + typ + A + "," + A + "sort" + A +
                ":0," + A + "flags" + A + ":{}," + A + "parent" + A + ":"+ (partentID??"null") + "," + A + "sorting" + A + ":" + A + "a" + A +
                "," + A + "color" + A + ":" + (string.IsNullOrEmpty( color)?"null": A +color+ A ) + "," + A + "_id" + A + ":" + A + _id + A + "}\n";

            if (IsLocalInstalliert)
            {
                string foldersFilePath = FoundryPfad + @"worlds\" + SelectedWorld + @"\data\folders.db";
                File.AppendAllText(foldersFilePath, outdata);
            }
            else
            {
                string foldersFilePath = FoundryPfad + @"worlds/" + SelectedWorld + "/data/folders.db";
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(foldersFilePath);
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                request.Credentials = new NetworkCredential(FTPUser, FTPPasswort, "");
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream);

                string FileData = reader.ReadToEnd();
                Console.WriteLine($"Download Complete, status {response.StatusDescription}");
                reader.Close();
                response.Close();

                FileData += outdata;
                FtpWebRequest request2 = (FtpWebRequest)WebRequest.Create(foldersFilePath);
                request2.Method = WebRequestMethods.Ftp.UploadFile;
                request2.Credentials = new NetworkCredential(FTPUser, FTPPasswort, "");

                // convert contents to byte.
                byte[] fileContents = Encoding.ASCII.GetBytes(FileData);
                request.ContentLength = fileContents.Length;

                using (Stream requestStream = request2.GetRequestStream())
                {
                    requestStream.Write(fileContents, 0, fileContents.Length);
                }
                using (response = (FtpWebResponse)request2.GetResponse())
                {
                    Console.WriteLine($"Upload File Complete, status {response.StatusDescription}");
                }
            }
            List<folder> lst = new List<folder>();
            lst.AddRange(lstFolders);
            lst.Add(new folder() { name = newFolder, color = "", sorting = "a", typ = typ, parent= partentID, _id = _id });
            lstFolders = lst;
            return _id;
        }

        private bool PathExists(string path)
        {
            if (IsLocalInstalliert)
            {
                return Directory.Exists(path);
            }
            else
            {
                // NACH DEM AUFRUF GIBR ES EINE EXCEPTION BEIM NÄCHSTEN ANFRAGEN DER FTP SEITE!!
                try
                {
                    FtpWebRequest request = (FtpWebRequest)WebRequest.Create(path);
                    request.Method = WebRequestMethods.Ftp.ListDirectory;
                    request.Credentials = new NetworkCredential(FTPUser, FTPPasswort, "");
                    FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                    return true;
                }
                catch (WebException ex)
                {
                    return false;
                }
            }
        }
        #endregion

        #region //---- EVENTS ----
        private Base.CommandBase _onBtnCreateFolder = null;
        public Base.CommandBase OnBtnCreateFolder
        {
            get
            {
                if (_onBtnCreateFolder == null)
                    _onBtnCreateFolder = new Base.CommandBase(CreateFolder, null);
                return _onBtnCreateFolder;
            }
        }

        public class worldJSON
        {
            public List<Pack> packs;
        }
        public class Pack
        {
            public string label;
            public string type;
            public string name;
            public string path;
            public string system;
            public string package;
            public string entity;
            public bool privat;
        }

        private void AddKompendium(string filename, string Kompendium, string Kompendiumtype)
        {
            string json;
            char A = (char)34;
            List<Pack> PackItems = new List<Pack>();
            worldJSON world = new worldJSON();
            using (StreamReader r = new StreamReader(filename))
            {
                json = r.ReadToEnd();
                json = json.Replace(A + "private" + A, A + "privat" + A);
                world = JsonConvert.DeserializeObject<worldJSON>(json);
            }
            world.packs.Remove( world.packs.Where(t => t.label == Kompendium).FirstOrDefault());
            Pack kampferPack = new Pack()                
            {
                label = Kompendium,
                type = Kompendiumtype,
                name = ErsetzeUmlaute(Kompendium),
                path = "packs/"+ ErsetzeUmlaute(Kompendium) + ".db",
                system = DSA41Version?"dsa-4.1": "dsa5",
                package = "world",
                entity = Kompendiumtype,
                privat = false
            };
            world.packs.Add(kampferPack);

            int start = json.IndexOf(A+"packs"+A);
            int end = json.IndexOf("]",start+6);
            string newJSON = json.Substring(0, start) + json.Substring(end+1);
            string jsonNewPacks = JsonConvert.SerializeObject(world).TrimStart((char)123).TrimEnd((char)125).Replace("privat","private");

            json = newJSON.Substring(0,start) + jsonNewPacks + newJSON.Substring(start);
            File.WriteAllText(filename, json);
        }

        private Base.CommandBase _onBtnExportGegner = null;
        public Base.CommandBase OnBtnExportGegner
        {
            get
            {
                if (_onBtnExportGegner == null)
                    _onBtnExportGegner = new Base.CommandBase(ExportGegner, null);
                return _onBtnExportGegner;
            }
        }
        private void ExportGegner(object sender)
        {
            try
            {
                if (SelectedWorld == null)
                {
                    ViewHelper.Popup("Wähle zuerst eine Welt");
                    return;
                }
                if ((SelectedGegnerFolder == null || SelectedGegnerFolder.name == "") &&
                    !ViewHelper.Confirm("Export der Gegner", "Die Gegner-Datenbank wird ins das Hauptverzeichnis exportiert\n" +
                    "Wir empfehlen hier dringend zuvor ein Verzeichnis zu erstellen, um Gegner, NSC und Helden zu separieren\n\nSollen die Gegner in das " +
                    "Hauptverzeichnis exportiert werden?"))
                    return;

                if (!ViewHelper.Confirm("Export von Daten zu FoundryVTT", "Um die Datenbank der FoundryVTT von extern zu ändern, muss die zu bearbeitende Welt abgemeldet sein!\n"+
                    "Ist dies nicht der Fall, werden Änderung nicht wirksam.\n\nSoll der Vorgang fortgesetzt werden?")) return;

                System.Windows.Input.Mouse.SetCursor(System.Windows.Input.Cursors.Wait);
                MyTimer.start_timer();
                GetGegnerData();

                List<string> lstErsetzt = new List<string>();
                string A = (new Char[] { '"' }).ToString();

                string actorsPath = null;
                if (IsGegnerInKompendium)
                {
                    if (string.IsNullOrEmpty(GegnerKompendium))
                        GegnerKompendium = ViewHelper.InputDialog("Name des Kompendiums", "Das Kompendium hat noch keinen Namen.\n\nWie soll das Kompendium heißen?", GegnerKompendium);

                    if (string.IsNullOrEmpty(GegnerKompendium))
                        return;
                    string worldsFile = IsLocalInstalliert ?
                        FoundryPfad + @"worlds\" + SelectedWorld + @"\world.json" :
                        FoundryPfad + @"worlds/" + SelectedWorld + @"/world.json";
                    AddKompendium(worldsFile, GegnerKompendium, "Actor");

                    actorsPath = IsLocalInstalliert ?
                        FoundryPfad + @"worlds\" + SelectedWorld + @"\packs\" + ErsetzeUmlaute(GegnerKompendium) + ".db" :
                        FoundryPfad + @"worlds/" + SelectedWorld + @"/packs/" + ErsetzeUmlaute(GegnerKompendium) + ".db";
                }
                else
                {
                    actorsPath = IsLocalInstalliert ?
                        FoundryPfad + @"worlds\" + SelectedWorld + @"\data\actors.db" :
                        FoundryPfad + @"worlds/" + SelectedWorld + @"/data/actors.db";
                }
                string FileData = GetFileData(actorsPath);
                if (IsGegnerInKompendium && FileData == null)
                {
                    if (IsLocalInstalliert)
                        using (StreamWriter sw = File.CreateText(actorsPath)) { }
                    else
                    { }
                    FileData = "";
                }
                if (FileData != null)
                {
                    List<string> lstFileData = FileData.Split(new Char[] { '\n' }).ToList(); //oldFileData
                    string newFileDataAdd = "";
                    //Check Gegner vorhanden -> Ja = Zeile löschen und ersetzen
                    foreach (GegnerArgument garg in lstGegnerArgument)
                    {
                        Nullable<int> pos = lstFileData.IndexOf(lstFileData.Where(t => t.Contains("name\":\"" + garg.g.Name + "\",")).FirstOrDefault());
                        if (pos != null && pos != -1)
                        {
                            lstFileData.RemoveAt(pos.Value);
                            lstErsetzt.Add(garg.g.Name);
                        }
                        newFileDataAdd += "\n" + garg.outcome;
                    }
                    string newFile = string.Join("\n", lstFileData);
                    newFile += newFileDataAdd;

                    //Gegner einfügen
                    SetFileData(actorsPath, newFile);
                    MyTimer.stop_timer("");
                    System.Windows.Input.Mouse.SetCursor(System.Windows.Input.Cursors.Arrow);
                    ViewHelper.Popup(string.Format("Alle {0} Einträge wurden überprüft.\nEs wurden {1} aktualisiert und nach Foundry exportiert"+
                        (!IsGegnerInKompendium ? "" : "\n\nWenn Foundry bereits läuft muss die Welt evtl 1x angemeldet und wieder abgemeldet werden, bevor die Änderung sichtbar werden."),
                        lstGegnerArgument.Count, lstErsetzt.Count));
                }
                else
                {
                    System.Windows.Input.Mouse.SetCursor(System.Windows.Input.Cursors.Arrow);
                    ViewHelper.Popup(string.Format("Die Foundry Datenbank '{0}' konnte nicht gefunden werden.\n\n Diese Datei sollte unter folgendem Pfad sein:\n{1}",
                        IsGegnerInKompendium ? GegnerKompendium : "actors.db", actorsPath));
                }
            }
            finally
            {
                System.Windows.Input.Mouse.SetCursor(System.Windows.Input.Cursors.Arrow);
            }
        }

        private List<Model.Waffe> _waffeListe = new List<Waffe>();
        public List<Model.Waffe> WaffeListe
        {
            get { return _waffeListe; }
            set { Set(ref _waffeListe, value); }
        }

        private Base.CommandBase _onBtnExportWaffen = null;
        public Base.CommandBase OnBtnExportWaffen
        {
            get
            {
                if (_onBtnExportWaffen == null)
                    _onBtnExportWaffen = new Base.CommandBase(ExportWaffen, null);
                return _onBtnExportWaffen;
            }
        }

        private void ExportWaffen(object sender)
        {
            try
            {
                if (SelectedWorld == null)
                {
                    ViewHelper.Popup("Wähle zuerst eine Welt");
                    return;
                }
                System.Windows.Input.Mouse.SetCursor(System.Windows.Input.Cursors.Wait);


                if ((SelectedWaffenFolder == null || SelectedWaffenFolder.name == "") &&
                    !ViewHelper.Confirm("Export der Waffen", "Die Waffen werden ins das Hauptverzeichnis exportiert\n" +
                    "Wir empfehlen hier zuvor ein Verzeichnis zu erstellen, um alle Gegenstände zu separieren\n\nSollen die Waffen trotzdem in das" +
                    "Hauptverzeichnis exportiert werden?"))
                    return;

                if (!ViewHelper.Confirm("Export von Daten zu FoundryVTT", "Um die Datenbank der FoundryVTT von extern zu ändern, muss die zu bearbeitende Welt abgemeldet sein!\n" +
                    "Ist dies nicht der Fall, werden Änderung nicht wirksam.\n\nSoll der Vorgang fortgesetzt werden?"))
                    return;


                System.Windows.Input.Mouse.SetCursor(System.Windows.Input.Cursors.Wait);

                if (!DSA41Version)
                    GetWaffenData_DSA50();
                else
                    GetWaffenData_DSA41();

                List<string> lstErsetzt = new List<string>();
                string A = (new Char[] { '"' }).ToString();

                string actorsPath = null;
                if (IsWaffenInKompendium)
                {
                    if (string.IsNullOrEmpty(WaffenKompendium))
                        WaffenKompendium = ViewHelper.InputDialog("Name des Kompendiums", "Das Kompendium hat noch keinen Namen.\n\nWie soll das Kompendium heißen?", WaffenKompendium);

                    if (string.IsNullOrEmpty(WaffenKompendium))
                        return;
                    string worldsFile = IsLocalInstalliert ?
                        FoundryPfad + @"worlds\" + SelectedWorld + @"\world.json" :
                        FoundryPfad + @"worlds/" + SelectedWorld + @"/world.json";
                    AddKompendium(worldsFile, WaffenKompendium, "Item");

                    actorsPath = IsLocalInstalliert ?
                        FoundryPfad + @"worlds\" + SelectedWorld + @"\packs\" + ErsetzeUmlaute(WaffenKompendium) + ".db" :
                        FoundryPfad + @"worlds/" + SelectedWorld + @"/packs/" + ErsetzeUmlaute(WaffenKompendium) + ".db";
                }
                else
                {
                    //Open actors.db
                    actorsPath = IsLocalInstalliert ?
                    FoundryPfad + @"worlds\" + SelectedWorld + @"\data\items.db" :
                    FoundryPfad + @"worlds/" + SelectedWorld + @"/data/items.db";
                }
                string FileData = GetFileData(actorsPath);
                if (IsWaffenInKompendium && FileData == null)
                {
                    if (IsLocalInstalliert)
                        using (StreamWriter sw = File.CreateText(actorsPath)) { }
                    else
                    { }
                    FileData = "";
                }
                if (FileData != null)
                {
                    List<string> lstFileData = FileData.Split(new Char[] { '\n' }).ToList();
                    string newFileDataAdd = "";
                    //Check Held vorhanden -> Ja = Zeile löschen und ersetzen
                    foreach (GegenstandArgument dbArg in lstWaffenArgument)
                    {
                        Nullable<int> pos = lstFileData.IndexOf(lstFileData.Where(t => t.Contains("name\":\"" + dbArg.name + "\",")).FirstOrDefault());
                        if (pos != null && pos != -1)
                        {
                            lstFileData.RemoveAt(pos.Value);
                            lstErsetzt.Add(dbArg.name);
                        }
                        newFileDataAdd += "\n" + dbArg.outcome;
                    }
                    string newFile = string.Join("\n", lstFileData);
                    newFile += newFileDataAdd;

                    //Waffen einfügen
                    SetFileData(actorsPath, newFile);
                    System.Windows.Input.Mouse.SetCursor(System.Windows.Input.Cursors.Arrow);
                    ViewHelper.Popup(string.Format("Alle {0} Einträge wurden überprüft.\nEs wurden {1} aktualisiert und nach Foundry exportiert" +
                        (!IsWaffenInKompendium?"":"\n\nWenn Foundry bereits läuft muss die Welt evtl 1x angemeldet und wieder abgemeldet werden, bevor die Änderung sichtbar werden."),
                        lstWaffenArgument.Count, lstErsetzt.Count));
                }
                else
                {
                    System.Windows.Input.Mouse.SetCursor(System.Windows.Input.Cursors.Arrow);
                    ViewHelper.Popup(string.Format("Die Foundry Datenbank '{0}' konnte nicht gefunden werden.\n\n Diese Datei sollte unter folgendem Pfad sein:\n{1}",
                        IsWaffenInKompendium ? WaffenKompendium : "items.db", actorsPath));
                }
            }
            finally
            {
                System.Windows.Input.Mouse.SetCursor(System.Windows.Input.Cursors.Arrow);
            }
        }

        private Base.CommandBase _onBtnExportHelden = null;
        public Base.CommandBase OnBtnExportHelden
        {
            get
            {
                if (_onBtnExportHelden == null)
                    _onBtnExportHelden = new Base.CommandBase(ExportHelden, null);
                return _onBtnExportHelden;
            }
        }

        private void ExportHelden(object sender)
        {
            try
            {
                if (SelectedWorld == null)
                {
                    ViewHelper.Popup("Wähle zuerst eine Welt");
                    return;
                }
                if ((SelectedHeldenFolder == null || SelectedHeldenFolder.name == "") &&
                    !ViewHelper.Confirm("Export der Helden", "Die Helden werden ins das Hauptverzeichnis exportiert\n" +
                    "Wir empfehlen hier zuvor ein Verzeichnis zu erstellen, um Helden, NSC und Gegner zu separieren\n\nSollen die Helden in das" +
                    "Hauptverzeichnis exportiert werden?"))
                    return;

                if (!ViewHelper.Confirm("Export von Daten zu FoundryVTT", "Um die Datenbank der FoundryVTT von extern zu ändern, muss die zu bearbeitende Welt abgemeldet sein!\n" +
                    "Ist dies nicht der Fall, werden Änderung nicht wirksam.\n\nSoll der Vorgang fortgesetzt werden?"))
                    return;


                System.Windows.Input.Mouse.SetCursor(System.Windows.Input.Cursors.Wait);
                MyTimer.start_timer();
                GetHeldenData();
                //Init();

                List<string> lstErsetzt = new List<string>();
                string A = (new Char[] { '"' }).ToString();

                //Open actors.db
                string actorsPath = null;
                if (IsHeldenInKompendium)
                {
                    if (string.IsNullOrEmpty(HeldenKompendium))
                        HeldenKompendium = ViewHelper.InputDialog("Name des Kompendiums", "Das Kompendium hat noch keinen Namen.\n\nWie soll das Kompendium heißen?", HeldenKompendium);

                    if (string.IsNullOrEmpty(HeldenKompendium))
                        return;
                    string worldsFile = IsLocalInstalliert ?
                        FoundryPfad + @"worlds\" + SelectedWorld + @"\world.json" :
                        FoundryPfad + @"worlds/" + SelectedWorld + @"/world.json";
                    AddKompendium(worldsFile, HeldenKompendium, "Actor");

                    actorsPath = IsLocalInstalliert ?
                        FoundryPfad + @"worlds\" + SelectedWorld + @"\packs\" + ErsetzeUmlaute(HeldenKompendium) + ".db" :
                        FoundryPfad + @"worlds/" + SelectedWorld + @"/packs/" + ErsetzeUmlaute(HeldenKompendium) + ".db";
                }
                else
                {
                    actorsPath = IsLocalInstalliert ?
                        FoundryPfad + @"worlds\" + SelectedWorld + @"\data\actors.db" :
                        FoundryPfad + @"worlds/" + SelectedWorld + @"/data/actors.db";
                }
                string FileData = GetFileData(actorsPath);
                if (IsHeldenInKompendium && FileData == null)
                {
                    if (IsLocalInstalliert)
                        using (StreamWriter sw = File.CreateText(actorsPath))
                        { }
                    else
                    { }
                    FileData = "";
                }
                if (FileData != null)
                {
                    List<string> lstFileData = FileData.Split(new Char[] { '\n' }).ToList();
                    string newFileDataAdd = "";
                    //Check Held vorhanden -> Ja = Zeile löschen und ersetzen
                    foreach (HeldenArgument harg in lstHeldArgument)
                    {
                        Nullable<int> pos = lstFileData.IndexOf(lstFileData.Where(t => t.Contains("name\":\"" + harg.h.Name + "\",")).FirstOrDefault());
                        if (pos != null && pos != -1)
                        {
                            lstFileData.RemoveAt(pos.Value);
                            lstErsetzt.Add(harg.h.Name);
                        }
                        newFileDataAdd += "\n" + harg.outcome;
                    }
                    string newFile = string.Join("\n", lstFileData);
                    newFile += newFileDataAdd;
                    newFile = newFile.TrimEnd(new Char[] { ',' });
                    //Held einfügen
                    SetFileData(actorsPath, newFile);
                    MyTimer.stop_timer("");
                    System.Windows.Input.Mouse.SetCursor(System.Windows.Input.Cursors.Arrow);
                    ViewHelper.Popup(MyTimer.stop_timer("Refresh Helden-Daten aktualisiert in") + "\n\nAlle Helden wurden eingefügt.\n" +
                        "Folgende Helden wurden ersetzt:\n" + (lstErsetzt.Count > 0 ? string.Join("\n", lstErsetzt.Select(t => "  * " + t).ToList()) : "") +
                    (!IsHeldenInKompendium ? "" : "\n\nWenn Foundry bereits läuft muss die Welt evtl 1x angemeldet und wieder abgemeldet werden, bevor die Änderung sichtbar werden."));
                }
                else
                {
                    System.Windows.Input.Mouse.SetCursor(System.Windows.Input.Cursors.Arrow);
                    ViewHelper.Popup(string.Format("Die Foundry Datenbank '{0}' konnte nicht gefunden werden.\n\n Diese Datei sollte unter folgendem Pfad sein:\n{1}",
                        IsHeldenInKompendium ? HeldenKompendium : "actors.db", actorsPath));
                }
            }
            finally
            {
                System.Windows.Input.Mouse.SetCursor(System.Windows.Input.Cursors.Arrow);
            }
        }

        public static void MakeFTPDir(string ftpAddress, string pathToCreate, string login, string password, byte[] fileContents, string ftpProxy = null)
        {
            FtpWebRequest reqFTP = null;
            Stream ftpStream = null;

            string[] subDirs = pathToCreate.Split('/');

            string currentDir = string.Format("ftp://{0}", ftpAddress);

            foreach (string subDir in subDirs)
            {
                try
                {
                    currentDir = currentDir + "/" + subDir;
                    reqFTP = (FtpWebRequest)FtpWebRequest.Create(currentDir);
                    reqFTP.Method = WebRequestMethods.Ftp.MakeDirectory;
                    reqFTP.UseBinary = true;
                    reqFTP.Credentials = new NetworkCredential(login, password);
                    FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                    ftpStream = response.GetResponseStream();
                    ftpStream.Close();
                    response.Close();
                }
                catch (Exception ex)
                {
                    //directory already exist I know that is weak but there is no way to check if a folder exist on ftp...
                }
            }
        }

        public static bool DeleteFileOnFtpServer(Uri serverUri, string ftpUsername, string ftpPassword)
        {
            try
            {
                // The serverUri parameter should use the ftp:// scheme.
                // It contains the name of the server file that is to be deleted.
                // Example: ftp://contoso.com/someFile.txt.
                // 

                if (serverUri.Scheme != Uri.UriSchemeFtp)
                {
                    return false;
                }
                // Get the object used to communicate with the server.
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(serverUri);
                request.Credentials = new NetworkCredential(ftpUsername, ftpPassword);
                request.Method = WebRequestMethods.Ftp.DeleteFile;

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                //Console.WriteLine("Delete status: {0}", response.StatusDescription);
                response.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private Base.CommandBase _onBtnExportPlaylists = null;
        public Base.CommandBase OnBtnExportPlaylists
        {
            get
            {
                if (_onBtnExportPlaylists == null)
                    _onBtnExportPlaylists = new Base.CommandBase(ExportPlaylists, null);
                return _onBtnExportPlaylists;
            }
        }
        private void ExportPlaylists(object sender)
        {
            if (SelectedWorld == null)
            {
                ViewHelper.Popup("Wähle zuerst eine Welt");
                return;
            }
            if (!ViewHelper.Confirm("Export von Daten zu FoundryVTT", "Um die Datenbank der FoundryVTT von extern zu ändern, muss die zu bearbeitende Welt abgemeldet sein!\n" +
                "Ist dies nicht der Fall, werden Änderung nicht wirksam.\n\nSoll der Vorgang fortgesetzt werden?"))
                return;

            Global.SetIsBusy(true, string.Format("Export der Playlisten..."));
            System.Windows.Input.Mouse.SetCursor(System.Windows.Input.Cursors.AppStarting);
            MyTimer.start_timer();
            System.Globalization.CultureInfo en = new System.Globalization.CultureInfo("en-US", false);
            int anzTotalTitel = 0;

            if (IsLocalInstalliert)
            {
                if (!Directory.Exists(string.Format(@"{0}{1}", FoundryPfad, MusikPfad)))
                    Directory.CreateDirectory(string.Format(@"{0}{1}", FoundryPfad, MusikPfad));
            }
            else
            {
                MakeFTPDir(FoundryPfad, MusikPfad, FTPUser, FTPPasswort, null, null);
            } 

            //Daten zusammenstellen
            List<PlaylistArgument> lstPArg = new List<PlaylistArgument>();
            string GMid = GetUserID(string.Format(@"{0}worlds\{1}\data\users.db", FoundryPfad, SelectedWorld), "Gamemaster");

            PlaylistStatus = "Alle Playlisten werden exportiert ...";
            Global.SetIsBusy(true, PlaylistStatus);
            foreach (Audio_Playlist aPlaylist in lstPlaylists)
            {
                PlaylistStatus = string.Format("Export '{0}' ...", aPlaylist.Name);
                Global.SetIsBusy(true, PlaylistStatus);
                int anzTitel = aPlaylist.Audio_Playlist_Titel.Count;
                PlaylistArgument pArg = new PlaylistArgument();
                char A = (char)34; // (new Char[] { '"' });
                                   //4e1d3250-f700-3000-0001-387712958942  => 0001387712958942
                pArg.GMid = GMid;
                pArg._id = aPlaylist.Audio_PlaylistGUID.ToString().Substring(19, 17).Replace("-", "");
                pArg.name = aPlaylist.Name;
                pArg.permission = A + "default" + A + ":0," + A + GMid + A + ":3";
                pArg.sort = 100001;
                pArg.mode = 1;
                pArg.playing = "false";

                int current = 1;
                pArg.lstSounds = new List<PlaylistArgument.SoundArg>();
                //List<Audio_Playlist_Titel> lstTitel = new List<Audio_Playlist_Titel>();
                foreach (Audio_Playlist_Titel aTitel in aPlaylist.Audio_Playlist_Titel.OrderBy(t => t.Audio_Titel.Name))
                {
                    string MGpathFile = aTitel.Audio_Titel.Pfad + @"\" + aTitel.Audio_Titel.Datei;
                    if (!File.Exists(MGpathFile))
                        continue;

                    PlaylistStatus = string.Format("Export '{0}' {1} of {2} ...", aPlaylist.Name, current, anzTitel);
                    PlaylistArgument.SoundArg sArg = new PlaylistArgument.SoundArg();
                    sArg.lstArg = new List<string>();
                    string aTitelTeilGuid = aTitel.Audio_TitelGUID.ToString().Substring(19, 17).Replace("-", "");
                    sArg.lstArg.Add("{" + A + "_id" + A + ":" + A + aTitelTeilGuid + A);
                    sArg.lstArg.Add(A + "flags" + A + ":{}");

                    stdPfad.ForEach((Action<string>)delegate (string s)
                    {
                        if (MGpathFile.StartsWith(s))
                            MGpathFile = MGpathFile.Replace(s, (string)this.MusikPfad);
                    });
                    string zielPfad = FoundryPfad + MGpathFile;
                    MGpathFile = MGpathFile.Replace(@"\", "/");
                    if (!MGpathFile.StartsWith(MusikPfad))
                        continue;

                    sArg.lstArg.Add(A + "path" + A + ":" + A + MGpathFile + A);
                    sArg.lstArg.Add(A + "repeat" + A + ":" + "false");
                    sArg.lstArg.Add(A + "volume" + A + ":" + ((decimal)(aTitel.Volume / 100)).ToString(en));
                    sArg.lstArg.Add(A + "name" + A + ":" + A + System.IO.Path.GetFileNameWithoutExtension(aTitel.Audio_Titel.Datei) + A);
                    sArg.lstArg.Add(A + "playing" + A + ":false");
                    sArg.lstArg.Add(A + "streaming" + A + ":false}");
                    sArg.lstTitel = string.Join(",", sArg.lstArg);
                    pArg.lstSounds.Add(sArg);

                    if (CopyTitelFile && !File.Exists(zielPfad))
                    {
                        if (!Directory.Exists(Path.GetDirectoryName(zielPfad)))
                            Directory.CreateDirectory(Path.GetDirectoryName(zielPfad));
                        File.Copy(aTitel.Audio_Titel.Pfad + @"\" + aTitel.Audio_Titel.Datei, zielPfad, false);

                    }
                    current++;
                    anzTotalTitel++;
                }
                lstPArg.Add(pArg);
            }
            //Daten zusammenstellen ENDE


            string playlistsFile = null;
            if (IsPlaylistsInKompendium)
            {
                if (string.IsNullOrEmpty(PlaylistsKompendium))
                    PlaylistsKompendium = ViewHelper.InputDialog("Name des Kompendiums", "Das Kompendium hat noch keinen Namen.\n\nWie soll das Kompendium heißen?", PlaylistsKompendium);

                if (string.IsNullOrEmpty(PlaylistsKompendium))
                    return;
                string worldsFile = IsLocalInstalliert ?
                    FoundryPfad + @"worlds\" + SelectedWorld + @"\world.json" :
                    FoundryPfad + @"worlds/" + SelectedWorld + @"/world.json";
                AddKompendium(worldsFile, PlaylistsKompendium, "Playlist");

                playlistsFile = IsLocalInstalliert ?
                    FoundryPfad + @"worlds\" + SelectedWorld + @"\packs\" + ErsetzeUmlaute(PlaylistsKompendium) + ".db" :
                    FoundryPfad + @"worlds/" + SelectedWorld + @"/packs/" + ErsetzeUmlaute(PlaylistsKompendium) + ".db";
            }
            else
            {
                playlistsFile = IsLocalInstalliert ?
                    FoundryPfad + @"worlds\" + SelectedWorld + @"\data\playlists.db" :
                    FoundryPfad + @"worlds/" + SelectedWorld + @"/data/playlists.db";
            }

            if (!IsLocalInstalliert)
                playlistsFile = playlistsFile.Replace(@"\", "/");
            if (OverwritePlaylistFile)
            {
                if (IsLocalInstalliert)
                    File.Delete(playlistsFile);
                else
                    DeleteFileOnFtpServer(new Uri(playlistsFile.Replace(@"\", "/")), FTPUser, FTPPasswort);
            }

            Global.SetIsBusy(true, "Daten werden zusammengestellt");
            List<string> lstErsetzt = new List<string>();
            string FileData = GetFileData(playlistsFile);

            if (IsPlaylistsInKompendium && FileData == null)
            {
                if (IsLocalInstalliert)
                    using (StreamWriter sw = File.CreateText(playlistsFile))
                    { }
                else
                { }
                FileData = "";
            }
            if (FileData != null)
            {
                List<string> lstFileData = FileData.Split(new Char[] { '\n' }).ToList(); //oldFileData
                string newFileDataAdd = "";
                //Check Playlist vorhanden -> Ja = Zeile löschen und ersetzen
                int i = 1;
                foreach (PlaylistArgument parg in lstPArg)
                {
                    Global.SetIsBusy(true, "Check auf vorhandene Playlisten " + i);
                    Nullable<int> pos = lstFileData.IndexOf(lstFileData.Where(t => t.Contains("name\":\"" + parg.name + "\",")).FirstOrDefault());
                    if (pos != null && pos != -1)
                    {
                        lstFileData.RemoveAt(pos.Value);
                        lstErsetzt.Add(parg.name);
                    }
                    newFileDataAdd += "\n" + parg.outtext;
                    i++;
                }
                string newFile = string.Join("\n", lstFileData);
                newFile += newFileDataAdd;

                Global.SetIsBusy(true, "Speichern der Playlisten");
                //Playlist einfügen
                SetFileData(playlistsFile, newFile);
                MyTimer.stop_timer("");
                System.Windows.Input.Mouse.SetCursor(System.Windows.Input.Cursors.Arrow);
                Global.SetIsBusy(false);
                ViewHelper.Popup(string.Format("Alle {0} Einträge wurden überprüft. Es wurden {1} aktualisiert und nach Foundry exportiert" +
                    (!IsPlaylistsInKompendium ? "" : "\n\nWenn Foundry bereits läuft muss die Welt evtl 1x angemeldet und wieder abgemeldet werden, bevor die Änderung sichtbar werden."),
                    lstPArg.Count, lstErsetzt.Count));
            }
            else
            {
                Global.SetIsBusy(false);
                System.Windows.Input.Mouse.SetCursor(System.Windows.Input.Cursors.Arrow);
                ViewHelper.Popup(string.Format("Die Foundry Datenbank '{0}' konnte nicht gefunden werden.\n\n Diese Datei sollte unter folgendem Pfad sein:\n{1}",
                    IsPlaylistsInKompendium ? PlaylistsKompendium : "playlists.db", playlistsFile));
                PlaylistStatus = null;
                return;
            }
            Global.SetIsBusy(false);

            PlaylistStatus = null;
            string stop = MyTimer.stop_timer("");
            PopUp(string.Format("Playlisten erfolgreich übertragen.\nEs wurden {0} in {1} übertragen.", anzTotalTitel, stop));
        }

        #endregion
    }
}
