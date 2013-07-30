using MathUtils.Interval;

namespace MathUtils.Geometry
{
    public class LatticeCell<T>
    {
        public LatticeCell(RealInterval rowRange, RealInterval columnRange, int row, int column)
        {
            _columnRange = columnRange;
            _rowRange = rowRange;
            _column = column;
            _row = row;
        }

        public T Item { get; set; }

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

        private readonly RealInterval _columnRange;
        public RealInterval ColumnRange
        {
            get { return _columnRange; }
        }

        private readonly RealInterval _rowRange;
        public RealInterval RowRange
        {
            get { return _rowRange; }
        }

        //readonly List<CellPoint3D> _cellPoint3Ds = new List<CellPoint3D>();

        //public IEnumerable<CellPoint3D> CellCellPoint3Ds
        //{
        //    get { return _cellPoint3Ds; }
        //}

        //public void AddPoint3D(Point3D point3D)
        //{
        //    _cellPoint3Ds.Add( new CellPoint3D(point3D, this));
        //}

        public LatticeCell<T> North { get; set; }
        public LatticeCell<T> South { get; set; }
        public LatticeCell<T> East { get; set; }
        public LatticeCell<T> West { get; set; }
    }
}
