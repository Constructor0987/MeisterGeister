using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using MeisterGeister.Daten;
using System.Data;
using System.Data.OleDb;
// Eigene Usings
//using MeisterGeister.Logic.General;
using MeisterGeister.Model;
using MeisterGeister.ViewModel.Helden.Logic;

namespace MeisterGeister.Logic.HeldenImport
{
    /// <summary>
    /// Klasse zum Konvertieren von Helden der Helden-Software.
    /// </summary>
    public class HeldenblattImporter
    {
        #region Mappings
        static HeldenblattImporter()
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
            _gegenstandMapping.Add("linkhand (kling.br.)", "linkhand mit klingenbrecher");
            _gegenstandMapping.Add("linkhand und klingenbrecher", "linkhand mit klingenbrecher");
            _gegenstandMapping.Add("hartholzharnisch", "maraskanischer hartholzharnisch");
            _gegenstandMapping.Add("gambeson", "gambeson/wattierter waffenrock");
            _gegenstandMapping.Add("wattierter waffenrock", "gambeson/wattierter waffenrock");
            _gegenstandMapping.Add("fuhrmannsmantel", "fellumhang/fuhrmannsmantel");
            _gegenstandMapping.Add("fellumhang", "fellumhang/fuhrmannsmantel");
            _gegenstandMapping.Add("bart", "bart/halsberge");
            _gegenstandMapping.Add("halsberge", "bart/halsberge");
            _gegenstandMapping.Add("beintaschen", "beintaschen/schürze");
            _gegenstandMapping.Add("schürze", "beintaschen/schürze");
            _gegenstandMapping.Add("lederweste", "lederweste/pelzweste");
            _gegenstandMapping.Add("pelzweste", "lederweste/pelzweste");
            _gegenstandMapping.Add("stechhelm", "stechhelm/visierhelm");
            _gegenstandMapping.Add("visierhelm", "stechhelm/visierhelm");
            _gegenstandMapping.Add("wattiertes unterzeug", "wattiertes unterzeug/wattierte unterkleidung");
            _gegenstandMapping.Add("wattierte unterkleidung", "wattiertes unterzeug/wattierte unterkleidung");
        }

        private static void SetSonderfertigkeitenMapping()
        {
            _sonderfertigkeitMapping.Add("akklimatisierung: hitze", "akklimatisierung (hitze)");
            _sonderfertigkeitMapping.Add("akklimatisierung: kälte", "akklimatisierung (kälte)");
            _sonderfertigkeitMapping.Add("fernzauberei", "fernzauberei i");
            _sonderfertigkeitMapping.Add("traumgänger", "traumgänger i");
            _sonderfertigkeitMapping.Add("apport", "objektritual: apport");
            _sonderfertigkeitMapping.Add("bannschwert", "objektritual: bannschwert");
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
        }

        private static void SetVorNachteilMapping()
        {
            _vorNachteilMapping.Add("adlige abstammung", "adlig (adlige abstammung)");
            _vorNachteilMapping.Add("adliges erbe", "adlig (adliges erbe)");
            _vorNachteilMapping.Add("amtsadel", "adlig (amtsadel)");
            _vorNachteilMapping.Add("astrale regeneration 1", "astrale regeneration i");
            _vorNachteilMapping.Add("astrale regeneration 2", "astrale regeneration ii");
            _vorNachteilMapping.Add("astrale regeneration 3", "astrale regeneration iii");
            _vorNachteilMapping.Add("begabung für [merkmal] dämonisch", "begabung für merkmal (dämonisch (gesamt))");
            _vorNachteilMapping.Add("begabung für [merkmal] elementar", "begabung für merkmal (elementar (gesamt))");
            _vorNachteilMapping.Add("begabung für [talent]", "begabung für talent");
            _vorNachteilMapping.Add("begabung für [ritual]", "begabung für ritual");
            _vorNachteilMapping.Add("begabung für [zauber]", "begabung für zauber");
            _vorNachteilMapping.Add("schnelle heilung 1", "schnelle heilung i");
            _vorNachteilMapping.Add("schnelle heilung 2", "schnelle heilung ii");
            _vorNachteilMapping.Add("schnelle heilung 3", "schnelle heilung iii");
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
            _zauberMapping.Add("weiße mähn und goldener huf", "weiße mähn'' und gold''ner huf");
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

        private static string[] zwölfGötter = new string[] { "Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja" };
        private static string[] hranga = new string[] { "H'Szinth", "Zsahh" };

        private static string GetGötterArt(string nameDesGottes, bool fürSpätweihe = false)
        {
            if (!fürSpätweihe && nameDesGottes == "Angrosch")
                return "Angrosch";
            else if (!fürSpätweihe && nameDesGottes == "Gravesh")
                return "Gravesh";
            else if (zwölfGötter.Contains(nameDesGottes))
                return fürSpätweihe?"Alveranische Gottheit":"zwölfgöttliche Kirche";
            else if (!fürSpätweihe && hranga.Contains(nameDesGottes))
                return "H'Ranga";
            else if(fürSpätweihe && nameDesGottes.StartsWith("Namenlos"))
                return "Namenloser";
            else
                return fürSpätweihe ? "Nichtalveranische Gottheit" : "nicht-alveranische Gottheit";
        }

        public static Held ImportHeldenblattFile(string _importPfad)
        {
            return ImportHeldenblattFile(_importPfad, Guid.Empty);
        }

        public static Held ImportHeldenblattFile(string _importPfad, Guid newGuid)
        {
            System.Collections.Generic.List<string> _importLog = new List<string>();
            OleDbConnection conn = new OleDbConnection(String.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\"", _importPfad));
            conn.Open();

            //Held Basisdaten
            Held _held = ImportHeld(conn, _importLog);
            if (newGuid != Guid.Empty)
                _held.HeldGUID = newGuid;
            Guid heldGuid = _held.HeldGUID;


            // Vor-/Nachteile
            ImportVorNachteile(conn, _held, _importLog);

            // Sonderfertigkeiten
            ImportSonderfertigkeiten(conn, _held, _importLog);

            // Talente
            ImportTalente(conn, _held, _importLog);

            // Zauber
            ImportZauber(conn, _held, _importLog);

            // Inventar
            ImportInventar(conn, _held, _importLog);

            //für tests
            conn.Close();
            return _held;

            /*
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
             * */
        }

