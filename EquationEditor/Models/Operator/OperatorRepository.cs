using System.Collections.Generic;
using EquationEditor.Models.Equation;

namespace EquationEditor.Models.Operator
{
    public class OperatorRepository : IOperatorRepository
    {
        public IEnumerable<IOperator> Operators
        {
            get
            {
                yield return new OperatorBase("Add", "add two numeric values", ScampsDataType.Numeric, StandardOrMacroOperator.Standard, new OperatorArg[] { new OperatorArg("arg1", ScampsDataType.Numeric), new OperatorArg("arg2", ScampsDataType.Numeric) });
                yield return new OperatorBase("Subtract", "subtract two numeric values", ScampsDataType.Numeric, StandardOrMacroOperator.Standard, new OperatorArg[] { new OperatorArg("arg1", ScampsDataType.Numeric), new OperatorArg("arg2", ScampsDataType.Numeric) });
                yield return new OperatorBase("Multiply", "multiply two numeric values", ScampsDataType.Numeric, StandardOrMacroOperator.Standard, new OperatorArg[] { new OperatorArg("arg1", ScampsDataType.Numeric), new OperatorArg("arg2", ScampsDataType.Numeric) });
                yield return new OperatorBase("Divide", "divide two numeric values", ScampsDataType.Numeric, StandardOrMacroOperator.Standard, new OperatorArg[] { new OperatorArg("arg1", ScampsDataType.Numeric), new OperatorArg("arg2", ScampsDataType.Numeric) });
                yield return new OperatorBase("Power", "raise the first value to the power of the second", ScampsDataType.Numeric, StandardOrMacroOperator.Standard, new OperatorArg[] { new OperatorArg("arg1", ScampsDataType.Numeric), new OperatorArg("arg2", ScampsDataType.Numeric) });
                yield return new OperatorBase("Or", "logical or of two boolean values", ScampsDataType.Bool, StandardOrMacroOperator.Standard, new OperatorArg[] { new OperatorArg("arg1", ScampsDataType.Bool), new OperatorArg("arg2", ScampsDataType.Bool) });
                yield return new OperatorBase("LessThan", "true of the first value is less than the second", ScampsDataType.Bool, StandardOrMacroOperator.Standard, new OperatorArg[] { new OperatorArg("arg1", ScampsDataType.Numeric), new OperatorArg("arg2", ScampsDataType.Numeric) });
                yield return new OperatorBase("GreaterThan", "true of the first value is greater than the second", ScampsDataType.Bool, StandardOrMacroOperator.Standard, new OperatorArg[] { new OperatorArg("arg1", ScampsDataType.Numeric), new OperatorArg("arg2", ScampsDataType.Numeric) });
                yield return new OperatorBase("Equals", "true if the values are equal", ScampsDataType.Bool, StandardOrMacroOperator.Macro, new OperatorArg[] { new OperatorArg("arg1", ScampsDataType.Numeric), new OperatorArg("arg2", ScampsDataType.Numeric) });
                yield return new OperatorBase("Average", "the average of a group of numeric values", ScampsDataType.Numeric, StandardOrMacroOperator.Macro, new OperatorArg[] { new OperatorArg("arg1", ScampsDataType.Numeric), new OperatorArg("arg2", ScampsDataType.Numeric) });
                yield return new OperatorBase("Case", "adding two numeric values", ScampsDataType.Numeric, StandardOrMacroOperator.Macro, new OperatorArg[] { new OperatorArg("arg1", ScampsDataType.Numeric), new OperatorArg("arg2", ScampsDataType.Numeric) });
                yield return new OperatorBase("DateDiff", "the difference between two date/time values", ScampsDataType.Timespan, StandardOrMacroOperator.Macro, new OperatorArg[] { new OperatorArg("arg1", ScampsDataType.Date), new OperatorArg("arg2", ScampsDataType.Date) });
                yield return new OperatorBase("EllipseArea", "adding two numeric values", ScampsDataType.Numeric, StandardOrMacroOperator.Macro, new OperatorArg[] { new OperatorArg("arg1", ScampsDataType.Numeric), new OperatorArg("arg2", ScampsDataType.Numeric) });
                yield return new OperatorBase("HasValue", "true if the element was assigned a value", ScampsDataType.Numeric, StandardOrMacroOperator.Macro, new OperatorArg[] { new OperatorArg("arg1", ScampsDataType.Numeric), new OperatorArg("arg2", ScampsDataType.Numeric) });
                yield return new OperatorBase("IfNull", "true if the element was not assigned a value", ScampsDataType.Numeric, StandardOrMacroOperator.Macro, new OperatorArg[] { new OperatorArg("arg1", ScampsDataType.Numeric), new OperatorArg("arg2", ScampsDataType.Numeric) });
                yield return new OperatorBase("MakeDate", "I don't know what this means", ScampsDataType.Date, StandardOrMacroOperator.Macro, new OperatorArg[] { new OperatorArg("arg1", ScampsDataType.Date), new OperatorArg("arg2", ScampsDataType.Date) });
                yield return new OperatorBase("Range", "I don't know what this means", ScampsDataType.Numeric, StandardOrMacroOperator.Macro, new OperatorArg[] { new OperatorArg("arg1", ScampsDataType.Numeric), new OperatorArg("arg2", ScampsDataType.Numeric) });
                yield return new OperatorBase("When", "I don't know what this means", ScampsDataType.Numeric, StandardOrMacroOperator.Macro, new OperatorArg[] { new OperatorArg("arg1", ScampsDataType.Numeric), new OperatorArg("arg2", ScampsDataType.Numeric) });
            }
        }
    }
}
