using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//Eigene Usings
using MeisterGeister.Logic.General;

namespace MeisterGeister.Model.Service
{
    public class NamenService : ServiceBase
    {
        #region //----- FELDER -----
        //intern
        private static List<string> OrteMaraskan = new List<string>() { 
            "Achazak", "Alrurdan", "As'Far", "As'Khunchak", "Boran", "Buli", "Cavazoab", "Dinoda", "Geran", 
            "Gipflak", "Guladasbîd", "Hemandu", "Huab", "Jergan", "Mazazaoab", "Mherweggyn", "Nuran", "Senan", 
            "Sindibab", "Sinoda", "Syneggyn", "Tarschoggyn", "Tuzak", "Tzab", "Usdaran", "Uuz'Dornak", "Vezarak", 
            "Yerkilan", "Zinobab" };
        
        #endregion
       
        #region //----- EIGENSCHAFTEN ----


        #endregion

        #region //----- KONSTRUKTOR ----

        public NamenService()
        {
        }

        #endregion

        #region //----- DATENBANKABFRAGEN ----
        //TODO MP: Adelstitel einfügen
        public string createName(string geschlecht, Kultur kultur)
        {
            //TODO MP: keine Holberker in Personengenerator?!
            //Kultur kann mehrere Namensherkünfte haben, z.B. Mittelländische Landbevölkerung kann sein: Garehtisch, Weiden und Albernia
            List<string> kulturNamen = Liste<Kultur>().Where(k => k.Name == kultur.Name).Join(Context.Kultur_Name, k => k, kn => kn.Kultur, (k, kn) => kn).Select(s => s.Herkunft).Distinct().ToList();
            string kulturName = kulturNamen[RandomNumberGenerator.Generator.Next(kulturNamen.Count)];
            switch (kulturName)
            {
                case "Achaz Namen": return createAchazName( geschlecht, kultur);
                case "Albernische Namen":return createAlbernischenNamen( geschlecht, kultur);
                case "Almadanische Namen":return createAlmadanischenNamen(geschlecht, kultur);
                case "Andergastsche Namen":return createAndergastschenNamen(geschlecht, kultur);
                case "Aranische Namen":return createAranischenNamen(geschlecht);
                case "Bornländische Namen":return createBornländischenNamen(geschlecht, kultur);
                case "Elfische Namen":return createElfischenNamen(geschlecht, kultur);
                case "Ferkina Namen":return createFerkinaName(geschlecht, kultur);
                case "Fjarningsche Namen":return createFjarningschenNamen(geschlecht, kultur);
                case "Garethische Namen":return createGarethischenNamen(geschlecht, kultur);;
                case "Gjalskerländische Namen":return createGjalskerländischenNamen(geschlecht, kultur);
                case "Goblinische Namen":return createGoblinischenNamen(geschlecht, kultur);
                case "Grolmische Namen": return createGrolmischenNamen(geschlecht, kultur);
                case "Holberker Namen":return createHolberkerName(geschlecht, kultur);
                case "Horasische Namen":return createHorasischenNamen(geschlecht, kultur);
                case "Hügelzwergische Namen":return createHügelzwergischenNamen(geschlecht, kultur);
                case "Maraskanische Namen":return createMaraskanischenNamen(geschlecht, kultur);
                case "Nivesische Namen":return createNivesischenNamen(geschlecht, kultur);
                case "Norbardische Namen":return createNorbardischenNamen(geschlecht, kultur);
                case "Nostrische Namen":return createNostrischenNamen(geschlecht, kultur);
                case "Novadische Namen":return createNovadischenNamen(geschlecht);
                case "Orkische Namen":return createOrkischenNamen(geschlecht, kultur);
                case "Südländische Namen":return createSüdländischenNamen(geschlecht, kultur);
                case "Thorwalsche Namen":return createThorwalschenNamen(geschlecht, kultur);
                case "Tocamuyac Namen":return createTocamuyacName(geschlecht, kultur);
                case "Trollische Namen":return createTrollischenNamen(geschlecht, kultur);
                case "Trollzacker Namen":return createTrollzackerName(geschlecht, kultur);
                case "Tulamidische Namen":return createTulamidischenNamen(geschlecht, kultur);
                case "Utulu Namen":return createUtuluName(geschlecht, kultur);
                case "Waldmenschen Namen":return createWaldmenschenName(geschlecht, kultur);
                case "Weidener Namen":return createWeidenerName(geschlecht, kultur);
                case "Zahori Namen":return createZahoriNamen(geschlecht, kultur);
                case "Zwergische Namen":return createZwergischenNamen(geschlecht, kultur);
                case "Zyklopäische Namen":return createZyklopäischenNamen(geschlecht, kultur);
                default:  throw new NotImplementedException();
            }         
        }

        private string createHolberkerName(string geschlecht, Kultur kultur)
        {/* Holberker Namen haben einen Vornamen und Nachnamen
          */
            //Vorname
            List<string> vornamen = Liste<Kultur>().Where(k => k.Name == kultur.Name).Join(Context.Kultur_Name, k => k, kn => kn.Kultur, (k, kn) => kn)
                 .Join(Context.Name, kn => kn.Herkunft, n => n.Herkunft, (kn, n) => n)
                 .Where(n => n.Art == "Vorname" && (n.Geschlecht == geschlecht || n.Geschlecht == null)).Select(n => n.Name1).Distinct().ToList();
            string name = vornamen[RandomNumberGenerator.Generator.Next(vornamen.Count)];

            //Nachname
            List<string> nachnamen = Liste<Kultur>().Where(k => k.Name == kultur.Name).Join(Context.Kultur_Name, k => k, kn => kn.Kultur, (k, kn) => kn)
                .Join(Context.Name, kn => kn.Herkunft, n => n.Herkunft, (kn, n) => n)
                .Where(n => n.Art == "Nachname" && (n.Geschlecht == geschlecht || n.Geschlecht == null)).Select(n => n.Name1).Distinct().ToList();
            name += " " + nachnamen[RandomNumberGenerator.Generator.Next(nachnamen.Count)];

            return name;
        }

        private string createZahoriNamen(string geschlecht, Kultur kultur)
        {/* Zahori Namen haben einen Vornamen und Nachnamen
          */
            //Vorname
            List<string> vornamen = Liste<Kultur>().Where(k => k.Name == kultur.Name).Join(Context.Kultur_Name, k => k, kn => kn.Kultur, (k, kn) => kn)
                 .Join(Context.Name, kn => kn.Herkunft, n => n.Herkunft, (kn, n) => n)
                 .Where(n => n.Art == "Vorname" && (n.Geschlecht == geschlecht || n.Geschlecht == null)).Select(n => n.Name1).Distinct().ToList();
            string name = vornamen[RandomNumberGenerator.Generator.Next(vornamen.Count)];

            //Nachname
            List<string> nachnamen = Liste<Kultur>().Where(k => k.Name == kultur.Name).Join(Context.Kultur_Name, k => k, kn => kn.Kultur, (k, kn) => kn)
                .Join(Context.Name, kn => kn.Herkunft, n => n.Herkunft, (kn, n) => n)
                .Where(n => n.Art == "Nachname" && (n.Geschlecht == geschlecht || n.Geschlecht == null)).Select(n => n.Name1).Distinct().ToList();
            name += " " + nachnamen[RandomNumberGenerator.Generator.Next(nachnamen.Count)];

            return name;
        }

        private string createTrollischenNamen(string geschlecht, Kultur kultur)
        {/* Utulu Namen haben nur einen Vornamen
          */
            //Vorname
            List<string> vornamen = Liste<Kultur>().Where(k => k.Name == kultur.Name).Join(Context.Kultur_Name, k => k, kn => kn.Kultur, (k, kn) => kn)
                 .Join(Context.Name, kn => kn.Herkunft, n => n.Herkunft, (kn, n) => n)
                 .Where(n => n.Art == "Vorname" && (n.Geschlecht == geschlecht || n.Geschlecht == null)).Select(n => n.Name1).Distinct().ToList();
            string name = vornamen[RandomNumberGenerator.Generator.Next(vornamen.Count)];

            return name;
        }

        private string createUtuluName(string geschlecht, Kultur kultur)
        {/* Utulu Namen haben nur einen Vornamen, 
          * TODO MP: mehr Namen integrieren
          */
            //Vorname
            List<string> vornamen = Liste<Kultur>().Where(k => k.Name == kultur.Name).Join(Context.Kultur_Name, k => k, kn => kn.Kultur, (k, kn) => kn)
                 .Join(Context.Name, kn => kn.Herkunft, n => n.Herkunft, (kn, n) => n)
                 .Where(n => n.Art == "Vorname" && (n.Geschlecht == geschlecht || n.Geschlecht == null)).Select(n => n.Name1).Distinct().ToList();
            string name = vornamen[RandomNumberGenerator.Generator.Next(vornamen.Count)];

            return name;
        }

