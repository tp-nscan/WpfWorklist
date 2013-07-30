using System.Collections.Generic;
using System.Collections.Immutable;
using Genomic.Locus;

namespace Genomic.Chromosome
{
    public interface IChromosome
    {
        ILinkedLocus Centromere { get; }
        IEnumerable<ILinkedLocus> Loci { get; }
        int LocusLength { get; }
        IChromosome Replicate(ref IImmutableStack<double> loci);
    }
}