using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeisterGeister.Daten;
using System.Xml;
// Eigene Usings
//using MeisterGeister.Logic.General;
using MeisterGeister.Model;
using MeisterGeister.ViewModel.Helden.Logic;
using System.Text.RegularExpressions;
using MeisterGeister.Logic.Extensions;

namespace MeisterGeister.Logic.HeldenImport
{
    /// <summary>
    /// Klasse zum Konvertieren von Helden der Helden-Software.
    /// </summary>
    public class HeldenSoftwareImporter
    {
        #region Mappings
        static HeldenSoftwareImporter()
        {
            // Talent-Mapping
            SetTalentMapping();

            // Zauber-Mapping
            SetZauberMapping();

            // VorNachteil-Mapping
            SetVorNachteilMapping();

            // Sonderfertigkeit-Mapping
            SetSonderfertigkeitenMapping();
            
            // Gegenstand-Mapping
            SetGegenstandMapping();
        }

        private static System.Collections.Generic.Dictionary<string, string> _talentMapping = new Dictionary<string, string>();
        private static System.Collections.Generic.Dictionary<string, string> _zauberMapping = new Dictionary<string, string>();
        private static System.Collections.Generic.Dictionary<string, string> _vorNachteilMapping = new Dictionary<string, string>();
        private static System.Collections.Generic.Dictionary<string, string> _sonderfertigkeitMapping = new Dictionary<string, string>();
        private static System.Collections.Generic.Dictionary<string, string> _gegenstandMapping = new Dictionary<string, string>();

        private static void SetGegenstandMapping()
        {
            _gegenstandMapping.Add("bart", "bart/halsberge");
            _gegenstandMapping.Add("beintaschen", "beintaschen/schürze");
            _gegenstandMapping.Add("fellumhang", "fellumhang/fuhrmannsmantel");
            _gegenstandMapping.Add("fuhrmannsmantel", "fellumhang/fuhrmannsmantel");
            _gegenstandMapping.Add("gambeson", "gambeson/wattierter waffenrock");
            _gegenstandMapping.Add("gänsekiel", "gänsekiel/griffel");
            _gegenstandMapping.Add("griffel", "gänsekiel/griffel");
            _gegenstandMapping.Add("gürteltasche, hartleder", "gürteltasche, verstärkt");
            _gegenstandMapping.Add("halsberge", "bart/halsberge");
            _gegenstandMapping.Add("hartholzharnisch", "maraskanischer hartholzharnisch");
            _gegenstandMapping.Add("jonglierbälle", "jonglierball");
            _gegenstandMapping.Add("lederweste", "lederweste/pelzweste");
            _gegenstandMapping.Add("linkhand (kling.br.)", "linkhand mit klingenbrecher");
            _gegenstandMapping.Add("linkhand und klingenbrecher", "linkhand mit klingenbrecher");
            _gegenstandMapping.Add("magierstab", "magierstab als stab");
            _gegenstandMapping.Add("magierstab (kristallkugel)", "magierstab m. kristallk.");
            _gegenstandMapping.Add("nägel, 50 stück", "50 nägel, sort.");
            _gegenstandMapping.Add("pelzweste", "lederweste/pelzweste");
            _gegenstandMapping.Add("satteltaschen", "satteltaschen (2 x 6 stein)");
            _gegenstandMapping.Add("schürze", "beintaschen/schürze");
            _gegenstandMapping.Add("stechhelm", "stechhelm/visierhelm");
            _gegenstandMapping.Add("visierhelm", "stechhelm/visierhelm");
            _gegenstandMapping.Add("wattiertes unterzeug", "wattiertes unterzeug/wattierte unterkleidung");
            _gegenstandMapping.Add("wattierte unterkleidung", "wattiertes unterzeug/wattierte unterkleidung");
            _gegenstandMapping.Add("wattierter waffenrock", "gambeson/wattierter waffenrock");
            _gegenstandMapping.Add("würfel, mammuton", "würfel, bein");
        }

