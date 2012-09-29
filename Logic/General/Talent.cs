using System;
using System.Windows.Documents;
using System.IO;
using MeisterGeister.Logic.Kalender;

namespace MeisterGeister.LogicAlt.General
{
    public class Talent : IDreierProbe
    {
        public Talent(string talentname)
        {
            var rows = (Daten.DatabaseDSADataSet.TalentRow[])App.DatenDataSet.Talent.Select("Talentname = '" + talentname.Replace("'", "''") + "'");
            if (rows.Length > 0)
                TalentDataRow = rows[0];
        }

        public Talent(Daten.DatabaseDSADataSet.TalentRow talentRow)
        {
            TalentDataRow = talentRow;
        }

        public Daten.DatabaseDSADataSet.TalentRow TalentDataRow { private set; get; }

        public string Name
        {
            get { return TalentDataRow == null ? string.Empty : TalentDataRow.Talentname; }
        }

        public string WikiLink
        {
            get
            {
                if (TalentDataRow != null && !TalentDataRow.IsWikiLinkNull() && TalentDataRow.WikiLink.Trim() != string.Empty)
                    return TalentDataRow.WikiLink.Replace(" ", "_");
                if (Name.StartsWith("Lesen/Schreiben"))
                    return Name.Replace("Lesen/Schreiben (", "").TrimEnd(')').Replace(" ", "_");
                if (Name.StartsWith("Sprachen Kennen"))
                    return Name.Replace("Sprachen Kennen (", "").TrimEnd(')').Replace(" ", "_");
                if (Name.StartsWith("Nahrung Sammeln"))
                    return "Nahrung Sammeln";
                if (Name.StartsWith("Pirschjagd"))
                    return "Pirschjagd";
                if (Name.StartsWith("Ansitzjagd"))
                    return "Ansitzjagd";
                return Name.Replace(" ", "_");
            }
        }

        public string Eigenschaft1
        {
            get
            {
                if (TalentDataRow == null || TalentDataRow.IsEigenschaft1Null())
                    return "?";
                return TalentDataRow.Eigenschaft1;
            }
        }
        public string Eigenschaft2
        {
            get
            {
                if (TalentDataRow == null || TalentDataRow.IsEigenschaft2Null())
                    return "?";
                return TalentDataRow.Eigenschaft2;
            }
        }
        public string Eigenschaft3
        {
            get
            {
                if (TalentDataRow == null || TalentDataRow.IsEigenschaft3Null())
                    return "?";
                return TalentDataRow.Eigenschaft3;
            }
        }

        public string Literatur
        {
            get
            {
                if (TalentDataRow == null || TalentDataRow.IsLiteraturNull())
                    return "?";
                return TalentDataRow.Literatur;
            }
        }

        public string EffektiveBehinderung
        {
            get
            {
                if (TalentDataRow == null || TalentDataRow.IseBENull())
                    return "-";
                return TalentDataRow.eBE;
            }
        }

        public string Voraussetzungen
        {
            get
            {
                if (TalentDataRow == null || TalentDataRow.IsVoraussetzungenNull())
                    return string.Empty;
                return TalentDataRow.Voraussetzungen;
            }
        }

        public string Spezialisierungen
        {
            get
            {
                if (TalentDataRow == null || TalentDataRow.IsSpezialisierungenNull())
                    return string.Empty;
                return TalentDataRow.Spezialisierungen;
            }
        }

