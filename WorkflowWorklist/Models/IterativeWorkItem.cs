using System;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;

namespace WorkflowWorklist.Models
{
    public interface IIterativeWorkItem<out T> : IWorkItem
    {
        T InitialConditon { get; }
        T CurrentConditon { get; }
        int TotalIterations { get; }
        int CurrentIteration { get; }
    }

    public static class IterativeWorkItem
    {
         public static IIterativeWorkItem<T> Make<T>(string name, T initialConditon, Func<T, T> updateOperation, int totalIterations)
         {
             return new IterativeWorkItemImpl<T>
                 (
                    name: name, 
                    initialConditon: initialConditon, 
                    updateOperation: updateOperation, 
                    totalIterations: totalIterations
                 );
         }

        public static IIterativeWorkItem<string> Test(int index)
        {
            return new IterativeWorkItemImpl<string>
                 (
                    name: "name_" + index,
                    initialConditon: "initialConditon",
                    updateOperation: s=>s+"_step",
                    totalIterations: 5
                 );
        }
    }

    class IterativeWorkItemImpl<T> : IIterativeWorkItem<T>
    {
        public IterativeWorkItemImpl(string name, T initialConditon, Func<T, T> updateOperation, int totalIterations)
        {
            _name = name;
            _guid = Guid.NewGuid();
            _totalIterations = totalIterations;
            _updateOperation = updateOperation;
            _initialConditon = initialConditon;
        }

        private readonly T _initialConditon;
        public T InitialConditon
        {
            get { return _initialConditon; }
        }

        public T CurrentConditon { get; private set; }

        private readonly int _totalIterations;
        public int TotalIterations
        {
            get { return _totalIterations; }
        }

        private readonly Func<T, T> _updateOperation;
        public Func<T, T> UpdateOperation
        {
            get { return _updateOperation; }
        }

        private int _currentIteration;
        public int CurrentIteration
        {
            get { return _currentIteration; }
        }

        private readonly Guid _guid;
        public Guid Guid
        {
            get { return _guid; }
        }

        private readonly string _name;
        public string Name
        {
            get { return _name; }
        }

        private readonly Subject<WorkItemEventArgs> _onWorkItemEvent = new Subject<WorkItemEventArgs>();
        public IObservable<WorkItemEventArgs> OnWorkItemEvent
        {
            get { return _onWorkItemEvent; }
        }

        private readonly Subject<IProgressEventArgs> _progressChanged = new Subject<IProgressEventArgs>();

        public IObservable<IProgressEventArgs> OnProgressChanged
        {
            get { return _progressChanged; }
        }

        public void RaiseWorkItemEvent(WorkItemEventType workItemEventType)
        {
            _onWorkItemEvent.OnNext(new WorkItemEventArgs(this, workItemEventType));
        }

        public object Result
        {
            get { return CurrentIteration; }
        }

        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        public CancellationTokenSource CancellationTokenSource
        {
            get { return _cancellationTokenSource; }
        }

        public async Task<object> RunAsync()
        {
            CurrentConditon = InitialConditon;
            _onWorkItemEvent.OnNext(new WorkItemEventArgs(this, WorkItemEventType.Started));

            for (_currentIteration = 0; (_currentIteration < TotalIterations); _currentIteration++)
            {
                await Task.Run
                (
                    () => CurrentConditon = UpdateOperation(CurrentConditon)
                    ,
                    CancellationTokenSource.Token
                );

                _progressChanged.OnNext (
                        ProgressEventArgs.Create
                        (
                            taskId: Guid,
                            message: string.Format("Step {0} of {1} completed", _currentIteration + 1, TotalIterations),
                            data: CurrentConditon
                        ));
            }

            _onWorkItemEvent.OnNext(new WorkItemEventArgs(this, WorkItemEventType.Completed));

            return CurrentConditon;
        }

        public void Cancel()
        {
            CancellationTokenSource.Cancel();
        }
    }
}
