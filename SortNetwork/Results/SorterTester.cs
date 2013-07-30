using System.Collections.Generic;
using System.Linq;
using SortNetwork.Sorters;
using SortNetwork.Switchables;

namespace SortNetwork.Results
{
    public interface ISorterTester
    {
        int HashCode { get; }
        ISorterResult SorterResult { get; }
        IEnumerable<ISwitchable> Switchables { get; }
        double Score { get; }
        int SuccessfulSorts { get; }
    }

    public static class SorterTester
    {
        public static ISorterTester Make(ISorter sorter, List<ISwitchable> switchableResults)
        {
            return new SorterTesterImpl(sorter, switchableResults);
        }
    }

    class SorterTesterImpl : ISorterTester
    {
        public SorterTesterImpl(ISorter sorter, List<ISwitchable> switchables)
        {
            _hashCode = sorter.ToHashCode();
            _switchables = switchables;

            var switchUsageCounts = new int[sorter.SwitchCount];

            foreach (var switchable in Switchables)
            {
                ISwitchable lastSwitchable = switchable;
                for (var switchDex = 0; switchDex < sorter.SwitchCount; switchDex++)
                {
                    if (lastSwitchable.IsSorted)
                    {
                        break;
                    }

                    var curSwitchable = lastSwitchable.Switch(sorter.SwitchAtIndex(switchDex).KeyPair);
                    if (Equals(curSwitchable, lastSwitchable))
                    {
                        continue;
                    }

                    lastSwitchable = curSwitchable;
                    switchUsageCounts[switchDex]++;

                }
               // var finalResult = SorterResult.Sort(switchable);
                _switchableResults.Add(new SwitchableResult(sorter, switchable, lastSwitchable));
            }

            _sorterResult = Results.SorterResult.Make
                (
                    sorter: sorter,
                    switchResults: Enumerable.Empty<ISwitchResult>(),
                    countOfTests: TestCount,
                    successfulSorts: SuccessfulSorts,
                    switchesUsed : switchUsageCounts.Count(T=>T>0)
                );

        }

        readonly List<SwitchableResult> _switchableResults = new List<SwitchableResult>();
        public IEnumerable<SwitchableResult> SwitchableResults
        {
            get { return _switchableResults; }
        }

        private readonly ISorterResult _sorterResult;
        public ISorterResult SorterResult
        {
            get { return _sorterResult; }
        }

        private readonly List<ISwitchable> _switchables;
        public IEnumerable<ISwitchable> Switchables
        {
            get { return _switchables; }
        }

        private double? _score;
        public double Score
        {
            get
            {
                if (!_score.HasValue)
                {
                    //_score = SorterResult.SwitchesUsed - SuccessfulSorts + ParentSwitchableRepo.Size;
                    _score = SorterResult.SwitchesUsed * _switchables.Count
                            /
                            (0.01 + SuccessfulSorts);
                }
                return _score.Value;
            }
        }

        private readonly int _hashCode;
        public int HashCode
        {
            get { return _hashCode; }
        }

        public int SuccessfulSorts
        {
            get { return SwitchableResults.Count(T => T.FinalResult.IsSorted); }
        }

        public int TestCount
        {
            get { return _switchableResults.Count; }
        }
    }
}
