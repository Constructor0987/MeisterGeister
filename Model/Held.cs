using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// Eigene Usings
using MeisterGeister.Logic.General;
using MeisterGeister.ViewModel.Kampf.Logic;
using LogicAlt = MeisterGeister.ViewModel.Kampf.LogicAlt;
using KampfLogic = MeisterGeister.ViewModel.Kampf.Logic;
using Mod = MeisterGeister.ViewModel.Kampf.Logic.Modifikatoren;
using System.ComponentModel;
using DependentProperty = MeisterGeister.Model.Extensions.DependentProperty;

namespace MeisterGeister.Model
{
    // Man kann Superklassen hinzufügen. Es sollten jedoch nicht die gleichen Eigenschaften, wie in der Datenbankklasse existieren.
    public partial class Held : ViewModel.Kampf.Logic.Wesen, KampfLogic.IKämpfer
    {

        public Held()
        {
            HeldGUID = Guid.NewGuid();
            PropertyChanged += DependentProperty.PropagateINotifyProperyChanged;
            SetDefaultValues();
        }

        private void SetDefaultValues()
        {
            Name = "Alrik";
            AktiveHeldengruppe = false;
        }

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

        #region Eigenschaften
        public int Mut
        {
            get
            {
                int mu = MU ?? 8;
                if(Modifikatoren!=null)
                    Modifikatoren.FindAll(m => m is Mod.IModMU).Select(m => (Mod.IModMU)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => mu = m.ApplyMUMod(mu));
                return mu;
            }
        }

        public int Klugheit
        {
            get
            {
                int e = KL ?? 8;
                if (Modifikatoren != null)
                    Modifikatoren.FindAll(m => m is Mod.IModKL).Select(m => (Mod.IModKL)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => e = m.ApplyKLMod(e));
                return e;
            }
        }

        public int Intuition
        {
            get
            {
                int e = IN ?? 8;
                if (Modifikatoren != null)
                    Modifikatoren.FindAll(m => m is Mod.IModIN).Select(m => (Mod.IModIN)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => e = m.ApplyINMod(e));
                return e;
            }
        }

        public int Charisma
        {
            get
            {
                int e = CH ?? 8;
                if (Modifikatoren != null)
                    Modifikatoren.FindAll(m => m is Mod.IModCH).Select(m => (Mod.IModCH)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => e = m.ApplyCHMod(e));
                return e;
            }
        }

        public int Fingerfertigkeit
        {
            get
            {
                int e = FF ?? 8;
                if (Modifikatoren != null)
                    Modifikatoren.FindAll(m => m is Mod.IModFF).Select(m => (Mod.IModFF)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => e = m.ApplyFFMod(e));
                return e;
            }
        }

        public int Gewandheit
        {
            get
            {
                int e = GE ?? 8;
                if (Modifikatoren != null)
                    Modifikatoren.FindAll(m => m is Mod.IModGE).Select(m => (Mod.IModGE)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => e = m.ApplyGEMod(e));
                return e;
            }
        }

        public int Konstitution
        {
            get
            {
                int e = KO ?? 8;
                if (Modifikatoren != null)
                    Modifikatoren.FindAll(m => m is Mod.IModKO).Select(m => (Mod.IModKO)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => e = m.ApplyKOMod(e));
                return e;
            }
        }

        public int Körperkraft
        {
            get
            {
                int e = KK ?? 8;
                if (Modifikatoren != null)
                    Modifikatoren.FindAll(m => m is Mod.IModKK).Select(m => (Mod.IModKK)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => e = m.ApplyKKMod(e));
                return e;
            }
        }
        #endregion

