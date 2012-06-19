using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeisterGeister.Model;
using MeisterGeister.ViewModel.Kampf.Logic;
using K = MeisterGeister.ViewModel.Kampf.Logic.Kampf;

namespace MeisterGeister.ViewModel.Kampf
{
    public class KampfViewModel : Base.ViewModelBase
    {
        private K _kampf;

        public K Kampf
        {
            get { return _kampf; }
            set { _kampf = value; }
        }

        #region Subklassen
        public class KämpferNahkampfwaffe : Logic.INahkampfwaffe
        {
            private Held _held;
            private Waffe _waffe;
            private Gegner_Angriff _gegner_angriff;

            public KämpferNahkampfwaffe(Held held, Waffe waffe)
            {
                _held = held; _waffe = waffe;
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
                    return 0;
                }
            }
        }

        public class KämpferFernkampfwaffe : Logic.IFernkampfwaffe
        {
            private Held _held;
            private Model.Fernkampfwaffe _waffe;
            private Gegner_Angriff _gegner_angriff;

            public KämpferFernkampfwaffe(Held held, Model.Fernkampfwaffe waffe)
            {
                _held = held; _waffe = waffe;
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
                    return 0;
                }
            }
        }
        #endregion

        //Command NeueKampfrunde
    }
}
