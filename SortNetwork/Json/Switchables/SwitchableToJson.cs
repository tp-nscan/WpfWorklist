using System;
using SortNetwork.Switchables;

namespace SortNetwork.Json.Switchables
{
    public static class SwitchableToJson
    {
        public static object ToJson(this ISwitchable switchable)
        {
            switch (switchable.SwitchableType)
            {
                case SwitchableType.BitArray:
                    return SwitchableBitArrayToJson.ToJson((ISwitchableBitArray)switchable);
                case SwitchableType.IntArray:
                    return SwitchableIntArrayToJson.ToJson((ISwitchableIntArray)switchable);
                case SwitchableType.Short:
                    return SwitchableShortToJson.ToJson((ISwitchableShort)switchable);
                default:
                    throw new Exception(switchable.SwitchableType + " not handled in StepToJsonConverter.ToJson");
            }
        }
    }
}
