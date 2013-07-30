using SortNetwork.KeySets;
using SortNetwork.Sorters;

namespace SortNetwork.Results
{
    public interface ISwitchResult : ISwitch
    {
        int UseCount { get; }
    }

    public static class SwitchResult
    {
        public static ISwitchResult Make(int index, IKeyPair keyPair, int useCount)
        {
            return new SwitchResultImpl(index, keyPair, useCount);
        }

        public static ISwitchResult ToSwitchResult(this ISwitch @switch)
        {
          return new SwitchResultImpl(@switch.Index, @switch.KeyPair, 0);
        }
    }

    class SwitchResultImpl : ISwitchResult
    {

        public SwitchResultImpl(int index, IKeyPair keyPair, int useCount)
        {
            _index = index;
            _keyPair = keyPair;
            _useCount = useCount;
        }

        private readonly IKeyPair _keyPair;
        public IKeyPair KeyPair
        {
            get { return _keyPair; }
        }

        private readonly int _index;
        public int Index
        {
            get { return _index; }
        }

        private int _useCount;
        public int UseCount
        {
            get { return _useCount; }
            set { _useCount = value; }
        }

    }
}
