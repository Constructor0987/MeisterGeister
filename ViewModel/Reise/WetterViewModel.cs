using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MeisterGeister.ViewModel.Reise
{
    public class WetterViewModel : Base.ViewModelBase
    {
        public WetterViewModel()
        {

        }

        private Wetter heute;
        public Wetter Heute
        {
            get
            {
                if (heute == null)
                    generiere();
                return heute;
            }
            set
            {
                heute = value;
                OnChanged();
            }
        }

        private void generiere()
        {
            heute = new Wetter();
            heute.Klimazone = Klimazone;
            heute.Windreich = Windreich;
            heute.Wüste = Wüste;
            //TODO: Jahreszeit setzten
            heute.Generiere();
            
            OnChanged("Heute");
        }

        private Klimazone klimazone = Klimazone.ZentralesMittelreich;

        public Klimazone Klimazone
        {
            get { return klimazone; }
            set
            {
                klimazone = value;
                generiere();
                OnChanged();
            }
        }

        private int windreich;

        public int Windreich
        {
            get { return windreich; }
            set
            {
                windreich = value;
                generiere();
                OnChanged();
            }
        }

        private bool wüste;

        public bool Wüste
        {
            get { return wüste; }
            set
            {
                wüste = value;
                generiere();
                OnChanged();
            }
        }

        private Base.CommandBase nächsterTagCmd = null;
        public Base.CommandBase NächsterTagCmd
        {
            get
            {
                if (nächsterTagCmd == null)
                    nächsterTagCmd = new Base.CommandBase(NächsterTag, null);
                return nächsterTagCmd;
            }
        }

        /// <summary>
        /// Das Wetter des nächsten Tags wird ermittelt
        /// </summary>
        private void NächsterTag(object obj)
        {
            Heute = Heute.Morgen;
        }
    }
}
