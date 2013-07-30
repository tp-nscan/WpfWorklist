using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using DynamicModel.Common;

namespace DynamicModel.Model
{
    public interface IWorkflowGroup : IEntityProvider
    {
        void AddWorkflow(IWorkflow workflow);
        void RemoveWorkflow(IWorkflow step);
        IObservable<IEntity> OnEntityAdded { get; }
        IObservable<IEntity> OnEntityRemoved { get; }
        IEnumerable<IWorkflow> Workflows { get; }
        IObservable<IWorkflow> OnWorkflowAdded { get; }
        IObservable<IWorkflow> OnWorkflowRemoved { get; }
    }

    public static class WorkflowGroup
    {
        public static IWorkflowGroup Make()
        {
            return new WorkflowGroupImpl();
        }
    }

    class WorkflowGroupImpl : IWorkflowGroup
    {
        public WorkflowGroupImpl()
        {
            _entities = new List<IEntity>();
        }


        readonly Dictionary<IWorkflow, IDisposable> _workflowRemoveSubscriptions
            = new Dictionary<IWorkflow, IDisposable>();

        readonly Dictionary<IWorkflow, IDisposable> _workflowEntityAddedSubscriptions
            = new Dictionary<IWorkflow, IDisposable>();

        public void AddWorkflow(IWorkflow workflow)
        {
            _workflows.Add(workflow);

            _workflowEntityAddedSubscriptions[workflow] =
                    workflow.OnEntityAdded.Subscribe(AddEntity);

            _workflowRemoveSubscriptions[workflow] = 
                    workflow.OnRequestClose.Subscribe(RemoveWorkflow);

            _onWorkflowAdded.OnNext(workflow);

            foreach (var entity in workflow.Entities)
            {
                AddEntity(entity);
            }
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

        public void RemoveWorkflow(IWorkflow workflow)
        {
            var otherWorkflowEntities = _workflows.Except(new[] {workflow})
                                            .SelectMany(T=>T.Entities).ToList();

            foreach (var entity in workflow.Entities
                        .Where(entity => ! otherWorkflowEntities.Contains(entity)))
            {
                RemoveEntity(entity);
            }

            _onWorkflowRemoved.OnNext(workflow);

            var entityAddedSubscription = _workflowEntityAddedSubscriptions[workflow];
            _workflowEntityAddedSubscriptions.Remove(workflow);
            entityAddedSubscription.Dispose();

            var workflowRemoveSubscription = _workflowRemoveSubscriptions[workflow];
            _workflowRemoveSubscriptions.Remove(workflow);
            workflowRemoveSubscription.Dispose();

            _workflows.Remove(workflow);
        }

        void RemoveEntity(IEntity entity)
        {
            _entities.Remove(entity);
            _entityRemoved.OnNext(entity);
        }

        private readonly List<IEntity> _entities;
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

        private readonly List<IWorkflow> _workflows = new List<IWorkflow>();
        public IEnumerable<IWorkflow> Workflows
        {
            get { return _workflows; }
        }

        private readonly Subject<IWorkflow> _onWorkflowAdded = new Subject<IWorkflow>();
        public IObservable<IWorkflow> OnWorkflowAdded
        {
            get { return _onWorkflowAdded; }
        }

        private readonly Subject<IWorkflow> _onWorkflowRemoved = new Subject<IWorkflow>();
        public IObservable<IWorkflow> OnWorkflowRemoved
        {
            get { return _onWorkflowRemoved; }
        }
    }
}
