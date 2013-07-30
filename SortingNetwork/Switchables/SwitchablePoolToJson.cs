using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SortingNetwork.Switchables
{
    public class SwitchablePoolToJson
    {
        public static SwitchablePoolToJson ToJson(ISwitchablePool switchablePool)
        {
            var swList = new List<object>();
            swList.AddRange(switchablePool.Switchables);
            return new SwitchablePoolToJson
                {
                    Comment = switchablePool.Comment,
                    Guid = switchablePool.Guid,
                    Switchables = swList
                };
        }

        public static ISwitchablePool ToSwitchablePool(SwitchablePoolToJson switchablePoolToJson)
        {
            return switchablePoolToJson.Switchables.Select(ConvertToSwitchable)
                    .ToSwitchablePool(switchablePoolToJson.Guid, switchablePoolToJson.Comment);
        }

        public static ISwitchable ConvertToSwitchable(object jsonObj)
        {
            var type = jsonObj.GetType();
            if (type == typeof (SwitchableBitArrayToJson))
            {
                var sba = (SwitchableBitArrayToJson)jsonObj;
                return SwitchableBitArray.Make(sba.Values, sba.Values.Count);
            }
            if (type == typeof(SwitchableIntArrayToJson))
            {
                var sba = (SwitchableIntArrayToJson)jsonObj;
                return SwitchableIntArray.Make(sba.Values, sba.Values.Count);
            }
            if (type == typeof(SwitchableShortToJson))
            {
                var sba = (SwitchableShortToJson)jsonObj;
                return SwitchableShort.Make(sba.Value);
            }
            throw new ArgumentException("cant convert to SwitchableToJson");
        }


    public string Comment { get; set; }

        public Guid Guid { get; set; }

        [JsonConverter(typeof(SwitchableJsonConverter))]
        public List<object> Switchables { get; set; }

    }
}
