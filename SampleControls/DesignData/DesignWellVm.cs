using System.Windows.Media;
using SampleControls.View;
using SampleControls.ViewModel;

namespace SampleControls.DesignData
{
    public class DesignWellVm : WellVm
    {
        public DesignWellVm() : base(0, 0, SamplePlatePart.Well)
        {
            WellText = "A1";
            RingBrush = new SolidColorBrush(Colors.DarkOrange);
            WellBrush = new SolidColorBrush(Colors.DarkSeaGreen);
        }
    }
}
