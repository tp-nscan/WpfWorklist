using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using DynamicModel.Common;

namespace DynamicModel.Model
{
    public interface IStep
    {
        bool CanExecute { get; }
        string Description { get; set; }
        bool Dirty { get; set; }
        void Execute(IRunAgent runAgent);
        Guid Guid { get; }
        int Index { get; }
        IEnumerable<IEntity> InputEntities { get; }
        IObservable<IEntity> OnInputEntityAdded { get; }
        IEnumerable<IEntity> OutputEntities { get; }
        IObservable<IEntity> OnOutputEntityAdded { get; }
        string Name { get; set; }
        string TypeName { get; }
        bool WasExecuted { get; }
    }

    public static class Step
    {
        public static IStep MakeTest
            (
                string name,
                int index,
                IEnumerable<IEntity> inputEntities = null,
                IEnumerable<IEntity> outputEntities = null
            )
        {
            return new StepTestImpl
                (
                    guid: Guid.NewGuid(),
                    name: name,
                    index: index,
                    inputEntities: inputEntities,
                    outputEntities: outputEntities
                );
        }

        public static bool CanExecute(this IStep step)
        {
            return 
                (!step.InputEntities.Any()) 
                ||
                step.InputEntities.All(T => T.IsLoaded);
        }

        public static bool WasExecuted(this IStep step)
        {
            return step.OutputEntities.All(T => T.IsLoaded);
        }

        public static bool ContainsEntity(this IStep step, IEntity entity)
        {
            return step.AllEntities().Any(T => T.Guid == entity.Guid);
        }

        public static IEnumerable<IEntity> AllEntities(this IStep step)
        {
            return step.InputEntities.Union(step.OutputEntities);
        }

    }

    /// <summary>
    /// For unloaded output entities maintain a list of their guids.
    /// </summary>
    public abstract class StepImpl : IStep
    {
        protected StepImpl
        (
            Guid guid,
            int index, 
            IEnumerable<IEntity> inputEntities,
            IEnumerable<IEntity> outputEntities = null
        )
        {
            _guid = guid;
            _index = index;

            if (inputEntities != null)
            {
                foreach (var inputEntity in inputEntities)
                {
                    AddInputEnity(inputEntity);
                } 
            }

            if (outputEntities != null)
            {
                foreach (var outputEntity in outputEntities)
                {
                    AddOutputEnity(outputEntity);
                }
            }
        }

        readonly Dictionary<IEntity, IDisposable> _subscriptions = new Dictionary<IEntity, IDisposable>();

        protected void AddInputEnity(IEntity entity)
        {
            _subscriptions[entity] = entity.OnEntityChanged.Subscribe(p => Dirty = true);
            _inputEntities.Add(entity);
            _inputEntityAdded.OnNext(entity);
            Dirty = true;
        }

        protected void AddOutputEnity(IEntity entity)
        {
            _subscriptions[entity] = entity.OnEntityChanged.Subscribe(p => Dirty = true);
            _outputEntities.Add(entity);
            _outputEntityAdded.OnNext(entity);
            Dirty = true;
        }

        public bool CanExecute
        {
            get { return ( ! WasExecuted ) && InputEntities.All(T => T.IsLoaded); }
        }

        public abstract string Description { get; set; }

        public bool Dirty
        {
            get; set; 
        }

        private readonly Subject<IEntity> _outputEntityAdded = new Subject<IEntity>();
        public IObservable<IEntity> OnOutputEntityAdded
        {
            get { return _outputEntityAdded; }
        }

        public abstract string Name { get; set; }

        public abstract void Execute(IRunAgent runAgent);

        private readonly Guid _guid;
        public Guid Guid
        {
            get { return _guid; }
        }

        private readonly int _index;

        public int Index
        {
            get { return _index; }
        }

        public abstract string TypeName { get; }

        public abstract bool WasExecuted { get; }

        readonly List<IEntity> _inputEntities = new List<IEntity>();
        public IEnumerable<IEntity> InputEntities { get { return _inputEntities; } }

        private readonly Subject<IEntity> _inputEntityAdded = new Subject<IEntity>();
        public IObservable<IEntity> OnInputEntityAdded
        {
            get { return _inputEntityAdded; }
        }

        readonly List<IEntity> _outputEntities = new List<IEntity>();
        public IEnumerable<IEntity> OutputEntities { get { return _outputEntities; } }

    }

    public class StepTestImpl : StepImpl
    {
        private string _description;
        private string _name;

        public StepTestImpl
            (
                Guid guid, 
                string name,
                int index, 
                IEnumerable<IEntity> inputEntities, 
                IEnumerable<IEntity> outputEntities = null)
            : base(guid , index, inputEntities, outputEntities)
        {
            _name = name;
            _description = string.Empty;
        }

        public override string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                Dirty = true;
            }
        }

        public override string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                Dirty = true;
            }
        }

        public override void Execute(IRunAgent runAgent)
        {
            AddOutputEnity(Entity.MakeTest("first_out"));
        }

        public override string TypeName
        {
            get { return "StepTestImpl"; }
        }

        public override bool WasExecuted
        {
            get { return false; }
        }
    }
}
