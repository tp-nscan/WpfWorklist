using System;
using System.Linq;
using DynamicModel.Common;
using DynamicModel.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DynamicModel.Test
{
    [TestClass]
    public class WorkflowFixture
    {
        [TestMethod]
        public void AddAndRemoveStep()
        {
            var workflow = Workflow.MakeTest
                (
                    "test"
                );

            var testStep =Step.MakeTest("first", 0, new[] { Entity.MakeTest("first_A"), Entity.MakeTest("first_B") });
            workflow.AddStep(testStep);
            Assert.AreEqual(workflow.Entities.Count(), 2);
            Assert.AreEqual(workflow.Steps.Count(), 1);

            workflow.RemoveStep(testStep);
            Assert.AreEqual(workflow.Entities.Count(), 0);
            Assert.AreEqual(workflow.Steps.Count(), 0);
        }

        [TestMethod]
        public void UpdateStep()
        {
            var workflow = Workflow.MakeTest
                (
                    "test"
                );

            var testStep = Step.MakeTest("first", 0, new[] { Entity.MakeTest("first_A"), Entity.MakeTest("first_B") });
            workflow.AddStep(testStep);
            Assert.AreEqual(workflow.Entities.Count(), 2);
            Assert.AreEqual(workflow.Steps.Count(), 1);

            testStep.Execute(RunAgent.MakeTest());

            Assert.AreEqual(workflow.Entities.Count(), 3);
            Assert.AreEqual(workflow.Steps.Count(), 1);

            workflow.RemoveStep(testStep);
            Assert.AreEqual(workflow.Entities.Count(), 0);
            Assert.AreEqual(workflow.Steps.Count(), 0);

        }
    }
}
