using System;
using System.ComponentModel;
using System.Windows.Input;
using DynamicModel.Common;
using SortNetwork.Switchables;
using SortingNetworkDm.Steps;
using Utils;
using WpfUtils;

namespace SorterControls.ViewModels.Bulders
{
    public class SwitchablePoolBuilderVm : StepBuilderVm, IDataErrorInfo
    {
        public SwitchablePoolBuilderVm(IIndexProvider indexProvider)
            : base(indexProvider)
        {
            _switchablePoolGuid = Guid.NewGuid();
            Name = "SwitchablePoolBuilder";
            Description = "Description of SwitchablePoolBuilder";
            KeyCount = 16;
            RandomSeedIn = 123;
            SwitchableCount = 1000;
        }

        private readonly Guid _switchablePoolGuid;
        public Guid SwitchablePoolGuid
        {
            get { return _switchablePoolGuid; }
        }

        private ISwitchablePoolStep _switchablePoolStep;
        public ISwitchablePoolStep SwitchablePoolStep
        {
            get { return _switchablePoolStep; }
            set
            {
                _switchablePoolStep = value;
                OnPropertyChanged("Index");
                OnPropertyChanged("RandomSeedOut");
            }
        }

        void Build()
        {
            SwitchablePoolStep = SortingNetworkDm.Steps.SwitchablePoolStep.Create
                (
                // ReSharper disable PossibleInvalidOperationException
                    guid: Guid.NewGuid(),
                    name: Name,
                    description: Description,
                    index: MyIndexProvider.MakeIndex(),
                    switchableType: SwitchableType,
                    keyCount: KeyCount.Value,
                    seedIn: RandomSeedIn.Value,
                    switchableCount: SwitchableCount.Value
                // ReSharper restore PossibleInvalidOperationException
                );

            ModelStepCreated.OnNext(SwitchablePoolStep);
            ClearFields();
        }

        void ClearFields()
        {
            Name = string.Empty;
            Description = string.Empty;
        }

        public bool CanBuild
        {
            get
            {
                return 
                    KeyCount.HasValue &&
                    (! String.IsNullOrEmpty(Name)) &&
                    (! String.IsNullOrEmpty(Description)) &&
                    RandomSeedIn.HasValue &&
                    SwitchableCount.HasValue;
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

        public int? Index
        {
            get
            {
                return (_switchablePoolStep == null) ?
                    (int?)null : _switchablePoolStep.Index;
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
                return (_switchablePoolStep == null) ?
                    (int?) null : _switchablePoolStep.SeedOut;
            }
        }

        private int? _switchableCount;
        public int? SwitchableCount
        {
            get { return _switchableCount; }
            set
            {
                _switchableCount = value;
                OnPropertyChanged("SwitchableCount");
            }
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
                            "BuildSwitchables",
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

        public const string TemplateName = "SwitchablePoolBuilder";
        public override string TypeName
        {
            get { return TemplateName; }
        }

        private SwitchableType _switchableType = SwitchableType.BitArray;

        public SwitchableType SwitchableType
        {
            get { return _switchableType; }
            set
            {
                _switchableType = value;
                OnPropertyChanged("SwitchableType");
            }
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

                if (propertyName == "SwitchableCount")
                {
                    if (SwitchableCount.IsNotValuedWith(p => p > 0))
                    {
                        error = "RandomSeedIn should be greater than 0";
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
