using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Windows;
using Utils;

namespace Lab.Model.Containers.Plate
{
    public class SamplePlate : ISamplePlate
    {
        public SamplePlate(SamplePlateSize samplePlateSize, string name)
        {
            _samplePlateSize = samplePlateSize;
            _name = name;
            InitWells();
        }

        void InitWells()
        {
            for (var i = 0; i < SamplePlateSize.RowCount(); i++)
            {
                for (var j = 0; j < SamplePlateSize.ColumnCount(); j++)
                {
                    _wells.Add(new Well(column: j, row: i) { SamplePlate = this });
                }
            }
        }

        public IEnumerable<IWell> Wells
        {
            get { return _wells; }
        }

        private readonly Subject<BeforePropertyChanged<INamedItem>> _onNameChanging
            = new Subject<BeforePropertyChanged<INamedItem>>();
        public IObservable<BeforePropertyChanged<INamedItem>> OnNameChanging { get { return _onNameChanging.AsObservable(); } }

        private readonly Subject<ISamplePlate> _onNameChanged
            = new Subject<ISamplePlate>();
        public IObservable<ISamplePlate> OnNameChanged { get { return _onNameChanged.AsObservable(); } }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                var bpc = new BeforePropertyChanged<INamedItem>(this, "Name", value);
                _onNameChanging.OnNext(bpc);
                if (bpc.Cancel)
                {
                    MessageBox.Show(string.Format(" The name {0} is taken by another plate in this group", value));
                    return;
                }
                _name = value;
                _onNameChanged.OnNext(this);
            }
        }


        private readonly SamplePlateSize _samplePlateSize;
        public SamplePlateSize SamplePlateSize
        {
            get { return _samplePlateSize; }
        }

        readonly List<IWell> _wells = new List<IWell>();

        public IWell Well(int row, int column)
        {
            if ((row < 0) || (row >= SamplePlateSize.RowCount()))
            {
                throw new ArgumentException("row value " + row + " is out of bounds");
            }

            if ((column < 0) || (row >= SamplePlateSize.ColumnCount()))
            {
                throw new ArgumentException("column value " + column + " is out of bounds");
            }

            return _wells.SingleOrDefault(T => ((T.Row == row) && (T.Column == column)));
        }

        public IWell Well(IWellLoc wellLoc)
        {
            if (wellLoc.Row != null)
                if (wellLoc.Column != null) return Well((int)wellLoc.Row, (int)wellLoc.Column);

            throw new ArgumentException("wellLoc is not valid");
        }
    }
}
