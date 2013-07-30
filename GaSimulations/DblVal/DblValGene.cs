using System;
using System.Collections.Generic;
using System.Linq;
using Genomic.Locus;
using MathUtils.Rand;

namespace GaSimulations.DblVal
{
    public class DblValGene : LocusBase
    {
        public DblValGene(Guid guid, double value, double mutationRate)
            : base(guid, MakeMutantDblValGene)
        {
            _value = value;
            _mutationRate = mutationRate;
        }

        static DblValGene MakeMutantDblValGene(ILocus locus, double rndVal)
        {
            var dblValGene = locus as DblValGene;
            if (dblValGene == null)
            {
                throw new Exception("DblValGene is null");
            }
            if (rndVal < dblValGene.MutationRate)
            {
                return new DblValGene(locus.Guid, rndVal/dblValGene.MutationRate, dblValGene.MutationRate);
            }
            return dblValGene;
        }

        private readonly double _mutationRate;
        public double MutationRate
        {
            get { return _mutationRate; }
        }

        private readonly double _value;
        public double Value
        {
            get { return _value; }
        }

        public override LocusType LocusType
        {
            get { return LocusType.Gene; }
        }

        public static IEnumerable<DblValGene> MakeDblValGenes(int seed, int count, double mutationRate)
        {
            return Generators.Doubles(count, seed).Select(T => new DblValGene(Guid.NewGuid(), T, mutationRate));
        }

        public static IEnumerable<DblValGene> MakeDblValGenes(IEnumerable<double> values, double mutationRate)
        {
            return values.Select(T => new DblValGene(Guid.NewGuid(), T, mutationRate));
        }
    }
}
