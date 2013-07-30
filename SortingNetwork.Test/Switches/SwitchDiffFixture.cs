using Microsoft.VisualStudio.TestTools.UnitTesting;
using SortingNetwork.KeyPair;
using SortingNetwork.Switches;

namespace SortingNetwork.Test.Switches
{
    [TestClass]
    public class SwitchDiffFixture
    {
        [TestMethod]
        public void TestSwitchDiff()
        {
            var swA1 = Switch.Make(1, KeySet.Instance.GetKeyPair(1, 2, 16));
            var swA2 = Switch.Make(2, KeySet.Instance.GetKeyPair(1, 2, 16));
            var swA3 = Switch.Make(2, KeySet.Instance.GetKeyPair(1, 3, 16));

            var a1DiffA2 = new SwitchDiff(1, swA1, swA2);

            Assert.IsFalse(a1DiffA2.AreDifferent);

            var a1DiffA3 = new SwitchDiff(1, swA1, swA3);

            Assert.IsTrue(a1DiffA3.AreDifferent);
        }
    }
}
