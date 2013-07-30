using Microsoft.VisualStudio.TestTools.UnitTesting;
using SortingNetwork.Common;
using SortingNetwork.KeyPair;

namespace SortingNetwork.Test.Common
{
    [TestClass]
    public class CommonExFixture
    {
         [TestMethod]
         public void TestToIndex()
         {
             var kp235 = KeySet.Instance.GetKeyPair(2, 3, 5);

             var kp023 = KeySet.Instance.GetKeyPair(0, 2, 3);

             var kp234 = KeySet.Instance.GetKeyPair(2, 3, 4);

             Assert.AreEqual(kp235.Index, 7);

             Assert.AreEqual(kp023.Index, 1);

             Assert.AreEqual(kp234.Index, 5);
         }
    }
}
