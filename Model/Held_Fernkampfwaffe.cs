using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeisterGeister.Model.Extensions;
using MeisterGeister.ViewModel.Kampf.Logic;

namespace MeisterGeister.Model
{
    public partial class Held_Fernkampfwaffe
    {
        public Held Held
        {
            get { return Held_Ausrüstung.Held; }
        }

        public Talent BestesTalent
        {
            get
            {
                return GetBestesTalent(Held, Fernkampfwaffe);
            }
        }

        public static Talent GetBestesTalent(Held h, Fernkampfwaffe w)
        {
            if (w == null)
                return null;
            if (h == null)
                return w.Talent.FirstOrDefault();
            else
            {
                Held_Talent ht = h.Kampftalente.Where(t => w.Talent.Contains(t.Talent)).OrderByDescending(t => t.TaW).FirstOrDefault();
                if (ht == null)
                    return null;
                else return ht.Talent;
            }
        }

        public Held_Talent BestesHeldTalent
        {
            get
            {
                return GetBestesHeldTalent(Held, Fernkampfwaffe);
            }
        }

        public static Held_Talent GetBestesHeldTalent(Held h, Fernkampfwaffe w)
        {
            if (w == null || h == null)
                return null;
            return h.Kampftalente.Where(t => w.Talent.Contains(t.Talent)).OrderByDescending(t => t.TaW).FirstOrDefault();
        }

        public List<Held_Talent> Kampftalente
        {
            get { return Held.Kampftalente.Where(t => Fernkampfwaffe.Talent.Contains(t.Talent)).OrderByDescending(t => t.TaW).ToList(); ; }
        }

        public List<KämpferFernkampfwaffe> KämpferWaffen
        {
            get
            {
                List<KämpferFernkampfwaffe> waffen = new List<KämpferFernkampfwaffe>();
                foreach (var talent in Kampftalente)
                    waffen.Add(new KämpferFernkampfwaffe(Held, Fernkampfwaffe, talent));
                return waffen;
            }
        }
    }
}
