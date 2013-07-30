using System;
using System.Linq;
using DynamicModel.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SortNetwork.Diff;
using SortNetwork.Sorters;
using SortNetwork.Switchables;
using SortNetwork.TestData;
using SortingNetworkDm.Entities;
using SortingNetworkDm.Steps;

namespace SortingNetworkDm.Test.Steps
{
    [TestClass]
    public class CompetePoolStepFixture
    {
        const int CSorterChampCount = 2;
        const int CSwitchableChampCount = 20;
        const int CSorterPoolSize = 2;
        const int CSwitchablePoolSize = 20;
        const int CStepZeroSeed = 121;
        const int CKeyCount = 16;
        private const int CSwitchesPerSorter = 80;
        const double CMutationRate = 0.02;
        private const int CNumGenerations = 1;

        [TestMethod]
        public void TestRandGenStep()
        {
            var compPoolStep1 = SorterPoolStep.CreateAndRun(
                    guid: Guid.NewGuid(),
                    index: 1,
                    description: "sorterPoolGenStep1",
                    name: "name",
                    seedIn: CStepZeroSeed,
                    keyCount: CKeyCount,
                    sorterCount: CSorterChampCount,
                    switchesPerSorter: CSwitchesPerSorter
                );

            var compPoolStep2 = SwitchablePoolStep.CreateAndRun
                (
                    guid: Guid.NewGuid(),
                    index: 2,
                    description: "switchablePoolGenStep2",
                    name: "name",
                    switchableType: TestConstants.SwitchableType,
                    keyCount: CKeyCount,
                    seedIn: compPoolStep1.SeedOut,
                    switchableCount: CSwitchableChampCount
                );

            var compPoolStep3 = CompetePoolStep.Create
                (
                    guid: Guid.NewGuid(),
                    name: "name of compPoolStep3",
                    description: "comement for compPoolStep3",
                    index: 3,
                    inputSorterPoolEntity: compPoolStep1.OutputSorterPoolEntity,
                    inputSwitchablePoolEntity: compPoolStep2.OutputSwitchablePoolEntity,
                    seedIn: CStepZeroSeed,
                    generationCount: CNumGenerations,
                    sorterPoolSize: CSorterPoolSize,
                    sorterChampCount: CSorterChampCount,
                    switchablePoolSize: CSwitchablePoolSize,
                    switchableChampCount: CSwitchableChampCount,
                    mutationRate: CMutationRate
                );

             var runAgent = RunAgent.MakeTest();

            compPoolStep3.Execute(runAgent);
            Assert.IsNotNull(compPoolStep3.OutputSwitchablePoolEntity.SwitchableRepo);
            Assert.IsNotNull(compPoolStep3.OutputSorterResultPoolEntity.SorterResultRepo);
        }

        [TestMethod]
        public void ZeroGenChangesNothing()
        {
            ISwitchablePoolEntity switchablePoolStep0;
            ISorterPoolEntity sorterPoolStep0;
            InitPools(out switchablePoolStep0, out sorterPoolStep0);

            int stepOneSeed;
            ISwitchablePoolEntity switchablePoolStep1;
            ISorterResultPoolEntity sorterPoolStep1;
            EvoForSteps
                (
                    generationCount: 0,
                    seedIn: CStepZeroSeed,
                    inSwitchables: switchablePoolStep0,
                    inSorters: sorterPoolStep0,
                    outputSwitchables: out switchablePoolStep1,
                    outputSorters: out sorterPoolStep1,
                    runAgent: RunAgent.MakeTest(),
                    seedOut: out stepOneSeed
                );

            Assert.AreEqual(CStepZeroSeed, stepOneSeed);
            var sorterPoolDiff = SorterPoolDiff.Make
                (   
                    sorterPoolStep0.SorterRepo, 
                    sorterPoolStep1.SorterResultRepo.Select(T=>T.Sorter).ToSorterRepo()
                );

            Assert.IsFalse(sorterPoolDiff.AnySwitchLevelDiffs);
        }

