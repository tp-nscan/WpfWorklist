using SortNetwork.Results;
using SortNetwork.Switchables;

namespace SortNetwork.Sorters
{
    public class SwitchableResult
    {
        private readonly ISwitchable _finalResult;
        private readonly ISwitchable _stepZeroValue;
        private readonly ISorter _sorter;

        public SwitchableResult(ISorter sorter, ISwitchable stepZeroValue, ISwitchable finalResult)
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
