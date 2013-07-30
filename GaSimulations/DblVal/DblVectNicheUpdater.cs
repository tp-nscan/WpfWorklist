using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using Ecosystem;
using Ecosystem.Habitat.TGrid;
using Ecosystem.Migrator;
using Ecosystem.Niche;
using Genomic.Genome;
using MathUtils.Rand;
using MathUtils.Repos;
using MathUtils.VectorSpace;

namespace GaSimulations.DblVal
{
    public class DblVectNicheUpdater : INicheUpdater
    {
        public DblVectNicheUpdater(IEnumerable<double> targetVector)
        {
            _targetVector = targetVector.ToList();
        }

        private readonly List<double> _targetVector;

        public INiche UpdateNiche
            (
                INiche oldNiche, 
                INicheImmigrants nicheImmigrants,
                IImmutableStack<double> rndMigrateDirection, 
                IImmutableStack<double> rndMutateGenes, 
                IImmutableStack<Tuple<double,double>> rndMutateRates
            )
        {
            var gnOld = oldNiche as IGridNiche;
            if (gnOld == null)
            {
                throw new Exception("oldNiche was null");
            }

            var parentOrgs = oldNiche.Organisims.Cast<DblVectOrg>().ToList();

            parentOrgs.AddRange(nicheImmigrants.Immigrants.Cast<DblVectOrg>());

            var nextGen = new List<DblVectOrg>();
            nextGen.AddRange(parentOrgs);

            foreach (var dblVectOrg in parentOrgs)
            {
                for (var childDex = 0; childDex < dblVectOrg.ReproductionRate; childDex++)
                {
                    var res = dblVectOrg.MonoChromGenome.Chromosome.Replicate(ref rndMutateGenes) as DblValChromosome;

                    var mr = rndMutateRates.Peek();
                    if (mr.Item1 < 0.01)
                    {
                        res = DblValChromosome.Create(res.Loci.Select(T => T.Locus), rndMutateRates.Peek().Item2, true);
                    }
                    rndMutateRates = rndMutateRates.Pop();
                    nextGen.Add
                    (
                        new DblVectOrg
                            (
                                Guid.NewGuid(),
                                new MonoChromGenome(Guid.NewGuid(), res), 
                                dblVectOrg.ReproductionRate
                            )
                    );
                }
            }

            var evaluatedOrgs = nextGen.Select(T =>
                new Tuple<double, DblVectOrg>
                        (
                            T.MonoChromGenome.Chromosome.Loci.Select(l => l.Locus)
                                .Cast<DblValGene>()
                                .Select(g => g.Value)
                                .DistanceSquared(_targetVector),
                            T
                       )
            );

            var winners = evaluatedOrgs.OrderBy(T => T.Item1)
                                       .Take(oldNiche.OrganisimCount + oldNiche.MigrantCount)
                                       .Select(e => e.Item2).ToList();

            double migrantScores = 0;
            double rate = 0;
            int wAdjusted = 0;
            int eUnAdjusted = 0;

            if (winners.Count > 0)
            {
                migrantScores = winners.Average(T =>
                        T.MonoChromGenome.Chromosome.Loci.Sum(
                        l => ((DblValGene)l.Locus).Value)
                    );
                rate = winners.Average(T =>
                        T.MonoChromGenome.Chromosome.Loci.Average(
                        l => ((DblValGene)l.Locus).MutationRate)
                    );
                wAdjusted = winners.Count(T =>
                        ((DblValChromosome)T.MonoChromGenome.Chromosome).MutationRateWasAdjusted);

                eUnAdjusted = nextGen.Count(T => !
                        ((DblValChromosome)T.MonoChromGenome.Chromosome).MutationRateWasAdjusted);
            }

            System.Diagnostics.Debug.WriteLine
                (
                    String.Format
                    (
                    "ave score: {0}\trate: {1}\t W_adj: {2}\t NW_adj: {3}", 
                        migrantScores.ToString("0.00000"), 
                        rate.ToString("0.0000"),
                        wAdjusted,
                        eUnAdjusted
                    )
                );



            ReadOnlyCollection<double> migrationRepo;
            rndMigrateDirection = rndMigrateDirection.PopToCollection(oldNiche.OrganisimCount + oldNiche.MigrantCount, out migrationRepo);
            var rp = new RandomPartition<IOrganisim>
                (
                    new ReadOnlyCollection<IOrganisim>(winners.Cast<IOrganisim>().ToList()),
                    migrationRepo,
                    new ReadOnlyCollection<int>(new[]
                        {
                            oldNiche.OrganisimCount,
                            oldNiche.MigrantCount
                        }));

            return new GridNiche(gnOld.Guid, gnOld.Location, rp.Partitions.First(), rp.Partitions.Skip(1).First(), oldNiche.OrganisimCount, oldNiche.MigrantCount);
        }
    }
}
