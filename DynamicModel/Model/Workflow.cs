using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using DynamicModel.Common;

namespace DynamicModel.Model
{
    public interface IWorkflow : IIndexProvider, IEntityProvider
    {
        void AddStep(IStep step);
        void Close();
        bool Dirty { get; set; }
        string FileExtension { get; }
        string FileName { get; set; }
        string FilePath { get; set; }
        Guid Guid { get; }
        IObservable<IEntity> OnEntityAdded { get; }
        IObservable<IEntity> OnEntityRemoved { get; }
        IObservable<IWorkflow> OnRequestClose { get; }
        IObservable<IStep> OnStepAdded { get; }
        IObservable<IStep> OnStepRemoved { get; }
        IEnumerable<IStep> Steps { get; }
        void RemoveStep(IStep step);
        string Type { get; }
    }

    public static class Workflow
    {
        public static string FilePathNameExtenstion(this IWorkflow workflow)
        {
            return workflow.FilePath + 
                   System.IO.Path.DirectorySeparatorChar + 
                   workflow.FileName + 
                   "." +
                   workflow.FileExtension;
        }

        public static int EntityUsageCount(this IWorkflow workflow, IEntity entity)
        {
            return workflow.Steps.SelectMany(s => s.AllEntities())
                .Count(e => e.Guid == entity.Guid);
        }

        public static IWorkflow MakeTest
            (
                string name,
                IEnumerable<IEntity> modelEntities = null,
                IEnumerable<IStep> modelSteps = null
            )
        {
            return new WorkflowTestImpl
                (
                    name,
                    modelEntities,
                    modelSteps
                );
        }
    }

    public abstract class WorkflowImpl : IWorkflow
    {
        private readonly Guid _guid;
        private readonly string _type;

        protected WorkflowImpl
            (
                string fileName,
                string filePath,
                Guid guid, 
                string type, 
                IEnumerable<IEntity> entities,
                IEnumerable<IStep> steps 
            )
        {
            FileName = fileName;
            FilePath = filePath;
            _guid = guid;
            _type = type;
            _entities.AddRange(entities ?? Enumerable.Empty<IEntity>());
            _steps.AddRange(steps ?? Enumerable.Empty<IStep>());
        }

        public void AddEntity(IEntity entity)
        {
            if (_entities.Any(T => T.Guid == entity.Guid))
            {
                return;
            }
            _entities.Add(entity);
            _entityAdded.OnNext(entity);
        }

        public void RemoveEntity(IEntity entity)
        {
            _entities.Remove(entity);
            _entityRemoved.OnNext(entity);
        }

        readonly Dictionary<IStep, IDisposable> _stepSubscriptions 
            = new Dictionary<IStep, IDisposable>();

        public void AddStep(IStep step)
        {
            _steps.Add(step);
            _stepSubscriptions[step] = step.OnOutputEntityAdded.Subscribe(AddEntity);
            _stepAdded.OnNext(step);

            foreach (var entity in step.AllEntities())
            {
                AddEntity(entity);
            }

            Dirty = true;
        }

        public void Close()
        {
            _onRequestClose.OnNext(this);
        }

        private bool _dirty;
        public bool Dirty 
        {
            get { return _dirty || _steps.Any(s => s.Dirty); }
            set
            {
                _dirty = value;
                if (! value)
                {
                    _steps.ForEach(s => s.Dirty = false);
                }
            }
        }

        public void RemoveStep(IStep step)
        {
            foreach (var entity in step.AllEntities()
                        .Where(entity => this.EntityUsageCount(entity) < 2))
            {
                RemoveEntity(entity);
            }

            var subscription = _stepSubscriptions[step];
            _stepSubscriptions.Remove(step);
            subscription.Dispose();
            _steps.Remove(step);
            Dirty = true;
        }

        public abstract string FileExtension { get; }

        public string FilePath { get; set; }

        public Guid Guid
        {
            get { return _guid; }
        }

        public string FileName { get; set; }

        public string Type
        {
            get { return _type; }
        }

        private readonly List<IEntity> _entities = new List<IEntity>();
        public IEnumerable<IEntity> Entities
        {
            get { return _entities; }
        }

        private readonly Subject<IEntity> _entityAdded = new Subject<IEntity>();
        public IObservable<IEntity> OnEntityAdded
        {
            get { return _entityAdded; }
        }

        private readonly Subject<IEntity> _entityRemoved = new Subject<IEntity>();
        public IObservable<IEntity> OnEntityRemoved
        {
            get { return _entityRemoved; }
        }

        private readonly Subject<IWorkflow> _onRequestClose = new Subject<IWorkflow>();
        public IObservable<IWorkflow> OnRequestClose
        {
            get { return _onRequestClose; }
        }

        private readonly List<IStep> _steps = new List<IStep>();
        public IEnumerable<IStep> Steps
        {
            get { return _steps; }
        }

        private readonly Subject<IStep> _stepAdded = new Subject<IStep>();
        public IObservable<IStep> OnStepAdded
        {
            get { return _stepAdded; }
        }

        private readonly Subject<IStep> _stepRemoved = new Subject<IStep>();
        public IObservable<IStep> OnStepRemoved
        {
            get { return _stepRemoved; }
        }

        public int MakeIndex()
        {
            return Steps.Count();
        }
    }

    class WorkflowTestImpl : WorkflowImpl
    {
        public WorkflowTestImpl
            (
            string name,
            IEnumerable<IEntity> entities,
            IEnumerable<IStep> steps
            )
            : base
                (
                    fileName: name,
                    filePath: string.Empty,
                    guid: Guid.NewGuid(),
                    type:"test", 
                    entities: entities, 
                    steps: steps
                )
        {

        }
        public override string FileExtension
        {
            get { return string.Empty; }
        }
    }
}