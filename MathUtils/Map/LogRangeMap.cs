using System;
using MathUtils.Interval;

namespace MathUtils.Map
{
    public class LogRangeMap : OrderedRangeMap
    {
        public LogRangeMap(RealInterval realInterval, int partitionCount) 
            : base(realInterval, partitionCount)
        {

        }

        protected override void AddPartitions()
        {
            if (PartitionCount > 0)
            {
                double interval = Math.Log(RealInterval.Span()) / (PartitionCount + 1);

                for (var i = 0; i < PartitionCount; i++)
                {
                    PartitioningOfRealInterval.AddPartitionPoint(RealInterval.Min + Math.Exp(interval * i), true);
                }
            }

            base.AddPartitions();
        }

    }
}