        private string createTocamuyacName(string geschlecht, Kultur kultur)
        {/* Tocamuyac Namen haben nur einen Vornamen, 
          * TODO MP: mehr Namen integrieren
          */
            //Vorname
            List<string> vornamen = Liste<Kultur>().Where(k => k.Name == kultur.Name).Join(Context.Kultur_Name, k => k, kn => kn.Kultur, (k, kn) => kn)
                 .Join(Context.Name, kn => kn.Herkunft, n => n.Herkunft, (kn, n) => n)
                 .Where(n => n.Art == "Vorname" && (n.Geschlecht == geschlecht || n.Geschlecht == null)).Select(n => n.Name1).Distinct().ToList();
            string name = vornamen[RandomNumberGenerator.Generator.Next(vornamen.Count)];

            return name;
        }

        private string createSüdländischenNamen(string geschlecht, Kultur kultur)
        {/* Südländische Namen haben mehrere Vornamen, annotiert mit Vorname
          * Koseformen werden mit "ito","elo"(m) oder "ita","ela"
          * bei Adel wird mit der Partikel "A'" vor den Wohnort gesetzt und angehängt und hängen mit "dyll"(m) oder "dylli"(w) zusaätzlich den Abstammungsort an
          */
            //Vorname
            List<string> vornamen = Liste<Kultur>().Where(k => k.Name == kultur.Name).Join(Context.Kultur_Name, k => k, kn => kn.Kultur, (k, kn) => kn)
                 .Join(Context.Name, kn => kn.Herkunft, n => n.Herkunft, (kn, n) => n)
                 .Where(n => n.Art == "Vorname" && (n.Geschlecht == geschlecht || n.Geschlecht == null)).Select(n => n.Name1).Distinct().ToList();
            string name = vornamen[RandomNumberGenerator.Generator.Next(vornamen.Count)];
            if (geschlecht == "w")
            {
                if (RandomNumberGenerator.Generator.Next(1, 3) == 1) name += "(ita)";
                else name += "(ela)";
            }
            else{
                if (RandomNumberGenerator.Generator.Next(1, 3) == 1)  name += "(ito)";
                else name += "(elo)";
            }

            //1-2 weitere Vornamen
            int ran = RandomNumberGenerator.Generator.Next(1, 3);
            for (int i = 0; i < ran; i++)
            {
                name += " " + vornamen[RandomNumberGenerator.Generator.Next(vornamen.Count)];
            }

            //Nachname
            //TODO MP: weitere nachnamen durch Vornamen durch ersetzen von "o" und "a" durch "ez" oder "uez"
            List<string> nachnamen = Liste<Kultur>().Where(k => k.Name == kultur.Name).Join(Context.Kultur_Name, k => k, kn => kn.Kultur, (k, kn) => kn)
                 .Join(Context.Name, kn => kn.Herkunft, n => n.Herkunft, (kn, n) => n)
                 .Where(n => n.Art == "Nachname" && (n.Geschlecht == geschlecht || n.Geschlecht == null)).Select(n => n.Name1).Distinct().ToList();
            name += " " + nachnamen[RandomNumberGenerator.Generator.Next(nachnamen.Count)];

            return name;
        }

        private string createZyklopäischenNamen(string geschlecht, Kultur kultur)
        {/* Zyklopäische Namen haben einen Vornamen, annotiert mit Vorname
          * bei Adel wird mit der Partikel "A'" vor den Wohnort gesetzt und angehängt und hängen mit "dyll"(m) oder "dylli"(w) zusaätzlich den Abstammungsort an
          */
            //TODO MP: Stand üergeben und beachten
            //Vorname
            List<string> vornamen = Liste<Kultur>().Where(k => k.Name == kultur.Name).Join(Context.Kultur_Name, k => k, kn => kn.Kultur, (k, kn) => kn)
                 .Join(Context.Name, kn => kn.Herkunft, n => n.Herkunft, (kn, n) => n)
                 .Where(n => n.Art == "Vorname" && (n.Geschlecht == geschlecht || n.Geschlecht == null)).Select(n => n.Name1).Distinct().ToList();
            string name = vornamen[RandomNumberGenerator.Generator.Next(vornamen.Count)];
            string nameBedeutung = name;

            //Nachname
            List<string> nachnamen = Liste<Kultur>().Where(k => k.Name == kultur.Name).Join(Context.Kultur_Name, k => k, kn => kn.Kultur, (k, kn) => kn)
                .Join(Context.Name, kn => kn.Herkunft, n => n.Herkunft, (kn, n) => n)
                .Where(n => n.Art == "Nachname" && (n.Geschlecht == geschlecht || n.Geschlecht == null)).Select(n => n.Name1).Distinct().ToList();
            int random = RandomNumberGenerator.Generator.Next(nachnamen.Count);
            name += " " + nachnamen[random];
            nameBedeutung+= " "+ nachnamen[random];
                        //Adel in 10%
            if (RandomNumberGenerator.Generator.Next(1, 11) == 1)
            {
                List<string> orte = new List<string>(new string[] { "Akiras", "Sienna", "Merymakon", "Rhetis", "Garén", "Mura", "Drum", "Athyros", "Tul'ka'var", "Tyrakos", "Balträa", "Aryios", "Kutaki", "Ferein", "Rhun", "Putras", "Kemethis", "Ayodon", "Teremon", "Skebos", "Laryios", "Palakar", "Lyios", "Rhetis", "Garén" });
                random = RandomNumberGenerator.Generator.Next(orte.Count);
                name += " A'" + orte[random];
                nameBedeutung += " wohnt in " + orte[random];
                random = RandomNumberGenerator.Generator.Next(orte.Count);
                if (geschlecht == "w") name += " dylli " + orte[random];
                else name += " dyll " + orte[random];
                nameBedeutung += " und kommt aus " + orte[random] + " (Adlig)";
            }
            else nameBedeutung = "";
            return name+"|"+nameBedeutung;
        }

        private string createHorasischenNamen(string geschlecht, Kultur kultur)
        {/* Horasische Namen haben einen Vornamen, annotiert mit Vorname
          * bei Adel je nach Rang "ya","de","di","du","della"
          * Nachname bei Stadtfreien wie aufgeführt
          */                       
            //TODO MP: Stand Übergeben und anwenden  
            //Vorname
            List<string> vornamen = Liste<Kultur>().Where(k => k.Name == kultur.Name).Join(Context.Kultur_Name, k => k, kn => kn.Kultur, (k, kn) => kn)
                 .Join(Context.Name, kn => kn.Herkunft, n => n.Herkunft, (kn, n) => n)
                 .Where(n => n.Art == "Vorname" && (n.Geschlecht == geschlecht || n.Geschlecht == null)).Select(n => n.Name1).Distinct().ToList();
            string name = vornamen[RandomNumberGenerator.Generator.Next(vornamen.Count)];
            
            //Adel in 10%
            if (RandomNumberGenerator.Generator.Next(1, 11) == 1)
            {
                List<string> adel = new List<string>(new string[] { "ya", "de", "di", "du", "della" });
                name += " "+adel[RandomNumberGenerator.Generator.Next(adel.Count)];
            }

            //Nachname
            List<string> nachnamen = Liste<Kultur>().Where(k => k.Name == kultur.Name).Join(Context.Kultur_Name, k => k, kn => kn.Kultur, (k, kn) => kn)
                .Join(Context.Name, kn => kn.Herkunft, n => n.Herkunft, (kn, n) => n)
                .Where(n => n.Art == "Nachname" && (n.Geschlecht == geschlecht || n.Geschlecht == null)).Select(n => n.Name1).Distinct().ToList();
            name += " " + nachnamen[RandomNumberGenerator.Generator.Next(nachnamen.Count)];

            return name;
        }

        private string createWeidenerName(string geschlecht, Kultur kultur)
        {/* Weidener Namen haben einen Vornamen, annotiert mit Vorname
          * Nachname bei Stadtfreien wie aufgeführt
          * TODO MP: bei Landfreien "von" +Herkunftsort
          * TODO MP: bei Leibeigenen "aus" + Herkunftsort
          */
            //Vorname
            List<string> vornamen = Liste<Kultur>().Where(k => k.Name == kultur.Name).Join(Context.Kultur_Name, k => k, kn => kn.Kultur, (k, kn) => kn)
                 .Join(Context.Name, kn => kn.Herkunft, n => n.Herkunft, (kn, n) => n)
                 .Where(n => n.Art == "Vorname" && (n.Geschlecht == geschlecht || n.Geschlecht == null)).Select(n => n.Name1).Distinct().ToList();
            string name = vornamen[RandomNumberGenerator.Generator.Next(vornamen.Count)];

            //Nachname, nur Stadtfreie
            List<string> nachnamen = Liste<Kultur>().Where(k => k.Name == kultur.Name).Join(Context.Kultur_Name, k => k, kn => kn.Kultur, (k, kn) => kn)
                .Join(Context.Name, kn => kn.Herkunft, n => n.Herkunft, (kn, n) => n)
                .Where(n => n.Art == "Nachname" && (n.Geschlecht == geschlecht || n.Geschlecht == null)).Select(n => n.Name1).Distinct().ToList();
            name += " " + nachnamen[RandomNumberGenerator.Generator.Next(nachnamen.Count)];          

            return name;
        }

