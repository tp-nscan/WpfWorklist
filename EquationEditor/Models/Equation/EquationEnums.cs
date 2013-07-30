namespace EquationEditor.Models.Equation
{

    public enum EquationNodeType
    {
        Element,
        Operator,
        Constant
        //,
        //Picklist,
        //FormulaRoot,
        //EmptyOperatorChild,
        //EmptyMacroChild
    }

    //public enum OperatorType
    //{
    //    Standard,
    //    Macro
    //}

    //public enum StandardOperatorType
    //{
    //    Add,
    //    Subtract,
    //    Multiply,
    //    Divide,
    //    Power,
    //    Or,
    //    And,
    //    LessThan,
    //    GreaterThan,
    //    Equals
    //}

    //public static class EnumConverters
    //{
    //    public static string ToSymbol(this StandardOperatorType operatorType)
    //    {
    //        switch (operatorType)
    //        {
    //            case StandardOperatorType.Add:
    //                return "+";
    //            case StandardOperatorType.Subtract:
    //                return "-";
    //            case StandardOperatorType.Multiply:
    //                return "*";
    //            case StandardOperatorType.Divide:
    //                return "/";
    //            case StandardOperatorType.Power:
    //                return "^";
    //            case StandardOperatorType.Or:
    //                return "OR";
    //            case StandardOperatorType.And:
    //                return "AND";
    //            case StandardOperatorType.LessThan:
    //                return "<";
    //            case StandardOperatorType.GreaterThan:
    //                return ">";
    //            case StandardOperatorType.Equals:
    //                return "=";
    //            default:
    //                throw new ArgumentException(String.Format("operator {0} not handled in EquationEditor.EnumConverters", operatorType));
    //        }
    //    }
    //}


}
