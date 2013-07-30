using System;
using System.Collections.Immutable;
using System.Linq;
using GaSimulations.DblVal;
using Genomic.Chromosome;
using MathUtils.Collections;
using MathUtils.Diff;
using MathUtils.Interval;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GaSimulations.Test.DblVal
{
    [TestClass]
    public class DblValGeneFixture
    {
        [TestMethod]
        public void TestMutate()
        {
            const int geneCount = 1000;
            const double mutationRate = 0.1; 
            const int seed1 = 333;
            var chr = new ChromosomeBase(DblValGene.MakeDblValGenes(seed1, geneCount, mutationRate), Replicator);
            var stack = new[] { 0.0, 0.0, 0.0 }.ToImmutableStack();
            var chr2 = chr.Replicate(ref stack);
            Assert.AreEqual(chr2.LocusLength, geneCount);
            var chrComp = new OrderedListDiff<DblValGene, DblValGene>
                (
                    chr.Loci.Select(T => (DblValGene)T.Locus), 
                    chr2.Loci.Select(T => (DblValGene)T.Locus), 
                    (t, u) => Math.Abs(t.Value - u.Value) < 0.001
                );

            Assert.IsTrue(
                new IntInterval (
                                    (int)(geneCount * mutationRate * 0.9), 
                                    (int)(geneCount * mutationRate * 1.1)
                                ).ClosureContains(chrComp.Diffs.Count()));
        }

        static IChromosome Replicator(IChromosome c, IImmutableStack<double> randos)
        {
            return c.Replicate(ref randos);
        }
    }
}
