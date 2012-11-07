using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeisterGeister.Model;
using MeisterGeister.Model.Extensions;
using MeisterGeister.ViewModel.Helden.Logic;

namespace MeisterGeister.Logic.General
{
    public class MetaTalent : Probe, IHatHeld
    {
        #region //---- PROBE ----

        override public int[] Werte
        {
            get
            {
                if (_werte == null || _werte.Length != 3)
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
                //_chanceBerechnet = false;
            }
        }

        override public string WerteNamen 
        { 
            get 
            {
                if (Talent != null)
                    return string.Format("({0}/{1}/{2})", Talent.Eigenschaft1, Talent.Eigenschaft2, Talent.Eigenschaft3);
                return string.Empty;
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

        public static List<Probe> GetMetaTalentListe(Talent talent, Held held = null)
        {
            List<Probe> talentListe = new List<Probe>();
            if (talent == null)
                return talentListe;

            if (talent.Talentname == "Kräuter Suchen" || talent.Talentname == "Nahrung Sammeln (Wildnis)")
            {
                AddTalent("Wildnisleben", talentListe, held);
                AddTalent("Sinnenschärfe", talentListe, held);
                AddTalent("Pflanzenkunde", talentListe, held);
            }
            else if (talent.Talentname == "Nahrung Sammeln (Agrarland)")
            {
                AddTalent("Wildnisleben", talentListe, held);
                AddTalent("Sinnenschärfe", talentListe, held);
                AddTalent("Ackerbau", talentListe, held);
            }
            else if (talent.Talentname.StartsWith("Pirschjagd"))
            {
                AddTalent("Wildnisleben", talentListe, held);
                AddTalent("Tierkunde", talentListe, held);
                AddTalent("Fährtensuchen", talentListe, held);
                AddTalent("Schleichen", talentListe, held);
                string waffe = talent.Talentname.Substring(12).TrimEnd(')');
                AddTalent(waffe, talentListe, held);
            }
            else if (talent.Talentname.StartsWith("Ansitzjagd"))
            {
                AddTalent("Wildnisleben", talentListe, held);
                AddTalent("Tierkunde", talentListe, held);
                AddTalent("Fährtensuchen", talentListe, held);
                AddTalent("Sich Verstecken", talentListe, held);
                string waffe = talent.Talentname.Substring(12).TrimEnd(')');
                AddTalent(waffe, talentListe, held);
            }
            return talentListe;
        }

        private static void AddTalent(string talent, List<Probe> talentListe, Held held = null)
        {
            if (held != null)
            {
                IEnumerable<Held_Talent> ht;
                ht = held.Held_Talent.Where(t => t.Talent.Talentname == talent);
                talentListe.Add(ht.Count() > 0 ? ht.First() : null);
            }
            else
            {
                IEnumerable<Talent> ht;
                ht = Global.ContextHeld.Liste<Talent>().Where(t => t.Talentname == talent);
                talentListe.Add(ht.Count() > 0 ? ht.First() : null);
            }
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
