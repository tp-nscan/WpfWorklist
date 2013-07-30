using DynamicModel;
using DynamicModel.Common;
using SorterControls.ViewModels.Bulders;

namespace SorterControls.DesignData.Builders
{
    public class DesignRandSwitchablePoolBuilderVm : SwitchablePoolBuilderVm
    {
        public DesignRandSwitchablePoolBuilderVm() : base(IndexProvider.MakeTest(1))
        {
            Name = "DesignRandSwitchablePoolBuilderVm name";
            Description = "DesignSorterPoolRandGenStepBuilderVm description";
            RandomSeedIn = 337;
            SwitchableCount = 33;
            KeyCount = 16;
        }
    }
}
