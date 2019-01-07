using System;
using System.Collections.Generic;
using System.Linq;
using MeisterGeister.Logic.General;
using MeisterGeister.Model;
using MeisterGeister.Model.Extensions;
using MeisterGeister.View.General;

namespace MeisterGeister.ViewModel.Kampf.Logic.Manöver
{
    public class FernkampfManöver : KampfManöver<IFernkampfwaffe>
    {       
        public FernkampfManöver(KämpferInfo ausführender, IFernkampfwaffe Fernkampfwaffe) : base(ausführender)
        {
            this.FernkampfWaffeSelected = Fernkampfwaffe;
            
            if (_deckung != null)
                _deckung.Value = ((FernkampfModifikator<int>)Ausführender.PreFernkampfMods[DECKUNG_MOD]).Value;
            if (_axxeleratusAktiv != null && Ausführender.PreFernkampfMods != null)
                _axxeleratusAktiv.Value = ((FernkampfModifikator<bool>)Ausführender.PreFernkampfMods[AXXELERATUSAKTIV_MOD]).Value;
        }

        public bool IstAxxeleratusAktiv
        {
            get { return (bool)((FernkampfModifikator<bool>)mods[AXXELERATUSAKTIV_MOD]).Value; }
        }

        [DependentProperty("IstAxxeleratusAktiv")]
        public bool HatSchnellladenBogen
        {
            get { return (Ausführender.Kämpfer is Held && 
                    ((Ausführender.Kämpfer as Held).HatSonderfertigkeit(Sonderfertigkeit.SchnellladenBogen, null, false) || IstAxxeleratusAktiv)); }
        }

        [DependentProperty("IstAxxeleratusAktiv")]
        public bool HatSchnellladenArmbrust
        {
            get { return (Ausführender.Kämpfer is Held && 
                    ((Ausführender.Kämpfer as Held).HatSonderfertigkeit(Sonderfertigkeit.SchnellladenArmbrust, null, false) || IstAxxeleratusAktiv)); }
        }

        [DependentProperty("IstAxxeleratusAktiv")]
        public bool HatSchnellziehen
        {
            get { return (Ausführender.Kämpfer is Held && 
                    ((Ausführender.Kämpfer as Held).HatSonderfertigkeit(Sonderfertigkeit.Schnellziehen, null, false) || IstAxxeleratusAktiv)); }
        }

        public int SchussDauer
        {
            get { return FernkampfWaffeSelected == null? 1: (FernkampfWaffeSelected.Name == "Kurzbogen"? 0: 1); }
        }

        private int _zielenDauer = 0;
        public int ZielenDauer
        {
            get { return _zielenDauer; }
            set { Set(ref _zielenDauer, value); }
        }

        private int _ansageDauer = 0;
        public int AnsageDauer
        {
            get { return _ansageDauer; }
            set { Set(ref _ansageDauer, value); }
        }

        #region Mods

        public const string WIND_MOD = "Wind";
        public const string AXXELERATUSAKTIV_MOD = "AxxeleratusAktiv";
        public const string STEILNACHUNTEN_MOD = "SteilNachUnten";
        public const string STEILNACHOBEN_MOD = "SteilNachOben";
        public const string BÖIGERWIND_MOD = "BöigerWind";
        public const string STARKERBÖIGERWIND_MOD = "StarkerBöigerWind";
        public const string POS_SELBST_MOD = "PositionSelbst";
        public const string SCHUSS2_MOD = "Schuss2";
        public const string EIGENE_MOD = "Eigene";

        public const string DECKUNG_MOD = "Deckung";
        public const string UNSICHTBAR_MOD = "Unsichtbar";
        public const string DISTANZ_MOD = "Entfernung";
        public const string TREFFERZONE_MOD = "Trefferzone";
        public const string BEWEGUNG_MOD = "Bewegung";
        public const string UNTERWASSER_MOD = "UnterWasser";
        public const string BEWKÖRPERTEIL_MOD = "BewKörperteil";
        public const string NAHKAMPF_MOD = "Nahkampf";
        public const string HANDGEMENGE_MOD = "Handgemenge";
        public const string ZIELEN_MOD = "Zielen";
        public const string ANSAGE_MOD = "Ansage";

