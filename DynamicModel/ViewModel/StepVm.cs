using System;
using System.Collections.ObjectModel;
using DynamicModel.Common;
using DynamicModel.Model;
using WpfUtils;
using WpfUtils.SelectableCollection;

namespace DynamicModel.ViewModel
{
    public interface IStepVm : ISelectableVm
    {
        string Description { get; set; }
        Guid Guid { get; }
        int Index { get; }
        ObservableCollection<IEntityVm> InputEntityVms { get; }
        ObservableCollection<IEntityVm> OutputEntityVms { get; }
        IRunAgent RunAgent { get; set; }
        CommandViewModel RunCommandVm { get; }
        string Name { get; set; }
        IStep Step { get; }
        string TypeName { get; }
        bool WasExecuted { get; }
    }

    public abstract class StepVm : ViewModelBase, IStepVm
    {
        protected StepVm(IStep step)
        {
            _step = step;
            _step.OnInputEntityAdded.Subscribe(MakeInputEntityVm);
            _step.OnOutputEntityAdded.Subscribe(MakeOutputEntityVm);
            _inputEntityVms = new ObservableCollection<IEntityVm>();
            _outputEntityVms = new ObservableCollection<IEntityVm>();
            foreach (var entity in Step.InputEntities)
            {
// ReSharper disable DoNotCallOverridableMethodsInConstructor
                MakeInputEntityVm(entity);
// ReSharper restore DoNotCallOverridableMethodsInConstructor
            }

            foreach (var entity in Step.OutputEntities)
            {
                // ReSharper disable DoNotCallOverridableMethodsInConstructor
                MakeOutputEntityVm(entity);
                // ReSharper restore DoNotCallOverridableMethodsInConstructor
            }
        }

        protected abstract void MakeInputEntityVm(IEntity entity);

        protected abstract void MakeOutputEntityVm(IEntity entity);

        private readonly IStep _step;
        public IStep Step
        {
            get { return _step; }
        }

        protected abstract bool CanRunExecute { get; }

        public string Description
        {
            get { return Step.Description; }
            set
            {
                Step.Description = value;
                OnPropertyChanged("Description");
            }
        }

        protected abstract void RunExecute();

        public Guid Guid { get { return Step.Guid;} }

        public int Index { get { return Step.Index; } }

        private readonly ObservableCollection<IEntityVm> _inputEntityVms;
        public ObservableCollection<IEntityVm> InputEntityVms
        {
            get { return _inputEntityVms; }
        }

        private readonly ObservableCollection<IEntityVm> _outputEntityVms;
        public ObservableCollection<IEntityVm> OutputEntityVms
        {
            get { return _outputEntityVms; }
        }

        public IRunAgent RunAgent { get; set; }

        public abstract CommandViewModel RunCommandVm { get; }

        public string Name
        {
            get { return Step.Name; }
            set
            {
                Step.Name = value;
                OnPropertyChanged("Name");
            }
        }

        public abstract string TypeName { get; }

        public bool WasExecuted { get { return Step.WasExecuted; } }

        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }
    }
}
