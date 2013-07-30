using Lab.Model.Containers;

namespace Lab.Model.Sample
{
    public interface ISample
    {
        IContainerLoc Location { get; }
        string Id { get; }
    }
}
