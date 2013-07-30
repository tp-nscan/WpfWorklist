using System.Collections.Generic;
using EquationEditor.Models.Elements;
using EquationEditor.Models.Equation;

namespace EquationEditor.DesignData
{
    public class DesignElementRepository : IElementRepository
    {
        public DesignElementRepository()
        {
            for (var i = 0; i < 1750; i++)
            {
                _elements.Add(new Element(ScampsDataType.Numeric) { ElementCode = "AA01", ElementDescr = "Dude, this is a good element" });
                _elements.Add(new Element(ScampsDataType.Date) { ElementCode = "AB01", ElementDescr = "Wassup homie, this is another good element" });
                _elements.Add(new Element(ScampsDataType.Time) { ElementCode = "AC01", ElementDescr = "Golly gee, Mitt Romney loves this element" });
                _elements.Add(new Element(ScampsDataType.Bool) { ElementCode = "AD01", ElementDescr = "Oooh yaaah, The Michelle Bachman element " });
                _elements.Add(new Element(ScampsDataType.Timespan) { ElementCode = "BA01", ElementDescr = "999, the Herman Cain elements " });
                _elements.Add(new Element(ScampsDataType.Numeric) { ElementCode = "CA01", ElementDescr = "You betchya! Presenting the Sarah Palin element" });
                _elements.Add(new Element(ScampsDataType.Numeric) { ElementCode = "CD01", ElementDescr = "Food Stamps anyone? The Newt Gingrich element" });
                _elements.Add(new Element(ScampsDataType.Numeric) { ElementCode = "CD02", ElementDescr = "Crazy Uncle time! The Ron Paul element" });
            }
        }

        readonly List<Element> _elements = new List<Element>();
        public IEnumerable<Element> Elements
        {
            get { return _elements; }
        }

    }
}
