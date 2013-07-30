using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonLib
{
    public class JsonModuleArrayConverter : JsonConverter
    {
        public static string UnknownModuleType = "Unknown";
        public static string HeaderModuleType = "Header";
        public static string ModuleTypeField = "KeyForType";

        public JsonModuleArrayConverter()
        {
        }

        static JsonModuleArrayConverter()
        {
            ConversionHelpers.Add
            (
                HeaderModuleType,
                new JsonConversionHelper
                (
                    typeof(HeaderModuleAdapter),
                    m => HeaderModuleAdapter.Make((HeaderModule)m)
                )
            );
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }

        public static Func<IModule, IJsonModuleAdapter> GetModuleConverter(string key)
        {
            return ConversionHelpers.ContainsKey(key) ? ConversionHelpers[key].Converter : null;
        }

        static protected readonly Dictionary<string, JsonConversionHelper> ConversionHelpers = new Dictionary<string, JsonConversionHelper>();

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jArray = JArray.Load(reader);
            var retList = new List<IJsonModuleAdapter>();

            for (var i = 0; i < jArray.Count; i++)
            {
                var jObject = jArray[i];
                var moduleType = (string)jObject[ModuleTypeField];

                if (ConversionHelpers.ContainsKey(moduleType))
                {
                    retList.Add(
                        (IJsonModuleAdapter) serializer.Deserialize(jObject.CreateReader(), ConversionHelpers[moduleType].Type));
                    continue;
                }
                retList.Add( new UnknownModuleAdapter(moduleType));
            }

            return retList;
        }

        public override bool CanConvert(Type objectType)
        {
            return true;
        }
    }

    public class JsonConversionHelper
    {
        public JsonConversionHelper(Type type, Func<IModule, IJsonModuleAdapter> converter)
        {
            if (type.GetInterface("IJsonModuleAdapter") == null)
            {
                throw new ArgumentException("type does not implement IJsonModuleAdapter");
            }
            _converter = converter;
            _type = type;
        }

        private readonly Type _type;
        public Type Type
        {
            get { return _type; }
        }

        private readonly Func<IModule, IJsonModuleAdapter> _converter;
        public Func<IModule, IJsonModuleAdapter> Converter
        {
            get { return _converter; }
        }
    }
}
