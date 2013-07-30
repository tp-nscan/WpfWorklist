using System;
using System.ComponentModel;
using System.Reactive.Subjects;
using DynamicModel.Common;
using DynamicModel.Model;
using WpfUtils;

namespace SorterControls.ViewModels.Bulders
{
    public interface IWorkflowStepBuilderVm : INotifyPropertyChanged
    {
        CommandViewModel BuildCommandVm { get; }
        Guid ModelGuid { get; }
        IIndexProvider MyIndexProvider { get; }
        IObservable<IStep> OnModelStepCreated { get; }
        string TypeName { get; }
    }

    public abstract class StepBuilderVm : ViewModelBase, IWorkflowStepBuilderVm
    {
        protected StepBuilderVm(IIndexProvider indexProvider)
        {
            _myIndexProvider = indexProvider;
        }


        public abstract CommandViewModel BuildCommandVm { get; }

        private Guid? _modelGuid;
        public Guid ModelGuid
        {
            get
            {
                if (! _modelGuid.HasValue)
                {
                    _modelGuid = Guid.NewGuid();
                }
                return _modelGuid.Value;
            }
        }

        private readonly IIndexProvider _myIndexProvider;
        public IIndexProvider MyIndexProvider
        {
            get { return _myIndexProvider; }
        }

        protected readonly Subject<IStep>  ModelStepCreated = new Subject<IStep>();
        public IObservable<IStep> OnModelStepCreated
        {
            get { return ModelStepCreated; }
        }

        public abstract string TypeName { get; }

    }
}
