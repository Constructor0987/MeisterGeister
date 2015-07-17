using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeisterGeister.ViewModel.Reise
{
    public class TemperaturZoneViewModel
    {
        public int MinTemp { get; private set; }
        public int MaxTemp { get; private set; }
        public int TempDiff
        {
            get { return MaxTemp - MinTemp; }
        }
        public Temperaturklasse Klasse { get; private set; }
        public double Height { get; set; }
        public double HeightPerDegree { get; set; }
        public bool IsColdest { get; set; }
        public bool IsHottest { get; set; }

        public TemperaturZoneViewModel(int temp, double heightPerDegree=0)
        {
            Klasse = getTemperaturKlasse(temp);
            int[] bounds = getTemperaturGrenzen(Klasse);
            MinTemp = bounds[0];
            MaxTemp = bounds[1];
            Height = TempDiff * heightPerDegree;
            HeightPerDegree = heightPerDegree;
            IsColdest = IsHottest = false;
        }

        public void UpdateHeight()
        {
            Height = TempDiff * HeightPerDegree;
        }

        public TemperaturZoneViewModel Wärmer()
        {
            if (Klasse == Temperaturklasse.Eisenschmelze)
                return this;
            else return new TemperaturZoneViewModel(MaxTemp + 1, HeightPerDegree);
        }

        public TemperaturZoneViewModel Kälter()
        {
            if (Klasse == Temperaturklasse.NiederhöllischKalt)
                return this;
            else return new TemperaturZoneViewModel(MinTemp - 1, HeightPerDegree);
        }

        private static readonly int[] temperaturGrenzen = new int[]
        {
            -150, -100, -50, -35, -20, -10, 0, 10, 20, 30, 40, 55, 70, 100, 150, 250, 500, 1500
        };

        public Temperaturklasse getTemperaturKlasse(int temp)
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

        public int[] getTemperaturGrenzen(Temperaturklasse klasse)
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
