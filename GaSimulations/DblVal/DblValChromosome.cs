using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Immutable;
using Genomic.Chromosome;
using Genomic.Locus;
using MathUtils.Collections;

namespace GaSimulations.DblVal
{
    public class DblValChromosome : ChromosomeBase
    {
        private DblValChromosome
            (
                IEnumerable<ILocus> loci,
                double mutationRate,
                bool mutationRateWasAdjusted
            )
            : base(loci, MakeReplicator())
        { 
            _mutationRate = mutationRate;
            MutationRateWasAdjusted = mutationRateWasAdjusted;
        }

        public static DblValChromosome Create(ref IImmutableStack<double> values, int count, double mutationRate)
        {
            IImmutableStack<double> myValues;
            values = values.MakeSubStack(out myValues, count);
            return new DblValChromosome
                (
                    values.Select(T=>new DblValGene(Guid.NewGuid(), T, mutationRate)),
                    mutationRate,
                    false
                );
        }

        private bool _mutationRateWasAdjusted;
        public bool MutationRateWasAdjusted
        {
            get { return _mutationRateWasAdjusted; }
            private set
            {
                _mutationRateWasAdjusted = value;
            }
        }

        public static DblValChromosome Create(IEnumerable<ILocus> loci, double mutationRate, bool mutationRateWasAdjusted)
        {
            return new DblValChromosome
                (
                    loci.Cast<DblValGene>().Select(T => new DblValGene(Guid.NewGuid(), T.Value, mutationRate)),
                    mutationRate,
                    mutationRateWasAdjusted
                );
        }

        static Func<IChromosome, IEnumerable<double>, IChromosome> MakeReplicator()
        {
            return (c, doubles) =>
                {
                    var dbl = c as DblValChromosome;
                    if (dbl == null)
                    {
                        throw new Exception("DblValChromosome is null");
                    }

                    return new DblValChromosome
                    (
                        doubles.JoinWith(c.Loci).Select(T=>T.Item2.Locus.Replicate(T.Item1)),
                        dbl.MutationRate,
                        dbl.MutationRateWasAdjusted
                    );
                };
        }

        private readonly double _mutationRate;
        public double MutationRate
        {
            get { return _mutationRate; }
        }
    }
}
