using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using SortNetwork.Diff;
using SortNetwork.Json.Sorters;
using SortNetwork.TestData;

namespace SortNetwork.Test.Json.Sorters
{
    [TestClass]
    public class SorterToJsonFixture
    {
        [TestMethod]
        public void SorterToJsonTest()
        {
            var serialized = JsonConvert.SerializeObject(SorterToJson.ToJsonAdapter(TestSorters.TheSorter), Formatting.Indented);
            var deserialized = JsonConvert.DeserializeObject<SorterToJson>(serialized);

            var newSorter = SorterToJson.ToSorter(deserialized);
            var sorterDiff = SorterDiff.Make(newSorter, TestSorters.TheSorter);
            Assert.IsFalse(sorterDiff.SwitchesAreDifferent);
        }
    }
}
