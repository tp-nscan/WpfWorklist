using MathUtils.Rand;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using SortingNetwork.KeyPair;
using SortingNetwork.Sorters;

namespace SortingNetwork.Test.Sorters
{
    [TestClass]
    public class SorterPoolFixture
    {
        [TestMethod]
        public void TestToJson()
        {
            const int cKeyCount = 16;
            const int cSwitchesPerSorter = 2;
            const int cSorterCount = 100;
            const int cSeed = 123;

            var sorterPool = KeySet.Instance.AllPairsForKeyCount(cKeyCount)
                                   .RandomDraw(Randy.Fast(cSeed).ToInt(), cSwitchesPerSorter * cSorterCount)
                                   .ToKeyPairRepo()
                                   .ToSorters(cSwitchesPerSorter, cSorterCount).ToSorterRepo();

            var sptj = SorterRepoToJson.ToJsonAdapter(sorterPool);
            var serialized = JsonConvert.SerializeObject(sptj, Formatting.Indented);
            var deserialized = JsonConvert.DeserializeObject<SorterRepoToJson>(serialized);

            var renewedPool = SorterRepoToJson.ToSorterRepo(deserialized);
        }

        [TestMethod]
        public void TestTwoSteps()
        {
            const int cKeyCount = 16;
            const int cSwitchesPerSorter = 200;
            const int cSorterCount = 100;
            const int cSeed = 123;

            var sorterPool = KeySet.Instance.AllPairsForKeyCount(cKeyCount)
                                   .RandomDraw(Randy.Fast(cSeed).ToInt(), cSwitchesPerSorter * cSorterCount)
                                   .ToKeyPairRepo()
                                   .ToSorters(cSwitchesPerSorter, cSorterCount).ToSorterRepo();

            var sptj = SorterRepoToJson.ToJsonAdapter(sorterPool);
            var serialized = JsonConvert.SerializeObject(sptj, Formatting.Indented);
            var deserialized = JsonConvert.DeserializeObject<SorterRepoToJson>(serialized);

            var renewedPool = SorterRepoToJson.ToSorterRepo(deserialized);
        }

    }
}
