using SortNetwork.TestData;
using SorterControls.ViewModels.Entities;

namespace SorterControls.DesignData.Entities
{
    public class DesignSorterVm : SorterVm
    {
        public DesignSorterVm()
            : base(TestSorters.TheSorter)
        {
        }
    }
}
