using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicModel;
using DynamicModel.Model;
using DynamicModel.ViewModel;
using SorterControls.ViewModels.Entities;
using SorterControls.ViewModels.Steps;
using SortingNetworkDm.Entities;
using SortingNetworkDm.Steps;

namespace SorterControls.ViewModels.Common
{
    public static class VmConverters
    {
        public static IStepVm ToStepVm(this IStep step)
        {
            switch (step.TypeName)
            {
                case SwitchablePoolStep.TypeName:
                    return SwitchablePoolStepVm.Make((ISwitchablePoolStep)step);
                case SorterPoolStep.TypeName:
                    return SorterPoolStepVm.Make((ISorterPoolStep)step);
                case CompetePoolStep.TypeName:
                    return CompetePoolStepVm.Make((ICompetePoolStep)step);
            }
            throw new Exception(step.TypeName + " not handled in VmConverters.ToStepVm");
        }

        public static IEntityVm ToEntityVm(this IEntity entity)
        {
            switch (entity.TypeName)
            {
                case SwitchablePoolEntity.TypeName:
                    return SwitchablePoolVm.Make((ISwitchablePoolEntity) entity);
                case SorterPoolEntity.TypeName:
                    return SorterPoolVm.Make((ISorterPoolEntity) entity);

            }
            throw new Exception(entity.TypeName + " not handled in VmConverters.ToEntityVm");
        }
    }
}
