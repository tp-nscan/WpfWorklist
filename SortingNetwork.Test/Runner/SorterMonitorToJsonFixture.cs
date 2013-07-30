using System;
using MathUtils.Rand;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using SortingNetwork.KeyPair;
using SortingNetwork.Runner;
using SortingNetwork.SorterMonitors;

namespace SortingNetwork.Test.Runner
{
    [TestClass]
    public class SorterMonitorToJsonFixture
    {
        [TestMethod]
        public void TestSorterMonitorToJson()
        {
            const int cSwitchsPerSorter = 200;
            const int cKeyCount = 16;
            const int cSeed = 144;

            var sorterMonitor = KeySet.Instance.AllPairsForKeyCount(cKeyCount)
                                      .RandomDraw(Randy.Fast(cSeed).ToInt(), cSwitchsPerSorter)
                                      .ToSorterMonitor(Guid.NewGuid());

            var sorterMonitorToJson = SorterMonitorToJson.ToJsonAdapter(sorterMonitor);
            var serialized = JsonConvert.SerializeObject(sorterMonitorToJson, Formatting.Indented);
            var deserialized = JsonConvert.DeserializeObject<SorterMonitorToJson>(serialized);

            var newSorterMonitor = SorterMonitorToJson.ToSorterMonitor(deserialized);

        }
    }
}
