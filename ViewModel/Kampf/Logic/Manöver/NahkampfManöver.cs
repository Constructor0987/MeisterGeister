using MeisterGeister.Logic.General;
using MeisterGeister.Model;
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
        public const string POS_SELBST_MOD = "PositionSelbst";
        public const string POS_GEGNER_MOD = "PositionGegner";
        public const string FALSCHE_HAND_MOD = "FalscheHand";
        public const string ÜBERZAHL_MOD = "Überzahl";
        public const string UNBEWAFFNET_MOD = "Unbewaffnet";

        protected NahkampfModifikator<Lichtstufe> licht;
        protected NahkampfModifikator<Wassertiefe> wasser;
        protected NahkampfModifikator<bool> beengt;
        protected NahkampfModifikator<int> distanzklasse;
        protected NahkampfModifikator<Position> position_selbst;
        protected NahkampfModifikator<Position> position_gegner;
        protected NahkampfModifikator<bool> falscheHand;
        protected NahkampfModifikator<int> überzahl;
        protected NahkampfModifikator<bool> unbewaffnet;
        protected NahkampfModifikator<Größe> größe;

        protected override void InitMods()
        {
            base.InitMods();

            licht = new NahkampfModifikator<Lichtstufe>(this);
            licht.GetMod = LichtMod;
            mods.Add(LICHT_MOD, licht);

            wasser = new NahkampfModifikator<Wassertiefe>(this);
            wasser.GetMod = WasserMod;
            mods.Add(WASSER_MOD, wasser);

            beengt = new NahkampfModifikator<bool>(this);
            beengt.GetMod = BeengtMod;
            mods.Add(BEENGT_MOD, beengt);

            distanzklasse = new NahkampfModifikator<int>(this);
            mods.Add(DISTANZLASSE_MOD, distanzklasse);

            //----------POSITION----------

            NahkampfModifikator<Position> position_selbst = new NahkampfModifikator<Position>(this);
            position_selbst.GetMod = PositionSelbstMod;
            mods.Add(POS_SELBST_MOD, position_selbst);

            NahkampfModifikator<Position> position_gegner = new NahkampfModifikator<Position>(this);
            position_gegner.GetMod = PositionGegnerMod;
            mods.Add(POS_GEGNER_MOD, position_gegner);

            NahkampfModifikator<bool> falscheHand = new NahkampfModifikator<bool>(this);
            falscheHand.GetMod = FalscheHandMod;
            mods.Add(FALSCHE_HAND_MOD, falscheHand);

            NahkampfModifikator<int> überzahl = new NahkampfModifikator<int>(this);
            überzahl.GetMod = ÜberzahlMod;
            mods.Add(ÜBERZAHL_MOD, überzahl);

            NahkampfModifikator<bool> unbewaffnet = new NahkampfModifikator<bool>(this);
            unbewaffnet.GetMod = UnbewaffnetMod;
            mods.Add(UNBEWAFFNET_MOD, unbewaffnet);

            NahkampfModifikator<Größe> größe = new NahkampfModifikator<Größe>(this);
            größe.Value = Größe.Mittel;
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

                //TODO: keine Peitsche/Lanzenreiten und keine Pikenreichweite um Blindkampf zu nutzen
                bool blindkampf = held.HatSonderfertigkeitUndVoraussetzungen("Blindkampf") &&
                    (waffe.Distanzklasse & Distanzklasse.HNS) != Distanzklasse.None;

                if (held.HatVorNachteil("Nachtsicht") || blindkampf)
                    mod = Math.Min(mod, 2);
                if (held.HatVorNachteil("Nachtblind"))
                    mod *= 2;
            }
            return Math.Min(8, mod);
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

        protected virtual int PositionSelbstMod(INahkampfwaffe waffe, Position value)
        {
            switch (value)
            {
                case Position.Liegend:
                    return 3;
                case Position.Kniend:
                    return 1;
                default:
                    return 0;
            }
        }

        protected abstract int PositionGegnerMod(INahkampfwaffe waffe, Position value);

        protected abstract int WasserMod(INahkampfwaffe waffe, Wassertiefe value);

        protected int CheckWasserkampf(int mod, Wassertiefe tiefe, Held ausführender)
        {
            if (ausführender == null)
                return mod;

            //TODO: Die SF kann in schultertiefem Wasser nur mit den Talenten Dolche, Infanteriewaffen und Speere genutzt werden, ansonsten mit allen Nahkampffertigkeiten außer Peitsche und Lanzenreiten.
            bool kampfImWasser = ausführender.HatSonderfertigkeitUndVoraussetzungen("Kampf im Wasser");
            if (kampfImWasser && tiefe != Wassertiefe.KeinWasser && tiefe != Wassertiefe.UnterWasser)
                mod = (int)Math.Round(mod * 0.5, MidpointRounding.AwayFromZero);
            //TODO: Kann nur mit den Nahkampf-Talenten Dolche, Fechtwaffen, Raufen, Ringen und Speere eingesetzt werden.
            if (ausführender.HatSonderfertigkeitUndVoraussetzungen("Unterwasserkampf") && tiefe == Wassertiefe.UnterWasser)
                mod = 0;

            return mod;
        }

        #endregion

        protected override void Init()
        {
            base.Init();
            //TODO: Sinnvolles Standardziel auswählen
            WaffeZiel[Ausführender.Kämpfer.Angriffswaffen.First()] = null;
        }

        protected override void Patzer(Probe p, KämpferInfo ziel)
        {
            int random = ViewHelper.ShowWürfelDialog("2W6", "Patzer");
            switch(random)
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
            base.Patzer(p, ziel);
        }
    }
}
