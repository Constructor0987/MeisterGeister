using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeisterGeister.Model.Service;
using MeisterGeister.Model;
// Eigene Usings
using MeisterGeister.Logic.General;

namespace MeisterGeister
{
    public class NscGenerator
    {
        public static List<string> Regionen
        {
            get
            {
                List<string> regionen = new List<string>();
               // regionen = new NscService().getRassenNamen();
                regionen.AddRange(new string[] {
                    "Garethische Namen",
                   "Albernische Namen",
                   "Almadanische Namen",
                   "Andergastsche Namen",
                   "Bornländische Namen",
                   "Nostrische Namen",
                   "Weidener Namen",
                   "Horasische Namen",
                   "Zyklopäische Namen",
                   "Südländische Namen",
                   "Thorwalsche Namen",
                   "Gjalskerländische Namen",
                   "Fjarningsche Namen",
                   "Nivesische Namen",
                   "Norbardische Namen",
                   "Tulamidische Namen",
                   "Novadische Namen",
                   "Aranische Namen",
                   "Ferkina Namen",
                   "Trollzacker Namen",
                   "Zahori Namen",
                   "Maraskanische Namen",
                   "Waldmenschen Namen",
                   "Utulu Namen",
                   "Tocamuyac Namen",
                   "Elfische Namen",
                   "Zwergische Namen",
                   "Hügelzwergische Namen",
                   "Orkische Namen",
                   "Goblinische Namen",
                   "Achaz Namen",
                   "Trollische Namen",
                   "Grolmische Namen",
                });
                regionen.Sort();
                return regionen;
            }
        }

        public static Daten.DatabaseDSADataSet.NameDataTable NameTable
        {
            get
            {
                if (App.DatenDataSet != null && App.DatenDataSet.Name != null & App.DatenDataSet.Name.Count == 0)
                {
                    App.DatenDataSetTableAdapters.NameTableAdapter.Fill(App.DatenDataSet.Name);
                }
                return App.DatenDataSet.Name;
            }
        }

        public static Name GenName(string herkunft, string geschlecht)
        {
            //TODO ??: adaptiere rasse an namensliste
            Name n = new Name();
            n.Geschlecht = geschlecht;
            switch (herkunft)
            {
                case "Mittelländer": herkunft = "Garethische Namen"; break;
                case "Norbarde": herkunft = "Norbardische Namen"; break;
                case "Thorwaler": herkunft = "Thorwalsche Namen"; break;
                case "Tulamide": herkunft = "Tulamidische Namen"; break;
                case "Halbelf": herkunft = "Elfische Namen"; break;
                case "Nivese": herkunft = "Nivesische Namen"; break;
                case "Trollzacker": herkunft = "Trollzacker Namen"; break;
                case "Waldmensch": herkunft = "Waldmenschen Namen"; break;
                case "Utulu": herkunft = "Utulu Namen"; break;
                case "Zwerg": herkunft = "Zwergische Namen"; break;
                case "Ork": herkunft = "Orkische Namen"; break;
                case "Halbork": herkunft = "Orkische Namen"; break;
                case "Goblin": herkunft = "Goblinische Namen"; break;
                case "Achaz": herkunft = "Achaz Namen"; break;
                case "Elf": herkunft = "Elfische Namen"; break;
                default: break;//throw new NotImplementedException();
            };
            n.Herkunft = herkunft;

            string g = string.Empty;

            if (geschlecht != "m/w")
                g = string.Format(" AND IsNull(Geschlecht, '{0}') = '{0}'", geschlecht);

            GenNameVorname(herkunft, g, ref n);
            GenNameVornameNachsilbe(herkunft, g, ref n);
            GenNameVornameVorsilbe(herkunft, g, ref n);

            GenNameNachname(herkunft, g, ref n);
            GenNameNachnameNachsilbe(herkunft, g, ref n);

            return n;
        }

