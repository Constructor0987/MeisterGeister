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
using E = MeisterGeister.Logic.Einstellung.Einstellungen;
using MeisterGeister.ViewModel.Inventar.Logic;
using MeisterGeister.ViewModel.Basar.Logic;

namespace MeisterGeister.Model {
    // Man kann Superklassen hinzufügen. Es sollten jedoch nicht die gleichen Eigenschaften, wie in der Datenbankklasse existieren.
    public partial class Held : ViewModel.Kampf.Logic.Wesen, KampfLogic.IKämpfer, Extensions.IInitializable, KampfLogic.IHasZonenRs, IHasWunden, IDisposable {
        public Held()
            : base() {
            HeldGUID = Guid.NewGuid();
            PropertyChanged += DependentProperty.PropagateINotifyProperyChanged;
            SetDefaultValues();
        }

        private void SetDefaultValues() {
            Name = "Alrik";
            AktiveHeldengruppe = true;
            MU = 8; KL = 8; IN = 8; CH = 8; FF = 8; GE = 8; KO = 8; KK = 8;
            LE_Aktuell = 12; AU_Aktuell = 12;
        }

        #region IInitializable
        private bool _isInitialized = false;
        public void Initialize() {
            if (_isInitialized)
                return;
            kämpferWunden = new KampfLogic.Wunden((Model.Held)this);
            //WundenByZone.UpdateWundenModifikatoren(); // das würde es zweimal durchlaufen
            _isInitialized = true;
        }
        #endregion

        #region Modifikatorlisten

