using System;
using System.Linq;
using MathUtils.Interval;
using MathUtils.Rand;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MathUtils.Tests.Rand
{
    [TestClass]
    public class WeightedDistributionFixture
    {
        [TestMethod]
        public void TestProbabilities()
        {
            var wd = new WeightedDistribution<string>(555,
                new[]
                    {
                        new Tuple<double, string>(1.0, "SpongeBob"),
                        new Tuple<double, string>(2.0, "Patrick"),
                        new Tuple<double, string>(3.0, "Squidward")
                    });

            var sbRange = new RealInterval(0.13, 0.2);
            var pRange = new RealInterval(0.30, 0.36);
            var sqRange = new RealInterval(0.47, 0.53);

            var results = wd.DrawWeighted(10000, 554).ToList();
            Assert.IsTrue(sbRange.Contains(results.Average(T => T == "SpongeBob" ? 1 : 0)));
            Assert.IsTrue(pRange.Contains(results.Average(T => T == "Patrick" ? 1 : 0)));
            Assert.IsTrue(sqRange.Contains(results.Average(T => T == "Squidward" ? 1 : 0)));
        }
    }
}
