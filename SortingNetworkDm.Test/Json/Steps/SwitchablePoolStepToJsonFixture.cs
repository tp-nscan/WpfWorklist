using System.Linq;
using DynamicModel.Common;
using DynamicModel.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using SortNetwork.Diff;
using SortingNetworkDm.Json.Steps;
using SortingNetworkDm.TestData;

namespace SortingNetworkDm.Test.Json.Steps
{
    [TestClass]
    public class SwitchablePoolStepToJsonFixture
    {
        [TestMethod]
        public void TestInitializedSwitchablePoolStepToJson()
        {
            var switchablePoolStep = TestSteps.TheInitializedSwitchablePoolStep ;
            var poolStepToJson = SwitchablePoolStepToJson.ToJson(switchablePoolStep);

            var serialized = JsonConvert.SerializeObject(poolStepToJson);
            var switchablePoolStepToJson = JsonConvert.DeserializeObject<SwitchablePoolStepToJson>(serialized);
            var newSorterPoolStep = SwitchablePoolStepToJson.ToSwitchablePoolStep(switchablePoolStepToJson, EntityProvider.Make());

            Assert.IsNull(newSorterPoolStep.OutputSwitchablePoolEntity);
            Assert.AreEqual(newSorterPoolStep.KeyCount, switchablePoolStep.KeyCount);
            Assert.AreEqual(newSorterPoolStep.Index, switchablePoolStep.Index);
            Assert.AreEqual(newSorterPoolStep.Name, switchablePoolStep.Name);
            Assert.AreEqual(newSorterPoolStep.SeedIn, switchablePoolStep.SeedIn);
            Assert.AreEqual(newSorterPoolStep.SeedOut, switchablePoolStep.SeedOut);
            Assert.AreEqual(newSorterPoolStep.SwitchableCount, switchablePoolStep.SwitchableCount);
            Assert.AreEqual(newSorterPoolStep.SwitchableType, switchablePoolStep.SwitchableType);
            Assert.AreEqual(newSorterPoolStep.WasExecuted, switchablePoolStep.WasExecuted);
        }


        [TestMethod]
        public void TestCompletedSwitchablePoolStepToJson()
        {
            var switchablePoolStep = TestSteps.TheCompletedSwitchablePoolStep;

            var testEntityProvider = EntityProvider.Make();
            foreach (var entity in switchablePoolStep.AllEntities())
            {
                testEntityProvider.AddEntity(entity);
            }

            var poolStepToJson = SwitchablePoolStepToJson.ToJson(switchablePoolStep);

            var serialized = JsonConvert.SerializeObject(poolStepToJson);
            var switchablePoolStepToJson = JsonConvert.DeserializeObject<SwitchablePoolStepToJson>(serialized);
            var newSwitchablePoolStep = SwitchablePoolStepToJson.ToSwitchablePoolStep(switchablePoolStepToJson, testEntityProvider);

            var switchableRepoDiff = SwitchableRepoDiff.Make
            (
                switchablePoolStep.OutputSwitchablePoolEntity.SwitchableRepo,
                newSwitchablePoolStep.OutputSwitchablePoolEntity.SwitchableRepo
            );
            Assert.IsFalse(switchableRepoDiff.HasDifferences);

            Assert.AreEqual(newSwitchablePoolStep.KeyCount, switchablePoolStep.KeyCount);
            Assert.AreEqual(newSwitchablePoolStep.Index, switchablePoolStep.Index);
            Assert.AreEqual(newSwitchablePoolStep.Name, switchablePoolStep.Name);
            Assert.AreEqual(newSwitchablePoolStep.SeedIn, switchablePoolStep.SeedIn);
            Assert.AreEqual(newSwitchablePoolStep.SeedOut, switchablePoolStep.SeedOut);
            Assert.AreEqual(newSwitchablePoolStep.SwitchableCount, switchablePoolStep.SwitchableCount);
            Assert.AreEqual(newSwitchablePoolStep.SwitchableType, switchablePoolStep.SwitchableType);
            Assert.AreEqual(newSwitchablePoolStep.WasExecuted, switchablePoolStep.WasExecuted);
        }

    }
}