        private static void SetSonderfertigkeitenMapping()
        {
            _sonderfertigkeitMapping.Add("akklimatisierung: hitze", "akklimatisierung (hitze)");
            _sonderfertigkeitMapping.Add("akklimatisierung: kälte", "akklimatisierung (kälte)");
            _sonderfertigkeitMapping.Add("fernzauberei", "fernzauberei i");
            _sonderfertigkeitMapping.Add("traumgänger", "traumgänger i");
            _sonderfertigkeitMapping.Add("apport", "objektritual: apport");
            _sonderfertigkeitMapping.Add("bannschwert", "objektritual: bannschwert");
            _sonderfertigkeitMapping.Add("elementarharmonisierte aura (humus/eis)", "elementarharmonisierte aura (eis/humus)");
            _sonderfertigkeitMapping.Add("elementarharmonisierte aura (luft/fels)", "elementarharmonisierte aura (fels/luft)");
            _sonderfertigkeitMapping.Add("elementarharmonisierte aura (wasser/feuer)", "elementarharmonisierte aura (feuer/wasser)");
            _sonderfertigkeitMapping.Add("kulturkunde (andergast/nostria)", "kulturkunde (andergast und nostria)");
            _sonderfertigkeitMapping.Add("kulturkunde (stammesachaz)", "kulturkunde (stammes-achaz)");
            _sonderfertigkeitMapping.Add("merkmalskenntnis: dämonisch", "merkmalskenntnis (dämonisch (gesamt))");
            _sonderfertigkeitMapping.Add("merkmalskenntnis: elementar", "merkmalskenntnis (elementar (gesamt))");
            _sonderfertigkeitMapping.Add("stabzauber: fackel", "stabzauber: ewige flamme");
            _sonderfertigkeitMapping.Add("stabzauber: seil", "stabzauber: seil des adepten");
            _sonderfertigkeitMapping.Add("stabzauber: stabverlängerung", "stabzauber: doppeltes maß/stabverlängerung");
            _sonderfertigkeitMapping.Add("repräsentation: achaz", "repräsentation (achaz-kristallomantisch)");
            _sonderfertigkeitMapping.Add("repräsentation: borbaradianer", "repräsentation (borbaradianisch)");
            _sonderfertigkeitMapping.Add("repräsentation: druide", "repräsentation (druidisch)");
            _sonderfertigkeitMapping.Add("repräsentation: elf", "repräsentation (elfisch)");
            _sonderfertigkeitMapping.Add("repräsentation: geode", "repräsentation (geodisch)");
            _sonderfertigkeitMapping.Add("repräsentation: hexe", "repräsentation (hexisch)");
            _sonderfertigkeitMapping.Add("repräsentation: magier", "repräsentation (gildenmagisch)");
            _sonderfertigkeitMapping.Add("repräsentation: scharlatan", "repräsentation (scharlatanisch)");
            _sonderfertigkeitMapping.Add("repräsentation: schelm", "repräsentation (schelmisch)");
            _sonderfertigkeitMapping.Add("repräsentation: alhanisch", "repräsentation (alhanisch)");
            _sonderfertigkeitMapping.Add("repräsentation: druidisch-geodisch", "repräsentation (druidisch-geodisch)");
            _sonderfertigkeitMapping.Add("repräsentation: grolmisch", "repräsentation (grolmisch)");
            _sonderfertigkeitMapping.Add("repräsentation: güldenländisch", "repräsentation (güldenländisch)");
            _sonderfertigkeitMapping.Add("repräsentation: kophtanisch", "repräsentation (kophtanisch)");
            _sonderfertigkeitMapping.Add("repräsentation: mudramulisch", "repräsentation (mudramulisch)");
            _sonderfertigkeitMapping.Add("repräsentation: satuarisch", "repräsentation (satuarisch)");
            _sonderfertigkeitMapping.Add("ritualkenntnis: gildenmagie", "ritualkenntnis (gildenmagie)");
            _sonderfertigkeitMapping.Add("ritualkenntnis: kristallomantie", "ritualkenntnis (kristallomantie)");
            _sonderfertigkeitMapping.Add("ritualkenntnis: scharlatan", "ritualkenntnis (scharlatanerie)");
            _sonderfertigkeitMapping.Add("ritualkenntnis: alchimist", "ritualkenntnis (alchimie)");
            _sonderfertigkeitMapping.Add("ritualkenntnis: hexe", "ritualkenntnis (hexenmagie)");
            _sonderfertigkeitMapping.Add("ritualkenntnis: geode", "ritualkenntnis (geoden)");
            _sonderfertigkeitMapping.Add("ritualkenntnis: druide", "ritualkenntnis (druiden)");
            _sonderfertigkeitMapping.Add("ritualkenntnis: derwisch", "ritualkenntnis (derwische)");
            _sonderfertigkeitMapping.Add("ritualkenntnis: zaubertänzer", "ritualkenntnis (zaubertänze)");
            _sonderfertigkeitMapping.Add("ritualkenntnis: zaubertänzer (novadische sharisad)", "ritualkenntnis (zaubertänze)");
            _sonderfertigkeitMapping.Add("ritualkenntnis: zaubertänzer (tulamische sharisad)", "ritualkenntnis (zaubertänze)");
            _sonderfertigkeitMapping.Add("ritualkenntnis: zaubertänzer (majuna)", "ritualkenntnis (zaubertänze)");
            _sonderfertigkeitMapping.Add("ritualkenntnis: zaubertänzer (hazaqi)", "ritualkenntnis (zaubertänze)");
            _sonderfertigkeitMapping.Add("ritualkenntnis: zibilja", "ritualkenntnis (zibilja)");
            _sonderfertigkeitMapping.Add("keulenritual: apport der keule", "objektritual: apport");
            _sonderfertigkeitMapping.Add("kristallkraft bündeln", "kristallomantisches ritual: kristallkraft bündeln");
            _sonderfertigkeitMapping.Add("zaubertanz: el vanidad (tanz der ilder)", "zaubertanz: tanz der bilder");
            _sonderfertigkeitMapping.Add("zaubertanz: heschinjas blick (tanz der wahrheit)", "zaubertanz: tanz der wahrheit");
            _sonderfertigkeitMapping.Add("zaubertanz: hesindes macht (tanz der erlösung)", "zaubertanz: tanz der erlösung");
            _sonderfertigkeitMapping.Add("zaubertanz: khablas verlockung (tanz der liebe)", "zaubertanz: tanz der liebe");
            _sonderfertigkeitMapping.Add("zaubertanz: marhibos hand (tanz der erlösung)", "zaubertanz: tanz der erlösung");
            _sonderfertigkeitMapping.Add("zaubertanz: nahemas traum (tanz ohne ende)", "zaubertanz: tanz ohne ende");
            _sonderfertigkeitMapping.Add("zaubertanz: orhimas tanz (tanz der weisheit)", "zaubertanz: tanz der weisheit");
            _sonderfertigkeitMapping.Add("zaubertanz: pavonearse (tanz der ermutigung)", "zaubertanz: tanz der ermutigung");
            _sonderfertigkeitMapping.Add("zaubertanz: peraines liebe (tanz der freude)", "zaubertanz: tanz der freude");
            _sonderfertigkeitMapping.Add("zaubertanz: perhinas segen (tanz der freude)", "zaubertanz: tanz der freude");
            _sonderfertigkeitMapping.Add("zaubertanz: phexens geschmeide (tanz der bilder)", "zaubertanz: tanz der bilder");
            _sonderfertigkeitMapping.Add("zaubertanz: rahjarra (tanz der liebe)", "zaubertanz: tanz der liebe");
            _sonderfertigkeitMapping.Add("zaubertanz: rhondaras forderung (tanz der ermutigung)", "zaubertanz: tanz der ermutigung");
            _sonderfertigkeitMapping.Add("zaubertanz: rondras mut (tanz der ermutigung)", "zaubertanz: tanz der ermutigung");
            _sonderfertigkeitMapping.Add("zaubertanz: satinavs gabe (tanz ohne ende)", "zaubertanz: tanz ohne ende");
            _sonderfertigkeitMapping.Add("zaubertanz: shimijas rausch (tanz der bilder)", "zaubertanz: tanz der bilder");
            _sonderfertigkeitMapping.Add("zaubertanz: suenyo (tanz ohne ende)", "zaubertanz: tanz ohne ende");
            _sonderfertigkeitMapping.Add("zaubertanz: tanz für rastullah (tanz der unantastbarkeit)", "zaubertanz: tanz der unantastbarkeit");
            _sonderfertigkeitMapping.Add("zaubertanz: zarpada (tanz der erlösung)", "zaubertanz: tanz der erlösung");
            _sonderfertigkeitMapping.Add("ritualkenntnis: runenzauberei", "runenkunde");
            _sonderfertigkeitMapping.Add("meisterliche zauberkontrolle", "meisterliche zauberkontrolle i");
            _sonderfertigkeitMapping.Add("zauberzeichen: satinavs siegel", "zauberzeichen: zusatzzeichen satinavs siegel");
            _sonderfertigkeitMapping.Add("zauberzeichen: schutzsiegel", "zauberzeichen: zusatzzeichen schutzsiegel");
            _sonderfertigkeitMapping.Add("zauberzeichen: bannkreis gegen chimären", "zauberzeichen: bann- und schutzkreis gegen chimären");
            _sonderfertigkeitMapping.Add("zauberzeichen: bannkreis gegen traumgänger", "zauberzeichen: schutzkreis gegen traumgänger");
            _sonderfertigkeitMapping.Add("zauberzeichen: bann- und schutzkreis gegen elemente", "zauberzeichen: glyphe der elementaren bannung");
            _sonderfertigkeitMapping.Add("schlangenszepters: bindung", "schlangenszepter: bindung");
            _sonderfertigkeitMapping.Add("schlangenszepters: ruf der fliegenden schlange", "schlangenszepter: ruf der fliegenden schlange");
            _sonderfertigkeitMapping.Add("ritual: brazoragh ghorkai", "schamanenritual: brazoragh ghorkai - brazoraghs hieb");
            _sonderfertigkeitMapping.Add("ritual: ergochai tairachi", "schamanenritual: ergochai tairachi -tairachs sklaven");
            _sonderfertigkeitMapping.Add("ritual: khurkachai tairachi", "schamanenritual: khurkachai tairachi -tairachs krieger");
            _sonderfertigkeitMapping.Add("ritual: m'char utrak rikaii", "schamanenritual: m'char utrak rikaii - rikais alchimie");
            _sonderfertigkeitMapping.Add("liturgie: phexens kunstverstand (blick für das handwerk)", "liturgie: phexens kunstverstand");
            _sonderfertigkeitMapping.Add("liturgie: angroschs zorn (waliburias wehr)", "liturgie: waliburias wehr");
            _sonderfertigkeitMapping.Add("liturgie: des herren goldener mittag (weisung des himmels)", "liturgie: weisung des himmels");
            _sonderfertigkeitMapping.Add("liturgie: wort der wahrheit (heiliger befehl)", "liturgie: heiliger befehl");
            _sonderfertigkeitMapping.Add("liturgie: lug und trug (unverstellter blick)", "liturgie: lug und trug");
            _sonderfertigkeitMapping.Add("liturgie: rahjas erquickung (schlaf des gesegneten)", "liturgie: schlaf des gesegneten");
            _sonderfertigkeitMapping.Add("waffenloser kampfstil: mercenario", "waffenlose kampftechnik (mercenario/legionärsstil/söldnerstil)");
            _sonderfertigkeitMapping.Add("waffenloser kampfstil: legionärsstil", "waffenlose kampftechnik (mercenario/legionärsstil/söldnerstil)");
            _sonderfertigkeitMapping.Add("waffenloser kampfstil: söldnerstil", "waffenlose kampftechnik (mercenario/legionärsstil/söldnerstil)");
            _sonderfertigkeitMapping.Add("waffenloser kampfstil: unauer schule", "waffenlose kampftechnik (unauer schule/cyclopeisches ringen/echsenzwinger)");
            _sonderfertigkeitMapping.Add("waffenloser kampfstil: cyclopeisches ringen", "waffenlose kampftechnik (unauer schule/cyclopeisches ringen/echsenzwinger)");
            _sonderfertigkeitMapping.Add("waffenloser kampfstil: echsenzwinger", "waffenlose kampftechnik (unauer schule/cyclopeisches ringen/echsenzwinger)");
            _sonderfertigkeitMapping.Add("waffenloser kampfstil: bornländisch", "waffenlose kampftechnik (bornländisch/gossenstil)");
            _sonderfertigkeitMapping.Add("waffenloser kampfstil: gossenstil", "waffenlose kampftechnik (bornländisch/gossenstil)");
            _sonderfertigkeitMapping.Add("spätweihe ddz", "spätweihe dunkle zeiten");
        }

