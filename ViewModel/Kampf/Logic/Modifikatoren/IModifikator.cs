using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.ViewModel.Kampf.Logic.Modifikatoren
{
    public interface IModifikator
    {
        string Name { get; }
        string ToString();
        string Literatur { get; }
        string Auswirkung { get; }
        //DSADateTime
        DateTime Erstellt { get; }
        //Dauer?
    }

}
