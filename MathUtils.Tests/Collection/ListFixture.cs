using System.Collections.Generic;
using System.Linq;
using MathUtils.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MathUtils.Tests.Collection
{
    [TestClass]
    public class ScramblerFixture
    {
        [TestMethod]
        public void Scramble()
        {
            var elements = Enumerable.Range(5, 10).ToList();
            var scrambled = elements.Scramble(4);
            Assert.AreEqual(elements.Count(), scrambled.Count());
        }

        [TestMethod]
        public void IsTheSameAs()
        {
            var list1 = new[] {1, 2, 3, 3};
            var list2 = new[] {1, 2, 3};
            var list3 = new[] {3, 2, 3, 1};

            var res1 = list1.HasSameElementsRepeatsAllowed(list2);
            var res2 = list1.HasSameElementsRepeatsAllowed(list3);
            var res3 = list1.HasSameElementsRepeatsAllowed(list2);
        }

        
        [TestMethod]
        public void Recombine()
        {
            var aList = new List<string> {"a0", "a1", "a2", "a3", "a4", "a5", "a6"};
            var a2List = new List<string> {"a0", "a1", "a2", "a3", "a4", "a5", "a6"};
            var bList = new List<string> { "b0", "b1", "b2", "b3", "b4", "b5", "b6" };
            var swaps1 = new List<bool> { false, false, true, false, false, false, false};
            var swaps2 = new List<bool> { true, false, false, false, false, false, false };

            var aRes = new List<string> { "a0", "a1", "b2", "b3", "b4", "b5", "b6" };
            var bRes = new List<string> { "b0", "b1", "a2", "a3", "a4", "a5", "a6" };

            List<string> aOut;
            List<string> bOut;

            List.Recombine(aList, bList, swaps1, out aOut, out bOut);

            Assert.IsTrue(aOut.SequenceEqual(aRes));
            Assert.IsTrue(bOut.SequenceEqual(bRes));


            List.Recombine(aList, bList, swaps2, out aOut, out bOut);
            Assert.IsTrue(aOut.SequenceEqual(bList));
            Assert.IsTrue(bOut.SequenceEqual(aList));
        }


        [TestMethod]
        public void ChunkTest()
        {
            var aList = new List<string> { "a0", "a1", "a2", "a3", "a4", "a5", "a6", "a7" };

            var chunks = aList.Chunk(2).ToList();

            Assert.AreEqual(chunks.Count, 4);
            Assert.AreEqual(chunks[0].First(), "a0");
            Assert.AreEqual(chunks[0].Last(), "a1");
            Assert.AreEqual(chunks[3].First(), "a6");
            Assert.AreEqual(chunks[3].Last(), "a7");
        }

        [TestMethod]
        public void ChunkLimitTest()
        {
            var aList = new List<string> { "a0", "a1", "a2", "a3", "a4", "a5", "a6", "a7" };

            var chunks = aList.Chunk(3,2).ToList();

            Assert.AreEqual(chunks.Count, 2);
            Assert.AreEqual(chunks[0].First(), "a0");
            Assert.AreEqual(chunks[0].Last(), "a2");
            Assert.AreEqual(chunks[1].First(), "a3");
            Assert.AreEqual(chunks[1].Last(), "a5");
        }
    }
}
