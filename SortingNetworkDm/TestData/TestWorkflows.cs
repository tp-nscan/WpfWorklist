using DynamicModel.Model;
using SortingNetworkDm.Workflows;

namespace SortingNetworkDm.TestData
{
    public static class TestWorkflows
    {
        #region Workflow with a CompletedCompetePoolStep

        private static ISorterWorkflow _theCompetePoolWorkflow;
        public static ISorterWorkflow OneCompletedCompetePoolWorkflow
        {
            get
            {
                return _theCompetePoolWorkflow ??
                 (
                    _theCompetePoolWorkflow = SorterWorkflow.Load
                    (
                    // ReSharper disable PossibleInvalidOperationException
                        guid: TestConstantsDm.CompetePoolStepGuid,
                        name: TestConstantsDm.CompetePoolStepName,
                        path: string.Empty,
                        entities: TestSteps.TheCompletedCompetePoolStep.AllEntities(),
                        steps: new[] { TestSteps.TheCompletedCompetePoolStep }
                    // ReSharper restore PossibleInvalidOperationException
                    )
                );
            }
        }

        #endregion

        #region Workflow with a CompletedSorterPoolWorkflow

        private static ISorterWorkflow _theSorterPoolWorkflow;
        public static ISorterWorkflow OneCompletedSorterPoolWorkflow
        {
            get
            {
                return _theSorterPoolWorkflow ??
                 (
                    _theSorterPoolWorkflow = SorterWorkflow.Load
                    (
                    // ReSharper disable PossibleInvalidOperationException
                        guid: TestConstantsDm.CompetePoolStepGuid,
                        name: TestConstantsDm.CompetePoolStepName,
                        path: string.Empty,
                        entities: TestSteps.TheCompletedCompetePoolStep.AllEntities(),
                        steps: new[] { TestSteps.TheCompletedSorterPoolStep }
                    // ReSharper restore PossibleInvalidOperationException
                    )
                );
            }
        }

        #endregion

        #region Workflow with a CompletedSwitchablePoolWorkflow

        private static ISorterWorkflow _theSwitchablePoolWorkflow;
        public static ISorterWorkflow OneCompletedSwitchablePoolWorkflow
        {
            get
            {
                return _theSwitchablePoolWorkflow ??
                 (
                    _theSwitchablePoolWorkflow = SorterWorkflow.Load
                    (
                    // ReSharper disable PossibleInvalidOperationException
                        guid: TestConstantsDm.CompetePoolStepGuid,
                        name: TestConstantsDm.CompetePoolStepName,
                        path: string.Empty,
                        entities: TestSteps.TheCompletedCompetePoolStep.AllEntities(),
                        steps: new[] { TestSteps.TheCompletedSwitchablePoolStep  }
                    // ReSharper restore PossibleInvalidOperationException
                    )
                );
            }
        }

        #endregion
    }
}
