using SortNetwork.KeySets;
using SortNetwork.Results;
using WpfUtils;

namespace SorterControls.ViewModels.Entities
{
    public class SwitchResultVm : ViewModelBase
    {
        public SwitchResultVm(ISwitchResult switchResult, double useFraction)
        {
            _switchResult = switchResult;
            _useFraction = useFraction;
        }

        private readonly ISwitchResult _switchResult;
        public ISwitchResult SwitchResult
        {
            get { return _switchResult; }
        }

        public string Label
        {
            get { return SwitchResult.KeyPair.ToLabel(); }
        }

        public int UseCount
        {
            get { return SwitchResult.UseCount; }  
        }

        private readonly double _useFraction;
        public double UseFraction
        {
            get { return _useFraction; }
        }
    }
}
