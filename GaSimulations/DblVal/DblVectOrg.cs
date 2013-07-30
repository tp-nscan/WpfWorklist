using System;
using System.Collections.Immutable;
using Ecosystem;
using Genomic.Genome;

namespace GaSimulations.DblVal
{
    public class DblVectOrg : IOrganisim
    {
        public DblVectOrg(Guid orgGuid, MonoChromGenome monoChromGenome, double reproductionRate)
        {
            _guid = orgGuid;
            _monoChromGenome = monoChromGenome;
            _reproductionRate = reproductionRate;
        }

        public DblVectOrg(Guid orgGuid, Guid genomeGuid, ref IImmutableStack<double> genes, double mutationRate, double reproductionRate)
            : this(orgGuid, MakeGenomeFromVector(genomeGuid, ref genes, mutationRate, false), reproductionRate)
        {
        }

        static MonoChromGenome MakeGenomeFromVector(Guid guid, ref IImmutableStack<double> geneVals, double mutationRate, bool mutationRateWasAdjusted)
        {
            return new MonoChromGenome(guid, DblValChromosome.Create(DblValGene.MakeDblValGenes(geneVals, mutationRate), mutationRate, mutationRateWasAdjusted));
        }

        private readonly Guid _guid;
        public Guid Guid
        {
            get { return _guid; }
        }

        private readonly double _reproductionRate;
        public double ReproductionRate
        {
            get { return _reproductionRate; }
        }

        private readonly MonoChromGenome _monoChromGenome;
        public MonoChromGenome MonoChromGenome
        {
            get { return _monoChromGenome; }
        }

        public int GeneCount
        {
            get { return MonoChromGenome.Chromosome.LocusLength; }
        }
    }

}
