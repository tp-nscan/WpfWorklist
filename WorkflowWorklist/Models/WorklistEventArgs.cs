namespace WorkflowWorklist.Models
{
    public class WorklistEventArgs
    {
        public WorklistEventArgs(Worklist worklist, IWorkItem workItem, WorklistEventType worklistEventType)
        {
            _worklist = worklist;
            _worklistEventType = worklistEventType;
            _workItem = workItem;
        }

        private readonly WorklistEventType _worklistEventType;
        public WorklistEventType WorklistEventType
        {
            get { return _worklistEventType; }
        }

        private readonly IWorkItem _workItem;
        public IWorkItem WorkItem
        {
            get { return _workItem; }
        }

        private readonly Worklist _worklist;
        public Worklist Worklist
        {
            get { return _worklist; }
        }
    }

    public enum WorklistEventType
    {
       ItemCancelled,
       ItemCompleted,
       ItemScheduled,
       Started,
       Stopped
    }
}
