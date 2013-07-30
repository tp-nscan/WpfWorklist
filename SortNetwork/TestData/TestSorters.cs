using System;
using MathUtils.Rand;
using SortNetwork.KeySets;
using SortNetwork.Sorters;

namespace SortNetwork.TestData
{
    public static class TestSorters
    {
        private static ISorter _testSorter;
        public static ISorter TheSorter
        {
            get
            {
                return _testSorter ??
                (
                    _testSorter = 
                    KeySet.Instance.AllKeyPairsForKeyCount(TestConstants.KeyCount)
                        .RandomDraw(Randy.Fast(TestConstants.Seed).ToInt(), TestConstants.SwitchesPerSorter)
                        .ToSorter(TestConstants.SorterGuid)
                );
            }
        }

        public static ISorter Sorter(int seed, Guid guid)
        {
            return KeySet.Instance.AllKeyPairsForKeyCount(TestConstants.KeyCount)
                         .RandomDraw(Randy.Fast(seed).ToInt(), TestConstants.SwitchesPerSorter)
                         .ToSorter(guid);
        }


        private static ISorterRepo _sorterRepo;
        public static ISorterRepo TheSorterRepo
        {
            get
            {
                return _sorterRepo ??
                    
                    (
                        _sorterRepo = 
                        
                        KeySet.Instance.AllKeyPairsForKeyCount(TestConstants.KeyCount).RandomDraw
                        (
                            Randy.Fast(TestConstants.Seed).ToInt()
                        )
                        .ToSorters
                        (
                            TestConstants.SwitchesPerSorter,
                            TestConstants.SorterCount, 
                            Randy.Fast(TestConstants.Seed).ToGuid()
                        )
                        .ToSorterRepo()
                    );
            }
        }

        public static ISorterRepo SorterRepo(int seed)
        {
            return KeySet.Instance.AllKeyPairsForKeyCount(TestConstants.KeyCount).RandomDraw
                    (
                        Randy.Fast(seed).ToInt()
                    )
                             .ToSorters(
                                 TestConstants.SwitchesPerSorter,
                                 TestConstants.SorterCount,
                                 Randy.Fast(seed).ToGuid()
                    )
                     .ToSorterRepo();
        }

    }
}
