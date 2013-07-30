using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using SortNetwork.Diff;
using SortingNetworkDm.Json.Entities;
using SortingNetworkDm.TestData;

namespace SortingNetworkDm.Test.Json.Entities
{
    /// <summary>
    /// Summary description for SwitchablePoolEntityToJsonFixture
    /// </summary>
    [TestClass]
    public class SwitchablePoolEntityToJsonFixture
    {
        [TestMethod]
        public void TestSwitchablePoolEntityToJson()
        {
            var serialized = JsonConvert.SerializeObject(SwitchablePoolEntityToJson.ToJson(TestEntities.TheSwitchablePoolEntity));

            var deserializeObject = JsonConvert.DeserializeObject<SwitchablePoolEntityToJson>(serialized);
            var convertedSwitchablePoolEntity = SwitchablePoolEntityToJson.ToSwitchablePoolEntity(deserializeObject);

            var diff = SwitchableRepoDiff.Make
            (
                TestEntities.TheSwitchablePoolEntity.SwitchableRepo,
                convertedSwitchablePoolEntity.SwitchableRepo
            );
            Assert.IsFalse(diff.HasDifferences);

            Assert.AreEqual(TestEntities.TheSwitchablePoolEntity.Description, convertedSwitchablePoolEntity.Description);
            Assert.AreEqual(TestEntities.TheSwitchablePoolEntity.Name, convertedSwitchablePoolEntity.Name);
            Assert.AreEqual(TestEntities.TheSwitchablePoolEntity.Guid, convertedSwitchablePoolEntity.Guid);
            Assert.AreEqual(TestEntities.TheSwitchablePoolEntity.TypeName, convertedSwitchablePoolEntity.TypeName);
        }
    }
}