        [DependsOnModifikator(typeof(Mod.IModMU))]
        public List<dynamic> ModifikatorenListeMU {
            get {
                return ModifikatorenListe(typeof(Mod.IModMU), MU);
            }
        }
        [DependsOnModifikator(typeof(Mod.IModKL))]
        public List<dynamic> ModifikatorenListeKL {
            get {
                return ModifikatorenListe(typeof(Mod.IModKL), KL);
            }
        }
        [DependsOnModifikator(typeof(Mod.IModCH))]
        public List<dynamic> ModifikatorenListeCH {
            get {
                return ModifikatorenListe(typeof(Mod.IModCH), CH);
            }
        }
        [DependsOnModifikator(typeof(Mod.IModIN))]
        public List<dynamic> ModifikatorenListeIN {
            get {
                return ModifikatorenListe(typeof(Mod.IModIN), IN);
            }
        }
        [DependsOnModifikator(typeof(Mod.IModFF))]
        public List<dynamic> ModifikatorenListeFF {
            get {
                return ModifikatorenListe(typeof(Mod.IModFF), FF);
            }
        }
        [DependsOnModifikator(typeof(Mod.IModGE))]
        public List<dynamic> ModifikatorenListeGE {
            get {
                return ModifikatorenListe(typeof(Mod.IModGE), GE);
            }
        }
        [DependsOnModifikator(typeof(Mod.IModKO))]
        public List<dynamic> ModifikatorenListeKO {
            get {
                return ModifikatorenListe(typeof(Mod.IModKO), KO);
            }
        }
        [DependsOnModifikator(typeof(Mod.IModKK))]
        public List<dynamic> ModifikatorenListeKK {
            get {
                return ModifikatorenListe(typeof(Mod.IModKK), KK);
            }
        }
        [DependsOnModifikator(typeof(Mod.IModINIBasis))]
        public List<dynamic> ModifikatorenListeINIbasis {
            get {
                return ModifikatorenListe(typeof(Mod.IModINIBasis), InitiativeBasisOhneMod);
            }
        }
        [DependsOnModifikator(typeof(Mod.IModATBasis))]
        public List<dynamic> ModifikatorenListeATbasis {
            get {
                return ModifikatorenListe(typeof(Mod.IModATBasis), AttackeBasisOhneMod);
            }
        }
        [DependsOnModifikator(typeof(Mod.IModPABasis))]
        public List<dynamic> ModifikatorenListePAbasis {
            get {
                return ModifikatorenListe(typeof(Mod.IModPABasis), ParadeBasisOhneMod);
            }
        }
        [DependsOnModifikator(typeof(Mod.IModAT))]
        public List<dynamic> ModifikatorenListeAT {
            get {
                List<dynamic> list = ModifikatorenListe(typeof(Mod.IModATBasis), AttackeBasisOhneMod);
                list.AddRange(ModifikatorenListe(typeof(Mod.IModAT), list.Count() == 0 ? AttackeBasisOhneMod : list.LastOrDefault().Wert));
                return list;
            }
        }
        [DependsOnModifikator(typeof(Mod.IModPA))]
        public List<dynamic> ModifikatorenListePA {
            get {
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
        public List<dynamic> ModifikatorenListeFKbasis {
            get {
                return ModifikatorenListe(typeof(Mod.IModFKBasis), FernkampfBasisOhneMod);
            }
        }
        [DependsOnModifikator(typeof(Mod.IModMR))]
        public List<dynamic> ModifikatorenListeMR {
            get {
                return ModifikatorenListe(typeof(Mod.IModMR), Magieresistenz);
            }
        }
        [DependsOnModifikator(typeof(Mod.IModGS))]
        [DependentProperty("Behinderung")]
        public List<dynamic> ModifikatorenListeGS {
            get {
                List<dynamic> li = ModifikatorenListe(typeof(Mod.IModGS), (double)GeschwindigkeitOhneMod);
                if (Behinderung > 0)
                    li.AddRange(ModifikatorenListe(typeof(Mod.IModBE), li.Count == 0 ? GeschwindigkeitOhneMod : (int)li.LastOrDefault().Wert, new List<Mod.IModifikator>() { new Mod.BehinderungModifikator(Behinderung) }));
                return li;
            }
        }
        [DependsOnModifikator(typeof(Mod.IModAusweichen))]
        [DependentProperty("Behinderung")]
        public List<dynamic> ModifikatorenListeAusweichen {
            get {
                List<dynamic> li = ModifikatorenListe(typeof(Mod.IModAusweichen), AusweichenOhneMod);
                if (Behinderung > 0)
                    li.AddRange(ModifikatorenListe(typeof(Mod.IModBE), li.Count == 0 ? AusweichenOhneMod : (int)li.LastOrDefault().Wert, new List<Mod.IModifikator>() { new Mod.BehinderungModifikator(Behinderung) }));
                return li;
            }
        }

        #endregion

        #region Eigenschaften
        [DependentProperty("MU")]
        [DependsOnModifikator(typeof(Mod.IModMU))]
        public int Mut {
            get {
                int mu = MU ?? 8;
                if (Modifikatoren != null)
                    Modifikatoren.Where(m => m is Mod.IModMU).Select(m => (Mod.IModMU)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => mu = m.ApplyMUMod(mu));
                return mu;
            }
        }

        [DependentProperty("KL")]
        [DependsOnModifikator(typeof(Mod.IModKL))]
        public int Klugheit {
            get {
                int e = KL ?? 8;
                if (Modifikatoren != null)
                    Modifikatoren.Where(m => m is Mod.IModKL).Select(m => (Mod.IModKL)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => e = m.ApplyKLMod(e));
                return e;
            }
        }

        [DependentProperty("IN")]
        [DependsOnModifikator(typeof(Mod.IModIN))]
        public int Intuition {
            get {
                int e = IN ?? 8;
                if (Modifikatoren != null)
                    Modifikatoren.Where(m => m is Mod.IModIN).Select(m => (Mod.IModIN)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => e = m.ApplyINMod(e));
                return e;
            }
        }

        [DependentProperty("CH")]
        [DependsOnModifikator(typeof(Mod.IModCH))]
        public int Charisma {
            get {
                int e = CH ?? 8;
                if (Modifikatoren != null)
                    Modifikatoren.Where(m => m is Mod.IModCH).Select(m => (Mod.IModCH)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => e = m.ApplyCHMod(e));
                return e;
            }
        }

        [DependentProperty("FF")]
        [DependsOnModifikator(typeof(Mod.IModFF))]
        public int Fingerfertigkeit {
            get {
                int e = FF ?? 8;
                if (Modifikatoren != null)
                    Modifikatoren.Where(m => m is Mod.IModFF).Select(m => (Mod.IModFF)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => e = m.ApplyFFMod(e));
                return e;
            }
        }

        [DependentProperty("GE")]
        [DependsOnModifikator(typeof(Mod.IModGE))]
        public int Gewandtheit {
            get {
                int e = GE ?? 8;
                if (Modifikatoren != null)
                    Modifikatoren.Where(m => m is Mod.IModGE).Select(m => (Mod.IModGE)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => e = m.ApplyGEMod(e));
                return e;
            }
        }

        [DependentProperty("KO")]
        [DependsOnModifikator(typeof(Mod.IModKO))]
        public int Konstitution {
            get {
                int e = KO ?? 8;
                if (Modifikatoren != null)
                    Modifikatoren.Where(m => m is Mod.IModKO).Select(m => (Mod.IModKO)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => e = m.ApplyKOMod(e));
                return e;
            }
        }

        [DependentProperty("KO")]
        [DependsOnModifikator(typeof(Mod.IModKO))]
        public int KonstitutionOhneWunden {
            get {
                int e = KO ?? 8;
                if (Modifikatoren != null)
                    Modifikatoren.Where(m => m is Mod.IModKO && !(m is Mod.WundenModifikatorBase)).Select(m => (Mod.IModKO)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => e = m.ApplyKOMod(e));
                return e;
            }
        }

        [DependentProperty("KK")]
        [DependsOnModifikator(typeof(Mod.IModKK))]
        public int Körperkraft {
            get {
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
        public int EigenschaftWert(string eigenschaft, bool ohneMod = false) {
            if (string.IsNullOrEmpty(eigenschaft))
                return 0;
            switch (eigenschaft) {
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
        public Eigenschaft Eigenschaft(string eigenschaft, bool ohneMod = false) {
            return new Eigenschaft(eigenschaft, EigenschaftWert(eigenschaft, ohneMod), this);
        }

        #endregion

        #region Abenteuerpunkte

        [DependentProperty("APGesamt"), DependentProperty("APEingesetzt")]
        public int APGuthaben {
            get { return APGesamt.GetValueOrDefault() - APEingesetzt.GetValueOrDefault(); }
            set {
                // TODO ??: Soll eine Änderung des Guthabens die abhängigen AP-Werte ändern? Soll das möglich sein, oder eher nicht?
            }
        }

        [DependentProperty("APEingesetzt")]
        public int Stufe {
            get { return APEingesetzt.GetValueOrDefault() / 1000; }
            set {
                // TODO ??: Änderung der Stufe erhöht die AP-Werte? Soll das möglich sein, oder eher nicht?
            }
        }

        #endregion


        #region BaseEigenschaften / Für die Berechnung von abgeleiteten Werten
        [DependentProperty("MU")]
        [DependsOnModifikator(typeof(Mod.IModMU))]
        public int BaseMU {
            get {
                int mu = MU ?? 8;
                if (Modifikatoren != null)
                    Modifikatoren.Where(m => m is Mod.IModMitAuswirkungAufBerechneteWerte && m is Mod.IModMU).Select(m => (Mod.IModMU)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => mu = m.ApplyMUMod(mu));
                return mu;
            }
        }
        [DependentProperty("KL")]
        [DependsOnModifikator(typeof(Mod.IModKL))]
        public int BaseKL {
            get {
                int e = KL ?? 8;
                if (Modifikatoren != null)
                    Modifikatoren.Where(m => m is Mod.IModMitAuswirkungAufBerechneteWerte && m is Mod.IModKL).Select(m => (Mod.IModKL)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => e = m.ApplyKLMod(e));
                return e;
            }
        }
        [DependentProperty("IN")]
        [DependsOnModifikator(typeof(Mod.IModIN))]
        public int BaseIN {
            get {
                int e = IN ?? 8;
                if (Modifikatoren != null)
                    Modifikatoren.Where(m => m is Mod.IModMitAuswirkungAufBerechneteWerte && m is Mod.IModIN).Select(m => (Mod.IModIN)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => e = m.ApplyINMod(e));
                return e;
            }
        }
        [DependentProperty("CH")]
        [DependsOnModifikator(typeof(Mod.IModCH))]
        public int BaseCH {
            get {
                int e = CH ?? 8;
                if (Modifikatoren != null)
                    Modifikatoren.Where(m => m is Mod.IModMitAuswirkungAufBerechneteWerte && m is Mod.IModCH).Select(m => (Mod.IModCH)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => e = m.ApplyCHMod(e));
                return e;
            }
        }
        [DependentProperty("FF")]
        [DependsOnModifikator(typeof(Mod.IModFF))]
        public int BaseFF {
            get {
                int e = FF ?? 8;
                if (Modifikatoren != null)
                    Modifikatoren.Where(m => m is Mod.IModMitAuswirkungAufBerechneteWerte && m is Mod.IModFF).Select(m => (Mod.IModFF)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => e = m.ApplyFFMod(e));
                return e;
            }
        }
        [DependentProperty("GE")]
        [DependsOnModifikator(typeof(Mod.IModGE))]
        public int BaseGE {
            get {
                int e = GE ?? 8;
                if (Modifikatoren != null)
                    Modifikatoren.Where(m => m is Mod.IModMitAuswirkungAufBerechneteWerte && m is Mod.IModGE).Select(m => (Mod.IModGE)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => e = m.ApplyGEMod(e));
                return e;
            }
        }
        [DependentProperty("KO")]
        [DependsOnModifikator(typeof(Mod.IModKO))]
        public int BaseKO {
            get {
                int e = KO ?? 8;
                if (Modifikatoren != null)
                    Modifikatoren.Where(m => m is Mod.IModMitAuswirkungAufBerechneteWerte && m is Mod.IModKO).Select(m => (Mod.IModKO)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => e = m.ApplyKOMod(e));
                return e;
            }
        }
        [DependentProperty("KK")]
        [DependsOnModifikator(typeof(Mod.IModKK))]
        public int BaseKK {
            get {
                int e = KK ?? 8;
                if (Modifikatoren != null)
                    Modifikatoren.Where(m => m is Mod.IModMitAuswirkungAufBerechneteWerte && m is Mod.IModKK).Select(m => (Mod.IModKK)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => e = m.ApplyKKMod(e));
                return e;
            }
        }
        #endregion

        #region Lebensenergie

        public string LebensenergieGrundwertFormel
        {
            get
            {
                string le = string.Empty;
                if (Regelsystem == "DSA 4.1")
                    le = "(KO + KO + KK) / 2";
                else if (Regelsystem == "DSA 5")
                    le = "KO + KO";
                return le;
            }
        }

        [DependentProperty("BaseKO"), DependentProperty("BaseKK")]
        public int LebensenergieBasis {
            get {
                int le = 0;
                if (Regelsystem == "DSA 4.1")
                    le = (int)Math.Round((BaseKO * 2 + BaseKK) / 2.0, 0, MidpointRounding.AwayFromZero);
                else if (Regelsystem == "DSA 5")
                    le = BaseKO * 2;
                return le;
            }
        }
        [DependentProperty("LebensenergieModSonstiges"), DependentProperty("LebensenergieModGenerierung"), DependentProperty("LebensenergieModVorNachteile"), DependentProperty("LebensenergieModZukauf")]
        public int LebensenergieMod {
            get { return LebensenergieModGenerierung + LebensenergieModSonstiges + LebensenergieModVorNachteile + LebensenergieModZukauf; }
        }

        [DependentProperty("LE_Mod")]
        public int LebensenergieModSonstiges
        {
            get { return LE_Mod ?? 0; }
            set
            {
                LE_Mod = value;
                OnChanged("LebensenergieModSonstiges");
            }
        }

        [DependentProperty("LE_ModGen")]
        public int LebensenergieModGenerierung
        {
            get { return LE_ModGen ?? 0; }
            set
            {
                LE_ModGen = value;
                OnChanged("LebensenergieModGenerierung");
            }
        }

        [DependentProperty("LE_ModZukauf")]
        public int LebensenergieModZukauf
        {
            get { return LE_ModZukauf ?? 0; }
            set
            {
                LE_ModZukauf = value;
                OnChanged("LebensenergieModZukauf");
            }
        }

        [DependentProperty("Nachteile"), DependentProperty("Vorteile")]
        public int LebensenergieModVorNachteile
        {
            get
            {
                int mod = 0;
                mod += CalcVorNachteilEnergieMod(VorNachteil.HoheLebenskraft);
                mod += CalcVorNachteilEnergieMod(VorNachteil.NiedrigeLebenskraft);
                return mod;
            }
        }

        private int CalcVorNachteilEnergieMod(string vn, int faktor = 1)
        {
            int mod = 0;
            if (HatVorNachteil(vn, false))
            {
                if (HatVorNachteil(vn))
                {
                    if (vn.StartsWith("Hohe") || vn == VorNachteil.Ausdauernd || vn == VorNachteil.Astralmacht)
                        mod += VorNachteilWertInt(vn).GetValueOrDefault(0) * faktor;
                    else if (vn.StartsWith("Niedrige") || vn == VorNachteil.Kurzatmig)
                        mod -= VorNachteilWertInt(vn).GetValueOrDefault(0) * faktor;
                }
            }
            return mod;
        }

        [DependentProperty("LE_Aktuell")]
        public int LebensenergieAktuell {
            get {
                return LE_Aktuell ?? 0;
            }
            set {
                LE_Aktuell = value;
            }
        }
        [DependentProperty("LebensenergieBasis"), DependentProperty("LebensenergieMod")]
        public int LebensenergieMax {
            get {
                int le = LebensenergieBasis + LebensenergieMod;
                if (Modifikatoren != null)
                    Modifikatoren.Where(m => m is Mod.IModLE).Select(m => (Mod.IModLE)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => le = m.ApplyLEMod(le));
                return le;
            }
        }

        [DependentProperty("LebensenergieAktuell"), DependentProperty("LebensenergieMax"), DependentProperty("Konstitution")]
        public string LebensenergieStatus {
            get {
                return GetLebensenergieStatus();
            }
        }
        #endregion

        #region Ausdauer

        public string AusdauerGrundwertFormel
        {
            get
            {
                string au = string.Empty;
                if (Regelsystem == "DSA 4.1")
                    au = "(MU + KO + GE) / 2";
                return au;
            }
        }

        [DependentProperty("BaseMU"), DependentProperty("BaseKO"), DependentProperty("BaseGE")]
        public int AusdauerBasis {
            get {
                if (Regelsystem == "DSA 4.1")
                    return (int)Math.Round((BaseMU + BaseKO + BaseGE) / 2.0, 0, MidpointRounding.AwayFromZero);
                else
                    return 0;
            }
        }
        [DependentProperty("AU_Aktuell")]
        public int AusdauerAktuell {
            get {
                return AU_Aktuell ?? 0;
            }
            set {
                AU_Aktuell = value;
            }
        }

        [DependentProperty("AusdauerModSonstiges"), DependentProperty("AusdauerModGenerierung"), DependentProperty("AusdauerModVorNachteile"), DependentProperty("AusdauerModZukauf")]
        public int AusdauerMod
        {
            get { return AusdauerModGenerierung + AusdauerModSonstiges + AusdauerModVorNachteile + AusdauerModZukauf; }
        }

        [DependentProperty("AU_Mod")]
        public int AusdauerModSonstiges {
            get { return AU_Mod ?? 0; }
            set {
                AU_Mod = value;
                OnChanged("AusdauerModSonstiges");
            }
        }

        [DependentProperty("AU_ModGen")]
        public int AusdauerModGenerierung
        {
            get { return AU_ModGen ?? 0; }
            set
            {
                AU_ModGen = value;
                OnChanged("AusdauerModGenerierung");
            }
        }

        [DependentProperty("AU_ModZukauf")]
        public int AusdauerModZukauf
        {
            get { return AU_ModZukauf ?? 0; }
            set
            {
                AU_ModZukauf = value;
                OnChanged("AusdauerModZukauf");
            }
        }

        [DependentProperty("Nachteile"), DependentProperty("Vorteile")]
        public int AusdauerModVorNachteile
        {
            get
            {
                int mod = 0;
                mod += CalcVorNachteilEnergieMod(VorNachteil.Ausdauernd);
                mod += CalcVorNachteilEnergieMod(VorNachteil.Kurzatmig);
                return mod;
            }
        }

        [DependentProperty("AusdauerBasis"), DependentProperty("AusdauerMod")]
        [DependsOnModifikator(typeof(Mod.IModAU))]
        public int AusdauerMax {
            get {
                int e = AusdauerBasis + AusdauerMod;
                if (Modifikatoren != null)
                    Modifikatoren.Where(m => m is Mod.IModAU).Select(m => (Mod.IModAU)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => e = m.ApplyAUMod(e));
                return e;
            }
        }

        [DependentProperty("AusdauerAktuell"), DependentProperty("AusdauerMax")]
        public string AusdauerStatus {
            get {
                return GetAusdauerStatus();
            }
        }
        #endregion

        #region Karmaenergie

        public bool Geweiht {
            get {
                if (Regelsystem == "DSA 4.1")
                    return HatVorNachteil(VorNachteil.GeweihtZwölfgöttlicheKirche) || HatVorNachteil(VorNachteil.GeweihtNichtAlveranischeGottheit) 
                        || HatVorNachteil(VorNachteil.GeweihtHRanga) || HatVorNachteil(VorNachteil.GeweihtGravesh) || HatVorNachteil(VorNachteil.GeweihtAngrosch) 
                        || HatVorNachteil(VorNachteil.Sacerdos) || HatVorNachteil(VorNachteil.GeweihtXoArtal)
                        || HatSonderfertigkeitUndVoraussetzungen(Sonderfertigkeit.SpätweiheAlveranischeGottheit) || HatSonderfertigkeitUndVoraussetzungen(Sonderfertigkeit.SpätweiheNamenloser) 
                        || HatSonderfertigkeitUndVoraussetzungen(Sonderfertigkeit.SpätweiheNichtAlveranischeGottheit) || HatSonderfertigkeitUndVoraussetzungen(Sonderfertigkeit.KontaktZumGroßenGeist) 
                        || HatSonderfertigkeitUndVoraussetzungen(Sonderfertigkeit.SpätweiheDunkleZeitenIII) || HatSonderfertigkeitUndVoraussetzungen(Sonderfertigkeit.SpätweiheDunkleZeitenII) || HatSonderfertigkeitUndVoraussetzungen(Sonderfertigkeit.SpätweiheDunkleZeitenI)
                        || HatSonderfertigkeitUndVoraussetzungen(Sonderfertigkeit.SpätweiheXoArtal);
                else if (Regelsystem == "DSA 5")
                    return HatVorNachteil(VorNachteil.Geweihter);
                return false;
            }
        }

        public string KarmaenergieGrundwertFormel
        {
            get
            {
                string ke = string.Empty;
                if (Regelsystem == "DSA 5")
                    ke = "Leiteigenschaft der Geweihtentradition";
                return ke;
            }
        }

        [DependentProperty("LeiteigenschaftKlerikal"), DependentProperty("BaseMU"), DependentProperty("BaseKL"), DependentProperty("BaseIN"), DependentProperty("BaseCH"), DependentProperty("BaseFF"), DependentProperty("BaseGE"), DependentProperty("BaseKO"), DependentProperty("BaseKK")]
        public int BaseLeiteigenschaftKarmal
        {
            get
            {
                return EigenschaftWert(LeiteigenschaftKlerikal, true);
            }
        }

        [DependentProperty("BaseLeiteigenschaftKarmal")]
        public int KarmaenergieBasis
        {
            get
            {
                int ke = 0;
                if (Regelsystem == "DSA 5")
                    ke = BaseLeiteigenschaftKarmal;
                return ke;
            }
        }

        [DependentProperty("KarmaenergieModSonstiges"), DependentProperty("KarmaenergieModGenerierung"), DependentProperty("KarmaenergieModVorNachteile"), DependentProperty("KarmaenergieModZukauf"), DependentProperty("KarmaenergieMod_pKaP")]
        public int KarmaenergieMod
        {
            get { return KarmaenergieModGenerierung + KarmaenergieModSonstiges + KarmaenergieModVorNachteile + KarmaenergieModZukauf - KarmaenergieMod_pKaP; }
        }

        [DependentProperty("KE_pKaP")]
        public int KarmaenergieMod_pKaP
        {
            get { return KE_pKaP ?? 0; }
            set
            {
                KE_pKaP = value;
                OnChanged("KarmaenergieMod_pKaP");
            }
        }

        [DependentProperty("KE_Mod")]
        public int KarmaenergieModSonstiges
        {
            get { return KE_Mod ?? 0; }
            set
            {
                KE_Mod = value;
                OnChanged("KarmaenergieModSonstiges");
            }
        }

        [DependentProperty("KE_ModGen")]
        public int KarmaenergieModGenerierung
        {
            get { return KE_ModGen ?? 0; }
            set
            {
                KE_ModGen = value;
                OnChanged("KarmaenergieModGenerierung");
            }
        }

        [DependentProperty("KE_ModZukauf")]
        public int KarmaenergieModZukauf
        {
            get { return KE_ModZukauf ?? 0; }
            set
            {
                KE_ModZukauf = value;
                OnChanged("KarmaenergieModZukauf");
            }
        }

        [DependentProperty("Nachteile"), DependentProperty("Vorteile")]
        public int KarmaenergieModVorNachteile
        {
            get
            {
                int mod = 0;
                if (Regelsystem == "DSA 4.1")
                {
                    if (HatVorNachteil(VorNachteil.GeweihtZwölfgöttlicheKirche) || HatVorNachteil(VorNachteil.GeweihtHRanga) || HatVorNachteil(VorNachteil.GeweihtGravesh) 
                        || HatVorNachteil(VorNachteil.GeweihtAngrosch) || HatVorNachteil(VorNachteil.GeweihtXoArtal) || HatSonderfertigkeit(Sonderfertigkeit.SpätweiheAlveranischeGottheit)
                        || HatSonderfertigkeit(Sonderfertigkeit.SpätweiheNamenloser) || HatSonderfertigkeit(Sonderfertigkeit.SpätweiheXoArtal))
                        mod += 24;
                    else if (HatVorNachteil(VorNachteil.GeweihtNichtAlveranischeGottheit) || HatSonderfertigkeit(Sonderfertigkeit.SpätweiheNichtAlveranischeGottheit) 
                        || HatSonderfertigkeit(Sonderfertigkeit.KontaktZumGroßenGeist) || HatSonderfertigkeit(Sonderfertigkeit.SpätweiheDunkleZeitenII))
                        mod += 12;
                    else if (HatVorNachteil(VorNachteil.Sacerdos))
                        mod += VorNachteilWertInt(VorNachteil.Sacerdos).GetValueOrDefault(0) * 6;
                    else if (HatSonderfertigkeit(Sonderfertigkeit.SpätweiheDunkleZeitenIII))
                        mod += 18;
                    else if (HatSonderfertigkeit(Sonderfertigkeit.SpätweiheDunkleZeitenI))
                        mod += 6;
                }
                else if (Regelsystem == "DSA 5")
                {
                    mod += CalcVorNachteilEnergieMod(VorNachteil.HoheKarmalkraft);
                    mod += CalcVorNachteilEnergieMod(VorNachteil.NiedrigeKarmalkraft);
                    if (HatVorNachteil(VorNachteil.Geweihter))
                        mod += 20;
                }
                return mod;
            }
        }

        [DependentProperty("KE_Aktuell")]
        public int KarmaenergieAktuell {
            get {
                return KE_Aktuell ?? 0;
            }
            set {
                KE_Aktuell = value;
            }
        }

        [DependsOnModifikator(typeof(Mod.IModKE))]
        [DependentProperty("KarmaenergieBasis"), DependentProperty("KarmaenergieMod")]
        public int KarmaenergieMax {
            get {
                int e = KarmaenergieBasis + KarmaenergieMod;
                if (Modifikatoren != null)
                    Modifikatoren.Where(m => m is Mod.IModKE).Select(m => (Mod.IModKE)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => e = m.ApplyKEMod(e));
                return e;
            }
        }

        #endregion

        #region Astralenergie

        public bool Magiebegabt {
            get {
                if (Regelsystem == "DSA 4.1")
                    return HatVorNachteil(VorNachteil.Vollzauberer) || HatVorNachteil(VorNachteil.Halbzauberer) || HatVorNachteil(VorNachteil.Viertelzauberer) || HatVorNachteil(VorNachteil.ViertelzaubererUnbewusst);
                else if (Regelsystem == "DSA 5")
                    return HatVorNachteil(VorNachteil.Zauberer);
                return false;
                
            }
        }

        public string AstralenergieGrundwertFormel
        {
            get
            {
                string ae = string.Empty;
                if (Regelsystem == "DSA 4.1")
                {
                    if (HatSonderfertigkeitUndVoraussetzungen(Sonderfertigkeit.GefäßDerSterne))
                        ae = "(MU + IN + CH + CH) / 2";
                    else
                        ae = "(MU + IN + CH) / 2";
                }
                else if (Regelsystem == "DSA 5")
                    ae = "Leiteigenschaft der Zauberertradition";
                return ae;
            }
        }

        [DependentProperty("BaseMU"), DependentProperty("BaseIN"), DependentProperty("BaseCH"), DependentProperty("BaseLeiteigenschaftMagisch")]
        public int AstralenergieBasis {
            get {
                int basis = 0;
                if (Regelsystem == "DSA 4.1")
                {
                    basis = BaseMU + BaseIN + BaseCH;
                    if (HatSonderfertigkeitUndVoraussetzungen(Sonderfertigkeit.GefäßDerSterne))
                        basis += BaseCH;
                    basis = (int)Math.Round(basis / 2.0, 0, MidpointRounding.AwayFromZero);
                }
                else if (Regelsystem == "DSA 5")
                    basis = BaseLeiteigenschaftMagisch;
                return basis;
            }
        }

        /// <summary>
        /// Gibt den Wert der magischen Leiteigenschaft zurück.
        /// </summary>
        [DependentProperty("LeiteigenschaftMagisch"), DependentProperty("BaseMU"), DependentProperty("BaseKL"), DependentProperty("BaseIN"), DependentProperty("BaseCH"), DependentProperty("BaseFF"), DependentProperty("BaseGE"), DependentProperty("BaseKO"), DependentProperty("BaseKK")]
        public int BaseLeiteigenschaftMagisch
        {
            get
            {
                return EigenschaftWert(LeiteigenschaftMagisch, true);
            }
        }

        [DependentProperty("AstralenergieModSonstiges"), DependentProperty("AstralenergieModGenerierung"), DependentProperty("AstralenergieModVorNachteile"), DependentProperty("AstralenergieModZukauf"), DependentProperty("AstralenergieMod_pAsP")]
        public int AstralenergieMod
        {
            get { return AstralenergieModGenerierung + AstralenergieModSonstiges + AstralenergieModVorNachteile + AstralenergieModZukauf - AstralenergieMod_pAsP; }
        }

        [DependentProperty("AE_pAsP")]
        public int AstralenergieMod_pAsP
        {
            get { return AE_pAsP ?? 0; }
            set
            {
                AE_pAsP = value;
                OnChanged("AstralenergieMod_pAsP");
            }
        }

        [DependentProperty("AE_Mod")]
        public int AstralenergieModSonstiges
        {
            get { return AE_Mod ?? 0; }
            set
            {
                AE_Mod = value;
                OnChanged("AstralenergieModSonstiges");
            }
        }

        [DependentProperty("AE_ModGen")]
        public int AstralenergieModGenerierung
        {
            get { return AE_ModGen ?? 0; }
            set
            {
                AE_ModGen = value;
                OnChanged("AstralenergieModGenerierung");
            }
        }

        [DependentProperty("AE_ModZukauf")]
        public int AstralenergieModZukauf
        {
            get { return AE_ModZukauf ?? 0; }
            set
            {
                AE_ModZukauf = value;
                OnChanged("AstralenergieModZukauf");
            }
        }

        [DependentProperty("Nachteile"), DependentProperty("Vorteile")]
        public int AstralenergieModVorNachteile
        {
            get
            {
                int mod = 0;
                if (Regelsystem == "DSA 4.1")
                {
                    mod += CalcVorNachteilEnergieMod(VorNachteil.Astralmacht, 2);
                    if (HatVorNachteil(VorNachteil.Vollzauberer))
                        mod += 12;
                    if (HatVorNachteil(VorNachteil.Halbzauberer))
                        mod += 6;
                    if (HatVorNachteil(VorNachteil.Viertelzauberer) || HatVorNachteil(VorNachteil.ViertelzaubererUnbewusst))
                        mod -= 6;
                    if (HatVorNachteil(VorNachteil.Zauberhaar))
                        mod += 7;
                }
                else if (Regelsystem == "DSA 5")
                {
                    mod += CalcVorNachteilEnergieMod(VorNachteil.HoheAstralkraft);
                    if (HatVorNachteil(VorNachteil.Zauberer))
                        mod += 20;
                }
                mod += CalcVorNachteilEnergieMod(VorNachteil.NiedrigeAstralkraft);
                return mod;
            }
        }

        [DependentProperty("AE_Aktuell")]
        public int AstralenergieAktuell {
            get {
                return AE_Aktuell ?? 0;
            }
            set {
                AE_Aktuell = value;
            }
        }

        [DependsOnModifikator(typeof(Mod.IModAE))]
        [DependentProperty("AstralenergieBasis"), DependentProperty("AstralenergieMod")]
        public int AstralenergieMax {
            get {
                // wenn er einen Zauberervorteil hat, sonst 0
                if (Magiebegabt) {
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

        public int MagieresistenzBasis {
            get {
                return (int)Math.Round((BaseMU + BaseKL + BaseKO) / 5.0, 0, MidpointRounding.AwayFromZero);
            }
        }

        [DependentProperty("MR_Mod")]
        public int MagieresistenzMod {
            get {
                return MR_Mod ?? 0;
            }
            set {
                MR_Mod = value;
                //OnPropertyChanged(string.Empty);
            }
        }

        [DependentProperty("MR_Mod"), DependentProperty("MR"), DependentProperty("KL"), DependentProperty("KO")]
        public int MagieresistenzOhneMod {
            get {
                return MagieresistenzBasis + MagieresistenzMod;
            }
        }

        [DependentProperty("MR_Mod"), DependentProperty("MR"), DependentProperty("KL"), DependentProperty("KO")]
        [DependsOnModifikator(typeof(Mod.IModMR))]
        public int Magieresistenz {
            get {
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
        public string Wundschwellen {
            get {
                return string.Format("{0} / {1} / {2}", Wundschwelle, Wundschwelle2, Wundschwelle3);
            }
        }

        [DependentProperty("Konstitution")]
        public int Wundschwelle {
            get {
                int ko = E.WundenVerändernWundschwelle ? Konstitution : KonstitutionOhneWunden;
                int ws = Convert.ToInt32(Math.Round(ko / 2.0, 0, MidpointRounding.AwayFromZero));
                if (HatVorNachteil(VorNachteil.Eisern))
                    ws += 2;
                if (HatVorNachteil(VorNachteil.Glasknochen))
                    ws -= 2;
                return ws;
            }
        }

        [DependentProperty("Konstitution")]
        public int Wundschwelle2 {
            get {
                int ko = E.WundenVerändernWundschwelle ? Konstitution : KonstitutionOhneWunden;
                int ws = ko;
                if (HatVorNachteil(VorNachteil.Eisern))
                    ws += 2;
                if (HatVorNachteil(VorNachteil.Glasknochen))
                    ws -= 2;
                return ws;
            }
        }

        [DependentProperty("Konstitution")]
        public int Wundschwelle3 {
            get {
                int ko = E.WundenVerändernWundschwelle ? Konstitution : KonstitutionOhneWunden;
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

        [DependentProperty("InitiativeModGen"), DependentProperty("MU"), DependentProperty("IN"), DependentProperty("GE")]
        public int InitiativeBasisOhneSonderfertigkeiten {
            get {
                return (int)Math.Round((BaseMU * 2 + BaseIN + BaseGE) / 5.0, 0, MidpointRounding.AwayFromZero) + InitiativeModGen;
            }
        }

        [DependentProperty("InitiativeBasisOhneSonderfertigkeiten")]
        public int InitiativeBasisOhneMod {
            get {
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
        public int InitiativeBasis {
            get {
                int ini = InitiativeBasisOhneMod;
                if (Modifikatoren != null)
                    Modifikatoren.Where(m => m is Mod.IModINIBasis).Select(m => (Mod.IModINIBasis)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => ini = m.ApplyINIBasisMod(ini));
                return ini;
            }
        }

        [DependentProperty("INI_Mod")]
        public int InitiativeModGen {
            get {
                return INI_Mod ?? 0;
            }
            set {
                INI_Mod = value;
            }
        }

        public WürfelEnum InitiativeZufall {
            get {
                if (HatSonderfertigkeitUndVoraussetzungen(Sonderfertigkeit.Klingentänzer) && Behinderung <= 2) {
                    return WürfelEnum._2W6;
                }
                return WürfelEnum._1W6;
            }
        }

        #endregion

        #region Attacke/Parade

        [DependentProperty("BaseMU"), DependentProperty("BaseGE"), DependentProperty("BaseKK")]
        public int AttackeBasisOhneMod {
            get {
                return (int)Math.Round((BaseMU + BaseGE + BaseKK) / 5.0, 0, MidpointRounding.AwayFromZero);
            }
        }

        [DependentProperty("AttackeBasisOhneMod")]
        [DependsOnModifikator(typeof(Mod.IModATBasis))]
        public int AttackeBasis {
            get {
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
        public int Attacke {
            get {
                int v = AttackeBasis;
                if (Modifikatoren != null)
                    Modifikatoren.Where(m => m is Mod.IModAT).Select(m => (Mod.IModAT)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => v = m.ApplyATMod(v));
                return v;
            }
        }

        [DependentProperty("BaseIN"), DependentProperty("BaseGE"), DependentProperty("BaseKK")]
        public int ParadeBasisOhneMod {
            get {
                return (int)Math.Round((BaseIN + BaseGE + BaseKK) / 5.0, 0, MidpointRounding.AwayFromZero);
            }
        }

        [DependentProperty("ParadeBasisOhneMod")]
        [DependsOnModifikator(typeof(Mod.IModPABasis))]
        public int ParadeBasis {
            get {
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
        public int Parade {
            get {
                int v = ParadeBasis;
                if (Modifikatoren != null)
                    Modifikatoren.Where(m => m is Mod.IModPA).Select(m => (Mod.IModPA)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => v = m.ApplyPAMod(v));
                return v;
            }
        }

        #endregion

        #region Fernkampf

        [DependentProperty("BaseIN"), DependentProperty("BaseFF"), DependentProperty("BaseKK")]
        public int FernkampfBasisOhneMod {
            get {
                return (int)Math.Round((BaseIN + BaseFF + BaseKK) / 5.0, 0, MidpointRounding.AwayFromZero);
            }
        }

        [DependentProperty("FernkampfBasisOhneMod")]
        [DependsOnModifikator(typeof(Mod.IModPABasis))]
        public int FernkampfBasis {
            get {
                int v = FernkampfBasisOhneMod;
                if (Modifikatoren != null)
                    Modifikatoren.Where(m => m is Mod.IModFKBasis).Select(m => (Mod.IModFKBasis)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => v = m.ApplyFKBasisMod(v));
                return v;
            }
        }

        /// <summary>
        /// Grund-FK-Wert inkl. Abzüge.
        /// </summary>
        [DependentProperty("FernkampfBasis")]
        [DependsOnModifikator(typeof(Mod.IModFK))]
        public int Fernkampf {
            get {
                int v = FernkampfBasis;
                if (Modifikatoren != null)
                    Modifikatoren.Where(m => m is Mod.IModFK).Select(m => (Mod.IModFK)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => v = m.ApplyFKMod(v));
                return v;
            }
        }
        #endregion

        #region Talente

        public Talent AddTalent(string tName, int wert) {
            Talent talent = Global.ContextTalent.TalentListe.Where(t => t.Talentname == tName).SingleOrDefault();

            if (talent == null)
                throw new System.ArgumentNullException("Talent nicht gefunden.");

            return AddTalent(talent, wert, null, null);
        }

        public Talent AddTalent(Talent t, int wert) {
            return AddTalent(t, wert, null, null);
        }

        public Talent AddTalent(Talent t, int wert, int? zuteilungAT, int? zuteilungPA) {
            if (t == null)
                return null;
            IEnumerable<Held_Talent> existierendeZuordnung = Held_Talent.Where(hta => hta.TalentGUID == t.TalentGUID && hta.HeldGUID == HeldGUID);
            if (existierendeZuordnung.Count() != 0) {
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

        public void DeleteTalent(string talentname) {
            if (HatTalent(talentname))
                DeleteTalent(Held_Talent.Where(h => h.Talent.Talentname == talentname).FirstOrDefault());
        }

        public void DeleteTalent(Held_Talent ht) {
            if (ht != null) {
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
        /// Held_Talent nach talentName.
        /// exactMatch==false ermöglicht suche nach dem Muster talentName*.
        /// Bei Mehrfachtreffern wird der Eintrag mit dem höchsten Modifizierten TaW zurückgegeben.
        /// </summary>
        public Held_Talent GetHeldTalent(string talentName, bool nurPositiv, out int taw, bool exactMatch = true) {
            Held_Talent ret = null;
            int maxtaw = Int32.MinValue;
            foreach (Model.Held_Talent ht in Held_Talent.Where(h => (exactMatch && h.Talent.Talentname == talentName) || (!exactMatch && h.Talent.Talentname.StartsWith(talentName)))) {
                int _taw = ht.TaW ?? 0;
                if (Modifikatoren != null) {
                    List<Mod.IModTalentwert> l = Modifikatoren.Where(m => m is Mod.IModTalentwert && (((Mod.IModTalentwert)m).Talentname == null || ((Mod.IModTalentwert)m).Talentname.Count == 0 || ((Mod.IModTalentwert)m).Talentname.Contains(ht.Talent.Talentname))).Select(m => (Mod.IModTalentwert)m).OrderBy(m => m.Erstellt).ToList();
                    foreach (Mod.IModTalentwert m in l) {
                        int tawneu = m.ApplyTalentwertMod(_taw);
                        if (!nurPositiv || tawneu > _taw)
                            _taw = tawneu;
                    }
                }
                if (maxtaw < _taw) {
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
        public int Talentwert(string talentName, bool nurPositiv, bool exactMatch = true) {
            if (String.IsNullOrWhiteSpace(talentName))
                return 0;
            int maxtaw = 0;
            Held_Talent ht = GetHeldTalent(talentName, nurPositiv, out maxtaw, exactMatch);
            return (maxtaw == Int32.MinValue) ? 0 : maxtaw;
        }

        /// <summary>
        /// Der TaW eines Talentes.
        /// </summary>
        public int Talentwert(string talentName) {
            return Talentwert(talentName, false);
        }

        /// <summary>
        /// Der TaW eines Talentes.
        /// </summary>
        public int Talentwert(Talent t) {
            return Talentwert(t.Talentname, false);
        }

        /// <summary>
        /// Der TaW eines Talentes.
        /// </summary>
        public int Talentwert(Talent t, bool nurPositiv) {
            if (t == null)
                return 0;
            return Talentwert(t.Talentname, nurPositiv);
        }

        /// <summary>
        /// Hat mindestens den angegebenen TaW
        /// </summary>
        public bool HatTalent(string talentname, int taw, bool exactMatch = true) {
            if (!HatTalent(talentname, exactMatch))
                return false;
            if (taw == int.MinValue)
                return true;
            return Talentwert(talentname, true) >= taw;
        }

        public bool HatTalent(Talent t) {
            return Held_Talent.Where(ht => ht.Talent == t).Count() > 0;
        }

        public bool HatTalent(string talentname, bool exactMatch = true) {
            return Held_Talent.Where(ht => (exactMatch && ht.Talent.Talentname == talentname) || (!exactMatch && ht.Talent.Talentname.StartsWith(talentname))).Count() > 0;
        }

        /// <summary>
        /// Hat mindestens den angegebenen TaW
        /// </summary>
        public bool HatTalent(Talent t, int taw) {
            return HatTalent(t.Talentname, taw);
        }

        public List<string> Talentspezialisierungen(string talentName) {
            //TODO ??: bei GUID Umstellung statt Sonderfertigkeit.Name evtl auf GUID prüfen
            if (Held_Sonderfertigkeit != null) {
                List<string> r = Held_Sonderfertigkeit.Where(hs => hs.Sonderfertigkeit.Name == "Talentspezialisierung" && hs.Wert != null && hs.Wert.StartsWith(talentName)).OrderBy(hs => hs.Wert).Select(hs => hs.Wert).ToList();
                r.ForEach(w => w = Talent.GetSpezialisierungName(talentName, w));
            }
            return null;
        }

        public List<string> Talentspezialisierungen(Talent t) {
            if (t == null)
                return null;
            return Talentspezialisierungen(t.Talentname);
        }

        public void AddBasisTalente() {
            foreach (Talent t in Global.ContextHeld.Liste<Talent>().Where(t => t.Talenttyp == "Basis" && t.TalentgruppeID != 0 && t.Regelsystem == Global.Regeledition).ToList()) {
                if (t.TalentgruppeID != 1)
                    AddTalent(t, 0);
                else
                    AddTalent(t, Global.DSA5 ? 6 : 0, 0, 0);
            }
        }

        /// <summary>
        /// Würfelt eine Probe auf das angegebene Talent.
        /// </summary>
        /// <returns></returns>
        public ProbenErgebnis TalentProbe(Talent t, int mod, string spezialisierung = null) {
            if (!HatTalent(t)) //TODO: stattdessen Ableiten.
            {
                ProbenErgebnis pe = new ProbenErgebnis();
                pe.Ergebnis = ErgebnisTyp.KEIN_ERGEBNIS;
                return pe;
            }
            t.WerteSetzen(this, spezialisierung);
            t.Modifikator = mod;
            t.IsBehinderung = true;
            if (false) //automatisch würfeln
            {
                //t.Modifikator += t.BehinderungEff;
                //return t.Würfeln();
            } else //per Dialog würfeln
            {
                return View.General.ViewHelper.ShowProbeDialog(t, this);
            }
        }

        #endregion

        #region Zauber

        public Zauber AddZauber(Zauber z, int wert, string rep) {
            if (z == null)
                return null;
            IEnumerable<Held_Zauber> existierendeZuordnung = Held_Zauber.Where(hza => hza.ZauberGUID == z.ZauberGUID
                && hza.Repräsentation == rep
                && hza.HeldGUID == HeldGUID);
            if (existierendeZuordnung.Count() != 0) {
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
        public bool HatZauber(Zauber z, string rep) {
            return Held_Zauber.Where(hz => hz.Zauber == z && hz.Repräsentation == rep).Count() > 0;
        }

        public bool HatZauber(Guid z, string rep) {
            return Held_Zauber.Where(hz => hz.ZauberGUID == z && hz.Repräsentation == rep).Count() > 0;
        }

        public bool HatZauber(string zaubername, bool exactMatch = true) {
            return Held_Zauber.Where(hz => (exactMatch && hz.Zauber.Name.ToUpperInvariant() == zaubername.ToUpperInvariant()) || (!exactMatch && hz.Zauber.Name.ToUpperInvariant().StartsWith(zaubername.ToUpperInvariant()))).Count() > 0;
        }

        //TODO JT: Eventuell auch nur auf den anfang des Zaubernamens abprüfen
        public bool HatZauber(string zaubername, int zfw, bool exactMatch = true) {
            if (!HatZauber(zaubername, exactMatch))
                return false;
            if (zfw == int.MinValue)
                return true;
            return Zauberfertigkeitswert(zaubername, true, exactMatch) >= zfw;
        }

        /// <summary>
        /// Held_Zauber nach zauberName.
        /// exactMatch==false ermöglicht suche nach dem Muster zauberName*.
        /// Bei Mehrfachtreffern wird der Eintrag mit dem höchsten Modifizierten ZfW zurückgegeben.
        /// </summary>
        public Held_Zauber GetHeldZauber(string zauberName, bool nurPositiv, out int zfw, bool exactMatch = true)
        {
            Held_Zauber ret = null;
            int maxzfw = Int32.MinValue;
            foreach (Model.Held_Zauber ht in Held_Zauber.Where(h => (exactMatch && h.Zauber.Name == zauberName) || (!exactMatch && h.Zauber.Name.StartsWith(zauberName))))
            {
                int _zfw = ht.ZfW ?? 0;
                if (Modifikatoren != null)
                {
                    List<Mod.IModZauberwert> l = Modifikatoren.Where(m => m is Mod.IModZauberwert && (((Mod.IModZauberwert)m).Zaubername == null || ((Mod.IModZauberwert)m).Zaubername.Count == 0 || ((Mod.IModZauberwert)m).Zaubername.Contains(ht.Zauber.Name))).Select(m => (Mod.IModZauberwert)m).OrderBy(m => m.Erstellt).ToList();
                    foreach (Mod.IModZauberwert m in l)
                    {
                        int zfneu = m.ApplyZauberwertMod(_zfw);
                        if (!nurPositiv || zfneu > _zfw)
                            _zfw = zfneu;
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
        public int Zauberfertigkeitswert(string zauberName, bool nurPositiv = false, bool exactMatch = true) {
            Model.Held_Zauber hz = Held_Zauber.Where(h => (exactMatch && h.Zauber.Name.ToUpperInvariant() == zauberName.ToUpperInvariant()) || (!exactMatch && h.Zauber.Name.ToUpperInvariant().StartsWith(zauberName.ToUpperInvariant()))).FirstOrDefault();
            if (hz == null)
                return 0;
            int zfw = hz.ZfW ?? 0;
            if (Modifikatoren != null) {
                List<Mod.IModZauberwert> l = Modifikatoren.Where(m => m is Mod.IModZauberwert && (((Mod.IModZauberwert)m).Zaubername == null || ((Mod.IModZauberwert)m).Zaubername.Count == 0 || ((Mod.IModZauberwert)m).Zaubername.Contains(hz.Zauber.Name))).Select(m => (Mod.IModZauberwert)m).OrderBy(m => m.Erstellt).ToList();
                foreach (Mod.IModZauberwert m in l) {
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
        public List<Zauber> ZauberWählbar {
            get {
                return Global.ContextZauber.ZauberListe.OrderBy(z => z.Name).ToList();
            }
        }

        /// <summary>
        /// Die erlernten Repräsentationen des Helden.
        /// </summary>
        public IDictionary<Sonderfertigkeit, string> Repräsentationen {
            get {
                return Held_Sonderfertigkeit.Where(hsf => hsf.Sonderfertigkeit.Name.StartsWith("Repräsentation")).ToDictionary(hsf => hsf.Sonderfertigkeit, hsf => hsf.Wert);
            }
        }

        public string RepräsentationStandard {
            get {
                var rep = Repräsentationen.Select(r => r.Key.Name.Replace("Repräsentation (", string.Empty).TrimEnd(')')).ToList();

                if (rep != null && rep.Count == 1) {
                    return Logic.General.Repräsentationen.GetKürzel(rep[0]);
                } else if (rep != null && rep.Count > 1) {
                    // Held hat mehrerer Repräsentationen.
                    // Ermitteln welche Repäsentation mit den meisten Zaubern vertren ist.

                    var maxRep = Held_Zauber.GroupBy(hz => hz.Repräsentation).OrderByDescending(r => r.Count());
                    return maxRep.FirstOrDefault().Key;
                } else
                    return "Mag";
            }
        }

        #endregion

        #region Vor/Nachteile

        public VorNachteil AddVorNachteil(string vorNachName, string wert = "") {
            VorNachteil vorNach = Global.ContextHeld.Liste<VorNachteil>().Where(vn => vn.Name == vorNachName).FirstOrDefault();
            return AddVorNachteil(vorNach, vorNach.Kosten, wert);
        }

        public VorNachteil AddVorNachteil(VorNachteil vn, double? kosten, string wert = "") {
            if (vn == null)
                return null;
            IEnumerable<Held_VorNachteil> existierendeZuordnung = Held_VorNachteil.Where(heldvn => heldvn.VorNachteilGUID == vn.VorNachteilGUID && heldvn.HeldGUID == HeldGUID);
            if (existierendeZuordnung.Count() != 0)
            { //es gibt bereits einen solchen VoNachteil auf dem helden
                if (!vn.HatWert ?? false)
                    //Da es eine ohne Wert ist, darf sie nur einmal vergeben werden
                    return existierendeZuordnung.First().VorNachteil;
                else if (existierendeZuordnung.Where(hvn1 => hvn1.Wert == wert || (string.IsNullOrWhiteSpace(wert) && string.IsNullOrWhiteSpace(hvn1.Wert))).Count() != 0)
                    //Wenn sie mit diesem Wert bereits existiert, dann darf sie auch nicht nochmal hinzugefügt werden.
                    return existierendeZuordnung.Where(hvn1 => hvn1.Wert == wert || (string.IsNullOrWhiteSpace(wert) && string.IsNullOrWhiteSpace(hvn1.Wert))).First().VorNachteil;
            }

            Held_VorNachteil hvn = new Model.Held_VorNachteil();
            hvn.HeldGUID = HeldGUID;
            hvn.Held = this;

            hvn.VorNachteilGUID = vn.VorNachteilGUID;
            hvn.VorNachteil = vn;

            // Kosten (AP bzw. GP)
            if (kosten == null)
                hvn.Kosten = vn.Kosten ?? 0.0;
            else
                hvn.Kosten = kosten ?? 0.0;

            hvn.Wert = wert ?? "";
            if (hvn.Wert == "" && vn.WertTyp == "int")
            {
                if (vn.WertMin.HasValue)
                    hvn.Wert = vn.WertMin.Value.ToString();
            }
            else if (hvn.Wert == "" && vn.Auswahl != null)
                hvn.Wert = vn.Auswahl; // mit Auswahl-Wert vorbelegen
            if (vn.Vorteil != null) {
                if ((bool)vn.Vorteil) {
                    hvn.VorNachteil.Vorteil = true;
                }
            } else if (vn.Nachteil != null) {
                if ((bool)vn.Nachteil) {
                    hvn.VorNachteil.Nachteil = true;
                }
            } else {
                throw new System.ArgumentNullException("Weder Vor noch Nachteil gesetzt");
            }
            //check
            if (!((bool)hvn.VorNachteil.Vorteil || (bool)hvn.VorNachteil.Nachteil)) {
                throw new System.ArgumentNullException("Weder Vor noch Nachteil gesetzt");
            }

            Held_VorNachteil.Add(hvn);

            // abhängige Talente automatisch einfügen
            // TODO ??: Ins Datenmodell einbauen. Eigenes DB-Feld mit Talentabhängigkeit.
            if (vn.Name == VorNachteil.Empathie)
                AddTalent("Empathie", 3);
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
            else if (vn.Name == VorNachteil.TierempathieAlle || vn.Name == VorNachteil.TierempathieSpeziell)
                AddTalent("Tierempathie", 3);

            // Gesamt-Kosten aktualisieren
            if (hvn.VorNachteil.Vorteil ?? false)
                SummeVorteile += hvn.KostenGesamt;
            if (hvn.VorNachteil.Nachteil ?? false)
                SummeNachteile += hvn.KostenGesamt;
            hvn.PropertyChanged += Hvn_PropertyChanged;

            return vn;
        }

        public void DeleteVorNachteil(string vnName) {
            if (HatVorNachteil(vnName))
                DeleteVorNachteil(Held_VorNachteil.Where(h => h.VorNachteil.Name == vnName).FirstOrDefault());
        }

        public void DeleteVorNachteil(Held_VorNachteil hvn) {
            if (hvn != null) {
                string vnName = hvn.VorNachteil.Name;

                // Gesamt-Kosten aktualisieren
                if (hvn.VorNachteil.Vorteil ?? false)
                    SummeVorteile -= hvn.KostenGesamt;
                if (hvn.VorNachteil.Nachteil ?? false)
                    SummeNachteile -= hvn.KostenGesamt;
                hvn.PropertyChanged -= Hvn_PropertyChanged;

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
                else if (vnName == VorNachteil.TierempathieAlle || vnName == VorNachteil.TierempathieSpeziell)
                    DeleteTalent("Tierempathie");
            }
        }

        /// <summary>
        /// Gibt den Wert eines VorNachteils zurück.
        /// </summary>
        /// <param name="vn">Axakter Name des VorNachteils.</param>
        /// <returns></returns>
        public string VorNachteilWert(string vn)
        {
            var heldVN= Held_VorNachteil.Where(hvn => hvn.VorNachteil != null && hvn.VorNachteil.Name == vn).FirstOrDefault();
            string val = heldVN != null ? heldVN.Wert : null;

            return val;
        }
        /// <summary>
        /// Gibt den Integer Wert eines VorNachteils zurück.
        /// </summary>
        /// <param name="vn">Axakter Name des VorNachteils.</param>
        /// <returns></returns>
        public Nullable<int> VorNachteilWertInt(string vn)
        {
            var heldVN = Held_VorNachteil.Where(hvn => hvn.VorNachteil != null && hvn.VorNachteil.Name == vn).FirstOrDefault();
            return heldVN != null ? heldVN.WertInt : null;
        }

        public bool HatVorNachteil(VorNachteil vn) {
            return Held_VorNachteil.Where(hvn => hvn.VorNachteil != null && hvn.VorNachteil == vn).Count() > 0;
        }

        public bool HatVorNachteil(string vn, bool exactMatch = true) {
            return HatVorNachteil(vn, null, exactMatch);
        }

        public bool HatVorNachteil(string vn, string wert, bool exactMatch = true) {
            string m1 = vn, m2 = string.Empty;
            if (!exactMatch) {
                var a = vn.Split('%');
                if (a.Length > 2) {
                    m1 = a[0];
                    m2 = a[1];
                }
            }
            List<Held_VorNachteil> hvnList = Held_VorNachteil.Where(hvn2 => hvn2.VorNachteil != null && ((exactMatch && hvn2.VorNachteil.Name == vn)
                || (
                    !exactMatch && hvn2.VorNachteil.Name.StartsWith(m1)
                    && (m2 == string.Empty || hvn2.VorNachteil.Name.EndsWith(m2))
                    )
                )).ToList();
            foreach (var hvn in hvnList) {
                //Wert abprüfen
                if (wert != null && (hvn.VorNachteil.HatWert ?? false)) {
                    if (hvn.VorNachteil.WertTyp != null && hvn.VorNachteil.WertTyp.ToLowerInvariant() == "int") {
                        int expected, actual;
                        if (int.TryParse(wert, out expected) && int.TryParse(hvn.Wert, out actual)) {
                            if (actual >= expected)
                                return true;
                            continue;
                        } else {
                            System.Diagnostics.Debug.WriteLine("Fehler beim Parsen des Wertes {0} oder {1} zu einem Integer. HatVorNachteil({2},{0})", wert, hvn.Wert, vn);
                            continue;
                        }
                    } else if (hvn.Wert == wert)
                        return true;
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Die Vorteile des Helden.
        /// Nicht zum ändern von Werten, da die Werte in Held_VorNachteil stehen.
        /// </summary>
        public IDictionary<VorNachteil, ICollection<string>> Vorteile {
            get {
                Dictionary<VorNachteil, ICollection<string>> d = new Dictionary<VorNachteil, ICollection<string>>();
                foreach (var hvn in Held_VorNachteil.Where(hvn => hvn.VorNachteil != null && hvn.VorNachteil.Vorteil == true))
                {
                    if (!d.ContainsKey(hvn.VorNachteil))
                        d.Add(hvn.VorNachteil, new List<string> { });
                    d[hvn.VorNachteil].Add(hvn.Wert);
                }
                return d;
            }
        }

        /// <summary>
        /// Die Vorteile, die der Held noch wählen kann.
        /// </summary>
        public List<VorNachteil> VorteileWählbar {
            get {
                return Global.ContextVorNachteil.VorNachteilListe.Where(v => v.Vorteil == true).Except(Vorteile.Keys.Where(s => !s.HatWert ?? false)).OrderBy(sf => sf.Name).ToList();
            }
        }

        /// <summary>
        /// Die Nachteile des Helden.
        /// Nicht zum ändern von Werten, da die Werte in Held_VorNachteil stehen.
        /// </summary>
        public IDictionary<VorNachteil, ICollection<string>> Nachteile {
            get {
                Dictionary<VorNachteil, ICollection<string>> d = new Dictionary<VorNachteil, ICollection<string>>();
                foreach (var hvn in Held_VorNachteil.Where(hvn => hvn.VorNachteil != null && hvn.VorNachteil.Nachteil == true))
                {
                    if (!d.ContainsKey(hvn.VorNachteil))
                        d.Add(hvn.VorNachteil, new List<string> { });
                    d[hvn.VorNachteil].Add(hvn.Wert);
                }
                return d;
            }
        }

        /// <summary>
        /// Die Nachteile, die der Held noch wählen kann.
        /// </summary>
        public List<VorNachteil> NachteileWählbar {
            get {
                return Global.ContextVorNachteil.VorNachteilListe.Where(n => n.Nachteil == true).Except(Nachteile.Keys.Where(s => !s.HatWert ?? false)).OrderBy(sf => sf.Name).ToList();
            }
        }

        private double? _summeNachteile = null;
        public double SummeNachteile
        {
            get
            {
                if (_summeNachteile == null)
                { // Summe zum ersten Mal berechnen
                    _summeNachteile = 0.0;
                    foreach (var hvn in Held_VorNachteil.Where(vn => vn.VorNachteil.Nachteil == true))
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

        private double? _summeVorteile = null;
        public double SummeVorteile
        {
            get
            {
                if (_summeVorteile == null)
                { // Summe zum ersten Mal berechnen
                    _summeVorteile = 0.0;
                    foreach (var hvn in Held_VorNachteil.Where(vn => vn.VorNachteil.Vorteil == true))
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

        private void Hvn_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender is Held_VorNachteil && e.PropertyName == "KostenGesamt")
            {
                // VorNachteile Summe neu berechnen
                Held_VorNachteil hvn = (Held_VorNachteil)sender;
                if (hvn.VorNachteil.Vorteil ?? false)
                {
                    _summeVorteile = null;
                    OnChanged("SummeVorteile");
                }
                if (hvn.VorNachteil.Nachteil ?? false)
                {
                    _summeNachteile = null;
                    OnChanged("SummeNachteile");
                }
            }
        }

        #endregion

        #region Sonderfertigkeiten

        public Sonderfertigkeit AddSonderfertigkeit(string sfName, string wert = "") {
            return AddSonderfertigkeit(Global.ContextHeld.Liste<Sonderfertigkeit>().Where(sf => sf.Name == sfName).FirstOrDefault(), wert);
        }

        public Sonderfertigkeit AddSonderfertigkeit(Sonderfertigkeit sf, string wert = "") {
            if (sf == null)
                return null;

            IEnumerable<Held_Sonderfertigkeit> existierendeZuordnung = Held_Sonderfertigkeit.Where(heldsf => heldsf.SonderfertigkeitGUID == sf.SonderfertigkeitGUID && heldsf.HeldGUID == HeldGUID);
            if (existierendeZuordnung.Count() != 0) //es gibt bereits eine solche sonderfertigkeit auf dem helden
            {
                if (!sf.HatWert ?? false)
                    //Da es eine ohne Wert ist, darf sie nur einmal vergeben werden
                    return existierendeZuordnung.First().Sonderfertigkeit;
                else if (existierendeZuordnung.Where(hsf => hsf.Wert == wert || (string.IsNullOrWhiteSpace(wert) && string.IsNullOrWhiteSpace(hsf.Wert))).Count() != 0)
                    //Wenn sie mit diesem Wert bereits existiert, dann darf sie auch nicht nochmal hinzugefügt werden.
                    return existierendeZuordnung.Where(hsf => hsf.Wert == wert || (string.IsNullOrWhiteSpace(wert) && string.IsNullOrWhiteSpace(hsf.Wert))).First().Sonderfertigkeit;
            }

            Held_Sonderfertigkeit hs = Global.ContextHeld.New<Held_Sonderfertigkeit>();
            hs.HeldGUID = HeldGUID;
            hs.Held = this;

            hs.SonderfertigkeitGUID = sf.SonderfertigkeitGUID;
            hs.Sonderfertigkeit = sf;

            hs.Wert = wert ?? "";

            Held_Sonderfertigkeit.Add(hs);

            // Abhängige Talente automatisch einfügen.
            // TODO ??: Später ins Datenmodell einbauen. Eigenes DB-Feld mit Talentabhängigkeit.
            // Falls Ritualkenntnis oder Liturgiekenntnis -> Talente automatisch einfügen
            if (sf.Name == Sonderfertigkeit.RitualkenntnisSchamaneAchaz || sf.Name == Sonderfertigkeit.RitualkenntnisSchamaneFerkina
                || sf.Name == Sonderfertigkeit.RitualkenntnisSchamaneGjalsker || sf.Name == Sonderfertigkeit.RitualkenntnisSchamaneGoblin
                || sf.Name == Sonderfertigkeit.RitualkenntnisSchamaneNivesen || sf.Name == Sonderfertigkeit.RitualkenntnisSchamaneOrk
                || sf.Name == Sonderfertigkeit.RitualkenntnisSchamaneTrollzacker || sf.Name == Sonderfertigkeit.RitualkenntnisSchamaneWaldmenschen) { // Schamanen...
                AddTalent("Geister aufnehmen", 3);
                AddTalent("Geister bannen", 3);
                AddTalent("Geister binden", 3);
                AddTalent("Geister rufen", 3);
            } // Runenkunde...
            else if (sf.Name == Sonderfertigkeit.Runenkunde)
                AddTalent("Ritualkenntnis (Runenzauberei)", 3);
            else if (sf.Name.StartsWith("Ritualkenntnis")) { // Ritualkenntnis...
                string tradition = sf.Name.Replace("Ritualkenntnis (", string.Empty).Replace(")", string.Empty);
                AddTalent("Ritualkenntnis (" + tradition + ")", 3);
            } // Liturgiekenntnis...
            else if (sf.Name.StartsWith("Liturgiekenntnis")) {
                string kirche = sf.Name.Replace("Liturgiekenntnis (", string.Empty).Replace(")", string.Empty);
                AddTalent("Liturgiekenntnis (" + kirche + ")", 3);
            }

            return sf;
        }

        public void DeleteSonderfertigkeit(string sfName) {
            if (HatSonderfertigkeit(sfName))
                DeleteSonderfertigkeit(Held_Sonderfertigkeit.Where(h => h.Sonderfertigkeit.Name == sfName).FirstOrDefault());
        }

        public void DeleteSonderfertigkeit(Held_Sonderfertigkeit hsf) {
            if (hsf != null) {
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
        public bool HatSonderfertigkeit(Sonderfertigkeit s) {
            return Held_Sonderfertigkeit.Where(hs => hs.Sonderfertigkeit != null && hs.Sonderfertigkeit == s).Count() > 0;
        }

        /// <summary>
        /// Hat die Sonderfertigkeit mit bestimmtem Wert.
        /// </summary>
        public bool HatSonderfertigkeit(string sonderfertigkeit, string wert = null, bool exactMatch = true) {
            string m1 = sonderfertigkeit, m2 = string.Empty;
            if (!exactMatch) {
                var a = sonderfertigkeit.Split('%');
                if (a.Length > 2) {
                    m1 = a[0];
                    m2 = a[1];
                }
            }
            IEnumerable<Held_Sonderfertigkeit> hso = Held_Sonderfertigkeit.Where(hs => hs.Sonderfertigkeit != null && ((exactMatch && hs.Sonderfertigkeit.Name == sonderfertigkeit)
                || (!exactMatch && hs.Sonderfertigkeit.Name.StartsWith(m1))
                && (m2 == string.Empty || hs.Sonderfertigkeit.Name.EndsWith(m2))
                ));
            if (hso.Count() == 0)
                return false;
            //rekursiv die voraussetzungen prüfen
            foreach (Held_Sonderfertigkeit hs in hso) {
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
        public bool HatSonderfertigkeitUndVoraussetzungen(Sonderfertigkeit s) {
            if (Held_Sonderfertigkeit.Where(hs => hs.Sonderfertigkeit != null && hs.Sonderfertigkeit == s).Count() > 0)
                return s.CheckVoraussetzungen(this);
            return false;
        }

        /// <summary>
        /// Hat die Sonderfertigkeit mit bestimmtem Wert inklusive der Voraussetzungen.
        /// </summary>
        public bool HatSonderfertigkeitUndVoraussetzungen(string sonderfertigkeit, string wert, bool exactMatch = true) {
            string m1 = sonderfertigkeit, m2 = string.Empty;
            if (!exactMatch) {
                var a = sonderfertigkeit.Split('%');
                if (a.Length > 2) {
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
            if (hso.Count() == 0)
                return false;
            //rekursiv die voraussetzungen prüfen
            foreach (Held_Sonderfertigkeit hs in hso) {
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
        public bool HatSonderfertigkeitUndVoraussetzungen(string sonderfertigkeit, bool exactMatch = true) {
            return HatSonderfertigkeitUndVoraussetzungen(sonderfertigkeit, null);
        }

        /// <summary>
        /// Die Sonderfertigkeiten des Helden.
        /// Nicht zum ändern von Werten, da die Werte in Held_Sonderfertigkeit stehen.
        /// </summary>
        public IDictionary<Sonderfertigkeit, ICollection<string>> Sonderfertigkeiten {
            get {
                Dictionary<Sonderfertigkeit, ICollection<string>> d = new Dictionary<Sonderfertigkeit, ICollection<string>>();
                foreach (var hsf in Held_Sonderfertigkeit) {
                    if (!d.ContainsKey(hsf.Sonderfertigkeit))
                        d.Add(hsf.Sonderfertigkeit, new List<string> { });
                    d[hsf.Sonderfertigkeit].Add(hsf.Wert);
                }
                return d;
            }
        }

        //TODO: Ob es ratsam ist nur die mit erfüllten Voraussetzungen anzuzeigen?
        /// <summary>
        /// Die Sonderfertigkeiten, die der Held wählen kann. Die Voraussetzungen müssen erfüllt sein.
        /// </summary>
        public List<Sonderfertigkeit> SonderfertigkeitenWählbar {
            get {
                return Global.ContextHeld.SonderfertigkeitListe.Except(Sonderfertigkeiten.Keys.Where(s => !s.HatWert ?? false)).OrderBy(sf => sf.Name).ToList();
            }
        }

        #endregion

        #region Bewegung / Geschwindigkeit

        [DependentProperty("BaseGE")]
        public int GeschwindigkeitOhneMod {
            get {
                int gs = 8;
                if (HatVorNachteil("Flink")) {
                    gs++;
                    if (HatVorNachteil("Flink II"))
                        gs++;
                }
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

        [DependentProperty("GeschwindigkeitOhneMod"), DependentProperty("Behinderung")]
        [DependsOnModifikator(typeof(Mod.IModGS))]
        public double Geschwindigkeit {
            get {
                double gs = GeschwindigkeitOhneMod;
                if (Modifikatoren != null)
                    Modifikatoren.Where(m => m is Mod.IModGS).Select(m => (Mod.IModGS)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => gs = m.ApplyGSMod(gs));
                if (HatVorNachteil("Zwergenwuchs")) // BE aus Last und Rüstung geht nur halb ein WdH 274
                    return gs - Behinderung / 2;
                return Math.Max(gs - Behinderung, 1.0);
            }
        }

        #endregion

        #region Kampfwerte

        /// <summary>
        /// Gibt alle Kampftalente des Helden als Liste zurück.
        /// </summary>
        [DependsOnModifikator(typeof(Mod.IModifikator))]
        public List<Model.Held_Talent> Kampftalente {
            get {
                return Held_Talent.Where(ht => ht.Talent.IsKampfTalent).OrderByDescending(ht => ht.TaW).ThenBy(ht => ht.Talent.Talentname).ToList();
            }
        }

        #endregion

        #region IKämpfer
        private int _initiativeWurf = 0;
        public int InitiativeWurf {
            get { return _initiativeWurf; }
        }

        public int Initiative(bool dialog = false) {
            // TODO ??: Dialog MVVM-konform aufrufen
            if (dialog) {
                int wurf = View.General.ViewHelper.ShowWürfelDialog(InitiativeZufall, "Initiative Würfel-Wurf");
                if (wurf != 0)
                    _initiativeWurf = wurf;
            } else
                _initiativeWurf = RandomNumberGenerator.Wurf(InitiativeZufall);
            int be = Behinderung;
            if (HatSonderfertigkeitUndVoraussetzungen("Rüstungsgewöhnung III")) //WdS 76
                be = (int)Math.Round(be / 2.0, MidpointRounding.AwayFromZero);
            
            return InitiativeBasis - be + InitiativeWurf + InitiativeWaffen;
        }

        public int InitiativeWaffen
        {
            get {
                //angelegte Waffen und Schilde verrechnen
                var wini = 0;
                foreach (var w in Nahkampfwaffen)
                    wini += w.INI ?? 0;
                foreach (var w in Schilde)
                    wini += w.INI;
                return wini; 
            }
        }

        public int InitiativeMax() {
            _initiativeWurf = (int)InitiativeZufall;
            int be = Behinderung;
            if (HatSonderfertigkeitUndVoraussetzungen("Rüstungsgewöhnung III")) //WdS 76
                be = (int)Math.Round(be / 2.0, MidpointRounding.AwayFromZero);
            return InitiativeBasis - be + InitiativeWurf;
        }

        // WdS 55
        public int? Orientieren(bool dialog = false) {
            // Mit SF Aufmerksamkeit keine Probe nötig
            if (HatSonderfertigkeitUndVoraussetzungen("Aufmerksamkeit"))
                return InitiativeMax();

            int mod = (int)Math.Floor(Talentwert("Kriegskunst") / 2.0) * -1;
            Eigenschaft intuition = Eigenschaft("IN");
            intuition.Modifikator = mod;
            ProbenErgebnis ergebnis;
            if (dialog) // TODO ??: Dialog MVVM-konform aufrufen
                ergebnis = View.General.ViewHelper.ShowProbeDialog(intuition, this);
            else
                ergebnis = intuition.Würfeln();
            if (ergebnis.Gelungen)
                return InitiativeMax();
            return null;
        }

        //TODO ??: Wert der aktuellen Waffe verwenden.
        public int? AT {
            get { return Attacke; }
        }

        //TODO ??: Wert der aktuellen Waffe verwenden.
        public int? PA {
            get { return Parade; }
        }

        public int MR {
            get { return Magieresistenz; }
        }

        public int MRGeist {
            //TODO ??: verschiedene Sonderfertigkeiten verändern die Geistmagieresistenz.
            get { return Magieresistenz; }
        }

        private Rüstungsschutz _rs = null;
        public IRüstungsschutz RS {
            get {
                if (_rs == null)
                    _rs = new Rüstungsschutz((Model.Held)this);
                return _rs;
            }
        }

        /// <summary>
        /// Ausweichen-Wert inklusive Akrobatik und Sonderfertigkeiten.
        /// </summary>
        [DependentProperty("ParadeBasis")]
        //[DependsOnModifikator(typeof(Mod.IModAusweichen))] //gibt noch keinen Mod für das Ausweichen
        public int AusweichenOhneMod {
            get {
                int ausweichen = ParadeBasis;
                if (HatSonderfertigkeitUndVoraussetzungen("Ausweichen I"))
                    ausweichen += 3;
                if (HatSonderfertigkeitUndVoraussetzungen("Ausweichen II"))
                    ausweichen += 3;
                if (HatSonderfertigkeitUndVoraussetzungen("Ausweichen III"))
                    ausweichen += 3;
                if (HatVorNachteil("Zwergenwuchs"))
                    ausweichen += 1;
                if (HatVorNachteil("Flink")) {
                    ausweichen += 1;
                    if (HatVorNachteil("Flink II"))
                        ausweichen += 1;
                }
                if (HatVorNachteil("Behäbig"))
                    ausweichen -= 1;
                ausweichen += (Math.Max(Talentwert("Akrobatik") - 9, 0) / 3);
                return ausweichen;
            }
        }

        /// <summary>
        /// Ausweichen-Wert inklusive Akrobatik, Sonderfertigkeiten und Behinderung.
        /// </summary>
        [DependentProperty("AusweichenOhneMod"), DependentProperty("Behinderung")]
        [DependsOnModifikator(typeof(Mod.IModAusweichen))]
        public int? Ausweichen {
            get {
                int a = AusweichenOhneMod;
                if (HatVorNachteil("Flink") && Behinderung >= 5) {
                    a -= 1;
                    if (HatVorNachteil("Flink II") && Behinderung >= 7)
                        a -= 1;
                }
                if (Modifikatoren != null)
                    Modifikatoren.Where(m => m is Mod.IModAusweichen).Select(m => (Mod.IModAusweichen)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => a = m.ApplyAusweichenMod(a));
                a -= Behinderung;
                return a;
            }
        }

        public List<KampfLogic.Manöver.Manöver> Manöver {
            get { return null; }
        }

        private Wunden kämpferWunden = null;
        public IWunden WundenByZone {
            get {
                if (kämpferWunden == null)
                    kämpferWunden = new KampfLogic.Wunden((Model.Held)this);
                return kämpferWunden;
            }
        }

        IWunden IKämpfer.Wunden {
            get {
                return WundenByZone;
            }
        }

        public IList<IWaffe> Angriffswaffen {
            get {
                //TODO: Cache?
                //alle Waffen
                List<IWaffe> waffen = new List<IWaffe>();
                waffen.AddRange(Held_Ausrüstung.Where(ha => ha.Angelegt && ha.Ausrüstung.Waffe != null).Select(ha => new KampfLogic.KämpferNahkampfwaffe(ha)));
                waffen.AddRange(Held_Ausrüstung.Where(ha => ha.Angelegt && ha.Ausrüstung.Fernkampfwaffe != null).Select(ha => new KampfLogic.KämpferFernkampfwaffe(ha)));
                //TODO: Raufen, Ringen
                return waffen;
            }
        }

        public IList<KämpferNahkampfwaffe> Nahkampfwaffen
        {
            get
            {
                List<KämpferNahkampfwaffe> waffen = new List<KämpferNahkampfwaffe>();
                waffen.AddRange(Held_Ausrüstung.Where(ha => ha.Angelegt && ha.Ausrüstung.Waffe != null).Select(ha => new KampfLogic.KämpferNahkampfwaffe(ha, true)));
                //TODO: Raufen, Ringen
                return waffen;
            }
        }

        public IList<KämpferFernkampfwaffe> Fernkampfwaffen
        {
            get
            {
                List<KämpferFernkampfwaffe> waffen = new List<KämpferFernkampfwaffe>();
                waffen.AddRange(Held_Ausrüstung.Where(ha => ha.Angelegt && ha.Ausrüstung.Fernkampfwaffe != null).Select(ha => new KampfLogic.KämpferFernkampfwaffe(ha, true)));
                return waffen;
            }
        }

        public IList<KämpferSchild> Schilde
        {
            get
            {
                List<KämpferSchild> schilde = new List<KämpferSchild>();
                //TODO KämpferSchild muss an die aktuelle Hauptwaffe herankommen können.
                schilde.AddRange(Held_Ausrüstung.Where(ha => ha.Angelegt && ha.Ausrüstung.Schild != null).Select(ha => new KampfLogic.KämpferSchild(ha)));
                //TODO wenn als Waffe verwendet, dann gar nicht erst als schild erstellen - siehe auch KämpferSchild
                // wenn schild in haupthand, und kampfstil PW oder SK, dann nicht in die Liste aufnehmen.
                // infos dafür: Kampfstil und ha.Trageort
                return schilde;
            }
        }

        private System.Windows.Media.Color _farbmarkierung = System.Windows.Media.Color.FromArgb(0, 0, 0, 0);
        public System.Windows.Media.Color Farbmarkierung {
            get { return _farbmarkierung; }
            set { _farbmarkierung = value; OnChanged("Farbmarkierung"); }
        }

        private string _hinweisText = string.Empty;
        public string HinweisText {
            get { return _hinweisText; }
            set { _hinweisText = value; OnChanged("HinweisText"); }
        }

        #endregion

        #region Import Export
        public static Held Import(string pfad, bool batch = false) {
            return Import(pfad, Guid.Empty, batch);
        }
        /// <summary>
        /// Wenn newGuid nicht Emtpy ist, dann wird der held mit der neuen Guid als Kopie importiert.
        /// </summary>
        /// <returns></returns>
        public static Held Import(string pfad, Guid newGuid, bool batch = false) {
            Service.SerializationService serialization = Service.SerializationService.GetInstance(!batch);
            Guid heldGuid = serialization.ImportHeld(pfad, newGuid);
            if (heldGuid == Guid.Empty)
                return null;
            if (!batch)
                UpdateLists();
            return Global.ContextHeld.Liste<Held>().Where(h => h.HeldGUID == heldGuid).FirstOrDefault();
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

        public void Export(string pfad, bool batch = false) {
            Service.SerializationService serialization = Service.SerializationService.GetInstance(!batch);
            serialization.ExportHeld(HeldGUID, pfad);
        }

        public Held Clone(bool batch = false) {
            return Clone(Guid.NewGuid(), batch);
        }

        public Held Clone(Guid newGuid, bool batch = false) {
            Service.SerializationService serialization = Service.SerializationService.GetInstance(!batch);
            Guid heldGuid = serialization.CloneHeld(HeldGUID, newGuid);
            if (heldGuid == Guid.Empty)
                return null;
            if (!batch)
                UpdateLists();
            return Global.ContextHeld.Liste<Held>().Where(h => h.HeldGUID == heldGuid).FirstOrDefault();
        }

        public static void UpdateLists() {
            Global.ContextHeld.UpdateList<Held>();
            Global.ContextHeld.UpdateList<Held_Talent>();
            Global.ContextHeld.UpdateList<Held_Zauber>();
            Global.ContextHeld.UpdateList<Held_Ausrüstung>();
            Global.ContextHeld.UpdateList<Held_Inventar>();
            Global.ContextHeld.UpdateList<Held_Munition>();
            Global.ContextHeld.UpdateList<Held_Sonderfertigkeit>();
            Global.ContextHeld.UpdateList<Held_VorNachteil>();
        }

        /// <summary>
        /// Prüft, ob der Held aus der HeldenSoftware importiert wurde.
        /// </summary>
        public bool IsImportedFromHeldenSoftware
        {
            get { return HeldGUID.ToString().ToLowerInvariant().StartsWith("4e1d3250-f700-3000-"); }
        }

        /// <summary>
        /// Prüft, ob der Held aus einem Heldenblatt importiert wurde.
        /// </summary>
        public bool IsImportedFromHeldenblatt
        {
            // TODO: Es gibt noch keine eindeutige Heldenblatt-GUID
            get { return HeldGUID.ToString().ToLowerInvariant().StartsWith("GUID..."); }
        }

        #endregion

        #region Sonstiges

        [DependentProperty("Name")]
        public string Kurzname {
            get {
                string[] namenTeile = Name.Trim().Split(' ');
                if (namenTeile.Length > 0)
                    return namenTeile[0];
                else
                    return Name;
            }
        }

        [DependentProperty("Kampfwerte")]
        public string Bemerkung {
            get { return Kampfwerte; }
            set { Kampfwerte = value; OnChanged("Bemerkung"); }
        }

        public override string ToString() {
            return Name;
        }

        #endregion

        #region IHasWunden
        int IHasWunden.Wunden {
            get {
                return this.Wunden ?? 0;
            }
            set {
                this.Wunden = value;
            }
        }
        #endregion

        #region Inventar

        #region //Felder
        private int? ueberlastung = null;
        private double? gewicht = null;
        private double? gewichtZuTragkraftProzent = null;
        #endregion

        #region //Eigenschaften
        [DependentProperty("Körperkraft")]
        public int Tragkraft {
            get { return Körperkraft * 40; }
        }
        public int Behinderung {
            get {
                if (E.IsMitUeberlastung && E.UeberlastungBerechnung == 0) {
                    return (BE ?? 0) + (ueberlastung ?? 0);
                } else {
                    return (BE ?? 0);
                }
            }
            set { BE = value; }
        }
        public int Ueberlastung {
            get {
                if (ueberlastung == null)
                    BerechneUeberlastung();
                return ueberlastung ?? 0;
            }
            set {
                if (E.IsMitUeberlastung == false) {
                    ueberlastung = 0;
                } else {
                    if (E.IsMitUeberlastung && E.UeberlastungBerechnung == 1) {
                        Behinderung = ((BE ?? 0) - ((ueberlastung ?? 0) - value));
                        ueberlastung = value;
                    } else {
                        ueberlastung = value;
                        if (E.UeberlastungBerechnung == 0) {
                            BerechneBehinderung();
                        }
                    }
                }
                OnChanged("Ueberlastung");
            }
        }
        public double Gewicht {
            get {
                if (gewicht == null)
                    BerechneAusruestungsGewicht();
                return gewicht ?? 0.0;
            }
            set {
                gewicht = value;
                GewichtZuTragkraftProzent = ((Gewicht / Tragkraft) * 100);
                OnChanged("Gewicht");
            }
        }
        public double GewichtZuTragkraftProzent {
            get {
                if (gewichtZuTragkraftProzent == null)
                    GewichtZuTragkraftProzent = ((Gewicht / Tragkraft) * 100);
                return Math.Round(gewichtZuTragkraftProzent ?? 0.0, 2, MidpointRounding.AwayFromZero);
            }
            set {
                gewichtZuTragkraftProzent = value;
                if (E.IsMitUeberlastung && E.UeberlastungBerechnung == 0) {
                    BerechneUeberlastung();
                }
                OnChanged("GewichtZuTragkraftProzent");
            }
        }
        #endregion

        #region //Public Methoden
        /// <summary>
        /// Berechnet Behinderung (Held.Behinderung) anhand der Ausrüstung + Ueberlastung
        /// </summary>
        /// <returns></returns>
        public int BerechneBehinderung() {
            int retVal = 0;
            foreach (Held_Ausrüstung ruestung in Held_Ausrüstung.Where(ha => ha.Angelegt && ha.Ausrüstung.Rüstung != null)) {
                var rsname = ruestung.Ausrüstung.Name;
                if(ruestung.Ausrüstung.BasisAusrüstung != null)
                    rsname = ruestung.Ausrüstung.BasisAusrüstung;
                var be = (ruestung.Ausrüstung.Rüstung.BE ?? 0);
                if (HatSonderfertigkeitUndVoraussetzungen("Rüstungsgewöhnung III"))
                    be -= 2;
                else if (HatSonderfertigkeitUndVoraussetzungen("Rüstungsgewöhnung II"))
                    be -= 1;
                else if (HatSonderfertigkeitUndVoraussetzungen("Rüstungsgewöhnung I", rsname, false))
                    be -= 1;
                retVal += Math.Max(be, 0);
            }
            if (E.IsMitUeberlastung) {
                Behinderung = retVal + Ueberlastung;
            } else {
                Behinderung = retVal;
            }
            return retVal;
        }
        /// <summary>
        /// Berechnet die aktuelle Ueberlastung (Held.Ueberlastung) anhand des GewichtZuTragkraftProzent
        /// </summary>
        public double BerechneUeberlastung() {
            int retVal;
            if (GewichtZuTragkraftProzent / 50 - 2 > 0) {
                retVal = Convert.ToInt32(Math.Floor(GewichtZuTragkraftProzent / 50 - 2 + 1));
            } else {
                retVal = 0;
            }
            Ueberlastung = retVal;
            //OnChanged("Ueberlastung");
            return retVal;
        }
        /// <summary>
        /// Berechnet das aktuelle Gewicht (Held.Gewicht) anhand aller Gegenstände neu
        /// </summary>
        public double BerechneAusruestungsGewicht() {
            double g = 0.0;
            foreach (Held_Ausrüstung ha in Held_Ausrüstung) {
                double effGewicht = ha.Trageort.TragkraftFaktor * ha.Ausrüstung.Gewicht;
                if (ha.Ausrüstung.Rüstung != null && ha.Angelegt)
                    effGewicht /= 2.0;
                g += effGewicht;
            }
            foreach (var hi in Held_Inventar) {
                g += hi.Trageort.TragkraftFaktor * (hi.Inventar.Gewicht ?? 0) * (hi.Anzahl ?? 0);
            }
            foreach (var hm in Held_Munition) {
                g += hm.Trageort.TragkraftFaktor * (hm.Fernkampfwaffe.Munitionsgewicht ?? 0) * (hm.Anzahl ?? 0);
            }
            Gewicht = g;
            return g;
        }
        /// <summary>
        /// Berechnet die Rüstungswerte (Held.RS) des Helden anhand der angelegten Ausrüstung neu.
        /// </summary>
        public void BerechneRüstungswerte() {
            bool zonenRüstung = E.RSBerechnung == (int)ViewModel.Settings.ermittleRuestung.AutomatischZonen
                                || E.RSBerechnung == (int)ViewModel.Settings.ermittleRuestung.Zonen;
            IRüstungsschutz rs = new RüstungsWerte();
            int einfacherRs = 0;
            foreach (Held_Ausrüstung ha in Held_Ausrüstung.Where(h_a => h_a.Ausrüstung.Rüstung != null)) {
                if (ha.Angelegt)
                {
                //Hier könnte man noch Rüstungskombinationen beachten (wenn man zu viel Zeit hat)
                    if (zonenRüstung) {
                        rs = ha.Ausrüstung.Rüstung + rs;
                    } else {
                        einfacherRs += ha.Ausrüstung.Rüstung.RS ?? 0;
                    }
                }
            }
            if (zonenRüstung)
                RS.SetValues(rs);
            else
                RS[Trefferzone.Gesamt] = einfacherRs;
        }

        public Held_Ausrüstung AddAusrüstung(Ausrüstung a)
        {
            Held_Ausrüstung ha = Global.ContextHeld.New<Held_Ausrüstung>();
            ha.HeldGUID = HeldGUID;
            ha.Angelegt = false;
            ha.TrageortGUID = Guid.Parse("00000000-0000-0000-001a-000000000011"); //Rucksack
            ha.Trageort = Global.ContextInventar.TrageortListe.Where(item => item.TrageortGUID == ha.TrageortGUID).FirstOrDefault();

            if(a.Waffe != null || a.Schild != null)
            {
                var bfa = Global.ContextHeld.New<Held_BFAusrüstung>();
                bfa.HeldAusrüstungGUID = ha.HeldAusrüstungGUID;
                int bf = 0;
                if(a.Waffe != null)
                {
                    var hw = Global.ContextHeld.New<Held_Waffe>();
                    hw.WaffeGUID = a.Waffe.WaffeGUID;
                    hw.Waffe = a.Waffe;
                    hw.TPBonus = a.Waffe.TPBonus;
                    hw.WMAT = a.Waffe.WMAT ?? 0;
                    hw.WMPA = a.Waffe.WMPA ?? 0;
                    hw.INI = a.Waffe.INI ?? 0;
                    bf = a.Waffe.BF ?? 0;
                    hw.Talent = Held_Waffe.GetBestesTalent(this, a.Waffe);
                    hw.HeldAusrüstungGUID = bfa.HeldAusrüstungGUID;
                    bfa.Held_Waffe = hw;
                }
                if (a.Schild != null)
                {
                    bf = a.Schild.BF;
                    bfa.Schild = a.Schild;
                }
                bfa.StartBF = bfa.BF = bf;
                ha.Held_BFAusrüstung = bfa;
            }
            if(a.Fernkampfwaffe != null)
            {
                Held_Fernkampfwaffe hf = Global.ContextHeld.New<Held_Fernkampfwaffe>();
                hf.FernkampfwaffeGUID = a.Fernkampfwaffe.FernkampfwaffeGUID;
                hf.Fernkampfwaffe = a.Fernkampfwaffe;
                hf.Talent = Held_Fernkampfwaffe.GetBestesTalent(this, a.Fernkampfwaffe);
                hf.FKErleichterung = 0;
                hf.KKErleichterung = false;
                hf.HeldAusrüstungGUID = ha.HeldAusrüstungGUID;
                ha.Held_Fernkampfwaffe = hf;
            }
            if (a.Rüstung != null)
            {
                var hr = Global.ContextHeld.New<Held_Rüstung>();
                hr.RüstungGUID = a.Rüstung.RüstungGUID;
                hr.Rüstung = a.Rüstung;
                var struktur = (a.Rüstung.RS ?? 0) * 10; //ohne zonen
                if (MeisterGeister.Logic.Einstellung.Einstellungen.RSBerechnung >= 2)
                    struktur = (int)Math.Ceiling((a.Rüstung.gRS ?? 0) * 10); //Trefferzonenmodell
                hr.StartStrukturpunkte = hr.Strukturpunkte = struktur;
                hr.HeldAusrüstungGUID = ha.HeldAusrüstungGUID;
                ha.Held_Rüstung = hr;
            }
            Held_Ausrüstung.Add(ha);
            return ha;
        }

        public void RemoveAusrüstung(Held_Ausrüstung ha)
        {
            Held_Ausrüstung.Remove(ha);
            Global.ContextHeld.Delete<Held_Ausrüstung>(ha);
        }

        // TODO: Diese Add-Logik sollte mit dem Importer und dem InventarViewModel homogenisiert werden, sodass alle Stellen diese Methode verwenden
        public void AddInventar(IHandelsgut gegenstand, int anzahl = 1)
        {
            // In Held_Ausrüstung oder Held_inventar hinzufügen, bzw anzahl erhöhen.
            if (gegenstand is IAusrüstung)
            {
                IAusrüstung a = (IAusrüstung)gegenstand;
                AddAusrüstung(a.Ausrüstung);
            }
            else if (gegenstand is Handelsgut)
            {
                Handelsgut h = (Handelsgut)gegenstand;
                Inventar i = Global.ContextHeld.Liste<Inventar>().Where(li => li.HandelsgutGUID == h.HandelsgutGUID && li.Name == h.Name).FirstOrDefault();

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
                        Global.ContextHeld.Insert<Inventar>(i);
                    }
                }

                Held_Inventar hi = Held_Inventar.Where(hhi => hhi.InventarGUID == i.InventarGUID).FirstOrDefault();
                
                if (hi == null)
                {
                    hi = Global.ContextHeld.New<Held_Inventar>();
                    hi.HeldGUID = HeldGUID;
                    hi.InventarGUID = i.InventarGUID;
                    hi.Anzahl = anzahl;
                    hi.Angelegt = false;
                    hi.TrageortGUID = Guid.Parse("00000000-0000-0000-001a-000000000011"); //Rucksack
                    hi.Trageort = Global.ContextInventar.TrageortListe.Where(item => item.TrageortGUID == hi.TrageortGUID).FirstOrDefault();
                    hi.Inventar = i;
                    Held_Inventar.Add(hi);
                }
                else
                    hi.Anzahl += anzahl;
            }

            BerechneAusruestungsGewicht();

        }

        #endregion

        #endregion

        #region IDisposable
        public void Dispose() {
            PropertyChanged -= DependentProperty.PropagateINotifyProperyChanged;
        }
        #endregion
    }
}