        private static DataTable GetTable(OleDbConnection conn, string commandText)
        {
            var cmd = conn.CreateCommand();
            cmd.CommandText = commandText;
            var reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            return dt;
        }

        private static Held ImportHeld(OleDbConnection conn, System.Collections.Generic.List<string> _importLog)
        {
            DataTable dt = GetTable(conn, "SELECT * FROM [MG_Held$]");
            if (dt.Rows.Count < 1)
                return null;
            var row = dt.Rows[0];

            Held _held = new Held();
            _held.Name = (string)row["Name"];
            _held.Rasse = (string)row["Rasse"];
            _held.Kultur = (string)row["Kultur"];
            _held.Spieler = (string)row["Spieler"];
            _held.Profession = (string)row["Profession"];
            // Bild
            _held.MU = (int?)row.Field<double?>("MU");
            _held.KL = (int?)row.Field<double?>("KL");
            _held.CH = (int?)row.Field<double?>("CH");
            _held.IN = (int?)row.Field<double?>("IN");
            _held.KK = (int?)row.Field<double?>("KK");
            _held.GE = (int?)row.Field<double?>("GE");
            _held.FF = (int?)row.Field<double?>("FF");
            _held.KO = (int?)row.Field<double?>("KO");
            _held.SO = (int?)row.Field<double?>("SO");
            _held.LE_Mod = (int?)row.Field<double?>("LE_Mod");
            _held.AU_Mod = (int?)row.Field<double?>("AU_Mod");
            _held.AE_Mod = (int?)row.Field<double?>("AE_Mod");
            _held.KE_Mod = (int?)row.Field<double?>("KE_Mod");
            _held.MR_Mod = (int?)row.Field<double?>("MR_Mod");
            _held.INI_Mod = (int?)row.Field<double?>("INI_Mod");
            _held.LE_Aktuell = (int?)row.Field<double?>("LE_Aktuell");
            _held.AU_Aktuell = (int?)row.Field<double?>("AU_Aktuell");
            _held.AE_Aktuell = (int?)row.Field<double?>("AE_Aktuell");
            _held.KE_Aktuell = (int?)row.Field<double?>("KE_Aktuell");
            _held.Wunden = (int?)row.Field<double?>("Wunden");
            _held.RSKopf = (int?)row.Field<double?>("RSKopf") ?? 0;
            _held.RSBrust = (int?)row.Field<double?>("RSBrust") ?? 0;
            _held.RSRücken = (int?)row.Field<double?>("RSRücken") ?? 0;
            _held.RSArmL = (int?)row.Field<double?>("RSArmL") ?? 0;
            _held.RSArmR = (int?)row.Field<double?>("RSArmR") ?? 0;
            _held.RSBauch = (int?)row.Field<double?>("RSBauch") ?? 0;
            _held.RSBeinL = (int?)row.Field<double?>("RSBeinL") ?? 0;
            _held.RSBeinR = (int?)row.Field<double?>("RSBeinR") ?? 0;
            _held.BE = (int?)row.Field<double?>("BE");
            _held.Vermögen = row.Field<double?>("Vermögen");
            _held.APGesamt = (int?)row.Field<double?>("APGesamt") ?? 0;
            _held.APEingesetzt = (int?)row.Field<double?>("APEingesetzt") ?? 0;

            return _held;
        }