        private static void SetVorNachteilMapping()
        {
            _vorNachteilMapping.Add("adlige abstammung", "adlig (adlige abstammung)");
            _vorNachteilMapping.Add("adliges erbe", "adlig (adliges erbe)");
            _vorNachteilMapping.Add("amtsadel", "adlig (amtsadel)");
            _vorNachteilMapping.Add("begabung für [merkmal] dämonisch", "begabung für merkmal (dämonisch (gesamt))");
            _vorNachteilMapping.Add("begabung für [merkmal] elementar", "begabung für merkmal (elementar (gesamt))");
            _vorNachteilMapping.Add("begabung für [talent]", "begabung für talent");
            _vorNachteilMapping.Add("begabung für [ritual]", "begabung für ritual");
            _vorNachteilMapping.Add("begabung für [zauber]", "begabung für zauber");
            _vorNachteilMapping.Add("dschinngeboren (ohne vz)", "dschinngeboren");
            _vorNachteilMapping.Add("gutaussehend", "gut aussehend");
            _vorNachteilMapping.Add("unfähigkeit für [talentgruppe] körperlich", "unfähigkeit für talentgruppe (körper)");
            _vorNachteilMapping.Add("unfähigkeit für [talent]", "unfähigkeit für talent");
            _vorNachteilMapping.Add("herausragender sinn gehör", "herausragender sinn (gehör)");
            _vorNachteilMapping.Add("herausragender sinn geruchssinn", "herausragender sinn (geruchssinn)");
            _vorNachteilMapping.Add("herausragender sinn sicht", "herausragender sinn (sicht)");
            _vorNachteilMapping.Add("herausragender sinn tastsinn", "herausragender sinn (tastsinn)");
            _vorNachteilMapping.Add("magiedilletant", "magiedilettant");
            _vorNachteilMapping.Add("titularadel", "adlig (amtsadel)");
            _vorNachteilMapping.Add("wolfskind intuitiv", "wolfskind (intuitiv)");
            _vorNachteilMapping.Add("wolfskind wissentlich", "wolfskind (wissentlich)");
            _vorNachteilMapping.Add("schlafstörungen 1", "schlafstörungen i");
            _vorNachteilMapping.Add("schlafstörungen 2", "schlafstörungen ii");
            _vorNachteilMapping.Add("krankhafte nekromantie", "nekromantismus");
            _vorNachteilMapping.Add("gesucht 1", "gesucht i");
            _vorNachteilMapping.Add("gesucht 2", "gesucht ii");
            _vorNachteilMapping.Add("gesucht 3", "gesucht iii");
            _vorNachteilMapping.Add("unbewusster viertelzauberer ", "viertelzauberer (unbewusst)");
            _vorNachteilMapping.Add("eingeschränkter sinn gehör", "eingeschränkter sinn (schwerhörig)");
            _vorNachteilMapping.Add("eingeschränkter sinn geruchssinn", "eingeschränkter sinn (geruchssinn)");
            _vorNachteilMapping.Add("eingeschränkter sinn sicht", "eingeschränkter sinn (kurzsichtig)");
            _vorNachteilMapping.Add("eingeschränkter sinn tastsinn", "eingeschränkter sinn (tastsinn)");
            _vorNachteilMapping.Add("tolpatsch", "tollpatsch");
            _vorNachteilMapping.Add("weltfremd bzgl.", "weltfremd");
        }

        private static void SetZauberMapping()
        {
            _zauberMapping.Add("analys arkanstruktur", "analys arcanstruktur");
            _zauberMapping.Add("aquafaxius wasserstrahl", "aquafaxius");
            _zauberMapping.Add("archofaxius erzstrahl", "archofaxius");
            _zauberMapping.Add("arcanovi artefakt", "arcanovi artefakt (spruchspeicher)");
            _zauberMapping.Add("brenne toter stoff!", "brenne, toter stoff!");
            _zauberMapping.Add("chronoautos zeitenfahrt", "chrononautos zeitenfahrt");
            _zauberMapping.Add("eigenschaft wiederherstellen", "eigenschaften wiederherstellen");
            _zauberMapping.Add("frigifaxius eisstrahl", "frigifaxius");
            _zauberMapping.Add("frigisphaero eisball", "frigosphaero");
            _zauberMapping.Add("gletscherwand", "wand aus eis (gletscherwand)");
            _zauberMapping.Add("humofaxius humusstrahl", "humofaxius");
            _zauberMapping.Add("karnifilio raserei", "karnifilo raserei");
            _zauberMapping.Add("orcanofaxius luftstrahl", "orcanofaxius");
            _zauberMapping.Add("orcanosphaero orkanball", "orcanosphaero");
            _zauberMapping.Add("orkanwand", "wand aus luft (orkanwand)");
            _zauberMapping.Add("planastrale anderswelt", "planastrale anderwelt");
            _zauberMapping.Add("unsichtbare jäger", "unsichtbarer jäger");
            _zauberMapping.Add("weiße mähn und goldener huf", "weiße mähn' und gold'ner huf");
            _zauberMapping.Add("aquasphaero wasserball", "aquasphaero");
            _zauberMapping.Add("archosphaero erzball", "archosphaero");
            _zauberMapping.Add("humosphaero humusball", "humosphaero");
        }

        private static void SetTalentMapping()
        {
            _talentMapping.Add("zweihandhiebwaffen", "zweihand-hiebwaffen");
            _talentMapping.Add("fallen stellen", "fallenstellen");
            _talentMapping.Add("geografie", "geographie");
            _talentMapping.Add("götter und kulte", "götter/kulte");
            _talentMapping.Add("sagen und legenden", "sagen/legenden");
            _talentMapping.Add("sich verstecken", "sich verstecken");
            _talentMapping.Add("heilkunde: gift", "heilkunde gift");
            _talentMapping.Add("heilkunde: krankheiten", "heilkunde krankheiten");
            _talentMapping.Add("heilkunde: seele", "heilkunde seele");
            _talentMapping.Add("heilkunde: wunden", "heilkunde wunden");
            _talentMapping.Add("kartografie", "kartographie");
            _talentMapping.Add("sprachen kennen (aureliani)", "sprachen kennen (alt-imperial/alt-güldenländisch/aureliani)");
            _talentMapping.Add("sprachen kennen (alt-imperial/aureliani)", "sprachen kennen (alt-imperial/alt-güldenländisch/aureliani)");
            _talentMapping.Add("sprachen kennen (alt-imperial)", "sprachen kennen (alt-imperial/alt-güldenländisch/aureliani)");
            _talentMapping.Add("sprachen kennen (alt-güldenländisch)", "sprachen kennen (alt-imperial/alt-güldenländisch/aureliani)");
            _talentMapping.Add("sprachen kennen (urtulamidya)", "sprachen kennen (ur-tulamidya)");
            _talentMapping.Add("sprachen kennen (angramm)", "sprachen kennen (angram)");
            _talentMapping.Add("sprachen kennen (alt-zwergisch)", "sprachen kennen (alt-zwergisch/rhoglossa)");
            _talentMapping.Add("sprachen kennen (rhoglossa)", "sprachen kennen (alt-zwergisch/rhoglossa)");
            _talentMapping.Add("sprachen kennen (archäisch)", "sprachen kennen (archäisch/bashurisch)");
            _talentMapping.Add("sprachen kennen (bashurisch)", "sprachen kennen (archäisch/bashurisch)");
            _talentMapping.Add("sprachen kennen (boa'goram)", "sprachen kennen (boa'goram/banbarguinisch)");
            _talentMapping.Add("sprachen kennen (banbarguinisch)", "sprachen kennen (boa'goram/banbarguinisch)");
            _talentMapping.Add("sprachen kennen (bramscho)", "sprachen kennen (bramscho/baramunisch)");
            _talentMapping.Add("sprachen kennen (baramunisch)", "sprachen kennen (bramscho/baramunisch)");
            _talentMapping.Add("sprachen kennen (gemein-amaunal)", "sprachen kennen (gemein-amaunal/ahma)");
            _talentMapping.Add("sprachen kennen (ahma)", "sprachen kennen (gemein-amaunal/ahma)");
            _talentMapping.Add("sprachen kennen (gemein-vesayitisch)", "sprachen kennen (gemein-vesayitisch/vesayo)");
            _talentMapping.Add("sprachen kennen (vesayo)", "sprachen kennen (gemein-vesayitisch/vesayo)");
            _talentMapping.Add("sprachen kennen (hiero-amaunal)", "sprachen kennen (hiero-amaunal/ahmagao)");
            _talentMapping.Add("sprachen kennen (ahmagao)", "sprachen kennen (hiero-amaunal/ahmagao)");
            _talentMapping.Add("sprachen kennen (hjaldingsch)", "sprachen kennen (hjaldingsch/saga-thorwalsch)");
            _talentMapping.Add("sprachen kennen (saga-thorwalsch)", "sprachen kennen (hjaldingsch/saga-thorwalsch)");
            _talentMapping.Add("sprachen kennen (leonal)", "sprachen kennen (leonal/khorrzu)");
            _talentMapping.Add("sprachen kennen (khorrzu)", "sprachen kennen (leonal/khorrzu)");
            _talentMapping.Add("sprachen kennen (lish'shi)", "sprachen kennen (lish'shi/wolfalbisch)");
            _talentMapping.Add("sprachen kennen (wolfalbisch)", "sprachen kennen (lish'shi/wolfalbisch)");
            _talentMapping.Add("sprachen kennen (lyncal)", "sprachen kennen (lyncal/fhi'ai)");
            _talentMapping.Add("sprachen kennen (fhi'ai)", "sprachen kennen (lyncal/fhi'ai)");
            _talentMapping.Add("sprachen kennen (padiral)", "sprachen kennen (padiral/bhagrach)");
            _talentMapping.Add("sprachen kennen (bhagrach)", "sprachen kennen (padiral/bhagrach)");
            _talentMapping.Add("sprachen kennen (proto-imperial)", "sprachen kennen (proto-imperial/dorinthisch)");
            _talentMapping.Add("sprachen kennen (dorinthisch)", "sprachen kennen (proto-imperial/dorinthisch)");
            _talentMapping.Add("sprachen kennen (sumurrisch)", "sprachen kennen (sumurrisch/ur-bansumitisch)");
            _talentMapping.Add("sprachen kennen (ur-bansumitisch)", "sprachen kennen (sumurrisch/ur-bansumitisch)");
            _talentMapping.Add("sprachen kennen (tighral)", "sprachen kennen (tighral/tharr'orr)");
            _talentMapping.Add("sprachen kennen (tharr'orr)", "sprachen kennen (tighral/tharr'orr)");
            _talentMapping.Add("lesen/schreiben (baramun-keilschrift)", "lesen/schreiben (bramschoromk/baramun-keilschrift)");
            _talentMapping.Add("lesen/schreiben (bramschoromk)", "lesen/schreiben (bramschoromk/baramun-keilschrift)");
            _talentMapping.Add("lesen/schreiben (kalshinishi)", "lesen/schreiben (kalshinishi/shingwanische knotenschrift)");
            _talentMapping.Add("lesen/schreiben (shingwanische knotenschrift)", "lesen/schreiben (kalshinishi/shingwanische knotenschrift)");
            _talentMapping.Add("lesen/schreiben (imperiale zeichen)", "lesen/schreiben (imperiale zeichen/altgüldenländisch/alt-imperiale buchstaben)");
            _talentMapping.Add("lesen/schreiben ((alt-)imperiale zeichen)", "lesen/schreiben (imperiale zeichen/altgüldenländisch/alt-imperiale buchstaben)");
            _talentMapping.Add("lesen/schreiben (alt-imperiale buchstaben)", "lesen/schreiben (imperiale zeichen/altgüldenländisch/alt-imperiale buchstaben)");
            _talentMapping.Add("lesen/schreiben (altes amulashtra)", "lesen/schreiben (alt-amulashtra)");
            _talentMapping.Add("lesen/schreiben (hjaldingsche runenzeichen)", "lesen/schreiben (hjaldingsche runen)");
            _talentMapping.Add("lesen/schreiben (alt-vesayitisch)", "lesen/schreiben (vesayitische wort- und silbenzeichen)");
            _talentMapping.Add("lesen/schreiben (chrmk)", "lesen/schreiben (chrmk/zelemja)");
            _talentMapping.Add("lesen/schreiben (zelemja)", "lesen/schreiben (chrmk/zelemja)");
            _talentMapping.Add("lesen/schreiben (chuchas)", "lesen/schreiben (chuchas/yash-hualay-glyphen/protozelemja)");
            _talentMapping.Add("lesen/schreiben (yash-hualay-glyphen)", "lesen/schreiben (chuchas/yash-hualay-glyphen/protozelemja)");
            _talentMapping.Add("lesen/schreiben (protozelemja)", "lesen/schreiben (chuchas/yash-hualay-glyphen/protozelemja)");
            _talentMapping.Add("lesen/schreiben (gimaril-glyphen)", "lesen/schreiben (gimaril)");
            _talentMapping.Add("lesen/schreiben (urtulamidya)", "lesen/schreiben (ur-tulamidya)");
            _talentMapping.Add("lesen/schreiben (angramm)", "lesen/schreiben (angram)");
            _talentMapping.Add("lesen/schreiben (isdira/asdharia)", "lesen/schreiben (isdira)");
            _talentMapping.Add("ritualkenntnis: gildenmagie", "ritualkenntnis (gildenmagie)");
            _talentMapping.Add("ritualkenntnis: kristallomantie", "ritualkenntnis (kristallomantie)");
            _talentMapping.Add("ritualkenntnis: scharlatan", "ritualkenntnis (scharlatanerie)");
            _talentMapping.Add("ritualkenntnis: alchimist", "ritualkenntnis (alchimie)");
            _talentMapping.Add("ritualkenntnis: hexe", "ritualkenntnis (hexenmagie)");
            _talentMapping.Add("ritualkenntnis: geode", "ritualkenntnis (geoden)");
            _talentMapping.Add("ritualkenntnis: druide", "ritualkenntnis (druiden)");
            _talentMapping.Add("ritualkenntnis: derwisch", "ritualkenntnis (derwische)");
            _talentMapping.Add("ritualkenntnis: durro-dûn", "ritualkenntnis (durro-dûn)");
            _talentMapping.Add("ritualkenntnis: zaubertänzer", "ritualkenntnis (zaubertänze)");
            _talentMapping.Add("ritualkenntnis: zaubertänzer (novadische sharisad)", "ritualkenntnis (zaubertänze)");
            _talentMapping.Add("ritualkenntnis: zaubertänzer (tulamische sharisad)", "ritualkenntnis (zaubertänze)");
            _talentMapping.Add("ritualkenntnis: zaubertänzer (majuna)", "ritualkenntnis (zaubertänze)");
            _talentMapping.Add("ritualkenntnis: zaubertänzer (hazaqi)", "ritualkenntnis (zaubertänze)");
            _talentMapping.Add("ritualkenntnis: zibilja", "ritualkenntnis (zibilja)");
            _talentMapping.Add("ritualkenntnis: runenzauberei", "ritualkenntnis (runenzauberei)");
            _talentMapping.Add("ritualkenntnis: güldenländisch", "ritualkenntnis (güldenländisch)");
            _talentMapping.Add("ritualkenntnis: alhanisch", "ritualkenntnis (alhanisch)");
            _talentMapping.Add("ritualkenntnis: kophtanisch", "ritualkenntnis (kophtanisch)");
            _talentMapping.Add("ritualkenntnis: mudramulisch", "ritualkenntnis (mudramulisch)");
            _talentMapping.Add("ritualkenntnis: satuarisch", "ritualkenntnis (satuarisch)");
            _talentMapping.Add("ritualkenntnis: tapasuul", "ritualkenntnis (tapasuul)");
            _talentMapping.Add("brettspiel", "brett-/kartenspiel");
        }
        #endregion

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

