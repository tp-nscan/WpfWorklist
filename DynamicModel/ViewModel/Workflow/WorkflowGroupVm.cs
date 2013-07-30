using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using DynamicModel.Common;
using DynamicModel.Model;
using WpfUtils;

namespace DynamicModel.ViewModel.Workflow
{
    public interface IWorkflowGroupVm : INotifyPropertyChanged
    {
        IWorkflowGroup WorkflowGroup { get; }
        ObservableCollection<IWorkflowVm> WorkflowVms { get; set; }
        ObservableCollection<IEntityVm> EntityVms { get; set; }
    }

    public static class WorkflowGroupVm
    {

    }

    public abstract class WorkflowGroupVmImpl : ViewModelBase, IWorkflowGroupVm
    {
        protected WorkflowGroupVmImpl(IWorkflowGroup workflowGroup)
        {
            _workflowGroup = workflowGroup;
            _workflowGroup.OnEntityAdded.Subscribe(EntityAdded);
            _workflowGroup.OnEntityRemoved.Subscribe(EntityRemoved);
            _workflowGroup.OnWorkflowAdded.Subscribe(WorkflowAdded);
            _workflowGroup.OnWorkflowRemoved.Subscribe(WorkflowRemoved);
        }

        protected abstract void EntityAdded(IEntity entity);

        protected abstract void EntityRemoved(IEntity entity);

        protected abstract void WorkflowAdded(IWorkflow workflow);

        protected abstract void WorkflowRemoved(IWorkflow workflow);


        private readonly IWorkflowGroup _workflowGroup;
        public IWorkflowGroup WorkflowGroup
        {
            get { return _workflowGroup; }
        }

        private ObservableCollection<IEntityVm> _entityVms 
            = new ObservableCollection<IEntityVm>();
        public ObservableCollection<IEntityVm> EntityVms
        {
            get { return _entityVms; }
            set { _entityVms = value; }
        }

        private ObservableCollection<IWorkflowVm> _workflowVms 
            = new ObservableCollection<IWorkflowVm>();
        public ObservableCollection<IWorkflowVm> WorkflowVms
        {
            get { return _workflowVms; }
            set { _workflowVms = value; }
        }
    }
}
