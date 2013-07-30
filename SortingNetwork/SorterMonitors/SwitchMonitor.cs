using SortingNetwork.KeyPair;
using SortingNetwork.Switches;

namespace SortingNetwork.SorterMonitors
{
    public static class SwitchMonitor
    {
        public static ISwitchMonitor Make(int index, IKeyPair keyPair, int useCount)
        {
            return new SwitchMonitorImpl(index, keyPair, useCount);
        }

        public static ISwitchMonitor ToSwitchMonitor(this ISwitch @switch)
        {
          return new SwitchMonitorImpl(@switch.Index, @switch.KeyPair, 0);
        }

        class SwitchMonitorImpl : SwitchBase, ISwitchMonitor
        {

            public SwitchMonitorImpl(int index, IKeyPair keyPair, int useCount)
                : base(index, keyPair)
            {
                _useCount = useCount;
            }

            private int _useCount;
            public int UseCount
            {
                get { return _useCount; }
                set { _useCount = value;  }
            }

            public override SwitchType SwitchType
            {
                get { return SwitchType.Simple; }
            }
        }
    }
}
