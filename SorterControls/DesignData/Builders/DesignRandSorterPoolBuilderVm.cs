using DynamicModel;
using DynamicModel.Common;
using SorterControls.ViewModels.Bulders;

namespace SorterControls.DesignData.Builders
{
    public class DesignRandSorterPoolBuilderVm : SorterPoolBuilderVm
    {
        public DesignRandSorterPoolBuilderVm()
            : base(IndexProvider.MakeTest(1))
        {
            Name = "DesignRandSorterPoolBuilderVm name";
            Description = "DesignSorterPoolRandGenStepBuilderVm description";
            RandomSeedIn = 337;
            SorterCount = 33;
            SwitchesPerSorter = 99;
            KeyCount = 16;
        }
    }
}
