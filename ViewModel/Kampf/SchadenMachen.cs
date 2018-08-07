using MeisterGeister.ViewModel.Base;
using MeisterGeister.ViewModel.Kampf.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MeisterGeister.ViewModel.Kampf
{
    public class SchadenMachen : ViewModelBase, ICommand
    {
        private IKämpfer kämpfer;
        public SchadenMachen(IKämpfer k)
        {
            kämpfer = k;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return kämpfer != null;
        }

        public void Execute(object parameter)
        {
            Execute(Schaden, Trefferzone, Verletzend, KeineWunden, Ausdauerschaden, IgnoriertRüstung);
        }

        public void Execute(int schaden, Trefferzone zone = Trefferzone.Zufall, bool verletzend = false, bool keineWunden = false, bool ausdauerschaden = false, bool ignoriertRüstung = false)
        {
            zone = Trefferzone == Trefferzone.Zufall ? TrefferzonenHelper.ZufallsZone() : Trefferzone;

            int rs = 0;
            if (!ignoriertRüstung)
                rs = kämpfer.RS[zone];
            int spa = 0;
            int sp = Math.Max(schaden - rs, 0);
            if (ausdauerschaden)
            {
                spa = sp;
                if (AusdauerschadenMachtKeinenEchtenSchaden)
                    sp = 0;
                else
                    sp = (int)Math.Round(spa / 2.0, MidpointRounding.AwayFromZero);
            }
            kämpfer.LebensenergieAktuell -= sp;
            kämpfer.AusdauerAktuell -= spa;

            //Log(string.Format("Treffer bei '{0}': {1} SP ({2} TP, RS {3}) in Zone {4}", k.Name, sp, tp, rs, zone));

            if (!keineWunden)
            {
                int wsmod = -(verletzend ? 2 : 0) + (ausdauerschaden ? 2 : 0);
                int wunden = 0;
                if (sp > kämpfer.Wundschwelle3 + wsmod)
                    wunden = 3;
                else if (sp > kämpfer.Wundschwelle2 + wsmod)
                    wunden = 2;
                else if (sp > kämpfer.Wundschwelle + wsmod)
                    wunden = 1;
                kämpfer.Wunden[zone] += wunden;

                if (wunden > 0)
                {
                    //Log(string.Format("Wunde(n) bei '{0}': {1} Wunde(n) in Zone {2}", k.Name, wunden, zone));
                }
            }

            // bei Tieren und wenn nur noch 1/3 der LeP -> Meisterhinweis "Flüchtet im Normalfall"
            if (kämpfer.LebensenergieAktuell > 0 && kämpfer.LebensenergieMax != 0 &&
                kämpfer.LebensenergieAktuell <= kämpfer.LebensenergieMax /3 && 
                kämpfer is IGegnerBase && (kämpfer as IGegnerBase).Tags.Contains("Tier"))
            {
                if (!Fliehen)
                    View.General.ViewHelper.Popup("Meister-Hinweis:" + Environment.NewLine + Environment.NewLine + "Das Tier flieht im Normalfall, da die LeP unter 1/3 der Gesamt-LeP liegen.");
                Fliehen = true;
            }

            LetzteTrefferzone = zone;
        }

        private bool fliehen = false;
        public bool Fliehen
        {
            get { return fliehen; }
            set { Set(ref fliehen, value); }
        }

        private int schaden = 5;
        public int Schaden
        {
            get { return schaden; }
            set { Set(ref schaden, value); }
        }

        private bool ausdauerschaden;
        public bool Ausdauerschaden
        {
            get { return ausdauerschaden; }
            set { Set(ref ausdauerschaden, value); }
        }

        private bool ausdauerschadenMachtKeinenEchtenSchaden;
        public bool AusdauerschadenMachtKeinenEchtenSchaden
        {
            get { return ausdauerschadenMachtKeinenEchtenSchaden; }
            set { Set(ref ausdauerschadenMachtKeinenEchtenSchaden, value); }
        }

        private bool ignoriertRüstung;
        public bool IgnoriertRüstung
        {
            get { return ignoriertRüstung; }
            set { Set(ref ignoriertRüstung, value); }
        }

        private bool verletzend;
        public bool Verletzend
        {
            get { return verletzend; }
            set { Set(ref verletzend, value); }
        }

        private bool keineWunden;
        public bool KeineWunden
        {
            get { return keineWunden; }
            set { Set(ref keineWunden, value); }
        }

        private Trefferzone trefferzone = Trefferzone.Zufall;
        public Trefferzone Trefferzone
        {
            get { return trefferzone; }
            set { Set(ref trefferzone, value); }
        }

        private Trefferzone letzteTrefferzone = Trefferzone.Unlokalisiert;
        public Trefferzone LetzteTrefferzone
        {
            get { return letzteTrefferzone; }
            private set { Set(ref letzteTrefferzone, value); }
        }

    }
}
