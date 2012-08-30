using System;
using System.ComponentModel;
using MeisterGeister.Daten;
using System.Xml;
using System.Collections.Generic;
// Eigene Usings
using MeisterGeister.Logic.Settings;
using MeisterGeister.ViewModel.Kampf.LogicAlt;
using Logic = MeisterGeister.Logic;

namespace MeisterGeister.Logic.General
{
    /// <summary>
    /// Beschreibt einen Helden.
    /// </summary>
    public class Held : Wesen, IKämpfer
    {
        /// <summary>
        /// Erzeugt ein Standard-Held.
        /// </summary>
        public Held()
        {
            App.DatenDataSet.Held_Talent.Held_TalentRowChanged += new DatabaseDSADataSet.Held_TalentRowChangeEventHandler(Held_TalentRowChanged);
        }

        /// <summary>
        /// Erzeugt einen Helden mit einem Namen.
        /// </summary>
        /// <param name="name">Name des Helden.</param>
        public Held(string name)
        {
            DatabaseDSADataSet.HeldRow[] rows =
                (DatabaseDSADataSet.HeldRow[])App.DatenDataSet.Held.Select("Name = '" + name.Replace("'", "''") + "'");
            if (rows.Length > 0)
            {
                HeldDataRow = rows[0];
            }
            App.DatenDataSet.Held_Talent.Held_TalentRowChanged += new DatabaseDSADataSet.Held_TalentRowChangeEventHandler(Held_TalentRowChanged);
        }

        public Held(Guid id)
        {
            HeldDataRow = App.DatenDataSet.Held.FindByHeldGUID(id);
            App.DatenDataSet.Held_Talent.Held_TalentRowChanged += new DatabaseDSADataSet.Held_TalentRowChangeEventHandler(Held_TalentRowChanged);
        }

        /// <summary>
        /// Erzeugt einen Helden mittels einer DataRow.
        /// </summary>
        /// <param name="heldRow">DataRow, mit der der Held erzeugt werden soll.</param>
        public Held(DatabaseDSADataSet.HeldRow heldRow)
        {
            HeldDataRow = heldRow;
            App.DatenDataSet.Held_Talent.Held_TalentRowChanged += new DatabaseDSADataSet.Held_TalentRowChangeEventHandler(Held_TalentRowChanged);
        }

        public Held This
        {
            get { return this; }
        }

        public new string Kurzname
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

        #region Lebensenergie

        public int LebensenergieBasis
        {
            get
            {
                return (int)Math.Round((KO * 2 + KK) / 2.0, 0, MidpointRounding.AwayFromZero);
            }
        }

        public override int LebensenergieAktuell
        {
            get
            {
                if (HeldDataRow != null && HeldDataRow.IsLE_AktuellNull())
                    LebensenergieAktuell = LebensenergieMax;
                return HeldDataRow == null ? 0 : HeldDataRow.LE_Aktuell;
            }
            set
            {
                if (HeldDataRow != null)
                    HeldDataRow.LE_Aktuell = value;
                OnPropertyChanged(string.Empty);
            }
        }

        public int LebensenergieMod
        {
            get { return HeldDataRow == null ? 0 : HeldDataRow.LE_Mod; }
            set
            {
                if (HeldDataRow != null)
                    HeldDataRow.LE_Mod = value;
                OnPropertyChanged(string.Empty);
            }
        }

        public override int LebensenergieMax
        {
            get
            {
                return LebensenergieBasis + LebensenergieMod;
            }
            set { ;}
        }

        #endregion

        #region Ausdauer

        public int AusdauerBasis
        {
            get
            {
                return (int)Math.Round((MU + KO + GE) / 2.0, 0, MidpointRounding.AwayFromZero);
            }
        }

        public override int AusdauerAktuell
        {
            get
            {
                if (HeldDataRow != null && HeldDataRow.IsAU_AktuellNull())
                    AusdauerAktuell = AusdauerMax;
                return HeldDataRow == null ? 0 : HeldDataRow.AU_Aktuell;
            }
            set
            {
                if (HeldDataRow != null)
                    HeldDataRow.AU_Aktuell = value;
                OnPropertyChanged(string.Empty);
            }
        }

        public int AusdauerMod
        {
            get
            {
                if (HeldDataRow == null || HeldDataRow.IsAU_ModNull())
                    return 0;
                return HeldDataRow.AU_Mod;
            }
            set
            {
                if (HeldDataRow != null)
                    HeldDataRow.AU_Mod = value;
                OnPropertyChanged(string.Empty);
            }
        }

        public override int AusdauerMax
        {
            get
            {
                return AusdauerBasis + AusdauerMod;
            }
            set { ;}
        }

        #endregion

        #region Karmaenergie

        public override int KarmaenergieAktuell
        {
            get
            {
                if (HeldDataRow != null && HeldDataRow.IsKE_AktuellNull())
                    KarmaenergieAktuell = KarmaenergieMax;
                return HeldDataRow == null ? 0 : HeldDataRow.KE_Aktuell;
            }
            set
            {
                if (HeldDataRow != null)
                    HeldDataRow.KE_Aktuell = value;
                OnPropertyChanged(string.Empty);
            }
        }

        public int KarmaenergieMod
        {
            get
            {
                if (HeldDataRow == null || HeldDataRow.IsKE_ModNull())
                    return 0;
                return HeldDataRow.KE_Mod;
            }
            set
            {
                if (HeldDataRow != null)
                    HeldDataRow.KE_Mod = value;
                OnPropertyChanged(string.Empty);
            }
        }

        public int KarmaenergieMax
        {
            get
            {
                return KarmaenergieMod;
            }
        }

        #endregion

        #region Astralenergie

        public int AstralenergieBasis
        {
            get
            {
                int basis = MU + IN + CH;
                if (HatGefäßDerSterne)
                    basis += CH;
                return (int)Math.Round(basis / 2.0, 0, MidpointRounding.AwayFromZero);
            }
        }

        public override int AstralenergieAktuell
        {
            get
            {
                if (HeldDataRow != null && HeldDataRow.IsAE_AktuellNull())
                    AstralenergieAktuell = AstralenergieMax;
                return HeldDataRow == null ? 0 : HeldDataRow.AE_Aktuell;
            }
            set
            {
                if (HeldDataRow != null)
                    HeldDataRow.AE_Aktuell = value;
                OnPropertyChanged(string.Empty);
            }
        }

        public int AstralenergieMod
        {
            get
            {
                if (HeldDataRow == null || HeldDataRow.IsAE_ModNull())
                    return 0;
                return HeldDataRow == null ? 0 : HeldDataRow.AE_Mod;
            }
            set
            {
                if (HeldDataRow != null)
                    HeldDataRow.AE_Mod = value;
                OnPropertyChanged(string.Empty);
            }
        }

        public int AstralenergieMax
        {
            get
            {
                return AstralenergieBasis + AstralenergieMod;
            }
        }

        #endregion

        #region Magieresistenz

        public int MagieresistenzBasis
        {
            get
            {
                return (int)Math.Round((MU + KL + KO) / 5.0, 0, MidpointRounding.AwayFromZero);
            }
        }

        public int MagieresistenzMod
        {
            get
            {
                if (HeldDataRow == null || HeldDataRow.IsMR_ModNull())
                    return 0;
                return HeldDataRow.MR_Mod;
            }
            set
            {
                if (HeldDataRow != null)
                    HeldDataRow.MR_Mod = value;
                OnPropertyChanged(string.Empty);
            }
        }

        public int Magieresistenz
        {
            get
            {
                return MagieresistenzBasis + MagieresistenzMod;
            }
        }

        #endregion

        public string Wundschwellen
        {
            get
            {
                return string.Format("{0} / {1} / {2}", Wundschwelle, Wundschwelle2, Wundschwelle3);
            }
        }

        public override int Wundschwelle
        {
            get
            {
                int ko = KO;
                ko += ModKO; // Abzüger durch Wunden
                int ws = Convert.ToInt32(Math.Round(ko / 2.0, 0, MidpointRounding.AwayFromZero));
                if (HatEisern)
                    ws += 2;
                if (HatGlasknochen)
                    ws -= 2;
                return ws;
            }
        }

        public override int Wundschwelle2
        {
            get
            {
                int ko = KO;
                ko += ModKO; // Abzüger durch Wunden
                int ws = ko;
                if (HatEisern)
                    ws += 2;
                if (HatGlasknochen)
                    ws -= 2;
                return ws;
            }
        }

