using System;
using System.Linq.Expressions;
using EquationEditor.Models.Equation.SoftCircuits;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EquationEditor.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Expression<Func<int, int, bool>> f = (a, b) =>  a < b;

            //Expression<delegate> fg = (a, b) => 3;

            var res = f.Compile();

        }

        [TestMethod]
        public void Testparser()
        {
            var eval = new Eval();
            var lret = eval.TokenizeExpression("(2 + 2)");

        }

    }
}
