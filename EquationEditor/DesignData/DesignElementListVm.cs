using EquationEditor.ViewModels.Elements;

namespace EquationEditor.DesignData
{
    public class DesignElementListVm : ElementListVm
    {
        public DesignElementListVm() : base(new DesignElementRepository())
        {
        }
    }
}
