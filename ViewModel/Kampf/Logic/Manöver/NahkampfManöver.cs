using MeisterGeister.Logic.General;
using MeisterGeister.Model;
using MeisterGeister.Model.Extensions;
using MeisterGeister.View.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeisterGeister.ViewModel.Kampf.Logic.Manöver
{
    public abstract class NahkampfManöver : KampfManöver<INahkampfwaffe>
    {
        public NahkampfManöver(KämpferInfo ausführender)
            : base(ausführender)
        { }

        #region Mods

        public const string WASSER_MOD = "Wasser";
        public const string BEENGT_MOD = "Beengt";
        public const string DISTANZLASSE_MOD = "Distanzklasse";
        public const string POS_GEGNER_MOD = "PositionGegner";
        public const string FALSCHE_HAND_MOD = "FalscheHand";
        public const string ÜBERZAHL_MOD = "Überzahl";
        public const string UNBEWAFFNET_MOD = "Unbewaffnet";
        public const string POS_SELBST_MOD = "PositionSelbst";

        protected NahkampfModifikator<Lichtstufe> licht;
        protected NahkampfModifikator<Sichtstufe> sicht;
        protected NahkampfModifikator<Position> positionSelbst;
        protected NahkampfModifikator<Wassertiefe> wasser;
        protected NahkampfModifikator<bool> beengt;
        protected NahkampfModifikator<int> distanzklasse;
        protected NahkampfModifikator<Position> position_gegner;
        protected NahkampfModifikator<bool> falscheHand;
        protected NahkampfModifikator<int> überzahl;
        protected NahkampfModifikator<bool> unbewaffnet;
        protected NahkampfModifikator<Größe> größe;

        protected override void InitMods(IWaffe waffe)
        {
            base.InitMods(waffe);

            licht = new NahkampfModifikator<Lichtstufe>(this);
            if (Ausführender.PreAngriffsMods != null)
                licht.Value = ((NahkampfModifikator<Lichtstufe>)Ausführender.PreAngriffsMods[LICHT_MOD]).Value;
            licht.GetMod = LichtMod;
            mods.Add(LICHT_MOD, licht);

            sicht = new NahkampfModifikator<Sichtstufe>(this);
            if (Ausführender.PreAngriffsMods != null)
                sicht.Value = ((NahkampfModifikator<Sichtstufe>)Ausführender.PreAngriffsMods[SICHT_MOD]).Value;
            sicht.GetMod = SichtMod;
            mods.Add(SICHT_MOD, sicht);
            
            wasser = new NahkampfModifikator<Wassertiefe>(this);
            if (Ausführender.PreAngriffsMods != null)
                wasser.Value = ((NahkampfModifikator<Wassertiefe>)Ausführender.PreAngriffsMods[WASSER_MOD]).Value;
            wasser.GetMod = WasserMod;
            mods.Add(WASSER_MOD, wasser);

            beengt = new NahkampfModifikator<bool>(this);
            if (Ausführender.PreAngriffsMods != null)
                beengt.Value = ((NahkampfModifikator<bool>)Ausführender.PreAngriffsMods[BEENGT_MOD]).Value;
            beengt.GetMod = BeengtMod;
            mods.Add(BEENGT_MOD, beengt);

            distanzklasse = new NahkampfModifikator<int>(this);
            if (Ausführender.PreAngriffsMods != null)
                distanzklasse.Value = ((NahkampfModifikator<int>)Ausführender.PreAngriffsMods[DISTANZLASSE_MOD]).Value;
            mods.Add(DISTANZLASSE_MOD, distanzklasse);

            //----------POSITION----------

            positionSelbst = new NahkampfModifikator<Position>(this);
            positionSelbst.GetMod = PositionSelbstMod;
       //     positionSelbst.Value = Ausführender.Kämpfer.Position;
            mods.Add(POS_SELBST_MOD, positionSelbst);

            NahkampfModifikator<Position> position_gegner = new NahkampfModifikator<Position>(this);
            if (Ausführender.PreAngriffsMods != null)
                position_gegner.Value = ((NahkampfModifikator<Position>)Ausführender.PreAngriffsMods[POS_GEGNER_MOD]).Value;
            position_gegner.GetMod = PositionGegnerMod;
            mods.Add(POS_GEGNER_MOD, position_gegner);

            NahkampfModifikator<bool> falscheHand = new NahkampfModifikator<bool>(this);
            if (Ausführender.PreAngriffsMods != null)
                falscheHand.Value = ((NahkampfModifikator<bool>)Ausführender.PreAngriffsMods[FALSCHE_HAND_MOD]).Value;
            falscheHand.GetMod = FalscheHandMod;
            mods.Add(FALSCHE_HAND_MOD, falscheHand);

            NahkampfModifikator<int> überzahl = new NahkampfModifikator<int>(this);
            if (Ausführender.PreAngriffsMods != null)
                überzahl.Value = ((NahkampfModifikator<int>)Ausführender.PreAngriffsMods[ÜBERZAHL_MOD]).Value;
            überzahl.GetMod = ÜberzahlMod;
            mods.Add(ÜBERZAHL_MOD, überzahl);

            NahkampfModifikator<bool> unbewaffnet = new NahkampfModifikator<bool>(this);
            if (Ausführender.PreAngriffsMods != null)
                unbewaffnet.Value = ((NahkampfModifikator<bool>)Ausführender.PreAngriffsMods[UNBEWAFFNET_MOD]).Value;
            unbewaffnet.GetMod = UnbewaffnetMod;
            mods.Add(UNBEWAFFNET_MOD, unbewaffnet);

            NahkampfModifikator<Größe> größe = new NahkampfModifikator<Größe>(this);
            größe.Value = Ausführender.PreAngriffsMods == null ? Größe.Mittel :
                ((NahkampfModifikator<Größe>)Ausführender.PreAngriffsMods[GRÖSSE_MOD]).Value;
            größe.GetMod = GrößeMod;
            mods.Add(GRÖSSE_MOD, größe);                        
        }

        protected override int LichtMod(INahkampfwaffe waffe, Lichtstufe value)
        {
            int mod = 0;
            switch (value)
            {
                case Lichtstufe.Mondlicht:
                    mod = 3;
                    break;
                case Lichtstufe.Sternenlicht:
                    mod = 5;
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

                //keine Peitsche/Lanzenreiten und keine Pikenreichweite um Blindkampf zu nutzen
                bool blindkampf = held.HatSonderfertigkeitUndVoraussetzungen("Blindkampf") &&
                    (waffe.Distanzklasse & Distanzklasse.HNS) != Distanzklasse.None &&
                    !KämpftMitTalent(waffe, "Peitsche", "Lanzenreiten");

                if (held.HatVorNachteil("Nachtsicht") || blindkampf)
                    mod = Math.Min(mod, 2);
                if (held.HatVorNachteil("Nachtblind"))
                    mod *= 2;
            }
            return Math.Min(8, mod);
        }


        protected override int SichtMod(INahkampfwaffe waffe, Sichtstufe value)
        {
            return 0;
        }

        protected int PositionSelbstMod(INahkampfwaffe waffe, Position value)
        {
            if (Global.CurrentKampf.SelectedManöver != null)
            {
                if (Global.CurrentKampf.BodenplanViewModel.DoChangeModPositionSelbst &&
                   ((ManöverModifikator<Position, INahkampfwaffe>)Mods[POS_SELBST_MOD]).Value != Global.CurrentKampf.SelectedManöver.Manöver.Ausführender.PositionSelbst)
                {
                    ((ManöverModifikator<Position, INahkampfwaffe>)Mods[POS_SELBST_MOD]).Value = Global.CurrentKampf.SelectedManöver.Manöver.Ausführender.PositionSelbst.Value;
                    value = Global.CurrentKampf.SelectedManöver.Manöver.Ausführender.PositionSelbst.Value;
                }

                IKämpfer bodenplanKämpfer = (Global.CurrentKampf.BodenplanViewModel.BattlegroundObjects.Where(t => t is IKämpfer)
                    .FirstOrDefault(t => ((IKämpfer)t) == Global.CurrentKampf.SelectedManöver.Manöver.Ausführender.Kämpfer) as IKämpfer);

                if (Global.CurrentKampf.Kampf.tempP == null &&
                    bodenplanKämpfer != null && bodenplanKämpfer.Position != ((ManöverModifikator<Position, INahkampfwaffe>)Mods[POS_SELBST_MOD]).Value)
                    bodenplanKämpfer.Position = ((ManöverModifikator<Position, INahkampfwaffe>)Mods[POS_SELBST_MOD]).Value;
            }
            Global.CurrentKampf.BodenplanViewModel.DoChangeModPositionSelbst = false;
            switch (value)
            {
                case Position.Kniend:
                    return 1;
                case Position.Liegend:
                    return 3;
                default:
                    return 0;
            }
        }

        protected abstract int BeengtMod(INahkampfwaffe waffe, bool value);

        protected abstract int UnbewaffnetMod(INahkampfwaffe waffe, bool value);

        protected abstract int ÜberzahlMod(INahkampfwaffe waffe, int value);

        private int FalscheHandMod(INahkampfwaffe waffe, bool value)
        {
            if (value)
            {
                int malus = 9;
                if (Ausführender.Kämpfer is Held)
                {
                    Held held = (Held)Ausführender.Kämpfer;
                    if (held.HatSonderfertigkeitUndVoraussetzungen("Linkhand"))
                        malus -= 3;
                    if (held.HatSonderfertigkeitUndVoraussetzungen("Beidhändiger Kampf I"))
                        malus -= 3;
                    if (held.HatSonderfertigkeitUndVoraussetzungen("Beidhändiger Kampf II"))
                        malus -= 3;
                    if (held.HatVorNachteil("Beidhändig"))
                        malus = 0;
                }
                return malus;
            }
            else return 0;
        }


        protected abstract int PositionGegnerMod(INahkampfwaffe waffe, Position value);

        protected abstract int WasserMod(INahkampfwaffe waffe, Wassertiefe value);

        protected int CheckWasserkampf(int mod, Wassertiefe tiefe, Held ausführender, INahkampfwaffe waffe)
        {
            if (ausführender == null || waffe == null)
                return mod;

            //Kampf im Wasser
            string[] schulterTiefTalente = new string[] { "Dolche", "Infanteriewaffen", "Speere" };
            string[] hüftKnieTiefTalente = new string[] { "Peitsche", "Lanzenreiten" };

            bool kampfImWasser = ausführender.HatSonderfertigkeitUndVoraussetzungen("Kampf im Wasser");
            if (tiefe == Wassertiefe.Schultertief)
                kampfImWasser &= KämpftMitTalent(waffe, "Dolche", "Infanteriewaffen", "Speere");
            else
                kampfImWasser &= !KämpftMitTalent(waffe, "Peitsche", "Lanzenreiten");

            if (kampfImWasser && tiefe != Wassertiefe.KeinWasser && tiefe != Wassertiefe.UnterWasser)
                mod = (int)Math.Round(mod * 0.5, MidpointRounding.AwayFromZero);

            //Unterwasserkampf
            bool unterwasserKampf = ausführender.HatSonderfertigkeitUndVoraussetzungen("Unterwasserkampf");
            unterwasserKampf &= KämpftMitTalent(waffe, "Dolche", "Fechtwaffen", "Raufen", "Ringen", "Speere");
            if (unterwasserKampf && tiefe == Wassertiefe.UnterWasser)
                mod = 0;

            return mod;
        }
        
        //TODO:  AKTUALISIERUNG in der Combobox NICHT OKAY
        //private Position _getKämpferPosition = Position.Stehend;
        //public Position GetKämpferPosition
        //{
        //    get
        //    {
        //        _getKämpferPosition = Ausführender.Kämpfer.Position;
        //        ((NahkampfModifikator<Position>)Mods["PositionSelbst"]).Value = Ausführender.Kämpfer.Position;
        //        return ((NahkampfModifikator<Position>)Mods["PositionSelbst"]).Value;
        //    }
        //    set 
        //    {
        //        ((NahkampfModifikator<Position>)Mods["PositionSelbst"]).Value = value;
        //        Set(ref _getKämpferPosition, value);
        //    }
        //}

        

        //private Base.CommandBase _onBtnPositionSelbstRefresh;
        //public Base.CommandBase OnBtnPositionSelbstRefresh
        //{
        //    get
        //    {
        //        if (_onBtnPositionSelbstRefresh == null)
        //        {
        //            _onBtnPositionSelbstRefresh = new Base.CommandBase(PositionSelbstRefresh, null);
        //        }
        //        return _onBtnPositionSelbstRefresh;
        //    }
        //}

        //private void PositionSelbstRefresh(object o)
        //{
        //    ((NahkampfModifikator<Position>)Mods["PositionSelbst"]).Value = Ausführender.Kämpfer.Position;

        //}
            

        #endregion

        protected override void Init()
        {
            base.Init();

            INahkampfwaffe waffe = Ausführender.Kämpfer.Angriffswaffen.FirstOrDefault();
            if (waffe != null)
                //TODO: Sinnvolles Standardziel auswählen
                WaffeZiel[waffe] = null;
        }

        protected override void Patzer(Probe p, KämpferInfo ziel, INahkampfwaffe waffe, ManöverEventArgs e_init)
        {
            int random = ViewHelper.ShowWürfelDialog("2W6", "Patzer");
            switch (random)
            {
                case 2:
                    //Waffe zerstört
                    Ausführender.Initiative -= 4;
                    break;
                case 3:
                case 4:
                case 5:
                    //Sturz
                    Ausführender.Initiative -= 2;
                    Ausführender.Kämpfer.Position = Position.Liegend;
                    break;
                case 6:
                case 7:
                case 8:
                    //Stolpern
                    Ausführender.Initiative -= 2;
                    break;
                case 9:
                case 10:
                    //Waffe verloren
                    Ausführender.Initiative -= 2;
                    break;
                case 11:
                    //Selbst verletzt
                    Ausführender.Initiative -= 3;
                    break;
                case 12:
                    //Schwerer Eigentreffer
                    Ausführender.Initiative -= 4;
                    break;
            }

            //Misserfolg
            base.Patzer(p, ziel, waffe, e_init);
        }
    }
}
