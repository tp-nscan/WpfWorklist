using System;
using System.Linq;
using DynamicModel.Common;
using DynamicModel.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DynamicModel.Test
{
    [TestClass]
    public class WorkflowGroupFixture
    {
        [TestMethod]
        public void TestAddRemoveWorkflow()
        {
            var workflow = Workflow.MakeTest
            (
                "test"
            );

            var testStep = Step.MakeTest("first", 0, new[] { Entity.MakeTest("first_A"), Entity.MakeTest("first_B") });
            workflow.AddStep(testStep);

            var workflowGroup = WorkflowGroup.Make();
            workflowGroup.AddWorkflow(workflow);

            Assert.AreEqual(workflowGroup.Entities.Count(), 2);
            Assert.AreEqual(workflowGroup.Workflows.Count(), 1);

            testStep.Execute(RunAgent.MakeTest());

            Assert.AreEqual(workflowGroup.Entities.Count(), 3);
            Assert.AreEqual(workflowGroup.Workflows.Count(), 1);


            workflowGroup.RemoveWorkflow(workflow);

            Assert.AreEqual(workflowGroup.Entities.Count(), 0);
            Assert.AreEqual(workflowGroup.Workflows.Count(), 0);
        }
    }
}