        public override int Wundschwelle3
        {
            get
            {
                int ko = KO;
                ko += ModKO; // Abzüger durch Wunden
                int ws = Convert.ToInt32(Math.Round(ko * 1.5, 0, MidpointRounding.AwayFromZero));
                if (HatEisern)
                    ws += 2;
                if (HatGlasknochen)
                    ws -= 2;
                return ws;
            }
        }

        public int InitiativeBasisOhneSonderfertigkeiten
        {
            get
            {
                return (int)Math.Round((MU * 2 + IN + GE) / 5.0, 0, MidpointRounding.AwayFromZero) + InitiativeModGen;
            }
        }

        public override int InitiativeBasis
        {
            get
            {
                // berechneter Basiswert
                int ini = InitiativeBasisOhneSonderfertigkeiten;

                // Sonderfertigkeiten
                if (HatKampfreflexe && HatVoraussetzungenKampfreflexe && Behinderung <= 4)
                    ini += 4;
                if (HatKampfgespür && HatVoraussetzungenKampfgespür)
                    ini += 2;

                return ini;
            }
        }

        public int InitiativeModGen
        {
            get
            {
                if (HeldDataRow == null || HeldDataRow.IsINI_ModNull())
                    return 0;
                return HeldDataRow.INI_Mod;
            }
            set
            {
                if (HeldDataRow != null)
                    HeldDataRow.INI_Mod = value;
                OnPropertyChanged(string.Empty);
            }
        }

        public override WürfelEnum InitiativeZufall
        {
            get
            {
                if (HatKlingentänzer && HatVoraussetzungenKlingentänzer && Behinderung <= 2)
                {
                    return WürfelEnum._2W6;
                }
                return WürfelEnum._1W6;
            }
        }

        public int AttackeBasis
        {
            get
            {
                return (int)Math.Round((MU + GE + KK) / 5.0, 0, MidpointRounding.AwayFromZero);
            }
        }

        public int ParadeBasis
        {
            get
            {
                return (int)Math.Round((IN + GE + KK) / 5.0, 0, MidpointRounding.AwayFromZero);
            }
        }

        public override int Parade
        {
            get
            {
                return ParadeBasis;
            }
            set { ;}
        }

        private string _besonderheiten;
        public string Besonderheiten
        {
            get { return _besonderheiten; }
            set { _besonderheiten = value; OnPropertyChanged("Besonderheiten"); }
        }

        public string Kampfwerte
        {
            get
            {
                return HeldDataRow == null || HeldDataRow.IsKampfwerteNull() ? string.Empty : HeldDataRow.Kampfwerte;
            }
            set
            {
                HeldDataRow.Kampfwerte = value; OnPropertyChanged("Kampfwerte");
            }
        }

        public int FernkampfBasis
        {
            get
            {
                return (int)Math.Round((IN + FF + KK) / 5.0, 0, MidpointRounding.AwayFromZero);
            }
        }

        /// <summary>
        /// Stellt die DataRow des Helden dar.
        /// </summary>
        public DatabaseDSADataSet.HeldRow HeldDataRow { set; get; }

