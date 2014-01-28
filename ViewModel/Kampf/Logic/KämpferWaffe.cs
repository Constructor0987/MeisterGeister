using MeisterGeister.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeisterGeister.ViewModel.Kampf.Logic
{
    public class KämpferNahkampfwaffe : Logic.INahkampfwaffe
    {
        private Held _held;
        private Waffe _waffe;
        private Held_Talent _talent;
        private GegnerBase_Angriff _gegner_angriff;

        public KämpferNahkampfwaffe(Held_Ausrüstung ha)
        {
            if (ha.Held == null || ha.Ausrüstung == null || ha.Ausrüstung.Waffe == null)
                throw new ArgumentNullException("Held_Ausrüstung enthält keinen Held oder keine Waffe.");
            _held = ha.Held;
            _waffe = ha.Ausrüstung.Waffe;
            _talent = _held.Held_Talent.Where(ht => ht.TalentGUID == ha.TalentGUID).FirstOrDefault();
        }

        public KämpferNahkampfwaffe(Held held, Waffe waffe, Held_Talent ht)
        {
            _held = held; _waffe = waffe; _talent = ht;
        }

        public KämpferNahkampfwaffe(GegnerBase_Angriff ga)
        {
            _gegner_angriff = ga;
        }

        //public Logic.IKämpfer Kämpfer
        //{
        //    get
        //    {
        //        if (_gegner_angriff != null)
        //            return _gegner_angriff.Gegner;
        //        return _held;
        //    }
        //}

        public Logic.Distanzklasse Distanzklasse
        {
            get
            {
                if (_gegner_angriff != null)
                    return _gegner_angriff.Distanzklasse;
                return _waffe.Distanzklasse;
            }
        }

        public string Name
        {
            get
            {
                if (_gegner_angriff != null)
                    return _gegner_angriff.Name;
                return _waffe.Name;
            }
        }

        public int TPWürfel
        {
            get
            {
                if (_gegner_angriff != null)
                    return _gegner_angriff.TPWürfel;
                return _waffe.TPWürfel;
            }
        }

        public int TPWürfelAnzahl
        {
            get
            {
                if (_gegner_angriff != null)
                    return _gegner_angriff.TPWürfelAnzahl;
                return _waffe.TPWürfelAnzahl;
            }
        }

        public int TPBonus
        {
            get
            {
                if (_gegner_angriff != null)
                    return _gegner_angriff.TPBonus;
                return _waffe.TPBonus;
            }
        }

        public int TPKKBonus
        {
            get
            {
                if (_gegner_angriff != null)
                    return _gegner_angriff.TPKKBonus;
                return _waffe.TPKKBonus(_held);
            }
        }

        public int AT
        {
            get
            {
                if (_gegner_angriff != null)
                    return _gegner_angriff.AT;
                int heldat = 0;
                if (_talent == null)
                    heldat = _held.AttackeBasis;
                else
                {
                    heldat = _talent.Attacke;
                    if (_held.HatSonderfertigkeitUndVoraussetzungen("Waffenspezialisierung", Name))
                    {
                        if (!_talent.IsZuteilbar)
                            heldat += 2;
                        else
                            heldat += 1;
                    }
                    //TODO Waffenmeister
                }
                return heldat;
            }
        }

        public int PA
        {
            get
            {
                if (_gegner_angriff != null)
                    return _gegner_angriff.PA;
                int heldpa = 0;
                if (_talent == null)
                    heldpa = _held.ParadeBasis;
                else
                {
                    heldpa = _talent.Parade;
                    if (_held.HatSonderfertigkeitUndVoraussetzungen("Waffenspezialisierung", Name))
                    {
                        if (_talent.IsZuteilbar)
                            heldpa += 1;
                    }
                    //TODO Waffenmeister
                }
                return heldpa;
            }
        }

    }

    public class KämpferFernkampfwaffe : Logic.IFernkampfwaffe
    {
        private Held _held;
        private Held_Talent _talent;
        private Model.Fernkampfwaffe _waffe;
        private GegnerBase_Angriff _gegner_angriff;

        public KämpferFernkampfwaffe(Held_Ausrüstung ha)
        {
            if (ha.Held == null || ha.Ausrüstung == null || ha.Ausrüstung.Fernkampfwaffe == null)
                throw new ArgumentNullException("Held_Ausrüstung enthält keinen Held oder keine Waffe.");
            _held = ha.Held;
            _waffe = ha.Ausrüstung.Fernkampfwaffe;
            _talent = _held.Held_Talent.Where(ht => ht.TalentGUID == ha.TalentGUID).FirstOrDefault();
        }

        public KämpferFernkampfwaffe(Held held, Model.Fernkampfwaffe waffe, Held_Talent ht)
        {
            _held = held; _waffe = waffe; _talent = ht;
        }

        public KämpferFernkampfwaffe(GegnerBase_Angriff ga)
        {
            _gegner_angriff = ga;
        }

        //public Logic.IKämpfer Kämpfer
        //{
        //    get
        //    {
        //        if (_gegner_angriff != null)
        //            return _gegner_angriff.Gegner;
        //        return _held;
        //    }
        //}

        public int? RWSehrNah
        {
            get
            {
                if (_gegner_angriff != null)
                    return _gegner_angriff.RWSehrNah;
                return _waffe.RWSehrNah;
            }
        }

        public int? RWNah
        {
            get
            {
                if (_gegner_angriff != null)
                    return _gegner_angriff.RWNah;
                return _waffe.RWNah;
            }
        }

        public int? RWMittel
        {
            get
            {
                if (_gegner_angriff != null)
                    return _gegner_angriff.RWMittel;
                return _waffe.RWMittel;
            }
        }

        public int? RWWeit
        {
            get
            {
                if (_gegner_angriff != null)
                    return _gegner_angriff.RWWeit;
                return _waffe.RWWeit;
            }
        }

        public int? RWSehrWeit
        {
            get
            {
                if (_gegner_angriff != null)
                    return _gegner_angriff.RWSehrWeit;
                return _waffe.RWSehrWeit;
            }
        }

        public int? TPSehrNah
        {
            get
            {
                if (_gegner_angriff != null)
                    return _gegner_angriff.TPSehrNah;
                return _waffe.TPSehrNah;
            }
        }

        public int? TPNah
        {
            get
            {
                if (_gegner_angriff != null)
                    return _gegner_angriff.TPNah;
                return _waffe.TPNah;
            }
        }

        public int? TPMittel
        {
            get
            {
                if (_gegner_angriff != null)
                    return _gegner_angriff.TPMittel;
                return _waffe.TPMittel;
            }
        }

        public int? TPWeit
        {
            get
            {
                if (_gegner_angriff != null)
                    return _gegner_angriff.TPWeit;
                return _waffe.TPWeit;
            }
        }

        public int? TPSehrWeit
        {
            get
            {
                if (_gegner_angriff != null)
                    return _gegner_angriff.TPSehrWeit;
                return _waffe.TPSehrWeit;
            }
        }

        public string Name
        {
            get
            {
                if (_gegner_angriff != null)
                    return _gegner_angriff.Name;
                return _waffe.Name;
            }
        }

        public int TPWürfel
        {
            get
            {
                if (_gegner_angriff != null)
                    return _gegner_angriff.TPWürfel;
                return _waffe.TPWürfel ?? 0;
            }
        }

        public int TPWürfelAnzahl
        {
            get
            {
                if (_gegner_angriff != null)
                    return _gegner_angriff.TPWürfelAnzahl;
                return _waffe.TPWürfelAnzahl ?? 0;
            }
        }

        public int TPBonus
        {
            get
            {
                if (_gegner_angriff != null)
                    return _gegner_angriff.TPBonus;
                return _waffe.TPBonus ?? 0;
            }
        }

        public int TPKKBonus
        {
            get
            {
                if (_gegner_angriff != null)
                    return _gegner_angriff.TPKKBonus;
                return _waffe.TPKKBonus(_held);
            }
        }

        public int AT
        {
            get
            {
                if (_gegner_angriff != null)
                    return _gegner_angriff.AT;
                int heldat = 0;
                if (_talent == null)
                    heldat = _held.FernkampfBasis;
                else
                {
                    heldat = _talent.Fernkampfwert;
                    if (_held.HatSonderfertigkeitUndVoraussetzungen("Waffenspezialisierung", Name))
                        heldat += 2;
                    //TODO Waffenmeister
                }
                return heldat;
            }
        }
    }

}
