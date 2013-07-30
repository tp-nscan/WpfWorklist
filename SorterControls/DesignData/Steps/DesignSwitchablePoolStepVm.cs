using System;
using SortNetwork.Switchables;
using SorterControls.ViewModels.Steps;
using SortingNetworkDm.TestData;

namespace SorterControls.DesignData.Steps
{
    public class DesignSwitchablePoolStepVm : SwitchablePoolStepVmImpl
    {
        public DesignSwitchablePoolStepVm() : base
            (
                TestSteps.TheCompletedSwitchablePoolStep
            )
        {
        }
    }
}
