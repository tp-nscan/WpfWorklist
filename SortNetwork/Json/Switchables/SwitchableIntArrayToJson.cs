using System.Collections.Generic;
using System.Linq;
using SortNetwork.Switchables;

namespace SortNetwork.Json.Switchables
{
    public class SwitchableIntArrayToJson
    {
        public static SwitchableIntArrayToJson ToJson(ISwitchableIntArray switchableIntArray)
        {
            return new SwitchableIntArrayToJson()
            {
                SwitchableType = switchableIntArray.SwitchableType,
                Values = switchableIntArray.Values.ToList()
            };
        }

        public SwitchableType SwitchableType { get; set; }

        public List<int> Values { get; set; }
    }
}