        [TestMethod]
        public void TestSeedOutAndIn()
        {
            ISwitchablePoolEntity initialSwitchables;
            ISorterPoolEntity initialSorters;
            InitPools(out initialSwitchables, out initialSorters);

            int seedOutStep1;
            ISwitchablePoolEntity step1Switchables;
            ISorterResultPoolEntity step1Sorters;
            EvoForSteps
                (
                    generationCount: 0,
                    seedIn: CStepZeroSeed,
                    inSwitchables: initialSwitchables,
                    inSorters: initialSorters,
                    outputSwitchables: out step1Switchables,
                    outputSorters: out step1Sorters,
                    runAgent: RunAgent.MakeTest(),
                    seedOut: out seedOutStep1
                );

            int seedOutStep2;
            ISwitchablePoolEntity step2Switchables;
            ISorterResultPoolEntity step2Sorters;

            var inputSorterPoolEntity = SorterPoolEntity.Make
                (
                    step1Sorters.Guid, 
                    step1Sorters.Name,
                    step1Sorters.Description,
                    step1Sorters.SorterResultRepo.Select(T => T.Sorter).ToSorterRepo()
               );

            EvoForSteps
                (
                    generationCount: 1,
                    seedIn: seedOutStep1,
                    inSwitchables: step1Switchables,
                    inSorters: inputSorterPoolEntity,
                    outputSwitchables: out step2Switchables,
                    outputSorters: out step2Sorters,
                    runAgent: RunAgent.MakeTest(),
                    seedOut: out seedOutStep2
                );


            var runAgent = RunAgent.MakeTest();
            runAgent.OnReport.Subscribe(p=> updateo(p, step1Sorters));


            int seedOutStep1N2;
            ISwitchablePoolEntity step1N2Switchables;
            ISorterResultPoolEntity step1N2Sorters;
            EvoForSteps
                (
                    generationCount: 1,
                    seedIn: CStepZeroSeed,
                    inSwitchables: initialSwitchables,
                    inSorters: initialSorters,
                    outputSwitchables: out step1N2Switchables,
                    outputSorters: out step1N2Sorters,
                    runAgent: runAgent,
                    seedOut: out seedOutStep1N2
                );

            Assert.AreEqual(seedOutStep1N2, seedOutStep2);

            var sorterPoolDiff = SorterResultPoolDiff.Make(step1N2Sorters.SorterResultRepo, step2Sorters.SorterResultRepo);

            Assert.IsFalse(sorterPoolDiff.AnySwitchResultDiffs);

            var switchableRepoDiff = SwitchableRepoDiff.Make
            (
                step1N2Switchables.SwitchableRepo,
                step2Switchables.SwitchableRepo
            );

            Assert.IsFalse(switchableRepoDiff.HasDifferences);
        }

        void updateo(IRunMessage message, ISorterResultPoolEntity entity)
        {
            var obj = message as ICompetePoolRunMessage;
            var sorterPoolDiff = SorterResultPoolDiff.Make(obj.SorterResultRepo, entity.SorterResultRepo);
            var isDiff = sorterPoolDiff.AnySwitchResultDiffs;
        }

        void EvoForSteps
            (
                int generationCount,
                int seedIn,
                ISwitchablePoolEntity inSwitchables,
                ISorterPoolEntity inSorters,
                out ISwitchablePoolEntity  outputSwitchables,
                out ISorterResultPoolEntity outputSorters,
                IRunAgent runAgent,
                out int seedOut
            )
        {
            var compPoolEvoStep = CompetePoolStep.Create
                (
                    guid: Guid.NewGuid(),
                    name: "",
                    description: "",
                    index: 3,
                    inputSorterPoolEntity: inSorters,
                    inputSwitchablePoolEntity: inSwitchables,
                    seedIn: seedIn,
                    generationCount: generationCount,
                    sorterPoolSize: CSorterPoolSize,
                    sorterChampCount: CSorterChampCount,
                    switchablePoolSize: CSwitchablePoolSize,
                    switchableChampCount: CSwitchableChampCount,
                    mutationRate: CMutationRate
                );

            compPoolEvoStep.Execute(runAgent);

            outputSorters = compPoolEvoStep.OutputSorterResultPoolEntity;
            outputSwitchables = compPoolEvoStep.OutputSwitchablePoolEntity;

            seedOut = compPoolEvoStep.SeedOut;
        }

        void InitPools
            (
                out ISwitchablePoolEntity outputSwitchables,
                out ISorterPoolEntity outputSorters
            )
        {
            const SwitchableType cSwitchableType = SwitchableType.BitArray;
            var sortersStep = SorterPoolStep.CreateAndRun
            (
                guid: Guid.NewGuid(),
                index: 1,
                description: "sorterPoolGenStep1",
                name: "name",
                seedIn: CStepZeroSeed,
                keyCount: CKeyCount,
                sorterCount: CSorterChampCount,
                switchesPerSorter: CSwitchesPerSorter
            );

            var switchablesStep = SwitchablePoolStep.CreateAndRun
            (
                guid: Guid.NewGuid(),
                index: 2,
                description: "switchablePoolGenStep2",
                name: "name",
                switchableType: cSwitchableType,
                keyCount: CKeyCount,
                seedIn: sortersStep.SeedOut,
                switchableCount: CSwitchableChampCount
            );

            outputSwitchables = switchablesStep.OutputSwitchablePoolEntity;
            outputSorters = sortersStep.OutputSorterPoolEntity;
        }
    }
}
