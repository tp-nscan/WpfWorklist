using System;
using System.ComponentModel;
using System.Windows.Input;
using FirstFloor.ModernUI.Presentation;
using WorkflowWorklist.Models;

namespace WorkflowWorklist.ViewModels
{
    public enum WorkItemVmState
    {
        Cancelled = 0,
        Completed = 1,
        Error = 2,
        Running = 3,
        Scheduled = 4
    }

    public interface IWorkItemVm : INotifyPropertyChanged
    {
        ICommand Cancel { get; }
        bool Cancelled { get; }
        bool Completed { get; }
        Guid Guid { get; }
        bool HasError { get; }
        bool IsRunning { get; }
        string Message { get; set; }
        string Name { get; }
        string Status { get; }
        bool WasRun { get; }
        WorkItemVmState WorkItemVmState { get; set; }
    }

    public static class WorkItemVm
    {
        //public static WorkItemVmState ToWorkItemVmState(this WorklistEventType worklistEventType)
        //{
        //    switch (worklistEventType)
        //    {
        //        case WorklistEventType.ItemCancelled:
        //            break;
        //        case WorklistEventType.ItemCompleted:
        //            break;
        //        case WorklistEventType.ItemScheduled:
        //            break;
        //        case WorklistEventType.Started:
        //            break;
        //        case WorklistEventType.Stopped:
        //            break;
        //        default:
        //            break;
        //    }
        //}

        public static IWorkItemVm Make(IWorkItem workItem)
        {
            return new WorkItemVmImpl(workItem);
        }

        public static bool UnRunnable(this IWorkItemVm workItemVm)
        {
            return workItemVm.HasError || workItemVm.Completed || workItemVm.Cancelled;
        }
    }

    public class WorkItemVmImpl : NotifyPropertyChanged, IWorkItemVm
    {
        public WorkItemVmImpl(IWorkItem workItem)
        {
            WorkItemVmState = WorkItemVmState.Scheduled;
            _workItem = workItem;
            WorkItem.OnProgressChanged.Subscribe(WorkItem_ProgressChanged);
            WorkItem.OnWorkItemEvent.Subscribe(WorkItem_WorkItemEvemt);
        }

        void WorkItem_ProgressChanged(IProgressEventArgs e)
        {
            Message = e.Message;
        }

        void WorkItem_WorkItemEvemt(WorkItemEventArgs e)
        {
            switch (e.WorkItemEventType)
            {
                case WorkItemEventType.Cancelled:
                    WorkItemVmState = WorkItemVmState.Cancelled;
                    break;
                case WorkItemEventType.Error:
                    WorkItemVmState = WorkItemVmState.Error;
                    break;
                case WorkItemEventType.Completed:
                    WorkItemVmState = WorkItemVmState.Completed;
                    break;
                case WorkItemEventType.Started:
                    WorkItemVmState = WorkItemVmState.Running;
                    break;
            }
        }

        private readonly IWorkItem _workItem;

        IWorkItem WorkItem
        {
            get { return _workItem; }
        }

        public Guid Guid
        {
            get { return WorkItem.Guid; }
        }

        private string _message;
        public string Message
        {
            get { return _message; }
            set
            {
                if(_message == value) return;
                _message = value;
                OnPropertyChanged("Message");
            }
        }

        public string Name
        {
            get { return WorkItem.Name; }
        }

        public bool Cancelled
        {
            get { return _workItemVmState == WorkItemVmState.Cancelled; }
        }

        public bool Completed
        {
            get { return _workItemVmState == WorkItemVmState.Completed; }
        }

        public bool IsRunning
        {
            get { return _workItemVmState == WorkItemVmState.Running; }
        }

        public bool HasError
        {
            get { return _workItemVmState == WorkItemVmState.Error; }
        }

        private WorkItemVmState _workItemVmState;
        public WorkItemVmState WorkItemVmState
        {
            get { return _workItemVmState; }
            set
            {
                if (_workItemVmState == value) return;
                _workItemVmState = value;
                OnPropertyChanged("Cancelled");
                OnPropertyChanged("Completed");
                OnPropertyChanged("IsRunning");
                OnPropertyChanged("HasError");
                OnPropertyChanged("WasRun");
                OnPropertyChanged("Status");
                CommandManager.InvalidateRequerySuggested();
            }
        }

        public string Status
        {
            get { return WorkItemVmState.ToString(); }
        }

        public bool WasRun
        {
            get
            {
                return 
                    (_workItemVmState == WorkItemVmState.Error)
                    ||
                    (_workItemVmState == WorkItemVmState.Cancelled)
                    ||
                    (_workItemVmState == WorkItemVmState.Completed);
            }
        }

        private ICommand _camcel;
        public ICommand Cancel
        {
            get
            {
                return _camcel ?? (_camcel = new RelayCommand
                        (
                            o => WorkItem.Cancel(),
                            o => IsRunning
                        )
                    );
            }
        }
    }
}
