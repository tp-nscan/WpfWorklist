using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace JsonLib.Test
{
    [TestClass]
    public class ModuleGroupFixture
    {
        [TestMethod]
        public void TestSerialize()
        {
            var mg = new ModuleGroup("da header", new[] { new MysteryModule() } );
            var mtj = ModulesToJson.ToJsonAdapter(mg.Modules);
            var serialized = JsonConvert.SerializeObject(mtj);
            var deserialized = JsonConvert.DeserializeObject<ModulesToJson>(serialized);
            var objOut = ModulesToJson.Modules(deserialized);
        }
    }

    public class MysteryModule : IModule
    {
        public string ModuleType { get { return "mystery"; } }
    }

}
