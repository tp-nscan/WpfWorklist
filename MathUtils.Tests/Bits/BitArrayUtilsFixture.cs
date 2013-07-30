using System.Linq;
using MathUtils.SortableUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MathUtils.Tests.Bits
{
    [TestClass]
    public class BitArrayUtilsFixture
    {
        [TestMethod]
        public void TestCompare()
        {
            var pos1 = new[] { false, true, false, false };
            var pos1A = new[] { false, true, false, false };
            var pos3 = new[] { false, false, false, true };

            Assert.AreEqual(pos1.CompareDict(pos3, 4), -1);
            Assert.AreEqual(pos3.CompareDict(pos1, 4), 1);
            Assert.AreEqual(pos1.CompareDict(pos1A, 4), 0);
        }

        [TestMethod]
        public void TestSorted()
        {
            var baSorted = new[] { false, true, true, true };
            var baSorted2 = new[] { true, true, true, true };
            var baSorted3 = new[] { false, false, false, false };
            var baSorted4 = new[] { false };
            var baUnsorted = new[] { false, true, false, false };

            Assert.IsTrue(baSorted.IsSorted(4));
            Assert.IsTrue(baSorted2.IsSorted(4));
            Assert.IsTrue(baSorted3.IsSorted(4));
            Assert.IsTrue(baSorted4.IsSorted(1));
            Assert.IsFalse(baUnsorted.IsSorted(4));
        }

        [TestMethod]
        public void TestBitArrayFromByte()
        {
            byte testByte = 0x01;
            var bits = testByte.ToBools().ToArray();
            Assert.IsTrue(bits[0]);
            Assert.IsFalse(bits[1]);
            Assert.IsFalse(bits[2]);
            Assert.IsFalse(bits[3]);
            Assert.IsFalse(bits[4]);
            Assert.IsFalse(bits[5]);
            Assert.IsFalse(bits[6]);
            Assert.IsFalse(bits[7]);

            testByte = 0xFE;
            bits = testByte.ToBools().ToArray();
            Assert.IsFalse(bits[0]);
            Assert.IsTrue(bits[1]);
            Assert.IsTrue(bits[2]);
            Assert.IsTrue(bits[3]);
            Assert.IsTrue(bits[4]);
            Assert.IsTrue(bits[5]);
            Assert.IsTrue(bits[6]);
            Assert.IsTrue(bits[7]);
        }

        [TestMethod]
        public void TestByteFromBitArray()
        {
            byte testByte = 0x01;
            var bits = testByte.ToBools().ToArray();
            byte retByte = bits.ToByte();
            Assert.AreEqual(testByte, retByte);

            testByte = 0xA1;
            bits = testByte.ToBools().ToArray();
            retByte = bits.ToByte();
            Assert.AreEqual(testByte, retByte);

            testByte = 0x4D;
            bits = testByte.ToBools().ToArray();
            retByte = bits.ToByte();
            Assert.AreEqual(testByte, retByte);
        }

        [TestMethod]
        public void TestXor()
        {
            var bsA = new[] { false, true, false, false };
            var bsB = new[] { false, false, false, true };

            var zor = bsA.Xor(bsB);
            Assert.IsTrue(zor.SequenceEqual(new[] {false, true, false, true}));
        }

        [TestMethod]
        public void TestFlip()
        {
            var bsA = new[] { false, true, false, false };
            var bsB = new[] { false, false, false, true };

            var zor = bsA.FlipWhen(bsB);
            Assert.IsTrue(zor.SequenceEqual(new[] { false, true, false, true }));
        }

        [TestMethod]
        public void TestAllBitSetsOfLength()
        {
            const int testLen = 18;

            foreach (var bitArray in SortableBitArrayUtils.AllBitSetsOfLength(testLen))
            {
                //System.Diagnostics.Debug.WriteLine(
                //    bitArray.Aggregate
                //    (
                //        "", 
                //        (s, bv) => s + (string.IsNullOrEmpty(s) ? string.Empty : ", " ) + bv + "\t")
                //    );
                System.Diagnostics.Debug.WriteLine(
                    bitArray.Aggregate
                    (
                        "",
                        (s, bv) => s + ", " + bv + "\t")
                    );
            }
        }
    }
}
