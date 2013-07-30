using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Reactive.Subjects;
using System.Threading.Tasks;

namespace WorkflowWorklist.Models
{
    [Export]
    public class Worklist : IDisposable
    {
        readonly Subject<WorklistEventArgs> _worklistEvent = new Subject<WorklistEventArgs>();
        public IObservable<WorklistEventArgs> OnWorklistEvent { get { return _worklistEvent; } }

        readonly ConcurrentQueue<IWorkItem> _queue;

        public Worklist()
        {
            _queue = new ConcurrentQueue<IWorkItem>();
        }

        public IEnumerable<IWorkItem> WorkItems
        {
            get
            {
                if (CurrentWorkItem != null)
                {
                    yield return CurrentWorkItem;
                }
                foreach (var workItem in _queue)
                {
                    yield return workItem;
                }
            }
        }

        public void Push(IWorkItem workItem)
        {
            _queue.Enqueue(workItem);
            _worklistEvent.OnNext(new WorklistEventArgs(this, workItem, WorklistEventType.ItemScheduled));
        }

        public void CancelCurrent()
        {
            if (CurrentWorkItem == null)
                return;

            CurrentWorkItem.Cancel();
        }

        public void CancelAll()
        {
            if (!IsRunning) return;

            Stop();

            IWorkItem workItem;
            while (_queue.TryDequeue(out workItem))
            {
                _worklistEvent.OnNext(new WorklistEventArgs(this, workItem, WorklistEventType.ItemCancelled));
            }
        }

        private bool _isRunning;
        public bool IsRunning
        {
            get { return _isRunning; }
        }

        public void Start()
        {
            _isRunning = true;
            QueueHandler();
        }

        public void Stop()
        {
            CancelCurrent();
            _isRunning = false;
        }
        
        private IWorkItem _currentWorkItem;
        private IWorkItem CurrentWorkItem
        {
            get { return _currentWorkItem; }
        }

        async void QueueHandler()
        {
            _worklistEvent.OnNext(new WorklistEventArgs(this, null, WorklistEventType.Started));
            do
            {
                if (!_queue.TryDequeue(out _currentWorkItem))
                {
                    _isRunning = false;
                    break;
                }

                try
                {
                    await CurrentWorkItem.RunAsync();
                    _worklistEvent.OnNext(new WorklistEventArgs(this, CurrentWorkItem, WorklistEventType.ItemCompleted));
                }
                catch (TaskCanceledException)
                {
                    CurrentWorkItem.RaiseWorkItemEvent(WorkItemEventType.Cancelled);
                    continue;
                }
                catch (Exception)
                {
                    CurrentWorkItem.RaiseWorkItemEvent(WorkItemEventType.Error);
                }

                _currentWorkItem = null;
            }

            while (IsRunning);

            _worklistEvent.OnNext(new WorklistEventArgs(this, null, WorklistEventType.Stopped));
        }

        public void Dispose()
        {
            CancelAll();
        }
    }
}
