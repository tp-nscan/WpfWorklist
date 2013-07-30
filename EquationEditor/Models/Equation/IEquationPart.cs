using EquationEditor.Models.Operator;

namespace EquationEditor.Models.Equation
{
    public interface IEquationPart
    {
        EquationNodeType EquationNodeType { get; }
        ScampsDataType ScampsDataType { get; }
    }
}
