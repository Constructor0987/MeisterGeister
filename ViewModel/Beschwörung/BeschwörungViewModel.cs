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
            Wesen = loadWesen();
            beschwören = new Base.CommandBase(beschwöre, null);
            beherrschen = new Base.CommandBase(beherrsche, null);
            resetCmd = new Base.CommandBase((o) => reset(), null);
            reset();
        }

        protected abstract List<GegnerBase> loadWesen();

        private Base.CommandBase resetCmd;
        public Base.CommandBase Reset
        {
            get { return resetCmd; }
        }

        protected virtual void reset()
        {

            Beschwörungsschwierigkeit = Kontrollschwierigkeit = 0;
            Beschwörungspunkte = 0;
            WahrerName = 0;
            Material = 0;
            BefehlHerrschMod = 0;
            DauerHerrschMod = 0;
            AusrüstungMod = 0;
            Blutmagie = Blutmagie.Keine;
            Sterne = 0;
            Ort = 0;
            DonariaRuf = DonariaHerrsch = 0;
            BezahlungHerrschMod = 0;
            SonstigesRufMod = SonstigesHerrschMod = 0;
            BeschworenesWesen = null;
            held = null;
            zauber = null;
            Status = BeschwörungsStatus.Beschwören;
        }

        #region Proben

        private Base.CommandBase beschwören;
        public Base.CommandBase Beschwören
        {
            get { return beschwören; }
        }

        private Base.CommandBase beherrschen;
        public Base.CommandBase Beherrschen
        {
            get { return beherrschen; }
        }

        private Held held;
        public virtual Held Held
        {
            get { return held; }
            set
            {
                Set(ref held, value);
                getZauber();
            }
        }

        private Model.Zauber zauber;

        private void beschwöre(object obj)
        {
            if (zauber != null)
            {
                zauber.Fertigkeitswert = ZauberWert;
                zauber.Modifikator = GesamtRufMod;
                var erg = ShowProbeDialog(zauber, held);
                if (erg == null)
                    return;
                if (!erg.Gelungen)
                {
                    beschwörungMisslungen(erg);
                }
                else
                {
                    Beschwörungspunkte = erg.Übrig;
                    Status = BeschwörungsStatus.Beherrschen;
                    Ergebnis = "Das Wesen erscheint. Versuche es zu beherrschen.";
                }
            }
        }

        protected abstract void beschwörungMisslungen(ProbenErgebnis erg);

        private void beherrsche(object obj)
        {
            if (zauber != null)
            {
                Eigenschaft kontrollwert = new Eigenschaft("Kontrollwert");
                kontrollwert.Abkürzung = "KW";
                kontrollwert.Fertigkeitswert = 0;
                kontrollwert.Wert = KontrollWert;
                kontrollwert.WerteNamen = "Kontrollwert";
                kontrollwert.Modifikator = GesamtHerrschMod;
                var erg = ShowProbeDialog(kontrollwert, held);
                if (erg.Gelungen)
                {
                    Ergebnis = "Das Wesen erfüllt den Dienst";
                }
                else
                {
                    beherrschungMisslungen(erg);
                }
                Status = BeschwörungsStatus.Beschwören;
            }
        }

        protected abstract void beherrschungMisslungen(ProbenErgebnis erg);

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

        private BeschwörungsStatus status;
        public BeschwörungsStatus Status
        {
            get { return status; }
            protected set { Set(ref status, value); }
        }


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

        private Blutmagie blutmagie = Blutmagie.Keine;
        public virtual Blutmagie Blutmagie
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
            get { return Ort; }
        }
        public int OrtHerrschMod
        {
            get { return (int)Math.Round(Ort / 3.0, MidpointRounding.AwayFromZero); }
        }

        public virtual int BlutmagieHerrschMod
        {
            get { return Blutmagie == Blutmagie.Keine ? 0 : 2; }
        }

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

        private string ergebnis;
        public string Ergebnis
        {
            get { return ergebnis; }
            protected set { Set(ref ergebnis, value); }
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
        Beherrschen
    }

    public enum Blutmagie
    {
        [Description("Keine")]
        Keine,
        [Description("Tieropfer")]
        Tieropfer,
        [Description("Opferung eines intelligenten Wesens")]
        IntelligentesWesen
    }
}
