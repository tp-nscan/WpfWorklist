using System;
using EquationEditor.Models.Equation;

namespace EquationEditor.ViewModels.Operator
{
    public static class OperatorUtils
    {
        public static string ToResourceName(this ScampsDataType scampsDataType)
        {
            switch (scampsDataType)
            {
                case ScampsDataType.Bool:
                    return "arg.bool";
                case ScampsDataType.Date:
                    return "arg.datetime";
                case ScampsDataType.Numeric:
                    return "arg.numeric";
                case ScampsDataType.Time:
                    return "arg.dateTime";
                case ScampsDataType.Timespan:
                    return "arg.timeSpan";
                case ScampsDataType.Any:
                    return "arg.any";
                default:
                    throw new Exception(string.Format("{0} not handled", scampsDataType));
            }
        }
    }
}
