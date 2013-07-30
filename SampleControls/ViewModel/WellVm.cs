using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Windows;
using System.Windows.Media;
using GalaSoft.MvvmLight;
using SampleControls.Model;
using SampleControls.Model.Legend;
using SampleControls.View;

namespace SampleControls.ViewModel
{
    public class WellVm : ViewModelBase
    {
        public WellVm(int row, int column, SamplePlatePart samplePlatePart)
        {
            _column = column;
            _samplePlatePart = samplePlatePart;
            _row = row;

            _ringBrushManager = new BrushForPropertyManager(this);
            _ringBrushManager.BrushChanged += RingBrushManagerBrushChanged;

            RingBrushManager.AddLegendProperty(new BrushForProperty("IsScheduled", new BrushesForBool(Brushes.Black, Brushes.White)));
            RingBrushManager.ActiveProperty = "IsScheduled";

            _wellBrushManager = new BrushForPropertyManager(this);
            _wellBrushManager.BrushChanged += WellBrushManagerBrushChanged;

            WellBrushManager.AddLegendProperty(new BrushForProperty("HasSample", new BrushesForBool(Brushes.Gray, Brushes.Green)));
            WellBrushManager.ActiveProperty = "HasSample";
        }

        private string _wellText;
        public string WellText
        {
            get { return _wellText; }
            set
            {
                _wellText = value;
                RaisePropertyChanged("WellText");
            }
        }

        private readonly int _column;
        public int Column
        {
            get { return _column; }
        }

        private readonly int _row;
        public int Row
        {
            get { return _row; }
        }

        private bool _hasSample;
        public bool HasSample
        {
            get { return _hasSample; }
            set
            {
                _hasSample = value;
                RaisePropertyChanged("HasSample");
            }
        }

        private bool _isScheduled;
        public bool IsScheduled
        {
            get { return _isScheduled; }
            set
            {
                _isScheduled = value;
                RaisePropertyChanged("IsScheduled");
            }
        }

        private readonly Subject<WellVm> _onWellSelected = new Subject<WellVm>();
        public IObservable<WellVm> OnWellSelected { get { return _onWellSelected.AsObservable(); } }
        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                _onWellSelected.OnNext(this);
                RaisePropertyChanged("IsSelected");
            }
        }

        private Point _location;
        public Point Location
        {
            get { return _location; }
            set
            {
                _location = value;
                RaisePropertyChanged("LocationX");
                RaisePropertyChanged("LocationY");
            }
        }

        public double LocationX
        {
            get { return Location.X; }
        }

        public double LocationY
        {
            get { return Location.Y; }
        }

        private readonly SamplePlatePart _samplePlatePart;
        public SamplePlatePart SamplePlatePart
        {
            get { return _samplePlatePart; }
        }

        private double _sideLength;
        public double SideLength
        {
            get { return _sideLength; }
            set
            {
                _sideLength = value;
                RaisePropertyChanged("SideLength");
            }
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

        private ObservableCollection<KeyValuePairVm> _keyValuePairVms 
            = new ObservableCollection<KeyValuePairVm>();
        public ObservableCollection<KeyValuePairVm> KeyValuePairVms
        {
            get { return _keyValuePairVms; }
            set { _keyValuePairVms = value; RaisePropertyChanged("KeyValuePairVms"); }
        }

        private readonly BrushForPropertyManager _ringBrushManager;
        private BrushForPropertyManager RingBrushManager
        {
            get
            {
                return _ringBrushManager;
            }
        }

        void RingBrushManagerBrushChanged(object sender, BrushChangedArgs e)
        {
            RingBrush = e.Brush;
        }

        private readonly BrushForPropertyManager _wellBrushManager;
        private BrushForPropertyManager WellBrushManager
        {
            get
            {
                return _wellBrushManager;
            }
        }

        void WellBrushManagerBrushChanged(object sender, BrushChangedArgs e)
        {
            WellBrush = e.Brush;
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
