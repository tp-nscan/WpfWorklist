using System;
using DynamicModel.Model;
using DynamicModel.ViewModel;
using WpfUtils;

namespace SorterControls.ViewModels.Steps
{
    public interface IEmptyStepVm : IStepVm
    {
        
    }

    public static class EmptyStepVm
    { 

    }

    public class EmptyStepVmImpl : StepVm
    {
        public EmptyStepVmImpl()
            : base(null)
        {
        }

        protected override void MakeInputEntityVm(IEntity entity)
        {
            throw new NotImplementedException();
        }

        protected override void MakeOutputEntityVm(IEntity entity)
        {
            throw new NotImplementedException();
        }

        protected override bool CanRunExecute
        {
            get { throw new NotImplementedException(); }
        }

        protected override void RunExecute()
        {
            throw new NotImplementedException();
        }

        public override CommandViewModel RunCommandVm
        {
            get { throw new NotImplementedException(); }
        }
        
        public const string TemplateName = "EmptyStep";
        public override string TypeName
        {
            get { return TemplateName; }
        }
    }
}