        #region BaseEigenschaften / Für die Berechnung von abgeleiteten Werten
        [DependentProperty("MU")]
        public int BaseMU
        {
            get
            {
                int mu = MU ?? 8;
                if (Modifikatoren != null)
                    Modifikatoren.FindAll(m => m is Mod.IModMitAuswirkungAufBerechneteWerte && m is Mod.IModMU).Select(m => (Mod.IModMU)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => mu = m.ApplyMUMod(mu));
                return mu;
            }
        }
        [DependentProperty("KL")]
        public int BaseKL
        {
            get
            {
                int e = KL ?? 8;
                if (Modifikatoren != null)
                    Modifikatoren.FindAll(m => m is Mod.IModMitAuswirkungAufBerechneteWerte && m is Mod.IModKL).Select(m => (Mod.IModKL)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => e = m.ApplyKLMod(e));
                return e;
            }
        }
        [DependentProperty("IN")]
        public int BaseIN
        {
            get
            {
                int e = IN ?? 8;
                if (Modifikatoren != null)
                    Modifikatoren.FindAll(m => m is Mod.IModMitAuswirkungAufBerechneteWerte && m is Mod.IModIN).Select(m => (Mod.IModIN)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => e = m.ApplyINMod(e));
                return e;
            }
        }
        [DependentProperty("CH")]
        public int BaseCH
        {
            get
            {
                int e = CH ?? 8;
                if (Modifikatoren != null)
                    Modifikatoren.FindAll(m => m is Mod.IModMitAuswirkungAufBerechneteWerte && m is Mod.IModCH).Select(m => (Mod.IModCH)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => e = m.ApplyCHMod(e));
                return e;
            }
        }
        [DependentProperty("FF")]
        public int BaseFF
        {
            get
            {
                int e = FF ?? 8;
                if (Modifikatoren != null)
                    Modifikatoren.FindAll(m => m is Mod.IModMitAuswirkungAufBerechneteWerte && m is Mod.IModFF).Select(m => (Mod.IModFF)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => e = m.ApplyFFMod(e));
                return e;
            }
        }
        [DependentProperty("GE")]
        public int BaseGE
        {
            get
            {
                int e = GE ?? 8;
                if (Modifikatoren != null)
                    Modifikatoren.FindAll(m => m is Mod.IModMitAuswirkungAufBerechneteWerte && m is Mod.IModGE).Select(m => (Mod.IModGE)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => e = m.ApplyGEMod(e));
                return e;
            }
        }
        [DependentProperty("KO")]
        public int BaseKO
        {
            get
            {
                int e = KO ?? 8;
                if (Modifikatoren != null)
                    Modifikatoren.FindAll(m => m is Mod.IModMitAuswirkungAufBerechneteWerte && m is Mod.IModKO).Select(m => (Mod.IModKO)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => e = m.ApplyKOMod(e));
                return e;
            }
        }
        [DependentProperty("KK")]
        public int BaseKK
        {
            get
            {
                int e = KK ?? 8;
                if (Modifikatoren != null)
                    Modifikatoren.FindAll(m => m is Mod.IModMitAuswirkungAufBerechneteWerte && m is Mod.IModKK).Select(m => (Mod.IModKK)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => e = m.ApplyKKMod(e));
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
                    Modifikatoren.FindAll(m => m is Mod.IModLE).Select(m => (Mod.IModLE)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => le = m.ApplyLEMod(le));
                return le;
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
        public int AusdauerMax
        {
            get
            {
                int e = AusdauerBasis + AusdauerMod;
                if (Modifikatoren != null)
                    Modifikatoren.FindAll(m => m is Mod.IModAU).Select(m => (Mod.IModAU)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => e = m.ApplyAUMod(e));
                return e;
            }
        }

        #endregion

        #region Karmaenergie

        public bool Geweiht
        {
            get
            {
                return HatVorNachteil(VorNachteil.GeweihtZwölfgöttlicheKirche) || HatVorNachteil(VorNachteil.GeweihtNichtAlveranischeGottheit) || HatVorNachteil(VorNachteil.GeweihtHRanga) || HatVorNachteil(VorNachteil.GeweihtGravesh) || HatVorNachteil(VorNachteil.GeweihtAngrosch) || HatVorNachteil(VorNachteil.Sacerdos) || 
                    HatSonderfertigkeit(Sonderfertigkeit.SpätweiheAlveranischeGottheit) || HatSonderfertigkeit(Sonderfertigkeit.SpätweiheNamenloser) || HatSonderfertigkeit(Sonderfertigkeit.SpätweiheNichtAlveranischeGottheit) || HatSonderfertigkeit(Sonderfertigkeit.KontaktZumGroßenGeist) || HatSonderfertigkeit(Sonderfertigkeit.SpätweiheDunkleZeiten);
            }
        }

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

        public int KarmaenergieMax
        {
            get
            {
                int e = KarmaenergieMod;
                if (Modifikatoren != null)
                    Modifikatoren.FindAll(m => m is Mod.IModKE).Select(m => (Mod.IModKE)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => e = m.ApplyKEMod(e));
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

        public int AstralenergieBasis
        {
            get
            {
                int basis = BaseMU + BaseIN + BaseCH;
                if (HatSonderfertigkeit(Sonderfertigkeit.GefäßDerSterne))
                    basis += BaseCH;
                return (int)Math.Round(basis / 2.0, 0, MidpointRounding.AwayFromZero);
            }
        }

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

        public int AstralenergieMax
        {
            get
            {
                // wenn er einen Zauberervorteil hat, sonst 0
                if (Magiebegabt)
                {
                    int e = AstralenergieBasis + AstralenergieMod;
                    if (Modifikatoren != null)
                        Modifikatoren.FindAll(m => m is Mod.IModAE).Select(m => (Mod.IModAE)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => e = m.ApplyAEMod(e));
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

        public int Magieresistenz
        {
            get
            {
                //TODO ??: Aurapanzer etc.
                int e = MagieresistenzBasis + MagieresistenzMod;
                if (Modifikatoren != null)
                    Modifikatoren.FindAll(m => m is Mod.IModMR).Select(m => (Mod.IModMR)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => e = m.ApplyMRMod(e));
                return e;
            }
        }

        #endregion

        #region Wundschwellen
        public string Wundschwellen
        {
            get
            {
                return string.Format("{0} / {1} / {2}", Wundschwelle, Wundschwelle2, Wundschwelle3);
            }
        }

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

        public int Wundschwelle2
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

        public int InitiativeBasis
        {
            get
            {
                // berechneter Basiswert
                int ini = InitiativeBasisOhneSonderfertigkeiten;

                // Sonderfertigkeiten
                if (HatSonderfertigkeit(Sonderfertigkeit.Kampfreflexe) && Behinderung <= 4)
                    ini += 4;
                if (HatSonderfertigkeit(Sonderfertigkeit.Kampfgespür))
                    ini += 2;

                return ini;
            }
        }

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
                if (HatSonderfertigkeit(Sonderfertigkeit.Klingentänzer) && Behinderung <= 2)
                {
                    return WürfelEnum._2W6;
                }
                return WürfelEnum._1W6;
            }
        }

        #endregion

        #region Attacke/Parade

        public int AttackeBasis
        {
            get
            {
                return (int)Math.Round((BaseMU + BaseGE + BaseKK) / 5.0, 0, MidpointRounding.AwayFromZero);
            }
        }

        public int ParadeBasis
        {
            get
            {
                return (int)Math.Round((BaseIN + BaseGE + BaseKK) / 5.0, 0, MidpointRounding.AwayFromZero);
            }
        }

        public int Parade
        {
            get
            {
                return ParadeBasis;
            }
        }

        #endregion

        #region Fernkampf

        public int FernkampfBasis
        {
            get
            {
                return (int)Math.Round((BaseIN + BaseFF + BaseKK) / 5.0, 0, MidpointRounding.AwayFromZero);
            }
        }

        #endregion
        // TODO ??: Übernahme der anderen Methoden aus dem alten Held

        #region Talente

        public Talent AddTalent(Talent t, int wert)
        {
            return AddTalent(t, wert, null, null);
        }

        public Talent AddTalent(Talent t, int wert, int? zuteilungAT, int? zuteilungPA)
        {
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
            return t;
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
                List <Mod.IModTalentwert> l = Modifikatoren.FindAll(m => m is Mod.IModTalentwert).Select(m => (Mod.IModTalentwert)m).OrderBy(m => m.Erstellt).ToList();
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

        #endregion

        #region Vor/Nachteile

        public VorNachteil AddVorNachteil(VorNachteil vn, string wert)
        {
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
            return vn;                        
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
            
            Held_VorNachteil hvn = Held_VorNachteil.Where(hvn2 => hvn2.VorNachteil != null && hvn2.VorNachteil.Name == vn).First();
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

        public Sonderfertigkeit AddSonderfertigkeit(Sonderfertigkeit s, string wert)
        {
            Held_Sonderfertigkeit hs = Global.ContextHeld.New<Held_Sonderfertigkeit>();            
            hs.HeldGUID = HeldGUID;
            hs.Held = this;

            hs.SonderfertigkeitID = s.SonderfertigkeitID;
            hs.Sonderfertigkeit = s;
            
            hs.Wert = wert;
                        
            Held_Sonderfertigkeit.Add(hs);
            return s;
        }

        /// <summary>
        /// Hat die Sonderfertigkeit inklusive der Voraussetzungen.
        /// </summary>
        public bool HatSonderfertigkeit(Sonderfertigkeit s)
        {
            if (Held_Sonderfertigkeit.Where(hs => hs.Sonderfertigkeit != null && hs.Sonderfertigkeit == s).Count() > 0)
                return s.CheckVoraussetzungen(this);
            return false;
        }

        /// <summary>
        /// Hat die Sonderfertigkeit mit bestimmtem Wert inklusive der Voraussetzungen.
        /// </summary>
        public bool HatSonderfertigkeit(string sonderfertigkeit, string wert)
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
        public bool HatSonderfertigkeit(string sonderfertigkeit)
        {
            return HatSonderfertigkeit(sonderfertigkeit, null);
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

        public int Geschwindigkeit
        {
            get {
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
                if (Modifikatoren != null)
                    Modifikatoren.FindAll(m => m is Mod.IModGS).Select(m => (Mod.IModGS)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => gs = m.ApplyGSMod(gs));
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

        public int? PA
        {
            get { return null; }
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
                if (HatSonderfertigkeit("Ausweichen I"))
                    ausweichen += 3;
                if (HatSonderfertigkeit("Ausweichen II"))
                    ausweichen += 3;
                if (HatSonderfertigkeit("Ausweichen III"))
                    ausweichen += 3;
                ausweichen += (Math.Max(Talentwert("Akrobatik") - 9, 0) / 3);
                ausweichen -= Behinderung;
                return ausweichen;
            }
        }

        //IWunden Wunden
        //{
        //    get { return null; }
        //}

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
            private set { _aktionen = value; }
        }

        private int _angriffsaktionen = 1;
        public int Angriffsaktionen
        {
            get { return _angriffsaktionen; }
            set { _angriffsaktionen = value; }
        }

        private int _abwehraktionen = 1;
        public int Abwehraktionen
        {
            get { return _abwehraktionen; }
            set { _abwehraktionen = value; }
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
                _kampfstil = value;
                //TODO JT: Sicherstellen, dass auch zwei Waffen geführt werden
                if (_kampfstil == KampfLogic.Kampfstil.BeidhändigerKampf && HatSonderfertigkeit("Beidhändiger Kampf II"))
                    Aktionen = 3;
                else if (_kampfstil == KampfLogic.Kampfstil.Schildkampf && HatSonderfertigkeit("Schildkampf II"))
                    Aktionen = 3;
                else if (_kampfstil == KampfLogic.Kampfstil.Parierwaffenstil && HatSonderfertigkeit("Parierwaffen II") || HatSonderfertigkeit("Tod von Links"))
                    Aktionen = 3;
                else
                    Aktionen = 2;
                //TODO JT: Myranor: Mehrhändig hinzufügen sicherstellen, dass auch entsprechend viele Waffen geführt werden
            }
        }

        private WaffenloserKampfstil _waffenloserKampfstil;
        public WaffenloserKampfstil WaffenloserKampfstil
        {
            get { return _waffenloserKampfstil; }
            set { _waffenloserKampfstil = value; }
        }

        private List<Mod.IModifikator> _modifikatoren = new List<Mod.IModifikator>();
        public List<Mod.IModifikator> Modifikatoren
        {
            get { return _modifikatoren; }
        }

        public List<KampfLogic.Manöver.Manöver> Manöver
        {
            get { return null; }
        }

        IWunden IKämpfer.Wunden
        {
            get { throw new NotImplementedException(); }
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

        public void AddBasisTalente()
        {
            foreach (Talent t in Global.ContextHeld.Liste<Talent>().Where(t => t.Talenttyp == "Basis").ToList())
            {
                if(t.TalentgruppeID != 1)
                    AddTalent(t, 0);
                else
                    AddTalent(t, 0, 0, 0);
            }
        }

    }
}
