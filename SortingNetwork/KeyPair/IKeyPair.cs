using System.Collections.Generic;

namespace SortingNetwork.KeyPair
{
    public interface IKeyPair
    {
        int LowKey { get; }
        int HiKey { get; }
        int KeyCount { get; }
        int Index { get; }
        IKeyPair GetSibling(int index);
        int SiblingCount { get; }
    }
}
