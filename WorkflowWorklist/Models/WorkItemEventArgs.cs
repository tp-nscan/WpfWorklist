using System;

namespace WorkflowWorklist.Models
{
    public class WorkItemEventArgs : EventArgs
    {
        public WorkItemEventArgs(IWorkItem workItem, WorkItemEventType workItemEventType)
        {
            _workItem = workItem;
            _workItemEventType = workItemEventType;
        }

        private readonly IWorkItem _workItem;
        public IWorkItem WorkItem
        {
            get { return _workItem; }
        }

        private readonly WorkItemEventType _workItemEventType;
        public WorkItemEventType WorkItemEventType
        {
            get { return _workItemEventType; }
        }
    }

    public enum WorkItemEventType
    {
        Cancelled,
        Completed,
        Error,
        Started
    }
}
