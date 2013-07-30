using SortingNetwork.KeyPair;

namespace SortingNetwork.Switches
{
    public abstract class SwitchBase : ISwitch
    {
        private readonly int _index;
        private readonly IKeyPair _keyPair;

        protected SwitchBase(int index, IKeyPair keyPair)
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

        public abstract SwitchType SwitchType { get; }
    }
}