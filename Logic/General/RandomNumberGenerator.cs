using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.Logic.General
{
    public class RandomNumberGenerator
    {
        public static readonly Random Generator = new Random();

        /// <summary>
        /// Gibt eine ganzzahlige Zufallszahl in vorgegebenen Grenzen zurück.
        /// </summary>
        /// <param name="minvalue">Die inklusive Untergrenze</param>
        /// <param name="maxvalue">Die inklusive Obergrenze</param>
        /// <returns></returns>
        public static int RandomInt(int minvalue, int maxvalue)
        {
            return Generator.Next(minvalue, maxvalue + 1);
        }

        public static int W100
        {
            get { return Generator.Next(1, 101); }
        }

        public static int W20
        {
            get { return Generator.Next(1, 21); }
        }

        public static int W10
        {
            get { return Generator.Next(1, 11); }
        }

        public static int W6
        {
            get { return Generator.Next(1, 7); }
        }

        public static int W4
        {
            get { return Generator.Next(1, 5); }
        }

        public static int W3
        {
            get { return Generator.Next(1, 4); }
        }
        
        public static int W2
        {
            get { return Generator.Next(1, 3); }
        }

        public static int Wurf(WürfelEnum w)
        {
            switch(w)
            {
                case WürfelEnum._1W3:
                    return W3;
                case WürfelEnum._1W6:
                    return W6;
                case WürfelEnum._2W6:
                    return W6+W6;
                case WürfelEnum._1W10:
                    return W10;
                case WürfelEnum._1W20:
                    return W20;
                default:
                    return W6;
            }
        }
    }
}
