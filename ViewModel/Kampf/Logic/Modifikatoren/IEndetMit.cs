using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.ViewModel.Kampf.Logic.Modifikatoren
{
    public interface IEndetMitKampf : IModifikator { }
    public interface IEndetMitKampfrunde : IModifikator { }
    public interface IEndetMitSpielrunde : IModifikator { }
    public interface IEndetMitAktion : IModifikator { }

    public interface IEndetMitZeitpunkt : IModifikator
    {
        //DSADateTime
        DateTime Endzeitpunkt { get; }
    }
    public interface IEndetMitTag : IModifikator { }
    public interface IEndetMitWoche : IModifikator { }
    public interface IEndetMitMond : IModifikator { }
    public interface IEndetMitSonnenwende : IModifikator { }
    public interface IEndetMitJahr : IModifikator { }

}
