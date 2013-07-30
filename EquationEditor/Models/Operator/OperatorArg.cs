using EquationEditor.Models.Equation;

namespace EquationEditor.Models.Operator
{
    public class OperatorArg
    {
        private readonly string _name;
        private readonly ScampsDataType _scampsDataType;

        public OperatorArg(string name, ScampsDataType scampsDataType)
        {
            _name = name;
            _scampsDataType = scampsDataType;
        }

        public string Name
        {
            get { return _name; }
        }

        public ScampsDataType ScampsDataType
        {
            get { return _scampsDataType; }
        }
    }
}
