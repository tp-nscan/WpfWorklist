using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Genomic.Chromosome;
using Genomic.Locus;
using MathUtils.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Genomic.Test.Chromosome
{
    [TestClass]
    public class ChromosomeOpsFixture
    {
        [TestMethod]
        public void Replicate()
        {
            var chr = new ChromosomeBase(ThreeLoci(), Replicator);
            var stack = new[] {0.0, 0.0, 0.0}.ToImmutableStack();
            var chr2 = chr.Replicate(ref stack);
            Assert.AreEqual(chr2.LocusLength, 3);
        }

        static IChromosome Replicator(IChromosome c, IImmutableStack<double> randos)
        {
            return c.Replicate(ref randos);
        }

        static IEnumerable<ILocus> ThreeLoci()
        {
            yield return new EmptyLocus(Guid.NewGuid());
            yield return new EmptyLocus(Guid.NewGuid());
            yield return new EmptyLocus(Guid.NewGuid());
        }
    }
}
