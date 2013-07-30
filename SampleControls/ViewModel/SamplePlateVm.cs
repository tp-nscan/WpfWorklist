using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using GalaSoft.MvvmLight;
using SampleControls.View;

namespace SampleControls.ViewModel
{
    public class SamplePlateVm : ViewModelBase, ISamplePlateVm
    {
        private readonly Subject<WellVm> _onWellSelected = new Subject<WellVm>();
        public IObservable<WellVm> OnWellSelected { get { return _onWellSelected.AsObservable(); } }

        public static SamplePlateVm Default = new SamplePlateVm(8,12); 
        public SamplePlateVm(int rowCount, int colCount)
        {
            _rowCount = rowCount;
            _colCount = colCount;

            for(int i=0; i<RowCount; i++)
            {
                for(int j=0; j<ColCount; j++)
                {
                    var wellVm = new WellVm(i, j, SamplePlatePart.Well);
                    wellVm.OnWellSelected.Subscribe(WellVmWellSelected);
                    WellVms.Add(wellVm);
                }
            }

            for (int j = 0; j < ColCount; j++)
            {
                WellVms.Add(new WellVm(0, j, SamplePlatePart.ColumnHeader));
            }

            for (int i = 0; i < RowCount; i++)
            {
                WellVms.Add(new WellVm(i, 0, SamplePlatePart.RowHeader));
            }

        }

        void WellVmWellSelected(WellVm wellVm)
        {
            _onWellSelected.OnNext(wellVm);
        }

        private readonly int _colCount;
        public int ColCount
        {
            get { return _colCount; }
        }

        private ObservableCollection<WellVm> _wellVms = new ObservableCollection<WellVm>();
        public ObservableCollection<WellVm> WellVms
        {
            get { return _wellVms; }
            set { _wellVms = value; }
        }

        private string _plateName;
        public string PlateName
        {
            get { return _plateName; }
            set
            {
                _plateName = value;
                RaisePropertyChanged("PlateName");
            }
        }

        private readonly int _rowCount;
        public int RowCount
        {
            get { return _rowCount; }
        }
    }
}
