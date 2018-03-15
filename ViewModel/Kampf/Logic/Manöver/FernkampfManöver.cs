using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeisterGeister.Logic.General;
using MeisterGeister.Model;
using MeisterGeister.View.General;
using MeisterGeister.Model.Extensions;
using System.Windows.Data;
using System.Windows;
using System.Globalization;

namespace MeisterGeister.ViewModel.Kampf.Logic.Manöver
{
    public class FernkampfManöver : KampfManöver<IFernkampfwaffe>
    {       
        public FernkampfManöver(KämpferInfo ausführender, IFernkampfwaffe Fernkampfwaffe) : base(ausführender)
        {
            this.FernkampfWaffeSelected = Fernkampfwaffe;            
        }

        #region Mods

        public const string WIND_MOD = "Wind";
        public const string STEILNACHUNTEN_MOD = "SteilNachUnten";
        public const string STEILNACHOBEN_MOD = "SteilNachOben";
        public const string BÖIGERWIND_MOD = "BöigerWind";
        public const string STARKERBÖIGERWIND_MOD = "StarkerBöigerWind";
        public const string POS_SELBST_MOD = "PositionSelbst";

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

        protected FernkampfModifikator<Lichtstufe> licht;
        protected FernkampfModifikator<Sichtstufe> sicht;
        protected FernkampfModifikator<Position> positionSelbst;

        protected FernkampfModifikator<bool> steilNachUnten;
        protected FernkampfModifikator<bool> steilNachOben;
        protected FernkampfModifikator<int> wind;

        protected FernkampfModifikator<Größe> größe;
        protected FernkampfModifikator<bool> unsichtbar;
        protected FernkampfModifikator<int> deckung;
        protected FernkampfModifikator<int> distanz;
        protected FernkampfModifikator<Trefferzone> trefferzone;
        protected FernkampfModifikator<Bewegung> bewegung;
        protected FernkampfModifikator<bool> bewKörperteil;
        protected FernkampfModifikator<bool> unterWasser;
        protected FernkampfModifikator<int> nahkampf;
        protected FernkampfModifikator<int> handgemenge;
        protected FernkampfModifikator<int> zielen;
        protected FernkampfModifikator<int> ansage;

        protected FernkampfModifikator<int> pferdBewegung;
        protected FernkampfModifikator<bool> ohneSattel;

