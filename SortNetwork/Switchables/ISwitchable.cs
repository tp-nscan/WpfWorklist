using System.Collections.Generic;
using MathUtils.Rand;
using SortNetwork.KeySets;

namespace SortNetwork.Switchables
{
    public interface ISwitchable
    {
        bool IsSorted { get; }
        IEnumerable<ISwitchable> CreateMutatedCopiesOf(IRando randomGen, int copyNumber);
        ISwitchable Switch(IKeyPair keyPair);
        int KeyCount { get; }
        string StringValue { get; }
        SwitchableType SwitchableType { get; }
    }
}
