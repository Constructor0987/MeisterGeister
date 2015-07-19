using MeisterGeister.Model;
using MeisterGeister.ViewModel.Helden.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MeisterGeister.ViewModel.Beschwörung
{
    public class BeschwörungViewModel : Base.ViewModelBase
    {
        public BeschwörungViewModel() : base(View.General.ViewHelper.ShowProbeDialog)
        {
            DatenHolen();
            
        }

        private void DatenHolen()
        {
            DämonenListe = Global.ContextHeld.LoadDämonen();
            OnProbeWürfeln = new Base.CommandBase(ProbeWürfeln, null);
        }

        List<GegnerBase> dämonenListe;

        private Base.CommandBase onProbeWürfeln;
        public Base.CommandBase OnProbeWürfeln
        {
            get { return onProbeWürfeln; }
            set { onProbeWürfeln = value; }
        }

        private void ProbeWürfeln(object obj)
        {
            int zfw = 0;
            Held_Zauber hz = Global.SelectedHeld.GetHeldZauber("Invocatio minor", false, out zfw, false);
            if (hz == null)
                return;
            Held h = hz.Held;
            Model.Zauber z = hz.Zauber;
            z.Fertigkeitswert = zfw;
            var ergebnis = ShowProbeDialog(z, hz.Held);
            if (ergebnis != null && ergebnis.Gelungen)
            {
                int hörner = View.General.ViewHelper.ShowWürfelDialog("2W6", "Würfele die Anzahl der Hörner");
                //MeisterGeister.Logic.General.Würfel.Parse("2W6", true);
            }
            Eigenschaft kontrollwert = new Eigenschaft("Kontrollwert");
            kontrollwert.Abkürzung = "KW";
            kontrollwert.Fertigkeitswert = 0;
            kontrollwert.Wert = (int)Math.Round(h.Mut + zfw / 5.0, MidpointRounding.AwayFromZero);
            kontrollwert.WerteNamen = "Kontrollwert";
            kontrollwert.Modifikator = +8;
            var kwergebnis = ShowProbeDialog(kontrollwert, h);
        }

        private List<GegnerBase> DämonenListe
        {
            get { return dämonenListe; }
            set { Set(ref dämonenListe, value); }
        }


        private void OnInputChanged([CallerMemberName]string propertyName = null)
        {
            base.OnChanged(propertyName);
            base.OnChanged(propertyName + "RufMod");
            base.OnChanged(propertyName + "HerrschMod");
            OnChangedSum();
        }

        private void OnChangedSum()
        {
            OnChanged("GesamtRufMod");
            OnChanged("GesamtHerrschMod");
        }

        #region Eingabe

        private int beschwörungsschwierigkeit;
        public int Beschwörungsschwierigkeit
        {
            get { return beschwörungsschwierigkeit; }
            set
            {
                beschwörungsschwierigkeit = value;
                OnChanged();
                OnChangedSum();
            }
        }

        private int kontrollschwierigkeit;
        public int Kontrollschwierigkeit
        {
            get { return kontrollschwierigkeit; }
            set
            {
                kontrollschwierigkeit = value;
                OnChanged();
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

        private int hörner = 0;
        public int Hörner
        {
            get { return hörner; }
            set
            {
                hörner = value;
                OnChanged();
            }
        }

        private bool bannschwert;
        public bool Bannschwert
        {
            get { return bannschwert; }
            set
            {
                bannschwert = value;
                OnInputChanged();
            }
        }

        private bool affinität;
        public bool Affinität
        {
            get { return affinität; }
            set
            {
                affinität = value;
                OnInputChanged();
            }
        }

        private bool blutmagie;
        public bool Blutmagie
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
                donariaRuf = value;
                OnChanged();
                OnChangedSum();
            }
        }

        private int donariaHerrsch;
        public int DonariaHerrsch
        {
            get { return donariaHerrsch; }
            set
            {
                donariaHerrsch = value;
                OnChanged();
                OnChangedSum();
            }
        }

        private int befehlHerrschMod;
        public int BefehlHerrschMod
        {
            get { return befehlHerrschMod; }
            set
            {
                befehlHerrschMod = value;
                OnChanged();
                OnChangedSum();
            }
        }

        private int dauerHerrschMod;
        public int DauerHerrschMod
        {
            get { return dauerHerrschMod; }
            set
            {
                dauerHerrschMod = value;
                OnChanged();
                OnChangedSum();
            }
        }

        private int sonstigesRufMod;
        public int SonstigesRufMod
        {
            get { return sonstigesRufMod; }
            set
            {
                sonstigesRufMod = value;
                OnChanged();
                OnChangedSum();
            }
        }

        private int sonstigesHerrschMod;
        public int SonstigesHerrschMod
        {
            get { return sonstigesHerrschMod; }
            set
            {
                sonstigesHerrschMod = value;
                OnChanged();
                OnChangedSum();
            }
        }

        private int bezahlungHerrschMod;
        public int BezahlungHerrschMod
        {
            get { return bezahlungHerrschMod; }
            set
            {
                bezahlungHerrschMod = value;
                OnChanged();
                OnChangedSum();
            }
        }

        private int ausrüstungMod;
        public int AusrüstungMod
        {
            get { return ausrüstungMod; }
            set
            {
                ausrüstungMod = value;
                OnChanged();
                OnChangedSum();
            }
        }



        #endregion

        #region Ausgabe

        public int WahrerNameRufMod
        {
            get { return WahrerName == 0 ? 7 : -WahrerName; }
        }

        public int WahrerNameHerrschMod
        {
            get { return (int)Math.Round(-WahrerName / 3.0); }
        }

        public int BannschwertRufMod
        {
            get { return Bannschwert ? -1 : 0; }
        }
        public int BannschwertHerrschMod
        {
            get { return Bannschwert ? -1 : 0; }
        }

        public int SterneRufMod
        {
            get { return Sterne; }
        }
        public int SterneHerrschMod
        {
            get { return (int)Math.Round(Sterne / 3.0); }
        }

        public int OrtRufMod
        {
            get { return Ort; }
        }
        public int OrtHerrschMod
        {
            get { return (int)Math.Round(Ort / 3.0); }
        }

        public int AffinitätHerrschMod
        {
            get { return Affinität ? -3 : 0; }
        }
        public int BlutmagieHerrschMod
        {
            get { return Blutmagie ? 2 : 0; }
        }

        public int GesamtRufMod
        {
            get
            {
                return Beschwörungsschwierigkeit
                    + WahrerNameRufMod
                    + AusrüstungMod
                    + BannschwertRufMod
                    + SterneRufMod
                    + OrtRufMod
                    + DonariaRuf
                    + SonstigesRufMod;
            }
        }

        public int GesamtHerrschMod
        {
            get
            {
                return Kontrollschwierigkeit
                    + WahrerNameHerrschMod
                    + BefehlHerrschMod
                    + DauerHerrschMod
                    + AusrüstungMod
                    + BannschwertHerrschMod
                    + AffinitätHerrschMod
                    + BlutmagieHerrschMod
                    + SterneHerrschMod
                    + OrtHerrschMod
                    + DonariaHerrsch
                    + BezahlungHerrschMod
                    + SonstigesHerrschMod;
            }
        }

        #endregion
    }
}
