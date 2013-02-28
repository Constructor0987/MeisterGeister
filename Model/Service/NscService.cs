using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
//Eigene Usings
using Model = MeisterGeister.Model;
using System.Diagnostics;
using MeisterGeister.Logic.General;

namespace MeisterGeister.Model.Service
{
    public class NscService : ServiceBase
    {

        #region //----- Felder ----
        // intern
        private static Würfel w2 = new Würfel(2);
        private static Würfel w6 = new Würfel(6);
        private static Würfel w20 = new Würfel(20);
  

        #endregion

        #region //----- EIGENSCHAFT ----

        /* public List<Model.NscMerkmal> WaffeListe
        {
            get { return Liste<NscMerkmal>(); }
        }*/

        #endregion

        #region //----- KONSTRUKTOR ----

        public NscService()
        {
        }

        #endregion

        #region //----- DATENBANKABFRAGEN ----

        public string getRandomHaltung()
        {
            List<NscMerkmal> list = Context.NscMerkmal.Where(m => m.Kategorie == "Haltung").ToList();
            return list[RandomNumberGenerator.Generator.Next(list.Count)].Merkmal;
        }

        public string getRandomGeste()
        {
            List<NscMerkmal> list = Context.NscMerkmal.Where(m => m.Kategorie == "Gesten").ToList();
            return list[RandomNumberGenerator.Generator.Next(list.Count)].Merkmal;
        }

        public string getRandomSprache()
        {
            List<NscMerkmal> list = Context.NscMerkmal.Where(m => m.Kategorie == "Sprache").ToList();
            return list[RandomNumberGenerator.Generator.Next(list.Count)].Merkmal;
        }
 
        public string getRandomGesicht()
        {
            List<NscMerkmal> list = Context.NscMerkmal.Where(m => m.Kategorie.StartsWith("Gesicht")).ToList();
            return list[RandomNumberGenerator.Generator.Next(list.Count)].Merkmal;
        }
        #endregion

        public List<Rasse> getRassenNameDistinct()
        {
            //nur rassen mit kulturen mit namen
            List<Rasse> ret = Liste<Rasse>().Where(r => r.Rasse_Kultur.Any(rk => rk.Kultur.Kultur_Name.Any())).OrderBy(o => o.Name).GroupBy(r => r.Name).Select(grp => grp.First()).ToList();
            ret.OrderBy(k => k.Name);
            ret.Insert(0, new Rasse() { Name = "keine Rasse" });
            return ret;
        }

        public List<Kultur> getKulturenDistinct()
        {
            //nur kulturen mit namen
            List<Kultur> ret = Liste<Kultur>().Where(k => k.Kultur_Name.Any()).OrderBy(o=>o.Name).GroupBy(r => r.Name).Select(grp => grp.First()).ToList();
            ret.Insert(0, new Kultur() { Name = "keine Kultur" });
            return ret;
        }

        public List<Rasse> getRasseVarianten(string geschlecht)
        {
            //nur rassen mit kulturen mit namen
            var rassen = Liste<Rasse>().Where(r => r.Rasse_Kultur.Any(rk => rk.Kultur.Kultur_Name.Any()));
            //handle Orks & Goblins
                if (geschlecht == "m")
                {
                     rassen = rassen.Where(r => !r.Variante.EndsWith("-Frau"));
                }
                    else
                {
                    rassen = rassen.Where(r => !r.Variante.EndsWith("-Mann"));
                }
            return rassen.ToList();
        }

        public List<string> getProfessionenNamenDistinct()
        {
            //TODO MP: distinct db selection of name row?!
            // List<string> ls =Context.Rasse.SelectValue<string>("Rasse.Name").ToList();
            //wird nicht benötigt
            List<string> ret = new List<string>();
            ret.Sort();
            ret.Insert(0, "keine Profession");
            return ret;
        }

        public List<string> getAltersklassen()
        {
            List<string> ret = new List<string>();
            ret.Add("kein Alter");
            ret.AddRange(new string[] { "Kind", "Jugendlich", "Erwachsen", "Senior" });
            return ret;
        }

