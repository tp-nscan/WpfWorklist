using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SortingNetwork.KeyPair;

namespace SortingNetwork.Test.KeySets
{
    [TestClass]
    public class KeyPairsFixture
    {
        [TestMethod]
        public void TestMethod1()
        {
            const string valueString = "onetwothree";
            var d = new Dictionary<IKeyPair, string>();

            var kp = KeySet.Instance.GetKeyPair(1, 2, 3);

            d.Add(kp, valueString);
            var kp2 = KeySet.Instance.GetKeyPair(1, 2, 3);

            Assert.AreEqual(d[kp2], valueString);
        }
    }
}
