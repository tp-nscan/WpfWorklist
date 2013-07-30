using MathUtils.SortableUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MathUtils.Tests.Bits
{
    [TestClass]
    public class IntegerBitUtilsFixture
    {
        [TestMethod]
        public void TestFlipBit()
        {
            const uint tV = 1 << 12;

            var res = tV.FilpBit(12);
            var res2 = res.FilpBit(12);
            Assert.IsTrue(res == 0);
            Assert.IsTrue(res2 == tV);
        }

        [TestMethod]
        public void TestHasBit()
        {
            const uint tV = 1 << 12;
            Assert.IsTrue(tV.BitValue(12));
            Assert.IsFalse(tV.BitValue(13));
        }
    }
}
