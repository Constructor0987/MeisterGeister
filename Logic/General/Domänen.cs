using MeisterGeister.Logic.Literatur;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeisterGeister.Logic.General
{
    public static class Domänen
    {
        private static List<Domäne> domänenListe = null;
        /// <summary>
        /// Erzeugt eine Liste von erzdämonsichen Domänen.
        /// </summary>
        public static IList<Domäne> DomänenListe
        {
            get
            {
                if (domänenListe == null)
                {
                    //TODO Bilder für die Ligaturen.
                    List<Domäne> rlist = new List<Domäne>();
                    rlist.Add(new Domäne(new List<string>() { "BelKZ", "BLK" }, "Blakharaz", "Tyakra'man", "Praios", "WdZ 395"));
                    rlist.Add(new Domäne(new List<string>() { "BLH", "BelHR" }, "Belhalhar", "Xarfai", "Rondra", "WdZ 396"));
                    rlist.Add(new Domäne(new List<string>() { "CPT" }, "Charyptoroth", "Gal'k'zuul", "Efferd", "WdZ 396"));
                    rlist.Add(new Domäne(new List<string>() { "LGM" }, "Lolgramoth", "Thezzphai", "Travia", "WdZ 396"));
                    rlist.Add(new Domäne(new List<string>() { "TGT", "TGNT", "TRGNT" }, "Thargunitoth", "Tijakool", "Boron", "WdZ 397"));
                    rlist.Add(new Domäne(new List<string>() { "AMZ" }, "Amazeroth", "Iribaar", "Hesinde", "WdZ 398"));
                    rlist.Add(new Domäne(new List<string>() { "BLS", "BelSR" }, "Belshirash", "Nagrach", "Firun", "WdZ 398"));
                    rlist.Add(new Domäne(new List<string>() { "ASF" }, "Asfaloth", "Calijnaar", "Tsa", "WdZ 399"));
                    rlist.Add(new Domäne(new List<string>() { "TSF" }, "Tasfarelel", "Zholvar", "Phex", "WdZ 399"));
                    rlist.Add(new Domäne(new List<string>() { "BLZ", "BelZH" }, "Belzhorash", "Mishkhara", "Peraine", "WdZ 400"));
                    rlist.Add(new Domäne(new List<string>() { "AGM" }, "Agrimoth", "Widharcal", "Ingerimm", "WdZ 400"));
                    rlist.Add(new Domäne(new List<string>() { "BLL", "BelKL" }, "Belkelel", "Dar-Klajid", "Rahja", "WdZ 401"));
                    domänenListe = rlist;
                }
                return domänenListe;
            }
        }

        public static Domäne GetDomäne(string kürzel_name_oder_gottheit)
        {
            if (String.IsNullOrWhiteSpace(kürzel_name_oder_gottheit))
                return null;
            Domäne ret = null;
            kürzel_name_oder_gottheit = kürzel_name_oder_gottheit.ToUpperInvariant();
            ret = DomänenListe.Where(d => d.Name.ToUpperInvariant().Equals(kürzel_name_oder_gottheit) || d.NameZhayad.ToUpperInvariant().Equals(kürzel_name_oder_gottheit) || d.Gegengottheit.ToUpperInvariant().Equals(kürzel_name_oder_gottheit)).FirstOrDefault();
            if (ret != null)
                return ret;

            //sonderfälle für den import aus dem heldenblatt.
            if (kürzel_name_oder_gottheit == "NGR")
                kürzel_name_oder_gottheit = "BLS";
            else if (kürzel_name_oder_gottheit == "MKA")
                kürzel_name_oder_gottheit = "BLZ";

            ret = DomänenListe.Where(d => d.Kürzel.Contains(kürzel_name_oder_gottheit)).FirstOrDefault();
            if (ret == null && kürzel_name_oder_gottheit.Length >= 3)
                ret = DomänenListe.Where(d => d.Name.StartsWith(kürzel_name_oder_gottheit) || d.NameZhayad.StartsWith(kürzel_name_oder_gottheit)).FirstOrDefault();
            return ret;
        }
    }

    public class Domäne : ILiteratur
    {
        private IList<string> kürzel;

        /// <summary>
        /// Kürzel des Erzdämonen. Zum Beispiel für Ligaturen in Dämonensiegeln.
        /// </summary>
        public IList<string> Kürzel
        {
            get { return kürzel; }
            set { kürzel = value; }
        }
        private string name;
        
        /// <summary>
        /// Der Name in Ur-Tulamidya oder Zelemja
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string nameZhayad;

        /// <summary>
        /// Name in Zhayad
        /// </summary>
        public string NameZhayad
        {
            get { return nameZhayad; }
            set { nameZhayad = value; }
        }
        private string gegengottheit;

        /// <summary>
        /// Die Gegengottheit zum Erzdämonen
        /// </summary>
        public string Gegengottheit
        {
            get { return gegengottheit; }
            set { gegengottheit = value; }
        }

        public Domäne(IList<string> kürzel, string urtulamidya, string zhayad, string gegengottheit, string literatur)
        {
            this.kürzel = kürzel;
            this.name = urtulamidya;
            this.nameZhayad = zhayad;
            this.gegengottheit = gegengottheit;
            this.literatur = literatur;
        }

        private string literatur;
        public string Literatur
        {
            get { return literatur; }
            set { literatur = value; }
        }
    }
}
