using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathUtils.Rand;
using SortNetwork.KeySets;
using SortNetwork.Sorters;

namespace SortNetwork.Results
{
    public interface ISorterResult
    {
        ISorter Sorter { get; }
        IEnumerable<ISwitchResult> SwitchResults { get; }
        int CountOfTests { get; }
        int SuccessfulSorts { get; }
        int SwitchesUsed { get; }
    }

    public static class SorterResult
    {
        public static ISorterResult Make
            (
                ISorter sorter,
                IEnumerable<ISwitchResult> switchResults,
                int countOfTests,
                int successfulSorts,
                int switchesUsed
            )
        {
            return new SorterResultImpl
                (
                    sorter: sorter,
                    switchResults: switchResults,
                    countOfTests: countOfTests,
                    successfulSorts: successfulSorts,
                    switchesUsed: switchesUsed
                );
        }

        public static ISorterResult MakeTest
        (
            ISorter sorter,
            int rndSeed,
            int countOfTests,
            int successfulSorts,
            int switchesUsed
        )
        {
            var rnd = Randy.Fast(rndSeed).ToInt(countOfTests);
            var switchResults = Enumerable.Range(0, sorter.SwitchCount)
                                          .Select(i => SwitchResult.Make
                                            (
                                                index: i,
                                                keyPair: sorter.SwitchAtIndex(i).KeyPair,
                                                useCount: rnd.Next()
                                            )
                );
            return new SorterResultImpl
                (
                    sorter: sorter,
                    switchResults: switchResults,
                    countOfTests: countOfTests,
                    successfulSorts: successfulSorts,
                    switchesUsed: switchesUsed
                );
        }

        //public static ISorterResult ToSorterResult(this ISorter sorter)
        //{
        //    return new SorterResultImpl
        //        (
        //            switchResults: sorter.Switches.Select(t => t.ToSwitchResult()),
        //            countOfTests: 0,
        //            successfulSorts: 0,
        //            switchesUsed: 0
        //        );
        //}


        //public static ISorterResult ToSorterResult(this IEnumerable<ISwitchResult> switchResults, Guid guid)
        //{
        //    return new SorterResultImpl
        //        (
        //            switchResults: switchResults,
        //            countOfTests: 0,
        //            successfulSorts: 0,
        //            switchesUsed: 0
        //        );
        //}

        //public static ISorterResult ToSorterResult(this IEnumerable<IKeyPair> keyPairs, Guid guid)
        //{
        //    var keyPairList = keyPairs as IList<IKeyPair> ?? keyPairs.ToList();
        //    return new SorterResultImpl
        //        (
        //        sorter:
        //            switchResults: keyPairList.Select((keyPair, index) => SwitchResult.Make(index, keyPair, 0)),
        //            countOfTests: 0,
        //            successfulSorts: 0,
        //            switchesUsed: 0
        //        );
        //}

        //public static ISorterResult ToSorterResult(this IEnumerable<Tuple<IKeyPair, int>> tuples, int testCount, Guid guid)
        //{
        //    return new SorterResultImpl
        //        (
        //            switchResults: tuples.Select((tuple, index) => SwitchResult.Make(index, tuple.Item1, tuple.Item2)),
        //            countOfTests: testCount,
        //            successfulSorts: 0,
        //            switchesUsed: 0
        //        );
        //}

        //public static IEnumerable<ISorterResult> ToSorterResults
        //(
        //    this IEnumerable<IKeyPair> keyPairs,
        //    int switchesPerSorter,
        //    int sorterCount,
        //    IEnumerable<Guid> randomGuid,
        //    int monitorTestCount,
        //    IEnumerable<int> enumerableInt
        //)
        //{
        //    var guidEnumerator = randomGuid.GetEnumerator();

        //    foreach (var tupe in keyPairs.DoubleChunk
        //                            (
        //                                enumerableInt,
        //                                switchesPerSorter,
        //                                sorterCount
        //                            )
        //    )
        //    {
        //        guidEnumerator.MoveNext();
        //        yield return tupe.ToSorterResult(monitorTestCount, guidEnumerator.Current);
        //    }
        //}

        public static string LongSwitchReport(this ISorterResult sorterResult)
        {
            var sb = new StringBuilder();
            var maxSwitch = sorterResult.SwitchResults.Where(T => T.UseCount > 0).Max(q => q.Index);
            foreach (var switchUsage in sorterResult.SwitchResults)
            {
                if (switchUsage.Index > maxSwitch)
                {
                    return sb.ToString();
                }

                sb.Append(switchUsage.UseCount > 0
                                ? string.Format("\t[{0} : {1}]", switchUsage.KeyPair.ToLabel(), switchUsage.UseCount)
                                : string.Format("\t[{0}]", switchUsage.KeyPair.ToLabel()));
            }
            return sb.ToString();
          }
    }

