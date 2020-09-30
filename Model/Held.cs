using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

// Eigene Usings
using MeisterGeister.Logic.General;
using MeisterGeister.ViewModel.Basar.Logic;
using MeisterGeister.ViewModel.Helden.Logic;
using MeisterGeister.ViewModel.Inventar;
using MeisterGeister.ViewModel.Inventar.Logic;
using MeisterGeister.ViewModel.Kampf.Logic;
using DependentProperty = MeisterGeister.Model.Extensions.DependentProperty;
using E = MeisterGeister.Logic.Einstellung.Einstellungen;

using KampfLogic = MeisterGeister.ViewModel.Kampf.Logic;

using Mod = MeisterGeister.ViewModel.Kampf.Logic.Modifikatoren;

namespace MeisterGeister.Model
{
    // Man kann Superklassen hinzufügen. Es sollten jedoch nicht die gleichen Eigenschaften, wie in
    // der Datenbankklasse existieren.
    public partial class Held : Wesen, IKämpfer, Extensions.IInitializable, IHasZonenRs, IHasWunden, IDisposable
    {
        public Held()
            : base()
        {
            HeldGUID = Guid.NewGuid();
            UpdateHinweis = string.Empty;
            HinweisText = string.Empty;
            PropertyChanged += DependentProperty.PropagateINotifyProperyChanged;
            PropertyChanged += Held_PropertyChanged;
            SetDefaultValues();
        }