        private string createNostrischenNamen(string geschlecht, Kultur kultur)
        {/* Nostrische Namen haben einen Vornamen, annotiert mit Vorname
          * Nachname nur bei Leuten von höherem Stand
          */
            //Vorname
            List<string> vornamen = Liste<Kultur>().Where(k => k.Name == kultur.Name).Join(Context.Kultur_Name, k => k, kn => kn.Kultur, (k, kn) => kn)
                 .Join(Context.Name, kn => kn.Herkunft, n => n.Herkunft, (kn, n) => n)
                 .Where(n => n.Art == "Vorname" && (n.Geschlecht == geschlecht || n.Geschlecht == null)).Select(n => n.Name1).Distinct().ToList();
            string name = vornamen[RandomNumberGenerator.Generator.Next(vornamen.Count)];

            //Nachname, in 20% der Fälle
            if (RandomNumberGenerator.Generator.Next(1, 6) == 1)
            {
                List<string> nachnamen = Liste<Kultur>().Where(k => k.Name == kultur.Name).Join(Context.Kultur_Name, k => k, kn => kn.Kultur, (k, kn) => kn)
                  .Join(Context.Name, kn => kn.Herkunft, n => n.Herkunft, (kn, n) => n)
                  .Where(n => n.Art == "Nachname" && (n.Geschlecht == geschlecht || n.Geschlecht == null)).Select(n => n.Name1).Distinct().ToList();
                name += " " + nachnamen[RandomNumberGenerator.Generator.Next(nachnamen.Count)];
            }

            return name;
        }

        private string createHügelzwergischenNamen(string geschlecht, Kultur kultur)
        {/* Zwergische Namen haben eine Anfangssilbe gefolgt von 
          */

            //create Vorname
            List<string> vornamen = Liste<Kultur>().Where(k => k.Name == "Erzzwerge").Join(Context.Kultur_Name, k => k, kn => kn.Kultur, (k, kn) => kn)
                 .Join(Context.Name, kn => kn.Herkunft, n => n.Herkunft, (kn, n) => n)
                 .Where(n => n.Art == "Vorname" && (n.Geschlecht == geschlecht || n.Geschlecht == null)).Select(n => n.Name1).Distinct().ToList();
            string name = vornamen[RandomNumberGenerator.Generator.Next(vornamen.Count)];
            // füge 1-2 Silben dazu
            List<string> silben = Liste<Kultur>().Where(k => k.Name == "Erzzwerge").Join(Context.Kultur_Name, k => k, kn => kn.Kultur, (k, kn) => kn)
                 .Join(Context.Name, kn => kn.Herkunft, n => n.Herkunft, (kn, n) => n)
                 .Where(n => n.Art == "Nachsilbe Vorname" && (n.Geschlecht == geschlecht || n.Geschlecht == null)).Select(n => n.Name1).Distinct().ToList();
            for (int i = 0; i < RandomNumberGenerator.Generator.Next(1, 3); i++)
            {
                name += silben[RandomNumberGenerator.Generator.Next(silben.Count)];
            }

            // Endung für weibliche Zwerge
            if (geschlecht == "w")
            {
                if (RandomNumberGenerator.Generator.Next(1, 3) == 1) if (name.EndsWith("a") == false) name += "a";
                else if (name.EndsWith("e") == false) name += "e";
            }

            //create Nachname
            List<string> nachnamen = Liste<Kultur>().Where(k => k.Name == kultur.Name).Join(Context.Kultur_Name, k => k, kn => kn.Kultur, (k, kn) => kn)
                 .Join(Context.Name, kn => kn.Herkunft, n => n.Herkunft, (kn, n) => n)
                 .Where(n => n.Art == "Nachname" && (n.Geschlecht == geschlecht || n.Geschlecht == null)).Select(n => n.Name1).Distinct().ToList();

            return name + " " + nachnamen[RandomNumberGenerator.Generator.Next(nachnamen.Count)];
        }

        private string createAndergastschenNamen(string geschlecht, Kultur kultur)
        {/* Andergastsche Namen haben einen Vornamen, annotiert mit Vorname
          * verheiratete Frauen haben den Nachnamen des MAnnes mit "in"
          * Nachname nur bei Leuten von höherem Stand, normale Leute mit Herkunftsort "aus", "vom XYHof"...
          * TODO MP: Nachnamen für normale Leute
          */
            //Vorname
            List<string> vornamen = Liste<Kultur>().Where(k => k.Name == kultur.Name).Join(Context.Kultur_Name, k => k, kn => kn.Kultur, (k, kn) => kn)
                 .Join(Context.Name, kn => kn.Herkunft, n => n.Herkunft, (kn, n) => n)
                 .Where(n => n.Art == "Vorname" && (n.Geschlecht == geschlecht || n.Geschlecht == null)).Select(n => n.Name1).Distinct().ToList();
            string name = vornamen[RandomNumberGenerator.Generator.Next(vornamen.Count)];

            //Nachname, in 20% der Fälle
            if (RandomNumberGenerator.Generator.Next(1, 11) == 1)
            {
                List<string> nachnamen = Liste<Kultur>().Where(k => k.Name == kultur.Name).Join(Context.Kultur_Name, k => k, kn => kn.Kultur, (k, kn) => kn)
                  .Join(Context.Name, kn => kn.Herkunft, n => n.Herkunft, (kn, n) => n)
                  .Where(n => n.Art == "Nachname" && (n.Geschlecht == geschlecht || n.Geschlecht == null)).Select(n => n.Name1).Distinct().ToList();
                if (geschlecht == "w") name += " " + nachnamen[RandomNumberGenerator.Generator.Next(nachnamen.Count)] + "in";
                else name += " " + nachnamen[RandomNumberGenerator.Generator.Next(nachnamen.Count)];
            }

            return name;
        }

        private string createAlbernischenNamen(string geschlecht, Kultur kultur)
        {/* Albernische Namen haben einen Vornamen, annotiert mit Vorname
          * Vorname kann von bestimmten Garehtischen Vornamen abgeleitet sein
          * Nachname annotiert mit Nachname
          */
            //TODO MP: Von STand Übergeben
            //Vorname
            List<string> vornamen = Liste<Kultur>().Where(k => k.Name == kultur.Name).Join(Context.Kultur_Name, k => k, kn => kn.Kultur, (k, kn) => kn)
                 .Join(Context.Name, kn => kn.Herkunft, n => n.Herkunft, (kn, n) => n)
                 .Where(n => n.Art == "Vorname" && (n.Geschlecht == geschlecht || n.Geschlecht == null)).Select(n => n.Name1).Distinct().ToList(); 
            //List<string> vornamenGarehti = Context.Name.Where(n => (n.Herkunft == "Garethische Namen" && (n.Herkunft.EndsWith("ian") || n.Herkunft.EndsWith("dan"))) && n.Art == "Vorname" && (n.Geschlecht == geschlecht || n.Geschlecht == null)).Select(n => n.Name1).Distinct().ToList();
            //vornamen.AddRange(vornamenGarehti);     
            string name = vornamen[RandomNumberGenerator.Generator.Next(vornamen.Count)];

            if (name.EndsWith("ian")) 
            {
                if (RandomNumberGenerator.Generator.Next(1, 3) == 1) name = name.Substring(0, name.Length - 3) + "wyn";
                else name = name.Substring(0, name.Length - 3) + "uin";
            }
            if (name.EndsWith("dan"))
            {
                if (RandomNumberGenerator.Generator.Next(1, 3) == 1) name = name.Substring(0, name.Length - 3) + "tin";
                else name = name.Substring(0, name.Length - 3) + "den";
            }

            // falls von Stand 10% der Fälle
            if (RandomNumberGenerator.Generator.Next(1, 11) == 1){
                if (geschlecht =="w") name+= " ni";
                else name += " ui";
            } 

            //Nachname
            List<string> nachname = Liste<Kultur>().Where(k => k.Name == kultur.Name).Join(Context.Kultur_Name, k => k, kn => kn.Kultur, (k, kn) => kn)
                 .Join(Context.Name, kn => kn.Herkunft, n => n.Herkunft, (kn, n) => n)
                 .Where(n => n.Art == "Nachname" && (n.Geschlecht == "w" || n.Geschlecht == "m" || n.Geschlecht == null)).Select(n => n.Name1).Distinct().ToList();
            name += " " + nachname[RandomNumberGenerator.Generator.Next(nachname.Count)];

            return name;
        }

