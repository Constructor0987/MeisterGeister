namespace MeisterGeister.LogicAlt.General
{
    public interface IProbe
    {
        string Name { get; }

        string WikiLink { get; }

        string Literatur { get; }

        string ProbenText { get; }
    }

    public interface IDreierProbe : IProbe
    {
        string Eigenschaft1 { get; }
        string Eigenschaft2 { get; }
        string Eigenschaft3 { get; }
    }
}
