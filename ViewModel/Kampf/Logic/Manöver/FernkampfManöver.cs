using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeisterGeister.Logic.General;
using MeisterGeister.Model;

namespace MeisterGeister.ViewModel.Kampf.Logic.Manöver
{
    public class FernkampfManöver : KampfManöver<IFernkampfwaffe>
    {
        public FernkampfManöver(KämpferInfo ausführender) : base(ausführender)
        {

        }

        #region Mods

        public const string DISTANZ_MOD = "Entfernung";
        public const string TREFFERZONE_MOD = "Trefferzone";
        public const string NAHKAMPF_MOD = "Nahkampf";
        public const string HANDGEMENGE_MOD = "Handgemenge";
        public const string ZIELEN_MOD = "Zielen";

        protected override void InitMods()
        {
            base.InitMods();
            FernkampfModifikator<Lichtstufe> licht = new FernkampfModifikator<Lichtstufe>(this);
            licht.GetMod = LichtMod;
            mods.Add(LICHT_MOD, licht);

            FernkampfModifikator<int> distanz = new FernkampfModifikator<int>(this);
            distanz.GetMod = DistanzMod;
            mods.Add(DISTANZ_MOD, distanz);

            FernkampfModifikator<Trefferzone> trefferzone = new FernkampfModifikator<Trefferzone>(this);
            trefferzone.GetMod = TrefferzoneMod;
            mods.Add(TREFFERZONE_MOD, trefferzone);

            FernkampfModifikator<int> nahkampf = new FernkampfModifikator<int>(this);
            nahkampf.GetMod = NahkampfMod;
            mods.Add(NAHKAMPF_MOD, nahkampf);

            FernkampfModifikator<int> handgemenge = new FernkampfModifikator<int>(this);
            handgemenge.GetMod = HandgemengeMod;
            mods.Add(HANDGEMENGE_MOD, handgemenge);

            FernkampfModifikator<int> zielen = new FernkampfModifikator<int>(this);
            zielen.GetMod = ZielenMod;
        }

        private int SchützenIndex(IFernkampfwaffe waffe)
        {
            Held held = Ausführender.Kämpfer as Held;
            if (held != null)
            {
                //TODO: Talent beachten
                if (held.HatSonderfertigkeitUndVoraussetzungen("Meisterschütze ()"))
                    return 2;
                else if (held.HatSonderfertigkeitUndVoraussetzungen("Scharfschütze ()"))
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
            return mod;
        }

        private int DistanzMod(IFernkampfwaffe waffe, int distanz)
        {
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
                case Größe.Winzig:
                    return 8;
                case Größe.SehrKlein:
                    return 6;
                case Größe.Klein:
                    return 4;
                case Größe.Mittel:
                    return 2;
                case Größe.Groß:
                    return 0;
                case Größe.SehrGroß:
                    return -2;
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

        private int ZielenMod(IFernkampfwaffe waffe, int value)
        {
            if (value == 0)
                return new int[] { 2, 1, 0 }[SchützenIndex(waffe)];
            else return -Math.Min(4, (int)Math.Floor(new double[] { 0.5, 1, 1 }[SchützenIndex(waffe)] * (value - 1)));
        }

        #endregion

        protected override void Erfolg(IKämpfer ziel)
        {
            throw new NotImplementedException();
        }

        protected override IEnumerable<Probe> ProbenAnlegen()
        {
            throw new NotImplementedException();
        }
    }
}
