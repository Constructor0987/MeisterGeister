using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.ViewModel.Basar.Logic
{
    public interface IHandelsgut
    {
        #region //---- EIGENSCHAFTEN ----

        string Name { get; }

        string Kategorie { get; }

        string Tags { get; }

        double? Gewicht { get; }

        string Pfad { get; }

        // Mengeneinheit
        string ME { get; }
        
        string Bemerkung { get; }

        string Literatur { get; }

        string Preis { get; }

        #endregion
    }
}
