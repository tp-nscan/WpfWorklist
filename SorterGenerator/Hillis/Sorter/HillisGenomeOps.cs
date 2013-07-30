//using System;
//using System.Collections.Generic;
//using System.Linq;
//using GaLib.Common;
//using MathUtils.Collections;
//using MathUtils.Rand;
//using MathUtils.Repo;
//using SorterGenomes.Common;
//using SortingNetworkLib.Sortable;
//using SortingNetworkLib.SorterSwitch;
//using SortingNetworkLib.SorterSwitch;
//using SortingNetworkLib.SwitchKeysRepo;

//namespace SorterGenomes.Hillis.SorterSwitch
//{
//    public static class HillisGenomeOps
//    {
//        public static HillisGenome MakeRandomGenome(int chromosomePairCount, int genesPerChromosome, int keyCount, int seed)
//        {
//            var chromosomePairs = new List<HillisChromosomePair>();
//            var switchKeysRepoRandom = new SwitchKeysRepoRandom(keyCount, SorterSwitchDefs.AllSwitchKeys(keyCount), seed, chromosomePairCount * 2 * genesPerChromosome)
//                                            .Split(chromosomePairCount).ToList();
//            for (var pairIndex = 0; pairIndex < chromosomePairCount; pairIndex++)
//            {
//                chromosomePairs.Add(MakeHillisChromosomePair(genesPerChromosome, switchKeysRepoRandom[pairIndex], pairIndex));
//            }

//            return new HillisGenome(SorterResultGuid.NewGuid(), chromosomePairs);
//        } 

//        public static void MateHillisGenomes(HillisGenome parentA, HillisGenome parentB, out HillisGenome siblingA,
//                                             out HillisGenome siblingB)
//        {
//            if (parentA.KeyCount != parentB.KeyCount)
//            {
//                throw new Exception("KeyCounts do not match");
//            }
//            if (parentA.ChromosomePairs.Count() != parentB.ChromosomePairs.Count())
//            {
//                throw new Exception("ChromosomePair counts do not match");
//            }
//            siblingA = new HillisGenome(SorterResultGuid.NewGuid(), null);
//            siblingB = new HillisGenome(SorterResultGuid.NewGuid(), null);
//        }

//        public static IGenome<TP, TC, TG> MutateZip<TP, TC, TG>(this ISorterGenome<TP, TC, TG> sorterGenome, ISwitchKeysOrderedRepo newSwitchKeysOrderedRepo, IOrderedRepo<bool> mutateFlags)
//            where TP : ISorterChromosomePair<TC, TG>
//            where TC : ISorterChromosome<TG>
//            where TG : ISorterGene
//        {
//            var newSwitchKeysList = newSwitchKeysOrderedRepo.Split(sorterGenome.ChromosomePairs.Count()).ToList();
//            var mutateFlagsList = mutateFlags.Split(sorterGenome.ChromosomePairs.Count()).ToList();
//            var mutantChromosomePairs = new List<TP>();

//            for (var i = 0; i < sorterGenome.ChromosomePairs.Count(); i++)
//            {
//                mutantChromosomePairs.Add(sorterGenome.ChromosomePairAtIndex(i).MutateZip<TP,TC,TG>(newSwitchKeysList[i], mutateFlagsList[i]));
//            }

//            return sorterGenome.ReplaceChromosomePairs(SorterResultGuid.NewGuid(), cp => mutantChromosomePairs.Single(T => T.SorterSwitchDef == cp.SorterSwitchDef));
//        }

//        public static TP MutateZip<TP, TC, TG>(this ISorterChromosomePair<TC, TG> sorterChromosomePair,
//                    ISwitchKeysOrderedRepo newSwitchKeysOrderedRepo, IOrderedRepo<bool> mutateFlags)
//            where TP : ISorterChromosomePair<TC, TG>
//            where TC : ISorterChromosome<TG>
//            where TG : ISorterGene
//        {
//            if (sorterChromosomePair.GenePairCount * 2 != newSwitchKeysOrderedRepo.KeyCount)
//            {
//                throw new ArgumentException("Incorrect number of new switchlines");
//            }
//            if (sorterChromosomePair.GenePairCount * 2 != mutateFlags.KeyCount)
//            {
//                throw new ArgumentException("Incorrect number of new mutateFlags");
//            }
//            if (sorterChromosomePair.KeyCount != newSwitchKeysOrderedRepo.KeyCount)
//            {
//                throw new ArgumentException("Incorrect number of new mutateFlags");
//            }

//            var newSwitchKeysList = newSwitchKeysOrderedRepo.Split(2).ToList();
//            var mutateFlagsList = mutateFlags.Split(2).ToList();
//            var newFirst = sorterChromosomePair.First.MutateZip<TC, TG>(newSwitchKeysList[0], mutateFlagsList[0]);
//            var newSecond = sorterChromosomePair.Second.MutateZip<TC, TG>(newSwitchKeysList[1], mutateFlagsList[1]);