        private string createAlmadanischenNamen(string geschlecht, Kultur kultur)
        {/* Almadische Namen haben einen Vornamen, annotiert mit Vorname
          * ein zweiter Vorname nach B
          * TODO MP: mehr zweite Vornamen
          * Nachname nach einem Vornamen
          */

            List<string> zweiter = new List<string>(new string[]{"Amat","Amirat","Desider","Honor"});

            //Vorname
            List<string> vornamen = Liste<Kultur>().Where(k => k.Name == kultur.Name).Join(Context.Kultur_Name, k => k, kn => kn.Kultur, (k, kn) => kn)
                 .Join(Context.Name, kn => kn.Herkunft, n => n.Herkunft, (kn, n) => n)
                 .Where(n => n.Art == "Vorname" && (n.Geschlecht == geschlecht || n.Geschlecht == null)).Select(n => n.Name1).Distinct().ToList(); 
            string name = vornamen[RandomNumberGenerator.Generator.Next(vornamen.Count)];

            //Zweitname
            name += " "+ zweiter[RandomNumberGenerator.Generator.Next(zweiter.Count)];

            //Nachname
            List<string> nachname = Liste<Kultur>().Where(k => k.Name == kultur.Name).Join(Context.Kultur_Name, k => k, kn => kn.Kultur, (k, kn) => kn)
                 .Join(Context.Name, kn => kn.Herkunft, n => n.Herkunft, (kn, n) => n)
                 .Where(n => n.Art == "Nachname" && (n.Geschlecht == "w" || n.Geschlecht == "m" || n.Geschlecht == null)).Select(n => n.Name1).Distinct().ToList();
            name += " " + nachname[RandomNumberGenerator.Generator.Next(nachname.Count)];

            return name;
        }

        private string createBornländischenNamen(string geschlecht, Kultur kultur)
        {/* Bornländische Namen haben einen Vornamen, annotiert mit Vorname
          * Nachname, annotiert mit Nachname
          */

            //Vorname
            List<string> vornamen = Liste<Kultur>().Where(k => k.Name == kultur.Name).Join(Context.Kultur_Name, k => k, kn => kn.Kultur, (k, kn) => kn)
                 .Join(Context.Name, kn => kn.Herkunft, n => n.Herkunft, (kn, n) => n)
                 .Where(n => n.Art == "Vorname" && (n.Geschlecht == geschlecht || n.Geschlecht == null)).Select(n => n.Name1).Distinct().ToList();
            string name = vornamen[RandomNumberGenerator.Generator.Next(vornamen.Count)];

            //Nachname
            List<string> nachname = Liste<Kultur>().Where(k => k.Name == kultur.Name).Join(Context.Kultur_Name, k => k, kn => kn.Kultur, (k, kn) => kn)
                 .Join(Context.Name, kn => kn.Herkunft, n => n.Herkunft, (kn, n) => n)
                 .Where(n => n.Art == "Nachname" && (n.Geschlecht == geschlecht || n.Geschlecht == null)).Select(n => n.Name1).Distinct().ToList();
            name += " "+ nachname[RandomNumberGenerator.Generator.Next(nachname.Count)];

            return name;
        }

        private string createFjarningschenNamen(string geschlecht, Kultur kultur)
        {/* Fjarningsche Namen haben einen Vornamen mit Bedeutung, annotiert mit Vorname
          * TODO MP: Bedeutung hinzufügen
          * TODO MP: Kampf- / Heldennamen hinzufügen
          */
            //Vorname
            List<string> vornamen = Liste<Kultur>().Where(k => k.Name == kultur.Name).Join(Context.Kultur_Name, k => k, kn => kn.Kultur, (k, kn) => kn)
                 .Join(Context.Name, kn => kn.Herkunft, n => n.Herkunft, (kn, n) => n)
                 .Where(n => n.Art == "Vorname" && (n.Geschlecht == geschlecht || n.Geschlecht == null)).Select(n => n.Name1).Distinct().ToList();
            string name = vornamen[RandomNumberGenerator.Generator.Next(vornamen.Count)];

            return name;
        }

        private string createGjalskerländischenNamen(string geschlecht, Kultur kultur)
        {/* Gjalskerländische Namen haben einen Vornamen, annotiert mit Vorname
          * Zweitnamen mit "bren"(m) oder "brai"(w) nach Mutter oder Vater
          */
            //Vorname
            List<string> vornamen = Liste<Kultur>().Where(k => k.Name == kultur.Name).Join(Context.Kultur_Name, k => k, kn => kn.Kultur, (k, kn) => kn)
                 .Join(Context.Name, kn => kn.Herkunft, n => n.Herkunft, (kn, n) => n)
                 .Where(n => n.Art == "Vorname" && (n.Geschlecht == geschlecht || n.Geschlecht == null)).Select(n => n.Name1).Distinct().ToList();
            string name = vornamen[RandomNumberGenerator.Generator.Next(vornamen.Count)];
            string nameBedeutung = name;
            //Zweitname, in 5% ein schamane
            if (RandomNumberGenerator.Generator.Next(1, 21) == 1)
            {
                List<string> durName = Liste<Kultur>().Where(k => k.Name == kultur.Name).Join(Context.Kultur_Name, k => k, kn => kn.Kultur, (k, kn) => kn)
                     .Join(Context.Name, kn => kn.Herkunft, n => n.Herkunft, (kn, n) => n)
                     .Where(n => n.Art == "Vorname" && (n.Geschlecht == "w" || n.Geschlecht == "m" || n.Geschlecht == null)).Select(n => n.Name1).Distinct().ToList();
                int random = RandomNumberGenerator.Generator.Next(durName.Count);
                name += " dur " + durName[random];
                nameBedeutung += ", Hüter des Geistes/Wissens von " + durName[random] + " (Schamane)";
            }
            else
            {
                int random = RandomNumberGenerator.Generator.Next(vornamen.Count);
                if (geschlecht == "w")
                {
                    name += " bren " + vornamen[random];
                    nameBedeutung += " Sohn des " + vornamen[random];
                }
                else
                {
                    name += " brai " + vornamen[random];
                    nameBedeutung += " Tochter der " + vornamen[random];
                }
            }

            return name+"|"+nameBedeutung;
        }

        private string createThorwalschenNamen(string geschlecht, Kultur kultur)
        {/* Thorwalsche Namen haben einen Vornamen mit Bedeutung, annotiert mit Vorname
          * TODO MP: Bedeutung hinzufügen
          * bei erprobten Recken wird Verkleinerungsform "ske","ke"(m) oder "je","ja"(w) angehängt
          * TODO MP: Heldennamen hinzufügen
          * Zweitnamen nach Mutter oder Vater mit angehängtem "son"(m) oder "dottir"
          */

            //Vorname
            List<string> vornamen = Liste<Kultur>().Where(k => k.Name == kultur.Name).Join(Context.Kultur_Name, k => k, kn => kn.Kultur, (k, kn) => kn)
                 .Join(Context.Name, kn => kn.Herkunft, n => n.Herkunft, (kn, n) => n)
                 .Where(n => n.Art == "Vorname" && (n.Geschlecht == geschlecht || n.Geschlecht == null)).Select(n => n.Name1).Distinct().ToList();
            string name = vornamen[RandomNumberGenerator.Generator.Next(vornamen.Count)];
            //erprobt in 30% der Fälle
            if(RandomNumberGenerator.Generator.Next(1,11) <=3){
                if(geschlecht=="w"){
                    if (RandomNumberGenerator.Generator.Next(1, 3) == 1) name += "je";
                    else name += "ja";
                }else{
                    if (RandomNumberGenerator.Generator.Next(1, 3) == 1) name += "ske";
                    else name += "ke";
                }
            }

            //Zweitname
            List<string> zweitnamen = Liste<Kultur>().Where(k => k.Name == kultur.Name).Join(Context.Kultur_Name, k => k, kn => kn.Kultur, (k, kn) => kn)
                 .Join(Context.Name, kn => kn.Herkunft, n => n.Herkunft, (kn, n) => n)
                 .Where(n => n.Art == "Vorname" && (n.Geschlecht == "w" || n.Geschlecht == "m" || n.Geschlecht == null)).Select(n => n.Name1).Distinct().ToList();
            name += " "+ zweitnamen[RandomNumberGenerator.Generator.Next(zweitnamen.Count)];
            if (geschlecht == "m") name += "son";
            else name += "dottir";

            return name;
        }

        private string createNivesischenNamen(string geschlecht, Kultur kultur)
        {/* Nivesische Namen haben einen Vornamen, annotiert mit Vorname
          * weibliche Vornamen können von männlichen abstammen mit Anhang "a" oder "ja"
          * TODO MP: Wolfsnamen
          * Nachnamen sind in der Form "von XYs Stamm"
          */

            //Vorname
            List<string> vornamen = Liste<Kultur>().Where(k => k.Name == kultur.Name).Join(Context.Kultur_Name, k => k, kn => kn.Kultur, (k, kn) => kn)
                 .Join(Context.Name, kn => kn.Herkunft, n => n.Herkunft, (kn, n) => n)
                 .Where(n => n.Art == "Vorname" && (n.Geschlecht == geschlecht || n.Geschlecht == null)).Select(n => n.Name1).Distinct().ToList();
            string name = vornamen[RandomNumberGenerator.Generator.Next(vornamen.Count)];

            //Stamm
            List<string> stamm = Liste<Kultur>().Where(k => k.Name == kultur.Name).Join(Context.Kultur_Name, k => k, kn => kn.Kultur, (k, kn) => kn)
                 .Join(Context.Name, kn => kn.Herkunft, n => n.Herkunft, (kn, n) => n)
                 .Where(n => n.Art == "Vorname" && (n.Geschlecht == "w" || n.Geschlecht =="m" || n.Geschlecht == null)).Select(n => n.Name1).Distinct().ToList();
            name += " von " + stamm[RandomNumberGenerator.Generator.Next(stamm.Count)] + "s Stamm";

            return name;

        }

