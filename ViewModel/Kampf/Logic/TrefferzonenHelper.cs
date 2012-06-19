using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// Eigene Usings
using MeisterGeister.Logic.General;

namespace MeisterGeister.ViewModel.Kampf.Logic
{
    public class TrefferzonenHelper
    {
        /// <summary>
        /// Zufallszone für die Trefferbestimmung.
        /// ArmL = Schildarm, ArmR = Waffenarm. Brust und Rücken sind Zone Brust.
        /// </summary>
        public static Trefferzone ZufallsZone()
        {
            return ZufallsZone(false, false, false);
        }

        //TODO ??: später um eine Angriffsrichtung erweitern oder es dem Meister überlassen?
        /// <summary>
        /// Zufallszone für die Trefferbestimmung.
        /// ArmL = Schildarm, ArmR = Waffenarm. Brust und Rücken sind Zone Brust.
        /// </summary>
        public static Trefferzone ZufallsZone(bool reiter, bool zwergenwuchs, bool gegenreiter)
        {
            int w20 = RandomNumberGenerator.RandomInt(1, 20);
            if (zwergenwuchs)
                w20--;
            if (gegenreiter && !reiter)
                w20 -= 6;
            w20 = Math.Max(w20, 1);
            if (!reiter || gegenreiter)
            {
                switch (w20)
                {
                    case 20:
                    case 19:
                        return Trefferzone.Kopf;
                    case 18:
                    case 17:
                    case 16:
                    case 15:
                        return Trefferzone.Brust;
                    case 14:
                    case 12:
                    case 10:
                        return Trefferzone.ArmR;
                    case 13:
                    case 11:
                    case 9:
                        return Trefferzone.ArmL;
                    case 8:
                    case 7:
                        return Trefferzone.Bauch;
                    case 6:
                    case 4:
                    case 2:
                        return Trefferzone.BeinR;
                    case 5:
                    case 3:
                    case 1:
                        return Trefferzone.BeinL;
                }
            }
            else
            {
                switch (w20)
                {
                    case 20:
                    case 19:
                    case 18:
                    case 17:
                        return Trefferzone.Kopf;
                    case 16:
                    case 15:
                    case 14:
                    case 13:
                    case 12:
                    case 11:
                        return Trefferzone.Brust;
                    case 10:
                    case 8:
                    case 6:
                    case 4:
                        return Trefferzone.ArmR;
                    case 9:
                    case 7:
                    case 5:
                    case 3:
                        return Trefferzone.ArmL;
                    case 1:
                    case 2: 
                        return Trefferzone.Bauch;
                }
            }
            return Trefferzone.Brust;
        }
    }
}
