using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.ViewModel.Kampf.Logic.Modifikatoren
{
    static class ModEndetmitInit
    {
        static void Initialize()
        {
            ModifikatorTyp.Liste.Add(new ModifikatorTyp(typeof(IEndetMitAktion), "Aktion", "Endet mit"));
            ModifikatorTyp.Liste.Add(new ModifikatorTyp(typeof(IEndetMitKampfrunde), "Kampfrunde", "Endet mit"));
            ModifikatorTyp.Liste.Add(new ModifikatorTyp(typeof(IEndetMitKampf), "Kampf", "Endet mit"));
            ModifikatorTyp.Liste.Add(new ModifikatorTyp(typeof(IEndetMitSpielrunde), "Spielrunde", "Endet mit"));
            ModifikatorTyp.Liste.Add(new ModifikatorTyp(typeof(IEndetMitTag), "Tag", "Endet mit"));
            ModifikatorTyp.Liste.Add(new ModifikatorTyp(typeof(IEndetMitWoche), "Woche", "Endet mit"));
            ModifikatorTyp.Liste.Add(new ModifikatorTyp(typeof(IEndetMitMond), "Mond", "Endet mit"));
            ModifikatorTyp.Liste.Add(new ModifikatorTyp(typeof(IEndetMitSonnenwende), "Sonnenwende", "Endet mit"));
            ModifikatorTyp.Liste.Add(new ModifikatorTyp(typeof(IEndetMitJahr), "Jahr", "Endet mit"));
            //auskommentiert bis der Kalender überarbeitet wurde
            //ModifikatorTyp.Liste.Add(new ModifikatorTyp(typeof(IEndetMitZeitpunkt), "Zeitpunkt", "Endet mit"));
        }
    }

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
