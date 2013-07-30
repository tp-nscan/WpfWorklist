using MathUtils.Repos;

namespace SortingNetwork.KeyPair
{
    public interface IKeyPairRepo : IIndexedRepo<IKeyPair>
    {
        int KeyCount { get; }
    }
}