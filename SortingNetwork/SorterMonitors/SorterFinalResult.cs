using SortingNetwork.Sorters;
using SortingNetwork.Switchables;

namespace SortingNetwork.SorterMonitors
{
    public class SorterFinalResult
    {
        private readonly ISwitchable _finalResult;
        private readonly ISwitchable _stepZeroValue;
        private readonly ISorter _sorter;

        public SorterFinalResult(ISorter sorter, ISwitchable stepZeroValue, ISwitchable finalResult)
        {
            _sorter = sorter;
            _stepZeroValue = stepZeroValue;
            _finalResult = finalResult;
        }

        public ISwitchable FinalResult
        {
            get { return _finalResult; }
        }

        public ISwitchable StepZeroValue
        {
            get { return _stepZeroValue; }
        }

        public ISorter Sorter
        {
            get { return _sorter; }
        }
    }
}
