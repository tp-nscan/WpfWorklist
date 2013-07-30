using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using SortNetwork.Switchables;

namespace SortNetwork.Json.Switchables
{
    public class SwitchableRepoToJson
    {
        public static SwitchableRepoToJson ToJson(ISwitchableRepo switchablePool)
        {
            return new SwitchableRepoToJson
                {
                    Switchables = switchablePool.Select(sw => sw.ToJson()).ToList()
                };
        }

        public static ISwitchableRepo ToSwitchableRepo(SwitchableRepoToJson switchablePoolToJson)
        {
            return switchablePoolToJson.Switchables.Select(ConvertToSwitchable).ToSwitchableRepo();
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
            throw new ArgumentException("cant convert to ISwitchable");
        }


    public string Description { get; set; }

        public Guid Guid { get; set; }

        [JsonConverter(typeof(JsonConverterForSwitchable))]
        public List<object> Switchables { get; set; }

    }
}
