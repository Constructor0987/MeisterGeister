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
        private Gegner_Angriff _gegner_angriff;

        public KämpferNahkampfwaffe(Held_Ausrüstung ha)
        {
            if (ha.Held == null || ha.Ausrüstung == null || ha.Ausrüstung.Waffe == null)
                throw new ArgumentNullException("Held_Ausrüstung enthält keinen Held oder keine Waffe.");
            _held = ha.Held;
            _waffe = ha.Ausrüstung.Waffe;
            _talent = _held.Held_Talent.Where(ht => ht.TalentGUID == ha.TalentGUID).OrderByDescending(ht => ht.TaW).FirstOrDefault();
        }

        public KämpferNahkampfwaffe(Held held, Waffe waffe, Held_Talent ht)
        {
            _held = held; _waffe = waffe; _talent = ht;
        }

        public KämpferNahkampfwaffe(Gegner_Angriff ga)
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

        public Held_Talent Talent
        {
            get
            {
                return _talent;
            }
        }

        public Logic.Distanzklasse Distanzklasse
        {
            get
            {
                if (_gegner_angriff != null)
                    return _gegner_angriff.Base_Angriff.Distanzklasse;
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

        public string TPString
        {
            get
            {
                int tpBonus = TPBonusMitKK;
                return String.Format("{0}W{1}", TPWürfelAnzahl, TPWürfel)
                    + ((tpBonus != 0) ? ((tpBonus > 0) ? "+" : "-") + tpBonus.ToString() : String.Empty);
            }
        }

        public int TPWürfel
        {
            get
            {
                if (_gegner_angriff != null)
                    return _gegner_angriff.Base_Angriff.TPWürfel;
                return _waffe.TPWürfel;
            }
        }

        public int TPWürfelAnzahl
        {
            get
            {
                if (_gegner_angriff != null)
                    return _gegner_angriff.Base_Angriff.TPWürfelAnzahl;
                return _waffe.TPWürfelAnzahl;
            }
        }

        public int TPBonus
        {
            get
            {
                if (_gegner_angriff != null)
                    return _gegner_angriff.Base_Angriff.TPBonus;
                return _waffe.TPBonus;
            }
        }

        public int TPKKBonus
        {
            get
            {
                if (_gegner_angriff != null)
                    return _gegner_angriff.Base_Angriff.TPKKBonus;
                return _waffe.TPKKBonus(_held);
            }
        }

        public int TPBonusMitKK
        {
            get
            {
                return TPBonus + TPKKBonus;
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
                    // Waffenmodifikator
                    if (_waffe != null)
                        heldat += _waffe.WMAT.Value;
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
                    // Waffenmodifikator
                    if (_waffe != null)
                        heldpa += _waffe.WMPA.Value;
                    //TODO Waffenmeister
                }
                return heldpa;
            }
        }

        public int AttackeOhneMod
        {
            get
            {
                int at = 0;
                if (_talent != null)
                {
                    at = _talent.AttackeOhneMod + (_waffe != null ? _waffe.WMAT.Value : 0);
                    if (_held != null && _held.HatSonderfertigkeitUndVoraussetzungen("Waffenspezialisierung", Name))
                    {
                        if (!_talent.IsZuteilbar)
                            at += 2;
                        else
                            at += 1;
                    }
                }
                else if (_gegner_angriff != null)
                    at = _gegner_angriff.AT;
                else
                    at = AT;
                return at;
            }
        }

        public int ParadeOhneMod
        {
            get
            {
                int pa = 0;
                if (_talent != null)
                {
                    pa = _talent.ParadeOhneMod + (_waffe != null ? _waffe.WMPA.Value : 0);
                    if (_held != null && _held.HatSonderfertigkeitUndVoraussetzungen("Waffenspezialisierung", Name))
                    {
                        if (_talent.IsZuteilbar)
                            pa += 1;
                    }
                }
                else if (_gegner_angriff != null)
                    pa = _gegner_angriff.PA;
                else
                    pa = PA;
                return pa;
            }
        }

        public List<dynamic> ModifikatorenListeAT
        {
            get
            {
                if (_held != null)
                    return _held.ModifikatorenListe(typeof(Modifikatoren.IModAT), AttackeOhneMod);
                else if (_gegner_angriff != null)
                    return _gegner_angriff.ModifikatorenListeAT;
                else
                    return new List<dynamic>();
            }
        }

        public List<dynamic> ModifikatorenListePA
        {
            get
            {
                if (_held != null)
                    return _held.ModifikatorenListe(typeof(Modifikatoren.IModPA), ParadeOhneMod);
                else if (_gegner_angriff != null)
                    return _gegner_angriff.ModifikatorenListePA;
                else
                    return new List<dynamic>();
            }
        }

    }

    public class KämpferFernkampfwaffe : Logic.IFernkampfwaffe
    {
        private Held _held;
        private Held_Talent _talent;
        private Model.Fernkampfwaffe _waffe;
        private Gegner_Angriff _gegner_angriff;

        public KämpferFernkampfwaffe(Held_Ausrüstung ha)
        {
            if (ha.Held == null || ha.Ausrüstung == null || ha.Ausrüstung.Fernkampfwaffe == null)
                throw new ArgumentNullException("Held_Ausrüstung enthält keinen Held oder keine Waffe.");
            _held = ha.Held;
            _waffe = ha.Ausrüstung.Fernkampfwaffe;
            _talent = _held.Held_Talent.Where(ht => ht.TalentGUID == ha.TalentGUID).OrderByDescending(ht => ht.TaW).FirstOrDefault();
        }

        public KämpferFernkampfwaffe(Held held, Model.Fernkampfwaffe waffe, Held_Talent ht)
        {
            _held = held; _waffe = waffe; _talent = ht;
        }

        public KämpferFernkampfwaffe(Gegner_Angriff ga)
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

        public Held_Talent Talent
        {
            get
            {
                return _talent;
            }
        }

        public string Reichweiten
        {
            get
            {
                return ((RWSehrNah == null) ? "-" : RWSehrNah.Value.ToString()) + "/" + ((RWNah == null) ? "-" : RWNah.Value.ToString()) + "/" + ((RWMittel == null) ? "-" : RWMittel.Value.ToString()) + "/" + ((RWWeit == null) ? "-" : RWWeit.Value.ToString()) + "/" + ((RWSehrWeit == null) ? "-" : RWSehrWeit.Value.ToString());
            }
        }

        public int? RWSehrNah
        {
            get
            {
                if (_gegner_angriff != null)
                    return _gegner_angriff.Base_Angriff.RWSehrNah;
                return _waffe.RWSehrNah;
            }
        }

        public int? RWNah
        {
            get
            {
                if (_gegner_angriff != null)
                    return _gegner_angriff.Base_Angriff.RWNah;
                return _waffe.RWNah;
            }
        }

        public int? RWMittel
        {
            get
            {
                if (_gegner_angriff != null)
                    return _gegner_angriff.Base_Angriff.RWMittel;
                return _waffe.RWMittel;
            }
        }

        public int? RWWeit
        {
            get
            {
                if (_gegner_angriff != null)
                    return _gegner_angriff.Base_Angriff.RWWeit;
                return _waffe.RWWeit;
            }
        }

        public int? RWSehrWeit
        {
            get
            {
                if (_gegner_angriff != null)
                    return _gegner_angriff.Base_Angriff.RWSehrWeit;
                return _waffe.RWSehrWeit;
            }
        }

        public string TPString
        {
            get
            {
                int tpBonus = TPBonusMitKK;
                return String.Format("{0}W{1}", TPWürfelAnzahl, TPWürfel)
                    + ((tpBonus != 0) ? ((tpBonus > 0) ? "+" : "-") + tpBonus.ToString() : String.Empty)
                    + " (" + ((TPSehrNah == null) ? "-" : TPSehrNah.Value.ToString()) + "/" + ((TPNah == null) ? "-" : TPNah.Value.ToString()) + "/" + ((TPMittel == null) ? "-" : TPMittel.Value.ToString()) + "/" + ((TPWeit == null) ? "-" : TPWeit.Value.ToString()) + "/" + ((TPSehrWeit == null) ? "-" : TPSehrWeit.Value.ToString()) + ")";
            }
        }

        public int? TPSehrNah
        {
            get
            {
                if (_gegner_angriff != null)
                    return _gegner_angriff.Base_Angriff.TPSehrNah;
                return _waffe.TPSehrNah;
            }
        }

        public int? TPNah
        {
            get
            {
                if (_gegner_angriff != null)
                    return _gegner_angriff.Base_Angriff.TPNah;
                return _waffe.TPNah;
            }
        }

        public int? TPMittel
        {
            get
            {
                if (_gegner_angriff != null)
                    return _gegner_angriff.Base_Angriff.TPMittel;
                return _waffe.TPMittel;
            }
        }

        public int? TPWeit
        {
            get
            {
                if (_gegner_angriff != null)
                    return _gegner_angriff.Base_Angriff.TPWeit;
                return _waffe.TPWeit;
            }
        }

        public int? TPSehrWeit
        {
            get
            {
                if (_gegner_angriff != null)
                    return _gegner_angriff.Base_Angriff.TPSehrWeit;
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
                    return _gegner_angriff.Base_Angriff.TPWürfel;
                return _waffe.TPWürfel ?? 0;
            }
        }

        public int TPWürfelAnzahl
        {
            get
            {
                if (_gegner_angriff != null)
                    return _gegner_angriff.Base_Angriff.TPWürfelAnzahl;
                return _waffe.TPWürfelAnzahl ?? 0;
            }
        }

        public int TPBonusMitKK
        {
            get
            {
                return TPBonus + TPKKBonus;
            }
        }

        public int TPBonus
        {
            get
            {
                if (_gegner_angriff != null)
                    return _gegner_angriff.Base_Angriff.TPBonus;
                return _waffe.TPBonus ?? 0;
            }
        }

        public int TPKKBonus
        {
            get
            {
                if (_gegner_angriff != null)
                    return _gegner_angriff.Base_Angriff.TPKKBonus;
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

        public int FernkampfOhneMod
        {
            get
            {
                int at = 0;
                if (_talent != null)
                {
                    at = _talent.FernkampfwertOhneMod;
                    if (_held != null && _held.HatSonderfertigkeitUndVoraussetzungen("Waffenspezialisierung", Name))
                        at += 2;
                }
                else if (_gegner_angriff != null)
                    at = _gegner_angriff.AT;
                else
                    at = AT;
                return at;
            }
        }

        public List<dynamic> ModifikatorenListeFK
        {
            get
            {
                if (_held != null)
                    return _held.ModifikatorenListe(typeof(Modifikatoren.IModFK), FernkampfOhneMod);
                else if (_gegner_angriff != null)
                    return _gegner_angriff.ModifikatorenListeAT;
                else
                    return new List<dynamic>();
            }
        }
    }

}
