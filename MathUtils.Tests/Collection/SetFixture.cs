using System.Linq;
using MathUtils.Collections;
using MathUtils.Rand;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MathUtils.Tests.Collection
{
    [TestClass]
    public class CommonFixture
    {
        [TestMethod]
        public void TestSwap()
        {
            var a1 = new MyClass() {Value = 1};
            var a2 = new MyClass() {Value = 2};
            Set.Swap(ref a1, ref a2);
            Assert.AreEqual(a1.Value, 2);
            Assert.AreEqual(a2.Value, 1);
        }

        [TestMethod]
        public void TestRandomDrawWithoutReplacement()
        {
            var testPool = Enumerable.Range(0, 100).ToList();
            var randomized = testPool.RandomDrawWithoutReplacement(Randy.Fast(22).ToInt()).ToList();
            Assert.IsTrue(testPool.HasSameElementsAs(randomized));
        }
    }

    class MyClass
    {
        public int Value;
    }
}
