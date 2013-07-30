using System.Linq;
using EquationEditor.Models.Equation;

namespace EquationEditor.ViewModels.Equation
{
    public class EquationOperatorArgNodeVm : EquationNodeBaseVm
    {
        public EquationOperatorArgNodeVm(string contentTemplateName, 
            ScampsDataType allowedScampsDataType)
            : base(string.Empty, EquationNodeVmType.OperatorArg, contentTemplateName, allowedScampsDataType)
        {
        }

        public override bool CanAddChild(IEquationPart node)
        {
            return AllowedScampsDataType.AcceptsChildrenOfDataType(node.ScampsDataType)
                   && ! ChildNodeVms.Any();
        }
    }
}
