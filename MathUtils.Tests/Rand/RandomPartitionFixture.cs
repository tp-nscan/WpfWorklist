using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MathUtils.Rand;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MathUtils.Tests.Rand
{
    [TestClass]
    public class RandomPartitionFixture
    {
        [TestMethod]
        public void TestThreeParts()
        {
            var rando = new ReadOnlyCollection<double> (Generators.Doubles(StringSet.Count(), 557).ToList());
            var splits = new ReadOnlyCollection<int>(new[] { 2, 5 });
            var randomPartition = new RandomPartition<string>(new ReadOnlyCollection<string>(StringSet.ToList()), rando, splits);

            var groups = randomPartition.Partitions.ToList();

            Assert.AreEqual(groups.Count, 3);
            Assert.AreEqual(groups[0].Count, 2);
            Assert.AreEqual(groups[1].Count, 5);
            Assert.AreEqual(groups[2].Count, 3);
        }

        IEnumerable<string> StringSet
        {
            get
            {
                yield return "A";
                yield return "B";
                yield return "C";
                yield return "D";
                yield return "E";
                yield return "F";
                yield return "G";
                yield return "H";
                yield return "I";
                yield return "J";
            }
        }
    }
}
