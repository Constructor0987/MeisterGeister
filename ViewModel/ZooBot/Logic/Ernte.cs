using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.ViewModel.ZooBot.Logic
{
    public interface Ernte
    {
        #region //---- EIGENSCHAFTEN ----

        Single Von { get; }

        Single Bis { get; }

        string Grundmenge { get; }

        string Pflanzenteil { get; }

        string Haltbarkeit { get; }

        string HaltbarkeitEinheit { get; }

        Single? Bestimmung2 { get; }

        string Bestimmungsausnahme { get; }

        Single? AusnahmeVon { get; }

        Single? AusnahmeBis { get; }
                
        string Literatur { get; }

        string Preis { get; }

        #endregion
    }
}