        public const string PFERDBEWEGUNG_MOD = "PferdBewegung";
        public const string OHNESATTEL_MOD = "OhneSattel";

        protected FernkampfModifikator<Lichtstufe> _licht;
        protected FernkampfModifikator<Sichtstufe> _sicht;
        protected FernkampfModifikator<Position> _positionSelbst;

        protected FernkampfModifikator<bool> _axxeleratusAktiv;
        protected FernkampfModifikator<bool> _steilNachUnten;
        protected FernkampfModifikator<bool> _steilNachOben;
        protected FernkampfModifikator<int> _wind;
        protected FernkampfModifikator<bool> _schuss2;

        protected FernkampfModifikator<Größe> _größe;
        protected FernkampfModifikator<bool> _unsichtbar;
        protected FernkampfModifikator<int> _deckung;
        protected FernkampfModifikator<int> _distanz;
        protected FernkampfModifikator<Trefferzone> _trefferzone;
        protected FernkampfModifikator<Bewegung> _bewegung;
        protected FernkampfModifikator<bool> _bewKörperteil;
        protected FernkampfModifikator<bool> _unterWasser;
        protected FernkampfModifikator<int> _nahkampf;
        protected FernkampfModifikator<int> _handgemenge;
        protected FernkampfModifikator<int> _zielen;
        protected FernkampfModifikator<int> _eigene;
        protected FernkampfModifikator<int> _ansage;

        protected FernkampfModifikator<int> _pferdBewegung;
        protected FernkampfModifikator<bool> _ohneSattel;