        private static void GenNameVornameVorsilbe(string herkunft, string g, ref Name n)
        {
            System.Data.DataRow[] rowsM = NameTable.Select(string.Format("Art = 'Vorsilbe Vorname' AND Herkunft = '{0}' AND IsNull(Geschlecht, 'm') = 'm'", herkunft));
            System.Data.DataRow[] rowsW = NameTable.Select(string.Format("Art = 'Vorsilbe Vorname' AND Herkunft = '{0}' AND IsNull(Geschlecht, 'w') = 'w'", herkunft));
            System.Data.DataRow[] rows = null;

            if (rowsM.Length > 0 || rowsW.Length > 0)
            {
                if (n.KeineVorsilbe || n.Nachsilbe)
                    return;
                W20 w20 = new W20();
                if (w20.Würfeln() <= 17) // Vorsilbe nur in 15% aller Fälle
                    return;

                if (n.Geschlecht == "w")
                    rows = rowsW;
                else
                    rows = rowsM;

                if (rows.Length > 0)
                {
                    uint ergebnis = ZufallszahlAusRows(rows);
                    if (ergebnis <= rows.Length)
                    {
                        System.Data.DataRow row = rows[ergebnis - 1];
                        if (!row.IsNull("Name"))
                        {
                            n.KeineVorsilbe = true;
                            string vor = n.VorName;
                            if (vor.Length >= 1)
                                vor = Char.ToLower(vor[0]) + vor.Remove(0, 1);
                            n.VorName = row["Name"].ToString() + vor;
                        }
                    }
                }
            }
        }

        private static void GenNameVornameNachsilbe(string herkunft, string g, ref Name n)
        {
            if (herkunft == "Hügelzwergische Namen")
                herkunft = "Zwergische Namen";
            System.Data.DataRow[] rowsM = NameTable.Select(string.Format("Art = 'Nachsilbe Vorname' AND Herkunft = '{0}' AND IsNull(Geschlecht, 'm') = 'm'", herkunft));
            System.Data.DataRow[] rowsW = NameTable.Select(string.Format("Art = 'Nachsilbe Vorname' AND Herkunft = '{0}' AND IsNull(Geschlecht, 'w') = 'w'", herkunft));
            System.Data.DataRow[] rows = null;

            if (rowsM.Length > 0 || rowsW.Length > 0)
            {
                if ((herkunft != "Maraskanische Namen" || (herkunft == "Maraskanische Namen" && n.KeineNachsilbeVorname))
                    && herkunft != "Zwergische Namen") // 'Maraskan' un 'Zwerge' immer eine Nachsilbe
                {
                    if (n.KeineNachsilbeVorname || n.Vorsilbe)
                        return;
                    W20 w20 = new W20();
                    if (w20.Würfeln() <= 17) // Nachsilbe nur in 15% aller Fälle
                        return;
                }

                if (n.Geschlecht == "w")
                    rows = rowsW;
                else
                    rows = rowsM;

                if (rows.Length > 0)
                {
                    Würfel w = new Würfel(2);
                    int j = 1;
                    if (herkunft == "Zwergische Namen")
                        j = (int)w.Würfeln();
                    for (int i = 0; i < j; i++)
                    {
                        uint ergebnis = ZufallszahlAusRows(rows);
                        if (ergebnis <= rows.Length)
                        {
                            System.Data.DataRow row = rows[ergebnis - 1];
                            if (!row.IsNull("Name"))
                            {
                                n.KeineNachsilbeVorname = true;
                                n.VorName += row["Name"].ToString();
                            }
                        }
                    }

                    // Endung für weibliche Zwerge
                    if (herkunft == "Zwergische Namen" && n.Geschlecht == "w")
                    {
                        if (w.Würfeln() == 1)
                        {
                            if (n.VorName.EndsWith("a") == false)
                                n.VorName += "a";
                        }
                        else
                        {
                            if (n.VorName.EndsWith("e") == false)
                                n.VorName += "e";
                        }
                    }
                }
            }
        }

