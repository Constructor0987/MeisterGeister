using System;
// Eigene Usings
using MeisterGeister.Logic.Settings;

namespace MeisterGeister.Logic.General
{
    public class ProbenErgebnisComparer : System.Collections.Generic.IComparer<ProbenErgebnis>
    {
        #region IComparer<ProbenErgebnis> Member

        public int Compare(ProbenErgebnis x, ProbenErgebnis y)
        {
            if (x == null)
            {
                if (y == null)
                    return 0; // gleich
                else
                    return 1; // y größer
            }
            else
            {
                if (y == null)
                    return -1; // x größer
                else
                {
                    int result = 0;
                    result = y.Wert.CompareTo(x.Wert);
                    if (result == 0) // gleich
                    {
                        int sum1 = x.EigenschaftenSumme, sum2 = y.EigenschaftenSumme;
                        result = sum2.CompareTo(sum1);
                        if (result == 0)
                            result = x.Held.Name.CompareTo(y.Held.Name);
                    }
                    return result;
                }
            }
        }

        #endregion
    }

    public class ProbenErgebnis
    {
        public Held Held { get; set; }

        public int Übrig { get; set; }

        public int Wert { get; set; }

        public virtual int EigenschaftenSumme { get { return Wert; } }

        public int Mod { get; set; }

        public bool Gelungen { get; set; }

        public virtual string ÜbrigText
        {
            get
            {
                return null;
            }
        }

        public virtual string ErfolgsChance
        {
            get
            {
                return String.Format("{0}% Chance", "?");
            }
        }

        public bool IstEigenschaftProbe
        {
            get
            {
                return this is EigenschaftProbenErgebnis;
            }
        }

        public bool IstDreierProbe
        {
            get
            {
                return this is DreierProbenErgebnis;
            }
        }
    }

    public class DreierProbenErgebnis : ProbenErgebnis
    {
        public EigenschaftProbenErgebnis E1Ergebnis { get; set; }
        public EigenschaftProbenErgebnis E2Ergebnis { get; set; }
        public EigenschaftProbenErgebnis E3Ergebnis { get; set; }

        public string TextHinweis { get; set; }

        public string PunkteArt = "TaP";

        public override int EigenschaftenSumme
        {
            get { return E1Ergebnis.Wert + E2Ergebnis.Wert + E3Ergebnis.Wert; }
        }

        public System.Windows.Visibility VisibilityGelungen
        {
            get
            {
                if (Gelungen && E1Ergebnis.Gewürfelt != 0)
                    return System.Windows.Visibility.Visible;
                else
                    return System.Windows.Visibility.Collapsed;
            }
        }

        public System.Windows.Visibility VisibilityMisslungen
        {
            get
            {
                if (!Gelungen && E1Ergebnis.Gewürfelt != 0)
                    return System.Windows.Visibility.Visible;
                else
                    return System.Windows.Visibility.Collapsed;
            }
        }

        public int Behinderung { get; set; }
        
        public string Eigenschaften
        {
            get
            {
                return string.Format("({0}/{1}/{2})", (E1Ergebnis == null ? "?" : E1Ergebnis.Wert.ToString()),
                    (E2Ergebnis == null ? "?" : E2Ergebnis.Wert.ToString()),
                    (E3Ergebnis == null ? "?" : E3Ergebnis.Wert.ToString()))
                    + ((Behinderung > 0) ? " eBE " + Behinderung : string.Empty);
            }
        }

        public PatzerGlückswurf PatzerGlückswurf
        {
            get
            {
                return (PatzerGlückswurf)PatzerGlückswurfCount();
            }
        }

        public override string ErfolgsChance
        {
            get
            {
                return String.Format("{0:P} Chance", ErfolgsChanceBerechnen(E1Ergebnis.Wert, E2Ergebnis.Wert, E3Ergebnis.Wert, Wert - Mod));
            }
        }

        public double ErfolgsChanceBerechnen(int e1, int e2, int e3, int taw)
        {
            int success, restTaP;
            if (taw < 0) return ErfolgsChanceBerechnen(e1 + taw, e2 + taw, e3 + taw, 0);

            success = 0;
            for (int w1 = 1; w1 <= 20; w1++)
            {
                for (int w2 = 1; w2 <= 20; w2++)
                {
                    for (int w3 = 1; w3 <= 20; w3++)
                    {
                        if (CheckMeisterhaft(w1, w2, w3))
                        {
                            success++;
                        }
                        else
                        {
                            if (!CheckPatzer(w1, w2, w3))
                            {
                                // schauen, ob die Rest-TaP nicht unter 0 fallen
                                restTaP = taw - Math.Max(0, w1 - e1) - Math.Max(0, w2 - e2) - Math.Max(0, w3 - e3);
                                if (restTaP >= 0)
                                {
                                    // hat gereicht
                                    success++;
                                }
                            }
                        }
                    }
                }
            }
            return (1d / 8000d * (success));
        }

        private static bool CheckMeisterhaft(int w1, int w2, int w3)
        {
            return (w1 == 1) && (w2 == 1) ||
                   (w2 == 1) && (w3 == 1) ||
                   (w1 == 1) && (w3 == 1);
        }

