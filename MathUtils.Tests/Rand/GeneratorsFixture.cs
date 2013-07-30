using System.Linq;
using MathUtils.Collections;
using MathUtils.Interval;
using MathUtils.Rand;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MathUtils.Tests.Rand
{
    [TestClass]
    public class GeneratorsFixture
    {
        [TestMethod]
        public void TestBitGenerator()
        {
            var target = new RealInterval(0.28, 0.32);
            var stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();
            var seq = Generators.RandomBits(2000, 557, 0.3).ToList();
            var res = seq.Average(T => T ? 1 : 0);
            stopwatch.Stop();
            var count = stopwatch.ElapsedMilliseconds;
            Assert.IsTrue(target.Contains(res));
            //IStructuralComparable f;
        }

        [TestMethod]
        public void TestExpGenerator()
        {
            var res = Generators.ExpDist(20, 557, 0.3);
        }

        [TestMethod]
        public void TestStraightPermutation()
        {
            Assert.IsTrue(Generators.IntegerPermutation(Randy.Fast(555).ToInt(), 23).HasSameElementsAs(Enumerable.Range(0, 23)));
        }
    }
}
