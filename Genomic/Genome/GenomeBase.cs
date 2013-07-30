using System;
using System.Collections.Generic;
using System.Linq;
using Genomic.Chromosome;

namespace Genomic.Genome
{
    public class GenomeBase : IGenome
    {
        public GenomeBase(Guid guid, IEnumerable<IChromosome> chromosomes)
        {
            _guid = guid;
            _chromosomes = chromosomes.ToList();
        }

        readonly List<IChromosome> _chromosomes;
        public IEnumerable<IChromosome> Chromosomes { get { return _chromosomes; } }

        private readonly Guid _guid;
        public Guid Guid
        {
            get { return _guid; }
        }
    }
}
