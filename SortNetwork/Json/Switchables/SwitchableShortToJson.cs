using SortNetwork.Switchables;

namespace SortNetwork.Json.Switchables
{
    public class SwitchableShortToJson
    {
        public static SwitchableShortToJson ToJson(ISwitchableShort switchableIntArray)
        {
            return new SwitchableShortToJson
            {
                SwitchableType = switchableIntArray.SwitchableType,
                Value = switchableIntArray.Value
            };
        }

        public SwitchableType SwitchableType { get; set; }

        public ushort Value { get; set; }
    }
}
