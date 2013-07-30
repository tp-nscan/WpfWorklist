using System;
using MathUtils.Interval;

namespace MathUtils.Partition
{
    public interface IPartitionPoint
    {
        bool IsFixed { get; }
        double LowerBound { get; }
        IPartitionPoint NeighborMax { get; set; }
        IPartitionPoint NeighborMin { get; set; }
        RealInterval RealInterval { get; }
        double Value { get; set; }
        double UpperBound { get; }
    }

    public class PartitionPoint : IPartitionPoint
    {
        public PartitionPoint(double point, bool isFixed,
            IPartitionPoint neighborMin, IPartitionPoint neighborMax)
        {
            Value = point;
            IsFixed = isFixed;
            NeighborMin = neighborMin;
            NeighborMax = neighborMax;
        }

        public bool IsFixed { get; private set; }

        public double LowerBound
        {
            get
            {
                if (IsFixed)
                {
                    return Value;
                }
                if (NeighborMin == null)
                {
                    return double.NegativeInfinity;
                }
                return NeighborMin.Value;
            }
        }

        public IPartitionPoint NeighborMin { get; set; }

        public IPartitionPoint NeighborMax { get; set; }

        private double _value;

        public RealInterval RealInterval
        {
            get { throw new NotImplementedException(); }
        }

        public double Value
        {
            get { return _value; }
            set
            {
                if ( (value < LowerBound) || (value > UpperBound) )
                {
                    throw new ArgumentException(String.Format("{0} is out of bounds", value));
                }
                _value = value;
            }
        }

        public double UpperBound
        {
            get
            {
                if (IsFixed)
                {
                    return Value;
                }
                if (NeighborMax == null)
                {
                    return double.PositiveInfinity;
                }
                return NeighborMax.Value;
            }
        }
    }

    public static class PartitionPointExt
    {
        public static RealInterval ToRealInterval(this IPartitionPoint pp)
        {
            return new RealInterval(pp.LowerBound, pp.UpperBound);
        }
    }
}