        private string createNorbardischenNamen(string geschlecht, Kultur kultur)
        {/* Norbardische Namen einen Vornamen, annotiert mit Vorname
          * weibliche Vornamen können von männlichen abstammen mit Anhang "a" oder "ja"
          * Nachnamen sind eher Sippennamen 
          */

            //create Vorname
            string name="Dummy";
            if (geschlecht == "w")
            {
                List<Name> vornamen = Liste<Kultur>().Where(k => k.Name == kultur.Name).Join(Context.Kultur_Name, k => k, kn => kn.Kultur, (k, kn) => kn)
                     .Join(Context.Name, kn => kn.Herkunft, n => n.Herkunft, (kn, n) => n)
                     .Where(n => n.Art == "Vorname" ).ToList();
                Name vorname = vornamen[RandomNumberGenerator.Generator.Next(vornamen.Count)];
                if (vorname.Geschlecht =="m" && (vorname.Name1.EndsWith("a") || vorname.Name1.EndsWith("e") || vorname.Name1.EndsWith("i") || vorname.Name1.EndsWith("o") || vorname.Name1.EndsWith("u"))) name = vorname.Name1+"ja";
                else if (vorname.Geschlecht =="m" && !(vorname.Name1.EndsWith("a") || vorname.Name1.EndsWith("e") || vorname.Name1.EndsWith("i") || vorname.Name1.EndsWith("o") || vorname.Name1.EndsWith("u"))) name = vorname.Name1+"a";
                else if (vorname.Geschlecht =="w") name = vorname.Name1;
            }
            else
            {
                List<string> vornamen = Liste<Kultur>().Where(k => k.Name == kultur.Name).Join(Context.Kultur_Name, k => k, kn => kn.Kultur, (k, kn) => kn)
                     .Join(Context.Name, kn => kn.Herkunft, n => n.Herkunft, (kn, n) => n)
                     .Where(n => n.Art == "Vorname" && (n.Geschlecht == geschlecht || n.Geschlecht == null)).Select(n => n.Name1).Distinct().ToList();
                name = vornamen[RandomNumberGenerator.Generator.Next(vornamen.Count)];
            }

            //Nachname 
            List<string> nachnamen = Liste<Kultur>().Where(k => k.Name == kultur.Name).Join(Context.Kultur_Name, k => k, kn => kn.Kultur, (k, kn) => kn)
                    .Join(Context.Name, kn => kn.Herkunft, n => n.Herkunft, (kn, n) => n)
                    .Where(n => n.Art == "Nachname" && (n.Geschlecht == geschlecht || n.Geschlecht == null)).Select(n => n.Name1).Distinct().ToList();
            name += " " + nachnamen[RandomNumberGenerator.Generator.Next(nachnamen.Count)];

            return name;
        }

        private string createGarethischenNamen(string geschlecht, Kultur kultur)
        {/* Garehtische Namen haben nur einen Namen, annotiert mit Vorname
          * Nachnamen nicht immer vorhanden
          * TODO MP: Unterregionen einbeziehen?!
          */

            //create Vorname
            List<string> vornamen = Liste<Kultur>().Where(k => k.Name == kultur.Name).Join(Context.Kultur_Name, k => k, kn => kn.Kultur, (k, kn) => kn)
                 .Join(Context.Name, kn => kn.Herkunft, n => n.Herkunft, (kn, n) => n)
                 .Where(n => n.Art == "Vorname" && (n.Geschlecht == geschlecht || n.Geschlecht == null)).Select(n => n.Name1).Distinct().ToList();
            string name = vornamen[RandomNumberGenerator.Generator.Next(vornamen.Count)];

            //Nachname in 80% der Fälle
            if (RandomNumberGenerator.Generator.Next(1, 21) <= 16)
            {
                List<string> nachnamen = Liste<Kultur>().Where(k => k.Name == kultur.Name).Join(Context.Kultur_Name, k => k, kn => kn.Kultur, (k, kn) => kn)
                     .Join(Context.Name, kn => kn.Herkunft, n => n.Herkunft, (kn, n) => n)
                     .Where(n => n.Art == "Nachname" && (n.Geschlecht == geschlecht || n.Geschlecht == null)).Select(n => n.Name1).Distinct().ToList();
                // ersetzt in 30% der Fälle den Vornamen
                if (RandomNumberGenerator.Generator.Next(1, 21) <= 6) name = nachnamen[RandomNumberGenerator.Generator.Next(nachnamen.Count)];
                else name += " " + nachnamen[RandomNumberGenerator.Generator.Next(nachnamen.Count)];
            }
            return name;
        }

        private string createAranischenNamen(string geschlecht)
        {/* Aranische Namen werden mit "ibn"(m) oder "saba" ,"suni","sunni","sunya"(w) der Vatername (mitsamt Titeln) angehängt
          * zusätzlich (seltener stattdessen) wird mit "sâl"(m) oder "sâla"(w) der Name des Lehrers zugefügt
          * zusätzlich ist manchaml der Heimatname mit "ay" angehängt
          * * Ehrennamen werden mit "al" oder "el" angehängt (mit Übersetzung)
          */
            //TODO MP: mehr Orte einfügen
            List<string> städte = new List<string>(new string[] { "Zorgan", "Baburin", "Perricum", "Elburum", "Llanka", "Barbrück", "Mendlikum", "Palmyrabad", "Kunchabad", "Revennis", "Nasirabad" });
            List<string> tochter = new List<string>(new string[] { " saba ", " suni ", " sunni ", " sunya " });

            //create Vorname
            List<string> vornamen = Context.Name.Where(n => n.Herkunft == "Aranische Namen" && n.Art == "Vorname" && (n.Geschlecht == geschlecht || n.Geschlecht == null)).Select(n => n.Name1).Distinct().ToList();
            string name = vornamen[RandomNumberGenerator.Generator.Next(vornamen.Count)];
            string nameBedeutung = name;

            //Nachname (Name des Vaters)
            List<string> nachnamen = Context.Name.Where(n => n.Herkunft == "Aranische Namen" && n.Art == "Vorname" && (n.Geschlecht == "m" || n.Geschlecht == null)).Select(n => n.Name1).Distinct().ToList();
            int random = RandomNumberGenerator.Generator.Next(nachnamen.Count);
            if (geschlecht == "m")
            {
                name += " ibn " + nachnamen[random];
                nameBedeutung+=", Sohn des "+ nachnamen[random];
            }
            else
            {
                name += tochter[RandomNumberGenerator.Generator.Next(tochter.Count)] + nachnamen[random];
                nameBedeutung += ", Tochter des " + nachnamen[random];
            }

            //Name des Lehrers in 15% der Fälle
            if (RandomNumberGenerator.Generator.Next(1, 21) <= 3)
            {
                random = RandomNumberGenerator.Generator.Next(nachnamen.Count);
                if (geschlecht == "m")
                {
                    name += " sâl " + nachnamen[random];
                    nameBedeutung += ", Schüler von " + nachnamen[random];
                }
                else
                {
                    name += " sâla " + nachnamen[random];
                    nameBedeutung += ", Schülerin von " + nachnamen[random];
                }
            }

            //Heimatname
            random = RandomNumberGenerator.Generator.Next(städte.Count);
            name += " ay " + städte[random];
            nameBedeutung += "aus " + städte[random];
            //Ehrenname in 10% der Fälle
            if (RandomNumberGenerator.Generator.Next(1, 11) == 1)
            {
                List<Name> ehrennamen = Context.Name.Where(n => n.Herkunft == "Tulamidische Namen" && n.Art == "Ehrenname" && (n.Geschlecht == geschlecht || n.Geschlecht == null)).ToList();
                Name ehrenname = ehrennamen[RandomNumberGenerator.Generator.Next(ehrennamen.Count)];
                if (RandomNumberGenerator.Generator.Next(1, 3) == 1)
                {
                    name += " al " + ehrenname.Name1 ;                    
                }
                else
                {
                    name += " el " + ehrenname.Name1 + " (" + ehrenname.Bedeutung + ")";
                }
                nameBedeutung += " \"" + ehrenname.Bedeutung + "\"";
            }

            return name+"|"+nameBedeutung;
        }

