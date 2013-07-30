using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace MathUtils.Distributions
{
    public static class Stats
    {
        public static void LeastSquaresFitLinear(IEnumerable<Point> pts, ref double M, ref double B)
        {
            var points = pts.ToList();
            var numPoints = points.Count();

            //Gives best fit of data to line Y = MC + B  
            int i;

            double x1 = 0.0;
            double y1 = 0.0;
            double xy = 0.0;
            double x2 = 0.0;

            for (i = 0; i < numPoints; i++)
            {
                x1 = x1 + points[i].X;
                y1 = y1 + points[i].Y;
                xy = xy + points[i].X * points[i].Y;
                x2 = x2 + points[i].X * points[i].X;
            }

            double J = (numPoints * x2) - (x1 * x1);
            if (J != 0.0)
            {
                M = (((double)numPoints * xy) - (x1 * y1)) / J;
                M = Math.Floor(1.0E3 * M + 0.5) / 1.0E3;
                B = ((y1 * x2) - (x1 * xy)) / J;
                B = Math.Floor(1.0E3 * B + 0.5) / 1.0E3;
            }
            else
            {
                M = 0;
                B = 0;
            }
        }

        public static double ArithmeticMean(IEnumerable<double> dat)
        {
            var data = dat.ToList();
            var items = data.Count();
            var sum = 0.0;

            for (var i = 0; i < items; i++)
            {
                sum += data[i];
            }

            var mean = sum / (double)items;
            return mean;
        }

        public static double Variance(IEnumerable<double> dat)
        {
            var data = dat.ToList();
            var deviation = new double[data.Count()];
            var mean = ArithmeticMean(data);

            for (var i = 0; i < data.Count(); i++)
            {
                deviation[i] = Math.Pow((data[i] - mean), 2);
            }

            var variance = Math.Sqrt(ArithmeticMean(deviation)) / (mean);

            return variance;
        }

        public static double VarianceReg(IEnumerable<double> dat)
        {
            var data = dat.ToList();
            var deviation = new double[data.Count()];
            var mean = ArithmeticMean(data);

            for (var i = 0; i < data.Count(); i++)
            {
                deviation[i] = Math.Pow((data[i] - mean), 2);
            }

            var variance = ArithmeticMean(deviation);

            return variance;
        }
    }
}
