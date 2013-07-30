using System.Collections.Generic;
using System.Linq;
using SortNetwork.Switchables;

namespace SortNetwork.Json.Switchables
{
    public class SwitchableBitArrayToJson
    {
        public static SwitchableBitArrayToJson ToJson(ISwitchableBitArray switchableBitArray)
        {
            return new SwitchableBitArrayToJson()
            {
                SwitchableType = switchableBitArray.SwitchableType,
                Values = switchableBitArray.Values.ToList()
            };
        }

        public SwitchableType SwitchableType { get; set; }

        public List<bool> Values { get; set; }
    }
}
