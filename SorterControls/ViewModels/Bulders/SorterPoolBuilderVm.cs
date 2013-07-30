using System;
using System.ComponentModel;
using System.Windows.Input;
using DynamicModel.Common;
using SortingNetworkDm.Steps;
using Utils;
using WpfUtils;

namespace SorterControls.ViewModels.Bulders
{
    public class SorterPoolBuilderVm : StepBuilderVm, IDataErrorInfo
    {
        public SorterPoolBuilderVm(IIndexProvider indexProvider)
            : base(indexProvider)
        {
            _sorterPoolGuid = Guid.NewGuid();
            Name = "SorterPoolBuilder";
            Description = "Description of SorterPoolBuilder";
            KeyCount = 16;
            RandomSeedIn = 123;
            SorterCount = 100;
            SwitchesPerSorter = 700;
        }

        private readonly Guid _sorterPoolGuid;
        public Guid SorterPoolGuid
        {
            get { return _sorterPoolGuid; }
        }

        private ISorterPoolStep _sorterPoolStep;
        public ISorterPoolStep SorterPoolStep
        {
            get { return _sorterPoolStep; }
            set
            {
                _sorterPoolStep = value;
                OnPropertyChanged("Index");
                OnPropertyChanged("RandomSeedOut");
            }
        }


        void Build()
        {
            SorterPoolStep = SortingNetworkDm.Steps.SorterPoolStep.Create(
                // ReSharper disable PossibleInvalidOperationException
                    guid: Guid.NewGuid(),
                    name: Name,
                    description: Description,
                    index: MyIndexProvider.MakeIndex(),
                    keyCount: KeyCount.Value,
                    seedIn: RandomSeedIn.Value,
                    sorterCount: SorterCount.Value,
                    switchesPerSorter: SwitchesPerSorter.Value
                // ReSharper restore PossibleInvalidOperationException
                );

            ModelStepCreated.OnNext(SorterPoolStep);
            ClearFields();
        }

        bool CanBuild
        {
            get
            {
                return
                 KeyCount.HasValue &&
                (!String.IsNullOrEmpty(Name)) &&
                (!String.IsNullOrEmpty(Description)) &&
                 RandomSeedIn.HasValue &&
                 SorterCount.HasValue &&
                 SwitchesPerSorter.HasValue;  
            }

        }

        void ClearFields()
        {
            Name = string.Empty;
            Description = string.Empty;
        }

        private string _description = string.Empty;
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

        public int? Index
        {
            get
            {
                return (_sorterPoolStep == null) ?
                    (int?) null : _sorterPoolStep.Index; 
            }
        }

        private int? _keyCount;
        public int? KeyCount
        {
            get { return _keyCount; }
            set
            {
                _keyCount = value;
                OnPropertyChanged("KeyCount");
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

        public int? RandomSeedOut
        {
            get 
            {
                return (_sorterPoolStep == null) ?
                    (int?) null : _sorterPoolStep.SeedOut; 
            }

        }

        private int? _sorterCount;
        public int? SorterCount
        {
            get { return _sorterCount; }
            set
            {
                _sorterCount = value;
                OnPropertyChanged("SorterCount");
            }
        }

        private int? _switchesPerSorter;
        public int? SwitchesPerSorter
        {
            get { return _switchesPerSorter; }
            set
            {
                _switchesPerSorter = value;
                OnPropertyChanged("SwitchesPerSorter");
            }
        }

        public const string TemplateName = "SorterPoolBuilder";

        private CommandViewModel _buildSorters;
        public override CommandViewModel BuildCommandVm
        {
            get
            {
                if (_buildSorters == null)
                {
                    _buildSorters = new CommandViewModel
                        (
                            "MakeTest",
                            new RelayCommand
                                (
                                    param => Build(),
                                    param => CanBuild
                                )
                        );
                }
                return _buildSorters;
            }
        }

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
                        error = "Please describe this step";
                    }
                }

                if (propertyName == "Name")
                {
                    if (string.IsNullOrEmpty(Name))
                    {
                        error = "Please name this step";
                    }
                }

                if (propertyName == "RandomSeedIn")
                {
                    if (RandomSeedIn.IsNotValuedWith(p => p > 0))
                    {
                        error = "RandomSeedIn should be greater than 0";
                    }
                }
                if (propertyName == "SorterCount")
                {
                    if (SorterCount.IsNotValuedWith(p => p > 0))
                    {
                        error = "SorterCount should be greater than 0";
                    }
                }
                if (propertyName == "SwitchesPerSorter")
                {
                    if (SwitchesPerSorter.IsNotValuedWith(p => p > 0))
                    {
                        error = "SwitchesPerSorter should be greater than 0";
                    }
                }
                if (propertyName == "KeyCount")
                {
                    if (KeyCount.IsNotValuedWith(p => p > 0))
                    {
                        error = "KeyCount should be greater than 0";
                    }
                }
              
                CommandManager.InvalidateRequerySuggested();

                return error;

            }
        }

        #endregion // IDataErrorInfo Members
    }
}
