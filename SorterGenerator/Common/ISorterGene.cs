using SortNetwork.KeySets;

namespace SorterGenomes.Common
{
    public interface ISorterGene 
    {
        IKeyPair KeyPair { get; }
    }
}