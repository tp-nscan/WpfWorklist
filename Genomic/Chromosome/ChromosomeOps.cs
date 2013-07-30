using System;
using System.Collections.Generic;
using System.Linq;
using Genomic.Locus;
using MathUtils.Repos;

namespace Genomic.Chromosome
{
    //public static class ChromosomeOps
    //{
    //    public static IChromosome Replicate
    //        (
    //            this IChromosome chromosome, 
    //            Func<IEnumerable<ILocus>, IChromosome> chromosomeMaker,
    //            IIndexedRepo<double> randomizer,
    //            double mutationRate
    //        )
    //    {
    //        if (randomizer.Size != chromosome.LocusLength)
    //        {
    //            throw new Exception("randomizer length not correct");
    //        }

    //        return chromosomeMaker(MutantLoci(chromosome.Loci.Select(T => T.Locus), randomizer.Items, mutationRate));
    //    }

    //    static IEnumerable<ILocus> MutantLoci(IEnumerable<ILocus> origLoci, IEnumerable<double> mutateThresholds, double mutationRate)
    //    {
    //        var lociEnumerator = origLoci.GetEnumerator();
    //        var mutateEnumerator = mutateThresholds.GetEnumerator();

    //        while (lociEnumerator.MoveNext() && mutateEnumerator.MoveNext())
    //        {
    //            yield return (lociEnumerator.Current.MutationRate > mutateEnumerator.Current)
    //                             ? lociEnumerator.Current.Mutator(lociEnumerator.Current, mutateEnumerator.Current, mutationRate)
    //                             : lociEnumerator.Current.NonMutator(lociEnumerator.Current, mutationRate);
    //        }
    //    }
    //}
}
