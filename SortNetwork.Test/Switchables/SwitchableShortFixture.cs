using Microsoft.VisualStudio.TestTools.UnitTesting;
using SortNetwork.KeySets;
using SortNetwork.Switchables;

namespace SortNetwork.Test.Switchables
{
    [TestClass]
    public class SwitchableShortFixture
    {
        [TestMethod]
        public void TestSwitch()
        {
            const ushort val4 = (ushort)(1 << 4);
            const ushort val7 = (ushort)(1 << 7);
            const ushort val4N7 = (ushort)(val4 | val7);

            var pos4 = SwitchableShort.Make(val4);
            var pos7 = SwitchableShort.Make(val7);
            var pos4N7 = SwitchableShort.Make(val4N7);

            var flipB4ToB7 = pos4.Switch(KeySet.Instance.GetKeyPair(4, 7, 16));
            var noFlipB4 = pos4.Switch(KeySet.Instance.GetKeyPair(6, 8, 16));
            var noChange4N7 = pos4N7.Switch(KeySet.Instance.GetKeyPair(4, 7, 16));

            Assert.AreSame(pos7, flipB4ToB7);
            Assert.AreSame(pos4, noFlipB4);
            Assert.AreSame(noChange4N7, pos4N7);
        }
    }
}
