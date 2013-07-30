using Microsoft.VisualStudio.TestTools.UnitTesting;
using SortNetwork.Diff;
using SortNetwork.KeySets;
using SortNetwork.Sorters;

namespace SortNetwork.Test.Diff
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

            var a1DiffA2 = SwitchDiff.Make(1, swA1, swA2);

            Assert.IsFalse(a1DiffA2.SwitchesAreDifferent);

            var a1DiffA3 = SwitchDiff.Make(1, swA1, swA3);

            Assert.IsTrue(a1DiffA3.SwitchesAreDifferent);
        }
    }
}
