using System;
using System.Collections.Generic;
using Genomic.Chromosome;

namespace Genomic.Genome
{
    public interface IGenome
    {
        IEnumerable<IChromosome> Chromosomes { get; }
        Guid Guid { get; }
    }
}