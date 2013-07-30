using System.Collections.Generic;

namespace EquationEditor.Models.Elements
{
    public interface IElementRepository
    {
        IEnumerable<Element> Elements { get; }
    }

}