        private static void GenNameVorname(string herkunft, string g, ref Name n)
        {
            if (herkunft == "Hügelzwergische Namen")
                herkunft = "Zwergische Namen";

            System.Data.DataRow[] rows = null;

            if (herkunft == "Maraskanische Namen")
                rows = NameTable.Select(string.Format("Art = 'Vorname' AND (Herkunft = 'Garethische Namen' OR Herkunft = 'Tulamidische Namen' OR Herkunft = 'Maraskanische Namen'){0}", g));
            else if (herkunft == "Albernische Namen")
                rows = NameTable.Select(string.Format("Art = 'Vorname' AND ((Herkunft = 'Garethische Namen' AND (Name LIKE '%ian' OR Name LIKE '%dan')) OR Herkunft = '{0}'){1}", herkunft, g));
            else if (herkunft == "Novadische Namen")
                rows = NameTable.Select(string.Format("Art = 'Vorname' AND Herkunft = '{0}'{1}", "Tulamidische Namen", g));
            else
                rows = NameTable.Select(string.Format("Art = 'Vorname' AND Herkunft = '{0}'{1}", herkunft, g));

            if (rows.Length > 0)
            {
                uint ergebnis = ZufallszahlAusRows(rows);
                if (ergebnis <= rows.Length)
                {
                    System.Data.DataRow row = rows[ergebnis - 1];
                    if (!row.IsNull("Name"))
                    {
                        n.VorName = row["Name"].ToString();
                        if (herkunft == "Albernische Namen" && row["Herkunft"].ToString() == "Garethische Namen")
                        {
                            W6 w6 = new W6();
                            if (n.VorName.EndsWith("ian"))
                            {
                                if (w6.Würfeln() <= 3)
                                    n.VorName = n.VorName.Remove(n.VorName.LastIndexOf("ian")) + "wyn";
                                else
                                    n.VorName = n.VorName.Remove(n.VorName.LastIndexOf("ian")) + "uin";
                            }
                            else if (n.VorName.EndsWith("dan"))
                            {
                                if (w6.Würfeln() <= 3)
                                    n.VorName = n.VorName.Remove(n.VorName.LastIndexOf("dan")) + "tin";
                                else
                                    n.VorName = n.VorName.Remove(n.VorName.LastIndexOf("dan")) + "den";
                            }
                        }
                    }

                    Würfel w = new Würfel(2);
                    if (n.Geschlecht == null || n.Geschlecht == string.Empty || n.Geschlecht == "m/w")
                    {
                        if (!row.IsNull("Geschlecht") && row["Geschlecht"].ToString() != string.Empty)
                            n.Geschlecht = row["Geschlecht"].ToString();
                        // Geschlecht bestimmen, falls  unbestimmt
                        if (n.Geschlecht == "m/w" || n.Geschlecht == null || n.Geschlecht == string.Empty)
                            if (w.Würfeln() == 1)
                                n.Geschlecht = "m";
                            else
                                n.Geschlecht = "w";
                    }

                    if (herkunft == "Waldmenschen Namen" || herkunft == "Utulu Namen" || herkunft == "Tocamuyac Namen")
                        if (n.Geschlecht == "m")
                            if (w.Würfeln() == 1)
                                n.VorName += " Hapa";
                            else
                                n.VorName += " Ha";
                        else
                            if (w.Würfeln() == 1)
                                n.VorName += " Cawe";
                            else
                                n.VorName += " Ca";

                    if (!row.IsNull("KeineVorsilbe"))
                        n.KeineVorsilbe = (bool)row["KeineVorsilbe"];
                    if (!row.IsNull("KeineNachsilbe"))
                        n.KeineNachsilbeVorname = (bool)row["KeineNachsilbe"];
                }
            }
        }

        private static void GenNameNachnameNachsilbe(string herkunft, string g, ref Name n)
        {
            System.Data.DataRow[] rows = NameTable.Select(string.Format("Art = 'Nachsilbe Nachname' AND Herkunft = '{0}'", herkunft));
            if (rows.Length == 0)
                return;

            if ((herkunft != "Maraskanische Namen" || n.NachName.StartsWith("von ")) && herkunft != "Elfische Namen") // 'Maraskan' und 'Elfisch' immer eine Nachsilbe
            {
                if (n.KeineNachsilbeNachname)
                    return;
                W20 w20 = new W20();
                if (w20.Würfeln() <= 17) // Nachsilbe nur in 15% aller Fälle
                    return;
            }

            uint ergebnis = ZufallszahlAusRows(rows);
            if (ergebnis <= rows.Length)
            {
                System.Data.DataRow row = rows[ergebnis - 1];
                if (!row.IsNull("Name"))
                    n.NachName += row["Name"].ToString();
            }
        }