        protected override void InitMods(IWaffe waffe)
        {
            base.InitMods(waffe);
            bool loadStd = Ausführender.PreFernkampfMods == null || Ausführender.PreFernkampfWaffe != waffe as IFernkampfwaffe;
            bool loadPre = Ausführender.PreFernkampfMods != null && Ausführender.PreFernkampfWaffe != null && Ausführender.PreFernkampfWaffe.Name == waffe.Name;

            _licht = new FernkampfModifikator<Lichtstufe>(this);
            if (loadStd)
                _licht.Value = Global.CurrentKampf.Kampf.Licht;
            if (loadPre)
                _licht.Value = ((FernkampfModifikator<Lichtstufe>)Ausführender.PreFernkampfMods[LICHT_MOD]).Value;
            _licht.GetMod = LichtMod;
            mods.Add(LICHT_MOD, _licht);

            _wind = new FernkampfModifikator<int>(this);
            _wind.GetMod = WindMod;
            mods.Add(WIND_MOD, _wind);

            _axxeleratusAktiv = new FernkampfModifikator<bool>(this);
            _axxeleratusAktiv.GetMod = AxxeleratusAktivMod;
            mods.Add(AXXELERATUSAKTIV_MOD, _axxeleratusAktiv);

            _steilNachUnten = new FernkampfModifikator<bool>(this);
            _steilNachUnten.GetMod = SteilNachUntenMod;
            mods.Add(STEILNACHUNTEN_MOD, _steilNachUnten);

            _steilNachOben = new FernkampfModifikator<bool>(this);
            _steilNachOben.GetMod = SteilNachObenMod;
            mods.Add(STEILNACHOBEN_MOD, _steilNachOben);

            _unsichtbar = new FernkampfModifikator<bool>(this);
            _unsichtbar.GetMod = UnsichtbarMod;
            mods.Add(UNSICHTBAR_MOD, _unsichtbar);

            _größe = new FernkampfModifikator<Größe>(this);
            if (loadStd)
                _größe.Value = Größe.Mittel;
            if (loadPre)
                _größe.Value = ((FernkampfModifikator<Größe>)Ausführender.PreFernkampfMods[GRÖSSE_MOD]).Value;
            _größe.GetMod = GrößeMod;
            mods.Add(GRÖSSE_MOD, _größe);
            
            FernkampfModifikator<int> deckung = new FernkampfModifikator<int>(this);
            if (loadPre)
                deckung.Value = ((FernkampfModifikator<int>)Ausführender.PreFernkampfMods[DECKUNG_MOD]).Value;
            mods.Add(DECKUNG_MOD, deckung);
            _distanz = new FernkampfModifikator<int>(this);
            if (loadStd)
                _distanz.Value = 1;
            if (loadPre)
                _distanz.Value = ((FernkampfModifikator<int>)Ausführender.PreFernkampfMods[DISTANZ_MOD]).Value;
            _distanz.GetMod = DistanzMod;
            mods.Add(DISTANZ_MOD, _distanz);

            _trefferzone = new FernkampfModifikator<Trefferzone>(this);
            if (loadStd)
                _trefferzone.Value = Trefferzone.Unlokalisiert;
            if (loadPre)
                _trefferzone.Value = ((FernkampfModifikator<Trefferzone>)Ausführender.PreFernkampfMods[TREFFERZONE_MOD]).Value;
            _trefferzone.GetMod = TrefferzoneMod;
            mods.Add(TREFFERZONE_MOD, _trefferzone);

            _bewKörperteil = new FernkampfModifikator<bool>(this);
            if (loadPre)
                _bewKörperteil.Value = ((FernkampfModifikator<bool>)Ausführender.PreFernkampfMods[BEWKÖRPERTEIL_MOD]).Value;
            _bewKörperteil.GetMod = BewKörperteilMod;
            mods.Add(BEWKÖRPERTEIL_MOD, _bewKörperteil);

            _nahkampf = new FernkampfModifikator<int>(this);
            if (loadPre)
                _nahkampf.Value = ((FernkampfModifikator<int>)Ausführender.PreFernkampfMods[NAHKAMPF_MOD]).Value;
            _nahkampf.GetMod = NahkampfMod;
            mods.Add(NAHKAMPF_MOD, _nahkampf);

            _handgemenge = new FernkampfModifikator<int>(this);
            if (loadPre)
                _handgemenge.Value = ((FernkampfModifikator<int>)Ausführender.PreFernkampfMods[HANDGEMENGE_MOD]).Value;
            _handgemenge.GetMod = HandgemengeMod;
            mods.Add(HANDGEMENGE_MOD, _handgemenge);

            _bewegung = new FernkampfModifikator<Bewegung>(this);
            if (loadStd)
                _bewegung.Value = Bewegung.Leicht;
            if (loadPre)
                _bewegung.Value = ((FernkampfModifikator<Bewegung>)Ausführender.PreFernkampfMods[BEWEGUNG_MOD]).Value;
            _bewegung.GetMod = BewegungMod;
            mods.Add(BEWEGUNG_MOD, _bewegung);

            _zielen = new FernkampfModifikator<int>(this);
            _zielen.GetMod = ZielenMod;
            mods.Add(ZIELEN_MOD, _zielen);
            if (loadStd)
                _zielen.Value = 1;
            if (loadPre)
                _zielen.Value = ((FernkampfModifikator<int>)Ausführender.PreFernkampfMods[ZIELEN_MOD]).Value;

            _eigene = new FernkampfModifikator<int>(this);
            _eigene.GetMod = EigeneMod;
            mods.Add(EIGENE_MOD, _eigene);

            _schuss2 = new FernkampfModifikator<bool>(this);
            _schuss2.GetMod = Schuss2Mod;
            mods.Add(SCHUSS2_MOD, _schuss2);

            _ansage = new FernkampfModifikator<int>(this);
            if (loadPre)
                _ansage.Value = ((FernkampfModifikator<int>)Ausführender.PreFernkampfMods[ANSAGE_MOD]).Value;
            _ansage.GetMod = AnsageMod;
            mods.Add(ANSAGE_MOD, _ansage);

            _sicht = new FernkampfModifikator<Sichtstufe>(this);
            if (loadStd)
                _sicht.Value = Global.CurrentKampf.Kampf.Sicht;
            if (loadPre)
                _sicht.Value = ((FernkampfModifikator<Sichtstufe>)Ausführender.PreFernkampfMods[SICHT_MOD]).Value;
            _sicht.GetMod = SichtMod;
            mods.Add(SICHT_MOD, _sicht);

            _ohneSattel = new FernkampfModifikator<bool>(this);
            if (loadPre)
                _ohneSattel.Value = ((FernkampfModifikator<bool>)Ausführender.PreFernkampfMods[OHNESATTEL_MOD]).Value;
            _ohneSattel.GetMod = OhneSattelMod;
            mods.Add(OHNESATTEL_MOD, _ohneSattel);

            _unterWasser = new FernkampfModifikator<bool>(this);
            if (loadPre)
                _unterWasser.Value = ((FernkampfModifikator<bool>)Ausführender.PreFernkampfMods[UNTERWASSER_MOD]).Value;
            _unterWasser.GetMod = UnterWasserMod;
            mods.Add(UNTERWASSER_MOD, _unterWasser);

            _positionSelbst = new FernkampfModifikator<Position>(this);
            if (loadStd)
                _positionSelbst.Value = Ausführender.Kämpfer.Position;
            if (loadPre)
                _positionSelbst.Value = ((FernkampfModifikator<Position>)Ausführender.PreFernkampfMods[POS_SELBST_MOD]).Value;
            _positionSelbst.GetMod = PositionSelbstMod;
            mods.Add(POS_SELBST_MOD, _positionSelbst);

            _pferdBewegung = new FernkampfModifikator<int>(this);
            if (loadPre)
                _pferdBewegung.Value = ((FernkampfModifikator<int>)Ausführender.PreFernkampfMods[PFERDBEWEGUNG_MOD]).Value;
            _pferdBewegung.GetMod = PferdBewegungMod;
            mods.Add(PFERDBEWEGUNG_MOD, _pferdBewegung);


            if (loadPre)
            {
                _wind.Value = ((FernkampfModifikator<int>)Ausführender.PreFernkampfMods[WIND_MOD]).Value;
                _steilNachUnten.Value = ((FernkampfModifikator<bool>)Ausführender.PreFernkampfMods[STEILNACHUNTEN_MOD]).Value;
                _steilNachOben.Value = ((FernkampfModifikator<bool>)Ausführender.PreFernkampfMods[STEILNACHOBEN_MOD]).Value;
                _unsichtbar.Value = ((FernkampfModifikator<bool>)Ausführender.PreFernkampfMods[UNSICHTBAR_MOD]).Value;
            }
        }

