using Microsoft.VisualStudio.TestTools.UnitTesting;
using SortNetwork.Diff;
using SortNetwork.TestData;

namespace SortNetwork.Test.Diff
{
    [TestClass]
    public class SwitchableRepoDiffFixture
    {
        [TestMethod]
        public void SwitchablePoolDiffTheSame()
        {
            var diff = SwitchableRepoDiff.Make
                (
                    TestSwitchable.TheSwitchableRepo,
                    TestSwitchable.TheSwitchableRepo
                );

            Assert.IsFalse(diff.HasDifferences);
        }

        [TestMethod]
        public void SwitchablePoolDiffDiff()
        {
            var diff = SwitchableRepoDiff.Make
                (
                    TestSwitchable.TheSwitchableRepo,
                    TestSwitchable.SwitchableRepo(TestConstants.Seed + 1)
                );

            Assert.IsTrue(diff.HasDifferences);
        }
    }
}