        protected override void InitMods()
        {
            base.InitMods();

            licht = new FernkampfModifikator<Lichtstufe>(this);
            licht.Value = Global.CurrentKampf.Kampf.Licht;
            licht.GetMod = LichtMod;
            mods.Add(LICHT_MOD, licht);
             
            wind = new FernkampfModifikator<int>(this);
            wind.GetMod = WindMod;
            mods.Add(WIND_MOD, wind);

            steilNachUnten = new FernkampfModifikator<bool>(this);
            steilNachUnten.GetMod = SteilNachUntenMod;
            mods.Add(STEILNACHUNTEN_MOD, steilNachUnten);

            steilNachOben = new FernkampfModifikator<bool>(this);
            steilNachOben.GetMod = SteilNachObenMod;
            mods.Add(STEILNACHOBEN_MOD, steilNachOben);

            unsichtbar = new FernkampfModifikator<bool>(this);
            unsichtbar.GetMod = UnsichtbarMod;
            mods.Add(UNSICHTBAR_MOD, unsichtbar);

            größe = new FernkampfModifikator<Größe>(this);
            größe.Value = Größe.Mittel;
            größe.GetMod = GrößeMod;
            mods.Add(GRÖSSE_MOD, größe);
            
            FernkampfModifikator<int> deckung = new FernkampfModifikator<int>(this);
            mods.Add(DECKUNG_MOD, deckung);
            
            distanz = new FernkampfModifikator<int>(this);
            distanz.Value = 1;
            distanz.GetMod = DistanzMod;
            mods.Add(DISTANZ_MOD, distanz);

            trefferzone = new FernkampfModifikator<Trefferzone>(this);
            trefferzone.Value = Trefferzone.Unlokalisiert;
            trefferzone.GetMod = TrefferzoneMod;
            mods.Add(TREFFERZONE_MOD, trefferzone);

            bewKörperteil = new FernkampfModifikator<bool>(this);
            bewKörperteil.Value = false;
            bewKörperteil.GetMod = BewKörperteilMod;
            mods.Add(BEWKÖRPERTEIL_MOD, bewKörperteil);

            nahkampf = new FernkampfModifikator<int>(this);
            nahkampf.GetMod = NahkampfMod;
            mods.Add(NAHKAMPF_MOD, nahkampf);

            handgemenge = new FernkampfModifikator<int>(this);
            handgemenge.GetMod = HandgemengeMod;
            mods.Add(HANDGEMENGE_MOD, handgemenge);

            bewegung = new FernkampfModifikator<Bewegung>(this);
            bewegung.Value = Bewegung.Leicht;
            bewegung.GetMod = BewegungMod;
            mods.Add(BEWEGUNG_MOD, bewegung);

            zielen = new FernkampfModifikator<int>(this);
            zielen.GetMod = ZielenMod;
            mods.Add(ZIELEN_MOD, zielen);
            zielen.Value = 1;

            ansage = new FernkampfModifikator<int>(this);
            ansage.GetMod = AnsageMod;
            mods.Add(ANSAGE_MOD, ansage);

            sicht = new FernkampfModifikator<Sichtstufe>(this);
            sicht.Value = Global.CurrentKampf.Kampf.Sicht;
            sicht.GetMod = SichtMod;
            mods.Add(SICHT_MOD, sicht);


            ohneSattel = new FernkampfModifikator<bool>(this);
            ohneSattel.GetMod = OhneSattelMod;
            mods.Add(OHNESATTEL_MOD, ohneSattel);

            unterWasser = new FernkampfModifikator<bool>(this);
            unterWasser.Value = false;
            unterWasser.GetMod = UnterWasserMod;
            mods.Add(UNTERWASSER_MOD, unterWasser);
            
            //positionSelbst = new FernkampfModifikator<Position>(this);
            //positionSelbst.Value = (Global.CurrentKampf.BodenplanViewModel.SelectedObject as Wesen).Position;
            //positionSelbst.GetMod = PositionSelbstMod;
            //mods.Add(POS_SELBST_MOD, positionSelbst);

            positionSelbst = new FernkampfModifikator<Position>(this);
            positionSelbst.Value = Ausführender.Kämpfer.Position;
            positionSelbst.GetMod = PositionSelbstMod;
            mods.Add(POS_SELBST_MOD, positionSelbst);

            pferdBewegung = new FernkampfModifikator<int>(this);
            pferdBewegung.Value = 0;
            pferdBewegung.GetMod = PferdBewegungMod;
            mods.Add(PFERDBEWEGUNG_MOD, pferdBewegung);
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
                if (held.HatSonderfertigkeitUndVoraussetzungen("Meisterschütze (" + waffe.Talent.Name + ")"))
                    return 2;
                else if (held.HatSonderfertigkeitUndVoraussetzungen("Scharfschütze (" + waffe.Talent.Name + ")"))
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
                if (held.HatVorNachteil("Dämmerungssicht") || held.HatVorNachteil("Nachtsicht"))
                    mod = (int)Math.Round(mod * 0.5, MidpointRounding.AwayFromZero);
                //TODO: Hier prüfen auf was sich die max. +5 bezieht. Nachtsicht beinhaltet ja Dämmerungssicht, was den Abzug halbiert.
                //Da die Maximalen Abzüge für Dunkelheit 8 sind macht das so keinen Sinn
                if (held.HatVorNachteil("Nachtsicht"))
                    mod = Math.Min(mod, 5);
                if (held.HatVorNachteil("Nachtblind"))
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
                if (held.HatVorNachteil("Entfernungssinn"))
                    mod -= 2;
                if (held.HatVorNachteil("Einäugig") && distanz < 10)
                    mod += 4;
                if (held.HatVorNachteil("Farbenblind") && distanz > 50)
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
            if (nahkampf.Value != 0 || handgemenge.Value != 0) return 0; 
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
            if (waffe != null && waffe.Talent != null)
            {
                d = waffe.LadeZeit.Value + Zielen + 1;

                if (held != null && FernkampfWaffeSelected != null)
                {
                    if (waffe.Talent == FernkampfWaffeSelected.Talent &&
                        held.HatSonderfertigkeit("Scharfschütze (" + waffe.Talent.Name + ")"))
                        d = waffe.LadeZeit.Value + (Zielen == 0 ? 0 : (Zielen - 2 >= 1 ? Zielen - 2 : 1)) + 1;


                    if (waffe.Talent == FernkampfWaffeSelected.Talent &&
                        waffe.Talent.Talentname == "Bogen" && held.HatSonderfertigkeit("Schnellladen (Bogen)"))
                        d = d - 1 >= 1 ? d - 1 : 1;
                    else
                        if (waffe.Talent == FernkampfWaffeSelected.Talent &&
                            waffe.Talent.Talentname == "Armbrust" && held.HatSonderfertigkeit("Schnellladen (Armbrust)"))
                            d = (int)Math.Round(d * .75 > 1 ? d * .75 : 1);
                        else
                            if (waffe.Talent == FernkampfWaffeSelected.Talent &&
                                waffe.Talent.Talentname == "Wurfwaffen" && held.HatSonderfertigkeit("Schnellziehen (Wurfwaffen)"))
                                d = d - 1 >= 1 ? d - 1 : 1;

                    if (Ansage > 0)
                    d += // mit Meisterschütze nur 1 Aktion hinzu
                         (waffe.Talent == FernkampfWaffeSelected.Talent &&
                          (held.HatSonderfertigkeit("Meisterschütze (" + waffe.Talent.Name + ")")) ? 1 :
                         // ohne Meisterschütze je Ansage 1 Aktion
                         (int)(Math.Round((double)Ansage/2, MidpointRounding.AwayFromZero)));
                }
            }
            else
                d = 2 + Zielen + 1;

            return d;
        }
        
        private int ZielenMod(IFernkampfwaffe waffe, int value)
        {
            int mod = 0;
            if (value == 0)
                mod = new int[] { 2, 1, 0 }[SchützenIndex(waffe)];
            else mod = -Math.Min(4, (int)Math.Floor(new double[] { 0.5, 1, 1 }[SchützenIndex(waffe)] * (value - 1)));

            //Dauer = Ladezeit + zielen + Schuss
            int d = GetDauer(waffe, value, Ansage);
            if (d != Dauer)
                Dauer = d;

            return mod;
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
                            held.HatSonderfertigkeit("Meisterschütze (" + waffe.Talent.Name + ")") ?
                    (waffe as KämpferFernkampfwaffe).FernkampfOhneMod: held.Talentwert(waffe.Talent.Name);

            int d = GetDauer(waffe, zielen.Value, value);
            if (d != Dauer)
                Dauer = d;

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
