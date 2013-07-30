using EquationEditor.Models;
using EquationEditor.Models.Equation;
using EquationEditor.Models.Operator;

namespace EquationEditor.ViewModels.Operator
{
    public class OperatorVm : IEquationPart
    {
        private readonly IOperator _operatorBase;

        public OperatorVm(IOperator operatorBase, string controlName)
        {
            _operatorBase = operatorBase;
            _controlName = controlName;
        }

        public IOperator OperatorBase
        {
            get { return _operatorBase; }
        }

        public string Name
        {
            get { return _operatorBase.Name; }
        }

        public StandardOrMacroOperator StandardOrMacroOperator
        {
            get { return _operatorBase.StandardOrMacroOperator; }
        }

        private readonly string _controlName;
        public string ControlName
        {
            get { return _controlName; }
        }

        public EquationNodeType EquationNodeType { get{return EquationNodeType.Operator;}}

        public ScampsDataType ScampsDataType { get { return OperatorBase.ScampsDataType; } }
    }
}
