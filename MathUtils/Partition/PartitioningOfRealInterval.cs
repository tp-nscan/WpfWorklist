using System;
using System.Linq;
using System.Collections.Generic;
using MathUtils.Interval;

namespace MathUtils.Partition
{
    public class PartitioningOfRealInterval
    {
        public PartitioningOfRealInterval(RealInterval containingRange, bool isMinValueFixed, bool isMaxValueFixed)
        {
            MinPartitionPoint = new PartitionPoint(containingRange.Min, isMinValueFixed, null, null);
            AddPartitionPoint(containingRange.Max, isMaxValueFixed);
        }

        private IPartitionPoint MinPartitionPoint { get; set; }

        private RealInterval _realInterval;
        public RealInterval RealInterval
        {
            get
            {
                if(_realInterval == null)
                {
                    if (MinPartitionPoint==null)
                    {
                        return RealInterval.Empty;
                    }
                    _realInterval = new RealInterval
                        (
                            MinPartitionPoint.Value, 
                            PartitionPointsInOrder.Max(T => T.Value)
                        );
                }
                return _realInterval;
            }
        }

        public IEnumerable<IPartitionPoint> PartitionPointsInOrder
        {
            get
            {
                var curPartitionPoint = MinPartitionPoint;

                while (curPartitionPoint !=null)
                {
                    yield return curPartitionPoint;
                    curPartitionPoint = curPartitionPoint.NeighborMax;
                }
            }
        }

        public void AddPartitionPoint(double pointValue, bool isFixed)
        {
            _realInterval = null;

            if(pointValue < MinPartitionPoint.Value)
            {
                var nmpp = new PartitionPoint(pointValue, isFixed, null, MinPartitionPoint)
                               {NeighborMax = MinPartitionPoint};
                MinPartitionPoint = nmpp;
                return;
            }

            var neighborMin = MinPartitionPoint;
            foreach (var partitionPoint in PartitionPointsInOrder.OrderBy(T=>T.Value))
            {
                if(partitionPoint.Value < pointValue)
                {
                    neighborMin = partitionPoint;
                }
                else
                {
                    break;
                }
            }

            var npp = new PartitionPoint(pointValue, isFixed, neighborMin, neighborMin.NeighborMax);

            if (neighborMin.NeighborMax != null)
            {
                neighborMin.NeighborMax.NeighborMin = npp;
            }

            neighborMin.NeighborMax = npp;
        }

        public void RemovePartitionPoint(double pointValue)
        {
            _realInterval = null;

            if (Math.Abs(MinPartitionPoint.Value - pointValue) < double.Epsilon)
            {
                MinPartitionPoint = MinPartitionPoint.NeighborMax;
                if(MinPartitionPoint != null)
                {
                    MinPartitionPoint.NeighborMin = null;
                }
                return;
            }
            var pp = PartitionPointsInOrder.SingleOrDefault(T => Math.Abs(T.Value - pointValue) < double.Epsilon);
            if (pp == null)
            {
                return;
            }
            if(pp.NeighborMin != null)
            {
                pp.NeighborMin.NeighborMax = pp.NeighborMax;
            }
            if(pp.NeighborMax != null)
            {
                pp.NeighborMax.NeighborMin = pp.NeighborMin;
            }
        }
    }

}
