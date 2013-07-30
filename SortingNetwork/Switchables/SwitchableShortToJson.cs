namespace SortingNetwork.Switchables
{
    public class SwitchableShortToJson
    {
        public static SwitchableShortToJson Make(SwitchableShortToJson switchableIntArray)
        {
            return new SwitchableShortToJson()
            {
                SwitchableType = switchableIntArray.SwitchableType,
                Value = switchableIntArray.Value
            };
        }

        public SwitchableType SwitchableType { get; set; }

        public ushort Value { get; set; }
    }
}