        public static void ImportHeldenSoftwareFile(string _importPfad)
        {
            ImportHeldenSoftwareFile(_importPfad, Guid.Empty);
        }

        public static Held ImportHeldenSoftwareFile(string _importPfad, Guid newGuid)
        {
            System.Collections.Generic.List<string> _importLog = new List<string>();
            XmlDocument _xmlDoc = new XmlDocument();
            _xmlDoc.Load(_importPfad);

            string name = null;
            Guid heldGuid = Guid.Empty;
            var held = _xmlDoc.GetElementsByTagName("held");
            if (held.Count == 1)
            {
                // Name
                name = held[0].Attributes["name"].Value.Trim();
                if (held[0].Attributes["key"] != null)
                {
                    string key = held[0].Attributes["key"].Value.Trim();
                    heldGuid = KeyToGuid(key);
                }
            }
            else // keine oder mehrere Helden in Datei
                return null;

            Held _held = new Held();
            if (newGuid != Guid.Empty)
                heldGuid = newGuid;
            _held.HeldGUID = heldGuid;
            _held.Name = name;

            // TODO: Regelsystem evtl. aus der XML-Datei ermitteln
            if (string.IsNullOrEmpty(_held.Regelsystem)) // falls keine Regeledition gesetzt, DSA 4.1 annehmen
                _held.Regelsystem = "DSA 4.1";

            // Rasse
            XmlNodeList rassen = _xmlDoc.SelectNodes("helden/held/basis/rasse");
            if (rassen.Count == 0)
                rassen = _xmlDoc.SelectNodes("helden/held/rasse");

            foreach (XmlNode rasse in rassen)
            {
                if (_held.Rasse == null)
                    _held.Rasse = string.Empty;
                if (_held.Rasse != string.Empty)
                    _held.Rasse += ", ";
                _held.Rasse += rasse.Attributes["string"].Value;
            }
            // Kultur
            XmlNodeList kulturen = _xmlDoc.SelectNodes("helden/held/basis/kultur");
            if (kulturen.Count == 0)
                kulturen = _xmlDoc.SelectNodes("helden/held/kultur");

            foreach (XmlNode kultur in kulturen)
            {
                if (_held.Kultur == null)
                    _held.Kultur = string.Empty;
                if (_held.Kultur != string.Empty)
                    _held.Kultur += ", ";
                _held.Kultur += kultur.Attributes["string"].Value;
            }
            // Profession
            XmlNodeList professionen = _xmlDoc.SelectNodes("helden/held/basis/ausbildungen/ausbildung");
            if (professionen.Count == 0)
                professionen = _xmlDoc.SelectNodes("helden/held/ausbildungen/ausbildung");

            foreach (XmlNode prof in professionen)
            {
                if (_held.Profession == null)
                    _held.Profession = string.Empty;
                if (_held.Profession != string.Empty)
                    _held.Profession += " / ";
                _held.Profession += prof.Attributes["string"].Value;
                if (professionen.Count > 1)
                    _held.Profession += " (" + prof.Attributes["art"].Value + ")";
            }

            // Abenteuerpunkte
            XmlNode ap = _xmlDoc.SelectSingleNode("helden/held/basis/abenteuerpunkte");
            if (ap == null)
                ap = _xmlDoc.SelectSingleNode("helden/held/abenteuerpunkte");
            if (ap != null)
                _held.APGesamt = Convert.ToInt32(ap.Attributes["value"].Value);
            
            ap = _xmlDoc.SelectSingleNode("helden/held/basis/freieabenteuerpunkte");
            if (ap == null)
                ap = _xmlDoc.SelectSingleNode("helden/held/freieabenteuerpunkte");
            if (ap != null)
                _held.APEingesetzt = _held.APGesamt - Convert.ToInt32(ap.Attributes["value"].Value);

            // Bild
            XmlNode bild = _xmlDoc.SelectSingleNode("helden/held/basis/portraet");
            if (bild == null)
                bild = _xmlDoc.SelectSingleNode("helden/held/portraet");

            if (bild != null)
            {
                _held.Bild = bild.Attributes["value"].Value;
            }

            // Eigenschaften
            int mod, value, permanent, grossemeditation, karmalqueste;
            XmlNodeList eigenschaften = _xmlDoc.SelectNodes("helden/held/eigenschaften/eigenschaft");
            if (eigenschaften.Count == 0)
                eigenschaften = _xmlDoc.SelectNodes("helden/held/eigenschaft");

            foreach (XmlNode eigenschaft in eigenschaften)
            {
                // Value (Zukauf)
                if (eigenschaft.Attributes["value"] != null)
                    value = Convert.ToInt32(eigenschaft.Attributes["value"].Value);
                else
                    value = 0;

                // Modifikator (Generierung, VorNachteile)
                if (eigenschaft.Attributes["mod"] != null)
                    mod = Convert.ToInt32(eigenschaft.Attributes["mod"].Value);
                else
                    mod = 0;

                // Permanent
                if (eigenschaft.Attributes["permanent"] != null)
                    permanent = Convert.ToInt32(eigenschaft.Attributes["permanent"].Value);
                else
                    permanent = 0;
                // Grossemeditation
                if (eigenschaft.Attributes["grossemeditation"] != null)
                    grossemeditation = Convert.ToInt32(eigenschaft.Attributes["grossemeditation"].Value);
                else
                    grossemeditation = 0;
                // Karmalqueste
                if (eigenschaft.Attributes["karmalqueste"] != null)
                    karmalqueste = Convert.ToInt32(eigenschaft.Attributes["karmalqueste"].Value);
                else
                    karmalqueste = 0;

                switch (eigenschaft.Attributes["name"].Value)
                {
                    case "Mut":
                        _held.MU = value + mod;
                        break;
                    case "Klugheit":
                        _held.KL = value + mod;
                        break;
                    case "Intuition":
                        _held.IN = value + mod;
                        break;
                    case "Charisma":
                        _held.CH = value + mod;
                        break;
                    case "Fingerfertigkeit":
                        _held.FF = value + mod;
                        break;
                    case "Gewandtheit":
                        _held.GE = value + mod;
                        break;
                    case "Konstitution":
                        _held.KO = value + mod;
                        break;
                    case "Körperkraft":
                        _held.KK = value + mod;
                        break;
                    case "Sozialstatus":
                        _held.SO = value + mod;
                        break;
                    case "Lebensenergie":
                        _held.LE_ModGen = mod - permanent; // VorNachteile werden in ImportVorNachteile() rausgerechnet
                        _held.LE_ModZukauf = value;
                        _held.LE_Mod = permanent;
                        break;
                    case "Ausdauer":
                        _held.AU_ModGen = mod - permanent; // VorNachteile werden in ImportVorNachteile() rausgerechnet
                        _held.AU_ModZukauf = value;
                        _held.AU_Mod = permanent;
                        break;
                    case "Astralenergie":
                        _held.AE_ModGen = mod - permanent - grossemeditation; // VorNachteile werden in ImportVorNachteile() rausgerechnet
                        _held.AE_pAsP = permanent * -1;
                        _held.AE_ModZukauf = value;
                        _held.AE_Mod = grossemeditation;
                        break;
                    case "Karmaenergie":
                        _held.KE_ModGen = mod - permanent - karmalqueste; // VorNachteile/SF werden in Import...() rausgerechnet
                        _held.KE_ModZukauf = value;
                        _held.KE_Mod = permanent + karmalqueste;
                        break;
                    case "Magieresistenz":
                        _held.MR_Mod = value + mod;
                        break;
                    default:
                        break;
                }
            }

            _held.LebensenergieAktuell = _held.LebensenergieMax;
            _held.AusdauerAktuell = _held.AusdauerMax;
            _held.KarmaenergieAktuell = _held.KarmaenergieMax;
            _held.AstralenergieAktuell = _held.AstralenergieMax;

            // Vor-/Nachteile
            ImportVorNachteile(_xmlDoc, _held, _importLog);

            // Sonderfertigkeiten
            ImportSonderfertigkeiten(_xmlDoc, _held, _importLog);

            // Talente
            ImportTalente(_xmlDoc, _held, _importLog);

            // Zauber
            ImportZauber(_xmlDoc, _held, _importLog);

            // Inventar
            ImportInventar(_xmlDoc, _held, _importLog);

            Model.Service.SerializationService serializer = Model.Service.SerializationService.GetInstance(true);
            if (!serializer.InsertOrUpdateHeld(_held))
            {
                //FEHLER! Held konnte nicht in die Datenbank eingefügt werden.
                throw new Exception("Held konnte nicht in die Datenbank eingefügt werden.");
            }
            Global.ContextHeld.UpdateList<Held>();

            // Import-Log erzeugen
            ShowLogWindow(_importPfad, _importLog);

            return Global.ContextHeld.Liste<Held>().Where(h => h.HeldGUID == heldGuid).FirstOrDefault();
        }

