using System;
using DynamicModel.Model;
using SortingNetworkDm.Steps;

namespace SortingNetworkDm.Json.Steps
{
    public static class StepToJson
    {
        public static object ToJson(this IStep step)
        {
            switch (step.TypeName)
            {
                case CompetePoolStep.TypeName:
                    return CompetePoolStepToJson.ToJson((ICompetePoolStep) step);
                case SorterPoolStep.TypeName:
                    return SorterPoolStepToJson.ToJson((ISorterPoolStep)step);
                case SwitchablePoolStep.TypeName:
                    return SwitchablePoolStepToJson.ToJson((ISwitchablePoolStep)step);
                default:
                    throw new Exception(step.TypeName + " not handled in StepToJsonConverter.ToJson");
            }
        }
    }
}
