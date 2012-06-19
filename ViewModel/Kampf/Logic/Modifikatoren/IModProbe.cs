using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.ViewModel.Kampf.Logic.Modifikatoren
{
    public interface IModProbe : IModifikator { }

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
        string Talentname { get; }
        int ApplyTalentprobeMod(int wert);
    }
    /// <summary>
    /// Ist Zaubername null, gilt der Modifikator für alle Zauber.
    /// </summary>
    public interface IModZauberprobe : IModProbe
    {
        string Talentname { get; }
        int ApplyZauberprobeMod(int wert);
    }
}