        private IFernkampfwaffe _fernkampfWaffeSelected = null;
        public IFernkampfwaffe FernkampfWaffeSelected
        {
            get { return _fernkampfWaffeSelected; }
            set 
            {
                Set(ref _fernkampfWaffeSelected, value);
                
                OnChanged("Mods");
                OnChanged("Mods[Ansage]");
            }
        }

        private string _größeBeispiel;
        
        public string GrößeBeispiel
        {
            get {  return _größeBeispiel; }
            set { Set(ref _größeBeispiel, value); }
        }

        private List<IFernkampfwaffe> _fernkampfWaffen = new List<IFernkampfwaffe>();
        public List<IFernkampfwaffe> FernkampfWaffen
        {
            get { return _fernkampfWaffen; }
            set { Set(ref _fernkampfWaffen, value); }
        }

        private int SchützenIndex(IFernkampfwaffe waffe)
        {
            Held held = Ausführender.Kämpfer as Held;
            if (held != null && waffe != null && waffe.Talent != null && FernkampfWaffeSelected  != null && waffe.Talent == FernkampfWaffeSelected.Talent)
            {
                if (held.HatSonderfertigkeitUndVoraussetzungen(Sonderfertigkeit.Meisterschütze + " (" + waffe.Talent.Name + ")"))
                    return 2;
                else if (held.HatSonderfertigkeitUndVoraussetzungen(Sonderfertigkeit.Scharfschütze + " (" + waffe.Talent.Name + ")"))
                    return 1;
            }
            return 0;
        }
        
        private int TrefferzoneMod(IFernkampfwaffe waffe, Trefferzone value)
        {
            int[] mod;
            switch (value)
            {
                case Trefferzone.Kopf:
                case Trefferzone.ArmL:
                case Trefferzone.ArmR:
                    mod = new int[] { 10, 7, 5 };
                    break;
                case Trefferzone.Brust:
                case Trefferzone.Bauch:
                    mod = new int[] { 6, 4, 3 };
                    break;
                case Trefferzone.BeinL:
                case Trefferzone.BeinR:
                    mod = new int[] { 8, 5, 4 };
                    break;
                default:
                    mod = new int[] { 0, 0, 0 };
                    break;
            }

            return mod[SchützenIndex(waffe)];
        }
                
