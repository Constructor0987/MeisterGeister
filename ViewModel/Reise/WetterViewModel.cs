using MeisterGeister.Logic.Kalender.DsaTool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MeisterGeister.ViewModel.Reise
{
    public class WetterViewModel : Base.ViewModelBase
    {
        #region Properties

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
                temperaturZonen = null;
                OnChanged("TemperaturZonen");
                if (graph != null)
                    graph.Wetter = this;
            }
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

        private Season jahreszeit;
        public Season Jahreszeit
        {
            get { return jahreszeit; }
            set
            {
                jahreszeit = value;
                generiere();
                OnChanged();
            }
        }

        public int TemperaturDifferenz
        {
            get
            {
                int[] temps = new int[] { heute.Gestern.Nachttemperatur, heute.Tagestemperatur, heute.Nachttemperatur };
                int min = temps.Min();
                int max = temps.Max();
                return max - min;
            }
        }

        private List<TemperaturZoneViewModel> temperaturZonen;
        public List<TemperaturZoneViewModel> TemperaturZonen
        {
            get
            {
                if (temperaturZonen == null)
                {
                    int[] temps = new int[] { heute.Gestern.Nachttemperatur, heute.Tagestemperatur, heute.Nachttemperatur };
                    int min = temps.Min();
                    int max = temps.Max();
                    temperaturZonen = TemperaturZoneViewModel.GetZonesInRange(min, max, Graph.Height);
                }
                return temperaturZonen;
            }
        }

        private WetterGraphViewModel graph;
        public WetterGraphViewModel Graph
        {
            get
            {
                if (graph == null)
                {
                    graph = new WetterGraphViewModel();
                    graph.Wetter = this;
                }

                return graph;
            }
        }

        #endregion

        private void generiere()
        {
            Wetter wetter = new Wetter();
            wetter.Klimazone = Klimazone;
            wetter.Windreich = Windreich;
            wetter.Wüste = Wüste;
            wetter.Jahreszeit = Jahreszeit;
            wetter.Generiere();
            Heute = wetter;
        }

        #region Commands

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

        #endregion
    }
}
