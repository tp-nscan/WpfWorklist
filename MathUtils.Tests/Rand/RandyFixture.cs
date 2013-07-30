using MathUtils.Rand;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MathUtils.Tests.Rand
{
    [TestClass]
    public class RandyFixture
    {
        [TestMethod]
        public void TestStandard()
        {
            const long cReps = 10000000;
            var sw = new System.Diagnostics.Stopwatch();
            sw.Start();

            var randy = Randy.Standard(123).ToDouble();
            for (long i = 0; i < cReps; i++)
            {
                var res = randy.Next();
            }

            sw.Stop();
            System.Diagnostics.Debug.WriteLine(sw.ElapsedMilliseconds);
            System.Diagnostics.Debug.WriteLine(randy.UseCount);
        }

        [TestMethod]
        public void TestFast()
        {
            const long cReps = 10000000;
            var sw = new System.Diagnostics.Stopwatch();
            sw.Start();

            var randy = Randy.Fast(123).ToDouble();
            for (long i = 0; i < cReps; i++)
            {
                var res = randy.Next();
            }

            sw.Stop();
            System.Diagnostics.Debug.WriteLine(sw.ElapsedMilliseconds);
        }

        [TestMethod]
        public void TestRandomBoolToShort()
        {
            var randy = Randy.Fast(123).ToBool(0.5);

            for (int i = 0; i < 100; i++)
            {
                System.Diagnostics.Debug.WriteLine((ushort)randy.ToInt().Next());
            }
        }
    }
}
