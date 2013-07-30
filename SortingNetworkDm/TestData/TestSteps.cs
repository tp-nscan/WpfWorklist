using SortNetwork.TestData;
using SortingNetworkDm.Steps;

namespace SortingNetworkDm.TestData
{
    public static class TestSteps
    {

        #region CompetePoolStep

        private static ICompetePoolStep _theCompletedComptetePoolStep;
        public static ICompetePoolStep TheCompletedCompetePoolStep
        {
            get
            {
                return _theCompletedComptetePoolStep ?? 
                 (
                    _theCompletedComptetePoolStep = CompetePoolStep.Load
                    (
                        // ReSharper disable PossibleInvalidOperationException
                        guid: TestConstantsDm.CompetePoolStepGuid,
                        name: TestConstantsDm.CompetePoolStepName,
                        description: TestConstantsDm.CompetePoolStepDescription,
                        index: TestConstantsDm.CompetePoolStepIndex,
                        inputSorterPoolEntity: TestEntities.TheSorterPoolEntity,
                        inputSwitchablePoolEntity: TestEntities.TheSwitchablePoolEntity,
                        outputSorterResultPoolEntity: TestEntities.TestSorterResultPoolEntity(TestConstants.Seed + 1),
                        outputSwitchablePoolEntity: TestEntities.TestSwitchablePoolEntity(TestConstants.Seed + 2),
                        seedIn: TestConstants.Seed,
                        generationCount: TestConstantsDm.NumGenerations,
                        sorterPoolSize: TestConstants.SorterCount,
                        sorterChampCount: TestConstantsDm.SorterChampCount,
                        switchablePoolSize: TestConstants.SwitchableCount,
                        switchableChampCount: TestConstantsDm.SwitchableChampCount,
                        mutationRate: TestConstantsDm.MutationRate
                        // ReSharper restore PossibleInvalidOperationException
                    )
                );
            }
        }

        private static ICompetePoolStep _theInitializedComptetePoolStep;
        public static ICompetePoolStep TheInitializedCompetePoolStep
        {
            get
            {
                return _theInitializedComptetePoolStep ??
                 (
                    _theInitializedComptetePoolStep = CompetePoolStep.Create
                    (
                    // ReSharper disable PossibleInvalidOperationException
                        guid: TestConstantsDm.CompetePoolStepGuid,
                        name: TestConstantsDm.CompetePoolStepName,
                        description: TestConstantsDm.CompetePoolStepDescription,
                        index: TestConstantsDm.CompetePoolStepIndex,
                        inputSorterPoolEntity: TestEntities.TheSorterPoolEntity,
                        inputSwitchablePoolEntity: TestEntities.TheSwitchablePoolEntity,
                        seedIn: TestConstants.Seed,
                        generationCount: TestConstantsDm.NumGenerations,
                        sorterPoolSize: TestConstants.SorterCount,
                        sorterChampCount: TestConstantsDm.SorterChampCount,
                        switchablePoolSize: TestConstants.SwitchableCount,
                        switchableChampCount: TestConstantsDm.SwitchableChampCount,
                        mutationRate: TestConstantsDm.MutationRate
                    // ReSharper restore PossibleInvalidOperationException
                    )
                );
            }
        }

        #endregion

        #region SorterPoolStep

        private static ISorterPoolStep _theCompletedSorterPoolStep;
        public static ISorterPoolStep TheCompletedSorterPoolStep
        {
            get
            {
                return _theCompletedSorterPoolStep ??
                 (
                    _theCompletedSorterPoolStep = SorterPoolStep.Load
                    (
                    // ReSharper disable PossibleInvalidOperationException
                        guid: TestConstantsDm.SorterPoolEntityGuid,
                        name: TestConstantsDm.CompetePoolStepName,
                        description: TestConstantsDm.CompetePoolStepDescription,
                        index: TestConstantsDm.CompetePoolStepIndex,
                        outputSorters: TestEntities.TheSorterPoolEntity,
                        keyCount: TestConstants.KeyCount,
                        seedIn: TestConstants.Seed,
                        sorterCount: TestConstants.SorterCount,
                        switchesPerSorter: TestConstants.SwitchesPerSorter
                    // ReSharper restore PossibleInvalidOperationException
                    )
                );
            }
        }

        private static ISorterPoolStep _theInitializedSorterPoolStep;
        public static ISorterPoolStep TheInitializedSorterPoolStep
        {
            get
            {
                return _theInitializedSorterPoolStep ??
                 (
                    _theInitializedSorterPoolStep = SorterPoolStep.Create
                    (
                    // ReSharper disable PossibleInvalidOperationException
                        guid: TestConstantsDm.SorterPoolEntityGuid,
                        name: TestConstantsDm.CompetePoolStepName,
                        description: TestConstantsDm.CompetePoolStepDescription,
                        index: TestConstantsDm.CompetePoolStepIndex,
                        keyCount: TestConstants.KeyCount,
                        seedIn: TestConstants.Seed,
                        sorterCount: TestConstants.SorterCount,
                        switchesPerSorter: TestConstants.SwitchesPerSorter
                    // ReSharper restore PossibleInvalidOperationException
                    )
                );
            }
        }

        #endregion


        #region SwitchablePoolStep

        private static ISwitchablePoolStep _theCompletedSwitchablePoolStep;
        public static ISwitchablePoolStep TheCompletedSwitchablePoolStep 
        {
            get
            {
                return _theCompletedSwitchablePoolStep ??
                 (
                    _theCompletedSwitchablePoolStep = SwitchablePoolStep.Load
                    (
                    // ReSharper disable PossibleInvalidOperationException
                        guid: TestConstantsDm.SwitchablePoolEntityGuid,
                        name: TestConstantsDm.CompetePoolStepName,
                        description: TestConstantsDm.CompetePoolStepDescription,
                        index: TestConstantsDm.CompetePoolStepIndex,
                        outputSwitchables: TestEntities.TheSwitchablePoolEntity,
                        switchableType: TestConstants.SwitchableType,
                        keyCount: TestConstants.KeyCount,
                        seedIn: TestConstants.Seed,
                        switchableCount: TestConstants.SwitchableCount
                    // ReSharper restore PossibleInvalidOperationException
                    )
                );
            }
        }

        private static ISwitchablePoolStep _theInitializedSwitchablePoolStep;
        public static ISwitchablePoolStep TheInitializedSwitchablePoolStep
        {
            get
            {
                return _theInitializedSwitchablePoolStep ??
                 (
                    _theInitializedSwitchablePoolStep = SwitchablePoolStep.Create
                    (
                    // ReSharper disable PossibleInvalidOperationException
                        guid: TestConstantsDm.SwitchablePoolEntityGuid,
                        name: TestConstantsDm.CompetePoolStepName,
                        description: TestConstantsDm.CompetePoolStepDescription,
                        index: TestConstantsDm.CompetePoolStepIndex,
                        switchableType: TestConstants.SwitchableType,
                        keyCount: TestConstants.KeyCount,
                        seedIn: TestConstants.Seed,
                        switchableCount: TestConstants.SwitchableCount
                    // ReSharper restore PossibleInvalidOperationException
                    )
                );
            }
        }

        #endregion
    }
}
