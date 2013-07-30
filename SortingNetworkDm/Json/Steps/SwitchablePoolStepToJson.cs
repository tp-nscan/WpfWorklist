using System;
using System.Linq;
using DynamicModel.Common;
using SortNetwork.Switchables;
using SortingNetworkDm.Entities;
using SortingNetworkDm.Steps;

namespace SortingNetworkDm.Json.Steps
{
    public class SwitchablePoolStepToJson
    {
        public static SwitchablePoolStepToJson ToJson(ISwitchablePoolStep switchablePoolStep)
        {
            return new SwitchablePoolStepToJson
            {
                Description = switchablePoolStep.Description,
                Guid = switchablePoolStep.Guid,
                Index = switchablePoolStep.Index,
                Name = switchablePoolStep.Name,
                TypeName = switchablePoolStep.TypeName,

                KeyCount = switchablePoolStep.KeyCount,
                SeedIn = switchablePoolStep.SeedIn,
                SeedOut = switchablePoolStep.SeedOut,
                SwitchableCount = switchablePoolStep.SwitchableCount,
                SwitchableType = switchablePoolStep.SwitchableType,

                OutputSwitchablePoolEntityGuid = switchablePoolStep.Guid
            };
        }

        public static ISwitchablePoolStep ToSwitchablePoolStep(SwitchablePoolStepToJson sorterPoolStepToJson, IEntityProvider entityProvider)
        {
            return SwitchablePoolStep.Load
                (
                    guid: sorterPoolStepToJson.Guid,
                    name: sorterPoolStepToJson.Name,
                    description: sorterPoolStepToJson.Description,
                    index: sorterPoolStepToJson.Index,
                    outputSwitchables: (ISwitchablePoolEntity) entityProvider.Entities.SingleOrDefault(T => T.Guid == sorterPoolStepToJson.OutputSwitchablePoolEntityGuid),
                    switchableType: sorterPoolStepToJson.SwitchableType,
                    keyCount: sorterPoolStepToJson.KeyCount,
                    seedIn: sorterPoolStepToJson.SeedIn,
                    switchableCount: sorterPoolStepToJson.SwitchableCount
                );
        }

        public string Description { get; set; }

        public Guid Guid { get; set; }

        public int Index { get; set; }

        public string Name { get; set; }

        public string TypeName { get; set; }

        public Guid OutputSwitchablePoolEntityGuid { get; set; }

        public int KeyCount { get; set; }

        public int SeedIn { get; set; }

        public int SeedOut { get; set; }

        public int SwitchableCount { get; set; }

        SwitchableType SwitchableType { get; set; }
    }
}
