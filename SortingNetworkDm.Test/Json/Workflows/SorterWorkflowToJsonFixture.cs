using System;
using System.Linq;
using DynamicModel.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using SortingNetworkDm.Json.Workflows;
using SortingNetworkDm.TestData;
using SortingNetworkDm.Workflows;

namespace SortingNetworkDm.Test.Json.Workflows
{
    [TestClass]
    public class SorterWorkflowToJsonFixture
    {
        [TestMethod]
        public void TestCreate()
        {
            const string fileName = "TestFileName";
            var origSorterWorkflow = SorterWorkflow.Create(fileName);
            var serialized = JsonConvert.SerializeObject(SorterWorkflowToJson.ToJson(origSorterWorkflow));
            var sorterWorkflowToJson = JsonConvert.DeserializeObject<SorterWorkflowToJson>(serialized);
            var newSorterWorkflow = SorterWorkflowToJson.ToSorterWorkflow(sorterWorkflowToJson);
            Assert.AreEqual(newSorterWorkflow.FileName, fileName);
        }

        [TestMethod]
        public void TestSerializeHeaders()
        {
            const string fileName = "TestFileName";
            const string filePath = @"c:\TestFilePath\TestFilePath";
            var guid = Guid.NewGuid();

            var origSorterWorkflow = SorterWorkflow.Load
                (
                    name:fileName,
                    path:filePath,
                    guid:guid,
                    entities: Enumerable.Empty<IEntity>(),
                    steps:Enumerable.Empty<IStep>()
                );

            var serialized = JsonConvert.SerializeObject(SorterWorkflowToJson.ToJson(origSorterWorkflow));
            var sorterWorkflowToJson = JsonConvert.DeserializeObject<SorterWorkflowToJson>(serialized);
            var newSorterWorkflow = SorterWorkflowToJson.ToSorterWorkflow(sorterWorkflowToJson);
            Assert.AreEqual(newSorterWorkflow.FileName, fileName);
            Assert.AreEqual(newSorterWorkflow.FilePath, filePath);
            Assert.AreEqual(newSorterWorkflow.Guid, guid);
        }

        [TestMethod]
        public void TestCompletedCompetePoolWorkflow()
        {
            var origSorterWorkflow = TestWorkflows.OneCompletedCompetePoolWorkflow;

            var serialized = JsonConvert.SerializeObject(SorterWorkflowToJson.ToJson(origSorterWorkflow));
            var sorterWorkflowToJson = JsonConvert.DeserializeObject<SorterWorkflowToJson>(serialized);
            var newSorterWorkflow = SorterWorkflowToJson.ToSorterWorkflow(sorterWorkflowToJson);
            Assert.AreEqual(origSorterWorkflow.FileName, newSorterWorkflow.FileName);
            Assert.AreEqual(origSorterWorkflow.FilePath, newSorterWorkflow.FilePath);
            Assert.AreEqual(origSorterWorkflow.Guid, newSorterWorkflow.Guid);
            Assert.AreEqual(origSorterWorkflow.FileExtension, newSorterWorkflow.FileExtension);
            Assert.AreEqual(origSorterWorkflow.Entities.Count(), newSorterWorkflow.Entities.Count());
            Assert.AreEqual(origSorterWorkflow.Steps.Count(), newSorterWorkflow.Steps.Count());
        }

        [TestMethod]
        public void TestCompletedSorterPoolWorkflow()
        {
            var origSorterWorkflow = TestWorkflows.OneCompletedSorterPoolWorkflow;

            var serialized = JsonConvert.SerializeObject(SorterWorkflowToJson.ToJson(origSorterWorkflow));
            var sorterWorkflowToJson = JsonConvert.DeserializeObject<SorterWorkflowToJson>(serialized);
            var newSorterWorkflow = SorterWorkflowToJson.ToSorterWorkflow(sorterWorkflowToJson);
            Assert.AreEqual(origSorterWorkflow.FileName, newSorterWorkflow.FileName);
            Assert.AreEqual(origSorterWorkflow.FilePath, newSorterWorkflow.FilePath);
            Assert.AreEqual(origSorterWorkflow.Guid, newSorterWorkflow.Guid);
            Assert.AreEqual(origSorterWorkflow.FileExtension, newSorterWorkflow.FileExtension);
            Assert.AreEqual(origSorterWorkflow.Entities.Count(), newSorterWorkflow.Entities.Count());
            Assert.AreEqual(origSorterWorkflow.Steps.Count(), newSorterWorkflow.Steps.Count());
        }

        [TestMethod]
        public void TestCompletedSwitchablePoolWorkflow()
        {
            var origSorterWorkflow = TestWorkflows.OneCompletedSwitchablePoolWorkflow;

            var serialized = JsonConvert.SerializeObject(SorterWorkflowToJson.ToJson(origSorterWorkflow));
            var sorterWorkflowToJson = JsonConvert.DeserializeObject<SorterWorkflowToJson>(serialized);
            var newSorterWorkflow = SorterWorkflowToJson.ToSorterWorkflow(sorterWorkflowToJson);
            Assert.AreEqual(origSorterWorkflow.FileName, newSorterWorkflow.FileName);
            Assert.AreEqual(origSorterWorkflow.FilePath, newSorterWorkflow.FilePath);
            Assert.AreEqual(origSorterWorkflow.Guid, newSorterWorkflow.Guid);
            Assert.AreEqual(origSorterWorkflow.FileExtension, newSorterWorkflow.FileExtension);
            Assert.AreEqual(origSorterWorkflow.Entities.Count(), newSorterWorkflow.Entities.Count());
            Assert.AreEqual(origSorterWorkflow.Steps.Count(), newSorterWorkflow.Steps.Count());
        }

    }
}
