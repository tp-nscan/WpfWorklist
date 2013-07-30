using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SortingNetwork.Runner;
using SortingNetwork.SorterMonitors;

namespace SortingNetwork.Sorters
{
    public class SorterJsonConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jArray = JArray.Load(reader);

            var retList = new List<object>();

            for (var i = 0; i < jArray.Count; i++)
            {
                var jObject = jArray[i];
                var fv = (SorterType)Enum.Parse(typeof(SorterType), (string)jObject["SorterType"]);
                switch (fv)
                {
                    case SorterType.Simple :
                        retList.Add(serializer.Deserialize<SorterToJson>(jObject.CreateReader()));
                        break;
                    case SorterType.Monitor :
                        retList.Add(serializer.Deserialize<SorterMonitorToJson>(jObject.CreateReader()));
                        break;
                    default:
                        throw new Exception("SwitchableType not handled");
                }
            }

            return retList;
        }

        public override bool CanConvert(Type objectType)
        {
            return true;
        }
    }
}
