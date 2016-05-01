using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeisterGeister.Logic.General;
using MeisterGeister.Model;
using MeisterGeister.View.General;

namespace MeisterGeister.ViewModel.Kampf.Logic.Manöver
{
    public abstract class AngriffsManöver : NahkampfManöver
    {
        public AngriffsManöver(KämpferInfo ausführender)
            : base(ausführender)
        { }

        #region Mods

        public const string WUCHTSCHLAG_MOD = "Wuchtschlag";
        public const string FINTE_MOD = "Finte";
        public const string STUMPF_MOD = "Stumpf";
        public const string ÜBERRASCHT_MOD = "Überrascht";
        public const string PASSIERSCHLAG_MOD = "Passierschlag";

        protected override void InitMods()
        {
            base.InitMods();

            //----------ANSAGEN----------

            NahkampfModifikator<int> wuchtschlag = new NahkampfModifikator<int>(this);
            wuchtschlag.GetMod = WuchtschlagMod;
            mods.Add(WUCHTSCHLAG_MOD, wuchtschlag);

            NahkampfModifikator<int> finte = new NahkampfModifikator<int>(this);
            finte.GetMod = FinteMod;
            mods.Add(FINTE_MOD, finte);

            NahkampfModifikator<int> stumpf = new NahkampfModifikator<int>(this);
            mods.Add(STUMPF_MOD, stumpf);

            //----------POSITION UND SITUATION----------

            NahkampfModifikator<bool> passierschlag = new NahkampfModifikator<bool>(this);
            passierschlag.GetMod = PassierschlagMod;
            mods.Add(PASSIERSCHLAG_MOD, passierschlag);

            NahkampfModifikator<int> überrascht = new NahkampfModifikator<int>(this);
            mods.Add(ÜBERRASCHT_MOD, überrascht);
        }

        protected virtual int WuchtschlagMod(INahkampfwaffe waffe, int value)
        {
            Held held = Ausführender.Kämpfer as Held;
            if (held != null && held.HatSonderfertigkeitUndVoraussetzungen("Wuchtschlag"))
            {
                return value;
            }
            else return Math.Max(0, value * 2 - 1);
        }

        protected virtual int FinteMod(INahkampfwaffe waffe, int value)
        {
            Held held = Ausführender.Kämpfer as Held;
            if (held != null && held.HatSonderfertigkeitUndVoraussetzungen("Finte"))
            {
                return value;
            }
            else return Math.Max(0, value * 2 - 1);
        }

        protected virtual int PassierschlagMod(INahkampfwaffe waffe, bool value)
        {
            //TODO: Ini-Modifikator beachten, Aufmerksamkeit(+4), Kampfgespür(+2)
            if (value)
                return 4;
            else return 0;
        }

        protected override int GrößeMod(INahkampfwaffe waffe, Größe value)
        {
            switch (value)
            {
                case Größe.Winzig:
                    return 4;
                case Größe.SehrKlein:
                    return 2;
                default:
                    return 0;
            }
        }

        protected override int PositionGegnerMod(INahkampfwaffe waffe, Position value)
        {
            switch (value)
            {
                case Position.Liegend:
                    return -3;
                case Position.Kniend:
                    return -1;
                case Position.Fliegend:
                    return 2;
                default:
                    return 0;
            }
        }

        protected override int ÜberzahlMod(INahkampfwaffe waffe, int value)
        {
            return value > 0 ? -1 : 0;
        }

        protected override int UnbewaffnetMod(INahkampfwaffe waffe, bool value)
        {
            return value ? 1 : 0;
        }

        protected override int BeengtMod(INahkampfwaffe waffe, bool value)
        {
            if (value)
            {
                //Anderthalbhänder, Kettenwaffen, Peitschen, Stäbe, Zweihandflegel, Zweihandhiebwaffen und Zweihandschwerter/-säbel
                //return 6;

                //Hiebwaffen, Infanteriewaffen, Säbel und Schwerter
                //return 2;
            }
            return 0;
        }

        protected override int WasserMod(INahkampfwaffe waffe, Wassertiefe value)
        {
            int mod;
            switch (value)
            {
                case Wassertiefe.Hüfttief:
                    mod = 2;
                    break;
                case Wassertiefe.Schultertief:
                    mod = 4;
                    break;
                case Wassertiefe.UnterWasser:
                    mod = 6;
                    break;
                default:
                    mod = 0;
                    break;
            }
            return CheckWasserkampf(mod, value, Ausführender.Kämpfer as Held);
        }

        #endregion

        protected override void Init()
        {
            base.Init();
            Angriffsaktionen = 1;
            Typ = ManöverTyp.Aktion;
        }

        public override void Ausführen()
        {
            base.Ausführen();

            foreach (var wz in WaffeZiel)
            {
                Probe p = new Probe();
                p.Probenname = Name;
                p.Werte = new int[] { wz.Key.AT };
                p.WerteNamen = "AT";
                p.Modifikator = Mods.Values.Sum(mod => mod.Result);
                ViewHelper.ShowProbeDialog(p, Ausführender.Kämpfer as Held);

                ProbeAuswerten(p, wz.Value);
            }
        }

        protected override void Erfolg(Probe p, KämpferInfo ziel)
        {
            //TODO: Prüfen ob das Ziel pariert o.Ä.
            //Ansonsten Schaden machen


        }

        //TODO JT: Wenn AusdauerImKampf
        //Wenn Waffe schwerer als KK*10 Unzen
        // Ausführender.AusdauerAktuell--;
        //Wenn BE / 3 > 0
        // Ausführender.AusdauerAktuell-= BE/3;
    }
}
