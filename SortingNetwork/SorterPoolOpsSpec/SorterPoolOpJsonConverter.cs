using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SortingNetwork.SorterPoolOpsSpec
{
    public class SorterPoolOpJsonConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer,value);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jArray = JArray.Load(reader);
            var retList = new List<ISorterPoolOp>();

            for (var i = 0; i < jArray.Count; i++)
            {
                var jObject = jArray[i];

                var fv = (SorterPoolOpType)Enum.Parse(typeof(SorterPoolOpType), (string)jObject["SorterPoolOpType"]);
                switch (fv)
                {
                    case SorterPoolOpType.RandomGen:
                        retList.Add(serializer.Deserialize<SorterPoolOpRandomGenImpl>(jObject.CreateReader()));
                        break;
                    case SorterPoolOpType.Manual:
                        retList.Add(serializer.Deserialize<SorterPoolOpManual>(jObject.CreateReader()));
                        break;
                    default:
                        throw new Exception("SorterPoolOpType not handled");
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
