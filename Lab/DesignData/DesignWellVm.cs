using System.Globalization;
using System.Windows.Media;
using Lab.ViewModel;

namespace Lab.DesignData
{
    public class DesignWellVm : WellVm
    {
        public DesignWellVm(int i) : base(null)
        {
            WellText = "Cmpd_" + i.ToString(CultureInfo.InvariantCulture);
            CellBrush = new SolidColorBrush(Colors.AliceBlue);
            WellBrush = new SolidColorBrush(Colors.BurlyWood);
        }


        //public int Column { get; set; }

        //public int Row { get; set; }
    }
}