        protected override int LichtMod(IFernkampfwaffe waffe, Lichtstufe value)
        {
            if (mods[UNSICHTBAR_MOD].Result != 0) return 0;
            int mod;
            switch (value)
            {
                case Lichtstufe.Dämmerung:
                    mod = 2;
                    break;
                case Lichtstufe.Mondlicht:
                    mod = 4;
                    break;
                case Lichtstufe.Sternenlicht:
                    mod = 6;
                    break;
                case Lichtstufe.Finsternis:
                    mod = 8;
                    break;
                default:
                    mod = 0;
                    break;
            }
            Held held = Ausführender.Kämpfer as Held;
            if (held != null)
            {
                if (held.HatVorNachteil(VorNachteil.Dämmerungssicht) || held.HatVorNachteil(VorNachteil.Nachtsicht))
                    mod = (int)Math.Round(mod * 0.5, MidpointRounding.AwayFromZero);
                //TODO: Hier prüfen auf was sich die max. +5 bezieht. Nachtsicht beinhaltet ja Dämmerungssicht, was den Abzug halbiert.
                //Da die Maximalen Abzüge für Dunkelheit 8 sind macht das so keinen Sinn
                if (held.HatVorNachteil(VorNachteil.Nachtsicht))
                    mod = Math.Min(mod, 5);
                if (held.HatVorNachteil(VorNachteil.Nachtblind))
                {
                    mod *= 2;
                    mod = Math.Min(mod, 8);
                }
            }
            Global.CurrentKampf.Kampf.Licht = value;
            return mod;
        }

        protected override int SichtMod(IFernkampfwaffe waffe, Sichtstufe value)
        {
            Global.CurrentKampf.Kampf.Sicht = value;
            switch (value)
            {
                case Sichtstufe.Dunst:
                    return 2;
                case Sichtstufe.Nebel:
                    return 4;
                default:
                    return 0;
            }
        }


        protected int PositionSelbstMod(IFernkampfwaffe waffe, Position value)
        {
            if (Global.CurrentKampf.SelectedManöver != null)
            {
                if (Global.CurrentKampf.BodenplanViewModel.DoChangeModPositionSelbst &&
                   ((ManöverModifikator<Position, IFernkampfwaffe>)Mods[POS_SELBST_MOD]).Value != Global.CurrentKampf.SelectedManöver.Manöver.Ausführender.PositionSelbst)
                {
                    ((ManöverModifikator<Position, IFernkampfwaffe>)Mods[POS_SELBST_MOD]).Value = Global.CurrentKampf.SelectedManöver.Manöver.Ausführender.PositionSelbst.Value;
                    value = Global.CurrentKampf.SelectedManöver.Manöver.Ausführender.PositionSelbst.Value;
                }
                Global.CurrentKampf.BodenplanViewModel.DoChangeModPositionSelbst = false;

                IKämpfer bodenplanKämpfer = (Global.CurrentKampf.BodenplanViewModel.BattlegroundObjects.Where(t => t is IKämpfer)
                    .FirstOrDefault(t => ((IKämpfer)t) == Global.CurrentKampf.SelectedManöver.Manöver.Ausführender.Kämpfer) as IKämpfer);

                if (bodenplanKämpfer != null && bodenplanKämpfer.Position != ((ManöverModifikator<Position, IFernkampfwaffe>)Mods[POS_SELBST_MOD]).Value)
                    bodenplanKämpfer.Position = ((ManöverModifikator<Position, IFernkampfwaffe>)Mods[POS_SELBST_MOD]).Value;
            }
            return 0;
        }
        
        private int WindMod(IFernkampfwaffe waffe, int value)
        {
            return value;
        }

        private int UnsichtbarMod(IFernkampfwaffe waffe, bool value)
        {
            return value? 8: 0;            
        }

        private int MunitionMod(IFernkampfwaffe waffe, Munition value)
        {
            return 0;
        }

        private int AxxeleratusAktivMod(IFernkampfwaffe waffe, bool value)
        {
            OnChanged("HatSchnellladenBogen");
            OnChanged("HatSchnellladenArmbrust");
            OnChanged("HatSchnellziehen");
            return 0;
        }

