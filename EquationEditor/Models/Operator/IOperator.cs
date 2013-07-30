using System.Collections.Generic;
using EquationEditor.Models.Equation;

namespace EquationEditor.Models.Operator
{
    public interface IOperator : IEquationPart
    {
        IEnumerable<OperatorArg> OperatorArgs { get; }
        StandardOrMacroOperator StandardOrMacroOperator { get; }
        string Description { get; }
        string Name { get; }
    }
}