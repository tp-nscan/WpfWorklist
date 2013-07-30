using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SortingNetworkDm.Steps;

namespace SortingNetworkDm.Json.Steps
{
    public class JsonConverterForSteps : JsonConverter
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
                    case SorterPoolStep.TypeName:
                        retList.Add(serializer.Deserialize<SorterPoolStepToJson>(jObject.CreateReader()));
                        break;
                    case SwitchablePoolStep.TypeName:
                        retList.Add(serializer.Deserialize<SwitchablePoolStepToJson>(jObject.CreateReader()));
                        break;
                    case CompetePoolStep.TypeName:
                        retList.Add(serializer.Deserialize<CompetePoolStepToJson>(jObject.CreateReader()));
                        break;
                    default:
                        throw new Exception("StepToJson not handled");
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
