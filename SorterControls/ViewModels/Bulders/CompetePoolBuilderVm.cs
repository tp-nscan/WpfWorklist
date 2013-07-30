using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using DynamicModel.Common;
using SorterControls.ViewModels.Entities;
using SortingNetworkDm.Entities;
using SortingNetworkDm.Steps;
using Utils;
using WpfUtils;

namespace SorterControls.ViewModels.Bulders
{
    public class CompetePoolBuilderVm : StepBuilderVm, IDataErrorInfo
    {
        public CompetePoolBuilderVm
            (   
                IIndexProvider indexProvider,
                IEnumerable<ISorterPoolEntity> sorterPools,
                IEnumerable<ISwitchablePoolEntity> switchablePoolVms
            )
            : base(indexProvider)
        {
            _outputSortersGuid = Guid.NewGuid();
            _outputSwitchablesGuid = Guid.NewGuid();
            _sorterPoolVms.AddMany(sorterPools.Select(SorterPoolVm.Make));
            _switchablePoolVms.AddMany(switchablePoolVms.Select(SwitchablePoolVm.Make));

            Name = "CompetePoolBuilder";
            Description = "Description of CompetePoolBuilder";
            SorterPoolSize = 100;
            SorterChampCount = 25;
            RandomSeedIn = 123;
            SwitchablePoolSize = 1000;
            SwitchableChampCount = 250;
            MutationRate = 0.05;
            GenerationCount = 10;
        }

        private ObservableCollection<ISorterPoolVm> _sorterPoolVms
            = new ObservableCollection<ISorterPoolVm>();
        public ObservableCollection<ISorterPoolVm> SorterPoolVms
        {
            get { return _sorterPoolVms; }
            set { _sorterPoolVms = value; }
        }

        private ObservableCollection<ISwitchablePoolVm> _switchablePoolVms 
            = new ObservableCollection<ISwitchablePoolVm>();
        public ObservableCollection<ISwitchablePoolVm> SwitchablePoolVms
        {
            get { return _switchablePoolVms; }
            set { _switchablePoolVms = value; }
        }

        private ICompetePoolStep _competePoolStep;
        public ICompetePoolStep CompetePoolStep
        {
            get { return _competePoolStep; }
            set
            {
                _competePoolStep = value;
                OnPropertyChanged("Index");
                OnPropertyChanged("RandomSeedOut");
            }
        }

        public int? Index
        {
            get
            {
                return (_competePoolStep == null) ?
                    (int?)null : _competePoolStep.Index;
            }
        }