        public List<Kultur> getKulturenByRasseName(Rasse rasse, bool unüblicheKulturen)
        {
            List<Rasse> rassen = Liste<Rasse>().Where(r => r.Name == rasse.Name).ToList();
            List<Rasse_Kultur> rasse_kultur = new List<Rasse_Kultur>();
            List<Rasse_Kultur> rasse_kulturUnüblich = new List<Rasse_Kultur>();
            //übliche Kulturen wählen 
            List<Kultur> kulturen = Liste<Rasse>().Where(r => r.Name == rasse.Name)
                .Join(Context.Rasse_Kultur, r => r.RasseGUID, rk => rk.RasseGUID, (r, rk) => rk)
                .Where(rk => rk.Unüblich == false).Join(Context.Kultur, rk => rk.KulturGUID, k => k.KulturGUID, (rk, k) => k).Distinct().ToList();
            kulturen.OrderBy(k=>k.Name);
            
            //unüblich unten anfügen falls gewünscht
            if (unüblicheKulturen)
            {
                List<Kultur> kulturenUnüblich = Liste<Rasse>().Where(r => r.Name == rasse.Name)
                .Join(Context.Rasse_Kultur, r => r.RasseGUID, rk => rk.RasseGUID, (r, rk) => rk)
                .Where(rk => rk.Unüblich == true).Join(Context.Kultur, rk => rk.KulturGUID, k => k.KulturGUID, (rk, k) => k).Distinct().ToList();
                kulturenUnüblich.OrderBy(k => k.Name);
                kulturen.AddRange(kulturenUnüblich);
            }

            kulturen.Insert(0, new Kultur() { Name = "keine Kultur" });
            return kulturen;
        }

        public List<string> getProfessionenNamenByKultur(Kultur kultur)
        {
            List<string> ret = new List<string>();
            ret.Add("keine Profession");
            ret.AddRange(new string[] { "Professionen by " + kultur.Name });
            return ret;
        }

        public List<Kultur> getKulturVariantenByKultur(Kultur kultur)
        {
            return Liste<Kultur>().Where(k => k.Name == kultur.Name && k.Kultur_Name.Any()).ToList();
        }

        public List<Rasse> getRasseVariantenByRasse(Rasse rasse, string geschlecht = "")
        {
            var rassen = Liste<Rasse>().Where(r => r.Name == rasse.Name && r.Rasse_Kultur.Any(rk => rk.Kultur.Kultur_Name.Any()));
            //handle Orks & Goblins
            if (geschlecht == "m")
                rassen = rassen.Where(r => !r.Variante.EndsWith("-Frau"));
            else if (geschlecht == "w")
                rassen = rassen.Where(r => !r.Variante.EndsWith("-Mann"));

            return rassen.ToList();
        }

        public List<Kultur> getKulturVariantenByRasseVariante(Rasse rasse, bool unüblich = false)
        {
            //eventuell nur die möglichen Filtern...
            List<Kultur> kulturen = Liste<Rasse_Kultur>().Where(rk => rk.RasseGUID == rasse.RasseGUID && (unüblich || (!unüblich && rk.Unüblich == false)))
                .Join(Context.Kultur.Where(k => k.Kultur_Name.Any()) , rk => rk.KulturGUID, k => k.KulturGUID, (rk, k) => k).Distinct().ToList();
            kulturen.OrderBy(k => k.Name);

            //kulturen.Insert(0, new Kultur() { Name = "keine Kultur" });
            return kulturen;
        }
        public List<Kultur> getKulturVariantenAllByRasseVariante(Rasse rasse)
        {
            List<Kultur> kulturen = Liste<Rasse_Kultur>().Where(rk => rk.RasseGUID == rasse.RasseGUID && rk.Unüblich == false)
                .Join(Context.Kultur.Where(k => k.Kultur_Name.Any()) , rk => rk.KulturGUID, k => k.KulturGUID, (rk, k) => k).Distinct().ToList();
            kulturen.OrderBy(k => k.Name);

           List<Kultur> kulturenUnüblich = Liste<Rasse_Kultur>().Where(rk => rk.RasseGUID == rasse.RasseGUID && rk.Unüblich == true)
                .Join(Context.Kultur.Where(k => k.Kultur_Name.Any()), rk => rk.KulturGUID, k => k.KulturGUID, (rk, k) => k).Distinct().ToList();
            kulturenUnüblich.OrderBy(k => k.Name);
            kulturen.AddRange(kulturenUnüblich);

            kulturen.Insert(0, new Kultur() { Name = "keine Kultur" });
            return kulturen;
        }

