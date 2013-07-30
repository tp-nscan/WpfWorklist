using System;
using System.Linq;
using DynamicModel.Common;
using SortingNetworkDm.Entities;
using SortingNetworkDm.Steps;

namespace SortingNetworkDm.Json.Steps
{
    public class CompetePoolStepToJson
    {
        public static CompetePoolStepToJson ToJson(ICompetePoolStep competePoolStep)
        {
            return new CompetePoolStepToJson
            {
                Description = competePoolStep.Description,
                Guid = competePoolStep.Guid,
                Index = competePoolStep.Index,
                Name = competePoolStep.Name,
                TypeName = competePoolStep.TypeName,
                KeyCount = competePoolStep.KeyCount,
                SeedIn = competePoolStep.SeedIn,
                SeedOut = competePoolStep.SeedOut,

                GenerationCount = competePoolStep.GenerationCount,
                MutationRate = competePoolStep.MutationRate,
                SorterPoolSize = competePoolStep.SorterPoolSize,
                SorterChampCount = competePoolStep.SorterChampCount,
                SwitchablePoolSize = competePoolStep.SwitchablePoolSize,
                SwitchableChampCount = competePoolStep.SwitchableChampCount,

                InputSorterPoolEntityGuid = competePoolStep.InputSorterPoolEntity.Guid,
                InputSwitchablePoolEntityGuid = competePoolStep.InputSwitchablePoolEntity.Guid,

                OutputSwitchablePoolEntityGuid = (competePoolStep.OutputSwitchablePoolEntity != null) ?
                                                    competePoolStep.OutputSwitchablePoolEntity.Guid
                                                    :
                                                    Guid.Empty,

                OutputSorterPoolEntityGuid = (competePoolStep.OutputSorterResultPoolEntity != null) ?
                                                    competePoolStep.OutputSorterResultPoolEntity.Guid 
                                                    : 
                                                    Guid.Empty
            };
        }

        public static ICompetePoolStep ToCompetePoolStep(CompetePoolStepToJson competePoolStepToJson, IEntityProvider entityProvider)
        {
            return CompetePoolStep.Load
                (
                    guid : competePoolStepToJson.Guid,
                    name : competePoolStepToJson.Name,
                    description : competePoolStepToJson.Description,
                    index : competePoolStepToJson.Index,
                    inputSorterPoolEntity :(ISorterPoolEntity) entityProvider.Entities.SingleOrDefault(T => T.Guid == competePoolStepToJson.InputSorterPoolEntityGuid),
                    inputSwitchablePoolEntity :(ISwitchablePoolEntity) entityProvider.Entities.SingleOrDefault(T => T.Guid == competePoolStepToJson.InputSwitchablePoolEntityGuid),
                    outputSorterResultPoolEntity: (ISorterResultPoolEntity)entityProvider.Entities.SingleOrDefault(T => T.Guid == competePoolStepToJson.OutputSorterPoolEntityGuid),
                    outputSwitchablePoolEntity :(ISwitchablePoolEntity) entityProvider.Entities.SingleOrDefault(T => T.Guid == competePoolStepToJson.OutputSwitchablePoolEntityGuid),
                    seedIn : competePoolStepToJson.SeedIn,
                    generationCount : competePoolStepToJson.GenerationCount,
                    sorterPoolSize : competePoolStepToJson.SorterPoolSize,
                    sorterChampCount : competePoolStepToJson.SorterChampCount,
                    switchablePoolSize : competePoolStepToJson.SwitchablePoolSize,
                    switchableChampCount : competePoolStepToJson.SwitchableChampCount,
                    mutationRate: competePoolStepToJson.MutationRate
                );
        }

        public string Description { get; set; }

        public Guid Guid { get; set; }

        public int Index { get; set; }

        public string Name { get; set; }

        public string TypeName { get; set; }


        public Guid InputSorterPoolEntityGuid { get; set; }
        public Guid OutputSorterPoolEntityGuid { get; set; }
        public Guid InputSwitchablePoolEntityGuid { get; set; }
        public Guid OutputSwitchablePoolEntityGuid { get; set; }

        public int KeyCount { get; set; }

        public int SeedIn { get; set; }

        public int SeedOut { get; set; }

        public int GenerationCount { get; set; }

        public double MutationRate { get; set; }

        public int SorterPoolSize { get; set; }

        public int SorterChampCount { get; set; }

        public int SwitchablePoolSize { get; set; }

        public int SwitchableChampCount { get; set; }
    }
}
