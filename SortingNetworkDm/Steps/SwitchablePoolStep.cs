using System;
using System.Linq;
using DynamicModel.Common;
using DynamicModel.Model;
using MathUtils.Rand;
using SortNetwork.Switchables;
using SortingNetworkDm.Entities;

namespace SortingNetworkDm.Steps
{
    public interface ISwitchablePoolStep : IStep
    {
        int KeyCount { get; }
        ISwitchablePoolEntity OutputSwitchablePoolEntity { get; }
        int SeedIn { get; }
        int SeedOut { get; }
        int SwitchableCount { get; }
        SwitchableType SwitchableType { get; }
    }

    public static class SwitchablePoolStep
    {
        public const string TypeName = "SwitchablePoolStep";

        public static ISwitchablePoolStep Load
        (
            Guid guid,
            string name,
            string description,
            int index,
            ISwitchablePoolEntity outputSwitchables,
            SwitchableType switchableType,
            int keyCount,
            int seedIn,
            int switchableCount
        )
        {
            return new SwitchablePoolStepImpl
                (
                    guid: guid,
                    name: name,
                    description: description,
                    index: index,
                    outputEntities: outputSwitchables,
                    switchableType: switchableType,
                    keyCount: keyCount,
                    seedIn: seedIn,
                    switchableCount: switchableCount
                );
        }

        public static ISwitchablePoolStep Create
        (
            Guid guid,
            int index,
            string name,
            string description,
            SwitchableType switchableType,
            int keyCount,
            int seedIn,
            int switchableCount
        )
        {
            var switchablePoolRandGen = new SwitchablePoolStepImpl
                (
                    guid: guid,
                    name: name,
                    description: description,
                    index: index,
                    outputEntities: null,
                    switchableType: switchableType,
                    keyCount: keyCount,
                    seedIn: seedIn,
                    switchableCount: switchableCount
                );

            return switchablePoolRandGen;
        }

        public static ISwitchablePoolStep CreateAndRun
        (
            Guid guid,
            int index,
            string name,
            string description,
            SwitchableType switchableType,
            int keyCount,
            int seedIn,
            int switchableCount
        )
        {
            var switchablePoolRandGen = Create
                (
                    guid: guid,
                    name: name,
                    description: description,
                    index: index,
                    switchableType: switchableType,
                    keyCount: keyCount,
                    seedIn: seedIn,
                    switchableCount: switchableCount
                );

            switchablePoolRandGen.Execute(runAgent: RunAgent.MakeTest());

            return switchablePoolRandGen;
        }
    }

    public class SwitchablePoolStepImpl : StepImpl, ISwitchablePoolStep
    {
        public SwitchablePoolStepImpl
        (
            Guid guid,
            string name,
            string description,
            int index,
            ISwitchablePoolEntity outputEntities,
            SwitchableType switchableType,
            int keyCount,
            int seedIn,
            int switchableCount
        )
            : base
            (
                guid: guid,
                index: index,
                inputEntities: Enumerable.Empty<IEntity>(),
                outputEntities: (outputEntities == null) ? Enumerable.Empty<ISwitchablePoolEntity>() : new[] { outputEntities }
            )
        {
// ReSharper disable DoNotCallOverridableMethodsInConstructor
            Name = name;
            Description = description;
// ReSharper restore DoNotCallOverridableMethodsInConstructor
            _switchableType = switchableType;
            _keyCount = keyCount;
            _seedIn = seedIn;
            _switchableCount = switchableCount;
        }

        public override void Execute(IRunAgent runAgent)
        {
            var rando = Randy.Fast(SeedIn);

            ISwitchableRepo switchableRepo = null;
            switch (SwitchableType)
            {
                case SwitchableType.BitArray:
                    switchableRepo = SwitchableBitArray.MakeRandoms(KeyCount, rando, SwitchableCount).ToSwitchableRepo();
                    break;
                case SwitchableType.IntArray:
                    switchableRepo = SwitchableIntArray.MakeRandoms(KeyCount, rando, SwitchableCount).ToSwitchableRepo();
                    break;
                case SwitchableType.Short:
                    switchableRepo = SwitchableShort.MakeRandoms(rando, SwitchableCount).ToSwitchableRepo();
                    break;
            }

            SeedOut = rando.ToInt().Next();

            AddOutputEnity
                (
                    SwitchablePoolEntity.Make
                    (
                        guid: Guid, 
                        name: Name, 
                        description: Description,
                        switchableRepo: switchableRepo
                    )
                );
        }

        public override bool WasExecuted
        {
            get { return OutputSwitchablePoolEntity != null; }
        }

        public override string TypeName
        {
            get { return SwitchablePoolStep.TypeName; }
        }

        private readonly int _keyCount;
        public int KeyCount
        {
            get { return _keyCount; }
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
                if (OutputSwitchablePoolEntity != null)
                {
                    OutputSwitchablePoolEntity.Description = value;
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
                if (OutputSwitchablePoolEntity != null)
                {
                    OutputSwitchablePoolEntity.Name = value;
                }
            }
        }

        public ISwitchablePoolEntity OutputSwitchablePoolEntity
        {
            get { return (ISwitchablePoolEntity) OutputEntities.SingleOrDefault(T => T.TypeName == SwitchablePoolEntity.TypeName); }
        }

        private readonly int _seedIn;
        public int SeedIn
        {
            get { return _seedIn; }
        }

        public int SeedOut { get; private set; }

        private readonly int _switchableCount;
        public int SwitchableCount
        {
            get { return _switchableCount; }
        }

        private readonly SwitchableType _switchableType;
        public SwitchableType SwitchableType
        {
            get { return _switchableType; }
        }
    }
}
