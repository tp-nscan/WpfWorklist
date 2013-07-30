using Microsoft.VisualStudio.TestTools.UnitTesting;
using SortNetwork.Diff;
using SortNetwork.KeySets;
using SortNetwork.Results;

namespace SortNetwork.Test.Diff
{
    [TestClass]
    public class SwitchResultDiffFixture
    {
        [TestMethod]
        public void TestSwitchResultDiff()
        {
            var swA1 = SwitchResult.Make(1, KeySet.Instance.GetKeyPair(1, 2, 16), 1);
            var swA2 = SwitchResult.Make(2, KeySet.Instance.GetKeyPair(1, 2, 16), 2);
            var swA3 = SwitchResult.Make(2, KeySet.Instance.GetKeyPair(1, 3, 16), 1);

            var a1DiffA2 = SwitchResultDiff.Make(1, swA1, swA2);

            Assert.IsFalse(a1DiffA2.SwitchesAreDifferent);
            Assert.IsTrue(a1DiffA2.UsagesAreDifferent);

            var a1DiffA3 = SwitchResultDiff.Make(1, swA1, swA3);
            Assert.IsTrue(a1DiffA3.SwitchesAreDifferent);
        }
    }
}
