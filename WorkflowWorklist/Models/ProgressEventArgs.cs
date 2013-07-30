using System;

namespace WorkflowWorklist.Models
{
    public interface IProgressEventArgs
    {
        object Data { get; }
        string Message { get; }
        Guid TaskId { get; }
    }

    public static class ProgressEventArgs 
    {
        public static IProgressEventArgs Create(Guid taskId, string message, object data)
        {
            return new ProgressEventArgsEventArgsImpl(taskId, message, data);
        }
    }

    class ProgressEventArgsEventArgsImpl : EventArgs, IProgressEventArgs
    {
        public ProgressEventArgsEventArgsImpl(Guid taskId, string message, object data)
        {
            _taskId = taskId;
            _message = message;
            _data = data;
        }

        private readonly Guid _taskId;
        public Guid TaskId
        {
            get { return _taskId; }
        }

        private readonly object _data;
        public object Data
        {
            get { return _data; }
        }

        private readonly string _message;
        public string Message
        {
            get { return _message; }
        }
    }
}
