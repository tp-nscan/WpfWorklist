using WorkflowWorklist.Models;

namespace WorkflowWorklist.ViewModels.Design
{
    public class DesignWorkItemVm : WorkItemVmImpl
    {
        public DesignWorkItemVm()
            : base(_workItem)
        {
        }

        private static readonly IWorkItem _workItem = IterativeWorkItem.Make<string>
            (
                name: "design name",
                initialConditon: "initial condition",
                updateOperation: s=>s,
                totalIterations: 5
            );
    }
}
