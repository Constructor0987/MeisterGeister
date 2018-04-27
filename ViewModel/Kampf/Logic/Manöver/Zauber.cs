using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeisterGeister.Logic.General;
using MeisterGeister.Model;
using System.ComponentModel;

namespace MeisterGeister.ViewModel.Kampf.Logic.Manöver
{
    public class Zauber : Manöver<IWaffe>
    {
        private Held_Zauber held_zauber;
        public Held_Zauber Held_Zauber
        {
            get
            {
                return held_zauber;
            }
        }
        private GegnerBase_Zauber gegnerBase_zauber;
        public GegnerBase_Zauber GegnerBase_Zauber
        {
            get
            {
                return gegnerBase_zauber;
            }
        }

        private int basisDauer = 1;
        public int BasisDauer
        {
            get { return basisDauer; }
            set
            {
                Set(ref basisDauer, value);
                calcDauer();
            }
        }
        private bool showName = false;
        public bool ShowName
        {
            get { return showName; }
            set
            {
                Set(ref showName, value);
                Name = "Zauber" + (value ? (": " + ((this.held_zauber != null) ? this.held_zauber.Zauber.Name : this.gegnerBase_zauber.Zauber.Name)) : "");
            }
        }

        private void calcDauer()
        {
            int dauer = BasisDauer + Math.Abs(wirkungsdauer.Value) + Math.Abs(reichweite.Value) + Math.Abs(erzwingen.Value) + (zielobjekt.Value ? 1 : 0) + (festeDauer.Value ? 1 : 0) + (technik.Value + technikZentral.Value) * 3;
            if (zauberdauer.Value <= 0)
                Dauer = dauer * (1 << -zauberdauer.Value);
            else
                Dauer = Math.Max(1, (int)Math.Round(dauer / (double)(1 << zauberdauer.Value), MidpointRounding.AwayFromZero));
            
            VerbleibendeDauer = Dauer;
        }

        public Zauber(KämpferInfo ausführender, Held_Zauber zauber) : base(ausführender, 1)
        {
            this.held_zauber = zauber;
            BasisDauer = zauber.Zauber.Zauberdauer == null? 1: zauber.Zauber.Zauberdauer.Value;
            Literatur = zauber.Zauber.Literatur;
        }

        public Zauber(KämpferInfo ausführender, GegnerBase_Zauber zauber)
            : base(ausführender, 1)
        {
            this.gegnerBase_zauber = zauber;
            BasisDauer = zauber.Zauber.Zauberdauer == null ? 1 : zauber.Zauber.Zauberdauer.Value;
            Literatur = zauber.Zauber.Literatur;
        }

        protected override void Init()
        {
            base.Init();
            Name = "Zauber";
            //TODO: Literatur
            Typ = ManöverTyp.Aktion;
        }

        #region Mods

        protected const string ZAUBER_DAUER_MOD = "Zauberdauer";
        protected const string WIRKUNGS_DAUER_MOD = "Wirkungsdauer";
        protected const string FESTE_DAUER_MOD = "FesteDauer";
        protected const string REICHWEITE_MOD = "Reichweite";
        protected const string ERZWINGEN_MOD = "Erzwingen";
        protected const string ZIELOBJEKT_MOD = "Zielobjekt";
        protected const string TECHNIK_MOD = "Technik";
        protected const string TECHNIK_ZENTRAL_MOD = "TechnikZentral";
        protected const string FALSCHEREPRÄSENTATION_MOD = "Technik";

        private ZauberModifikator<int> zauberdauer, wirkungsdauer, reichweite, erzwingen, technik, technikZentral, falscheRepräsenation;
        private ZauberModifikator<bool> festeDauer, zielobjekt;

