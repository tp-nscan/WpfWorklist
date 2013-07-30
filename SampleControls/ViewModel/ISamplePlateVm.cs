using System.Collections.ObjectModel;

namespace SampleControls.ViewModel
{
    public interface ISamplePlateVm
    {
        int ColCount { get; }
        ObservableCollection<WellVm> WellVms { get; set; }
        string PlateName { get; set; }
        int RowCount { get; }
    }
}