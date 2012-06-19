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
        /// <summary>
        /// Erzeugt eine Liste von Repräsentationen.
        /// </summary>
        public static Dictionary<string, string> RepräsentationenListe
        {
            get
            {
                Dictionary<string, string> list = new Dictionary<string, string>();
                list.Add("Mag", "Gildenmagisch");
                list.Add("Elf", "Elfisch");
                list.Add("Dru", "Druidisch");
                list.Add("Hex", "Hexisch");
                list.Add("Geo", "Geodisch");
                list.Add("Sch", "Schelmisch");
                list.Add("Srl", "Scharlatanisch");
                list.Add("Bor", "Borbaradianisch");
                list.Add("Ach", "Achaz-Kristallomantisch");
                list.Add("Dil", "Magiedilletant");
                list.Add("Alh", "Alhanisch");
                list.Add("Dru/Geo", "Druidisch-Geodisch");
                list.Add("Gro", "Grolmisch");
                list.Add("Gül", "Güldenländisch");
                list.Add("Kop", "Kophtanisch");
                list.Add("Mud", "Mudramulisch");
                list.Add("Sat", "Satuarisch");
                return list;
            }
        }

        public static string GetKürzel(string rep)
        {
            switch (rep)
            {
                case "Gildenmagisch":
                    return "Mag";
                case "Elfisch":
                    return "Elf";
                case "Druidisch":
                    return "Dru";
                case "Hexisch":
                    return "Hex";
                case "Geodisch":
                    return "Geo";
                case "Schelmisch":
                    return "Sch";
                case "Scharlatanisch":
                    return "Srl";
                case "Borbaradianisch":
                    return "Bor";
                case "Achaz-Kristallomantisch":
                    return "Ach";
                case "Magiedilletant":
                    return "Dil";
                case "Alhanisch":
                    return "Alh";
                case "Druidisch-Geodisch":
                    return "Dru/Geo";
                case "Grolmisch":
                    return "Gro";
                case "Güldenländisch":
                    return "Gül";
                case "Kophtanisch":
                    return "Kop";
                case "Mudramulisch":
                    return "Mud";
                case "Satuarisch":
                    return "Sat";
                default:
                    return "Mag";
            }
        }

        public static string GetName(string rep)
        {
            switch (rep)
            {
                case "Mag":
                    return "Gildenmagisch";
                case "Magier":
                    return "Gildenmagisch";
                case "Elf":
                    return "Elfisch";
                case "Dru":
                    return "Druidisch";
                case "Druide":
                    return "Druidisch";
                case "Hex":
                    return "Hexisch";
                case "Hexe":
                    return "Hexisch";
                case "Geo":
                    return "Geodisch";
                case "Geode":
                    return "Geodisch";
                case "Sch":
                    return "Schelmisch";
                case "Schelm":
                    return "Schelmisch";
                case "Srl":
                    return "Scharlatanisch";
                case "Scharlatan":
                    return "Scharlatanisch";
                case "Borbaradianer":
                    return "Borbaradianisch";
                case "Bor":
                    return "Borbaradianisch";
                case "Ach":
                    return "Achaz-Kristallomantisch";
                case "Achaz":
                    return "Achaz-Kristallomantisch";
                case "Dil":
                    return "Magiedilletant";
                case "Magiedilletant":
                    return "Magiedilletant";
                case "Alh":
                    return "Alhanisch";
                case "Alhanisch":
                    return "Alhanisch";
                case "Dru/Geo":
                    return "Druidisch-Geodisch";
                case "Druide/Geode":
                    return "Druidisch-Geodisch";
                case "Gro":
                    return "Grolmisch";
                case "Grolmisch":
                    return "Grolmisch";
                case "Gül":
                    return "Güldenländisch";
                case "Güldenländisch":
                    return "Güldenländisch";
                case "Kop":
                    return "Kophtanisch";
                case "Kophtanisch":
                    return "Kophtanisch";
                case "Mud":
                    return "Mudramulisch";
                case "Mudramulisch":
                    return "Mudramulisch";
                case "Sat":
                    return "Satuarisch";
                case "Satuarisch":
                    return "Satuarisch";
                default:
                    return "Gildenmagisch";
            }
        }
    }
}
