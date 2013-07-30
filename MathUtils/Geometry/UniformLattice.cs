using System.Collections.Generic;
using System.Linq;
using MathUtils.Interval;

namespace MathUtils.Geometry
{
    public class UniformLattice<T> where T : new()
    {
        public UniformLattice(RealInterval rowRange, RealInterval columnRange, int columnCount, int rowCount)
        {
            _columnRange = columnRange;
            _rowRange = rowRange;
            _columnCount = columnCount;
            _rowCount = rowCount;
            _xIntervals = ColumnRange.SplitToEvenIntervals(columnCount).ToList();

            //the y axis increases towards the top of the screen.
            _yIntervals = ColumnRange.SplitToEvenIntervals(rowCount).OrderBy(T => -T.Min).ToList();
            _latticeCells = new LatticeCell<T>[RowCount, ColumnCount];
            SetupLatticeCells();
        }

        void SetupLatticeCells()
        {
            for(var row=0; row< RowCount; row++)
            {
                for(var column=0; column<ColumnCount; column++)
                {
                    _latticeCells[row, column] = new LatticeCell<T>
                        (
                            _xIntervals[column], _yIntervals[row], row, column
                        ) 
                            { Item = new T() };
                }
            }

            for (var row = 0; row < RowCount; row++)
            {
                for (var column = 0; column < ColumnCount; column++)
                {
                    _latticeCells[row, column].North = (row == 0)                  ?  null :   _latticeCells[row - 1, column     ];
                    _latticeCells[row, column].South = (row == RowCount - 1)      ?  null :   _latticeCells[row + 1, column     ];
                    _latticeCells[row, column].East =  (column == ColumnCount - 1)   ?  null :   _latticeCells[row,     column + 1 ];
                    _latticeCells[row, column].West =  (column == 0)               ?  null :   _latticeCells[row,     column - 1 ];
                }
            }
        }

        public IEnumerable<T> LatticeCells
        {
            get
            {
                foreach (var latticeCell in _latticeCells)
                {
                    yield return latticeCell.Item;
                }
            }
        }

        readonly List<RealInterval> _xIntervals = new List<RealInterval>();

        readonly List<RealInterval> _yIntervals = new List<RealInterval>();

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

        private readonly LatticeCell<T>[,] _latticeCells;

        private readonly int _columnCount;
        public int ColumnCount
        {
            get { return _columnCount; }
        }

        private readonly int _rowCount;
        public int RowCount
        {
            get { return _rowCount; }
        }

        public LatticeCell<T> GetLatticeCell(double rowVal, double colVal)
        {
            if (!RowRange.Contains(rowVal))
            {
                return null;
            }

            if (!ColumnRange.Contains(colVal))
            {
                return null;
            }

            var xIndex = (int)((RowRange.Max - rowVal) * RowCount / RowRange.Span());
            var yIndex = (int)((colVal - ColumnRange.Min) * ColumnCount / ColumnRange.Span());

            return _latticeCells[xIndex, yIndex];
        }
    }
}
