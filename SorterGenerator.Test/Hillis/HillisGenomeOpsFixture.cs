//using System;
//using System.Collections.Generic;
//using System.Linq;
//using MathUtils.Rand;
//using MathUtils.Repo;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using SorterGenomes.Common;
//using SorterGenomes.Hillis.SorterSwitch;
//using SortingNetworkLib.Sortable;
//using SortingNetworkLib.SorterSwitch;
//using SortingNetworkLib.SorterSwitch;
//using SortingNetworkLib.SwitchKeysRepo;

//namespace SorterGenomes.Test.Hillis
//{
//    [TestClass]
//    public class HillisGenomeOpsFixture
//    {
//        [TestMethod]
//        public void TestMutateSortableGenome()
//        {
//            const int keyCount = 16;
//            const int seedOrig = 557;
//            const int seedMutant = 5578;
//            const int genesPerChromosome = 6;
//            const int chromosomePairCount = 4;

//            var rootGenome = HillisGenomeOps.MakeRandomGenome(chromosomePairCount, genesPerChromosome, keyCount,
//                                                              seedOrig);

//            var stopwatch = new System.Diagnostics.Stopwatch();
//            stopwatch.Start();

//            var difftotal = new List<int>();
//            for (var i = 0; i < 10000; i++)
//            {
//                var mutateFlags = new OrderedRepo<bool>(Generators.RandomBits(2 * genesPerChromosome * chromosomePairCount, seedMutant + i, 0.25));

//                var newSwitchKeysRepo = new SwitchKeysRepoRandom(
//                    keyCount: keyCount,
//                    switchKeys: SorterSwitchDefs.AllSwitchKeys(keyCount),
//                    seed: seedMutant + 1,
//                    size: 2 * genesPerChromosome * chromosomePairCount);

//                var res = rootGenome.MutateZip(newSwitchKeysRepo, mutateFlags);
//                var gd = new GenomeDiff<HillisChromosomePair, HillisChromosome, SorterGene>(rootGenome, res);
//                difftotal.Add(gd.GeneDiffs.Count());
//            }

//            stopwatch.Stop();
//            var et = stopwatch.ElapsedMilliseconds;
//        }

//        [TestMethod]
//        public void TestMutateHillisChromosomePair()
//        {
//            const int keyCount = 16;
//            const int seedOrig = 557;
//            const int seedMutant = 5578;
//            const int genesPerChromosome = 2500;

//            var stopwatch = new System.Diagnostics.Stopwatch();
//                        stopwatch.Start();
//            for (int i = 0; i < 50; i++)
//            {
//                var cp = HillisGenomeOps.MakeHillisChromosomePair(
//                    genesPerChromosome: genesPerChromosome,
//                    switchKeysOrderedRepo: new SwitchKeysRepoRandom(
//                        keyCount: keyCount,
//                        switchKeys: SorterSwitchDefs.AllSwitchKeys(keyCount),
//                        seed: seedOrig,
//                        size: 2*genesPerChromosome),
//                    pairIndex: 0);

//                var newSwitchKeysRepo = new SwitchKeysRepoRandom(
//                    keyCount: keyCount,
//                    switchKeys: SorterSwitchDefs.AllSwitchKeys(keyCount),
//                    seed: seedMutant,
//                    size: 2*genesPerChromosome);

//                var mutateFlags = new OrderedRepo<bool>(Generators.RandomBits(2*genesPerChromosome, seedMutant, 0.25));

//                var result = cp.MutateZip<HillisChromosomePair,HillisChromosome,SorterGene>(newSwitchKeysRepo, mutateFlags);

//                var cd = new ChromosomeDiff<HillisChromosomePair, HillisChromosome, SorterGene>(cp, result);

//                var diffs = cd.GeneDiffs.ToList();
//            }
//            stopwatch.Stop();
//            var et = stopwatch.ElapsedMilliseconds;
//            //43021 @50, 2500 =>362
//            //4733 @500, 250 =>376
//            //976 @5000, 25
//        }

//        [TestMethod]
//        public void TestRandomRegularSingleThreaded()
//        {
//            var stopwatch = new System.Diagnostics.Stopwatch();
//            var results = new List<ISorter<SortableBitArray>>();
//            stopwatch.Start();
//            for (int i = 0; i < 50000; i++)
//            {
//                var sorter = HillisGenomeOps.MakeRandomGenome(5, 6, 16, 337 + i);
//                results.Add(sorter.MakeSorterOfSortableBitArray(Guid.NewGuid(), SorterNotificationType.Always));
//            }

//            stopwatch.Stop();
//            var et = stopwatch.ElapsedMilliseconds;
//            //9051ms
//        }

//        [TestMethod]
//        public void TestRandomRegularMultiThreaded()
//        {
//            var stopwatch = new System.Diagnostics.Stopwatch();
//            stopwatch.Start();
//            IEnumerable<ISorter<SortableBitArray>> results = from item in (new int[50000]).AsParallel()
//                                                             select MakeASorter(item);

//            var lst = results.ToList();
//            //Parallel.For(0, 50000, MakeASorter);
//            stopwatch.Stop();
//            var et = stopwatch.ElapsedMilliseconds;
//            //2584ms
//        }

//        static ISorter<SortableBitArray> MakeASorter(int i)
//        {
//            var sorter = HillisGenomeOps.MakeRandomGenome(5, 6, 16, 337 + i);
//            return sorter.MakeSorterOfSortableBitArray(Guid.NewGuid(), SorterNotificationType.Always);
//        }
//    }
//}
