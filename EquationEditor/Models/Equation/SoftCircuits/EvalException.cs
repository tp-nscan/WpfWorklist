using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EquationEditor.Models.Equation.SoftCircuits
{
    /// <summary>
    /// Custom exception for evaluation errors
    /// </summary>
    public class EvalException : Exception
    {
        /// <summary>
        /// Zero-base position in expression where exception occurred
        /// </summary>
        public int Column { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Message that describes this exception</param>
        /// <param name="position">Position within expression where exception occurred</param>
        public EvalException(string message, int position)
            : base(message)
        {
            Column = position;
        }

        /// <summary>
        /// Gets the message associated with this exception
        /// </summary>
        public override string Message
        {
            get
            {
                return String.Format("{0} (column {1})", base.Message, Column + 1);
            }
        }
    }


}
