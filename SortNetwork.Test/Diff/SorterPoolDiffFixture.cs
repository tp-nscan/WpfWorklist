using Microsoft.VisualStudio.TestTools.UnitTesting;
using SortNetwork.Diff;
using SortNetwork.TestData;

namespace SortNetwork.Test.Diff
{
    [TestClass]
    public class SorterPoolDiffFixture
    {
        [TestMethod]
        public void TestSamePoolsHaveNoDiffs()
        {
            var sorterPoolDiff = SorterPoolDiff.Make
                                    (
                                        TestSorters.TheSorterRepo, 
                                        TestSorters.TheSorterRepo
                                    );

           Assert.IsFalse(sorterPoolDiff.AnySwitchLevelDiffs);
        }

        [TestMethod]
        public void TestDiffPoolsHaveDiffs()
        {
            var sorterPoolDiff = SorterPoolDiff.Make
                                    (
                                        TestSorters.TheSorterRepo, 
                                        TestSorters.SorterRepo(TestConstants.Seed + 1)
                                    );

            Assert.IsTrue(sorterPoolDiff.AnySwitchLevelDiffs);
        }
    }
}
