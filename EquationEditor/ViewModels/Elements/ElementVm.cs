using EquationEditor.Models;
using EquationEditor.Models.Elements;
using EquationEditor.Models.Equation;
using WpfUtils;

namespace EquationEditor.ViewModels.Elements
{
    public class ElementVm : ViewModelBase, IEquationPart
    {
        public ElementVm(Element element)
        {
            _element = element;
        }

        private readonly Element _element;
        private Element Element
        {
            get { return _element; }
        }

        public string ElementCode
        {
            get { return Element.ElementCode; }
            set
            {
                Element.ElementCode = value;
                OnPropertyChanged("ElementCode");
            }
        }

        public string ElementDescr
        {
            get { return Element.ElementDescr; }
            set
            {
                Element.ElementDescr = value;
                OnPropertyChanged("ElementDescr");
            }
        }


        public string DataType
        {
            get { return Element.ScampsDataType.ToString(); }
        }

        public EquationNodeType EquationNodeType { get { return Element.EquationNodeType; } }

        public ScampsDataType ScampsDataType { get { return Element.ScampsDataType; } }
    }
}
