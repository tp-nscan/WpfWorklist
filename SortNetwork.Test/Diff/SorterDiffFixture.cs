using System;
using MathUtils.Rand;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SortNetwork.Diff;
using SortNetwork.KeySets;
using SortNetwork.TestData;

namespace SortNetwork.Test.Diff
{
    [TestClass]
    public class SorterDiffFixture
    {
        [TestMethod]
        public void TestSorterSameAreSame()
        {
            var sorterDiff = SorterDiff.Make(TestSorters.TheSorter, TestSorters.TheSorter);

            Assert.IsFalse(sorterDiff.GuidsAreDifferent);
            Assert.IsFalse(sorterDiff.SwitchesAreDifferent);
        }

        [TestMethod]
        public void TestSorterDiffAreDiff()
        {
            var sorterDiff = SorterDiff.Make
                (
                    TestSorters.TheSorter, 
                    TestSorters.Sorter(TestConstants.Seed+1, Guid.NewGuid())
                );

            Assert.IsTrue(sorterDiff.GuidsAreDifferent);
            Assert.IsTrue(sorterDiff.SwitchesAreDifferent);
        }
    }
}
