using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.ViewModel.Kampf.Logic.Modifikatoren
{
    public interface IModEigenschaft : IModifikator { }
    public interface IModMitAuswirkungAufBerechneteWerte : IModifikator { }

    #region Basiseigenschaften
    public interface IModMU : IModEigenschaft
    {
        int ApplyMUMod(int wert);
    }

    public interface IModKL : IModEigenschaft
    {
        int ApplyKLMod(int wert);
    }

    public interface IModIN : IModEigenschaft
    {
        int ApplyINMod(int wert);
    }

    public interface IModCH : IModEigenschaft
    {
        int ApplyCHMod(int wert);
    }

    public interface IModFF : IModEigenschaft
    {
        int ApplyFFMod(int wert);
    }

    public interface IModGE : IModEigenschaft
    {
        int ApplyGEMod(int wert);
    }

    public interface IModKO : IModEigenschaft
    {
        int ApplyKOMod(int wert);
    }

    public interface IModKK : IModEigenschaft
    {
        int ApplyKKMod(int wert);
    }
    #endregion

    #region Abgeleitete Eigenschaften
    public interface IModGS : IModEigenschaft
    {
        int ApplyGSMod(int wert);
    }

    public interface IModLE : IModEigenschaft
    {
        int ApplyLEMod(int wert);
    }

    public interface IModAU : IModEigenschaft
    {
        int ApplyAUMod(int wert);
    }

    public interface IModAE : IModEigenschaft
    {
        int ApplyAEMod(int wert);
    }

    public interface IModKE : IModEigenschaft
    {
        int ApplyKEMod(int wert);
    }

    public interface IModMR : IModEigenschaft
    {
        int ApplyMRMod(int wert);
    }

    public interface IModINIBasis : IModEigenschaft
    {
        int ApplyINIBasisMod(int wert);
    }

    public interface IModINI : IModEigenschaft
    {
        int ApplyINIMod(int wert);
    }

    public interface IModATBasis : IModEigenschaft
    {
        int ApplyATBasisMod(int wert);
    }

    public interface IModAT : IModEigenschaft
    {
        int ApplyATMod(int wert);
    }

    public interface IModPABasis : IModEigenschaft
    {
        int ApplyPABasisMod(int wert);
    }

    public interface IModPA : IModEigenschaft
    {
        int ApplyPAMod(int wert);
    }

    public interface IModFKBasis : IModEigenschaft
    {
        int ApplyFKBasisMod(int wert);
    }

    public interface IModFK : IModEigenschaft
    {
        int ApplyFKMod(int wert);
    }
    #endregion

    #region Talente und Zauber
    
    /// <summary>
    /// Ist Talentname null, gilt der Modifikator für alle Talente.
    /// </summary>
    public interface IModTalentwert : IModEigenschaft
    {
        string Talentname { get; }
        int ApplyTalentwertMod(int wert);
    }
    /// <summary>
    /// Ist Zaubername null, gilt der Modifikator für alle Zauber.
    /// </summary>
    public interface IModZauberwert : IModEigenschaft
    {
        string Zaubername { get; }
        int ApplyZauberwertMod(int wert);
    }
    #endregion
}
