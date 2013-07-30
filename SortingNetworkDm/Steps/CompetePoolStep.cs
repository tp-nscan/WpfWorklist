using System;
using System.Collections.Generic;
using System.Linq;
using DynamicModel.Common;
using DynamicModel.Model;
using MathUtils.Collections;
using MathUtils.Rand;
using SortNetwork.Results;
using SortNetwork.Sorters;
using SortNetwork.Switchables;
using SortingNetworkDm.Entities;

namespace SortingNetworkDm.Steps
{
    public interface ICompetePoolStep : IStep
    {
        ISwitchablePoolEntity InputSwitchablePoolEntity { get; }
        ISwitchablePoolEntity OutputSwitchablePoolEntity { get; }
        ISorterPoolEntity InputSorterPoolEntity { get; }
        ISorterResultPoolEntity OutputSorterResultPoolEntity { get; }
        int GenerationCount { get; }
        int KeyCount { get; }
        double MutationRate { get; }
        int SeedIn { get; }
        int SorterPoolSize { get; }
        int SorterChampCount { get; }
        int SwitchablePoolSize { get; }
        int SwitchableChampCount { get; }
        int SeedOut { get; }
    }

    public static class CompetePoolStep
    {
        public const string TypeName = "CompetePoolStep";

        public static ICompetePoolStep Load
        (
            Guid guid,
            string name,
            string description,
            int index,
            ISorterPoolEntity inputSorterPoolEntity,
            ISwitchablePoolEntity inputSwitchablePoolEntity,
            ISorterResultPoolEntity outputSorterResultPoolEntity,
            ISwitchablePoolEntity outputSwitchablePoolEntity,
            int seedIn,
            int generationCount,
            int sorterPoolSize,
            int sorterChampCount,
            int switchablePoolSize,
            int switchableChampCount,
            double mutationRate
        )
        {
            return new CompetePoolImpl
                (
                    guid: guid,
                    name: name,
                    description: description,
                    index: index,
                    inputSorterPoolEntity: inputSorterPoolEntity,
                    inputSwitchablePoolEntity: inputSwitchablePoolEntity,
                    outputSorterResultPoolEntity: outputSorterResultPoolEntity,
                    outputSwitchablePoolEntity: outputSwitchablePoolEntity,
                    seedIn: seedIn,
                    generationCount: generationCount,
                    sorterPoolSize: sorterPoolSize,
                    sorterChampCount: sorterChampCount,
                    switchablePoolSize: switchablePoolSize,
                    switchableChampCount: switchableChampCount,
                    mutationRate: mutationRate
                );
        }

        public static ICompetePoolStep Create
        (
            Guid guid,
            string name,
            string description,
            int index,
            ISorterPoolEntity inputSorterPoolEntity,
            ISwitchablePoolEntity inputSwitchablePoolEntity,
            int seedIn,
            int generationCount,
            int sorterPoolSize,
            int sorterChampCount,
            int switchablePoolSize,
            int switchableChampCount,
            double mutationRate
        )
        {
            return new CompetePoolImpl
                (
                    guid: guid,
                    name: name,
                    description: description,
                    index: index,
                    inputSorterPoolEntity: inputSorterPoolEntity,
                    inputSwitchablePoolEntity: inputSwitchablePoolEntity,
                    outputSorterResultPoolEntity: null,
                    outputSwitchablePoolEntity: null,
                    seedIn: seedIn,
                    generationCount: generationCount,
                    sorterPoolSize: sorterPoolSize,
                    sorterChampCount: sorterChampCount,
                    switchablePoolSize: switchablePoolSize,
                    switchableChampCount: switchableChampCount,
                    mutationRate: mutationRate
                );
        }

