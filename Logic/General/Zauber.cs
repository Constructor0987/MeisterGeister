using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeisterGeister.Logic.Kalender;

namespace MeisterGeister.Logic.General
{
    public class Zauber : IDreierProbe
    {
        public Zauber(Daten.DatabaseDSADataSet.ZauberRow zRow)
        {
            ZauberDataRow = zRow;
        }

        public Zauber(string zauber)
        {
            var rows = App.DatenDataSet.Zauber.Select(string.Format("Name = '{0}'", zauber));
            if (rows != null && rows.Length > 0)
                ZauberDataRow = (Daten.DatabaseDSADataSet.ZauberRow)rows[0];
        }

        public string Name
        {
            get { return ZauberDataRow == null ? string.Empty : ZauberDataRow.Name; }
        }

        public string Eigenschaft1
        {
            get
            {
                if (ZauberDataRow == null || ZauberDataRow.IsEigenschaft1Null())
                    return "?";
                return ZauberDataRow.Eigenschaft1;
            }
        }
        public string Eigenschaft2
        {
            get
            {
                if (ZauberDataRow == null || ZauberDataRow.IsEigenschaft2Null())
                    return "?";
                return ZauberDataRow.Eigenschaft2;
            }
        }
        public string Eigenschaft3
        {
            get
            {
                if (ZauberDataRow == null || ZauberDataRow.IsEigenschaft3Null())
                    return "?";
                return ZauberDataRow.Eigenschaft3;
            }
        }

        public string WikiLink
        {
            get
            {
                return Name.Replace(" ", "_");
            }
        }

        public string Literatur
        {
            get
            {
                if (ZauberDataRow == null || ZauberDataRow.IsLiteraturNull())
                    return "?";
                return ZauberDataRow.Literatur;
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

        public Daten.DatabaseDSADataSet.ZauberRow ZauberDataRow { private set; get; }

        /// <summary>
        /// Stellt ein Zauber auf die Probe.
        /// </summary>
        /// <param name="eigenschaft1">Wert der ersten Eigenschaft</param>
        /// <param name="eigenschaft2">Wert der zweiten Eigenschaft</param>
        /// <param name="eigenschaft3">Wert der dritten Eigenschaft</param>
        /// <param name="zfw">Talentwert</param>
        /// <param name="held"></param>
        /// <param name="mod">Erschwernis (+) oder Erleichterung (-)</param>
        /// <param name="würfel1">Ergebnis des 1. Würfels</param>
        /// <param name="würfel2">Ergebnis des 2. Würfels</param>
        /// <param name="würfel3">Ergebnis des 3. Würfels</param>
        /// <returns></returns>
        public static MeisterGeister.LogicAlt.General.DreierProbenErgebnis Probe(int eigenschaft1, int eigenschaft2, int eigenschaft3,
            int zfw, string probeName, Held held = null, int mod = 0, uint würfel1 = 0u, uint würfel2 = 0u, uint würfel3 = 0u)
        {
            int zfwEff = zfw - mod;
            var ergebnis = new MeisterGeister.LogicAlt.General.DreierProbenErgebnis { Held = held, Wert = zfw, Mod = mod };

            int modEigenschaftProbe = Math.Min(zfwEff, 0) * -1;
            ergebnis.E1Ergebnis = Eigenschaft.Probe(eigenschaft1, held, modEigenschaftProbe, würfel1);
            ergebnis.E1Ergebnis.TalentProbenErgebnis = ergebnis;
            ergebnis.E2Ergebnis = Eigenschaft.Probe(eigenschaft2, held, modEigenschaftProbe, würfel2);
            ergebnis.E2Ergebnis.TalentProbenErgebnis = ergebnis;
            ergebnis.E3Ergebnis = Eigenschaft.Probe(eigenschaft3, held, modEigenschaftProbe, würfel3);
            ergebnis.E3Ergebnis.TalentProbenErgebnis = ergebnis;
            int zfpÜ = Math.Max(zfwEff, 0);
            zfpÜ += Math.Min(ergebnis.E1Ergebnis.Übrig, 0);
            zfpÜ += Math.Min(ergebnis.E2Ergebnis.Übrig, 0);
            zfpÜ += Math.Min(ergebnis.E3Ergebnis.Übrig, 0);
            ergebnis.Übrig = Math.Min(zfpÜ, zfw);
            if (ergebnis.Übrig == 0)
                ergebnis.Übrig = 1;
            ergebnis.Gelungen = (ergebnis.Übrig >= 0 || (int)ergebnis.PatzerGlückswurf >= 2)
                && (int)ergebnis.PatzerGlückswurf >= -1;

            SpezielleErfahrungSpeichern(probeName, ergebnis);

            return ergebnis;
        }

        public static void Probe(ref MeisterGeister.LogicAlt.General.DreierProbenErgebnis probe, string probeName,
            Held held = null, int mod = 0, uint würfel1 = 0u, uint würfel2 = 0u, uint würfel3 = 0u)
        {
            if (probe != null)
            {
                int zfwEff = probe.Wert - mod;
                int modEigenschaftProbe = Math.Min(zfwEff, 0) * -1;
                probe.E1Ergebnis = Eigenschaft.Probe(probe.E1Ergebnis.Wert, held, modEigenschaftProbe, würfel1);
                probe.E1Ergebnis.TalentProbenErgebnis = probe;
                probe.E2Ergebnis = Eigenschaft.Probe(probe.E2Ergebnis.Wert, held, modEigenschaftProbe, würfel2);
                probe.E2Ergebnis.TalentProbenErgebnis = probe;
                probe.E3Ergebnis = Eigenschaft.Probe(probe.E3Ergebnis.Wert, held, modEigenschaftProbe, würfel3);
                probe.E3Ergebnis.TalentProbenErgebnis = probe;
                int zfpÜ = Math.Max(zfwEff, 0);
                zfpÜ += Math.Min(probe.E1Ergebnis.Übrig, 0);
                zfpÜ += Math.Min(probe.E2Ergebnis.Übrig, 0);
                zfpÜ += Math.Min(probe.E3Ergebnis.Übrig, 0);
                probe.Übrig = Math.Min(zfpÜ, probe.Wert);
                if (probe.Übrig == 0)
                    probe.Übrig = 1;
                probe.Gelungen = (probe.Übrig >= 0 || (int)probe.PatzerGlückswurf >= 2)
                    && (int)probe.PatzerGlückswurf >= -1;

                SpezielleErfahrungSpeichern(probeName, probe);
            }
        }

        private static void SpezielleErfahrungSpeichern(string probeName, MeisterGeister.LogicAlt.General.DreierProbenErgebnis ergebnis)
        {
            // Bei Doppel1 oder Dreifach1 eine SE
            if ((int)ergebnis.PatzerGlückswurf >= 2)
            {
                string seTxt = string.Format("\n{0} - {1} - Spezielle Erfahrung in {2} (ZfW {3}) für {4} ({5})\n",
                    DateTime.Now.ToString("g"), Datum.Aktuell.ToStringShort(),
                    probeName, ergebnis.Wert, ergebnis.Held.Name, ergebnis.PatzerGlückswurf.ToString());

                Global.ContextNotizen.NotizErfahrungen.AppendText(seTxt);
            }
        }

        public static System.Collections.Generic.SortedList<string, int> ZauberList
        {
            get
            {
                System.Collections.Generic.SortedList<string, int> zauber = new System.Collections.Generic.SortedList<string, int>();
                foreach (var z in App.DatenDataSet.Zauber)
                {
                    zauber.Add(z.Name, z.ZauberID);
                }
                return zauber;
            }
        }
    }
}
