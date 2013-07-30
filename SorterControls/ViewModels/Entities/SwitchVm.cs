using SortNetwork.Sorters;
using WpfUtils;

namespace SorterControls.ViewModels.Entities
{
    public class SwitchVm : ViewModelBase
    {
        public SwitchVm(ISwitch @switch)
        {
            _switch = @switch;
        }

        private readonly ISwitch _switch;
        public ISwitch Switch
        {
            get { return _switch; }
        }
    }
}
