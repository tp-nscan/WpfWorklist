using System;
using System.Collections.Generic;
using System.Linq;
using DynamicModel.Common;
using DynamicModel.Model;
using Newtonsoft.Json;
using SortingNetworkDm.Json.Entities;
using SortingNetworkDm.Json.Steps;
using SortingNetworkDm.Workflows;

namespace SortingNetworkDm.Json.Workflows
{
    public class SorterWorkflowToJson
    {
        public static SorterWorkflowToJson ToJson(ISorterWorkflow sorterWorkflow)
        {
            return new SorterWorkflowToJson
            {
                FileName = sorterWorkflow.FileName,
                FilePath = sorterWorkflow.FilePath,
                Guid = sorterWorkflow.Guid,
                Steps = sorterWorkflow.Steps.Select(t=>t.ToJson()).ToList(),
                Entities = sorterWorkflow.Entities.Select(t=>t.ToJson()).ToList()
            };
        }

        public static ISorterWorkflow ToSorterWorkflow(SorterWorkflowToJson sorterWorkflowToJson)
        {
            var entityProviderFromWorkflow = new EntityProviderFromWorkflow(sorterWorkflowToJson);

            return SorterWorkflow.Load
                (
                    name: sorterWorkflowToJson.FileName,
                    path: sorterWorkflowToJson.FilePath,
                    guid: sorterWorkflowToJson.Guid,
                    entities: entityProviderFromWorkflow.Entities,
                    steps: sorterWorkflowToJson.Steps.Select
                    (
                        T=>
                        ConvertToStep(T, entityProviderFromWorkflow)
                    )
                );
        }

        public static IStep ConvertToStep(object jsonObj, IEntityProvider entityProvider)
        {
            var type = jsonObj.GetType();

            if (type == typeof(CompetePoolStepToJson))
            {
                var sba = (CompetePoolStepToJson)jsonObj;
                return CompetePoolStepToJson.ToCompetePoolStep(sba, entityProvider);
            }
            if (type == typeof(SorterPoolStepToJson))
            {
                var sba = (SorterPoolStepToJson)jsonObj;
                return SorterPoolStepToJson.ToSorterPoolStep(sba, entityProvider);
            }
            if (type == typeof(SwitchablePoolStepToJson))
            {
                var sba = (SwitchablePoolStepToJson)jsonObj;
                return SwitchablePoolStepToJson.ToSwitchablePoolStep(sba, entityProvider);
            }

            throw new ArgumentException("cant convert to IStep");
        }

        public string FileName { get; set; }

        public string FilePath { get; set; }

        public Guid Guid { get; set; }

        private List<object> _steps = new List<object>();
        [JsonConverter(typeof(JsonConverterForSteps))]
        public List<object> Steps
        {
            get { return _steps; }
            set { _steps = value; }
        }

        private List<object> _entities = new List<object>();
        [JsonConverter(typeof(JsonConverterForEntities))]
        public List<object> Entities
        {
            get { return _entities; }
            set { _entities = value; }
        }
    }

    class EntityProviderFromWorkflow : IEntityProvider
    {
        public EntityProviderFromWorkflow(SorterWorkflowToJson sorterWorkflowToJson)
        {
            _entities = sorterWorkflowToJson.Entities.Select(ConvertToEntity).ToList();
        }

        private readonly List<IEntity> _entities;

        public IEnumerable<IEntity> Entities
        {
            get { return _entities; }
        }

        public void AddEntity(IEntity entity)
        {
            throw new NotImplementedException();
        }

        static IEntity ConvertToEntity(object jsonObj)
        {
            var type = jsonObj.GetType();

            if (type == typeof(SorterResultPoolEntityToJson))
            {
                var sba = (SorterResultPoolEntityToJson)jsonObj;
                return SorterResultPoolEntityToJson.ToSorterResultPoolEntity(sba);
            }
            if (type == typeof(SorterPoolEntityToJson))
            {
                var sba = (SorterPoolEntityToJson)jsonObj;
                return SorterPoolEntityToJson.ToSorterPoolEntity(sba);
            }
            if (type == typeof(SwitchablePoolEntityToJson))
            {
                var sba = (SwitchablePoolEntityToJson)jsonObj;
                return SwitchablePoolEntityToJson.ToSwitchablePoolEntity(sba);
            }

            throw new ArgumentException("cant convert to IEntity");
        }
    }
}
