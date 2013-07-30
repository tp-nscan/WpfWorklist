using MathUtils.Repos;

namespace SortingNetwork.Switchables
{
    public interface ISwitchableRepo<out T> : IIndexedRepo<T> where T : ISwitchable
    {
        int KeyCount { get; }
    }
}