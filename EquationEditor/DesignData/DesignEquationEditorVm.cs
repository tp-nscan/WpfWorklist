using EquationEditor.Models.Equation;
using EquationEditor.Models.Operator;
using EquationEditor.ViewModels;

namespace EquationEditor.DesignData
{
    public class DesignEquationEditorVm : EquationEditorVm
    {
        public DesignEquationEditorVm()
            : base(new EquationBase("the quadratic formula", ScampsDataType.Any, "(-b + (b^2 - 4*a*c)^0.5) / 2a"),  
            new DesignElementRepository(), new OperatorRepository())
        {
            Title = "Scamps Equation Editor";
        }
    }
}