        private int SteilNachUntenMod(IFernkampfwaffe waffe, bool value)
        {
            return value? 2: 0;            
        }

        private int SteilNachObenMod(IFernkampfwaffe waffe, bool value)
        {
            return !value? 0: 
                waffe != null && waffe.Talent != null && waffe.Talent.Talentname.Contains("Wurf")? 
                8 : 4;
        }

        private int BöigerWindMod(IFernkampfwaffe waffe, int value)
        {
            return value *4;
        }

        private int StarkerBöigerWindMod(IFernkampfwaffe waffe, int value)
        {
            return value *8;
        }

        private int DistanzMod(IFernkampfwaffe waffe, int distanz)
        {
            if (waffe == null)
                return 0;
            int mod;
            if (distanz <= waffe.RWSehrNah)
                mod = -2;
            else if (distanz <= waffe.RWNah)
                mod = 0;
            else if (distanz <= waffe.RWMittel)
                mod = 4;
            else if (distanz <= waffe.RWWeit)
                mod = 8;
            else if (distanz <= waffe.RWSehrWeit)
                mod = 12;
            else mod = 100;

            Held held = Ausführender.Kämpfer as Held;
            if (held != null)
            {
                if (held.HatVorNachteil(VorNachteil.Entfernungssinn))
                    mod -= 2;
                if (held.HatVorNachteil(VorNachteil.Einäugig) && distanz < 10)
                    mod += 4;
                if (held.HatVorNachteil(VorNachteil.Farbenblind) && distanz > 50)
                    mod += 4;
            }
            return mod;
        }
        
        protected override int GrößeMod(IFernkampfwaffe waffe, Größe value)
        {            
            switch (value)
            {
                case Größe.Kleiner2AlsWinzig:
                    GrößeBeispiel = "2 Größenklassen kleiner als z.B. eine Münze, Drachenauge, Maus, Kröte";
                    return 12;
                case Größe.Kleiner1AlsWinzig:
                    GrößeBeispiel = "1 Größenklasse kleiner als z.B. eine Münze, Drachenauge, Maus, Kröte";
                    return 10;
                case Größe.Winzig:
                    GrößeBeispiel = "Münze, Drachenauge, Maus, Kröte";
                    return 8;
                case Größe.SehrKlein:
                    GrößeBeispiel = "Schlange, Fasan, Katze, Rabe";
                    return 6;
                case Größe.Klein:
                    GrößeBeispiel = "Wolf, Reh, Schaf";
                    return 4;
                case Größe.Mittel:
                    GrößeBeispiel = "Mensch, Elf, Ork, Goblin, Zwerg";
                    return 2;
                case Größe.Groß:
                    GrößeBeispiel = "Pferd, Steppenrind, Oder, Troll";
                    return 0;
                case Größe.SehrGroß:
                    GrößeBeispiel = "Scheunentor, Drache, Elefant, Riese";
                    return -2;
                case Größe.Größer1AlsSehrGroß:
                    GrößeBeispiel = "1 Größenklasse größer als z.B. ein Scheunentor, Drache, Elefant, Riese";
                    return -4;
                case Größe.Größer2AlsSehrGroß:
                    GrößeBeispiel = "2 Größenklassen größer als z.B. ein Scheunentor, Drache, Elefant, Riese";
                    return -6;
                default:
                    return 0;
            }
        }
                
        private int BewegungMod(IFernkampfwaffe waffe, Bewegung value)
        {
            if (_nahkampf.Value != 0 || _handgemenge.Value != 0) return 0; 
            switch (value)
            {
                case Bewegung.Unbeweglich: 
                    return -4;
                case Bewegung.StillStehend:
                    return -2;
                case Bewegung.Leicht:
                    return 0;
                case Bewegung.Schnell:
                    return 2;
                case Bewegung.SehrSchnell:
                    return 4;
                default:
                    return 0;
            }
        }

        private int NahkampfMod(IFernkampfwaffe waffe, int value)
        {
            return value * 2;
        }

        private int HandgemengeMod(IFernkampfwaffe waffe, int value)
        {
            return value * 3;
        }

        private int BewKörperteilMod(IFernkampfwaffe waffe, bool value)
        {
            return value ? 2 : 0;
        }

