using System.Collections.Generic;
using MathUtils.Rand;
using SortingNetwork.KeyPair;

namespace SortingNetwork.Switchables
{
    public interface ISwitchable
    {
        bool IsSorted { get; }
        IEnumerable<ISwitchable> CreateMutatedCopiesOf(IRando randomGen, int copyNumber);
        ISwitchable Switch(IKeyPair keyPair);
        int KeyCount { get; }
        SwitchableType SwitchableType { get; }
    }
}
