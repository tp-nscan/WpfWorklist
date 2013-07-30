using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using SortNetwork.Diff;
using SortingNetworkDm.Json.Entities;
using SortingNetworkDm.TestData;

namespace SortingNetworkDm.Test.Json.Entities
{
    /// <summary>
    /// Summary description for SorterPoolEntityToJsonFixture
    /// </summary>
    [TestClass]
    public class SorterPoolEntityToJsonFixture
    {
        [TestMethod]
        public void TestSorterPoolEntityToJson()
        {
            var serialized = JsonConvert.SerializeObject(SorterPoolEntityToJson.ToJson(TestEntities.TheSorterPoolEntity));

            var deserializeObject = JsonConvert.DeserializeObject<SorterPoolEntityToJson>(serialized);
            var convertedSorterPoolEntity = SorterPoolEntityToJson.ToSorterPoolEntity(deserializeObject);

            var diff = SorterPoolDiff.Make
                (
                    TestEntities.TheSorterPoolEntity.SorterRepo,
                    convertedSorterPoolEntity.SorterRepo
                );

            Assert.IsFalse(diff.AnySwitchLevelDiffs);

            Assert.AreEqual(TestEntities.TheSorterPoolEntity.Description, convertedSorterPoolEntity.Description);
            Assert.AreEqual(TestEntities.TheSorterPoolEntity.Name, convertedSorterPoolEntity.Name);
            Assert.AreEqual(TestEntities.TheSorterPoolEntity.Guid, convertedSorterPoolEntity.Guid);
            Assert.AreEqual(TestEntities.TheSorterPoolEntity.TypeName, convertedSorterPoolEntity.TypeName);

        }
    }
}
