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

        public KämpferNahkampfwaffe(Held_Ausrüstung ha, bool bestesTalent = false)
        {
            if (ha == null)
                throw new ArgumentNullException("ha darf nicht null sein.");
            if (ha.Held == null || ha.Ausrüstung == null || ha.Ausrüstung.Waffe == null)
                throw new ArgumentNullException("Held_Ausrüstung enthält keinen Held oder keine Waffe.");
            _held = ha.Held;
            _waffe = ha.Ausrüstung.Waffe;
            if (bestesTalent)
                _talent = ha.Held_BFAusrüstung.Held_Waffe.BestesHeldTalent;
            else
                _talent = _held.Held_Talent.Where(ht => ht.TalentGUID == ha.Held_BFAusrüstung.Held_Waffe.TalentGUID).FirstOrDefault();
        }

        public KämpferNahkampfwaffe(Held held, Waffe waffe, Held_Talent ht)
        {
            if (held == null)
                throw new ArgumentNullException("held darf nicht null sein.");
            if (waffe == null)
                throw new ArgumentNullException("waffe darf nicht null sein.");
            if (ht == null)
                throw new ArgumentNullException("ht darf nicht null sein.");
            _held = held; _waffe = waffe; _talent = ht;
        }

        public KämpferNahkampfwaffe(Gegner_Angriff ga)
        {
            if (ga == null)
                throw new ArgumentNullException("ga darf nicht null sein.");
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

        public int? INI
        {
            get
            {
                if (_gegner_angriff != null)
                    return 0;
                return _waffe.INI;
            }
        }

        public string WMString
        {
            get
            {
                if (_gegner_angriff != null)
                    return "-";
                return string.Format("{0}/{1}", _waffe.WMAT, _waffe.WMPA);
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

        public string Bemerkung
        {
            get
            {
                if (_gegner_angriff != null)
                    return _gegner_angriff.Base_Angriff.Bemerkung;
                return _waffe.Bemerkung;
            }
        }

        public string TPStringOhneKK
        {
            get
            {
                return String.Format("{0}W{1}", TPWürfelAnzahl, TPWürfel)
                    + ((TPBonus != 0) ? ((TPBonus > 0) ? "+" : "-") + TPBonus.ToString() : String.Empty);
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

        public string TPKKString
        {
            get
            {
                if (_gegner_angriff != null)
                    return "-";
                return string.Format("{0}/{1}", _waffe.TPKKSchwelle, _waffe.TPKKSchritt);
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
                        heldat += _waffe.WMAT ?? 0;
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
                        heldpa += _waffe.WMPA ?? 0;
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
                    at = _talent.AttackeOhneMod + (_waffe != null ? _waffe.WMAT ?? 0 : 0);
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
                    pa = _talent.ParadeOhneMod + (_waffe != null ? _waffe.WMPA ?? 0 : 0);
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

        public KämpferFernkampfwaffe(Held_Ausrüstung ha, bool bestesTalent = false)
        {
            if (ha.Held == null || ha.Ausrüstung == null || ha.Ausrüstung.Fernkampfwaffe == null)
                throw new ArgumentNullException("Held_Ausrüstung enthält keinen Held oder keine Waffe.");
            _held = ha.Held;
            _waffe = ha.Ausrüstung.Fernkampfwaffe;
            if (bestesTalent)
                _talent = ha.Held_Fernkampfwaffe.BestesHeldTalent;
            else
                _talent = _held.Held_Talent.Where(ht => ht.TalentGUID == ha.Held_Fernkampfwaffe.TalentGUID).FirstOrDefault();
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

    public class KämpferSchild
    {
        private Held _held;
        private Schild _schild;

        public KämpferSchild(Held_Ausrüstung ha)
        {
            if (ha.Held == null || ha.Ausrüstung == null || ha.Ausrüstung.Schild == null)
                throw new ArgumentNullException("Held_Ausrüstung enthält keinen Held oder keinen Schild.");
            _held = ha.Held;
            _schild = ha.Ausrüstung.Schild;
        }

        public KämpferSchild(Held held, Schild schild)
        {
            if(held == null || schild == null)
                throw new ArgumentNullException("Held oder Schild ist null.");
            _held = held; _schild = schild;
        }

        public int INI
        {
            get
            {
                //TODO Kampfstil finden - aber wie? - Kampfstil muss im Kämpfer liegen
                //Wenn der Kampfstil PW oder Schild ist und der Schild eine Waffe ist und in der Haupthand geführt wird, dann sollte hier 0 zurückgegeben werden, weil er dann als waffe geführt wird.
                // alternativ gar nicht erst als schild erstellen - finde ich noch besser
                //return 0;
                return _schild.INI;
            }
        }

        public string WMString
        {
            get
            {
                return string.Format("{0}/{1}", _schild.WMAT, _schild.WMPA);
            }
        }

        public string Name
        {
            get
            {
                return _schild.Name;
            }
        }

        public string Bemerkung
        {
            get
            {
                return _schild.Bemerkung;
            }
        }

        public int ATMod
        {
            get
            {
                return _schild.WMAT;
            }
        }

        public int PAMod
        {
            get
            {
                var heldpa = _schild.WMPA;
                //TODO Schildkampf-SFs / Parierwaffen-SFs
                return heldpa;
            }
        }

        public int PA
        {
            get
            {
                var pa = 0;
                //TODO hauptwaffe finden - aber wie? - Kampfstil muss im Kämpfer liegen
                var hauptwaffepa = 0;
                //Schild
                //TODO Kampfstil auswerten - aber wie? // && Kampstil == Schildkampf
                if (_schild.Typ.Contains("S"))
                {
                    //PA-Basis + Schild-WM + LH/SK I/SK II (1/3/5) + (hauptwaffe-PA-14)
                    pa = _held.ParadeBasis;
                    if (_held.HatSonderfertigkeitUndVoraussetzungen("Schildkampf II"))
                        pa += 5;
                    else if (_held.HatSonderfertigkeitUndVoraussetzungen("Schildkampf I"))
                        pa += 3;
                    else if (_held.HatSonderfertigkeitUndVoraussetzungen("Linkhand"))
                        pa += 1;
                    if (hauptwaffepa >= 21)
                        pa += 3;
                    else if (hauptwaffepa >= 18)
                        pa += 2;
                    else if (hauptwaffepa >= 15)
                        pa += 1;
                }
                //Parierwaffe
                else if (_schild.Typ.Contains("P"))  // && Kampstil == Parierwaffe
                {
                    pa = hauptwaffepa;
                    if (_held.HatSonderfertigkeitUndVoraussetzungen("Parierwaffen II"))
                        pa += 2;
                    else if (_held.HatSonderfertigkeitUndVoraussetzungen("Parierwaffen I"))
                        pa += -1;
                    else if (_held.HatSonderfertigkeitUndVoraussetzungen("Linkhand"))
                        pa += -4;
                }
                pa += PAMod;
                return pa;
            }
        }
    }


}
