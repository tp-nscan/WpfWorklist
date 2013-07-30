using System.Collections.Generic;
using System.Linq;
using MathUtils.Rand;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SortNetwork.Results;
using SortNetwork.Sorters;
using SortNetwork.TestData;

namespace SortNetwork.Test.Results
{
    [TestClass]
    public class CompPoolsFixture
    {
        [TestMethod]
        public void TestCompetePool()
        {
            var sorterRunners = new List<ISorterTester>();

            var sw = new System.Diagnostics.Stopwatch();
            sw.Start();

            var parentSorters = TestSorters.TheSorterRepo.ToList();
            var switchables = TestSwitchable.TheSwitchableRepo.ToList();

            for (var i = 0; i < 3; i++)
            {
                var populationGenerator = new CompetePoolGen
                    (
                        randomGenerator: Randy.Fast(TestConstants.Seed + i),
                        switchMutationRate: 0.05,
                        sorterPopulationSize: TestConstants.SwitchableCount,
                        switchablePopulationSize: TestConstants.SwitchesPerSorter,
                        parentSorters: parentSorters,
                        parentSwitchables: switchables
                    );

                sorterRunners.Clear();
                var testRuns = from testFig in populationGenerator.TestInfo.AsParallel()
                               select SorterTester.Make(testFig.Item1, testFig.Item2);

                sorterRunners.AddRange(testRuns.ToList());
                var bestRunners = sorterRunners.OrderBy(T => T.Score).Take(TestConstants.SorterCount / 5).ToList();
                parentSorters = bestRunners
                                    .Select(T => T.SorterResult.SwitchResults.ToSorter(T.SorterResult.Sorter.Guid)).ToList();

                switchables = populationGenerator.SwitchablePop
                                                 .Take(populationGenerator.SwitchableParentPopulationSize)
                                                 .ToList();

                //parentSwitchables = SwitchableBitArray.MakeRandoms(16, Randy.Fast(TestConstants.Seed + i).ToBool(0.5), TestConstants.SwitchableCount).ToSwitchableRepo();

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
                     bestRunners.First().SorterResult.SwitchesUsed,
                     bestRunners.First().SuccessfulSorts,
                     sorterRunners.OrderBy(T => T.Score)
                             .Aggregate("", (current, t) => current + "\t" + t.Score.ToString("0.00"))

                    // sorterRunners.First().SorterResult.LongSwitchReport()
                 );

            }


            sw.Stop();
            System.Diagnostics.Debug.WriteLine(sw.ElapsedMilliseconds);

            //var switchesUsed = runners.Select(T => T.SorterResult.SwitchResultsToJson.Count(s => s.CountOfTests > 0)).ToList();
            //var finalRes = runners.Select(T => T.SorterFinalResults.Count(r => r.FinalResult.IsSorted));

            //var rtj = RepoToJson<SwitchableShort, SwitchableShortToJson>.ToJson(parentSwitchables,
            //                                                                          SwitchableShortToJson.ToSorters);
            ////System.Diagnostics.Debug.WriteLine(JsonConvert.SerializeObject(SorterToJson.ToJson(sorterRepo[0]), Formatting.Indented));
            //System.Diagnostics.Debug.WriteLine(JsonConvert.SerializeObject(rtj, Formatting.Indented));
        }

    }
}
