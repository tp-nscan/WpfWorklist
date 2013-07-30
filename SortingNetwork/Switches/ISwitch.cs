using SortingNetwork.KeyPair;

namespace SortingNetwork.Switches
{
    public interface ISwitch
    {
        IKeyPair KeyPair { get; }
        int Index { get; }
        SwitchType SwitchType { get; }
    }
}