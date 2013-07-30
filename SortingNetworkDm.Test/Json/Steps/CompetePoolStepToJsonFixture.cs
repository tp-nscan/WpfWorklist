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
    public class CompetePoolStepToJsonFixture
    {
        [TestMethod]
        public void TestInitializedCompetePoolStepToJson()
        {
            var competePoolStep = TestSteps.TheInitializedCompetePoolStep ;

            var testEntityProvider = EntityProvider.Make();
            foreach (var entity in competePoolStep.AllEntities())
            {
                testEntityProvider.AddEntity(entity);
            }

            var poolStepToJson = CompetePoolStepToJson.ToJson(competePoolStep);

            var serialized = JsonConvert.SerializeObject(poolStepToJson);
            var competePoolStepToJson = JsonConvert.DeserializeObject<CompetePoolStepToJson>(serialized);
            var newCompetePoolStep = CompetePoolStepToJson.ToCompetePoolStep(competePoolStepToJson, testEntityProvider);

            var sorterPoolDiff = SorterPoolDiff.Make
            (
                competePoolStep.InputSorterPoolEntity.SorterRepo,
                newCompetePoolStep.InputSorterPoolEntity.SorterRepo
            );
            Assert.IsFalse(sorterPoolDiff.AnySwitchLevelDiffs);

            var switchableRepoDiff = SwitchableRepoDiff.Make
            (
                competePoolStep.InputSwitchablePoolEntity.SwitchableRepo,
                newCompetePoolStep.InputSwitchablePoolEntity.SwitchableRepo
            );
            Assert.IsFalse(switchableRepoDiff.HasDifferences);

            Assert.IsNull(newCompetePoolStep.OutputSorterResultPoolEntity);
            Assert.IsNull(newCompetePoolStep.OutputSwitchablePoolEntity);

            Assert.AreEqual(newCompetePoolStep.KeyCount, competePoolStep.KeyCount);
            Assert.AreEqual(newCompetePoolStep.Index, competePoolStep.Index);
            Assert.AreEqual(newCompetePoolStep.Name, competePoolStep.Name);
            Assert.AreEqual(newCompetePoolStep.SeedIn, competePoolStep.SeedIn);
            Assert.AreEqual(newCompetePoolStep.SeedOut, competePoolStep.SeedOut);

            Assert.AreEqual(newCompetePoolStep.GenerationCount, competePoolStep.GenerationCount);
            Assert.AreEqual(newCompetePoolStep.SwitchableChampCount, competePoolStep.SwitchableChampCount);
            Assert.AreEqual(newCompetePoolStep.SorterChampCount, competePoolStep.SorterChampCount);
            Assert.AreEqual(newCompetePoolStep.MutationRate, competePoolStep.MutationRate);
            Assert.AreEqual(newCompetePoolStep.SorterPoolSize, competePoolStep.SorterPoolSize);
            Assert.AreEqual(newCompetePoolStep.WasExecuted, competePoolStep.WasExecuted);
        }


        [TestMethod]
        public void TestCompletedSorterPoolStepToJson()
        {
            var competePoolStep = TestSteps.TheCompletedCompetePoolStep;

            var testEntityProvider = EntityProvider.Make();
            foreach (var entity in competePoolStep.AllEntities())
            {
                testEntityProvider.AddEntity(entity);
            }

            var poolStepToJson = CompetePoolStepToJson.ToJson(competePoolStep);

            var serialized = JsonConvert.SerializeObject(poolStepToJson);
            var switchablePoolStepToJson = JsonConvert.DeserializeObject<CompetePoolStepToJson>(serialized);
            var newCompetePoolStep = CompetePoolStepToJson.ToCompetePoolStep(switchablePoolStepToJson, testEntityProvider);

            var inputSorterPoolDiff = SorterPoolDiff.Make
            (
                competePoolStep.InputSorterPoolEntity.SorterRepo,
                newCompetePoolStep.InputSorterPoolEntity.SorterRepo
            );
            Assert.IsFalse(inputSorterPoolDiff.AnySwitchLevelDiffs);

            var inputDSwitchableRepoDiff = SwitchableRepoDiff.Make
            (
                competePoolStep.InputSwitchablePoolEntity.SwitchableRepo,
                newCompetePoolStep.InputSwitchablePoolEntity.SwitchableRepo
            );
            Assert.IsFalse(inputDSwitchableRepoDiff.HasDifferences);

            var sorterMonitiorPoolDiff = SorterResultPoolDiff.Make
            (
                competePoolStep.OutputSorterResultPoolEntity.SorterResultRepo,
                newCompetePoolStep.OutputSorterResultPoolEntity.SorterResultRepo
            );
            Assert.IsFalse(sorterMonitiorPoolDiff.AnySwitchResultDiffs);

            var outputSwitchableRepoDiff = SwitchableRepoDiff.Make
            (
                competePoolStep.OutputSwitchablePoolEntity.SwitchableRepo,
                newCompetePoolStep.OutputSwitchablePoolEntity.SwitchableRepo
            );
            Assert.IsFalse(outputSwitchableRepoDiff.HasDifferences);

            Assert.AreEqual(newCompetePoolStep.KeyCount, competePoolStep.KeyCount);
            Assert.AreEqual(newCompetePoolStep.Index, competePoolStep.Index);
            Assert.AreEqual(newCompetePoolStep.Name, competePoolStep.Name);
            Assert.AreEqual(newCompetePoolStep.SeedIn, competePoolStep.SeedIn);
            Assert.AreEqual(newCompetePoolStep.SeedOut, competePoolStep.SeedOut);

            Assert.AreEqual(newCompetePoolStep.GenerationCount, competePoolStep.GenerationCount);
            Assert.AreEqual(newCompetePoolStep.SwitchableChampCount, competePoolStep.SwitchableChampCount);
            Assert.AreEqual(newCompetePoolStep.SorterChampCount, competePoolStep.SorterChampCount);
            Assert.AreEqual(newCompetePoolStep.MutationRate, competePoolStep.MutationRate);
            Assert.AreEqual(newCompetePoolStep.SorterPoolSize, competePoolStep.SorterPoolSize);
            Assert.AreEqual(newCompetePoolStep.WasExecuted, competePoolStep.WasExecuted);
        }
    }
}