        private void Held_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "AktiveHeldengruppe" && Global.MainVM != null)
            {
                Held oldHeld = Global.SelectedHeld;
                Global.MainVM.HeldenGruppe.Refresh();
                Global.SelectedHeld = oldHeld;
            }
        }

        public bool keineWeiterenAuswirkungenBeiWunden
        {
            get { return _keineWeiterenAuswirkungenBeiWunden; }
            set { Set(ref _keineWeiterenAuswirkungenBeiWunden, value); }
        }

        private bool _keineWeiterenAuswirkungenBeiWunden = false;

        private void SetDefaultValues()
        {
            Name = "Alrik";
            AktiveHeldengruppe = true;
            MU = 8;
            KL = 8;
            IN = 8;
            CH = 8;
            FF = 8;
            GE = 8;
            KO = 8;
            KK = 8;
            LE_Aktuell = 12;
            AU_Aktuell = 12;
        }

        #region IInitializable

        public void Initialize()
        {
            if (_isInitialized)
            {
                return;
            }

            kämpferWunden = new Wunden(this);
            _isInitialized = true;
        }

        private bool _isInitialized = false;

        #endregion IInitializable

        #region Modifikatorlisten

        [DependsOnModifikator(typeof(Mod.IModMU))]
        public List<dynamic> ModifikatorenListeMU
        {
            get
            {
                return ModifikatorenListe(typeof(Mod.IModMU), MU);
            }
        }

        [DependsOnModifikator(typeof(Mod.IModKL))]
        public List<dynamic> ModifikatorenListeKL
        {
            get
            {
                return ModifikatorenListe(typeof(Mod.IModKL), KL);
            }
        }

        [DependsOnModifikator(typeof(Mod.IModCH))]
        public List<dynamic> ModifikatorenListeCH
        {
            get
            {
                return ModifikatorenListe(typeof(Mod.IModCH), CH);
            }
        }

        [DependsOnModifikator(typeof(Mod.IModIN))]
        public List<dynamic> ModifikatorenListeIN
        {
            get
            {
                return ModifikatorenListe(typeof(Mod.IModIN), IN);
            }
        }

        [DependsOnModifikator(typeof(Mod.IModFF))]
        public List<dynamic> ModifikatorenListeFF
        {
            get
            {
                return ModifikatorenListe(typeof(Mod.IModFF), FF);
            }
        }

        [DependsOnModifikator(typeof(Mod.IModGE))]
        public List<dynamic> ModifikatorenListeGE
        {
            get
            {
                return ModifikatorenListe(typeof(Mod.IModGE), GE);
            }
        }

        [DependsOnModifikator(typeof(Mod.IModKO))]
        public List<dynamic> ModifikatorenListeKO
        {
            get
            {
                return ModifikatorenListe(typeof(Mod.IModKO), KO);
            }
        }

        [DependsOnModifikator(typeof(Mod.IModKK))]
        public List<dynamic> ModifikatorenListeKK
        {
            get
            {
                return ModifikatorenListe(typeof(Mod.IModKK), KK);
            }
        }

        [DependsOnModifikator(typeof(Mod.IModINIBasis))]
        public List<dynamic> ModifikatorenListeINIbasis
        {
            get
            {
                return ModifikatorenListe(typeof(Mod.IModINIBasis), InitiativeBasisOhneMod);
            }
        }

        [DependsOnModifikator(typeof(Mod.IModATBasis))]
        public List<dynamic> ModifikatorenListeATbasis
        {
            get
            {
                return ModifikatorenListe(typeof(Mod.IModATBasis), AttackeBasisOhneMod);
            }
        }

        [DependsOnModifikator(typeof(Mod.IModPABasis))]
        public List<dynamic> ModifikatorenListePAbasis
        {
            get
            {
                return ModifikatorenListe(typeof(Mod.IModPABasis), ParadeBasisOhneMod);
            }
        }

        [DependsOnModifikator(typeof(Mod.IModAT))]
        public List<dynamic> ModifikatorenListeAT
        {
            get
            {
                List<dynamic> list = ModifikatorenListe(typeof(Mod.IModATBasis), AttackeBasisOhneMod);
                list.AddRange(ModifikatorenListe(typeof(Mod.IModAT), list.Count() == 0 ? AttackeBasisOhneMod : list.LastOrDefault().Wert));
                return list;
            }
        }

        [DependsOnModifikator(typeof(Mod.IModPA))]
        public List<dynamic> ModifikatorenListePA
        {
            get
            {
                List<dynamic> list = ModifikatorenListe(typeof(Mod.IModPABasis), ParadeBasisOhneMod);
                list.AddRange(ModifikatorenListe(typeof(Mod.IModPA), list.Count() == 0 ? ParadeBasisOhneMod : list.LastOrDefault().Wert));
                return list;
            }
        }

        [DependsOnModifikator(typeof(Mod.IModFK))]
        public List<dynamic> ModifikatorenListeFK
        {
            get
            {
                List<dynamic> list = ModifikatorenListe(typeof(Mod.IModFKBasis), FernkampfBasisOhneMod);
                list.AddRange(ModifikatorenListe(typeof(Mod.IModFK), list.Count() == 0 ? FernkampfBasisOhneMod : list.LastOrDefault().Wert));
                return list;
            }
        }

        [DependsOnModifikator(typeof(Mod.IModFKBasis))]
        public List<dynamic> ModifikatorenListeFKbasis
        {
            get
            {
                return ModifikatorenListe(typeof(Mod.IModFKBasis), FernkampfBasisOhneMod);
            }
        }

        [DependsOnModifikator(typeof(Mod.IModMR))]
        public List<dynamic> ModifikatorenListeMR
        {
            get
            {
                return ModifikatorenListe(typeof(Mod.IModMR), Magieresistenz);
            }
        }

        [DependsOnModifikator(typeof(Mod.IModGS))]
        [DependentProperty(nameof(Behinderung))]
        public List<dynamic> ModifikatorenListeGS
        {
            get
            {
                List<dynamic> li = ModifikatorenListe(typeof(Mod.IModGS), (double)GeschwindigkeitOhneMod);
                if (Behinderung > 0)
                {
                    li.AddRange(ModifikatorenListe(typeof(Mod.IModBE), li.Count == 0 ?
                        GeschwindigkeitOhneMod :
                        (int)li.LastOrDefault().Wert, new List<Mod.IModifikator>() { new Mod.BehinderungModifikator(Behinderung) }));
                }

                return li;
            }
        }

        [DependsOnModifikator(typeof(Mod.IModAusweichen))]
        [DependentProperty(nameof(Behinderung))]
        public List<dynamic> ModifikatorenListeAusweichen
        {
            get
            {
                List<dynamic> li = ModifikatorenListe(typeof(Mod.IModAusweichen), AusweichenOhneMod);
                if (Behinderung > 0)
                {
                    li.AddRange(ModifikatorenListe(typeof(Mod.IModBE), li.Count == 0 ?
                        AusweichenOhneMod :
                        (int)li.LastOrDefault().Wert, new List<Mod.IModifikator>() { new Mod.BehinderungModifikator(Behinderung) }));
                }

                return li;
            }
        }

        #endregion Modifikatorlisten

        #region Eigenschaften

        [DependentProperty(nameof(MU))]
        [DependsOnModifikator(typeof(Mod.IModMU))]
        public int Mut
        {
            get
            {
                var mu = MU ?? 8;
                if (Modifikatoren != null)
                {
                    Modifikatoren.Where(m => m is Mod.IModMU).Select(m => (Mod.IModMU)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => mu = m.ApplyMUMod(mu));
                }

                return mu;
            }
        }

        [DependentProperty(nameof(KL))]
        [DependsOnModifikator(typeof(Mod.IModKL))]
        public int Klugheit
        {
            get
            {
                var e = KL ?? 8;
                if (Modifikatoren != null)
                {
                    Modifikatoren.Where(m => m is Mod.IModKL).Select(m => (Mod.IModKL)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => e = m.ApplyKLMod(e));
                }

                return e;
            }
        }

        [DependentProperty(nameof(IN))]
        [DependsOnModifikator(typeof(Mod.IModIN))]
        public int Intuition
        {
            get
            {
                var e = IN ?? 8;
                if (Modifikatoren != null)
                {
                    Modifikatoren.Where(m => m is Mod.IModIN).Select(m => (Mod.IModIN)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => e = m.ApplyINMod(e));
                }

                return e;
            }
        }

        [DependentProperty(nameof(CH))]
        [DependsOnModifikator(typeof(Mod.IModCH))]
        public int Charisma
        {
            get
            {
                var e = CH ?? 8;
                if (Modifikatoren != null)
                {
                    Modifikatoren.Where(m => m is Mod.IModCH).Select(m => (Mod.IModCH)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => e = m.ApplyCHMod(e));
                }

                return e;
            }
        }

        [DependentProperty(nameof(FF))]
        [DependsOnModifikator(typeof(Mod.IModFF))]
        public int Fingerfertigkeit
        {
            get
            {
                var e = FF ?? 8;
                if (Modifikatoren != null)
                {
                    Modifikatoren.Where(m => m is Mod.IModFF).Select(m => (Mod.IModFF)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => e = m.ApplyFFMod(e));
                }

                return e;
            }
        }

        [DependentProperty(nameof(GE))]
        [DependsOnModifikator(typeof(Mod.IModGE))]
        public int Gewandtheit
        {
            get
            {
                var e = GE ?? 8;
                if (Modifikatoren != null)
                {
                    Modifikatoren.Where(m => m is Mod.IModGE).Select(m => (Mod.IModGE)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => e = m.ApplyGEMod(e));
                }

                return e;
            }
        }

        [DependentProperty(nameof(KO))]
        [DependsOnModifikator(typeof(Mod.IModKO))]
        public int Konstitution
        {
            get
            {
                var e = KO ?? 8;
                if (Modifikatoren != null)
                {
                    Modifikatoren.Where(m => m is Mod.IModKO).Select(m => (Mod.IModKO)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => e = m.ApplyKOMod(e));
                }

                return e;
            }
        }

        [DependentProperty(nameof(KO))]
        [DependsOnModifikator(typeof(Mod.IModKO))]
        public int KonstitutionOhneWunden
        {
            get
            {
                var e = KO ?? 8;
                if (Modifikatoren != null)
                {
                    Modifikatoren
                        .Where(m => m is Mod.IModKO && !(m is Mod.WundenModifikatorBase))
                        .Select(m => (Mod.IModKO)m)
                        .OrderBy(m => m.Erstellt)
                        .ToList()
                        .ForEach(m => e = m.ApplyKOMod(e));
                }

                return e;
            }
        }

        [DependentProperty(nameof(KK))]
        [DependsOnModifikator(typeof(Mod.IModKK))]
        public int Körperkraft
        {
            get
            {
                var e = KK ?? 8;
                if (Modifikatoren != null)
                {
                    Modifikatoren
                        .Where(m => m is Mod.IModKK)
                        .Select(m => (Mod.IModKK)m)
                        .OrderBy(m => m.Erstellt)
                        .ToList()
                        .ForEach(m => e = m.ApplyKKMod(e));
                }

                return e;
            }
        }

        /// <summary>
        /// Gibt den Eigenschaftswert zurück.
        /// </summary>
        /// <param name="eigenschaft">Name oder Abkürzung der gewünschten Eigenschaft.</param>
        /// <param name="ohneMod">'True' falls der unmodifizierte Wert gewünscht ist.</param>
        /// <returns>Eigenschaftswert.</returns>
        public int EigenschaftWert(string eigenschaft, bool ohneMod = false)
        {
            if (string.IsNullOrEmpty(eigenschaft))
            {
                return 0;
            }

            switch (eigenschaft)
            {
                case nameof(MU):
                case nameof(Mut):
                    return ohneMod ? MU ?? 0 : Mut;

                case nameof(KL):
                case nameof(Klugheit):
                    return ohneMod ? KL ?? 0 : Klugheit;

                case nameof(IN):
                case nameof(Intuition):
                    return ohneMod ? IN ?? 0 : Intuition;

                case nameof(CH):
                case nameof(Charisma):
                    return ohneMod ? CH ?? 0 : Charisma;

                case nameof(FF):
                case nameof(Fingerfertigkeit):
                    return ohneMod ? FF ?? 0 : Fingerfertigkeit;

                case nameof(GE):
                case nameof(Gewandtheit):
                    return ohneMod ? GE ?? 0 : Gewandtheit;

                case nameof(KO):
                case nameof(Konstitution):
                    return ohneMod ? KO ?? 0 : Konstitution;

                case nameof(KK):
                case nameof(Körperkraft):
                    return ohneMod ? KK ?? 0 : Körperkraft;

                case nameof(SO):
                case "Sozialstatus":
                    return SO ?? 0;

                default:
                    return 0;
            }
        }

        /// <summary>
        /// Gibt die Eigenschaft zurück. ACHTUNG: Änderungen am Wert wirken sich nicht auf den
        /// DataMember aus!
        /// </summary>
        /// <param name="eigenschaft">Name oder Abkürzung der gewünschten Eigenschaft.</param>
        /// <param name="ohneMod">'True' falls der unmodifizierte Wert gewünscht ist.</param>
        /// <returns>Eigenschaft.</returns>
        public Eigenschaft Eigenschaft(string eigenschaft, bool ohneMod = false)
        {
            return new Eigenschaft(eigenschaft, EigenschaftWert(eigenschaft, ohneMod), this);
        }

        #endregion Eigenschaften

        #region Abenteuerpunkte

        [DependentProperty(nameof(APGesamt)), DependentProperty(nameof(APEingesetzt))]
        public int APGuthaben
        {
            get { return APGesamt.GetValueOrDefault() - APEingesetzt.GetValueOrDefault(); }

            set
            {
                // TODO ??: Soll eine Änderung des Guthabens die abhängigen AP-Werte ändern? Soll das
                // möglich sein, oder eher nicht?
            }
        }

        [DependentProperty(nameof(APEingesetzt))]
        public int Stufe
        {
            get { return APEingesetzt.GetValueOrDefault() / 1000; }

            set
            {
                // TODO ??: Änderung der Stufe erhöht die AP-Werte? Soll das möglich sein, oder eher nicht?
            }
        }

        #endregion Abenteuerpunkte

        #region BaseEigenschaften / Für die Berechnung von abgeleiteten Werten

        [DependentProperty(nameof(MU))]
        [DependsOnModifikator(typeof(Mod.IModMU))]
        public int BaseMU
        {
            get
            {
                var mu = MU ?? 8;
                if (Modifikatoren != null)
                {
                    Modifikatoren
                        .Where(m => m is Mod.IModMitAuswirkungAufBerechneteWerte && m is Mod.IModMU)
                        .Select(m => (Mod.IModMU)m)
                        .OrderBy(m => m.Erstellt)
                        .ToList()
                        .ForEach(m => mu = m.ApplyMUMod(mu));
                }

                return mu;
            }
        }

        [DependentProperty(nameof(KL))]
        [DependsOnModifikator(typeof(Mod.IModKL))]
        public int BaseKL
        {
            get
            {
                var e = KL ?? 8;
                if (Modifikatoren != null)
                {
                    Modifikatoren
                        .Where(m => m is Mod.IModMitAuswirkungAufBerechneteWerte && m is Mod.IModKL)
                        .Select(m => (Mod.IModKL)m)
                        .OrderBy(m => m.Erstellt)
                        .ToList()
                        .ForEach(m => e = m.ApplyKLMod(e));
                }

                return e;
            }
        }

        [DependentProperty(nameof(IN))]
        [DependsOnModifikator(typeof(Mod.IModIN))]
        public int BaseIN
        {
            get
            {
                var e = IN ?? 8;
                if (Modifikatoren != null)
                {
                    Modifikatoren
                        .Where(m => m is Mod.IModMitAuswirkungAufBerechneteWerte && m is Mod.IModIN)
                        .Select(m => (Mod.IModIN)m)
                        .OrderBy(m => m.Erstellt)
                        .ToList()
                        .ForEach(m => e = m.ApplyINMod(e));
                }

                return e;
            }
        }

        [DependentProperty(nameof(CH))]
        [DependsOnModifikator(typeof(Mod.IModCH))]
        public int BaseCH
        {
            get
            {
                var e = CH ?? 8;
                if (Modifikatoren != null)
                {
                    Modifikatoren
                        .Where(m => m is Mod.IModMitAuswirkungAufBerechneteWerte && m is Mod.IModCH)
                        .Select(m => (Mod.IModCH)m)
                        .OrderBy(m => m.Erstellt)
                        .ToList()
                        .ForEach(m => e = m.ApplyCHMod(e));
                }

                return e;
            }
        }

        [DependentProperty(nameof(FF))]
        [DependsOnModifikator(typeof(Mod.IModFF))]
        public int BaseFF
        {
            get
            {
                var e = FF ?? 8;
                if (Modifikatoren != null)
                {
                    Modifikatoren
                        .Where(m => m is Mod.IModMitAuswirkungAufBerechneteWerte && m is Mod.IModFF)
                        .Select(m => (Mod.IModFF)m)
                        .OrderBy(m => m.Erstellt)
                        .ToList()
                        .ForEach(m => e = m.ApplyFFMod(e));
                }

                return e;
            }
        }

        [DependentProperty(nameof(GE))]
        [DependsOnModifikator(typeof(Mod.IModGE))]
        public int BaseGE
        {
            get
            {
                var e = GE ?? 8;
                if (Modifikatoren != null)
                {
                    Modifikatoren
                        .Where(m => m is Mod.IModMitAuswirkungAufBerechneteWerte && m is Mod.IModGE)
                        .Select(m => (Mod.IModGE)m)
                        .OrderBy(m => m.Erstellt)
                        .ToList()
                        .ForEach(m => e = m.ApplyGEMod(e));
                }

                return e;
            }
        }

        [DependentProperty(nameof(KO))]
        [DependsOnModifikator(typeof(Mod.IModKO))]
        public int BaseKO
        {
            get
            {
                var e = KO ?? 8;
                if (Modifikatoren != null)
                {
                    Modifikatoren
                        .Where(m => m is Mod.IModMitAuswirkungAufBerechneteWerte && m is Mod.IModKO)
                        .Select(m => (Mod.IModKO)m)
                        .OrderBy(m => m.Erstellt)
                        .ToList()
                        .ForEach(m => e = m.ApplyKOMod(e));
                }

                return e;
            }
        }

        [DependentProperty(nameof(KK))]
        [DependsOnModifikator(typeof(Mod.IModKK))]
        public int BaseKK
        {
            get
            {
                var e = KK ?? 8;
                if (Modifikatoren != null)
                {
                    Modifikatoren
                        .Where(m => m is Mod.IModMitAuswirkungAufBerechneteWerte && m is Mod.IModKK)
                        .Select(m => (Mod.IModKK)m)
                        .OrderBy(m => m.Erstellt)
                        .ToList()
                        .ForEach(m => e = m.ApplyKKMod(e));
                }

                return e;
            }
        }

        #endregion BaseEigenschaften / Für die Berechnung von abgeleiteten Werten

        #region Lebensenergie

        public string LebensenergieGrundwertFormel
        {
            get
            {
                var le = string.Empty;
                if (Regelsystem == Regelwerke.Version_4_1)
                {
                    le = $"({nameof(KO)} + {nameof(KO)} + {nameof(KK)}) / 2";
                }
                else if (Regelsystem == Regelwerke.Version_5)
                {
                    le = $"{nameof(KO)} + {nameof(KO)}";
                }

                return le;
            }
        }

        [DependentProperty(nameof(BaseKO)), DependentProperty(nameof(BaseKK))]
        public int LebensenergieBasis
        {
            get
            {
                var le = 0;
                if (Regelsystem == Regelwerke.Version_4_1)
                {
                    le = (int)Math.Round((BaseKO * 2 + BaseKK) / 2.0, 0, MidpointRounding.AwayFromZero);
                }
                else if (Regelsystem == Regelwerke.Version_5)
                {
                    le = BaseKO * 2;
                }

                return le;
            }
        }

        [DependentProperty(nameof(LebensenergieModSonstiges)),
            DependentProperty(nameof(LebensenergieModGenerierung)),
            DependentProperty(nameof(LebensenergieModVorNachteile)),
            DependentProperty(nameof(LebensenergieModZukauf))]
        public int LebensenergieMod
        {
            get { return LebensenergieModGenerierung + LebensenergieModSonstiges + LebensenergieModVorNachteile + LebensenergieModZukauf; }
        }

        [DependentProperty(nameof(LE_Mod))]
        public int LebensenergieModSonstiges
        {
            get { return LE_Mod ?? 0; }

            set
            {
                LE_Mod = value;
                OnChanged(nameof(LebensenergieModSonstiges));
            }
        }

        [DependentProperty(nameof(LE_ModGen))]
        public int LebensenergieModGenerierung
        {
            get { return LE_ModGen ?? 0; }

            set
            {
                LE_ModGen = value;
                OnChanged(nameof(LebensenergieModGenerierung));
            }
        }

        [DependentProperty(nameof(LE_ModZukauf))]
        public int LebensenergieModZukauf
        {
            get { return LE_ModZukauf ?? 0; }

            set
            {
                LE_ModZukauf = value;
                OnChanged(nameof(LebensenergieModZukauf));
            }
        }

        [DependentProperty(nameof(Nachteile)), DependentProperty(nameof(Vorteile))]
        public int LebensenergieModVorNachteile
        {
            get
            {
                var mod = 0;
                mod += CalcVorNachteilEnergieMod(VorNachteil.HoheLebenskraft);
                mod += CalcVorNachteilEnergieMod(VorNachteil.NiedrigeLebenskraft);
                return mod;
            }
        }

        [DependentProperty(nameof(LE_Aktuell))]
        public int LebensenergieAktuell
        {
            get
            {
                return LE_Aktuell ?? 0;
            }

            set
            {
                LE_Aktuell = value;
                if (LE_Aktuell <= 0)
                {
                    Farbmarkierung = System.Windows.Media.Color.FromArgb(0, 255, 0, 0);
                }
                else if (LE_Aktuell <= 5)
                {
                    Farbmarkierung = System.Windows.Media.Color.FromArgb(0, 255, 255, 0);
                }
                else if (LE_Aktuell > 5)
                {
                    Farbmarkierung = System.Windows.Media.Color.FromArgb(0, 0, 0, 0);
                }
            }
        }

        [DependentProperty(nameof(LebensenergieBasis)),
            DependentProperty(nameof(LebensenergieMod))]
        public int LebensenergieMax
        {
            get
            {
                var le = LebensenergieBasis + LebensenergieMod;
                if (Modifikatoren != null)
                {
                    Modifikatoren
                        .Where(m => m is Mod.IModLE)
                        .Select(m => (Mod.IModLE)m)
                        .OrderBy(m => m.Erstellt)
                        .ToList()
                        .ForEach(m => le = m.ApplyLEMod(le));
                }

                return le;
            }
        }

        [DependentProperty(nameof(LebensenergieAktuell)),
            DependentProperty(nameof(LebensenergieMax)),
            DependentProperty(nameof(Konstitution))]
        public string LebensenergieStatus
        {
            get
            {
                return GetLebensenergieStatus();
            }
        }

        private int CalcVorNachteilEnergieMod(string vn, int faktor = 1)
        {
            var mod = 0;
            if (HatVorNachteil(vn, false))
            {
                if (HatVorNachteil(vn))
                {
                    if (vn.StartsWith("Hohe") || vn == VorNachteil.Ausdauernd || vn == VorNachteil.Astralmacht)
                    {
                        mod += VorNachteilWertInt(vn).GetValueOrDefault(0) * faktor;
                    }
                    else if (vn.StartsWith("Niedrige") || vn == VorNachteil.Kurzatmig)
                    {
                        mod -= VorNachteilWertInt(vn).GetValueOrDefault(0) * faktor;
                    }
                }
            }
            return mod;
        }

        #endregion Lebensenergie

        #region Ausdauer

        public string AusdauerGrundwertFormel
        {
            get
            {
                var au = string.Empty;
                if (Regelsystem == Regelwerke.Version_4_1)
                {
                    au = $"({nameof(MU)} + {nameof(KO)} + {nameof(GE)}) / 2";
                }

                return au;
            }
        }

        [DependentProperty(nameof(BaseMU)),
            DependentProperty(nameof(BaseKO)),
            DependentProperty(nameof(BaseGE))]
        public int AusdauerBasis
        {
            get
            {
                if (Regelsystem == Regelwerke.Version_4_1)
                {
                    return (int)Math.Round((BaseMU + BaseKO + BaseGE) / 2.0, 0, MidpointRounding.AwayFromZero);
                }
                else
                {
                    return 0;
                }
            }
        }

        [DependentProperty(nameof(AU_Aktuell))]
        public int AusdauerAktuell
        {
            get
            {
                return AU_Aktuell ?? 0;
            }

            set
            {
                AU_Aktuell = value;
            }
        }

        [DependentProperty(nameof(AusdauerModSonstiges)),
            DependentProperty(nameof(AusdauerModGenerierung)),
            DependentProperty(nameof(AusdauerModVorNachteile)),
            DependentProperty(nameof(AusdauerModZukauf))]
        public int AusdauerMod
        {
            get { return AusdauerModGenerierung + AusdauerModSonstiges + AusdauerModVorNachteile + AusdauerModZukauf; }
        }

        [DependentProperty(nameof(AU_Mod))]
        public int AusdauerModSonstiges
        {
            get { return AU_Mod ?? 0; }

            set
            {
                AU_Mod = value;
                OnChanged(nameof(AusdauerModSonstiges));
            }
        }

        [DependentProperty(nameof(AU_ModGen))]
        public int AusdauerModGenerierung
        {
            get { return AU_ModGen ?? 0; }

            set
            {
                AU_ModGen = value;
                OnChanged(nameof(AusdauerModGenerierung));
            }
        }

        [DependentProperty(nameof(AU_ModZukauf))]
        public int AusdauerModZukauf
        {
            get { return AU_ModZukauf ?? 0; }

            set
            {
                AU_ModZukauf = value;
                OnChanged(nameof(AusdauerModZukauf));
            }
        }

        [DependentProperty(nameof(Nachteile)),
            DependentProperty(nameof(Vorteile))]
        public int AusdauerModVorNachteile
        {
            get
            {
                var mod = 0;
                mod += CalcVorNachteilEnergieMod(VorNachteil.Ausdauernd);
                mod += CalcVorNachteilEnergieMod(VorNachteil.Kurzatmig);
                return mod;
            }
        }

        [DependentProperty(nameof(AusdauerBasis)), DependentProperty(nameof(AusdauerMod))]
        [DependsOnModifikator(typeof(Mod.IModAU))]
        public int AusdauerMax
        {
            get
            {
                var e = AusdauerBasis + AusdauerMod;
                if (Modifikatoren != null)
                {
                    Modifikatoren
                        .Where(m => m is Mod.IModAU)
                        .Select(m => (Mod.IModAU)m)
                        .OrderBy(m => m.Erstellt)
                        .ToList()
                        .ForEach(m => e = m.ApplyAUMod(e));
                }

                return e;
            }
        }

        [DependentProperty(nameof(AusdauerAktuell)), DependentProperty(nameof(AusdauerMax))]
        public string AusdauerStatus
        {
            get
            {
                return GetAusdauerStatus();
            }
        }

        #endregion Ausdauer

        #region Karmaenergie

        public bool Geweiht
        {
            get
            {
                if (Regelsystem == Regelwerke.Version_4_1)
                {
                    return HatVorNachteil(VorNachteil.GeweihtZwölfgöttlicheKirche) || HatVorNachteil(VorNachteil.GeweihtNichtAlveranischeGottheit)
                        || HatVorNachteil(VorNachteil.GeweihtHRanga) || HatVorNachteil(VorNachteil.GeweihtGravesh) || HatVorNachteil(VorNachteil.GeweihtAngrosch)
                        || HatVorNachteil(VorNachteil.Sacerdos) || HatVorNachteil(VorNachteil.GeweihtXoArtal) || HatVorNachteil(VorNachteil.Karmatiker)
                        || HatSonderfertigkeitUndVoraussetzungen(Sonderfertigkeit.SpätweiheAlveranischeGottheit) || HatSonderfertigkeitUndVoraussetzungen(Sonderfertigkeit.SpätweiheNamenloser)
                        || HatSonderfertigkeitUndVoraussetzungen(Sonderfertigkeit.SpätweiheNichtAlveranischeGottheit) || HatSonderfertigkeitUndVoraussetzungen(Sonderfertigkeit.KontaktZumGroßenGeist)
                        || HatSonderfertigkeitUndVoraussetzungen(Sonderfertigkeit.SpätweiheDunkleZeitenIII) || HatSonderfertigkeitUndVoraussetzungen(Sonderfertigkeit.SpätweiheDunkleZeitenII) || HatSonderfertigkeitUndVoraussetzungen(Sonderfertigkeit.SpätweiheDunkleZeitenI)
                        || HatSonderfertigkeitUndVoraussetzungen(Sonderfertigkeit.SpätweiheXoArtal);
                }
                else if (Regelsystem == Regelwerke.Version_5)
                {
                    return HatVorNachteil(VorNachteil.Geweihter);
                }

                return false;
            }
        }

        public string KarmaenergieGrundwertFormel
        {
            get
            {
                var ke = string.Empty;
                if (Regelsystem == Regelwerke.Version_5)
                {
                    ke = "Leiteigenschaft der Geweihtentradition";
                }

                return ke;
            }
        }

        [DependentProperty(nameof(LeiteigenschaftKlerikal)),
            DependentProperty(nameof(BaseMU)),
            DependentProperty(nameof(BaseKL)),
            DependentProperty(nameof(BaseIN)),
            DependentProperty(nameof(BaseCH)),
            DependentProperty(nameof(BaseFF)),
            DependentProperty(nameof(BaseGE)),
            DependentProperty(nameof(BaseKO)),
            DependentProperty(nameof(BaseKK))]
        public int BaseLeiteigenschaftKarmal
        {
            get
            {
                return EigenschaftWert(LeiteigenschaftKlerikal, true);
            }
        }

        [DependentProperty(nameof(BaseLeiteigenschaftKarmal))]
        public int KarmaenergieBasis
        {
            get
            {
                var ke = 0;
                if (Regelsystem == Regelwerke.Version_5)
                {
                    ke = BaseLeiteigenschaftKarmal;
                }

                return ke;
            }
        }

        [DependentProperty(nameof(KarmaenergieModSonstiges)),
            DependentProperty(nameof(KarmaenergieModGenerierung)),
            DependentProperty(nameof(KarmaenergieModVorNachteile)),
            DependentProperty(nameof(KarmaenergieModZukauf)),
            DependentProperty(nameof(KarmaenergieMod_pKaP))]
        public int KarmaenergieMod
        {
            get { return KarmaenergieModGenerierung + KarmaenergieModSonstiges + KarmaenergieModVorNachteile + KarmaenergieModZukauf - KarmaenergieMod_pKaP; }
        }

        [DependentProperty(nameof(KE_pKaP))]
        public int KarmaenergieMod_pKaP
        {
            get { return KE_pKaP ?? 0; }

            set
            {
                KE_pKaP = value;
                OnChanged(nameof(KarmaenergieMod_pKaP));
            }
        }

        [DependentProperty(nameof(KE_Mod))]
        public int KarmaenergieModSonstiges
        {
            get { return KE_Mod ?? 0; }

            set
            {
                KE_Mod = value;
                OnChanged(nameof(KarmaenergieModSonstiges));
            }
        }

        [DependentProperty(nameof(KE_ModGen))]
        public int KarmaenergieModGenerierung
        {
            get { return KE_ModGen ?? 0; }

            set
            {
                KE_ModGen = value;
                OnChanged(nameof(KarmaenergieModGenerierung));
            }
        }

        [DependentProperty(nameof(KE_ModZukauf))]
        public int KarmaenergieModZukauf
        {
            get { return KE_ModZukauf ?? 0; }

            set
            {
                KE_ModZukauf = value;
                OnChanged(nameof(KarmaenergieModZukauf));
            }
        }

        [DependentProperty(nameof(Nachteile)), DependentProperty(nameof(Vorteile))]
        public int KarmaenergieModVorNachteile
        {
            get
            {
                var mod = 0;
                if (Regelsystem == Regelwerke.Version_4_1)
                {
                    if (HatVorNachteil(VorNachteil.GeweihtZwölfgöttlicheKirche)
                        || HatVorNachteil(VorNachteil.GeweihtHRanga)
                        || HatVorNachteil(VorNachteil.GeweihtGravesh)
                        || HatVorNachteil(VorNachteil.GeweihtAngrosch)
                        || HatVorNachteil(VorNachteil.GeweihtXoArtal)
                        || HatSonderfertigkeit(Sonderfertigkeit.SpätweiheAlveranischeGottheit)
                        || HatSonderfertigkeit(Sonderfertigkeit.SpätweiheNamenloser)
                        || HatSonderfertigkeit(Sonderfertigkeit.SpätweiheXoArtal))
                    {
                        mod += 24;
                    }
                    else if (HatVorNachteil(VorNachteil.GeweihtNichtAlveranischeGottheit)
                        || HatSonderfertigkeit(Sonderfertigkeit.SpätweiheNichtAlveranischeGottheit)
                        || HatSonderfertigkeit(Sonderfertigkeit.KontaktZumGroßenGeist)
                        || HatSonderfertigkeit(Sonderfertigkeit.SpätweiheDunkleZeitenII))
                    {
                        mod += 12;
                    }
                    else if (HatVorNachteil(VorNachteil.Sacerdos))
                    {
                        mod += VorNachteilWertInt(VorNachteil.Sacerdos).GetValueOrDefault(0) * 6;
                    }
                    else if (HatVorNachteil(VorNachteil.Karmatiker))
                    {
                        mod += VorNachteilWertInt(VorNachteil.Karmatiker).GetValueOrDefault(0) * 6;
                    }
                    else if (HatSonderfertigkeit(Sonderfertigkeit.SpätweiheDunkleZeitenIII))
                    {
                        mod += 18;
                    }
                    else if (HatSonderfertigkeit(Sonderfertigkeit.SpätweiheDunkleZeitenI))
                    {
                        mod += 6;
                    }
                    else if (HatVorNachteil(VorNachteil.HoheKarmaenergie))
                    {
                        mod += CalcVorNachteilEnergieMod(VorNachteil.HoheKarmaenergie);
                    }
                }
                else if (Regelsystem == Regelwerke.Version_5)
                {
                    mod += CalcVorNachteilEnergieMod(VorNachteil.HoheKarmalkraft);
                    mod += CalcVorNachteilEnergieMod(VorNachteil.NiedrigeKarmalkraft);
                    if (HatVorNachteil(VorNachteil.Geweihter))
                    {
                        mod += 20;
                    }
                }
                return mod;
            }
        }

        [DependentProperty(nameof(KE_Aktuell))]
        public int KarmaenergieAktuell
        {
            get
            {
                return KE_Aktuell ?? 0;
            }

            set
            {
                KE_Aktuell = value;
            }
        }

        [DependsOnModifikator(typeof(Mod.IModKE))]
        [DependentProperty(nameof(KarmaenergieBasis)),
            DependentProperty(nameof(KarmaenergieMod))]
        public int KarmaenergieMax
        {
            get
            {
                var e = KarmaenergieBasis + KarmaenergieMod;
                if (Modifikatoren != null)
                {
                    Modifikatoren
                        .Where(m => m is Mod.IModKE)
                        .Select(m => (Mod.IModKE)m)
                        .OrderBy(m => m.Erstellt)
                        .ToList()
                        .ForEach(m => e = m.ApplyKEMod(e));
                }

                return e;
            }
        }

        #endregion Karmaenergie

        #region Astralenergie

        public bool Magiebegabt
        {
            get
            {
                if (Regelsystem == Regelwerke.Version_4_1)
                {
                    return HatVorNachteil(VorNachteil.Vollzauberer)
                        || HatVorNachteil(VorNachteil.Halbzauberer)
                        || HatVorNachteil(VorNachteil.Viertelzauberer)
                        || HatVorNachteil(VorNachteil.ViertelzaubererUnbewusst);
                }
                else if (Regelsystem == Regelwerke.Version_5)
                {
                    return HatVorNachteil(VorNachteil.Zauberer);
                }

                return false;
            }
        }

        public string AstralenergieGrundwertFormel
        {
            get
            {
                var ae = string.Empty;
                if (Regelsystem == Regelwerke.Version_4_1)
                {
                    if (HatSonderfertigkeitUndVoraussetzungen(Sonderfertigkeit.GefäßDerSterne))
                    {
                        ae = $"({nameof(MU)} + {nameof(IN)} + {nameof(CH)} + {nameof(CH)}) / 2";
                    }
                    else
                    {
                        ae = $"({nameof(MU)} + {nameof(IN)} + {nameof(CH)}) / 2";
                    }
                }
                else if (Regelsystem == Regelwerke.Version_5)
                {
                    ae = "Leiteigenschaft der Zauberertradition";
                }

                return ae;
            }
        }

        [DependentProperty(nameof(BaseMU)),
            DependentProperty(nameof(BaseIN)),
            DependentProperty(nameof(BaseCH)),
            DependentProperty(nameof(BaseLeiteigenschaftMagisch))]
        public int AstralenergieBasis
        {
            get
            {
                var basis = 0;
                if (Regelsystem == Regelwerke.Version_4_1)
                {
                    basis = BaseMU + BaseIN + BaseCH;
                    if (HatSonderfertigkeitUndVoraussetzungen(Sonderfertigkeit.GefäßDerSterne))
                    {
                        basis += BaseCH;
                    }

                    basis = (int)Math.Round(basis / 2.0, 0, MidpointRounding.AwayFromZero);
                }
                else if (Regelsystem == Regelwerke.Version_5)
                {
                    basis = BaseLeiteigenschaftMagisch;
                }

                return basis;
            }
        }

        /// <summary>
        /// Gibt den Wert der magischen Leiteigenschaft zurück.
        /// </summary>
        [DependentProperty(nameof(LeiteigenschaftMagisch)),
            DependentProperty(nameof(BaseMU)),
            DependentProperty(nameof(BaseKL)),
            DependentProperty(nameof(BaseIN)),
            DependentProperty(nameof(BaseCH)),
            DependentProperty(nameof(BaseFF)),
            DependentProperty(nameof(BaseGE)),
            DependentProperty(nameof(BaseKO)),
            DependentProperty(nameof(BaseKK))]
        public int BaseLeiteigenschaftMagisch
        {
            get
            {
                return EigenschaftWert(LeiteigenschaftMagisch, true);
            }
        }

        [DependentProperty(nameof(AstralenergieModSonstiges)),
            DependentProperty(nameof(AstralenergieModGenerierung)),
            DependentProperty(nameof(AstralenergieModVorNachteile)),
            DependentProperty(nameof(AstralenergieModZukauf)),
            DependentProperty(nameof(AstralenergieMod_pAsP))]
        public int AstralenergieMod
        {
            get { return AstralenergieModGenerierung + AstralenergieModSonstiges + AstralenergieModVorNachteile + AstralenergieModZukauf - AstralenergieMod_pAsP; }
        }

        [DependentProperty(nameof(AE_pAsP))]
        public int AstralenergieMod_pAsP
        {
            get { return AE_pAsP ?? 0; }

            set
            {
                AE_pAsP = value;
                OnChanged(nameof(AstralenergieMod_pAsP));
            }
        }

        [DependentProperty(nameof(AE_Mod))]
        public int AstralenergieModSonstiges
        {
            get { return AE_Mod ?? 0; }

            set
            {
                AE_Mod = value;
                OnChanged(nameof(AstralenergieModSonstiges));
            }
        }

        [DependentProperty(nameof(AE_ModGen))]
        public int AstralenergieModGenerierung
        {
            get { return AE_ModGen ?? 0; }

            set
            {
                AE_ModGen = value;
                OnChanged(nameof(AstralenergieModGenerierung));
            }
        }

        [DependentProperty(nameof(AE_ModZukauf))]
        public int AstralenergieModZukauf
        {
            get { return AE_ModZukauf ?? 0; }

            set
            {
                AE_ModZukauf = value;
                OnChanged(nameof(AstralenergieModZukauf));
            }
        }

        [DependentProperty(nameof(Nachteile)),
            DependentProperty(nameof(Vorteile))]
        public int AstralenergieModVorNachteile
        {
            get
            {
                var mod = 0;
                if (Regelsystem == Regelwerke.Version_4_1)
                {
                    mod += CalcVorNachteilEnergieMod(VorNachteil.Astralmacht, 2);
                    if (HatVorNachteil(VorNachteil.Vollzauberer))
                    {
                        mod += 12;
                    }

                    if (HatVorNachteil(VorNachteil.Halbzauberer))
                    {
                        mod += 6;
                    }

                    if (HatVorNachteil(VorNachteil.Viertelzauberer) || HatVorNachteil(VorNachteil.ViertelzaubererUnbewusst))
                    {
                        mod -= 6;
                    }

                    if (HatVorNachteil(VorNachteil.Zauberhaar))
                    {
                        mod += 7;
                    }
                }
                else if (Regelsystem == Regelwerke.Version_5)
                {
                    mod += CalcVorNachteilEnergieMod(VorNachteil.HoheAstralkraft);
                    if (HatVorNachteil(VorNachteil.Zauberer))
                    {
                        mod += 20;
                    }
                }
                mod += CalcVorNachteilEnergieMod(VorNachteil.NiedrigeAstralkraft);
                return mod;
            }
        }

        [DependentProperty(nameof(AE_Aktuell))]
        public int AstralenergieAktuell
        {
            get
            {
                return AE_Aktuell ?? 0;
            }

            set
            {
                AE_Aktuell = value;
            }
        }

        [DependsOnModifikator(typeof(Mod.IModAE))]
        [DependentProperty(nameof(AstralenergieBasis)),
            DependentProperty(nameof(AstralenergieMod))]
        public int AstralenergieMax
        {
            get
            {
                // wenn er einen Zauberervorteil hat, sonst 0
                if (Magiebegabt)
                {
                    var e = AstralenergieBasis + AstralenergieMod;
                    if (Modifikatoren != null)
                    {
                        Modifikatoren
                            .Where(m => m is Mod.IModAE)
                            .Select(m => (Mod.IModAE)m)
                            .OrderBy(m => m.Erstellt)
                            .ToList()
                            .ForEach(m => e = m.ApplyAEMod(e));
                    }

                    return e;
                }
                return 0;
            }
        }

        #endregion Astralenergie

        #region Regeneration

        private string RegenerationAstralenergie
        {
            get
            {
                var reg = "1W6"; // Grundwert
                // TODO: Regenerations-Modifikatoren
                return reg;
            }
        }

        #endregion Regeneration

        #region Magieresistenz

        public int MagieresistenzBasis
        {
            get { return (int)Math.Round((BaseMU + BaseKL + BaseKO) / 5.0, 0, MidpointRounding.AwayFromZero); }
        }

        [DependentProperty(nameof(MR_Mod)),
            DependentProperty(nameof(MR_Mod_Temp))]
        public int MagieresistenzMod
        {
            get { return MR_Mod ?? 0; }
            set { MR_Mod = value; }
        }

        [DependentProperty(nameof(MR_Mod)),
            DependentProperty(nameof(MR)),
            DependentProperty(nameof(KL)),
            DependentProperty(nameof(KO)),
            DependentProperty(nameof(MR_Mod_Temp))]
        public int MagieresistenzOhneMod
        {
            get { return MagieresistenzBasis + MagieresistenzMod; }
        }

        [DependentProperty(nameof(MR_Mod)),
            DependentProperty(nameof(MR)),
            DependentProperty(nameof(KL)),
            DependentProperty(nameof(KO)),
            DependentProperty(nameof(MR_Mod_Temp))]
        [DependsOnModifikator(typeof(Mod.IModMR))]
        public int Magieresistenz
        {
            get
            {
                //TODO ??: Aurapanzer etc.
                var e = MagieresistenzOhneMod;
                if (Modifikatoren != null)
                {
                    Modifikatoren
                        .Where(m => m is Mod.IModMR)
                        .Select(m => (Mod.IModMR)m)
                        .OrderBy(m => m.Erstellt)
                        .ToList()
                        .ForEach(m => e = m.ApplyMRMod(e));
                }

                MRGeist = e + MR_Mod_Temp;
                return e + MR_Mod_Temp;
            }
        }

        [DependentProperty(nameof(MR_Mod)),
            DependentProperty(nameof(MR)),
            DependentProperty(nameof(KL)),
            DependentProperty(nameof(KO))]
        public int MR_Mod_Temp
        {
            get { return _MR_Mod_Temp; }

            set
            {
                Set(ref _MR_Mod_Temp, value);
                MRGeist = Magieresistenz;
                OnChanged(nameof(MRGeist));
            }
        }

        private int _MR_Mod_Temp = 0;

        #endregion Magieresistenz

        #region Wundschwellen

        [DependentProperty(nameof(Wundschwelle)),
            DependentProperty(nameof(Wundschwelle2)),
            DependentProperty(nameof(Wundschwelle3))]
        public string Wundschwellen
        {
            get { return string.Format("{0} / {1} / {2}", Wundschwelle, Wundschwelle2, Wundschwelle3); }
        }

        [DependentProperty(nameof(Konstitution))]
        public int Wundschwelle
        {
            get
            {
                var ko = E.WundenVerändernWundschwelle ? Konstitution : KonstitutionOhneWunden;
                var ws = Convert.ToInt32(Math.Round(ko / 2.0, 0, MidpointRounding.AwayFromZero));
                if (HatVorNachteil(VorNachteil.Eisern))
                {
                    ws += 2;
                }

                if (HatVorNachteil(VorNachteil.Glasknochen))
                {
                    ws -= 2;
                }

                return ws;
            }
        }

        [DependentProperty(nameof(Konstitution))]
        public int Wundschwelle2
        {
            get
            {
                var ko = E.WundenVerändernWundschwelle ? Konstitution : KonstitutionOhneWunden;
                var ws = ko;
                if (HatVorNachteil(VorNachteil.Eisern))
                {
                    ws += 2;
                }

                if (HatVorNachteil(VorNachteil.Glasknochen))
                {
                    ws -= 2;
                }

                return ws;
            }
        }

        [DependentProperty(nameof(Konstitution))]
        public int Wundschwelle3
        {
            get
            {
                var ko = E.WundenVerändernWundschwelle ? Konstitution : KonstitutionOhneWunden;
                var ws = Convert.ToInt32(Math.Round(ko * 1.5, 0, MidpointRounding.AwayFromZero));
                if (HatVorNachteil(VorNachteil.Eisern))
                {
                    ws += 2;
                }

                if (HatVorNachteil(VorNachteil.Glasknochen))
                {
                    ws -= 2;
                }

                return ws;
            }
        }

        #endregion Wundschwellen

        #region Initiative

        [DependentProperty(nameof(InitiativeModGen)),
            DependentProperty(nameof(MU)),
            DependentProperty(nameof(IN)),
            DependentProperty(nameof(GE))]
        public int InitiativeBasisOhneSonderfertigkeiten
        {
            get
            {
                return (int)Math.Round((BaseMU * 2 + BaseIN + BaseGE) / 5.0, 0, MidpointRounding.AwayFromZero) + InitiativeModGen;
            }
        }

        [DependentProperty(nameof(InitiativeBasisOhneSonderfertigkeiten))]
        public int InitiativeBasisOhneMod
        {
            get
            {
                // berechneter Basiswert
                var ini = InitiativeBasisOhneSonderfertigkeiten;

                // Sonderfertigkeiten
                if (HatSonderfertigkeitUndVoraussetzungen(Sonderfertigkeit.Kampfreflexe) && Behinderung <= 4)
                {
                    ini += 4;
                }

                if (HatSonderfertigkeitUndVoraussetzungen(Sonderfertigkeit.Kampfgespür))
                {
                    ini += 2;
                }

                return ini;
            }
        }

        [DependentProperty(nameof(InitiativeBasisOhneMod))]
        [DependsOnModifikator(typeof(Mod.IModINIBasis))]
        public int InitiativeBasis
        {
            get
            {
                var ini = InitiativeBasisOhneMod;
                if (Modifikatoren != null)
                {
                    Modifikatoren
                        .Where(m => m is Mod.IModINIBasis)
                        .Select(m => (Mod.IModINIBasis)m)
                        .OrderBy(m => m.Erstellt)
                        .ToList()
                        .ForEach(m => ini = m.ApplyINIBasisMod(ini));
                }

                return ini;
            }
        }

        [DependentProperty(nameof(INI_Mod))]
        public int InitiativeModGen
        {
            get
            {
                return INI_Mod ?? 0;
            }

            set
            {
                INI_Mod = value;
            }
        }

        public WürfelEnum InitiativeZufall
        {
            get
            {
                if (HatSonderfertigkeitUndVoraussetzungen(Sonderfertigkeit.Klingentänzer) && Behinderung <= 2)
                {
                    return WürfelEnum._2W6;
                }
                return WürfelEnum._1W6;
            }
        }

        #endregion Initiative

        #region Attacke/Parade

        [DependentProperty(nameof(BaseMU)),
            DependentProperty(nameof(BaseGE)),
            DependentProperty(nameof(BaseKK))]
        public int AttackeBasisOhneMod
        {
            get
            {
                return (int)Math.Round((BaseMU + BaseGE + BaseKK) / 5.0, 0, MidpointRounding.AwayFromZero);
            }
        }

        [DependentProperty(nameof(AttackeBasisOhneMod))]
        [DependsOnModifikator(typeof(Mod.IModATBasis))]
        public int AttackeBasis
        {
            get
            {
                var v = AttackeBasisOhneMod;
                if (Modifikatoren != null)
                {
                    Modifikatoren
                        .Where(m => m is Mod.IModATBasis)
                        .Select(m => (Mod.IModATBasis)m)
                        .OrderBy(m => m.Erstellt)
                        .ToList()
                        .ForEach(m => v = m.ApplyATBasisMod(v));
                }

                return v;
            }
        }

        /// <summary>
        /// Grund-AT-Wert inkl. Abzüge.
        /// </summary>
        [DependentProperty(nameof(AttackeBasis))]
        [DependsOnModifikator(typeof(Mod.IModAT))]
        public int Attacke
        {
            get
            {
                var v = AttackeBasis;
                if (Modifikatoren != null)
                {
                    Modifikatoren
                        .Where(m => m is Mod.IModAT)
                        .Select(m => (Mod.IModAT)m)
                        .OrderBy(m => m.Erstellt)
                        .ToList()
                        .ForEach(m => v = m.ApplyATMod(v));
                }

                return v;
            }
        }

        [DependentProperty(nameof(BaseIN)),
            DependentProperty(nameof(BaseGE)),
            DependentProperty(nameof(BaseKK))]
        public int ParadeBasisOhneMod
        {
            get
            {
                return (int)Math.Round((BaseIN + BaseGE + BaseKK) / 5.0, 0, MidpointRounding.AwayFromZero);
            }
        }

        [DependentProperty(nameof(ParadeBasisOhneMod))]
        [DependsOnModifikator(typeof(Mod.IModPABasis))]
        public int ParadeBasis
        {
            get
            {
                var v = ParadeBasisOhneMod;
                if (Modifikatoren != null)
                {
                    Modifikatoren
                        .Where(m => m is Mod.IModPABasis)
                        .Select(m => (Mod.IModPABasis)m)
                        .OrderBy(m => m.Erstellt)
                        .ToList()
                        .ForEach(m => v = m.ApplyPABasisMod(v));
                }

                return v;
            }
        }

        /// <summary>
        /// Grund-PA-Wert inkl. Abzüge.
        /// </summary>
        [DependentProperty(nameof(ParadeBasis))]
        [DependsOnModifikator(typeof(Mod.IModPA))]
        public int Parade
        {
            get
            {
                var v = ParadeBasis;
                if (Modifikatoren != null)
                {
                    Modifikatoren
                        .Where(m => m is Mod.IModPA)
                        .Select(m => (Mod.IModPA)m)
                        .OrderBy(m => m.Erstellt)
                        .ToList()
                        .ForEach(m => v = m.ApplyPAMod(v));
                }

                return v;
            }
        }

        #endregion Attacke/Parade

        #region Fernkampf

        [DependentProperty(nameof(BaseIN)),
            DependentProperty(nameof(BaseFF)),
            DependentProperty(nameof(BaseKK))]
        public int FernkampfBasisOhneMod
        {
            get
            {
                return (int)Math.Round((BaseIN + BaseFF + BaseKK) / 5.0, 0, MidpointRounding.AwayFromZero);
            }
        }

        [DependentProperty(nameof(FernkampfBasisOhneMod))]
        [DependsOnModifikator(typeof(Mod.IModPABasis))]
        public int FernkampfBasis
        {
            get
            {
                var v = FernkampfBasisOhneMod;
                if (Modifikatoren != null)
                {
                    Modifikatoren
                        .Where(m => m is Mod.IModFKBasis)
                        .Select(m => (Mod.IModFKBasis)m)
                        .OrderBy(m => m.Erstellt)
                        .ToList()
                        .ForEach(m => v = m.ApplyFKBasisMod(v));
                }

                return v;
            }
        }

        /// <summary>
        /// Grund-FK-Wert inkl. Abzüge.
        /// </summary>
        [DependentProperty(nameof(FernkampfBasis))]
        [DependsOnModifikator(typeof(Mod.IModFK))]
        public int Fernkampf
        {
            get
            {
                var v = FernkampfBasis;
                if (Modifikatoren != null)
                {
                    Modifikatoren
                        .Where(m => m is Mod.IModFK)
                        .Select(m => (Mod.IModFK)m)
                        .OrderBy(m => m.Erstellt)
                        .ToList()
                        .ForEach(m => v = m.ApplyFKMod(v));
                }

                return v;
            }
        }

        #endregion Fernkampf

        #region Talente

        public Talent AddTalent(string tName, int wert)
        {
            Talent talent = Global.ContextTalent.TalentListe.Where(t => t.Talentname == tName).SingleOrDefault();

            if (talent == null)
            {
                throw new ArgumentNullException("Talent nicht gefunden.");
            }

            return AddTalent(talent, wert, null, null);
        }

        public Talent AddTalent(Talent t, int wert)
        {
            return AddTalent(t, wert, null, null);
        }

        public Talent AddTalent(Talent t, int wert, int? zuteilungAT, int? zuteilungPA)
        {
            if (t == null)
            {
                return null;
            }

            IEnumerable<Held_Talent> existierendeZuordnung = Held_Talent.Where(hta => hta.TalentGUID == t.TalentGUID && hta.HeldGUID == HeldGUID);
            if (existierendeZuordnung.Count() != 0)
            {
                //Oder eine Exception werfen?
                return existierendeZuordnung.First().Talent;
            }

            Held_Talent ht = Global.ContextHeld.New<Held_Talent>();
            ht.HeldGUID = HeldGUID;
            ht.Held = this;

            ht.TalentGUID = t.TalentGUID;
            ht.Talent = t;

            ht.TaW = wert;
            ht.ZuteilungAT = zuteilungAT;
            ht.ZuteilungPA = zuteilungPA;

            Held_Talent.Add(ht);

            // Abhängige VorNachteile und Sonderfertigkeiten automatisch einfügen. TODO ??: Später
            // ins Datenmodell einbauen. Eigenes DB-Feld mit Talentabhängigkeit.
            if (t.Talentgruppe.TalentgruppeID == 9) // Gabe
            {
                AddVorNachteil(t.Talentname, null);
            } // Falls Ritualkenntnis oder Liturgiekenntnis -> auch passende Sonderfertigkeit hinzufügen
            else if (t.Talentgruppe.TalentgruppeID == 10 || t.Talentgruppe.TalentgruppeID == 11) // Ritualkenntnis, Liturgiekenntnis
            {
                AddSonderfertigkeit(t.Talentname);
            }

            return t;
        }

        public void DeleteTalent(string talentname)
        {
            if (HatTalent(talentname))
            {
                DeleteTalent(Held_Talent.Where(h => h.Talent.Talentname == talentname).FirstOrDefault());
            }
        }

        public void DeleteTalent(Held_Talent ht)
        {
            if (ht != null)
            {
                Talent t = ht.Talent;
                Global.ContextHeld.Delete<Model.Held_Talent>(ht);

                // Abhängige VorNachteile und Sonderfertigkeiten automatisch löschen. TODO ??: Später
                // ins Datenmodell einbauen. Eigenes DB-Feld mit Talentabhängigkeit.
                if (t.Talentgruppe.TalentgruppeID == 9) // Gabe
                {
                    DeleteVorNachteil(t.Talentname);
                } // Falls Ritualkenntnis oder Liturgiekenntnis -> auch passende Sonderfertigkeit hinzufügen
                else if (t.Talentgruppe.TalentgruppeID == 10 || t.Talentgruppe.TalentgruppeID == 11) // Ritualkenntnis, Liturgiekenntnis
                {
                    DeleteSonderfertigkeit(t.Talentname);
                }
            }
        }

        /// <summary>
        /// Held_Talent nach talentName. exactMatch==false ermöglicht suche nach dem Muster
        /// talentName*. Bei Mehrfachtreffern wird der Eintrag mit dem höchsten Modifizierten TaW zurückgegeben.
        /// </summary>
        public Held_Talent GetHeldTalent(string talentName, bool nurPositiv, out int taw, bool exactMatch = true)
        {
            Held_Talent ret = null;
            var maxtaw = int.MinValue;
            foreach (Model.Held_Talent ht in Held_Talent.Where(h => (exactMatch && h.Talent != null && h.Talent.Talentname == talentName)
            || (!exactMatch && h.Talent.Talentname.StartsWith(talentName))))
            {
                var _taw = ht.TaW ?? 0;
                if (Modifikatoren != null)
                {
                    var l = Modifikatoren.Where(m => m is Mod.IModTalentwert && (((Mod.IModTalentwert)m).Talentname == null
                    || ((Mod.IModTalentwert)m).Talentname.Count == 0
                    || ((Mod.IModTalentwert)m).Talentname.Contains(ht.Talent.Talentname))).Select(m => (Mod.IModTalentwert)m).OrderBy(m => m.Erstellt).ToList();
                    foreach (Mod.IModTalentwert m in l)
                    {
                        var tawneu = m.ApplyTalentwertMod(_taw);
                        if (!nurPositiv || tawneu > _taw)
                        {
                            _taw = tawneu;
                        }
                    }
                }
                if (maxtaw < _taw)
                {
                    maxtaw = _taw;
                    ret = ht;
                }
            }
            taw = maxtaw;
            return ret;
        }

        /// <summary>
        /// Der TaW eines Talentes. Liefert bei exactMatch = false den höchsten TaW zurück.
        /// </summary>
        public int Talentwert(string talentName, bool nurPositiv, bool exactMatch = true)
        {
            if (string.IsNullOrWhiteSpace(talentName))
            {
                return 0;
            }

            Held_Talent ht = GetHeldTalent(talentName, nurPositiv, out var maxtaw, exactMatch);
            return (maxtaw == int.MinValue) ? 0 : maxtaw;
        }

        /// <summary>
        /// Der TaW eines Talentes.
        /// </summary>
        public int Talentwert(string talentName)
        {
            return Talentwert(talentName, false);
        }

        /// <summary>
        /// Der TaW eines Talentes.
        /// </summary>
        public int Talentwert(Talent t)
        {
            return Talentwert(t.Talentname, false);
        }

        /// <summary>
        /// Der TaW eines Talentes.
        /// </summary>
        public int Talentwert(Talent t, bool nurPositiv)
        {
            if (t == null)
            {
                return 0;
            }

            return Talentwert(t.Talentname, nurPositiv);
        }

        /// <summary>
        /// Hat mindestens den angegebenen TaW
        /// </summary>
        public bool HatTalent(string talentname, int taw, bool exactMatch = true)
        {
            if (!HatTalent(talentname, exactMatch))
            {
                return false;
            }

            if (taw == int.MinValue)
            {
                return true;
            }

            return Talentwert(talentname, true) >= taw;
        }

        public bool HatTalent(Talent t)
        {
            return Held_Talent.Where(ht => ht.Talent == t).Count() > 0;
        }

        public bool HatTalent(string talentname, bool exactMatch = true)
        {
            return Held_Talent.Where(ht => (exactMatch && ht.Talent.Talentname == talentname) || (!exactMatch && ht.Talent.Talentname.StartsWith(talentname))).Count() > 0;
        }

        /// <summary>
        /// Hat mindestens den angegebenen TaW
        /// </summary>
        public bool HatTalent(Talent t, int taw)
        {
            return HatTalent(t.Talentname, taw);
        }

        public List<string> Talentspezialisierungen(string talentName)
        {
            //TODO ??: bei GUID Umstellung statt Sonderfertigkeit.Name evtl auf GUID prüfen
            if (Held_Sonderfertigkeit != null)
            {
                var r = Held_Sonderfertigkeit.Where(hs => hs.Sonderfertigkeit.Name == Sonderfertigkeit.Talentspezialisierung
                && hs.Wert != null && hs.Wert.StartsWith(talentName)).OrderBy(hs => hs.Wert).Select(hs => hs.Wert).ToList();
                r.ForEach(w => w = Talent.GetSpezialisierungName(talentName, w));
            }
            return null;
        }

        public List<string> Talentspezialisierungen(Talent t)
        {
            if (t == null)
            {
                return null;
            }

            return Talentspezialisierungen(t.Talentname);
        }

        public void AddBasisTalente()
        {
            foreach (Talent t in Global.ContextHeld.Liste<Talent>().Where(t => t.Talenttyp == Talenttypen.Basis
            && t.TalentgruppeID != 0 && t.Regelsystem == Global.Regeledition).ToList())
            {
                if (t.TalentgruppeID != 1)
                {
                    AddTalent(t, 0);
                }
                else
                {
                    AddTalent(t, Global.DSA5 ? 6 : 0, 0, 0);
                }
            }
        }

        /// <summary>
        /// Würfelt eine Probe auf das angegebene Talent.
        /// </summary>
        /// <returns></returns>
        public ProbenErgebnis TalentProbe(Talent t, int mod, string spezialisierung = null)
        {
            if (!HatTalent(t)) //TODO: stattdessen Ableiten.
            {
                var pe = new ProbenErgebnis
                {
                    Ergebnis = ErgebnisTyp.KEIN_ERGEBNIS
                };
                return pe;
            }
            t.WerteSetzen(this, spezialisierung);
            t.Modifikator = mod;
            t.IsBehinderung = true;
            if (false) //automatisch würfeln
            {
                //t.Modifikator += t.BehinderungEff;
                //return t.Würfeln();
            }
            else //per Dialog würfeln
            {
                return View.General.ViewHelper.ShowProbeDialog(t, this);
            }
        }

        #endregion Talente

        #region Zauber

        /// <summary>
        /// Die Zauber, die der Held noch wählen kann.
        /// </summary>
        public List<Zauber> ZauberWählbar
        {
            get
            {
                return Global.ContextZauber.ZauberListe.OrderBy(z => z.Name).ToList();
            }
        }

        /// <summary>
        /// Die erlernten Repräsentationen des Helden.
        /// </summary>
        public IDictionary<Sonderfertigkeit, string> Repräsentationen
        {
            get
            {
                return Held_Sonderfertigkeit
                    .Where(hsf => hsf.Sonderfertigkeit.Name.StartsWith("Repräsentation"))
                    .ToDictionary(hsf => hsf.Sonderfertigkeit, hsf => hsf.Wert);
            }
        }

        public string RepräsentationStandard
        {
            get
            {
                var rep = Repräsentationen.Select(r => r.Key.Name.Replace("Repräsentation (", string.Empty).TrimEnd(')')).ToList();

                if (rep != null && rep.Count == 1)
                {
                    return Logic.General.Repräsentationen.GetKürzel(rep[0]);
                }
                else if (rep != null && rep.Count > 1)
                {
                    // Held hat mehrerer Repräsentationen. Ermitteln welche Repäsentation mit den
                    // meisten Zaubern vertren ist.

                    IOrderedEnumerable<IGrouping<string, Held_Zauber>> maxRep = Held_Zauber.GroupBy(hz => hz.Repräsentation).OrderByDescending(r => r.Count());
                    return maxRep.FirstOrDefault().Key;
                }
                else
                {
                    return "Mag";
                }
            }
        }

        public Zauber AddZauber(Zauber z, int wert, string rep)
        {
            if (z == null)
            {
                return null;
            }

            IEnumerable<Held_Zauber> existierendeZuordnung = Held_Zauber.Where(hza => hza.ZauberGUID == z.ZauberGUID
                && hza.Repräsentation == rep
                && hza.HeldGUID == HeldGUID);
            if (existierendeZuordnung.Count() != 0)
            {
                //Oder eine Exception werfen?
                return existierendeZuordnung.First().Zauber;
            }

            Held_Zauber hz = Global.ContextHeld.New<Held_Zauber>();
            hz.HeldGUID = HeldGUID;
            hz.Held = this;

            hz.ZauberGUID = z.ZauberGUID;
            hz.Zauber = z;

            hz.ZfW = wert;
            hz.Repräsentation = rep;

            Held_Zauber.Add(hz);
            return z;
        }

        /// <summary>
        /// Prüft, ob der Held den Zauber in der angegebenen Repräsentation besitzt.
        /// </summary>
        /// <param name="z">Zauber.</param>
        /// <param name="rep">Repräsentation.</param>
        /// <returns></returns>
        public bool HatZauber(Zauber z, string rep)
        {
            return Held_Zauber.Where(hz => hz.Zauber == z && hz.Repräsentation == rep).Any();
        }

        public bool HatZauber(Guid z, string rep)
        {
            return Held_Zauber.Where(hz => hz.ZauberGUID == z && hz.Repräsentation == rep).Any();
        }

        public bool HatZauber(string zaubername, bool exactMatch = true)
        {
            return Held_Zauber.Where(hz => (exactMatch && hz.Zauber.Name.ToUpperInvariant() == zaubername.ToUpperInvariant())
            || (!exactMatch && hz.Zauber.Name.ToUpperInvariant().StartsWith(zaubername.ToUpperInvariant()))).Any();
        }

        //TODO JT: Eventuell auch nur auf den anfang des Zaubernamens abprüfen
        public bool HatZauber(string zaubername, int zfw, bool exactMatch = true)
        {
            if (!HatZauber(zaubername, exactMatch))
            {
                return false;
            }

            if (zfw == int.MinValue)
            {
                return true;
            }

            return Zauberfertigkeitswert(zaubername, true, exactMatch) >= zfw;
        }

        /// <summary>
        /// Held_Zauber nach zauberName. exactMatch==false ermöglicht suche nach dem Muster
        /// zauberName*. Bei Mehrfachtreffern wird der Eintrag mit dem höchsten Modifizierten ZfW zurückgegeben.
        /// </summary>
        public Held_Zauber GetHeldZauber(string zauberName, bool nurPositiv, out int zfw, bool exactMatch = true)
        {
            Held_Zauber ret = null;
            var maxzfw = int.MinValue;
            foreach (Model.Held_Zauber ht in Held_Zauber.Where(h => (exactMatch && h.Zauber.Name == zauberName)
            || (!exactMatch && h.Zauber.Name.StartsWith(zauberName))))
            {
                var _zfw = ht.ZfW ?? 0;
                if (Modifikatoren != null)
                {
                    var l = Modifikatoren.Where(m => m is Mod.IModZauberwert && (((Mod.IModZauberwert)m).Zaubername == null
                    || ((Mod.IModZauberwert)m).Zaubername.Count == 0
                    || ((Mod.IModZauberwert)m).Zaubername.Contains(ht.Zauber.Name))).Select(m => (Mod.IModZauberwert)m).OrderBy(m => m.Erstellt).ToList();
                    foreach (Mod.IModZauberwert m in l)
                    {
                        var zfneu = m.ApplyZauberwertMod(_zfw);
                        if (!nurPositiv || zfneu > _zfw)
                        {
                            _zfw = zfneu;
                        }
                    }
                }
                if (maxzfw < _zfw)
                {
                    maxzfw = _zfw;
                    ret = ht;
                }
            }
            zfw = maxzfw;
            return ret;
        }

        /// <summary>
        /// Der ZfW eines Zaubers.
        /// </summary>
        public int Zauberfertigkeitswert(string zauberName, bool nurPositiv = false, bool exactMatch = true)
        {
            Model.Held_Zauber hz = Held_Zauber.Where(h => (exactMatch && h.Zauber.Name.ToUpperInvariant() == zauberName.ToUpperInvariant())
            || (!exactMatch && h.Zauber.Name.ToUpperInvariant().StartsWith(zauberName.ToUpperInvariant()))).FirstOrDefault();
            if (hz == null)
            {
                return 0;
            }

            var zfw = hz.ZfW ?? 0;
            if (Modifikatoren != null)
            {
                var l = Modifikatoren.Where(m => m is Mod.IModZauberwert && (((Mod.IModZauberwert)m).Zaubername == null
                || ((Mod.IModZauberwert)m).Zaubername.Count == 0
                || ((Mod.IModZauberwert)m).Zaubername.Contains(hz.Zauber.Name))).Select(m => (Mod.IModZauberwert)m).OrderBy(m => m.Erstellt).ToList();
                foreach (Mod.IModZauberwert m in l)
                {
                    var zfwneu = m.ApplyZauberwertMod(zfw);
                    if (!nurPositiv || zfwneu > zfw)
                    {
                        zfw = zfwneu;
                    }
                }
            }
            return zfw;
        }

        #endregion Zauber

        #region Vor/Nachteile

        /// <summary>
        /// Die Vorteile des Helden. Nicht zum ändern von Werten, da die Werte in Held_VorNachteil stehen.
        /// </summary>
        public IDictionary<VorNachteil, ICollection<string>> Vorteile
        {
            get
            {
                var d = new Dictionary<VorNachteil, ICollection<string>>();
                foreach (Held_VorNachteil hvn in Held_VorNachteil.Where(hvn => hvn.VorNachteil != null && hvn.VorNachteil.Vorteil == true))
                {
                    if (!d.ContainsKey(hvn.VorNachteil))
                    {
                        d.Add(hvn.VorNachteil, new List<string> { });
                    }

                    d[hvn.VorNachteil].Add(hvn.Wert);
                }
                return d;
            }
        }

        /// <summary>
        /// Die Vorteile, die der Held noch wählen kann.
        /// </summary>
        public List<VorNachteil> VorteileWählbar
        {
            get
            {
                return Global.ContextVorNachteil.VorNachteilListe
                    .Where(v => v.Vorteil == true)
                    .Except(Vorteile.Keys.Where(s => !s.HatWert ?? false))
                    .OrderBy(sf => sf.Name)
                    .ToList();
            }
        }

        /// <summary>
        /// Die Nachteile des Helden. Nicht zum ändern von Werten, da die Werte in Held_VorNachteil stehen.
        /// </summary>
        public IDictionary<VorNachteil, ICollection<string>> Nachteile
        {
            get
            {
                var d = new Dictionary<VorNachteil, ICollection<string>>();
                foreach (Held_VorNachteil hvn in Held_VorNachteil.Where(hvn => hvn.VorNachteil != null && hvn.VorNachteil.Nachteil == true))
                {
                    if (!d.ContainsKey(hvn.VorNachteil))
                    {
                        d.Add(hvn.VorNachteil, new List<string> { });
                    }

                    d[hvn.VorNachteil].Add(hvn.Wert);
                }
                return d;
            }
        }

        /// <summary>
        /// Die Nachteile, die der Held noch wählen kann.
        /// </summary>
        public List<VorNachteil> NachteileWählbar
        {
            get
            {
                return Global.ContextVorNachteil.VorNachteilListe
                    .Where(n => n.Nachteil == true)
                    .Except(Nachteile.Keys
                    .Where(s => !s.HatWert ?? false))
                    .OrderBy(sf => sf.Name).ToList();
            }
        }

        public double SummeNachteile
        {
            get
            {
                if (_summeNachteile == null)
                { // Summe zum ersten Mal berechnen
                    _summeNachteile = 0.0;
                    foreach (Held_VorNachteil hvn in Held_VorNachteil.Where(vn => vn.VorNachteil.Nachteil == true))
                    {
                        _summeNachteile += hvn.KostenGesamt;
                        hvn.PropertyChanged += Hvn_PropertyChanged;
                    }
                }
                return _summeNachteile.Value;
            }

            set
            {
                if (_summeNachteile != value)
                {
                    _summeNachteile = value;
                    OnChanged();
                }
            }
        }

        public double SummeVorteile
        {
            get
            {
                if (_summeVorteile == null)
                { // Summe zum ersten Mal berechnen
                    _summeVorteile = 0.0;
                    foreach (Held_VorNachteil hvn in Held_VorNachteil.Where(vn => vn.VorNachteil.Vorteil == true))
                    {
                        _summeVorteile += hvn.KostenGesamt;
                        hvn.PropertyChanged += Hvn_PropertyChanged;
                    }
                }
                return _summeVorteile.Value;
            }

            set
            {
                if (_summeVorteile != value)
                {
                    _summeVorteile = value;
                    OnChanged();
                }
            }
        }

        public VorNachteil AddVorNachteil(string vorNachName, string wert = "")
        {
            VorNachteil vorNach = Global.ContextHeld.Liste<VorNachteil>()
                .Where(vn => vn.Name == vorNachName)
                .FirstOrDefault();
            return AddVorNachteil(vorNach, vorNach.KostenGrund, vorNach.KostenFaktor, wert);
        }

        public VorNachteil AddVorNachteil(VorNachteil vn, double? kostenGrund, double? kostenFaktor, string wert = "")
        {
            if (vn == null)
            {
                return null;
            }

            IEnumerable<Held_VorNachteil> existierendeZuordnung = Held_VorNachteil
                .Where(heldvn => heldvn.VorNachteilGUID == vn.VorNachteilGUID && heldvn.HeldGUID == HeldGUID);
            if (existierendeZuordnung.Any())
            { //es gibt bereits einen solchen VoNachteil auf dem helden
                if (!vn.HatWert ?? false)
                {
                    //Da es eine ohne Wert ist, darf sie nur einmal vergeben werden
                    return existierendeZuordnung.First().VorNachteil;
                }
                else if (existierendeZuordnung.Where(hvn1 => hvn1.Wert == wert
                || (string.IsNullOrWhiteSpace(wert) && string.IsNullOrWhiteSpace(hvn1.Wert))).Any())
                {
                    //Wenn sie mit diesem Wert bereits existiert, dann darf sie auch nicht nochmal hinzugefügt werden.
                    return existierendeZuordnung.Where(hvn1 => hvn1.Wert == wert
                    || (string.IsNullOrWhiteSpace(wert) && string.IsNullOrWhiteSpace(hvn1.Wert))).First().VorNachteil;
                }
            }

            var hvn = new Held_VorNachteil
            {
                HeldGUID = HeldGUID,
                Held = this,

                VorNachteilGUID = vn.VorNachteilGUID,
                VorNachteil = vn
            };

            // Kosten (AP bzw. GP)
            if (kostenGrund == null)
            {
                hvn.KostenGrund = vn.KostenGrund ?? 0.0;
            }
            else
            {
                hvn.KostenGrund = kostenGrund ?? 0.0;
            }

            if (kostenFaktor == null)
            {
                hvn.KostenFaktor = vn.KostenFaktor ?? 0.0;
            }
            else
            {
                hvn.KostenFaktor = kostenFaktor ?? 0.0;
            }

            hvn.Wert = wert ?? string.Empty;
            if (hvn.Wert == string.Empty && vn.WertTyp == "int")
            {
                if (vn.WertMin.HasValue)
                {
                    hvn.Wert = vn.WertMin.Value.ToString();
                }
            }
            else if (hvn.Wert == "" && vn.Auswahl != null)
            {
                hvn.Wert = vn.Auswahl; // mit Auswahl-Wert vorbelegen
            }

            if (vn.Vorteil != null)
            {
                if ((bool)vn.Vorteil)
                {
                    hvn.VorNachteil.Vorteil = true;
                }
            }
            else if (vn.Nachteil != null)
            {
                if ((bool)vn.Nachteil)
                {
                    hvn.VorNachteil.Nachteil = true;
                }
            }
            else
            {
                throw new ArgumentNullException("Weder Vor- noch Nachteil gesetzt");
            }
            //check
            if (!((bool)hvn.VorNachteil.Vorteil || (bool)hvn.VorNachteil.Nachteil))
            {
                throw new ArgumentNullException("Weder Vor- noch Nachteil gesetzt");
            }

            Held_VorNachteil.Add(hvn);

            // abhängige Talente automatisch einfügen TODO ??: Ins Datenmodell einbauen. Eigenes
            // DB-Feld mit Talentabhängigkeit.
            if (vn.Name == VorNachteil.Empathie)
            {
                AddTalent(VorNachteil.Empathie, 3);
            }
            else if (vn.Name == VorNachteil.Gefahreninstinkt)
            {
                AddTalent(VorNachteil.Gefahreninstinkt, 3);
            }
            else if (vn.Name == VorNachteil.Geräuschhexerei)
            {
                AddTalent(VorNachteil.Geräuschhexerei, 3);
            }
            else if (vn.Name == VorNachteil.Magiegespür)
            {
                AddTalent(VorNachteil.Magiegespür, 3);
            }
            else if (vn.Name == VorNachteil.Prophezeien)
            {
                AddTalent(VorNachteil.Prophezeien, 3);
            }
            else if (vn.Name == VorNachteil.Zwergennase)
            {
                AddTalent(VorNachteil.Zwergennase, 3);
            }
            else if (vn.Name == VorNachteil.TierempathieAlle || vn.Name == VorNachteil.TierempathieSpeziell)
            {
                AddTalent("Tierempathie", 3);
            }

            // Gesamt-Kosten aktualisieren
            if (hvn.VorNachteil.Vorteil ?? false)
            {
                SummeVorteile += hvn.KostenGesamt;
            }

            if (hvn.VorNachteil.Nachteil ?? false)
            {
                SummeNachteile += hvn.KostenGesamt;
            }

            hvn.PropertyChanged += Hvn_PropertyChanged;

            return vn;
        }

        public void DeleteVorNachteil(string vnName)
        {
            if (HatVorNachteil(vnName))
            {
                DeleteVorNachteil(Held_VorNachteil.Where(h => h.VorNachteil.Name == vnName).FirstOrDefault());
            }
        }

        public void DeleteVorNachteil(Held_VorNachteil hvn)
        {
            if (hvn != null)
            {
                var vnName = hvn.VorNachteil.Name;

                // Gesamt-Kosten aktualisieren
                if (hvn.VorNachteil.Vorteil ?? false)
                {
                    SummeVorteile -= hvn.KostenGesamt;
                }

                if (hvn.VorNachteil.Nachteil ?? false)
                {
                    SummeNachteile -= hvn.KostenGesamt;
                }

                hvn.PropertyChanged -= Hvn_PropertyChanged;

                Global.ContextHeld.Delete<Model.Held_VorNachteil>(hvn);

                // Falls Gabe -> Talent mit löschen TODO ??: Ins Datenmodell einbauen. Eigenes
                // DB-Feld mit Talentabhängigkeit.
                if (hvn == null || hvn.VorNachteil == null)
                {
                    return;
                }

                if (vnName == VorNachteil.Empathie)
                {
                    DeleteTalent(VorNachteil.Empathie);
                }
                else if (vnName == VorNachteil.Gefahreninstinkt)
                {
                    DeleteTalent(VorNachteil.Gefahreninstinkt);
                }
                else if (vnName == VorNachteil.Geräuschhexerei)
                {
                    DeleteTalent(VorNachteil.Geräuschhexerei);
                }
                else if (vnName == VorNachteil.Magiegespür)
                {
                    DeleteTalent(VorNachteil.Magiegespür);
                }
                else if (vnName == VorNachteil.Prophezeien)
                {
                    DeleteTalent(VorNachteil.Prophezeien);
                }
                else if (vnName == VorNachteil.Zwergennase)
                {
                    DeleteTalent(VorNachteil.Zwergennase);
                }
                else if (vnName == VorNachteil.TierempathieAlle || vnName == VorNachteil.TierempathieSpeziell)
                {
                    DeleteTalent("Tierempathie");
                }
            }
        }

        /// <summary>
        /// Gibt den Wert eines VorNachteils zurück.
        /// </summary>
        /// <param name="vn">Axakter Name des VorNachteils.</param>
        /// <returns></returns>
        public string VorNachteilWert(string vn)
        {
            Held_VorNachteil heldVN = Held_VorNachteil.Where(hvn => hvn.VorNachteil != null && hvn.VorNachteil.Name == vn).FirstOrDefault();
            var val = heldVN != null ? heldVN.Wert : null;

            return val;
        }

        /// <summary>
        /// Gibt den Integer Wert eines VorNachteils zurück.
        /// </summary>
        /// <param name="vn">Axakter Name des VorNachteils.</param>
        /// <returns></returns>
        public Nullable<int> VorNachteilWertInt(string vn)
        {
            Held_VorNachteil heldVN = Held_VorNachteil.Where(hvn => hvn.VorNachteil != null && hvn.VorNachteil.Name == vn).FirstOrDefault();
            return heldVN != null ? heldVN.WertInt : null;
        }

        public bool HatVorNachteil(VorNachteil vn)
        {
            return Held_VorNachteil.Where(hvn => hvn.VorNachteil != null && hvn.VorNachteil == vn).Any();
        }

        public bool HatVorNachteil(string vn, bool exactMatch = true)
        {
            return HatVorNachteil(vn, null, exactMatch);
        }

        public bool HatVorNachteil(string vn, string wert, bool exactMatch = true)
        {
            string m1 = vn, m2 = string.Empty;
            if (!exactMatch)
            {
                var a = vn.Split('%');
                if (a.Length > 2)
                {
                    m1 = a[0];
                    m2 = a[1];
                }
            }
            try
            {
                var hvnList = Held_VorNachteil.Where(hvn2 => hvn2.VorNachteil != null && ((exactMatch && hvn2.VorNachteil.Name == vn)
                || (
                    !exactMatch && hvn2.VorNachteil.Name.StartsWith(m1)
                    && (m2 == string.Empty || hvn2.VorNachteil.Name.EndsWith(m2))
                    )
                )).ToList();
                foreach (Held_VorNachteil hvn in hvnList)
                {
                    //Wert abprüfen
                    if (wert != null && (hvn.VorNachteil.HatWert ?? false))
                    {
                        if (hvn.VorNachteil.WertTyp != null && hvn.VorNachteil.WertTyp.ToLowerInvariant() == "int")
                        {
                            if (int.TryParse(wert, out var expected) && int.TryParse(hvn.Wert, out var actual))
                            {
                                if (actual >= expected)
                                {
                                    return true;
                                }

                                continue;
                            }
                            else
                            {
                                System.Diagnostics.Debug.WriteLine($"Fehler beim Parsen des Wertes {wert} oder {hvn.Wert} zu einem Integer. HatVorNachteil({vn},{wert}");
                                continue;
                            }
                        }
                        else if (hvn.Wert == wert)
                        {
                            return true;
                        }
                    }
                    return true;
                }
                return false;
            }
            catch (Exception)
            { return false; }
        }

        private double? _summeNachteile = null;
        private double? _summeVorteile = null;

        private void Hvn_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender is Held_VorNachteil && e.PropertyName == nameof(Model.Held_VorNachteil.KostenGesamt))
            {
                // VorNachteile Summe neu berechnen
                var hvn = (Held_VorNachteil)sender;
                if (hvn.VorNachteil.Vorteil ?? false)
                {
                    _summeVorteile = null;
                    OnChanged(nameof(SummeVorteile));
                }
                if (hvn.VorNachteil.Nachteil ?? false)
                {
                    _summeNachteile = null;
                    OnChanged(nameof(SummeNachteile));
                }
            }
        }

        #endregion Vor/Nachteile

        #region Sonderfertigkeiten

        /// <summary>
        /// Die Sonderfertigkeiten des Helden. Nicht zum ändern von Werten, da die Werte in
        /// Held_Sonderfertigkeit stehen.
        /// </summary>
        public IDictionary<Sonderfertigkeit, ICollection<string>> Sonderfertigkeiten
        {
            get
            {
                var d = new Dictionary<Sonderfertigkeit, ICollection<string>>();
                foreach (Held_Sonderfertigkeit hsf in Held_Sonderfertigkeit)
                {
                    if (!d.ContainsKey(hsf.Sonderfertigkeit))
                    {
                        d.Add(hsf.Sonderfertigkeit, new List<string> { });
                    }

                    d[hsf.Sonderfertigkeit].Add(hsf.Wert);
                }
                return d;
            }
        }

        //TODO: Ob es ratsam ist nur die mit erfüllten Voraussetzungen anzuzeigen?
        /// <summary>
        /// Die Sonderfertigkeiten, die der Held wählen kann. Die Voraussetzungen müssen erfüllt sein.
        /// </summary>
        public List<Sonderfertigkeit> SonderfertigkeitenWählbar
        {
            get
            {
                return Global.ContextHeld.SonderfertigkeitListe
                    .Except(Sonderfertigkeiten.Keys.Where(s => !s.HatWert ?? false))
                    .OrderBy(sf => sf.Name).ToList();
            }
        }

        public Sonderfertigkeit AddSonderfertigkeit(string sfName, string wert = "")
        {
            return AddSonderfertigkeit(Global.ContextHeld.Liste<Sonderfertigkeit>()
                .Where(sf => sf.Name == sfName)
                .FirstOrDefault(), wert);
        }

        public Sonderfertigkeit AddSonderfertigkeit(Sonderfertigkeit sf, string wert = "")
        {
            if (sf == null)
            {
                return null;
            }

            IEnumerable<Held_Sonderfertigkeit> existierendeZuordnung = Held_Sonderfertigkeit.Where(heldsf => heldsf.SonderfertigkeitGUID == sf.SonderfertigkeitGUID && heldsf.HeldGUID == HeldGUID);
            if (existierendeZuordnung.Any()) //es gibt bereits eine solche sonderfertigkeit auf dem helden
            {
                if (!sf.HatWert ?? false)
                {
                    //Da es eine ohne Wert ist, darf sie nur einmal vergeben werden
                    return existierendeZuordnung.First().Sonderfertigkeit;
                }
                else if (existierendeZuordnung.Where(hsf => hsf.Wert == wert
                || (string.IsNullOrWhiteSpace(wert) && string.IsNullOrWhiteSpace(hsf.Wert))).Any())
                {
                    //Wenn sie mit diesem Wert bereits existiert, dann darf sie auch nicht nochmal hinzugefügt werden.
                    return existierendeZuordnung.Where(hsf => hsf.Wert == wert
                    || (string.IsNullOrWhiteSpace(wert) && string.IsNullOrWhiteSpace(hsf.Wert))).First().Sonderfertigkeit;
                }
            }

            Held_Sonderfertigkeit hs = Global.ContextHeld.New<Held_Sonderfertigkeit>();
            hs.HeldGUID = HeldGUID;
            hs.Held = this;

            hs.SonderfertigkeitGUID = sf.SonderfertigkeitGUID;
            hs.Sonderfertigkeit = sf;

            hs.Wert = wert ?? string.Empty;

            Held_Sonderfertigkeit.Add(hs);

            // Abhängige Talente automatisch einfügen. TODO ??: Später ins Datenmodell einbauen.
            // Eigenes DB-Feld mit Talentabhängigkeit. Falls Ritualkenntnis oder Liturgiekenntnis ->
            // Talente automatisch einfügen
            if (sf.Name == Sonderfertigkeit.RitualkenntnisSchamaneAchaz || sf.Name == Sonderfertigkeit.RitualkenntnisSchamaneFerkina
                || sf.Name == Sonderfertigkeit.RitualkenntnisSchamaneGjalsker || sf.Name == Sonderfertigkeit.RitualkenntnisSchamaneGoblin
                || sf.Name == Sonderfertigkeit.RitualkenntnisSchamaneNivesen || sf.Name == Sonderfertigkeit.RitualkenntnisSchamaneOrk
                || sf.Name == Sonderfertigkeit.RitualkenntnisSchamaneTrollzacker || sf.Name == Sonderfertigkeit.RitualkenntnisSchamaneWaldmenschen)
            { // Schamanen...
                AddTalent(Talente.GeisterAufnehmen, 3);
                AddTalent(Talente.GeisterBannen, 3);
                AddTalent(Talente.GeisterBinden, 3);
                AddTalent(Talente.GeisterRufen, 3);
            } // Runenkunde...
            else if (sf.Name == Sonderfertigkeit.Runenkunde)
            {
                AddTalent(Talente.RitualkenntnisRunenzauberei, 3);
            }
            else if (sf.Name.StartsWith("Ritualkenntnis"))
            { // Ritualkenntnis...
                var tradition = sf.Name.Replace("Ritualkenntnis (", string.Empty).Replace(")", string.Empty);
                AddTalent("Ritualkenntnis (" + tradition + ")", 3);
            } // Liturgiekenntnis...
            else if (sf.Name.StartsWith("Liturgiekenntnis"))
            {
                var kirche = sf.Name.Replace("Liturgiekenntnis (", string.Empty).Replace(")", string.Empty);
                AddTalent("Liturgiekenntnis (" + kirche + ")", 3);
            }

            return sf;
        }

        public void DeleteSonderfertigkeit(string sfName)
        {
            if (HatSonderfertigkeit(sfName))
            {
                DeleteSonderfertigkeit(Held_Sonderfertigkeit.Where(h => h.Sonderfertigkeit.Name == sfName).FirstOrDefault());
            }
        }

        public void DeleteSonderfertigkeit(Held_Sonderfertigkeit hsf)
        {
            if (hsf != null)
            {
                var sfName = hsf.Sonderfertigkeit.Name;

                Global.ContextHeld.Delete<Model.Held_Sonderfertigkeit>(hsf);

                // Falls Ritualkenntnis oder Liturgiekenntnis -> Talent mit löschen TODO ??: Später
                // ins Datenmodell einbauen. Eigenes DB-Feld mit Talentabhängigkeit.
                if (sfName.StartsWith("Ritualkenntnis") || sfName.StartsWith("Liturgiekenntnis"))
                {
                    DeleteTalent(sfName);
                }
            }
        }

        /// <summary>
        /// Hat die Sonderfertigkeit.
        /// </summary>
        public bool HatSonderfertigkeit(Sonderfertigkeit s)
        {
            return Held_Sonderfertigkeit.Where(hs => hs.Sonderfertigkeit != null && hs.Sonderfertigkeit == s).Any();
        }

        /// <summary>
        /// Hat die Sonderfertigkeit mit bestimmtem Wert.
        /// </summary>
        public bool HatSonderfertigkeit(string sonderfertigkeit, string wert = null, bool exactMatch = true)
        {
            string m1 = sonderfertigkeit, m2 = string.Empty;
            if (!exactMatch)
            {
                var a = sonderfertigkeit.Split('%');
                if (a.Length > 2)
                {
                    m1 = a[0];
                    m2 = a[1];
                }
            }
            IEnumerable<Held_Sonderfertigkeit> hso = Held_Sonderfertigkeit
                .Where(hs => hs.Sonderfertigkeit != null
                && ((exactMatch && hs.Sonderfertigkeit.Name == sonderfertigkeit)
                || (!exactMatch && hs.Sonderfertigkeit.Name.StartsWith(m1))
                && (m2 == string.Empty || hs.Sonderfertigkeit.Name.EndsWith(m2))
                ));
            if (hso.Count() == 0)
            {
                return false;
            }
            //rekursiv die Voraussetzungen prüfen
            foreach (Held_Sonderfertigkeit hs in hso)
            {
                //Wert prüfen
                if (wert != null && (hs.Sonderfertigkeit.HatWert ?? false) && hs.Wert != wert)
                {
                    continue;
                }

                return true;
            }
            return false;
        }

        /// <summary>
        /// Hat die Sonderfertigkeit inklusive der Voraussetzungen.
        /// </summary>
        public bool HatSonderfertigkeitUndVoraussetzungen(Sonderfertigkeit s)
        {
            if (Held_Sonderfertigkeit.Where(hs => hs.Sonderfertigkeit != null && hs.Sonderfertigkeit == s).Any())
            {
                return s.CheckVoraussetzungen(this);
            }

            return false;
        }

        /// <summary>
        /// Hat die Sonderfertigkeit mit bestimmtem Wert inklusive der Voraussetzungen.
        /// </summary>
        public bool HatSonderfertigkeitUndVoraussetzungen(string sonderfertigkeit, string wert, bool exactMatch = true)
        {
            string m1 = sonderfertigkeit, m2 = string.Empty;
            if (!exactMatch)
            {
                var a = sonderfertigkeit.Split('%');
                if (a.Length > 2)
                {
                    m1 = a[0];
                    m2 = a[1];
                }
            }
            IEnumerable<Held_Sonderfertigkeit> hso = Held_Sonderfertigkeit.Where(hs => hs.Sonderfertigkeit != null &&
                (
                    (exactMatch && hs.Sonderfertigkeit.Name == sonderfertigkeit)
                    ||
                    (
                        !exactMatch && hs.Sonderfertigkeit.Name.StartsWith(m1)
                        &&
                        (m2 == string.Empty || hs.Sonderfertigkeit.Name.EndsWith(m2))
                    )
                )
            );
            if (!hso.Any())
            {
                return false;
            }
            //rekursiv die voraussetzungen prüfen
            foreach (Held_Sonderfertigkeit hs in hso)
            {
                //Wert prüfen
                if (wert != null && (hs.Sonderfertigkeit.HatWert ?? false) && hs.Wert != wert)
                {
                    continue;
                }

                if (hs.Sonderfertigkeit.CheckVoraussetzungen(this))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Hat die Sonderfertigkeit inklusive der Voraussetzungen.
        /// </summary>
        public bool HatSonderfertigkeitUndVoraussetzungen(string sonderfertigkeit, bool exactMatch = true)
        {
            return HatSonderfertigkeitUndVoraussetzungen(sonderfertigkeit, null);
        }

        #endregion Sonderfertigkeiten

        #region Bewegung / Geschwindigkeit

        [DependentProperty(nameof(BaseGE))]
        public int GeschwindigkeitOhneMod
        {
            get
            {
                var gs = 8;
                if (HatVorNachteil(VorNachteil.Flink))
                {
                    gs++;
                    if (HatVorNachteil(VorNachteil.FlinkII))
                    {
                        gs++;
                    }
                }
                if (HatVorNachteil(VorNachteil.Zwergenwuchs))
                {
                    gs -= 2;
                }

                if (HatVorNachteil(VorNachteil.Kleinwüchsig))
                {
                    gs--;
                }

                if (HatVorNachteil(VorNachteil.Behäbig))
                {
                    gs--;
                }

                if (BaseGE >= 16)
                {
                    gs++;
                }
                else if (BaseGE <= 10)
                {
                    gs--;
                }

                return gs;
            }
        }

        [DependentProperty(nameof(GeschwindigkeitOhneMod)),
            DependentProperty(nameof(Behinderung))]
        [DependsOnModifikator(typeof(Mod.IModGS))]
        public double Geschwindigkeit
        {
            get
            {
                double gs = GeschwindigkeitOhneMod;
                if (Modifikatoren != null)
                {
                    Modifikatoren
                        .Where(m => m is Mod.IModGS)
                        .Select(m => (Mod.IModGS)m)
                        .OrderBy(m => m.Erstellt)
                        .ToList()
                        .ForEach(m => gs = m.ApplyGSMod(gs));
                }

                if (HatVorNachteil(VorNachteil.Zwergenwuchs)) // BE aus Last und Rüstung geht nur halb ein WdH 274
                {
                    return gs - Behinderung / 2;
                }

                return Math.Max(gs - Behinderung, 1.0);
            }
        }

        #endregion Bewegung / Geschwindigkeit

        #region Kampfwerte

        /// <summary>
        /// Gibt alle Kampftalente des Helden als Liste zurück.
        /// </summary>
        [DependsOnModifikator(typeof(Mod.IModifikator))]
        public List<Model.Held_Talent> Kampftalente
        {
            get
            {
                return Held_Talent
                    .Where(ht => ht.Talent != null && ht.Talent.IsKampfTalent)
                    .OrderByDescending(ht => ht.TaW)
                    .ThenBy(ht => ht.Talent.Talentname)
                    .ToList();
            }
        }

        #endregion Kampfwerte

        #region IKämpfer

        public string Initialen
        {
            get
            {
                var initialen = string.Empty;
                var worte = Name.Split(new char[] { ' ', '\'', '-', '\t', '\n' });
                if (worte.Length <= 0)
                {
                    return null;
                }

                initialen += worte[0][0];
                if (worte.Length > 1)
                {
                    initialen += worte[worte.Length - 1][0];
                }

                return initialen;
            }
        }

        public int InitiativeWurf
        {
            get { return _initiativeWurf; }
        }

        public int InitiativeWaffen
        {
            get
            {
                //angelegte Waffen und Schilde verrechnen
                var wini = 0;
                foreach (KämpferNahkampfwaffe w in Nahkampfwaffen)
                {
                    wini += w.INI ?? 0;
                }

                foreach (KämpferSchild w in Schilde)
                {
                    wini += w.INI;
                }

                return wini;
            }
        }

        //TODO ??: Wert der aktuellen Waffe verwenden.
        public int? AT
        {
            get { return Attacke; }
        }

        //TODO ??: Wert der aktuellen Waffe verwenden.
        public int? PA
        {
            get { return Parade; }
        }

        public int MR
        {
            get { return Magieresistenz; }
        }

        [DependentProperty(nameof(MR_Mod)),
            DependentProperty(nameof(MR)),
            DependentProperty(nameof(KL)),
            DependentProperty(nameof(KO)),
            DependentProperty(nameof(MR_Mod_Temp))]
        public int MRGeist
        {
            //TODO ??: verschiedene Sonderfertigkeiten verändern die Geistmagieresistenz.
            //  get { return Magieresistenz; }
            get { return _mrGeist == -99 ? Magieresistenz : _mrGeist; }
            set { Set(ref _mrGeist, value); }
        }

        public IRüstungsschutz RS
        {
            get
            {
                if (_rs == null)
                {
                    _rs = new Rüstungsschutz(this);
                }

                return _rs;
            }
        }

        /// <summary>
        /// Ausweichen-Wert inklusive Akrobatik und Sonderfertigkeiten.
        /// </summary>
        [DependentProperty(nameof(ParadeBasis))]
        //[DependsOnModifikator(typeof(Mod.IModAusweichen))] //gibt noch keinen Mod für das Ausweichen
        public int AusweichenOhneMod
        {
            get
            {
                var ausweichen = ParadeBasis;
                if (HatSonderfertigkeitUndVoraussetzungen(Sonderfertigkeit.AusweichenI))
                {
                    ausweichen += 3;
                }

                if (HatSonderfertigkeitUndVoraussetzungen(Sonderfertigkeit.AusweichenII))
                {
                    ausweichen += 3;
                }

                if (HatSonderfertigkeitUndVoraussetzungen(Sonderfertigkeit.AusweichenIII))
                {
                    ausweichen += 3;
                }

                if (HatVorNachteil(VorNachteil.Zwergenwuchs))
                {
                    ausweichen += 1;
                }

                if (HatVorNachteil(VorNachteil.Flink))
                {
                    ausweichen += 1;
                    if (HatVorNachteil(VorNachteil.FlinkII))
                    {
                        ausweichen += 1;
                    }
                }
                if (HatVorNachteil(VorNachteil.Behäbig))
                {
                    ausweichen -= 1;
                }

                ausweichen += (Math.Max(Talentwert(Talente.Akrobatik) - 9, 0) / 3);
                return ausweichen;
            }
        }

        /// <summary>
        /// Ausweichen-Wert inklusive Akrobatik, Sonderfertigkeiten und Behinderung.
        /// </summary>
        [DependentProperty(nameof(AusweichenOhneMod)),
            DependentProperty(nameof(Behinderung))]
        [DependsOnModifikator(typeof(Mod.IModAusweichen))]
        public int? Ausweichen
        {
            get
            {
                var a = AusweichenOhneMod;
                if (HatVorNachteil(VorNachteil.Flink) && Behinderung >= 5)
                {
                    a -= 1;
                    if (HatVorNachteil(VorNachteil.FlinkII) && Behinderung >= 7)
                    {
                        a -= 1;
                    }
                }
                if (Modifikatoren != null)
                {
                    Modifikatoren
                        .Where(m => m is Mod.IModAusweichen)
                        .Select(m => (Mod.IModAusweichen)m)
                        .OrderBy(m => m.Erstellt)
                        .ToList()
                        .ForEach(m => a = m.ApplyAusweichenMod(a));
                }

                a -= Behinderung;
                return a;
            }
        }

        public List<KampfLogic.Manöver.Manöver> Manöver
        {
            get { return null; }
        }

        public IWunden WundenByZone
        {
            get
            {
                if (kämpferWunden == null)
                {
                    kämpferWunden = new Wunden(this);
                }

                return kämpferWunden;
            }
        }

        IWunden IKämpfer.Wunden
        {
            get
            {
                return WundenByZone;
            }
        }

        public IList<INahkampfwaffe> Angriffswaffen
        {
            get
            {
                return Nahkampfwaffen
                    .Where(w => w.AT > 0)
                    .OrderByDescending(w => w.AT)
                    .Cast<INahkampfwaffe>()
                    .ToList();
            }
        }

        public IList<INahkampfwaffe> Paradewaffen
        {
            get
            {
                return Nahkampfwaffen
                    .Where(w => w.PA > 0)
                    .OrderByDescending(w => w.PA)
                    .Cast<INahkampfwaffe>()
                    .ToList();
            }
        }

        public IList<KämpferNahkampfwaffe> Nahkampfwaffen
        {
            get
            {
                //TODO: Cache?
                //alle Waffen
                var waffen = new List<KämpferNahkampfwaffe>();
                waffen.AddRange(Held_Ausrüstung
                    .Where(ha => ha.Angelegt && ha.Ausrüstung.Waffe != null)
                    .Select(ha => new KämpferNahkampfwaffe(ha)));
                //Raufen, Ringen
                var raufen = new KämpferNahkampfwaffe(this, Waffe.Raufen, GetHeldTalent(Talente.Raufen, false, out var taw));
                waffen.Add(raufen);
                var ringen = new KämpferNahkampfwaffe(this, Waffe.Ringen, GetHeldTalent(Talente.Raufen, false, out taw));
                waffen.Add(ringen);
                return waffen;
            }
        }

        public IList<IFernkampfwaffe> Fernkampfwaffen
        {
            get
            {
                var waffen = new List<IFernkampfwaffe>();
                waffen
                    .AddRange(Held_Ausrüstung
                    .Where(ha => ha.Angelegt && ha.Ausrüstung.Fernkampfwaffe != null)
                    .Select(ha => new KämpferFernkampfwaffe(ha, true)));
                try
                {
                    waffen.Sort((a, b) => ((KämpferFernkampfwaffe)a).AT.CompareTo((b)));
                }
                catch (Exception)
                { return waffen; }
                return waffen;
            }
        }

        public IList<KämpferSchild> Schilde
        {
            get
            {
                var schilde = new List<KämpferSchild>();
                //TODO KämpferSchild muss an die aktuelle Hauptwaffe herankommen können.
                schilde
                    .AddRange(Held_Ausrüstung.Where(ha => ha.Angelegt && ha.Ausrüstung.Schild != null)
                    .Select(ha => new KämpferSchild(ha)));
                //TODO wenn als Waffe verwendet, dann gar nicht erst als schild erstellen - siehe auch KämpferSchild
                // wenn schild in haupthand, und kampfstil PW oder SK, dann nicht in die Liste aufnehmen.
                // infos dafür: Kampfstil und ha.Trageort
                return schilde;
            }
        }

        public System.Windows.Media.Color Farbmarkierung
        {
            get { return _farbmarkierung; }

            set
            {
                _farbmarkierung = value;
                OnChanged(nameof(Farbmarkierung));
            }
        }

        public string HinweisText
        {
            get { return _hinweisText; }

            set
            {
                _hinweisText = value;
                OnChanged(nameof(HinweisText));
                // if (Global.CurrentKampf.BodenplanViewModel.SelectedObject is Wesen) {
                // (Global.CurrentKampf.BodenplanViewModel.SelectedObject as
                // Wesen).ki.Kämpfer.HinweisText = "TEST"; }
            }
        }

        public int Initiative(bool dialog = false)
        {
            // TODO ??: Dialog MVVM-konform aufrufen
            if (dialog)
            {
                var wurf = View.General.ViewHelper.ShowWürfelDialog(InitiativeZufall, "Initiative Würfel-Wurf");
                if (wurf != 0)
                {
                    _initiativeWurf = wurf;
                }
            }
            else
            {
                _initiativeWurf = RandomNumberGenerator.Wurf(InitiativeZufall);
            }

            var be = Behinderung;
            if (HatSonderfertigkeitUndVoraussetzungen(Sonderfertigkeit.RüstungsgewöhnungIII)) //WdS 76
            {
                be = (int)Math.Round(be / 2.0, MidpointRounding.AwayFromZero);
            }

            return InitiativeBasis - be + InitiativeWurf + InitiativeWaffen;
        }

        public int Initiative(int INImanuell)
        {
            _initiativeWurf = INImanuell;

            var be = Behinderung;
            if (HatSonderfertigkeitUndVoraussetzungen(Sonderfertigkeit.RüstungsgewöhnungIII)) //WdS 76
            {
                be = (int)Math.Round(be / 2.0, MidpointRounding.AwayFromZero);
            }

            return InitiativeBasis - be + InitiativeWurf + InitiativeWaffen;
        }

        public int InitiativeMax()
        {
            _initiativeWurf = (int)InitiativeZufall;
            var be = Behinderung;
            if (HatSonderfertigkeitUndVoraussetzungen(Sonderfertigkeit.RüstungsgewöhnungIII)) //WdS 76
            {
                be = (int)Math.Round(be / 2.0, MidpointRounding.AwayFromZero);
            }

            return InitiativeBasis - be + InitiativeWurf;
        }

        // WdS 55
        public int? Orientieren(bool dialog = false)
        {
            // Mit SF Aufmerksamkeit keine Probe nötig
            if (HatSonderfertigkeitUndVoraussetzungen(Sonderfertigkeit.Aufmerksamkeit))
            {
                return InitiativeMax();
            }

            var mod = (int)Math.Floor(Talentwert(Talente.Kriegskunst) / 2.0) * -1;
            Eigenschaft intuition = Eigenschaft(nameof(IN));
            intuition.Modifikator = mod;
            ProbenErgebnis ergebnis;
            if (dialog) // TODO ??: Dialog MVVM-konform aufrufen
            {
                ergebnis = View.General.ViewHelper.ShowProbeDialog(intuition, this);
            }
            else
            {
                ergebnis = intuition.Würfeln();
            }

            if (ergebnis.Gelungen)
            {
                return InitiativeMax();
            }

            return null;
        }

        private int _initiativeWurf = 0;
        private int _mrGeist = -99;
        private Rüstungsschutz _rs = null;
        private Wunden kämpferWunden = null;
        private System.Windows.Media.Color _farbmarkierung = System.Windows.Media.Color.FromArgb(0, 0, 0, 0);
        private string _hinweisText = string.Empty;

        #endregion IKämpfer

        #region Import Export

        /// <summary>
        /// Prüft, ob der Held aus der HeldenSoftware importiert wurde.
        /// </summary>
        public bool IsImportedFromHeldenSoftware
        {
            get
            {
                return HeldGUID.ToString().ToLowerInvariant().StartsWith("4e1d3250-f700-3000-");
            }
        }

        /// <summary>
        /// Prüft, ob der Held aus einem Heldenblatt importiert wurde.
        /// </summary>
        public bool IsImportedFromHeldenblatt
        {
            // TODO: Es gibt noch keine eindeutige Heldenblatt-GUID
            get
            {
                return HeldGUID.ToString().ToLowerInvariant().StartsWith("GUID...");
            }
        }

        public static Held Import(string pfad, bool batch = false)
        {
            return Import(pfad, Guid.Empty, batch);
        }

        /// <summary>
        /// Wenn newGuid nicht Emtpy ist, dann wird der held mit der neuen Guid als Kopie importiert.
        /// </summary>
        /// <returns></returns>
        public static Held Import(string pfad, Guid newGuid, bool batch = false)
        {
            var serialization = Service.SerializationService.GetInstance(!batch);
            Guid heldGuid = serialization.ImportHeld(pfad, newGuid);
            if (heldGuid == Guid.Empty)
            {
                return null;
            }

            if (!batch)
            {
                UpdateLists();
            }

            return Global.ContextHeld.Liste<Held>().Where(h => h.HeldGUID == heldGuid).FirstOrDefault();
        }

        public static void UpdateLists()
        {
            Global.ContextHeld.UpdateList<Held>();
            Global.ContextHeld.UpdateList<Held_Talent>();
            Global.ContextHeld.UpdateList<Held_Zauber>();
            Global.ContextHeld.UpdateList<Held_Ausrüstung>();
            Global.ContextHeld.UpdateList<Held_Inventar>();
            Global.ContextHeld.UpdateList<Held_Munition>();
            Global.ContextHeld.UpdateList<Held_Sonderfertigkeit>();
            Global.ContextHeld.UpdateList<Held_VorNachteil>();
        }

        public bool CheckEnergieständeAbwechend(Held h)
        {
            return (LebensenergieAktuell != h.LebensenergieAktuell)
                || (AusdauerAktuell != h.AusdauerAktuell)
                || (AstralenergieAktuell != h.AstralenergieAktuell)
                || (KarmaenergieAktuell != h.KarmaenergieAktuell)
                || (Wunden != h.Wunden)
                || (WundenArmL != h.WundenArmL)
                || (WundenArmR != h.WundenArmR)
                || (WundenBauch != h.WundenBauch)
                || (WundenBeinL != h.WundenBeinL)
                || (WundenBeinR != h.WundenBeinR)
                || (WundenBrust != h.WundenBrust)
                || (WundenKopf != h.WundenKopf);
        }

        public void Export(string pfad, bool batch = false)
        {
            var serialization = Service.SerializationService.GetInstance(!batch);
            serialization.ExportHeld(HeldGUID, pfad);
        }

        public Held Clone(bool batch = false)
        {
            return Clone(Guid.NewGuid(), batch);
        }

        public Held Clone(Guid newGuid, bool batch = false)
        {
            var serialization = Service.SerializationService.GetInstance(!batch);
            Guid heldGuid = serialization.CloneHeld(HeldGUID, newGuid);
            if (heldGuid == Guid.Empty)
            {
                return null;
            }

            if (!batch)
            {
                UpdateLists();
            }

            return Global.ContextHeld.Liste<Held>().Where(h => h.HeldGUID == heldGuid).FirstOrDefault();
        }

        #endregion Import Export

        #region Sonstiges

        [DependentProperty(nameof(UpdateHinweis))]
        public bool HatUpdateHinweis
        {
            get
            {
                return !string.IsNullOrWhiteSpace(UpdateHinweis);
            }
        }

        [DependentProperty(nameof(Name))]
        public string Kurzname
        {
            get
            {
                var namenTeile = Name.Trim().Split(' ');
                if (namenTeile.Length > 0)
                {
                    return namenTeile[0];
                }
                else
                {
                    return Name;
                }
            }
        }

        [DependentProperty(nameof(Kampfwerte))]
        public string Bemerkung
        {
            get { return Kampfwerte; }

            set
            {
                Kampfwerte = value;
                OnChanged(nameof(Bemerkung));
            }
        }

        public override string ToString()
        {
            return Name;
        }

        #endregion Sonstiges

        #region IHasWunden

        int IHasWunden.Wunden
        {
            get
            {
                return Wunden ?? 0;
            }

            set
            {
                Wunden = value;
            }
        }

        #endregion IHasWunden

        #region Inventar

        #region //Felder

        private int? ueberlastung = null;
        private double? gewicht = null;
        private double? gewichtZuTragkraftProzent = null;

        #endregion //Felder

        #region //Eigenschaften

        [DependentProperty(nameof(Körperkraft))]
        public int Tragkraft
        {
            get { return Körperkraft * 40; }
        }

        public int Behinderung
        {
            get
            {
                if (E.MitUeberlastung && E.UeberlastungBerechnung == 0)
                {
                    return (BE ?? 0) + (ueberlastung ?? 0);
                }
                else
                {
                    return (BE ?? 0);
                }
            }

            set { BE = value; }
        }

        public int Ueberlastung
        {
            get
            {
                if (ueberlastung == null)
                {
                    BerechneUeberlastung();
                }

                return ueberlastung ?? 0;
            }

            set
            {
                if (E.MitUeberlastung == false)
                {
                    ueberlastung = 0;
                }
                else
                {
                    if (E.MitUeberlastung && E.UeberlastungBerechnung == 1)
                    {
                        Behinderung = ((BE ?? 0) - ((ueberlastung ?? 0) - value));
                        ueberlastung = value;
                    }
                    else
                    {
                        ueberlastung = value;
                        if (E.UeberlastungBerechnung == 0)
                        {
                            BerechneBehinderung();
                        }
                    }
                }
                OnChanged(nameof(Ueberlastung));
            }
        }

        public double Gewicht
        {
            get
            {
                if (gewicht == null)
                {
                    BerechneAusruestungsGewicht();
                }

                return gewicht ?? 0.0;
            }

            set
            {
                gewicht = value;
                GewichtZuTragkraftProzent = ((Gewicht / Tragkraft) * 100);
                OnChanged(nameof(Gewicht));
            }
        }

        public double GewichtZuTragkraftProzent
        {
            get
            {
                if (gewichtZuTragkraftProzent == null)
                {
                    GewichtZuTragkraftProzent = ((Gewicht / Tragkraft) * 100);
                }

                return Math.Round(gewichtZuTragkraftProzent ?? 0.0, 2, MidpointRounding.AwayFromZero);
            }

            set
            {
                gewichtZuTragkraftProzent = value;
                if (E.MitUeberlastung && E.UeberlastungBerechnung == 0)
                {
                    BerechneUeberlastung();
                }
                OnChanged(nameof(GewichtZuTragkraftProzent));
            }
        }

        #endregion //Eigenschaften

        #region //Public Methoden

        /// <summary>
        /// Berechnet Behinderung (Held.Behinderung) anhand der Ausrüstung + Ueberlastung
        /// </summary>
        /// <returns></returns>
        public int BerechneBehinderung()
        {
            var retVal = 0;
            foreach (Held_Ausrüstung ruestung in Held_Ausrüstung.Where(ha => ha.Angelegt && ha.Ausrüstung.Rüstung != null))
            {
                var rsname = ruestung.Ausrüstung.Name;
                if (ruestung.Ausrüstung.BasisAusrüstung != null)
                {
                    rsname = ruestung.Ausrüstung.BasisAusrüstung;
                }

                var be = (ruestung.Ausrüstung.Rüstung.BE ?? 0);
                if (HatSonderfertigkeitUndVoraussetzungen(Sonderfertigkeit.RüstungsgewöhnungIII))
                {
                    be -= 2;
                }
                else if (HatSonderfertigkeitUndVoraussetzungen(Sonderfertigkeit.RüstungsgewöhnungII))
                {
                    be -= 1;
                }
                else if (HatSonderfertigkeitUndVoraussetzungen(Sonderfertigkeit.RüstungsgewöhnungI, rsname, false))
                {
                    be -= 1;
                }

                retVal += Math.Max(be, 0);
            }
            if (E.MitUeberlastung)
            {
                Behinderung = retVal + Ueberlastung;
            }
            else
            {
                Behinderung = retVal;
            }
            return retVal;
        }

        /// <summary>
        /// Berechnet die aktuelle Ueberlastung (Held.Ueberlastung) anhand des GewichtZuTragkraftProzent
        /// </summary>
        public double BerechneUeberlastung()
        {
            int retVal;
            if (GewichtZuTragkraftProzent / 50 - 2 > 0)
            {
                retVal = Convert.ToInt32(Math.Floor(GewichtZuTragkraftProzent / 50 - 2 + 1));
            }
            else
            {
                retVal = 0;
            }
            Ueberlastung = retVal;
            //OnChanged("Ueberlastung));
            return retVal;
        }

        /// <summary>
        /// Berechnet das aktuelle Gewicht (Held.Gewicht) anhand aller Gegenstände neu
        /// </summary>
        public double BerechneAusruestungsGewicht()
        {
            var g = 0.0;
            foreach (Held_Ausrüstung ha in Held_Ausrüstung)
            {
                if (ha.SpezGewicht != null && ha.SpezGewicht.Value == 0)
                {
                    ha.SpezGewicht = null;
                }
                if (ha.Trageort == null && ha.TrageortGUID != Guid.Empty)
                {
                    Global.ContextHeld.Update<Held>(this);
                    ha.Trageort = Global.ContextInventar.TrageortListe.Where(item => item.TrageortGUID == ha.TrageortGUID).FirstOrDefault();
                }
                var effGewicht = ha.Trageort.TragkraftFaktor * (ha.SpezGewicht == null ? ha.Ausrüstung.Gewicht : ha.SpezGewicht.Value);
                if (ha.Ausrüstung.Rüstung != null && ha.Angelegt)
                {
                    effGewicht /= 2.0;
                }

                g += effGewicht;
            }
            foreach (Held_Inventar hi in Held_Inventar)
            {
                g += (hi.Trageort.TragkraftFaktor * (hi.Inventar.Gewicht ?? 0) * (hi.Anzahl ?? 0)) / (hi.Angelegt? 2:1);
            }
            foreach (Held_Munition hm in Held_Munition)
            {
                g += hm.Trageort.TragkraftFaktor * (hm.Fernkampfwaffe.Munitionsgewicht ?? 0) * (hm.Anzahl ?? 0);
            }
            Gewicht = g;
            return g;
        }

        /// <summary>
        /// Berechnet die Rüstungswerte (Held.RS) des Helden anhand der angelegten Ausrüstung neu.
        /// </summary>
        public void BerechneRüstungswerte()
        {
            var zonenRüstung = E.RSBerechnung == (int)ViewModel.Settings.ermittleRuestung.AutomatischZonen
                                || E.RSBerechnung == (int)ViewModel.Settings.ermittleRuestung.Zonen;
            IRüstungsschutz rs = new RüstungsWerte();
            var einfacherRs = 0;
            foreach (Held_Ausrüstung ha in Held_Ausrüstung.Where(h_a => h_a.Ausrüstung.Rüstung != null))
            {
                if (ha.Angelegt)
                {
                    //Hier könnte man noch Rüstungskombinationen beachten (wenn man zu viel Zeit hat)
                    if (zonenRüstung)
                    {
                        rs = ha.Ausrüstung.Rüstung + rs;
                    }
                    else
                    {
                        einfacherRs += ha.Ausrüstung.Rüstung.RS ?? 0;
                    }
                }
            }
            if (zonenRüstung)
            {
                RS.SetValues(rs);
            }
            else
            {
                RS[Trefferzone.Gesamt] = einfacherRs;
            }
        }

        public Held_Ausrüstung AddAusrüstung(Ausrüstung a, bool detached = false)
        {
            var ha = new Held_Ausrüstung();
            if (!detached)
            {
                ha = Global.ContextHeld.New<Held_Ausrüstung>();
            }

            ha.HeldGUID = HeldGUID;
            ha.Angelegt = false;
            ha.TrageortGUID = Trageorte.Rucksack;
            if (!detached)
            {
                ha.Trageort = Global.ContextInventar.TrageortListe.Where(item => item.TrageortGUID == ha.TrageortGUID).FirstOrDefault();
            }

            if (a.Waffe != null || a.Schild != null)
            {
                var bfa = new Held_BFAusrüstung();
                if (!detached)
                {
                    bfa = Global.ContextHeld.New<Held_BFAusrüstung>();
                }

                bfa.HeldAusrüstungGUID = ha.HeldAusrüstungGUID;
                var bf = 0;
                if (a.Waffe != null)
                {
                    var hw = new Held_Waffe();
                    if (!detached)
                    {
                        hw = Global.ContextHeld.New<Held_Waffe>();
                    }

                    hw.WaffeGUID = a.Waffe.WaffeGUID;
                    if (!detached)
                    {
                        hw.Waffe = a.Waffe;
                    }

                    hw.TPBonus = a.Waffe.TPBonus;
                    hw.WMAT = a.Waffe.WMAT ?? 0;
                    hw.WMPA = a.Waffe.WMPA ?? 0;
                    hw.INI = a.Waffe.INI ?? 0;
                    bf = a.Waffe.BF ?? 0;
                    Talent t = Held_Waffe.GetBestesTalent(this, a.Waffe);
                    if (t != null)
                    {
                        hw.TalentGUID = t.TalentGUID;
                    }

                    if (!detached)
                    {
                        hw.Talent = t;
                    }

                    hw.HeldAusrüstungGUID = bfa.HeldAusrüstungGUID;
                    bfa.Held_Waffe = hw;
                }
                if (a.Schild != null)
                {
                    bf = a.Schild.BF;
                    if (!detached)
                    {
                        bfa.Schild = a.Schild;
                    }
                    else
                    {
                        var s = new Schild
                        {
                            SchildGUID = a.Schild.SchildGUID
                        };
                        bfa.Schild = s;
                    }
                }
                bfa.StartBF = bfa.BF = bf;
                ha.Held_BFAusrüstung = bfa;
            }
            if (a.Fernkampfwaffe != null)
            {
                var hf = new Held_Fernkampfwaffe();
                if (!detached)
                {
                    hf = Global.ContextHeld.New<Held_Fernkampfwaffe>();
                }

                hf.FernkampfwaffeGUID = a.Fernkampfwaffe.FernkampfwaffeGUID;
                if (!detached)
                {
                    hf.Fernkampfwaffe = a.Fernkampfwaffe;
                }

                Talent t = Held_Fernkampfwaffe.GetBestesTalent(this, a.Fernkampfwaffe);
                if (t != null)
                {
                    hf.TalentGUID = t.TalentGUID;
                }

                if (!detached)
                {
                    hf.Talent = t;
                }

                if (a.Fernkampfwaffe.Ausrüstung.Bemerkung != null && a.Fernkampfwaffe.Ausrüstung.Bemerkung.Length > 17)
                {
                    int fkLeichter = 0;
                    if (int.TryParse(a.Fernkampfwaffe.Ausrüstung.Bemerkung.Substring(17, 1), out fkLeichter))
                        hf.FKErleichterung = fkLeichter;
                    else
                        hf.FKErleichterung = 0;
                }
                else
                    hf.FKErleichterung = 0;

                if (a.Fernkampfwaffe.Ausrüstung.Bemerkung != null && a.Fernkampfwaffe.Ausrüstung.Bemerkung.Contains("KK-Erleichterung"))
                    hf.KKErleichterung = true;
                else
                    hf.KKErleichterung = false;
                hf.HeldAusrüstungGUID = ha.HeldAusrüstungGUID;
                ha.Held_Fernkampfwaffe = hf;
            }
            if (a.Rüstung != null)
            {
                var hr = new Held_Rüstung();
                if (!detached)
                {
                    hr = Global.ContextHeld.New<Held_Rüstung>();
                }

                hr.RüstungGUID = a.Rüstung.RüstungGUID;
                if (!detached)
                {
                    hr.Rüstung = a.Rüstung;
                }

                var struktur = (a.Rüstung.RS ?? 0) * 10; //ohne zonen
                if (E.RSBerechnung >= 2)
                {
                    struktur = (int)Math.Ceiling((a.Rüstung.gRS ?? 0) * 10); //Trefferzonenmodell
                }

                hr.StartStrukturpunkte = hr.Strukturpunkte = struktur;
                hr.HeldAusrüstungGUID = ha.HeldAusrüstungGUID;
                ha.Held_Rüstung = hr;
            }
            Held_Ausrüstung.Add(ha);
            return ha;
        }

        public void RemoveAusrüstung(Held_Ausrüstung ha, bool detached = false)
        {
            Held_Ausrüstung.Remove(ha);
            if (!detached)
            {
                Global.ContextHeld.Delete(ha);
            }
        }

        // TODO: Diese Add-Logik sollte mit dem Importer und dem InventarViewModel homogenisiert werden, sodass alle Stellen diese Methode verwenden
        public Held_Inventar AddInventar(IHandelsgut gegenstand, int anzahl = 1)
        {
            Held_Inventar hi = null;
            // In Held_Ausrüstung oder Held_inventar hinzufügen, bzw anzahl erhöhen.
            if (gegenstand is IAusrüstung)
            {
                var a = (IAusrüstung)gegenstand;
                AddAusrüstung(a.Ausrüstung);
            }
            else if (gegenstand is Handelsgut)
            {
                var h = (Handelsgut)gegenstand;
                Inventar i = Global.ContextHeld.Liste<Inventar>()
                    .Where(li => li.HandelsgutGUID == h.HandelsgutGUID && li.Name == h.Name)
                    .FirstOrDefault();

                if (i == null)
                {
                    if (h != null)
                    {
                        i = Global.ContextHeld.New<Inventar>();
                        i.Name = h.Name;
                        i.Tags = h.Tags;
                        i.Preis = h.Preis;
                        i.ME = h.ME;
                        i.Literatur = h.Literatur;
                        i.Kategorie = h.Kategorie;
                        i.HandelsgutGUID = h.HandelsgutGUID;
                        i.Gewicht = h.Gewicht;
                        i.Bemerkung = h.Bemerkung;
                        Global.ContextHeld.Insert(i);
                    }
                }

                hi = Held_Inventar.Where(hhi => hhi.InventarGUID == i.InventarGUID).FirstOrDefault();

                if (hi == null)
                {
                    hi = Global.ContextHeld.New<Held_Inventar>();
                    hi.HeldGUID = HeldGUID;
                    hi.InventarGUID = i.InventarGUID;
                    hi.Anzahl = anzahl;
                    hi.Angelegt = false;
                    hi.TrageortGUID = Trageorte.Rucksack;
                    hi.Trageort = Global.ContextInventar.TrageortListe.Where(item => item.TrageortGUID == hi.TrageortGUID).FirstOrDefault();
                    hi.Inventar = i;
                    Held_Inventar.Add(hi);
                }
                else
                {
                    hi.Anzahl += anzahl;
                    hi = null;
                }
            }

            BerechneAusruestungsGewicht();
            return hi;
        }

        #endregion //Public Methoden

        #endregion Inventar

        #region IDisposable

        public void Dispose()
        {
            PropertyChanged -= DependentProperty.PropagateINotifyProperyChanged;
            PropertyChanged -= Held_PropertyChanged;
        }

        #endregion IDisposable
    }
}
