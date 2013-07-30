using System.Collections.ObjectModel;
using System.Windows.Input;
using EquationEditor.Models.Equation;
using WpfUtils;

namespace EquationEditor.ViewModels.Equation
{
    public abstract class EquationNodeBaseVm : ViewModelBase
    {
        protected EquationNodeBaseVm(string name, EquationNodeVmType equationNodeVmType, string contentTemplateName, 
            ScampsDataType allowedScampsDataType)
        {
            _name = name;
            _equationNodeVmType = equationNodeVmType;
            _contentTemplateName = contentTemplateName;
            _allowedScampsDataType = allowedScampsDataType;
        }

        private readonly ScampsDataType _allowedScampsDataType;
        public ScampsDataType AllowedScampsDataType
        {
            get { return _allowedScampsDataType; }
        }

        private ObservableCollection<EquationNodeBaseVm> _childNodeVms  = new ObservableCollection<EquationNodeBaseVm>();
        public ObservableCollection<EquationNodeBaseVm> ChildNodeVms
        {
            get { return _childNodeVms; }
            set { _childNodeVms = value; }
        }

        private readonly string _contentTemplateName;
        public string ContentTemplateName
        {
            get { return _contentTemplateName; }
        }

        private readonly string _name;
        public string Name
        {
            get { return _name; }
        }

        public abstract bool CanAddChild(IEquationPart node);

        private readonly EquationNodeVmType _equationNodeVmType;
        public EquationNodeVmType EquationNodeVmType
        {
            get { return _equationNodeVmType; }
        }

        private EquationNodeBaseVm _parentNode;
        public EquationNodeBaseVm ParentNode
        {
            get { return _parentNode; }
            set
            {
                _parentNode = value;
                OnPropertyChanged("ParentNode");
            }
        }

        private bool _isNodeExpanded;
        public bool IsNodeExpanded
        {
            get { return _isNodeExpanded; }
            set
            {
                _isNodeExpanded = value;
                OnPropertyChanged("IsNodeExpanded");
            }
        }


        private bool _isNodeSelected;
        public bool IsNodeSelected
        {
            get { return _isNodeSelected; }
            set
            {
                _isNodeSelected = value;
                OnPropertyChanged("IsNodeSelected");
                CommandManager.InvalidateRequerySuggested();
            }
        }

        private bool _isAllExpanded;
        public bool IsAllExpanded
        {
            get { return _isAllExpanded; }
            set
            {
                _isAllExpanded = value;
                IsNodeExpanded = value;
                foreach (var equationNodeBaseVm in ChildNodeVms)
                {
                    equationNodeBaseVm.IsAllExpanded = value;
                }
            }
        }
    }
}
