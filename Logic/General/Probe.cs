﻿using System;
using System.Runtime.Serialization;
using System.Linq;
using System.Collections.Generic;
using MeisterGeister.ViewModel.Helden.Logic;

namespace MeisterGeister.Logic.General
{
    //Eigenschaften mit taw und modifikator
    [DataContract(IsReference = true)]
    public class Probe
    {
        virtual public string Probenname { get; set; }

        protected int[] _werte = new int[0];
        virtual public int[] Werte
        {
            get { return _werte; }
            set 
            { 
                _werte = value;
                //_chanceBerechnet = false;
            }
        }

        private int _modifikator = 0;
        public int Modifikator
        {
            get { return _modifikator; }
            set { 
                _modifikator = value;
                //_chanceBerechnet = false;
            }
        }

        public int ModifikatorProben
        {
            get 
            {
                if (this is IHatHeld && (this as IHatHeld).Held != null)
                    return (this as IHatHeld).Held.GetModifikatorProben(this);
                return 0; 
            }
        }

        private bool _isBehinderung = true;
        /// <summary>
        /// Behinderung bei Probe berücksichtigen oder nicht.
        /// </summary>
        public bool IsBehinderung
        {
            get { return _isBehinderung; }
            set
            {
                _isBehinderung = value;
                //_chanceBerechnet = false;
            }
        }

        public int BehinderungEff
        {
            get
            {
                if (IsBehinderung && this is Model.Held_Talent)
                    return (this as Model.Held_Talent).BerechneEffBehinderung();
                return 0;
            }
        }

        private int _fertigkeitswert = 0;
        virtual public int Fertigkeitswert
        {
            get { return _fertigkeitswert; }
            set { 
                _fertigkeitswert = value;
                //_chanceBerechnet = false;
            }
        }

        public string WertText
        {
            get
            {
                if (this is Model.Talent || this is MetaTalent
                    || this is Model.Held_Talent)
                    return "TaW";
                else if (this is Model.Zauber
                    || this is Model.Held_Zauber)
                    return "ZfW";
                return "Wert";
            }
        }

        public string PunkteText
        {
            get
            {
                if (this is Model.Talent || this is MetaTalent
                    || this is Model.Held_Talent)
                    return "TaP";
                else if (this is Model.Zauber
                    || this is Model.Held_Zauber)
                    return "ZfP";
                return "Punkte";
            }
        }

        virtual public string WerteNamen { get { return string.Empty; } }

        public double Erfolgschance {
            get
            {
                //if (!_chanceBerechnet)
                    ErfolgsChanceBerechnen();
                return _erfolgsschance;
            }
        }
        public double Erwartungswert
        {
            get
            {
                //if (!_chanceBerechnet)
                    ErfolgsChanceBerechnen();
                return _erwartungswert;
            }
        }
        private ProbenErgebnis _ergebnis = null;
        public ProbenErgebnis Ergebnis {
            get
            {
                if (_ergebnis == null)
                    _ergebnis = Würfeln();
                return _ergebnis;
            }
            set
            {
                _ergebnis = value;
            }
        }

        public ProbenErgebnis Würfeln()
        {
            ProbenErgebnis pe = new ProbenErgebnis();
            pe.Würfe = new int[Werte.Length];

            for (int i = 0; i < Werte.Length; i++)
                pe.Würfe[i] = Würfel.Wurf(20);

            ProbenErgebnisBerechnen(pe);

            Ergebnis = pe; // Speichert das Probenergebnis
            return pe;
        }

