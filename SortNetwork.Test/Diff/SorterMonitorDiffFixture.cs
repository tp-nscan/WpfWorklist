using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SortNetwork.Diff;
using SortNetwork.TestData;

namespace SortNetwork.Test.Diff
{
    [TestClass]
    public class SorterResultDiffFixture
    {
        [TestMethod]
        public void TestSorterResultForSwitchDiff()
        {
            var sorterDiff = SorterResultDiff.Make
                (
                    TestSorterResults.SorterResult(TestConstants.Seed + 1, Guid.NewGuid()),
                    TestSorterResults.TheSorterResult
                );

            Assert.IsTrue(sorterDiff.GuidsAreDifferent);
            Assert.IsTrue(sorterDiff.SwitchResultsAreDifferent);
        }
    }
}

