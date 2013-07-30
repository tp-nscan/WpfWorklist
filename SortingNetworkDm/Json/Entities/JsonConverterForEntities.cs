using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SortingNetworkDm.Entities;

namespace SortingNetworkDm.Json.Entities
{
    public class JsonConverterForEntities : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
                                        JsonSerializer serializer)
        {
            var jArray = JArray.Load(reader);
            var retList = new List<object>();

            for (var i = 0; i < jArray.Count; i++)
            {
                var jObject = jArray[i];
                var fv = (string)jObject["TypeName"];
                switch (fv)
                {
                    case SorterResultPoolEntity.TypeName:
                        retList.Add(serializer.Deserialize<SorterResultPoolEntityToJson>(jObject.CreateReader()));
                        break;
                    case SorterPoolEntity.TypeName:
                        retList.Add(serializer.Deserialize<SorterPoolEntityToJson>(jObject.CreateReader()));
                        break;
                    case SwitchablePoolEntity.TypeName:
                        retList.Add(serializer.Deserialize<SwitchablePoolEntityToJson>(jObject.CreateReader()));
                        break;
                    default:
                        throw new Exception("EntityToJson not handled");
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