        private string createNovadischenNamen(string geschlecht)
        {/* Novadische Namen werden mit "ben"(m) oder "saba" ,"suni", "sunni", "sunya"(w) der Vatername (mitsamt Titeln)  angehängt
          * Frauen mit zwölfgöttergläubigem Vater nehmen "bint" und den Mutternamen
          * zusätzlich (seltener stattdessen) wird mit "sâl"(m) oder "sâla"(w) der Name des Lehrers zugefügt
          * Oasenname wird mit "ay" angehängt
          * Ehrennamen werden mit "al" oder "el" angehängt (mit Übersetzung)
          */
            
            //TODO MP: mehr Orte/Sippennamen einfügen
            List<string> städte = new List<string>(new string[] { "Hayàbeth", "Tarfui", "Unau", "Al'Rifat", "Keft", "El'Karram", "Manesh", "Yiyimris", "Birscha", "Kireh", "Shebah", "Terekh", "Achan", "El'Ankhra", "Virinlassih", "Eslamsbad", "Fasar" });
            List<string> tochter = new List<string>(new string[] { " saba ", " suni ", " sunni ", " sunya " });
            List<string> sippennamen = new List<string>(new string[] { "Tirah", "Ranah", "Ulah", "Sanrash" });

            //create Vorname
            List<string> vornamen = Context.Name.Where(n => n.Herkunft == "Tulamidische Namen" && n.Art == "Vorname" && (n.Geschlecht == geschlecht || n.Geschlecht == null)).Select(n => n.Name1).Distinct().ToList();
            string name = vornamen[RandomNumberGenerator.Generator.Next(vornamen.Count)];
            string nameBedeutung = name;

            //Nachname 
            List<string> nachnamen = Context.Name.Where(n => n.Herkunft == "Tulamidische Namen" && n.Art == "Vorname" && (n.Geschlecht == "m" || n.Geschlecht == null)).Select(n => n.Name1).Distinct().ToList();
            if (geschlecht == "m") {
                string nachn= nachnamen[RandomNumberGenerator.Generator.Next(nachnamen.Count)];
                name += " ben " + nachn;
                nameBedeutung +=", Sohn des "+ nachn;
                }
            else
            {
                //Zwölgöttergläubig 20% der Fälle
                if (RandomNumberGenerator.Generator.Next(1, 6) == 1)
                {
                    List<string> nachnamenMutter = Context.Name.Where(n => n.Herkunft == "Tulamidische Namen" && n.Art == "Vorname" && (n.Geschlecht == "w" || n.Geschlecht == null)).Select(n => n.Name1).Distinct().ToList();
                    string nachn =nachnamenMutter[RandomNumberGenerator.Generator.Next(nachnamenMutter.Count)];
                    name += " bint " + nachn;
                    nameBedeutung += ", Tochter der " + nachn;
                }
                else
                {
                    string nachn = nachnamen[RandomNumberGenerator.Generator.Next(nachnamen.Count)];
                    name += tochter[RandomNumberGenerator.Generator.Next(tochter.Count)] + nachn;
                    nameBedeutung += ", Tochter des " + nachn;
                }
            }

            //Name des Lehrers in 15% der Fälle
            if (RandomNumberGenerator.Generator.Next(1, 21) <= 3)
            {
                if (geschlecht == "m")
                {
                    string nachn =nachnamen[RandomNumberGenerator.Generator.Next(nachnamen.Count)];
                    name += " sâl " + nachn;
                    nameBedeutung += ", Schüler von "+nachn;
                }
                else
                {
                    string nachn =nachnamen[RandomNumberGenerator.Generator.Next(nachnamen.Count)];
                    name += " sâla " + nachn;
                    nameBedeutung += ", Schülerin von " + nachn;
                }
            }

            //Heimatname
            string stadt =städte[RandomNumberGenerator.Generator.Next(städte.Count)] ;
            name += ", ay " + stadt;
            nameBedeutung += ", aus " + stadt;
            //Ehrenname in 10% der Fälle
            if (RandomNumberGenerator.Generator.Next(1, 11) == 1)
            {
                List<Name> ehrennamen = Context.Name.Where(n => n.Herkunft == "Tulamidische Namen" && n.Art == "Ehrenname" && (n.Geschlecht == geschlecht || n.Geschlecht == null)).ToList();
                Name ehrenname = ehrennamen[RandomNumberGenerator.Generator.Next(ehrennamen.Count)];
                if (RandomNumberGenerator.Generator.Next(1, 3) == 1) name += " al " + ehrenname.Name1;// + " (" + ehrenname.Bedeutung + ")";
                else name += " el " + ehrenname.Name1;// +" (" + ehrenname.Bedeutung + ")";
                nameBedeutung += " \""+ehrenname.Bedeutung+"\"";
            }

            //Sippenname
            if (geschlecht == "m")
            {
                string sippe =sippennamen[RandomNumberGenerator.Generator.Next(sippennamen.Count)];
                name += " ben " + sippe;
                nameBedeutung +=", Sohn der Sippe "+ sippe;
            }
            else
            {
                string sippe =sippennamen[RandomNumberGenerator.Generator.Next(sippennamen.Count)];
                name += " saba " + sippe;
                nameBedeutung += ", Tochter der Sippe " + sippe;
            }

            return name+'|'+nameBedeutung;
        }

        private string createTulamidischenNamen(string geschlecht, Kultur kultur)
        {/* Tulamidischen Namen wird mit "ibn"(m) oder "saba" ,"suni","sunni","sunya"(w) der Vatername (mitsamt Titeln) angehängt
          * zusätzlich (seltener stattdessen) wird mit "sâl"(m) oder "sâla"(w) der Name des Lehrers zugefügt
          * zusätzlich ist manchaml der Heimatname mit "ay" angehängt
          * * Ehrennamen werden mit "al" oder "el" angehängt (mit Übersetzung)
          */
            //TODO MP: mehr Orte einfügen
            List<string> städte = new List<string>(new string[] { "Fasar", "Khunchom", "Rashdul", "Thalusa", "Anchopal", "Mherwed", "Temphis", "Borbra", "Naggilah", "Jindir", "Chalukand" });
            List<string> tochter = new List<string>(new string[] { " saba ", " suni ", " sunni ", " sunya " });

            //create Vorname
            List<string> vornamen = Liste<Kultur>().Where(k => k.Name == kultur.Name).Join(Context.Kultur_Name, k => k, kn => kn.Kultur, (k, kn) => kn)
                 .Join(Context.Name, kn => kn.Herkunft, n => n.Herkunft, (kn, n) => n)
                 .Where(n => n.Art == "Vorname" && (n.Geschlecht == geschlecht || n.Geschlecht == null)).Select(n => n.Name1).Distinct().ToList();
            string name = vornamen[RandomNumberGenerator.Generator.Next(vornamen.Count)];
            string nameBedeutung = name;
            //Nachname (Name des Vaters)
            List<string> nachnamen = Liste<Kultur>().Where(k => k.Name == kultur.Name).Join(Context.Kultur_Name, k => k, kn => kn.Kultur, (k, kn) => kn)
                 .Join(Context.Name, kn => kn.Herkunft, n => n.Herkunft, (kn, n) => n)
                 .Where(n => n.Art == "Vorname" && (n.Geschlecht == "m" || n.Geschlecht == null)).Select(n => n.Name1).Distinct().ToList();
            int random = RandomNumberGenerator.Generator.Next(nachnamen.Count);
            if (geschlecht == "m")
            {
                name += " ibn " + nachnamen[random];
                nameBedeutung += ", Sohn des " + nachnamen[random];
            }
            else
            {
                name += tochter[RandomNumberGenerator.Generator.Next(tochter.Count)] + nachnamen[random];
                nameBedeutung += ", Tochter des " + nachnamen[random];
            }

            //Name des Lehrers in 15% der Fälle
            if (RandomNumberGenerator.Generator.Next(1, 21) <= 3) 
            {
                random = RandomNumberGenerator.Generator.Next(nachnamen.Count);
                if (geschlecht == "m")
                {
                    name += " sâl " + nachnamen[random];
                    nameBedeutung += ", Schüler von " + nachnamen[random];
                }
                else
                {
                    name += " sâla " + nachnamen[random];
                    nameBedeutung += ", Schülerin von " + nachnamen[random];
                }
            } 

            //Heimatname
            random = RandomNumberGenerator.Generator.Next(städte.Count);
            name += " ay " + städte[random];
            nameBedeutung += ", aus " + städte[random];

            //Ehrenname in 10% der Fälle
            if (RandomNumberGenerator.Generator.Next(1, 11) == 1)
            {
                List<Name> ehrennamen = Liste<Kultur>().Where(k => k.Name == kultur.Name).Join(Context.Kultur_Name, k => k, kn => kn.Kultur, (k, kn) => kn)
                    .Join(Context.Name, kn => kn.Herkunft, n => n.Herkunft, (kn, n) => n)
                    .Where(n => n.Art == "Ehrenname" && (n.Geschlecht == geschlecht || n.Geschlecht == null)).ToList();
                Name ehrenname = ehrennamen[RandomNumberGenerator.Generator.Next(ehrennamen.Count)];
                if (RandomNumberGenerator.Generator.Next(1, 3) == 1)
                {
                    name += " al " + ehrenname.Name1;
                }
                else
                {
                    name += " el " + ehrenname.Name1;
                }
                nameBedeutung += ", \"" + ehrenname.Bedeutung + "\"";
            }

            return name+"|"+nameBedeutung;
        }