        private static Regex reKlammern = new Regex("([^\\(]+)\\((.+)\\)");

        private static void ImportZauber(OleDbConnection conn, Held _held, System.Collections.Generic.List<string> _importLog)
        {
            int wert;
            string zauberName = string.Empty;
            string rep = string.Empty;
            string variante = string.Empty;
            string bemerkung = string.Empty;

            DataTable dt = GetTable(conn, "Select * from [MG_Zauber$] where Aktiviert=True");

            foreach (DataRow zRow in dt.Rows)
            {
                //bemerkung = null;
                bool added = false;
                zauberName = zRow.Field<string>("Name");
                if (zauberName != null)
                    zauberName = zauberName.Trim();
                wert = (int?)zRow.Field<double?>("ZfW") ?? 0;
                rep = zRow.Field<string>("Rep");
                if (rep != null)
                    rep = rep.Trim();
                else
                    rep = "Dil";
                bemerkung = string.Empty;
                variante = string.Empty;
                //TODO Übernatürliche Begabungen
                rep = Logic.General.Repräsentationen.GetKürzel(Logic.General.Repräsentationen.GetName(rep));
                
                //zunächst nach einem exakten treffer suchen
                Zauber z = Global.ContextHeld.LoadZauberByName(zauberName);
                if (z == null)
                {
                    // Zauber wurde nicht gefunden, evtl. Konvertierung möglich
                    if (_zauberMapping.ContainsKey(zauberName.ToLowerInvariant()))
                    {
                        z = Global.ContextHeld.LoadZauberByName(_zauberMapping[zauberName.ToLowerInvariant()]);
                    }
                    else
                    {
                        //ohne die Variante in Klammern versuchen
                        Match m = reKlammern.Match(zauberName);
                        
                        if (m != null && m.Groups.Count == 3)
                        {
                            variante = m.Groups[2].Value.Trim();
                            z = Global.ContextHeld.LoadZauberByName(m.Groups[1].Value.Trim());
                        }
                        if (z == null)
                            added = false; //TODO alternativ als User-Zauber importieren.
                        else
                            bemerkung = variante;
                    }

                    
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

        private static void ImportTalente(OleDbConnection conn, Held _held, System.Collections.Generic.List<string> _importLog)
        {
            int wert;
            int atZuteilung = 0;
            int paZuteilung = 0;
            int atBasis = _held.AttackeBasisOhneMod;
            int paBasis = _held.ParadeBasisOhneMod;
            string talentName = string.Empty;

            DataTable dt = GetTable(conn, "Select * from [MG_Talente$] where Aktiviert=True");

            foreach (DataRow tRow in dt.Rows)
            {
                talentName = tRow.Field<string>("Name");
                if (talentName != null)
                    talentName = talentName.Trim();
                wert = (int?)tRow.Field<double?>("TaW") ?? 0;
                atZuteilung = (int?)tRow.Field<double?>("ZuteilungAT") ?? 0;
                paZuteilung = (int?)tRow.Field<double?>("ZuteilungPA") ?? 0;

                //geklammerte angaben entfernen
                Match m = reKlammern.Match(talentName);
                if (m != null && m.Groups.Count == 3)
                {
                    talentName = m.Groups[1].Value.Trim();
                }

                // Sonderfälle: Sprachen Kennen und Lesen/Schreiben
                if (talentName.StartsWith("L/S: "))
                    talentName = string.Format("Lesen/Schreiben ({0})", talentName.Replace("L/S: ", string.Empty));
                else if (talentName.StartsWith("Sprachen Kennen: "))
                    talentName = string.Format("Sprachen Kennen ({0})", talentName.Replace("Sprachen Kennen: ", string.Empty));

                Talent t = Global.ContextHeld.LoadTalentByName(talentName);
                if (t == null)
                    if (_talentMapping.ContainsKey(talentName.ToLowerInvariant())) // Talent wurde nicht gefunden, evtl. Konvertierung möglich
                        t = Global.ContextHeld.LoadTalentByName(_talentMapping[talentName.ToLowerInvariant()]);
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

        private static void ImportSonderfertigkeiten(OleDbConnection conn, Held _held, System.Collections.Generic.List<string> _importLog)
        {
            string sfName = string.Empty;
            string wertString = string.Empty;

            DataTable dt = GetTable(conn, "Select * from [MG_Talente$] where Aktiviert=True");

            foreach (DataRow tRow in dt.Rows)
            {
                sfName = tRow.Field<string>("Name");
                if (sfName == null)
                    continue;
                sfName = sfName.Trim();
                wertString = tRow.Field<string>("Wert");
                if (wertString != null)
                    wertString = wertString.Trim();

                bool added = false;

                string sfNameNeu = sfName;
                //sonderfälle
                if (!added && sfName.StartsWith("Waffenlos "))
                {
                    sfNameNeu = String.Format("Waffenlose Kampftechnik ({0})", sfName.Replace("Waffenlos ", ""));
                    added = AddSonderfertigkeitTryMapping(sfNameNeu, wertString, _held);
                }
                if (!added && sfName.StartsWith("Karmalqueste"))
                {
                    sfNameNeu = "Karmalqueste";
                    added = AddSonderfertigkeit(sfNameNeu, wertString, _held);
                }
                if (!added && sfName.StartsWith("Spätweihe "))
                {
                    string göttername = sfName.Replace("Spätweihe ", "").Trim(); //enthält nun die Gottheit oder nichts
                    //nach Gottheit uterscheiden:
                    //Spätweihe Alveranische Gottheit
                    //Spätweihe Dunkle Zeiten
                    //Spätweihe Namenloser
                    //Spätweihe Nichtalveranische Gottheit
                    sfNameNeu = String.Format("Spätweihe {0}", GetGötterArt(göttername, true));
                    added = AddSonderfertigkeit(sfNameNeu, wertString, _held);
                }
                //TODO: Ritualkenntnis, Merkmalskenntnis, Schamanen, Geländekunde mappen, Rituale, Liturgien
                

                //allgemein
                sfNameNeu = sfName;
                if (wertString != null && wertString != string.Empty)
                {
                    sfNameNeu = String.Format("{0} ({1})", sfName, wertString);
                    added = AddSonderfertigkeitTryMapping(sfNameNeu, wertString, _held);
                }
                sfNameNeu = sfName;
                added = AddSonderfertigkeitTryMapping(sfNameNeu, wertString, _held);

                //weitere probieren
                if (!added)
                    added = AddSonderfertigkeit(string.Format("Waffenloses Manöver ({0})", sfName), wertString, _held); // waffenlose manöver


                // Import nicht möglich

                if (!added) 
                    AddImportLog(ImportTypen.Sonderfertigkeit, sfName, wertString, _importLog);

            //    // Kulturkunde,  Scharfschütze, Meisterschütze, Schnellladen
            //    if (!added && (sfName == "Kulturkunde" || sfName == "Scharfschütze"
            //        || sfName == "Meisterschütze" || sfName == "Schnellladen"))
            //    {
            //        string sub = null;
            //        if (sfName == "Kulturkunde")
            //            sub = "kultur";
            //        else if (sfName == "Ortskenntnis")
            //            sub = "auswahl";
            //        else if (sfName == "Scharfschütze" || sfName == "Meisterschütze" || sfName == "Schnellladen")
            //            sub = "talent";
            //        XmlNodeList subNodes = _xmlDoc.SelectNodes(string.Format("{2}[contains(@name,'{0}')]/{1}", sfName.Replace("'", "&apos;"), sub, sfXpath));
            //        foreach (XmlNode s in subNodes)
            //        {
            //            string sfNameNeu = string.Format("{0} ({1})", sfName, s.Attributes["name"].Value);
                //if (_sonderfertigkeitMapping.ContainsKey(sfNameNeu.ToLowerInvariant()))
                //    sfNameNeu = _sonderfertigkeitMapping[sfNameNeu.ToLowerInvariant()];

            //            added = AddSonderfertigkeit(sfNameNeu, wertString, _held);
            //            if (!added) // Import nicht mögliche
            //                AddImportLog(ImportTypen.Sonderfertigkeit, sfName, s.Attributes["name"].Value, _importLog);
            //        }
            //        continue;
            //    }
            //    // Rüstungsgewöhnung I, Ortskenntnis
            //    if (!added && (sfName == "Rüstungsgewöhnung I" || sfName == "Ortskenntnis"))
            //    {
            //        string sub = null;
            //        if (sfName == "Rüstungsgewöhnung I")
            //            sub = "gegenstand";
            //        else if (sfName == "Ortskenntnis")
            //            sub = "auswahl";
            //        XmlNodeList subNodes = _xmlDoc.SelectNodes(string.Format("{2}[contains(@name,'{0}')]/{1}", sfName.Replace("'", "&apos;"), sub, sfXpath));
            //        foreach (XmlNode s in subNodes)
            //        {
            //            added = AddSonderfertigkeit(sfName, s.Attributes["name"].Value, _held);
            //            if (!added) // Import nicht mögliche
            //                AddImportLog(ImportTypen.Sonderfertigkeit, sfName, s.Attributes["name"].Value, _importLog);
            //        }
            //        continue;
            //    }
            //    // Talentspezialisierung
            //    if (!added && sfName.StartsWith("Talentspezialisierung"))
            //        added = AddSonderfertigkeit("Talentspezialisierung", sfName.Replace("Talentspezialisierung ", null), _held);
            //    // Zauberspezialisierung
            //    if (!added && sfName.StartsWith("Zauberspezialisierung"))
            //        added = AddSonderfertigkeit("Zauberspezialisierung", sfName.Replace("Zauberspezialisierung ", null), _held);
            //    // Ritualkenntnis (Schamanentradition)
            //    if (!added && sfName.StartsWith("Ritualkenntnis"))
            //        added = AddSonderfertigkeit(sfName.Replace("Ritualkenntnis: ", "Ritualkenntnis (") + ")", wertString, _held);
            //    // Göttliche....
            //    if (!added && (sfName.StartsWith("Göttliche Beseelung rufen") || sfName.StartsWith("Göttliche Essenz kanalisieren")
            //        || sfName.StartsWith("Göttliche Macht binden") || sfName.StartsWith("Göttlichen Schutz erflehen")
            //        || sfName.StartsWith("Göttlichen Willen erzwingen") || sfName.StartsWith("Göttliches Prinzip stärken")))
            //        added = AddSonderfertigkeit("Liturgie: " + sfName, wertString, _held);
            //    // Waffenlose Kampftechniken
            //    if (!added && sfName.StartsWith("Waffenloser Kampfstil"))
            //        added = AddSonderfertigkeit(sfName.Replace("Waffenloser Kampfstil: ", "Waffenlose Kampftechnik (") + ")", wertString, _held);
            //    if (!added && sfName.StartsWith("Merkmalskenntnis"))
            //        added = AddSonderfertigkeit(sfName.Replace("Merkmalskenntnis: ", "Merkmalskenntnis (") + ")", wertString, _held); // Merkmalskenntnis
            //    if (!added && sfName.StartsWith("Gabe des Odûn"))
            //        added = AddSonderfertigkeit(sfName.Replace("Gabe des Odûn", "Odûn-Gabe"), wertString, _held); // Odûn-Gabe
            //    if (!added && sfName.StartsWith("Ritual:"))
            //        added = AddSonderfertigkeit(sfName.Replace("Ritual:", "Schamanenritual:"), wertString, _held); // Schamanenritual
            //    if (!added)
            //        added = AddSonderfertigkeit(sfName, wertString, _held);
            //    if (!added)
            //        added = AddSonderfertigkeit(string.Format("Geländekunde ({0})", sfName), wertString, _held); // Geländekunden

            //    if (!added)
            //    {
            //        if (_sonderfertigkeitMapping.ContainsKey(sfName.ToLowerInvariant()))
            //            added = AddSonderfertigkeit(_sonderfertigkeitMapping[sfName.ToLowerInvariant()], wertString, _held);
            //    }
            //    // Sonderfertigkeit wurde immer noch nicht gefunden, evtl. Mapping mit Wert möglich
            //    if (!added)
            //        added = AddSonderfertigkeit(sfName + " " + wertString, null, _held);
            //    // Vor-/Nachteil wurde immer noch nicht gefunden, evtl. Mapping mit Wert möglich
            //    if (!added)
            //    {
            //        if (_sonderfertigkeitMapping.ContainsKey((sfName + " " + wertString).ToLowerInvariant()))
            //            added = AddSonderfertigkeit(_sonderfertigkeitMapping[(sfName + " " + wertString).ToLowerInvariant()], null, _held);
            //    }

            //    if (!added) // Import nicht mögliche
            //        AddImportLog(ImportTypen.Sonderfertigkeit, sfName, wertString, _importLog);
            }
        }

        private static void ImportVorNachteile(OleDbConnection conn, Held _held, System.Collections.Generic.List<string> _importLog)
        {
            string vorNachteilName = string.Empty;
            string wertString = string.Empty;
            
            DataTable dt = GetTable(conn, "Select * from [MG_Vor$] where Aktiviert=True");

            foreach (DataRow tRow in dt.Rows)
            {
                vorNachteilName = tRow.Field<string>("Name");
                if (vorNachteilName == null)
                    continue;
                vorNachteilName = vorNachteilName.Trim();
                wertString = tRow.Field<string>("Wert");
                if (wertString != null)
                    wertString = wertString.Trim();

                bool added = false;

                if (!added && vorNachteilName == "Begabung für [Merkmal]")
                    added = AddVorNachteil(string.Format("Begabung für Merkmal ({0})", wertString), null, _held); // Begabung für Merkmal
                else if (!added && vorNachteilName == "Unfähigkeit für [Merkmal]")
                    added = AddVorNachteil(string.Format("Unfähigkeit für Merkmal ({0})", wertString), null, _held); // Unfähigkeit für Merkmal
                else if (!added && vorNachteilName == "Begabung für [Talentgruppe]")
                {
                    //TODO Talentgruppen gleichziehen
                    added = AddVorNachteil(string.Format("Begabung für Talentgruppe ({0})", (wertString == "Körperlich" ? "Körper" : wertString)), null, _held); // Begabung für Talentgruppe
                }
                else if (!added && vorNachteilName == "Unfähigkeit für [Talentgruppe]")
                {
                    //TODO Talentgruppen gleichziehen
                    added = AddVorNachteil(string.Format("Unfähigkeit für Talentgruppe ({0})", wertString), null, _held); // Unfähigkeit für Talentgruppe
                }
                else if (!added && vorNachteilName == "Geweiht ")
                {
                    var m = reKlammern.Match(vorNachteilName);
                    if (m.Groups.Count == 3)
                    {
                        string göttername = m.Groups[2].Value;
                        //nach Götternamen entscheiden: Praios -> zwölfgöttliche Kirche
                        added = AddVorNachteil(string.Format("Geweiht [{0}]", GetGötterArt(göttername)), göttername, _held); // Geweiht
                    }
                }
                
                
            //    else if (!added && vorNachteilName.StartsWith("Angst vor")) // Angst vor...
            //    {
            //        string[] angstVorTeile = vorNachteilName.Split(' ');
            //        string angstVor = angstVorTeile[2] + string.Format(" ({0})", wertString);
            //        added = AddVorNachteil("Angst vor", angstVor, _held);
            //    }
            //    else if (!added && vorNachteilName == "Vorurteile (stark)") // Vorurteile (stark)...
            //        added = AddVorNachteil("Vorurteile", string.Format("stark ({0})", vorNachteil.Attributes["value"].Value), _held);
            //    else if (!added && vorNachteilName.StartsWith("Moralkodex")) // Moralkodex
            //        added = AddVorNachteil("Moralkodex Kirche", vorNachteilName.Replace("Moralkodex [", null).TrimEnd(']'), _held);
            //    else if (!added && (vorNachteilName.StartsWith("Herausragende Eigenschaft")
            //        || vorNachteilName.StartsWith("Miserable Eigenschaft"))) // Herausragende/Miserable Eigenschaft
            //    {
            //        string eigenschaft = vorNachteilName.Split(':')[1].Trim();
            //        string eigenschaftKürzel = Eigenschaft.GetAbkürzung(eigenschaft);
            //        if (vorNachteilName.StartsWith("Herausragende Eigenschaft"))
            //            added = AddVorNachteil(vorNachteilName.Replace("Herausragende Eigenschaft: ", "Herausragende Eigenschaft (")
            //                .Replace(eigenschaft, eigenschaftKürzel + ")"), wertString, _held);
            //        else
            //            added = AddVorNachteil(vorNachteilName.Replace("Miserable Eigenschaft: ", "Miserable Eigenschaft (")
            //                .Replace(eigenschaft, eigenschaftKürzel + ")"), wertString, _held);
            //    }

            //    if (!added)
            //        added = AddVorNachteil(vorNachteilName, wertString, _held);

            //    // Vor-/Nachteil wurde nicht gefunden, evtl. Mapping möglich
            //    if (!added)
            //    {
            //        if (_vorNachteilMapping.ContainsKey(vorNachteilName.ToLowerInvariant()))
            //            added = AddVorNachteil(_vorNachteilMapping[vorNachteilName.ToLowerInvariant()], wertString, _held);
            //    }
            //    // Vor-/Nachteil wurde immer noch nicht gefunden, evtl. Mapping mit Wert möglich
            //    if (!added)
            //        added = AddVorNachteil(vorNachteilName + " " + wertString, null, _held);
            //    // Vor-/Nachteil wurde immer noch nicht gefunden, evtl. Mapping mit Wert möglich
            //    if (!added)
            //    {
            //        if (_vorNachteilMapping.ContainsKey((vorNachteilName + " " + wertString).ToLowerInvariant()))
            //            added = AddVorNachteil(_vorNachteilMapping[(vorNachteilName + " " + wertString).ToLowerInvariant()], null, _held);
            //    }

            //    if (!added) // Import nicht möglich
            //        AddImportLog(ImportTypen.VorNachteil, vorNachteilName, wertString, _importLog);
            }
        }

        private static void ImportInventar(OleDbConnection conn, Held _held, System.Collections.Generic.List<string> _importLog)
        {
            //string name = string.Empty;
            //double gewicht = 0.0;
            //double preis = 0.0;
            //int anzahl = 0;
            //string vtXpath = "helden/held/gegenstand";
            //XmlNodeList gegenstaende = _xmlDoc.SelectNodes(vtXpath);

            //foreach (XmlNode gegenstand in gegenstaende)
            //{
            //    name = gegenstand.Attributes["name"].Value.Trim();
            //    if (!Int32.TryParse(gegenstand.Attributes["anzahl"].Value.Trim(), out anzahl))
            //        anzahl = 1;
            //    if (gegenstand.HasChildNodes)
            //        foreach (XmlNode gchild in gegenstand.ChildNodes)
            //        {
            //            if (gchild.Name == "modallgemein")
            //            {
            //                foreach (XmlNode mchild in gegenstand.ChildNodes)
            //                {
            //                    if (mchild.Name == "name")
            //                    {
            //                        name = mchild.Attributes["value"].Value.Trim();
            //                    }
            //                    else if (mchild.Name == "preis")
            //                    {
            //                        Double.TryParse(mchild.Attributes["value"].Value.Trim(), out preis);
            //                    }
            //                    else if (mchild.Name == "gewicht")
            //                    {
            //                        Double.TryParse(mchild.Attributes["value"].Value.Trim(), out gewicht);
            //                    }
            //                }
            //            }
            //        }

            //    bool isnew = false;
            //    Ausrüstung a = null;
            //    Inventar i = null;

            //    if (_gegenstandMapping.ContainsKey(name.ToLowerInvariant()))
            //        name = _gegenstandMapping[name.ToLowerInvariant()];
            //    //alles durchsuchen
            //    a = Global.ContextHeld.Liste<Ausrüstung>().Where(li => li.Name.ToLowerInvariant() == name.ToLowerInvariant()).FirstOrDefault();
            //    if (i == null && a == null)
            //    {
            //        i = Global.ContextHeld.Liste<Inventar>().Where(li => li.Name.ToLowerInvariant() == name.ToLowerInvariant()).FirstOrDefault();
            //    }
            //    if (i == null && a == null)
            //    {
            //        Handelsgut h = Global.ContextHeld.Liste<Handelsgut>().Where(li => li.Name.ToLowerInvariant() == name.ToLowerInvariant()).FirstOrDefault();
            //        if (h != null)
            //        {
            //            i = new Inventar();
            //            i.Name = h.Name;
            //            i.Tags = h.Tags;
            //            i.Preis = h.Preis;
            //            i.ME = h.ME;
            //            i.Literatur = h.Literatur;
            //            i.Kategorie = h.Kategorie;
            //            i.HandelsgutGUID = h.HandelsgutGUID;
            //            i.Gewicht = h.Gewicht;
            //            i.Bemerkung = h.Bemerkung;
            //            isnew = true;
            //        }
            //    }
            //    //wenn nichts gefunden, dann einen Gegenstand im inventar neu erstellen
            //    if (a == null && i == null)
            //    {
            //        i = new Inventar();
            //        i.Name = name;
            //        i.Gewicht = gewicht;
            //        i.Kategorie = "Import";
            //        //TODO JT: Einheit automatisch bestimmen
            //        i.Preis = String.Format("{0}S", preis / 100.0);
            //        isnew = true;
            //        //gegenstand wurde nicht erkannt und wurde im Inventar hinzugefügt
            //        AddImportLog(ImportTypen.Gegenstand, name, null, _importLog);
            //    }
            //    //sonst in held_Ausrüstung oder Held_inventar hinzufügen, bzw anzahl erhöhen.
            //    if (i != null)
            //    {
            //        Held_Inventar hi = _held.Held_Inventar.Where(hhi => hhi.InventarGUID == i.InventarGUID).FirstOrDefault();
            //        if (hi == null)
            //        {
            //            hi = new Held_Inventar();
            //            hi.HeldGUID = _held.HeldGUID;
            //            hi.InventarGUID = i.InventarGUID;
            //            hi.Anzahl = anzahl;
            //            hi.Angelegt = false;
            //            hi.TrageortGUID = Guid.Parse("00000000-0000-0000-001a-000000000011"); //Rucksack
            //            if (isnew)
            //                hi.Inventar = i;
            //            _held.Held_Inventar.Add(hi);
            //        }
            //        else
            //            hi.Anzahl += anzahl;
            //    }
            //    else if (a != null)
            //    {
            //        Held_Ausrüstung ha = _held.Held_Ausrüstung.Where(hha => hha.AusrüstungGUID == a.AusrüstungGUID).FirstOrDefault();
            //        if (ha == null)
            //        {
            //            ha = new Held_Ausrüstung();
            //            ha.AusrüstungGUID = a.AusrüstungGUID;
            //            ha.HeldGUID = _held.HeldGUID;
            //            ha.Angelegt = false;
            //            ha.Anzahl = anzahl;
            //            ha.TrageortGUID = Guid.Parse("00000000-0000-0000-001a-000000000011"); //Rucksack
            //            _held.Held_Ausrüstung.Add(ha);
            //        }
            //        else
            //        {
            //            ha.Anzahl += anzahl;
            //        }
            //    }
            //}
        }


        private static bool AddVorNachteil(string vorNachteilName, string wertString, Held _held)
        {
            VorNachteil vn = Global.ContextHeld.LoadVorNachteilByName(vorNachteilName);
            if (vn != null)
            {
                Held_VorNachteil hvn = _held.Held_VorNachteil.Where(hvn1 => hvn1.HeldGUID == _held.HeldGUID && hvn1.VorNachteilGUID == vn.VorNachteilGUID).FirstOrDefault();
                if (hvn == null)
                {
                    hvn = new Held_VorNachteil();
                    hvn.HeldGUID = _held.HeldGUID;
                    hvn.VorNachteilGUID = vn.VorNachteilGUID;
                    hvn.Wert = wertString;
                    _held.Held_VorNachteil.Add(hvn);
                }
                else // Vor-Nachteil bereits vorhanden
                {
                    if ((hvn.Wert + ", " + wertString).Length > 255)
                        return false;
                    hvn.Wert += ", " + wertString;
                }
                return true;
            }
            return false;
        }

        private static bool AddSonderfertigkeitTryMapping(string sfName, string wertString, Held _held)
        {
            if (AddSonderfertigkeit(sfName, wertString, _held))
                return true;
            if (_sonderfertigkeitMapping.ContainsKey(sfName.ToLowerInvariant()))
            {
                sfName = _sonderfertigkeitMapping[sfName.ToLowerInvariant()];
                return AddSonderfertigkeit(sfName, wertString, _held);
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
                hs = _held.Held_Sonderfertigkeit.Where(hs1 => hs1.HeldGUID == _held.HeldGUID && hs1.SonderfertigkeitGUID == sf.SonderfertigkeitGUID).FirstOrDefault();
                if (hs == null)
                {
                    hs = new Held_Sonderfertigkeit();
                    hs.HeldGUID = _held.HeldGUID;
                    hs.SonderfertigkeitGUID = sf.SonderfertigkeitGUID;
                    hs.Wert = wertString;
                    _held.Held_Sonderfertigkeit.Add(hs);
                }
                else //Sonderfertigkeit bereits vorhanden
                {
                    if (hs.Wert == null || hs.Wert == String.Empty)
                        hs.Wert = wertString;
                    else
                    {
                        if ((hs.Wert + ", " + wertString).Length > 1200)
                            return false;
                        hs.Wert += ", " + wertString;
                    }
                }
                return true;
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
                    + "\n\nEinige Werte konnten nicht importiert werden.\nGegenstände, die hier aufgelistet werden, konnten denen in unserer Datenbank nicht zugeordnet wereden. Diese wurden im Inventar unter Sonstiges aufgenommen.\nWenn du bei der Verbesserung der Import-Funktion mitwirken möchtest, melde das Problem im Forum und lade die XML-Datei dort hoch (http://meistergeister.siteboard.org/f14-bug-meldungen.html). Vielen Dank!.\n\n";
                txtLog.Text += log;
                txtLog.TextWrapping = System.Windows.TextWrapping.Wrap;
                txtLog.VerticalScrollBarVisibility = System.Windows.Controls.ScrollBarVisibility.Visible;
                gui.Content = txtLog;
                gui.Show();
            }
        }

        public static bool IsHeldenblattFile(string xlsFile)
        {
            OleDbConnection conn = new OleDbConnection(String.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\"", xlsFile));
            try
            {
                conn.Open();
                DataTable tables = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                //TODO check tables: MG_Held, MG_INV, MG_Vor, MG_Nach, MG_SF
                string[] mgtables = new string[] { "MG_Held", "MG_INV", "MG_Vor", "MG_Nach", "MG_SF", "MG_Talente" };
                var view = tables.DefaultView;
                foreach (string tablename in mgtables)
                {
                    view.RowFilter = String.Format("TABLE_NAME = '{0}$'", tablename);
                    if (view.Count == 0)
                        return false;
                }
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
            finally
            {
                if ((conn.State & ConnectionState.Open) == ConnectionState.Open)
                    conn.Close();
            }
        }

    }
}
