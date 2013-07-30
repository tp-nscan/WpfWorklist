using System;
using SorterControls.ViewModels.Steps;

namespace SorterControls.DesignData.Steps
{
    public class DesignSorterPoolStepVm : SorterPoolStepVmImpl
    {
        public DesignSorterPoolStepVm() : base
            (
                SortingNetworkDm.Steps.SorterPoolStep.Load
                (
                    guid: Guid.NewGuid(),
                    name: "name of DesignSorterPoolStepVm",
                    description: "description for DesignSorterPoolStepVm",
                    index: 1,
                    outputSorters: null,
                    keyCount: 16,
                    seedIn: 33,
                    sorterCount: 100,
                    switchesPerSorter: 100
                )
            )
        {
        }

    }
}
