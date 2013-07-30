namespace EquationEditor.Models.Equation
{
    public class EquationBase
    {
        public EquationBase(string name, ScampsDataType scampsDataType, string formulaText)
        {
            _name = name;
            _scampsDataType = scampsDataType;
            _formulaText = formulaText;
        }

        private readonly ScampsDataType _scampsDataType;
        public ScampsDataType ScampsDataType
        {
            get { return _scampsDataType; }
        }

        private readonly string _formulaText;
        public string FormulaText
        {
            get { return _formulaText; }
        }

        private readonly string _name;
        public string Name
        {
            get { return _name; }
        }

        public bool IsComplete
        {
            get { throw new System.NotImplementedException(); }
            set { throw new System.NotImplementedException(); }
        }
    }
}
