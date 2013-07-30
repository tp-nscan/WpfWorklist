using System.Collections.Generic;
using System.Linq;

namespace SortingNetwork.Switchables
{
    public class SwitchableBitArrayToJson
    {
        public static SwitchableBitArrayToJson Make(ISwitchableBitArray switchableBitArray)
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