        private static bool CheckPatzer(int w1, int w2, int w3)
        {
            return (w1 == 20) && (w2 == 20) ||
               (w2 == 20) && (w3 == 20) ||
               (w1 == 20) && (w3 == 20);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns>Kleiner 0: Patzer; größer 0: Glückswurf</returns>
        private int PatzerGlückswurfCount()
        {
            int patzerCount = 0;
            patzerCount += E1Ergebnis.Patzer ? 1 : 0;
            patzerCount += E2Ergebnis.Patzer ? 1 : 0;
            patzerCount += E3Ergebnis.Patzer ? 1 : 0;
            int glücksCount = 0;
            glücksCount += E1Ergebnis.Glückswurf ? 1 : 0;
            glücksCount += E2Ergebnis.Glückswurf ? 1 : 0;
            glücksCount += E3Ergebnis.Glückswurf ? 1 : 0;
            if (patzerCount >= 2)
                return patzerCount * -1;
            if (glücksCount >= 2)
                return glücksCount;
            return 0;
        }

        public override string ToString()
        {
            string txt = String.Format("{0}/{1}/{2}\t-> {3} {4}*{5}",
                (Math.Abs(E1Ergebnis.Übrig) > 9 ? E1Ergebnis.Übrig.ToString("+#;-#;  0") : E1Ergebnis.Übrig.ToString("+ #;- #;  0")),
                (Math.Abs(E2Ergebnis.Übrig) > 9 ? E2Ergebnis.Übrig.ToString("+#;-#;  0") : E2Ergebnis.Übrig.ToString("+ #;- #;  0")),
                (Math.Abs(E3Ergebnis.Übrig) > 9 ? E3Ergebnis.Übrig.ToString("+#;-#;  0") : E3Ergebnis.Übrig.ToString("+ #;- #;  0")),
                Übrig, PunkteArt,
                PatzerGlückswurf != PatzerGlückswurf.Normal ? " (" + PatzerGlückswurf + ")" : string.Empty);
            return txt;
        }

        public override string ÜbrigText
        {
            get { return String.Format("{0} {1}*", Übrig, PunkteArt); }
        }
    }

    public class EigenschaftProbenErgebnis : ProbenErgebnis
    {
        public EigenschaftProbenErgebnis() {}

        public EigenschaftProbenErgebnis(DreierProbenErgebnis ergebnis)
        {
            TalentProbenErgebnis = ergebnis;
        }

        public System.Windows.Visibility VisibilityGelungen
        {
            get
            {
                if (Gelungen && Gewürfelt != 0)
                    return System.Windows.Visibility.Visible;
                else
                    return System.Windows.Visibility.Collapsed;
            }
        }

        public System.Windows.Visibility VisibilityMisslungen
        {
            get
            {
                if (!Gelungen && Gewürfelt != 0)
                    return System.Windows.Visibility.Visible;
                else
                    return System.Windows.Visibility.Collapsed;
            }
        }

        public DreierProbenErgebnis TalentProbenErgebnis { get; set; }

        public uint Gewürfelt { get; set; }
        
        public bool Patzer
        {
            get 
            { 
                if (Regeln.EigenschaftenProbePatzerGlück)
                    return Gewürfelt == 20;
                return false;
            }
        }
        public bool Glückswurf
        {
            get 
            {
                if (Regeln.EigenschaftenProbePatzerGlück)
                    return Gewürfelt == 1;
                return false;
            }
        }
        public PatzerGlückswurf PatzerGlückswurf
        {
            get
            {
                return (PatzerGlückswurf)PatzerGlückswurfCount();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Kleiner 0: Patzer; größer 0: Glückswurf</returns>
        private int PatzerGlückswurfCount()
        {
            int patzerCount = Patzer ? 1 : 0;
            int glücksCount = Glückswurf ? 1 : 0;
            if (patzerCount >= 1)
                return patzerCount * -1;
            if (glücksCount >= 1)
                return glücksCount;
            return 0;
        }

        public override string ErfolgsChance
        {
            get
            {
                return String.Format("{0:P} Chance", ErfolgsChanceBerechnen(Wert - Mod));
            }
        }

        public double ErfolgsChanceBerechnen(int wert)
        {
            if (Regeln.EigenschaftenProbePatzerGlück)
            {
                if (wert <= 1)
                    return 0.05; // Glückswurf
                if (wert >= 20)
                    return 0.95;
                return (1d / 20d * (wert));
            }
            else
            {
                if (wert < 0)
                    return 0;
                if (wert > 20)
                    return 1;
                return (1d / 20d * (wert));
            }
        }

        public override string ToString()
        {
            string txt = String.Format("\t-> {0} übrig{1}",
                (Math.Abs(Übrig) > 9 ? Übrig.ToString("+#;-#;  0") : Übrig.ToString("+ #;- #;  0")),
                PatzerGlückswurf != PatzerGlückswurf.Normal ? " (" + PatzerGlückswurf + ")" : string.Empty);
            return txt;
        }

        public override string ÜbrigText
        {
            get { return String.Format("{0}", Übrig); }
        }
    }
}