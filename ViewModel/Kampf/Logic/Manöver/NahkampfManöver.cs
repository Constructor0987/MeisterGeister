using MeisterGeister.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeisterGeister.ViewModel.Kampf.Logic.Manöver
{
    public abstract class NahkampfManöver : Manöver<INahkampfwaffe>
    {
        public NahkampfManöver(KämpferInfo ausführender)
            : base(ausführender)
        { }

        public NahkampfManöver(KämpferInfo ausführender, IDictionary<INahkampfwaffe, KämpferInfo> waffe_ziel)
            : base(ausführender, waffe_ziel)
        { }

        public NahkampfManöver(KämpferInfo ausführender, INahkampfwaffe waffe, KämpferInfo ziel)
            : base(ausführender, waffe, ziel)
        { }

        #region Mods

        public const string LICHT_MOD = "Licht";
        public const string WASSER_MOD = "Wasser";
        public const string BEENGT_MOD = "Beengt";
        public const string DISTANZLASSE_MOD = "Distanzklasse";
        public const string POS_SELBST_MOD = "PositionSelbst";
        public const string POS_GEGNER_MOD = "PositionGegner";
        public const string FALSCHE_HAND_MOD = "FalscheHand";
        public const string ÜBERZAHL_MOD = "Überzahl";
        public const string UNBEWAFFNET_MOD = "Unbewaffnet";
        public const string GRÖSSE_MOD = "Zielgröße";

        protected override void InitMods()
        {
            base.InitMods();

            NahkampfModifikator<int> licht = new NahkampfModifikator<int>(this);
            licht.GetMod = LichtMod;
            mods.Add(LICHT_MOD, licht);

            NahkampfModifikator<Wassertiefe> wasser = new NahkampfModifikator<Wassertiefe>(this);
            wasser.GetMod = WasserMod;
            mods.Add(WASSER_MOD, wasser);

            NahkampfModifikator<bool> beengt = new NahkampfModifikator<bool>(this);
            beengt.GetMod = BeengtMod;
            mods.Add(BEENGT_MOD, beengt);

            NahkampfModifikator<int> distanzklasse = new NahkampfModifikator<int>(this);
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

        protected abstract int GrößeMod(INahkampfwaffe waffe, Größe value);

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

        protected virtual int LichtMod(INahkampfwaffe waffe, int value)
        {
            int mod = value;
            Held held = Ausführender.Kämpfer as Held;
            if (held != null)
            {
                if (held.HatVorNachteil("Dämmerungssicht") || held.HatVorNachteil("Nachtsicht"))
                    mod = (int)Math.Round(mod * 0.5, MidpointRounding.AwayFromZero);
                if (held.HatVorNachteil("Nachtsicht") ||
                   //TODO: keine Peitsche/Lanzenreiten und keine Pikenreichweite um Blindkampf zu nutzen
                   (held.HatSonderfertigkeitUndVoraussetzungen("Blindkampf") && (waffe.Distanzklasse & Distanzklasse.HNS) != Distanzklasse.None))
                    mod = Math.Min(mod, 2);
                if (held.HatVorNachteil("Nachtblind"))
                    mod *= 2;
            }
            return Math.Min(8, mod);
        }

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
    }
}
