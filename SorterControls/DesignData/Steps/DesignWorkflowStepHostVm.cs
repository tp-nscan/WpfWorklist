using SorterControls.ViewModels.Steps;
using SortingNetworkDm.TestData;

namespace SorterControls.DesignData.Steps
{
    public class DesignWorkflowStepHostVm : WorkflowStepHostVm
    {
        public DesignWorkflowStepHostVm()
        {
            Items.Add(SwitchablePoolStepVm.Make(TestSteps.TheInitializedSwitchablePoolStep));
            Items.Add(SorterPoolStepVm.Make(TestSteps.TheInitializedSorterPoolStep));
            Items.Add(SorterPoolStepVm.Make(TestSteps.TheCompletedSorterPoolStep));
            Items.Add(CompetePoolStepVm.Make(TestSteps.TheCompletedCompetePoolStep));
        }
    }
}
