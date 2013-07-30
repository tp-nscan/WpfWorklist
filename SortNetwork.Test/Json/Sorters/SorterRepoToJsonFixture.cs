using MathUtils.Rand;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using SortNetwork.Diff;
using SortNetwork.Json.Sorters;
using SortNetwork.KeySets;
using SortNetwork.Sorters;
using SortNetwork.TestData;

namespace SortNetwork.Test.Json.Sorters
{
    [TestClass]
    public class SorterRepoToJsonFixture
    {
        [TestMethod]
        public void SorterRepoToJsonTest()
        {
            var rando = Randy.Fast(TestConstants.Seed);

            var sorterRepo = KeySet.Instance.AllKeyPairsForKeyCount(TestConstants.KeyCount)
                                   .RandomDraw(rando.ToInt(), TestConstants.SwitchesPerSorter * TestConstants.SwitchableCount)
                                   .ToKeyPairRepo()
                                   .ToSorters(TestConstants.SwitchesPerSorter, TestConstants.SwitchableCount, rando.ToGuid())
                                   .ToSorterRepo();

            var serialized = JsonConvert.SerializeObject(SorterRepoToJson.ToJson(sorterRepo), Formatting.Indented);
            var deserialized = JsonConvert.DeserializeObject<SorterRepoToJson>(serialized);

            var renewedPool = SorterRepoToJson.ToSorterRepo(deserialized);

            var sorterPoolDiff = SorterPoolDiff.Make(sorterRepo, renewedPool);
            Assert.IsFalse(sorterPoolDiff.AnySwitchLevelDiffs);
        }
    }
}
