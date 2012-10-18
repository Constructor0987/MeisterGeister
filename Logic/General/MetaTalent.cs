using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeisterGeister.Model;
using MeisterGeister.Model.Extensions;

namespace MeisterGeister.Logic.General
{
    public class MetaTalent : Probe
    {
        #region //---- PROBE ----

        override public int[] Werte
        {
            get
            {
                if (_werte == null)
                    _werte = new int[3];
                if (Held != null && Talent != null)
                {
                    _werte[0] = Held.EigenschaftWert(Talent.Eigenschaft1);
                    _werte[1] = Held.EigenschaftWert(Talent.Eigenschaft2);
                    _werte[2] = Held.EigenschaftWert(Talent.Eigenschaft3);
                }
                return _werte;
            }
            set
            {
                _werte = value;
                _chanceBerechnet = false;
            }
        }

        #endregion //---- PROBE ----

        public int? TaW { get; set; }

        public bool Aktiviert
        {
            get { return TaW.HasValue; }
        }

        public Held Held { get; set; }

        public Talent Talent { get; set; }

        public List<Probe> MetaTalentListe { get; set; }

        public MetaTalent(Talent talent, Held held)
        {
            Talent = talent;
            Probenname = talent.Talentname;
            Held = held;
            MetaTalentListe = GetMetaTalentListe(Talent, Held);

            TaW = MetaTalentTaW(MetaTalentListe);
            Fertigkeitswert = TaW.GetValueOrDefault();
        }

        public static List<Probe> GetMetaTalentListe(Talent talent, Held held)
        {
            List<Probe> talentListe = new List<Probe>();

            if (talent.Talentname == "Kräuter Suchen" || talent.Talentname == "Nahrung Sammeln (Wildnis)")
            {
                AddTalent("Wildnisleben", held, talentListe);
                AddTalent("Sinnenschärfe", held, talentListe);
                AddTalent("Pflanzenkunde", held, talentListe);
            }
            else if (talent.Talentname == "Nahrung Sammeln (Agrarland)")
            {
                AddTalent("Wildnisleben", held, talentListe);
                AddTalent("Sinnenschärfe", held, talentListe);
                AddTalent("Ackerbau", held, talentListe);
            }
            else if (talent.Talentname.StartsWith("Pirschjagd"))
            {
                AddTalent("Wildnisleben", held, talentListe);
                AddTalent("Tierkunde", held, talentListe);
                AddTalent("Fährtensuchen", held, talentListe);
                AddTalent("Schleichen", held, talentListe);
                string waffe = talent.Talentname.Substring(12).TrimEnd(')');
                AddTalent(waffe, held, talentListe);
            }
            else if (talent.Talentname.StartsWith("Ansitzjagd"))
            {
                AddTalent("Wildnisleben", held, talentListe);
                AddTalent("Tierkunde", held, talentListe);
                AddTalent("Fährtensuchen", held, talentListe);
                AddTalent("Sich Verstecken", held, talentListe);
                string waffe = talent.Talentname.Substring(12).TrimEnd(')');
                AddTalent(waffe, held, talentListe);
            }
            return talentListe;
        }

        private static void AddTalent(string talent, Held held, List<Probe> talentListe)
        {
            IEnumerable<Held_Talent> ht;
            ht = held.Held_Talent.Where(t => t.Talent.Talentname == talent);
            talentListe.Add(ht.Count() > 0 ? ht.First() : null);
        }

        /// <summary>
        /// Berechnet den Meta-TaW aus einer Proben-Liste.
        /// </summary>
        /// <param name="proben">Die Einzelproben, aus denen sich das Meta-Talent zusammensetzt.</param>
        /// <returns>Der TaW oder 'null', falls es nicht aktivierte Einzelproben gibt.</returns>
        public static int? MetaTalentTaW(List<Probe> proben)
        {
            if (proben.Where(p => p == null).Count() > 0)
                return null; // es gibt nicht aktivierte Talente

            int wert = 0;
            int tawTemp;
            int max = proben.Where(p => p != null).Max(p => p.Fertigkeitswert); // maximaler Fertigkeitswert
            foreach (Probe p in proben)
            {
                tawTemp = p.Fertigkeitswert;
                wert += tawTemp;
                max = Math.Min(tawTemp, max);
            }
            wert = (int)Math.Round(wert / (double)proben.Count, 0, MidpointRounding.AwayFromZero);
            wert = Math.Min(max * 2, wert);

            return wert;
        }


    }
}