        /// <summary>
        /// Stellt den Talentspiegel des Helden dar.
        /// </summary>
        public DatabaseDSADataSet.Held_TalentRow[] TalentSpiegel
        {
            get
            {
                return (DatabaseDSADataSet.Held_TalentRow[])App.DatenDataSet.Held_Talent
                        .Select("HeldGUID = '" + Id + "'");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public List<HeldKampfTalent> KampfTalente
        {
            get
            {
                List<HeldKampfTalent> li = new List<HeldKampfTalent>();
                var rows = (DatabaseDSADataSet.Held_TalentRow[])App.DatenDataSet.Held_Talent
                        .Select("HeldGUID = '" + Id + "' AND Parent(Talent_FK).TalentgruppeID = 1", "Talentname ASC");
                foreach (var item in rows)
                {
                    li.Add(new HeldKampfTalent()
                    {
                        HeldTalentRow = item,
                        Held = this
                    });
                }
                return li;
            }
        }

        public HeldKampfTalent KampfTalent(string talentname)
        {
            var rows = (DatabaseDSADataSet.Held_TalentRow[])App.DatenDataSet.Held_Talent
                        .Select("HeldGUID = '" + Id + "' AND Parent(Talent_FK).TalentgruppeID = 1 AND Talentname = '" + talentname + "'");
            if (rows.Length > 0)
            {
                return new HeldKampfTalent()
                {
                    HeldTalentRow = rows[0],
                    Held = this
                };
            }
            return null;
        }

        void Held_TalentRowChanged(object sender, DatabaseDSADataSet.Held_TalentRowChangeEvent e)
        {
            OnPropertyChanged("KampfTalente");
            OnPropertyChanged("TalentSpiegel");
        }

        public DatabaseDSADataSet.Held_VorNachteilRow[] VorNachteileProfil
        {
            get
            {
                return (DatabaseDSADataSet.Held_VorNachteilRow[])App.DatenDataSet.Held_VorNachteil
                        .Select("HeldGUID = '" + Id + "'");
            }
        }

        public DatabaseDSADataSet.Held_SonderfertigkeitRow[] SonderfertigkeitenProfil
        {
            get
            {
                return (DatabaseDSADataSet.Held_SonderfertigkeitRow[])App.DatenDataSet.Held_Sonderfertigkeit
                        .Select("HeldGUID = '" + Id + "'", "Sonderfertigkeit");
            }
        }

        public DatabaseDSADataSet.Held_SonderfertigkeitRow[] RepräsentationenErlernt
        {
            get
            {
                return (DatabaseDSADataSet.Held_SonderfertigkeitRow[])App.DatenDataSet.Held_Sonderfertigkeit
                        .Select("HeldGUID = '" + Id + "' AND Sonderfertigkeit LIKE 'Repräsentation%'");
            }
        }

        public DatabaseDSADataSet.Held_ZauberRow[] ZauberProfil
        {
            get
            {
                return (DatabaseDSADataSet.Held_ZauberRow[])App.DatenDataSet.Held_Zauber
                           .Select("HeldGUID = '" + Id + "'");
            }
        }

        public DatabaseDSADataSet.Held_ZauberRow[] ZauberProfilRep(string rep)
        {
            return (DatabaseDSADataSet.Held_ZauberRow[])App.DatenDataSet.Held_Zauber
                    .Select(string.Format("HeldGUID = '{0}' AND Repräsentation = '{1}'", Id, rep));
        }

        public System.Collections.Generic.List<string> TalenteAktivierbar
        {
            get
            {
                System.Collections.Generic.List<string> talente = new System.Collections.Generic.List<string>();
                System.Collections.Generic.List<string> talentSpiegel = new System.Collections.Generic.List<string>();
                foreach (var row in TalentSpiegel)
                    talentSpiegel.Add(row.Talentname);

                bool ddz = Regeln.SettingDunkleZeiten;
                foreach (var t in App.DatenDataSet.Talent)
                {
                    if (t.TalentgruppeID == 8) // Meta-Talent
                        continue;
                    if (!ddz && !t.IsSettingNull() && t.Setting.Contains("Dunkle Zeiten"))
                        continue;
                    if (talentSpiegel.Contains(t.Talentname))
                        continue;
                    talente.Add(t.Talentname);
                }
                return talente;
            }
        }

        public System.Collections.Generic.SortedList<string, int> SonderfertigkeitenErlernbar
        {
            get
            {
                System.Collections.Generic.SortedList<string, int> sf = new System.Collections.Generic.SortedList<string, int>();
                System.Collections.Generic.List<string> sfVorhanden = new System.Collections.Generic.List<string>();
                foreach (var row in SonderfertigkeitenProfil)
                    sfVorhanden.Add(row.Sonderfertigkeit);

                bool ddz = Regeln.SettingDunkleZeiten;
                foreach (var s in App.DatenDataSet.Sonderfertigkeit)
                {
                    if (sfVorhanden.Contains(s.Name))
                        continue;
                    if (!ddz && !s.IsSettingNull() && s.Setting.Contains("Dunkle Zeiten"))
                        continue;
                    sf.Add(s.Name, s.SonderfertigkeitID);
                }
                return sf;
            }
        }

        public System.Collections.Generic.SortedList<string, int> VorNachteileWählbar
        {
            get
            {
                System.Collections.Generic.SortedList<string, int> vorNach = new System.Collections.Generic.SortedList<string, int>();
                System.Collections.Generic.List<string> vorNachVorhanden = new System.Collections.Generic.List<string>();
                foreach (var row in VorNachteileProfil)
                    vorNachVorhanden.Add(row.VorNachteil);

                bool ddz = Regeln.SettingDunkleZeiten;
                foreach (var vn in App.DatenDataSet.VorNachteil)
                {
                    if (vorNachVorhanden.Contains(vn.Name))
                        continue;
                    if (!ddz && !vn.IsSettingNull() && vn.Setting.Contains("Dunkle Zeiten"))
                        continue;
                    vorNach.Add(vn.Name, vn.VorNachteilID);
                }
                return vorNach;
            }
        }

        public System.Collections.Generic.SortedList<string, int> VorteileWählbar
        {
            get
            {
                System.Collections.Generic.SortedList<string, int> vorNach = new System.Collections.Generic.SortedList<string, int>();
                System.Collections.Generic.List<string> vorNachVorhanden = new System.Collections.Generic.List<string>();
                foreach (var row in VorNachteileProfil)
                    vorNachVorhanden.Add(row.VorNachteil);

                bool ddz = Regeln.SettingDunkleZeiten;
                foreach (var vn in App.DatenDataSet.VorNachteil)
                {
                    if (vorNachVorhanden.Contains(vn.Name) || !vn.Vorteil)
                        continue;
                    if (!ddz && !vn.IsSettingNull() && vn.Setting.Contains("Dunkle Zeiten"))
                        continue;
                    vorNach.Add(vn.Name, vn.VorNachteilID);
                }
                return vorNach;
            }
        }

        public System.Collections.Generic.SortedList<string, int> NachteileWählbar
        {
            get
            {
                System.Collections.Generic.SortedList<string, int> vorNach = new System.Collections.Generic.SortedList<string, int>();
                System.Collections.Generic.List<string> vorNachVorhanden = new System.Collections.Generic.List<string>();
                foreach (var row in VorNachteileProfil)
                    vorNachVorhanden.Add(row.VorNachteil);

                bool ddz = Regeln.SettingDunkleZeiten;
                foreach (var vn in App.DatenDataSet.VorNachteil)
                {
                    if (vorNachVorhanden.Contains(vn.Name) || !vn.Nachteil)
                        continue;
                    if (!ddz && !vn.IsSettingNull() && vn.Setting.Contains("Dunkle Zeiten"))
                        continue;
                    vorNach.Add(vn.Name, vn.VorNachteilID);
                }
                return vorNach;
            }
        }

        public Guid Id
        {
            get { return HeldDataRow == null ? Guid.Empty : HeldDataRow.HeldGUID; }
        }

        public override string Name
        {
            get { return HeldDataRow == null || HeldDataRow.IsNameNull() ? string.Empty : HeldDataRow.Name; }
            set { HeldDataRow.Name = value; OnPropertyChanged("Name"); }
        }

        public int MU
        {
            get { return HeldDataRow == null || HeldDataRow.IsMUNull() ? 0 : HeldDataRow.MU; }
            set
            {
                if (HeldDataRow != null)
                    HeldDataRow.MU = value;
                OnPropertyChanged(string.Empty);
            }
        }

        public int ModMU
        {
            get
            {
                int mod = 0;

                // Wunden
                mod -= WundenKopf * 2;

                return mod;
            }
        }

        public int ModErschwernisMU
        {
            get
            {
                int mod = ModMU;

                // Niedrige LE und AU
                mod -= ModErschwernisEigenschaft;

                return mod;
            }
        }

        public int KL
        {
            get { return HeldDataRow == null || HeldDataRow.IsKLNull() ? 0 : HeldDataRow.KL; }
            set
            {
                if (HeldDataRow != null)
                    HeldDataRow.KL = value;
                OnPropertyChanged(string.Empty);
            }
        }

        public int ModKL
        {
            get
            {
                int mod = 0;

                // Wunden
                mod -= WundenKopf * 2;

                return mod;
            }
        }

        public int ModErschwernisKL
        {
            get
            {
                int mod = ModKL;

                // Niedrige LE und AU
                mod -= ModErschwernisEigenschaft;

                return mod;
            }
        }

        public int IN
        {
            get { return HeldDataRow == null || HeldDataRow.IsINNull() ? 0 : HeldDataRow.IN; }
            set
            {
                if (HeldDataRow != null)
                    HeldDataRow.IN = value;
                OnPropertyChanged(string.Empty);
            }
        }

        public int ModIN
        {
            get
            {
                int mod = 0;

                // Wunden
                mod -= WundenKopf * 2;

                return mod;
            }
        }

        public int ModErschwernisIN
        {
            get
            {
                int mod = ModIN;

                // Niedrige LE und AU
                mod -= ModErschwernisEigenschaft;

                return mod;
            }
        }

        public int CH
        {
            get { return HeldDataRow == null || HeldDataRow.IsCHNull() ? 0 : HeldDataRow.CH; }
            set
            {
                if (HeldDataRow != null)
                    HeldDataRow.CH = value;
                OnPropertyChanged(string.Empty);
            }
        }

        public int ModCH
        {
            get
            {
                int mod = 0;
                return mod;
            }
        }

        public int ModErschwernisCH
        {
            get
            {
                int mod = ModCH;

                // Niedrige LE und AU
                mod -= ModErschwernisEigenschaft;

                return mod;
            }
        }

        public int FF
        {
            get { return HeldDataRow == null || HeldDataRow.IsFFNull() ? 0 : HeldDataRow.FF; }
            set
            {
                if (HeldDataRow != null)
                    HeldDataRow.FF = value;
                OnPropertyChanged(string.Empty);
            }
        }

        public int ModFF
        {
            get
            {
                int mod = 0;

                // Wunden
                mod -= WundenArmL * 2; //TODO ??: wenn Arm verwendet
                mod -= WundenArmR * 2; //TODO ??: wenn Arm verwendet

                return mod;
            }
        }

        public int ModErschwernisFF
        {
            get
            {
                int mod = ModFF;

                // Niedrige LE und AU
                mod -= ModErschwernisEigenschaft;

                return mod;
            }
        }

        public int GE
        {
            get { return HeldDataRow == null || HeldDataRow.IsGENull() ? 0 : HeldDataRow.GE; }
            set
            {
                if (HeldDataRow != null)
                    HeldDataRow.GE = value;
                OnPropertyChanged(string.Empty);
            }
        }

        public int ModGE
        {
            get
            {
                int mod = 0;

                // Wunden
                mod -= Wunden * 2;
                mod -= WundenBeinL * 2;
                mod -= WundenBeinR * 2;

                return mod;
            }
        }

        public int ModErschwernisGE
        {
            get
            {
                int mod = ModGE;

                // Niedrige LE und AU
                mod -= ModErschwernisEigenschaft;

                return mod;
            }
        }

        public override int KO
        {
            get { return HeldDataRow == null || HeldDataRow.IsKONull() ? 0 : HeldDataRow.KO; }
            set
            {
                if (HeldDataRow != null)
                    HeldDataRow.KO = value;
                OnPropertyChanged(string.Empty);
            }
        }

        public int KK
        {
            get { return HeldDataRow == null || HeldDataRow.IsKKNull() ? 0 : HeldDataRow.KK; }
            set
            {
                if (HeldDataRow != null)
                    HeldDataRow.KK = value;
                OnPropertyChanged(string.Empty);
            }
        }

        public int ModKK
        {
            get
            {
                int mod = 0;

                // Wunden
                mod -= WundenBrust;
                mod -= WundenArmL * 2; //TODO ??: wenn Arm verwendet
                mod -= WundenArmR * 2; //TODO ??: wenn Arm verwendet
                mod -= WundenBauch;

                return mod;
            }
        }

        public int ModErschwernisKK
        {
            get
            {
                int mod = ModKK;

                // Niedrige LE und AU
                mod -= ModErschwernisEigenschaft;

                return mod;
            }
        }

        public int Sozialstatus
        {
            get { return HeldDataRow == null || HeldDataRow.IsSONull() ? 0 : HeldDataRow.SO; }
            set
            {
                if (HeldDataRow != null)
                    HeldDataRow.SO = value;
                OnPropertyChanged(string.Empty);
            }
        }

        /// <summary>
        /// Unlokalisierte Wunden.
        /// </summary>
        public override int Wunden
        {
            get { return HeldDataRow == null || HeldDataRow.IsWundenNull() ? 0 : HeldDataRow.Wunden; }
            set
            {
                if (HeldDataRow != null)
                    HeldDataRow.Wunden = value < 0 ? 0 : value;
                OnPropertyChanged(string.Empty);
            }
        }

        public override int WundenKopf
        {
            get { return HeldDataRow == null || HeldDataRow.IsWundenKopfNull() ? 0 : HeldDataRow.WundenKopf; }
            set
            {
                if (HeldDataRow != null)
                    HeldDataRow.WundenKopf = value < 0 ? 0 : value;
                OnPropertyChanged(string.Empty);
            }
        }

        public override int WundenBrust
        {
            get { return HeldDataRow == null || HeldDataRow.IsWundenBrustNull() ? 0 : HeldDataRow.WundenBrust; }
            set
            {
                if (HeldDataRow != null)
                    HeldDataRow.WundenBrust = value < 0 ? 0 : value;
                OnPropertyChanged(string.Empty);
            }
        }

        public override int WundenArmL
        {
            get { return HeldDataRow == null || HeldDataRow.IsWundenArmLNull() ? 0 : HeldDataRow.WundenArmL; }
            set
            {
                if (HeldDataRow != null)
                    HeldDataRow.WundenArmL = value < 0 ? 0 : value;
                OnPropertyChanged(string.Empty);
            }
        }

        public override int WundenArmR
        {
            get { return HeldDataRow == null || HeldDataRow.IsWundenArmRNull() ? 0 : HeldDataRow.WundenArmR; }
            set
            {
                if (HeldDataRow != null)
                    HeldDataRow.WundenArmR = value < 0 ? 0 : value;
                OnPropertyChanged(string.Empty);
            }
        }

        public override int WundenBauch
        {
            get { return HeldDataRow == null || HeldDataRow.IsWundenBauchNull() ? 0 : HeldDataRow.WundenBauch; }
            set
            {
                if (HeldDataRow != null)
                    HeldDataRow.WundenBauch = value < 0 ? 0 : value;
                OnPropertyChanged(string.Empty);
            }
        }

        public override int WundenBeinL
        {
            get { return HeldDataRow == null || HeldDataRow.IsWundenBeinLNull() ? 0 : HeldDataRow.WundenBeinL; }
            set
            {
                if (HeldDataRow != null)
                    HeldDataRow.WundenBeinL = value < 0 ? 0 : value;
                OnPropertyChanged(string.Empty);
            }
        }

        public override int WundenBeinR
        {
            get { return HeldDataRow == null || HeldDataRow.IsWundenBeinRNull() ? 0 : HeldDataRow.WundenBeinR; }
            set
            {
                if (HeldDataRow != null)
                    HeldDataRow.WundenBeinR = value < 0 ? 0 : value;
                OnPropertyChanged(string.Empty);
            }
        }

        public override int Behinderung
        {
            get { return HeldDataRow == null || HeldDataRow.IsBENull() ? 0 : HeldDataRow.BE; }
            set
            {
                if (HeldDataRow != null)
                    HeldDataRow.BE = value < 0 ? 0 : value;
                OnPropertyChanged(string.Empty);
            }
        }

        public override int RüstungsschutzGesamt
        {
            get
            {
                //return HeldDataRow == null || HeldDataRow.IsRSNull() ? 0 : HeldDataRow.RS; 
                return (int)Math.Round(BerechneRüstungsschutzGesamt(), 0, MidpointRounding.AwayFromZero);
            }
            set
            {
                //HeldDataRow.RS = value < 0 ? 0 : value; OnPropertyChanged(string.Empty);
                SetRüstungsschutz(value);
                OnPropertyChanged(string.Empty);
            }
        }

        public override int RüstungsschutzKopf
        {
            get { return HeldDataRow == null || HeldDataRow.IsRSKopfNull() ? 0 : HeldDataRow.RSKopf; }
            set
            {
                if (HeldDataRow != null)
                    HeldDataRow.RSKopf = value < 0 ? 0 : value;
                OnPropertyChanged(string.Empty);
            }
        }

        public override int RüstungsschutzBrust
        {
            get { return HeldDataRow == null || HeldDataRow.IsRSBrustNull() ? 0 : HeldDataRow.RSBrust; }
            set
            {
                if (HeldDataRow != null)
                    HeldDataRow.RSBrust = value < 0 ? 0 : value;
                OnPropertyChanged(string.Empty);
            }
        }

        public override int RüstungsschutzRücken
        {
            get { return HeldDataRow == null || HeldDataRow.IsRSRückenNull() ? 0 : HeldDataRow.RSRücken; }
            set
            {
                if (HeldDataRow != null)
                    HeldDataRow.RSRücken = value < 0 ? 0 : value;
                OnPropertyChanged(string.Empty);
            }
        }

        public override int RüstungsschutzArmL
        {
            get { return HeldDataRow == null || HeldDataRow.IsRSArmLNull() ? 0 : HeldDataRow.RSArmL; }
            set
            {
                if (HeldDataRow != null)
                    HeldDataRow.RSArmL = value < 0 ? 0 : value;
                OnPropertyChanged(string.Empty);
            }
        }

        public override int RüstungsschutzArmR
        {
            get { return HeldDataRow == null || HeldDataRow.IsRSArmRNull() ? 0 : HeldDataRow.RSArmR; }
            set
            {
                if (HeldDataRow != null)
                    HeldDataRow.RSArmR = value < 0 ? 0 : value;
                OnPropertyChanged(string.Empty);
            }
        }

        public override int RüstungsschutzBauch
        {
            get { return HeldDataRow == null || HeldDataRow.IsRSBauchNull() ? 0 : HeldDataRow.RSBauch; }
            set
            {
                if (HeldDataRow != null)
                    HeldDataRow.RSBauch = value < 0 ? 0 : value;
                OnPropertyChanged(string.Empty);
            }
        }

        public override int RüstungsschutzBeinL
        {
            get { return HeldDataRow == null || HeldDataRow.IsRSBeinLNull() ? 0 : HeldDataRow.RSBeinL; }
            set
            {
                if (HeldDataRow != null)
                    HeldDataRow.RSBeinL = value < 0 ? 0 : value;
                OnPropertyChanged(string.Empty);
            }
        }

        public override int RüstungsschutzBeinR
        {
            get { return HeldDataRow == null || HeldDataRow.IsRSBeinRNull() ? 0 : HeldDataRow.RSBeinR; }
            set
            {
                if (HeldDataRow != null)
                    HeldDataRow.RSBeinR = value < 0 ? 0 : value;
                OnPropertyChanged(string.Empty);
            }
        }

        public int EigenschaftenSumme
        {
            get
            {
                return MU + KL + IN + CH + FF + GE + KO + KK;
            }
        }

        public int Eigenschaft(string kürzel)
        {
            switch (kürzel)
            {
                case "MU":
                case "Mut":
                    return MU;
                case "KL":
                case "Klugheit":
                    return KL;
                case "IN":
                case "Intuition":
                    return IN;
                case "CH":
                case "Charisma":
                    return CH;
                case "FF":
                case "Fingerfertigkeit":
                    return FF;
                case "GE":
                case "Gewandtheit":
                    return GE;
                case "KO":
                case "Konstitution":
                    return KO;
                case "KK":
                case "Körperkraft":
                    return KK;
                case "SO":
                case "Sozialstatus":
                    return Sozialstatus;
                default:
                    return 0;
            }
        }

        public override bool Magiebegabt
        {
            get
            {
                return HatVollzauberer || HatHalbzauberer || HatViertelzauberer || HatViertelzaubererUnbewusst;
            }
        }

        public override bool Geweiht
        {
            get
            {
                return HatGeweiht || HatSpätweihe || HatSacerdos;
            }
        }

        #region Sonderfertigkeiten

        public string Sonderfertigkeiten
        {
            get
            {
                string sfGesamt = string.Empty;
                foreach (DatabaseDSADataSet.Held_SonderfertigkeitRow sf in SonderfertigkeitenProfil)
                {
                    if (sfGesamt != string.Empty)
                        sfGesamt += ", ";
                    sfGesamt += sf.SonderfertigkeitRow.Name;
                    if (sf.SonderfertigkeitRow.HatWert && !sf.IsWertNull() && sf.Wert != string.Empty && sf.Wert != null)
                        sfGesamt += string.Format(" ({0})", sf.Wert);
                }
                return sfGesamt;
            }
            set { ;}
        }

        /// <summary>
        /// Gibt eine Held_SonderfertigkeitRow zu einer Sonderfertigkeit zurück, falls der Held diese besitzt.
        /// </summary>
        /// <param name="sonderferigkeit">Name der Sonderfertigkeits.</param>
        /// <returns>Die gesuchte Held_SonderfertigkeitRow, falls der Held diese besitzt.
        /// Andernfalls 'null'.</returns>
        public DatabaseDSADataSet.Held_SonderfertigkeitRow SonderfertigkeitRow(string sonderferigkeit)
        {
            var sonderfertigkeitRows = App.DatenDataSet.Sonderfertigkeit.Select(string.Format("Name = '{0}'", sonderferigkeit));
            int sonderfertigkeitlID = 0;
            if (sonderfertigkeitRows.Length > 0)
                sonderfertigkeitlID = Convert.ToInt32(sonderfertigkeitRows[0]["SonderfertigkeitID"]);
            DatabaseDSADataSet.Held_SonderfertigkeitRow[] rows =
                (DatabaseDSADataSet.Held_SonderfertigkeitRow[])App.DatenDataSet.Held_Sonderfertigkeit
                                                                .Select(string.Format("HeldGUID = '{0}' AND SonderfertigkeitID = {1}", Id, sonderfertigkeitlID));
            if (rows.Length > 0)
                return rows[0];
            return null;
        }

        public bool HatSonderfertigkeit(string sonderfertigkeit)
        {
            if (App.DatenDataSet != null)
            {
                var sf = App.DatenDataSet.Held_Sonderfertigkeit.Select(
                    string.Format("HeldGUID = '{0}' AND SonderfertigkeitID = {1}", Id, Sonderfertigkeit.GetSonderfertigkeitId(sonderfertigkeit)));
                return sf.Length > 0;
            }
            else
                return false;
        }

        public bool HatSpätweihe
        {
            get
            {
                if (App.DatenDataSet != null)
                {
                    var sf = App.DatenDataSet.Held_Sonderfertigkeit.Select(
                    string.Format("HeldGUID = '{0}' AND (SonderfertigkeitID = {1} OR SonderfertigkeitID = {2} OR SonderfertigkeitID = {3} OR SonderfertigkeitID = {4})", Id,
                        Sonderfertigkeit.GetSonderfertigkeitId(Sonderfertigkeit.SpätweiheAlveranischeGottheit),
                        Sonderfertigkeit.GetSonderfertigkeitId(Sonderfertigkeit.SpätweiheNichtAlveranischeGottheit),
                        Sonderfertigkeit.GetSonderfertigkeitId(Sonderfertigkeit.SpätweiheNamenloser),
                        Sonderfertigkeit.GetSonderfertigkeitId(Sonderfertigkeit.SpätweiheDunkleZeiten)));
                    return sf.Length > 0;
                }
                else
                    return false;
            }
        }

        public override bool HatAufmerksamkeit
        {
            get
            {
                return HatSonderfertigkeit(Sonderfertigkeit.Aufmerksamkeit);
            }
        }

        public override bool HatVoraussetzungenAufmerksamkeit
        {
            get
            {
                return IN >= 12;
            }
        }

        public bool HatAusfall
        {
            get
            {
                return HatSonderfertigkeit(Sonderfertigkeit.Ausfall);
            }
        }

        public bool HatVoraussetzungenAusfall
        {
            get
            {
                return KO >= 12
                    && HatFinte && HatVoraussetzungenFinte;
            }
        }

        public bool HatDefensiverKampfstil
        {
            get
            {
                return HatSonderfertigkeit(Sonderfertigkeit.DefensiverKampfstil);
            }
        }

        public bool HatVoraussetzungenDefensiverKampfstil
        {
            get
            {
                return GE >= 12
                    && HatMeisterparade && HatVoraussetzungenMeisterparade;
            }
        }

        public bool HatFinte
        {
            get
            {
                return HatSonderfertigkeit(Sonderfertigkeit.Finte);
            }
        }

        public bool HatVoraussetzungenFinte
        {
            get
            {
                return GE >= 12
                    && AttackeBasis >= 8;
            }
        }

        public bool HatKampfreflexe
        {
            get
            {
                return HatSonderfertigkeit(Sonderfertigkeit.Kampfreflexe);
            }
        }

        public bool HatVoraussetzungenKampfreflexe
        {
            get
            {
                return InitiativeBasisOhneSonderfertigkeiten >= 10;
            }
        }

        public bool HatKampfgespür
        {
            get
            {
                return HatSonderfertigkeit(Sonderfertigkeit.Kampfgespür);
            }
        }

        public bool HatVoraussetzungenKampfgespür
        {
            get
            {
                return IN >= 15
                    && HatAufmerksamkeit && HatVoraussetzungenAufmerksamkeit
                    && HatKampfreflexe && HatVoraussetzungenKampfreflexe;
            }
        }

        public bool HatKlingentänzer
        {
            get
            {
                return HatSonderfertigkeit(Sonderfertigkeit.Klingentänzer);
            }
        }

        public bool HatVoraussetzungenKlingentänzer
        {
            get
            {
                return GE >= 15
                    && HatKampfgespür && HatVoraussetzungenKampfgespür
                    && HatKlingensturm && HatVoraussetzungenKlingensturm
                    && HatKlingenwand && HatVoraussetzungenKlingenwand;
            }
        }

        public bool HatKlingensturm
        {
            get
            {
                return HatSonderfertigkeit(Sonderfertigkeit.Klingensturm);
            }
        }

        public bool HatVoraussetzungenKlingensturm
        {
            get
            {
                return AttackeBasis >= 9
                    && HatAusfall && HatVoraussetzungenAusfall
                    && HatKampfreflexe && HatVoraussetzungenKampfreflexe;
            }
        }

        public bool HatKlingenwand
        {
            get
            {
                return HatSonderfertigkeit(Sonderfertigkeit.Klingenwand);
            }
        }

        public bool HatVoraussetzungenKlingenwand
        {
            get
            {
                return ParadeBasis >= 9
                    && HatDefensiverKampfstil && HatVoraussetzungenDefensiverKampfstil
                    && HatKampfreflexe && HatVoraussetzungenKampfreflexe;
            }
        }

        public bool HatMeisterparade
        {
            get
            {
                return HatSonderfertigkeit(Sonderfertigkeit.Meisterparade);
            }
        }

        public bool HatVoraussetzungenMeisterparade
        {
            get
            {
                return ParadeBasis >= 8;
            }
        }

        public bool HatGefäßDerSterne
        {
            get
            {
                return HatSonderfertigkeit(Sonderfertigkeit.GefäßDerSterne);
            }
        }

        public bool HatVoraussetzungenGefäßDerSterne
        {
            get
            {
                return CH >= 15 && IN >= 13;
            }
        }

        #endregion

        #region VorNachteile

        public string VorNachteile
        {
            get
            {
                string vorNachGesamt = string.Empty;
                foreach (DatabaseDSADataSet.Held_VorNachteilRow vorNach in VorNachteileProfil)
                {
                    if (vorNachGesamt != string.Empty)
                        vorNachGesamt += ", ";
                    vorNachGesamt += vorNach.VorNachteilRow.Name;
                    if (vorNach.VorNachteilRow.HatWert && !vorNach.IsWertNull() && vorNach.Wert != string.Empty && vorNach.Wert != null)
                        vorNachGesamt += string.Format(" ({0})", vorNach.Wert);
                }
                return vorNachGesamt;
            }
            set { ;}
        }

        /// <summary>
        /// Gibt eine Held_VorNachteilRow zu einem Vor-/Nachteils zurück, falls der Held diesen besitzt.
        /// </summary>
        /// <param name="vorNachteil">Name des Vor- bzw. Nachteils.</param>
        /// <returns>Die gesuchte Held_VorNachteilRow, falls der Held diesen besitzt.
        /// Andernfalls 'null'.</returns>
        public DatabaseDSADataSet.Held_VorNachteilRow VorNachteilRow(string vorNachteil)
        {
            var vorNachRows = App.DatenDataSet.VorNachteil.Select(string.Format("Name = '{0}'", vorNachteil));
            int vorNachteilID = 0;
            if (vorNachRows.Length > 0)
                vorNachteilID = Convert.ToInt32(vorNachRows[0]["VorNachteilID"]);
            DatabaseDSADataSet.Held_VorNachteilRow[] rows =
                (DatabaseDSADataSet.Held_VorNachteilRow[])App.DatenDataSet.Held_VorNachteil
                                                                .Select(string.Format("HeldGUID = '{0}' AND VorNachteilID = {1}", Id, vorNachteilID));
            if (rows.Length > 0)
                return rows[0];
            return null;
        }

        public bool HatVorNachteile(string vorNachteil)
        {
            if (App.DatenDataSet != null)
            {
                var vorNach = App.DatenDataSet.Held_VorNachteil.Select(
                    string.Format("HeldGUID = '{0}' AND VorNachteilID = {1}", Id, VorNachteil.GetVorNachteilId(vorNachteil)));
                return vorNach.Length > 0;
            }
            else
                return false;
        }

        public bool HatGeweiht
        {
            get
            {
                if (App.DatenDataSet != null)
                {
                    var sf = App.DatenDataSet.Held_VorNachteil.Select(
                        string.Format("HeldGUID = '{0}' AND (VorNachteilID = {1} OR VorNachteilID = {2} OR VorNachteilID = {3} OR VorNachteilID = {4} OR VorNachteilID = {5})", Id,
                            VorNachteil.GetVorNachteilId(VorNachteil.GeweihtZwölfgöttlicheKirche),
                            VorNachteil.GetVorNachteilId(VorNachteil.GeweihtHRanga),
                            VorNachteil.GetVorNachteilId(VorNachteil.GeweihtAngrosch),
                            VorNachteil.GetVorNachteilId(VorNachteil.GeweihtGravesh),
                            VorNachteil.GetVorNachteilId(VorNachteil.GeweihtNichtAlveranischeGottheit)));
                    return sf.Length > 0;
                }
                else
                    return false;
            }
        }

        public bool HatSacerdos
        {
            get
            {
                return HatVorNachteile(VorNachteil.Sacerdos);
            }
        }

        public bool HatEisern
        {
            get
            {
                return HatVorNachteile(VorNachteil.Eisern);
            }
        }

        public bool HatGlasknochen
        {
            get
            {
                return HatVorNachteile(VorNachteil.Glasknochen);
            }
        }

        public bool HatVollzauberer
        {
            get
            {
                return HatVorNachteile(VorNachteil.Vollzauberer);
            }
        }

        public bool HatHalbzauberer
        {
            get
            {
                return HatVorNachteile(VorNachteil.Halbzauberer);
            }
        }

        public bool HatViertelzauberer
        {
            get
            {
                return HatVorNachteile(VorNachteil.Viertelzauberer);
            }
        }

        public bool HatViertelzaubererUnbewusst
        {
            get
            {
                return HatVorNachteile(VorNachteil.ViertelzaubererUnbewusst);
            }
        }

        #endregion

        public string InitiativeInfo
        {
            get
            {
                return string.Format("INI: {0}, Basis {1}", Initiative, InitiativeBasis);
            }
        }

        public string InitiativeInfoDetails
        {
            get
            {
                string kampfreflexe = string.Empty, kampfgespür = string.Empty;
                if (HatKampfreflexe && HatVoraussetzungenKampfreflexe && Behinderung <= 4)
                    kampfreflexe = " +4 (Kampfreflexe)";
                if (HatKampfgespür && HatVoraussetzungenKampfgespür)
                    kampfgespür = " +2 (Kampfgespür)";

                return string.Format("INI: {0}\nINI-Basis ({1}) - BE ({2}) + INI-Wurf ({3}) + INI-Modifikator ({4})\nINI-Basis: {1} ({5}{6}{7})",
                    Initiative, InitiativeBasis, Behinderung, (int)InitiativeWurf, InitiativeMod,
                    InitiativeBasisOhneSonderfertigkeiten, kampfreflexe, kampfgespür);
            }
        }

        public DreierProbenWert MetaTalentTaW(string talent)
        {
            DreierProbenWert t = new DreierProbenWert();
            if (talent == "Kräuter Suchen" || talent == "Nahrung Sammeln (Wildnis)")
            {
                t = MetaTalentBerechnen("Wildnisleben", "Sinnenschärfe", "Pflanzenkunde");
            }
            else if (talent == "Nahrung Sammeln (Agrarland)")
            {
                t = MetaTalentBerechnen("Wildnisleben", "Sinnenschärfe", "Ackerbau");
            }
            else if (talent.StartsWith("Pirschjagd"))
            {
                string waffe = talent.Substring(12).TrimEnd(')');
                t = MetaTalentBerechnen("Wildnisleben", "Tierkunde", "Fährtensuchen", "Schleichen", waffe);
            }
            else if (talent.StartsWith("Ansitzjagd"))
            {
                string waffe = talent.Substring(12).TrimEnd(')');
                t = MetaTalentBerechnen("Wildnisleben", "Tierkunde", "Fährtensuchen", "Sich Verstecken", waffe);
            }
            else
            {
                t.Aktiviert = false;
                t.Wert = 0;
            }
            return t;
        }

        public DreierProbenWert MetaTalentBerechnen(params string[] talente)
        {
            DreierProbenWert tMeta = new DreierProbenWert();
            DreierProbenWert tawTemp;
            int max = Talentwert(talente[0]).Wert;
            tMeta.Aktiviert = Talentwert(talente[0]).Aktiviert;
            foreach (string name in talente)
            {
                tawTemp = Talentwert(name);
                tMeta.Wert += tawTemp.Wert;
                max = Math.Min(tawTemp.Wert, max);
                tMeta.Aktiviert = tMeta.Aktiviert && tawTemp.Aktiviert;
            }
            tMeta.Wert = (int)Math.Round(tMeta.Wert / (double)talente.Length, 0, MidpointRounding.AwayFromZero);
            tMeta.Wert = Math.Min(max * 2, tMeta.Wert);

            return tMeta;
        }

        public DreierProbenWert Talentwert(string talent)
        {
            return Talentwert(new Talent(talent));
        }

        public void InitiativeWürfeln()
        {
            switch (InitiativeZufall)
            {
                case WürfelEnum._1W6:
                    InitiativeWurf = new W6().Würfeln();
                    break;
                case WürfelEnum._2W6:
                    InitiativeWurf = Convert.ToUInt32(new W6().Würfeln(2).Summe);
                    break;
                default:
                    break;
            }
        }

        public DreierProbenWert Talentwert(Talent talent)
        {
            DreierProbenWert t = new DreierProbenWert();
            if (talent.TalentDataRow.TalentgruppeID == 8)
            { // Meta Talent
                t = MetaTalentTaW(talent.Name);
            }
            else
            {
                DatabaseDSADataSet.Held_TalentRow[] rows =
                    (DatabaseDSADataSet.Held_TalentRow[])App.DatenDataSet.Held_Talent
                                                                    .Select("HeldGUID = '" + Id + "' AND Talentname='" +
                                                                            talent.Name.Replace("'", "''") + "'");
                if (rows.Length > 0 && !rows[0].IsTaWNull())
                {
                    t.Aktiviert = true;
                    t.Wert = rows[0].TaW;
                }
            }
            return t;
        }

        public DatabaseDSADataSet.Held_TalentRow Talent(string talent)
        {
            DatabaseDSADataSet.Held_TalentRow[] rows =
                (DatabaseDSADataSet.Held_TalentRow[])App.DatenDataSet.Held_Talent
                                                                .Select("HeldGUID = '" + Id + "' AND Talentname='" +
                                                                        talent.Replace("'", "''") + "'");
            if (rows.Length > 0)
                return rows[0];
            return null;
        }

        public List<DreierProbenWert> Zauberwert(Zauber zauber)
        {
            List<DreierProbenWert> zauberList = new List<DreierProbenWert>();
            DatabaseDSADataSet.Held_ZauberRow[] rows =
                (DatabaseDSADataSet.Held_ZauberRow[])App.DatenDataSet.Held_Zauber
                                                                .Select("HeldGUID = '" + Id + "' AND ZauberID=" + zauber.ZauberDataRow.ZauberID);
            foreach (var row in rows)
            {
                DreierProbenWert w = new DreierProbenWert();
                if (!row.IsZfWNull())
                {
                    w.Aktiviert = true;
                    w.Wert = row.ZfW;
                }
                w.TextHinweis = string.Format("[{0}]", row.Repräsentation);
                zauberList.Add(w);
            }
            return zauberList;
        }

        public bool AddTalent(string talentName, int taw = 0)
        {
            var talentSpiegel = (DatabaseDSADataSet.Held_TalentRow[])App.DatenDataSet.Held_Talent
                .Select("HeldGUID = '" + Id + "' AND Talentname='" + talentName.Replace("'", "''") + "'");
            DatabaseDSADataSet.TalentRow talent = App.DatenDataSet.Talent.FindByTalentname(talentName);
            if (talentSpiegel.Length == 0 && HeldDataRow != null)
            {
                App.DatenDataSet.Held_Talent.AddHeld_TalentRow(talent, taw, HeldDataRow, null, 0, 0, talent.TalentgruppeID);

                // Falls Gabe -> auch passenden Vorteil hinzufügen
                if (talent.TalentgruppeRow.Kurzname == "Gabe")
                {
                    AddVorNachteil(talent.Talentname);
                } // Falls Ritualkenntnis oder Liturgiekenntnis -> auch passende Sonderfertigkeit hinzufügen
                else if (talent.TalentgruppeRow.Kurzname == "Ritualkenntnis" || talent.TalentgruppeRow.Kurzname == "Liturgiekenntnis")
                {
                    AddSonderfertigkeit(talent.Talentname);
                }
                if (talent.TalentgruppeRow.Kurzname == "Kampf")
                {
                    OnPropertyChanged("KampfTalente");
                }

                return true;
            }
            return false;
        }

        /// <summary>
        /// Löscht ein Talent aus dem Talentspiegel des Helden.
        /// </summary>
        /// <param name="talent"></param>
        public void DeleteTalent(DatabaseDSADataSet.Held_TalentRow talent)
        {
            // Falls Gabe -> auch passenden Vorteil löschen
            string gruppe = talent.TalentRow.TalentgruppeRow.Kurzname;
            if (gruppe == "Gabe")
            {
                var vorNach = VorNachteilRow(talent.Talentname);
                if (vorNach != null)
                    vorNach.Delete();
            } // Falls Ritualkenntnis oder Liturgiekenntnis -> auch passende Sonderfertigkeit löschen
            else if (gruppe == "Ritualkenntnis" || gruppe == "Liturgiekenntnis")
            {
                var sonderfertigkeit = SonderfertigkeitRow(talent.Talentname);
                if (sonderfertigkeit != null)
                    sonderfertigkeit.Delete();
            }
            else if (gruppe == "Kampf")
                OnPropertyChanged("KampfTalente");

            talent.Delete();
        }

        public bool AddSonderfertigkeit(string sonderfertigkeit)
        {
            var sonderfertigkeitRows = App.DatenDataSet.Sonderfertigkeit.Select(string.Format("Name = '{0}'", sonderfertigkeit.Replace("'", "''")));
            if (sonderfertigkeitRows.Length == 1)
                return AddSonderfertigkeit(Convert.ToInt32(sonderfertigkeitRows[0]["SonderfertigkeitID"]));
            return false;
        }

        public bool AddSonderfertigkeit(int sfId)
        {
            DatabaseDSADataSet.SonderfertigkeitRow sf = App.DatenDataSet.Sonderfertigkeit.FindBySonderfertigkeitID(sfId);
            if (App.DatenDataSet.Held_Sonderfertigkeit.FindByHeldGUIDSonderfertigkeitID(Id, sfId) == null && HeldDataRow != null)
            {
                App.DatenDataSet.Held_Sonderfertigkeit.AddHeld_SonderfertigkeitRow(HeldDataRow, sf, null, sf.Name, sf.Typ);

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
                    string tradition = sf.Name.Replace("Ritualkenntnis (", string.Empty).Replace(')', '\0');
                    AddTalent("Ritualkenntnis (" + tradition + ")", 3);
                } // Liturgiekenntnis...
                else if (sf.Name.StartsWith("Liturgiekenntnis"))
                {
                    string kirche = sf.Name.Replace("Liturgiekenntnis (", string.Empty).Replace(')', '\0');
                    AddTalent("Liturgiekenntnis (" + kirche + ")", 3);
                }

                return true;
            }
            return false;
        }

        /// <summary>
        /// Löscht eine Sonderfertigkeit aus dem Sonderfertigkeitspiegel des Helden.
        /// </summary>
        /// <param name="sf"></param>
        public void DeleteSonderfertigkeit(DatabaseDSADataSet.Held_SonderfertigkeitRow sf)
        {
            // Falls Ritualkenntnis oder Liturgiekenntnis -> Talent mit löschen
            string sfName = sf.SonderfertigkeitRow.Name;
            if (sfName.StartsWith("Ritualkenntnis") || sfName.StartsWith("Liturgiekenntnis"))
            {
                var talent = Talent(sfName);
                if (talent != null)
                    talent.Delete();
            }

            sf.Delete();
        }

        public bool AddVorNachteil(string vorNach)
        {
            var vorNachRows = App.DatenDataSet.VorNachteil.Select(string.Format("Name = '{0}'", vorNach));
            if (vorNachRows.Length == 1)
                return AddVorNachteil(Convert.ToInt32(vorNachRows[0]["VorNachteilID"]));
            return false;
        }

        public bool AddVorNachteil(int vorNachId)
        {
            DatabaseDSADataSet.VorNachteilRow vorNach = App.DatenDataSet.VorNachteil.FindByVorNachteilID(vorNachId);
            if (App.DatenDataSet.Held_VorNachteil.FindByHeldGUIDVorNachteilID(Id, vorNachId) == null && HeldDataRow != null)
            {
                App.DatenDataSet.Held_VorNachteil.AddHeld_VorNachteilRow(HeldDataRow, vorNach, null, vorNach.Name, vorNach.Typ);

                // Falls Gabe -> Talent automatisch einfügen
                if (vorNach.Name == VorNachteil.Empathie)
                    AddTalent("Empathie", 3);
                else if (vorNach.Name == VorNachteil.Gefahreninstinkt)
                    AddTalent("Gefahreninstinkt", 3);
                else if (vorNach.Name == VorNachteil.Geräuschhexerei)
                    AddTalent("Geräuschhexerei", 3);
                else if (vorNach.Name == VorNachteil.Magiegespür)
                    AddTalent("Magiegespür", 3);
                else if (vorNach.Name == VorNachteil.Prophezeien)
                    AddTalent("Prophezeien", 3);
                else if (vorNach.Name == VorNachteil.Zwergennase)
                    AddTalent("Zwergennase", 3);

                return true;
            }
            return false;
        }

        /// <summary>
        /// Löscht ein Vor-/Nachteil aus dem VorNachteilspiegel des Helden.
        /// </summary>
        /// <param name="vorNach"></param>
        public void DeleteVorNachteil(DatabaseDSADataSet.Held_VorNachteilRow vorNach)
        {
            // Falls Gabe -> Talent mit löschen
            DatabaseDSADataSet.Held_TalentRow t = null;
            if (vorNach.VorNachteilRow.Name == VorNachteil.Empathie)
                t = Talent("Empathie");
            else if (vorNach.VorNachteilRow.Name == VorNachteil.Gefahreninstinkt)
                t = Talent("Gefahreninstinkt");
            else if (vorNach.VorNachteilRow.Name == VorNachteil.Geräuschhexerei)
                t = Talent("Geräuschhexerei");
            else if (vorNach.VorNachteilRow.Name == VorNachteil.Magiegespür)
                t = Talent("Magiegespür");
            else if (vorNach.VorNachteilRow.Name == VorNachteil.Prophezeien)
                t = Talent("Prophezeien");
            else if (vorNach.VorNachteilRow.Name == VorNachteil.Zwergennase)
                t = Talent("Zwergennase");
            if (t != null)
                t.Delete();

            vorNach.Delete();
        }

        public bool AddZauber(int zauberId, string rep)
        {
            DatabaseDSADataSet.ZauberRow zauber = App.DatenDataSet.Zauber.FindByZauberID(zauberId);
            if (App.DatenDataSet.Held_Zauber.FindByHeldGUIDZauberIDRepräsentation(Id, zauberId, rep) == null && HeldDataRow != null)
            {
                App.DatenDataSet.Held_Zauber.AddHeld_ZauberRow(HeldDataRow, zauber, 0, rep, null, zauber.Name);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Löscht einen Zauber aus dem Zauberspiegel des Helden.
        /// </summary>
        /// <param name="zauber"></param>
        public void DeleteZauber(DatabaseDSADataSet.Held_ZauberRow zauber)
        {
            zauber.Delete();
        }

        public void AddBasistalente()
        {
            var basisTalente = (DatabaseDSADataSet.TalentRow[])App.DatenDataSet.Talent
                    .Select("Talenttyp='Basis'");
            foreach (DatabaseDSADataSet.TalentRow talent in basisTalente)
            {
                var talentSpiegel = (DatabaseDSADataSet.Held_TalentRow[])App.DatenDataSet.Held_Talent
                    .Select("HeldGUID = '" + Id + "' AND Talentname='" + talent.Talentname.Replace("'", "''") + "'");
                if (talentSpiegel.Length == 0)
                    App.DatenDataSet.Held_Talent.AddHeld_TalentRow(talent, 0, HeldDataRow, null, 0, 0, talent.TalentgruppeID);
            }
        }

        public void Delete()
        {
            if (HeldDataRow != null)
            {
                foreach (var heldTalentRow in TalentSpiegel)
                {
                    heldTalentRow.Delete();
                    if (heldTalentRow.RowState != System.Data.DataRowState.Detached)
                        heldTalentRow.AcceptChanges();
                }
                foreach (var heldVorNachteilRow in VorNachteileProfil)
                {
                    heldVorNachteilRow.Delete();
                    if (heldVorNachteilRow.RowState != System.Data.DataRowState.Detached)
                        heldVorNachteilRow.AcceptChanges();
                }
                foreach (var heldSonderfertigkeitRow in SonderfertigkeitenProfil)
                {
                    heldSonderfertigkeitRow.Delete();
                    if (heldSonderfertigkeitRow.RowState != System.Data.DataRowState.Detached)
                        heldSonderfertigkeitRow.AcceptChanges();
                }
                foreach (var heldZauberRow in ZauberProfil)
                {
                    heldZauberRow.Delete();
                    if (heldZauberRow.RowState != System.Data.DataRowState.Detached)
                        heldZauberRow.AcceptChanges();
                }
                HeldDataRow.Delete();
                App.SaveHelden();
            }
        }

        /// <summary>
        /// Erzeugt eine XML-Datei mit Demo-Helden.
        /// </summary>
        public static void GeneriereDemoHelden()
        {
            App.DatenDataSet.Held.WriteXml("Daten\\demoHelden.xml", true);
        }

        /// <summary>
        /// Lädt Demo-Helden aus der demoHelden.xml Datei.
        /// </summary>
        /// <exception cref="System.IO.FileNotFoundException">Wird geworfen, wenn die Demo-Helden Datei nicht vorhanden ist.</exception>
        /// <exception cref="System.Xml.XmlException">Wird geworfen, wenn die Demo-Helden Datei einen Fehler aufweist.</exception>
        public static void LadeDemoHelden()
        {
            DatabaseDSADataSet ds = new DatabaseDSADataSet();
            ds.BeginInit();
            ds.EnforceConstraints = false;
            try
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.LoadXml(Properties.Resources.demoHelden);
                XmlParserContext context = new XmlParserContext(null, null, null, XmlSpace.Default);
                XmlTextReader xtReader = new XmlTextReader(xDoc.OuterXml, XmlNodeType.Document, context);
                ds.ReadXml((XmlReader)xtReader);
            }
            catch (System.Xml.XmlException ex)
            {
                throw ex;
            }

            ds.EndInit();

            App.DatenDataSet.Merge(ds, true);

            App.SaveHelden();
            App.DatenDataSetTableAdapters.HeldTableAdapter.Fill(App.DatenDataSet.Held);
            App.DatenDataSetTableAdapters.Held_TalentTableAdapter.Fill(App.DatenDataSet.Held_Talent);
            App.DatenDataSetTableAdapters.Held_SonderfertigkeitTableAdapter.Fill(App.DatenDataSet.Held_Sonderfertigkeit);
            App.DatenDataSetTableAdapters.Held_VorNachteilTableAdapter.Fill(App.DatenDataSet.Held_VorNachteil);
            App.DatenDataSetTableAdapters.Held_ZauberTableAdapter.Fill(App.DatenDataSet.Held_Zauber);
        }

        public static string ExportHelden(string pfad = "Export\\Helden.xml")
        {
            App.DatenDataSet.Held.WriteXml(pfad, true);
            return pfad;
        }

        public string ExportHeld(string pfad = "Held.xml", bool heldenSoftwareFormat = false)
        {
            if (heldenSoftwareFormat == false)
            {
                DatabaseDSADataSet dsExport = new DatabaseDSADataSet();
                dsExport.BeginInit();
                dsExport.EnforceConstraints = false;
                dsExport.Held.ImportRow(HeldDataRow);
                foreach (var heldTalentRow in TalentSpiegel)
                    dsExport.Held_Talent.ImportRow(heldTalentRow);
                foreach (var heldVorNachteilRow in VorNachteileProfil)
                    dsExport.Held_VorNachteil.ImportRow(heldVorNachteilRow);
                foreach (var heldSonderfertigkeitRow in SonderfertigkeitenProfil)
                    dsExport.Held_Sonderfertigkeit.ImportRow(heldSonderfertigkeitRow);
                foreach (var heldZauberRow in ZauberProfil)
                    dsExport.Held_Zauber.ImportRow(heldZauberRow);
                dsExport.EndInit();

                dsExport.Held.WriteXml(pfad, true);
            }
            else // Im Helden-Software Format exportieren
            {

            }
            return pfad;
        }

        public string ImportHeld(string pfad)
        {
            DatabaseDSADataSet dsExport = new DatabaseDSADataSet();
            dsExport.BeginInit();
            dsExport.EnforceConstraints = false;

            System.Data.DataSet ds = new System.Data.DataSet();
            ds.ReadXml(pfad);
            if (ds.Tables.Contains("helden") && ds.Tables.Contains("held"))
            { // Helden-Software Format
                //var heldConverter = new Logic.HeldenImport.HeldenSoftwareImporter(dsExport, this, pfad);
                //heldConverter.ImportHeldenSoftwareFile();
                dsExport.EndInit();
            }
            else
            {
                dsExport.ReadXml(pfad);
                dsExport.EndInit();
                dsExport.Held[0].HeldGUID = Id;
                foreach (var heldTalentRow in dsExport.Held_Talent)
                    heldTalentRow.HeldGUID = Id;
                foreach (var heldVorNachteilRow in dsExport.Held_VorNachteil)
                    heldVorNachteilRow.HeldGUID = Id;
                foreach (var heldSonderfertigkeitRow in dsExport.Held_Sonderfertigkeit)
                    heldSonderfertigkeitRow.HeldGUID = Id;
                foreach (var heldZauberRow in dsExport.Held_Zauber)
                    heldZauberRow.HeldGUID = Id;
            }
            Delete();

            App.DatenDataSet.Merge(dsExport, true);

            App.SaveHelden();

            return pfad;
        }

        public static string ImportHeldNeu(string pfad)
        {
            Guid id = Neu(false).Id;
            Held h = new Held(id);
            return h.ImportHeld(pfad);
        }

        public static Held Neu(bool genBasisTalente = true, string name = "Alrik")
        {
            DatabaseDSADataSet.HeldRow h = App.DatenDataSet.Held.NewHeldRow();
            int i = 1;
            while (App.DatenDataSet.Held.Select("Name = '" + name.Replace("'", "''") + " " + i + "'").Length > 0)
                i++;
            h.Name = name + " " + i;
            h.HeldGUID = Guid.NewGuid(); //Guid umstellung
            App.DatenDataSet.Held.AddHeldRow(h);
            App.SaveHelden();
            Held held = new Held(h);
            if (genBasisTalente)
                held.AddBasistalente();
            return held;
        }

        public void Kopie()
        {
            Held heldKopie = Neu(false, Name);
            Guid id = heldKopie.Id;
            string name = heldKopie.Name;

            string fileTemp = ExportHeld(Guid.NewGuid() + ".xml");
            Held h = new Held(id);
            h.ImportHeld(fileTemp);
            h = new Held(id);
            h.Name = name;
            System.IO.File.Delete(fileTemp);
        }

        public static System.Collections.Generic.List<Held> AktiveHelden()
        {
            System.Collections.Generic.List<Held> heldList = new System.Collections.Generic.List<Held>();
            foreach (DatabaseDSADataSet.HeldRow heldRow in App.DatenDataSet.Held)
            {
                if (!heldRow.IsAktiveHeldengruppeNull() && heldRow.AktiveHeldengruppe)
                    heldList.Add(new Held(heldRow));
            }
            return heldList;
        }

        public string RepräsentationStandard()
        {
            List<string> rep = new List<string>();

            var reps = RepräsentationenErlernt;
            foreach (var row in reps)
            {
                rep.Add(row.SonderfertigkeitRow.Name.Replace("Repräsentation (", string.Empty).TrimEnd(')'));
            }

            if (rep.Count == 1)
            {
                return Repräsentationen.GetKürzel(rep[0]);
            }
            else if (rep.Count > 1)
            {
                Dictionary<string, int> repZauber = new Dictionary<string, int>();
                foreach (string r in rep)
                {
                    var zauber = ZauberProfilRep(Repräsentationen.GetKürzel(r));
                    if (repZauber.ContainsKey(r))
                        repZauber[r] += zauber.Length;
                    else
                        repZauber.Add(r, zauber.Length);
                }
                int maxCount = 0;
                string maxRep = "Mag";
                foreach (var item in repZauber)
                {
                    if (item.Value >= maxCount)
                    {
                        maxCount = item.Value;
                        maxRep = Repräsentationen.GetKürzel(item.Key);
                    }
                }
                return maxRep;
            }
            else
                return "Mag";
        }
        
    }

    public delegate ProbenErgebnis ProbeWürfeln(Wesen wesen, IProbe probe, string aktion);

    public class DreierProbenWert
    {
        public DreierProbenWert()
        {
            Aktiviert = false;
        }

        public bool Aktiviert { get; set; }
        public int Wert { get; set; }
        public string TextHinweis { get; set; }
    }

}