        private static void ImportZauber(XmlDocument _xmlDoc, Held _held, System.Collections.Generic.List<string> _importLog)
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
                    rep = Logic.General.Repräsentationen.GetKürzel(Logic.General.Repräsentationen.GetName(rep));
                }
                if (zauber.Attributes["variante"] != null && zauber.Attributes["variante"].Value != string.Empty)
                {
                    variante = zauber.Attributes["variante"].Value;
                    if (zauberName == "Adlerschwinge Wolfsgestalt")
                        bemerkung = variante;
                    else
                        zauberName += " (" + variante + ")";
                }


                Zauber z = Global.ContextHeld.LoadZauberByName(zauberName);
                if (z == null)
                { // Zauber wurde nicht gefunden, evtl. Konvertierung möglich
                    if (_zauberMapping.ContainsKey(zauberName.ToLowerInvariant()))
                    {
                        z = Global.ContextHeld.LoadZauberByName(_zauberMapping[zauberName.ToLowerInvariant()]);
                    }
                    else
                        added = false;
                }

                if (z != null)
                {
                    // TODO ??: Import von Zauber-Varianten ermöglichen
                    if (!_held.HatZauber(z.ZauberGUID, rep))
                    {
                        Held_Zauber hz = new Held_Zauber();
                        hz.HeldGUID = _held.HeldGUID;
                        hz.ZauberGUID = z.ZauberGUID;
                        hz.Repräsentation = rep;
                        hz.ZfW = wert;
                        hz.Bemerkung = bemerkung;
                        _held.Held_Zauber.Add(hz);
                        added = true;
                    }
                }

