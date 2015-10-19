using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeisterGeister.Model.Extensions;
using MeisterGeister.ViewModel.Kampf.Logic;

namespace MeisterGeister.Model
{
    public partial class Held_Waffe
    {
        public Held Held
        {
            get { return Held_BFAusrüstung.Held_Ausrüstung.Held; }
        }

        public Talent BestesTalent
        {
            get {
                return GetBestesTalent(Held, Waffe);
            }
        }

        public static Talent GetBestesTalent(Held h, Waffe w)
        {
            if (w == null)
                return null;
            if (h == null)
                return w.Talent.FirstOrDefault();
            var ht = h.Kampftalente.Where(t => w.Talent != null && w.Talent.Contains(t.Talent)).OrderByDescending(t => t.TaW).FirstOrDefault();
            if (ht == null)
                return null;
            return ht.Talent;
        }

        public Held_Talent BestesHeldTalent
        {
            get
            {
                return GetBestesHeldTalent(Held, Waffe);
            }
        }

        public static Held_Talent GetBestesHeldTalent(Held h, Waffe w)
        {
            if (w == null || h == null)
                return null;
            return h.Kampftalente.Where(t => w.Talent != null && t.Talent != null && w.Talent.Contains(t.Talent)).OrderByDescending(t => t.TaW).FirstOrDefault();
        }

        public List<Held_Talent> Kampftalente
        {
            get { return Held.Kampftalente.Where(t => Waffe.Talent.Contains(t.Talent)).OrderByDescending(t => t.TaW).ToList(); ; }
        }

        public List<KämpferNahkampfwaffe> KämpferWaffen
        {
            get
            {
                List<KämpferNahkampfwaffe> waffen = new List<KämpferNahkampfwaffe>();
                foreach (var talent in Kampftalente)
                    waffen.Add(new KämpferNahkampfwaffe(Held, Waffe, talent));
                return waffen;
            }
        }
    }
}
