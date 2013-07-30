using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using Lab.ViewModel;

namespace Lab.DesignData
{
    public class DesignPlate96Vm : ViewModelBase
    {
        public DesignPlate96Vm()
        {
            for (int i = 0; i < 96; i++)
            {
                WellVms.Add(new DesignWellVm(i));
            }
        }

        private ObservableCollection<WellVm> _wellVms = new ObservableCollection<WellVm>();
        public ObservableCollection<WellVm> WellVms
        {
            get { return _wellVms; }
            set { _wellVms = value; }
        }

        public string PlateName
        {
            get { return "PlateName"; }
        }

    } 
}
