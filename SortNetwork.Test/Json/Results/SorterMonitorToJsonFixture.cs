using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using SortNetwork.Diff;
using SortNetwork.Json.Results;
using SortNetwork.TestData;

namespace SortNetwork.Test.Json.Results
{
    [TestClass]
    public class SorterResultToJsonFixture
    {
        [TestMethod]
        public void TestSorterResultToJson()
        {
            var serialized = JsonConvert.SerializeObject(SorterResultToJson.ToJsonAdapter(TestSorterResults.TheSorterResult), Formatting.Indented);
            var deserialized = JsonConvert.DeserializeObject<SorterResultToJson>(serialized);

            var newSorterResult = SorterResultToJson.ToSorterResult(deserialized);

            var sorterDiff = SorterResultDiff.Make(newSorterResult, TestSorterResults.TheSorterResult);
            Assert.IsFalse(sorterDiff.SwitchesAreDifferent);
        }
    }
}
