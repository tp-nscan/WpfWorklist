using System.Collections.ObjectModel;
using System.Windows.Media;
using GalaSoft.MvvmLight;
using Lab.Model.Containers.Plate;

namespace Lab.ViewModel
{
    public class WellVm : ViewModelBase
    {
        public WellVm(IWell well)
        {
            _well = well;
        }

        private readonly IWell _well;
        protected IWell Well
        {
            get { return _well; }
        }

        private string _wellText;
        public string WellText
        {
            get { return _wellText; }
            protected set { _wellText = value; RaisePropertyChanged("WellText"); }
        }

        private Brush _cellBrush;
        public Brush CellBrush
        {
            get { return _cellBrush; }
            set { _cellBrush = value; RaisePropertyChanged("CellBrush"); }
        }

        private Brush _backgroundBrush;
        public Brush BackgroundBrush
        {
            get { return _backgroundBrush; }
            set { _backgroundBrush = value; RaisePropertyChanged("BackgroundBrush"); }
        }

        private Brush _ringBrush;
        public Brush RingBrush
        {
            get { return _ringBrush; }
            set { _ringBrush = value; RaisePropertyChanged("RingBrush"); }
        }

        private Brush _wellBrush;
        public Brush WellBrush
        {
            get { return _wellBrush; }
            set { _wellBrush = value; RaisePropertyChanged("WellBrush"); }
        }

        private string _toolTip;
        public string ToolTip
        {
            get { return _toolTip; }
            set { _toolTip = value; RaisePropertyChanged("ToolTip"); }
        }

        private ObservableCollection<KeyValuePairVm> _keyValuePairVms = new ObservableCollection<KeyValuePairVm>();
        public ObservableCollection<KeyValuePairVm> KeyValuePairVms
        {
            get { return _keyValuePairVms; }
            set { _keyValuePairVms = value; RaisePropertyChanged("KeyValuePairVms"); }
        }
    }

    public class KeyValuePairVm : ViewModelBase
    {
        private string _key;
        public string Key
        {
            get { return _key; }
            set { _key = value; RaisePropertyChanged("Key"); }
        }

        private string _value;
        public string Value
        {
            get { return _value; }
            set { _value = value; RaisePropertyChanged("Value"); }
        }

        private Brush _valueBrush;
        public Brush ValueBrush
        {
            get { return _valueBrush; }
            set { _valueBrush = value; RaisePropertyChanged("ValueBrush"); }
        }
    }
}