        private static void GenNameNachname(string herkunft, string g, ref Name n)
        {
            System.Data.DataRow[] rows = null;

            W20 w20 = new W20();
            if (herkunft == "Zwergische Namen")
            {
                Name nElter = new Name();
                nElter.Geschlecht = n.Geschlecht;
                GenNameVorname(herkunft, g, ref nElter);
                GenNameVornameNachsilbe(herkunft, g, ref nElter);
                if (n.Geschlecht == "m")
                    n.NachName = "Sohn des " + nElter.VorName;
                else
                    n.NachName = "Tochter der " + nElter.VorName;
                return;
            }
            else if (herkunft == "Maraskanische Namen")
            {
                if (w20.Würfeln() <= 13) // Nachname nur in 35% aller Fälle
                {
                    n.KeineNachsilbeNachname = true;
                    n.NachName = "von ";
                    Würfel wOrt = new Würfel(Convert.ToUInt32(OrteMaraskan.Count));
                    n.NachName += OrteMaraskan[(int)(wOrt.Würfeln() - 1)];
                    //if (w20.Würfeln() <= 10) // in 50% der Fälle zusätzliche Beschreibung
                    //{
                    //    rows = NameTable.Select(string.Format("Art = 'Beschreibung' AND Herkunft = '{0}'{1}", herkunft, g));
                    //    uint ergebnis = ZufallszahlAusRows(rows);
                    //    if (ergebnis <= rows.Length)
                    //    {
                    //        System.Data.DataRow row = rows[ergebnis - 1];
                    //        if (!row.IsNull("Name"))
                    //        {
                    //            W6 w6 = new W6();
                    //            if (n.Geschlecht == "m")
                    //                n.NachName += ", " + row["Name"].ToString().Replace("DIEDER", "der").Replace("ZAHL", w6.Würfeln(1, 1).Summe.ToString())
                    //                    .Replace("IN", string.Empty);
                    //            else
                    //                n.NachName += ", " + row["Name"].ToString().Replace("DIEDER", "die").Replace("ZAHL", w6.Würfeln(1, 1).Summe.ToString())
                    //                    .Replace("IN", "in");
                    //        }
                    //    }
                    //}
                    return;
                }
                rows = NameTable.Select(string.Format("Art = 'Nachname' AND (Herkunft = 'Garethische Namen' OR Herkunft = 'Tulamidische Namen')"));
            }
            else if (herkunft == "Weidener Namen")
                rows = NameTable.Select(string.Format("Art = 'Nachname' AND (Herkunft = 'Garethische Namen' OR Herkunft = 'Weidener Namen')"));
            else if (herkunft == "Thorwalsche Namen")
                rows = NameTable.Select(string.Format("Art = 'Vorname' AND Herkunft = '{0}'", herkunft));
            else if (herkunft == "Gjalskerländische Namen")
                if (n.Geschlecht == "m")
                    rows = NameTable.Select("Art = 'Vorname' AND Herkunft = 'Gjalskerländische Namen' AND Geschlecht = 'm'");
                else
                    rows = NameTable.Select("Art = 'Vorname' AND Herkunft = 'Gjalskerländische Namen' AND Geschlecht = 'w'");
            else if (herkunft == "Tulamidische Namen")
                rows = NameTable.Select(string.Format("Art = 'Vorname' AND Geschlecht = 'm' AND Herkunft = '{0}'", herkunft));
            else if (herkunft == "Novadische Namen")
                rows = NameTable.Select("Art = 'Vorname' AND Geschlecht = 'm' AND Herkunft = 'Tulamidische Namen'");
            else if (herkunft == "Aranische Namen")
                rows = NameTable.Select(string.Format("Art = 'Vorname' AND Herkunft = 'Aranische Namen' AND Geschlecht = '{0}'", n.Geschlecht));
            else if (herkunft == "Ferkina Namen")
                rows = NameTable.Select("Art = 'Vorname' AND Geschlecht = 'm' AND Herkunft = 'Ferkina Namen'");
            else
                rows = NameTable.Select(string.Format("Art = 'Nachname' AND Herkunft = '{0}'{1}", herkunft, g));

            if (rows.Length > 0)
            {
                uint ergebnis = ZufallszahlAusRows(rows);
                if (ergebnis <= rows.Length)
                {
                    System.Data.DataRow row = rows[ergebnis - 1];
                    if (!row.IsNull("Name"))
                        n.NachName = row["Name"].ToString();
                    if (!row.IsNull("KeineNachsilbe"))
                        n.KeineNachsilbeNachname = (bool)row["KeineNachsilbe"];
                    if (herkunft == "Andergastsche Namen" && n.Geschlecht == "w")
                        n.NachName += "in";
                    else if (herkunft == "Thorwalsche Namen" && n.Geschlecht == "m")
                        n.NachName += "son";
                    else if (herkunft == "Thorwalsche Namen" && n.Geschlecht == "w")
                        n.NachName += "dottir";
                    else if (herkunft == "Gjalskerländische Namen" && n.Geschlecht == "m")
                        n.NachName += "bren";
                    else if (herkunft == "Gjalskerländische Namen" && n.Geschlecht == "w")
                        n.NachName += "brai";
                    else if (herkunft == "Novadische Namen" && n.Geschlecht == "m")
                        n.NachName = "ben " + n.NachName;
                    else if (herkunft == "Ferkina Namen" && n.Geschlecht == "m")
                        n.NachName = "iban " + n.NachName;
                    else if (herkunft == "Ferkina Namen" && n.Geschlecht == "w")
                        n.NachName = "sabu " + n.NachName;
                    else if ((herkunft == "Tulamidische Namen" || herkunft == "Aranische Namen") && n.Geschlecht == "m")
                        n.NachName = "ibn " + n.NachName;
                    else if ((herkunft == "Tulamidische Namen" || herkunft == "Novadische Namen" || herkunft == "Aranische Namen") && n.Geschlecht == "w")
                    {
                        w20.Würfeln();
                        if (w20.Ergebnis <= 5)
                            n.NachName = "saba " + n.NachName;
                        else if (w20.Ergebnis <= 10)
                            n.NachName += "suni";
                        else if (w20.Ergebnis <= 15)
                            n.NachName += "sunni";
                        else
                            n.NachName += "sunya";
                    }
                }
            }
        }

