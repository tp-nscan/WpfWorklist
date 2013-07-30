using System;

namespace EquationEditor.Models.Equation
{
    [Flags]
    public enum ScampsDataType
    {
        None = 0,
        Bool = 1,
        Date = 2,
        Numeric = 4,
        Time = 8,
        Timespan = 16,
        Any = Bool | Date | Numeric | Time | Timespan
    }

    public static class ScampsDataTypeEx
    {
        public static bool AcceptsChildrenOfDataType(this ScampsDataType parent, ScampsDataType child)
        {
            return (parent & child) == child;
        }
    }
}
