using SortNetwork.KeySets;

namespace SortNetwork.Sorters
{
    public interface ISwitch
    {
        IKeyPair KeyPair { get; }
        int Index { get; }
    }

    public static class Switch
    {
        public static ISwitch Make(int index, IKeyPair keyPair)
        {
            return new SwitchImpl(index, keyPair);
        }
    }

    class SwitchImpl : ISwitch
    {
        private readonly int _index;
        private readonly IKeyPair _keyPair;

        public SwitchImpl(int index, IKeyPair keyPair)
        {
            _index = index;
            _keyPair = keyPair;
        }

        public IKeyPair KeyPair
        {
            get { return _keyPair; }
        }

        public int Index
        {
            get { return _index; }
        }
    }

}