        private static uint ZufallszahlAusRows(System.Data.DataRow[] rows)
        {
            uint count = Convert.ToUInt32(rows.Length);
            Würfel w = new Würfel(count);
            uint ergebnis = w.Würfeln();
            return ergebnis;
        }

        public static List<Name> GenNamen(string herkunft, string geschlecht, int anzahl)
        {
            //List<Person> namen = new List<Person>();
            List<Name> namen = new List<Name>();
            for (int i = 0; i < anzahl; i++)
            {
                
                //Person person = new Person(herkunft);
                //namen.Add(new Person(herkunft));
                namen.Add(GenName(herkunft, geschlecht));
            }
            return namen;
        }

        private static List<string> OrteMaraskan = new List<string>() { 
            "Achazak", "Alrurdan", "As'Far", "As'Khunchak", "Boran", "Buli", "Cavazoab", "Dinoda", "Geran", 
            "Gipflak", "Guladasbîd", "Hemandu", "Huab", "Jergan", "Mazazaoab", "Mherweggyn", "Nuran", "Senan", 
            "Sindibab", "Sinoda", "Syneggyn", "Tarschoggyn", "Tuzak", "Tzab", "Usdaran", "Uuz'Dornak", "Vezarak", 
            "Yerkilan", "Zinobab" };
    }

    public class Name
    {
        public string VorName = string.Empty;
        public string NachName = string.Empty;
        public string Geschlecht = string.Empty;

        public bool KeineNachsilbeVorname = false;
        public bool KeineNachsilbeNachname = false;
        public bool KeineVorsilbe = false;

        public bool Nachsilbe = false;
        public bool Vorsilbe = false;
        public string Herkunft = string.Empty;

        public override string ToString()
        {
            string name = VorName;
            if (NachName != string.Empty)
                name += " " + NachName;
            if (Geschlecht != null && Geschlecht != string.Empty)
                name += " (" + Geschlecht + ")";
            return name;
        }
    }

    public class Persoenlichkeitsdimension
    {
        public int Offenheit;
        public int Gewissenhaftigkeit;
        public int Extraversion;
        public int Vertraeglichkeit;
        public int Neurotizismus;
        public int Verhalten;

        public Persoenlichkeitsdimension(int Offenheit, int Gewissenahftigkeit, int Extraversion, int Vertraeglichkeit, int Neurotizismus, int Verhalten)
        {
            this.Offenheit = Offenheit;
            this.Gewissenhaftigkeit = Gewissenahftigkeit;
            this.Extraversion = Extraversion;
            this.Vertraeglichkeit = Vertraeglichkeit;
            this.Neurotizismus = Neurotizismus;
            this.Verhalten = Verhalten;
        }

    }

    public class Aussehen
    {
        private NscService nscService = new NscService();
        public string Koerpergroesse;
        public string Koerperbau;
        public string Haarfarbe;
        public string Augenfarbe;
        public string Haartracht;
        public string Bart;
        public List<string> gesichtsmerkmale;
        private W20 w20 = new W20();
        public Aussehen()
        {
            int tmp = (int)this.w20.Würfeln();

        }
        public override string ToString()
        {
            string tmp = "Aussehen: ";
            foreach (string str in this.gesichtsmerkmale)
            {
                tmp += str + ", ";
            }
            return tmp;
        }
    }