    public class SorterResultImpl : ISorterResult
    {
        public SorterResultImpl
            (
                ISorter sorter, 
                IEnumerable<ISwitchResult> switchResults, 
                int countOfTests, 
                int successfulSorts, 
                int switchesUsed
            )
        {
            _sorter = sorter;
            _switchResults = switchResults.OrderBy(s => s.Index).ToList();
            _countOfTests = countOfTests;
            _successfulSorts = successfulSorts;
            _switchesUsed = switchesUsed;
        }

        private readonly ISorter _sorter;
        public ISorter Sorter
        {
            get { return _sorter; }
        }

        private readonly List<ISwitchResult> _switchResults;
        public IEnumerable<ISwitchResult> SwitchResults
        {
            get { return _switchResults; }
        }

        private readonly int _countOfTests;
        public int CountOfTests
        {
            get { return _countOfTests; }
        }

        private readonly int _successfulSorts;
        public int SuccessfulSorts
        {
            get { return _successfulSorts; }
        }

        private readonly int _switchesUsed;
        public int SwitchesUsed
        {
            get { return _switchesUsed; }
        }
    }

//    class SorterResultImpl : ISorterResult
//    {
//        public SorterResultImpl(Guid guid, int CountOfTests, IEnumerable<ISwitchResult> switchResults)
//        {
//            _guid = guid;
//            _switches = switchResults.ToList();
//            _monitorTestCount = CountOfTests;
//            if (_switches.Count == 0) { return; }

//            #if SAFE_MODE

//            SafeModeCheck();

//            #endif

//            _keyCount = _switches[0].KeyPair.KeyCount;
//        }

//// ReSharper disable UnusedMember.Local
//        void SafeModeCheck()
//// ReSharper restore UnusedMember.Local
//        {
//            if (_switches.AreOutOfOrder(sw => sw.Index))
//            {
//                throw new ArgumentException("SwitchResultsToJson are not sequentially indexed");
//            }

//            var keyCountGroups = SwitchResults.GroupBy(T => T.KeyPair.KeyCount).ToList();
//            if (keyCountGroups.Count != 1)
//            {
//                throw new Exception("switchRepos must all have the same KeyCount");
//            }
//        }

//        private readonly Guid _guid;
//        public Guid Guid
//        {
//            get { return _guid; }
//        }

//        private readonly int _keyCount;
//        public int KeyCount
//        {
//            get { return _keyCount; }
//        }

//        private readonly List<ISwitchResult> _switches;
//        ISwitchable ISorter.Sort(ISwitchable switchable)
//        {
//            throw new NotImplementedException();
//        }

//        IEnumerable<ISwitch> ISorter.SwitchResults
//        {
//            get { return SwitchResults; }
//        }

//        private int _monitorTestCount;
//        public int CountOfTests
//        {
//            get { return _monitorTestCount; }
//        }

//        public int SuccessfulSorts { get; set; }

//        public IEnumerable<ISwitchResult> SwitchResults
//        {
//            get
//            {
//                for (var i = 0; i < _switches.Count; i++)
//                {
//                    yield return _switches[i];
//                }
//            }
//        }

//        public ISwitchableResult Sort(ISwitchableResult switchableResult)
//        {
//            _monitorTestCount++;
//            var lastSwitchable = switchableResult;

//            foreach (var @switch in SwitchResults)
//            {
//                if (lastSwitchable.Switchable.IsSorted)
//                {
//                    return lastSwitchable;
//                }
//                var curSwitchable = lastSwitchable.Switch(@switch);
//                if (Equals(curSwitchable, lastSwitchable))
//                {
//                    continue;
//                }

//                lastSwitchable = curSwitchable;
//                @switch.UseCount++;
//            }
//            return lastSwitchable;
//        }

//        private int? _switchesUsed;

//        public int SwitchesUsed
//        {
//            get
//            {
//                if (! _switchesUsed.HasValue)
//                {
//                    _switchesUsed = SwitchResults.Count(T => T.UseCount > 0);
//                }
//                return _switchesUsed.Value;
//            }
//        }

//        public int TotalSwitches
//        {
//            get { return SwitchResults.Count(); }
//        }
//    }
}
