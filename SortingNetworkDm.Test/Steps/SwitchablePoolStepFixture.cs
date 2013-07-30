using DynamicModel.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SortNetwork.TestData;
using SortingNetworkDm.TestData;

namespace SortingNetworkDm.Test.Steps
{
    [TestClass]
    public class SwitchablePoolStepFixture
    {
        [TestMethod]
        public void TestRandGenStep()
        {
            var switchableGenOp = TestSteps.TheInitializedSwitchablePoolStep;

            switchableGenOp.Execute
                (
                    runAgent: RunAgent.MakeTest()
                );

            Assert.IsTrue(switchableGenOp.OutputSwitchablePoolEntity.SwitchableRepo.Count == TestConstants.SwitchableCount);
        }
    }
}
