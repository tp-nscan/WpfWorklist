using SortNetwork.KeySets;
using SorterControls.ViewModels.Entities;

namespace SorterControls.DesignData.Entities
{
    public class DesignSwitchVm : SwitchVm
    {
        public DesignSwitchVm()
            : base(SortNetwork.Sorters.Switch.Make(1, KeySet.Instance.GetKeyPair(2, 4, 16)))
        {
        }
    }
}
