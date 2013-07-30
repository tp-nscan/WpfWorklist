using System.Collections.ObjectModel;
using EquationEditor.Models.Equation;
using EquationEditor.ViewModels.Operator;
using WpfUtils;

namespace EquationEditor.ViewModels.Equation
{
    public class EquationVm : ViewModelBase
    {
        public EquationVm(EquationBase equationBase)
        {
            _equationBase = equationBase;
            var root = new EquationRootNodeVm(Equation.Name, "equationRoot") { IsNodeExpanded = true };
            var arg = new EquationOperatorArgNodeVm(Equation.ScampsDataType.ToResourceName(), Equation.ScampsDataType);
            arg.ParentNode = root;
            root.ChildNodeVms.Add(arg);
            ChildNodeVms.Add(root);

            //for(int i=0; i<200; i++)
            //{
            //    var node = new EquationOperatorNodeVm("node " + i, ""){IsNodeExpanded = true};
            //    root.ChildNodeVms.Add(node);
            //    for (int j = 0; j < 5; j++ )
            //    {
            //        var node2 = new EquationElementNodeVm(1, "", "node " + i +"__" + j, "") {IsNodeExpanded=true};
            //        node.ChildNodeVms.Add(node2);
            //    }
            //}
        }

        private readonly EquationBase _equationBase;
        public EquationBase Equation
        {
            get { return _equationBase; }
        }

        public string FormulaText
        {
            get { return _equationBase.FormulaText; }
        }

        public string Name
        {
            get { return Equation.Name; }
        }

        public EquationNodeVmType EquationNodeVmType { get {return EquationNodeVmType.Root;} }

        private readonly ObservableCollection<EquationNodeBaseVm> _childNodeVms = new ObservableCollection<EquationNodeBaseVm>();

        public ObservableCollection<EquationNodeBaseVm> ChildNodeVms
        {
            get { return _childNodeVms; }
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

        private bool _isAllExpanded;
        public bool IsAllExpanded
        {
            get { return _isAllExpanded; }
            set
            {
                IsNodeExpanded = value;
                _isAllExpanded = value;
                foreach (var childNodeVm in ChildNodeVms)
                {
                    childNodeVm.IsAllExpanded = value;
                }
            }
        }

        public bool IsComplete
        {
            get
            {
                return Equation.IsComplete;
            }
        }
    }
}
