using System.Data.SqlTypes;

namespace Lab.Model.Containers
{
    public interface IContainerLoc : INullable
    {
        ContainerType ContainerType { get; }
        string LocId { get; }
    }
}
