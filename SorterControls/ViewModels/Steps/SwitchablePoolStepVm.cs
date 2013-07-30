using DynamicModel;
using DynamicModel.Model;
using DynamicModel.ViewModel;
using SortNetwork.Switchables;
using SorterControls.ViewModels.Entities;
using SortingNetworkDm.Entities;
using SortingNetworkDm.Steps;
using WpfUtils;

namespace SorterControls.ViewModels.Steps
{
    public interface ISwitchablePoolStepVm : IStepVm
    {
        int KeyCount { get; }
        ISwitchablePoolVm OutputSwitchablePoolVm { get; }
        int SeedIn { get; }
        int SeedOut { get; }
        int SwitchableCount { get; }
        SwitchableType SwitchableType { get; }
    }

    public static class SwitchablePoolStepVm
    {
        public static ISwitchablePoolStepVm Make(ISwitchablePoolStep switchablePoolStep)
        {
            return new SwitchablePoolStepVmImpl(switchablePoolStep);
        }
    }

    public class SwitchablePoolStepVmImpl : StepVm, ISwitchablePoolStepVm
    {
        public SwitchablePoolStepVmImpl(IStep step) : base(step)
        {

        }

        private CommandViewModel _runCommandVm;
        public override CommandViewModel RunCommandVm
        {
            get
            {
                if (_runCommandVm == null)
                {
                    _runCommandVm = new CommandViewModel
                        (
                            "MakeTest",
                            new RelayCommand
                                (
                                    param => RunExecute(),
                                    param => CanRunExecute
                                )
                        );
                }
                return _runCommandVm;
            }
        }

        public const string TemplateName = "SwitchablePoolStep";
        public override string TypeName
        {
            get { return TemplateName; }
        }

        public ISwitchablePoolStep SwitchablePoolStep
        {
            get { return (ISwitchablePoolStep) Step; }
        }

        protected override void MakeInputEntityVm(IEntity entity)
        {
        }

        protected override void MakeOutputEntityVm(IEntity entity)
        {
            OutputSwitchablePoolVm = SwitchablePoolVm.Make((ISwitchablePoolEntity) entity);
        }

        protected override bool CanRunExecute
        {
            get { return Step.CanExecute() && (!Step.WasExecuted); }
        }

        protected override void RunExecute()
        {
            SwitchablePoolStep.Execute(DynamicModel.Common.RunAgent.MakeTest());
        }

        public int KeyCount
        {
            get { return SwitchablePoolStep.KeyCount; }
        }

        private ISwitchablePoolVm _outputSwitchablePoolVm;
        public ISwitchablePoolVm OutputSwitchablePoolVm
        {
            get { return _outputSwitchablePoolVm; }
            set 
            {
                _outputSwitchablePoolVm = value;
                OnPropertyChanged("OutputSwitchablePoolVm");
            }
        }

        public int SeedIn
        {
            get { return SwitchablePoolStep.SeedIn; }
        }

        public int SeedOut
        {
            get { return SwitchablePoolStep.SeedOut; }
        }

        public int SwitchableCount
        {
            get { return SwitchablePoolStep.SwitchableCount; }
        }

        public SwitchableType SwitchableType
        {
            get { return SwitchablePoolStep.SwitchableType; }
        }
    }
}