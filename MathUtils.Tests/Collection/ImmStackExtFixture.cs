using System.Collections.Immutable;
using System.Linq;
using MathUtils.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MathUtils.Tests.Collection
{
    [TestClass]
    public class ImmStackExtFixture
    {
        [TestMethod]
        public void MakeImmutableStack()
        {
            const int stackSize = 500;
            var stack = Enumerable.Range(0, stackSize).ToList().ToImmutableStack();
            Assert.AreEqual(stack.Count(), stackSize);
        }

        [TestMethod]
        public void MakeSubStack()
        {
            const int stackSize = 500;
            const int subStackSize = 100;
            var stack = Enumerable.Range(0, stackSize).ToList().ToImmutableStack();
            IImmutableStack<int> subStack;
            stack = stack.MakeSubStack(out subStack, subStackSize);

            Assert.AreEqual(stack.Count(), stackSize - subStackSize);
            Assert.AreEqual(subStack.Count(), subStackSize);
        }
    }
}