        protected override void InitMods()
        {
            base.InitMods();

            zauberdauer = new ZauberModifikator<int>(this);
            zauberdauer.GetMod = ZauberDauerMod;
            mods.Add(ZAUBER_DAUER_MOD, zauberdauer);

            wirkungsdauer = new ZauberModifikator<int>(this);
            wirkungsdauer.GetMod = WirkungsDauerMod;
            mods.Add(WIRKUNGS_DAUER_MOD, wirkungsdauer);

            festeDauer = new ZauberModifikator<bool>(this);
            festeDauer.GetMod = FesteDauerMod;
            mods.Add(FESTE_DAUER_MOD, festeDauer);

            reichweite = new ZauberModifikator<int>(this);
            reichweite.GetMod = ReichweiteMod;
            mods.Add(REICHWEITE_MOD, reichweite);

            erzwingen = new ZauberModifikator<int>(this);
            erzwingen.GetMod = ErzwingenMod;
            mods.Add(ERZWINGEN_MOD, erzwingen);

            zielobjekt = new ZauberModifikator<bool>(this);
            zielobjekt.GetMod = ZielobjektMod;
            mods.Add(ZIELOBJEKT_MOD, zielobjekt);

            technik = new ZauberModifikator<int>(this);
            technik.GetMod = TechnikMod;
            mods.Add(TECHNIK_MOD, technik);

            technikZentral = new ZauberModifikator<int>(this);
            technikZentral.GetMod = TechnikZentralMod;
            mods.Add(TECHNIK_ZENTRAL_MOD, technikZentral);

            zauberdauer.PropertyChanged += mod_PropertyChanged;
            wirkungsdauer.PropertyChanged += mod_PropertyChanged;
            festeDauer.PropertyChanged += mod_PropertyChanged;
            reichweite.PropertyChanged += mod_PropertyChanged;
            erzwingen.PropertyChanged += mod_PropertyChanged;
            zielobjekt.PropertyChanged += mod_PropertyChanged;
            technik.PropertyChanged += mod_PropertyChanged;
            technikZentral.PropertyChanged += mod_PropertyChanged;
        }

        private void mod_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Value")
                calcDauer();
        }

        private int ZauberDauerMod(IWaffe waffe, int value)
        {
            if (value < 0)
                return -3;
            else return value * 5;
        }

        private int WirkungsDauerMod(IWaffe waffe, int value)
        {
            Held h = Ausführender.Kämpfer as Held;
            if (h != null && h.Rasse.ToLower().Contains("elf") && held_zauber.Probenname.Contains("Elf") )
                return value * (Math.Sign(value) == 1 ? 4 : -3);
            return value * (Math.Sign(value) == 1 ? 7 : -3);
        }

        private int FesteDauerMod(IWaffe waffe, bool value)
        {
            return value ? 7 : 0;
        }

        private int ReichweiteMod(IWaffe waffe, int value)
        {
            return value * (Math.Sign(value) == 1 ? 5 : -3);
        }

        private int ErzwingenMod(IWaffe waffe, int value)
        {
            //Kosten sparen
            if (value > 0)
                return 3 * value;
            else
                return value;
        }

        private int ZielobjektMod(IWaffe waffe, bool value)
        {
            if (value)
            {
                if (held_zauber != null)
                {
                    if (held_zauber.Zauber.Magieresistenz)
                        return 2;
                    else return 5;
                }
                else
                {
                    if (gegnerBase_zauber.Zauber.Magieresistenz)
                        return 2;
                    else return 5;
                }
            }
            return 0;
        }

        private int TechnikMod(IWaffe waffe, int value)
        {
            return value * 7;
        }

        private int TechnikZentralMod(IWaffe waffe, int value)
        {
            return value * 12;
        }



        #endregion

        protected override void Erfolg(Probe p, KämpferInfo ziel, IWaffe waffe, ManöverEventArgs e_init)
        {
            //TODO: Logeintrag o.Ä. schreiben damit der Meister weis was zu tun ist
            //Zauberwirkungen sind sehr vielfältig und werden wohl vorerst nicht von MG abgedeckt werden
        }
    }
}
