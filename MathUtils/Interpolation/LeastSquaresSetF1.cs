using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace MathUtils.Interpolation
{
    public class LeastSquaresSetF1 : IList<Point>, ILeastSquaresSet
    {
       private readonly List<Point> _internalList = new List<Point>();

        public LeastSquaresSetF1(IEnumerable<Point> points)
        {
            ResetValues();

            foreach (var point in points)
            {
                var count = 5.5/point.X;
                for (int i = 0; i < count; i++)
                {
                    Add(point);
                }
            }
        }

        public int Count { get { return _internalList.Count; } }

        public bool IsReadOnly { get { return false; } }

        private double _maxX = Double.NegativeInfinity;
        public double XMax { get { return _internalList[XMaxIndex].X; } }
        private double _minX = Double.PositiveInfinity;
        public double XMin { get { return _internalList[XMinIndex].X; } }
        public int XMaxIndex { get; protected set; }
        public int XMinIndex { get; protected set; }

        private double _maxY = Double.NegativeInfinity;
        public double YMax { get { return _internalList[YMaxIndex].Y; } }
        private double _minY = Double.PositiveInfinity;
        public double YMin { get { return _internalList[YMinIndex].Y; } }
        public int YMaxIndex { get; protected set; }
        public int YMinIndex { get; protected set; }

        public double XMean { get { return XSum / Count; } }
        public double YMean { get { return YSum / Count; } }

        public double RSquare { get; protected set; }

        public Point RegressionPoint0 { get; protected set; }
        public Point RegressionPointN { get; protected set; }

        public double Slope { get; protected set; }

        public double XSum { get; set; }
        public double YSum { get; set; }
        public double XSquaredSum { get; set; }
        public double XyProductSum { get; set; }

        public double XIntercept { get { return -YIntercept / Slope; } }
        public double YIntercept { get; protected set; }


        public Point this[int index]
        {
            get { return _internalList[index]; }
            set
            {
                var p = value;
                var old = _internalList[index];
                _internalList[index] = p;

                ComputeSums(old, SumMode.Subtract);
                ComputeSums(p, SumMode.Add);
                ComputeMinAndMax();
                ComputeSlopeAndYIntercept();
            }
        }

        public void Add(double x, double y)
        {
            Add(new Point(x, y));
        }

        public void Add(Point p)
        {
            _internalList.Add(p);
            RSquare = double.NaN;

            ComputeSums(p, SumMode.Add);
            ComputeMinAndMax(Count - 1, p);
            ComputeSlopeAndYIntercept();
        }

        public void Clear()
        {
            _internalList.Clear();
            ResetValues();
        }

        public void ComputeSlopeAndYIntercept()
        {
            double delta = Count * XSquaredSum - Math.Pow(XSum, 2.0);
            YIntercept = (1.0 / delta) * (XSquaredSum * YSum - XSum * XyProductSum);
            Slope = (1.0 / delta) * (Count * XyProductSum - XSum * YSum);

            RegressionPoint0 = new Point(XMin, Slope * XMin + YIntercept);
            RegressionPointN = new Point(XMax, Slope * XMax + YIntercept);
        } 

        public double ComputeRSquared()
        {
            var sStot = _internalList.Sum(p => Math.Pow(p.Y - YMean, 2.0));
            var sSerr = _internalList.Sum(p => Math.Pow(p.Y - (Slope * p.X + YIntercept), 2.0));
            RSquare = 1.0 - sSerr / sStot;
            return RSquare;
        }

        public bool Contains(Point p)
        {
            return _internalList.Contains(p);
        }

        public void CopyTo(Point[] points, int index)
        {
            _internalList.CopyTo(points, index);
        }

        public IEnumerator<Point> GetEnumerator()
        {
            return _internalList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _internalList.GetEnumerator();
        }

        public int IndexOf(Point p)
        {
            return _internalList.IndexOf(p);
        }

        public void Insert(int index, Point p)
        {
            _internalList.Insert(index, p);
            RSquare = double.NaN;

            ComputeSums(p, SumMode.Add);
            ComputeMinAndMax();
            ComputeSlopeAndYIntercept();
        }

        public bool Remove(Point p)
        {
            var success = _internalList.Remove(p);
            if (success)
            {
                RSquare = double.NaN;
                ComputeSums(p, SumMode.Subtract);
                ComputeMinAndMax();
                ComputeSlopeAndYIntercept();
            }
            return success;
        }

        public void RemoveAt(int index)
        {
            var old = _internalList[index];
            _internalList.RemoveAt(index);
            RSquare = double.NaN;

            ComputeSums(old, SumMode.Subtract);
            ComputeMinAndMax();
            ComputeSlopeAndYIntercept();
        }

        protected void ComputeMinAndMax()
        { //methods that call this, Insert, 
            ResetMinAndMax();

            for (int i = 0; i < _internalList.Count; ++i)
                ComputeMinAndMax(i, _internalList[i]);
        }

        protected void ComputeMinAndMax(int index, Point newPoint)
        {
            if (newPoint.X <= _minX)
            {
                _minX = newPoint.X;
                XMinIndex = index;
            }

            if (newPoint.X >= _maxX)
            {
                _maxX = newPoint.X;
                XMaxIndex = index;
            }

            if (newPoint.Y <= _minY)
            {
                _minY = newPoint.Y;
                YMinIndex = index;
            }

            if (newPoint.Y >= _maxY)
            {
                _maxY = newPoint.Y;
                YMaxIndex = index;
            }
        }

        protected enum SumMode { Add, Subtract };
        protected void ComputeSums(Point p, SumMode mode)
        {
            if (mode == SumMode.Add)
            {
                XSum += p.X;
                YSum += p.Y;
                XSquaredSum += Math.Pow(p.X, 2.0);
                XyProductSum += (p.X * p.Y);
            }
            else if (mode == SumMode.Subtract)
            {
                XSum -= p.X;
                YSum -= p.Y;
                XSquaredSum -= Math.Pow(p.X, 2.0);
                XyProductSum -= (p.X * p.Y);
            }
        }

        protected void ResetMinAndMax()
        {
            _maxX = double.NegativeInfinity;
            _maxY = double.NegativeInfinity;
            _minX = double.PositiveInfinity;
            _minY = double.PositiveInfinity;
        }

        protected void ResetValues()
        {
            ResetMinAndMax();

            RegressionPoint0 = new Point();
            RegressionPointN = new Point();

            RSquare = double.NaN;

            Slope = double.NaN;
            YIntercept = double.NaN;

            XSum = 0.0;
            YSum = 0.0;
            XSquaredSum = 0.0;
            XyProductSum = 0.0;

            XMaxIndex = -1;
            YMaxIndex = -1;
            XMinIndex = -1;
            YMinIndex = -1;
        }
    } 
}