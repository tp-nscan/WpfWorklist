using System;
using System.Linq;
using Genomic.Chromosome;

namespace Genomic.Genome
{
    public class MonoChromGenome : GenomeBase
    {
        public MonoChromGenome(Guid guid, IChromosome chromosome) : base(guid, new[] {chromosome})
        {
            if (chromosome == null)
            {
                throw new Exception("chromosome was null");
            }
        }

        public IChromosome Chromosome { get { return Chromosomes.Single(); } }
    }
}
