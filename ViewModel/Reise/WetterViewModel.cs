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

        private void generiere()
        {
            heute = new Wetter();
            heute.Klimazone = Klimazone;
            heute.Windreich = Windreich;
            heute.Wüste = Wüste;
            heute.Jahreszeit = Jahreszeit;
            heute.Generiere();

            Heute = heute;
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

        private List<TemperaturZoneViewModel> temperaturZonen;

        public List<TemperaturZoneViewModel> TemperaturZonen
        {
            get
            {
                if (temperaturZonen == null)
                {
                    List<TemperaturZoneViewModel> zonen = new List<TemperaturZoneViewModel>();
                    int[] temps = new int[] { heute.Gestern.Nachttemperatur, heute.Tagestemperatur, heute.Nachttemperatur };
                    int min = temps.Min();
                    int max = temps.Max();
                    TemperaturZoneViewModel zone = new TemperaturZoneViewModel(max);
                    zonen.Add(zone);
                    while (zone.MinTemp > min)
                    {
                        zone = zone.Kälter();
                        zonen.Add(zone);
                    }
                    double heightPerDegree = Graph.Height / (double)(zonen.First().MaxTemp - zonen.Last().MinTemp);
                    zonen.ForEach((z) =>
                    {
                        z.HeightPerDegree = heightPerDegree;
                        z.UpdateHeight();
                    });
                    zonen.First().IsHottest = zonen.Last().IsColdest = true;
                    temperaturZonen = zonen;
                }
                return temperaturZonen;
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

    }
}
