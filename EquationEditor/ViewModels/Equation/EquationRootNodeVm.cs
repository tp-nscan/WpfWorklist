using System.Linq;
using EquationEditor.Models.Equation;

namespace EquationEditor.ViewModels.Equation
{
    public class EquationRootNodeVm : EquationNodeBaseVm
    {
        public EquationRootNodeVm(string name, string contentTemplateName) 
            : base(name, EquationNodeVmType.Root, contentTemplateName, ScampsDataType.Any)
        {
        }

        public override bool CanAddChild(IEquationPart node)
        {
            return !ChildNodeVms.Any();
        }
    }
}
