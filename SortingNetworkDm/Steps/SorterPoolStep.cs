using System;
using System.Linq;
using DynamicModel.Common;
using DynamicModel.Model;
using MathUtils.Rand;
using SortNetwork.KeySets;
using SortNetwork.Sorters;
using SortingNetworkDm.Entities;

namespace SortingNetworkDm.Steps
{
    public interface ISorterPoolStep : IStep
    {
        int KeyCount { get; }
        ISorterPoolEntity OutputSorterPoolEntity { get; }
        int SeedIn { get; }
        int SeedOut { get; }
        int SorterCount { get; }
        int SwitchesPerSorter { get; }
    }

    public static class SorterPoolStep
    {
        public const string TypeName = "SorterPoolStep";

        public static ISorterPoolStep Load
        (
            Guid guid,
            string name,
            string description,
            int index,
            ISorterPoolEntity outputSorters,
            int keyCount,
            int seedIn,
            int sorterCount,
            int switchesPerSorter
        )
        {
            return new SorterPoolStepImpl
                (
                    guid: guid,
                    name: name,
                    description: description,
                    index: index,
                    outputSorterPoolEntity: outputSorters,
                    keyCount: keyCount,
                    seedIn: seedIn,
                    sorterCount: sorterCount,
                    switchesPerSorter: switchesPerSorter
                );
        }

        public static ISorterPoolStep Create
        (
            Guid guid,
            int index,
            string description,
            string name,
            int seedIn,
            int keyCount,
            int sorterCount,
            int switchesPerSorter
        )
        {
            var sorterPoolGenStep = Load
                (
                    guid: guid,
                    name: name,
                    description: description,
                    index: index,
                    outputSorters: null,
                    keyCount: keyCount,
                    seedIn: seedIn,
                    sorterCount: sorterCount,
                    switchesPerSorter: switchesPerSorter
                );

            return sorterPoolGenStep;
        }

        public static ISorterPoolStep CreateAndRun
            (
                Guid guid,
                int index, 
                string description,
                string name,
                int seedIn,
                int keyCount,
                int sorterCount,
                int switchesPerSorter
            )
        {
            var sorterPoolGenStep = Create
                (
                    guid: guid,
                    name: name,
                    description: description,
                    index: index,
                    keyCount: keyCount,
                    seedIn: seedIn,
                    sorterCount: sorterCount,
                    switchesPerSorter: switchesPerSorter
                );

            sorterPoolGenStep.Execute(runAgent: RunAgent.MakeTest());

            return sorterPoolGenStep;
        }
    }

    class SorterPoolStepImpl : StepImpl, ISorterPoolStep
    {
        public SorterPoolStepImpl
        (
            Guid guid,
            string name,
            string description,
            int index,
            ISorterPoolEntity outputSorterPoolEntity,
            int keyCount,
            int seedIn,
            int sorterCount,
            int switchesPerSorter
        )
            : base
            (
                guid:guid,
                index: index,
                inputEntities: Enumerable.Empty<IEntity>(),
                outputEntities: (outputSorterPoolEntity==null) ? Enumerable.Empty<ISorterPoolEntity>() : new[] { outputSorterPoolEntity }
            )
        {
            // ReSharper disable DoNotCallOverridableMethodsInConstructor
            Name = name;
            Description = description;
            // ReSharper restore DoNotCallOverridableMethodsInConstructor
            _keyCount = keyCount;
            _seedIn = seedIn;
            _sorterCount = sorterCount;
            _switchesPerSorter = switchesPerSorter;
        }

        public override void Execute(IRunAgent runAgent)
        {
            var rando = Randy.Fast(SeedIn);

            SeedOut = rando.ToInt().Next();

            AddOutputEnity
                (
                    SorterPoolEntity.Make
                    (
                        guid: Guid, 
                        name: Name, 
                        description: Description,
                        sorterRepo: KeySet.Instance.AllKeyPairsForKeyCount(KeyCount).RandomDraw
                                    (
                                        rando.ToInt(), SwitchesPerSorter * SorterCount
                                    )
                                    .ToKeyPairRepo()
                                    .ToSorters(SwitchesPerSorter, SorterCount, rando.ToGuid())
                                    .ToSorterRepo()
                    )
                );
        }

        private string _description;
        public override string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                if (OutputSorterPoolEntity != null)
                {
                    OutputSorterPoolEntity.Description = value;
                }
            }
        }

        private string _name;
        public override string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                if (OutputSorterPoolEntity != null)
                {
                    OutputSorterPoolEntity.Name = value;
                }
            }
        }

        public override bool WasExecuted
        {
            get { return OutputSorterPoolEntity != null; }
        }

        public override string TypeName
        {
            get { return SorterPoolStep.TypeName; }
        }

        private readonly int _keyCount;
        public int KeyCount
        {
            get { return _keyCount; }
        }

        public ISorterPoolEntity OutputSorterPoolEntity
        {
            get { return (ISorterPoolEntity) OutputEntities.SingleOrDefault(T=>T.TypeName == SorterPoolEntity.TypeName); }
        }

        private readonly int _seedIn;
        public int SeedIn
        {
            get { return _seedIn; }
        }

        public int SeedOut { get; private set; }

        private readonly int _sorterCount;
        public int SorterCount
        {
            get { return _sorterCount; }
        }

        private readonly int _switchesPerSorter;
        public int SwitchesPerSorter
        {
            get { return _switchesPerSorter; }
        }
    }
}
