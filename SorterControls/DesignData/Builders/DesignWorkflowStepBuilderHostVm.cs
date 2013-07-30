using System;
using System.Collections.Generic;
using DynamicModel.Common;
using DynamicModel.Model;
using SorterControls.ViewModels.Bulders;
using SortingNetworkDm.Entities;

namespace SorterControls.DesignData.Builders
{
    public class DesignWorkflowStepBuilderHostVm : WorkflowStepBuilderHostVmImpl
    {
        public DesignWorkflowStepBuilderHostVm()
            : base(IndexProvider.MakeTest(1), new TestEntityProvider())
        {
        }
    }

    public class TestEntityProvider : IEntityProvider
    {
        public IEnumerable<IEntity> Entities
        {
            get
            {
                return
                    new IEntity[]
                    {
                        SorterPoolEntity.Make(Guid.NewGuid(), "test0", "test0 descr", null),
                        SwitchablePoolEntity.Make(Guid.NewGuid(), "test1", "test1 descr", null),
                        SorterPoolEntity.Make(Guid.NewGuid(), "test2", "test2 descr", null)
                    };
            }
        }

        public void AddEntity(IEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
