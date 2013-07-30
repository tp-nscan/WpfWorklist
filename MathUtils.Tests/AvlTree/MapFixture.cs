using System.Linq;
using MathUtils.AvlTree;
using MathUtils.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MathUtils.Tests.AvlTree
{
    [TestClass]
    public class MapFixture
    {
        [TestMethod]
        public void CanGetSuccessfully()
        {
            var m = Map<int, int>.Empty
                .Add(1, 11)
                .Add(2, 22)
                .Add(3, 33);

            Assert.AreEqual(11, m[1]);
            Assert.AreEqual(22, m[2]);
            Assert.AreEqual(33, m[3]);
        }

        [TestMethod]
        public void CanEnumerateSuccessfully()
        {
            var m = Map<int, int>.Empty
                .Add(1, 11)
                .Add(2, 22)
                .Add(3, 33);

            var enumer = m.GetEnumerator();

            enumer.MoveNext();
            Assert.AreEqual(11, enumer.Current);
            enumer.MoveNext();
            Assert.AreEqual(22, enumer.Current);
            enumer.MoveNext();
            Assert.AreEqual(33, enumer.Current);
        }

        [TestMethod]
        public void LlCase()
        {
            var m = Map<int, int>.Empty
                .Add(5, 1)
                .Add(4, 2)
                .Add(3, 3);

            Assert.AreEqual(4, m.Root.Key);
            Assert.AreEqual(3, m.Root.Left.Key);
            Assert.AreEqual(5, m.Root.Right.Key);
        }

        [TestMethod]
        public void TreeRemainsBalancedAfterUnbalancedInsertIntoBalancedTree()
        {
            var m = Map<int, int>.Empty
                .Add(5, 1)
                .Add(4, 2)
                .Add(3, 3)
                .Add(2, 4)
                .Add(1, 5);

            Assert.AreEqual(4, m.Root.Key);
            Assert.AreEqual(2, m.Root.Left.Key);
            Assert.AreEqual(1, m.Root.Left.Left.Key);
            Assert.AreEqual(3, m.Root.Left.Right.Key);
            Assert.AreEqual(5, m.Root.Right.Key);
        }

        [TestMethod]
        public void LrCase()
        {
            var m = Map<int, int>.Empty
                .Add(5, 1)
                .Add(3, 2)
                .Add(4, 3);

            Assert.AreEqual(4, m.Root.Key);
            Assert.AreEqual(3, m.Root.Left.Key);
            Assert.AreEqual(5, m.Root.Right.Key);
        }

        [TestMethod]
        public void RrCase()
        {
            var m = Map<int, int>.Empty
                .Add(3, 1)
                .Add(4, 2)
                .Add(5, 3);

            Assert.AreEqual(4, m.Root.Key);
            Assert.AreEqual(3, m.Root.Left.Key);
            Assert.AreEqual(5, m.Root.Right.Key);
        }

        [TestMethod]
        public void RlCase()
        {
            var m = Map<int, int>.Empty
                .Add(3, 1)
                .Add(5, 2)
                .Add(4, 3);

            Assert.AreEqual(4, m.Root.Key);
            Assert.AreEqual(3, m.Root.Left.Key);
            Assert.AreEqual(5, m.Root.Right.Key);
        }

        [TestMethod]
        public void EnumerateInOrder()
        {
            var scrambledNums = Enumerable.Range(5, 1000).Scramble(333).ToList();
            var map = Map<int, int>.Empty;

            foreach (var scrambledNum in scrambledNums)
            {
               map = map.Add(scrambledNum, scrambledNum);
            }

            var reorderedNums = map.ToList();
            Assert.IsTrue(scrambledNums.HasSameElementsRepeatsAllowed(reorderedNums));
            Assert.IsTrue(reorderedNums.IsOrdered());
        }
    }
}
