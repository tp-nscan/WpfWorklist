using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using SortNetwork.Diff;
using SortingNetworkDm.Json.Entities;
using SortingNetworkDm.TestData;

namespace SortingNetworkDm.Test.Json.Entities
{
    [TestClass]
    public class SorterResultPoolEntityToJsonFixture
    {
        [TestMethod]
        public void TestSorterResultPoolEntityToJson()
        {
            var serialized = JsonConvert.SerializeObject(SorterResultPoolEntityToJson.ToJson(
                TestEntities.TheSorterResultPoolEntity
                ));

            var deserializeObject = JsonConvert.DeserializeObject<SorterResultPoolEntityToJson>(serialized);
            var convertedSorterResultPoolEntity = SorterResultPoolEntityToJson.ToSorterResultPoolEntity(deserializeObject);

            var diff = SorterResultPoolDiff.Make
                (
                    TestEntities.TheSorterResultPoolEntity.SorterResultRepo,
                    convertedSorterResultPoolEntity.SorterResultRepo
                );

            Assert.IsFalse(diff.AnySwitchResultDiffs);

            Assert.AreEqual(TestEntities.TheSorterResultPoolEntity.Description, convertedSorterResultPoolEntity.Description);
            Assert.AreEqual(TestEntities.TheSorterResultPoolEntity.Name, convertedSorterResultPoolEntity.Name);
            Assert.AreEqual(TestEntities.TheSorterResultPoolEntity.Guid, convertedSorterResultPoolEntity.Guid);
            Assert.AreEqual(TestEntities.TheSorterResultPoolEntity.TypeName, convertedSorterResultPoolEntity.TypeName);

        }
    }
}
