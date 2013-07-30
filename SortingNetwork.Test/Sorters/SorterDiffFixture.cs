using System;
using MathUtils.Rand;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SortingNetwork.KeyPair;
using SortingNetwork.Sorters;

namespace SortingNetwork.Test.Sorters
{
    [TestClass]
    public class SorterDiffFixture
    {
        [TestMethod]
        public void TestMethod1()
        {
            const int cKeyCount = 16;
            const int cSwitchesPerSorter = 200;
            const int cSeed = 123;

            var sorterA = KeySet.Instance.AllPairsForKeyCount(cKeyCount)
                                   .RandomDraw(Randy.Fast(cSeed).ToInt(), cSwitchesPerSorter)
                                   .ToSorter(Guid.NewGuid());

            var sorterAprime = KeySet.Instance.AllPairsForKeyCount(cKeyCount)
                                   .RandomDraw(Randy.Fast(cSeed).ToInt(), cSwitchesPerSorter)
                                   .ToSorter(Guid.NewGuid());

            var sorterDiff = new SorterDiff(sorterA, sorterAprime);
        }
    }
}
