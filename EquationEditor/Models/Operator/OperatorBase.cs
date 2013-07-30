using System.Collections.Generic;
using System.Linq;
using EquationEditor.Models.Equation;

namespace EquationEditor.Models.Operator
{
    public class OperatorBase : IOperator
    {
        private readonly string _name;
        private readonly string _description;
        private readonly ScampsDataType _returnType;
        private readonly StandardOrMacroOperator _standardOrMacroOperator;

        public OperatorBase(string name, string description, ScampsDataType returnType, 
            StandardOrMacroOperator standardOrMacroOperator, IEnumerable<OperatorArg> operatorArgs)
        {
            _name = name;
            _description = description;
            _returnType = returnType;
            _standardOrMacroOperator = standardOrMacroOperator;
            _operatorArgs = operatorArgs.ToList();
        }

        public ScampsDataType ScampsDataType
        {
            get { return _returnType; }
        }

        public string Description
        {
            get { return _description; }
        }

        public string Name
        {
            get { return _name; }
        }

        public StandardOrMacroOperator StandardOrMacroOperator
        {
            get { return _standardOrMacroOperator; }
        }

        public EquationNodeType EquationNodeType { get { return EquationNodeType.Operator; } }

        private readonly List<OperatorArg> _operatorArgs;
        public IEnumerable<OperatorArg> OperatorArgs
        {
            get { return _operatorArgs; }
        }
    }
}
