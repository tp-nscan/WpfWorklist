using System;
using System.Linq;
using MathUtils.Rand;
using SortNetwork.KeySets;
using SortNetwork.Results;

namespace SortNetwork.TestData
{
    public static class TestSorterResults
    {
        private static ISorterResult _testSorterResult;
        public static ISorterResult TheSorterResult
        {
            get
            {
                return _testSorterResult ??
                (
                    _testSorterResult =
                        SortNetwork.Results.SorterResult.MakeTest
                        (
                            sorter: TestData.TestSorters.TheSorter,
                            rndSeed: TestConstants.Seed,
                            countOfTests: TestConstants.ResultTestCount,
                            switchesUsed: TestConstants.SwitchesPerSorter,
                            successfulSorts: TestConstants.ResultTestCount
                        )
                );

            }
        }

        public static ISorterResult SorterResult(int seed, Guid guid)
        {
            return SortNetwork.Results.SorterResult.MakeTest
                        (
                            sorter: TestData.TestSorters.Sorter(seed, guid),
                            rndSeed: seed,
                            countOfTests: TestConstants.ResultTestCount,
                            switchesUsed: TestConstants.SwitchesPerSorter,
                            successfulSorts: TestConstants.ResultTestCount
                        );
        }

        private static ISorterResultRepo _sorterResultRepo;
        public static ISorterResultRepo TheSorterResultRepo
        {
            get
            {
                var rndInt = Randy.Fast(TestConstants.Seed).ToInt();
                var rndGuid = Randy.Fast(TestConstants.Seed + 1).ToGuid();
                return _sorterResultRepo ??
                (
                    _sorterResultRepo =
                        Enumerable.Range(0, TestConstants.SorterCount)
                        .Select(i => SorterResult(rndInt.Next(), rndGuid.Next()))
                        .ToSorterResultRepo()
                );
            }
        }

        public static ISorterResultRepo SorterResultRepo(int seed)
        {
            var rndInt = Randy.Fast(seed).ToInt();
            var rndGuid = Randy.Fast(seed + 1).ToGuid();
            return Enumerable.Range(0, TestConstants.SorterCount)
                             .Select(i => SorterResult(rndInt.Next(), rndGuid.Next()))
                             .ToSorterResultRepo();
        }

    }
}
