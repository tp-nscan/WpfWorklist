using System;

namespace MathUtils.Functions
{
    public static class Gauss
    {
        public static double MeanZeroSigmaOne(double x)
        {
            return Math.Exp(-x*x/2.0) / Math.Sqrt(2*Math.PI);
        }
    }
}
