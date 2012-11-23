using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.Model
{
    public partial class Munition
    {
        public Munition()
        {
            MunitionGUID = Guid.NewGuid();
        }

        public bool Usergenerated
        {
            get { return !MunitionGUID.ToString().StartsWith("00000000-0000-0000-00"); }
        }

        /// <summary>
        /// Gibt zurück, ob Munition im aktuellen Setting als gehärtet vorkommt
        /// </summary>
        /// <returns>true oder false</returns>
        public bool HärtbarNachSetting
        {
            get {
                return this.Härtbar;
                //TODO: Bei Setting Aventurien sind nur Kriegspfeile und Bolzen härtbar;
                // Schmiede: Probe fest +4, Preis zusätzlich *4
            }
        }
    }
}
