using MeisterGeister.Logic.Literatur;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.Logic.General
{
    /// <summary>
    /// Beschreibt eine Liste von Repräsentationen.
    /// </summary>
    public static class Repräsentationen
    {
        private static List<Repräsentation> repräsentationenListe = null;
        /// <summary>
        /// Erzeugt eine Liste von Repräsentationen.
        /// </summary>
        public static List<Repräsentation> RepräsentationenListe
        {
            get
            {
                if (repräsentationenListe == null)
                {
                    List<Repräsentation> rlist = new List<Repräsentation>();
                    rlist.Add(new Repräsentation("Gildenmagisch", "Mag", "KL", "WdZ 259"));
                    rlist.Add(new Repräsentation("Elfisch", "Elf", "IN", "WdZ 321"));
                    rlist.Add(new Repräsentation("Druidisch", "Dru", "KL", "WdZ 318"));
                    rlist.Add(new Repräsentation("Hexisch", "Hex", "IN", "WdZ 309"));
                    rlist.Add(new Repräsentation("Geodisch", "Geo", "KL", "WdZ 314"));
                    rlist.Add(new Repräsentation("Schelmisch", "Sch", "IN", "WdZ 327"));
                    rlist.Add(new Repräsentation("Scharlatanisch", "Srl", "KL", "WdZ 304"));
                    rlist.Add(new Repräsentation("Borbaradianisch", "Bor", "KL", "WdZ 259"));
                    rlist.Add(new Repräsentation("Achaz-Kristallomantisch", "Ach", "KL", "WdZ 323"));
                    rlist.Add(new Repräsentation("Magiedilletant", "Dil", "IN", "WdZ 37")); //ist eigentlich keine Repräsentation
                    rlist.Add(new Repräsentation("Alhanisch", "Alh", "IN", "OiC 49"));
                    rlist.Add(new Repräsentation("Grolmisch", "Gro", "KL", "OiC 49"));
                    rlist.Add(new Repräsentation("Güldenländisch", "Gül", "KL", "OiC 50"));
                    rlist.Add(new Repräsentation("Kophtanisch", "Kop", "CH", "OiC 50"));
                    rlist.Add(new Repräsentation("Mudramulisch", "Mud", "IN", "OiC 50"));
                    rlist.Add(new Repräsentation("Satuarisch", "Sat", "IN", "OiC 51"));
                    rlist.Add(new Repräsentation("Druidisch-Geodisch", "Dru/Geo", "KL", "OiC 49"));
                    repräsentationenListe = rlist;
                }
                return repräsentationenListe;
            }
        }

        public static string GetKürzel(string rep)
        {
            var r = GetRepräsentation(rep);
            return r.Kürzel;
        }

        public static string GetName(string rep)
        {
            var r = GetRepräsentation(rep);
            return r.Name;
        }

        public static Repräsentation GetRepräsentation(string rep)
        {
            Repräsentation ret = null;
            ret = RepräsentationenListe.Where(r => r.Name == rep || r.Kürzel == rep).FirstOrDefault();
            if (ret == null)
            {
                switch (rep)
                {
                    case "Magier":
                        rep = "Gildenmagisch";
                        break;
                    case "Druide":
                        rep = "Druidisch";
                        break;
                    case "Hexe":
                        rep = "Hexisch";
                        break;
                    case "Geode":
                        rep = "Geodisch";
                        break;
                    case "Schelm":
                        rep = "Schelmisch";
                        break;
                    case "Scharlatan":
                        rep = "Scharlatanisch";
                        break;
                    case "Borbaradianer":
                        rep = "Borbaradianisch";
                        break;
                    case "Achaz":
                        rep = "Achaz-Kristallomantisch";
                        break;
                    case "Druide/Geode":
                        rep = "Druidisch-Geodisch";
                        break;
                    default:
                        rep = "Gildenmagisch"; // sollte evtl. null oder Magiedilletant sein
                        break;
                }
                ret = RepräsentationenListe.Where(r => r.Name == rep || r.Kürzel == rep).FirstOrDefault();
            }
            return ret;
        }

        private static string[] sonderfälle = new string[] { "Geodisch" };
        public static string GetLeitattribut(Model.Held held, Repräsentation r)
        {
            if (held != null && r != null && sonderfälle.Contains(r.Name))
            {
                //Profession prüfen
                if (held.Profession != null && held.Profession.Contains("Diener Sumus"))
                    return "IN";
            }
            return r.Leitattribut;
        }

        public static string GetLeitattribut(Model.Held held, string rep)
        {
            var r = GetRepräsentation(rep);
            return GetLeitattribut(held, r);
        }
    }

    public class Repräsentation : ILiteratur
    {
        string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        string kürzel;

        public string Kürzel
        {
            get { return kürzel; }
            set { kürzel = value; }
        }
        string leitattribut;

        public string Leitattribut
        {
            get { return leitattribut; }
            set { leitattribut = value; }
        }
        string literatur;

        public string Literatur
        {
            get { return literatur; }
            set { literatur = value; }
        }

        public Repräsentation(string name, string kürzel, string leitattribut, string literatur)
        {
            Name = name;
            Kürzel = kürzel;
            Leitattribut = leitattribut;
            Literatur = literatur;
        }
    }
}