        public List<Rasse> getRasseVariantenByKultur(string geschlecht, Kultur kultur)
        {
            List<Rasse> rassen = new List<Rasse>();
            //handle Orks & Goblins
                if (geschlecht == "m")
                {
                    rassen = Liste<Rasse_Kultur>().Where(rk => rk.KulturGUID == kultur.KulturGUID)
                .Join(Context.Rasse, rk => rk.RasseGUID, r => r.RasseGUID, (rk, r) => r).Where(r => !r.Variante.EndsWith("-Frau")).ToList();
                }
                else
                {
                    rassen = Liste<Rasse_Kultur>().Where(rk => rk.KulturGUID == kultur.KulturGUID)
               .Join(Context.Rasse, rk => rk.RasseGUID, r => r.RasseGUID, (rk, r) => r).Where(r => !r.Variante.EndsWith("-Mann")).ToList();
                }     
            rassen.OrderBy(r => r.Name);
            return rassen;
        }

        public string getHaar(string geschlecht, Rasse rasse)
        {
            string ret;           
            if (geschlecht == "m")
            {
                ret = Liste<NscMerkmal>().Where(n => n.Kategorie == "Haartracht Mann").ToList()[RandomNumberGenerator.Generator.Next(Liste<NscMerkmal>().Where(n => n.Kategorie == "Haartracht Mann").ToList().Count)].Merkmal;
            }
            else
            {
                ret = Liste<NscMerkmal>().Where(n => n.Kategorie == "Haartracht Frau").ToList()[RandomNumberGenerator.Generator.Next(Liste<NscMerkmal>().Where(n => n.Kategorie == "Haartracht Frau").ToList().Count)].Merkmal;            
            }
 
            return ret;
        }

        public string getBart(string geschlecht, Rasse rasse)
        {
            string ret = "";
            if (geschlecht == "m")
            {
                //Bart?
                if (!(rasse.Name == "Elf") && RandomNumberGenerator.Generator.Next(1, 20) > 11 && rasse.Name != "Elf")
                {
                    ret = ret + "; " + Liste<NscMerkmal>().Where(n => n.Kategorie == "Barttracht").ToList()[RandomNumberGenerator.Generator.Next(Liste<NscMerkmal>().Where(n => n.Kategorie == "Barttracht").ToList().Count)].Merkmal;
                }
            }
            else
            {
                //Bart?
                if (!(rasse.Name == "Elf") && RandomNumberGenerator.Generator.Next(1, 20) > 11)
                {
                    int random = RandomNumberGenerator.Generator.Next(1, 6);
                    if (random <= 3) ret = ret + "; leichter Damenbart";
                    else if (random <= 5) ret = ret + "; mittlerer Damenbart";
                    else ret = ret + "; starker Damenbart";
                }
            }
            return ret;
        }

        public string getStand()
        {
            //TODO ??: Stand nach Rasse, Kultur, etc.
            int random = RandomNumberGenerator.Generator.Next(1, 10);
            if (random <= 1) return "fahrendes Volk";
            else if (random <= 2) return "mittellos";
            else if (random <= 6) return "Unterschicht";
            else if (random <= 8) return "Mittelschicht";
            else if (random <= 9) return "Oberschicht";
            else return "Adel";
        }

        public string getVerhaltenUndDarstellung()
        {
            string ret = Liste<NscMerkmal>().Where(n => n.Kategorie == "Haltung").ToList()[RandomNumberGenerator.Generator.Next(Liste<NscMerkmal>().Where(n => n.Kategorie == "Haltung").ToList().Count)].Merkmal;
            ret = ret + "; " + Liste<NscMerkmal>().Where(n => n.Kategorie == "Gesten").ToList()[RandomNumberGenerator.Generator.Next(Liste<NscMerkmal>().Where(n => n.Kategorie == "Gesten").ToList().Count)].Merkmal;
            ret = ret + "; " + Liste<NscMerkmal>().Where(n => n.Kategorie == "Sprache").ToList()[RandomNumberGenerator.Generator.Next(Liste<NscMerkmal>().Where(n => n.Kategorie == "Sprache").ToList().Count)].Merkmal;
            return ret;
        }

        public string getSoziales()
        {
            return Liste<NscMerkmal>().Where(n => n.Kategorie == "Soziales").ToList()[RandomNumberGenerator.Generator.Next(Liste<NscMerkmal>().Where(n => n.Kategorie == "Soziales").ToList().Count)].Merkmal;
        }

