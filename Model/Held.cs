using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// Eigene Usings
using MeisterGeister.Logic.General;
using MeisterGeister.ViewModel.Kampf.Logic;
using KampfLogic = MeisterGeister.ViewModel.Kampf.Logic;
using Mod = MeisterGeister.ViewModel.Kampf.Logic.Modifikatoren;
using System.ComponentModel;
using DependentProperty = MeisterGeister.Model.Extensions.DependentProperty;
using MeisterGeister.Logic.Extensions;
using MeisterGeister.ViewModel.Helden.Logic;

namespace MeisterGeister.Model
{
    // Man kann Superklassen hinzufügen. Es sollten jedoch nicht die gleichen Eigenschaften, wie in der Datenbankklasse existieren.
    public partial class Held : ViewModel.Kampf.Logic.Wesen, KampfLogic.IKämpfer, Extensions.IInitializable, KampfLogic.IHasZonenRs, IHasWunden
    {
        public Held() : base()
        {
            HeldGUID = Guid.NewGuid();
            PropertyChanged += DependentProperty.PropagateINotifyProperyChanged;
            SetDefaultValues();
        }

        private void SetDefaultValues()
        {
            Name = "Alrik";
            AktiveHeldengruppe = true;
            MU = 8; KL = 8; IN = 8; CH = 8; FF = 8; GE = 8; KO = 8; KK = 8;
            LE_Aktuell = 12; AU_Aktuell = 12;
        }

        #region IInitializable
        private bool _isInitialized = false;
        public void Initialize()
        {
            if (_isInitialized)
                return;
            kämpferWunden = new KampfLogic.Wunden((Model.Held)this);
            //WundenByZone.UpdateWundenModifikatoren(); // das würde es zweimal durchlaufen
            _isInitialized = true;
        }
        #endregion

