using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace DynamicModel.Common
{
    public interface IRunAgent : IRunClient<IRunMessage>, IRunServer<IRunMessage>
    {
    }

    public interface IRunClient<in T>
    {
        bool Continue { get; }
        bool WasStopped { set; }
        void Update(T result);
    }

    public interface IRunServer<out T>
    {
        bool IsRunning { get; }
        IObservable<T> OnReport { get; }
        void Stop();
    }

    public interface IRunMessage
    {
        string Message { get; }
        string Type { get; }
    }

    public static class RunAgent
    {
        public static IRunAgent MakeTest()
        {
            return new RunAgentImpl
                (
                    @continue: true
                );
        }
    }

    class RunAgentImpl : IRunAgent
    {
        public RunAgentImpl(bool @continue)
        {
            _continue = @continue;
        }

        private bool _continue;
        bool IRunClient<IRunMessage>.Continue
        {
            get { return _continue; }
        }

        bool IRunClient<IRunMessage>.WasStopped
        {
            set { _isRunning = value; }
        }

        void IRunClient<IRunMessage>.Update(IRunMessage result)
        {
            _onReport.OnNext(result);
        }

        private bool _isRunning;
        bool IRunServer<IRunMessage>.IsRunning
        {
            get { return _isRunning; }
        }

        private readonly Subject<IRunMessage> _onReport = new Subject<IRunMessage>();
        IObservable<IRunMessage> IRunServer<IRunMessage>.OnReport
        {
            get { return _onReport.AsObservable(); }
        }

        void IRunServer<IRunMessage>.Stop()
        {
            _continue = false;
        }
    }
}
