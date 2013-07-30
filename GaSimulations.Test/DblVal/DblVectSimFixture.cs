using System;
using System.Collections.ObjectModel;
using System.Linq;
using Ecosystem.Habitat;
using Ecosystem.Habitat.TGrid;
using Ecosystem.Migrator;
using GaSimulations.DblVal;
using MathUtils.Collections;
using MathUtils.Rand;
using MathUtils.Repos;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GaSimulations.Test.DblVal
{
    [TestClass]
    public class DblVectSimFixture
    {
        [TestMethod]
        public void TestMakeHabitat()
        {
            const int gridSize = 10;
            const int vectorLength = 20;
            const int orgsPerNiche = 17;
            const int migrantsPerNiche = 9;
            const int replicantsPerOrg = 5;
            const int seed = 447;
            const double mutationRate = 0.1;

            var habitat = DblVectSim.MakeHabitat(gridSize, vectorLength, orgsPerNiche, migrantsPerNiche,
                                                   replicantsPerOrg, mutationRate, seed);

            Assert.AreEqual(habitat.GridSize, gridSize);
            Assert.AreEqual(habitat.Niches.Count() , gridSize* gridSize);
            Assert.AreEqual(habitat.Niches.First().Organisims.Count(), orgsPerNiche);
            Assert.AreEqual(habitat.Niches.First().Migrants.Count(), migrantsPerNiche);
            var org = (DblVectOrg) habitat.Niches.First().Migrants.First();
            Assert.AreEqual(org.MonoChromGenome.Chromosome.LocusLength, vectorLength);
            var locus = (DblValGene)org.MonoChromGenome.Chromosome.Loci.First().Locus;
            Assert.AreEqual(locus.MutationRate, mutationRate);
        }

        //[TestMethod]
        //public void TestUpdateHabitat()
        //{
        //    const int gridSize = 5;
        //    const int vectorLength = 20;
        //    const int paddedVectorLength = 25;
        //    const int orgsPerNiche = 17;
        //    const int migrantsPerNiche = 8;
        //    const int replicantsPerOrg = 5;
        //    const int seed = 447;
        //    const double startingMutationRate = 0.0876;
        //    const double maxMutationRate = 0.5;
        //    const double power = 2.5;
        //    const int reps = 40;

        //    var stopwatch = new System.Diagnostics.Stopwatch();
        //    stopwatch.Start();

        //    var habitat = DblVectSim.MakeHabitat(gridSize, vectorLength, orgsPerNiche, migrantsPerNiche,
        //                                           replicantsPerOrg, startingMutationRate, seed);

        //    var targetVector = Enumerable.Repeat(0.0, vectorLength).ToList();
        //    var targetVector2 = Enumerable.Repeat(0.5, vectorLength).ToList();

        //    for (var i = 0; i < reps; i++)
        //    {
        //        System.Diagnostics.Debug.WriteLine("\nRound " + i);

        //        var rndMigrate = new ReadOnlyCollection<int>(Generators.Ints(gridSize * gridSize, seed + 1 + i*5).ToList());
        //        var rndMigrateSelect = new ReadOnlyCollection<double>(Generators.Doubles(gridSize * gridSize * (orgsPerNiche + migrantsPerNiche), seed + 2 + i * 5).ToList());
        //        var rndMutateGenes = new ReadOnlyCollection<double>(Generators.Doubles(gridSize * gridSize * (orgsPerNiche + migrantsPerNiche) * replicantsPerOrg * paddedVectorLength, seed + 3 + i * 5).ToList());
        //        var rndMutateRate = new ReadOnlyCollection<Tuple<double, double>>
        //            (
        //                 Generators.Doubles(gridSize * gridSize * (orgsPerNiche + migrantsPerNiche) * replicantsPerOrg, seed + 3 + i * 5)
        //                    .JoinWith ( 
        //                     Generators.PowDist(gridSize * gridSize * (orgsPerNiche + migrantsPerNiche) * replicantsPerOrg, 
        //                                        seed + 3 + i * 5, maxMutationRate, power)
        //                              ).ToList()
                        
        //                );

        //        var hupdator = new HabitatUpdater
        //            (
        //                migrator: HabitatMigrator.Migrate,
        //                habitatBuilder: DblVectSim.MakeHabitat,
        //                nicheUpdater: new DblVectNicheUpdater(targetVector)
        //            );

        //        var hab2 = hupdator.Update(habitat, rndMigrate, rndMigrateSelect, rndMutateGenes, rndMutateRate);
        //        habitat = (IGridHabitat) hab2;
        //    }

        //    for (var i = 0; i < reps; i++)
        //    {
        //        System.Diagnostics.Debug.WriteLine("\nRound " + i);

        //        var rndMigrate = new ReadOnlyCollection<int>(Generators.Ints(gridSize * gridSize, seed + 1 + i * 5).ToList());
        //        var rndMigrateSelect = new ReadOnlyCollection<double>(Generators.Doubles(gridSize * gridSize * (orgsPerNiche + migrantsPerNiche), seed + 2 + i * 5).ToList());
        //        var rndMutateGenes = new ReadOnlyCollection<double>(Generators.Doubles(gridSize * gridSize * (orgsPerNiche + migrantsPerNiche) * replicantsPerOrg * paddedVectorLength, seed + 3 + i * 5).ToList());
        //        var rndMutateRate = new ReadOnlyCollection<Tuple<double, double>>
        //            (
        //                 Generators.Doubles(gridSize * gridSize * (orgsPerNiche + migrantsPerNiche) * replicantsPerOrg, seed + 3 + i * 5)
        //                    .JoinWith(
        //                     Generators.PowDist(gridSize * gridSize * (orgsPerNiche + migrantsPerNiche) * replicantsPerOrg,
        //                                        seed + 3 + i * 5, maxMutationRate, power)
        //                              ).ToList()

        //                );

        //        var hupdator = new HabitatUpdater
        //            (
        //                migrator: HabitatMigrator.Migrate,
        //                habitatBuilder: DblVectSim.MakeHabitat,
        //                nicheUpdater: new DblVectNicheUpdater(targetVector2)
        //            );

        //        var hab2 = hupdator.Update(habitat, rndMigrate, rndMigrateSelect, rndMutateGenes, rndMutateRate);
        //        habitat = (IGridHabitat)hab2;
        //    }

        //    stopwatch.Stop();
        //    var count = stopwatch.ElapsedMilliseconds;
        //    System.Diagnostics.Debug.WriteLine("\n Time:{0}\n", count);
        //}



    }
}
