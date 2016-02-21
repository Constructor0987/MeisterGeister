using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeisterGeister.Logic.General;
using MeisterGeister.Model;

namespace MeisterGeister.ViewModel.Kampf.Logic.Manöver
{
    public abstract class AngriffsManöver : NahkampfManöver
    {
        public AngriffsManöver(KämpferInfo ausführender)
            : base(ausführender)
        { }

        public AngriffsManöver(KämpferInfo ausführender, IDictionary<INahkampfwaffe, KämpferInfo> waffe_ziel)
            : base(ausführender, waffe_ziel)
        { }

        public AngriffsManöver(KämpferInfo ausführender, INahkampfwaffe waffe, KämpferInfo ziel)
            : base(ausführender, waffe, ziel)
        { }

        public const string LICHT_MOD = "Licht";
        public const string WASSER_MOD = "Wasser";
        public const string UMGEBUNG_MOD = "Umgebung";
        public const string DISTANZLASSE_MOD = "Distanzklasse";
        public const string WUCHTSCHLAG_MOD = "Wuchtschlag";
        public const string FINTE_MOD = "Finte";
        public const string STUMPF_MOD = "Stumpf";
        public const string POS_SELBST_MOD = "PositionSelbst";
        public const string POS_ZIEL_MOD = "PositionZiel";
        public const string ÜBERRASCHT_MOD = "Überrascht";
        public const string ÜBERZAHL_MOD = "Überzahl";
        public const string FALSCHE_HAND_MOD = "FalscheHand";
        public const string PASSIERSCHLAG_MOD = "Passierschlag";
        public const string GRÖSSE_MOD = "Zielgröße";
        public const string UNBEWAFFNET_MOD = "Unbewaffnet";

