using System;
using System.Reactive.Subjects;
using System.Windows.Input;
using DynamicModel.Model;
using WpfUtils;
using WpfUtils.SelectableCollection;

namespace DynamicModel.ViewModel.Workflow
{
    public interface IWorkflowVm : IWorkspaceViewModel
    {
        bool Dirty { get; }
        Guid Guid { get; }
        string Name { get; set; }
        ICommand SaveWorkflow { get; }
        ICommand SaveWorkflowAs { get; }
        ICommand CloseWorkflow { get; }
        IWorkflow Workflow { get; }
        ObservableSelectableCollection<IStepVm> StepVms { get; set; }
        ObservableSelectableCollection<IEntityVm> EntityVms { get; set; }
    }

    public static class WorkflowVm
    {
        public static bool FileWasSaved(this IWorkflow workflow)
        {
            return !String.IsNullOrEmpty(workflow.FilePath);
        }
    }

    public abstract class WorkflowVmImpl : WorkspaceViewModel, IWorkflowVm
    {
        protected WorkflowVmImpl(IWorkflow workflow)
        {
            _workflow = workflow;

            _workflow.OnEntityAdded.Subscribe(AddEntity);
            _workflow.OnEntityRemoved.Subscribe();
            _workflow.OnStepAdded.Subscribe(AddStep);
            _workflow.OnStepRemoved.Subscribe();
        }

        protected abstract IEntityVm MakeEntityVm(IEntity entity);
        protected abstract IStepVm MakeStepVm(IStep step);

        void AddEntity(IEntity entity)
        {
            var entityVm = MakeEntityVm(entity);
            EntityVms.Items.Add(entityVm);
        }

        void AddStep(IStep step)
        {
            var stepVm = MakeStepVm(step);
            StepVms.Items.Add(stepVm);
        }

        readonly Subject<IStep> _stepCreated = new Subject<IStep>();
        public IObservable<IStep> OnStepCreated
        {
            get { return _stepCreated; }
        }

        public bool Dirty
        {
            get { return Workflow.Dirty; }
            protected set
            {
                Workflow.Dirty = value;
                OnPropertyChanged("Dirty");
            }
        }

        public Guid Guid
        {
            get { return Workflow.Guid; }
        }

        public string Name
        {
            get { return Workflow.FileName; }
            set
            {
                Workflow.FileName = value;
                Dirty = true;
                OnPropertyChanged("Name");
            }
        }

        readonly IWorkflow _workflow;
        public IWorkflow Workflow
        {
            get { return _workflow; }
        }

        private ObservableSelectableCollection<IStepVm> _stepVms = new ObservableSelectableCollection<IStepVm>();
        public ObservableSelectableCollection<IStepVm> StepVms
        {
            get { return _stepVms; }
            set { _stepVms = value; }
        }

        private ObservableSelectableCollection<IEntityVm> _entityVms = new ObservableSelectableCollection<IEntityVm>();
        public ObservableSelectableCollection<IEntityVm> EntityVms
        {
            get { return _entityVms; }
            set { _entityVms = value; }
        }


        #region SaveWorkflow Command

        RelayCommand _saveWorspaceCommand;

        public ICommand CloseWorkflow
        {
            get
            {
                return _saveWorspaceCommand ?? (_saveWorspaceCommand
                    = new RelayCommand
                        (
                            param => OnCloseWorkflow(),
                            param => CanCloseWorkflow()
                        ));
            }
        }

        protected virtual void OnCloseWorkflow()
        {
            Workflow.Close();
        }

        protected abstract bool CanCloseWorkflow();


        #endregion // SaveWorkflow Command
    

        public abstract ICommand SaveWorkflow { get; }

        public abstract ICommand SaveWorkflowAs { get; }
    }
}
