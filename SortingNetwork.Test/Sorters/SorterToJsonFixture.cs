using System;
using MathUtils.Rand;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using SortingNetwork.KeyPair;
using SortingNetwork.Runner;
using SortingNetwork.SorterMonitors;
using SortingNetwork.Sorters;

namespace SortingNetwork.Test.Sorters
{
    [TestClass]
    public class SorterToJsonFixture
    {
        [TestMethod]
        public void SorterToJsonTest()
        {
            const int cSwitchesPerSorter = 200;
            const int cKeyCount = 16;
            const int cSeed = 144;

            var sorter = KeySet.Instance.AllPairsForKeyCount(cKeyCount)
                                   .RandomDraw(Randy.Fast(cSeed).ToInt(), cSwitchesPerSorter)
                                   .ToSorter(Guid.NewGuid());

            var sorterToJson = SorterToJson.ToJsonAdapter(sorter);
            var serialized = JsonConvert.SerializeObject(sorterToJson, Formatting.Indented);
            var deserialized = JsonConvert.DeserializeObject<SorterToJson>(serialized);

            var newSorter = SorterToJson.ToSorter(deserialized);
        }

        [TestMethod]
        public void SorterMonitorToJsonTest()
        {
            const int cKeyCount = 16;
            const int cSwitchesPerSorter = 200;
            const int cSeed = 123;

            var sorterMonitor = KeySet.Instance.AllPairsForKeyCount(cKeyCount)
                                   .RandomDraw(Randy.Fast(cSeed).ToInt(), cSwitchesPerSorter)
                                   .ToSorterMonitor(Guid.NewGuid());

            var sorterMonitorToJson = SorterMonitorToJson.ToJsonAdapter(sorterMonitor);
            var serialized = JsonConvert.SerializeObject(sorterMonitorToJson, Formatting.Indented);
            var deserialized = JsonConvert.DeserializeObject<SorterMonitorToJson>(serialized);

            System.Diagnostics.Debug.WriteLine(serialized);

            var newSorterMonitor = SorterMonitorToJson.ToSorterMonitor(deserialized);
        }
    }
}