        public string ProbenText
        {
            get
            {
                string txt = String.Format("{0}-Probe ({1}/{2}/{3})",
                    Name, Eigenschaft1, Eigenschaft2, Eigenschaft3);
                return txt;
            }
        }

        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// Stellt ein Talent auf die Probe.
        /// </summary>
        /// <param name="eigenschaft1">Wert der ersten Eigenschaft</param>
        /// <param name="eigenschaft2">Wert der zweiten Eigenschaft</param>
        /// <param name="eigenschaft3">Wert der dritten Eigenschaft</param>
        /// <param name="taw">Talentwert</param>
        /// <param name="held"></param>
        /// <param name="mod">Erschwernis (+) oder Erleichterung (-)</param>
        /// <param name="würfel1">Ergebnis des 1. Würfels</param>
        /// <param name="würfel2">Ergebnis des 2. Würfels</param>
        /// <param name="würfel3">Ergebnis des 3. Würfels</param>
        /// <returns></returns>
        public static MeisterGeister.LogicAlt.General.DreierProbenErgebnis Probe(int eigenschaft1, int eigenschaft2, int eigenschaft3,
            int taw, string probeName, Held held = null, int mod = 0, uint würfel1 = 0u, uint würfel2 = 0u, uint würfel3 = 0u)
        {
            int tawEff = taw - mod;
            var ergebnis = new MeisterGeister.LogicAlt.General.DreierProbenErgebnis { Held = held, Wert = taw, Mod = mod };

            int modEigenschaftProbe = Math.Min(tawEff, 0) * -1;
            ergebnis.E1Ergebnis = Eigenschaft.Probe(eigenschaft1, held, modEigenschaftProbe, würfel1);
            ergebnis.E1Ergebnis.TalentProbenErgebnis = ergebnis;
            ergebnis.E2Ergebnis = Eigenschaft.Probe(eigenschaft2, held, modEigenschaftProbe, würfel2);
            ergebnis.E2Ergebnis.TalentProbenErgebnis = ergebnis;
            ergebnis.E3Ergebnis = Eigenschaft.Probe(eigenschaft3, held, modEigenschaftProbe, würfel3);
            ergebnis.E3Ergebnis.TalentProbenErgebnis = ergebnis;
            int tapÜ = Math.Max(tawEff, 0);
            tapÜ += Math.Min(ergebnis.E1Ergebnis.Übrig, 0);
            tapÜ += Math.Min(ergebnis.E2Ergebnis.Übrig, 0);
            tapÜ += Math.Min(ergebnis.E3Ergebnis.Übrig, 0);
            ergebnis.Übrig = Math.Min(tapÜ, taw);
            if (ergebnis.Übrig == 0)
                ergebnis.Übrig = 1;
            ergebnis.Gelungen = (ergebnis.Übrig >= 0 || (int)ergebnis.PatzerGlückswurf >= 2)
                && (int)ergebnis.PatzerGlückswurf >= -1;

            SpezielleErfahrungSpeichern(probeName, ergebnis);

            return ergebnis;
        }

        private static void SpezielleErfahrungSpeichern(string probeName, MeisterGeister.LogicAlt.General.DreierProbenErgebnis ergebnis)
        {
            // Bei Doppel1 oder Dreifach1 eine SE
            if ((int)ergebnis.PatzerGlückswurf >= 2)
            {
                string seTxt = string.Format("\n{0} - {1} - Spezielle Erfahrung in {2} (TaW {3}) für {4} ({5})\n",
                    DateTime.Now.ToString("g"), Datum.Aktuell.ToStringShort(),
                    probeName, ergebnis.Wert, ergebnis.Held.Name, ergebnis.PatzerGlückswurf.ToString());

                Global.ContextNotizen.NotizErfahrungen.AppendText(seTxt);
            }
        }

        public static void Probe(ref MeisterGeister.LogicAlt.General.DreierProbenErgebnis probe, string probeName,
            Held held = null, int mod = 0, uint würfel1 = 0u, uint würfel2 = 0u, uint würfel3 = 0u)
        {
            if (probe != null)
            {
                int tawEff = probe.Wert - mod;
                int modEigenschaftProbe = Math.Min(tawEff, 0) * -1;
                probe.E1Ergebnis = Eigenschaft.Probe(probe.E1Ergebnis.Wert, held, modEigenschaftProbe, würfel1);
                probe.E1Ergebnis.TalentProbenErgebnis = probe;
                probe.E2Ergebnis = Eigenschaft.Probe(probe.E2Ergebnis.Wert, held, modEigenschaftProbe, würfel2);
                probe.E2Ergebnis.TalentProbenErgebnis = probe;
                probe.E3Ergebnis = Eigenschaft.Probe(probe.E3Ergebnis.Wert, held, modEigenschaftProbe, würfel3);
                probe.E3Ergebnis.TalentProbenErgebnis = probe;
                int tapÜ = Math.Max(tawEff, 0);
                tapÜ += Math.Min(probe.E1Ergebnis.Übrig, 0);
                tapÜ += Math.Min(probe.E2Ergebnis.Übrig, 0);
                tapÜ += Math.Min(probe.E3Ergebnis.Übrig, 0);
                probe.Übrig = Math.Min(tapÜ, probe.Wert);
                if (probe.Übrig == 0)
                    probe.Übrig = 1;
                probe.Gelungen = (probe.Übrig >= 0 || (int)probe.PatzerGlückswurf >= 2)
                    && (int)probe.PatzerGlückswurf >= -1;

                SpezielleErfahrungSpeichern(probeName, probe);
            }
        }
    }

    public enum PatzerGlückswurf
    {
        Normal = 0,
        Einfach1 = 1,
        Doppel1 = 2,
        Dreifach1 = 3,
        Einfach20 = -1,
        Doppel20 = -2,
        Dreifach20 = -3
    }
}
