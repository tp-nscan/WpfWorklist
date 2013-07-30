using System;
using MathUtils.Rand;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using SortingNetwork.Switchables;

namespace SortingNetwork.Test.Switchable
{
    [TestClass]
    public class SwitchableToJsonFixture
    {
        [TestMethod]
        public void TestSwitchablePoolToJson()
        {
            const int cSwitchableCount = 100;
            const int cKeyCount = 16;
            const int cSeed = 144;

            var swPool = SwitchableBitArray.MakeRandoms(cKeyCount, Randy.Fast(cSeed)
                                           .ToBool(0.5), cSwitchableCount).ToRepo()
                                           .Items.ToSwitchablePool(Guid.NewGuid(), "hi");

            var swpTj = SwitchablePoolToJson.ToJson(swPool);

            var serialized = JsonConvert.SerializeObject(swpTj, Formatting.Indented);
            var deserialized = JsonConvert.DeserializeObject<SwitchablePoolToJson>(serialized);

            var pool = SwitchablePoolToJson.ToSwitchablePool(deserialized);
        }
    }
}
