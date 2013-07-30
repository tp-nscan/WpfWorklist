using System;
using System.Collections.Generic;
using MathUtils.Rand;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using SortingNetwork.Common;
using SortingNetwork.KeyPair;
using SortingNetwork.Runner;
using SortingNetwork.SorterMonitors;
using SortingNetwork.SorterPoolOpsSpec;
using SortingNetwork.SorterPoolSteps;
using SortingNetwork.Sorters;

namespace SortingNetwork.Test.SorterPoolSteps
{
    [TestClass]
    public class SorterPoolStepToJsonFixture
    {
        [TestMethod]
        public void TestUnknownSource()
        {
            var guid = Guid.NewGuid();
            var sp = SorterPoolState.Make(guid, new[] {SorterPoolOpManual.Instance}, null, "hi");
            var spTj = SorterPoolStateToJson.Make(sp);
            var serialized = JsonConvert.SerializeObject(spTj, Formatting.Indented);
            var deserialized = JsonConvert.DeserializeObject<SorterPoolStateToJson>(serialized);
        }

        [TestMethod]
        public void TestSorterPoolStepWithRandomSwitchSource()
        {
            const int ckeyCount = 16;
            const int cSwitchesPerSorter = 200;
            const int cSorterCount = 100;
            const int cSeed = 1234;

            var guid = Guid.NewGuid();
            var sp = SorterPoolState.Make
                (
                    guid,
                    new[] {SorterPoolOpRandomGen.Make
                        (
                            comment:"Comment",
                            keyCount: ckeyCount,
                            seedIn: 123,
                            seedOut: 1234,
                            switchesPerSorter:cSwitchesPerSorter,
                            sorterCount: cSorterCount
                        )},

                    KeySet.Instance.AllPairsForKeyCount(ckeyCount)
                        .RandomDraw(Randy.Fast(cSeed).ToInt(), cSorterCount * cSwitchesPerSorter).ToKeyPairRepo()
                        .ToSorters(cSwitchesPerSorter, cSorterCount)
                        ,
                    "hi"
                );

            var sorterPoolStepToJson = SorterPoolStateToJson.Make(sp);
            var serialized = JsonConvert.SerializeObject(sorterPoolStepToJson, Formatting.Indented);
            var deserialized = JsonConvert.DeserializeObject<SorterPoolStateToJson>(serialized);
            var stepOut = SorterPoolStateToJson.ToSorterPool(deserialized);
        }

        [TestMethod]
        public void TestT()
        {
            const int cKeyCount = 16;
            const int cSwitchesPerSorter = 200;
            const int cSorterCount = 100;
            const int cSeed = 144;

            var sorters = KeySet.Instance.AllPairsForKeyCount(cKeyCount)
                                   .RandomDraw(Randy.Fast(cSeed).ToInt(), cSwitchesPerSorter * cSorterCount)
                                   .ToKeyPairRepo()
                                   .ToSorters(cSwitchesPerSorter, cSorterCount);

            var sorterMonitors = KeySet.Instance.AllPairsForKeyCount(cKeyCount)
                                   .RandomDraw(Randy.Fast(cSeed).ToInt(), cSwitchesPerSorter * cSorterCount)
                                   .ToKeyPairRepo()
                                   .ToSorterMonitors(cSwitchesPerSorter, cSorterCount);

            var lst = new List<ISorter>();
            lst.AddRange(sorters);
            lst.AddRange(sorterMonitors);

        }

    }
}
