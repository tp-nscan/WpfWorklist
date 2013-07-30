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
    public class SorterPoolStepToJsonFixture
    {
        [TestMethod]
        public void TestInitializedSorterPoolStepToJson()
        {
            var sorterPoolStep = TestSteps.TheInitializedSorterPoolStep;
            var spstj = SorterPoolStepToJson.ToJson(sorterPoolStep);

            var serialized = JsonConvert.SerializeObject(spstj);
            var sorterPoolStepToJson = JsonConvert.DeserializeObject<SorterPoolStepToJson>(serialized);
            var newSorterPoolStep = SorterPoolStepToJson.ToSorterPoolStep(sorterPoolStepToJson, EntityProvider.Make());

            Assert.IsNull(newSorterPoolStep.OutputSorterPoolEntity);
            Assert.AreEqual(newSorterPoolStep.KeyCount, sorterPoolStep.KeyCount);
            Assert.AreEqual(newSorterPoolStep.Index, sorterPoolStep.Index);
            Assert.AreEqual(newSorterPoolStep.Name, sorterPoolStep.Name);
            Assert.AreEqual(newSorterPoolStep.SeedIn, sorterPoolStep.SeedIn);
            Assert.AreEqual(newSorterPoolStep.SeedOut, sorterPoolStep.SeedOut);
            Assert.AreEqual(newSorterPoolStep.SorterCount, sorterPoolStep.SorterCount);
            Assert.AreEqual(newSorterPoolStep.SwitchesPerSorter, sorterPoolStep.SwitchesPerSorter);
            Assert.AreEqual(newSorterPoolStep.WasExecuted, sorterPoolStep.WasExecuted);
        }


        [TestMethod]
        public void TestCompletedSorterPoolStepToJson()
        {
            var sorterPoolStep = TestSteps.TheCompletedSorterPoolStep;

            var testEntityProvider = EntityProvider.Make();
            foreach (var entity in sorterPoolStep.AllEntities())
            {
                testEntityProvider.AddEntity(entity);
            }

            var spstj = SorterPoolStepToJson.ToJson(sorterPoolStep);

            var serialized = JsonConvert.SerializeObject(spstj);
            var sorterPoolStepToJson = JsonConvert.DeserializeObject<SorterPoolStepToJson>(serialized);
            var newSorterPoolStep = SorterPoolStepToJson.ToSorterPoolStep(sorterPoolStepToJson, testEntityProvider);

            var diff = SorterPoolDiff.Make
            (
                newSorterPoolStep.OutputSorterPoolEntity.SorterRepo,
                sorterPoolStep.OutputSorterPoolEntity.SorterRepo
            );

            Assert.IsFalse(diff.AnySwitchLevelDiffs);

            Assert.AreEqual(newSorterPoolStep.KeyCount, sorterPoolStep.KeyCount);
            Assert.AreEqual(newSorterPoolStep.Index, sorterPoolStep.Index);
            Assert.AreEqual(newSorterPoolStep.Name, sorterPoolStep.Name);
            Assert.AreEqual(newSorterPoolStep.SeedIn, sorterPoolStep.SeedIn);
            Assert.AreEqual(newSorterPoolStep.SeedOut, sorterPoolStep.SeedOut);
            Assert.AreEqual(newSorterPoolStep.SorterCount, sorterPoolStep.SorterCount);
            Assert.AreEqual(newSorterPoolStep.SwitchesPerSorter, sorterPoolStep.SwitchesPerSorter);
            Assert.AreEqual(newSorterPoolStep.WasExecuted, sorterPoolStep.WasExecuted);
        }

    }


}
