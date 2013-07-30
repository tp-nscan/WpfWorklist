using MathUtils.Interval;

namespace MathUtils.Map
{
    public class LinearRangeMap : OrderedRangeMap
    {
        public LinearRangeMap(RealInterval realInterval, int partitionCount) 
            : base(realInterval, partitionCount)
        {

        }

        protected override void AddPartitions()
        {
            if (PartitionCount > 0)
            {
                double interval = RealInterval.Span() / (PartitionCount + 1);

                for (var i = 0; i < PartitionCount; i++)
                {
                    PartitioningOfRealInterval.AddPartitionPoint(RealInterval.Min + interval * i, true);
                }
            }

            base.AddPartitions();
        }

    }
}
