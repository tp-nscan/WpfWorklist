using System.Windows;
using System.Windows.Controls;
using SampleControls.ViewModel;

namespace SampleControls.View
{
    public class SamplePlatePartsItemContainerStyleSelector : StyleSelector
    {
        public Style WellStyle { get; set; }
        public Style ColumnHeaderStyle { get; set; }
        public Style RowHeaderStyle { get; set; }

        public override Style SelectStyle(object item, DependencyObject container)
        {
            var wellVm = item as WellVm;

            if(wellVm==null)
            {
                return null;
            }

            switch (wellVm.SamplePlatePart)
            {
                case SamplePlatePart.ColumnHeader:
                    return ColumnHeaderStyle;
                case SamplePlatePart.RowHeader:
                    return RowHeaderStyle;
                case SamplePlatePart.Well:
                    return WellStyle;
                default:
                    return null;
            }


        }
    }
}
