using System;
using System.Collections.Generic;
using System.Windows;
using MathUtils.Interval;

namespace MathUtils.Functions
{
    public static class Sampler
    {
        public static IEnumerable<Point> UniformSample(this Func<double, double> f, RealInterval domain, int partitions)
        {
            if(partitions<2)
            {
                throw new ArgumentException(String.Format("Expected more than 1 partition, found {0} in {1}", 
                    partitions, "Sampler.Uniform"));
            }

            var interval = domain.Span()/partitions;
            for (var x = domain.Min; x < domain.Max; x+=interval)
            {
                yield return new Point(x, f(x));
            }
        }

        public static double BinRound(this double value, double binSize)
        {
            return Math.Round(value*binSize, 0)/binSize;
        }
    }
}
