using System.Collections.Generic;

namespace EquationEditor.Models.Operator
{
    public interface IOperatorRepository
    {
        IEnumerable<IOperator> Operators { get; }
    }
}
