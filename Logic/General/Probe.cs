using System;

namespace MeisterGeister.Logic.General.INACTIVE
{
    //Eigenschaften mit taw und modifikator
    public class Probe
    {
        int[] werte;
        public int[] Werte
        {
            get { return werte; }
            set { 
                werte = value;
                chanceBerechnet = false;
            }
        }

        int modifikator;
        public int Modifikator
        {
            get { return modifikator; }
            set { 
                modifikator = value;
                chanceBerechnet = false;
            }
        }

        int taW;
        public int TaW
        {
            get { return taW; }
            set { 
                taW = value;
                chanceBerechnet = false;
            }
        }

        public double Erfolgsschance {
            get
            {
                if (!chanceBerechnet)
                    ErfolgsChanceBerechnen();
                return erfolgsschance;
            }
        }
        public double Erwartungswert
        {
            get
            {
                if (!chanceBerechnet)
                    ErfolgsChanceBerechnen();
                return erwartungswert;
            }
        }
        private ProbenErgebnis ergebnis = null;
        ProbenErgebnis Ergebnis {
            get
            {
                if (ergebnis == null)
                    ergebnis = Würfeln();
                return ergebnis;
            }
            set
            {
                ergebnis = value;
            }
        }

        public ProbenErgebnis Würfeln()
        {
            ProbenErgebnis pe = new ProbenErgebnis();
            pe.Würfe = new int[Werte.Length];
            pe.Übrig = TaW;
            int einsen = 0, zwanzigen = 0;
            for (int i = 0; i < Werte.Length; i++)
            {
                pe.Würfe[i] = Würfel.Wurf(20);
                pe.Übrig -= Math.Max(0, pe.Würfe[i] - Werte[i]);
                if (pe.Würfe[i] == 1)
                    einsen++;
                else if (pe.Würfe[i] == 20)
                    zwanzigen++;
            }
            if (einsen >= Werte.Length)
                pe.Ergebnis = ErgebnisTyp.MEISTERHAFT;
            else if (einsen >= Werte.Length / 2.0)
                pe.Ergebnis = ErgebnisTyp.GLÜCKLICH;
            else if (zwanzigen >= Werte.Length)
                pe.Ergebnis = ErgebnisTyp.FATALER_PATZER;
            else if (zwanzigen >= Werte.Length / 2.0)
                pe.Ergebnis = ErgebnisTyp.PATZER;
            else if (pe.Übrig >= 0)
                pe.Ergebnis = ErgebnisTyp.GELUNGEN;
            return pe;
        }

        private double erfolgsschance = 0;
        private double erwartungswert = 0;
        private bool chanceBerechnet = false;

        private double ErfolgsChanceBerechnen()
        {
            if(Werte.Length==3)
                return ErfolgsChanceBerechnen(Werte[0], Werte[1], Werte[2], TaW - Modifikator);
            chanceBerechnet = true;
            if (Werte.Length == 1)
            {
                //nicht optimal. 1 und 20 nicht betrachetet.
                erwartungswert = Werte[0] - 10.5 + TaW - Modifikator;
                return erfolgsschance = Math.Min( (Werte[0]-(TaW - Modifikator))/20, 1.0);
            }
            return erfolgsschance = erwartungswert = 0;
        }

        private double ErfolgsChanceBerechnen(int e1, int e2, int e3, int taw)
        {
            int success, restTaP;
            double tapsum = 0;
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
                            tapsum += taw;
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
                                    tapsum += Math.Max(restTaP,1);
                                }
                            }
                        }
                    }
                }
            }
            erfolgsschance = (1d / 8000d * (success));
            erwartungswert = tapsum / success;
            chanceBerechnet = true;
            return erfolgsschance;
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
    }
    

    public class ProbenErgebnis
    {
        public bool Gelungen { get { return (Ergebnis & ErgebnisTyp.GELUNGEN) == ErgebnisTyp.GELUNGEN; } }
        public ErgebnisTyp Ergebnis { get; set; }
        public int Qualität { get; set; }
        public int Übrig { get; set; }
        public int[] Würfe { get; set; }
    }

    [Flags]
    public enum ErgebnisTyp
    {
        KEIN_ERGEBNIS = 0x0,
        MISSLUNGEN = 0x1,
        PATZER = 0x3,
        FATALER_PATZER = 0x7, //der heisst irgendwie anders
        GELUNGEN = 0x8,
        GLÜCKLICH = 0x18,
        MEISTERHAFT = 0x38
    }
}
