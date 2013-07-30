using System.Linq;
using DynamicModel;
using DynamicModel.Common;
using SorterControls.ViewModels.Bulders;
using SorterControls.ViewModels.Entities;
using SortingNetworkDm.Entities;

namespace SorterControls.DesignData.Builders
{
    class DesignCompetePoolStepBuilderVm : CompetePoolBuilderVm
    {
        public DesignCompetePoolStepBuilderVm()
            : base
                (
                    IndexProvider.MakeTest(1),
                    Enumerable.Empty<ISorterPoolEntity>(),
                    Enumerable.Empty<ISwitchablePoolEntity>()
                )
        {
            Name = "DesignCompetePoolStepBuilderVm name";
            Description = "DesignCompetePoolStepBuilderVm description";
            GenerationCount = 75;
            MutationRate = 0.02;
            RandomSeedIn = 337;
            SorterChampCount = 33;
            SorterPoolSize = 99;
            SwitchableChampCount = 333;
            SwitchablePoolSize = 999;
        }
    }
}
