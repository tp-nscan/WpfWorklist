using DynamicModel.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SortNetwork.TestData;
using SortingNetworkDm.TestData;

namespace SortingNetworkDm.Test.Steps
{
    [TestClass]
    public class SorterPoolStepFixture
    {
        const int CIndex = 1;

        [TestMethod]
        public void TestRandGenStep()
        {
            var sorterPoolStep = TestSteps.TheInitializedSorterPoolStep;

            sorterPoolStep.Execute
                (
                    runAgent: RunAgent.MakeTest()
                );

            Assert.IsTrue(sorterPoolStep.OutputSorterPoolEntity.SorterRepo.Count == TestConstants.SorterCount);
        }

    }
}
