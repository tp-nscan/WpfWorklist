using System;
using MathUtils.Interval;
using MathUtils.Partition;

namespace MathUtils.Map
{
    /// <summary>
    /// Breaks the real line into a finite number of intervals, indexed by their order,
    /// and provides a map of a number to the index of the interval that contains it.
    /// </summary>
    public abstract class OrderedRangeMap
    {
        protected OrderedRangeMap(RealInterval realInterval, int partitionCount)
        {
            _realInterval = realInterval;
            _partitionCount = partitionCount;
            _partitioningOfRealInterval = new PartitioningOfRealInterval(
                new RealInterval(Double.NegativeInfinity, Double.PositiveInfinity), true, true);
        }

        protected bool PartitionsAdded { get; private set; }
        protected virtual void AddPartitions()
        {
            PartitioningOfRealInterval.AddPartitionPoint(RealInterval.Min, true);
            PartitioningOfRealInterval.AddPartitionPoint(RealInterval.Max, true);
            PartitionsAdded = true;
        }

        public int IndexOfValue(double value)
        {
            if(! PartitionsAdded)
            {
                AddPartitions();
            }

            var index = 0;
            foreach (var partition in PartitioningOfRealInterval.PartitionPointsInOrder)
            {
                if(partition.RealInterval.Contains(value))
                {
                    return index;
                }
                index++;
            }
            return index;
        }

        private readonly PartitioningOfRealInterval _partitioningOfRealInterval;
        protected PartitioningOfRealInterval PartitioningOfRealInterval
        {
            get { return _partitioningOfRealInterval; }
        }

        private readonly RealInterval _realInterval;
        protected RealInterval RealInterval
        {
            get { return _realInterval; }
        }

        private readonly int _partitionCount;
        public int PartitionCount
        {
            get { return _partitionCount; }
        }
    }
}
