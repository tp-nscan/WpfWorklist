using SorterControls.ViewModels.Entities;
using SortingNetworkDm.TestData;

namespace SorterControls.DesignData.Entities
{
    public class DesignSwitchablePoolVm : SwitchablePoolVmImpl
    {
        public DesignSwitchablePoolVm()
            : base(TestEntities.TheSwitchablePoolEntity)
        {
        }
    }
}
