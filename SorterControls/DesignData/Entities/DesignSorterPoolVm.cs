using SorterControls.ViewModels.Entities;
using SortingNetworkDm.TestData;

namespace SorterControls.DesignData.Entities
{
    public class DesignSorterPoolVm : SorterPoolVmImpl
    {
        public DesignSorterPoolVm()
            : base(TestEntities.TheSorterPoolEntity)
        {
        }
    }
}
