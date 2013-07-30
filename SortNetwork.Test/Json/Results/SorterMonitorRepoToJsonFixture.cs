using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using SortNetwork.Diff;
using SortNetwork.Json.Results;
using SortNetwork.TestData;

namespace SortNetwork.Test.Json.Results
{
    [TestClass]
    public class SorterResultRepoToJsonFixture
    {
        [TestMethod]
        public void TestSorterResultRepoToJson()
        {
            var serialized = JsonConvert.SerializeObject(SorterResultRepoToJson.ToJson(TestSorterResults.TheSorterResultRepo), Formatting.Indented);
            var deserialized = JsonConvert.DeserializeObject<SorterResultRepoToJson>(serialized);
            var renewedPool = SorterResultRepoToJson.ToSorterResultRepo(deserialized);

            var sorterPoolDiff = SorterResultPoolDiff.Make(TestSorterResults.TheSorterResultRepo, renewedPool);
            Assert.IsFalse(sorterPoolDiff.AnySwitchResultDiffs);
        }
    }
}
