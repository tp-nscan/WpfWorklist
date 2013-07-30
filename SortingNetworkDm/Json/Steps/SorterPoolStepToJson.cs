using System;
using System.Linq;
using DynamicModel.Common;
using SortingNetworkDm.Entities;
using SortingNetworkDm.Steps;

namespace SortingNetworkDm.Json.Steps
{
    public class SorterPoolStepToJson
    {
        public static SorterPoolStepToJson ToJson(ISorterPoolStep sorterPoolStep)
        {
            return new SorterPoolStepToJson
            {
                Description = sorterPoolStep.Description,
                Guid = sorterPoolStep.Guid,
                Index = sorterPoolStep.Index,
                Name = sorterPoolStep.Name,
                TypeName = sorterPoolStep.TypeName,
                KeyCount = sorterPoolStep.KeyCount,
                SeedIn = sorterPoolStep.SeedIn,
                SeedOut = sorterPoolStep.SeedOut,
                SorterCount = sorterPoolStep.SorterCount,
                SwitchesPerSorter = sorterPoolStep.SwitchesPerSorter,

                OutputSorterPoolEntityGuid = sorterPoolStep.Guid
            };
        }

        public static ISorterPoolStep ToSorterPoolStep(SorterPoolStepToJson sorterPoolStepToJson, IEntityProvider entityProvider)
        {
            return SorterPoolStep.Load
                (
                    guid: sorterPoolStepToJson.Guid,
                    name: sorterPoolStepToJson.Name,
                    description: sorterPoolStepToJson.Description,
                    index: sorterPoolStepToJson.Index,
                    outputSorters: (ISorterPoolEntity) entityProvider.Entities.SingleOrDefault(T => T.Guid == sorterPoolStepToJson.OutputSorterPoolEntityGuid),
                    keyCount: sorterPoolStepToJson.KeyCount,
                    seedIn: sorterPoolStepToJson.SeedIn,
                    sorterCount: sorterPoolStepToJson.SorterCount,
                    switchesPerSorter: sorterPoolStepToJson.SwitchesPerSorter
                );
        }

        public string Description { get; set; }

        public Guid Guid { get; set; }

        public int Index { get; set; }

        public string Name { get; set; }

        public string TypeName { get; set; }

        public int KeyCount { get; set; }

        public Guid OutputSorterPoolEntityGuid { get; set; }

        public int SeedIn { get; set; }

        public int SeedOut { get; set; }

        public int SorterCount { get; set; }

        public int SwitchesPerSorter { get; set; }
    }
}
