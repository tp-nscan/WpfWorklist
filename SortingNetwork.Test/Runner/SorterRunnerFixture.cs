using System.Collections.Generic;
using System.Linq;
using MathUtils.Rand;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SortingNetwork.KeyPair;
using SortingNetwork.Runner;
using SortingNetwork.Sorters;
using SortingNetwork.Switchables;

namespace SortingNetwork.Test.Runner
{
    [TestClass]
    public class SorterRunnerFixture
    {
        [TestMethod]
        public void TestSwitchableBitArray()
        {

        }

        [TestMethod]
        public void TestSwitchableShort()
        {
            const int cSorterCount = 100;
            int cChampCount = 20;
            const int cSwitchableChampCount = 500;
            const int cSwitchsPerSorter = 800;
            const int cKeyCount = 16;
            const int cSeed = 154;
            const int cNumGenerations = 3;
            const double cMutationRate = 0.02;

            var sorterRunners = new List<SorterTester>();

            var sw = new System.Diagnostics.Stopwatch();
            sw.Start();

            var parentSorters = KeySet.Instance.AllPairsForKeyCount(cKeyCount).RandomDraw
                (
                    Randy.Fast(cSeed).ToInt(), cSwitchsPerSorter*cChampCount
                )
                    .ToKeyPairRepo()
                    .ToSorters(cSwitchsPerSorter, cChampCount).ToList();


            //var switchableMonitors =
            //    SwitchableShort.MakeRandoms(Randy.Fast(cSeed - 1).ToBool(0.5), cSwitchableChampCount)
            //    .Select(T=>T.ToSwitchableMonitor(Guid.NewGuid()))
            //    .ToList();

            var switchables =
                new List<ISwitchable>(SwitchableShort.MakeRandoms(Randy.Fast(cSeed - 1).ToBool(0.5), cSwitchableChampCount)
                                        .ToList());

            for (var i = 0; i < cNumGenerations; i++)
            {
                var populationGenerator = new SorterPoolRunner
                    (
                        randomGenerator: Randy.Fast(cSeed + i),
                        switchMutationRate: cMutationRate,
                        sorterPopulationSize: cSorterCount,
                        switchablePopulationSize: cSwitchsPerSorter,
                        parentSorters: parentSorters,
                        parentSwitchables: switchables
                    );

                sorterRunners.Clear();
                var testRuns = from testFig in populationGenerator.TestInfo.AsParallel()
                               select new SorterTester(testFig.Item1, testFig.Item2);

                sorterRunners.AddRange(testRuns.ToList());
                var bestRunners = sorterRunners.OrderBy(T => T.Score).Take(cChampCount).ToList();
                parentSorters = bestRunners
                                    .Select(T=>T.SorterMonitor.Switches.ToSorter(T.SorterMonitor.Guid)).ToList();

                switchables = populationGenerator.SwitchableMonitors
                                                 .Take(populationGenerator.SwitchableParentPopulationSize)
                                                 .Select(T=>T.Switchable)
                                                 .ToList();

                //parentSwitchables = SwitchableBitArray.MakeRandoms(16, Randy.Fast(cSeed + i).ToBool(0.5), cSwitchableCount).ToRepo();

                //System.Diagnostics.Debug.WriteLine
                // (
                //     "{0}\t{1}",
                //     i,
                //     sorterRunners.OrderBy(T => T.Score)
                //           .Aggregate("", (current, t) => current + "\t" + t.Score.ToString("0.00"))
                // );

                System.Diagnostics.Debug.WriteLine
                 (
                     "{0}\t{1}\t{2}\t{3}\t{4}",
                     i,
                     bestRunners.First().HashCode,
                     bestRunners.First().SorterMonitor.SwitchesUsed,
                     bestRunners.First().SuccessfulSorts,
                     sorterRunners.OrderBy(T => T.Score)
                             .Aggregate("", (current, t) => current + "\t" + t.Score.ToString("0.00"))

                    // sorterRunners.First().SorterMonitor.LongSwitchReport()
                 );

            }


            sw.Stop();
            System.Diagnostics.Debug.WriteLine(sw.ElapsedMilliseconds);

            //var switchesUsed = runners.Select(T => T.SorterMonitor.Switches.Count(s => s.UseCount > 0)).ToList();
            //var finalRes = runners.Select(T => T.SorterFinalResults.Count(r => r.FinalResult.IsSorted));

            //var rtj = RepoToJson<SwitchableShort, SwitchableShortToJson>.ToJsonAdapter(parentSwitchables,
            //                                                                          SwitchableShortToJson.ToSorters);
            ////System.Diagnostics.Debug.WriteLine(JsonConvert.SerializeObject(SorterToJson.ToJsonAdapter(sorterRepo[0]), Formatting.Indented));
            //System.Diagnostics.Debug.WriteLine(JsonConvert.SerializeObject(rtj, Formatting.Indented));
        }

        [TestMethod]
        public void TestMethod11()
        {
            var sw = new System.Diagnostics.Stopwatch();
            sw.Start();


            sw.Stop();
            System.Diagnostics.Debug.WriteLine(sw.ElapsedMilliseconds);
        }
    }
}
