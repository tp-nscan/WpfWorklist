using System;
using MathUtils.Rand;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SortingNetwork.KeyPair;
using SortingNetwork.Sorters;

namespace SortingNetwork.Test.Sorters
{
    [TestClass]
    public class SorterFixture
    {
        [TestMethod]
        public void TestMutate()
        {
            //const int cKeyCount = 16;
            //const int cSwitchesPerSorter = 200;
            //const int cSeed = 123;
            //const double cMutationRate = 0.1;

            //var randomForKeyPairs = Randy.Fast(cSeed).ToInt();
            //var randomForMutation = Randy.Fast(cSeed).ToDouble();

            //var sorterA = KeySet.Instance.AllPairsForKeyCount(cKeyCount)
            //                       .RandomDraw(Randy.Fast(cSeed).ToInt(), cSwitchesPerSorter)
            //                       .ToSorter(Guid.NewGuid());

            //var sorterB = sorterA.Mutate
            //    (
            //        randomForMutation,
            //        randomForKeyPairs,

            //    )
        }
    }
}