        private string createFerkinaName(string geschlecht, Kultur kultur)
        {/* Ferkina fügen dem Vornamen "iban"(m) oder "sabu"(w) mit dem Vornamen des Vaters zu (unverheiratet)
          * verheiratete Frauen tragen "zawsh(i)" und den Namen des Mannes
          * Stammesnamen werden mit "Ban"(m) oder "Banu"(w) mit dem Stammesgründer gebildet
          * TODO MP: statt Stammesgründer auch markanter Ort oder Eigenschaft
          * eventuell Bedeutung dazu
          */
            List<string> sippe = new List<string>(new string[] { " Bân ", " Ulad ", " Bem " });
            //create Vorname
            List<string> vornamen = Liste<Kultur>().Where(k => k.Name == kultur.Name).Join(Context.Kultur_Name, k => k, kn => kn.Kultur, (k, kn) => kn)
                 .Join(Context.Name, kn => kn.Herkunft, n => n.Herkunft, (kn, n) => n)
                 .Where(n => n.Art == "Vorname" && (n.Geschlecht == geschlecht || n.Geschlecht == null)).Select(n => n.Name1).Distinct().ToList();
            int random=RandomNumberGenerator.Generator.Next(vornamen.Count);
            string name = vornamen[random];
            string nameBedeutung = name;

            //Nachname
            List<string> nachnamen = Liste<Kultur>().Where(k => k.Name == kultur.Name).Join(Context.Kultur_Name, k => k, kn => kn.Kultur, (k, kn) => kn)
                 .Join(Context.Name, kn => kn.Herkunft, n => n.Herkunft, (kn, n) => n)
                 .Where(n => n.Art == "Vorname" && (n.Geschlecht == "m" || n.Geschlecht == null)).Select(n => n.Name1).Distinct().ToList();            
            random=RandomNumberGenerator.Generator.Next(nachnamen.Count);
            if (geschlecht == "m"){
                name += " iban " + nachnamen[random];
                nameBedeutung += ", Sohn des " + nachnamen[random];
            }
            //in 20% der Fälle verheiratet
            else
            {
                random = RandomNumberGenerator.Generator.Next(nachnamen.Count);
                if (RandomNumberGenerator.Generator.Next(1, 6) > 1)
                {
                    name += " sabu " + nachnamen[random];
                    nameBedeutung += ", Tochter des " + nachnamen[random];
                }
                else
                {
                    if (RandomNumberGenerator.Generator.Next(1, 3) == 1) name += " zawsh-" + nachnamen[random];
                    else name += " zawsh-i-" + nachnamen[random];
                    nameBedeutung += ", Frau des " + nachnamen[random];
                }
            }

            //Stammesname
            random = RandomNumberGenerator.Generator.Next(nachnamen.Count);
            string sippeN = sippe[RandomNumberGenerator.Generator.Next(sippe.Count)];
            if (geschlecht == "m")
            {
                name += " "+sippeN+" " + nachnamen[random];
            }
            else
            {
                name += " " + sippeN + "u " + nachnamen[random];
            }
            nameBedeutung += ", Kind von " + nachnamen[random];
            return name+"|"+nameBedeutung;
        }

        private string createTrollzackerName(string geschlecht, Kultur kultur)
        {/* Trollzacker haben meist nur einen Namen, annotiert mit Vorname
          * Nachnamen, falls vorhanden sind meist Ehren oder Schandnamen, die eventuell den Vornamen ersetzen
          */
            //create Vorname
            List<string> vornamen = Liste<Kultur>().Where(k => k.Name == kultur.Name).Join(Context.Kultur_Name, k => k, kn => kn.Kultur, (k, kn) => kn)
                 .Join(Context.Name, kn => kn.Herkunft, n => n.Herkunft, (kn, n) => n)
                 .Where(n => n.Art == "Vorname" && (n.Geschlecht == geschlecht || n.Geschlecht == null)).Select(n => n.Name1).Distinct().ToList();
            int random = RandomNumberGenerator.Generator.Next(vornamen.Count);
            string name = vornamen[random];
            //Nachname nur in 10% der Fälle
            if (RandomNumberGenerator.Generator.Next(1, 11) == 1)
            {
                List<Name> nachnamen = Liste<Kultur>().Where(k => k.Name == kultur.Name).Join(Context.Kultur_Name, k => k, kn => kn.Kultur, (k, kn) => kn)
                     .Join(Context.Name, kn => kn.Herkunft, n => n.Herkunft, (kn, n) => n)
                     .Where(n => n.Art == "Nachname" && (n.Geschlecht == geschlecht || n.Geschlecht == null)).ToList(); 
                // ersetzt in 30% der Fälle den Vornamen
                random = RandomNumberGenerator.Generator.Next(nachnamen.Count);
                Name nachname = nachnamen[random];
                if (RandomNumberGenerator.Generator.Next(1, 21) <= 6) name = nachname.Name1 + "|" + nachname.Bedeutung;
                else
                {
                    string nameBedeutung = name;
                    name += " " + nachname.Name1 + "|" + nameBedeutung +" "+nachname.Bedeutung;
                }
            }
            return name;
        }

        private string createMaraskanischenNamen(string geschlecht, Kultur kultur)
        {/* Maraskanische Namen stammen aus dem Garehti oder Tulamidya, annotiert mit Vorname
          * Nachname, wenn überhaupt vorhanden, mit Nachsilbe, oder "von" mit Herkunftsort 
          */

            //create Vorname
            List<Name> vornamen = Context.Name.Where(n => (n.Herkunft == "Garethische Namen" || n.Herkunft == "Tulamidische Namen" || n.Herkunft == "Maraskanische Namen") && n.Art == "Vorname" && (n.Geschlecht == geschlecht || n.Geschlecht == null)).ToList();
            Name vorname = vornamen[RandomNumberGenerator.Generator.Next(vornamen.Count)]; 
            string name;
            //falls nötig eine Endung
            if(vorname.Herkunft!="Maraskanische Namen"){
                List<Name> nachsilben =Context.Name.Where(n => n.Herkunft == "Maraskanische Namen" && n.Art == "Nachsilbe Vorname" && (n.Geschlecht == geschlecht || n.Geschlecht == null)).ToList();
                name = vorname.Name1 + nachsilben[RandomNumberGenerator.Generator.Next(nachsilben.Count)].Name1;
            }else name = vorname.Name1;

            //create Nachname in 35% der Fälle
            if (RandomNumberGenerator.Generator.Next(1, 21) <= 7)
            {
                //Familienname oder Ortsname
                if (RandomNumberGenerator.Generator.Next(1, 3) == 1)
                {
                    List<Name> nachnamen = Context.Name.Where(n => (n.Herkunft == "Garethische Namen" || n.Herkunft == "Tulamidische Namen") && n.Art == "Nachname" && (n.Geschlecht == geschlecht || n.Geschlecht == null)).ToList();
                    List<Name> nachsilben = Context.Name.Where(n => n.Herkunft == "Maraskanische Namen" && n.Art == "Nachsilbe Nachname" && (n.Geschlecht == geschlecht || n.Geschlecht == null)).ToList();

                    name += " " + nachnamen[RandomNumberGenerator.Generator.Next(nachnamen.Count)].Name1 + nachsilben[RandomNumberGenerator.Generator.Next(nachsilben.Count)].Name1;
                }
                else name += " von " + OrteMaraskan[RandomNumberGenerator.Generator.Next(OrteMaraskan.Count)];
            }
            return name;
        }

        private string createWaldmenschenName(string geschlecht, Kultur kultur)
        {/* Waldmenschen haben nur einen Namen, annotiert mit Vorname
          * Name ist nicht geschlechtsspezifisch           
          */
            string vorname;
            string vornameBedeutung;
            //create Vorname
            List<Name> vornamen = Liste<Kultur>().Where(k => k.Name == kultur.Name).Join(Context.Kultur_Name, k => k, kn => kn.Kultur, (k, kn) => kn)
                 .Join(Context.Name, kn => kn.Herkunft, n => n.Herkunft, (kn, n) => n)
                 .Where(n => n.Art == "Vorname" && (n.Geschlecht == geschlecht || n.Geschlecht == null)).ToList();
            int random = RandomNumberGenerator.Generator.Next(vornamen.Count);
            vorname = vornamen[random].Name1;
            vornameBedeutung = vornamen[random].Bedeutung;
            if (geschlecht == "w")
            {
                vorname += ", Tochter";
                vornameBedeutung += ", Tochter";
            }
            else
            {
                vorname += ", Sohn";
                vornameBedeutung += ", Sohn";
            }
            if (RandomNumberGenerator.Generator.Next(1, 3) == 1)
            {
                random = RandomNumberGenerator.Generator.Next(vornamen.Count);
                vorname += " der " + vornamen[random].Name1;
                vornameBedeutung += " der " + vornamen[random].Bedeutung;
                if (RandomNumberGenerator.Generator.Next(1, 3) == 1) vorname += " Ca"; else vorname += " Cawe";
            }
            else
            {
                random = RandomNumberGenerator.Generator.Next(vornamen.Count);
                vorname += " des " + vornamen[random].Name1;
                vornameBedeutung += " des " + vornamen[random].Bedeutung;
                if (RandomNumberGenerator.Generator.Next(1, 3) == 1) vorname += " Ha"; else vorname += " Hapa";
            }
            return vorname + "|" + vornameBedeutung;
        }