        private int GetDauer(IFernkampfwaffe waffe, int Zielen, int Ansage)
        {
            Held held = Ausführender.Kämpfer as Held;
            int d = 1;
            ZielenDauer = Zielen;
            AnsageDauer = waffe == null || waffe.Talent == null? Ansage :
                 // mit Meisterschütze nur 1 Aktion hinzu
                 (waffe.Talent == FernkampfWaffeSelected.Talent &&
                  (held.HatSonderfertigkeit(Sonderfertigkeit.Meisterschütze + " (" + waffe.Talent.Name + ")")) ? 1 :
                 // ohne Meisterschütze je Ansage 1 Aktion
                 (int)(Math.Round((double)Ansage / 2, MidpointRounding.AwayFromZero)));

            if (waffe != null && waffe.Talent != null)
            {
                d = waffe.LadeZeit.Value + ZielenDauer + SchussDauer;
                
                if (held != null && FernkampfWaffeSelected != null)
                {
                    if (waffe.Talent == FernkampfWaffeSelected.Talent &&
                        held.HatSonderfertigkeit(Sonderfertigkeit.Scharfschütze + " (" + waffe.Talent.Name + ")"))
                    {
                        ZielenDauer = (Zielen == 0 ? 0 : (Zielen - 2 >= 1 ? Zielen - 2 : 1));
                        d = waffe.LadeZeit.Value + ZielenDauer + SchussDauer;
                    }

                    if (waffe.Talent == FernkampfWaffeSelected.Talent &&
                        waffe.Talent.TalentGUID.StringConvert() == "00000000-0000-0000-007A-000000000024" && //"Bogen" 
                        (held.HatSonderfertigkeit(Sonderfertigkeit.SchnellladenBogen) || IstAxxeleratusAktiv))
                        d = d - 1 >= 1 ? d - 1 : 1;
                    else
                        if (waffe.Talent == FernkampfWaffeSelected.Talent &&
                            waffe.Talent.TalentGUID.StringConvert() == "00000000-0000-0000-007A-000000000015" && //"Armbrust" 
                            (held.HatSonderfertigkeit(Sonderfertigkeit.SchnellladenArmbrust) || IstAxxeleratusAktiv))
                            d = (int)Math.Round(d * .75 > 1 ? d * .75 : 1);
                        else
                            if (waffe.Talent == FernkampfWaffeSelected.Talent &&
                                held.HatSonderfertigkeit(Sonderfertigkeit.Schnellziehen) &&
                                (waffe.Talent.TalentGUID.StringConvert() == "00000000-0000-0000-007A-000000000379" || //"Wurfbeile" 
                                 waffe.Talent.TalentGUID.StringConvert() == "00000000-0000-0000-007A-000000000380" || //"Wurfmesser"
                                 waffe.Talent.TalentGUID.StringConvert() == "00000000-0000-0000-007A-000000000381" || //"Wurfspeere"
                                 waffe.Talent.TalentGUID.StringConvert() == "00000000-0000-0000-007A-000000000382"))  //"Wurfwaffen" 
                                d = d - 1 >= 1 ? d - 1 : 1;

                    if (Ansage > 0)
                    {
                        d += AnsageDauer;
                    }
                    else
                        AnsageDauer = 0;
                }                
            }
            else
                d = 3 + Ansage + ZielenDauer + SchussDauer;

            return d;
        }
        
        private int ZielenMod(IFernkampfwaffe waffe, int value)
        {
            int mod = 0;
            if (value == 0)
                mod = new int[] { 2, 1, 0 }[SchützenIndex(waffe)];
            else mod = -Math.Min(4, (int)Math.Floor(new double[] { 0.5, 1, 1 }[SchützenIndex(waffe)] * (value - 1)));

            //Dauer = Ladezeit + zielen + Schuss
            int d = GetDauer(waffe, value, _ansage != null? _ansage.Value: Ansage);
            if (d != Dauer)
                Dauer = d;
            VerbleibendeDauer = Dauer;

            return mod;
        }

