using MeisterGeister.Logic.General;
using MeisterGeister.Model;
using MeisterGeister.Model.Extensions;
using MeisterGeister.ViewModel.Helden.Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MeisterGeister.ViewModel.Beschwörung
{
    public abstract class BeschwörungViewModel : Base.ViewModelBase
    {
        public BeschwörungViewModel()
            : base(View.General.ViewHelper.ShowProbeDialog)
        {
            //Verfügbare Wesen laden
            Wesen = loadWesen();
            //Commands initialisieren
            beschwören = new Base.CommandBase((o) => beschwöre(), (o) => beschwörungMöglich());
            beherrschen = new Base.CommandBase((o) => beherrsche(), (o)=> beherrschungMöglich());
            resetCmd = new Base.CommandBase((o) => reset(), null);
            //Standardwerte setzen
            reset();
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
            Beschwörungsschwierigkeit = Kontrollschwierigkeit = 0;
            Beschwörungspunkte = 0;
            WahrerName = 0;
            Material = 0;
            BefehlHerrschMod = 0;
            DauerHerrschMod = 0;
            AusrüstungMod = 0;
            Blutmagie = false;
            Sterne = 0;
            Ort = 0;
            DonariaRuf = DonariaHerrsch = 0;
            BezahlungHerrschMod = 0;
            SonstigesRufMod = SonstigesHerrschMod = 0;
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
                var erg = ShowProbeDialog(zauber, held);
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
                    Beschwörungspunkte = erg.Übrig;
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
                var erg = ShowProbeDialog(kontrollwert, held);
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
                    Beschwörungsschwierigkeit = beschworenesWesen.Beschwörung ?? 0;
                    Kontrollschwierigkeit = beschworenesWesen.Kontrolle ?? 0;
                }
            }
        }

        private Held held;
        public virtual Held Held
        {
            get { return held; }
            set
            {
                Set(ref held, value);
                getZauber();
                checkHeld();
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
        public virtual int ZauberWert
        {
            get { return zauberBasisWert; }
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


        private int beschwörungsschwierigkeit;
        public int Beschwörungsschwierigkeit
        {
            get { return beschwörungsschwierigkeit; }
            set
            {
                Set(ref beschwörungsschwierigkeit, value);
                OnChangedSum();
            }
        }

        private int kontrollschwierigkeit;
        public int Kontrollschwierigkeit
        {
            get { return kontrollschwierigkeit; }
            set
            {
                Set(ref kontrollschwierigkeit, value);
                OnChangedSum();
            }
        }

        private int wahrerName;
        public int WahrerName
        {
            get { return wahrerName; }
            set
            {
                wahrerName = value;
                OnInputChanged();
            }
        }

        private int material;
        public int Material
        {
            get { return material; }
            set
            {
                material = value;
                OnInputChanged();
            }
        }

        public int MaterialRufMod
        {
            get { return -Material; }
        }

        private bool blutmagie = false;
        public virtual bool Blutmagie
        {
            get { return blutmagie; }
            set
            {
                blutmagie = value;
                OnInputChanged();
            }
        }


        private int sterne;
        public int Sterne
        {
            get { return sterne; }
            set
            {
                sterne = value;
                OnInputChanged();
            }
        }

        private int ort;
        public int Ort
        {
            get { return ort; }
            set
            {
                ort = value;
                OnInputChanged();
            }
        }

        private int donariaRuf;
        public int DonariaRuf
        {
            get { return donariaRuf; }
            set
            {
                Set(ref donariaRuf, value);
                OnChangedSum();
            }
        }

        private int donariaHerrsch;
        public int DonariaHerrsch
        {
            get { return donariaHerrsch; }
            set
            {
                Set(ref donariaHerrsch, value);
                OnChangedSum();
            }
        }

        private int befehlHerrschMod;
        public int BefehlHerrschMod
        {
            get { return befehlHerrschMod; }
            set
            {
                Set(ref befehlHerrschMod, value);
                OnChangedSum();
            }
        }

        private int dauerHerrschMod;
        public int DauerHerrschMod
        {
            get { return dauerHerrschMod; }
            set
            {
                Set(ref dauerHerrschMod, value);
                OnChangedSum();
            }
        }

        private int sonstigesRufMod;
        public int SonstigesRufMod
        {
            get { return sonstigesRufMod; }
            set
            {
                Set(ref sonstigesRufMod, value);
                OnChangedSum();
            }
        }

        private int sonstigesHerrschMod;
        public int SonstigesHerrschMod
        {
            get { return sonstigesHerrschMod; }
            set
            {
                Set(ref sonstigesHerrschMod, value);
                OnChangedSum();
            }
        }

        private int bezahlungHerrschMod;
        public int BezahlungHerrschMod
        {
            get { return bezahlungHerrschMod; }
            set
            {
                Set(ref bezahlungHerrschMod, value);
                OnChangedSum();
            }
        }

        private int ausrüstungMod;
        public int AusrüstungMod
        {
            get { return ausrüstungMod; }
            set
            {
                Set(ref ausrüstungMod, value);
                OnChangedSum();
            }
        }

        private int beschwörungspunkte;
        public int Beschwörungspunkte
        {
            get { return beschwörungspunkte; }
            set
            {
                Set(ref beschwörungspunkte, value);
                OnChanged("Beschwörungsbonus");
                OnChangedSum();
            }
        }

        private List<GegnerBase> wesen;
        public List<GegnerBase> Wesen
        {
            get { return wesen; }
            private set { Set(ref wesen, value); }
        }

        public int Beschwörungsbonus
        {
            get { return -Math.Min(Beschwörungspunkte / 3, 7); }
        }


        public virtual int WahrerNameRufMod
        {
            get { return -WahrerName; }
        }

        public int WahrerNameHerrschMod
        {
            get { return (int)Math.Round(-WahrerName / 3.0, MidpointRounding.AwayFromZero); }
        }

        public int SterneRufMod
        {
            get { return Sterne; }
        }
        public int SterneHerrschMod
        {
            get { return (int)Math.Round(Sterne / 3.0, MidpointRounding.AwayFromZero); }
        }

        public int OrtRufMod
        {
            get { return -Ort; }
        }
        public int OrtHerrschMod
        {
            get { return (int)Math.Round(-Ort / 3.0, MidpointRounding.AwayFromZero); }
        }

        public virtual int BlutmagieHerrschMod
        {
            get { return Blutmagie ? 2 : 0; }
        }

        /// <summary>
        /// Probenerschwernis uaf die Beschwörungsprobe
        /// </summary>
        public virtual int GesamtRufMod
        {
            get
            {
                return Beschwörungsschwierigkeit
                    + WahrerNameRufMod
                    + MaterialRufMod
                    + AusrüstungMod
                    + SterneRufMod
                    + OrtRufMod
                    + DonariaRuf
                    + SonstigesRufMod;
            }
        }

        /// <summary>
        /// Probenerschwernis auf die Beherrschungsprobe
        /// </summary>
        public virtual int GesamtHerrschMod
        {
            get
            {
                return Kontrollschwierigkeit
                    + WahrerNameHerrschMod
                    + Beschwörungsbonus
                    + BefehlHerrschMod
                    + DauerHerrschMod
                    + AusrüstungMod
                    + BlutmagieHerrschMod
                    + SterneHerrschMod
                    + OrtHerrschMod
                    + DonariaHerrsch
                    + BezahlungHerrschMod
                    + SonstigesHerrschMod;
            }
        }

        /// <summary>
        /// Gesamtkosten der Beschwörung
        /// </summary>
        public virtual int GesamtAstralKosten
        {
            get
            {
                return 0;
            }
        }

        #endregion

        #region Helfer

        protected void OnInputChanged([CallerMemberName]string propertyName = null)
        {
            base.OnChanged(propertyName);
            base.OnChanged(propertyName + "RufMod");
            base.OnChanged(propertyName + "HerrschMod");
            OnChangedSum();
        }

        protected void OnChangedSum()
        {
            OnChanged("GesamtRufMod");
            OnChanged("GesamtHerrschMod");
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
