using System;
using System.Collections.Generic;
using Genomic.Chromosome;
using Genomic.Locus;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Genomic.Test.Chromosome
{
    [TestClass]
    public class ChromosomeFixture
    {
        [TestMethod]
        public void ChromosomeLociCount()
        {
            var chr = new ChromosomeBase(ThreeLoci(), null);
            Assert.AreEqual(chr.LocusLength, 3);
        }

        static IEnumerable<ILocus> ThreeLoci()
        {
            yield return new EmptyLocus(Guid.NewGuid());
            yield return new EmptyLocus(Guid.NewGuid());
            yield return new EmptyLocus(Guid.NewGuid());
        }
    }
}
