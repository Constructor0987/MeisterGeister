using MeisterGeister.Logic.General;
using MeisterGeister.Model;
using MeisterGeister.Model.Extensions;
using MeisterGeister.ViewModel.Helden.Logic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MeisterGeister.ViewModel.Beschwörung
{
    public abstract class BeschwörungViewModel : Base.ViewModelBase
    {
        public const string MOD_SCHWIERIGKEIT = "Schwierigkeit";
        public const string MOD_NAME = "WahrerName";
        public const string MOD_MATERIAL = "Material";
        public const string MOD_AUSRÜSTUNG = "Ausrüstung";
        public const string MOD_BLUTMAGIE = "Blutmagie";
        public const string MOD_STERNE = "Sterne";
        public const string MOD_ORT = "Ort";
        public const string MOD_BEFEHL = "Befehl";
        public const string MOD_DAUER = "Dauer";
        public const string MOD_BEZAHLUNG = "Bezahlung";
        public const string MOD_DONARIA = "Donaria";
        public const string MOD_SONSTIGES = "Sonstiges";
        public const string MOD_BESCHWÖRUNGS_PUNKTE = "Beschwörungspunkte";

        private Dictionary<string, BeschwörungsModifikator> mods;
        public Dictionary<string, BeschwörungsModifikator> Mods
        {
            get { return mods; }
        }

        public BeschwörungViewModel()
            : base(View.General.ViewHelper.ShowProbeDialog)
        {
            initMods();

            //Verfügbare Wesen laden
            Wesen = loadWesen();
            //Commands initialisieren
            beschwören = new Base.CommandBase((o) => beschwöre(), (o) => beschwörungMöglich());
            beherrschen = new Base.CommandBase((o) => beherrsche(), (o) => beherrschungMöglich());
            resetCmd = new Base.CommandBase((o) => reset(), null);
            //Standardwerte setzen
            reset();
            PropertyChanged += onPropertyChanged;
        }

        protected BeschwörungsModifikator<int, int> schwierigkeit;
        protected BeschwörungsModifikator<int> name;
        protected BeschwörungsModifikator<int> material;
        protected BeschwörungsModifikator<int> ausrüstung;
        protected BeschwörungsModifikator<bool> blutmagie;
        protected BeschwörungsModifikator<int> sterne;
        protected BeschwörungsModifikator<int> ort;
        protected BeschwörungsModifikator<int> punkte;
        protected BeschwörungsModifikator<int> befehl;
        protected BeschwörungsModifikator<int> dauer;
        protected BeschwörungsModifikator<int> bezahlung;

        private void initMods()
        {
            mods = new Dictionary<string, BeschwörungsModifikator>();

            //Schwierigkeit wird einfach als Mod übernommen
            schwierigkeit = new BeschwörungsModifikator<int, int>();
            mods.Add(MOD_SCHWIERIGKEIT, schwierigkeit);

            //Wahrer Name wird komplett auf die Anrufung und zu 1/3 auf die Kontrolle angerechnet
            name = new BeschwörungsModifikator<int>();
            name.GetAnrufungsMod = () => -name.Value;
            name.GetKontrollMod = () => -div(name.Value, 3);
            mods.Add(MOD_NAME, name);

            //Materialqualität wirkt sich positiv auf die Anrufung aus
            material = new BeschwörungsModifikator<int>();
            material.GetAnrufungsMod = () => -material.Value;
            mods.Add(MOD_MATERIAL, material);

            //Ausrüstung wird für beide Proben einfach übernommen
            ausrüstung = new BeschwörungsModifikator<int>();
            ausrüstung.GetAnrufungsMod = ausrüstung.GetKontrollMod = () => ausrüstung.Value;
            mods.Add(MOD_AUSRÜSTUNG, ausrüstung);

            //Blutmagie erschwert die Kontrolle um 2
            blutmagie = new BeschwörungsModifikator<bool>();
            blutmagie.GetKontrollMod = () => blutmagie.Value ? 2 : 0;
            mods.Add(MOD_BLUTMAGIE, blutmagie);

            //Sternenkonstellation wirkt sich voll auf die Anrufung und zu 1/3 auf die Kontrolle aus
            sterne = new BeschwörungsModifikator<int>();
            sterne.GetAnrufungsMod = () => sterne.Value;
            sterne.GetKontrollMod = () => div(sterne.Value, 3);
            mods.Add(MOD_STERNE, sterne);

            //Umstände des Ortes wirken sich voll auf die Anrufung und zu 1/3 auf die Kontrolle aus
            ort = new BeschwörungsModifikator<int>();
            ort.GetAnrufungsMod = () => -ort.Value;
            ort.GetKontrollMod = () => -div(ort.Value, 3);
            mods.Add(MOD_ORT, ort);

            //Übrige Punkte aus der Anrufungsprobe wirken sich zu 1/3 günstig auf die Kontrolle aus.
            //Mehr als 7 Punkte gibt es trotzdem nicht
            punkte = new BeschwörungsModifikator<int>();
            punkte.GetKontrollMod = () => -Math.Min(div(punkte.Value, 3), 7);
            mods.Add(MOD_BESCHWÖRUNGS_PUNKTE, punkte);

            //Der Befehl wirkt sich auf die Kontrolle aus
            befehl = new BeschwörungsModifikator<int>();
            befehl.GetKontrollMod = () => befehl.Value;
            mods.Add(MOD_BEFEHL, befehl);

            //Die Befehlsdauer wirkt sich auf die Kontrolle aus
            dauer = new BeschwörungsModifikator<int>();
            dauer.GetKontrollMod = () => dauer.Value;
            mods.Add(MOD_DAUER, dauer);

            //Zusätzlich bezahlte AsP wirken sich auf die Kontrolle aus
            bezahlung = new BeschwörungsModifikator<int>();
            bezahlung.GetKontrollMod = () => bezahlung.Value;
            mods.Add(MOD_BEZAHLUNG, bezahlung);

            //Donaria werden einfach als Mod übernommen
            mods.Add(MOD_DONARIA, new BeschwörungsModifikator<int, int>());

            //Sonstiges wird auch einfach übernommen
            mods.Add(MOD_SONSTIGES, new BeschwörungsModifikator<int, int>());

            //Danach wird am Standardset, je nach Implementierung, noch irgendwas geändert
            addMods();

            //Wenn sich ein Modifikator ändert müssen wir die Summe updaten
            foreach (BeschwörungsModifikator m in mods.Values)
            {
                m.PropertyChanged += mod_PropertyChanged;
            }
        }

        protected abstract void addMods();

        protected int div(int a, int b)
        {
            return (int)Math.Round(a / (double)b, MidpointRounding.AwayFromZero);
        }

        private void mod_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "AnrufungsMod")
            {
                OnChanged("GesamtRufMod");
            }
            else if (e.PropertyName == "KontrollMod")
            {
                OnChanged("GesamtHerrschMod");
            }
            else if(e.PropertyName == "ZauberMod")
            {
                OnChanged("ZauberWert");
                OnChanged("KontrollWert");
            }
        }

        /// <summary>
        /// Lädt die Verfügbaren Wesen, welche beschworen werden können
        /// </summary>
        /// <returns></returns>
        protected abstract List<GegnerBase> loadWesen();

        private Base.CommandBase resetCmd;
        /// <summary>
        /// Setzt alle Eigenschaften zurück
        /// </summary>
        public Base.CommandBase Reset
        {
            get { return resetCmd; }
        }

        protected virtual void reset()
        {
            //Hier wird alle auf Standard gesetzt
            foreach (BeschwörungsModifikator mod in mods.Values)
            {
                mod.Reset();
            }
            //Held = null;
            //Zauber = null;
            Status = BeschwörungsStatus.Beschwören;

            //Hier werden Held und Wesen nochmal gesetzt um die Standardwerte zu laden
            Held = Held;
            BeschworenesWesen = BeschworenesWesen;
        }

        #region Proben

        private Base.CommandBase beschwören;
        /// <summary>
        /// Führt mit den aktuellen Einstellungen eine Beschwörungsprobe durch
        /// </summary>
        public Base.CommandBase Beschwören
        {
            get { return beschwören; }
        }

        private Base.CommandBase beherrschen;
        /// <summary>
        /// Führt mit den aktuellen Einstellungen eine Beherrschungsprobe durch
        /// </summary>
        public Base.CommandBase Beherrschen
        {
            get { return beherrschen; }
        }

        protected virtual bool beschwörungMöglich()
        {
            return Status == BeschwörungsStatus.Beschwören;
        }

        private void beschwöre()
        {
            if (zauber != null)
            {
                //Der entsprechende Zauber wird auf den richtigen Wert gesetzt und für die Probe modifiziert
                //Der Fertigkeitswert ist wichtig weil er sich z.B. durch InvocatioIntegra erhöhen kann
                zauber.Fertigkeitswert = ZauberWert;
                zauber.Modifikator = GesamtRufMod;
                var erg = ShowProbeDialog(zauber, Held);
                if (erg == null)
                    return;
                if (!erg.Gelungen)
                {
                    //Falls es nicht geklappt hat regelt u.U. die konkrete Klasse wie es weitergeht
                    beschwörungMisslungen(erg);
                }
                else
                {
                    //Falls es klappt freuen wir und über evtl. übrige Punkte und machen mit der Beherrschung weiter
                    punkte.Value = erg.Übrig;
                    Status = BeschwörungsStatus.Beherrschen;
                }
            }
        }

        /// <summary>
        /// Wird aufgerufen wenn die Beschwörung misslingt
        /// </summary>
        /// <param name="erg"></param>
        protected virtual void beschwörungMisslungen(ProbenErgebnis erg)
        {
            Status = BeschwörungsStatus.BeschwörungMisslungen;
        }

        protected virtual bool beherrschungMöglich()
        {
            return Status == BeschwörungsStatus.Beherrschen;
        }

        private void beherrsche()
        {
            if (zauber != null)
            {
                //Hier wird eine Eigenschaftsprobe auf den Kontrollwert abgelegt
                Eigenschaft kontrollwert = new Eigenschaft("Kontrollwert");
                kontrollwert.Abkürzung = "KW";
                kontrollwert.Fertigkeitswert = 0;
                //Der Kontrollwert berechnet sich je nach beschworener Wesenheit anders
                kontrollwert.Wert = KontrollWert;
                kontrollwert.WerteNamen = "Kontrollwert";
                kontrollwert.Modifikator = GesamtHerrschMod;
                var erg = ShowProbeDialog(kontrollwert, Held);
                if (erg.Gelungen)
                {
                    //Wenn klappt freuen wird uns und sind fertig
                    Status = BeschwörungsStatus.BeherrschungGelungen;
                }
                else
                {
                    //Falls es nicht klappt regelt die konkrete Implementierung was nun passieren soll
                    //Im einfachsten Fall passiert gar nichts
                    beherrschungMisslungen(erg);
                }
            }
        }

        /// <summary>
        /// Wird aufgerufen wenn die Beherrschungsprobe nicht geklappt hat
        /// </summary>
        /// <param name="erg"></param>
        protected virtual void beherrschungMisslungen(ProbenErgebnis erg)
        {
            Status = BeschwörungsStatus.BeherrschungMisslungen;
        }

        #endregion

        #region Properties

        private GegnerBase beschworenesWesen;
        public GegnerBase BeschworenesWesen
        {
            get { return beschworenesWesen; }
            set
            {
                Set(ref beschworenesWesen, value);
                if (beschworenesWesen != null)
                {
                    ((BeschwörungsModifikator<int, int>)mods[MOD_SCHWIERIGKEIT]).Value1 = beschworenesWesen.Beschwörung ?? 0;
                    ((BeschwörungsModifikator<int, int>)mods[MOD_SCHWIERIGKEIT]).Value2 = beschworenesWesen.Kontrolle ?? 0;
                }
            }
        }

        public override void RegisterEvents()
        {
            base.RegisterEvents();
            Global.HeldSelectionChanged += Global_HeldSelectionChanged;
            OnChanged("Held");
        }
        public override void UnregisterEvents()
        {
            base.UnregisterEvents();
            Global.HeldSelectionChanged -= Global_HeldSelectionChanged;
        }

        private void Global_HeldSelectionChanged(object sender, EventArgs e)
        {
            OnChanged("Held");
        }

        private void onPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Held")
            {
                getZauber();
                checkHeld();
            }
        }

        public virtual Held Held
        {
            get { return Global.SelectedHeld; }
            set
            {
                Global.SelectedHeld = value;
                OnChanged();
            }
        }

        protected virtual void checkHeld()
        {
        }

        private bool? beschwörungGelungen = null;
        public bool? BeschwörungGelungen
        {
            get { return beschwörungGelungen; }
            set { Set(ref beschwörungGelungen, value); }
        }

        private bool? beherrschungGelungen = null;
        public bool? BeherrschungGelungen
        {
            get { return beherrschungGelungen; }
            set { Set(ref beherrschungGelungen, value); }
        }

        private BeschwörungsStatus status;
        public virtual BeschwörungsStatus Status
        {
            get { return status; }
            protected set
            {
                switch (value)
                {
                    case BeschwörungsStatus.Beschwören:
                        BeschwörungGelungen = BeherrschungGelungen = null;
                        break;
                    case BeschwörungsStatus.BeschwörungMisslungen:
                        BeschwörungGelungen = false;
                        break;
                    case BeschwörungsStatus.Beherrschen:
                        if (status == BeschwörungsStatus.Beschwören)
                            BeschwörungGelungen = true;
                        break;
                    case BeschwörungsStatus.BeherrschungMisslungen:
                        BeherrschungGelungen = false;
                        break;
                    case BeschwörungsStatus.BeherrschungGelungen:
                        BeherrschungGelungen = true;
                        break;
                }
                Set(ref status, value);
                Beschwören.Invalidate();
                Beherrschen.Invalidate();
            }
        }

        private Model.Zauber zauber;
        private string zauberName;
        public string Zauber
        {
            get { return zauberName; }
            set
            {
                Set(ref zauberName, value);
                getZauber();
            }
        }

        private void getZauber()
        {
            if (Held != null && !String.IsNullOrEmpty(Zauber))
            {
                int zfw = 0;
                Held_Zauber hz = Held.GetHeldZauber(zauberName, false, out zfw, false);
                if (hz != null)
                {
                    zauber = hz.Zauber;
                    zauberBasisWert = zfw;
                }
                else
                {
                    zauberBasisWert = 0;
                    zauber = null;
                }
            }
            else
            {
                zauberBasisWert = 0;
                zauber = null;
            }
            OnChanged("ZauberWert");
            OnChanged("KontrollWert");
        }

        private int zauberBasisWert;
        public int ZauberWert
        {
            get { return zauberBasisWert + Mods.Values.Sum((mod) => mod.ZauberMod); }
        }

        public int KontrollWert
        {
            get
            {
                if (zauber == null) return 0;
                else return calcKontrollWert();
            }
        }

        protected abstract int calcKontrollWert();

        public abstract string KontrollFormel
        {
            get;
        }

        private List<GegnerBase> wesen;
        public List<GegnerBase> Wesen
        {
            get { return wesen; }
            private set { Set(ref wesen, value); }
        }

        /// <summary>
        /// Probenerschwernis uaf die Beschwörungsprobe
        /// </summary>
        public int GesamtRufMod
        {
            get
            {
                return mods.Values.Sum((mod) => mod.AnrufungsMod);
            }
        }

        /// <summary>
        /// Probenerschwernis auf die Beherrschungsprobe
        /// </summary>
        public int GesamtHerrschMod
        {
            get
            {
                return mods.Values.Sum((mod) => mod.KontrollMod);
            }
        }

        /// <summary>
        /// Gesamtkosten der Beschwörung
        /// </summary>
        public virtual int GesamtAstralKosten
        {
            get
            {
                return mods.Values.Sum((mod) => mod.KostenMod);
            }
        }

        #endregion
    }

    public enum BeschwörungsStatus
    {
        Beschwören,
        BeschwörungMisslungen,
        Beherrschen,
        BeherrschungMisslungen,
        BeherrschungGelungen
    }
}
