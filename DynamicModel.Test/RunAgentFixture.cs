using System;
using DynamicModel.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DynamicModel.Test
{
    [TestClass]
    public class RunAgentFixture
    {
        [TestMethod]
        public void TestRunAgent()
        {
            var ra = RunAgent.MakeTest();
            ra.OnReport.Subscribe(Sub);

        }

        void Sub(IRunMessage runMessage)
        {
            
        }
    }
}
