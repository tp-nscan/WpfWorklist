using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace JsonLib
{
    public class ModulesToJson
    {
        public static ModulesToJson ToJsonAdapter(IEnumerable<IModule> modules)
        {
            var jsonModuleAdapters = new List<IJsonModuleAdapter>();

            foreach (var module in modules)
            {
                var converter = JsonModuleArrayConverter.GetModuleConverter(module.ModuleType);
                if (converter == null)
                {
                    jsonModuleAdapters.Add(new UnknownModuleAdapter(module.ModuleType));
                    continue;
                }
                jsonModuleAdapters.Add(converter(module));
            }

            return new ModulesToJson
            {
                ModuleAdapters = jsonModuleAdapters
            };
        }

        public static IEnumerable<IModule> Modules(ModulesToJson modulesToJson)
        {
            return modulesToJson.ModuleAdapters.Select(T => T.ToModule());
        }

        [JsonConverter(typeof(JsonModuleArrayConverter))]
        public List<IJsonModuleAdapter> ModuleAdapters { get; set; }
    }
}
