using DynamicModel.Model;
using DynamicModel.ViewModel;
using SorterControls.ViewModels.Entities;
using SortingNetworkDm.Entities;
using SortingNetworkDm.Steps;
using WpfUtils;

namespace SorterControls.ViewModels.Steps
{
    public interface ICompetePoolStepVm : IStepVm
    {
        ISwitchablePoolVm InputSwitchablePoolVm { get; }
        ISwitchablePoolVm OutputSwitchablePoolVm { get; }
        ISorterPoolVm InputSorterPoolVm { get; }
        ISorterResultPoolVm OutputSorterPoolVm { get; }
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

    public class CompetePoolStepVm
    {
        public static ICompetePoolStepVm Make(ICompetePoolStep competePoolStep) 
        {
            return new CompetePoolStepVmImpl(competePoolStep);
        }
    }

    public class CompetePoolStepVmImpl : StepVm, ICompetePoolStepVm
    {
        private ISwitchablePoolVm _outputSwitchablePoolVm;
        public CompetePoolStepVmImpl(IStep step) : base(step)
        {
        }

        public ICompetePoolStep CompetePoolStep
        {
            get { return (ICompetePoolStep) Step; }
        }

        #region CancelCommand

        protected void CancelExecute()
        {
            CompetePoolStep.Execute(DynamicModel.Common.RunAgent.MakeTest());
            OutputSwitchablePoolVm = SwitchablePoolVm.Make(CompetePoolStep.OutputSwitchablePoolEntity);
            OutputSorterPoolVm = SorterResultPoolVm.Make(CompetePoolStep.OutputSorterResultPoolEntity);
        }

        protected bool CanCancelExecute
        {
            get { return Step.CanExecute() && (!Step.WasExecuted); }
        }

        private CommandViewModel _cancelCommandVm;
        public CommandViewModel CancelCommandVm
        {
            get
            {
                return _cancelCommandVm ??
                    (
                        _cancelCommandVm = new CommandViewModel
                                                (
                                                    "Cancel",
                                                    new RelayCommand
                                                        (
                                                        param => CancelExecute(),
                                                        param => CanCancelExecute
                                                        )
                                                )
                    );
            }
        }

        #endregion

        #region RunCommand

        protected override void RunExecute()
        {
            CompetePoolStep.Execute(DynamicModel.Common.RunAgent.MakeTest());
        }

        protected override void MakeInputEntityVm(IEntity entity)
        {
            var switchablePoolEntity = entity as ISwitchablePoolEntity;
            if (switchablePoolEntity != null)
            {
                InputSwitchablePoolVm = SwitchablePoolVm.Make(switchablePoolEntity);
            }

            var sorterPoolEntity = entity as ISorterPoolEntity;
            if (sorterPoolEntity != null)
            {
                InputSorterPoolVm = SorterPoolVm.Make(sorterPoolEntity);
            }
        }

        protected override void MakeOutputEntityVm(IEntity entity)
        {
            var switchablePoolEntity = entity as ISwitchablePoolEntity;
            if (switchablePoolEntity != null)
            {
                OutputSwitchablePoolVm = SwitchablePoolVm.Make(switchablePoolEntity);
            }

            var sorterResultPoolEntity = entity as ISorterResultPoolEntity;
            if (sorterResultPoolEntity != null)
            {
                OutputSorterPoolVm = SorterResultPoolVm.Make(sorterResultPoolEntity);
            }
        }

        protected override bool CanRunExecute
        {
            get { return Step.CanExecute() && (!Step.WasExecuted); }
        }

        private CommandViewModel _runCommandVm;
        public override CommandViewModel RunCommandVm
        {
            get
            {
                return _runCommandVm ?? 
                    (_runCommandVm = new CommandViewModel
                                        (
                                        "Run",
                                        new RelayCommand
                                            (
                                            param => RunExecute(),
                                            param => CanRunExecute
                                            )
                                        ));
            }
        }

        #endregion

        public const string TemplateName = "CompetePoolStep";
        public override string TypeName
        {
            get { return TemplateName; }
        }

        private ISwitchablePoolVm _inputSwitchablePoolVm;
        public ISwitchablePoolVm InputSwitchablePoolVm
        {
            get { return _inputSwitchablePoolVm; }
            set
            {
                _inputSwitchablePoolVm = value;
                OnPropertyChanged("InputSwitchablePoolVm");
            }
        }

        public ISwitchablePoolVm OutputSwitchablePoolVm
        {
            get { return _outputSwitchablePoolVm; }
            set 
            { 
                _outputSwitchablePoolVm = value;
                OnPropertyChanged("OutputSwitchablePoolVm");
            }
        }

        private ISorterPoolVm _inputSorterPoolVm;
        public ISorterPoolVm InputSorterPoolVm
        {
            get { return _inputSorterPoolVm; }
            set
            {
                _inputSorterPoolVm = value;
                OnPropertyChanged("InputSorterPoolVm");
            }
        }

        private ISorterResultPoolVm _outputSorterPoolVm;
        public ISorterResultPoolVm OutputSorterPoolVm
        {
            get { return _outputSorterPoolVm; }
            set
            {
                _outputSorterPoolVm = value;
                OnPropertyChanged("OutputSorterPoolVm");
            }
        }

        public int GenerationCount
        {
            get { return CompetePoolStep.GenerationCount; }
        }

        public int KeyCount
        {
            get { return CompetePoolStep.KeyCount; }
        }

        public double MutationRate
        {
            get { return CompetePoolStep.MutationRate; }
        }

        public int SeedIn
        {
            get { return CompetePoolStep.SeedIn; }
        }

        public int SorterPoolSize
        {
            get { return CompetePoolStep.SorterPoolSize; }
        }

        public int SorterChampCount
        {
            get { return CompetePoolStep.SorterChampCount; }
        }

        public int SwitchablePoolSize
        {
            get { return CompetePoolStep.SwitchablePoolSize; }
        }

        public int SwitchableChampCount
        {
            get { return CompetePoolStep.SwitchableChampCount; }
        }

        public int SeedOut
        {
            get { return CompetePoolStep.SeedOut; }
        }
    }

}
