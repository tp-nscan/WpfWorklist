using System;
using System.Collections.Generic;
using System.Linq;
using DynamicModel.Model;

namespace SortingNetworkDm.Workflows
{
    public interface ISorterWorkflow : IWorkflow
    {
        void RemoveEntity(IEntity entity);
    }

    public static class SorterWorkflow
    {
        public static ISorterWorkflow Create
        (
            string name
        )
        {
            return new SorterWorkflowImpl
            (
                fileName: name,
                filePath: string.Empty,
                guid: Guid.NewGuid(),
                type: typeof(SorterWorkflow).Name,
                entities: Enumerable.Empty<IEntity>(),
                steps: Enumerable.Empty<IStep>()
            );
        }

        public static ISorterWorkflow Load
            (
                string name,
                string path,
                Guid guid,
                IEnumerable<IEntity> entities,
                IEnumerable<IStep> steps
            )
        {
            return new SorterWorkflowImpl
                (
                    fileName: name,
                    filePath: path,
                    guid: guid,
                    type: typeof(SorterWorkflow).Name,
                    entities: entities,
                    steps: steps
                );
        }
    }

    class SorterWorkflowImpl : WorkflowImpl, ISorterWorkflow
    {
        public SorterWorkflowImpl
            (
                string fileName, 
                string filePath, 
                Guid guid, 
                string type, 
                IEnumerable<IEntity> entities, 
                IEnumerable<IStep> steps) 
                : base(fileName, filePath, guid, type, entities, steps)
        {
        }

        public override string FileExtension
        {
            get { return "txt"; }
        }
    }
}
