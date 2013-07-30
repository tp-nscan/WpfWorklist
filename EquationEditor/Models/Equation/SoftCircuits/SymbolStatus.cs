using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EquationEditor.Models.Equation.SoftCircuits
{
    public enum SymbolStatus
    {
        OK,
        UndefinedSymbol,
    }


    // ProcessSymbol arguments
    public class SymbolEventArgs : EventArgs
    {
        public string Name { get; set; }
        public double Result { get; set; }
        public SymbolStatus Status { get; set; }
    }

    public enum FunctionStatus
    {
        OK,
        UndefinedFunction,
        WrongParameterCount,
    }

    // ProcessFunction arguments
    public class FunctionEventArgs : EventArgs
    {
        public string Name { get; set; }
        public List<double> Parameters { get; set; }
        public double Result { get; set; }
        public FunctionStatus Status { get; set; }
    }
}