        protected override void InitMods()
        {
            base.InitMods();

            //----------UMGEBUNG----------

            NahkampfModifikator<int> licht = new NahkampfModifikator<int>(this);
            licht.GetMod = (waffe) =>
            {
                int mod = licht.Value;
                Held held = Ausführender.Kämpfer as Held;
                if (held != null)
                {
                    if (held.HatVorNachteil("Dämmerungssicht") || held.HatVorNachteil("Nachtsicht"))
                        mod = (int)Math.Round(mod * 0.5, MidpointRounding.AwayFromZero);
                    if (held.HatVorNachteil("Nachticht") ||
                       //TODO: keine Peitsche/Lanzenreiten und keine Pikenreichweite um Blindkampf zu nutzen
                       (held.HatSonderfertigkeitUndVoraussetzungen("Blindkampf") && (waffe.Distanzklasse&Distanzklasse.HNS) != Distanzklasse.None))
                        mod = Math.Min(mod, 2);
                    if (held.HatVorNachteil("Nachtblind"))
                        mod *= 2;
                }
                return Math.Min(8, mod);
            };
            mods.Add(LICHT_MOD, licht);

            NahkampfModifikator<int> wasser = new NahkampfModifikator<int>(this);
            wasser.GetMod = (waffe) =>
            {
                int mod = wasser.Value;
                Held held = Ausführender.Kämpfer as Held;
                if (held != null)
                {
                    //TODO: Die SF kann in schultertiefem Wasser nur mit den Talenten Dolche, Infanteriewaffen und Speere genutzt werden, ansonsten mit allen Nahkampffertigkeiten außer Peitsche und Lanzenreiten.
                    if (held.HatSonderfertigkeitUndVoraussetzungen("Kampf im Wasser"))
                        mod = (int)Math.Round(mod * 0.5, MidpointRounding.AwayFromZero);
                    //TODO: Kann nur mit den Nahkampf-Talenten Dolche, Fechtwaffen, Raufen, Ringen und Speere eingesetzt werden.
                    if (mod == 6 && held.HatSonderfertigkeitUndVoraussetzungen("Unterwasserkampf"))
                        mod = 0;
                }
                return mod;
            };
            mods.Add(WASSER_MOD, wasser);

            //Anderthalbhänder, Kettenwaffen, Peitschen, Stäbe, Zweihandflegel, Zweihandhiebwaffen und Zweihandschwerter/-säbel
            //Hiebwaffen, Infanteriewaffen, Säbel und Schwerter
            NahkampfModifikator<int> umgebung = new NahkampfModifikator<int>(this);
            mods.Add(UMGEBUNG_MOD, umgebung);

            NahkampfModifikator<int> distanzklasse = new NahkampfModifikator<int>(this);
            mods.Add(DISTANZLASSE_MOD, distanzklasse);

            //----------ANSAGEN----------

            NahkampfModifikator<int> wuchtschlag = new NahkampfModifikator<int>(this);
            mods.Add(WUCHTSCHLAG_MOD, wuchtschlag);

            NahkampfModifikator<int> finte = new NahkampfModifikator<int>(this);
            mods.Add(FINTE_MOD, finte);

            NahkampfModifikator<int> stumpf = new NahkampfModifikator<int>(this);
            mods.Add(STUMPF_MOD, stumpf);

            //----------POSITION UND SITUATION----------

            NahkampfModifikator<Position> position_selbst = new NahkampfModifikator<Position>(this);
            position_selbst.GetMod = (waffe) =>
            {
                switch (position_selbst.Value)
                {
                    case Position.Liegend:
                        return 3;
                    case Position.Kniend:
                        return 1;
                    default:
                        return 0;
                }
            };
            mods.Add(POS_SELBST_MOD, position_selbst);

            NahkampfModifikator<Position> position_ziel = new NahkampfModifikator<Position>(this);
            position_ziel.GetMod = (waffe) =>
            {
                switch (position_ziel.Value)
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
            };
            mods.Add(POS_ZIEL_MOD, position_ziel);

            NahkampfModifikator<bool> passierschlag = new NahkampfModifikator<bool>(this);
            passierschlag.GetMod = (waffe) =>
            {
                //TODO: Ini-Modifikator beachten, Aufmerksamkeit
                if (passierschlag.Value)
                    return 4;
                else return 0;
            };
            mods.Add(PASSIERSCHLAG_MOD, passierschlag);

            NahkampfModifikator<int> überzahl = new NahkampfModifikator<int>(this);
            überzahl.GetMod = (waffe) =>
            {
                return überzahl.Value > 0 ? -1 : 0;
            };
            mods.Add(ÜBERZAHL_MOD, überzahl);

            NahkampfModifikator<int> überrascht = new NahkampfModifikator<int>(this);
            mods.Add(ÜBERRASCHT_MOD, überrascht);

            NahkampfModifikator<Größe> größe = new NahkampfModifikator<Größe>(this);
            größe.Value = Größe.Mittel;
            größe.GetMod = (waffe) =>
            {
                switch (größe.Value)
                {
                    case Größe.Winzig:
                        return 4;
                    case Größe.SehrKlein:
                        return 2;
                    default:
                        return 0;
                }
            };
            mods.Add(GRÖSSE_MOD, größe);

            NahkampfModifikator<bool> falscheHand = new NahkampfModifikator<bool>(this);
            falscheHand.GetMod = (waffe) =>
            {
                if (falscheHand.Value)
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
            };
            mods.Add(FALSCHE_HAND_MOD, falscheHand);

            NahkampfModifikator<bool> unbewaffnet = new NahkampfModifikator<bool>(this);
            unbewaffnet.GetMod = (waffe) => unbewaffnet.Value ? -1 : 0;
            mods.Add(UNBEWAFFNET_MOD, unbewaffnet);
        }

        protected override void Init()
        {
            base.Init();
            Angriffsaktionen = 1;
        }

        protected override IEnumerable<Probe> ProbenAnlegen()
        {
            Probe p = new Probe();
            p.Probenname = Name;
            p.Werte = new int[] { Ausführender.Kämpfer.AT ?? 0 };
            p.WerteNamen = "AT";
            p.Modifikator = mods.Values.Sum(mod => mod.Result);
            yield return p;
        }

        //TODO JT: Wenn AusdauerImKampf
        //Wenn Waffe schwerer als KK*10 Unzen
        // Ausführender.AusdauerAktuell--;
        //Wenn BE / 3 > 0
        // Ausführender.AusdauerAktuell-= BE/3;
    }
}
