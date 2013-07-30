using System;
using System.Threading;
using System.Threading.Tasks;

namespace WorkflowWorklist.Models
{
    public interface IWorkItem
    {
        Guid Guid { get; }
        string Name { get; }
        IObservable<WorkItemEventArgs> OnWorkItemEvent { get; }
        IObservable<IProgressEventArgs> OnProgressChanged { get; }
        void RaiseWorkItemEvent(WorkItemEventType workItemEventType);
        object Result { get; }
        Task<object> RunAsync();
        void Cancel();
    }
}