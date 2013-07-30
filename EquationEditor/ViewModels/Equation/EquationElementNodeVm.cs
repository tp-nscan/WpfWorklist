using EquationEditor.Models.Equation;

namespace EquationEditor.ViewModels.Equation
{
    public class EquationElementNodeVm : EquationNodeBaseVm
    {
        public EquationElementNodeVm(double value, string description, string name, string contentTemplateName, 
               ScampsDataType allowedScampsDataType) 
            : base(name, EquationNodeVmType.Element, contentTemplateName, allowedScampsDataType)
        {
            _value = value;
            _description = description;
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }

        private readonly double _value;

        public double Value
        {
            get { return _value; }
        }

        public override bool CanAddChild(IEquationPart node)
        {
            return false;
        }
    }
}
