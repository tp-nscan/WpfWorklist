using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathUtils.Rand;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MathUtils.Tests.Rand
{
    [TestClass]
    public class MutatorFixture
    {
        [TestMethod]
        public void TestMutator()
        {
            var oneList = new[] {10, 11, 12, 13, 14, 15};
            var twoList = new[] {20, 21, 22, 23, 24, 25};

            var changeList = new[] {false, true, false, false, true, false};
            var expectedList = new[] {10, 21, 12, 13, 24, 15}.ToList();

            var mutatedList = oneList.MutateZip(twoList, changeList).ToList();

            Assert.IsTrue(expectedList.SequenceEqual(mutatedList));
        }


    }
}