        public static ICompetePoolStep CreateAndRun
        (
            Guid guid,
            string name,
            string description,
            int index,
            ISorterPoolEntity inputSorterPoolEntity,
            ISwitchablePoolEntity inputSwitchablePoolEntity,
            ISorterPoolEntity outputSorterPoolEntity,
            ISwitchablePoolEntity outputSwitchablePoolEntity,
            int seedIn,
            int generationCount,
            int sorterPoolSize,
            int sorterChampCount,
            int switchablePoolSize,
            int switchableChampCount,
            double mutationRate
        )
        {
            var competePool =  Create
                (
                    guid: guid,
                    name: name,
                    description: description,
                    index: index,
                    inputSorterPoolEntity: inputSorterPoolEntity,
                    inputSwitchablePoolEntity: inputSwitchablePoolEntity,
                    seedIn: seedIn,
                    generationCount: generationCount,
                    sorterPoolSize: sorterPoolSize,
                    sorterChampCount: sorterChampCount,
                    switchablePoolSize: switchablePoolSize,
                    switchableChampCount: switchableChampCount,
                    mutationRate: mutationRate
                );

            competePool.Execute(runAgent: RunAgent.MakeTest());

            return competePool;
        }
    }

    class CompetePoolImpl : StepImpl, ICompetePoolStep
    {
        public CompetePoolImpl
            (
                Guid guid,
                string name,
                string description,
                int index,
                ISorterPoolEntity inputSorterPoolEntity,
                ISwitchablePoolEntity inputSwitchablePoolEntity,
                ISorterResultPoolEntity outputSorterResultPoolEntity,
                ISwitchablePoolEntity outputSwitchablePoolEntity,
                int seedIn,
                int generationCount,
                int sorterPoolSize,
                int sorterChampCount,
                int switchablePoolSize,
                int switchableChampCount,
                double mutationRate
            )
            : base
                (
                    guid : guid,
                    index: index,
                    inputEntities: new IEntity[] {inputSorterPoolEntity, inputSwitchablePoolEntity},
                    outputEntities: new IEntity[] { outputSorterResultPoolEntity, outputSwitchablePoolEntity }
                                                        .Where(T=>T != null)
                )
        {
            _name = name;
            _description = description;
            _seedIn = seedIn;
            _keyCount = inputSorterPoolEntity.SorterRepo.KeyCount;
            _seedIn = seedIn;
            _generationCount = generationCount;
            _sorterPoolSize = sorterPoolSize;
            _sorterChampCount = sorterChampCount;
            _switchablePoolSize = switchablePoolSize;
            _switchableChampCount = switchableChampCount;
            _mutationRate = mutationRate;
        }

        private string _description;
        public override string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        private string _name;
        public override string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public override void Execute(IRunAgent runAgent)
        {
            var seedOut = SeedIn;

            var currentSorterChamps = InputSorterPoolEntity.SorterRepo.Take(SorterChampCount)
                                            .Select(T=>SorterResult.Make(T, Enumerable.Empty<ISwitchResult>(), 0, 0, 0));
            var currentSwitchableChamps = InputSwitchablePoolEntity.SwitchableRepo.Take(SwitchableChampCount).ToSwitchableRepo();

            for (var curGeneration = 0; curGeneration < GenerationCount; curGeneration++)
            {
                if (!runAgent.Continue)
                {
                    _generationCount = curGeneration;
                    break;
                }

                var randomGenerator = Randy.Fast(seedOut);

                ISorterResultRepo newSorterChamps;
                ISwitchableRepo newSwitchableChamps;

                ProcAGeneration
                    (
                        randomGenerator: randomGenerator,
                        switchMutationRate: MutationRate,
                        sorterPopSize: SorterPoolSize,
                        switchablePopSize: SwitchablePoolSize,
                        currentSorterChamps: currentSorterChamps.Select(T=>T.Sorter),
                        currentSwitchableChamps: currentSwitchableChamps,
                        newSorterChamps: out newSorterChamps,
                        newSwitchableChamps: out newSwitchableChamps
                    );

                currentSorterChamps = newSorterChamps;
                currentSwitchableChamps = newSwitchableChamps;
                seedOut = randomGenerator.ToInt().Next();

                runAgent.Update
                    (
                        CompetePoolRunMessage.Make
                        (
                            generation: curGeneration,
                            seedOut: seedOut,
                            sorterResultRepo: newSorterChamps,
                            switchableRepo: currentSwitchableChamps
                        )
                    );
            }

            AddOutputEnity
                (
                    SorterResultPoolEntity.Make
                    (
                        guid: Guid.NewGuid(), 
                        name: Name + "_" + InputSorterPoolEntity.Name,
                        description: Description,
                        sorterResultRepo: currentSorterChamps.ToSorterResultRepo()
                    )
                );

            AddOutputEnity
                (
                    SwitchablePoolEntity.Make
                        (
                            guid: Guid.NewGuid(), 
                            name: Name + "_" + InputSwitchablePoolEntity.Name, 
                            description: Description,
                            switchableRepo: currentSwitchableChamps
                        )
                );

            SeedOut = seedOut;
        }

