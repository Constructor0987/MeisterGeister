using System.Collections.Generic;

namespace MeisterGeister.Logic.General
{
    /// <summary>
    /// Beschreibt eine Liste von Talenttypen.
    /// </summary>
    public class TalentTypen : List<string>
    {
        /// <summary>
        /// Erzeugt eine Liste von Talenttypen.
        /// </summary>
        public TalentTypen()
        {
            Add("Basis");
            Add("Spezial");
        }
    }
}