                if (!added) // Import nicht möglich
                    AddImportLog(ImportTypen.Zauber, string.Format("{0} [{1}]", zauberName, rep), wert, _importLog);
            }
        }

        private static void ImportTalente(XmlDocument _xmlDoc, Held _held, System.Collections.Generic.List<string> _importLog)
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
                    paBasis = Convert.ToInt32(paBasisNode.Attributes["value"].Value);

            XmlNodeList talente = _xmlDoc.SelectNodes("helden/held/talentliste/talent");
            if (talente.Count == 0)
                talente = _xmlDoc.SelectNodes("helden/held/talent");

            foreach (XmlNode talent in talente)
            {
                talentName = talent.Attributes["name"].Value;

                // Kampfwerte
                XmlNode atNode = _xmlDoc.SelectSingleNode(string.Format("helden/held/kampf/kampfwerte[@name='{0}']/attacke", talentName.Replace("'", "&apos;")));
                if (atNode == null)
                    atNode = _xmlDoc.SelectSingleNode(string.Format("helden/held/kampfwerte[@name='{0}']/attacke", talentName.Replace("'", "&apos;")));
                XmlNode paNode = _xmlDoc.SelectSingleNode(string.Format("helden/held/kampf/kampfwerte[@name='{0}']/parade", talentName.Replace("'", "&apos;")));
                if (paNode == null)
                    paNode = _xmlDoc.SelectSingleNode(string.Format("helden/held/kampfwerte[@name='{0}']/parade", talentName.Replace("'", "&apos;")));

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

                Talent t = Global.ContextHeld.LoadTalentByName(talentName, _held.Regelsystem);
                if (t == null)
                    if (_talentMapping.ContainsKey(talentName.ToLowerInvariant())) // Talent wurde nicht gefunden, evtl. Konvertierung möglich
                        t = Global.ContextHeld.LoadTalentByName(_talentMapping[talentName.ToLowerInvariant()], _held.Regelsystem);
                if (t != null)
                {
                    Held_Talent ht = new Held_Talent();
                    ht.HeldGUID = _held.HeldGUID;
                    ht.TalentGUID = t.TalentGUID;
                    ht.TaW = wert;
                    ht.ZuteilungAT = atZuteilung;
                    ht.ZuteilungPA = paZuteilung;
                    if (_held.Held_Talent.Any(_ht => ht.TalentGUID == _ht.TalentGUID))
                    {
                        AddImportLog(ImportTypen.Talent, talentName, wert, _importLog);
                        continue;
                    }
                    _held.Held_Talent.Add(ht);
                }
                else // Import nicht möglich
                    AddImportLog(ImportTypen.Talent, talentName, wert, _importLog);
            }
        }

        private static Regex reKlammern = new Regex("([^\\(]+)\\((.+)\\)");

        private static void ImportSonderfertigkeiten(XmlDocument _xmlDoc, Held _held, System.Collections.Generic.List<string> _importLog)
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

                List<string> werte = new List<string>(2);

                if (sonderfertigkeit.Attributes["value"] != null)
                    werte.Add(sonderfertigkeit.Attributes["value"].Value);
                foreach (XmlNode child in sonderfertigkeit.ChildNodes)
                {
                    if (child.Name == "auswahl" && child.Attributes["value"] != null)
                        werte.Add(child.Attributes["value"].Value);
                }

                if (werte.Count == 0)
                    werte.Add(null);
                wertString = werte[0];

                bool added = false;

                // Kulturkunde,  Scharfschütze, Meisterschütze, Schnellladen, Elementarharmonisierte Aura
                if (!added && (sfName == "Kulturkunde" || sfName == "Scharfschütze"
                    || sfName == "Meisterschütze" || sfName == "Schnellladen" || sfName == "Elementarharmonisierte Aura"))
                {
                    string sub = null;
                    if (sfName == "Kulturkunde")
                        sub = "kultur";
                    else if (sfName == "Ortskenntnis" || sfName == "Elementarharmonisierte Aura")
                        sub = "auswahl";
                    else if (sfName == "Scharfschütze" || sfName == "Meisterschütze" || sfName == "Schnellladen")
                        sub = "talent";
                    XmlNodeList subNodes = _xmlDoc.SelectNodes(string.Format("{2}[contains(@name,'{0}')]/{1}", sfName.Replace("'", "&apos;"), sub, sfXpath));
                    foreach (XmlNode s in subNodes)
                    {
                        string sfNameNeu = string.Format("{0} ({1})", sfName, s.Attributes["name"].Value);
                        if (_sonderfertigkeitMapping.ContainsKey(sfNameNeu.ToLowerInvariant()))
                            sfNameNeu = _sonderfertigkeitMapping[sfNameNeu.ToLowerInvariant()];

                        added = AddSonderfertigkeit(sfNameNeu, wertString, _held);
                        if (!added) // Import nicht mögliche
                            AddImportLog(ImportTypen.Sonderfertigkeit, sfName, s.Attributes["name"].Value, _importLog);
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
                    XmlNodeList subNodes = _xmlDoc.SelectNodes(string.Format("{2}[contains(@name,'{0}')]/{1}", sfName.Replace("'", "&apos;"), sub, sfXpath));
                    foreach (XmlNode s in subNodes)
                    {
                        added = AddSonderfertigkeit(sfName, s.Attributes["name"].Value, _held);
                        if (!added) // Import nicht mögliche
                            AddImportLog(ImportTypen.Sonderfertigkeit, sfName, s.Attributes["name"].Value, _importLog);
                    }
                    continue;
                }
                // Talentspezialisierung
                if (!added && sfName.StartsWith("Talentspezialisierung"))
                    added = AddSonderfertigkeit("Talentspezialisierung", sfName.Replace("Talentspezialisierung ", null), _held);
                // Zauberspezialisierung
                if (!added && sfName.StartsWith("Zauberspezialisierung"))
                    added = AddSonderfertigkeit("Zauberspezialisierung", sfName.Replace("Zauberspezialisierung ", null), _held);
                // Ritualkenntnis (Schamanentradition)
                if (!added && sfName.StartsWith("Ritualkenntnis"))
                    added = AddSonderfertigkeit(sfName.Replace("Ritualkenntnis: ", "Ritualkenntnis (") + ")", wertString, _held);
                // Göttliche....
                if (!added && (sfName.StartsWith("Göttliche Beseelung rufen") || sfName.StartsWith("Göttliche Essenz kanalisieren")
                    || sfName.StartsWith("Göttliche Macht binden") || sfName.StartsWith("Göttlichen Schutz erflehen")
                    || sfName.StartsWith("Göttlichen Willen erzwingen") || sfName.StartsWith("Göttliches Prinzip stärken")))
                    added = AddSonderfertigkeit("Liturgie: " + sfName, wertString, _held);
                // Waffenlose Kampftechniken
                if (!added && sfName.StartsWith("Waffenloser Kampfstil"))
                    added = AddSonderfertigkeit(sfName.Replace("Waffenloser Kampfstil: ", "Waffenlose Kampftechnik (") + ")", wertString, _held);
                if (!added && sfName.StartsWith("Merkmalskenntnis"))
                    added = AddSonderfertigkeit(sfName.Replace("Merkmalskenntnis: ", "Merkmalskenntnis (") + ")", wertString, _held); // Merkmalskenntnis
                if (!added && sfName.StartsWith("Gabe des Odûn"))
                    added = AddSonderfertigkeit(sfName.Replace("Gabe des Odûn", "Odûn-Gabe"), wertString, _held); // Odûn-Gabe
                if (!added && sfName.StartsWith("Ritual:"))
                    added = AddSonderfertigkeit(sfName.Replace("Ritual:", "Schamanenritual:"), wertString, _held); // Schamanenritual
                if (!added)
                    added = AddSonderfertigkeit(sfName, wertString, _held);
                if (!added && sfName.StartsWith("Liturgie:") && sfName.Contains("("))
                {
                    //Die original Liturgie ist im Klammern genannt
                    Match m = reKlammern.Match(sfName);
                    if (m != null && m.Groups.Count == 3)
                        added = AddSonderfertigkeit(String.Format("Liturgie: {0}", m.Groups[2].Value.Trim()), wertString, _held);
                }
                if (!added)
                    added = AddSonderfertigkeit(string.Format("Waffenloses Manöver ({0})", sfName), wertString, _held); // Waffenlose Manöver
                if (!added)
                    added = AddSonderfertigkeit(string.Format("Geländekunde ({0})", sfName), wertString, _held); // Geländekunden
                

                if (!added)
                {
                    if (_sonderfertigkeitMapping.ContainsKey(sfName.ToLowerInvariant()))
                        added = AddSonderfertigkeit(_sonderfertigkeitMapping[sfName.ToLowerInvariant()], wertString, _held);
                }
                // Sonderfertigkeit wurde immer noch nicht gefunden, evtl. Mapping mit Wert möglich
                if (!added)
                    added = AddSonderfertigkeit(sfName + " " + wertString, null, _held);
                //evtl mit römischer Zahl
                if (!added)
                {
                    int iWert = 0;
                    if ((Int32.TryParse(wertString, out iWert) || wertString == null || wertString == String.Empty) && iWert >= 0 && iWert < 4000)
                    {
                        if (iWert <= 0)
                            iWert = 1;
                        added = AddSonderfertigkeit(sfName + " " + iWert.ToRoman(), null, _held);
                    }
                }
                // Vor-/Nachteil wurde immer noch nicht gefunden, evtl. Mapping mit Wert möglich
                if (!added)
                {
                    if (_sonderfertigkeitMapping.ContainsKey((sfName + " " + wertString).ToLowerInvariant()))
                        added = AddSonderfertigkeit(_sonderfertigkeitMapping[(sfName + " " + wertString).ToLowerInvariant()], null, _held);
                }
                //evtl mit römischer Zahl und mapping
                if (!added)
                {
                    int iWert = 0;
                    if ((Int32.TryParse(wertString, out iWert) || wertString == null || wertString == String.Empty) && iWert >= 0 && iWert < 4000)
                    {
                        if (iWert <= 0)
                            iWert = 1;
                        string sfNameNeu = (sfName + " " + iWert.ToRoman()).ToLowerInvariant();
                        if (_sonderfertigkeitMapping.ContainsKey(sfNameNeu))
                            added = AddSonderfertigkeit(_sonderfertigkeitMapping[sfNameNeu], null, _held);
                    }
                }

                if (added)
                {
                    int iWert = 0;
                    Int32.TryParse(wertString, out iWert);
                    if (sfName == "Spätweihe Alveranische Gottheit" || sfName == "Spätweihe Namenloser" || sfName == "Spätweihe (Xo'Artal-Pantheon)")
                        _held.KE_ModGen -= 24; // Hinweis: 'Spätweihe (Xo'Artal-Pantheon)' gibt es derzeit nicht in der HeldenSoftware
                    else if (sfName == "Spätweihe Nichtalveranische Gottheit" || sfName == "Kontakt zum Großen Geist")
                        _held.KE_ModGen -= 12;
                    else if (sfName == "Spätweihe Dunkle Zeiten") // TODO: wie ist 'Spätweihe Dunkle Zeiten' in der HeldenSoftware abgebildet?
                        _held.KE_ModGen -= 6 * Math.Max(1, iWert);
                }

                if (!added) // Import nicht mögliche
                    AddImportLog(ImportTypen.Sonderfertigkeit, sfName, String.Join(";", werte), _importLog);
            }
        }

        private static void ImportVorNachteile(XmlDocument _xmlDoc, Held _held, System.Collections.Generic.List<string> _importLog)
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

                List<string> werte = new List<string>(2);

                if (vorNachteil.Attributes["value"] != null)
                    werte.Add(vorNachteil.Attributes["value"].Value);
                foreach (XmlNode child in vorNachteil.ChildNodes)
                {
                    if(child.Name == "auswahl" && child.Attributes["value"] != null)
                        werte.Add(child.Attributes["value"].Value);
                }

                if (werte.Count == 0)
                    werte.Add(null);
                wertString = werte[0];

                bool added = false;

                if (!added && vorNachteilName == "Begabung für [Merkmal]")
                    added = AddVorNachteil(string.Format("Begabung für Merkmal ({0})", wertString), null, _held); // Begabung für Merkmal
                else if (!added && vorNachteilName == "Begabung für [Talentgruppe]")
                    added = AddVorNachteil(string.Format("Begabung für Talentgruppe ({0})", (wertString == "Körperlich" ? "Körper" : wertString)), null, _held); // Begabung für Talentgruppe
                else if (!added && vorNachteilName == "Unfähigkeit für [Merkmal]")
                    added = AddVorNachteil(string.Format("Unfähigkeit für Merkmal ({0})", wertString), null, _held); // Unfähigkeit für Merkmal
                else if (!added && vorNachteilName == "Unfähigkeit für [Talentgruppe]")
                    added = AddVorNachteil(string.Format("Unfähigkeit für Talentgruppe ({0})", wertString), null, _held); // Unfähigkeit für Talentgruppe
                else if (!added && vorNachteilName.StartsWith("Angst vor")) // Angst vor...
                {
                    string[] angstVorTeile = vorNachteilName.Split(' ');
                    string angstVor = angstVorTeile[2] + string.Format(" ({0})", wertString);
                    added = AddVorNachteil("Angst vor", angstVor, _held);
                }
                else if(!added && vorNachteilName.StartsWith("Vorurteile gegen") && werte.Count==2)
                    added = AddVorNachteil("Vorurteile", string.Format("{1} ({0})", werte[0], werte[1]), _held);
                else if (!added && vorNachteilName.StartsWith("Vorurteile gegen (stark)") && werte.Count == 2)
                    added = AddVorNachteil("Vorurteile", string.Format("{1} ({0})", werte[0], werte[1]), _held);
                else if (!added && vorNachteilName == "Vorurteile (stark)") // Vorurteile (stark)...
                    added = AddVorNachteil("Vorurteile", string.Format("stark ({0})", wertString), _held);
                else if (!added && vorNachteilName.StartsWith("Moralkodex")) // Moralkodex
                    added = AddVorNachteil("Moralkodex Kirche", vorNachteilName.Replace("Moralkodex [", null).TrimEnd(']'), _held);
                else if (!added && (vorNachteilName.StartsWith("Herausragende Eigenschaft")
                    || vorNachteilName.StartsWith("Miserable Eigenschaft"))) // Herausragende/Miserable Eigenschaft
                {
                    string eigenschaft = vorNachteilName.Split(':')[1].Trim();
                    string eigenschaftKürzel = Eigenschaft.GetAbkürzung(eigenschaft);
                    if (vorNachteilName.StartsWith("Herausragende Eigenschaft"))
                        added = AddVorNachteil(vorNachteilName.Replace("Herausragende Eigenschaft: ", "Herausragende Eigenschaft (")
                            .Replace(eigenschaft, eigenschaftKürzel + ")"), wertString, _held);
                    else
                        added = AddVorNachteil(vorNachteilName.Replace("Miserable Eigenschaft: ", "Miserable Eigenschaft (")
                            .Replace(eigenschaft, eigenschaftKürzel + ")"), wertString, _held);
                }

                if (!added && werte.Count == 2)
                    added = AddVorNachteil(vorNachteilName, String.Format("{1} ({0})", werte[0], werte[1]), _held);
                if (!added && werte.Count == 2)
                {
                    if (_vorNachteilMapping.ContainsKey(vorNachteilName.ToLowerInvariant()))
                        added = AddVorNachteil(_vorNachteilMapping[vorNachteilName.ToLowerInvariant()], String.Format("{1} ({0})", werte[0], werte[1]), _held);
                }

                if (!added)
                    added = AddVorNachteil(vorNachteilName, wertString, _held);

                //evtl mit römischer Zahl
                if (!added)
                {
                    int iWert = 0;
                    if ((Int32.TryParse(wertString, out iWert) || wertString == null || wertString == String.Empty) && iWert >= 0 && iWert < 4000)
                    {
                        if(iWert <= 0)
                            iWert = 1;
                        added = AddVorNachteil(vorNachteilName + " " + iWert.ToRoman(), null, _held);
                    }
                }

                // Vor-/Nachteil wurde nicht gefunden, evtl. Mapping möglich
                if (!added)
                {
                    if (_vorNachteilMapping.ContainsKey(vorNachteilName.ToLowerInvariant()))
                        added = AddVorNachteil(_vorNachteilMapping[vorNachteilName.ToLowerInvariant()], wertString, _held);
                }
                // Vor-/Nachteil wurde immer noch nicht gefunden, evtl. Mapping mit Wert möglich
                if (!added)
                    added = AddVorNachteil(vorNachteilName + " " + wertString, null, _held);
                // Vor-/Nachteil wurde immer noch nicht gefunden, evtl. Mapping mit Wert möglich
                if (!added)
                {
                    if (_vorNachteilMapping.ContainsKey((vorNachteilName + " " + wertString).ToLowerInvariant()))
                        added = AddVorNachteil(_vorNachteilMapping[(vorNachteilName + " " + wertString).ToLowerInvariant()], null, _held);
                }
                //evtl mit römischer Zahl und mapping
                if (!added)
                {
                    int iWert = 0;
                    if ((Int32.TryParse(wertString, out iWert) || wertString == null || wertString == String.Empty) && iWert >= 0 && iWert < 4000)
                    {
                        if (iWert <= 0)
                            iWert = 1;
                        string vtNameNeu = (vorNachteilName + " " + iWert.ToRoman()).ToLowerInvariant();
                        if (_vorNachteilMapping.ContainsKey(vtNameNeu))
                            added = AddVorNachteil(_vorNachteilMapping[vtNameNeu], null, _held);
                    }
                }

                if (added)
                {
                    int iWert = 0;
                    Int32.TryParse(wertString, out iWert);
                    if (vorNachteilName == "Hohe Lebenskraft")
                        _held.LE_ModGen -= iWert;
                    else if (vorNachteilName == "Niedrige Lebenskraft")
                        _held.LE_ModGen += iWert;
                    if (vorNachteilName == "Hohe Karmaenergie")
                        _held.KE_ModGen -= iWert;
                    else if (vorNachteilName == "Ausdauernd")
                        _held.AU_ModGen -= iWert;
                    else if (vorNachteilName == "Kurzatmig")
                        _held.AU_ModGen += iWert;
                    else if (vorNachteilName == "Vollzauberer")
                        _held.AE_ModGen -= 12;
                    else if (vorNachteilName == "Halbzauberer")
                        _held.AE_ModGen -= 6;
                    else if (vorNachteilName == "Viertelzauberer" || vorNachteilName == "Unbewusster Viertelzauberer")
                        _held.AE_ModGen -= 6;
                    else if (vorNachteilName == "Zauberhaar")
                        _held.AE_ModGen -= 7;
                    else if (vorNachteilName == "Astralmacht")
                        _held.AE_ModGen -= iWert;
                    else if (vorNachteilName == "Niedrige Astralkraft")
                        _held.AE_ModGen += iWert;
                    else if (vorNachteilName == "Geweiht [zwölfgöttliche Kirche]" || vorNachteilName == "Geweiht[H'Ranga]" || vorNachteilName == "Geweiht [Gravesh]" 
                        || vorNachteilName == "Geweiht [Angrosch]" || vorNachteilName == "Geweiht [Xo'Artal-Stadtpantheon]")
                        _held.KE_ModGen -= 24;
                    else if (vorNachteilName == "Geweiht [nicht-alveranische Gottheit]")
                        _held.KE_ModGen -= 12;
                    else if (vorNachteilName == "Sacerdos" || vorNachteilName == "Karmatiker")
                        _held.KE_ModGen -= 6 * Math.Max(1, iWert);
                }

                if (!added) // Import nicht möglich
                    AddImportLog(ImportTypen.VorNachteil, vorNachteilName, String.Join(";", werte), _importLog);
            }
        }

        private static bool AddVorNachteil(string vorNachteilName, string wertString, Held _held)
        {
            VorNachteil vn = Global.ContextHeld.LoadVorNachteilByName(vorNachteilName, _held.Regelsystem);
            if (vn != null)
            {
                Held_VorNachteil hvn = null;
                wertString = wertString ?? "";
                hvn = _held.Held_VorNachteil.Where(hvn1 => hvn1.HeldGUID == _held.HeldGUID && hvn1.VorNachteilGUID == vn.VorNachteilGUID && hvn1.Wert == wertString).FirstOrDefault();
                if (hvn == null)
                {
                    hvn = new Held_VorNachteil();
                    hvn.HeldGUID = _held.HeldGUID;
                    hvn.VorNachteilGUID = vn.VorNachteilGUID;
                    hvn.Wert = wertString ?? "";
                    hvn.KostenGrund = vn.KostenGrund ?? 0;
                    hvn.KostenFaktor = vn.KostenFaktor ?? 0;
                    _held.Held_VorNachteil.Add(hvn);
                    return true;
                }
                //VorNachteil mit diesem Wert bereits vorhanden
            }
            return false;
        }

        private static bool AddSonderfertigkeit(string sfName, Held _held)
        {
            return AddSonderfertigkeit(sfName, null, _held);
        }
        private static bool AddSonderfertigkeit(string sfName, string wertString, Held _held)
        {
            Sonderfertigkeit sf = Global.ContextHeld.LoadSonderfertigkeitByName(sfName);
            if (sf != null)
            {
                Held_Sonderfertigkeit hs = null;
                wertString = wertString ?? "";
                hs = _held.Held_Sonderfertigkeit.Where(hs1 => hs1.HeldGUID == _held.HeldGUID && hs1.SonderfertigkeitGUID == sf.SonderfertigkeitGUID && hs1.Wert == wertString).FirstOrDefault();
                if (hs == null)
                {
                    hs = new Held_Sonderfertigkeit();
                    hs.HeldGUID = _held.HeldGUID;
                    hs.SonderfertigkeitGUID = sf.SonderfertigkeitGUID;
                    hs.Wert = wertString ?? "";
                    _held.Held_Sonderfertigkeit.Add(hs);
                    return true;
                }
                //Sonderfertigkeit mit diesem Wert bereits vorhanden
            }
            return false;
        }

        private static void AddImportLog(ImportTypen typ, string name, object wert, System.Collections.Generic.List<string> _importLog)
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
                    // TODO ??: Hinweis für Adlerschwinge-Varianten entfernen, sobald import mögich
                    if (name.StartsWith("Adlerschwinge Wolfsgestalt"))
                        hinweis += "(Es kann derzeit nur eine Variante dieses Zaubers import werden)";
                    break;
                case ImportTypen.Gegenstand:
                    typString = "Gegenstand: ";
                    break;
                default:
                    break;
            }

            _importLog.Add(string.Format("{0}{1} {2} {3}", typString, name, wert, hinweis));
        }

        private static void ShowLogWindow(string _importPfad, System.Collections.Generic.List<string> _importLog)
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
                    + "\n\nEinige Werte konnten nicht importiert werden.\nGegenstände, die hier aufgelistet sind, konnten denen in unserer Datenbank nicht zugeordnet werden. Diese wurden im Inventar unter Sonstiges aufgenommen.\nWenn du bei der Verbesserung der Import-Funktion mitwirken möchtest, melde das Problem im Forum (http://forum.meistergeister.org/forumdisplay.php?fid=79) oder unserem Issue-Tracker (http://moonvega.pmhost.de/trac/) und lade die XML-Datei dort hoch. Vielen Dank!.\n\n";
                txtLog.Text += log;
                txtLog.TextWrapping = System.Windows.TextWrapping.Wrap;
                txtLog.VerticalScrollBarVisibility = System.Windows.Controls.ScrollBarVisibility.Visible;
                gui.Content = txtLog;
                gui.Show();
            }
        }

        public static bool IsHeldenSoftwareFile(string xmlFile)
        {
            System.IO.FileStream fs = null;
            System.IO.StreamReader sr = null;
            try
            {
                fs = System.IO.File.OpenRead(xmlFile);
                sr = new System.IO.StreamReader(fs);
                for (int i = 1; i <= 7 && !sr.EndOfStream; i++)
                {
                    string line = sr.ReadLine();
                    if (line.Contains("helden.xsd") || line.Contains("helden.xsl"))
                        return true;
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                if (sr != null)
                    sr.Dispose();
                if (fs != null)
                    fs.Dispose();
            }
            return false;
        }

        public static Guid GetGuidFromFile(string xmlFile)
        {
            string key = GetKeyFromFile(xmlFile);
            if (key == null)
                return Guid.Empty;
            return KeyToGuid(key);
        }

        public static string GetKeyFromFile(string xmlFile)
        {
            System.IO.FileStream fs = null;
            System.IO.StreamReader sr = null;
            try
            {
                fs = new System.IO.FileStream(xmlFile, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                sr = new System.IO.StreamReader(fs);
                for (int i = 1; i <= 6 && !sr.EndOfStream; i++)
                {
                    string line = sr.ReadLine();
                    if (line != null && line.Contains("<held "))
                    {
                        System.Text.RegularExpressions.Regex re = new System.Text.RegularExpressions.Regex("key=\"(\\w+)\"");
                        System.Text.RegularExpressions.Match match = re.Match(line);
                        return match.Groups[1].Value;
                    }
                }
            }
            catch(Exception e)
            {
                if (e is SystemException)
                    throw;
                return null;
            }
            finally
            {
                if (sr != null)
                    sr.Dispose();
                if (fs != null)
                    fs.Dispose();
            }
            return null;
        }

        private static void ImportInventar(XmlDocument _xmlDoc, Held _held, System.Collections.Generic.List<string> _importLog)
        {
            string name = string.Empty;
            double gewicht = 0.0;
            double preis = 0.0;
            int anzahl = 0;
            string xpath = "helden/held/gegenstände/gegenstand";
            XmlNodeList gegenstaende = _xmlDoc.SelectNodes(xpath);
            if (gegenstaende.Count == 0)
            {
                xpath = "helden/held/gegenstand";
                gegenstaende = _xmlDoc.SelectNodes(xpath);
            }

            foreach (XmlNode gegenstand in gegenstaende)
            {
                name = gegenstand.Attributes["name"].Value.Trim();
                if (!Int32.TryParse(gegenstand.Attributes["anzahl"].Value.Trim(), out anzahl))
                    anzahl = 1;
                if (gegenstand.HasChildNodes)
                    foreach (XmlNode gchild in gegenstand.ChildNodes)
                    {
                        if (gchild.Name == "modallgemein")
                        {
                            foreach (XmlNode mchild in gegenstand.ChildNodes)
                            {
                                if (mchild.Name == "name")
                                {
                                    name = mchild.Attributes["value"].Value.Trim();
                                }
                                else if (mchild.Name == "preis")
                                {
                                    Double.TryParse(mchild.Attributes["value"].Value.Trim(), out preis);
                                }
                                else if (mchild.Name == "gewicht")
                                {
                                    Double.TryParse(mchild.Attributes["value"].Value.Trim(), out gewicht);
                                }
                            }
                        }
                    }

                bool isnew = false;
                Ausrüstung a = null;
                Inventar i = null;

                if(_gegenstandMapping.ContainsKey(name.ToLowerInvariant()))
                    name = _gegenstandMapping[name.ToLowerInvariant()];
                //alles durchsuchen
                a = Global.ContextHeld.Liste<Ausrüstung>().Where(li => li.Name.ToLowerInvariant() == name.ToLowerInvariant()).FirstOrDefault();
                if (i == null && a == null)
                {
                    i = Global.ContextHeld.Liste<Inventar>().Where(li => li.Name.ToLowerInvariant() == name.ToLowerInvariant()).FirstOrDefault();
                }
                if(i == null && a == null)
                {
                    Handelsgut h = Global.ContextHeld.Liste<Handelsgut>().Where(li => li.Name.ToLowerInvariant() == name.ToLowerInvariant()).FirstOrDefault();
                    if (h != null)
                    {
                        i = new Inventar();
                        i.Name = h.Name;
                        i.Tags = h.Tags;
                        i.Preis = h.Preis;
                        i.ME = h.ME;
                        i.Literatur = h.Literatur;
                        i.Kategorie = h.Kategorie;
                        i.HandelsgutGUID = h.HandelsgutGUID;
                        i.Gewicht = h.Gewicht;
                        i.Bemerkung = h.Bemerkung;
                        isnew = true;
                    }
                }
                //wenn nichts gefunden, dann einen Gegenstand im inventar neu erstellen
                if (a==null && i==null)
                {
                    i = new Inventar();
                    i.Name = name;
                    i.Gewicht = gewicht;
                    i.Kategorie = "Import";
                    //TODO JT: Einheit automatisch bestimmen
                    i.Preis = String.Format("{0}S", preis/100.0);
                    isnew = true;
                    //gegenstand wurde nicht erkannt und wurde im Inventar hinzugefügt
                    AddImportLog(ImportTypen.Gegenstand, name, null, _importLog);
                }
                //sonst in held_Ausrüstung oder Held_inventar hinzufügen, bzw anzahl erhöhen.
                if (i != null)
                {
                    Held_Inventar hi = _held.Held_Inventar.Where(hhi => hhi.InventarGUID == i.InventarGUID).FirstOrDefault();
                    if (hi == null)
                    {
                        hi = new Held_Inventar();
                        hi.HeldGUID = _held.HeldGUID;
                        hi.InventarGUID = i.InventarGUID;
                        hi.Anzahl = anzahl;
                        hi.Angelegt = false;
                        hi.TrageortGUID = Guid.Parse("00000000-0000-0000-001a-000000000011"); //Rucksack
                        if(isnew)
                            hi.Inventar = i;
                        _held.Held_Inventar.Add(hi);
                    }
                    else
                        hi.Anzahl += anzahl;
                }
                else if (a != null)
                {
                    for (int ai = 1; ai <= anzahl; ai++)
                        _held.AddAusrüstung(a, true);
                }
            }
        }
    }

    public enum ImportTypen
    {
        VorNachteil,
        Sonderfertigkeit,
        Talent,
        Zauber,
        Gegenstand
    }
}