        public override string TypeName
        {
            get { return CompetePoolStep.TypeName; }
        }

        public override bool WasExecuted
        {
            get
            {
                return OutputSorterResultPoolEntity != null &&
                       OutputSwitchablePoolEntity != null;
            }
        }

        static void ProcAGeneration
        (
            IRando randomGenerator,
            double switchMutationRate,
            int sorterPopSize,
            int switchablePopSize,
            IEnumerable<ISorter> currentSorterChamps,
            IEnumerable<ISwitchable> currentSwitchableChamps,
            out ISorterResultRepo newSorterChamps,
            out ISwitchableRepo newSwitchableChamps
        )
        {
            var competePoolGen = new CompetePoolGen
            (
                randomGenerator: randomGenerator,
                switchMutationRate: switchMutationRate,
                sorterPopulationSize: sorterPopSize,
                switchablePopulationSize: switchablePopSize,
                parentSorters: currentSorterChamps,
                parentSwitchables: currentSwitchableChamps
            );

            var sorterTesters = (
                    from testFig in competePoolGen.TestInfo.AsParallel()
                    select SorterTester.Make(testFig.Item1, testFig.Item2)
                                ).ToList();

            newSorterChamps =
                    sorterTesters.OrderBy(T => T.Score)
                                 .Take(competePoolGen.SorterParentPopulationSize)
                                 .Select(T => T.SorterResult)
                                 .ToSorterResultRepo();


            newSwitchableChamps =
                competePoolGen.SwitchablePop
                                   .RandomDrawWithoutReplacement(randomGenerator.ToInt())
                                   .Take(competePoolGen.SwitchableParentPopulationSize)
                                   .ToSwitchableRepo();
        }

        public ISwitchablePoolEntity InputSwitchablePoolEntity
        {
            get { return (ISwitchablePoolEntity)InputEntities.SingleOrDefault(T => T.TypeName == SwitchablePoolEntity.TypeName); }
        }

        public ISorterPoolEntity InputSorterPoolEntity
        {
            get { return (ISorterPoolEntity)InputEntities.SingleOrDefault(T => T.TypeName == SorterPoolEntity.TypeName); }
        }

        public ISwitchablePoolEntity OutputSwitchablePoolEntity
        {
            get { return (ISwitchablePoolEntity)OutputEntities.SingleOrDefault(T => T.TypeName == SwitchablePoolEntity.TypeName); }
        }

        public ISorterResultPoolEntity OutputSorterResultPoolEntity
        {
            get { return (ISorterResultPoolEntity)OutputEntities.SingleOrDefault(T => T.TypeName == SorterResultPoolEntity.TypeName); }
        }

        private readonly int _keyCount;
        public int KeyCount
        {
            get { return _keyCount; }
        }

        private readonly double _mutationRate;
        public double MutationRate
        {
            get { return _mutationRate; }
        }

        private readonly int _seedIn;
        public int SeedIn
        {
            get { return _seedIn; }
        }

        public int SeedOut { get; private set; }

        private int _generationCount;
        public int GenerationCount
        {
            get { return _generationCount; }
        }

        private readonly int _switchableChampCount;
        public int SwitchableChampCount
        {
            get { return _switchableChampCount; }
        }

        private readonly int _switchablePoolSize;
        public int SwitchablePoolSize
        {
            get { return _switchablePoolSize; }
        }

        private readonly int _sorterChampCount;
        public int SorterChampCount
        {
            get { return _sorterChampCount; }
        }

        private readonly int _sorterPoolSize;
        public int SorterPoolSize
        {
            get { return _sorterPoolSize; }
        }
    }
}