    public class Stand
    {
        public string Standbezeichnung;
        public int sozialstatus;

        public Stand(string Stand)
        {
            this.Standbezeichnung = Stand;
        }
    }


    public class Darstellung
    {
        private NscService nscService = new NscService();
        private string haltung;
        private string geste;
        private string sprache;

        public Darstellung()
        {
            this.haltung = nscService.getRandomHaltung();
            this.geste = nscService.getRandomGeste();
            this.sprache = nscService.getRandomSprache();
            nscService.getRassenNameDistinct();
        }
        public override string ToString()
        {
            return "Darstellung: " + this.haltung + ", " + this.geste + ", " + this.sprache;
        }
    }


    public class Person
    {

        //wichtige Eigenschaften
        private Name name;
        private string geschlecht;
        private Rasse rasse = new Rasse();//diarium
        //TODO ??: Nie verwendet: private string rassenVariante;
        //TODO ??: Nie verwendet: private string kultur;//diarium
        //TODO ??: Nie verwendet: private string profession;//diarium  //1001NSC
        private string altersklasse; // wege des meisters S173ff //1001NSC //evtl. Mod draus machen, bzw. altersgrenzen.. in % für rassenanpassung
        private int groesse; // in Zentimeter //1001NSC //diarium // wege des meisters S173ff
        private int gewicht;// in Stein // wege des meisters S173ff //diarium
        private int alter;//1001NSC //diarium
        //TODO ??: Nie verwendet: private string augenfarbe;//1001NSC //diarium 
        //TODO ??: Nie verwendet: private string haarfarbe;//1001NSC //diarium 

        private Darstellung darstellung = new Darstellung();//diarium//1001NSC
        private Aussehen aussehen = new Aussehen();

        //1001NSC //diarium 
        //1001NSC // wege des meisters
        //diarium // wege des meisters
        //1001NSC //diarium // wege des meisters

        // wege des meisters S173ff
        //TODO ??: Nie verwendet: private string[] besEigenschaften;
        //TODO ??: Nie verwendet: private string[] besAussehen;
        //TODO ??: Nie verwendet: private string[] behinderungen;
        //TODO ??: Nie verwendet: private string[] schlAngewohnheiten;
        //TODO ??: Nie verwendet: private string[] launenUeigenschaften;
        //TODO ??: Nie verwendet: private string groessenMod;
        //TODO ??: Nie verwendet: private string gewichtMod;

        //TODO ??: Nie verwendet: private string[] sonstigeBesonderheiten;

        //diarium
        //TODO ??: Nie verwendet: private string[] auessereErscheinungen;
        //TODO ??: Nie verwendet: private string[] charaktereigenschaften;
        //TODO ??: Nie verwendet: private string aktVerfassung;
        //TODO ??: Nie verwendet: private string gespraechsthema;
        //1001NSC
        //TODO ??: Nie verwendet: private string[] koerperbau;//wie auessereErscheinungen
        //TODO ??: Nie verwendet: private string haartracht;
        //TODO ??: Nie verwendet: private string bart;
        //TODO ??: Nie verwendet: private string gesichtsmerkmale;
        //TODO ??: Nie verwendet: private string stand;
        //TODO ??: Nie verwendet: private string kompetenz;
        //TODO ??: Nie verwendet: private string besonderheiten;
        //TODO ??: Nie verwendet: private Persoenlichkeitsdimension persoenlichkeitsdimension;
        //TODO ??: Nie verwendet: private bool magiebegabt;
        //TODO ??: Nie verwendet: private string[] spezialgebiete;
        //TODO ??: Nie verwendet: private string schwesternschaft;
        // handwerker können weiter aufgeteilt werden S.64
        //Magiebegabte bekommen spezialgebiet(e) S76
        // Hexen bekommen eine Schwesternschaft (&Vertrauten) S80
        // evtl. aufgliederung nach Kathegorien
        private W20 w20 = new W20();
        private W6 w6 = new W6();
        private static readonly Random ZahlenGenerator = new Random();

        public Person(string rasse)
        {

            genGeschlecht();
            genName();
            genAltersklasse();
            genAlter();
            genGroesseUndGewicht();
            //Haarfarbe nach Alter (&Rasse) grau meliert...
        }