        [DependentProperty("Name")]
        public string Kurzname
        {
            get
            {
                string[] namenTeile = Name.Trim().Split(' ');
                if (namenTeile.Length > 0)
                    return namenTeile[0];
                else
                    return Name;
            }
        }

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
                list.AddRange(ModifikatorenListe(typeof(Mod.IModAT), list.Count() == 0 ? 0 : list.LastOrDefault().Wert));
                return list;
            }
        }
        [DependsOnModifikator(typeof(Mod.IModPA))]
        public List<dynamic> ModifikatorenListePA
        {
            get
            {
                List<dynamic> list = ModifikatorenListe(typeof(Mod.IModPABasis), ParadeBasisOhneMod);
                list.AddRange(ModifikatorenListe(typeof(Mod.IModPA), list.Count() == 0 ? 0 : list.LastOrDefault().Wert));
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

        #endregion

        #region Eigenschaften
        [DependentProperty("MU")]
        [DependsOnModifikator(typeof(Mod.IModMU))]
        public int Mut
        {
            get
            {
                int mu = MU ?? 8;
                if(Modifikatoren!=null)
                    Modifikatoren.Where(m => m is Mod.IModMU).Select(m => (Mod.IModMU)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => mu = m.ApplyMUMod(mu));
                return mu;
            }
        }

        [DependentProperty("KL")]
        [DependsOnModifikator(typeof(Mod.IModKL))]
        public int Klugheit
        {
            get
            {
                int e = KL ?? 8;
                if (Modifikatoren != null)
                    Modifikatoren.Where(m => m is Mod.IModKL).Select(m => (Mod.IModKL)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => e = m.ApplyKLMod(e));
                return e;
            }
        }

        [DependentProperty("IN")]
        [DependsOnModifikator(typeof(Mod.IModIN))]
        public int Intuition
        {
            get
            {
                int e = IN ?? 8;
                if (Modifikatoren != null)
                    Modifikatoren.Where(m => m is Mod.IModIN).Select(m => (Mod.IModIN)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => e = m.ApplyINMod(e));
                return e;
            }
        }

        [DependentProperty("CH")]
        [DependsOnModifikator(typeof(Mod.IModCH))]
        public int Charisma
        {
            get
            {
                int e = CH ?? 8;
                if (Modifikatoren != null)
                    Modifikatoren.Where(m => m is Mod.IModCH).Select(m => (Mod.IModCH)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => e = m.ApplyCHMod(e));
                return e;
            }
        }

        [DependentProperty("FF")]
        [DependsOnModifikator(typeof(Mod.IModFF))]
        public int Fingerfertigkeit
        {
            get
            {
                int e = FF ?? 8;
                if (Modifikatoren != null)
                    Modifikatoren.Where(m => m is Mod.IModFF).Select(m => (Mod.IModFF)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => e = m.ApplyFFMod(e));
                return e;
            }
        }

        [DependentProperty("GE")]
        [DependsOnModifikator(typeof(Mod.IModGE))]
        public int Gewandtheit
        {
            get
            {
                int e = GE ?? 8;
                if (Modifikatoren != null)
                    Modifikatoren.Where(m => m is Mod.IModGE).Select(m => (Mod.IModGE)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => e = m.ApplyGEMod(e));
                return e;
            }
        }

        [DependentProperty("KO")]
        [DependsOnModifikator(typeof(Mod.IModKO))]
        public int Konstitution
        {
            get
            {
                int e = KO ?? 8;
                if (Modifikatoren != null)
                    Modifikatoren.Where(m => m is Mod.IModKO).Select(m => (Mod.IModKO)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => e = m.ApplyKOMod(e));
                return e;
            }
        }

        [DependentProperty("KK")]
        [DependsOnModifikator(typeof(Mod.IModKK))]
        public int Körperkraft
        {
            get
            {
                int e = KK ?? 8;
                if (Modifikatoren != null)
                    Modifikatoren.Where(m => m is Mod.IModKK).Select(m => (Mod.IModKK)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => e = m.ApplyKKMod(e));
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
            switch (eigenschaft)
            {
                case "MU":
                case "Mut":
                    return ohneMod ? MU ?? 0 : Mut;
                case "KL":
                case "Klugheit":
                    return ohneMod ? KL ?? 0 : Klugheit;
                case "IN":
                case "Intuition":
                    return ohneMod ? IN ?? 0 : Intuition;
                case "CH":
                case "Charisma":
                    return ohneMod ? CH ?? 0 : Charisma;
                case "FF":
                case "Fingerfertigkeit":
                    return ohneMod ? FF ?? 0 : Fingerfertigkeit;
                case "GE":
                case "Gewandtheit":
                    return ohneMod ? GE ?? 0 : Gewandtheit;
                case "KO":
                case "Konstitution":
                    return ohneMod ? KO ?? 0 : Konstitution;
                case "KK":
                case "Körperkraft":
                    return ohneMod ? KK ?? 0 : Körperkraft;
                case "SO":
                case "Sozialstatus":
                    return SO ?? 0;
                default:
                    return 0;
            }
        }

        /// <summary>
        /// Gibt die Eigenschaft zurück. ACHTUNG: Änderungen am Wert wirken sich nicht auf den DataMember aus!
        /// </summary>
        /// <param name="eigenschaft">Name oder Abkürzung der gewünschten Eigenschaft.</param>
        /// <param name="ohneMod">'True' falls der unmodifizierte Wert gewünscht ist.</param>
        /// <returns>Eigenschaft.</returns>
        public Eigenschaft Eigenschaft(string eigenschaft, bool ohneMod = false)
        {
            return new Eigenschaft(eigenschaft, EigenschaftWert(eigenschaft, ohneMod), this);
        }

        #endregion

        #region BaseEigenschaften / Für die Berechnung von abgeleiteten Werten
        [DependentProperty("MU")]
        [DependsOnModifikator(typeof(Mod.IModMU))]
        public int BaseMU
        {
            get
            {
                int mu = MU ?? 8;
                if (Modifikatoren != null)
                    Modifikatoren.Where(m => m is Mod.IModMitAuswirkungAufBerechneteWerte && m is Mod.IModMU).Select(m => (Mod.IModMU)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => mu = m.ApplyMUMod(mu));
                return mu;
            }
        }
        [DependentProperty("KL")]
        [DependsOnModifikator(typeof(Mod.IModKL))]
        public int BaseKL
        {
            get
            {
                int e = KL ?? 8;
                if (Modifikatoren != null)
                    Modifikatoren.Where(m => m is Mod.IModMitAuswirkungAufBerechneteWerte && m is Mod.IModKL).Select(m => (Mod.IModKL)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => e = m.ApplyKLMod(e));
                return e;
            }
        }
        [DependentProperty("IN")]
        [DependsOnModifikator(typeof(Mod.IModIN))]
        public int BaseIN
        {
            get
            {
                int e = IN ?? 8;
                if (Modifikatoren != null)
                    Modifikatoren.Where(m => m is Mod.IModMitAuswirkungAufBerechneteWerte && m is Mod.IModIN).Select(m => (Mod.IModIN)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => e = m.ApplyINMod(e));
                return e;
            }
        }
        [DependentProperty("CH")]
        [DependsOnModifikator(typeof(Mod.IModCH))]
        public int BaseCH
        {
            get
            {
                int e = CH ?? 8;
                if (Modifikatoren != null)
                    Modifikatoren.Where(m => m is Mod.IModMitAuswirkungAufBerechneteWerte && m is Mod.IModCH).Select(m => (Mod.IModCH)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => e = m.ApplyCHMod(e));
                return e;
            }
        }
        [DependentProperty("FF")]
        [DependsOnModifikator(typeof(Mod.IModFF))]
        public int BaseFF
        {
            get
            {
                int e = FF ?? 8;
                if (Modifikatoren != null)
                    Modifikatoren.Where(m => m is Mod.IModMitAuswirkungAufBerechneteWerte && m is Mod.IModFF).Select(m => (Mod.IModFF)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => e = m.ApplyFFMod(e));
                return e;
            }
        }
        [DependentProperty("GE")]
        [DependsOnModifikator(typeof(Mod.IModGE))]
        public int BaseGE
        {
            get
            {
                int e = GE ?? 8;
                if (Modifikatoren != null)
                    Modifikatoren.Where(m => m is Mod.IModMitAuswirkungAufBerechneteWerte && m is Mod.IModGE).Select(m => (Mod.IModGE)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => e = m.ApplyGEMod(e));
                return e;
            }
        }
        [DependentProperty("KO")]
        [DependsOnModifikator(typeof(Mod.IModKO))]
        public int BaseKO
        {
            get
            {
                int e = KO ?? 8;
                if (Modifikatoren != null)
                    Modifikatoren.Where(m => m is Mod.IModMitAuswirkungAufBerechneteWerte && m is Mod.IModKO).Select(m => (Mod.IModKO)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => e = m.ApplyKOMod(e));
                return e;
            }
        }
        [DependentProperty("KK")]
        [DependsOnModifikator(typeof(Mod.IModKK))]
        public int BaseKK
        {
            get
            {
                int e = KK ?? 8;
                if (Modifikatoren != null)
                    Modifikatoren.Where(m => m is Mod.IModMitAuswirkungAufBerechneteWerte && m is Mod.IModKK).Select(m => (Mod.IModKK)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => e = m.ApplyKKMod(e));
                return e;
            }
        }
        #endregion

        #region Lebensenergie
        [DependentProperty("BaseKO"), DependentProperty("BaseKK")]
        public int LebensenergieBasis
        {
            get
            {
                return (int)Math.Round((BaseKO * 2 + BaseKK) / 2.0, 0, MidpointRounding.AwayFromZero);
            }
        }
        [DependentProperty("LE_Mod")]
        public int LebensenergieMod
        {
            get { return LE_Mod ?? 0; }
            set
            {
                LE_Mod = value;
                OnChanged("LebensenergieMod");
            }
        }
        [DependentProperty("LE_Aktuell")]
        public int LebensenergieAktuell
        {
            get
            {
                return LE_Aktuell ?? 0;
            }
            set
            {
                LE_Aktuell = value;
            }
        }
        [DependentProperty("LebensenergieBasis"), DependentProperty("LebensenergieMod")]
        public int LebensenergieMax
        {
            get
            {
                int le = LebensenergieBasis + LebensenergieMod;
                if (Modifikatoren != null)
                    Modifikatoren.Where(m => m is Mod.IModLE).Select(m => (Mod.IModLE)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => le = m.ApplyLEMod(le));
                return le;
            }
        }

        [DependentProperty("LebensenergieAktuell"), DependentProperty("LebensenergieMax"), DependentProperty("Konstitution")]
        public string LebensenergieStatus
        {
            get
            {
                return GetLebensenergieStatus();
            }
        }
        #endregion

        #region Ausdauer

        [DependentProperty("BaseMU"), DependentProperty("BaseKO"), DependentProperty("BaseGE")]
        public int AusdauerBasis
        {
            get
            {
                return (int)Math.Round((BaseMU + BaseKO + BaseGE) / 2.0, 0, MidpointRounding.AwayFromZero);
            }
        }
        [DependentProperty("AU_Aktuell")]
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
        [DependentProperty("AU_Mod")]
        public int AusdauerMod
        {
            get { return AU_Mod ?? 0; }
            set 
            {
                AU_Mod = value;
                //OnPropertyChanged(string.Empty);
            }
        }
        [DependentProperty("AusdauerBasis"), DependentProperty("AusdauerMod")]
        [DependsOnModifikator(typeof(Mod.IModAU))]
        public int AusdauerMax
        {
            get
            {
                int e = AusdauerBasis + AusdauerMod;
                if (Modifikatoren != null)
                    Modifikatoren.Where(m => m is Mod.IModAU).Select(m => (Mod.IModAU)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => e = m.ApplyAUMod(e));
                return e;
            }
        }

        [DependentProperty("AusdauerAktuell"), DependentProperty("AusdauerMax")]
        public string AusdauerStatus
        {
            get
            {
                return GetAusdauerStatus();
            }
        }
        #endregion

        #region Karmaenergie

        public bool Geweiht
        {
            get
            {
                return HatVorNachteil(VorNachteil.GeweihtZwölfgöttlicheKirche) || HatVorNachteil(VorNachteil.GeweihtNichtAlveranischeGottheit) || HatVorNachteil(VorNachteil.GeweihtHRanga) || HatVorNachteil(VorNachteil.GeweihtGravesh) || HatVorNachteil(VorNachteil.GeweihtAngrosch) || HatVorNachteil(VorNachteil.Sacerdos) || 
                    HatSonderfertigkeitUndVoraussetzungen(Sonderfertigkeit.SpätweiheAlveranischeGottheit) || HatSonderfertigkeitUndVoraussetzungen(Sonderfertigkeit.SpätweiheNamenloser) || HatSonderfertigkeitUndVoraussetzungen(Sonderfertigkeit.SpätweiheNichtAlveranischeGottheit) || HatSonderfertigkeitUndVoraussetzungen(Sonderfertigkeit.KontaktZumGroßenGeist) || HatSonderfertigkeitUndVoraussetzungen(Sonderfertigkeit.SpätweiheDunkleZeiten);
            }
        }

        [DependentProperty("KE_Aktuell")]
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

        [DependentProperty("KE_Mod")]
        public int KarmaenergieMod
        {
            get {
                //if(Geweiht) // hmm erstmal nicht
                    return KE_Mod ?? 0;
                //return 0;
            }
            set
            {
                KE_Mod = value;
                //OnPropertyChanged(string.Empty);
            }
        }

        [DependsOnModifikator(typeof(Mod.IModKE))]
        [DependentProperty("KarmaenergieMod")]
        public int KarmaenergieMax
        {
            get
            {
                int e = KarmaenergieMod;
                if (Modifikatoren != null)
                    Modifikatoren.Where(m => m is Mod.IModKE).Select(m => (Mod.IModKE)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => e = m.ApplyKEMod(e));
                return e;
            }
        }

        #endregion

        #region Astralenergie

        public bool Magiebegabt
        {
            get
            {
                return HatVorNachteil(VorNachteil.Vollzauberer) || HatVorNachteil(VorNachteil.Halbzauberer) || HatVorNachteil(VorNachteil.Viertelzauberer) || HatVorNachteil(VorNachteil.ViertelzaubererUnbewusst);
            }
        }

        [DependentProperty("BaseMU"), DependentProperty("BaseIN"), DependentProperty("BaseCH")]
        public int AstralenergieBasis
        {
            get
            {
                int basis = BaseMU + BaseIN + BaseCH;
                if (HatSonderfertigkeitUndVoraussetzungen(Sonderfertigkeit.GefäßDerSterne))
                    basis += BaseCH;
                return (int)Math.Round(basis / 2.0, 0, MidpointRounding.AwayFromZero);
            }
        }

        [DependentProperty("AE_Aktuell")]
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

        [DependentProperty("AE_Mod")]
        public int AstralenergieMod
        {
            get
            {
                return AE_Mod ?? 0;
            }
            set
            {
                AE_Mod = value;
                //OnPropertyChanged(string.Empty);
            }
        }

        [DependsOnModifikator(typeof(Mod.IModAE))]
        [DependentProperty("AstralenergieBasis"), DependentProperty("AstralenergieMod")]
        public int AstralenergieMax
        {
            get
            {
                // wenn er einen Zauberervorteil hat, sonst 0
                if (Magiebegabt)
                {
                    int e = AstralenergieBasis + AstralenergieMod;
                    if (Modifikatoren != null)
                        Modifikatoren.Where(m => m is Mod.IModAE).Select(m => (Mod.IModAE)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => e = m.ApplyAEMod(e));
                    return e;
                }
                return 0;
            }
        }

        #endregion

        #region Magieresistenz

        public int MagieresistenzBasis
        {
            get
            {
                return (int)Math.Round((BaseMU + BaseKL + BaseKO) / 5.0, 0, MidpointRounding.AwayFromZero);
            }
        }

        [DependentProperty("MR_Mod")]
        public int MagieresistenzMod
        {
            get
            {
                return MR_Mod ?? 0;
            }
            set
            {
                MR_Mod = value;
                //OnPropertyChanged(string.Empty);
            }
        }

        [DependentProperty("MR_Mod"), DependentProperty("MR"), DependentProperty("KL"), DependentProperty("KO")]
        public int MagieresistenzOhneMod
        {
            get
            {
                return MagieresistenzBasis + MagieresistenzMod;
            }
        }

        [DependentProperty("MR_Mod"), DependentProperty("MR"), DependentProperty("KL"), DependentProperty("KO")]
        [DependsOnModifikator(typeof(Mod.IModMR))]
        public int Magieresistenz
        {
            get
            {
                //TODO ??: Aurapanzer etc.
                int e = MagieresistenzOhneMod;
                if (Modifikatoren != null)
                    Modifikatoren.Where(m => m is Mod.IModMR).Select(m => (Mod.IModMR)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => e = m.ApplyMRMod(e));
                return e;
            }
        }

        #endregion

        #region Wundschwellen
        [DependentProperty("Wundschwelle"), DependentProperty("Wundschwelle2"), DependentProperty("Wundschwelle3")]
        public string Wundschwellen
        {
            get
            {
                return string.Format("{0} / {1} / {2}", Wundschwelle, Wundschwelle2, Wundschwelle3);
            }
        }

        [DependentProperty("Konstitution")]
        public int Wundschwelle
        {
            get
            {
                int ko = Konstitution;
                int ws = Convert.ToInt32(Math.Round(ko / 2.0, 0, MidpointRounding.AwayFromZero));
                if (HatVorNachteil(VorNachteil.Eisern))
                    ws += 2;
                if (HatVorNachteil(VorNachteil.Glasknochen))
                    ws -= 2;
                return ws;
            }
        }

        [DependentProperty("Konstitution")]
        public int Wundschwelle2
        {
            get
            {
                int ko = Konstitution;
                int ws = ko;
                if (HatVorNachteil(VorNachteil.Eisern))
                    ws += 2;
                if (HatVorNachteil(VorNachteil.Glasknochen))
                    ws -= 2;
                return ws;
            }
        }

        [DependentProperty("Konstitution")]
        public int Wundschwelle3
        {
            get
            {
                int ko = Konstitution;
                int ws = Convert.ToInt32(Math.Round(ko * 1.5, 0, MidpointRounding.AwayFromZero));
                if (HatVorNachteil(VorNachteil.Eisern))
                    ws += 2;
                if (HatVorNachteil(VorNachteil.Glasknochen))
                    ws -= 2;
                return ws;
            }
        }
        #endregion

        #region Initiative

        public int InitiativeBasisOhneSonderfertigkeiten
        {
            get
            {
                return (int)Math.Round((BaseMU * 2 + BaseIN + BaseGE) / 5.0, 0, MidpointRounding.AwayFromZero) + InitiativeModGen;
            }
        }

        [DependentProperty("INI_Mod"), DependentProperty("MU"), DependentProperty("IN"), DependentProperty("GE")]
        public int InitiativeBasisOhneMod
        {
            get
            {
                // berechneter Basiswert
                int ini = InitiativeBasisOhneSonderfertigkeiten;

                // Sonderfertigkeiten
                if (HatSonderfertigkeitUndVoraussetzungen(Sonderfertigkeit.Kampfreflexe) && Behinderung <= 4)
                    ini += 4;
                if (HatSonderfertigkeitUndVoraussetzungen(Sonderfertigkeit.Kampfgespür))
                    ini += 2;

                return ini;
            }
        }

        [DependentProperty("InitiativeBasisOhneMod")]
        [DependsOnModifikator(typeof(Mod.IModINIBasis))]
        public int InitiativeBasis
        {
            get
            {
                int ini = InitiativeBasisOhneMod;
                if (Modifikatoren != null)
                    Modifikatoren.Where(m => m is Mod.IModINIBasis).Select(m => (Mod.IModINIBasis)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => ini = m.ApplyINIBasisMod(ini));
                return ini;
            }
        }

        [DependentProperty("INI_Mod")]
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

        #endregion

        #region Attacke/Parade

        [DependentProperty("BaseMU"), DependentProperty("BaseGE"), DependentProperty("BaseKK")]
        public int AttackeBasisOhneMod
        {
            get
            {
                return (int)Math.Round((BaseMU + BaseGE + BaseKK) / 5.0, 0, MidpointRounding.AwayFromZero);
            }
        }

        [DependentProperty("AttackeBasisOhneMod")]
        [DependsOnModifikator(typeof(Mod.IModATBasis))]
        public int AttackeBasis
        {
            get
            {
                int v = AttackeBasisOhneMod;
                if (Modifikatoren != null)
                    Modifikatoren.Where(m => m is Mod.IModATBasis).Select(m => (Mod.IModATBasis)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => v = m.ApplyATBasisMod(v));
                return v;
            }
        }

        /// <summary>
        /// Grund-AT-Wert inkl. Abzüge.
        /// </summary>
        [DependentProperty("AttackeBasis")]
        [DependsOnModifikator(typeof(Mod.IModAT))]
        public int Attacke
        {
            get
            {
                int v = AttackeBasis;
                if (Modifikatoren != null)
                    Modifikatoren.Where(m => m is Mod.IModAT).Select(m => (Mod.IModAT)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => v = m.ApplyATMod(v));
                return v;
            }
        }

        [DependentProperty("BaseIN"), DependentProperty("BaseGE"), DependentProperty("BaseKK")]
        public int ParadeBasisOhneMod
        {
            get
            {
                return (int)Math.Round((BaseIN + BaseGE + BaseKK) / 5.0, 0, MidpointRounding.AwayFromZero);
            }
        }

        [DependentProperty("ParadeBasisOhneMod")]
        [DependsOnModifikator(typeof(Mod.IModPABasis))]
        public int ParadeBasis
        {
            get
            {
                int v = ParadeBasisOhneMod;
                if (Modifikatoren != null)
                    Modifikatoren.Where(m => m is Mod.IModPABasis).Select(m => (Mod.IModPABasis)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => v = m.ApplyPABasisMod(v));
                return v;
            }
        }

        /// <summary>
        /// Grund-PA-Wert inkl. Abzüge.
        /// </summary>
        [DependentProperty("ParadeBasis")]
        [DependsOnModifikator(typeof(Mod.IModPA))]
        public int Parade
        {
            get
            {
                int v = ParadeBasis;
                if (Modifikatoren != null)
                    Modifikatoren.Where(m => m is Mod.IModPA).Select(m => (Mod.IModPA)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => v = m.ApplyPAMod(v));
                return v;
            }
        }

        #endregion

        #region Fernkampf

        [DependentProperty("BaseIN"), DependentProperty("BaseFF"), DependentProperty("BaseKK")]
        public int FernkampfBasisOhneMod
        {
            get
            {
                return (int)Math.Round((BaseIN + BaseFF + BaseKK) / 5.0, 0, MidpointRounding.AwayFromZero);
            }
        }

        [DependentProperty("FernkampfBasisOhneMod")]
        [DependsOnModifikator(typeof(Mod.IModPABasis))]
        public int FernkampfBasis
        {
            get
            {
                int v = FernkampfBasisOhneMod;
                if (Modifikatoren != null)
                    Modifikatoren.Where(m => m is Mod.IModFKBasis).Select(m => (Mod.IModFKBasis)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => v = m.ApplyFKBasisMod(v));
                return v;
            }
        }

        #endregion
        // TODO ??: Übernahme der anderen Methoden aus dem alten Held

        #region Talente

        public Talent AddTalent(string tName, int wert)
        {
            Talent talent = Global.ContextTalent.TalentListe.Where(t => t.Talentname == tName).SingleOrDefault();

            if (talent == null)
                throw new System.ArgumentNullException("Talent nicht gefunden.");

            return AddTalent(talent, wert, null, null);
        }

        public Talent AddTalent(Talent t, int wert)
        {
            return AddTalent(t, wert, null, null);
        }

        public Talent AddTalent(Talent t, int wert, int? zuteilungAT, int? zuteilungPA)
        {
            if (t == null)
                return null;
            IEnumerable<Held_Talent> existierendeZuordnung = Held_Talent.Where(hta => hta.Talentname == t.Talentname && hta.HeldGUID == HeldGUID);
            if (existierendeZuordnung.Count() != 0)
            {
                //Oder eine Exception werfen?
                return existierendeZuordnung.First().Talent;
            }

            Held_Talent ht = Global.ContextHeld.New<Held_Talent>();
            ht.HeldGUID = HeldGUID;
            ht.Held = this;

            ht.Talentname = t.Talentname;
            ht.Talent = t;

            ht.TaW = wert;
            ht.ZuteilungAT = zuteilungAT;
            ht.ZuteilungPA = zuteilungPA;

            Held_Talent.Add(ht);

            // Abhängige VorNachteile und Sonderfertigkeiten automatisch einfügen.
            // TODO ??: Später ins Datenmodell einbauen. Eigenes DB-Feld mit Talentabhängigkeit.
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
                DeleteTalent(Held_Talent.Where(h => h.Talentname == talentname).FirstOrDefault());
        }

        public void DeleteTalent(Held_Talent ht)
        {
            if (ht != null)
            {
                Talent t = ht.Talent;
                Global.ContextHeld.Delete<Model.Held_Talent>(ht);

                // Abhängige VorNachteile und Sonderfertigkeiten automatisch löschen.
                // TODO ??: Später ins Datenmodell einbauen. Eigenes DB-Feld mit Talentabhängigkeit.
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
        /// Der TaW eines Talentes.
        /// </summary>
        public int Talentwert(string talentName, bool nurPositiv)
        {
            Model.Held_Talent ht = Held_Talent.Where(h => h.Talentname == talentName).FirstOrDefault();
            if(ht == null)
                return 0;
            int taw = ht.TaW ?? 0;
            if (Modifikatoren != null)
            {
                List<Mod.IModTalentwert> l = Modifikatoren.Where(m => m is Mod.IModTalentwert).Select(m => (Mod.IModTalentwert)m).OrderBy(m => m.Erstellt).ToList();
                foreach(Mod.IModTalentwert m in l)
                {
                    int tawneu = m.ApplyTalentwertMod(taw);
                    if(!nurPositiv || tawneu > taw)
                        taw = tawneu;
                }
            }
            return taw;
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
            return Talentwert(t.Talentname, nurPositiv);
        }

        /// <summary>
        /// Hat mindestens den angegebenen TaW
        /// </summary>
        public bool HatTalent(string talentname, int taw)
        {
            if (!HatTalent(talentname))
                return false;
            if(taw == int.MinValue)
                return true;
            return Talentwert(talentname, true) >= taw;
        }

        public bool HatTalent(Talent t)
        {
            return Held_Talent.Where(ht => ht.Talent == t).Count() > 0;
        }

        public bool HatTalent(string talentname)
        {
            return Held_Talent.Where(ht => ht.Talent.Talentname == talentname).Count() > 0;
        }

        /// <summary>
        /// Hat mindestens den angegebenen TaW
        /// </summary>
        public bool HatTalent(Talent t, int taw)
        {
            return HatTalent(t.Talentname, taw);
        }
        
        public List<string> Talentspezialisierungen(Talent t)
        {
            return Talentspezialisierungen(t.Talentname);
        }

        public List<string> Talentspezialisierungen(string talentName)
        {
            //TODO ??: bei GUID Umstellung statt Sonderfertigkeit.Name evtl auf GUID prüfen
            if (Held_Sonderfertigkeit != null)
            {
                List<string> r = Held_Sonderfertigkeit.Where(hs => hs.Sonderfertigkeit.Name == "Talentspezialisierung" && hs.Wert != null && hs.Wert.StartsWith(talentName)).OrderBy(hs => hs.Wert).Select(hs => hs.Wert).ToList();
                r.ForEach(w => w = Talent.GetSpezialisierungName(talentName, w));
            }
            return null;
        }

        public void AddBasisTalente()
        {
            foreach (Talent t in Global.ContextHeld.Liste<Talent>().Where(t => t.Talenttyp == "Basis").ToList())
            {
                if (t.TalentgruppeID != 1)
                    AddTalent(t, 0);
                else
                    AddTalent(t, 0, 0, 0);
            }
        }

        #endregion

        #region Zauber

        public Zauber AddZauber(Zauber z, int wert, string rep)
        {
            if (z == null)
                return null;
            IEnumerable<Held_Zauber> existierendeZuordnung = Held_Zauber.Where(hza => hza.ZauberID == z.ZauberID
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

            hz.ZauberID = z.ZauberID;
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
            return Held_Zauber.Where(hz => hz.Zauber == z && hz.Repräsentation == rep).Count() > 0;
        }

        public bool HatZauber(string zaubername)
        {
            return Held_Zauber.Where(hz => hz.Zauber.Name.ToUpperInvariant() == zaubername.ToUpperInvariant()).Count() > 0;
        }

        //TODO JT: Eventuell auch nur auf den anfang des Zaubernamens abprüfen
        public bool HatZauber(string zaubername, int zfw)
        {
            if (!HatZauber(zaubername))
                return false;
            if (zfw == int.MinValue)
                return true;
            return Zauberfertigkeitswert(zaubername, true) >= zfw;
        }

        /// <summary>
        /// Der ZfW eines Zaubers.
        /// </summary>
        public int Zauberfertigkeitswert(string zauberName, bool nurPositiv = false)
        {
            Model.Held_Zauber hz = Held_Zauber.Where(h => h.Zauber.Name.ToUpperInvariant() == zauberName.ToUpperInvariant()).FirstOrDefault();
            if (hz == null)
                return 0;
            int zfw = hz.ZfW ?? 0;
            if (Modifikatoren != null)
            {
                List<Mod.IModZauberwert> l = Modifikatoren.Where(m => m is Mod.IModZauberwert).Select(m => (Mod.IModZauberwert)m).OrderBy(m => m.Erstellt).ToList();
                foreach (Mod.IModZauberwert m in l)
                {
                    int zfwneu = m.ApplyZauberwertMod(zfw);
                    if (!nurPositiv || zfwneu > zfw)
                        zfw = zfwneu;
                }
            }
            return zfw;
        }

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
                return Held_Sonderfertigkeit.Where(hsf => hsf.Sonderfertigkeit.Name.StartsWith("Repräsentation")).ToDictionary(hsf => hsf.Sonderfertigkeit, hsf => hsf.Wert);
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
                    // Held hat mehrerer Repräsentationen.
                    // Ermitteln welche Repäsentation mit den meisten Zaubern vertren ist.

                    var maxRep = Held_Zauber.GroupBy(hz => hz.Repräsentation).OrderByDescending(r => r.Count());
                    return maxRep.FirstOrDefault().Key;
                }
                else
                    return "Mag";
            }
        }

        #endregion

        #region Vor/Nachteile

        public VorNachteil AddVorNachteil(string vorNachName, string wert)
        {
            return AddVorNachteil(Global.ContextHeld.Liste<VorNachteil>().Where(vn => vn.Name == vorNachName).FirstOrDefault(), wert);
        }

        public VorNachteil AddVorNachteil(VorNachteil vn, string wert)
        {
            if (vn == null)
                return null;
            IEnumerable<Held_VorNachteil> existierendeZuordnung = Held_VorNachteil.Where(heldvn => heldvn.VorNachteilID == vn.VorNachteilID && heldvn.HeldGUID == HeldGUID);
            if (existierendeZuordnung.Count() != 0)
            {
                //Oder eine Exception werfen?
                return existierendeZuordnung.First().VorNachteil;
            }

            Held_VorNachteil hvn = new Model.Held_VorNachteil();
            hvn.HeldGUID = HeldGUID;
            hvn.Held = this;

            hvn.VorNachteilID = vn.VorNachteilID;
            hvn.VorNachteil = vn;
            
            hvn.Wert = wert;
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
                throw new System.ArgumentNullException("Weder Vor noch Nachteil gesetzt");
            }
            //check
            if ( ! ( (bool)hvn.VorNachteil.Vorteil  ||  (bool)hvn.VorNachteil.Nachteil ) )
            {
                throw new System.ArgumentNullException("Weder Vor noch Nachteil gesetzt");
            }
            Held_VorNachteil.Add(hvn);

            // abhängige Talente automatisch einfügen
            // TODO ??: Ins Datenmodell einbauen. Eigenes DB-Feld mit Talentabhängigkeit.
            if (vn.Name == VorNachteil.Empathie)
                AddTalent("Empathie5", 3);
            else if (vn.Name == VorNachteil.Gefahreninstinkt)
                AddTalent("Gefahreninstinkt", 3);
            else if (vn.Name == VorNachteil.Geräuschhexerei)
                AddTalent("Geräuschhexerei", 3);
            else if (vn.Name == VorNachteil.Magiegespür)
                AddTalent("Magiegespür", 3);
            else if (vn.Name == VorNachteil.Prophezeien)
                AddTalent("Prophezeien", 3);
            else if (vn.Name == VorNachteil.Zwergennase)
                AddTalent("Zwergennase", 3);

            return vn;                        
        }

        public void DeleteVorNachteil(string vnName)
        {
            if (HatVorNachteil(vnName))
                DeleteVorNachteil(Held_VorNachteil.Where(h => h.VorNachteil.Name == vnName).FirstOrDefault());
        }

        public void DeleteVorNachteil(Held_VorNachteil hvn)
        {
            if (hvn != null)
            {
                string vnName = hvn.VorNachteil.Name;
                Global.ContextHeld.Delete<Model.Held_VorNachteil>(hvn);

                // Falls Gabe -> Talent mit löschen
                // TODO ??: Ins Datenmodell einbauen. Eigenes DB-Feld mit Talentabhängigkeit.
                if (hvn == null || hvn.VorNachteil == null)
                    return;
                if (vnName == VorNachteil.Empathie)
                    DeleteTalent("Empathie");
                else if (vnName == VorNachteil.Gefahreninstinkt)
                    DeleteTalent("Gefahreninstinkt");
                else if (vnName == VorNachteil.Geräuschhexerei)
                    DeleteTalent("Geräuschhexerei");
                else if (vnName == VorNachteil.Magiegespür)
                    DeleteTalent("Magiegespür");
                else if (vnName == VorNachteil.Prophezeien)
                    DeleteTalent("Prophezeien");
                else if (vnName == VorNachteil.Zwergennase)
                    DeleteTalent("Zwergennase");
            }
        }

        public bool HatVorNachteil(VorNachteil vn)
        {
            return Held_VorNachteil.Where(hvn => hvn.VorNachteil != null && hvn.VorNachteil == vn).Count() > 0;
        }

        public bool HatVorNachteil(string vn)
        {
            return HatVorNachteil(vn, null);
        }

        public bool HatVorNachteil(string vn, string wert)
        {
            
            Held_VorNachteil hvn = Held_VorNachteil.Where(hvn2 => hvn2.VorNachteil != null && hvn2.VorNachteil.Name == vn).FirstOrDefault();
            //Wert abprüfen
            if (hvn != null && wert != null && (hvn.VorNachteil.HatWert ?? false))
            {
                if (hvn.VorNachteil.WertTyp != null && hvn.VorNachteil.WertTyp.ToLowerInvariant() == "int")
                {
                    int expected, actual;
                    if (int.TryParse(wert, out expected) && int.TryParse(hvn.Wert, out actual))
                        return actual >= expected;
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("Fehler beim Parsen des Wertes {0} oder {1} zu einem Integer. HatVorNachteil({2},{0})", wert, hvn.Wert, vn);
                        return false;
                    }
                }
                else
                    return hvn.Wert == wert;
            }
            return hvn != null;
        }

        /// <summary>
        /// Die Vorteile des Helden.
        /// Nicht zum ändern von Werten, da die Werte in Held_VorNachteil stehen.
        /// </summary>
        public IDictionary<VorNachteil, string> Vorteile
        {
            get
            {
                return Held_VorNachteil.Where(hvn => hvn.VorNachteil != null && hvn.VorNachteil.Vorteil == true).ToDictionary(hvn => hvn.VorNachteil, hvn => hvn.Wert);
            }
        }

        /// <summary>
        /// Die Vorteile, die der Held noch wählen kann.
        /// </summary>
        public List<VorNachteil> VorteileWählbar
        {
            get
            {
                return Global.ContextVorNachteil.VorNachteilListe.Where(v => v.Vorteil == true).Except(Vorteile.Keys).OrderBy(v => v.Name).ToList();
            }
        }

        /// <summary>
        /// Die Nachteile des Helden.
        /// Nicht zum ändern von Werten, da die Werte in Held_VorNachteil stehen.
        /// </summary>
        public IDictionary<VorNachteil, string> Nachteile
        {
            get
            {
                return Held_VorNachteil.Where(hvn => hvn.VorNachteil != null && hvn.VorNachteil.Nachteil == true).ToDictionary(hvn => hvn.VorNachteil, hvn => hvn.Wert);
            }
        }

        /// <summary>
        /// Die Nachteile, die der Held noch wählen kann.
        /// </summary>
        public List<VorNachteil> NachteileWählbar
        {
            get
            {
                return Global.ContextVorNachteil.VorNachteilListe.Where(n => n.Nachteil == true).Except(Nachteile.Keys).OrderBy(n => n.Name).ToList();
            }
        }


        #endregion

        #region Sonderfertigkeiten

        public Sonderfertigkeit AddSonderfertigkeit(string sfName, string wert = null)
        {
            return AddSonderfertigkeit(Global.ContextHeld.Liste<Sonderfertigkeit>().Where(sf => sf.Name == sfName).FirstOrDefault(), wert);
        }

        public Sonderfertigkeit AddSonderfertigkeit(Sonderfertigkeit sf, string wert = null)
        {
            if (sf == null)
                return null;
            IEnumerable<Held_Sonderfertigkeit> existierendeZuordnung = Held_Sonderfertigkeit.Where(heldsf => heldsf.SonderfertigkeitID == sf.SonderfertigkeitID && heldsf.HeldGUID == HeldGUID);
            if (existierendeZuordnung.Count() != 0)
            {
                //Oder eine Exception werfen?
                return existierendeZuordnung.First().Sonderfertigkeit;
            }

            Held_Sonderfertigkeit hs = Global.ContextHeld.New<Held_Sonderfertigkeit>();            
            hs.HeldGUID = HeldGUID;
            hs.Held = this;

            hs.SonderfertigkeitID = sf.SonderfertigkeitID;
            hs.Sonderfertigkeit = sf;
            
            hs.Wert = wert;
                        
            Held_Sonderfertigkeit.Add(hs);

            // Abhängige Talente automatisch einfügen.
            // TODO ??: Später ins Datenmodell einbauen. Eigenes DB-Feld mit Talentabhängigkeit.
            // Falls Ritualkenntnis oder Liturgiekenntnis -> Talente automatisch einfügen
            if (sf.Name == Sonderfertigkeit.RitualkenntnisSchamaneAchaz || sf.Name == Sonderfertigkeit.RitualkenntnisSchamaneFerkina
                || sf.Name == Sonderfertigkeit.RitualkenntnisSchamaneGjalsker || sf.Name == Sonderfertigkeit.RitualkenntnisSchamaneGoblin
                || sf.Name == Sonderfertigkeit.RitualkenntnisSchamaneNivesen || sf.Name == Sonderfertigkeit.RitualkenntnisSchamaneOrk
                || sf.Name == Sonderfertigkeit.RitualkenntnisSchamaneTrollzacker || sf.Name == Sonderfertigkeit.RitualkenntnisSchamaneWaldmenschen)
            { // Schamanen...
                AddTalent("Geister aufnehmen", 3);
                AddTalent("Geister bannen", 3);
                AddTalent("Geister binden", 3);
                AddTalent("Geister rufen", 3);
            } // Runenkunde...
            else if (sf.Name == Sonderfertigkeit.Runenkunde)
                AddTalent("Ritualkenntnis (Runenzauberei)", 3);
            else if (sf.Name.StartsWith("Ritualkenntnis"))
            { // Ritualkenntnis...
                string tradition = sf.Name.Replace("Ritualkenntnis (", string.Empty).Replace(")", string.Empty);
                AddTalent("Ritualkenntnis (" + tradition + ")", 3);
            } // Liturgiekenntnis...
            else if (sf.Name.StartsWith("Liturgiekenntnis"))
            {
                string kirche = sf.Name.Replace("Liturgiekenntnis (", string.Empty).Replace(")", string.Empty);
                AddTalent("Liturgiekenntnis (" + kirche + ")", 3);
            }

            return sf;
        }

        public void DeleteSonderfertigkeit(string sfName)
        {
            if (HatSonderfertigkeit(sfName))
                DeleteSonderfertigkeit(Held_Sonderfertigkeit.Where(h => h.Sonderfertigkeit.Name == sfName).FirstOrDefault());
        }

        public void DeleteSonderfertigkeit(Held_Sonderfertigkeit hsf)
        {
            if (hsf != null)
            {
                string sfName = hsf.Sonderfertigkeit.Name;

                Global.ContextHeld.Delete<Model.Held_Sonderfertigkeit>(hsf);

                // Falls Ritualkenntnis oder Liturgiekenntnis -> Talent mit löschen
                // TODO ??: Später ins Datenmodell einbauen. Eigenes DB-Feld mit Talentabhängigkeit.
                if (sfName.StartsWith("Ritualkenntnis") || sfName.StartsWith("Liturgiekenntnis"))
                    DeleteTalent(sfName);
            }
        }

        /// <summary>
        /// Hat die Sonderfertigkeit.
        /// </summary>
        public bool HatSonderfertigkeit(Sonderfertigkeit s)
        {
            return Held_Sonderfertigkeit.Where(hs => hs.Sonderfertigkeit != null && hs.Sonderfertigkeit == s).Count() > 0;
        }

        /// <summary>
        /// Hat die Sonderfertigkeit mit bestimmtem Wert.
        /// </summary>
        public bool HatSonderfertigkeit(string sonderfertigkeit, string wert = null)
        {
            IEnumerable<Held_Sonderfertigkeit> hso = Held_Sonderfertigkeit.Where(hs => hs.Sonderfertigkeit != null && hs.Sonderfertigkeit.Name == sonderfertigkeit);
            if (hso.Count() == 0)
                return false;
            foreach (Held_Sonderfertigkeit hs in hso)
            {
                //Wert prüfen
                if (wert != null && (hs.Sonderfertigkeit.HatWert ?? false) && hs.Wert != wert)
                    continue;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Hat die Sonderfertigkeit inklusive der Voraussetzungen.
        /// </summary>
        public bool HatSonderfertigkeitUndVoraussetzungen(Sonderfertigkeit s)
        {
            if (Held_Sonderfertigkeit.Where(hs => hs.Sonderfertigkeit != null && hs.Sonderfertigkeit == s).Count() > 0)
                return s.CheckVoraussetzungen(this);
            return false;
        }

        /// <summary>
        /// Hat die Sonderfertigkeit mit bestimmtem Wert inklusive der Voraussetzungen.
        /// </summary>
        public bool HatSonderfertigkeitUndVoraussetzungen(string sonderfertigkeit, string wert)
        {
            IEnumerable<Held_Sonderfertigkeit> hso = Held_Sonderfertigkeit.Where(hs => hs.Sonderfertigkeit != null && hs.Sonderfertigkeit.Name == sonderfertigkeit);
            if(hso.Count() == 0)
                return false;
            //rekursiv die voraussetzungen prüfen
            foreach (Held_Sonderfertigkeit hs in hso)
            {
                //Wert prüfen
                if (wert != null && (hs.Sonderfertigkeit.HatWert ?? false) && hs.Wert != wert)
                    continue;
                if (hs.Sonderfertigkeit.CheckVoraussetzungen(this))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Hat die Sonderfertigkeit inklusive der Voraussetzungen.
        /// </summary>
        public bool HatSonderfertigkeitUndVoraussetzungen(string sonderfertigkeit)
        {
            return HatSonderfertigkeitUndVoraussetzungen(sonderfertigkeit, null);
        }

        /// <summary>
        /// Die Sonderfertigkeiten des Helden.
        /// Nicht zum ändern von Werten, da die Werte in Held_Sonderfertigkeit stehen.
        /// </summary>
        public IDictionary<Sonderfertigkeit, string> Sonderfertigkeiten
        {
            get
            {
                return Held_Sonderfertigkeit.ToDictionary(hsf => hsf.Sonderfertigkeit, hsf => hsf.Wert);
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
                return Global.ContextHeld.Liste<Sonderfertigkeit>().Where(sf => sf.CheckVoraussetzungen(this) ).Except(Sonderfertigkeiten.Keys).OrderBy(sf => sf.Name).ToList();
            }
        }

        #endregion

        public int Behinderung
        {
            get { return BE ?? 0; }
            set { BE = value; }
        }

        [DependentProperty("BaseGE")]
        public int GeschwindigkeitOhneMod
        {
            get
            {
                int gs = 8;
                if (HatVorNachteil("Flink"))
                    gs++;
                if (HatVorNachteil("Zwergenwuchs"))
                    gs -= 2;
                if (HatVorNachteil("Kleinwüchsig"))
                    gs--;
                if (HatVorNachteil("Behäbig"))
                    gs--;
                if (BaseGE >= 16)
                    gs++;
                else if (BaseGE <= 10)
                    gs--;
                return gs;
            }
        }

        [DependentProperty("GeschwindigkeitOhneMod")]
        [DependsOnModifikator(typeof(Mod.IModGS))]
        public int Geschwindigkeit
        {
            get {
                int gs = GeschwindigkeitOhneMod;
                if (Modifikatoren != null)
                    Modifikatoren.Where(m => m is Mod.IModGS).Select(m => (Mod.IModGS)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => gs = m.ApplyGSMod(gs));
                return gs; 
            }
        }

        #region IKämpfer
        public int Initiative()
        {
            return InitiativeBasis + RandomNumberGenerator.Wurf(InitiativeZufall);
        }

        public int InitiativeMax()
        {
            return InitiativeBasis + (int)InitiativeZufall;
        }

        public string Bild
        {
            get { return null; }
        }

        public string Position
        {
            get { return null; }
        }

        public int? AT
        {
            get { return null; }
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

        public int MRGeist
        {
            //TODO ??: verschiedene Sonderfertigkeiten verändern die Geistmagieresistenz.
            get { return Magieresistenz; }
        }

        private Rüstungsschutz _rs = null;
        public IRüstungsschutz RS
        {
            get
            {
                if(_rs == null)
                    _rs = new Rüstungsschutz((Model.Held)this);
                return _rs;
            }
        }

        /// <summary>
        /// Ausweichen-Wert inklusive Akrobatik, Sonderfertigkeiten und Behinderung.
        /// </summary>
        public int? Ausweichen
        {
            get {
                int ausweichen = ParadeBasis;
                if (HatSonderfertigkeitUndVoraussetzungen("Ausweichen I"))
                    ausweichen += 3;
                if (HatSonderfertigkeitUndVoraussetzungen("Ausweichen II"))
                    ausweichen += 3;
                if (HatSonderfertigkeitUndVoraussetzungen("Ausweichen III"))
                    ausweichen += 3;
                ausweichen += (Math.Max(Talentwert("Akrobatik") - 9, 0) / 3);
                ausweichen -= Behinderung;
                return ausweichen;
            }
        }

        //beschreibbar, da es von der INI abhängt. Die Initiative wird in Kampf gespeichert und verwaltet.
        private int _freieAktionen = 2;
        public int FreieAktionen
        {
            get { 
                return _freieAktionen;
            }
            set
            {
                _freieAktionen = value;
            }
        }

        private int _aktionen = 2;
        public int Aktionen
        {
            get
            {
                return _aktionen;
            }
            private set {
                _aktionen = value;
                if (_aktionen < Abwehraktionen + Angriffsaktionen)
                {
                    if (Abwehraktionen + Angriffsaktionen == 0)
                        Angriffsaktionen = 0;
                    else
                        Angriffsaktionen = (int)Math.Round(Math.Min((double)Angriffsaktionen / (double)(Abwehraktionen + Angriffsaktionen), 1) * Aktionen, MidpointRounding.AwayFromZero);
                }
            }
        }

        private int _angriffsaktionen = 1;
        public int Angriffsaktionen
        {
            get { return _angriffsaktionen; }
            set {
                if (value > Aktionen)
                    value = Aktionen;
                _angriffsaktionen = value;
                _abwehraktionen = Aktionen - _angriffsaktionen;
                AktionenBerechnen();
                _abwehraktionen = Aktionen - _angriffsaktionen;
                OnChanged("Abwehraktionen"); OnChanged("Angriffsaktionen"); OnChanged("Aktionen");
            }
        }

        private int _abwehraktionen = 1;
        public int Abwehraktionen
        {
            get { return _abwehraktionen; }
            set {
                if (value > Aktionen)
                    value = Aktionen;
                _abwehraktionen = value;
                _angriffsaktionen = Aktionen - _abwehraktionen;
                AktionenBerechnen();
                _angriffsaktionen = Aktionen - _abwehraktionen;
                OnChanged("Abwehraktionen"); OnChanged("Angriffsaktionen"); OnChanged("Aktionen");
            }
        }


        private int _verbrauchteAngriffsaktionen = 0;
        public int VerbrauchteAngriffsaktionen
        {
            get { return _verbrauchteAngriffsaktionen; }
            set { _verbrauchteAngriffsaktionen = value; }
        }

        private int _verbrauchteAbwehraktionen = 0;
        public int VerbrauchteAbwehraktionen
        {
            get { return _verbrauchteAbwehraktionen; }
            set { _verbrauchteAbwehraktionen = value; }
        }

        private int _verbrauchteFreieAktionen = 0;
        public int VerbrauchteFreieAktionen
        {
            get { return _verbrauchteFreieAktionen; }
            set { _verbrauchteFreieAktionen = value; }
        }

        private Kampfstil _kampfstil;
        public Kampfstil Kampfstil
        {
            get { return _kampfstil; }
            set {
                if (_kampfstil == value)
                    return;
                _kampfstil = value;
                AktionenBerechnen();
                OnChanged("Kampfstil");
                OnChanged("Abwehraktionen"); OnChanged("Angriffsaktionen"); OnChanged("Aktionen");
            }
        }

        private void AktionenBerechnen()
        {
            bool parierwaffenII = false;
            //TODO JT: Sicherstellen, dass auch zwei Waffen geführt werden
            if (Kampfstil == KampfLogic.Kampfstil.BeidhändigerKampf && HatSonderfertigkeitUndVoraussetzungen("Beidhändiger Kampf II"))
            {
                Aktionen = 3;
                if (Abwehraktionen + Angriffsaktionen < 3)
                    _angriffsaktionen = Aktionen - Abwehraktionen;
            }
            else if (Kampfstil == KampfLogic.Kampfstil.Schildkampf && HatSonderfertigkeitUndVoraussetzungen("Schildkampf II") && Abwehraktionen >= 1)
            {
                if (Angriffsaktionen >= 2)
                    Aktionen = 2;
                else
                    Aktionen = 3;
                if (Abwehraktionen + Angriffsaktionen < 3)
                    _abwehraktionen = Aktionen - Angriffsaktionen;
            }
            else if (Kampfstil == KampfLogic.Kampfstil.Parierwaffenstil && (parierwaffenII = HatSonderfertigkeitUndVoraussetzungen("Parierwaffen II") && Abwehraktionen >= 1 || HatSonderfertigkeitUndVoraussetzungen("Tod von Links") && Angriffsaktionen >= 1))
            {
                Aktionen = 3;
                if (Abwehraktionen + Angriffsaktionen < 3)
                    if (parierwaffenII)
                        _abwehraktionen = Aktionen - Angriffsaktionen;
                    else
                        _angriffsaktionen = Aktionen - Abwehraktionen;
            }
            else
            {
                Aktionen = 2;
                if (Abwehraktionen + Angriffsaktionen < 2)
                    _angriffsaktionen = _abwehraktionen = 1;
            }
            //TODO JT: Myranor: Mehrhändig hinzufügen sicherstellen, dass auch entsprechend viele Waffen geführt werden
        }

        private WaffenloserKampfstil _waffenloserKampfstil;
        public WaffenloserKampfstil WaffenloserKampfstil
        {
            get { return _waffenloserKampfstil; }
            set { _waffenloserKampfstil = value; }
        }

        public List<KampfLogic.Manöver.Manöver> Manöver
        {
            get { return null; }
        }

        private Wunden kämpferWunden = null;
        public IWunden WundenByZone
        {
            get
            {
                if (kämpferWunden == null)
                    kämpferWunden = new KampfLogic.Wunden((Model.Held)this);
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

        public IList<IWaffe> Angriffswaffen
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region Import Export
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
            Service.SerializationService serialization = Service.SerializationService.GetInstance(!batch);
            Guid heldGuid = serialization.ImportHeld(pfad, newGuid);
            if (heldGuid == Guid.Empty)
                return null;
            Global.ContextHeld.UpdateList<Held>();
            return Global.ContextHeld.Liste<Held>().Where(h => h.HeldGUID == heldGuid).FirstOrDefault();
        }

        public void Export(string pfad, bool batch = false)
        {
            Service.SerializationService serialization = Service.SerializationService.GetInstance(!batch);
            serialization.ExportHeld(HeldGUID, pfad);
        }

        public Held Clone(bool batch = false)
        {
            return Clone(Guid.NewGuid(), batch);
        }

        public Held Clone(Guid newGuid, bool batch = false)
        {
            Service.SerializationService serialization = Service.SerializationService.GetInstance(!batch);
            Guid heldGuid = serialization.CloneHeld(HeldGUID, newGuid);
            if (heldGuid == Guid.Empty)
                return null;
            Global.ContextHeld.UpdateList<Held>();
            return Global.ContextHeld.Liste<Held>().Where(h => h.HeldGUID == heldGuid).FirstOrDefault();
        }
        #endregion

        public override string ToString()
        {
            return Name;
        }


    }
}