        private string createElfischenNamen(string geschlecht, Kultur kultur)
        {/*
          * 
          */
            //create Vorname
            List<string> vornamen = Liste<Kultur>().Where(k => k.Name == kultur.Name).Join(Context.Kultur_Name, k => k, kn => kn.Kultur, (k, kn) => kn)
                 .Join(Context.Name, kn => kn.Herkunft, n => n.Herkunft, (kn, n) => n)
                 .Where(n => n.Art == "Vorname" && (n.Geschlecht == geschlecht || n.Geschlecht == null)).Select(n => n.Name1).Distinct().ToList();
            string name = vornamen[RandomNumberGenerator.Generator.Next(vornamen.Count)];

            //Nachname
            List<string> nachnamen = Liste<Kultur>().Where(k => k.Name == kultur.Name).Join(Context.Kultur_Name, k => k, kn => kn.Kultur, (k, kn) => kn)
                 .Join(Context.Name, kn => kn.Herkunft, n => n.Herkunft, (kn, n) => n)
                 .Where(n => n.Art == "Nachname" && (n.Geschlecht == geschlecht || n.Geschlecht == null)).Select(n => n.Name1).Distinct().ToList();
            name += " "+nachnamen[RandomNumberGenerator.Generator.Next(nachnamen.Count)];
            //Abschluß
            List<string> nachnameAbschlüsse = Liste<Kultur>().Where(k => k.Name == kultur.Name).Join(Context.Kultur_Name, k => k, kn => kn.Kultur, (k, kn) => kn)
                 .Join(Context.Name, kn => kn.Herkunft, n => n.Herkunft, (kn, n) => n)
                 .Where(n => n.Art == "Nachsilbe Nachname" && (n.Geschlecht == geschlecht || n.Geschlecht == null)).Select(n => n.Name1).Distinct().ToList();
            name += nachnameAbschlüsse[RandomNumberGenerator.Generator.Next(nachnameAbschlüsse.Count)];
            return name;
        }

        private string createZwergischenNamen(string geschlecht, Kultur kultur)
        {/* Zwergische Namen haben eine Anfangssilbe gefolgt von 
          */

            //create Vorname
            List<string> vornamen = Liste<Kultur>().Where(k => k.Name == kultur.Name).Join(Context.Kultur_Name, k => k, kn => kn.Kultur, (k, kn) => kn)
                 .Join(Context.Name, kn => kn.Herkunft, n => n.Herkunft, (kn, n) => n)
                 .Where(n => n.Art == "Vorname" && (n.Geschlecht == geschlecht || n.Geschlecht == null)).Select(n => n.Name1).Distinct().ToList();
            string name = vornamen[RandomNumberGenerator.Generator.Next(vornamen.Count)];
            string nameZeuger = name;
            // füge 1-2 Silben dazu
            List<string> silben = Liste<Kultur>().Where(k => k.Name == kultur.Name).Join(Context.Kultur_Name, k => k, kn => kn.Kultur, (k, kn) => kn)
                 .Join(Context.Name, kn => kn.Herkunft, n => n.Herkunft, (kn, n) => n)
                 .Where(n => n.Art == "Nachsilbe Vorname" && (n.Geschlecht == geschlecht || n.Geschlecht == null)).Select(n => n.Name1).Distinct().ToList();
            for (int i = 0; i < RandomNumberGenerator.Generator.Next(1, 3);i++ )
            {
                name += silben[RandomNumberGenerator.Generator.Next(silben.Count)];
            }

            // Endung für weibliche Zwerge
            if (geschlecht == "w")
            {
                if (RandomNumberGenerator.Generator.Next(1, 3) == 1) if (name.EndsWith("a") == false) name += "a";              
                else if (name.EndsWith("e") == false) name += "e";
            }

            //create Nachname
            if (geschlecht == "w"){ 
                //selten den Namen des Gegengeschlechts aufgrund von Heldentaten des Elternteils
                if (RandomNumberGenerator.Generator.Next(1, 21) == 1) { name += " Tochter des"; nameZeuger = vornamen[RandomNumberGenerator.Generator.Next(vornamen.Count)]; }
                else name += " Tochter der";
            }else{
                //selten den Namen des Gegengeschlechts aufgrund von Heldentaten des Elternteils
                if (RandomNumberGenerator.Generator.Next(1, 21) == 1) { name += " Sohn der"; nameZeuger = vornamen[RandomNumberGenerator.Generator.Next(vornamen.Count)]; }
                else name += " Sohn des";
            }
            // füge 1-2 Silben dazu
            for (int i = 0; i < RandomNumberGenerator.Generator.Next(1, 3); i++)
            {
                nameZeuger += silben[RandomNumberGenerator.Generator.Next(silben.Count)];
            }

            return name + " " + nameZeuger;
        }

        private string createOrkischenNamen(string geschlecht, Kultur kultur)
        {/*Orks haben nur einen Namen;
          * TODO MP: Orks aus dem Svellttal haben typische mittelländische Familiennamen, wie Mauerbrech oder Rittertot
          */
            List<string> vornamen = Liste<Kultur>().Where(k => k.Name == kultur.Name).Join(Context.Kultur_Name, k => k, kn => kn.Kultur, (k, kn) => kn)
                 .Join(Context.Name, kn => kn.Herkunft, n => n.Herkunft, (kn, n) => n)
                 .Where(n => n.Art == "Vorname" && (n.Geschlecht == geschlecht || n.Geschlecht == null)).Select(n => n.Name1).Distinct().ToList();
            return vornamen[RandomNumberGenerator.Generator.Next(vornamen.Count)];
        }

        private string createGoblinischenNamen(string geschlecht, Kultur kultur)
        {
            /* Goblins haben nur einen Namen; 
             * TODO MP: Zweitname(Ehrenname) für Goblin (sehr selten);
             * TODO MP: Goblinbanden haben Mischformen
             * TODO MP: Festumer Goblins haben bornländische Namen
             */
            List<string> vornamen = Liste<Kultur>().Where(k => k.Name == kultur.Name).Join(Context.Kultur_Name, k => k, kn => kn.Kultur, (k, kn) => kn)
                .Join(Context.Name, kn => kn.Herkunft, n => n.Herkunft, (kn, n) => n)
                .Where(n => n.Art == "Vorname" && (n.Geschlecht == geschlecht || n.Geschlecht == null)).Select(n => n.Name1).Distinct().ToList();
            return vornamen[RandomNumberGenerator.Generator.Next(vornamen.Count)];
        }

        private string createGrolmischenNamen(string geschlecht, Kultur kultur)
        {/* Grolme haben nur einen Namen, annotiert als Vorname
          */
            List<string> vornamen = Liste<Kultur>().Where(k => k.Name == kultur.Name).Join(Context.Kultur_Name, k => k, kn => kn.Kultur, (k, kn) => kn)
                .Join(Context.Name, kn => kn.Herkunft, n => n.Herkunft, (kn, n) => n)
                .Where(n => n.Art == "Vorname" && (n.Geschlecht == geschlecht || n.Geschlecht == null)).Select(n => n.Name1).Distinct().ToList();
            return vornamen[RandomNumberGenerator.Generator.Next(vornamen.Count)];
        }

        private string createAchazName(string geschlecht, Kultur kultur)
        {
            /* Achaz haben nur einen Namen, annotiert mit Vorname
             * Name ist nicht geschlechtsspezifisch
             */
            List<Name> vornamen = Liste<Kultur>().Where(k => k.Name == kultur.Name).Join(Context.Kultur_Name, k => k, kn => kn.Kultur, (k, kn) => kn)
                .Join(Context.Name, kn => kn.Herkunft, n => n.Herkunft, (kn, n) => n)
                .Where(n => n.Art == "Vorname" && (n.Geschlecht == geschlecht || n.Geschlecht == null)).ToList();    
            //falls vorhanden mit Bedeutung
            int random = RandomNumberGenerator.Generator.Next(vornamen.Count);
            if (vornamen[random].Bedeutung != null){ 
                return vornamen[random].Name1 + "|" + vornamen[random].Bedeutung;
            }
            else return vornamen[random].Name1;
        }

        #endregion


 
    }
}

