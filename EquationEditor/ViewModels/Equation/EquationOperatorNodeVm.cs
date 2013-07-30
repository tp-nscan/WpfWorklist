using System.Linq;
using System.Windows.Input;
using EquationEditor.Models.Equation;
using EquationEditor.ViewModels.Operator;
using Microsoft.Practices.Prism.Commands;
using WpfUtils;

namespace EquationEditor.ViewModels.Equation
{
    public class EquationOperatorNodeVm : EquationNodeBaseVm
    {
        public EquationOperatorNodeVm(string name, string contentTemplateName, ScampsDataType scampsDataType, 
               string description)
            : base(name, EquationNodeVmType.OperatorArg, contentTemplateName, scampsDataType)
        {
            _description = description;
        }

        public override bool CanAddChild(IEquationPart node)
        {
            return false;
        }

        private string _description;
        public string Description
        {
            get { return _description; }
        }
        #region AddChild Command

        private ICommand _addChild;
        public ICommand AddChild
        {
            get
            {
                return _addChild ?? (_addChild = new DelegateCommand<string>(DoTheAddChild, arg => CanAddChild()));
            }
        }

        private bool CanAddChild()
        {
            return true;
        }

        void DoTheAddChild(string searchText)
        {
            if (ChildNodeVms.FirstOrDefault() == null) { return; }
            var newSibling = new EquationOperatorArgNodeVm(AllowedScampsDataType.ToResourceName(), AllowedScampsDataType);
            newSibling.ParentNode = this;
            ChildNodeVms.Add(newSibling);
            newSibling.IsNodeSelected = true;
        }

        #endregion // AddChild Command

        #region RemoveChild Command

        private ICommand _removeChild;
        public ICommand RemoveChild
        {
            get {
                return _removeChild ?? (_removeChild = new RelayCommand(DoTheRemoveChild, arg => CanRemoveChild()));
            }
        }

        private bool CanRemoveChild()
        {
            var target = ChildNodeVms.FirstOrDefault(T => T.IsNodeSelected);
            if (target == null) { return false; }
            return ChildNodeVms.Count() > 1;
            //return true;
        }

        void DoTheRemoveChild(object searchText)
        {
            var target = ChildNodeVms.FirstOrDefault(T=>T.IsNodeSelected);
            if (target == null || ChildNodeVms.Count() < 2) { return; }
            ChildNodeVms.Remove(target);
            var equationNodeBaseVm = ChildNodeVms.FirstOrDefault();
            if (equationNodeBaseVm != null) equationNodeBaseVm.IsNodeSelected = true;
        }

        #endregion // RemoveChild Command
    }
}
