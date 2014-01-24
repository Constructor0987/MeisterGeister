using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeisterGeister.Logic.Extensions;

namespace MeisterGeister.Logic.General
{
    /// <summary>
    /// Beschreibt eine Liste von Repräsentationen.
    /// </summary>
    public static class Merkmale
    {
        private static List<Merkmal> merkmalListe = null;
        /// <summary>
        /// Erzeugt eine Liste von Merkmalen.
        /// </summary>
        public static List<Merkmal> MerkmalListe
        {
            get
            {
                if (merkmalListe == null)
                {
                    List<Merkmal> rlist = new List<Merkmal>();
                    rlist.Add(new Merkmal("Antimagie", new List<string>() { "Anti" }));
                    rlist.Add(new Merkmal("Beschwörung", new List<string>() { "Besw" }));
                    rlist.Add(new Merkmal("Dämonisch", new List<string>() { "Dämo" }));
                    foreach (var domäne in Domänen.DomänenListe)
                        rlist.Add(new Merkmal(String.Format("Dämonisch ({0})", domäne.Name), domäne.Kürzel.Select(d => String.Format("Dämo ({0})", d)).ToList()));
                    rlist.Add(new Merkmal("Eigenschaften", new List<string>() { "Eign" }));
                    rlist.Add(new Merkmal("Einfluss", new List<string>() { "Einfl", "Einf" }));
                    rlist.Add(new Merkmal("Elementar", new List<string>() { "Elem" }));
                    rlist.Add(new Merkmal("Elementar (Eis)", new List<string>() { "Elem (Eis)" }));
                    rlist.Add(new Merkmal("Elementar (Erz)", new List<string>() { "Elem (Erz)" }));
                    rlist.Add(new Merkmal("Elementar (Feuer)", new List<string>() { "Elem (Feuer)" }));
                    rlist.Add(new Merkmal("Elementar (Humus)", new List<string>() { "Elem (Humus)" }));
                    rlist.Add(new Merkmal("Elementar (Luft)", new List<string>() { "Elem (Luft)" }));
                    rlist.Add(new Merkmal("Elementar (Wasser)", new List<string>() { "Elem (Wasser)" }));
                    rlist.Add(new Merkmal("Form", new List<string>() { "Form" }));
                    rlist.Add(new Merkmal("Geisterwesen", new List<string>() { "Geis" }));
                    rlist.Add(new Merkmal("Heilung", new List<string>() { "Heil" }));
                    rlist.Add(new Merkmal("Hellsicht", new List<string>() { "Hell" }));
                    rlist.Add(new Merkmal("Herrschaft", new List<string>() { "Herr" }));
                    rlist.Add(new Merkmal("Illusion", new List<string>() { "Illu" }));
                    rlist.Add(new Merkmal("Kraft", new List<string>() { "Krft" }));
                    rlist.Add(new Merkmal("Limbus", new List<string>() { "Limb" }));
                    rlist.Add(new Merkmal("Metamagie", new List<string>() { "Meta" }));
                    rlist.Add(new Merkmal("Objekt", new List<string>() { "Objk" }));
                    rlist.Add(new Merkmal("Herbeirufung", new List<string>() { "Herb", "Rufe" }));
                    rlist.Add(new Merkmal("Schaden", new List<string>() { "Scha" }));
                    rlist.Add(new Merkmal("Telekinese", new List<string>() { "Tele" }));
                    rlist.Add(new Merkmal("Temporal", new List<string>() { "Temp" }));
                    rlist.Add(new Merkmal("Umwelt", new List<string>() { "Umwt" }));
                    rlist.Add(new Merkmal("Verständigung", new List<string>() { "Vers", "Verst" }));
                    merkmalListe = rlist;
                }
                return merkmalListe;
            }
        }

        public static Merkmal GetMerkmal(string merk)
        {
            Merkmal ret = null;
            ret = MerkmalListe.Where(r => r.Name == merk || r.Kürzel.Contains(merk) || r.Kürzel.Where(k => k.Left(4) == merk.Left(4)).FirstOrDefault() != null).FirstOrDefault();
            if (ret == null)
            {
                if (merk.StartsWith("Dämo ") || merk.StartsWith("Dämo(") || merk.StartsWith("D("))
                {
                    var d = merk.GetAusdruckInKlammern();
                    if(d.ToLowerInvariant().StartsWith("ges"))
                        ret = MerkmalListe.Where(r => r.Name == "Dämonisch").FirstOrDefault();
                    var domäne = Domänen.GetDomäne(d);
                    if(domäne != null)
                        ret = MerkmalListe.Where(r => r.Name == String.Format("Dämonisch ({0})", domäne.Name)).FirstOrDefault();
                }
                else if (merk.StartsWith("Elem ") || merk.StartsWith("Elem(") || merk.StartsWith("E("))
                {
                    var element = merk.GetAusdruckInKlammern();
                    if (element != null)
                    {
                        if(element.ToLowerInvariant().StartsWith("ges"))
                            ret = merkmalListe.Where(r => r.Name == "Elementar").FirstOrDefault();
                        ret = MerkmalListe.Where(r => r.Name == String.Format("Elementar ({0})", element)).FirstOrDefault();
                    }
                }
            }
            return ret;
        }
    }

    public class Merkmal
    {
        string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        IList<string> kürzel;

        public IList<string> Kürzel
        {
            get { return kürzel; }
            set { kürzel = value; }
        }
        string literatur;

        public Merkmal(string name, IList<string> kürzel)
        {
            Name = name;
            Kürzel = kürzel;
        }
    }
}
