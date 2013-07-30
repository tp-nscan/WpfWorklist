using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using SortNetwork.Diff;
using SortNetwork.Json.Switchables;
using SortNetwork.TestData;

namespace SortNetwork.Test.Json.Switchables
{
    [TestClass]
    public class SwitchablePoolToJsonFixture
    {
        [TestMethod]
        public void TestSwitchablePoolToJson()
        {
            var serialized = JsonConvert.SerializeObject(SwitchableRepoToJson.ToJson(TestSwitchable.TheSwitchableRepo), Formatting.Indented);
            var deserialized = JsonConvert.DeserializeObject<SwitchableRepoToJson>(serialized);

            var switchableRepoOut = SwitchableRepoToJson.ToSwitchableRepo(deserialized);

            var diff = SwitchableRepoDiff.Make
            (
                TestSwitchable.TheSwitchableRepo,
                switchableRepoOut
            );

            Assert.IsFalse(diff.HasDifferences);
        }
    }
}
