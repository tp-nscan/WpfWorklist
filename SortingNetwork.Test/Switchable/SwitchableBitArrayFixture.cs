using Microsoft.VisualStudio.TestTools.UnitTesting;
using SortingNetwork.KeyPair;
using SortingNetwork.Switchables;

namespace SortingNetwork.Test.Switchable
{
    [TestClass]
    public class SwitchableBitArrayFixture
    {
        [TestMethod]
        public void TestEquality()
        {
            const int keyCount = 4;

            var unSwitch = new[] { false, true, false, true };
            var switch03 = new[] { true, true, false, false };
            var switch03P = new[] { true, true, false, false };
            var switch12 = new[] { false, false, true, true };

            var sba = SwitchableBitArray.Make(unSwitch, keyCount);
            var sba03 = SwitchableBitArray.Make(switch03, keyCount);
            var sba03P = SwitchableBitArray.Make(switch03, keyCount);
            var sba03Pp = SwitchableBitArray.Make(switch03P, keyCount);
            var sba12 = SwitchableBitArray.Make(switch12, keyCount);

            var sw03 = sba.Switch(KeySet.Instance.GetKeyPair(0, 3, sba.KeyCount));
            var sw12 = sba.Switch(KeySet.Instance.GetKeyPair(1, 2, sba.KeyCount));
  

            Assert.AreEqual(sba03.Equals(sba03P), true);
            Assert.AreEqual(sba03.Equals(sba03Pp), true);
            Assert.AreEqual(sba03.Equals(switch12), false);

            Assert.AreEqual(sba.Equals(sw03), false);
            Assert.AreEqual(sba.Equals(sw12), false);
            Assert.AreEqual(sba12.Equals(sw12), true);

        }

        [TestMethod]
        public void TestSwitch()
        {
            const int keyCount = 4;

            var unSwitch = new[] { false, true, false, true };
            var switch03 = new[] { true, true, false, false };
            var switch03P = new[] { true, true, false, false };
            var switch12 = new[] { false, false, true, true };

            var sba = SwitchableBitArray.Make(unSwitch, keyCount);
            var sba03 = SwitchableBitArray.Make(switch03, keyCount);
            var sba03P = SwitchableBitArray.Make(switch03, keyCount);
            var sba03Pp = SwitchableBitArray.Make(switch03P, keyCount);
            var sba12 = SwitchableBitArray.Make(switch12, keyCount);

            var sk03 = KeySet.Instance.GetKeyPair(0, 3, keyCount);
            var sk12 = KeySet.Instance.GetKeyPair(1, 2, keyCount);

            var res03 = sba.Switch(sk03);
            var res12 = sba.Switch(sk12);

            Assert.AreEqual(sba03.Equals(sba03P), true);
            Assert.AreEqual(sba03.Equals(sba03Pp), true);
            Assert.AreEqual(sba03.Equals(switch12), false);

            Assert.AreEqual(sba.Equals(res03), false);
            Assert.AreEqual(sba.Equals(res12), false);
            Assert.AreEqual(sba12.Equals(res12), true);

        }
    }
}
