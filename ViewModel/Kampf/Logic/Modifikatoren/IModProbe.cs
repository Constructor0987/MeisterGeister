using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.ViewModel.Kampf.Logic.Modifikatoren
{
    static class ModProbeInit
    {
        static void Initialize()
        {
            ModifikatorTyp.Liste.Add(new ModifikatorTyp(typeof(IModAlleProben), "Alle Proben", "Proben"));
            ModifikatorTyp.Liste.Add(new ModifikatorTyp(typeof(IModAlleEigenschaftsProben), "Alle Eigenschaftsproben", "Proben"));
            ModifikatorTyp.Liste.Add(new ModifikatorTyp(typeof(IModTalentprobe), "Talentprobe", "Proben"));
            ModifikatorTyp.Liste.Add(new ModifikatorTyp(typeof(IModZauberprobe), "Zauberprobe", "Proben"));
        }
    }
    public interface IModProbe : IModifikator { }

    /// <summary>
    /// Modifikator für alle Dreier-Proben.
    /// </summary>
    public interface IModAlleProben : IModProbe
    {
        int ApplyAlleProbenMod(int wert);
    }

    public interface IModAlleEigenschaftsProben : IModProbe
    {
        int ApplyAlleEigenschaftsProbenMod(int wert);
    }

    /// <summary>
    /// Ist Talentname null, gilt der Modifikator für alle Talente.
    /// </summary>
    public interface IModTalentprobe : IModProbe
    {
        ISet<string> Talentname { get; }
        int ApplyTalentprobeMod(int wert);
    }
    /// <summary>
    /// Ist Zaubername null, gilt der Modifikator für alle Zauber.
    /// </summary>
    public interface IModZauberprobe : IModProbe
    {
        ISet<string> Zaubername { get; }
        int ApplyZauberprobeMod(int wert);
    }
}
