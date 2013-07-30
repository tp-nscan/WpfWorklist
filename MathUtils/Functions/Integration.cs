using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using MathUtils.Interval;

namespace MathUtils.Functions
{
    public static class Integration
    {
        public static double BlockIntegral(this Func<double, double> f, RealInterval domain, int partitions)
        {
            if (f == null) throw new ArgumentNullException("f");
            if (domain == null) throw new ArgumentNullException("domain");
            if (partitions < 1)
            {
                throw new ArgumentException(String.Format("Expected at least 1 partition, found {0} in {1}",
                    partitions, "Integration.BlockStyle"));
            }

            double curTotal = 0;
            var interval = domain.Span() / partitions;
            for (var x = domain.Min + interval; x <= domain.Max; x += interval)
            {
                curTotal += f(x) * interval;
            }
            return curTotal;
        }

        public static double TrapezoidIntegral(this Func<double, double> f, RealInterval domain, int partitions)
        {
            if (f == null) throw new ArgumentNullException("f");
            if (domain == null) throw new ArgumentNullException("domain");
            if (partitions < 1)
            {
                throw new ArgumentException(String.Format("Expected at least 1 partition, found {0} in {1}",
                    partitions, "Integration.BlockStyle"));
            }

            double curTotal = 0;
            var interval = domain.Span() / partitions;
            for (var x = domain.Min; x < domain.Max; x += interval)
            {
                curTotal += (f(x) + f(x + interval)) * interval / 2;
            }
            return curTotal;
        }

        public static double TrapezoidIntegral(this IEnumerable<Point> points)
        {
            double retVal = 0;
            var pointList = points.ToList();

            if( pointList.Count<1)
            {
                return double.NaN;
            }

            for (int i = 1; i < pointList.Count(); i++ )
            {
                retVal += (pointList[i].Y + pointList[i - 1].Y) * (pointList[i].X - pointList[i - 1].X) * 0.5;
            }
            
            return retVal;
        }
    }
}
