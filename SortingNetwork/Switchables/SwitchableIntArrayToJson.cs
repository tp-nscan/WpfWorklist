using System.Collections.Generic;
using System.Linq;

namespace SortingNetwork.Switchables
{
    public class SwitchableIntArrayToJson
    {
        public static SwitchableIntArrayToJson Make(ISwitchableIntArray switchableIntArray)
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