//            return (TP) sorterChromosomePair.ReplaceChromosomes(newFirst, newSecond);
//        }

//        public static TC MutateZip<TC, TG>(this TC sorterChromosome, ISwitchKeysOrderedRepo newSwitchKeysOrderedRepo,
//                IOrderedRepo<bool> mutateFlags)
//            where TC : ISorterChromosome<TG>
//            where TG : ISorterGene
//        {
//            if (sorterChromosome.GeneCount != newSwitchKeysOrderedRepo.KeyCount)
//            {
//                throw new ArgumentException("Incoorect number of new switchlines");
//            }
//            if (sorterChromosome.GeneCount != mutateFlags.KeyCount)
//            {
//                throw new ArgumentException("Incoorect number of new mutateFlags");
//            }
//            if (sorterChromosome.KeyCount != newSwitchKeysOrderedRepo.KeyCount)
//            {
//                throw new ArgumentException("Incoorect number of new mutateFlags");
//            }

//            var slr = new SwitchKeysRepo(sorterChromosome.KeyCount,
//                                              sorterChromosome.Genes.Select(T => T.SorterSwitchDefs)
//                                                              .MutateZip(newSwitchKeysOrderedRepo.Items, mutateFlags.Items));
//            return (TC) sorterChromosome.ReplaceSwitches(slr);
//        }

//        public static HillisChromosomePair MakeHillisChromosomePair(int genesPerChromosome, ISwitchKeysOrderedRepo switchKeysOrderedRepo, int pairIndex)
//        {
//            var repoPair = switchKeysOrderedRepo.Split(2).ToList();
//            var hc1 = new HillisChromosome(
//                    pairIndex, 
//                    DiploidMember.First, 
//                    switchKeysOrderedRepo.KeyCount,
//                    repoPair[0].Items
//                        .Select((T, i) => new SorterGene(T, i)) );

//            var hc2 = new HillisChromosome(
//                    pairIndex,
//                    DiploidMember.Second,
//                    switchKeysOrderedRepo.KeyCount,
//                    repoPair[1].Items
//                        .Select((T, i) => new SorterGene(T, i)));


//            return new HillisChromosomePair(hc1, hc2);
//        }

//        public static HillisChromosomePair Recombine(HillisChromosomePair chromosomePair, List<bool> recombFlags)
//        {
//            if (recombFlags.Count != chromosomePair.GenePairCount)
//            {
//                throw new Exception(String.Format("Chromosome length:{0} does not match recombFlags lenght:{1}", chromosomePair.GenePairCount, recombFlags.Count));
//            }

//            List<SorterGene> recA;
//            List<SorterGene> recB;
//            List.Recombine(
//                chromosomePair.First.Genes,
//                chromosomePair.Second.Genes,
//                recombFlags,
//                out recA,
//                out recB);

//            return new HillisChromosomePair
//                (
//                    new HillisChromosome(chromosomePair.SorterSwitchDef, DiploidMember.First, chromosomePair.KeyCount, recA),
//                    new HillisChromosome(chromosomePair.SorterSwitchDef, DiploidMember.Second, chromosomePair.KeyCount, recB)
//                );
//        }

//        public static IEnumerable<SorterSwitchDefs> ReducedSwitchKeys<TP, TC, TG>(this ISorterGenome<TP, TC, TG> sorterGenome)
//                where TP : ISorterChromosomePair<TC, TG>
//                where TC : ISorterChromosome<TG>
//                where TG : ISorterGene
//        {
//            foreach (var chromosomePair in sorterGenome.ChromosomePairs.OrderBy(T => T.SorterSwitchDef))
//            {
//                for (var geneIndex = 0; geneIndex < chromosomePair.GenePairCount; geneIndex++)
//                {
//                    var firstSwitchKeys = chromosomePair.First[geneIndex].SorterSwitchDefs;
//                    yield return firstSwitchKeys;

//                    var secondSwitchKeys = chromosomePair.Second[geneIndex].SorterSwitchDefs;
//                    if (secondSwitchKeys.Key != firstSwitchKeys.Key)
//                    {
//                        yield return secondSwitchKeys;
//                    }
//                }
//            }
//        }

//        public static ISorter<SortableBitArray> MakeSorterOfSortableBitArray<TP, TC, TG>(
//            this ISorterGenome<TP, TC, TG> sorterGenome, SorterResultGuid guid, SorterNotificationType sorterNotificationType)
//            where TP : ISorterChromosomePair<TC, TG>
//            where TC : ISorterChromosome<TG>
//            where TG : ISorterGene
//        {
//            return new SorterBase<SortableBitArray>(
//                guid, 
//                sorterGenome.KeyCount, 
//                sorterGenome.ReducedSwitchKeys().Select(T=>new BitArraySwitch(T.LowKey, T.HiKey, sorterGenome.KeyCount)),
//                sorterNotificationType);
//        }
//    }
//}