        private void genName()
        {
            this.name = NscGenerator.GenName(this.rasse.Name, this.geschlecht);
        }

        private void genGroesseUndGewicht()
        {
            this.groesse = (int)this.rasse.Größe;
            foreach (string wuerfel in this.rasse.GrößeMod.Split('+').ToList())
            {
                //TODO ??: remove as soon as DB is clean
                int count;
                if (wuerfel.Split('W').ToList()[0] == "") count = 1;
                else count = Convert.ToInt16(wuerfel.Split('W').ToList()[0]);
                //----
                for (int i = 0; i<count; i++)
                {
                    if (Convert.ToInt16(wuerfel.Split('W').ToList()[1]) == 3) this.groesse += (int)(new W3().Würfeln());
                    else if (Convert.ToInt16(wuerfel.Split('W').ToList()[1]) == 6) this.groesse += (int)(new W6().Würfeln());
                    else if (Convert.ToInt16(wuerfel.Split('W').ToList()[1]) == 10) this.groesse += (int)(new W10().Würfeln());
                    else if (Convert.ToInt16(wuerfel.Split('W').ToList()[1]) == 20) this.groesse += (int)(new W20().Würfeln());
                    else throw new NotImplementedException();
                };
            };
           /* switch (this.rasse)
            {
                case "Mittelländer": this.groesse = 160 + (int)this.w20.Würfeln() + (int)this.w20.Würfeln();
                    this.gewicht = this.groesse - 100; break;
                case "Norbarde": this.groesse = 158 + (int)this.w20.Würfeln() + (int)this.w20.Würfeln();
                    this.gewicht = this.groesse - 100; break;
                case "Thorwaler": this.groesse = 168 + (int)this.w20.Würfeln() + (int)this.w20.Würfeln();
                    this.gewicht = this.groesse - 95; break;
                case "Tulamide": this.groesse = 155 + (int)this.w20.Würfeln() + (int)this.w20.Würfeln();
                    this.gewicht = this.groesse - 105; break;
                case "Halbelf": this.groesse = 158 + (int)this.w20.Würfeln() + (int)this.w6.Würfeln() + (int)this.w6.Würfeln() + (int)this.w6.Würfeln() + (int)this.w6.Würfeln();
                    this.gewicht = this.groesse - 120; break;
                case "Nivese": this.groesse = 155 + (int)this.w20.Würfeln() + (int)this.w20.Würfeln();
                    this.gewicht = this.groesse - 110; break;
                case "Trollzacker": this.groesse = 195 + (int)this.w20.Würfeln();
                    this.gewicht = this.groesse - 100; break;
                case "Waldmensch": this.groesse = 152 + (int)this.w6.Würfeln() + (int)this.w6.Würfeln() + (int)this.w6.Würfeln();
                    this.gewicht = this.groesse - 110; break;
                case "Utulu": this.groesse = 165 + (int)this.w20.Würfeln() + (int)this.w20.Würfeln();
                    this.gewicht = this.groesse - 110; break;
                case "Zwerg": this.groesse = 128 + (int)this.w6.Würfeln() + (int)this.w6.Würfeln();
                    this.gewicht = this.groesse - 80; break;
                case "Ork":
                case "Halbork": this.groesse = 160 + (int)this.w6.Würfeln() + (int)this.w6.Würfeln() + (int)this.w6.Würfeln() + (int)this.w6.Würfeln();
                    this.gewicht = this.groesse - 100; break;
                case "Goblin": this.groesse = 135 + (int)this.w6.Würfeln() + (int)this.w6.Würfeln() + (int)this.w6.Würfeln() + (int)this.w6.Würfeln();
                    this.gewicht = this.groesse - 100; break;
                case "Achaz":
                case "Elf": break;
                default: throw new NotImplementedException();
            }*/
            if (this.altersklasse == "Kind")
            {
                this.groesse = 50 + (this.groesse - 50) / 12 * this.alter;
                this.gewicht = 4 + (this.groesse - 4) / 22 * this.alter;
            }
        }