        private int Schuss2Mod(IFernkampfwaffe waffe, bool value)
        {
            int mod = 0;
            if (value)
            {
                if (waffe == null || waffe.Talent == null ||
                    waffe.Talent == FernkampfWaffeSelected.Talent &&
                    (waffe.Talent.TalentGUID.StringConvert() == "00000000-0000-0000-007A-000000000024" || //"Bogen"
                            waffe.Talent.TalentGUID.StringConvert() == "00000000-0000-0000-007A-000000000015" //"Armbrust" 
                    ))
                    mod = 4;
                else
                    mod = 2;
            }
            return mod;

        }

        private int EigeneMod(IFernkampfwaffe waffe, int value)
        {
            return value;
        }

        private int maxAnsage = 10;
        public int MaxAnsage
        { 
            get { return maxAnsage; }
            set { Set(ref maxAnsage, value); }
        }

        private int AnsageMod(IFernkampfwaffe waffe, int value)
        {
            Held held = Ausführender.Kämpfer as Held;
            if (held != null && waffe != null && waffe.Talent != null && FernkampfWaffeSelected != null)
                MaxAnsage = waffe.Talent == FernkampfWaffeSelected.Talent &&
                            held.HatSonderfertigkeit(Sonderfertigkeit.Meisterschütze + " (" + waffe.Talent.Name + ")") ?
                    (waffe as KämpferFernkampfwaffe).FernkampfOhneMod: held.Talentwert(waffe.Talent.Name);

            int d = GetDauer(waffe, _zielen.Value, value);
            if (d != Dauer)
                Dauer = d;
            VerbleibendeDauer = Dauer;

            if (SchützenIndex(waffe) > 0)
            {
                return value;
            }
            else return Math.Max(0, value * 2 - 1);
        }

        private int PferdBewegungMod(IFernkampfwaffe waffe, int value)
        {
            if (((ManöverModifikator<Position, IFernkampfwaffe>)Mods[POS_SELBST_MOD]).Value != Position.Reitend) return 0; // positionSelbst.Value 
            switch (value)
            {
                case 0: return (waffe != null && waffe.Talent != null && waffe.Talent.Talentname.Contains("Wurf") ? 1 : 2);
                case 1: return (waffe != null && waffe.Talent != null && waffe.Talent.Talentname.Contains("Wurf") ? 2 : 4);
                case 2: return (waffe != null && waffe.Talent != null && waffe.Talent.Talentname.Contains("Wurf") ? 4 : 8);
            }
            return 0;
        }

        private int UnterWasserMod(IFernkampfwaffe waffe, bool value)
        {
            return value ? 3 : 0;
        }

        private int OhneSattelMod(IFernkampfwaffe waffe, bool value)
        {
            return (!value || ((ManöverModifikator<Position, IFernkampfwaffe>)Mods[POS_SELBST_MOD]).Value != Position.Reitend) ? 0 ://positionSelbst.Value 
                (waffe != null && waffe.Talent != null && waffe.Talent.Talentname.Contains("Wurf") ? 2 : 4);
        }

        #endregion

        private int _anzAkt = 0;
        public int anzAkt
        {
            get { return _anzAkt; }
            set{ Set(ref _anzAkt, value);}
        }

        protected override void Init()
        {
            base.Init();
            //TODO: Aktionen?
            Typ = ManöverTyp.Aktion;
        }

        protected override void Erfolg(Probe p, KämpferInfo ziel, IFernkampfwaffe waffe, ManöverEventArgs e_init)
        {
            //TODO: Implementieren
        }

        //TODO: Ausführen implementieren

        protected override void Patzer(Probe p, KämpferInfo ziel, IFernkampfwaffe waffe, ManöverEventArgs e_init)
        {
            int random = ViewHelper.ShowWürfelDialog("2W6", "Patzer");
            switch (random)
            {
                case 2:
                    //Waffe zerstört
                    Ausführender.Initiative -= 4;
                    break;
                case 3:
                    //Waffe beschädigt
                    Ausführender.Initiative -= 3;
                    break;
                case 4:
                case 5:
                case 6:
                case 7:
                case 9:
                case 10:
                    //Fehlschuss
                    Ausführender.Initiative -= 2;
                    break;
                case 11:
                case 12:
                    //Kamerad getroffen
                    Ausführender.Initiative -= 3;
                    break;
            }

            base.Patzer(p, ziel, waffe, e_init);
        }
    }
}
