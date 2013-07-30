using System.Linq;
using DynamicModel.Model;
using DynamicModel.ViewModel.Workflow;
using SortingNetworkDm.Workflows;

namespace GaSorter.ViewModel
{
    public interface ISorterGaWorkflowGroupVm : IWorkflowGroupVm
    {
    
    }

    public static class SorterGaWorkflowGroupVm
    {
        public static ISorterGaWorkflowGroupVm Make(IWorkflowGroup workflowGroup)
        {
            return new SorterGaWorkflowGroupVmImpl(workflowGroup);
        }
    }

    class SorterGaWorkflowGroupVmImpl : WorkflowGroupVmImpl, ISorterGaWorkflowGroupVm
    {
        public SorterGaWorkflowGroupVmImpl(IWorkflowGroup workflowGroup)
            : base(workflowGroup)
        {

        }

        protected override void EntityAdded(IEntity entity)
        {
            EntityVms.Add(entity.ToEntityVm());
        }

        protected override void EntityRemoved(IEntity entity)
        {
            EntityVms.Remove(EntityVms.Single(t => t.Guid == entity.Guid));
        }

        protected override void WorkflowAdded(IWorkflow workflow)
        {
            WorkflowVms.Add(SorterGaWorkflowVm.Make((ISorterWorkflow) workflow, WorkflowGroup));
        }

        protected override void WorkflowRemoved(IWorkflow workflow)
        {
            WorkflowVms.Remove(WorkflowVms.Single(w => w.Guid == workflow.Guid));
        }

    }
}
