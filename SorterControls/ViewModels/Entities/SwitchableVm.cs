using SortNetwork.Switchables;
using WpfUtils;

namespace SorterControls.ViewModels.Entities
{
    public class SwitchableVm : ViewModelBase
    {

        public SwitchableVm(ISwitchable switchable)
        {
            _switchable = switchable;
        }

        private readonly ISwitchable _switchable;
        public ISwitchable Switchable
        {
            get { return _switchable; }
        }

        public string StringValue
        {
            get { return _switchable.StringValue; }
        }
    }
}
