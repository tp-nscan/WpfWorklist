using System.Collections.Generic;
using System.Linq;
using SortingNetwork.Common;
using SortingNetwork.SorterMonitors;
using SortingNetwork.Sorters;
using SortingNetwork.SwitchableMonitors;

namespace SortingNetwork.Runner
{
    public class SorterTester
    {
        public SorterTester(ISorter sorter, List<ISwitchableMonitor> switchableMonitors)
        {
            _hashCode = sorter.ToHashCode();

            _sorterMonitor = sorter.Switches.Select(T=>SwitchMonitor.Make(T.Index, T.KeyPair, 0))
                                .ToSorterMonitor(sorter.Guid);

            _switchableMonitors = switchableMonitors;

            foreach (var switchable in SwitchableMonitors)
            {
                var finalResult = SorterMonitor.Sort(switchable);
                _sorterFinalResults.Add(new SorterFinalResult(SorterMonitor, switchable, finalResult));
            }

            _successfulSorts = SorterFinalResults.Count(T => T.FinalResult.IsSorted);
        }

        readonly List<SorterFinalResult> _sorterFinalResults = new List<SorterFinalResult>();
        public IEnumerable<SorterFinalResult> SorterFinalResults
        {
            get { return _sorterFinalResults; }
        }

        private readonly ISorterMonitor _sorterMonitor;
        public ISorterMonitor SorterMonitor
        {
            get { return _sorterMonitor; }
        }

        private readonly List<ISwitchableMonitor> _switchableMonitors;
        public IEnumerable<ISwitchableMonitor> SwitchableMonitors
        {
            get { return _switchableMonitors; }
        }

        private double? _score;
        public double Score
        {
            get
            {
                if (! _score.HasValue)
                {
                    //_score = SorterMonitor.SwitchesUsed - SuccessfulSorts + ParentSwitchableRepo.Size;
                    _score = SorterMonitor.SwitchesUsed * _switchableMonitors.Count
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

        private readonly int _successfulSorts;
        public int SuccessfulSorts
        {
            get { return _successfulSorts; }
        }
    }
}
