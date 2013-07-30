using SortingNetwork.KeyPair;

namespace SortingNetwork.Switches
{
    public static class Switch
    {
        public static ISwitch Make(int index, IKeyPair keyPair)
        {
            return new SwitchImpl(index, keyPair);
        }

        class SwitchImpl : SwitchBase
        {
            public SwitchImpl(int index, IKeyPair keyPair) : base(index, keyPair)
            {
            }

            public override SwitchType SwitchType
            {
                get { return SwitchType.Simple; }
            }
        }
    }
}
