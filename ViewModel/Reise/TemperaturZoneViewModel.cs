using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeisterGeister.ViewModel.Reise
{
    /// <summary>
    /// Stellt eine TemperaturZone für den WetterGraph dar
    /// </summary>
    public class TemperaturZoneViewModel
    {
        private static readonly int[] temperaturGrenzen = new int[]
        {
            -150, -100, -50, -35, -20, -10, 0, 10, 20, 30, 40, 55, 70, 100, 150, 250, 500, 1500
        };

        public static List<TemperaturZoneViewModel> GetZonesInRange(int min, int max, int height)
        {
            List<TemperaturZoneViewModel> zonen = new List<TemperaturZoneViewModel>();
            TemperaturZoneViewModel zone = new TemperaturZoneViewModel(max);
            zonen.Add(zone);
            while (zone.MinTemp > min)
            {
                zone = zone.Kälter();
                zonen.Add(zone);
            }
            double heightPerDegree = height / (double)(zonen.First().MaxTemp - zonen.Last().MinTemp);
            zonen.ForEach((z) =>
            {
                z.HeightPerDegree = heightPerDegree;
                z.Height = z.TempDiff * heightPerDegree;
            });
            zonen.First().IsHottest = zonen.Last().IsColdest = true;
            return zonen;
        }

        #region Properties
        public int MinTemp { get; private set; }
        public int MaxTemp { get; private set; }
        public int TempDiff
        {
            get { return MaxTemp - MinTemp; }
        }
        public Temperaturklasse Klasse { get; private set; }
        /// <summary>
        /// Höhe der Zone im Graph
        /// </summary>
        public double Height { get; set; }
        /// <summary>
        /// Höhe eines Grads im Graph
        /// </summary>
        public double HeightPerDegree { get; set; }
        /// <summary>
        /// Gibt an ob es die Kälteste Temperaturzone im Graphen ist
        /// </summary>
        public bool IsColdest { get; set; }
        /// <summary>
        /// Gibt an ob es die heisseste Temperaturzone im Graphen ist
        /// </summary>
        public bool IsHottest { get; set; }
        #endregion

        public TemperaturZoneViewModel(int temp)
        {
            Klasse = getTemperaturKlasse(temp);
            int[] bounds = getTemperaturGrenzen(Klasse);
            MinTemp = bounds[0];
            MaxTemp = bounds[1];
            IsColdest = IsHottest = false;
        }

        public TemperaturZoneViewModel Wärmer()
        {
            if (Klasse == Temperaturklasse.Eisenschmelze)
                return this;
            else return new TemperaturZoneViewModel(MaxTemp + 1);
        }

        public TemperaturZoneViewModel Kälter()
        {
            if (Klasse == Temperaturklasse.NiederhöllischKalt)
                return this;
            else return new TemperaturZoneViewModel(MinTemp - 1);
        }

        private Temperaturklasse getTemperaturKlasse(int temp)
        {
            Temperaturklasse klasse = Temperaturklasse.NiederhöllischKalt;
            for (int i = 0; i < temperaturGrenzen.Length; i++)
            {
                if (temperaturGrenzen[i] >= temp)
                {
                    klasse = (Temperaturklasse)(i - 8);
                    break;
                }
            }
            return klasse;
        }

        private int[] getTemperaturGrenzen(Temperaturklasse klasse)
        {
            int indexOfUpperBound = (int)klasse + 8;
            return new int[]
            {
                getBound(indexOfUpperBound - 1),
                getBound(indexOfUpperBound)
            };
        }

        private int getBound(int index)
        {
            if (index < 0)
                return -10000;
            else if (index >= temperaturGrenzen.Length)
                return 10000;
            else
                return temperaturGrenzen[index];
        }
    }

    public enum Temperaturklasse
    {
        [Description("Niederhöllisch kalt")]
        NiederhöllischKalt = -8,
        [Description("Namenlos kalt")]
        NamenlosKalt = -7,
        [Description("Grimmfrostig")]
        Grimmfrostig = -6,
        [Description("Firunsfrostig")]
        Firunsfrostig = -5,
        [Description("Eisig")]
        Eisig = -4,
        [Description("Frostig")]
        Frostig = -3,
        [Description("Kalt")]
        Kalt = -2,
        [Description("Kühl")]
        Kühl = -1,
        [Description("Mäßig warm")]
        MäßigWarm = 0,
        [Description("Sehr warm")]
        SehrWarm = 1,
        [Description("Heiß")]
        Heiß = 2,
        [Description("Sehr heiß")]
        SehrHeiß = 3,
        [Description("Glühend heiß")]
        GlühendHeiß = 4,
        [Description("Khômglut")]
        Khômglut = 5,
        [Description("Kochend heiß")]
        KochendHeiß = 6,
        [Description("Backofen")]
        Backofen = 7,
        [Description("Kohlenglut")]
        Kohlenglut = 8,
        [Description("Vulkanglut")]
        Vulkanglut = 9,
        [Description("Eisenschmelze")]
        Eisenschmelze = 10
    }
}
