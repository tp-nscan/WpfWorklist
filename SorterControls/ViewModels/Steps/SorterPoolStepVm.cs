using DynamicModel.Model;
using DynamicModel.ViewModel;
using SorterControls.ViewModels.Entities;
using SortingNetworkDm.Entities;
using SortingNetworkDm.Steps;
using WpfUtils;

namespace SorterControls.ViewModels.Steps
{
    public interface ISorterPoolStepVm : IStepVm
    {
        int KeyCount { get; }
        ISorterPoolVm OutputSorterPoolVm { get; }
        int SeedIn { get; }
        int SeedOut { get; }
        int SorterCount { get; }
        int SwitchesPerSorter { get; }
    }

    public static class SorterPoolStepVm 
    {
        public static ISorterPoolStepVm Make(ISorterPoolStep sorterPoolStep)
        {
            return new SorterPoolStepVmImpl(sorterPoolStep);
        }
    }

    public class SorterPoolStepVmImpl : StepVm, ISorterPoolStepVm
    {

        public SorterPoolStepVmImpl(IStep step) : base(step)
        {

        }

        public ISorterPoolStep SorterPoolStep
        {
            get { return (ISorterPoolStep) Step; }
        }

        protected override void MakeInputEntityVm(IEntity entity)
        {
        }

        protected override void MakeOutputEntityVm(IEntity entity)
        {
            OutputSorterPoolVm = SorterPoolVm.Make((ISorterPoolEntity) entity);
        }

        protected override bool CanRunExecute
        {
            get { return Step.CanExecute() && (! Step.WasExecuted); }
        }

        protected override void RunExecute()
        {
            SorterPoolStep.Execute(DynamicModel.Common.RunAgent.MakeTest());
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

        public const string TemplateName = "SorterPoolStep";
        public override string TypeName
        {
            get { return TemplateName; }
        }

        public int KeyCount
        {
            get { return SorterPoolStep.KeyCount; }
        }

        private ISorterPoolVm _outputSorterPoolVm;
        public ISorterPoolVm OutputSorterPoolVm
        {
            get {return _outputSorterPoolVm; }
            set 
            {
                _outputSorterPoolVm = value;
                OnPropertyChanged("OutputSorterPoolVm");
            }
        }

        public int SeedIn
        {
            get { return SorterPoolStep.SeedIn; }
        }

        public int SeedOut
        {
            get { return SorterPoolStep.SeedOut; }
        }

        public int SorterCount
        {
            get { return SorterPoolStep.SorterCount; }
        }

        public int SwitchesPerSorter
        {
            get { return SorterPoolStep.SwitchesPerSorter; }
        }
    }
}