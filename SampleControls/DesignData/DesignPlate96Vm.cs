using System.Globalization;
using Lab.Model.Containers.Plate;
using SampleControls.ViewModel;

namespace SampleControls.DesignData
{
    public class DesignPlate96Vm : SamplePlateVm
    {
        public DesignPlate96Vm() : base(8,12)
        {
            foreach (var wellVm in WellVms)
            {
                switch (wellVm.SamplePlatePart)
                {
                    case View.SamplePlatePart.ColumnHeader:
                        wellVm.WellText = (wellVm.Column +1).ToString(CultureInfo.InvariantCulture);
                        break;
                    case View.SamplePlatePart.RowHeader:
                        wellVm.WellText = wellVm.Row.RowLabel();
                        break;
                    case View.SamplePlatePart.Well:
                        wellVm.WellText = string.Format("r{0}c{1}", wellVm.Row, wellVm.Column);
                        break;
                }
                wellVm.HasSample = (wellVm.Row % 2) == 0;
                wellVm.IsScheduled = wellVm.Column != 2;
                //wellVm.HasSample = wellVm.Column == 2;
                //wellVm.RingBrush = new SolidColorBrush(Colors.DarkOrchid);
                //wellVm.WellBrush = new SolidColorBrush(Colors.DarkSeaGreen);
            }
        }
    }
}
