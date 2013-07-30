using DynamicModel.Common;
using GaSorter.ViewModel;
using SorterControls.DesignData.Steps;

namespace GaSorter.DesignData
{
    public class DesignSorterGaWorkflowVm : SorterGaWorkflowVmImpl
    {
        public DesignSorterGaWorkflowVm() : base
            (
                SortingNetworkDm.Workflows.SorterWorkflow.Create("DesignSorterGaWorkflowVm"), 
                EntityProvider.Make()
            )
        {
            StepVms.Items.Add(new DesignSorterPoolStepVm());
        }
    }
}
