using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SortNetwork.KeySets;
using SortNetwork.Sorters;

namespace SortNetwork.Test.Sorters
{
    [TestClass]
    public class StagedSwitchFixture
    {
        [TestMethod]
        public void TestOneSwitch()
        {
            var stagedSwitches = TestGroup1.ToStagedSwitches().ToList();

            Assert.IsTrue(stagedSwitches.Select(T => T.StageNumber).SequenceEqual(TestGroup1Stages));
        }

        public IEnumerable<ISwitch> TestGroup1
        {
            get
            {
                const int keyCount = 8;
                yield return Switch.Make(0, KeySet.Instance.GetKeyPair(0, 1, keyCount));
            }
        }

        public IEnumerable<int> TestGroup1Stages
        {
            get
            {
                yield return 0;
            }
        }

        [TestMethod]
        public void TestTwoSwitches()
        {
            var stagedSwitches = TestGroup2.ToStagedSwitches().ToList();

            Assert.IsTrue(stagedSwitches.Select(T => T.StageNumber).SequenceEqual(TestGroup2Stages));
        }

        public IEnumerable<ISwitch> TestGroup2
        {
            get
            {
                const int keyCount = 8;
                yield return Switch.Make(0, KeySet.Instance.GetKeyPair(0, 1, keyCount));
                yield return Switch.Make(1, KeySet.Instance.GetKeyPair(0, 1, keyCount));
            }
        }

        public IEnumerable<int> TestGroup2Stages
        {
            get
            {
                yield return 0;
                yield return 1;
            }
        }


        [TestMethod]
        public void TestThreeSwitches()
        {
            var stagedSwitches = TestGroup3.ToStagedSwitches().ToList();

            Assert.IsTrue(stagedSwitches.Select(T => T.StageNumber).SequenceEqual(TestGroup3Stages));
        }

        public IEnumerable<ISwitch> TestGroup3
        {
            get
            {
                const int keyCount = 8;
                yield return Switch.Make(0, KeySet.Instance.GetKeyPair(0, 1, keyCount));
                yield return Switch.Make(1, KeySet.Instance.GetKeyPair(0, 1, keyCount));
                yield return Switch.Make(2, KeySet.Instance.GetKeyPair(2, 3, keyCount));
            }
        }

        public IEnumerable<int> TestGroup3Stages
        {
            get
            {
                yield return 0;
                yield return 1;
                yield return 0;
            }
        }

        [TestMethod]
        public void Test5Switches()
        {
            var stagedSwitches = TestGroup4.ToStagedSwitches().ToList();

            Assert.IsTrue(stagedSwitches.Select(T => T.StageNumber).SequenceEqual(TestGroup4Stages));
        }

        public IEnumerable<ISwitch> TestGroup4
        {
            get
            {
                const int keyCount = 8;
                yield return Switch.Make(0, KeySet.Instance.GetKeyPair(0, 1, keyCount));
                yield return Switch.Make(1, KeySet.Instance.GetKeyPair(0, 4, keyCount));
                yield return Switch.Make(2, KeySet.Instance.GetKeyPair(2, 3, keyCount));
                yield return Switch.Make(3, KeySet.Instance.GetKeyPair(4, 5, keyCount));
                yield return Switch.Make(3, KeySet.Instance.GetKeyPair(6, 7, keyCount));
            }
        }

        public IEnumerable<int> TestGroup4Stages
        {
            get
            {
                yield return 0;
                yield return 1;
                yield return 0;
                yield return 2;
                yield return 0;
            }
        }

    }


}