        public string getHistorie()
        {
            return Liste<NscMerkmal>().Where(n => n.Kategorie == "Historie").ToList()[RandomNumberGenerator.Generator.Next(Liste<NscMerkmal>().Where(n => n.Kategorie == "Historie").ToList().Count)].Merkmal;
        }

        public string getCharakter()
        {
            return Liste<NscMerkmal>().Where(n => n.Kategorie == "Charakter").ToList()[RandomNumberGenerator.Generator.Next(Liste<NscMerkmal>().Where(n => n.Kategorie == "Charakter").ToList().Count)].Merkmal;
        }

        public List<Rasse> getRasseByKulturName(Kultur kultur)
        {
            List<Rasse> rassen = new List<Rasse>();
            Kultur kulturBase = Liste<Kultur>().Where(k => k.Name == kultur.Name && k.Variante.StartsWith(kultur.Name)).FirstOrDefault();
            if (kulturBase == null)
            {
                rassen.Insert(0, new Rasse() { Name = "keine Rasse" });
                return rassen;
            }
            rassen = Liste<Rasse_Kultur>().Where(rk => rk.KulturGUID == kulturBase.KulturGUID)
                .Join(Context.Rasse, rk => rk.RasseGUID, r => r.RasseGUID, (rk, r) => r).OrderBy(o => o.Name).GroupBy(r => r.Name).Select(grp => grp.First()).ToList();

            rassen.Insert(0, new Rasse() { Name = "keine Rasse" });
            return rassen;
        }

        public string getHaarfarbe(Rasse rasse)
        {
            int ran = RandomNumberGenerator.Generator.Next(1, 21);                     
            string haarfarbe="";
            Rasse_Farbe link = Liste<Rasse_Farbe>().Where(rf => rf.RasseGUID == rasse.RasseGUID && rf.Kategorie == "Haare" && rf.W20 >= ran).OrderBy(rf => rf.W20).First();
            if (rasse.Name == "Achaz")
            {
                haarfarbe = Liste<Farbe>().Where(f => f.FarbeID == link.FarbeID).Select(s => s.Name).First().ToString() + "/";
                ran = RandomNumberGenerator.Generator.Next(1, 21);
                link = Liste<Rasse_Farbe>().Where(rf => rf.RasseGUID == rasse.RasseGUID && rf.Kategorie == "Haare" && rf.W20 >= ran).OrderBy(rf => rf.W20).First();
            }
            return haarfarbe + Liste<Farbe>().Where(f => f.FarbeID == link.FarbeID).Select(s => s.Name).First().ToString();
        }

        public string getAugenfarbe(Rasse rasse)
        {
            return Liste<Rasse_Farbe>().Where(rf => rf.RasseGUID == rasse.RasseGUID && rf.Kategorie == "Augen" && rf.W20 >= RandomNumberGenerator.Generator.Next(1, 21)).OrderBy(rf => rf.W20)
                .Join(Context.Farbe, rf => rf.FarbeID, f => f.FarbeID, (rf, f) => f).Select(s => s.Name).First().ToString();
        }

        internal string getKörperlicheEigenschaft()
        {
            List<NscMerkmal> list = Context.NscMerkmal.Where(m => m.Kategorie.StartsWith("Körperliche Eigenschaft")).ToList();
            return list[RandomNumberGenerator.Generator.Next(list.Count)].Merkmal;
        }

        internal string getBehinderung()
        {
            List<NscMerkmal> list = Context.NscMerkmal.Where(m => m.Kategorie.StartsWith("Behinderung")).ToList();
            return list[RandomNumberGenerator.Generator.Next(list.Count)].Merkmal;
        }

        internal string getBesonderes()
        {
            List<NscMerkmal> list = Context.NscMerkmal.Where(m => m.Kategorie.StartsWith("Besonderes")).ToList();
            return list[RandomNumberGenerator.Generator.Next(list.Count)].Merkmal;
        }

        internal string getVorlieben()
        {
            List<NscMerkmal> list = Context.NscMerkmal.Where(m => m.Kategorie.StartsWith("Vorlieben")).ToList();
            return list[RandomNumberGenerator.Generator.Next(list.Count)].Merkmal;
        }
    }
}