        private void genAlter()
        {
            //TODO ??: wahrscheinlichkeit für alter (weniger senioren, etc.)
            switch (this.rasse.Name)
            {
                case "Mittelländer":
                case "Norbarde":
                case "Thorwaler":
                case "Tulamide":
                case "Halbelf": switch (this.altersklasse)
                    {
                        case "Kind": this.gen(1, 11); break;
                        case "Jugendlich": this.gen(12, 16); break;
                        case "Erwachsen": this.gen(17, 50); break;
                        case "Senior": this.gen(51, 85); break;
                        default: throw new NotImplementedException();
                    } break;
                case "Nivese":
                case "Trollzacker":
                case "Waldmensch":
                case "Utulu": switch (this.altersklasse)
                    {
                        case "Kind": this.gen(1, 10); break;
                        case "Jugendlich": this.gen(11, 15); break;
                        case "Erwachsen": this.gen(16, 50); break;
                        case "Senior": this.gen(51, 95); break;
                        default: throw new NotImplementedException();
                    } break;
                case "Zwerg": switch (this.altersklasse)
                    {
                        case "Kind": this.gen(1, 20); break;
                        case "Jugendlich": this.gen(21, 35); break;
                        case "Erwachsen": this.gen(35, 300); break;
                        case "Senior": this.gen(301, 500); break;
                        default: throw new NotImplementedException();
                    } break;
                case "Ork": switch (this.altersklasse)
                    {
                        case "Kind": this.gen(1, 10); break;
                        case "Jugendlich": this.gen(11, 16); break;
                        case "Erwachsen": this.gen(17, 35); break;
                        case "Senior": this.gen(36, 45); break;
                        default: throw new NotImplementedException();
                    } break;
                case "Halbork": switch (this.altersklasse)
                    {
                        case "Kind": this.gen(1, 10); break;
                        case "Jugendlich": this.gen(11, 16); break;
                        case "Erwachsen": this.gen(17, 45); break;
                        case "Senior": this.gen(46, 60); break;
                        default: throw new NotImplementedException();
                    } break;
                case "Goblin": switch (this.altersklasse)
                    {
                        case "Kind": this.gen(1, 8); break;
                        case "Jugendlich": this.gen(9, 12); break;
                        case "Erwachsen": this.gen(13, 28); break;
                        case "Senior": this.gen(29, 40); break;
                        default: throw new NotImplementedException();
                    } break;
                case "Achaz": switch (this.altersklasse)
                    {
                        case "Kind": this.gen(1, 14); break;
                        case "Jugendlich": this.gen(15, 20); break;
                        case "Erwachsen": this.gen(21, 120); break;
                        case "Senior": this.gen(121, 180); break;
                        default: throw new NotImplementedException();
                    } break;
                case "Elf": switch (this.altersklasse)
                    {
                        case "Kind": this.gen(1, 18); break;
                        case "Jugendlich": this.gen(19, 35); break;
                        case "Erwachsen": this.gen(36, 600); break;
                        case "Senior": this.gen(601, 1000); break;
                        default: throw new NotImplementedException();
                    } break;
                default: throw new NotImplementedException();

            }

        }
        private void gen(int min, int max)
        {
            this.alter = ZahlenGenerator.Next(max - min) + min;
        }
        private void genAltersklasse()
        {
            if (this.altersklasse == null)
            {
                uint tmp = this.w20.Würfeln();
                if (tmp <= 2) this.altersklasse = "Kind";
                else if (tmp <= 6) this.altersklasse = "Jugendlich";
                else if (tmp <= 18) this.altersklasse = "Erwachsen";
                else if (tmp <= 20) this.altersklasse = "Senior";
                else this.altersklasse = "Erwachsen";
            }
        }
        private void genGeschlecht()
        {
            if (this.geschlecht == null)
            {
                if (w6.Würfeln() <= 3) this.geschlecht = "w";
                else this.geschlecht = "m";
            }
        }

        public override string ToString()
        {
            string name = this.name.ToString() +
                "\nRasse: " + this.rasse.Name +" ("+this.rasse.Variante+")"+ " Kultur: Kultur" + " Alter: " + this.alter + " (" + this.altersklasse + ")"
                + "\nProfession: Profession (Kompetenz X)"
                + "\nGröße: " + (double)this.groesse / 100 + " (" + this.groesse / 2 + " Finger) Gewicht: " + this.gewicht + " Stein"
                + "\n" + this.aussehen + ", Haartracht,(Bart), Besonderheiten"
                + "\nCharakter: Charaktereigenschaften"
                + "\nSoziales: Freunde, Feinde, Gesprächsthema"
                + "\n" + this.darstellung.ToString();
            name = name + "\n----------------------------------------";

            return name;
        }
    }

}
