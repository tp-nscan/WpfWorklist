using SorterControls.ViewModels.Entities;
using SortingNetworkDm.TestData;

namespace SorterControls.DesignData.Entities
{
    public class DesignSorterResultPoolVm : SorterResultPoolVmImpl
    {
        public DesignSorterResultPoolVm()
            : base(TestEntities.TheSorterResultPoolEntity)
        {
        }
    }
}