        public int? RandomSeedOut
        {
            get
            {
                return (_competePoolStep == null) ?
                    (int?)null : _competePoolStep.SeedOut;
            }

        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        private int? _generationCount;
        public int? GenerationCount
        {
            get { return _generationCount; }
            set
            {
                _generationCount = value;
                OnPropertyChanged("GenerationCount");
            }
        }

        private double? _mutationRate;
        public double? MutationRate
        {
            get { return _mutationRate; }
            set
            {
                _mutationRate = value;
                OnPropertyChanged("MutationRate");
            }
        }

        private int? _switchableChampCount;
        public int? SwitchableChampCount
        {
            get { return _switchableChampCount; }
            set
            {
                _switchableChampCount = value;
                OnPropertyChanged("SwitchableChampCount");
            }
        }

        private int? _randomSeedIn;
        public int? RandomSeedIn
        {
            get { return _randomSeedIn; }
            set
            {
                _randomSeedIn = value;
                OnPropertyChanged("RandomSeedIn");
            }
        }

        private int? _switchablePoolSize;
        public int? SwitchablePoolSize
        {
            get { return _switchablePoolSize; }
            set
            {
                _switchablePoolSize = value;
                OnPropertyChanged("SwitchablePoolSize");
            }
        }

        private Guid? _outputSortersGuid;
        public Guid? OutputSortersGuid
        {
            get { return _outputSortersGuid; }
            set
            {
                _outputSortersGuid = value;
                OnPropertyChanged("OutputSortersGuid");
            }
        }

        private Guid? _outputSwitchablesGuid;
        public Guid? OutputSwitchablesGuid
        {
            get { return _outputSwitchablesGuid; }
            set
            {
                _outputSwitchablesGuid = value;
                OnPropertyChanged("OutputSwitchablesGuid");
            }
        }

        private int? _sorterPoolSize;
        public int? SorterPoolSize
        {
            get { return _sorterPoolSize; }
            set
            {
                _sorterPoolSize = value;
                OnPropertyChanged("SorterPoolSize");
            }
        }

        private int? _sorterChampCount;

        public int? SorterChampCount
        {
            get { return _sorterChampCount; }
            set
            {
                _sorterChampCount = value;
                OnPropertyChanged("SorterChampCount");
            }
        }
        
        void Build()
        {
            CompetePoolStep = SortingNetworkDm.Steps.CompetePoolStep.Create
                (
                // ReSharper disable PossibleInvalidOperationException
                    guid: ModelGuid,
                    name: Name,
                    description: Description,
                    index: MyIndexProvider.MakeIndex(),
                    inputSorterPoolEntity: InputSorterPoolVm.SorterPoolEntity,
                    inputSwitchablePoolEntity: InputSwitchablePoolVm.SwitchablePoolEntity,
                    seedIn: RandomSeedIn.Value,
                    generationCount: GenerationCount.Value,
                    sorterPoolSize: SorterPoolSize.Value,
                    sorterChampCount: SorterChampCount.Value,
                    switchablePoolSize: SwitchablePoolSize.Value,
                    switchableChampCount: SwitchableChampCount.Value,
                    mutationRate: MutationRate.Value
                // ReSharper restore PossibleInvalidOperationException
                );

            ClearFields();
            ModelStepCreated.OnNext(CompetePoolStep);

        }

        void ClearFields()
        {
            Name = string.Empty;
            Description = string.Empty;
        }

        bool CanBuild
        {
            get
            {
                return
                       (InputSwitchablePoolVm != null) &&
                       (InputSorterPoolVm != null) &&
                        (!String.IsNullOrEmpty(Name)) &&
                        (!String.IsNullOrEmpty(Description)) &&
                       OutputSortersGuid.HasValue &&
                       OutputSwitchablesGuid.HasValue &&
                       RandomSeedIn.HasValue &&
                       GenerationCount.HasValue &&
                       SorterPoolSize.HasValue &&
                       SorterChampCount.HasValue &&
                       SwitchablePoolSize.HasValue &&
                       SwitchableChampCount.HasValue &&
                       MutationRate.HasValue;
            }
        }

        public ISwitchablePoolVm InputSwitchablePoolVm
        {

            get { return SwitchablePoolVms.SelectedItem(); }
        }

        public ISorterPoolVm InputSorterPoolVm
        {
            get { return SorterPoolVms.SelectedItem(); }
        }

        private CommandViewModel _buildCommandVm;
        public override CommandViewModel BuildCommandVm
        {
            get
            {
                if (_buildCommandVm == null)
                {
                    _buildCommandVm = new CommandViewModel
                        (
                            "BuildCompetePool",
                            new RelayCommand
                                (
                                    param => Build(),
                                    param => CanBuild
                                )
                        );
                }
                return _buildCommandVm;
            }
        }

        public const string TemplateName = "CompetePoolBuilder";
        public override string TypeName
        {
            get { return TemplateName; }
        }

        #region IDataErrorInfo Members

        string IDataErrorInfo.Error
        {
            get { return null; }
        }

        string IDataErrorInfo.this[string propertyName]
        {
            get
            {
                string error = null;

                if (propertyName == "Description")
                {
                    if (string.IsNullOrEmpty(Description))
                    {
                        error = "Please describe this entity";
                    }
                }

                if (propertyName == "Name")
                {
                    if (string.IsNullOrEmpty(Name))
                    {
                        error = "Please name this entity";
                    }
                }

                if (propertyName == "GenerationCount")
                {
                    if (GenerationCount.IsNotValuedWith(p => p > 0))
                    {
                        error = "GenerationCount should be greater than 0";
                    }
                }

                if (propertyName == "MutationRate")
                {
                    if (MutationRate.IsNotValuedWith(p => ((p > 0) && (p < 1.0))))
                    {
                        error = "MutationRate should be between 0 and 1";
                    }
                }

                if (propertyName == "RandomSeedIn")
                {
                    if (RandomSeedIn.IsNotValuedWith(p => p>0))
                    {
                        error = "RandomSeedIn should be greater than 0";
                    }
                }

                if (propertyName == "SorterChampCount")
                {
                    if (SorterChampCount.IsNotValuedWith(p => (SorterPoolSize % p == 0)))
                    {
                        error = "SorterPoolSize should be multiple of SorterChampCount";
                    }
                }

                if (propertyName == "SorterPoolSize")
                {
                    if (SorterPoolSize.IsNotValuedWith(p => ( p % SorterChampCount == 0)))
                    {
                        error = "SorterPoolSize should be multiple of SorterChampCount";
                    }
                    else
                    {
                        OnPropertyChanged("SorterChampCount");
                    }
                }

                if (propertyName == "SwitchableChampCount")
                {
                    if (SwitchableChampCount.IsNotValuedWith(p => (SwitchablePoolSize % p == 0)))
                    {
                        error = "SwitchablePoolSize should be multiple of SwitchableChampCount";
                    }
                }

                if (propertyName == "SwitchablePoolSize")
                {
                    if (SwitchablePoolSize.IsNotValuedWith(p => (p % SwitchableChampCount == 0)))
                    {
                        error = "SwitchablePoolSize should be multiple of SwitchableChampCount";
                    }
                    else
                    {
                        OnPropertyChanged("SwitchableChampCount");
                    }
                }

                CommandManager.InvalidateRequerySuggested();

                return error;

            }
        }

        #endregion // IDataErrorInfo Members
    }
}