        /// <summary>
        /// Berechnet das Probenergebnis auf Basis der Würfe in 'pe'.
        /// </summary>
        /// <param name="pe">Das Proben-Ergebnis, auf dessen Basis das Ergebnis berechnet werden soll.</param>
        /// <returns>Das berechnete Proben-Ergebnis.</returns>
        public ProbenErgebnis ProbenErgebnisBerechnen(ProbenErgebnis pe)
        {
            // Modifikatoren verändern den effektiven Fertigkeitswert
            // Modifikator > 0 : Erschwernis
            // Modifikator < 0 : Erleichterung
            if (Werte.Length == 0)
                return pe;

            int fertigkeitswertEff = Fertigkeitswert - (Modifikator + ModifikatorProben + BehinderungEff);
            pe.Übrig = fertigkeitswertEff;
            pe.Qualität = fertigkeitswertEff;
            if (pe.Würfe == null)
                pe.Würfe = new int[Werte.Length];

            int einsen = 0, zwanzigen = 0;
            int[] wurfQualität = new int[Werte.Length];
            for (int i = 0; i < Werte.Length; i++)
            {
                wurfQualität[i] = Werte[i] + Math.Min(fertigkeitswertEff, 0) - pe.Würfe[i];
                if (pe.Würfe[i] == 1)
                    einsen++;
                else if (pe.Würfe[i] == 20)
                    zwanzigen++;
            }
            int tmpÜbrig = fertigkeitswertEff;
            wurfQualität.Where(q => q < 0).ToList().ForEach(q => tmpÜbrig += q);
            int minQ = wurfQualität.Count() == 0 ? 0 : wurfQualität.Min();
            bool erfolg = tmpÜbrig >= 0 || minQ >= 0;
            tmpÜbrig = Math.Max(0, tmpÜbrig);
            if (erfolg)
            {
                if(minQ<0)
                    minQ = 0;
                pe.Qualität = tmpÜbrig + minQ;
                tmpÜbrig = Math.Max(1, Math.Min(Fertigkeitswert, tmpÜbrig));
            }
            else
                pe.Qualität = tmpÜbrig + wurfQualität.Min();
            pe.Übrig = tmpÜbrig;

            if (einsen >= Werte.Length)
            {
                pe.Ergebnis = ErgebnisTyp.MEISTERHAFT;
                pe.Übrig = Math.Max(Math.Min(Fertigkeitswert, fertigkeitswertEff), 1);  // Alle Punkte übrig
            }
            else if (einsen >= Werte.Length / 2.0)
            {
                pe.Ergebnis = ErgebnisTyp.GLÜCKLICH;
                pe.Übrig = Math.Max(Math.Min(Fertigkeitswert, fertigkeitswertEff), 1); // Alle Punkte übrig
            }
            else if (zwanzigen >= Werte.Length)
                pe.Ergebnis = ErgebnisTyp.FATALER_PATZER;
            else if (zwanzigen >= Werte.Length / 2.0)
                pe.Ergebnis = ErgebnisTyp.PATZER;
            else if (erfolg && pe.Würfe.Sum() > 0)
                pe.Ergebnis = ErgebnisTyp.GELUNGEN;
            else if (!erfolg)
                pe.Ergebnis = ErgebnisTyp.MISSLUNGEN;
            else
                pe.Ergebnis = ErgebnisTyp.KEIN_ERGEBNIS;

            // Sonderfall 1er Probe
            if (Werte.Length <= 1)
                pe.Übrig = pe.Qualität;

            // Spezielle Erfahrung
            if (einsen >= 2) // WdS 162
                SpezielleErfahrungSpeichern(einsen);

            return pe;
        }

        private void SpezielleErfahrungSpeichern(int einsen)
        {
            // Bei Doppel1 oder Dreifach1 eine SE (WdS 162)
            // TODO ??: Eventuell User per YesNo-Dialog fragen?
            string held = "Unbekannt";
            if (this is IHatHeld && (this as IHatHeld).Held != null)
                held = (this as IHatHeld).Held.Name;
            string seTxt = string.Format("{0} - {1} - Spezielle Erfahrung in {2} (TaW {3}) für {4} ({5} Einsen)",
                DateTime.Now.ToString("g"), MeisterGeister.Logic.Kalender.Datum.Aktuell.ToStringShort(),
                Probenname, Fertigkeitswert, held, einsen);

            if (Global.ContextNotizen != null)
                Global.ContextNotizen.NotizErfahrungen.AppendText("\n" + seTxt + "\n");

            if (SpezielleErfahrung != null)
                SpezielleErfahrung(seTxt, new EventArgs());
        }

        public static EventHandler SpezielleErfahrung;

        private double _erfolgsschance = 0;
        private double _erwartungswert = 0;
        //protected bool _chanceBerechnet = false;

        private double ErfolgsChanceBerechnen()
        {
            if(Werte.Length==3)
                return ErfolgsChanceBerechnen(Werte[0], Werte[1], Werte[2], Fertigkeitswert - (Modifikator + ModifikatorProben + BehinderungEff));
            if (Werte.Length == 1)
                return ErfolgsChancheBerechnen(Werte[0] - (Modifikator + ModifikatorProben + BehinderungEff));
            return _erfolgsschance = 0;
        }

        private double ErfolgsChancheBerechnen(int wertEff)
        {
            //_chanceBerechnet = true;

            _erwartungswert = wertEff - 10.5;

            if (Settings.Regeln.EigenschaftenProbePatzerGlück)
            {
                if (wertEff <= 1)
                    return _erfolgsschance = 0.05; // Glückswurf
                if (wertEff >= 20)
                    return _erfolgsschance = 0.95;
                return _erfolgsschance = (wertEff / 20d);
            }
            else
            {
                if (wertEff < 0)
                    return _erfolgsschance = 0;
                if (wertEff > 20)
                    return _erfolgsschance = 1;
                return _erfolgsschance = (wertEff / 20d);
            }
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
            _erfolgsschance = (1d / 8000d * (success));
            _erwartungswert = tapsum / success;
            //_chanceBerechnet = true;
            return _erfolgsschance;
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

        public static ProbenErgebnis KeinErgebnis
        {
            get { return new ProbenErgebnis() { Ergebnis = ErgebnisTyp.KEIN_ERGEBNIS, Qualität = 0, Übrig = 0, Würfe = new int[] { 0, 0, 0 } }; }
        }
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
