using EquationEditor.Models.Equation;

namespace EquationEditor.Models.Elements
{
    public class Element : IEquationPart
    {
        public Element(ScampsDataType scampsDataType)
        {
            _scampsDataType = scampsDataType;
        }

        private string _elementCode;
        public string ElementCode
        {
            get { return _elementCode; }
            set
            {
                _elementCode = value;
            }
        }

        private string _elementDescr;
        public string ElementDescr
        {
            get { return _elementDescr; }
            set
            {
                _elementDescr = value;
            }
        }

        public EquationNodeType EquationNodeType { get {return EquationNodeType.Element;} }

        private readonly ScampsDataType _scampsDataType;
        public ScampsDataType ScampsDataType
        {
            get { return _scampsDataType; }
        }
    }
}
